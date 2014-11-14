using ClientLogicLibrary.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ClientLogicLibrary.ScreenManagement;

namespace ClientLogicLibrary.Overlays
{
	public class StationOverlay : Overlay
	{
		#region init
		public StationOverlay(Vector2 screenPosition)
		{
			ScreenPosition = screenPosition;

			
		}
		#endregion

		#region xna
		public override void Update(GameTime gameTime)
		{

		}

		public override void HandleInput(InputState input)
		{

		}
		
		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			spriteBatch.DrawString(DockedScreenTextureManager.GetFont("kootenay36"), "Station Management", TransformOverlayToScreen(new Vector2(0, 0)), Color.White);

		}
		#endregion
	}
}
