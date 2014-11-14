using System;
using GameLogicLibrary.Mobiles.Npcs;
using Microsoft.Xna.Framework;

namespace GameLogicLibrary.Mobiles.Behaviors.Actions
{
	public class FireSpinalWeapons : AiAction
	{
		public FireSpinalWeapons(Npc theNpc, Random rand)
			: base(theNpc, rand)
		{
			
		}

		public override void Update(GameTime gameTime)
		{

			if (!Complete)
			{
				TheNpc.CurrentShip.FireSpinalWeapons(TheNpc);
				Complete = true;
			}

			base.Update(gameTime);
		}
	}
}
