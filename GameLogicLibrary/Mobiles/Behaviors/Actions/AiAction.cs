using GameLogicLibrary.Mobiles.Npcs;
using Microsoft.Xna.Framework;
using System;

namespace GameLogicLibrary.Mobiles.Behaviors.Actions
{
	public abstract class AiAction
	{
		public Npc TheNpc { get; protected set; }
		public bool Complete { get; protected set; }
		public float TimeElapsed { get; protected set; }
		protected Random _rand;

		public AiAction(Npc theNpc, Random rand)
		{
			_rand = rand;
			TheNpc = theNpc;
			TimeElapsed = 0f;
			Complete = false;
		}

		public virtual void Update(GameTime gameTime)
		{
			if (Complete)
				return;
			float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
			TimeElapsed += elapsed;
		}
	}
}
