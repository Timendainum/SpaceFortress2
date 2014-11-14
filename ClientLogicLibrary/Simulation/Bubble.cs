using Microsoft.Xna.Framework;

namespace ClientLogicLibrary.Simulation
{
    public static class Bubble
    {
        #region Declarations
        public static Vector2 Position = Vector2.Zero;
		private static Vector2 _BubbleSize = new Vector2(5000, 5000);
        #endregion

        #region Properties
        public static int BubbleWidth
        {
			get { return (int)_BubbleSize.X; }
        }

        public static int BubbleHeight
        {
            get { return (int)_BubbleSize.Y; }
        }

		public static Vector2 BubbleSize
		{
			get
			{
				return _BubbleSize;
			}

			set
            {
            	_BubbleSize = value;
            }
		}

        public static Rectangle BubbleRectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, BubbleWidth, BubbleHeight);
            }
        }

		public static Vector2 Center
		{
			get
			{
				return new Vector2(Position.X + (BubbleWidth / 2), Position.Y + (BubbleHeight / 2));
			}
		}
        #endregion

        #region Public Methods
        public static void Move(Vector2 offset)
        {
            Position += offset;
        }

		public static void MoveCenterTo(Vector2 location)
		{
			Position = new Vector2(location.X - (BubbleWidth / 2), location.Y - (BubbleHeight / 2));
		}

        public static bool ObjectInBubble(Rectangle bounds)
        {
            return (BubbleRectangle.Contains(bounds));
        }

        public static Vector2 Transform(Vector2 point)
        {
            return point - Position;
        }

        public static Rectangle Transform(Rectangle rectangle)
        {
            return new Rectangle(rectangle.Left - (int)Position.X, rectangle.Top - (int)Position.Y, rectangle.Width, rectangle.Height);
        }
        #endregion

    }
}
