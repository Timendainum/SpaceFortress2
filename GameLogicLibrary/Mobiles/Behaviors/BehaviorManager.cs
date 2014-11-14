using GameLogicLibrary.Mobiles.Npcs;
using Microsoft.Xna.Framework;
using System;
using GameLogicLibrary.Simulation;

namespace GameLogicLibrary.Mobiles.Behaviors
{
	public abstract class BehaviorManager
	{
		public Npc TheNpc { get; protected set; }
		public Behavior CurrentBehavior { get; set; }
		public Entity CurrentTarget { get; set; }

		protected Random _rand;

		public BehaviorManager(Npc theNpc)
		{
			_rand = new Random();
			TheNpc = theNpc;

		}

		public virtual void Update(GameTime gameTime)
		{
			CurrentBehavior.Update(gameTime);
		}
	}
}
