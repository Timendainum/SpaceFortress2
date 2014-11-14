using System;
using ClientLogicLibrary.Effects;
using ClientLogicLibrary.Graphics;
using ClientLogicLibrary.Overlays;
using ClientLogicLibrary.ScreenManagement;
using ClientLogicLibrary.Simulation;
using GameLogicLibrary.Immobiles;
using GameLogicLibrary.Mobiles.Ships;
using GameLogicLibrary.Simulation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ClientLogicLibrary.Screens
{
	public class DockedScreen : GameScreen
	{
		#region Declarations
		public enum ScreenModes { DockingBay, CargoBay, Factory, Scanner, Station, Anomaly }
		public ScreenModes ScreenMode = ScreenModes.DockingBay;
		ContentManager content;
		float pauseAlpha;
		public Station DockedStation;

		//Overlays
		ClientLogicLibrary.Overlays.DockedScreenOverlays.AnomalyOverlay anomalyOverlay;
		ClientLogicLibrary.Overlays.DockedScreenOverlays.CargoBayOverlay cargoBayOverlay;
		ClientLogicLibrary.Overlays.DockedScreenOverlays.DockingBayOverlay dockingBayOverlay;
		ClientLogicLibrary.Overlays.DockedScreenOverlays.FactoryOverlay factoryOverlay;
		ClientLogicLibrary.Overlays.DockedScreenOverlays.ScannerOverlay scannerOverlay;
		StationOverlay stationOverlay;

		//Menus
		ButtonOverlay undockMenu;
		ButtonOverlay mainMenu;
		#endregion

		#region Initialization
		public DockedScreen(Station dockedStation)
        {
			//transition times
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

			//Set up docked station
			DockedStation = dockedStation;

			
        }

        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

			//Init game manager -----------------------------------------------
			DockedScreenTextureManager.Init(content);

			//Overlays --------------------------------------------------------
			Vector2 mainOverlayPosition = new Vector2(160, 90);
			anomalyOverlay = new ClientLogicLibrary.Overlays.DockedScreenOverlays.AnomalyOverlay(mainOverlayPosition);
			cargoBayOverlay = new ClientLogicLibrary.Overlays.DockedScreenOverlays.CargoBayOverlay(mainOverlayPosition);
			dockingBayOverlay = new ClientLogicLibrary.Overlays.DockedScreenOverlays.DockingBayOverlay(mainOverlayPosition, this);
			factoryOverlay = new ClientLogicLibrary.Overlays.DockedScreenOverlays.FactoryOverlay(mainOverlayPosition);
			scannerOverlay = new ClientLogicLibrary.Overlays.DockedScreenOverlays.ScannerOverlay(mainOverlayPosition, DockedStation);
			stationOverlay = new StationOverlay(mainOverlayPosition);

			//menus -----------------------------------------------------------

			//undock menu
			undockMenu = new ButtonOverlay();

			//Set up buttons
			Button unDockButton = new Button(undockMenu, DockedScreenTextureManager.GetFont("miramonte14"), "Undock", new Vector2(0, 0));
			unDockButton.Clicked += Button_OnClick;
			unDockButton.Clicked += UndockButton_OnClick;
			undockMenu.Buttons.Add(unDockButton);

			//main menu
			//DockingBay, CargoBay, Market, Factory, Station, Anomaly
			mainMenu = new ButtonOverlay();
			mainMenu.ScreenPosition = new Vector2(20, 90);
			Button mmDockingBay, mmCargoBay, mmFactory, mmScanner, mmStation, mmAnomaly;

			mmDockingBay = new Button(mainMenu, DockedScreenTextureManager.GetFont("miramonte14"), "Docking Bay", new Vector2(0, 0));
			mmDockingBay.Clicked += Button_OnClick;
			mmDockingBay.Clicked += mmDockingBayButton_OnClick;
			mainMenu.Buttons.Add(mmDockingBay);

			mmCargoBay = new Button(mainMenu, DockedScreenTextureManager.GetFont("miramonte14"), "Cargo Bay", new Vector2(0, 31));
			mmCargoBay.Clicked += Button_OnClick;
			mmCargoBay.Clicked += mmCargoBayButton_OnClick;
			mainMenu.Buttons.Add(mmCargoBay);

			mmFactory = new Button(mainMenu, DockedScreenTextureManager.GetFont("miramonte14"), "Factory", new Vector2(0, 62));
			mmFactory.Clicked += Button_OnClick;
			mmFactory.Clicked += mmFactoryButton_OnClick;
			mainMenu.Buttons.Add(mmFactory);

			mmScanner = new Button(mainMenu, DockedScreenTextureManager.GetFont("miramonte14"), "Scanner", new Vector2(0, 93));
			mmScanner.Clicked += Button_OnClick;
			mmScanner.Clicked += mmScannerButton_OnClick;
			mainMenu.Buttons.Add(mmScanner);

			mmStation = new Button(mainMenu, DockedScreenTextureManager.GetFont("miramonte14"), "Station", new Vector2(0, 124));
			mmStation.Clicked += Button_OnClick;
			mmStation.Clicked += mmStationButton_OnClick;
			mainMenu.Buttons.Add(mmStation);

			mmAnomaly = new Button(mainMenu, DockedScreenTextureManager.GetFont("miramonte14"), "Anomaly", new Vector2(0, 155));
			mmAnomaly.Clicked += Button_OnClick;
			mmAnomaly.Clicked += mmAnomalyButton_OnClick;
			mainMenu.Buttons.Add(mmAnomaly);


			

			//Wire up event handlers ------------------------------------------
			if (GameManager.TheGameManager.ThePlayer.Expired)
			{
				GameManager.TheGameManager.ThePlayer.Expired = false;
				GameManager.TheGameManager.ThePlayer.CurrentShip = new NullShip();
			}

			//set up docking player -------------------------------------------
			GameManager.TheGameManager.ThePlayer.CurrentSector = DockedStation.CurrentSector;
			GameManager.TheGameManager.ThePlayer.WorldLocation = DockedStation.WorldCenter;
			GameManager.TheGameManager.ThePlayer.Velocity = Vector2.Zero;
			GameManager.TheGameManager.ThePlayer.CurrentShip.ShieldDamage = 0;

			// -------------------------------------------------------------------------
            // once the load has finished, we use ResetElapsedTime to tell the game's
            // timing mechanism that we have just finished a very long frame, and that
            // it should not try to catch up.
            ScreenManager.Game.ResetElapsedTime();
        }


        /// <summary>
        /// Unload graphics content used by the game.
        /// </summary>
        public override void UnloadContent()
        {
            content.Unload();
        }
        #endregion

        #region Update and Draw


        /// <summary>
        /// Updates the state of the game. This method checks the GameScreen.IsActive
        /// property, so the game will stop updating when the pause menu is active,
        /// or if you tab away to a different application.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);

            // Gradually fade in or out depending on whether we are covered by the pause screen.
            if (coveredByOtherScreen)
                pauseAlpha = Math.Min(pauseAlpha + 1f / 32, 1);
            else
                pauseAlpha = Math.Max(pauseAlpha - 1f / 32, 0);

            if (IsActive)
            {
				//Update Camera size
				Camera.ViewPortSize = new Vector2(ScreenManager.ViewportRectangle.Width, ScreenManager.ViewportRectangle.Height);

				//update overlays
				switch (ScreenMode)
				{
					case ScreenModes.Anomaly:
						anomalyOverlay.Update(gameTime);
						break;
					case ScreenModes.CargoBay:
						cargoBayOverlay.Update(gameTime);
						break;
					case ScreenModes.DockingBay:
						dockingBayOverlay.Update(gameTime);
						break;
					case ScreenModes.Factory:
						factoryOverlay.Update(gameTime);
						break;
					case ScreenModes.Scanner:
						scannerOverlay.Update(gameTime);
						break;
					case ScreenModes.Station:
						stationOverlay.Update(gameTime);
						break;
				}
				

				//update menus
				//mainMenu.ScreenPosition = new Vector2(Camera.ViewPortWidth - 150, 10);
				undockMenu.ScreenPosition = new Vector2(20, Camera.ViewPortHeight - 30);
				undockMenu.Update(gameTime);
				mainMenu.Update((gameTime));
				
            }
        }


        /// <summary>
        /// Lets the game respond to player input. Unlike the Update method,
        /// this will only be called when the gameplay screen is active.
        /// </summary>
        public override void HandleInput(InputState input)
        {
			base.HandleInput(input);

            if (input.IsPauseGame())
            {
                ScreenManager.AddScreen(new PauseMenuScreen());
            }
            else
            {
				if (input.IsNewKeyPress(Keys.Tab))
				{
					Undock();
				}

				//handle overlay inputs ---------------------------------------
				switch (ScreenMode)
				{
					case ScreenModes.Anomaly:
						anomalyOverlay.HandleInput(input);
						break;
					case ScreenModes.CargoBay:
						cargoBayOverlay.HandleInput(input);
						break;
					case ScreenModes.DockingBay:
						dockingBayOverlay.HandleInput(input);
						break;
					case ScreenModes.Factory:
						factoryOverlay.HandleInput(input);
						break;
					case ScreenModes.Scanner:
						scannerOverlay.HandleInput(input);
						break;
					case ScreenModes.Station:
						stationOverlay.HandleInput(input);
						break;
				}

				//menus -------------------------------------------------------
				undockMenu.HandleInput(input);
				mainMenu.HandleInput(input);
            }
        }


        /// <summary>
        /// Draws the gameplay screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target, Color.Black, 0, 0);
			if (IsActive)
			{
				SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

				spriteBatch.Begin();

				//draw here
				spriteBatch.DrawString(DockedScreenTextureManager.GetFont("miramonte14"), String.Format("Docked At: {0}", DockedStation.Name), new Vector2(20, 10), Color.White);

				Ship playerShip = GameManager.TheGameManager.ThePlayer.CurrentShip;
				spriteBatch.DrawString(DockedScreenTextureManager.GetFont("miramonte14"), String.Format("Ship Type: {0}", playerShip.ShipTypeName), new Vector2(20, 30), Color.White);


				//string theText = "Press tab to undock and dock!";

				//spriteBatch.DrawString(DockedScreenTextureManager.GetFont("kootenay36"), theText,
				//		new Vector2(ScreenManager.ViewportRectangle.Width / 2, ScreenManager.ViewportRectangle.Height / 2),
				//		Color.White, 0.0f,
				//		new Vector2(DockedScreenTextureManager.GetFont("kootenay36").MeasureString(theText).X / 2, DockedScreenTextureManager.GetFont("kootenay36").MeasureString(theText).Y / 2),
				//		1.0f, SpriteEffects.None, 0.0f);

				//Draw overlays ---------------------------------------------------
				switch (ScreenMode)
				{
					case ScreenModes.Anomaly:
						anomalyOverlay.Draw(spriteBatch, gameTime);
						break;
					case ScreenModes.CargoBay:
						cargoBayOverlay.Draw(spriteBatch, gameTime);
						break;
					case ScreenModes.DockingBay:
						dockingBayOverlay.Draw(spriteBatch, gameTime);
						break;
					case ScreenModes.Factory:
						factoryOverlay.Draw(spriteBatch, gameTime);
						break;
					case ScreenModes.Scanner:
						scannerOverlay.Draw(spriteBatch, gameTime);
						break;
					case ScreenModes.Station:
						stationOverlay.Draw(spriteBatch, gameTime);
						break;
				}

				//Draw menus ------------------------------------------------------
				undockMenu.Draw(spriteBatch, gameTime);
				mainMenu.Draw(spriteBatch, gameTime);

				spriteBatch.End();
			}

            // If the game is transitioning on or off, fade it out to black.
            if (TransitionPosition > 0 || pauseAlpha > 0)
            {
                float alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, pauseAlpha / 2);

                ScreenManager.FadeBackBufferToBlack(alpha);
            }
        }
        #endregion

		#region helper methods
		private void Undock()
		{
			if (GameManager.TheGameManager.ThePlayer.CurrentShip.IsFitValid())
			{
				GameManager.TheGameManager.ThePlayer.CurrentSector.AddPlayer(GameManager.TheGameManager.ThePlayer);
				GameManager.TheGameManager.ThePlayer.Velocity = Vector2.Zero;
				LoadingScreen.Load(ScreenManager, true, new TaticalScreen());
				SoundManager.PlayEffect("menu_select", 1f);
			}
			else
				SoundManager.PlayEffect("menu_bad_select", 1f);
		}
		#endregion

		#region event handlers
		private void Button_OnClick(object o, EventArgs e)
		{
			SoundManager.PlayEffect("menu_select3", 1f);
		}

		private void UndockButton_OnClick(object o, EventArgs e)
		{
			Undock();
		}

		#region main menu button handlers
		//mmDockingBay, mmCargoBay, mmFactory, mmStation, mmAnomaly;
		private void mmDockingBayButton_OnClick(object o, EventArgs e)
		{
			ScreenMode = ScreenModes.DockingBay;
		}

		private void mmCargoBayButton_OnClick(object o, EventArgs e)
		{
			ScreenMode = ScreenModes.CargoBay;
		}

		private void mmFactoryButton_OnClick(object o, EventArgs e)
		{
			ScreenMode = ScreenModes.Factory;
		}

		private void mmStationButton_OnClick(object o, EventArgs e)
		{
			ScreenMode = ScreenModes.Station;
		}

		private void mmAnomalyButton_OnClick(object o, EventArgs e)
		{
			ScreenMode = ScreenModes.Anomaly;
		}
		private void mmScannerButton_OnClick(object o, EventArgs e)
		{
			ScreenMode = ScreenModes.Scanner;
		}
		#endregion
		#endregion

	}

}

