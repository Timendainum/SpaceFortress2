using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameLogicLibrary.Simulation;
using Microsoft.Xna.Framework;

namespace GameLogicLibrary.Mobiles.Ships
{
	public class NullShip : Ship
	{
		public NullShip()
		{
			ShipName = "No Ship";
			ShipTypeName = "No Ship";
			MassHull = 0f;
			StructureTotal = 0;


			Size = Vector2.Zero;
			CollisionRadius = 20;
			CollisionMap = CollisionMapManager.GetTexture("white_pixel");


			ResetShip();
		}
		public override bool IsFitValid()
		{
			return false;
		}
	}
}
