using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLogicLibrary.Mobiles.Modules.Armors
{
	public class MediumArmor : Armor
	{
		public MediumArmor()
		{
			Name = "Medium Armor";
			ArmorPoints = 400;
			Power = 0f;
			Mass = 16f;
		}
	}
}
