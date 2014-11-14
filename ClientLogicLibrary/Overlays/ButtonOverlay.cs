using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ClientLogicLibrary.ScreenManagement;

namespace ClientLogicLibrary.Overlays
{
	public class ButtonOverlay : Overlay
	{
		#region Declarations

		List<Button> buttons = new List<Button>();
		public List<Button> Buttons
		{
			get { return buttons; }
		}

		public SpriteFont Font;
		public Button SelectedButton = null;
		#endregion

		#region Initialization

		public ButtonOverlay()
		{
		}
		#endregion

		#region Update and Draw

		public override void Update(GameTime gameTime)
		{
			foreach (Button button in Buttons)
			{
				button.Update(gameTime);
			}
		}

		public override void HandleInput(InputState input)
		{
			foreach (Button button in Buttons)
			{
				button.HandleInput(input);
			}
		}

		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			// Draw each menu entry in turn.
			foreach (Button button in Buttons)
			{
				button.Draw(spriteBatch, gameTime);
			}
		}

		#endregion

		public void UnActivateOtherButtons(Button caller)
		{
			ButtonToggle toggle;
			foreach (Button button in Buttons)
			{

				if (button != caller && button is ButtonToggle)
				{
					toggle = (ButtonToggle)button;
					toggle.SetActivated(false);
				}
			}
		}
	}
}
