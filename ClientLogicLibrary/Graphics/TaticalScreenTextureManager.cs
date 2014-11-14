using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ClientLogicLibrary.Graphics
{
	public static class TaticalScreenTextureManager
	{
		private static Dictionary<string, SpriteFont> _Fonts = new Dictionary<string, SpriteFont>();
		private static Dictionary<string, Texture2D> _Textures = new Dictionary<string, Texture2D>();

		public static void Init(ContentManager content)
		{
			Unload();

			//Fonts
			_Fonts["kootenay14"] = content.Load<SpriteFont>(@"Fonts\kootenay14");
			_Fonts["kootenay36"] = content.Load<SpriteFont>(@"Fonts\kootenay36");
			_Fonts["lindsey14"] = content.Load<SpriteFont>(@"Fonts\lindsey14");
			_Fonts["miramonte14"] = content.Load<SpriteFont>(@"Fonts\miramonte14");
			_Fonts["pericles14"] = content.Load<SpriteFont>(@"Fonts\pericles14");
			_Fonts["periclesLight14"] = content.Load<SpriteFont>(@"Fonts\periclesLight14");
			_Fonts["pescadero14"] = content.Load<SpriteFont>(@"Fonts\pescadero14");

			//Textures
			//Background
			//_Textures["planet_bug"] = content.Load<Texture2D>(@"Sprites\Background\planet_bug");
			_Textures["planet_human"] = content.Load<Texture2D>(@"Sprites\Background\planet_human");
			//_Textures["planet_red"] = content.Load<Texture2D>(@"Sprites\Background\planet_red");
			_Textures["planet_red_small"] = content.Load<Texture2D>(@"Sprites\Background\planet_red_small");
			//_Textures["planet_shad"] = content.Load<Texture2D>(@"Sprites\Background\planet_shad");
			_Textures["planet_shad_small"] = content.Load<Texture2D>(@"Sprites\Background\planet_shad_small");
			//_Textures["planet_yellowblue"] = content.Load<Texture2D>(@"Sprites\Background\planet_yellowblue");
			_Textures["planet_yellowblue_small"] = content.Load<Texture2D>(@"Sprites\Background\planet_yellowblue_small");
			//_Textures["planet_jungle"] = content.Load<Texture2D>(@"Sprites\Background\planet_jungle");
			_Textures["planet_jungle_small"] = content.Load<Texture2D>(@"Sprites\Background\planet_jungle_small");

			_Textures["star_orange"] = content.Load<Texture2D>(@"Sprites\star_orange");
			_Textures["cloudlayer_big"] = content.Load<Texture2D>(@"Sprites\cloudlayer_big");
			_Textures["cloudlayer_tile"] = content.Load<Texture2D>(@"Sprites\cloudlayer_tile");
			_Textures["starfield"] = content.Load<Texture2D>(@"Sprites\starfield");
			_Textures["starfield_bigstars"] = content.Load<Texture2D>(@"Sprites\starfield_bigstars");

			//Immobiles
			_Textures["moons"] = content.Load<Texture2D>(@"Sprites\immobile_moon");
			_Textures["crosshairs"] = content.Load<Texture2D>(@"Sprites\sprites_crosshair");

			//Mobiles
			_Textures["playership"] = content.Load<Texture2D>(@"Sprites\sprite_playership");
			_Textures["projectile_plasmablast"] = content.Load<Texture2D>(@"Sprites\projectile_plasmablast");
			_Textures["projectile_orb"] = content.Load<Texture2D>(@"Sprites\projectile_orb");
			_Textures["projectile_sabot"] = content.Load<Texture2D>(@"Sprites\projectile_sabot");

			//Ships
			_Textures["ship_human_fighter"] = content.Load<Texture2D>(@"Sprites\Ships\ship_human_fighter");
			_Textures["ship_human_frigate_1"] = content.Load<Texture2D>(@"Sprites\Ships\ship_human_frigate_1");
			_Textures["ship_human_cruiser_1"] = content.Load<Texture2D>(@"Sprites\Ships\ship_human_cruiser_1");
			_Textures["ship_human_battleship_1"] = content.Load<Texture2D>(@"Sprites\Ships\ship_human_battleship_1");
			_Textures["ship_human_capital_1"] = content.Load<Texture2D>(@"Sprites\Ships\ship_human_capital_1");

			_Textures["ship_alien_fighter"] = content.Load<Texture2D>(@"Sprites\Ships\ship_alien_fighter");
			_Textures["ship_alien_frigate_1"] = content.Load<Texture2D>(@"Sprites\Ships\ship_alien_frigate_1");
			_Textures["ship_alien_cruiser_1"] = content.Load<Texture2D>(@"Sprites\Ships\ship_alien_cruiser_1");

			//Stations
			_Textures["human_station1_level0"] = content.Load<Texture2D>(@"Sprites\Stations\human_station1_level0");
			_Textures["human_station1_level1"] = content.Load<Texture2D>(@"Sprites\Stations\human_station1_level1");
			_Textures["human_station1_level2"] = content.Load<Texture2D>(@"Sprites\Stations\human_station1_level2");
			_Textures["human_station1_level3"] = content.Load<Texture2D>(@"Sprites\Stations\human_station1_level3");

			_Textures["alien_station1_level0"] = content.Load<Texture2D>(@"Sprites\Stations\alien_station1_level0");
			_Textures["alien_station1_level1"] = content.Load<Texture2D>(@"Sprites\Stations\alien_station1_level1");
			_Textures["alien_station1_level2"] = content.Load<Texture2D>(@"Sprites\Stations\alien_station1_level2");

			//Particles
			_Textures["square_white"] = content.Load<Texture2D>(@"Sprites\square_white");
			_Textures["white_pixel"] = content.Load<Texture2D>(@"Sprites\white_pixel");

			//Hud
			_Textures["marker_basic"] = content.Load<Texture2D>(@"Sprites\marker_basic");
			_Textures["marker_basic02"] = content.Load<Texture2D>(@"Sprites\marker_basic02");

			//mini map
			_Textures["minimap"] = content.Load<Texture2D>(@"Sprites\minimap");
			_Textures["minimap_large"] = content.Load<Texture2D>(@"Sprites\minimap_large");
			_Textures["minimap_icons"] = content.Load<Texture2D>(@"Sprites\minimap_icons");

			_Textures["healthbar_small"] = content.Load<Texture2D>(@"Sprites\healthbar_small");
		}

		public static void Unload()
		{
			_Fonts.Clear();
			_Textures.Clear();
		}

		public static Texture2D GetTexture(string name)
		{
			return _Textures[name];
		}

		public static SpriteFont GetFont(string name)
		{
			return _Fonts[name];
		}

	}
}
