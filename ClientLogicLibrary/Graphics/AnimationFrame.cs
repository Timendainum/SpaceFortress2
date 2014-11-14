using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ClientLogicLibrary.Graphics
{
	public class AnimationFrame
	{
		public Texture2D Texture;
		public Rectangle SourceRectangle;
		public float PlayTime = 0.0f;

		public AnimationFrame(Texture2D texture, Rectangle sourceRectangle, float playTime)
		{
			Texture = texture;
			SourceRectangle = sourceRectangle;
			PlayTime = playTime;
		}

	}
}
