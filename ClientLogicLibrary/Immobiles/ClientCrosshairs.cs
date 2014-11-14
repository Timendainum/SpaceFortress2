using System.Collections.Generic;
using GameLogicLibrary.Immobiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ClientLogicLibrary.Graphics;

namespace ClientLogicLibrary.Immobiles
{
	public class ClientCrosshairs : ClientLogicLibrary.Immobiles.ClientImmobile
	{
		public Crosshairs ServerCrosshairs;
		public Sprite MainSprite;

		#region costructor
		public ClientCrosshairs(Crosshairs crosshairs)
		{
			ServerCrosshairs = crosshairs;
			ServerImmobile = crosshairs;
			MainSprite = new Sprite(ServerCrosshairs.WorldLocation, ServerCrosshairs.Size, TaticalScreenTextureManager.GetTexture("crosshairs"), new Rectangle(0, 0, 1000, 1000));
		}
		#endregion

		#region xna methods
		public override void Draw(SpriteBatch spriteBatch)
		{
			MainSprite.Draw(spriteBatch);
		}

		public override void Update(GameTime gameTime)
		{
			MainSprite.Update(gameTime);
		}
		#endregion

		#region helpers
		public override Rectangle GetWorldRectangle()
		{
			return ServerCrosshairs.WorldRectangle;
		}
		#endregion
	}
}

//Luna 0,0 799,799

//Hyperion 817,0 1416,593

//Phobos 1434,0 2034,298
