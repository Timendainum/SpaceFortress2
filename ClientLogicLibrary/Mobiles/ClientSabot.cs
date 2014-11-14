using ClientLogicLibrary.Graphics;
using GameLogicLibrary.Mobiles.Modules.Weapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ClientLogicLibrary.Mobiles
{
	public class ClientSabot : ClientProjectile
	{
		public Sabot ServerSabot;
		Sprite SabotSprite;

		public ClientSabot(Sabot serverObject)
		{
			ServerSabot = serverObject;
			ServerMobile = serverObject;
			SabotSprite = new Sprite(ServerSabot.WorldLocation, new Vector2(11, 8), TaticalScreenTextureManager.GetTexture("projectile_sabot"), new Rectangle(0, 0, 11, 8));
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			SabotSprite.Draw(spriteBatch);
		}
		public override void Update(GameTime gameTime)
		{
			//Update graphics based on server state
			SabotSprite.Rotation = ServerSabot.Rotation;
			SabotSprite.WorldLocation = ServerSabot.WorldLocation;
			SabotSprite.Update(gameTime);
		}

		#region helpers
		public override Rectangle GetWorldRectangle()
		{
			return ServerSabot.WorldRectangle;
		}
		#endregion
	}
}
