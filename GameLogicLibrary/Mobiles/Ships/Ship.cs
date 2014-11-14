using System;
using System.Collections.Generic;
using GameLogicLibrary.Maths;
using GameLogicLibrary.Mobiles.Modules;
using GameLogicLibrary.Mobiles.Modules.Armors;
using GameLogicLibrary.Mobiles.Modules.Engines;
using GameLogicLibrary.Mobiles.Modules.Generators;
using GameLogicLibrary.Mobiles.Modules.Shields;
using GameLogicLibrary.Mobiles.Modules.Utilities;
using GameLogicLibrary.Mobiles.Modules.Weapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameLogicLibrary.Immobiles;

namespace GameLogicLibrary.Mobiles.Ships
{

	public abstract class Ship
	{
		#region Delarations
		public event ShipDestroyedEventHandler ShipDestroyed;
        public event ShipFiredSpinalWeaponEventHandler ShipFiredSpinalWeapon;
        public event ShipFiredTurretWeaponEventHandler ShipFiredTurretWeapon;
		
		public string ShipTypeName { get; protected set; }
		public string ShipName { get; set; }
		public ShipPilot ShipPilot { get; set; }
		public bool Destroyed = false;

		public List<Armor> Armors = new List<Armor>();
		public List<Engine> Engines = new List<Engine>();
		public List<Generator> Generators = new List<Generator>();
		public List<Shield> Shields = new List<Shield>();
		public List<SpinalWeapon> SpinalWeapons = new List<SpinalWeapon>();
		public List<TurretWeapon> TurretWeapons = new List<TurretWeapon>();
		public List<Utility> Utilities = new List<Utility>();
		public List<Module> AllModules = new List<Module>();

		public float MaxModuleMass { get; protected set; }

		public int CollisionRadius { get; protected set; }
		public Texture2D CollisionMap { get; protected set; }
		public Vector2 Size { get; protected set; }
		public float MassHull { get; protected set; }

		private float _MassTotal;
		public float MassTotal
		{
			get
			{
				return _MassTotal;
			}
		}

		private float _Thrust;
		public float Thrust
		{
			get
			{
				return _Thrust;
			}
		}

		private float _RotationalThrust;
		public float RotationalThrust
		{
			get
			{
				return _RotationalThrust;
			}
		}

		private float _PowerTotal;
		public float PowerTotal
		{
			get
			{
				return _PowerTotal;
			}
		}

		private float _PowerCurrent;
		public float PowerCurrent
		{
			get
			{
				return _PowerCurrent;
			}
		}

		#region defense properties
		public int StructureTotal { get; protected set; }
		protected int StructureDamage;
		public int StructureCurrent { get { return StructureTotal - StructureDamage;} }
		public float StructureCurrentPercent
		{
			get
			{
				return ((float)StructureCurrent / (float)StructureTotal);
			}
		}


		private int _ArmorTotal;
		public int ArmorTotal
		{
			get
			{
				return _ArmorTotal;
			}
		}

		protected int ArmorDamage;
		public int ArmorCurrent { get { return ArmorTotal - ArmorDamage; } }

		public float ArmorCurrentPercent
		{
			get
			{
				return ((float)ArmorCurrent / (float)ArmorTotal);
			}
		}

		private int _ShieldTotal;
		public int ShieldTotal
		{
			get
			{
				return _ShieldTotal;
			}
		}

		public int ShieldDamage
		{
			get
			{
				return _ShieldDamage;
			}
			set
			{
				_ShieldDamage = value;
				if (_ShieldDamage < 0)
					_ShieldDamage = 0;
			}
		}
		private int _ShieldDamage;

		public int ShieldCurrent { get { return ShieldTotal - ShieldDamage; } }

		public float ShieldCurrentPercent
		{
			get
			{
				float result = ((float)ShieldCurrent / (float)ShieldTotal);
				return result;
			}
		}

		float ShieldRechargeTimer = 0f;

