using Microsoft.Xna.Framework;
using GameLogicLibrary.Simulation;
using GameLogicLibrary.Factions;
using GameLogicLibrary.Immobiles;

namespace GameLogicLibrary.Anomalies
{
	public abstract class Anomaly
	{
		#region declarations

		public Sector MySector { get; protected set; }
		public Vector2 WorldPosition { get; set; }
		public Faction OwnerFaction { get; protected set; }
		public int Width { get; protected set; }
		public int Height { get; protected set; }

		public Vector2 RelativeCenter
		{
			get
			{
				return new Vector2(Width / 2, Height / 2);
			}
		}

		public Vector2 WorldCenter
		{
			get
			{
				return RelativeCenter + WorldPosition;
			}
		}

		//Background object
		//(Vector2 location, Vector2 size, string textureName, float paralaxFactor)
		#endregion

		public Anomaly(Sector mySector, int height, int width, Faction faction)
		{
			MySector = mySector;
			Height = height;
			Width = width;
			OwnerFaction = faction;
		}

		#region xna
		public virtual void Update(GameTime gameTime)
		{
			//nothing
		}
		#endregion

		public Rectangle GetWorldRectangle()
		{
			return new Rectangle((int)WorldPosition.X, (int)WorldPosition.Y, Width, Height);
		}

		public Vector2 TransformAnomalyToWorld(Vector2 point)
		{
			return WorldPosition + point;
		}

		public void GenerateImmobileWorldPosition(Immobile imob)
		{
			imob.WorldLocation = GenerateWorldPointInAnomaly();

			//check to see if spot is taken, if so try to find a new place to spawn
			bool isSpotFree = false;
			bool isInAnomaly = false;

			while (!isSpotFree && !isInAnomaly)
			{
				bool isColliding = false;
				foreach (Immobile imob1 in MySector.Immobiles)
				{
					isColliding = CollisionHelper.IsCircleColliding(imob, imob1);
					if (isColliding)
						break;
				}

				if (!isColliding)
					isSpotFree = true;
				else
				{
					int offsetDirection = RandomManager.TheRandom.Next(0, 4);
					Vector2 location = imob.WorldLocation;
					switch (offsetDirection)
					{
						case 0:
							location.X += (float)imob.CollisionRadius * 2;
							break;
						case 1:
							location.X -= (float)imob.CollisionRadius * 2;
							break;
						case 2:
							location.Y += (float)imob.CollisionRadius * 2;
							break;
						case 3:
							location.Y -= (float)imob.CollisionRadius * 2;
							break;
					}
					imob.WorldLocation = location;
				}

				if (isSpotFree)
				{
					if (GetWorldRectangle().Contains(imob.WorldRectangle))
						isInAnomaly = true;
					else
						imob.WorldLocation = GenerateWorldPointInAnomaly();
				}
			}

		}

		protected Vector2 GenerateWorldPointInAnomaly()
		{
			return TransformAnomalyToWorld(new Vector2(RandomManager.TheRandom.Next(0, Width), RandomManager.TheRandom.Next(0, Height)));
		}

		public abstract void GenerateContents();
	}
		

}
