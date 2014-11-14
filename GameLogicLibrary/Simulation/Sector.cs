using System;
using System.Collections.Generic;
using GameLogicLibrary.Immobiles;
using GameLogicLibrary.Mobiles;
using GameLogicLibrary.Mobiles.Modules.Weapons;
using Microsoft.Xna.Framework;
using GameLogicLibrary.Immobiles.Lairs;
using GameLogicLibrary.Anomalies;
using GameLogicLibrary.Factions;
using GameLogicLibrary.Maths;

namespace GameLogicLibrary.Simulation
{	
	public class Sector
	{
		#region declarations
		public Random rand = new Random();
		public Universe TheUniverse { get; protected set; }

		public float Elapsed { get; protected set; }

        public event ImmobileAddedHandler ImmobileAdded;
		public event MobileAddedHandler MobileAdded;
		public event ProjectileAddedHandler ProjectileAdded;
		public event ProjectileRemovedHandler ProjectileRemoved;
        public event PlayerAddedHandler PlayerAdded;
        public event PlayerRemovedHandler PlayerRemoved;

		public Faction OwnerFaction { get; protected set; }

		public Station CapitalStation;

		private List<SectorBackgroundObject> _SectorBackgroundObjects = new List<SectorBackgroundObject>();
		public List<SectorBackgroundObject> SectorBackgroundObjects
		{
			get
			{
				return _SectorBackgroundObjects;
			}
			set
			{
				_SectorBackgroundObjects = value;
			}
		}

		private List<Anomaly> _Anomalies = new List<Anomaly>();
		public List<Anomaly> Anomalies
		{
			get
			{
				return _Anomalies;
			}
			set
			{
				_Anomalies = value;
			}
		}

		private List<Immobile> _Immobiles = new List<Immobile>();
		public List<Immobile> Immobiles
		{
			get
			{
				return _Immobiles;
			}
			set
			{
				_Immobiles = value;
			}
		}

		private List<Mobile> _Mobiles = new List<Mobile>();
		public List<Mobile> Mobiles
		{
			get
			{
				return _Mobiles;
			}
			set
			{
				_Mobiles = value;
			}
		}

		private List<Player> _Players = new List<Player>();
		public List<Player> Players
		{
			get
			{
				return _Players;
			}
			set
			{
				_Players = value;
			}
		}

		private List<Projectile> _Projectiles = new List<Projectile>();
		public List<Projectile> Projectiles
		{
			get
			{
				return _Projectiles;
			}
			set
			{
				_Projectiles = value;
			}
		}
		#endregion

		#region constructor
		public Sector(Universe universe, Faction ownerFaction)
		{
			TheUniverse = universe;
			OwnerFaction = ownerFaction;

			SectorBackgroundObjects.Add(new SectorBackgroundObject(Vector2.Zero, new Vector2(4096, 4096), "starfield", 0.9999f));
			SectorBackgroundObjects.Add(new SectorBackgroundObject(Vector2.Zero, new Vector2(4096, 4096), "starfield_bigstars", 0.999f));
			Vector2 starPosition = new Vector2(RandomManager.TheRandom.Next(-50000, 50000), RandomManager.TheRandom.Next(-50000, 50000));
			SectorBackgroundObjects.Add(new SectorBackgroundObject(starPosition, new Vector2(1000, 1000), "star_orange", 0.99f));
			//backgroundObjects.Add(new BackgroundObject(Vector2.Zero, new Vector2(4096, 4096), "starfield", 0.9999f));
			//backgroundObjects.Add(new BackgroundObject(Vector2.Zero, new Vector2(4096, 4096), "starfield_bigstars", 0.999f));
			//backgroundObjects.Add(new BackgroundObject(new Vector2(35000, -30000), new Vector2(1000, 1000), "star_orange", 0.99f));
			//backgroundObjects.Add(new BackgroundObject(new Vector2(-5000, -5000), new Vector2(870, 870), "planet_green", 0.9f));
			//backgroundObjects.Add(new BackgroundObject(new Vector2(2000, 2000), new Vector2(590, 590), "planet_blue", 0.8f));

			GenerateAnomalies();

		}

		#endregion

