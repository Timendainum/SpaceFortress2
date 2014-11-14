using System;
using GameLogicLibrary.Maths;
using GameLogicLibrary.Mobiles.Behaviors.Actions;
using GameLogicLibrary.Mobiles.Npcs;
using Microsoft.Xna.Framework;
using GameLogicLibrary.Simulation;

namespace GameLogicLibrary.Mobiles.Behaviors
{
	public class FireAtTarget : Behavior
	{
		public Entity Target { get; private set; }

		private float _Slop = 0.05f;
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
		
		public FireAtTarget(Npc theNpc, Random rand, Entity target)
			: base(theNpc, rand)
		{
			Target = target;
			
		}

		public override void Update(GameTime gameTime)
		{
			float currentRotation = MathsHelper.AbsoluteRotation(TheNpc.Rotation);
			float interceptRotation = MathsHelper.AbsoluteRotation(MathsHelper.DirectInterceptAngle(TheNpc.WorldCenter, Target.WorldCenter));

			if (CurrentAction == null || CurrentAction.Complete)
			{
				if (!MathsHelper.IsWithin(currentRotation, interceptRotation, Slop))
					CurrentAction = new RotateTo(TheNpc, _rand, interceptRotation);
				else
					CurrentAction = new FireAllWeapons(TheNpc, _rand);
			}

			base.Update(gameTime);
		}
		
	}
}
