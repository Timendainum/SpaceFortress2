using GameLogicLibrary.Simulation;
using Microsoft.Xna.Framework;
using GameLogicLibrary.Mobiles.Npcs;

namespace GameLogicLibrary.Immobiles.Lairs
{
	public class AlienLair1 : Lair
	{

		float rotationSpeed = 0.02f;

		public AlienLair1(Vector2 location)
			: base(location)
		{
			Size = new Vector2(800, 800);
			CollisionRadius = 425;
			CollisionMap = CollisionMapManager.GetTexture("alien_station1");
			MaxSpawn = RandomManager.TheRandom.Next(1, 10);
			SpawnDelay = 30f;
		}

		public override void Update(GameTime gameTime)
		{
			if (!Expired)
			{
				float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

				Rotation += (rotationSpeed * elapsed);
			}
			base.Update(gameTime);
		}

		public override void SpawnNpc()
		{
			int randomChance = RandomManager.TheRandom.Next(0, 100);

			Npc newNpc = null;

			if (randomChance <= 1)
				newNpc = new AlienCruiserInfestor(WorldLocation);
			else if (randomChance <= 10)
				newNpc = new AlienFrigateInfestor(WorldLocation);
			else
				newNpc = new AlienFighterInfestor(WorldLocation);
			AddNpc(newNpc);
		}
	}
}
