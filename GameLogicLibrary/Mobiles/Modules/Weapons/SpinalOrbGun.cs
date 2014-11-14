using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLogicLibrary.Mobiles.Modules.Weapons
{
	public class SpinalOrbGun : SpinalWeapon
	{
		public SpinalOrbGun()
		{
			Name = "Spinal Orb Gun";
			Cooldown = 0.75f;
			CooldownTimer = 1.0f;

			Power = -20f;
			Mass = 4f;

			FiredSoundEffect = "shot2";
		}
	}
}