		#region slot properties
		private Armor _ArmorSlot1 = new NullArmor();
		public Armor ArmorSlot1
		{
			get
			{
				return _ArmorSlot1;
			}
			set
			{
				_ArmorSlot1 = value;
				//Should fire event here to handle unfitting other item
				Armors[0] = ArmorSlot1;
			}
		}

		private Engine _EngineSlot1 = new NullEngine();
		public Engine EngineSlot1
		{
			get
			{
				return _EngineSlot1;
			}
			set
			{
				_EngineSlot1 = value;
				//Should fire event here to handle unfitting other item
				Engines[0] = EngineSlot1;
			}
		}

		private Generator _GeneratorSlot1 = new NullGenerator();
		public Generator GeneratorSlot1
		{
			get
			{
				return _GeneratorSlot1;
			}
			set
			{
				_GeneratorSlot1 = value;
				//Should fire event here to handle unfitting other item
				Generators[0] = GeneratorSlot1;
			}
		}

		private Shield _ShieldSlot1 = new NullShield();
		public Shield ShieldSlot1
		{
			get
			{
				return _ShieldSlot1;
			}
			set
			{
				_ShieldSlot1 = value;
				//Should fire event here to handle unfitting other item
				Shields[0] = ShieldSlot1;
			}
		}
		#endregion

		#endregion

		#endregion

		#region constructor
		public Ship()
		{
			ResetShip();
		}
		#endregion

		#region fitting methods
		public abstract bool IsFitValid();

