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
	public class HumanCruiser1 : Cruiser
	{


		public HumanCruiser1()
		{
			ShipName = "Cruiser Mark 1";

			MassHull = 20f;
			StructureTotal = 100;
			SpinalWeaponSlot1FirePosition = new Vector2(60, 31);
			TurretWeaponSlot1FirePosition = new Vector2(45, 10);
			TurretWeaponSlot2FirePosition = new Vector2(45, 53);
			ArmorSlot1 = new LightArmor();
			EngineSlot1 = new LightEngine();
			GeneratorSlot1 = new LightGenerator();
			ShieldSlot1 = new LightShield();
			SpinalWeaponSlot1 = new SpinalOrbGun();
			TurretWeaponSlot1 = new TurretRailGun();
			TurretWeaponSlot2 = new TurretRailGun();
			

			Size = new Vector2(64, 63);
			CollisionRadius = 42;
			CollisionMap = CollisionMapManager.GetTexture("ship_human_cruiser_1");


			ResetShip();
		}
	}
}
