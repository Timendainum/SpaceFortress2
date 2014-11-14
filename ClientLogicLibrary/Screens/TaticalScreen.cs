#region Using Statements
using System;
using ClientLogicLibrary.Simulation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ClientLogicLibrary.ScreenManagement;
using ClientLogicLibrary.Graphics;
using ClientLogicLibrary.Effects;
using ClientLogicLibrary.Overlays;
using GameLogicLibrary.Simulation;
using ClientLogicLibrary.Immobiles;
using ClientLogicLibrary.Mobiles;
using System.Collections.Generic;
using GameLogicLibrary.Mobiles;
using GameLogicLibrary.Mobiles.Ships;
using GameLogicLibrary.Mobiles.Modules.Weapons;
using GameLogicLibrary.Immobiles;
using GameLogicLibrary.Maths;
using GameLogicLibrary.Mobiles.Npcs;
using ClientLogicLibrary.Overlays.TaticalScreenOverlays;

#endregion

namespace ClientLogicLibrary.Screens
{
    /// <summary>
    /// This screen implements the actual game logic. It is just a
    /// placeholder to get the idea across: you'll probably want to
    /// put some more interesting gameplay in here!
    /// </summary>
    public class TaticalScreen : GameScreen
    {
        #region Declarations

        ContentManager content;
		float pauseAlpha;

		private const int _bubblePadding = 500;
		private const float _gameOverMaxTime = 5.0f;
		private float _gameOverTimer = 0.0f;

		private BackgroundOverlay backgroundOverlay;
		private DebugOverlay debugOverlay;
		private ShortRangeScannerOverlay shortRangeScannerOverlay;
		private MinimapOverlay minimapOverlay;

		public List<ClientImmobile> ClientImmobiles = new List<ClientImmobile>();
		public List<ClientMobile> ClientMobiles = new List<ClientMobile>();
		public List<ClientProjectile> ClientProjectiles = new List<ClientProjectile>();
		public ClientPlayer ThePlayer;
		
		#endregion

        #region Initialization
        /// <summary>
        /// Constructor.
        /// </summary>
        public TaticalScreen()
        {			
			TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
			
        }


        /// <summary>
        /// Load graphics content for the game.
        /// </summary>
        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

			//Call client manager
			//This screens 
			EffectManager.ResetParticles();
			TaticalScreenTextureManager.Init(content);
			
			//add player to Client

			ThePlayer = new ClientPlayer(GameManager.TheGameManager.ThePlayer);
			ThePlayer.ServerPlayer.ChangedShip += ShipPilot_ChangedShip;
			ThePlayer.ServerPlayer.OnChangedShip(new ChangedShipHandlerEventArgs(null, ThePlayer.ServerPlayer.CurrentShip));
			
			//Set up display and bubble
			Bubble.BubbleSize = new Vector2(ScreenManager.ViewportRectangle.Width + _bubblePadding, ScreenManager.ViewportRectangle.Height + _bubblePadding);
			//Set up camera
			Camera.ViewPortSize = new Vector2(ScreenManager.ViewportRectangle.Width, ScreenManager.ViewportRectangle.Height);
			Camera.MoveCenterTo(ThePlayer.ServerPlayer.WorldLocation);

			//Update Bubble position
			Bubble.BubbleSize = new Vector2(Camera.ViewPortWidth + _bubblePadding, Camera.ViewPortHeight + _bubblePadding);
			Bubble.MoveCenterTo(Camera.WorldCenter);

			//Instance overlays
			backgroundOverlay = new BackgroundOverlay(ThePlayer.ServerPlayer.CurrentSector);
			debugOverlay = new DebugOverlay(ThePlayer);
			shortRangeScannerOverlay = new ShortRangeScannerOverlay(ThePlayer.ServerPlayer);
			minimapOverlay = new MinimapOverlay(ThePlayer.ServerPlayer);

