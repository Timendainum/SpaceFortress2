using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using GameLogicLibrary.Simulation;
using GameLogicLibrary.Factions;

namespace GameLogicLibrary.Mobiles.Modules.Weapons
{
	public class PlasmaBlast : Projectile
	{

		public PlasmaBlast(Vector2 location, Weapon weaponFiredFrom, Entity pilotFiredBy)
			: base(location, weaponFiredFrom, pilotFiredBy)
		{
			Size = new Vector2(12, 12);
			CollisionRadius = 6;
			
			Mass = 0.1f;
			MaxLifetime = 0.5f;
			StartingSpeed = 1000.0f;
			Damage = 2;

			CollisionMap = CollisionMapManager.GetTexture("projectile_plasmablast");
			ImpactSoundEffect = "damage1";
		}
	}
}
