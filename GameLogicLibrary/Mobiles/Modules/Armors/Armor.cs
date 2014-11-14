using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLogicLibrary.Mobiles.Modules.Armors
{
	public abstract class Armor : Module
	{
		private int _ArmorPoints;
		public int ArmorPoints
		{
			get
			{
				return _ArmorPoints;
			}
			protected set
			{
				_ArmorPoints = value;
			}
		}
	}
}
