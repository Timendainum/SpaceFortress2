using System;
using System.Collections.Generic;
using System.Linq;
using GameLogicLibrary.Mobiles.Modules.Armors;
using GameLogicLibrary.Mobiles.Modules.Engines;
using GameLogicLibrary.Mobiles.Modules.Generators;
using GameLogicLibrary.Mobiles.Modules.Shields;
using GameLogicLibrary.Simulation;
using GameLogicLibrary.Mobiles.Modules.Weapons;
using Microsoft.Xna.Framework;

namespace GameLogicLibrary.Mobiles.Ships
{
	public class AlienFrigate1 : Frigate
	{


		public AlienFrigate1()
		{
			ShipName = "Alien Frigate";

			MassHull = 5f;
			StructureTotal = 50;
			SpinalWeaponSlot1FirePosition = new Vector2(20, 16);
			TurretWeaponSlot1FirePosition = new Vector2(20, 15);
			ArmorSlot1 = new BasicArmor();
			EngineSlot1 = new BasicEngine();
			GeneratorSlot1 = new BasicGenerator();
			ShieldSlot1 = new BasicShield();
			SpinalWeaponSlot1 = new SpinalPlasmaGun();
			TurretWeaponSlot1 = new TurretPlasmaGun();

			Size = new Vector2(32, 32);
			CollisionRadius = 22;
			CollisionMap = CollisionMapManager.GetTexture("ship_alien_frigate_1");


			ResetShip();
		}
	}
}
