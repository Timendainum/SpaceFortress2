
namespace GameLogicLibrary.Mobiles.Modules.Shields
{
	public class NullShield : Shield
	{
		public NullShield()
		{
			Name = "No Shield";
			ShieldPoints = 0;
			RegenerationRate = 0;
			Power = 0f;
			Mass = 0f;
		}
	}
}
