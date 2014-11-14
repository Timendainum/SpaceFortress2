using Microsoft.Xna.Framework;
using System;
using GameLogicLibrary.Maths;

namespace GameLogicLibrary.Simulation
{

	public abstract class Mobile : Entity
	{
		public Vector2 Velocity = Vector2.Zero;
		public Vector2 Acceleration = Vector2.Zero;
		public float MaxAcceleration
		{
			get
			{
				return Thrust / Mass;
			}
		}
		public float Thrust = 0f;
		public float RotationalThrust = 0f;
		public float Mass = 20.0f;
		public const float EtherConstant = 0.99995f; //can't be more than one

		public float Speed
		{
			get
			{
				return (float)Math.Abs(Velocity.Length());
			}
		}

		public override void Update(GameTime gameTime)
		{
			if (!Expired)
			{
				float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

				//Figure out acceleration modifier
				float etherModifier = (float)Math.Pow(EtherConstant, (Speed * Speed) / MaxAcceleration);
				//Take ether mod into account ether
				Velocity *= etherModifier;

				//Add acceleration to velocity
				Velocity += (Acceleration * elapsed);

				

				//Add new velocity to 
				WorldLocation += (Velocity * elapsed);
			}
		}

		/// <summary>
		/// Adds rotation in radians to the mobile.
		/// </summary>
		/// <param name="rotation"></param>
		public void ApplyRotatationalThrust(float rotationPercent)
		{
			float calculatedRotationPercent = rotationPercent * (RotationalThrust / Mass);
			Rotation += calculatedRotationPercent % MathHelper.TwoPi;
		}

		/// <summary>
		/// This applies thrust to the mobile on a vector opposite the facing
		/// of the mobile. Negative thrustPercent will apply thrust in the
		/// direction of the mobile.
		/// </summary>
		/// <param name="thrustPercent"></param>
		public void ApplyThrust(float thrustPercent)
		{
			float calculatedThrust = thrustPercent * (Thrust/Mass);
			Vector2 direction = MathsHelper.RadiansToVector(Rotation);

			//reverse the direction of thrust
			calculatedThrust *= -1;

			Acceleration = direction * calculatedThrust;
		}
	}
}
