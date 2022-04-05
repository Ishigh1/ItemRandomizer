using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace ItemRandomizer;

public class RandomizedPlayer : ModPlayer
{
	public override void CatchFish(FishingAttempt attempt, ref int itemDrop, ref int npcSpawn,
		ref AdvancedPopupRequest sonar,
		ref Vector2 sonarPosition)
	{
		if (itemDrop > 0 && RandomizerSettings.Instance.Fish)
			itemDrop = RandomizedWorld.Translator[itemDrop];
	}
}