using System;
using GameLogicLibrary.Mobiles.Npcs;
using Microsoft.Xna.Framework;
using GameLogicLibrary.Mobiles.Behaviors.Actions;
using GameLogicLibrary.Maths;

namespace GameLogicLibrary.Mobiles.Behaviors
{
	public class GoToWorldLocation : Behavior
	{
		#region declarations
		public Vector2 Destination { get; set; }
		private float _ThrustPercent = -1f;
		public float ThrustPercent
		{
			get
			{
				return _ThrustPercent;
			}
			set
			{
				_ThrustPercent = value;
			}
		}

		private float _Slop = 0.025f;
		public float Slop
		{
			get
			{
				return _Slop;
			}
			set
			{
				_Slop = value;
			}
		}
		#endregion

		#region constructors
		public GoToWorldLocation(Npc theNpc, Random rand, Vector2 destination)
			: base(theNpc, rand)
		{
			Destination = destination;
		}

		public GoToWorldLocation(Npc theNpc, Random rand, Vector2 destination, float thrustPercent)
			: base(theNpc, rand)
		{
			Destination = destination;
			ThrustPercent = thrustPercent;
		}
		#endregion

		public override void Update(GameTime gameTime)
		{
			float currentRotation = MathsHelper.AbsoluteRotation(TheNpc.Rotation);
			float interceptRotation = MathsHelper.AbsoluteRotation(MathsHelper.DirectInterceptAngle(TheNpc.WorldCenter, Destination));
			
			if (CurrentAction == null || CurrentAction.Complete)
			{
				if (!MathsHelper.IsWithin(currentRotation, interceptRotation, Slop))
					CurrentAction = new RotateTo(TheNpc, _rand, interceptRotation);
				else
					CurrentAction = new ApplyThrust(TheNpc, _rand, MathsHelper.FrameTime, ThrustPercent);
			}

			base.Update(gameTime);
		}
	}
}
