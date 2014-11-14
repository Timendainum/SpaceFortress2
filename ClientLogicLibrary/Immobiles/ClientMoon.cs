using System.Collections.Generic;
using GameLogicLibrary.Immobiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ClientLogicLibrary.Graphics;

namespace ClientLogicLibrary.Immobiles
{
	public class ClientMoon : ClientImmobile
	{
		public Moon ServerMoon;
		public Sprite ImmobileSprite;

		#region properties
		
		#endregion

		#region costructor
		public ClientMoon(Moon serverMoon)
		{
			ServerMoon = serverMoon;
			ServerImmobile = serverMoon;
			ImmobileSprite = new Sprite(ServerMoon.WorldLocation, ServerMoon.Size, TaticalScreenTextureManager.GetTexture("moons"), GetTextureSource(ServerMoon.Type));
		}
		#endregion

		#region xna methods
		public override void Draw(SpriteBatch spriteBatch)
		{
			ImmobileSprite.Draw(spriteBatch);
		}

		public override void Update(GameTime gameTime)
		{
			ImmobileSprite.Update(gameTime);
		}
		#endregion

		#region helpers
		public override Rectangle GetWorldRectangle()
		{
			return ServerMoon.WorldRectangle;
		}
		#endregion

		#region graphics helpers
		private Rectangle GetTextureSource(MoonType type)
		{
			switch (type)
			{
				case MoonType.Large:
					return new Rectangle(0, 0, 800, 800);
					break;
				case MoonType.Medium:
					return new Rectangle(0, 825, 585, 585);
					break;
				case MoonType.Small:
					return new Rectangle(0, 1445, 272, 370);
					break;
			}


			//fail
			return new Rectangle(0,0,0,0);
		}
		#endregion
	}
}

//Luna 0,0 799,799

//Hyperion 817,0 1416,593

//Phobos 1434,0 2034,298
