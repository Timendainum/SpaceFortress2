using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using GameLogicLibrary.Simulation;
using GameLogicLibrary.Factions;

namespace GameLogicLibrary.Mobiles.Modules.Weapons
{
	public class Sabot : Projectile
	{

		public Sabot(Vector2 location, Weapon weaponFiredFrom, Entity pilotFiredBy)
			: base(location, weaponFiredFrom, pilotFiredBy)
		{
			Size = new Vector2(11, 8);
			CollisionRadius = 6;
			
			Mass = 0.5f;
			MaxLifetime = 1.0f;
			StartingSpeed = 2000.0f;
			Damage = 4;

			CollisionMap = CollisionMapManager.GetTexture("projectile_sabot");
			ImpactSoundEffect = "damage1";
		}
	}
}
