using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameLogicLibrary.Factions
{
	public abstract class Team : Faction
	{
		public enum TeamControlType { Human, Computer }
		public TeamControlType TeamControl = TeamControlType.Computer;

		public virtual void Update(GameTime gameTime)
		{

		}
	}
}
