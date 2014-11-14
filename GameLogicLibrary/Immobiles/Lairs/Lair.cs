using System.Collections.Generic;
using GameLogicLibrary.Mobiles.Npcs;
using Microsoft.Xna.Framework;

namespace GameLogicLibrary.Immobiles.Lairs
{
	public abstract class Lair : Immobile
	{

		public int MaxSpawn = 0;
		public float SpawnDelay = 0f;
		public float SpawnTimer = 99999f;
		List<Npc> _SpawnedNpcs = new List<Npc>();

		public Lair(Vector2 location)
		{
			Collidable = true;
			WorldLocation = location;
		}

		public override void Update(GameTime gameTime)
		{
			if (!Expired)
			{
				float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

				SpawnTimer += elapsed;

				//try to spawn new npc
				if (SpawnTimer > SpawnDelay && _SpawnedNpcs.Count < MaxSpawn)
					SpawnNpc();

				//Remove killed spawns
				Queue<Npc> npcsToRemove = new Queue<Npc>();
				foreach (Npc npc in _SpawnedNpcs)
				{
					if (npc.Expired)
						npcsToRemove.Enqueue(npc);
				}

				for (int i = 0; i < npcsToRemove.Count; i++)
				{
					_SpawnedNpcs.Remove(npcsToRemove.Dequeue());
				}

			}

			base.Update(gameTime);
		}

		public abstract void SpawnNpc();

		protected void AddNpc(Npc newNpc)
		{
			SpawnTimer = 0f;
			_SpawnedNpcs.Add(newNpc);
			CurrentSector.AddMobile(newNpc);
		}
	}
}
