using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ClientLogicLibrary.Simulation
{
	public abstract class ClientEntity
	{
		public abstract Rectangle GetWorldRectangle();
		
		//public abstract void Update(GameTime gameTime);
		public abstract void Draw(SpriteBatch spriteBatch);
		public abstract void Update(GameTime gameTime);
	}
}