		#region methods
		#region xna
		public void Update(GameTime gameTime)
		{
			Elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
			//imobs
			Queue<Immobile> imobsToRemove = new Queue<Immobile>();
			foreach (Immobile imob in Immobiles)
			{
				imob.Update(gameTime);
				if (imob.Expired)
					imobsToRemove.Enqueue(imob);
			}

			for (int i = 0; i < imobsToRemove.Count; i++)
			{
				Immobiles.Remove(imobsToRemove.Dequeue());
			}
			
			//mobs
			Queue<Mobile> mobsToRemove = new Queue<Mobile>();
			foreach (Mobile mob in Mobiles)
			{
				mob.Update(gameTime);
				if (mob.Expired)
					mobsToRemove.Enqueue(mob);
			}

			for (int i = 0; i < mobsToRemove.Count; i++)
			{
				Mobiles.Remove(mobsToRemove.Dequeue());
			}

			Queue<Player> playersToRemove = new Queue<Player>();
			foreach (Player player in Players)
			{
				player.Update(gameTime);
				if (player.Expired)
					playersToRemove.Enqueue(player);
			}

			for (int i = 0; i < playersToRemove.Count; i++)
			{
				Players.Remove(playersToRemove.Dequeue());
			}

			Queue<Projectile> projToRemove = new Queue<Projectile>();
			foreach (Projectile proj in Projectiles)
			{
				proj.Update(gameTime);
				if (proj.Expired)
					projToRemove.Enqueue(proj);
			}

			//Remove expired bullets
			for (int i = 0; i < projToRemove.Count; i++)
			{
				Projectile proj = projToRemove.Dequeue();
				OnProjectileRemoved(new ProjectileRemovedHandlerEventArgs(proj));
				Projectiles.Remove(proj);
			}

			//detect collisions
			DetectCollisionsWithProjectiles();
			DetectCollisionsWithImmobies();
			DetectCollisionsWithMobies();

		}
		#endregion

		#region collision detection
		private void DetectCollisionsWithImmobies()
		{
			foreach (Mobile mob in Mobiles)
			{
				foreach (Immobile imob in Immobiles)
				{
					List<Vector2> collisionPoints;
					if (CollisionHelper.IsColliding(mob, imob, out collisionPoints))
					{
						Vector2 collisionPoint = CollisionHelper.GetSignificantCollisionPoint(mob, imob, collisionPoints, Elapsed);
						CollisionHelper.CorrectCollision(mob, imob, collisionPoints, Elapsed);
						CollisionHelper.BounceOffImmobile(mob, imob, collisionPoint);
					}
				}
			}

			foreach (Player player in Players)
			{
				foreach (Immobile imob in Immobiles)
				{
					List<Vector2> collisionPoints;
					if (CollisionHelper.IsColliding(player, imob, out collisionPoints))
					{
						Vector2 collisionPoint = CollisionHelper.GetSignificantCollisionPoint(player, imob, collisionPoints, Elapsed);
						CollisionHelper.CorrectCollision(player, imob, collisionPoints, Elapsed);
						CollisionHelper.BounceOffImmobile(player, imob, collisionPoint);
					}
				}
			}
		}

		private void DetectCollisionsWithMobies()
		{
			
			for (int x = 0; x < Mobiles.Count; x++)
			{
				for (int y = x + 1; y < Mobiles.Count; y++)
				{
					List<Vector2> collisionPoints;
					if (CollisionHelper.IsColliding(Mobiles[x], Mobiles[y], out collisionPoints))
					{
						Vector2 collisionPoint = CollisionHelper.GetSignificantCollisionPoint(Mobiles[x], Mobiles[y], collisionPoints, Elapsed);
						CollisionHelper.CorrectCollision(Mobiles[x], Mobiles[y], collisionPoints, Elapsed);
						CollisionHelper.BounceOffMobile(Mobiles[x], Mobiles[y], collisionPoint);
					}
					//if (CollisionHelper.IsCircleColliding(Mobiles[x], Mobiles[y]))
					//{
					//	CollisionHelper.CorrectCollision(Mobiles[x], Elapsed);
					//	CollisionHelper.BounceOffMobile(Mobiles[x], Mobiles[y]);
					//}
				}
			}

			

			foreach (Player player in Players)
			{
				foreach (Mobile imob in Mobiles)
				{
					List<Vector2> collisionPoints;
					if (CollisionHelper.IsColliding(player, imob, out collisionPoints))
					{
						Vector2 collisionPoint = CollisionHelper.GetSignificantCollisionPoint(player, imob, collisionPoints, Elapsed);
						CollisionHelper.CorrectCollision(player, imob, collisionPoints, Elapsed);
						CollisionHelper.BounceOffMobile(player, imob, collisionPoint);
					}
					//if (CollisionHelper.IsCircleColliding(player, imob))
					//{
					//	CollisionHelper.CorrectCollision(player, Elapsed);
					//	CollisionHelper.BounceOffMobile(player, imob);
					//}
				}
			}

			return;

			foreach (Player player in Players)
			{
				foreach (Player player2 in Players)
				{
					List<Vector2> collisionPoints;
					if (CollisionHelper.IsColliding(player, player2, out collisionPoints))
					{
						Vector2 collisionPoint = CollisionHelper.GetSignificantCollisionPoint(player, player2, collisionPoints, Elapsed);
						CollisionHelper.CorrectCollision(player, player2, collisionPoints, Elapsed);
						CollisionHelper.BounceOffMobile(player, player2, collisionPoint);
					}
					//if (CollisionHelper.IsCircleColliding(player, player2))
					//{
					//	CollisionHelper.CorrectCollision(player, Elapsed);
					//	CollisionHelper.BounceOffMobile(player, player2);
					//}
				}
			}
		}

