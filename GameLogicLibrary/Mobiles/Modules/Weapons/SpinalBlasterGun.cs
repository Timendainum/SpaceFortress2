
namespace GameLogicLibrary.Mobiles.Modules.Weapons
{
	public class SpinalBlasterGun : SpinalWeapon
	{
		public SpinalBlasterGun()
		{
			Name = "Spinal Blaster Gun";
			Cooldown = 0.2f;
			CooldownTimer = 1.0f;

			Power = -2f;
			Mass = 0.2f;

			FiredSoundEffect = "shot1";
			
		}
	}
}
