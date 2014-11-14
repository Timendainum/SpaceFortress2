using GameLogicLibrary.Mobiles.Modules.Armors;
using GameLogicLibrary.Mobiles.Modules.Engines;
using GameLogicLibrary.Mobiles.Modules.Generators;
using GameLogicLibrary.Mobiles.Modules.Shields;
using GameLogicLibrary.Mobiles.Modules.Weapons;
using Microsoft.Xna.Framework;

namespace GameLogicLibrary.Mobiles.Ships
{

	public abstract  class Fighter : Ship
	{
		#region slot properties
		public Vector2 SpinalWeaponSlot1FirePosition { get; set; }
		private SpinalWeapon _SpinalWeaponSlot1 = new NullSpinalGun();
		public SpinalWeapon SpinalWeaponSlot1
		{
			get
			{
				return _SpinalWeaponSlot1;
			}
			set
			{
				_SpinalWeaponSlot1 = value;
				//Should fire event here to handle unfitting other item
				SpinalWeapons[0] = SpinalWeaponSlot1;
				SpinalWeaponSlot1.RelativeFirePosition = SpinalWeaponSlot1FirePosition;
			}
		}


		#endregion

		#region constructor
		public Fighter()
		{
			ShipTypeName = "Fighter";
			MaxModuleMass = 0.3f;
			Armors.Add(ArmorSlot1);
			Engines.Add(EngineSlot1);
			Generators.Add(GeneratorSlot1);
			Shields.Add(ShieldSlot1);
			SpinalWeapons.Add(SpinalWeaponSlot1);
		}
		#endregion




		/// <summary>
		/// This will enforce fitting rules for this ship
		/// </summary>
		/// <returns></returns>
		public override bool IsFitValid()
		{
			if (PowerCurrent < 0)
				return false;
			if (Armors.Count > 1)
				return false;
			if (Engines.Count > 1)
				return false;
			if (Generators.Count > 1)
				return false;
			if (Shields.Count > 1)
				return false;
			if (SpinalWeapons.Count > 1)
				return false;
			if (TurretWeapons.Count > 0)
				return false;
			if (Utilities.Count > 0)
				return false;

			return true;
		}
		
	}
}

