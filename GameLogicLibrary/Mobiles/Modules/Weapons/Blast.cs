using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using GameLogicLibrary.Simulation;
using GameLogicLibrary.Factions;

namespace GameLogicLibrary.Mobiles.Modules.Weapons
{
	public class Blast : Projectile
	{

		public Blast(Vector2 location, Weapon weaponFiredFrom, Entity pilotFiredBy)
			: base(location, weaponFiredFrom, pilotFiredBy)
		{
			Size = new Vector2(1, 1);
			CollisionRadius = 1;
			
			Mass = 0.1f;
			MaxLifetime = 0.3f;
			StartingSpeed = 1000.0f;
			Damage = 1;

			CollisionMap = CollisionMapManager.GetTexture("white_pixel");
			ImpactSoundEffect = "damage1";
		}
	}
}
