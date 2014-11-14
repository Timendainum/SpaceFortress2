using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLogicLibrary.Mobiles.Modules.Shields
{
	public class BasicShield : Shield
	{
		public BasicShield()
		{
			Name = "Basic Shield";
			ShieldPoints = 100;
			RegenerationRate = 1;
			Power = -10f;
			Mass = 2f;
		}
	}
}
