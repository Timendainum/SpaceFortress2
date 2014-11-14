using System;

namespace GameLogicLibrary.Mobiles.Modules.Engines
{
	public class SmallEngine : Engine
	{
		public SmallEngine()
		{
			Name = "Small Engine";
			Thrust = 175f;
			RotationalThrust = 0.1f;
			Power = -5.0f;
			Mass = 0.2f;
		}
	}
}
