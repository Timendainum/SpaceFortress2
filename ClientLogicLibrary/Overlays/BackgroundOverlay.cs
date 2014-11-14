using System;
using System.Collections.Generic;
using ClientLogicLibrary.Graphics;
using ClientLogicLibrary.ScreenManagement;
using GameLogicLibrary.Simulation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ClientLogicLibrary.Overlays
{
	public class BackgroundOverlay : Overlay
	{
		#region fields
		private List<BackgroundParticle> particle = new List<BackgroundParticle>();
		private Random rand = new Random();
		private const int starCount = 200;

		private List<BackgroundObject> backgroundObjects = new List<BackgroundObject>();
		private Sector _sector = null;

	//Sprite CloudLayer;
	//Vector2 CloudLayerLocation = new Vector2(-2048, -2048);
	//float CloudLayerParalaxFactor = 0.9999f;
		
		#endregion

		#region constructor
		public BackgroundOverlay(Sector sector)
		{
			_sector = sector;

			//Generate particles ----------------------------------------------------------------------
			Vector2 newLocation;
			for (int i = 0; i < starCount; i++)
			{
				newLocation = new Vector2(rand.Next(0, Camera.ViewPortWidth), rand.Next(0, Camera.ViewPortHeight));
				newLocation = Camera.TransformCameraToWorld(newLocation);
				BackgroundParticle star = new BackgroundParticle(newLocation, TaticalScreenTextureManager.GetTexture("square_white"), new Rectangle(0, 0, 1, 1), rand);
				particle.Add(star);
			}

			//Sort sector objects
			_sector.SectorBackgroundObjects.Sort(delegate(SectorBackgroundObject p1, SectorBackgroundObject p2) { return p1.ParalaxFactor.CompareTo(p2.ParalaxFactor); });

			for (int i = _sector.SectorBackgroundObjects.Count - 1; i >= 0; i--)
			{

				backgroundObjects.Add(new BackgroundObject(_sector.SectorBackgroundObjects[i].Location, _sector.SectorBackgroundObjects[i].Size, _sector.SectorBackgroundObjects[i].TextureName, _sector.SectorBackgroundObjects[i].ParalaxFactor));
			}

			//backgroundObjects.Add(new BackgroundObject(Vector2.Zero, new Vector2(4096, 4096), "starfield", 0.9999f));
			//backgroundObjects.Add(new BackgroundObject(Vector2.Zero, new Vector2(4096, 4096), "starfield_bigstars", 0.999f));
			//backgroundObjects.Add(new BackgroundObject(new Vector2(35000, -30000), new Vector2(1000, 1000), "star_orange", 0.99f));
			//backgroundObjects.Add(new BackgroundObject(new Vector2(-5000, -5000), new Vector2(870, 870), "planet_green", 0.9f));
			//backgroundObjects.Add(new BackgroundObject(new Vector2(2000, 2000), new Vector2(590, 590), "planet_blue", 0.8f));
			
			//CloudLayer = new Sprite(CloudLayerLocation, new Vector2(-4096, 4096), TextureManager.GetTexture("cloudlayer_big"), new Rectangle(0, 0, 4096, 4096)) { TintColor = Color.DarkGray };

			Camera.CameraResized += Camera_OnResized;
		}
		#endregion

		#region xna
		public override void Update(GameTime gameTime)
		{
			foreach (BackgroundParticle star in particle)
			{
				star.Update(gameTime);
			}

			//Update planet
			foreach (BackgroundObject obj in backgroundObjects)
			{
				obj.Update(gameTime);
			}
			

			//CloudLayer.WorldLocation = (Camera.WorldPosition * CloudLayerParalaxFactor) + CloudLayerLocation;

		}

		public override void HandleInput(InputState input)
		{
			
		}

		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			foreach (BackgroundObject obj in backgroundObjects)
			{
				obj.Draw(spriteBatch);
			}

			foreach (BackgroundParticle star in particle)
			{
				star.Draw(spriteBatch);
			}
		}
		#endregion

		#region event handlers
		public void Camera_OnResized(EventArgs e)
		{
			//Some logic here to respond to event
			RelocateStars();
		}

		private void RelocateStars()
		{
			Vector2 newLocation;

			foreach (BackgroundParticle star in particle)
			{
				newLocation = new Vector2(rand.Next(0, Camera.ViewPortWidth), rand.Next(0, Camera.ViewPortHeight));
				star.WorldLocation = Camera.TransformCameraToWorld(newLocation);
			}
		}

		#endregion
	}
}
