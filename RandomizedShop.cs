using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ItemRandomizer;

public class RandomizedShop : GlobalNPC
{
	public override void SetupShop(int type, Chest shop, ref int nextSlot)
	{
		if (RandomizerSettings.Instance.Shop)
			foreach (Item item in shop.item)
			{
				int? price = item.shopCustomPrice;
				int currency = item.shopSpecialCurrency;

				RandomizedWorld.TranslateItem(item);

				item.shopCustomPrice = price;
				item.shopSpecialCurrency = currency;
			}

		if (RandomizerSettings.Instance.SellablePowerCell) shop.item[nextSlot++].SetDefaults(ItemID.LihzahrdPowerCell);
	}
}