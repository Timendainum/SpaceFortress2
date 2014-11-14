using GameLogicLibrary.Simulation;
using Microsoft.Xna.Framework;

namespace GameLogicLibrary.Immobiles
{
	public enum MoonType { Small, Medium, Large };

	public class Moon : Immobile
	{
		public MoonType Type;

		public Moon(Vector2 location)
		{
			int type = RandomManager.TheRandom.Next(0, 3);
			switch (type)
			{
				case 0:
					Type = MoonType.Large;
					Size = new Vector2(800, 800);
					CollisionRadius = 400;
					CollisionMap = CollisionMapManager.GetTexture("moon_large");
					break;
				case 1:
					Type = MoonType.Medium;
					Size = new Vector2(585, 585);
					CollisionRadius = 375;
					CollisionMap = CollisionMapManager.GetTexture("moon_medium");
					break;
				case 2:
					Type = MoonType.Small;
					Size = new Vector2(272, 370);
					CollisionRadius = 210;
					CollisionMap = CollisionMapManager.GetTexture("moon_small");
					break;
			}

			Collidable = true;
			WorldLocation = location;
			
		}
		
		public Moon(Vector2 location, MoonType type)
		{
			Type = type;
			
			switch (type)
			{
				case  MoonType.Large:
					Size = new Vector2(800, 800);
					CollisionRadius = 400;
					CollisionMap = CollisionMapManager.GetTexture("moon_large");
					break;
				case MoonType.Medium:
					Size = new Vector2(585, 585);
					CollisionRadius = 375;
					CollisionMap = CollisionMapManager.GetTexture("moon_medium");
					break;
				case MoonType.Small:
					Size = new Vector2(272, 370);
					CollisionRadius = 210;
					CollisionMap = CollisionMapManager.GetTexture("moon_small");
					break;
			}

			Collidable = true;
			WorldLocation = location;
			

		}
	}
}
