using Terraria;
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
}