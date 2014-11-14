using ClientLogicLibrary.ScreenManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameLogicLibrary.Maths;
using ClientLogicLibrary.Effects;

namespace ClientLogicLibrary.Overlays
{
	public class ScrollingButtonOverlay : ButtonOverlay
	{
		public Vector2 ItemSize = Vector2.Zero;
		float scrollOffset = 0f;

		public ScrollingButtonOverlay()
		{
			
		}

		#region xna
		public override void HandleInput(InputState input)
		{
			Vector2 mousePosition = new Vector2(input.CurrentMouseState.X, input.CurrentMouseState.Y);
			if (input.IsNewKeyPress(Keys.OemPlus) || (input.IsMouseScrollUp() && MathsHelper.IsVector2InsideRectangle(mousePosition, GetScreenRectangle())))
			{
				ScrollUp();
			}
			if (input.IsNewKeyPress(Keys.OemMinus) || (input.IsMouseScrollDown() && MathsHelper.IsVector2InsideRectangle(mousePosition, GetScreenRectangle())))
			{
				ScrollDown();
			}


			foreach (Button item in Buttons)
			{
				item.HandleInput(input);
			}
		}

		public override void Update(GameTime gameTime)
		{
			//Update position
			int y = 0;
			foreach (Button item in Buttons)
			{
				item.Position = new Vector2(0, y + scrollOffset);
				item.Update(gameTime);
				y += (int)ItemSize.Y;
			}
		}

		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			foreach (Button item in Buttons)
			{
				if (GetScreenRectangle().Intersects(item.GetScreenRectangle()))
					item.Draw(spriteBatch, gameTime);
			}
		}
		#endregion

		private void ScrollUp()
		{
			if (scrollOffset < 0)
			{
				scrollOffset += ItemSize.Y;
				SoundManager.PlayEffect("manu_advance", 1f);
			}
			else
				SoundManager.PlayEffect("menu_bad_select", 1f);
		}

		private void ScrollDown()
		{
			if (Buttons.Count > 0 && Buttons[Buttons.Count - 1].ScreenPosition.Y >= (ScreenPosition.Y + Height - ItemSize.Y))
			{
				scrollOffset -= ItemSize.Y;
				SoundManager.PlayEffect("manu_back", 1f);
			}
			else
				SoundManager.PlayEffect("menu_bad_select", 1f);
		}
	}
}
