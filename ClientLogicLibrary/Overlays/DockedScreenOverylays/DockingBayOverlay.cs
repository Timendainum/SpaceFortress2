using ClientLogicLibrary.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ClientLogicLibrary.ScreenManagement;
using ClientLogicLibrary.Simulation;
using System;
using ClientLogicLibrary.Effects;
using GameLogicLibrary.Simulation;
using GameLogicLibrary.Mobiles.Ships;
using ClientLogicLibrary.Screens;
using GameLogicLibrary.Immobiles;
using GameLogicLibrary.Mobiles.Modules;
using GameLogicLibrary.Mobiles;
using ClientLogicLibrary.Overlays;
using GameLogicLibrary.Mobiles.Modules.Armors;
using GameLogicLibrary.Mobiles.Modules.Engines;
using GameLogicLibrary.Mobiles.Modules.Generators;
using GameLogicLibrary.Mobiles.Modules.Shields;
using GameLogicLibrary.Mobiles.Modules.Utilities;
using GameLogicLibrary.Mobiles.Modules.Weapons;

namespace ClientLogicLibrary.Overlays.DockedScreenOverlays
{
	
	public class DockingBayOverlay : Overlay
	{
		#region declarations
		ButtonOverlay repairButtonOverlay;
		ButtonOverlay boardButtonOverlay;
		ButtonOverlay listTabsButtonOverlay;
		ButtonOverlay moduleFittingButtonOverlay;
		ScrollingButtonOverlay shipListOverlay;
		ScrollingButtonOverlay moduleListOverlay;
		ShipInfoOverlay shipInfoOverlay;
		ModuleInfoOverlay fitModuleInfoOverlay;
        ModuleInfoOverlay cargoModuleInfoOverlay;
		public enum OverlayModes { Ship, Fitting };
		public OverlayModes CurrentOverlayMode { get; private set; }
		Station DockedStation;
		public ShipModes CurrentShipMode = ShipModes.Null;
		public enum ShipModes { Null, Fighter, Frigate, Cruiser, Battleship, Capital };
		#endregion

