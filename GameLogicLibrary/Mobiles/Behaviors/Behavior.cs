using Microsoft.Xna.Framework;
using GameLogicLibrary.Mobiles.Npcs;
using GameLogicLibrary.Mobiles.Behaviors.Actions;
using System;

namespace GameLogicLibrary.Mobiles.Behaviors
{
	public abstract class Behavior
	{
		public Npc TheNpc { get; protected set; }
		public AiAction CurrentAction { get; set; }
		protected Random _rand;

		public Behavior(Npc theNpc, Random rand)
		{
			_rand = rand;
			TheNpc = theNpc;
		}

		public virtual void Update(GameTime gameTime)
		{
			CurrentAction.Update(gameTime);
		}
	}
}
