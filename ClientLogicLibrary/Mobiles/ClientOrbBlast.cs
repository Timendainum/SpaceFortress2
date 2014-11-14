using ClientLogicLibrary.Graphics;
using GameLogicLibrary.Mobiles.Modules.Weapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ClientLogicLibrary.Mobiles
{
	public class ClientOrbBlast : ClientProjectile
	{
		public OrbBlast ServerOrbBlast;
		Sprite OrbSprite;

		public ClientOrbBlast(OrbBlast serverObject)
		{
			ServerOrbBlast = serverObject;
			ServerMobile = serverObject;
			OrbSprite = new Sprite(serverObject.WorldLocation, new Vector2(45, 45), TaticalScreenTextureManager.GetTexture("projectile_orb"), new Rectangle(0, 0, 45, 45));
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			OrbSprite.Draw(spriteBatch);
		}
		public override void Update(GameTime gameTime)
		{
			//Update graphics based on server state
			OrbSprite.Rotation = ServerOrbBlast.Rotation;
			OrbSprite.WorldLocation = ServerOrbBlast.WorldLocation;
			OrbSprite.Update(gameTime);
		}

		#region helpers
		public override Rectangle GetWorldRectangle()
		{
			return ServerOrbBlast.WorldRectangle;
		}
		#endregion
	}
}
