using ClientLogicLibrary.Graphics;
using ClientLogicLibrary.ScreenManagement;
using GameLogicLibrary.Immobiles;
using GameLogicLibrary.Maths;
using GameLogicLibrary.Mobiles;
using GameLogicLibrary.Simulation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ClientLogicLibrary.Effects;
using GameLogicLibrary.Immobiles.Lairs;

namespace ClientLogicLibrary.Overlays.TaticalScreenOverlays
{
	public class MinimapOverlay : Overlay
	{
		#region declarations

		public enum MapSize { Small, Large }
		public MapSize CurrentSize = MapSize.Small;
		private Player _player;
		public float Zoomfactor = 0.025f;
		public float ZoomFactorMax = 0.070f;
		public float ZoomFactorMin = 0.020f;

		private Texture2D _iconTexture = TaticalScreenTextureManager.GetTexture("minimap_icons");

		private Texture2D _MinimapTexture
		{
			get 
			{
				if (CurrentSize == MapSize.Small)	
					return TaticalScreenTextureManager.GetTexture("minimap");
				else
					return TaticalScreenTextureManager.GetTexture("minimap_large");
			}
		}
		private Rectangle _MinimapSourceRectangle
		{
			get
			{
				if (CurrentSize == MapSize.Small)
					return new Rectangle(0, 0, 300, 200);
				else
					return new Rectangle(0, 0, 600, 400);
			}
		}
		private Rectangle _minimapViewableArea
		{
			get
			{
				if (CurrentSize == MapSize.Small)
					return new Rectangle(5, 5, 292, 192);
				else
					return new Rectangle(5, 5, 592, 392);
			}
		}
		private Rectangle _minimapScreenViewableArea;		
		


		#endregion


		#region init
		public MinimapOverlay(Player player)
		{
			_player = player;
			Width = _MinimapTexture.Width;
			Height = _MinimapTexture.Height;
			IsActive = true;
		}
		#endregion



		#region xna
		public override void Update(GameTime gameTime)
		{
				ScreenPosition = new Vector2(Camera.ViewPortWidth - Width - 20, 20);
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
				if (input.IsNewKeyPress(Keys.N))
				{
					ToggleSize();
				}
			}
		}

		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			if (IsActive)
			{
				DrawMiniMap(spriteBatch);


				//Draw icons on map		
				foreach (Immobile imob in _player.CurrentSector.Immobiles)
				{
					if (imob.Collidable)
					{
						DrawIcon(spriteBatch, imob);
					}
				}

				foreach (Mobile mob in _player.CurrentSector.Mobiles)
				{
					if (!mob.Expired)
					{
						DrawIcon(spriteBatch, mob);
					}
				}

				//Draw player
				DrawIcon(spriteBatch, _player);
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
			
			Vector2 playerAdjusted = point - _player.WorldCenter;
			Vector2 mapAdjusted = playerAdjusted * Zoomfactor;
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

		private void ToggleSize()
		{
			if (CurrentSize == MapSize.Small)
			{
				CurrentSize = MapSize.Large;
				
				SoundManager.PlayEffect("menu_advance", 1f);
			}
			else
			{
				CurrentSize = MapSize.Small;
				SoundManager.PlayEffect("menu_back", 1f);
			}

			Width = _MinimapTexture.Width;
			Height = _MinimapTexture.Height;
		}
	}
}
