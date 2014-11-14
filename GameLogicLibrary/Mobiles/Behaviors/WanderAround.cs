using GameLogicLibrary.Mobiles.Npcs;
using GameLogicLibrary.Mobiles.Behaviors.Actions;
using System;
using Microsoft.Xna.Framework;

namespace GameLogicLibrary.Mobiles.Behaviors
{
	public class WanderAround : Behavior
	{
		public WanderAround(Npc theNpc, Random rand)
			: base(theNpc, rand)
		{

		}

		public override void Update(GameTime gameTime)
		{
			if (CurrentAction == null)
			{
				float nextRotation = (_rand.Next(0, (int)MathHelper.TwoPi * 10)) * 0.1f;
				CurrentAction = new RotateTo(TheNpc, _rand, nextRotation);
			}
			else if (CurrentAction.Complete)
			{
				if (CurrentAction is RotateTo)
				{
					float thrustTime = _rand.Next(10, 50) * 0.1f;
					CurrentAction = new ApplyThrust(TheNpc, _rand, thrustTime, -0.25f);
				}
				else
				{
					float nextRotation = (_rand.Next(0, (int)MathHelper.TwoPi * 10)) * 0.1f;
					CurrentAction = new RotateTo(TheNpc, _rand, nextRotation);
				}
			}
			
			base.Update(gameTime);
		}
	}
}
