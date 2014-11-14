using System;
using System.Collections.Generic;
using ClientLogicLibrary.Graphics;
using ClientLogicLibrary.Simulation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ClientLogicLibrary.Effects
{
    public static class EffectManager
    {
        #region Declarations
        static Random _rand = new Random();
		static List<Particle> _Particles = new List<Particle>();
        #endregion

        #region Helper Methods
		public static void ResetParticles()
		{
			_Particles.Clear();
		}
		
		public static Vector2 randomDirection(float scale)
        {
            Vector2 direction;
            do
            {
                direction = new Vector2(
                _rand.Next(0, 100) - 50,
                _rand.Next(0, 100) - 50);
            } while (direction.Length() == 0);
            direction.Normalize();
            direction *= scale;

            return direction;
        }
        #endregion

        #region Public Methods
        static public void Update(GameTime gameTime)
        {
			Queue<Particle> particleToRemove = new Queue<Particle>();
			foreach (Particle particle in _Particles)
			{
				particle.Update(gameTime);
				if (particle.Expired)
					particleToRemove.Enqueue(particle);
			}

			for (int i = 0; i < particleToRemove.Count; i++)
			{
				_Particles.Remove(particleToRemove.Dequeue());
			}
        }

        static public void Draw(SpriteBatch spriteBatch)
        {
			foreach (Particle particle in _Particles)
			{
				if (Bubble.BubbleRectangle.Intersects(particle.GetWorldRectangle()))
					particle.Draw(spriteBatch);
			}
        }

		public static void AddSparkEffect(Vector2 location)
        {
            int particleCount = _rand.Next(10, 20);
            for (int x = 0; x < particleCount; x++)
            {
                Particle particle = new Particle(
					location,
                    TaticalScreenTextureManager.GetTexture("square_white"),
                    new Rectangle(0,0,2,2),
					1.0f,
					randomDirection((float)_rand.Next(10, 50)),
					Vector2.Zero,
					0f,
					1000,
					20,
					Color.Yellow,
					Color.Orange);
				_Particles.Add(particle);
            }
        }

		public static void AddExplosionEffect(Vector2 location)
		{
			int particleCount = _rand.Next(50, 100);
			for (int x = 0; x < particleCount; x++)
			{
				Particle particle = new Particle(
					location,
					TaticalScreenTextureManager.GetTexture("square_white"),
					new Rectangle(0, 0, 2, 2),
					1.0f,
					randomDirection((float)_rand.Next(10, 50)),
					Vector2.Zero,
					0f,
					1000,
					60,
					Color.Yellow,
					Color.Orange);
				_Particles.Add(particle);
			}
		}
        #endregion

    }
}
