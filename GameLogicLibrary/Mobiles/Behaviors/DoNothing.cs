using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameLogicLibrary.Mobiles.Npcs;
using GameLogicLibrary.Mobiles.Behaviors.Actions;
using Microsoft.Xna.Framework;

namespace GameLogicLibrary.Mobiles.Behaviors
{
	public class DoNothing : Behavior
	{
		public DoNothing(Npc theNpc, Random rand)
			: base(theNpc, rand)
		{
			CurrentAction = new Nothing(TheNpc, _rand);
		}
	}
}
