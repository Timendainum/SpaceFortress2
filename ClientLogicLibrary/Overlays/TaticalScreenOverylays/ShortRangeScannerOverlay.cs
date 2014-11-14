using ClientLogicLibrary.Graphics;
using ClientLogicLibrary.ScreenManagement;
using GameLogicLibrary.Immobiles;
using GameLogicLibrary.Maths;
using GameLogicLibrary.Mobiles;
using GameLogicLibrary.Simulation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ClientLogicLibrary.Overlays.TaticalScreenOverlays
{
	public struct TintColor
	{
		static TintColor()
		{
			
		}
	}
    public class ShortRangeScannerOverlay : Overlay
	{
		private Player _player;
		private const int RANGE = 2000;

		public ShortRangeScannerOverlay(Player player)
		{
			_player = player;
		}


		public override void Update(GameTime gameTime)
		{

		}

		public override void HandleInput(InputState input)
		{

		}

		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			foreach (Immobile imob in _player.CurrentSector.Immobiles)
			{
				if (imob.Collidable)
				{
					DrawMarker(spriteBatch, imob);
				}
			}

			foreach (Mobile mob in _player.CurrentSector.Mobiles)
			{
				if (!mob.Expired)
				{
					DrawMarker(spriteBatch, mob);
				}
			}
		}
		private void DrawMarker(SpriteBatch spriteBatch, Entity entity)
		{
			if (Vector2.Distance(_player.WorldCenter, entity.WorldCenter) > RANGE)
				return;
			
			float angle = MathsHelper.AbsoluteRotation(MathsHelper.DirectInterceptAngle(_player.WorldCenter, entity.WorldCenter));

			Texture2D texture;

			if (entity is Immobile)
			{
				texture = TaticalScreenTextureManager.GetTexture("marker_basic02");
			}
			else
			{
				texture = TaticalScreenTextureManager.GetTexture("marker_basic");
			}

			//Position based on circle
			//Vector2 screenCenter = MathsHelper.RotateAroundCircle(angle, 400f, Camera.TransformWorldToCamera(_player.WorldCenter));

			//Position based on screen square
			int frameFuffer = 36;
			Line topLine = new Line(new Vector2(Camera.ViewPort.X + frameFuffer, Camera.ViewPort.Y + frameFuffer), new Vector2(Camera.ViewPort.X + Camera.ViewPort.Width - frameFuffer, Camera.ViewPort.Y + frameFuffer));
			Line rightLine = new Line(new Vector2(Camera.ViewPort.X + Camera.ViewPort.Width - frameFuffer, Camera.ViewPort.Y + frameFuffer), new Vector2(Camera.ViewPort.X + Camera.ViewPort.Width - frameFuffer, Camera.ViewPort.Y + Camera.ViewPort.Height - frameFuffer));
			Line bottomLine = new Line(new Vector2(Camera.ViewPort.X + Camera.ViewPort.Width - frameFuffer, Camera.ViewPort.Y + Camera.ViewPort.Height - frameFuffer), new Vector2(Camera.ViewPort.X + frameFuffer, Camera.ViewPort.Y + Camera.ViewPort.Height - frameFuffer));
			Line leftLine = new Line(new Vector2(Camera.ViewPort.X + frameFuffer, Camera.ViewPort.Y + Camera.ViewPort.Height - frameFuffer), new Vector2(Camera.ViewPort.X + frameFuffer, Camera.ViewPort.Y + frameFuffer));
			float topLeftAngle = MathsHelper.AbsoluteRotation(MathsHelper.DirectInterceptAngle(Camera.WorldCenter, new Vector2(Camera.ViewPort.X + frameFuffer, Camera.ViewPort.Y + frameFuffer)));
			float topRightAngle = MathsHelper.AbsoluteRotation(MathsHelper.DirectInterceptAngle(Camera.WorldCenter, new Vector2(Camera.ViewPort.X + Camera.ViewPortWidth - frameFuffer, Camera.ViewPort.Y + frameFuffer)));
			float bottomRightAngle = MathsHelper.AbsoluteRotation(MathsHelper.DirectInterceptAngle(Camera.WorldCenter, new Vector2(Camera.ViewPort.X + Camera.ViewPortWidth - frameFuffer, Camera.ViewPort.Y + Camera.ViewPortHeight - frameFuffer)));
			float bottomLeftAngle = MathsHelper.AbsoluteRotation(MathsHelper.DirectInterceptAngle(Camera.WorldCenter, new Vector2(Camera.ViewPort.X + frameFuffer, Camera.ViewPort.Y + Camera.ViewPortHeight - frameFuffer)));
			
			Line interceptLine = new Line(_player.WorldCenter, entity.WorldCenter);
			Line screenLine = null;

			if (angle > topLeftAngle && angle <= topRightAngle)
			{
				screenLine = topLine;
			}
			else if (angle > topRightAngle || (angle >= 0.0f && angle <= bottomRightAngle))
			{
				screenLine = rightLine;
			}
			else if (angle > bottomRightAngle && angle <= bottomLeftAngle)
			{
				screenLine = bottomLine;
			}
			else if (angle > bottomLeftAngle && angle <= topLeftAngle)
			{
				screenLine = leftLine;
			}

			if (screenLine != null)
			{
				//Set screen center
				Vector2 screenCenter = MathsHelper.LineIntersectionPoint(interceptLine, screenLine);

				if (screenCenter != Vector2.Zero)
				{
					//Draw it!
					Rectangle sourceRectangle = new Rectangle(0, 0, 64, 40);
					float rotation = angle;
					Vector2 relativeCenter = new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2);
					float scale = 1.0f;

					spriteBatch.Draw(
						texture,
						Camera.TransformWorldToCamera(screenCenter),
						sourceRectangle,
						Color.White,
						rotation,
						relativeCenter,
						scale,
						SpriteEffects.None,
						0.0f);
				}
			}
		}
	}
}
