using ClientLogicLibrary.Graphics;
using GameLogicLibrary.Immobiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ClientLogicLibrary.Immobiles
{
	public class ClientHumanStation1 : ClientImmobile
	{
		public HumanStation1 ServerStation;
		public Sprite Level0Sprite;
		public Sprite Level1Sprite;
		public Sprite Level2Sprite;
		public Sprite Level3Sprite;

		#region costructor
		public ClientHumanStation1(HumanStation1 serverStation)
		{
			ServerStation = serverStation;
			ServerImmobile = ServerStation;
			Level0Sprite = new Sprite(ServerStation.WorldLocation, ServerStation.Size, TaticalScreenTextureManager.GetTexture("human_station1_level0"), new Rectangle(0, 0, (int)ServerStation.Size.X, (int)ServerStation.Size.Y));
			Level1Sprite = new Sprite(ServerStation.WorldLocation, ServerStation.Size, TaticalScreenTextureManager.GetTexture("human_station1_level1"), new Rectangle(0, 0, (int)ServerStation.Size.X, (int)ServerStation.Size.Y));
			Level2Sprite = new Sprite(ServerStation.WorldLocation, ServerStation.Size, TaticalScreenTextureManager.GetTexture("human_station1_level2"), new Rectangle(0, 0, (int)ServerStation.Size.X, (int)ServerStation.Size.Y));
			Level3Sprite = new Sprite(ServerStation.WorldLocation, ServerStation.Size, TaticalScreenTextureManager.GetTexture("human_station1_level3"), new Rectangle(0, 0, (int)ServerStation.Size.X, (int)ServerStation.Size.Y));

			Level2Sprite.Rotation = MathHelper.Pi;
		}
		#endregion

		#region xna methods
		public override void Draw(SpriteBatch spriteBatch)
		{
			Level0Sprite.Draw(spriteBatch);
			Level1Sprite.Draw(spriteBatch);
			Level2Sprite.Draw(spriteBatch);
			Level3Sprite.Draw(spriteBatch);
		}

		public override void Update(GameTime gameTime)
		{
			float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

			Level2Sprite.Rotation += (-0.05f * elapsed);
			Level3Sprite.Rotation += (0.025f * elapsed);

			Level1Sprite.Rotation = ServerStation.Rotation;
			Level0Sprite.Update(gameTime);
			Level1Sprite.Update(gameTime);
			Level2Sprite.Update(gameTime);
			Level3Sprite.Update(gameTime);
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
