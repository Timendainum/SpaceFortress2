using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameLogicLibrary.Simulation
{
	public static class CollisionMapManager
	{
		private static Dictionary<string, Texture2D> _CollisionMaps = new Dictionary<string, Texture2D>();

		public static void Init(ContentManager content)
		{
			_CollisionMaps["moon_large"] = content.Load<Texture2D>(@"Sprites\Collision\moon_large_collision");
			_CollisionMaps["moon_medium"] = content.Load<Texture2D>(@"Sprites\Collision\moon_medium_collision");
			_CollisionMaps["moon_small"] = content.Load<Texture2D>(@"Sprites\Collision\moon_small_collision");

			_CollisionMaps["projectile_plasmablast"] = content.Load<Texture2D>(@"Sprites\Collision\projectile_plasmablast_collision");
			_CollisionMaps["projectile_orb"] = content.Load<Texture2D>(@"Sprites\Collision\projectile_orb_collision");
			_CollisionMaps["projectile_sabot"] = content.Load<Texture2D>(@"Sprites\Collision\projectile_sabot_collision");

			_CollisionMaps["white_pixel"] = content.Load<Texture2D>(@"Sprites\white_pixel");

			_CollisionMaps["ship_human_fighter"] = content.Load<Texture2D>(@"Sprites\Collision\ship_human_fighter_collision");
			_CollisionMaps["ship_human_frigate_1"] = content.Load<Texture2D>(@"Sprites\Collision\ship_human_frigate_1_collision");
			_CollisionMaps["ship_human_cruiser_1"] = content.Load<Texture2D>(@"Sprites\Collision\ship_human_cruiser_1_collision");
			_CollisionMaps["ship_human_battleship_1"] = content.Load<Texture2D>(@"Sprites\Collision\ship_human_battleship_1_collision");
			_CollisionMaps["ship_human_capital_1"] = content.Load<Texture2D>(@"Sprites\Collision\ship_human_capital_1_collision");

			_CollisionMaps["ship_alien_fighter"] = content.Load<Texture2D>(@"Sprites\Collision\ship_alien_fighter_collision");
			_CollisionMaps["ship_alien_frigate_1"] = content.Load<Texture2D>(@"Sprites\Collision\ship_alien_frigate_1_collision");
			_CollisionMaps["ship_alien_cruiser_1"] = content.Load<Texture2D>(@"Sprites\Collision\ship_alien_cruiser_1_collision");
			
			_CollisionMaps["human_station1"] = content.Load<Texture2D>(@"Sprites\Collision\human_station1_collision");
			_CollisionMaps["alien_station1"] = content.Load<Texture2D>(@"Sprites\Collision\alien_station1_collision");
		}

		public static Texture2D GetTexture(string name)
		{
			return _CollisionMaps[name];
		}

	}
}