		public bool FitModuleFromStation(Module modToFit, Module modToReplace, Station station)
		{
			if (modToFit.Mass > MaxModuleMass)
				return false;

			if (modToReplace == ArmorSlot1)
			{
				if (!(modToReplace is NullArmor))
					station.AddModule(modToReplace);
					
				station.RemoveModule(modToFit);
				ArmorSlot1 = (Armor)modToFit;
				ResetShip();
				return true;
			}


			if (modToReplace == EngineSlot1)
			{
				if (!(modToReplace is NullEngine))
					station.AddModule(modToReplace);

				station.RemoveModule(modToFit);
				EngineSlot1 = (Engine)modToFit;
				ResetShip();
				return true;
			}


			if (modToReplace == GeneratorSlot1)
			{
				if (!(modToReplace is NullGenerator))
					station.AddModule(modToReplace);

				station.RemoveModule(modToFit);
				GeneratorSlot1 = (Generator)modToFit;
				ResetShip();
				return true;
			}

			if (modToReplace == ShieldSlot1)
			{
				if (!(modToReplace is NullShield))
					station.AddModule(modToReplace);

				station.RemoveModule(modToFit);
				ShieldSlot1 = (Shield)modToFit;
				ResetShip();
				return true;
			}

			//Check weapons
			if (this is Fighter)
			{
				Fighter me = (Fighter)this;
				if (me.SpinalWeaponSlot1 == modToReplace)
				{
					if (!(modToReplace is NullSpinalGun))
						station.AddModule(modToReplace);

					station.RemoveModule(modToFit);
					me.SpinalWeaponSlot1 = (SpinalWeapon)modToFit;
					ResetShip();
					return true;
				}
			}

			//Check weapons
			if (this is Frigate)
			{
				Frigate me = (Frigate)this;
				if (me.SpinalWeaponSlot1 == modToReplace)
				{
					if (!(modToReplace is NullSpinalGun))
						station.AddModule(modToReplace);

					station.RemoveModule(modToFit);
					me.SpinalWeaponSlot1 = (SpinalWeapon)modToFit;
					ResetShip();
					return true;
				}

				if (me.TurretWeaponSlot1 == modToReplace)
				{
					if (!(modToReplace is NullTurretGun))
						station.AddModule(modToReplace);

					station.RemoveModule(modToFit);
					me.TurretWeaponSlot1 = (TurretWeapon)modToFit;
					ResetShip();
					return true;
				}
			}

			//Check weapons
			if (this is Cruiser)
			{
				Cruiser me = (Cruiser)this;
				if (me.SpinalWeaponSlot1 == modToReplace)
				{
					if (!(modToReplace is NullSpinalGun))
						station.AddModule(modToReplace);

					station.RemoveModule(modToFit);
					me.SpinalWeaponSlot1 = (SpinalWeapon)modToFit;
					ResetShip();
					return true;
				}

				if (me.TurretWeaponSlot1 == modToReplace)
				{
					if (!(modToReplace is NullTurretGun))
						station.AddModule(modToReplace);

					station.RemoveModule(modToFit);
					me.TurretWeaponSlot1 = (TurretWeapon)modToFit;
					ResetShip();
					return true;
				}

				if (me.TurretWeaponSlot2 == modToReplace)
				{
					if (!(modToReplace is NullTurretGun))
						station.AddModule(modToReplace);

					station.RemoveModule(modToFit);
					me.TurretWeaponSlot2 = (TurretWeapon)modToFit;
					ResetShip();
					return true;
				}
			}

			//Check weapons
			if (this is Battleship)
			{
				Battleship me = (Battleship)this;
				if (me.SpinalWeaponSlot1 == modToReplace)
				{
					if (!(modToReplace is NullSpinalGun))
						station.AddModule(modToReplace);

					station.RemoveModule(modToFit);
					me.SpinalWeaponSlot1 = (SpinalWeapon)modToFit;
					ResetShip();
					return true;
				}

				if (me.SpinalWeaponSlot2 == modToReplace)
				{
					if (!(modToReplace is NullSpinalGun))
						station.AddModule(modToReplace);

					station.RemoveModule(modToFit);
					me.SpinalWeaponSlot2 = (SpinalWeapon)modToFit;
					ResetShip();
					return true;
				}

				if (me.TurretWeaponSlot1 == modToReplace)
				{
					if (!(modToReplace is NullTurretGun))
						station.AddModule(modToReplace);

					station.RemoveModule(modToFit);
					me.TurretWeaponSlot1 = (TurretWeapon)modToFit;
					ResetShip();
					return true;
				}

				if (me.TurretWeaponSlot2 == modToReplace)
				{
					if (!(modToReplace is NullTurretGun))
						station.AddModule(modToReplace);

					station.RemoveModule(modToFit);
					me.TurretWeaponSlot2 = (TurretWeapon)modToFit;
					ResetShip();
					return true;
				}

				if (me.TurretWeaponSlot3 == modToReplace)
				{
					if (!(modToReplace is NullTurretGun))
						station.AddModule(modToReplace);

					station.RemoveModule(modToFit);
					me.TurretWeaponSlot3 = (TurretWeapon)modToFit;
					ResetShip();
					return true;
				}

				if (me.TurretWeaponSlot4 == modToReplace)
				{
					if (!(modToReplace is NullTurretGun))
						station.AddModule(modToReplace);

					station.RemoveModule(modToFit);
					me.TurretWeaponSlot4 = (TurretWeapon)modToFit;
					ResetShip();
					return true;
				}
			}

			//Check weapons
			if (this is Capitalship)
			{
				Capitalship me = (Capitalship)this;
				if (me.SpinalWeaponSlot1 == modToReplace)
				{
					if (!(modToReplace is NullSpinalGun))
						station.AddModule(modToReplace);

					station.RemoveModule(modToFit);
					me.SpinalWeaponSlot1 = (SpinalWeapon)modToFit;
					ResetShip();
					return true;
				}

				if (me.SpinalWeaponSlot2 == modToReplace)
				{
					if (!(modToReplace is NullSpinalGun))
						station.AddModule(modToReplace);

					station.RemoveModule(modToFit);
					me.SpinalWeaponSlot2 = (SpinalWeapon)modToFit;
					ResetShip();
					return true;
				}

				if (me.SpinalWeaponSlot3 == modToReplace)
				{
					if (!(modToReplace is NullSpinalGun))
						station.AddModule(modToReplace);

					station.RemoveModule(modToFit);
					me.SpinalWeaponSlot3 = (SpinalWeapon)modToFit;
					ResetShip();
					return true;
				}

				if (me.TurretWeaponSlot1 == modToReplace)
				{
					if (!(modToReplace is NullTurretGun))
						station.AddModule(modToReplace);

					station.RemoveModule(modToFit);
					me.TurretWeaponSlot1 = (TurretWeapon)modToFit;
					ResetShip();
					return true;
				}

				if (me.TurretWeaponSlot2 == modToReplace)
				{
					if (!(modToReplace is NullTurretGun))
						station.AddModule(modToReplace);

					station.RemoveModule(modToFit);
					me.TurretWeaponSlot2 = (TurretWeapon)modToFit;
					ResetShip();
					return true;
				}

				if (me.TurretWeaponSlot3 == modToReplace)
				{
					if (!(modToReplace is NullTurretGun))
						station.AddModule(modToReplace);

					station.RemoveModule(modToFit);
					me.TurretWeaponSlot3 = (TurretWeapon)modToFit;
					ResetShip();
					return true;
				}

				if (me.TurretWeaponSlot4 == modToReplace)
				{
					if (!(modToReplace is NullTurretGun))
						station.AddModule(modToReplace);

					station.RemoveModule(modToFit);
					me.TurretWeaponSlot4 = (TurretWeapon)modToFit;
					ResetShip();
					return true;
				}

				if (me.TurretWeaponSlot5 == modToReplace)
				{
					if (!(modToReplace is NullTurretGun))
						station.AddModule(modToReplace);

					station.RemoveModule(modToFit);
					me.TurretWeaponSlot5 = (TurretWeapon)modToFit;
					ResetShip();
					return true;
				}

				if (me.TurretWeaponSlot6 == modToReplace)
				{
					if (!(modToReplace is NullTurretGun))
						station.AddModule(modToReplace);

					station.RemoveModule(modToFit);
					me.TurretWeaponSlot6 = (TurretWeapon)modToFit;
					ResetShip();
					return true;
				}
			}


			return false;
		}

