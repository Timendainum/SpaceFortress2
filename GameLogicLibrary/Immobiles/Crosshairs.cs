using System;
using GameLogicLibrary.Simulation;
using Microsoft.Xna.Framework;

namespace GameLogicLibrary.Immobiles
{

	public class Crosshairs : Immobile
	{
		public MoonType Type;
		
		public Crosshairs(Vector2 location)
		{
			Random r = new Random();
			Size = new Vector2(1000, 1000);

			Collidable = false;

			WorldLocation = location;

		}
	}
}
