using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ClientLogicLibrary.Graphics
{
	public class ParticleEmitter
	{
		private Random random;
		public Vector2 EmitterWorldLocation { get; set; }
		private List<Particle> particles;
		private List<Texture2D> textures;

		public ParticleEmitter(List<Texture2D> textures, Vector2 worldlocation)
		{
			EmitterWorldLocation = worldlocation;
			this.textures = textures;
			particles = new List<Particle>();
			random = new Random();
		}

		public void Update(GameTime gameTime)
		{
			int total = 10;

			for (int i = 0; i < total; i++)
			{
				particles.Add(GenerateNewParticle());
			}

			//update particles
			for (int particle = 0; particle < particles.Count; particle++)
			{
				particles[particle].Update(gameTime);
				if (!particles[particle].IsActive)
				{
					particles.RemoveAt(particle);
					particle--;
				}
			}
		}

		private Particle GenerateNewParticle()
		{
			
			Vector2 position = EmitterWorldLocation;
			Texture2D texture = textures[random.Next(textures.Count)];
			Rectangle sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
			float scale = random.Next(100) * 0.009f;
			Vector2 velocity = new Vector2(
									1f * (float)(random.NextDouble() * 2 - 1),
									1f * (float)(random.NextDouble() * 2 - 1));
			float angularVelocity = random.Next(100) * 0.1f;
			Vector2 acceleration = new Vector2(random.Next(-50, 50), random.Next(-50, 50));
			int maxSpeed = random.Next(20, 70);
			int duration = random.Next(5, 30);

			//Color color = new Color(
			//			(float)random.NextDouble(),
			//			(float)random.NextDouble(),
			//			(float)random.NextDouble());
			//Color color1 = new Color(
			//			(float)random.NextDouble(),
			//			(float)random.NextDouble(),
			//			(float)random.NextDouble());
			Color color = Color.AntiqueWhite;
			Color color1 = Color.Black;

			return new Particle(position, texture, sourceRectangle, scale, velocity, acceleration, angularVelocity, maxSpeed, duration, color, color1);

			//return new Particle(texture, position, velocity, angle, angularVelocity, color, size, ttl);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			for (int index = 0; index < particles.Count; index++)
			{
				particles[index].Draw(spriteBatch);
			}
		}
	}
}
