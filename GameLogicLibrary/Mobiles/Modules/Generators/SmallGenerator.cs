using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLogicLibrary.Mobiles.Modules.Generators
{
	public class SmallGenerator : Generator
	{
		public SmallGenerator()
		{
			Name = "Small Generator";
			Power = 20.0f;
			Mass = 0.1f;
		}
	}
}
