using System;

namespace GameLogicLibrary.Mobiles.Modules.Engines
{
	public class BasicEngine : Engine
	{
		public BasicEngine()
		{
			Name = "Basic Engine";
			Thrust = 2250f;
			RotationalThrust = 0.875f;
			Power = -50.0f;
			Mass = 3.0f;
		}
	}
}
