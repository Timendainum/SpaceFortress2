using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameLogicLibrary.Mobiles.Modules.Weapons
{
	public class TurretPlasmaGun : TurretWeapon
	{
		public TurretPlasmaGun()
		{
			Name = "Turret Plasma Gun";
			Cooldown = 0.25f;
			CooldownTimer = 1.0f;

			Power = -15f;
			Mass = 2f;

			FiredSoundEffect = "shot1";

			MaxRotationSpeed = MathHelper.TwoPi;
			
		}
	}
}
