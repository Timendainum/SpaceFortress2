using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ClientLogicLibrary.Graphics
{
	public static class DockedScreenTextureManager
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

			_Textures["scannerFrame"] = content.Load<Texture2D>(@"Sprites\scannerFrame");
			_Textures["minimap_icons"] = content.Load<Texture2D>(@"Sprites\minimap_icons");
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
