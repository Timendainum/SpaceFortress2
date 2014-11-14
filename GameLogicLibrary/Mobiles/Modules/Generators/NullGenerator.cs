using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLogicLibrary.Mobiles.Modules.Generators
{
	public class NullGenerator : Generator
	{
		public NullGenerator()
		{
			Name = "No Generator";
			Power = 0f;
			Mass = 0f;
		}
	}
}
