using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using GameLogicLibrary.Simulation;

namespace ClientLogicLibrary.Graphics
{
	public class AnimatedSprite : Sprite
	{
		public Dictionary<string, AnimationStrip> Animations = new Dictionary<string, AnimationStrip>();
		public string PlayingAnimation = string.Empty;
		public string NextAnimation = string.Empty;

		public AnimatedSprite(Vector2 worldLocation, Vector2 size, Texture2D texture, Rectangle textureSource, string name, int frameCount, float fps)
		: base(worldLocation, size, texture, textureSource)
		{
			SetAnimation(name, texture, textureSource, frameCount, fps);
			PlayingAnimation = name;
		}



		public void SetAnimation(string name, Texture2D texture, Rectangle initialFrame, int frameCount, float fps)
		{
			Animations[name] = new AnimationStrip(texture, initialFrame, frameCount, fps);
		}

		public virtual void Update(GameTime gameTime)
		{
			Animations[PlayingAnimation].Update(gameTime);
		}

		public virtual void Draw(SpriteBatch spriteBatch)
		{
			if (Camera.ObjectIsVisible(WorldRectangle))
			{
				AnimationStrip strip = Animations[PlayingAnimation];
				
				spriteBatch.Draw(
					strip.Frames[strip.CurrentFrame].Texture,
					ScreenCenter,
					strip.Frames[strip.CurrentFrame].SourceRectangle,
					TintColor,
					Rotation,
					RelativeCenter,
					Scale,
					SpriteEffects.None,
					0.0f);
			}
		}

	}
}