//Ship playerShip = GameManager.TheGameManager.ThePlayer.CurrentShip;
//spriteBatch.DrawString(DockedScreenTextureManager.GetFont("kootenay14"), String.Format("Ship Type: {0} Mass H/T: {1}/{2}", playerShip.ShipTypeName, playerShip.MassHull, playerShip.MassTotal), new Vector2(200, 30), Color.White);
//spriteBatch.DrawString(DockedScreenTextureManager.GetFont("kootenay14"), String.Format("Thrust: {0} Rotational Thrust: {1}", playerShip.Thrust, playerShip.RotationalThrust), new Vector2(200, 50), Color.White);
//spriteBatch.DrawString(DockedScreenTextureManager.GetFont("kootenay14"), String.Format("Power C/T: {0}/{1}", playerShip.PowerCurrent, playerShip.PowerTotal), new Vector2(200, 70), Color.White);
//spriteBatch.DrawString(DockedScreenTextureManager.GetFont("kootenay14"), String.Format("Hull/Armor/Shield: {0},{1}/{2},{3}/{4},{5}", playerShip.StructureCurrent, playerShip.StructureTotal, playerShip.ArmorCurrent, playerShip.ArmorTotal, playerShip.ShieldCurrent, playerShip.ShieldTotal), new Vector2(200, 90), Color.White);
//			