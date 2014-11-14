using GameLogicLibrary.Mobiles.Ships;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ClientLogicLibrary.Overlays
{
	public class ButtonToggleShip : ButtonToggle
	{
		public Ship ButtonShip { get; private set; }
		/// <summary>
		/// Constructs a new menu entry with the specified text.
		/// </summary>
		public ButtonToggleShip(ButtonOverlay myOverlay, SpriteFont font, string text, Vector2 position, Ship buttonShip)
			: base(myOverlay, font, text, position)
		{
			ButtonShip = buttonShip;
		}
	}
}