		private void DetectCollisionsWithProjectiles()
		{
			foreach (Projectile proj in Projectiles)
			{
				//Check immobiles
				foreach (Immobile imob in Immobiles)
				{
					List<Vector2> collisionPoints;
					if (proj.FiredBy != imob && CollisionHelper.IsColliding(proj, imob, out collisionPoints))
					{
						Vector2 collisionPoint = CollisionHelper.GetSignificantCollisionPoint(proj, imob, collisionPoints, Elapsed);
						proj.OnHitEntity(new HitEntityEventArgs(imob, collisionPoint));
					}
				}

				//Check mobs
				foreach (Mobile mob in Mobiles)
				{
					List<Vector2> collisionPoints;
					if (proj.FiredBy != mob && CollisionHelper.IsColliding(proj, mob, out collisionPoints))
					{
						Vector2 collisionPoint = CollisionHelper.GetSignificantCollisionPoint(proj, mob, collisionPoints, Elapsed);
						proj.OnHitEntity(new HitEntityEventArgs(mob, collisionPoint));
					}
				}

				//Check players
				foreach (Player player in Players)
				{
					List<Vector2> collisionPoints;
					if (proj.FiredBy != player && CollisionHelper.IsColliding(proj, player, out collisionPoints))
					{
						Vector2 collisionPoint = CollisionHelper.GetSignificantCollisionPoint(proj, player, collisionPoints, Elapsed);
						proj.OnHitEntity(new HitEntityEventArgs(player, collisionPoint));
					}
				}
			}
		}

		
		#endregion

		#region GenerateAnomalies
		private void GenerateAnomalies()
		{
			//Crosshairs
			//AddImmobile(new Crosshairs(new Vector2(-500,-500)));
			Anomaly newAnomaly = null;
			if (OwnerFaction is TeamHuman)
			{
				newAnomaly = new HumanHomeworld(this, OwnerFaction);
				GenerateNewAnomalyPosition(newAnomaly);
				newAnomaly.GenerateContents();
				Anomalies.Add(newAnomaly);
			}

			int numberOfAnoms = RandomManager.TheRandom.Next(20, 30);

			for (int i = 0; i < numberOfAnoms; i++)
			{
				int random = RandomManager.TheRandom.Next(1, 100);

				if (random >= 90 && i > 10)
				{
					newAnomaly = new HabitablePlanet(this, OwnerFaction);
					GenerateNewAnomalyPosition(newAnomaly);
					newAnomaly.GenerateContents();
					Anomalies.Add(newAnomaly);
				}
				else if (random >= 75)
				{
					newAnomaly = new RockyPlanet(this, TheUniverse.Hostile);
					GenerateNewAnomalyPosition(newAnomaly);
					newAnomaly.GenerateContents();
					Anomalies.Add(newAnomaly);
				}
				else
				{
					newAnomaly = new AsteroidBelt(this, TheUniverse.Hostile);
					GenerateNewAnomalyPosition(newAnomaly);
					newAnomaly.GenerateContents();
					Anomalies.Add(newAnomaly);
				}
			}




			//AddImmobile(new Moon(new Vector2(1200, 1000), MoonType.Medium));

			//AddImmobile(new Moon(new Vector2(-1500, 800), MoonType.Small));


			//AddImmobile(new Moon(new Vector2(-5500, -5200), MoonType.Small));

			//AddImmobile(new Moon(new Vector2(-4900, -5350), MoonType.Medium));

			////Alien Lair
			//AddImmobile(new Moon(new Vector2(-3500, 1350), MoonType.Medium));

			//AddImmobile(new Moon(new Vector2(-4500, 250), MoonType.Small));

			//AddImmobile(new AlienLair1(new Vector2(-5000, 1550)));

			////Alien Lair
			//AddImmobile(new Moon(new Vector2(3500, 350), MoonType.Medium));

			//AddImmobile(new AlienLair1(new Vector2(2500, 100)));

			////AddMobile(new BasicNpc(new Vector2(100, 100)));
		}