		/// <summary>
		/// This needs to be refactored and move the ship specific logic
		/// to those classes.
		/// </summary>
		/// <param name="mod"></param>
		/// <param name="station"></param>
		/// <returns></returns>
		public bool RemoveModuleToStation(Module mod, Station station)
		{
			if (ArmorSlot1 == mod)
			{
				station.AddModule(mod);
				ArmorSlot1 = new NullArmor();
				ResetShip();
				return true;
			}

			if (EngineSlot1 == mod)
			{
				station.AddModule(mod);
				EngineSlot1 = new NullEngine();
				ResetShip();
				return true;
			}

			if (GeneratorSlot1 == mod)
			{
				station.AddModule(mod);
				GeneratorSlot1 = new NullGenerator();
				ResetShip();
				return true;
			}

			if (ShieldSlot1 == mod)
			{
				station.AddModule(mod);
				ShieldSlot1 = new NullShield();
				ResetShip();
				return true;
			}

			//Check weapons
			if (this is Fighter)
			{
				Fighter me = (Fighter)this;
				if (me.SpinalWeaponSlot1 == mod)
				{
					station.AddModule(mod);
					me.SpinalWeaponSlot1 = new NullSpinalGun();
					ResetShip();
					return true;
				}
			}

			//Check weapons
			if (this is Frigate)
			{
				Frigate me = (Frigate)this;
				if (me.SpinalWeaponSlot1 == mod)
				{
					station.AddModule(mod);
					me.SpinalWeaponSlot1 = new NullSpinalGun();
					ResetShip();
					return true;
				}

				if (me.TurretWeaponSlot1 == mod)
				{
					station.AddModule(mod);
					me.TurretWeaponSlot1 = new NullTurretGun();
					ResetShip();
					return true;
				}
			}

			//Check weapons
			if (this is Cruiser)
			{
				Cruiser me = (Cruiser)this;
				if (me.SpinalWeaponSlot1 == mod)
				{
					station.AddModule(mod);
					me.SpinalWeaponSlot1 = new NullSpinalGun();
					ResetShip();
					return true;
				}

				if (me.TurretWeaponSlot1 == mod)
				{
					station.AddModule(mod);
					me.TurretWeaponSlot1 = new NullTurretGun();
					ResetShip();
					return true;
				}

				if (me.TurretWeaponSlot2 == mod)
				{
					station.AddModule(mod);
					me.TurretWeaponSlot2 = new NullTurretGun();
					ResetShip();
					return true;
				}
			}

			//Check weapons
			if (this is Battleship)
			{
				Battleship me = (Battleship)this;
				if (me.SpinalWeaponSlot1 == mod)
				{
					station.AddModule(mod);
					me.SpinalWeaponSlot1 = new NullSpinalGun();
					ResetShip();
					return true;
				}

				if (me.SpinalWeaponSlot2 == mod)
				{
					station.AddModule(mod);
					me.SpinalWeaponSlot2 = new NullSpinalGun();
					ResetShip();
					return true;
				}

				if (me.TurretWeaponSlot1 == mod)
				{
					station.AddModule(mod);
					me.TurretWeaponSlot1 = new NullTurretGun();
					ResetShip();
					return true;
				}

				if (me.TurretWeaponSlot2 == mod)
				{
					station.AddModule(mod);
					me.TurretWeaponSlot2 = new NullTurretGun();
					ResetShip();
					return true;
				}

				if (me.TurretWeaponSlot3 == mod)
				{
					station.AddModule(mod);
					me.TurretWeaponSlot3 = new NullTurretGun();
					ResetShip();
					return true;
				}

				if (me.TurretWeaponSlot4 == mod)
				{
					station.AddModule(mod);
					me.TurretWeaponSlot4 = new NullTurretGun();
					ResetShip();
					return true;
				}
			}

			//Check weapons
			if (this is Capitalship)
			{
				Capitalship me = (Capitalship)this;
				if (me.SpinalWeaponSlot1 == mod)
				{
					station.AddModule(mod);
					me.SpinalWeaponSlot1 = new NullSpinalGun();
					ResetShip();
					return true;
				}

				if (me.SpinalWeaponSlot2 == mod)
				{
					station.AddModule(mod);
					me.SpinalWeaponSlot2 = new NullSpinalGun();
					ResetShip();
					return true;
				}

				if (me.SpinalWeaponSlot3 == mod)
				{
					station.AddModule(mod);
					me.SpinalWeaponSlot3 = new NullSpinalGun();
					ResetShip();
					return true;
				}

				if (me.TurretWeaponSlot1 == mod)
				{
					station.AddModule(mod);
					me.TurretWeaponSlot1 = new NullTurretGun();
					ResetShip();
					return true;
				}

				if (me.TurretWeaponSlot2 == mod)
				{
					station.AddModule(mod);
					me.TurretWeaponSlot2 = new NullTurretGun();
					ResetShip();
					return true;
				}

				if (me.TurretWeaponSlot3 == mod)
				{
					station.AddModule(mod);
					me.TurretWeaponSlot3 = new NullTurretGun();
					ResetShip();
					return true;
				}

				if (me.TurretWeaponSlot4 == mod)
				{
					station.AddModule(mod);
					me.TurretWeaponSlot4 = new NullTurretGun();
					ResetShip();
					return true;
				}

				if (me.TurretWeaponSlot5 == mod)
				{
					station.AddModule(mod);
					me.TurretWeaponSlot5 = new NullTurretGun();
					ResetShip();
					return true;
				}

				if (me.TurretWeaponSlot6 == mod)
				{
					station.AddModule(mod);
					me.TurretWeaponSlot6 = new NullTurretGun();
					ResetShip();
					return true;
				}
			}

			return false;
		}
		#endregion

