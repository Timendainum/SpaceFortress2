using System;

namespace GameLogicLibrary.Mobiles.Modules.Engines
{
	public class LightEngine : Engine
	{
		public LightEngine()
		{
			Name = "Light Engine";
			Thrust = 6500f;
			RotationalThrust = 1.6f;
			Power = -100.0f;
			Mass = 9.0f;
		}
	}
}
