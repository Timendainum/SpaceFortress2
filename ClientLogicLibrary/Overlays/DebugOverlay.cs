using System;
using System.Collections.Generic;
using ClientLogicLibrary.Simulation;
using GameLogicLibrary.Mobiles.Ships;
using GameLogicLibrary.Simulation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ClientLogicLibrary.ScreenManagement;
using ClientLogicLibrary.Graphics;
using ClientLogicLibrary.Mobiles;
using Microsoft.Xna.Framework.Input;

namespace ClientLogicLibrary.Overlays
{
	public class DebugOverlay : Overlay
	{
		private float fps;
		private float ups;
		private Vector2 mousePosition;
		ClientPlayer ThePlayer;

		float elapsed;


		public DebugOverlay(ClientPlayer thePlayer)
		{
			ThePlayer = thePlayer;
			ScreenPosition = new Vector2(10, 10);
			Width = 600;
			Height = 210;
		}


		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			if (!IsActive)
				return;
			
			//FPS Counter
			elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
			fps = 1 / elapsed;
			spriteBatch.DrawString(TaticalScreenTextureManager.GetFont("kootenay14"), String.Format("FPS: {0} UPS: {1}", fps, ups), TransformOverlayToScreen(new Vector2(10, 10)), Color.White);
			spriteBatch.DrawString(TaticalScreenTextureManager.GetFont("kootenay14"), String.Format("Screen Size: {0},{1} Camera World Center: {2},{3}", Camera.ViewPortWidth, Camera.ViewPortHeight, Camera.WorldCenter.X, Camera.WorldCenter.Y), TransformOverlayToScreen(new Vector2(10, 30)), Color.White);
			spriteBatch.DrawString(TaticalScreenTextureManager.GetFont("kootenay14"), 
				String.Format("Immobiles: {0} Mobiles: {1} Projectiles: {2}",
				ThePlayer.ServerPlayer.CurrentSector.Immobiles.Count,
				ThePlayer.ServerPlayer.CurrentSector.Mobiles.Count,
				ThePlayer.ServerPlayer.CurrentSector.Projectiles.Count), TransformOverlayToScreen(new Vector2(10, 50)), Color.White);

			Vector2 mouseWorldPosition = Camera.TransformCameraToWorld(mousePosition);
			spriteBatch.DrawString(TaticalScreenTextureManager.GetFont("kootenay14"), String.Format("Mouse Screen: {0},{1} World: {2},{3}", mousePosition.X, mousePosition.Y, mouseWorldPosition.X, mouseWorldPosition.Y), TransformOverlayToScreen(new Vector2(10, 70)), Color.White);

			float rotationRad = ThePlayer.ServerMobile.Rotation;
			float rotationDegrees = MathHelper.ToDegrees(rotationRad);
			spriteBatch.DrawString(TaticalScreenTextureManager.GetFont("kootenay14"), String.Format("Rotation (R/D): {0}/{1}", rotationRad, rotationDegrees), TransformOverlayToScreen(new Vector2(10, 90)), Color.White);

			Vector2 velocity = ThePlayer.ServerMobile.Velocity;
			Vector2 acceleration = ThePlayer.ServerMobile.Acceleration;
			float speed = ThePlayer.ServerMobile.Speed;
			spriteBatch.DrawString(TaticalScreenTextureManager.GetFont("kootenay14"), String.Format("Velocity/Speed: {0},{1}/{4} Acceleration: {2},{3}", velocity.X, velocity.Y, acceleration.X, acceleration.Y, speed), TransformOverlayToScreen(new Vector2(10, 110)), Color.White);

			Ship playerShip = ThePlayer.ServerPlayer.CurrentShip;
			spriteBatch.DrawString(TaticalScreenTextureManager.GetFont("kootenay14"), String.Format("Ship Type: {0} Mass H/T: {1}/{2}", playerShip.ShipTypeName, playerShip.MassHull, playerShip.MassTotal), TransformOverlayToScreen(new Vector2(10, 130)), Color.White);
			spriteBatch.DrawString(TaticalScreenTextureManager.GetFont("kootenay14"), String.Format("Thrust: {0} Rotational Thrust: {1}", playerShip.Thrust, playerShip.RotationalThrust), TransformOverlayToScreen(new Vector2(10, 150)), Color.White);
			spriteBatch.DrawString(TaticalScreenTextureManager.GetFont("kootenay14"), String.Format("Power C/T: {0}/{1}", playerShip.PowerCurrent, playerShip.PowerTotal), TransformOverlayToScreen(new Vector2(10, 170)), Color.White);
			spriteBatch.DrawString(TaticalScreenTextureManager.GetFont("kootenay14"), String.Format("Hull/Armor/Shield: {0},{1}/{2},{3}/{4},{5}", playerShip.StructureCurrent, playerShip.StructureTotal, playerShip.ArmorCurrent, playerShip.ArmorTotal, playerShip.ShieldCurrent, playerShip.ShieldTotal), TransformOverlayToScreen(new Vector2(10, 190)), Color.White);
			
		}

		public override void HandleInput(InputState input)
		{
			//Debug toggle --------------------------------------------------------------------
			if (input.IsNewKeyPress(Keys.OemTilde))
				ToggleActive();

			mousePosition = new Vector2(input.CurrentMouseState.X, input.CurrentMouseState.Y);
		}

		public override void Update(GameTime gameTime)
		{
			if (!IsActive)
				return;

			elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
			ups = 1 / elapsed;
		}

	}
}
