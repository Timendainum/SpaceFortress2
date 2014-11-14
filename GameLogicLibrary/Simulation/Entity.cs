using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameLogicLibrary.Factions;

namespace GameLogicLibrary.Simulation
{
	public abstract class Entity
	{		
		public bool Expired = false;
		public string Name { get; set; }
		public Faction OwnerFaction { get; protected set; }

		#region Definition properties
		private Texture2D _CollisionMap;
		public Texture2D CollisionMap
		{
			get
			{
				return _CollisionMap;
			}
			protected set
			{
				_CollisionMap = value;
				ColorArray = CollisionHelper.TextureTo2DArray(CollisionMap);
			}
		}
		public Color[,] ColorArray { get; protected set; }

		public Matrix CollisionMatrix 
		{
			get
			{
				Matrix theMatrix =
					Matrix.CreateTranslation(RelativeCenter.X * -1, RelativeCenter.Y * -1, 0) *
					Matrix.CreateRotationZ(Rotation) *
					Matrix.CreateTranslation(WorldCenter.X, WorldCenter.Y, 0);
				return theMatrix;
			}
		}

		public float LastRotation { get; private set; }

		private float _Rotation = 0.0f;
		public float Rotation
		{
			get
			{
				return _Rotation;
			}
			set
			{
				LastRotation = Rotation;
				_Rotation = value % MathHelper.TwoPi;

			}
		}

		private Vector2 _Size = Vector2.Zero;
		public Vector2 Size
		{
			get
			{
				return _Size;
			}
			set
			{
				_Size = value;
			}
		}

		public int Width
		{
			get
			{
				return (int)Size.X;
			}
		}

		public int Height
		{
			get
			{
				return (int)Size.Y;
			}
		}

		private Vector2 _Padding = Vector2.Zero;
		public Vector2 Padding
		{
			get
			{
				return _Padding;
			}
			set
			{
				_Padding = value;
			}
		}

		public int PaddingX
		{
			get
			{
				return (int)Padding.X;
			}
		}

		public int PaddingY
		{
			get
			{
				return (int)Padding.Y;
			}
		}

		private bool _Collidable = false;
		public bool Collidable
		{
			get
			{
				return _Collidable;
			}
			set
			{
				_Collidable = value;
			}
		}

		private int _CollisionRadius = 0;
		public int CollisionRadius
		{
			get
			{
				return _CollisionRadius;
			}
			set
			{
				_CollisionRadius = value;
			}
		}
		#endregion

		#region location properties
		public Sector CurrentSector;
		private Vector2 _WorldLocation = Vector2.Zero;
		public Vector2 WorldLocation
		{
			get
			{
				return _WorldLocation;
			}
			set
			{
				_WorldLocation = value;
			}
		}

		public int WorldLocationX
		{
			get
			{
				return (int)WorldLocation.X;
			}
		}

		public int WorldLocationY
		{
			get
			{
				return (int)WorldLocation.Y;
			}
		}

		public Rectangle WorldRectangle
		{
			get
			{
				return new Rectangle(
					WorldLocationX,
					WorldLocationY,
					Width,
					Height);
			}
		}

		public Vector2 RelativeCenter
		{
			get { return new Vector2(Width / 2, Height / 2); }
		}

		public Vector2 WorldCenter
		{
			get { return WorldLocation + RelativeCenter; }
		}

		public Vector2 ScreenLocation
		{
			get
			{
				return Camera.TransformWorldToCamera(WorldLocation);
			}
		}

		public Rectangle ScreenRectangle
		{
			get
			{
				return Camera.Transform(WorldRectangle);
			}
		}

		public Vector2 ScreenCenter
		{
			get
			{
				return Camera.TransformWorldToCamera(WorldLocation + RelativeCenter);
			}
		}
		#endregion

		public Rectangle BoundingBox
		{
			get
			{
				return new Rectangle(
					WorldLocationX + PaddingX,
					WorldLocationY + PaddingY,
					Width - (PaddingX * 2),
					Height - (PaddingY * 2));
			}
		}

		public virtual void Update(GameTime gameTime)
		{
			//nothing
		}
	}
}
