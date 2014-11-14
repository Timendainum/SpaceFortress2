using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameLogicLibrary.Mobiles.Modules.Weapons
{
	public class TurretRailGun : TurretWeapon
	{
		public TurretRailGun()
		{
			Name = "Turret Railgun";
			Cooldown = 0.27f;
			CooldownTimer = 1.2f;

			Power = -25f;
			Mass = 7f;

			FiredSoundEffect = "tx0_fire1";

			MaxRotationSpeed = MathHelper.Pi;
			
		}
	}
}
