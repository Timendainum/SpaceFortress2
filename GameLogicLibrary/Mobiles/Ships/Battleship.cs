using GameLogicLibrary.Mobiles.Modules.Armors;
using GameLogicLibrary.Mobiles.Modules.Engines;
using GameLogicLibrary.Mobiles.Modules.Generators;
using GameLogicLibrary.Mobiles.Modules.Shields;
using GameLogicLibrary.Mobiles.Modules.Weapons;
using Microsoft.Xna.Framework;

namespace GameLogicLibrary.Mobiles.Ships
{

	public abstract  class  Battleship : Ship
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

		public Vector2 SpinalWeaponSlot2FirePosition { get; set; }
		private SpinalWeapon _SpinalWeaponSlot2 = new NullSpinalGun();
		public SpinalWeapon SpinalWeaponSlot2
		{
			get
			{
				return _SpinalWeaponSlot2;
			}
			set
			{
				_SpinalWeaponSlot2 = value;
				//Should fire event here to handle unfitting other item
				SpinalWeapons[1] = SpinalWeaponSlot2;
				SpinalWeaponSlot2.RelativeFirePosition = SpinalWeaponSlot2FirePosition;
			}
		}

		public Vector2 TurretWeaponSlot1FirePosition { get; set; }
		private TurretWeapon _TurretWeaponSlot1 = new NullTurretGun();
		public TurretWeapon TurretWeaponSlot1
		{
			get
			{
				return _TurretWeaponSlot1;
			}
			set
			{
				_TurretWeaponSlot1 = value;
				//Should fire event here to handle unfitting other item
				TurretWeapons[0] = TurretWeaponSlot1;
				TurretWeaponSlot1.RelativeFirePosition = TurretWeaponSlot1FirePosition;
			}
		}

		public Vector2 TurretWeaponSlot2FirePosition { get; set; }
		private TurretWeapon _TurretWeaponSlot2 = new NullTurretGun();
		public TurretWeapon TurretWeaponSlot2
		{
			get
			{
				return _TurretWeaponSlot2;
			}
			set
			{
				_TurretWeaponSlot2 = value;
				//Should fire event here to handle unfitting other item
				TurretWeapons[1] = TurretWeaponSlot2;
				TurretWeaponSlot2.RelativeFirePosition = TurretWeaponSlot2FirePosition;
			}
		}

		public Vector2 TurretWeaponSlot3FirePosition { get; set; }
		private TurretWeapon _TurretWeaponSlot3 = new NullTurretGun();
		public TurretWeapon TurretWeaponSlot3
		{
			get
			{
				return _TurretWeaponSlot3;
			}
			set
			{
				_TurretWeaponSlot3 = value;
				//Should fire event here to handle unfitting other item
				TurretWeapons[2] = TurretWeaponSlot3;
				TurretWeaponSlot3.RelativeFirePosition = TurretWeaponSlot3FirePosition;
			}
		}

		public Vector2 TurretWeaponSlot4FirePosition { get; set; }
		private TurretWeapon _TurretWeaponSlot4 = new NullTurretGun();
		public TurretWeapon TurretWeaponSlot4
		{
			get
			{
				return _TurretWeaponSlot4;
			}
			set
			{
				_TurretWeaponSlot4 = value;
				//Should fire event here to handle unfitting other item
				TurretWeapons[3] = TurretWeaponSlot4;
				TurretWeaponSlot4.RelativeFirePosition = TurretWeaponSlot4FirePosition;
			}
		}
		#endregion

		#region constructor
		public Battleship()
		{
			ShipTypeName = "Battleship";
			MaxModuleMass = 25f;
			Armors.Add(ArmorSlot1);
			Engines.Add(EngineSlot1);
			Generators.Add(GeneratorSlot1);
			Shields.Add(ShieldSlot1);
			SpinalWeapons.Add(SpinalWeaponSlot1);
			SpinalWeapons.Add(SpinalWeaponSlot2);
			TurretWeapons.Add(TurretWeaponSlot1);
			TurretWeapons.Add(TurretWeaponSlot2);
			TurretWeapons.Add(TurretWeaponSlot3);
			TurretWeapons.Add(TurretWeaponSlot4);

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
			if (SpinalWeapons.Count > 2)
				return false;
			if (TurretWeapons.Count > 4)
				return false;
			if (Utilities.Count > 0)
				return false;

			return true;
		}
		
	}
}