		#region ship methods
		public void FireSpinalWeapons(ShipPilot firedBy)
		{
			Vector2 weaponWorldLocation;
			float distanceToWeapon, angleToWeapon;
			
			foreach (SpinalWeapon weapon in SpinalWeapons)
			{
				distanceToWeapon = Vector2.Distance(weapon.RelativeFirePosition, firedBy.RelativeCenter);
				angleToWeapon = (MathsHelper.DirectInterceptAngle(firedBy.RelativeCenter, weapon.RelativeFirePosition) + firedBy.Rotation) % MathHelper.TwoPi;
				weaponWorldLocation = MathsHelper.RotateAroundCircle(angleToWeapon, distanceToWeapon, firedBy.RelativeCenter) + firedBy.WorldLocation;
				weapon.Fire(firedBy, weaponWorldLocation);
			}
		}

		public void FireTurretWeapons(ShipPilot firedBy)
		{
			Vector2 weaponWorldLocation;
			float distanceToWeapon, angleToWeapon;

			foreach (TurretWeapon weapon in TurretWeapons)
			{
				distanceToWeapon = Vector2.Distance(weapon.RelativeFirePosition, firedBy.RelativeCenter);
				angleToWeapon = (MathsHelper.DirectInterceptAngle(firedBy.RelativeCenter, weapon.RelativeFirePosition) + firedBy.Rotation) % MathHelper.TwoPi;
				weaponWorldLocation = MathsHelper.RotateAroundCircle(angleToWeapon, distanceToWeapon, firedBy.RelativeCenter) + firedBy.WorldLocation;
				weapon.Fire(firedBy, weaponWorldLocation);
			}
		}