			foreach (Mobile mob in ThePlayer.ServerPlayer.CurrentSector.Mobiles)
			{
				ClientMobiles.Add(ClientMobileFactory.Create(mob));
				if (mob is ShipPilot)
				{
					ShipPilot sp = (ShipPilot)mob;
					sp.CurrentShip.ShipFiredSpinalWeapon += Ship_OnShipFiredSpinalWeapon;
					sp.CurrentShip.ShipFiredTurretWeapon += Ship_OnShipFiredTurretWeapon;
					sp.CurrentShip.ShipDestroyed += Ship_OnShipDestroyed;
				}
			}

			foreach (Projectile proj in ThePlayer.ServerPlayer.CurrentSector.Projectiles)
			{
				ClientProjectiles.Add(ClientProjectileFactory.Create(proj));
				proj.HitEntity += Projectile_OnHitEntity;
			}

			//Populate client with objects that already exit in sector 
			foreach (Immobile imob in ThePlayer.ServerPlayer.CurrentSector.Immobiles)
			{
				ClientImmobiles.Add(ClientImmobileFactory.Create(imob));
			}
	
			//hook up sector event handlers
			foreach (Sector sector in GameManager.TheGameManager.GameUniverse.Sectors)
			{
				sector.ImmobileAdded += Sector_ImmobileAdded;
				sector.MobileAdded += Sector_MobileAdded;
				sector.ProjectileAdded += Sector_ProjectileAdded;
				//sector.ProjectileRemoved += Sector_ProjectileRemoved;
			}

			

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
			ClientProjectiles.Clear();
			ClientMobiles.Clear();
			ClientImmobiles.Clear();
			
			//TaticalScreenTextureManager.Unload();
            content.Unload();
        }
        #endregion

        #region Update / handle input / Draw


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
				if (!ThePlayer.ServerPlayer.Expired)
				{


					//update client side objects
					//update imobs
					Queue<ClientImmobile> imobToRemove = new Queue<ClientImmobile>();
					foreach (ClientImmobile entity in ClientImmobiles)
					{
						entity.Update(gameTime);
						if (entity.ServerImmobile.Expired)
							imobToRemove.Enqueue(entity);
					}

					for (int i = 0; i < imobToRemove.Count; i++)
					{
						ClientImmobiles.Remove(imobToRemove.Dequeue());
					}

					//Update mobs
					Queue<ClientMobile> mobsToRemove = new Queue<ClientMobile>();
					foreach (ClientMobile entity in ClientMobiles)
					{
						entity.Update(gameTime);
						if (entity.ServerMobile.Expired)
							mobsToRemove.Enqueue(entity);
					}

					for (int i = 0; i < mobsToRemove.Count; i++)
					{
						ClientMobiles.Remove(mobsToRemove.Dequeue());
					}

					//Update projectiles
					Queue<ClientProjectile> projectilesToRemove = new Queue<ClientProjectile>();
					foreach (ClientProjectile entity in ClientProjectiles)
					{
						entity.Update(gameTime);
						if (entity.ServerMobile.Expired)
							projectilesToRemove.Enqueue(entity);
					}

					//Remove projectiles
					for (int i = 0; i < projectilesToRemove.Count; i++)
					{
						ClientProjectiles.Remove(projectilesToRemove.Dequeue());
					}

					//Update the player
					ThePlayer.Update(gameTime);
				}
				else
				{
					float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
					_gameOverTimer += elapsed;
					if (_gameOverTimer > _gameOverMaxTime)
					{
						LoadingScreen.Load(ScreenManager, true, new DockedScreen(GameManager.TheGameManager.ThePlayer.HomeStation));
					}
				}

				//Update Camera position
				Camera.ViewPortSize = new Vector2(ScreenManager.ViewportRectangle.Width, ScreenManager.ViewportRectangle.Height);
				Camera.MoveCenterTo(ThePlayer.ServerPlayer.WorldCenter);

				//Update Bubble position
				Bubble.BubbleSize = new Vector2(Camera.ViewPortWidth + _bubblePadding, Camera.ViewPortHeight + _bubblePadding);
				Bubble.MoveCenterTo(Camera.WorldCenter);

				//Update effect manager
				EffectManager.Update(gameTime);

