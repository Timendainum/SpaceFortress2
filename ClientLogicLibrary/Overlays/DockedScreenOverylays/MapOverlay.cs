using ClientLogicLibrary.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ClientLogicLibrary.ScreenManagement;
using Microsoft.Xna.Framework.Input;
using ClientLogicLibrary.Effects;
using GameLogicLibrary.Maths;
using GameLogicLibrary.Simulation;
using GameLogicLibrary.Immobiles;
using GameLogicLibrary.Mobiles;
using GameLogicLibrary.Immobiles.Lairs;
using ClientLogicLibrary.Overlays;

namespace ClientLogicLibrary.Overlays.DockedScreenOverlays
{
	
	public class MapOverlay : Overlay
	{
		Station MyStation;
		public float Zoomfactor = 0.025f;
		public float ZoomFactorMax = 0.050f;
		public float ZoomFactorMin = 0.010f;

		private Texture2D _iconTexture = DockedScreenTextureManager.GetTexture("minimap_icons");
		private Texture2D _MinimapTexture = DockedScreenTextureManager.GetTexture("scannerFrame");

		private Rectangle _MinimapSourceRectangle = new Rectangle(0, 0, 724, 628);
		private Rectangle _minimapViewableArea = new Rectangle(5, 5, 714, 618);
		private Rectangle _minimapScreenViewableArea;

		#region init
		public MapOverlay(Vector2 screenPosition, Station myStation)
		{
			Width = 724;
			Height = 628;
			IsActive = true;
			MyStation = myStation;
			ScreenPosition = screenPosition;
		}
		#endregion

		#region xna
		public override void Update(GameTime gameTime)
		{
			_minimapScreenViewableArea = new Rectangle((int)_minimapViewableArea.X + (int)ScreenPosition.X, (int)_minimapViewableArea.Y + (int)ScreenPosition.Y, _minimapViewableArea.Width, _minimapViewableArea.Height);
		}

		public override void HandleInput(InputState input)
		{
			if (input.IsNewKeyPress(Keys.M))
			{
				if (IsActive)
					SoundManager.PlayEffect("menu_back", 1f);
				else
					SoundManager.PlayEffect("menu_advance", 1f);
				ToggleActive();
			}

			if (IsActive)
			{
				Vector2 mousePosition = new Vector2(input.CurrentMouseState.X, input.CurrentMouseState.Y);
				if (input.IsNewKeyPress(Keys.OemPlus) || (input.IsMouseScrollUp() && MathsHelper.IsVector2InsideRectangle(mousePosition, GetScreenRectangle())))
				{
					ZoomIn();
				}
				if (input.IsNewKeyPress(Keys.OemMinus) || (input.IsMouseScrollDown() && MathsHelper.IsVector2InsideRectangle(mousePosition, GetScreenRectangle())))
				{
					ZoomOut();
				}
			}
		}

		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			if (IsActive)
			{
				spriteBatch.DrawString(DockedScreenTextureManager.GetFont("kootenay36"), "Scanner", TransformOverlayToScreen(new Vector2(0, 0)), Color.White);

				DrawMiniMap(spriteBatch);


				//Draw icons on map		
				foreach (Immobile imob in MyStation.CurrentSector.Immobiles)
				{
					if (imob.Collidable)
					{
						DrawIcon(spriteBatch, imob);
					}
				}

				foreach (Mobile mob in MyStation.CurrentSector.Mobiles)
				{
					if (!mob.Expired)
					{
						DrawIcon(spriteBatch, mob);
					}
				}
			}
		}

		#endregion

		#region drawing helpers
		private void DrawMiniMap(SpriteBatch spriteBatch)
		{
			//Draw mini map
			Vector2 relativeCenter = new Vector2(_MinimapSourceRectangle.Width / 2, _MinimapSourceRectangle.Height / 2);
			spriteBatch.Draw(
				_MinimapTexture,
				ScreenCenter,
				_MinimapSourceRectangle,
				Color.White,
				0.0f,
				relativeCenter,
				1.0f,
				SpriteEffects.None,
				0.0f);
		}

		private void DrawIcon(SpriteBatch spriteBatch, Entity entity)
		{
			//Set screen center
			Vector2 screenCenter = TransformWorldToMap(entity.WorldCenter);
			if (screenCenter != Vector2.Zero && MathsHelper.IsVector2InsideRectangle(screenCenter, _minimapScreenViewableArea))
			{
				//Draw it!
				Rectangle squareRectangle = new Rectangle(0, 0, 8, 8);
				Rectangle circleRectangle = new Rectangle(0, 8, 8, 8);
				Rectangle triangleRectangle = new Rectangle(0, 16, 8, 8);
				Rectangle plusRectangle = new Rectangle(0, 24, 8, 8);
				Color iconColor = Color.White;


				Rectangle sourceRectangle;
				if (entity is Player)
				{
					sourceRectangle = triangleRectangle;
					iconColor = Color.Green;
				}
				else if (entity is Station)
				{
					sourceRectangle = plusRectangle;
					iconColor = Color.Blue;
				}
				else if (entity is Lair)
				{
					sourceRectangle = squareRectangle;
					iconColor = Color.Yellow;
				}
				else if (entity is Immobile)
				{
					sourceRectangle = circleRectangle;
					iconColor = Color.Gray;
				}
				else if (entity is Mobile)
				{
					sourceRectangle = triangleRectangle;
					iconColor = Color.Red;
				}
				else
				{
					sourceRectangle = circleRectangle;
					iconColor = Color.Goldenrod;
				}

				Vector2 relativeCenter = new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2);
				float scale = 1.0f;

				spriteBatch.Draw(
					_iconTexture,
					screenCenter,
					sourceRectangle,
					iconColor,
					entity.Rotation,
					relativeCenter,
					scale,
					SpriteEffects.None,
					0.0f);
			}
		}
		#endregion

		public Vector2 TransformWorldToMap(Vector2 point)
		{

			Vector2 stationAdjusted = point - MyStation.WorldCenter;
			Vector2 mapAdjusted = stationAdjusted * Zoomfactor;
			Vector2 miniMapAdjusted = mapAdjusted + ScreenCenter;

			return miniMapAdjusted;
		}

		private void ZoomIn()
		{
			if (Zoomfactor < ZoomFactorMax)
			{
				Zoomfactor += 0.005f;
				SoundManager.PlayEffect("menu_advance", 1f);
			} else
				SoundManager.PlayEffect("menu_bad_select", 1f);

		}

		private void ZoomOut()
		{
			if (Zoomfactor > ZoomFactorMin)
			{
				Zoomfactor -= 0.005f;
				SoundManager.PlayEffect("menu_back", 1f);
			} else
				SoundManager.PlayEffect("menu_bad_select", 1f);
		}
	}
}
