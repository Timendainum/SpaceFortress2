
using Microsoft.Xna.Framework;
namespace GameLogicLibrary.Mobiles.Modules.Weapons
{
	public class NullTurretGun : TurretWeapon
	{
		public NullTurretGun()
		{
			Name = "No Turret";
			Cooldown = 0f;
			CooldownTimer = 0f;

			Power = 0f;
			Mass = 0f;

			FiredSoundEffect = "";
			
		}

		public override void Fire(ShipPilot firedBy, Vector2 fireWorldPosition)
		{
			return; //null wepaon does nothing
		}
	}
}
