using GameLogicLibrary.Mobiles.Npcs;
using System;
using Microsoft.Xna.Framework;

namespace GameLogicLibrary.Mobiles.Behaviors.Actions
{
	public class ApplyThrust : AiAction
	{
		public float ThrustTime { get; private set; }
		public float ThrustPercent { get; private set; }

		#region constructor
		public ApplyThrust(Npc theNpc, Random rand, float thrustTime, float thrustPercent)
			: base(theNpc, rand)
		{
			ThrustTime = thrustTime;
			ThrustPercent = thrustPercent;
		}
		#endregion

		public override void Update(GameTime gameTime)
		{
			if (TimeElapsed >= ThrustTime)
			{
				Complete = true;
			}

			if (!Complete)
			{
				TheNpc.ApplyThrust(ThrustPercent);
			}
			else
			{
				TheNpc.ApplyThrust(0f);
			}

			base.Update(gameTime);
		}
	}
}
