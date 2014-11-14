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
	public class HumanFighter : Fighter
	{


		public HumanFighter()
		{
			ShipName = "Human Fighter";

			MassHull = 0.5f;
			StructureTotal = 2;
			SpinalWeaponSlot1FirePosition = new Vector2(21, 8);
			ArmorSlot1 = new SmallArmor();
			EngineSlot1 = new SmallEngine();
			GeneratorSlot1 = new SmallGenerator();
			ShieldSlot1 = new SmallShield();
			SpinalWeaponSlot1 = new SpinalBlasterGun();

			Size = new Vector2(23, 16);
			CollisionRadius = 12;
			CollisionMap = CollisionMapManager.GetTexture("ship_human_fighter");


			ResetShip();
		}
	}
}
