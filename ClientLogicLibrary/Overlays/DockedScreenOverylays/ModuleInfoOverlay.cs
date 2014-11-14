using System;
using ClientLogicLibrary.Graphics;
using ClientLogicLibrary.Simulation;
using GameLogicLibrary.Mobiles.Ships;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ClientLogicLibrary.Overlays;
using GameLogicLibrary.Mobiles.Modules;
using ClientLogicLibrary.ScreenManagement;

namespace ClientLogicLibrary.Overlays.DockedScreenOverlays
{
	
	public class ModuleInfoOverlay : Overlay
	{


		public Module CurrentModule;
		public Module OtherModule;

		public enum ModuleInfoModes { Cargo, Fitting }

		public ModuleInfoModes CurrentMode { get; set; }

		public Button FitButton { get; private set; }

		public ModuleInfoOverlay()
		{
			FitButton = new Button(this, DockedScreenTextureManager.GetFont("kootenay14"), "Fit Button", TransformOverlayToScreen(new Vector2(10, 150)));
		}

		public override void HandleInput(InputState input)
		{
			FitButton.HandleInput(input);
		}

		public override void Update(GameTime gameTime)
		{
			if (CurrentMode == ModuleInfoModes.Cargo)
			{
				if (OtherModule is NullModule)
				{
					FitButton.Text = "Fit Module";
				}
				else
				{
					FitButton.Text = "Swap Module";
				}
			}
			else if (CurrentMode == ModuleInfoModes.Fitting)
			{
				if (OtherModule is NullModule)
				{
					FitButton.Text = "Remove Module";
				}
				else
				{
					FitButton.Text = "Swap Module";
				}
			}
			FitButton.Update(gameTime);
		}

		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			spriteBatch.DrawString(DockedScreenTextureManager.GetFont("kootenay14"), "Module Info:", TransformOverlayToScreen(Vector2.Zero), Color.Yellow);

			string type = string.Format("Type: {0}", CurrentModule.TypeName);
			string name = string.Format("Name: {0}", CurrentModule.Name);
			spriteBatch.DrawString(DockedScreenTextureManager.GetFont("kootenay14"), type, TransformOverlayToScreen(new Vector2(10, 30)), Color.White);
			spriteBatch.DrawString(DockedScreenTextureManager.GetFont("kootenay14"), name, TransformOverlayToScreen(new Vector2(10, 60)), Color.White);

			string power = string.Format("Power: {0}", CurrentModule.Power);
			string mass = string.Format("Mass: {0}", CurrentModule.Mass);
			spriteBatch.DrawString(DockedScreenTextureManager.GetFont("kootenay14"), power, TransformOverlayToScreen(new Vector2(10, 90)), Color.White);
			spriteBatch.DrawString(DockedScreenTextureManager.GetFont("kootenay14"), mass, TransformOverlayToScreen(new Vector2(10, 120)), Color.White);

			FitButton.Draw(spriteBatch, gameTime);

		}
	}
}
