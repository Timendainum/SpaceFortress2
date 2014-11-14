using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ClientLogicLibrary.Overlays
{
	public class ButtonToggle : Button
	{
		public bool IsActivated { get; private set; }
		
		/// <summary>
		/// Constructs a new menu entry with the specified text.
		/// </summary>
		public ButtonToggle(ButtonOverlay myOverlay, SpriteFont font, string text, Vector2 position)
			: base(myOverlay, font, text, position)
		{
			
		}

		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			if (!IsActivated)
				base.Draw(spriteBatch, gameTime);
			else
			{
				// Draw the selected entry in yellow, otherwise white.
				Color color = IsHovered ? Color.YellowGreen : Color.Green;

				// Pulsate the size of the selected menu entry.
				double time = gameTime.TotalGameTime.TotalSeconds;

				float pulsate = (float)Math.Sin(time * 6) + 1;

				float scale = 1 + pulsate * 0.05f * selectionFade;

				// Modify the alpha to fade text out during transitions.
				//color *= MyScreen.TransitionAlpha;

				spriteBatch.DrawString(Font, Text, ScreenPosition, color, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
			}
		}

		protected override void OnClick()
		{
			ToggleActivated();
			base.OnClick();
		}

		public void SetActivated(bool value)
		{
			IsActivated = value;
		}

		public void ToggleActivated()
		{
			if (IsActivated)
				IsActivated = false;
			else
				IsActivated = true;
		}
	}
}
