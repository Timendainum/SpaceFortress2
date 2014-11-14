using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLogicLibrary.Mobiles.Modules.Shields
{
	public class HeavyShield : Shield
	{
		public HeavyShield()
		{
			Name = "Heavy Shield";
			ShieldPoints = 800;
			RegenerationRate = 8;
			Power = -80f;
			Mass = 32f;
		}
	}
}
