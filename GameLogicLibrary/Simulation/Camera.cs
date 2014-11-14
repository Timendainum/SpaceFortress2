using Microsoft.Xna.Framework;
using System;

namespace GameLogicLibrary.Simulation
{
	

    public static class Camera
    {
        #region Declarations
		public static event CameraResizedHandler CameraResized;
		
		private static Vector2 viewPortSize = Vector2.Zero;
		private static Vector2 _LastPosition = Vector2.Zero;
		private static Vector2 _Position;
		public static Vector2 WorldPosition
		{
			get
			{
				return _Position;
			}
			set
			{
				_LastPosition = _Position;
				_Position = new Vector2(value.X, value.Y);
			}
		}

		public static Vector2 CurrentVelocity
		{
			get
			{
				return _Position - _LastPosition;
			}

		}


		public static Vector2 ViewPortSize
		{
			get
			{
				return viewPortSize;
			}
			set
			{
				if (viewPortSize == value)
					return;
				viewPortSize = value;
				OnCameraResized(new EventArgs());
			}
		}

        public static int ViewPortWidth
        {
            get { return (int)viewPortSize.X; }
        }

        public static int ViewPortHeight
        {
            get { return (int)viewPortSize.Y; }
        }

        public static Rectangle ViewPort
        {
            get
            {
                return new Rectangle(
                    (int)WorldPosition.X, (int)WorldPosition.Y,
                    ViewPortWidth, ViewPortHeight);
            }
        }

		public static Vector2 ViewPortCenter
		{
			get
			{
				return new Vector2((ViewPortWidth / 2), (ViewPortHeight / 2));
			}
		}

		public static Vector2 WorldCenter
		{
			get
			{
				return new Vector2(WorldPosition.X + (ViewPortWidth / 2), WorldPosition.Y + (ViewPortHeight / 2));
			}
		}
        #endregion

        #region Public Methods
        public static void Move(Vector2 offset)
        {
            WorldPosition += offset;
        }

		public static void MoveCenterTo(Vector2 location)
		{
			WorldPosition = new Vector2(location.X - (ViewPortWidth / 2), location.Y - (ViewPortHeight / 2));
		}

        public static bool ObjectIsVisible(Rectangle bounds)
        {
            return (ViewPort.Intersects(bounds));
        }

		/// <summary>
		/// Tranforms world coordinates to screen coordinates
		/// </summary>
		/// <param name="point"></param>
		/// <returns></returns>
        public static Vector2 TransformWorldToCamera(Vector2 point)
        {
            return point - WorldPosition;
        }

		/// <summary>
		/// Tranforms world coordinates to screen coordinates
		/// </summary>
		/// <param name="point"></param>
		/// <returns></returns>
		public static Vector2 TransformCameraToWorld(Vector2 point)
		{
			return WorldPosition + point;
		}


        public static Rectangle Transform(Rectangle rectangle)
        {
            return new Rectangle(
                rectangle.Left - (int)WorldPosition.X,
                rectangle.Top - (int)WorldPosition.Y,
                rectangle.Width,
                rectangle.Height);
        }

		public static Rectangle GetScreenRectangle()
		{
			return new Rectangle(0, 0, ViewPortWidth, ViewPortHeight);
		}

		public static Rectangle GetWorldRectangle()
		{
			return new Rectangle((int)WorldPosition.X, (int)WorldPosition.Y, ViewPortWidth, ViewPortHeight);
		}
        #endregion


		#region event anouncement methods
		public static void OnCameraResized(EventArgs e)
		{
			if (CameraResized != null)
				CameraResized(e);

		}
		#endregion
    }

	public delegate void CameraResizedHandler(EventArgs e);
}