		#region init
		public DockingBayOverlay(Vector2 screenPosition, DockedScreen screen)
		{
			DockedStation = screen.DockedStation;

			ScreenPosition = screenPosition;

			// -------------------- listTabsButtonOverlay -----------------------
			listTabsButtonOverlay = new ButtonOverlay();
			listTabsButtonOverlay.Width = 200;
			listTabsButtonOverlay.Height = 25;
			listTabsButtonOverlay.ScreenPosition = new Vector2(Camera.ViewPortWidth - listTabsButtonOverlay.Width, ScreenPosition.Y);

			ButtonToggle shipsTab = new ButtonToggle(listTabsButtonOverlay, DockedScreenTextureManager.GetFont("kootenay14"), "Ships", new Vector2(0, 0));
			shipsTab.Clicked += Button_OnClick;
			shipsTab.Clicked += ShipsTabButton_OnClick;
			listTabsButtonOverlay.Buttons.Add(shipsTab);

			ButtonToggle fittingTab = new ButtonToggle(listTabsButtonOverlay, DockedScreenTextureManager.GetFont("kootenay14"), "Fitting", new Vector2(100, 0));
			fittingTab.Clicked += Button_OnClick;
			fittingTab.Clicked += FittingTabButton_OnClick;
			listTabsButtonOverlay.Buttons.Add(fittingTab);


			// ---- ship list overlay ---------------------------------------
			shipListOverlay = new ScrollingButtonOverlay();
			shipListOverlay.ScreenPosition = new Vector2(Camera.ViewPortWidth - listTabsButtonOverlay.Width, ScreenPosition.Y + 25);
			shipListOverlay.Width = 200;
			shipListOverlay.Height = (int)(Camera.ViewPortHeight - shipListOverlay.ScreenPosition.Y);
			shipListOverlay.ItemSize = new Vector2(200, 50);
			foreach (Ship ship in DockedStation.ShipStorage)
			{
				ButtonToggleShip button = new ButtonToggleShip(shipListOverlay, DockedScreenTextureManager.GetFont("kootenay14"), ship.ShipName, new Vector2(0, 0), ship);
				button.Clicked += Button_OnClick;
				button.Clicked += ShipListButton_OnClick;
				shipListOverlay.Buttons.Add(button);
			}

			// ---- module list overlay ---------------------------------------
			moduleListOverlay = new ScrollingButtonOverlay();
			moduleListOverlay.ScreenPosition = new Vector2(Camera.ViewPortWidth - listTabsButtonOverlay.Width, ScreenPosition.Y + 25);
			moduleListOverlay.Width = 200;
			moduleListOverlay.Height = (int)(Camera.ViewPortHeight - moduleListOverlay.ScreenPosition.Y);
			moduleListOverlay.ItemSize = new Vector2(200, 50);
			foreach (Module module in DockedStation.ModuleStorage)
			{
				ButtonToggleModule button = new ButtonToggleModule(moduleListOverlay, DockedScreenTextureManager.GetFont("kootenay14"), module.Name, new Vector2(0, 0), module);
				button.Clicked += Button_OnClick;
				button.Clicked += ModuleListButton_OnClick;
				moduleListOverlay.Buttons.Add(button);
			}

			//ShipInfo Overlay -------------------------------------------------------
			shipInfoOverlay = new ClientLogicLibrary.Overlays.DockedScreenOverlays.ShipInfoOverlay();
			shipInfoOverlay.Width = 450;
			shipInfoOverlay.Height = 150;
			shipInfoOverlay.ScreenPosition = new Vector2(ScreenPosition.X + 15, ScreenPosition.Y + 60);

			//Module fittings overlay ----------------------------------------------
			moduleFittingButtonOverlay = new ButtonOverlay();
			moduleFittingButtonOverlay.Width = 550;
			moduleFittingButtonOverlay.Height = 450;
			moduleFittingButtonOverlay.ScreenPosition = new Vector2(ScreenPosition.X - 150, ScreenPosition.Y + 210);

			//fitModuleInfoOverlay -----------------------------------------
			fitModuleInfoOverlay = new ModuleInfoOverlay();
			fitModuleInfoOverlay.CurrentMode = ModuleInfoOverlay.ModuleInfoModes.Fitting;
			fitModuleInfoOverlay.Width = 150;
			fitModuleInfoOverlay.Height = 150;
			fitModuleInfoOverlay.ScreenPosition = new Vector2(moduleFittingButtonOverlay.ScreenPosition.X + moduleFittingButtonOverlay.Width + 5, ScreenPosition.Y + 300);
			fitModuleInfoOverlay.CurrentModule = new NullModule();
			fitModuleInfoOverlay.FitButton.Clicked += Button_OnClick;
			fitModuleInfoOverlay.FitButton.Clicked += FittingFitButton_OnClick;

			//cargoModuleInfoOverlay -----------------------------------------------------------------
			cargoModuleInfoOverlay = new ModuleInfoOverlay();
			cargoModuleInfoOverlay.CurrentMode = ModuleInfoOverlay.ModuleInfoModes.Cargo;
			cargoModuleInfoOverlay.Width = 150;
			cargoModuleInfoOverlay.Height = 150;
			cargoModuleInfoOverlay.ScreenPosition = new Vector2(moduleFittingButtonOverlay.ScreenPosition.X + moduleFittingButtonOverlay.Width + 5, ScreenPosition.Y + 90);
			cargoModuleInfoOverlay.CurrentModule = new NullModule();
			cargoModuleInfoOverlay.FitButton.Clicked += Button_OnClick;
			cargoModuleInfoOverlay.FitButton.Clicked += CargoFitButton_OnClick;

			//mod info setup
			fitModuleInfoOverlay.OtherModule = cargoModuleInfoOverlay.CurrentModule;
			cargoModuleInfoOverlay.OtherModule = fitModuleInfoOverlay.CurrentModule;

			// --------------------- board ship button
			boardButtonOverlay = new ButtonOverlay();
			boardButtonOverlay.Width = 125;
			boardButtonOverlay.Height = 50;
			boardButtonOverlay.ScreenPosition = new Vector2(Camera.ViewPortWidth - listTabsButtonOverlay.Width - boardButtonOverlay.Width, ScreenPosition.Y + 25);

			Button boardButton = new Button(boardButtonOverlay, DockedScreenTextureManager.GetFont("kootenay14"), "Board Ship", new Vector2(0, 0));
			boardButton.Clicked += Button_OnClick;
			boardButton.Clicked += BoardButton_OnClick;
			boardButtonOverlay.Buttons.Add(boardButton);

			// --------------------- repair
			repairButtonOverlay = new ButtonOverlay();
			repairButtonOverlay.Width = 125;
			repairButtonOverlay.Height = 50;
			repairButtonOverlay.ScreenPosition = new Vector2(Camera.ViewPortWidth - listTabsButtonOverlay.Width - repairButtonOverlay.Width, ScreenPosition.Y + 50);

			Button repairButton = new Button(repairButtonOverlay, DockedScreenTextureManager.GetFont("kootenay14"), "Repair", new Vector2(0, 0));
			repairButton.Clicked += Button_OnClick;
			repairButton.Clicked += RepairButton_OnClick;
			repairButtonOverlay.Buttons.Add(repairButton);

			//---- set up station events --------------------------
			DockedStation.ShipAdded += Station_OnShipAdded;
			DockedStation.ShipRemoved += Station_OnShipRemoved;
			DockedStation.ModuleAdded += Station_OnModuleAdded;
			DockedStation.ModuleRemoved += Station_OnModuleRemoved;


			//Set up player ship changed event
			GameManager.TheGameManager.ThePlayer.ChangedShip += ShipPilot_ChangedShip;
			//Artifically call the event handler to configure the screenPosition
			GameManager.TheGameManager.ThePlayer.OnChangedShip(new ChangedShipHandlerEventArgs(null, GameManager.TheGameManager.ThePlayer.CurrentShip));

			//set default overlay mode --------------------------------
			CurrentOverlayMode = OverlayModes.Ship;
			shipsTab.SetActivated(true);

		}
		#endregion

