using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace ItemRandomizer;

public class ItemSlime : GlobalNPC
{
	public int Item;

	public override bool InstancePerEntity => true;

	public override void AI(NPC npc)
	{
		if (npc.aiStyle == 1 && npc.ai[1] > 0 && RandomizerSettings.Instance.Loot)
			Item = (int)npc.ai[1];
	}

	public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		if (npc.aiStyle == 1 && Item > 0 && RandomizerSettings.Instance.Loot)
			npc.ai[1] = Item;
		return true;
	}

	public override bool PreKill(NPC npc)
	{
		if (npc.aiStyle == 1 && Item > 0 && RandomizerSettings.Instance.Loot)
			npc.ai[1] = RandomizedWorld.ReverseTranslator[Item];
		return true;
	}
}