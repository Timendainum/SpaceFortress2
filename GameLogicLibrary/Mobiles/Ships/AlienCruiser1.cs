using System;
using GameLogicLibrary.Mobiles.Modules.Armors;
using GameLogicLibrary.Mobiles.Modules.Engines;
using GameLogicLibrary.Mobiles.Modules.Generators;
using GameLogicLibrary.Mobiles.Modules.Shields;
using GameLogicLibrary.Simulation;
using GameLogicLibrary.Mobiles.Modules.Weapons;
using Microsoft.Xna.Framework;

namespace GameLogicLibrary.Mobiles.Ships
{
	public class AlienCruiser1 : Cruiser
	{


		public AlienCruiser1()
		{
			ShipName = "Alien Cruiser";

			MassHull = 20f;
			StructureTotal = 100;
			SpinalWeaponSlot1FirePosition = new Vector2(48, 32);
			TurretWeaponSlot1FirePosition = new Vector2(36, 5);
			TurretWeaponSlot2FirePosition = new Vector2(36, 59);
			ArmorSlot1 = new LightArmor();
			EngineSlot1 = new LightEngine();
			GeneratorSlot1 = new LightGenerator();
			ShieldSlot1 = new LightShield();
			SpinalWeaponSlot1 = new SpinalOrbGun();
			TurretWeaponSlot1 = new TurretPlasmaGun();
			TurretWeaponSlot2 = new TurretPlasmaGun();
			

			Size = new Vector2(64, 64);
			CollisionRadius = 42;
			CollisionMap = CollisionMapManager.GetTexture("ship_alien_cruiser_1");


			ResetShip();
		}
	}
}
