using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace ItemRandomizer;

[SuppressMessage("ReSharper", "UnassignedField.Global")]
public class RandomizerSettings : ModConfig
{
	[Label("$Mods.ItemRandomizer.Config.SameRarityRandomizing.Label")]
	[Tooltip("$Mods.ItemRandomizer.Config.SameRarityRandomizing.Tooltip")]
	[DefaultValue(true)]
	[ReloadRequired]
	public bool Rarity;

	#region RandomCategories

	[Label("$Mods.ItemRandomizer.Config.RandomCategories.Box.Label")] [DefaultValue(true)] [ReloadRequired]
	public bool Box;

	[Label("$Mods.ItemRandomizer.Config.RandomCategories.Craft.Label")] [DefaultValue(true)] [ReloadRequired]
	public bool Craft;

	[Label("$Mods.ItemRandomizer.Config.RandomCategories.Fish.Label")] [DefaultValue(true)] [ReloadRequired]
	public bool Fish;

	[Label("$Mods.ItemRandomizer.Config.RandomCategories.Loot.Label")] [DefaultValue(true)] [ReloadRequired]
	public bool Loot;

	[Label("$Mods.ItemRandomizer.Config.RandomCategories.Shop.Label")] [DefaultValue(true)] [ReloadRequired]
	public bool Shop;

	[Label("$Mods.ItemRandomizer.Config.RandomCategories.Terrain.Label")] [DefaultValue(true)] [ReloadRequired]
	public bool Terrain;

	[Label("$Mods.ItemRandomizer.Config.RandomCategories.WorldGen.Label")] [DefaultValue(true)] [ReloadRequired]
	public bool WorldGen;

	[Label("$Mods.ItemRandomizer.Config.RandomCategories.PickupItem.Label")]
	[Tooltip("$Mods.ItemRandomizer.Config.RandomCategories.PickupItem.Tooltip")]
	[DefaultValue(true)]
	[ReloadRequired]
	public bool PickupItem;

	#endregion

	#region Doability

	[Label("$Mods.ItemRandomizer.Config.Doability.Pwnhammer.Label")]
	[Tooltip("$Mods.ItemRandomizer.Config.Doability.Pwnhammer.Tooltip")]
	[DefaultValue(true)]
	[ReloadRequired]
	public bool Pwnhammer;

	[Label("$Mods.ItemRandomizer.Config.Doability.DirectMecha.Label")]
	[Tooltip("$Mods.ItemRandomizer.Config.Doability.DirectMecha.Tooltip")]
	[DefaultValue(false)]
	[ReloadRequired]
	public bool DirectMecha;

	[Label("$Mods.ItemRandomizer.Config.Doability.UniquePowerCell.Label")]
	[Tooltip("$Mods.ItemRandomizer.Config.Doability.UniquePowerCell.Tooltip")]
	[DefaultValue(true)]
	[ReloadRequired]
	public bool UniquePowerCell;

	[Label("$Mods.ItemRandomizer.Config.Doability.SellablePowerCell.Label")]
	[DefaultValue(false)]
	[ReloadRequired]
	public bool SellablePowerCell;

	[Label("$Mods.ItemRandomizer.Config.Doability.DontConsumePowerCell.Label")]
	[DefaultValue(false)]
	[ReloadRequired]
	public bool DontConsumePowerCell;

	[Label("$Mods.ItemRandomizer.Config.Doability.UnrequirePowerCell.Label")]
	[Tooltip("$Mods.ItemRandomizer.Config.Doability.UnrequirePowerCell.Tooltip")]
	[DefaultValue(false)]
	[ReloadRequired]
	public bool UnrequirePowerCell;

	#endregion

	public static RandomizerSettings Instance => ModContent.GetInstance<RandomizerSettings>();

	public override ConfigScope Mode => ConfigScope.ServerSide;
}