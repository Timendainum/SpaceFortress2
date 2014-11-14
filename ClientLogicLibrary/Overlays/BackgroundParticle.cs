using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ClientLogicLibrary.Graphics;
using GameLogicLibrary.Simulation;

namespace ClientLogicLibrary.Overlays
{
	public class BackgroundParticle : MobileSprite
	{
		public float VelocityFactor;
		private static Color[] colors = { Color.White, Color.Yellow, Color.Wheat, Color.WhiteSmoke, Color.SlateGray };
		private Random _rand;
		
		public BackgroundParticle(Vector2 worldLocation, Texture2D texture, Rectangle textureSource, Random rand)
			: base(worldLocation, texture, textureSource)
		{
			_rand = rand;
			
			VelocityFactor = rand.Next(10, 30) * 0.5f;

			Color starColor = colors[rand.Next(0, colors.Length)];
			starColor *= (float)(rand.Next(30, 80) / 100f);
			TintColor = starColor;
			
		}

		public override void Update(GameTime gameTime)
		{
			Velocity = Camera.CurrentVelocity * VelocityFactor;
			Vector2 newLocation = Vector2.Zero;

			//x constraint
			if (ScreenLocation.X > Camera.ViewPortWidth)
			{
				newLocation = new Vector2(0, _rand.Next(0, Camera.ViewPortHeight));
				WorldLocation = Camera.TransformCameraToWorld(newLocation);
			}
			else if (ScreenLocation.X < 0)
			{
				newLocation = new Vector2(Camera.ViewPortWidth, _rand.Next(0, Camera.ViewPortHeight));
				WorldLocation = Camera.TransformCameraToWorld(newLocation);
			}

			//y constraint
			else if (ScreenLocation.Y > Camera.ViewPortHeight)
			{
				newLocation = new Vector2(_rand.Next(0, Camera.ViewPortWidth), 0);
				WorldLocation = Camera.TransformCameraToWorld(newLocation);
			}
			else if (ScreenLocation.Y < 0)
			{
				newLocation = new Vector2(_rand.Next(0, Camera.ViewPortWidth), Camera.ViewPortHeight);
				WorldLocation = Camera.TransformCameraToWorld(newLocation);
			}

			base.Update(gameTime);
		}
	}
}
