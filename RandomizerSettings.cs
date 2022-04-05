using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace ItemRandomizer;

public class RandomizerSettings : ModConfig
{
	[Label("$Mods.ItemRandomizer.Config.RandomCategories.Box.Label")] [DefaultValue(true)] [ReloadRequired]
	public bool Box;

	[Label("$Mods.ItemRandomizer.Config.RandomCategories.Craft.Label")] [DefaultValue(true)] [ReloadRequired]
	public bool Craft;

	[Label("$Mods.ItemRandomizer.Config.RandomCategories.Fish.Label")] [DefaultValue(true)] [ReloadRequired]
	public bool Fish;

	[Label("$Mods.ItemRandomizer.Config.RandomCategories.Loot.Label")] [DefaultValue(true)] [ReloadRequired]
	public bool Loot;

	[Label("$Mods.ItemRandomizer.Config.RandomCategories.PickupItem.Label")]
	[Tooltip("$Mods.ItemRandomizer.Config.RandomCategories.PickupItem.Tooltip")]
	[DefaultValue(true)]
	[ReloadRequired]
	public bool PickupItem;

	[Label("$Mods.ItemRandomizer.Config.SameRarityRandomizing.Label")]
	[Tooltip("$Mods.ItemRandomizer.Config.SameRarityRandomizing.Tooltip")]
	[DefaultValue(true)]
	[ReloadRequired]
	public bool Rarity;

	[Label("$Mods.ItemRandomizer.Config.RandomCategories.Shop.Label")] [DefaultValue(true)] [ReloadRequired]
	public bool Shop;

	[Label("$Mods.ItemRandomizer.Config.RandomCategories.Terrain.Label")] [DefaultValue(true)] [ReloadRequired]
	public bool Terrain;

	[Label("$Mods.ItemRandomizer.Config.RandomCategories.WorldGen.Label")] [DefaultValue(true)] [ReloadRequired]
	public bool WorldGen;

	public static RandomizerSettings Instance => ModContent.GetInstance<RandomizerSettings>();

	public override ConfigScope Mode => ConfigScope.ServerSide;
}