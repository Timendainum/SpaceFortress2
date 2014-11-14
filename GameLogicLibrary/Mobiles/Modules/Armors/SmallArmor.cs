using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLogicLibrary.Mobiles.Modules.Armors
{
	public class SmallArmor : Armor
	{
		public SmallArmor()
		{
			Name = "Small Armor";
			ArmorPoints = 5;
			Power = 0f;
			Mass = 0.1f;
		}
	}
}
