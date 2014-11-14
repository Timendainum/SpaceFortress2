using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ClientLogicLibrary.Graphics
{
	public class Particle : MobileSprite
	{
		#region Declarations
		private Vector2 acceleration;
		private float maxSpeed;
		private int initialDuration;
		private int remainingDuration;
		private Color initialColor;
		private Color finalColor;
		public float AngularVelocity = 0.0f;
		#endregion

		#region Properties
		public int ElapsedDuration
		{
			get
			{
				return initialDuration - remainingDuration;
			}
		}

		public float DurationProgress
		{
			get
			{
				return (float)ElapsedDuration /	(float)initialDuration;
			}
		}

		public bool IsActive
		{
			get
			{
				return (remainingDuration > 0);
			}
		}
		#endregion

		#region Constructor
		public Particle(
			Vector2 worldLocation,
			Texture2D texture,
			Rectangle textureSource,
			float scale,
			Vector2 velocity,
			Vector2 acceleration,
			float angularVelocity,
			float maxSpeed,
			int duration,
			Color initialColor,
			Color finalColor)
			: base(worldLocation, texture, textureSource)
		{
			Scale = scale;
			initialDuration = duration;
			remainingDuration = duration;
			Velocity = velocity;
			AngularVelocity = angularVelocity;
			this.acceleration = acceleration;
			this.initialColor = initialColor;
			this.maxSpeed = maxSpeed;
			this.finalColor = finalColor;
		}
		#endregion

		#region Update and Draw
		public override void Update(GameTime gameTime)
		{
			float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
			
			if (remainingDuration <= 0)
			{
				Expired = true;
			}

			if (!Expired)
			{
				Velocity += acceleration;
				if (Velocity.Length() > maxSpeed)
				{
					Vector2 vel = Velocity;
					vel.Normalize();
					Velocity = vel * maxSpeed;
				}
				TintColor = Color.Lerp(
					initialColor,
					finalColor,
					DurationProgress);
				remainingDuration--;
			}

			Rotation += AngularVelocity;
			WorldLocation += (Velocity * elapsed);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			if (IsActive)
			{
				base.Draw(spriteBatch);
			}
		}
		#endregion

	}
}


/*


 * 
 * 
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace ClientLogicLibrary.Graphics
{
	public class Particle
	{
		public Texture2D Texture { get; set; }
		public Vector2 WorldPosition { get; set; }
		public Vector2 Velocity { get; set; }
		public float Angle { get; set; }
		public float AngularVelocity { get; set; }
		public Color Color { get; set; }
		public float Size { get; set; }
		public int TTL { get; set; }

		public Particle(Texture2D texture, Vector2 worldPosition, Vector2 velocity,
			float angle, float angularVelocity, Color color, float size, int ttl)
		{
			Texture = texture;
			WorldPosition = worldPosition;
			Velocity = velocity;
			Angle = angle;
			AngularVelocity = angularVelocity;
			Color = color;
			Size = size;
			TTL = ttl;
		}

		public void Update()
		{
			TTL--;
			WorldPosition += Velocity;
			Angle += AngularVelocity;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			Rectangle sourceRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);
			Vector2 origin = new Vector2(Texture.Width / 2, Texture.Height / 2);

			spriteBatch.Draw(Texture, Camera.Transform(WorldPosition), sourceRectangle, Color, Angle, origin, Size, SpriteEffects.None, 0f);
		}
	}
}
*/