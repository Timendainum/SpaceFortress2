using GameLogicLibrary.Simulation;
using Microsoft.Xna.Framework;
using GameLogicLibrary.Factions;

namespace GameLogicLibrary.Mobiles.Modules.Weapons
{
	public class OrbBlast : Projectile
	{
		public OrbBlast(Vector2 location, Weapon weaponFiredFrom, Entity pilotFiredBy)
			: base(location, weaponFiredFrom, pilotFiredBy)
		{
			Size = new Vector2(45, 45);
			CollisionRadius = 23;

			Mass = 0.1f;
			MaxLifetime = 1.5f;
			StartingSpeed = 500f;
			Damage = 20;

			CollisionMap = CollisionMapManager.GetTexture("projectile_orb");
			ImpactSoundEffect = "damage2";
		}

	}
}