				//Update overlays
				backgroundOverlay.Update(gameTime);
				debugOverlay.Update(gameTime);
				minimapOverlay.Update(gameTime);
            }
        }

		/// <summary>
        /// Lets the game respond to player input. Unlike the Update method,
        /// this will only be called when the gameplay screen is active.
        /// </summary>
        public override void HandleInput(InputState input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            KeyboardState keyboardState = input.CurrentKeyboardState;
            GamePadState gamePadState = input.CurrentGamePadState;
			MouseState mouseState = input.CurrentMouseState;
			Vector2 mousePosition = new Vector2(mouseState.X, mouseState.Y);
			float directionToMouse = MathsHelper.DirectInterceptAngle(Camera.ViewPortCenter, mousePosition);


            if (input.IsPauseGame())
            {
                ScreenManager.AddScreen(new PauseMenuScreen());
            }
            else
            {
				//Dock ----------------------------------------------------------------------------
				if (input.IsNewKeyPress(Keys.Tab))
				{
					AttemptDock();
				}

				//Switch ships
				//if (input.IsNewKeyPress(Keys.F1))
				//{
				//	ThePlayer.ServerPlayer.CurrentShip = new HumanFrigate1();
				//}
				//else if (input.IsNewKeyPress(Keys.F2))
				//{
				//	ThePlayer.ServerPlayer.CurrentShip = new HumanCruiser1();
				//}
				//else if (input.IsNewKeyPress(Keys.F3))
				//{
				//	ThePlayer.ServerPlayer.CurrentShip = new HumanBattleship1();
				//}
				//else if (input.IsNewKeyPress(Keys.F4))
				//{
				//	ThePlayer.ServerPlayer.CurrentShip = new HumanCapitalship1();
				//}

				//move the player position --------------------------------------------------------------------
				Vector2 thrust = Vector2.Zero;
				Vector2 rotation = Vector2.Zero;

				if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
					rotation.X--;

				if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
					rotation.X++;

				if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
					thrust.Y--;

				if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
					thrust.Y++;

				Vector2 thumbstick = gamePadState.ThumbSticks.Left;

				rotation.X += thumbstick.X;
				thrust.Y -= thumbstick.Y;


				//mouse thrust
				if (mouseState.RightButton == ButtonState.Pressed)
				{
					thrust.Y--;
					float slop = 0.01f;
					float rotationDifference = MathHelper.WrapAngle(directionToMouse - ThePlayer.ServerPlayer.Rotation);

					if (Math.Abs(rotationDifference) >= slop)
					{
						if (rotationDifference >= 0)
							rotation.X += 1f;
						else
							rotation.X -= 1f;
					}
				}

				if (thrust.Length() > 1)
					thrust.Normalize();
				if (rotation.Length() > 1)
					rotation.Normalize();

				ThePlayer.ServerPlayer.ApplyRotatationalThrust(rotation.X);
				ThePlayer.ServerPlayer.ApplyThrust(thrust.Y);

				//Player shooting -----------------------------------------------------------------
				if (keyboardState.IsKeyDown(Keys.Space) || mouseState.LeftButton == ButtonState.Pressed)
				{
					ThePlayer.ServerPlayer.CurrentShip.FireSpinalWeapons(ThePlayer.ServerPlayer);
					ThePlayer.ServerPlayer.CurrentShip.FireTurretWeapons(ThePlayer.ServerPlayer);
				}

				//align player turrets to mouseState ----------------------------------------------
				foreach (TurretWeapon turret in ThePlayer.ServerPlayer.CurrentShip.TurretWeapons)
				{
					float distanceToWeapon = Vector2.Distance(turret.RelativeFirePosition, ThePlayer.ServerPlayer.RelativeCenter);
					float angleToWeapon = (MathsHelper.DirectInterceptAngle(ThePlayer.ServerPlayer.RelativeCenter, turret.RelativeFirePosition) + ThePlayer.ServerPlayer.Rotation) % MathHelper.TwoPi;

					//Stairt forward toward mouse
					turret.RotateTo = MathsHelper.DirectInterceptAngle(Camera.TransformWorldToCamera(ThePlayer.ServerPlayer.WorldCenter), mousePosition);

					//Direct to mouse
					//Vector2 weaponWorldLocation = MathsHelper.RotateAroundCircle(angleToWeapon, distanceToWeapon, ThePlayer.ServerPlayer.RelativeCenter) + ThePlayer.ServerPlayer.WorldLocation;
					//turret.RotateTo = MathsHelper.DirectInterceptAngle(Camera.TransformWorldToCamera(weaponWorldLocation), mousePosition);
				}

				//Spawn ---------------------------------------------------------------------------
				if (input.IsNewKeyPress(Keys.Enter))
					ThePlayer.ServerPlayer.CurrentSector.AddMobile(new AlienFighterInfestor(Vector2.Zero));

				//handle overlays input -----------------------------------------------------------
				backgroundOverlay.HandleInput(input);
				debugOverlay.HandleInput(input);
				minimapOverlay.HandleInput(input);
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

				backgroundOverlay.Draw(spriteBatch, gameTime);

				foreach (ClientImmobile entity in ClientImmobiles)
				{
					if (Bubble.BubbleRectangle.Intersects(entity.GetWorldRectangle()))
						entity.Draw(spriteBatch);
				}

				foreach (ClientMobile entity in ClientMobiles)
				{
					if (Bubble.BubbleRectangle.Intersects(entity.GetWorldRectangle()))
						entity.Draw(spriteBatch);
				}

				foreach (ClientProjectile entity in ClientProjectiles)
				{
					if (Bubble.BubbleRectangle.Intersects(entity.GetWorldRectangle()))
						entity.Draw(spriteBatch);
				}

				//Draw the player
				ThePlayer.Draw(spriteBatch);

				//Draw effects
				EffectManager.Draw(spriteBatch);

				//Draw overlays
				minimapOverlay.Draw(spriteBatch, gameTime);
				shortRangeScannerOverlay.Draw(spriteBatch, gameTime);
				debugOverlay.Draw(spriteBatch, gameTime);

				if (ThePlayer.ServerPlayer.Expired)
				{
					string gameOverText = "Your ship was blowed up!";

					spriteBatch.DrawString(TaticalScreenTextureManager.GetFont("kootenay36"), gameOverText,
							new Vector2(ScreenManager.ViewportRectangle.Width / 2, ScreenManager.ViewportRectangle.Height / 2),
							Color.White, 0.0f,
							new Vector2(TaticalScreenTextureManager.GetFont("kootenay36").MeasureString(gameOverText).X / 2, TaticalScreenTextureManager.GetFont("kootenay36").MeasureString(gameOverText).Y / 2),
							1.0f, SpriteEffects.None, 0.0f);
				}

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
		private void AttemptDock()
		{
			bool canDock = false;
			Station dockingStation = null;

			foreach (Immobile imob in ThePlayer.ServerPlayer.CurrentSector.Immobiles)
			{
				if (imob is Station)
				{
					Station station = (Station)imob;
					if (Vector2.Distance(ThePlayer.ServerPlayer.WorldCenter, station.WorldCenter) < station.DockingRange)
					{
						canDock = true;
						dockingStation = station;
						break;
					}
					
				}
			}

			if (canDock)
			{
				SoundManager.PlayEffect("menu_select2", 1f);
				GameManager.TheGameManager.ThePlayer.CurrentSector.RemovePlayer(GameManager.TheGameManager.ThePlayer);
				LoadingScreen.Load(ScreenManager, true, new DockedScreen(dockingStation));
			}
			else
			{
				SoundManager.PlayEffect("menu_bad_select", 1f);
			}
		}
		#endregion

		#region event handlers
		private void ShipPilot_ChangedShip(object o, ChangedShipHandlerEventArgs e)
		{
			ShipPilot pilot = (ShipPilot)o;

			//update playerSectorIndex
			if (pilot == ThePlayer.ServerPlayer)
			{
				//Clean up old ship
				if (e.OldShip != null)
				{
					e.OldShip.ShipFiredSpinalWeapon -= Ship_OnShipFiredSpinalWeapon;
					e.OldShip.ShipFiredTurretWeapon -= Ship_OnShipFiredTurretWeapon;
					e.OldShip.ShipDestroyed -= Ship_OnShipDestroyed;
				}

				//handle new ship
				ThePlayer.NewShipRenderer();
				e.NewShip.ShipFiredSpinalWeapon += Ship_OnShipFiredSpinalWeapon;
				e.NewShip.ShipFiredTurretWeapon += Ship_OnShipFiredTurretWeapon;
				e.NewShip.ShipDestroyed += Ship_OnShipDestroyed;
			}

			//Filter out ones that are not in our sector
			if (pilot.CurrentSector != ThePlayer.ServerPlayer.CurrentSector)
				return;
			//Find and handle other players and npcs
		}


		private void Sector_ImmobileAdded(object o, ImmobileAddedHandlerEventArgs e)
		{
			Sector sector = (Sector)o;

			if (sector == ThePlayer.ServerPlayer.CurrentSector)
				ClientImmobiles.Add(ClientImmobileFactory.Create(e.Imob));
		}

		private void Sector_MobileAdded(object o, MobileAddedHandlerEventArgs e)
		{
			Sector sector = (Sector)o;

			if (sector == ThePlayer.ServerPlayer.CurrentSector)
			{
				ClientMobiles.Add(ClientMobileFactory.Create(e.Mob));
				if (e.Mob is ShipPilot)
				{
					ShipPilot sp = (ShipPilot)e.Mob;
					sp.CurrentShip.ShipFiredSpinalWeapon += Ship_OnShipFiredSpinalWeapon;
					sp.CurrentShip.ShipDestroyed += Ship_OnShipDestroyed;
					sp.ChangedShip += ShipPilot_ChangedShip;
				}
			}
		}

		private void Sector_ProjectileAdded(object o, ProjectileAddedHandlerEventArgs e)
		{
			Sector sector = (Sector)o;

			if (sector == ThePlayer.ServerPlayer.CurrentSector)
			{
				ClientProjectiles.Add(ClientProjectileFactory.Create(e.Proj));
				e.Proj.HitEntity += Projectile_OnHitEntity;
			}
		}

		//public void Sector_ProjectileRemoved(object o, ProjectileRemovedHandlerEventArgs e)
		//{
		//	Sector sector = (Sector)o;

		//	if (sector == GameUniverse.Sectors[PlayerSectorIndex])
		//	{
		//		ClientProjectile removeMe = ClientProjectiles.Find(item => item.ServerMobile == e.Proj);
		//		if (removeMe!= null)
		//			ClientProjectiles.Remove(removeMe);
		//	}
		//}

		private void Ship_OnShipFiredSpinalWeapon(object o, ShipFiredSpinalWeaponEventArgs e)
		{
			//figure out sound effect and play it

			SoundManager.PlayEffectRanged(e.WeaponFired.FiredSoundEffect, ThePlayer.ServerPlayer, e.FiredBy);
		}

		private void Ship_OnShipFiredTurretWeapon(object o, ShipFiredTurretWeaponEventArgs e)
		{
			//figure out sound effect and play it

			SoundManager.PlayEffectRanged(e.WeaponFired.FiredSoundEffect, ThePlayer.ServerPlayer, e.FiredBy);
		}

		private void Ship_OnShipDestroyed(object o, ShipDestroyedEventArgs e)
		{
			SoundManager.PlayEffectRanged("explosion1", ThePlayer.ServerPlayer, e.DestroyedShip.ShipPilot);
			EffectManager.AddExplosionEffect(e.DestroyedShip.ShipPilot.WorldCenter);
		}

		private void Projectile_OnHitEntity(object o, HitEntityEventArgs e)
		{
			Projectile proj = (Projectile)o;
			SoundManager.PlayEffectRanged(proj.ImpactSoundEffect, ThePlayer.ServerPlayer, e.EntityHit);

			if (e.EntityHit is Mobile)
			{
				Mobile mob = (Mobile)e.EntityHit;
				EffectManager.AddSparkEffect(e.CollisionPoint);
			}
			else
				EffectManager.AddSparkEffect(e.CollisionPoint);

		}
		#endregion
	}
}
