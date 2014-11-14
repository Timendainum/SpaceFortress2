using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLogicLibrary.Mobiles.Modules.Armors
{
	public class LightArmor : Armor
	{
		public LightArmor()
		{
			Name = "Light Armor";
			ArmorPoints = 200;
			Power = 0f;
			Mass = 8f;
		}
	}
}
