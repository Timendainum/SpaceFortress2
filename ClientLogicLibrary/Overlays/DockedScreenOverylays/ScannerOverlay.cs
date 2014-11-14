using ClientLogicLibrary.ScreenManagement;
using GameLogicLibrary.Immobiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ClientLogicLibrary.Overlays;

namespace ClientLogicLibrary.Overlays.DockedScreenOverlays
{
	
	public class ScannerOverlay : Overlay
	{
		MapOverlay scannerOverlay = null;
		#region init
		public ScannerOverlay(Vector2 screenPosition, Station myStation)
		{
			Width = 864;
			Height = 688;
			IsActive = true;
			ScreenPosition = screenPosition;
			scannerOverlay = new ClientLogicLibrary.Overlays.DockedScreenOverlays.MapOverlay(TransformOverlayToScreen(Vector2.Zero), myStation);
		}
		#endregion

		#region xna
		public override void Update(GameTime gameTime)
		{
			if (IsActive)
			{
				scannerOverlay.Update(gameTime);
			}
		}

		public override void HandleInput(InputState input)
		{
			if (IsActive)
			{
				scannerOverlay.HandleInput(input);
			}
		}

		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			if (IsActive)
			{
				//spriteBatch.DrawString(DockedScreenTextureManager.GetFont("kootenay36"), "Scanner", TransformOverlayToScreen(new Vector2(0, 0)), Color.White);
				scannerOverlay.Draw(spriteBatch, gameTime);
			}
		}

		#endregion
	}
}
