using Microsoft.Xna.Framework;
using GameLogicLibrary.Maths;
using GameLogicLibrary.Simulation;

namespace GameLogicLibrary.Mobiles.Modules.Weapons
{
	public abstract class SpinalWeapon : Weapon
	{
		public override void Fire(ShipPilot firedBy, Vector2 fireWorldPosition)
		{
			//fireWorldPosition is already adjusted to the fire position on the ship

			//Set up a offset if we are shooting the weapon for the first time
			if (CooldownTimer > Cooldown + 0.2f && Offset == 0f)
			{
				Offset = RandomManager.TheRandom.Next(0, 16) * 0.01f;
				OffsetTimer = 0f;
			}
			else if (CooldownTimer > Cooldown && OffsetTimer > Offset)
			{
				Offset = 0f;
				//create projectile factory
				Projectile proj = ProjectileFactory.Create(fireWorldPosition, this, firedBy);
				proj.WorldLocation -= proj.RelativeCenter;
				proj.FiredBy = firedBy;

				Vector2 playerDirection = MathsHelper.RadiansToVector(firedBy.Rotation);
				playerDirection.Normalize();
				proj.Rotation = firedBy.Rotation;
				proj.Velocity = (playerDirection * proj.StartingSpeed) + firedBy.Velocity;

				firedBy.CurrentSector.AddProjectile(proj);

				CooldownTimer = 0.0f;

				//Announce ShipFiredSpinalWeapon
				firedBy.CurrentShip.OnShipFiredSpinalWeapon(new Ships.ShipFiredSpinalWeaponEventArgs(firedBy, this));
			}
		}
	}
}