		#region xna
		public override void Update(GameTime gameTime)
		{
			
			if (CurrentShipMode != ShipModes.Null)
			{
				repairButtonOverlay.ScreenPosition = new Vector2(Camera.ViewPortWidth - listTabsButtonOverlay.Width - repairButtonOverlay.Width, ScreenPosition.Y + 50);
				repairButtonOverlay.Update(gameTime);

				fitModuleInfoOverlay.Update(gameTime);
				moduleFittingButtonOverlay.Update(gameTime);
			}

			listTabsButtonOverlay.ScreenPosition = new Vector2(Camera.ViewPortWidth - listTabsButtonOverlay.Width, ScreenPosition.Y);
			listTabsButtonOverlay.Update(gameTime);

			if (CurrentOverlayMode == OverlayModes.Ship)
			{
				shipListOverlay.ScreenPosition = new Vector2(Camera.ViewPortWidth - listTabsButtonOverlay.Width, ScreenPosition.Y + 25);
				shipListOverlay.Height = (int)(Camera.ViewPortHeight - shipListOverlay.ScreenPosition.Y);
				shipListOverlay.Update(gameTime);

				boardButtonOverlay.ScreenPosition = new Vector2(Camera.ViewPortWidth - listTabsButtonOverlay.Width - boardButtonOverlay.Width, ScreenPosition.Y);
				boardButtonOverlay.Update(gameTime);
			}
			else
			{
				moduleListOverlay.ScreenPosition = new Vector2(Camera.ViewPortWidth - listTabsButtonOverlay.Width, ScreenPosition.Y + 25);
				moduleListOverlay.Height = (int)(Camera.ViewPortHeight - moduleListOverlay.ScreenPosition.Y);
				moduleListOverlay.Update(gameTime);

				cargoModuleInfoOverlay.Update(gameTime);
			}

			
		}

		public override void HandleInput(InputState input)
		{
			if (CurrentShipMode != ShipModes.Null)
			{
				repairButtonOverlay.HandleInput(input);

				fitModuleInfoOverlay.HandleInput(input);
				moduleFittingButtonOverlay.HandleInput(input);
			}

            listTabsButtonOverlay.HandleInput(input);
			if (CurrentOverlayMode == OverlayModes.Ship)
			{
				shipListOverlay.HandleInput(input);
				boardButtonOverlay.HandleInput(input);
			}
			else
			{
				moduleListOverlay.HandleInput(input);
				cargoModuleInfoOverlay.HandleInput(input);
			}

			
		}
		
		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
		{
			spriteBatch.DrawString(DockedScreenTextureManager.GetFont("kootenay36"), "Docking Bay", TransformOverlayToScreen(new Vector2(0, 0)), Color.White);

			//draw shipinfo overlay here
			shipInfoOverlay.Draw(spriteBatch, gameTime);

			if (CurrentShipMode != ShipModes.Null)
			{
				repairButtonOverlay.Draw(spriteBatch, gameTime);
				fitModuleInfoOverlay.Draw(spriteBatch, gameTime);
				moduleFittingButtonOverlay.Draw(spriteBatch, gameTime);
			}

			listTabsButtonOverlay.Draw(spriteBatch, gameTime);
			if (CurrentOverlayMode == OverlayModes.Ship)
			{
				shipListOverlay.Draw(spriteBatch, gameTime);
				boardButtonOverlay.Draw(spriteBatch, gameTime);
			}
			else
			{
				moduleListOverlay.Draw(spriteBatch, gameTime);
				cargoModuleInfoOverlay.Draw(spriteBatch, gameTime);
			}
		}
		#endregion

		#region event handlers

		#region buttons
		private void Button_OnClick(object o, EventArgs e)
		{
			SoundManager.PlayEffect("menu_select3", 1f);
		}

		private void RepairButton_OnClick(object o, EventArgs e)
		{
			GameManager.TheGameManager.ThePlayer.CurrentShip.RepairDamage(int.MaxValue);
		}

		private void BoardButton_OnClick(object o, EventArgs e)
		{
			ButtonToggleShip boardingShipButton = null;
			Ship boardingShip = null;
			foreach (Button button in shipListOverlay.Buttons)
			{
				if (button is ButtonToggleShip)
				{
					boardingShipButton = (ButtonToggleShip)button;
					if (!boardingShipButton.IsActivated)
						boardingShipButton = null;
					else
						break;
				}
			}

			if (boardingShipButton != null)
			{
				boardingShip = boardingShipButton.ButtonShip;
				Ship oldShip = GameManager.TheGameManager.ThePlayer.CurrentShip;
				if (!(oldShip is NullShip))
					DockedStation.AddShip(oldShip);
				GameManager.TheGameManager.ThePlayer.CurrentShip = boardingShip;
				DockedStation.RemoveShip(boardingShip);
				SoundManager.PlayEffect("points_tally", 1f);
				//Reset fit module info
				fitModuleInfoOverlay.CurrentModule = new NullModule();
			} else
				SoundManager.PlayEffect("menu_bad_select", 1f);

		}
		#endregion

