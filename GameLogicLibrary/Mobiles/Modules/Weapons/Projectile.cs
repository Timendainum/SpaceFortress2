using Microsoft.Xna.Framework;
using GameLogicLibrary.Simulation;
using GameLogicLibrary.Immobiles;
using GameLogicLibrary.Mobiles.Npcs;
using System;
using GameLogicLibrary.Factions;

namespace GameLogicLibrary.Mobiles.Modules.Weapons
{
	public abstract class Projectile : Mobile
	{
		#region declarations
		protected float MaxLifetime = 1.0f;
		private float Lifetime = 0.0f;
		public float StartingSpeed = 1000.0f;
		public Entity FiredBy;
		public Weapon FiredFrom;
		public int Damage = 0;
		public string ImpactSoundEffect { get; protected set; }

		public event HitEntityEventHandler HitEntity;
		#endregion

		#region constructor
		public Projectile(Vector2 location, Weapon weaponFiredFrom, Entity pilotFiredBy)
		{
			FiredBy = pilotFiredBy;
			FiredFrom = weaponFiredFrom;
			OwnerFaction = FiredBy.OwnerFaction;
			Collidable = true;
			WorldLocation = location;
			Thrust = 1000f;
		}
		#endregion

		#region xna
		public override void Update(GameTime gameTime)
		{
			float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
			Lifetime += elapsed;

			if (Lifetime > MaxLifetime)
				Expired = true;

			base.Update(gameTime);
		}
		#endregion


		#region hit methods
		/// <summary>
		/// Announce the HitEntity event and does server hit logic.
		/// </summary>
		/// <param name="e"></param>
		public void OnHitEntity(HitEntityEventArgs e)
		{
			Expired = true;

			if (e.EntityHit is ShipPilot)
			{
				ShipPilot sp = (ShipPilot)e.EntityHit;
				if (sp.OwnerFaction != OwnerFaction)
					sp.CurrentShip.ApplyDamage(CalculateDamage());
			}
			else if (e.EntityHit is Mobile)
			{
				e.EntityHit.Expired = true;
			}

			//Announce HitEntity event
			if (HitEntity != null)
				HitEntity(this, e);
		}

		protected int CalculateDamage()
		{
			return Damage;
		}
		#endregion
	}

	public delegate void HitEntityEventHandler(object o, HitEntityEventArgs e);

	public class HitEntityEventArgs : EventArgs
	{
		public readonly Entity EntityHit;
		public readonly Vector2 CollisionPoint;

		public HitEntityEventArgs(Entity entityHit, Vector2 collisionPoint)
		{
			EntityHit = entityHit;
			CollisionPoint = collisionPoint;
		}

	}


}
