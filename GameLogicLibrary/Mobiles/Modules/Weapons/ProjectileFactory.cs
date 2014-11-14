using Microsoft.Xna.Framework;
using GameLogicLibrary.Simulation;

namespace GameLogicLibrary.Mobiles.Modules.Weapons
{
	public static class ProjectileFactory
	{
		public static Projectile Create(Vector2 location, Weapon firedFrom, Entity firedBy)
		 {
			if (firedFrom is SpinalBlasterGun)
				 return new Blast(location, firedFrom, firedBy); 
			if (firedFrom is SpinalPlasmaGun)
				return new PlasmaBlast(location, firedFrom, firedBy);
			if (firedFrom is SpinalOrbGun)
				return new OrbBlast(location, firedFrom, firedBy);
			if (firedFrom is TurretPlasmaGun)
				return new PlasmaBlast(location, firedFrom, firedBy);
			if (firedFrom is TurretRailGun)
				return new Sabot(location, firedFrom, firedBy);

			return null;
		}

	}
}
