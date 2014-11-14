using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLogicLibrary.Mobiles.Modules.Engines
{
	public abstract class Engine : Module
	{
		private float _Thrust;
		public float Thrust
		{
			get
			{
				return _Thrust;
			}
			protected set
			{
				_Thrust = value;
			}
		}
		private float _RotationalThrust;
		public float RotationalThrust
		{
			get
			{
				return _RotationalThrust;
			}
			protected set
			{
				_RotationalThrust = value;
			}
		}
	}
}
