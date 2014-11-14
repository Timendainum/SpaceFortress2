using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameLogicLibrary.Immobiles;
using GameLogicLibrary.Maths;
using System.Collections.Generic;

namespace GameLogicLibrary.Simulation
{
	public static class CollisionHelper
	{
		#region collision
		public static bool IsColliding(Entity e1, Entity e2)
		{
			List<Vector2> collisionPoints;
			return IsColliding(e1, e2, out collisionPoints);
		}
		
		public static bool IsColliding(Entity e1, Entity e2, out List<Vector2> collisionPoints)
		{
			collisionPoints = new List<Vector2>();
			
			if (e1 == e2)
			{
				return false;
			}

			if (!IsCircleColliding(e1, e2))
				return false;
			else if (e1.CollisionMap == null || e2.CollisionMap == null)
			{
				throw new UndefinedCollisionMapException(e1, e2);
			}
			else
				return IsPixelColliding(e1, e2, out collisionPoints);
		}

		public static bool IsBoxColliding(Entity e1, Entity e2)
		{
			if (e1.Collidable && !e1.Expired && e2.Collidable && !e2.Expired)
			{
				return e1.BoundingBox.Intersects(e2.BoundingBox);
			}
			else
			{
				return false;
			}
		}

		public static bool IsCircleColliding(Entity e1, Entity e2)
		{
			if (e1.Collidable && !e1.Expired && e2.Collidable && !e2.Expired)
			{
				if (Vector2.Distance(e1.WorldCenter, e2.WorldCenter) < (e1.CollisionRadius + e2.CollisionRadius))
					return true;
				else
					return false;
			}
			else
			{
				return false;
			}
		}

		public static bool IsPixelColliding(Entity e1, Entity e2, out List<Vector2> collisionPoints)
		{
			Color[,] e1ColorArray = e1.ColorArray;
			Color[,] e2ColorArray = e2.ColorArray;
			Matrix e1Matrix = e1.CollisionMatrix;
			Matrix e2Matrix = e2.CollisionMatrix;

			return TexturesCollide(e1ColorArray, e1Matrix, e2ColorArray, e2Matrix, out collisionPoints);
		}


		public static Color[,] TextureTo2DArray(Texture2D texture)
		{
			Color[] colors1D = new Color[texture.Width * texture.Height];
			texture.GetData(colors1D);

			Color[,] colors2D = new Color[texture.Width, texture.Height];
			for (int x = 0; x < texture.Width; x++)
				for (int y = 0; y < texture.Height; y++)
					colors2D[x, y] = colors1D[x + y * texture.Width];

			return colors2D;
		}

		private static bool TexturesCollide(Color[,] tex1, Matrix mat1, Color[,] tex2, Matrix mat2, out List<Vector2> collisionPoints)
		{
			collisionPoints = new List<Vector2>();
			Matrix mat1to2 = mat1 * Matrix.Invert(mat2);
			int width1 = tex1.GetLength(0);
			int height1 = tex1.GetLength(1);
			int width2 = tex2.GetLength(0);
			int height2 = tex2.GetLength(1);

			for (int x1 = 0; x1 < width1; x1++)
			{
				for (int y1 = 0; y1 < height1; y1++)
				{
					Vector2 pos1 = new Vector2(x1, y1);
					Vector2 pos2 = Vector2.Transform(pos1, mat1to2);

					int x2 = (int)pos2.X;
					int y2 = (int)pos2.Y;
					if ((x2 >= 0) && (x2 < width2))
					{
						if ((y2 >= 0) && (y2 < height2))
						{
							//At this point the textures overlap, check the alpha channel and see if
							//the point is solid
							if (tex1[x1, y1].A > 0)
							{
								if (tex2[x2, y2].A > 0)
								{
									collisionPoints.Add(Vector2.Transform(pos1, mat1));
								}
							}
						}
					}
				}
			}

			if (collisionPoints.Count > 0)
				return true;
			else
				return false;
		}


		#endregion

