using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLogicLibrary.Mobiles.Modules.Armors
{
	public class HeavyArmor : Armor
	{
		public HeavyArmor()
		{
			Name = "Heavy Armor";
			ArmorPoints = 800;
			Power = 0f;
			Mass = 32f;
		}
	}
}
