using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using GameLogicLibrary.Mobiles;

namespace ClientLogicLibrary.Mobiles
{
	public abstract class ShipRenderer
	{
		protected ShipPilot _ServerPilot;
		protected HeathBar _healthBar;


		public ShipRenderer(ShipPilot serverPilot)
		{
			_ServerPilot = serverPilot;
			_healthBar = new HeathBar(serverPilot);
		}

		public abstract Rectangle GetWorldRectangle();

		public virtual void Update(GameTime gameTime)
		{
			_healthBar.Update(gameTime);
		}

		public virtual void Draw(SpriteBatch spriteBatch)
		{
			_healthBar.Draw(spriteBatch);
		}
	}
}
