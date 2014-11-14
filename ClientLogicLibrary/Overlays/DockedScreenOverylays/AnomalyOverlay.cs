using ClientLogicLibrary.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ClientLogicLibrary.ScreenManagement;
using ClientLogicLibrary.Overlays;

namespace ClientLogicLibrary.Overlays.DockedScreenOverlays
{
	
	public class AnomalyOverlay : Overlay
	{
		#region init
		public AnomalyOverlay(Vector2 screenPosition)
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
			spriteBatch.DrawString(DockedScreenTextureManager.GetFont("kootenay36"), "Anomaly Management", TransformOverlayToScreen(new Vector2(0, 0)), Color.White);

		}
		#endregion
	}
}
