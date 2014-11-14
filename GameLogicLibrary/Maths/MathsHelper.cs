using System;
using Microsoft.Xna.Framework;


namespace GameLogicLibrary.Maths
{
	public static class MathsHelper
	{
		public static float FrameTime = 1f / 60f;

		#region rotation

		public static Vector2 RadiansToVector(float radians)
		{
			return new Vector2((float)Math.Cos(radians), (float)Math.Sin(radians));
		}

		public static Vector2 RotateAroundCircle(float angle, float distance, Vector2 center)
		{
			return new Vector2((float)(distance * Math.Cos(angle)), (float)(distance * Math.Sin(angle))) + center;
		}


		/// <summary>
		/// Returns an equivilent positive radian between 0 and 2Pi given any positive or ngative radian.
		/// </summary>
		/// <param name="radian"></param>
		/// <returns></returns>
		public static float AbsoluteRotation(float radian)
		{
			float result = radian % MathHelper.TwoPi;
			if (result >= 0)
				return result;

			return MathHelper.TwoPi + result;
		}

		public static float DirectInterceptAngle(Vector2 position, Vector2 faceThis)
		{
			// consider this diagram:
			//         C 
			//        /|
			//      /  |
			//    /    | y
			//  / o    |
			// S--------
			//     x
			// 
			// where S is the position of the spot light, C is the position of the cat,
			// and "o" is the angle that the spot light should be facing in order to 
			// point at the cat. we need to know what o is. using trig, we know that
			//      tan(theta)       = opposite / adjacent
			//      tan(o)           = y / x
			// if we take the arctan of both sides of this equation...
			//      arctan( tan(o) ) = arctan( y / x )
			//      o                = arctan( y / x )
			// so, we can use x and y to find o, our "desiredAngle."
			// x and y are just the differences in position between the two objects.
			float x = faceThis.X - position.X;
			float y = faceThis.Y - position.Y;

			// we'll use the Atan2 function. Atan will calculates the arc tangent of 
			// y / x for us, and has the added benefit that it will use the signs of x
			// and y to determine what cartesian quadrant to put the result in.
			// http://msdn2.microsoft.com/en-us/library/system.math.atan2.aspx
			return (float)Math.Atan2(y, x);
		}

		public static float RotateTo(float currentAngle, float desiredAngle, float rotationSpeed, float elapsed)
		{
			float currentRotation = AbsoluteRotation(currentAngle);
			float desiredRotation = AbsoluteRotation(desiredAngle);
			float rotationDifference = desiredRotation - currentRotation;
			float absRotationDifference = Math.Abs(rotationDifference);
			float rotationDirection = MathHelper.WrapAngle(rotationDifference);
			float rotateThisFrame = rotationSpeed * elapsed;
			float rotateThis = 0f;
			float result = currentRotation;

			//See if amount we need to rotate is less than how much we will rotate this frame
			if (absRotationDifference < rotateThisFrame)
				rotateThis = absRotationDifference;
			else
				rotateThis = rotateThisFrame;

			//add or subtract rotation
			if (rotationDirection >= 0)
				result += rotateThis;
			else
				result -= rotateThis;

			//fix rotation
			result = result % MathHelper.TwoPi;

			//return result
			return result;
		}
		#endregion

		#region IsWithin
		public static bool IsWithin(float value, float targetValue, float slop)
		{
			float difference = Math.Abs(value - targetValue);
			if (difference <= slop)
				return true;
			else
				return false;
		}

		public static bool IsWithin(int value, int targetValue, int slop)
		{
			int difference = Math.Abs(value - targetValue);
			if (difference <= slop)
				return true;
			else
				return false;
		}
		#endregion

		public static bool IsVector2InsideRectangle(Vector2 vector, Rectangle rectangle)
		{
			bool result = false;

			if (vector.X >= rectangle.X && vector.X <= rectangle.X + rectangle.Width)
				if (vector.Y >= rectangle.Y && vector.Y <= rectangle.Y + rectangle.Height)
					result = true;

			return result;
		}

		#region geometry
		
		private static float DistanceLineSegmentToPoint(Vector2 A, Vector2 B, Vector2 p)
		{

			//get the normalized line segment vector
			Vector2 v = B - A;
			v.Normalize();

			//determine the point on the line segment nearest to the point p
			float distanceAlongLine = Vector2.Dot(p, v) - Vector2.Dot(A, v);
			Vector2 nearestPoint;
			if (distanceAlongLine < 0)
			{
				//closest point is A
				nearestPoint = A;
			}
			else if (distanceAlongLine > Vector2.Distance(A, B))
			{
				//closest point is B
				nearestPoint = B;
			}
			else
			{
				//closest point is between A and B... A + d  * ( ||B-A|| )
				nearestPoint = A + distanceAlongLine * v;
			}

			//Calculate the distance between the two points
			float actualDistance = Vector2.Distance(nearestPoint, p);
			return actualDistance;

		}


		public static Vector2 LineIntersectionPoint(Line firstLine, Line secondLine)
		{

			double Ua, Ub;



			// Equations to determine whether lines intersect

			Ua = ((secondLine.EndPos.X - secondLine.StartPos.X) * (firstLine.StartPos.Y - secondLine.StartPos.Y) - (secondLine.EndPos.Y - secondLine.StartPos.Y) * (firstLine.StartPos.X - secondLine.StartPos.X)) /

					((secondLine.EndPos.Y - secondLine.StartPos.Y) * (firstLine.EndPos.X - firstLine.StartPos.X) - (secondLine.EndPos.X - secondLine.StartPos.X) * (firstLine.EndPos.Y - firstLine.StartPos.Y));



			Ub = ((firstLine.EndPos.X - firstLine.StartPos.X) * (firstLine.StartPos.Y - secondLine.StartPos.Y) - (firstLine.EndPos.Y - firstLine.StartPos.Y) * (firstLine.StartPos.X - secondLine.StartPos.X)) /

					((secondLine.EndPos.Y - secondLine.StartPos.Y) * (firstLine.EndPos.X - firstLine.StartPos.X) - (secondLine.EndPos.X - secondLine.StartPos.X) * (firstLine.EndPos.Y - firstLine.StartPos.Y));



			if (Ua >= 0.0f && Ua <= 1.0f && Ub >= 0.0f && Ub <= 1.0f)
			{

				double x = firstLine.StartPos.X + Ua * (firstLine.EndPos.X - firstLine.StartPos.X);

				double y = firstLine.StartPos.Y + Ua * (firstLine.EndPos.Y - firstLine.StartPos.Y);



				return new Vector2((float)x, (float)y);



			}

			else
			{

				return new Vector2();

			}

		}

		#endregion
	}


	public class Line
	{
		public Vector2 StartPos;
		public Vector2 EndPos;
		public Line(Vector2 startPos, Vector2 endPos)
		{
			StartPos = startPos;
			EndPos = endPos;
		}
	}
}
