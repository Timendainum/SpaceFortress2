using ClientLogicLibrary.Graphics;
using GameLogicLibrary.Simulation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ClientLogicLibrary.Overlays
{
	public class BackgroundObject
	{
		Sprite backgroundObject;
		Vector2 objectCenter;
		Vector2 objectLocation;
		float objectParalaxFactor;
		Vector2 cameraDifference;


		//constructor
		public BackgroundObject(Vector2 location, Vector2 size, string textureName, float paralaxFactor)
		{
			objectParalaxFactor = paralaxFactor;
			//worldCenter needs to be adjusted to offset texture size
			objectCenter = location;
			objectLocation = location - (size/2);

			backgroundObject = new Sprite(Vector2.Zero, size, TaticalScreenTextureManager.GetTexture(textureName), new Rectangle(0, 0, (int)size.X, (int)size.Y));
		}

		public void Update(GameTime gameTime)
		{
			//Update planet
			//backgroundObject.WorldLocation = ((Camera.WorldCenter) * objectParalaxFactor) + objectLocation;

			cameraDifference = Camera.WorldCenter - objectCenter;
			backgroundObject.WorldLocation = (cameraDifference * objectParalaxFactor) + objectLocation;
		}

		public void Draw(SpriteBatch spriteBatch)
		{

			backgroundObject.Draw(spriteBatch);

		}
	}
}
