using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ClientLogicLibrary.Simulation;
using GameLogicLibrary.Simulation;

namespace ClientLogicLibrary.Graphics
{
	public class MobileSprite : Mobile
	{
		 #region Declarations
        public Texture2D Texture;
		public Rectangle TextureSource;
		public Color TintColor = Color.White;
		public float Scale = 1.0f;
		
        #endregion

        #region Constructors
		public MobileSprite(Vector2 worldLocation, Texture2D texture, Rectangle textureSource)
        {
			WorldLocation = worldLocation;
			Texture = texture;
			TextureSource = textureSource;
			Size = new Vector2(textureSource.Width, textureSource.Height);
		}
		#endregion

		#region Positional Properties
		
		#endregion

		public Rectangle GetWorldRectangle()
		{
			return new Rectangle(
						WorldLocationX,
						WorldLocationY,
						Width,
						Height);
		}

		#region Update and Draw Methods
		public virtual void Update(GameTime gameTime)
		{
			base.Update(gameTime);
		}
		
		public virtual void Draw(SpriteBatch spriteBatch)
        {
			spriteBatch.Draw(
				Texture,
				ScreenCenter,
				TextureSource,
				TintColor,
				Rotation,
				RelativeCenter,
				Scale,
				SpriteEffects.None,
				0.0f);
        }
        #endregion
	}
}
