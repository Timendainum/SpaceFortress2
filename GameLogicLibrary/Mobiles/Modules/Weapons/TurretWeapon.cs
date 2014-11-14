using System;
using GameLogicLibrary.Maths;
using GameLogicLibrary.Mobiles.Ships;
using Microsoft.Xna.Framework;
using GameLogicLibrary.Simulation;

namespace GameLogicLibrary.Mobiles.Modules.Weapons
{
	public abstract class TurretWeapon : Weapon
	{
		public float Rotation = 0f;
		public float RotateTo = 0f;
		public float MaxRotationSpeed = 0f;
		public float Slop = 0.005f;

		public override void Update(GameTime gameTime)
		{
			//update Rotation
			Rotation = MathsHelper.RotateTo(Rotation, RotateTo, MaxRotationSpeed, (float)gameTime.ElapsedGameTime.TotalSeconds);

			base.Update(gameTime);
		}

		public override void Fire(ShipPilot firedBy, Vector2 fireWorldPosition)
		{
			//fireWorldPosition is already adjusted to the fire position on the ship
			//Set up a offset if we are shooting the weapon for the first time
			if (CooldownTimer > Cooldown + 0.2f && Offset == 0f)
			{
				Offset = RandomManager.TheRandom.Next(0, 51) * 0.01f;
				OffsetTimer = 0f;
			}
			else if (CooldownTimer > Cooldown && OffsetTimer > Offset)
			{
				Offset = 0f;

				//create projectile factory
				Projectile proj = ProjectileFactory.Create(fireWorldPosition, this, firedBy);
				proj.WorldLocation -= proj.RelativeCenter;
				proj.FiredBy = firedBy;

				Vector2 turretDirection = MathsHelper.RadiansToVector(Rotation);
				turretDirection.Normalize();
				proj.Rotation = Rotation;
				proj.Velocity = (turretDirection * proj.StartingSpeed) + firedBy.Velocity;

				firedBy.CurrentSector.AddProjectile(proj);

				CooldownTimer = 0.0f;

				//Announce ShipFiredSpinalWeapon
				firedBy.CurrentShip.OnShipFiredTurretWeapon(new ShipFiredTurretWeaponEventArgs(firedBy, this));
			}
		}
	}
}
