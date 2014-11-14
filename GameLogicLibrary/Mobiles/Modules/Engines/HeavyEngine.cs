using System;


namespace GameLogicLibrary.Mobiles.Modules.Engines
{
	public class HeavyEngine : Engine
	{
		public HeavyEngine()
		{
			Name = "Heavy Engine";
			Thrust = 32500f;
			RotationalThrust = 3.0f;
			Power = -400.0f;
			Mass = 50.0f;
		}
	}
}
