using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLogicLibrary.Mobiles.Modules.Armors
{
	public class BasicArmor : Armor
	{
		public BasicArmor()
		{
			Name = "Basic Armor";
			ArmorPoints = 100;
			Power = 0f;
			Mass = 4f;
		}
	}
}