		public void ApplyDamage(int damage)
		{
			ShieldDamage += damage;
			NormalizeShieldDamage();
			NormalizeArmorDamage();
			NormalizeStructureDamage();
		}

		public void RepairDamage(int repairAmount)
		{
			int repairAmountLeft = repairAmount;

			//Repair structure
			while (StructureDamage > 0 && repairAmountLeft > 0)
            {
				StructureDamage--;
				repairAmountLeft--;
            }
			//repair armor
			while (ArmorDamage > 0 && repairAmountLeft > 0)
			{
				ArmorDamage--;
				repairAmountLeft--;
			}

		}

		private void NormalizeShieldDamage()
		{
			if (ShieldDamage >= ShieldTotal)
			{
				ArmorDamage += (ShieldDamage - ShieldTotal);
				ShieldDamage = ShieldTotal;
			}
		}

		private void NormalizeArmorDamage()
		{
			if (ArmorDamage >= ArmorTotal)
			{
				StructureDamage += (ArmorDamage - ArmorTotal);
				ArmorDamage = ArmorTotal;
			}
		}

		private void NormalizeStructureDamage()
		{
			if (StructureDamage >= StructureTotal)
			{
				Destroyed = true;
				OnShipDestroyed(new ShipDestroyedEventArgs(this));
			}
		}
		#endregion

		#region xna
		public virtual void Update(GameTime gameTime)
		{
			float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

			ShieldRechargeTimer += elapsed;
			if (ShieldRechargeTimer >= 1f)
			{
				ShieldDamage -= Shields[0].RegenerationRate;
				ShieldRechargeTimer = 0;
			}

			foreach (SpinalWeapon weapon in SpinalWeapons)
			{
				weapon.Update(gameTime);
			}

			foreach (TurretWeapon weapon in TurretWeapons)
			{
				weapon.Update(gameTime);
			}
		}
		#endregion

		#region calculation methods
		protected void ResetShip()
		{
			UpdateAllModules();
			CalculateArmorTotal();
			CalculateMassTotal();
			CalculatePowerCurrent();
			CalculatePowerTotal();
			CalculateRotationalThrust();
			CalculateShieldTotal();
			CalculateThrust();
		}
		
