using GameLogicLibrary.Mobiles.Modules;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ClientLogicLibrary.Overlays
{
	public class ButtonToggleModule : ButtonToggle
	{
		public Module ButtonModule { get; private set; }
		/// <summary>
		/// Constructs a new menu entry with the specified text.
		/// </summary>
		public ButtonToggleModule(ButtonOverlay myOverlay, SpriteFont font, string text, Vector2 position, Module buttonModule)
			: base(myOverlay, font, text, position)
		{
			ButtonModule = buttonModule;
		}
	}
}
