using System;
using GameLogicLibrary.Mobiles.Npcs;
using Microsoft.Xna.Framework;

namespace GameLogicLibrary.Mobiles.Behaviors.Actions
{
	public class FireTurretWeapons : AiAction
	{
		public FireTurretWeapons(Npc theNpc, Random rand)
			: base(theNpc, rand)
		{
			
		}

		public override void Update(GameTime gameTime)
		{

			if (!Complete)
			{
				TheNpc.CurrentShip.FireTurretWeapons(TheNpc);
				Complete = true;
			}

			base.Update(gameTime);
		}
	}
}