		public static void BounceOffMobile(Mobile me, Mobile mob, Vector2 collisionPoint)
		{
			Vector2 cOfMass = ((me.Mass * me.Mass * me.Velocity) + (mob.Mass * mob.Mass * mob.Velocity)) / (me.Mass + mob.Mass);

			Vector2 normal = collisionPoint - me.WorldCenter;
			normal.Normalize();
			Vector2 mobNormal = collisionPoint - mob.WorldCenter;
			mobNormal.Normalize();

			me.Velocity -= cOfMass;
			me.Velocity = Vector2.Reflect(me.Velocity, normal);
			me.Velocity += cOfMass;

			mob.Velocity -= cOfMass;
			mob.Velocity = Vector2.Reflect(mob.Velocity, mobNormal);
			mob.Velocity += cOfMass;
			
			//If I'm moving faster then correct me, otherwise we will correct the mob
			Mobile mobToCorrect = null;
			Mobile otherMob = null;
			if (me.Speed >= mob.Speed)
			{
				mobToCorrect = me;
				otherMob = mob;
			}
			else
			{
				mobToCorrect = mob;
				otherMob = me;
			}

			//Insure that resulting collision will send the mobile away from the collisionPoint
			Vector2 collisionIntercept = MathsHelper.RadiansToVector(MathsHelper.DirectInterceptAngle(mobToCorrect.WorldCenter, collisionPoint));
			Vector2 correctionVelocity = mobToCorrect.Velocity - otherMob.Velocity;
			float dotProduct = Vector2.Dot(correctionVelocity, collisionIntercept);

			//If the angle of movement is approaching the immobile reverse it
			if (dotProduct >= 0f)
			{
				mobToCorrect.Velocity += (correctionVelocity * -2);
			}
			
			//float dotProduct = Vector2.Dot(me.Velocity, mob.Velocity);

			////If the angle of movement is approaching the immobile reverse it
			//if (dotProduct >= 0f)
			//{
			//	Vector2 collisionInterceptM = MathsHelper.RadiansToVector(MathsHelper.DirectInterceptAngle(me.WorldCenter, collisionPoint));
			//	Vector2 collisionInterceptU = MathsHelper.RadiansToVector(MathsHelper.DirectInterceptAngle(mob.WorldCenter, collisionPoint));
			//	Vector2 newVelocityM = collisionInterceptM * -1;
			//	Vector2 newVelocityU = collisionInterceptU * -1;
			//	if (me.Speed < 5f)
			//		newVelocityM *= 5f;
			//	else
			//		newVelocityM *= me.Speed;

			//	me.Velocity = newVelocityM;

			//	if (mob.Speed < 5f)
			//		newVelocityU *= 5f;
			//	else
			//		newVelocityU *= mob.Speed;

			//	mob.Velocity = newVelocityU;
			//}
			
		}

		public static void BounceOffImmobile(Mobile me, Immobile imob, Vector2 collisionPoint)
		{
			Vector2 normal = collisionPoint - me.WorldCenter;
			normal.Normalize();

			me.Velocity = Vector2.Reflect(me.Velocity, normal);

			//Insure that resulting collision will send the mobile away from the collisionPoint
			//Vector2 collisionIntercept = MathsHelper.RadiansToVector(MathsHelper.DirectInterceptAngle(me.WorldCenter, collisionPoint));
			//float dotProduct = Vector2.Dot(me.Velocity, collisionIntercept);

			////If the angle of movement is approaching the immobile reverse it
			//if (dotProduct >= 0f)
			//{
			//	Vector2 newVelocity = collisionIntercept * -1;
			//	if (me.Speed <= 5f)
			//		newVelocity *= 5f;
			//	else
			//		newVelocity *= me.Speed;
			//	
			//	me.Velocity = newVelocity;
			//}
		}

		public static Vector2 GetSignificantCollisionPoint(Mobile mob, Entity entity, List<Vector2> collisionPoints, float elapsed)
		{
			return collisionPoints[0];
		}

