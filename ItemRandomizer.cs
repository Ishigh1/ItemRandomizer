using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using OnCommonCode = On.Terraria.GameContent.ItemDropRules.CommonCode;
using OnItem = On.Terraria.Item;
using OnPlayer = On.Terraria.Player;
using OnLang = On.Terraria.Lang;
using OnNPC = On.Terraria.NPC;
using OnWorldGen = On.Terraria.WorldGen;
using OnMain = On.Terraria.Main;

namespace ItemRandomizer;

public class ItemRandomizer : Mod
{
	public static bool ToTranslate;
	public static bool OnlyOnce;
	public static Dictionary<int, int> Dropables;

	public override void Load()
	{
		BaseEdit();
		DoabilityEdit();
	}

	private static void BaseEdit()
	{
		OnItem.SetDefaults_int_bool += (orig, self, type, check) =>
		{
			orig(self, type, check);
			if (ToTranslate)
			{
				ToTranslate = false;
				RandomizedWorld.TranslateItem(self);
				if (!OnlyOnce)
					ToTranslate = true;
			}
		};

		OnPlayer.QuickSpawnItem_IEntitySource_int_int += (orig, self, source, item, stack) =>
		{
			if (RandomizerSettings.Instance.Loot)
			{
				ToTranslate = true;
				int returnValue = orig(self, source, item, stack);
				ToTranslate = false;
				return returnValue;
			}
			else
			{
				return orig(self, source, item, stack);
			}
		};

		OnPlayer.OpenFishingCrate += (orig, self, type) =>
		{
			if (RandomizerSettings.Instance.Box)
			{
				ToTranslate = true;
				orig(self, type);
				ToTranslate = false;
			}
			else
			{
				orig(self, type);
			}
		};

		OnPlayer.OpenPresent += (orig, self, type) =>
		{
			if (RandomizerSettings.Instance.Box)
			{
				ToTranslate = true;
				orig(self, type);
				ToTranslate = false;
			}
			else
			{
				orig(self, type);
			}
		};

		OnCommonCode.DropItemFromNPC += (orig, npc, id, stack, scattered) =>
		{
			if (RandomizerSettings.Instance.Loot)
			{
				ToTranslate = true;
				orig(npc, id, stack, scattered);
				ToTranslate = false;
			}
			else
			{
				orig(npc, id, stack, scattered);
			}
		};

		OnCommonCode.DropItemLocalPerClientAndSetNPCMoneyTo0 += (orig, npc, id, stack, required) =>
		{
			if (RandomizerSettings.Instance.Loot)
			{
				ToTranslate = true;
				orig(npc, id, stack, required);
				ToTranslate = false;
			}
			else
			{
				orig(npc, id, stack, required);
			}
		};

		OnLang.BartenderChat += (orig, npc) =>
		{
			if (RandomizerSettings.Instance.Shop)
			{
				ToTranslate = true;
				OnlyOnce = true;
				return orig(npc);
			}
			else
			{
				return orig(npc);
			}
		};

		OnNPC.AI_87_BigMimic_ShootItem += (orig, self, id) =>
		{
			if (RandomizerSettings.Instance.Loot)
			{
				ToTranslate = true;
				orig(self, id);
				ToTranslate = false;
			}
			else
			{
				orig(self, id);
			}
		};

		OnNPC.CountKillForBannersAndDropThem += (orig, self) =>
		{
			if (RandomizerSettings.Instance.Loot)
			{
				ToTranslate = true;
				orig(self);
				ToTranslate = false;
			}
			else
			{
				orig(self);
			}
		};

		OnNPC.DropItemInstanced += (orig, self, position, size, type, stack, required) =>
		{
			if (RandomizerSettings.Instance.Loot)
			{
				ToTranslate = true;
				orig(self, position, size, type, stack, required);
				ToTranslate = false;
			}
			else
			{
				orig(self, position, size, type, stack, required);
			}
		};

		OnWorldGen.KillTile_GetItemDrops += (OnWorldGen.orig_KillTile_GetItemDrops orig, int i, int i1, Tile cache,
			out int item, out int stack, out int secondaryItem, out int itemStack, bool drops) =>
		{
			orig(i, i1, cache, out item, out stack, out secondaryItem, out itemStack, drops);
			if (RandomizerSettings.Instance.WorldGen)
			{
				if (item != 0 && Dropables[item] != cache.TileType)
					item = RandomizedWorld.Translator[item];
				if (secondaryItem != 0 && Dropables[secondaryItem] != cache.TileType)
					secondaryItem = RandomizedWorld.Translator[secondaryItem];
			}
		};

		OnWorldGen.ShakeTree += (orig, i, i1) =>
		{
			if (RandomizerSettings.Instance.Terrain)
			{
				ToTranslate = true;
				orig(i, i1);
				ToTranslate = false;
			}
			else
			{
				orig(i, i1);
			}
		};

		OnWorldGen.CheckPot += (orig, i, i1, type) =>
		{
			if (RandomizerSettings.Instance.Terrain)
			{
				bool secondPot = ToTranslate;
				ToTranslate = true;
				orig(i, i1, type);
				ToTranslate = secondPot;
			}
			else
			{
				orig(i, i1, type);
			}
		};

		OnWorldGen.CheckOrb += (orig, i, i1, type) =>
		{
			if (RandomizerSettings.Instance.Terrain)
			{
				bool secondPot = ToTranslate;
				ToTranslate = true;
				orig(i, i1, type);
				ToTranslate = secondPot;
			}
			else
			{
				orig(i, i1, type);
			}
		};

		OnNPC.AI_001_Slimes_GenerateItemInsideBody +=
			(orig, ballooned) => RandomizerSettings.Instance.Loot
				? RandomizedWorld.Translator[orig(ballooned)]
				: orig(ballooned);
	}

	private static void DoabilityEdit()
	{
		OnMain.UpdateTime_StartNight += (OnMain.orig_UpdateTime_StartNight orig, ref bool events) =>
		{
			if (!RandomizerSettings.Instance.DirectMecha)
				orig(ref events);
			else
			{
				WorldGen.altarCount += 3;
				orig(ref events);
				WorldGen.altarCount -= 3;
			}
		};

		OnPlayer.TileInteractionsUse += (orig, self, x, y) =>
		{
			if (RandomizerSettings.Instance.UnrequirePowerCell && NPC.downedPlantBoss && Main.tile[x, y].TileType == TileID.LihzahrdAltar)
			{
				SoundEngine.PlaySound(SoundID.Roar, (int)self.position.X, (int)self.position.Y, 0);
				if (Main.netMode != NetmodeID.MultiplayerClient)
					NPC.SpawnOnPlayer(self.whoAmI, NPCID.Golem);
				else
					NetMessage.SendData(MessageID.SpawnBoss, -1, -1, null, self.whoAmI, NPCID.Golem);
			}
			else
			{
				orig(self, x, y);
			}
		};
	}

	public override void PostSetupContent()
	{
		Dropables = new Dictionary<int, int>();
		for (int i = 0; i < ItemLoader.ItemCount; i++) Dropables.Add(i, new Item(i).createTile);
	}
}