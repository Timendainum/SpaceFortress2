using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLogicLibrary.Mobiles.Modules.Generators
{
	public class HeavyGenerator : Generator
	{
		public HeavyGenerator()
		{
			Name = "Heavy Generator";
			Power = 800.0f;
			Mass = 40.0f;
		}
	}
}