		#region fitting lists
		private void ShipsTabButton_OnClick(object o, EventArgs e)
		{
			listTabsButtonOverlay.UnActivateOtherButtons((Button)o);
			CurrentOverlayMode = OverlayModes.Ship;
		}

		private void FittingTabButton_OnClick(object o, EventArgs e)
		{
			listTabsButtonOverlay.UnActivateOtherButtons((Button)o);
			CurrentOverlayMode = OverlayModes.Fitting;
		}

		private void ShipListButton_OnClick(object o, EventArgs e)
		{
			shipListOverlay.UnActivateOtherButtons((Button)o);
		}

		private void ModuleListButton_OnClick(object o, EventArgs e)
		{
			moduleListOverlay.UnActivateOtherButtons((Button)o);

			ButtonToggleModule button = (ButtonToggleModule)o;
			if (button.IsActivated)
			{
				cargoModuleInfoOverlay.CurrentModule = button.ButtonModule;
				moduleListOverlay.UnActivateOtherButtons((Button)o);
			}
			else
			{
				cargoModuleInfoOverlay.CurrentModule = new NullModule();
			}
			fitModuleInfoOverlay.OtherModule = cargoModuleInfoOverlay.CurrentModule;
		}

		private void CargoFitButton_OnClick(object o, EventArgs e)
		{
			//Cant do anything
			if (cargoModuleInfoOverlay.CurrentModule is NullModule || fitModuleInfoOverlay.CurrentModule is NullModule)
			{
				SoundManager.PlayEffect("menu_bad_select", 1f);
			}
			else
			{
				if (GameManager.TheGameManager.ThePlayer.CurrentShip.FitModuleFromStation(cargoModuleInfoOverlay.CurrentModule, fitModuleInfoOverlay.CurrentModule, DockedStation))
				{
					ShipPilot_ChangedShip(GameManager.TheGameManager.ThePlayer, new ChangedShipHandlerEventArgs(GameManager.TheGameManager.ThePlayer.CurrentShip, GameManager.TheGameManager.ThePlayer.CurrentShip));
					SoundManager.PlayEffect("points_tally", 1f);
				}
				else
					SoundManager.PlayEffect("menu_bad_select", 1f);
			}
		}
		#endregion

		#region fitting screen
		private void FittingModuleButton_OnClick(object o, EventArgs e)
		{
			ButtonToggleModule button = (ButtonToggleModule)o;
			if (button.IsActivated)
			{
				fitModuleInfoOverlay.CurrentModule = button.ButtonModule;
				moduleFittingButtonOverlay.UnActivateOtherButtons((Button)o);
			}
			else
			{
				fitModuleInfoOverlay.CurrentModule = new NullModule();
			}
			cargoModuleInfoOverlay.OtherModule = fitModuleInfoOverlay.CurrentModule;
			
		}

		private void FittingFitButton_OnClick(object o, EventArgs e)
		{
			//Cant do anything
			if (fitModuleInfoOverlay.CurrentModule is NullModule)
			{
				SoundManager.PlayEffect("menu_bad_select", 1f);
			}
			//Cargo list is unselect, remove the module
			else if (cargoModuleInfoOverlay.CurrentModule is NullModule)
			{
				if (!GameManager.TheGameManager.ThePlayer.CurrentShip.RemoveModuleToStation(fitModuleInfoOverlay.CurrentModule, DockedStation))
					SoundManager.PlayEffect("menu_bad_select", 1f);
				else
				{
					ShipPilot_ChangedShip(GameManager.TheGameManager.ThePlayer, new ChangedShipHandlerEventArgs(GameManager.TheGameManager.ThePlayer.CurrentShip, GameManager.TheGameManager.ThePlayer.CurrentShip));
					SoundManager.PlayEffect("points_tally", 1f);
				}
			}
			else
			{
				if (GameManager.TheGameManager.ThePlayer.CurrentShip.FitModuleFromStation(cargoModuleInfoOverlay.CurrentModule, fitModuleInfoOverlay.CurrentModule, DockedStation))
				{
					ShipPilot_ChangedShip(GameManager.TheGameManager.ThePlayer, new ChangedShipHandlerEventArgs(GameManager.TheGameManager.ThePlayer.CurrentShip, GameManager.TheGameManager.ThePlayer.CurrentShip));
					SoundManager.PlayEffect("points_tally", 1f);
				}
				else
					SoundManager.PlayEffect("menu_bad_select", 1f);
			}

			//else if (fitModuleInfoOverlay.CurrentModule is Armor)
			//{

			//}
			//else if (fitModuleInfoOverlay.CurrentModule is Engine)
			//{

			//}
			//else if (fitModuleInfoOverlay.CurrentModule is Generator)
			//{

			//}
			//else if (fitModuleInfoOverlay.CurrentModule is Shield)
			//{

			//}
			//else if (fitModuleInfoOverlay.CurrentModule is Utility)
			//{

			//}
			//else if (fitModuleInfoOverlay.CurrentModule is SpinalWeapon)
			//{

			//}
			//else if (fitModuleInfoOverlay.CurrentModule is TurretWeapon)
			//{

			//}

		}
		#endregion

