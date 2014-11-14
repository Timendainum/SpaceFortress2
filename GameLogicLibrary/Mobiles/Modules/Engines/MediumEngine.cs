using System;


namespace GameLogicLibrary.Mobiles.Modules.Engines
{
	public class MediumEngine : Engine
	{
		public MediumEngine()
		{
			Name = "Medium Engine";
			Thrust = 13500f;
			RotationalThrust = 2.0f;
			Power = -200.0f;
			Mass = 20.0f;
		}
	}
}
