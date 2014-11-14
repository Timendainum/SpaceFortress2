using ClientLogicLibrary.ScreenManagement;
using GameLogicLibrary.Anomalies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ClientLogicLibrary.Overlays.DockedScreenOverlays
{
	public class AnomalyListItem : Overlay
	{
		public Anomaly MyAnomaly { get; private set; }

		public AnomalyListItem(Anomaly anom)
		{
			MyAnomaly = anom;
		}

		#region RegionName
		public override void HandleInput(InputState input)
		{

		}

		public override void Update(GameTime gameTime)
		{

		}

		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{

		}
		#endregion


	}
}