		protected void UpdateAllModules()
		{
			AllModules.Clear();

			foreach (Module mod in Armors)
			{
				AllModules.Add(mod);
			}
			foreach (Module mod in Engines)
			{
				AllModules.Add(mod);
			}
			foreach (Module mod in Generators)
			{
				AllModules.Add(mod);
			}
			foreach (Module mod in Shields)
			{
				AllModules.Add(mod);
			}
			foreach (Module mod in SpinalWeapons)
			{
				AllModules.Add(mod);
			}
			foreach (Module mod in TurretWeapons)
			{
				AllModules.Add(mod);
			}
			foreach (Module mod in Utilities)
			{
				AllModules.Add(mod);
			}
		}

		//Armor related
		protected void CalculateArmorTotal()
		{
			_ArmorTotal = 0;

			foreach (Armor mod in Armors)
			{
				_ArmorTotal += mod.ArmorPoints;
			}
		}

		//Shield related
		protected void CalculateShieldTotal()
		{
			_ShieldTotal = 0;

			foreach (Shield mod in Shields)
			{
				_ShieldTotal += mod.ShieldPoints;
			}
		}


		//Engine related
		protected void CalculateMassTotal()
		{
			_MassTotal = 0f;
			_MassTotal += MassHull;

			foreach (Module mod in AllModules)
			{
				_MassTotal += mod.Mass;
			}
		}

		protected void CalculateThrust()
		{
			_Thrust = 0f;

			foreach (Engine engine in Engines)
			{
				_Thrust += engine.Thrust;
			}
		}

		protected void CalculateRotationalThrust()
		{
			_RotationalThrust = 0f;

			foreach (Engine engine in Engines)
			{
				_RotationalThrust += engine.RotationalThrust;
			}
		}


		//Fitting related
		protected void CalculatePowerTotal()
		{
			_PowerTotal = 0f;

			foreach (Module mod in AllModules)
			{
				if (mod.Power > 0)
				_PowerTotal += mod.Power;
			}
		}

		protected void CalculatePowerCurrent()
		{
			_PowerCurrent = 0f;

			foreach (Module mod in AllModules)
			{
				_PowerCurrent += mod.Power;
			}
		}
		#endregion

		#region event announcing methods
		public void OnShipDestroyed(ShipDestroyedEventArgs e)
		{
			if (ShipDestroyed != null)
				ShipDestroyed(this, e);
		}

		public void OnShipFiredSpinalWeapon(ShipFiredSpinalWeaponEventArgs e)
		{
			if (ShipFiredSpinalWeapon != null)
				ShipFiredSpinalWeapon(this, e);
		}

		public void OnShipFiredTurretWeapon(ShipFiredTurretWeaponEventArgs e)
		{
			if (ShipFiredTurretWeapon != null)
				ShipFiredTurretWeapon(this, e);
		}
		#endregion
	}

	#region events

	public delegate void ShipDestroyedEventHandler(object o , ShipDestroyedEventArgs e);

	public class ShipDestroyedEventArgs : EventArgs
	{
		public readonly Ship DestroyedShip;

		public ShipDestroyedEventArgs(Ship destroyedShip)
		{
			DestroyedShip = destroyedShip;
		}
	}

	public delegate void ShipFiredSpinalWeaponEventHandler(object o, ShipFiredSpinalWeaponEventArgs e);

	public class ShipFiredSpinalWeaponEventArgs : EventArgs
	{
		public readonly ShipPilot FiredBy;
		public readonly SpinalWeapon WeaponFired;

		public ShipFiredSpinalWeaponEventArgs(ShipPilot firedBy, SpinalWeapon weaponFired)
		{
			FiredBy = firedBy;
			WeaponFired = weaponFired;
		}
	}

	public delegate void ShipFiredTurretWeaponEventHandler(object o, ShipFiredTurretWeaponEventArgs e);

	public class ShipFiredTurretWeaponEventArgs : EventArgs
	{
		public readonly ShipPilot FiredBy;
		public readonly TurretWeapon WeaponFired;

		public ShipFiredTurretWeaponEventArgs(ShipPilot firedBy, TurretWeapon weaponFired)
		{
			FiredBy = firedBy;
			WeaponFired = weaponFired;
		}
	}
	#endregion
}
