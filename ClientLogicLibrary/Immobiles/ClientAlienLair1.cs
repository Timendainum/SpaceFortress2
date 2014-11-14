using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameLogicLibrary.Immobiles.Lairs;
using ClientLogicLibrary.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ClientLogicLibrary.Immobiles
{
	public class ClientAlienLair1 : ClientImmobile
	{
		public AlienLair1 ServerStation;
		public Sprite Level0Sprite;
		public Sprite Level1Sprite;
		public Sprite Level2Sprite;

		#region costructor
		public ClientAlienLair1(AlienLair1 serverStation)
		{
			ServerStation = serverStation;
			ServerImmobile = ServerStation;
			Level0Sprite = new Sprite(ServerStation.WorldLocation, ServerStation.Size, TaticalScreenTextureManager.GetTexture("alien_station1_level0"), new Rectangle(0, 0, (int)ServerStation.Size.X, (int)ServerStation.Size.Y));
			Level1Sprite = new Sprite(ServerStation.WorldLocation, ServerStation.Size, TaticalScreenTextureManager.GetTexture("alien_station1_level1"), new Rectangle(0, 0, (int)ServerStation.Size.X, (int)ServerStation.Size.Y));
			Level2Sprite = new Sprite(ServerStation.WorldLocation, ServerStation.Size, TaticalScreenTextureManager.GetTexture("alien_station1_level2"), new Rectangle(0, 0, (int)ServerStation.Size.X, (int)ServerStation.Size.Y));

			Level2Sprite.Rotation = MathHelper.Pi;
		}
		#endregion

		#region xna methods
		public override void Draw(SpriteBatch spriteBatch)
		{
			Level0Sprite.Draw(spriteBatch);
			Level1Sprite.Draw(spriteBatch);
			Level2Sprite.Draw(spriteBatch);
		}

		public override void Update(GameTime gameTime)
		{
			float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

			Level1Sprite.Rotation = ServerStation.Rotation;
			Level2Sprite.Rotation += (-0.05f * elapsed);

			Level0Sprite.Update(gameTime);
			Level1Sprite.Update(gameTime);
			Level2Sprite.Update(gameTime);
		}
		#endregion

		#region helpers
		public override Rectangle GetWorldRectangle()
		{
			return ServerStation.WorldRectangle;
		}
		#endregion
	}
}
