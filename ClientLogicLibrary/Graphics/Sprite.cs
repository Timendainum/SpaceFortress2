using GameLogicLibrary.Immobiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ClientLogicLibrary.Graphics
{
    public class Sprite : Immobile
    {
        #region Declarations
        public Texture2D Texture;
		public Rectangle TextureSource;
		public Color TintColor = Color.White;
		public float Scale = 1.0f;
		
        #endregion

        #region Constructors
        public Sprite(Vector2 worldLocation, Vector2 size, Texture2D texture, Rectangle textureSource)
        {
			WorldLocation = worldLocation;
			Texture = texture;
			TextureSource = textureSource;
			Size = size;
		}
		#endregion

		#region Positional Properties
		
		#endregion


		#region Update and Draw Methods
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

		public virtual void Update(GameTime gameTime)
		{
			return;
		}
        #endregion

    }
}
