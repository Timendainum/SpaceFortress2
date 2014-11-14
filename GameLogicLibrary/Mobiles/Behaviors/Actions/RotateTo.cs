using GameLogicLibrary.Mobiles.Npcs;
using Microsoft.Xna.Framework;
using GameLogicLibrary.Maths;
using System;

namespace GameLogicLibrary.Mobiles.Behaviors.Actions
{
	public class RotateTo : AiAction
	{
		private float _RadiansToRotateTo;
		public float RadiansToRotateTo
		{
			get
			{
				return _RadiansToRotateTo;
			}
			private set
			{
				_RadiansToRotateTo = MathsHelper.AbsoluteRotation((value % MathHelper.TwoPi));
			}
		}
		
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

		#region constuctor
		public RotateTo(Npc theNpc, Random rand, float radiansToRotateTo)
			: base(theNpc, rand)
		{
			RadiansToRotateTo = radiansToRotateTo;
		}

		public RotateTo(Npc theNpc, Random rand, float radiansToRotateTo, float slop)
			: base(theNpc, rand)
		{
			RadiansToRotateTo = radiansToRotateTo;
			Slop = slop;
		}
		#endregion


		public override void Update(GameTime gameTime)
		{
			float currentRotation = MathsHelper.AbsoluteRotation(TheNpc.Rotation);
			float rotationDifference = MathHelper.WrapAngle(RadiansToRotateTo - currentRotation);

			if (Math.Abs(rotationDifference) <= Slop)
			{
				Complete = true;
			}

			if (!Complete)
			{
				if (rotationDifference >= 0)
					TheNpc.ApplyRotatationalThrust(1f);
				else
					TheNpc.ApplyRotatationalThrust(-1f);
			}

			base.Update(gameTime);
		}
	}
}
