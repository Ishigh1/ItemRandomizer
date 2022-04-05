using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ItemRandomizer;

public class RandomizedItem : GlobalItem
{
	public override void SetDefaults(Item item)
	{
		if (RandomizedWorld.ReverseTranslator != null &&
		    RandomizedWorld.ReverseTranslator.TryGetValue(item.type, out int oldType))
			item.SetNameOverride(Lang.GetItemNameValue(item.type) + " (" + Lang.GetItemNameValue(oldType) + ")");
	}

	public override bool ConsumeItem(Item item, Player player)
	{
		return !RandomizerSettings.Instance.DontConsumePowerCell || item.type != ItemID.LihzahrdPowerCell;
	}
}