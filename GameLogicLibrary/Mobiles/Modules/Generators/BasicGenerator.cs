using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLogicLibrary.Mobiles.Modules.Generators
{
	public class BasicGenerator : Generator
	{
		public BasicGenerator()
		{
			Name = "Basic Generator";
			Power = 85.0f;
			Mass = 2.0f;
		}
	}
}
