using ClientLogicLibrary.Graphics;
using GameLogicLibrary.Mobiles.Modules.Weapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ClientLogicLibrary.Mobiles
{
	public class ClientBlast : ClientProjectile
	{
		public Blast ServerBlast;
		Sprite BlastSprite;

		public ClientBlast(Blast serverObject)
		{
			ServerBlast = serverObject;
			ServerMobile = serverObject;
			BlastSprite = new Sprite(serverObject.WorldLocation, new Vector2(2, 1), TaticalScreenTextureManager.GetTexture("white_pixel"), new Rectangle(0, 0, 1, 1));
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			BlastSprite.Draw(spriteBatch);
		}
		public override void Update(GameTime gameTime)
		{
			//Update graphics based on server state
			BlastSprite.Rotation = ServerBlast.Rotation;
			BlastSprite.WorldLocation = ServerBlast.WorldLocation;
			BlastSprite.Update(gameTime);
		}

		#region helpers
		public override Rectangle GetWorldRectangle()
		{
			return ServerBlast.WorldRectangle;
		}
		#endregion
	}
}