		private void GenerateNewAnomalyPosition(Anomaly anom)
		{
			anom.WorldPosition = new Vector2(RandomManager.TheRandom.Next(-2500, 2500), RandomManager.TheRandom.Next(-2500, 2500));

			//check to see if spot is taken, if so try to find a new place to spawn
			bool isSpotFree = false;

			while (!isSpotFree)
			{
				bool isColliding = false;
				foreach (Anomaly anom1 in Anomalies)
				{
					isColliding = anom.GetWorldRectangle().Intersects(anom1.GetWorldRectangle());
					if (isColliding)
						break;
				}

				if (!isColliding)
					isSpotFree = true;
				else
				{
					int offsetDirection = RandomManager.TheRandom.Next(0, 4);
					Vector2 location = anom.WorldPosition;

					switch (offsetDirection)
					{
						case 0:
							location.X += (float)anom.Width;
							break;
						case 1:
							location.X -= (float)anom.Width;
							break;
						case 2:
							location.Y += (float)anom.Height;
							break;
						case 3:
							location.Y -= (float)anom.Height;
							break;
					}
					anom.WorldPosition = location;
				}
			}
		}

		#endregion

		#region Add Mobs/Imobs
		public void AddImmobile(Immobile imob)
		{
			Immobiles.Add(imob);
			imob.CurrentSector = this;
			OnImmobileAdded(new ImmobileAddedHandlerEventArgs(imob));
		}

		public void AddMobile(Mobile mob)
		{
			//check to see if spot is taken, if so try to find a new place to spawn
			bool isSpotFree = false;

			while (!isSpotFree)
			{
				bool isColliding = false;
				foreach (Immobile imob in Immobiles)
				{
					isColliding = CollisionHelper.IsCircleColliding(mob, imob);
					if (isColliding)
						break;
				}
				if (!isColliding)
				{
					foreach (Mobile mob1 in Mobiles)
					{
						isColliding = CollisionHelper.IsCircleColliding(mob, mob1);
						if (isColliding)
							break;
					}
				}
				if (!isColliding)
				{
					foreach (Mobile player in Players)
					{
						isColliding = CollisionHelper.IsCircleColliding(mob, player);
						if (isColliding)
							break;
					}
				}

				if (!isColliding)
					isSpotFree = true;
				else
				{
					int offsetDirection = rand.Next(0, 4);
					Vector2 location = mob.WorldLocation;
					switch (offsetDirection)
					{
						case 0:
							location.X += (float)mob.CollisionRadius * 2;
							break;
						case 1:
							location.X -= (float)mob.CollisionRadius * 2;
							break;
						case 2:
							location.Y += (float)mob.CollisionRadius * 2;
							break;
						case 3:
							location.Y -= (float)mob.CollisionRadius * 2;
							break;
					}
					mob.WorldLocation = location;
				}
			}
			
			//When a free spot if found, add the mob to the sector
			Mobiles.Add(mob);
			mob.CurrentSector = this;
			OnMobileAdded(new MobileAddedHandlerEventArgs(mob));
		}

		public void AddPlayer(Player player)
		{
			//check to see if spot is taken, if so try to find a new place to spawn
			bool isSpotFree = false;

			while (!isSpotFree)
			{
				bool isColliding = false;
				foreach (Immobile imob in Immobiles)
				{
					isColliding = CollisionHelper.IsCircleColliding(player, imob);
					if (isColliding)
						break;
				}
				if (!isColliding)
				{
					foreach (Mobile mob1 in Mobiles)
					{
						isColliding = CollisionHelper.IsCircleColliding(player, mob1);
						if (isColliding)
							break;
					}
				}
				if (!isColliding)
				{
					foreach (Mobile pl in Players)
					{
						isColliding = CollisionHelper.IsCircleColliding(player, pl);
						if (isColliding)
							break;
					}
				}

				if (!isColliding)
					isSpotFree = true;
				else
				{
					int offsetDirection = rand.Next(0, 4);
					Vector2 location = player.WorldLocation;
					switch (offsetDirection)
					{
						case 0:
							location.X += (float)player.CollisionRadius * 2;
							break;
						case 1:
							location.X -= (float)player.CollisionRadius * 2;
							break;
						case 2:
							location.Y += (float)player.CollisionRadius * 2;
							break;
						case 3:
							location.Y -= (float)player.CollisionRadius * 2;
							break;
					}
					player.WorldLocation = location;
				}
			}

			//When a free spot if found, add the mob to the sector
			Players.Add(player);
			player.CurrentSector = this;
			OnPlayerAdded(new PlayerAddedHandlerEventArgs(player));
		}

