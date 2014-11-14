using System.Collections.Generic;
using GameLogicLibrary.Mobiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ClientLogicLibrary.Mobiles
{
	public abstract class ClientShipPilot : ClientMobile
	{
		public ShipRenderer shipRenderer;

		public ClientShipPilot(ShipPilot serverPilot)
		{
			ServerMobile = serverPilot;
			NewShipRenderer();
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			shipRenderer.Draw(spriteBatch);
		}


		public override void Update(GameTime gameTime)
		{
			shipRenderer.Update(gameTime);
		}

		#region helpers
		public override Rectangle GetWorldRectangle()
		{
			return shipRenderer.GetWorldRectangle();
		}

		public void NewShipRenderer()
		{
			shipRenderer = ShipRendererFactory.Create((ShipPilot)ServerMobile);
		}
		#endregion
	}
}
