using ClientLogicLibrary.Graphics;
using GameLogicLibrary.Mobiles.Modules.Weapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ClientLogicLibrary.Mobiles
{
	public class ClientPlasmaBlast : ClientProjectile
	{
		public PlasmaBlast ServerPlasmaBlast;
		Sprite PlasmaSprite;

		public ClientPlasmaBlast(PlasmaBlast serverObject)
		{
			ServerPlasmaBlast = serverObject;
			ServerMobile = serverObject;
			PlasmaSprite = new Sprite(serverObject.WorldLocation, new Vector2(12, 12), TaticalScreenTextureManager.GetTexture("projectile_plasmablast"), new Rectangle(0, 0, 12, 12));
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			PlasmaSprite.Draw(spriteBatch);
		}
		public override void Update(GameTime gameTime)
		{
			//Update graphics based on server state
			PlasmaSprite.Rotation = ServerPlasmaBlast.Rotation;
			PlasmaSprite.WorldLocation = ServerPlasmaBlast.WorldLocation;
			PlasmaSprite.Update(gameTime);
		}

		#region helpers
		public override Rectangle GetWorldRectangle()
		{
			return ServerPlasmaBlast.WorldRectangle;
		}
		#endregion
	}
}
