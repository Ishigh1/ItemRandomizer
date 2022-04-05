using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.WorldBuilding;

namespace ItemRandomizer;

public class RandomizedWorld : ModSystem
{
	public static Dictionary<int, int> Translator;
	public static Dictionary<int, int> ReverseTranslator;

	public override void NetSend(BinaryWriter writer)
	{
		writer.Write(Main.ActiveWorldFileData.SeedText);
	}

	public override void NetReceive(BinaryReader reader)
	{
		if (ReverseTranslator == null)
		{
			Main.ActiveWorldFileData.SetSeed(reader.ReadString());
			SetupTranslator();
			if (RandomizerSettings.Instance.Craft)
				TranslateRecipes();
		}
	}

	public override void OnWorldLoad()
	{
		if (Main.netMode != NetmodeID.MultiplayerClient)
		{
			SetupTranslator();
			if (RandomizerSettings.Instance.Craft)
				TranslateRecipes();
		}
	}

	public override void OnWorldUnload()
	{
		if (RandomizerSettings.Instance.Craft)
			UntranslateRecipes();
		Translator = null;
		ReverseTranslator = null;
	}

	public override void PreWorldGen()
	{
		SetupTranslator();
	}

	public static void SetupTranslator()
	{
		if (Translator != null) return;

		List<int> legalItems = new();
		Dictionary<int, List<int>> legalItemsForRarity = new();
		Item dummy = new();
		if (RandomizerSettings.Instance.Rarity)
			for (int i = -13; i < RarityLoader.RarityCount; i++)
				legalItemsForRarity.Add(i, new List<int>());

		Mod tModLoader = ModLoader.GetMod("ModLoader");
		for (int itemId = 1; itemId < ItemLoader.ItemCount; itemId++)
			if (!ItemID.Sets.Deprecated[itemId] && ItemLoader.GetItem(itemId)?.Mod != tModLoader &&
			    (!ItemID.Sets.IgnoresEncumberingStone[itemId] || RandomizerSettings.Instance.PickupItem) &&
			    (itemId != ItemID.Pwnhammer || RandomizerSettings.Instance.Pwnhammer))
				if (RandomizerSettings.Instance.Rarity)
				{
					dummy.SetDefaults(itemId);
					legalItemsForRarity[dummy.rare].Add(itemId);
				}
				else
				{
					legalItems.Add(itemId);
				}


		UnifiedRandom random = new(Main.ActiveWorldFileData.Seed);
		Translator = new Dictionary<int, int>();
		ReverseTranslator = new Dictionary<int, int>();

		if (RandomizerSettings.Instance.Rarity)
		{
			for (int i = -13; i < RarityLoader.RarityCount; i++)
			{
				List<int> items = legalItemsForRarity[i];
				List<int> availableItems = new(items);
				foreach (int itemId in items)
				{
					int location = random.Next(availableItems.Count);

					Translator.Add(itemId, availableItems[location]);
					ReverseTranslator.Add(availableItems[location], itemId);
					availableItems.RemoveAt(location);
				}
			}
		}
		else
		{
			List<int> availableItems = new(legalItems);
			foreach (int itemId in legalItems)
			{
				int location = random.Next(availableItems.Count);

				Translator.Add(itemId, availableItems[location]);
				ReverseTranslator.Add(availableItems[location], itemId);
				availableItems.RemoveAt(location);
			}
		}
	}

	public static void TranslateRecipes()
	{
		foreach (Recipe recipe in Main.recipe) TranslateItem(recipe.createItem);
		if (ModLoader.TryGetMod("RecursiveCraft", out Mod recursiveCraftMod))
			recursiveCraftMod.Call("DiscoverRecipes");
	}

	public static void UntranslateRecipes()
	{
		foreach (Recipe recipe in Main.recipe) UntranslateItem(recipe.createItem);
		if (ModLoader.TryGetMod("RecursiveCraft", out Mod recursiveCraftMod))
			recursiveCraftMod.Call("DiscoverRecipes");
	}

	public static void TranslateItem(Item item)
	{
		int previousType = item.type;
		if (Translator != null && previousType != ItemID.None)
		{
			int stack = item.stack;
			if (stack > 0)
			{
				int price = item.value;
				item.SetDefaults(Translator[previousType]);
				item.stack = stack;
				item.value = price;
			}
		}
	}

	public static void UntranslateItem(Item item)
	{
		if (ReverseTranslator != null && item.type != ItemID.None)
		{
			int stack = item.stack;
			if (stack > 0)
			{
				int price = item.value;
				item.SetDefaults(ReverseTranslator[item.type]);
				item.stack = stack;
				item.value = price;
			}
		}
	}

	public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
	{
		if (RandomizerSettings.Instance.WorldGen)
		{
			tasks.Add(new PassLegacy("Randomize Chests", RandomizeChests, 1));
			totalWeight += 1;
		}

		if (RandomizerSettings.Instance.UniquePowerCell)
		{
			tasks.Add(new PassLegacy("Add a cell", AddCell, 1));
			totalWeight += 1;
		}
	}

	public static void RandomizeChests(GenerationProgress progress, GameConfiguration configuration)
	{
		foreach (Item item in Main.chest.Where(chest => chest != null).SelectMany(chest => chest.item))
			TranslateItem(item);
	}

	private static void AddCell(GenerationProgress progress, GameConfiguration configuration)
	{
		HashSet<int> chests = new();
		for (int index = 0; index < Main.chest.Length; index++)
		{
			Chest chest = Main.chest[index];
			if (chest != null) chests.Add(index);
		}

		Chest cellContainer = Main.chest[chests.ElementAt(WorldGen.genRand.Next(chests.Count))];
		Item cell = cellContainer.item.First(item => item.IsAir);
		cell.SetDefaults(ItemID.LihzahrdPowerCell);
		cell.stack = 1;
	}
}