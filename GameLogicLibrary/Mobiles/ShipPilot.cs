using GameLogicLibrary.Mobiles.Ships;
using GameLogicLibrary.Simulation;
using Microsoft.Xna.Framework;
using System;

namespace GameLogicLibrary.Mobiles
{
	public abstract class ShipPilot : Mobile
	{
		public event ChangedShipHandler ChangedShip;
		
		private Ship _CurrentShip;
		public Ship CurrentShip
		{
			get
			{
				return _CurrentShip;
			}
			set
			{
				//handle old ship
				Ship lastShip = CurrentShip;
				if (lastShip != null)
					_CurrentShip.ShipDestroyed -= Ship_OnShipDestroyed;
				
				//Set up new ship
				_CurrentShip = value;
				_CurrentShip.ShipPilot = this;
				_CurrentShip.ShipDestroyed += new ShipDestroyedEventHandler(Ship_OnShipDestroyed);
				UpdateStatsByShip();
				//Announce ChangedShip event
				OnChangedShip(new ChangedShipHandlerEventArgs(lastShip, CurrentShip));
			}
		}

		public ShipPilot(Vector2 location)
		{
			Collidable = true;
			WorldLocation = location;
		}


		#region helper
		public void UpdateStatsByShip()
		{
			RotationalThrust = CurrentShip.RotationalThrust;
			Thrust = CurrentShip.Thrust;
			Mass = CurrentShip.MassTotal;

			CollisionRadius = CurrentShip.CollisionRadius;
			CollisionMap = CurrentShip.CollisionMap;

			Size = CurrentShip.Size;
		}
		#endregion

		#region xna
		public override void Update(GameTime gameTime)
		{
			CurrentShip.Update(gameTime);

			base.Update(gameTime);
		}
		#endregion

		
		public void Ship_OnShipDestroyed(object o, ShipDestroyedEventArgs e)
		{
			Expired = true;
		}

		#region event announcement methods
		public void OnChangedShip(ChangedShipHandlerEventArgs e)
		{
			if (ChangedShip != null)
				ChangedShip(this, e);
		}
		#endregion
	}

	#region ImmobileAdded
	public delegate void ChangedShipHandler(object o, ChangedShipHandlerEventArgs e);

	public class ChangedShipHandlerEventArgs : EventArgs
	{
		public readonly Ship OldShip;
		public readonly Ship NewShip;

		public ChangedShipHandlerEventArgs(Ship oldShip, Ship newShip)
		{
			OldShip = oldShip;
			NewShip = newShip;
		}
	}


	#endregion
}
