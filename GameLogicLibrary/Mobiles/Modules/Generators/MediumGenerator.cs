using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLogicLibrary.Mobiles.Modules.Generators
{
	public class MediumGenerator : Generator
	{
		public MediumGenerator()
		{
			Name = "Medium Generator";
			Power = 400.0f;
			Mass = 20.0f;
		}
	}
}
