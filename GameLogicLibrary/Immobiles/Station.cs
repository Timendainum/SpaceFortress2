using GameLogicLibrary.Simulation;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using GameLogicLibrary.Mobiles.Modules;
using GameLogicLibrary.Mobiles.Ships;
using System;

namespace GameLogicLibrary.Immobiles
{
	public class Station : Immobile
	{
		#region declarations
		public List<Module> ModuleStorage { get; protected set; }
		public List<Ship> ShipStorage { get; protected set; }


		private float _DockingRange = 0f;
		public float DockingRange
		{
			get
			{
				return _DockingRange;
			}
			protected set
			{
				_DockingRange = value;
			}
		}
		#endregion

		#region constructor
		public Station(Vector2 location)
		{
			ModuleStorage = new List<Module>();
			ShipStorage = new List<Ship>();
			Collidable = true;
			WorldLocation = location;
		}
		#endregion

		#region add/remove ship 
		public void AddShip(Ship ship)
		{
			ShipStorage.Add(ship);
			OnShipAdded(ship);
		}

		public void RemoveShip(Ship ship)
		{
			ShipStorage.Remove(ship);
			OnShipRemoved(ship);
		}

		public event EventHandler<StationShipAddRemoveEventArgs> ShipAdded;

		public virtual void OnShipAdded(Ship ship)
		{
			if (ShipAdded != null)
				ShipAdded(this, new StationShipAddRemoveEventArgs(ship));
		}

		public event EventHandler<StationShipAddRemoveEventArgs> ShipRemoved;

		public virtual void OnShipRemoved(Ship ship)
		{
			if (ShipRemoved != null)
				ShipRemoved(this, new StationShipAddRemoveEventArgs(ship));
		}
		#endregion

		#region add/remove module
		public void AddModule(Module module)
		{
			ModuleStorage.Add(module);
			OnModuleAdded(module);
		}

		public void RemoveModule(Module module)
		{
			ModuleStorage.Remove(module);
			OnModuleRemoved(module);
		}

		public event EventHandler<StationModuleAddRemoveEventArgs> ModuleAdded;

		public virtual void OnModuleAdded(Module module)
		{
			if (ModuleAdded != null)
				ModuleAdded(this, new StationModuleAddRemoveEventArgs(module));
		}

		public event EventHandler<StationModuleAddRemoveEventArgs> ModuleRemoved;

		public virtual void OnModuleRemoved(Module module)
		{
			if (ModuleRemoved != null)
				ModuleRemoved(this, new StationModuleAddRemoveEventArgs(module));
		}
		#endregion
	}

	public class StationShipAddRemoveEventArgs : EventArgs
	{
		public readonly Ship TheShip;

		public StationShipAddRemoveEventArgs(Ship ship)
		{
			TheShip = ship;
		}
	}

	public class StationModuleAddRemoveEventArgs : EventArgs
	{
		public readonly Module TheModule;

		public StationModuleAddRemoveEventArgs(Module mod)
		{
			TheModule = mod;
		}
	}
}
