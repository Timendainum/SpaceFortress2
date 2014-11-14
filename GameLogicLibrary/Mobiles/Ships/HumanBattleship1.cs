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
	public class HumanBattleship1 : Battleship
	{


		public HumanBattleship1()
		{
			ShipName = "Battleship Mark 1";

			MassHull = 50f;
			StructureTotal = 250;
			SpinalWeaponSlot1FirePosition = new Vector2(112, 46);
			SpinalWeaponSlot2FirePosition = new Vector2(112, 77);
			TurretWeaponSlot1FirePosition = new Vector2(66, 10);
			TurretWeaponSlot2FirePosition = new Vector2(68, 37);
			TurretWeaponSlot3FirePosition = new Vector2(68, 86);
			TurretWeaponSlot4FirePosition = new Vector2(66, 113);
			ArmorSlot1 = new MediumArmor();
			EngineSlot1 = new MediumEngine();
			GeneratorSlot1 = new MediumGenerator();
			ShieldSlot1 = new MediumShield();
			SpinalWeaponSlot1 = new SpinalOrbGun();
			SpinalWeaponSlot2 = new SpinalOrbGun();
			TurretWeaponSlot1 = new TurretRailGun();
			TurretWeaponSlot2 = new TurretRailGun();
			TurretWeaponSlot3 = new TurretRailGun();
			TurretWeaponSlot4 = new TurretRailGun();
			

			Size = new Vector2(123, 123);
			CollisionRadius = 70;
			CollisionMap = CollisionMapManager.GetTexture("ship_human_battleship_1");


			ResetShip();
		}
	}
}
