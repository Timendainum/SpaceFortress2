
namespace GameLogicLibrary.Mobiles.Modules.Weapons
{
	public class SpinalPlasmaGun : SpinalWeapon
	{
		public SpinalPlasmaGun()
		{
			Name = "Spinal Plasma Gun";
			Cooldown = 0.24f;
			CooldownTimer = 1.0f;

			Power = -10f;
			Mass = 1f;

			FiredSoundEffect = "shot1";
			
		}
	}
}
