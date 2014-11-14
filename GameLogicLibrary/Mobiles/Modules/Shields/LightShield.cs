using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLogicLibrary.Mobiles.Modules.Shields
{
	public class LightShield : Shield
	{
		public LightShield()
		{
			Name = "Light Shield";
			ShieldPoints = 200;
			RegenerationRate = 2;
			Power = -20f;
			Mass = 6f;
		}
	}
}