		#region ship handlers
		private void ShipPilot_ChangedShip(object o, ChangedShipHandlerEventArgs e)
		{
			Ship currentShip = GameManager.TheGameManager.ThePlayer.CurrentShip;
			
			Fighter thisFighter;
			Frigate thisFrigate;
			Cruiser thisCruiser;
			Battleship thisBattleship;
			Capitalship thisCapitalship;

			//Set ship mode ------------------------
			//clear buttons
			moduleFittingButtonOverlay.Buttons.Clear();

			if (currentShip is NullShip)
			{
				CurrentShipMode = ShipModes.Null;
			} else {
				//create new buttons
				Vector2 shieldButtonPosition = new Vector2(moduleFittingButtonOverlay.Width / 2, 150);
				ButtonToggleModule shieldButton = new ButtonToggleModule(moduleFittingButtonOverlay, DockedScreenTextureManager.GetFont("kootenay14"), currentShip.ShieldSlot1.Name, shieldButtonPosition, currentShip.ShieldSlot1);
				shieldButton.Clicked += Button_OnClick;
				shieldButton.Clicked += FittingModuleButton_OnClick;
				moduleFittingButtonOverlay.Buttons.Add(shieldButton);

				Vector2 armorButtonPosition = new Vector2(moduleFittingButtonOverlay.Width / 2, 180);
				ButtonToggleModule armorButton = new ButtonToggleModule(moduleFittingButtonOverlay, DockedScreenTextureManager.GetFont("kootenay14"), currentShip.ArmorSlot1.Name, armorButtonPosition, currentShip.ArmorSlot1);
				armorButton.Clicked += Button_OnClick;
				armorButton.Clicked += FittingModuleButton_OnClick;
				moduleFittingButtonOverlay.Buttons.Add(armorButton);

				Vector2 generatorButtonPosition = new Vector2(moduleFittingButtonOverlay.Width / 2, 210);
				ButtonToggleModule generatorButton = new ButtonToggleModule(moduleFittingButtonOverlay, DockedScreenTextureManager.GetFont("kootenay14"), currentShip.GeneratorSlot1.Name, generatorButtonPosition, currentShip.GeneratorSlot1);
				generatorButton.Clicked += Button_OnClick;
				generatorButton.Clicked += FittingModuleButton_OnClick;
				moduleFittingButtonOverlay.Buttons.Add(generatorButton);

				Vector2 engineButtonPosition = new Vector2(moduleFittingButtonOverlay.Width / 2, 240);
				ButtonToggleModule engineButton = new ButtonToggleModule(moduleFittingButtonOverlay, DockedScreenTextureManager.GetFont("kootenay14"), currentShip.EngineSlot1.Name, engineButtonPosition, currentShip.EngineSlot1);
				engineButton.Clicked += Button_OnClick;
				engineButton.Clicked += FittingModuleButton_OnClick;
				moduleFittingButtonOverlay.Buttons.Add(engineButton);
			}

			if (currentShip is Fighter)
			{
				CurrentShipMode = ShipModes.Fighter;
				thisFighter = (Fighter)currentShip;

				Vector2 spinalWeaponButtonPosition = new Vector2(moduleFittingButtonOverlay.Width / 2, 120);
				ButtonToggleModule spinalWeaponButton = new ButtonToggleModule(moduleFittingButtonOverlay, DockedScreenTextureManager.GetFont("kootenay14"), thisFighter.SpinalWeaponSlot1.Name, spinalWeaponButtonPosition, thisFighter.SpinalWeaponSlot1);
				spinalWeaponButton.Clicked += Button_OnClick;
				spinalWeaponButton.Clicked += FittingModuleButton_OnClick;
				moduleFittingButtonOverlay.Buttons.Add(spinalWeaponButton);
			}
			else if (currentShip is Frigate)
			{
				CurrentShipMode = ShipModes.Frigate;
				thisFrigate = (Frigate)currentShip;

				Vector2 spinalWeaponButtonPosition = new Vector2(moduleFittingButtonOverlay.Width / 2, 90);
				ButtonToggleModule spinalWeaponButton = new ButtonToggleModule(moduleFittingButtonOverlay, DockedScreenTextureManager.GetFont("kootenay14"), thisFrigate.SpinalWeaponSlot1.Name, spinalWeaponButtonPosition, thisFrigate.SpinalWeaponSlot1);
				spinalWeaponButton.Clicked += Button_OnClick;
				spinalWeaponButton.Clicked += FittingModuleButton_OnClick;
				moduleFittingButtonOverlay.Buttons.Add(spinalWeaponButton);

				Vector2 turretWeaponButtonPosition = new Vector2(moduleFittingButtonOverlay.Width / 2, 120);
				ButtonToggleModule turretWeaponButton = new ButtonToggleModule(moduleFittingButtonOverlay, DockedScreenTextureManager.GetFont("kootenay14"), thisFrigate.TurretWeaponSlot1.Name, turretWeaponButtonPosition, thisFrigate.TurretWeaponSlot1);
				turretWeaponButton.Clicked += Button_OnClick;
				turretWeaponButton.Clicked += FittingModuleButton_OnClick;
				moduleFittingButtonOverlay.Buttons.Add(turretWeaponButton);
			}
			else if (currentShip is Cruiser)
			{
				CurrentShipMode = ShipModes.Cruiser;
				thisCruiser = (Cruiser)currentShip;

				Vector2 spinalWeaponButtonPosition = new Vector2(moduleFittingButtonOverlay.Width / 2, 90);
				ButtonToggleModule spinalWeaponButton = new ButtonToggleModule(moduleFittingButtonOverlay, DockedScreenTextureManager.GetFont("kootenay14"), thisCruiser.SpinalWeaponSlot1.Name, spinalWeaponButtonPosition, thisCruiser.SpinalWeaponSlot1);
				spinalWeaponButton.Clicked += Button_OnClick;
				spinalWeaponButton.Clicked += FittingModuleButton_OnClick;
				moduleFittingButtonOverlay.Buttons.Add(spinalWeaponButton);

				Vector2 turretWeaponButtonPosition = new Vector2(moduleFittingButtonOverlay.Width / 4, 120);
				ButtonToggleModule turretWeaponButton = new ButtonToggleModule(moduleFittingButtonOverlay, DockedScreenTextureManager.GetFont("kootenay14"), thisCruiser.TurretWeaponSlot1.Name, turretWeaponButtonPosition, thisCruiser.TurretWeaponSlot1);
				turretWeaponButton.Clicked += Button_OnClick;
				turretWeaponButton.Clicked += FittingModuleButton_OnClick;
				moduleFittingButtonOverlay.Buttons.Add(turretWeaponButton);

				Vector2 turretWeapon2ButtonPosition = new Vector2((int)(moduleFittingButtonOverlay.Width * 0.75f), 120);
				ButtonToggleModule turret2WeaponButton = new ButtonToggleModule(moduleFittingButtonOverlay, DockedScreenTextureManager.GetFont("kootenay14"), thisCruiser.TurretWeaponSlot2.Name, turretWeapon2ButtonPosition, thisCruiser.TurretWeaponSlot2);
				turret2WeaponButton.Clicked += Button_OnClick;
				turret2WeaponButton.Clicked += FittingModuleButton_OnClick;
				moduleFittingButtonOverlay.Buttons.Add(turret2WeaponButton);
			}
			else if (currentShip is Battleship)
			{
				CurrentShipMode = ShipModes.Battleship;
				thisBattleship = (Battleship)currentShip;

				Vector2 spinalWeaponButtonPosition = new Vector2(moduleFittingButtonOverlay.Width / 4, 60);
				ButtonToggleModule spinalWeaponButton = new ButtonToggleModule(moduleFittingButtonOverlay, DockedScreenTextureManager.GetFont("kootenay14"), thisBattleship.SpinalWeaponSlot1.Name, spinalWeaponButtonPosition, thisBattleship.SpinalWeaponSlot1);
				spinalWeaponButton.Clicked += Button_OnClick;
				spinalWeaponButton.Clicked += FittingModuleButton_OnClick;
				moduleFittingButtonOverlay.Buttons.Add(spinalWeaponButton);

				Vector2 spinalWeapon2ButtonPosition = new Vector2((int)(moduleFittingButtonOverlay.Width * 0.75f), 60);
				ButtonToggleModule spinalWeapon2Button = new ButtonToggleModule(moduleFittingButtonOverlay, DockedScreenTextureManager.GetFont("kootenay14"), thisBattleship.SpinalWeaponSlot2.Name, spinalWeapon2ButtonPosition, thisBattleship.SpinalWeaponSlot2);
				spinalWeapon2Button.Clicked += Button_OnClick;
				spinalWeapon2Button.Clicked += FittingModuleButton_OnClick;
				moduleFittingButtonOverlay.Buttons.Add(spinalWeapon2Button);

				Vector2 turretWeaponButtonPosition = new Vector2(moduleFittingButtonOverlay.Width / 4, 90);
				ButtonToggleModule turretWeaponButton = new ButtonToggleModule(moduleFittingButtonOverlay, DockedScreenTextureManager.GetFont("kootenay14"), thisBattleship.TurretWeaponSlot1.Name, turretWeaponButtonPosition, thisBattleship.TurretWeaponSlot1);
				turretWeaponButton.Clicked += Button_OnClick;
				turretWeaponButton.Clicked += FittingModuleButton_OnClick;
				moduleFittingButtonOverlay.Buttons.Add(turretWeaponButton);

				Vector2 turretWeapon2ButtonPosition = new Vector2(moduleFittingButtonOverlay.Width / 4, 120);
				ButtonToggleModule turret2WeaponButton = new ButtonToggleModule(moduleFittingButtonOverlay, DockedScreenTextureManager.GetFont("kootenay14"), thisBattleship.TurretWeaponSlot2.Name, turretWeapon2ButtonPosition, thisBattleship.TurretWeaponSlot2);
				turret2WeaponButton.Clicked += Button_OnClick;
				turret2WeaponButton.Clicked += FittingModuleButton_OnClick;
				moduleFittingButtonOverlay.Buttons.Add(turret2WeaponButton);

				Vector2 turretWeapon3ButtonPosition = new Vector2((int)(moduleFittingButtonOverlay.Width * 0.75f), 90);
				ButtonToggleModule turret3WeaponButton = new ButtonToggleModule(moduleFittingButtonOverlay, DockedScreenTextureManager.GetFont("kootenay14"), thisBattleship.TurretWeaponSlot3.Name, turretWeapon3ButtonPosition, thisBattleship.TurretWeaponSlot3);
				turret3WeaponButton.Clicked += Button_OnClick;
				turret3WeaponButton.Clicked += FittingModuleButton_OnClick;
				moduleFittingButtonOverlay.Buttons.Add(turret3WeaponButton);

				Vector2 turretWeapon4ButtonPosition = new Vector2((int)(moduleFittingButtonOverlay.Width * 0.75f), 120);
				ButtonToggleModule turret4WeaponButton = new ButtonToggleModule(moduleFittingButtonOverlay, DockedScreenTextureManager.GetFont("kootenay14"), thisBattleship.TurretWeaponSlot4.Name, turretWeapon4ButtonPosition, thisBattleship.TurretWeaponSlot4);
				turret4WeaponButton.Clicked += Button_OnClick;
				turret4WeaponButton.Clicked += FittingModuleButton_OnClick;
				moduleFittingButtonOverlay.Buttons.Add(turret4WeaponButton);
			}
			else if (currentShip is Capitalship)
			{
				CurrentShipMode = ShipModes.Capital;
				thisCapitalship = (Capitalship)currentShip;

				Vector2 spinalWeaponButtonPosition = new Vector2(moduleFittingButtonOverlay.Width / 4, 30);
				ButtonToggleModule spinalWeaponButton = new ButtonToggleModule(moduleFittingButtonOverlay, DockedScreenTextureManager.GetFont("kootenay14"), thisCapitalship.SpinalWeaponSlot1.Name, spinalWeaponButtonPosition, thisCapitalship.SpinalWeaponSlot1);
				spinalWeaponButton.Clicked += Button_OnClick;
				spinalWeaponButton.Clicked += FittingModuleButton_OnClick;
				moduleFittingButtonOverlay.Buttons.Add(spinalWeaponButton);

				Vector2 spinalWeapon2ButtonPosition = new Vector2(moduleFittingButtonOverlay.Width / 2, 0);
				ButtonToggleModule spinalWeapon2Button = new ButtonToggleModule(moduleFittingButtonOverlay, DockedScreenTextureManager.GetFont("kootenay14"), thisCapitalship.SpinalWeaponSlot2.Name, spinalWeapon2ButtonPosition, thisCapitalship.SpinalWeaponSlot2);
				spinalWeapon2Button.Clicked += Button_OnClick;
				spinalWeapon2Button.Clicked += FittingModuleButton_OnClick;
				moduleFittingButtonOverlay.Buttons.Add(spinalWeapon2Button);

				Vector2 spinalWeapon3ButtonPosition = new Vector2((int)(moduleFittingButtonOverlay.Width * 0.75f), 30);
				ButtonToggleModule spinalWeapon3Button = new ButtonToggleModule(moduleFittingButtonOverlay, DockedScreenTextureManager.GetFont("kootenay14"), thisCapitalship.SpinalWeaponSlot3.Name, spinalWeapon3ButtonPosition, thisCapitalship.SpinalWeaponSlot3);
				spinalWeapon3Button.Clicked += Button_OnClick;
				spinalWeapon3Button.Clicked += FittingModuleButton_OnClick;
				moduleFittingButtonOverlay.Buttons.Add(spinalWeapon3Button);

				Vector2 turretWeaponButtonPosition = new Vector2(moduleFittingButtonOverlay.Width / 4, 60);
				ButtonToggleModule turretWeaponButton = new ButtonToggleModule(moduleFittingButtonOverlay, DockedScreenTextureManager.GetFont("kootenay14"), thisCapitalship.TurretWeaponSlot1.Name, turretWeaponButtonPosition, thisCapitalship.TurretWeaponSlot1);
				turretWeaponButton.Clicked += Button_OnClick;
				turretWeaponButton.Clicked += FittingModuleButton_OnClick;
				moduleFittingButtonOverlay.Buttons.Add(turretWeaponButton);

				Vector2 turretWeapon2ButtonPosition = new Vector2(moduleFittingButtonOverlay.Width / 4, 90);
				ButtonToggleModule turret2WeaponButton = new ButtonToggleModule(moduleFittingButtonOverlay, DockedScreenTextureManager.GetFont("kootenay14"), thisCapitalship.TurretWeaponSlot2.Name, turretWeapon2ButtonPosition, thisCapitalship.TurretWeaponSlot2);
				turret2WeaponButton.Clicked += Button_OnClick;
				turret2WeaponButton.Clicked += FittingModuleButton_OnClick;
				moduleFittingButtonOverlay.Buttons.Add(turret2WeaponButton);

				Vector2 turretWeapon3ButtonPosition = new Vector2(moduleFittingButtonOverlay.Width / 4, 120);
				ButtonToggleModule turret3WeaponButton = new ButtonToggleModule(moduleFittingButtonOverlay, DockedScreenTextureManager.GetFont("kootenay14"), thisCapitalship.TurretWeaponSlot3.Name, turretWeapon3ButtonPosition, thisCapitalship.TurretWeaponSlot3);
				turret3WeaponButton.Clicked += Button_OnClick;
				turret3WeaponButton.Clicked += FittingModuleButton_OnClick;
				moduleFittingButtonOverlay.Buttons.Add(turret3WeaponButton);

				Vector2 turretWeapon4ButtonPosition = new Vector2((int)(moduleFittingButtonOverlay.Width * 0.75f), 60);
				ButtonToggleModule turret4WeaponButton = new ButtonToggleModule(moduleFittingButtonOverlay, DockedScreenTextureManager.GetFont("kootenay14"), thisCapitalship.TurretWeaponSlot4.Name, turretWeapon4ButtonPosition, thisCapitalship.TurretWeaponSlot4);
				turret4WeaponButton.Clicked += Button_OnClick;
				turret4WeaponButton.Clicked += FittingModuleButton_OnClick;
				moduleFittingButtonOverlay.Buttons.Add(turret4WeaponButton);

				Vector2 turretWeapon5ButtonPosition = new Vector2((int)(moduleFittingButtonOverlay.Width * 0.75f), 90);
				ButtonToggleModule turret5WeaponButton = new ButtonToggleModule(moduleFittingButtonOverlay, DockedScreenTextureManager.GetFont("kootenay14"), thisCapitalship.TurretWeaponSlot5.Name, turretWeapon5ButtonPosition, thisCapitalship.TurretWeaponSlot5);
				turret5WeaponButton.Clicked += Button_OnClick;
				turret5WeaponButton.Clicked += FittingModuleButton_OnClick;
				moduleFittingButtonOverlay.Buttons.Add(turret5WeaponButton);

				Vector2 turretWeapon6ButtonPosition = new Vector2((int)(moduleFittingButtonOverlay.Width * 0.75f), 120);
				ButtonToggleModule turret6WeaponButton = new ButtonToggleModule(moduleFittingButtonOverlay, DockedScreenTextureManager.GetFont("kootenay14"), thisCapitalship.TurretWeaponSlot6.Name, turretWeapon6ButtonPosition, thisCapitalship.TurretWeaponSlot6);
				turret6WeaponButton.Clicked += Button_OnClick;
				turret6WeaponButton.Clicked += FittingModuleButton_OnClick;
				moduleFittingButtonOverlay.Buttons.Add(turret6WeaponButton);
			}
		}
		#endregion

