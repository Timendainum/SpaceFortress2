using Microsoft.Xna.Framework;
using GameLogicLibrary.Mobiles.Modules.Armors;
using GameLogicLibrary.Mobiles.Modules.Engines;
using GameLogicLibrary.Mobiles.Modules.Generators;
using GameLogicLibrary.Mobiles.Modules.Shields;
using GameLogicLibrary.Mobiles.Modules.Utilities;
using GameLogicLibrary.Mobiles.Modules.Weapons;

namespace GameLogicLibrary.Mobiles.Modules
{

	public abstract class Module
	{
		
		// Module Size
		public string Name;

		public string TypeName
		{
			get
			{
				if (this is Armor)
					return "Armor";
				if (this is Engine)
					return "Engine";
				if (this is Generator)
					return "Generator";
				if (this is Shield)
					return "Shield";
				if (this is Utility)
					return "Utility";
				if (this is Weapon)
				{
					if (this is SpinalWeapon)
						return "Spinal Weapon";
					if (this is TurretWeapon)
						return "Turret Weapon";
				}

				return "Other";
			}
		}
		//Module Power
		private float _Power;
		public float Power
		{
			get
			{
				return _Power;
			}
			protected set
			{
				_Power = value;
			}
		}

		//Module Mass
		private float _Mass;
		public float Mass
		{
			get
			{
				return _Mass;
			}
			protected set
			{
				_Mass = value;
			}
		}

	}
}
