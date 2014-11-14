
namespace GameLogicLibrary.Mobiles.Modules.Engines
{
	public class NullEngine : Engine
	{
		public NullEngine()
		{
			Name = "No Engine";
			Thrust = 0.0f;
			RotationalThrust = 0.0f;
			Power = 0.0f;
			Mass = 0.0f;
		}
	}
}