		#region station handlers
		private void Station_OnShipAdded(object o, StationShipAddRemoveEventArgs e)
		{
			ButtonToggleShip button = new ButtonToggleShip(shipListOverlay, DockedScreenTextureManager.GetFont("kootenay14"), e.TheShip.ShipName, new Vector2(0, 0), e.TheShip);
			button.Clicked += Button_OnClick;
			button.Clicked += ShipListButton_OnClick;
			shipListOverlay.Buttons.Add(button);
		}

		private void Station_OnShipRemoved(object o, StationShipAddRemoveEventArgs e)
		{
			ButtonToggleShip removeMe = null;
			foreach (Button button in shipListOverlay.Buttons)
			{
				if (button is ButtonToggleShip)
				{
					removeMe = (ButtonToggleShip)button;
					if (removeMe.ButtonShip != e.TheShip)
						removeMe = null;
					else
						break;
				}
			}

			if (removeMe!= null)
				shipListOverlay.Buttons.Remove(removeMe);
		}

		private void Station_OnModuleAdded(object o, StationModuleAddRemoveEventArgs e)
		{
			ButtonToggleModule button = new ButtonToggleModule(moduleListOverlay, DockedScreenTextureManager.GetFont("kootenay14"), e.TheModule.Name, new Vector2(0, 0), e.TheModule);
			button.Clicked += Button_OnClick;
			button.Clicked += ModuleListButton_OnClick;
			moduleListOverlay.Buttons.Add(button);
		}

		private void Station_OnModuleRemoved(object o, StationModuleAddRemoveEventArgs e)
		{
			ButtonToggleModule removeMe = null;
			foreach (Button button in moduleListOverlay.Buttons)
			{
				if (button is ButtonToggleModule)
				{
					removeMe = (ButtonToggleModule)button;
					if (removeMe.ButtonModule != e.TheModule)
						removeMe = null;
					else
						break;
				}
			}

			if (removeMe != null)
				moduleListOverlay.Buttons.Remove(removeMe);
		}
		#endregion
		#endregion
	}
}
