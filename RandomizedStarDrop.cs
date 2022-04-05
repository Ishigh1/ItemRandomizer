using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ItemRandomizer;

public class RandomizedStarDrop : GlobalProjectile
{
	public override bool PreKill(Projectile projectile, int timeLeft)
	{
		if (projectile.type == ProjectileID.FallingStar && projectile.damage > 500 &&
		    RandomizerSettings.Instance.Terrain)
		{
			ItemRandomizer.ToTranslate = true;
			ItemRandomizer.OnlyOnce = true;
		}

		return true;
	}
}