		public void RemovePlayer(Player player)
		{
			Players.Remove(player);
			OnPlayerRemoved(new PlayerRemovedHandlerEventArgs(player));
		}

		public void AddProjectile(Projectile proj)
		{
			Projectiles.Add(proj);
			OnProjectileAdded(new ProjectileAddedHandlerEventArgs(proj));
		}
		#endregion

		#region event announcement methods
		/// <summary>
		/// Announces a ImmobileAdded event on Sector
		/// </summary>
		/// <param name="e"></param>
		public void OnImmobileAdded(ImmobileAddedHandlerEventArgs e)
		{
			if (ImmobileAdded != null)
				ImmobileAdded(this, e);
		}

		/// <summary>
		/// Announces an OnImmobileAdded event on sector
		/// </summary>
		/// <param name="e"></param>
		public void OnMobileAdded(MobileAddedHandlerEventArgs e)
		{
			if (MobileAdded != null)
				MobileAdded(this, e);
		}

		/// <summary>
		/// Announces an OnProjectileAdded event on sector
		/// </summary>
		/// <param name="e"></param>
		public void OnProjectileAdded(ProjectileAddedHandlerEventArgs e)
		{
			if (ProjectileAdded != null)
				ProjectileAdded(this, e);
		}

		/// <summary>
		/// Announces an OnProjectileRemoved event on sector
		/// </summary>
		/// <param name="e"></param>
		public void OnProjectileRemoved(ProjectileRemovedHandlerEventArgs e)
		{
			if (ProjectileRemoved != null)
				ProjectileRemoved(this, e);
		}


		public void OnPlayerAdded(PlayerAddedHandlerEventArgs e)
		{
			if (PlayerAdded != null)
				PlayerAdded(this, e);
		}


		public void OnPlayerRemoved(PlayerRemovedHandlerEventArgs e)
		{
			if (PlayerRemoved != null)
				PlayerRemoved(this, e);
		}

		#endregion
		#endregion
	}

	#region events


	#region ImmobileAdded
	public delegate void ImmobileAddedHandler(object o, ImmobileAddedHandlerEventArgs e);

	public class ImmobileAddedHandlerEventArgs : EventArgs
	{
		public readonly Immobile Imob;

		public ImmobileAddedHandlerEventArgs(Immobile imob)
		{
			Imob = imob;
		}
	}

	
	#endregion

	#region MobileAdded
	public delegate void MobileAddedHandler(object o, MobileAddedHandlerEventArgs e);

	public class MobileAddedHandlerEventArgs : EventArgs
	{
		public readonly Mobile Mob;

		public MobileAddedHandlerEventArgs(Mobile mob)
		{
			Mob = mob;
		}
	}
	#endregion

	#region ProjectileAdded
	public delegate void ProjectileAddedHandler(object o, ProjectileAddedHandlerEventArgs e);

	public class ProjectileAddedHandlerEventArgs : EventArgs
	{
		public readonly Projectile Proj;

		public ProjectileAddedHandlerEventArgs(Projectile proj)
		{
			Proj = proj;
		}
	}
	#endregion

	#region ProjectileRemoved
	public delegate void ProjectileRemovedHandler(object o, ProjectileRemovedHandlerEventArgs e);

	public class ProjectileRemovedHandlerEventArgs : EventArgs
	{
		public readonly Projectile Proj;

		public ProjectileRemovedHandlerEventArgs(Projectile proj)
		{
			Proj = proj;
		}
	}
	#endregion

	#region player added/removed
	public delegate void PlayerAddedHandler(object o, PlayerAddedHandlerEventArgs e);

	public class PlayerAddedHandlerEventArgs : EventArgs
	{
		public readonly Player ThePlayer;

		public PlayerAddedHandlerEventArgs(Player player)
		{
			ThePlayer = player;
		}
	}

	public delegate void PlayerRemovedHandler(object o, PlayerRemovedHandlerEventArgs e);

	public class PlayerRemovedHandlerEventArgs : EventArgs
	{
		public readonly Player ThePlayer;

		public PlayerRemovedHandlerEventArgs(Player mob)
		{
			ThePlayer = mob;
		}
	}
	#endregion

	#endregion

}
