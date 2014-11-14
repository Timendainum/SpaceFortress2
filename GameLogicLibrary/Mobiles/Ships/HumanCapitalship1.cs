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
	public class HumanCapitalship1 : Capitalship
	{


		public HumanCapitalship1()
		{
			ShipName = "Capital Ship Mark 1";

			MassHull = 200f;
			StructureTotal = 500;
			SpinalWeaponSlot1FirePosition = new Vector2(240, 100);
			SpinalWeaponSlot2FirePosition = new Vector2(128, 142);
			SpinalWeaponSlot3FirePosition = new Vector2(240, 185);
			TurretWeaponSlot1FirePosition = new Vector2(153, 16);
			TurretWeaponSlot2FirePosition = new Vector2(140, 56);
			TurretWeaponSlot3FirePosition = new Vector2(119, 81);
			TurretWeaponSlot4FirePosition = new Vector2(119, 213);
			TurretWeaponSlot5FirePosition = new Vector2(140, 230);
			TurretWeaponSlot6FirePosition = new Vector2(153, 270);
			ArmorSlot1 = new HeavyArmor();
			EngineSlot1 = new HeavyEngine();
			GeneratorSlot1 = new HeavyGenerator();
			ShieldSlot1 = new HeavyShield();
			SpinalWeaponSlot1 = new SpinalOrbGun();
			SpinalWeaponSlot2 = new SpinalOrbGun();
			SpinalWeaponSlot3 = new SpinalOrbGun();
			TurretWeaponSlot1 = new TurretRailGun();
			TurretWeaponSlot2 = new TurretRailGun();
			TurretWeaponSlot3 = new TurretRailGun();
			TurretWeaponSlot4 = new TurretRailGun();
			TurretWeaponSlot5 = new TurretRailGun();
			TurretWeaponSlot6 = new TurretRailGun();
			

			Size = new Vector2(256, 286);
			CollisionRadius = 150;
			CollisionMap = CollisionMapManager.GetTexture("ship_human_capital_1");


			ResetShip();
		}
	}
}
