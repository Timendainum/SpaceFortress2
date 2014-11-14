using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLogicLibrary.Mobiles.Modules.Shields
{
	public class MediumShield : Shield
	{
		public MediumShield()
		{
			Name = "Medium Shield";
			ShieldPoints = 400;
			RegenerationRate = 4;
			Power = -40f;
			Mass = 16f;
		}
	}
}
