using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ClientLogicLibrary.Graphics
{
	public class AnimationStrip
	{
		#region declarations
		public List<AnimationFrame> Frames = new List<AnimationFrame>();
		public float FrameTimer = 0.0f;
		private int _CurrentFrame = 0;
		public int CurrentFrame
		{
			get
			{ return _CurrentFrame; }
			set
			{ _CurrentFrame = (int)MathHelper.Clamp(value, 0, Frames.Count); }
		}
		public bool IsLooping = true;
		public bool FinishedPlaying = false;
		#endregion

		#region constrctors
		public AnimationStrip()
		{
		}

		public AnimationStrip(Texture2D texture, Rectangle initialFrame, int frameCount, float fps)
		{
			for (int i = 0; i < frameCount; i++)
			{
				Frames.Add(new AnimationFrame(texture, new Rectangle(initialFrame.X + (i * initialFrame.Width), initialFrame.Y, initialFrame.Width, initialFrame.Height), 1 / fps));
			}
		}
		#endregion

		public void Play()
		{
			CurrentFrame = 0;
			FinishedPlaying = false;
		}

		public void Update(GameTime gameTime)
		{
			float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

			FrameTimer += elapsed;

			if (FrameTimer >= Frames[CurrentFrame].PlayTime)
			{
				CurrentFrame++;
				if (CurrentFrame >= Frames.Count)
				{
					if (IsLooping)
					{
						CurrentFrame = 0;
					}
					else
					{
						CurrentFrame = Frames.Count - 1;
						FinishedPlaying = true;
					}
				}

				FrameTimer = 0f;
			}
		}
	}
}
