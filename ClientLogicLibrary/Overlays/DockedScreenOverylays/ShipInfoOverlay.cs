using System;
using ClientLogicLibrary.Graphics;
using ClientLogicLibrary.Simulation;
using GameLogicLibrary.Mobiles.Ships;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ClientLogicLibrary.Overlays.DockedScreenOverlays
{
	public class ShipInfoOverlay : Overlay
	{



		public override void HandleInput(ScreenManagement.InputState input)
		{
			
		}

		public override void Update(GameTime gameTime)
		{
			
		}

		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			Ship playerShip = GameManager.TheGameManager.ThePlayer.CurrentShip;
			
			string shipType = string.Format("Ship Type: {0}", playerShip.ShipTypeName);
			string shipName = string.Format("Ship Name: {0}", playerShip.ShipName);
			spriteBatch.DrawString(DockedScreenTextureManager.GetFont("kootenay14"), shipType, TransformOverlayToScreen(Vector2.Zero), Color.White);
			spriteBatch.DrawString(DockedScreenTextureManager.GetFont("kootenay14"), shipName, TransformOverlayToScreen(new Vector2(0, 30)), Color.White);
			
			string shipHullArmorShield = String.Format("Hull/Armor/Shield: {0},{1}/{2},{3}/{4},{5}", playerShip.StructureCurrent, playerShip.StructureTotal, playerShip.ArmorCurrent, playerShip.ArmorTotal, playerShip.ShieldCurrent, playerShip.ShieldTotal);
			spriteBatch.DrawString(DockedScreenTextureManager.GetFont("kootenay14"), shipHullArmorShield, TransformOverlayToScreen(new Vector2(0, 60)), Color.White);

			string shipPower = String.Format("Power: Current/Max: {0}/{1}", playerShip.PowerCurrent, playerShip.PowerTotal);
			spriteBatch.DrawString(DockedScreenTextureManager.GetFont("kootenay14"), shipPower, TransformOverlayToScreen(new Vector2(0, 90)), Color.White);

			string shipMass = String.Format("Mass: Hull/Total: {0}/{1}", playerShip.MassHull, playerShip.MassTotal);
			spriteBatch.DrawString(DockedScreenTextureManager.GetFont("kootenay14"), shipMass, TransformOverlayToScreen(new Vector2(0, 120)), Color.White);
		}
	}
}