		public static void CorrectCollision(Mobile mob, Entity entity, List<Vector2> collisionPoints, float elapsed)
		{
			//find Velocity to adjust
			Vector2 mobVelocity = Vector2.Zero;
			if (entity is Mobile)
			{
				Mobile m = (Mobile)entity;
				mobVelocity = mob.Velocity - m.Velocity;
			}
			else
				mobVelocity = mob.Velocity;

			//return mob to previous position and rotation
			Vector2 adustedVelocity = mobVelocity * elapsed * -1;
			mob.WorldLocation += adustedVelocity;

			if (IsColliding(mob, entity))
			{
				if (mob.Rotation != mob.LastRotation)
				{
					float correctedRotation = MathsHelper.AbsoluteRotation(mob.Rotation);
					correctedRotation += (MathsHelper.AbsoluteRotation(mob.Rotation) - MathsHelper.AbsoluteRotation(mob.LastRotation)) * -2;
					mob.Rotation = correctedRotation;
				}

				//return entity to previous rotation
				if (entity.Rotation != entity.LastRotation)
				{
					float correctedRotation = MathsHelper.AbsoluteRotation(entity.Rotation);
					correctedRotation += (MathsHelper.AbsoluteRotation(entity.Rotation) - MathsHelper.AbsoluteRotation(entity.LastRotation)) * -2;
					entity.Rotation = correctedRotation;
				}
			}

			bool stillColliding = IsColliding(mob, entity);

			if (stillColliding)
			{
				CorrectCollision(mob, entity, collisionPoints, elapsed);
			}
		}
	}
}


//public static void BounceOffMobile(Mobile me, Mobile mob, Vector2 collisionPoint)
//		{

//			Vector2 cOfMass = (me.Mass * me.Velocity + mob.Mass * mob.Velocity) / (me.Mass + mob.Mass);


//			Vector2 normal = mob.WorldCenter - me.WorldCenter;
//			normal.Normalize();
//			Vector2 mobNormal = me.WorldCenter - mob.WorldCenter;
//			mobNormal.Normalize();

//			me.Velocity -= cOfMass;
//			me.Velocity = Vector2.Reflect(me.Velocity, normal);
//			me.Velocity += cOfMass;

//			mob.Velocity -= cOfMass;
//			mob.Velocity = Vector2.Reflect(mob.Velocity, mobNormal);
//			mob.Velocity += cOfMass;
//		}

//		public static void BounceOffImmobile(Mobile me, Immobile imob, Vector2 collisionPoint)
//		{
//			Vector2 normal = imob.WorldCenter - me.WorldCenter;
//			normal.Normalize();

//			me.Velocity = Vector2.Reflect(me.Velocity, normal);
//		}












		//private static bool TexturesCollide(Color[,] tex1, Matrix mat1, Color[,] tex2, Matrix mat2, out Vector2 collisionPoint)
		//{
		//	collisionPoint = Vector2.Zero;
		//	Matrix mat1to2 = mat1 * Matrix.Invert(mat2);
		//	int width1 = tex1.GetLength(0);
		//	int height1 = tex1.GetLength(1);
		//	int width2 = tex2.GetLength(0);
		//	int height2 = tex2.GetLength(1);

		//	for (int x1 = 0; x1 < width1; x1++)
		//	{
		//		for (int y1 = 0; y1 < height1; y1++)
		//		{
		//			Vector2 pos1 = new Vector2(x1, y1);
		//			Vector2 pos2 = Vector2.Transform(pos1, mat1to2);

		//			int x2 = (int)pos2.X;
		//			int y2 = (int)pos2.Y;
		//			if ((x2 >= 0) && (x2 < width2))
		//			{
		//				if ((y2 >= 0) && (y2 < height2))
		//				{
		//					//At this point the textures overlap, check the alpha channel and see if
		//					//the point is solid
		//					if (tex1[x1, y1].A > 0)
		//					{
		//						if (tex2[x2, y2].A > 0)
		//						{
		//							collisionPoint = Vector2.Transform(pos1, mat1);
		//							return true;
		//						}
		//					}
		//				}
		//			}
		//		}
		//	}

		//	return false;
		//}