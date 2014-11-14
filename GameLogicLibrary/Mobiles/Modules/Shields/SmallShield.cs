using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLogicLibrary.Mobiles.Modules.Shields
{
	public class SmallShield : Shield
	{
		public SmallShield()
		{
			Name = "Small Shield";
			ShieldPoints = 5;
			RegenerationRate = 1;
			Power = -3f;
			Mass = 0.1f;
		}
	}
}
