using GameLogicLibrary.Factions;
using GameLogicLibrary.Immobiles;
using GameLogicLibrary.Simulation;
using Microsoft.Xna.Framework;
using GameLogicLibrary.Immobiles.Lairs;

namespace GameLogicLibrary.Anomalies
{
	public class RockyPlanet : Anomaly
	{
		public RockyPlanet(Sector mySector, Faction faction)
			: base(mySector, 5000, 5000, faction)
		{
			
		}

		public override void GenerateContents()
		{
			//int rand = RandomManager.TheRandom.Next(0, 2);
			//if (rand == 1)
			//	MySector.SectorBackgroundObjects.Add(new SectorBackgroundObject(TransformAnomalyToWorld(RelativeCenter), new Vector2(1884, 1884), "planet_red", 0.7f));
			//else
			MySector.SectorBackgroundObjects.Add(new SectorBackgroundObject(TransformAnomalyToWorld(RelativeCenter), new Vector2(600, 600), "planet_red_small", 0.89f));

			//Alien Lair
			if (RandomManager.TheRandom.Next(0, 3) == 1)
			{
				AlienLair1 lair = new AlienLair1(Vector2.Zero);
				GenerateImmobileWorldPosition(lair);
				MySector.AddImmobile(lair);
				for (int i = 0; i < lair.MaxSpawn; i++)
				{
					lair.SpawnNpc();
				}
			}
			
			
			int numberOfMoons = RandomManager.TheRandom.Next(1, 5);

			for (int i = 0; i < numberOfMoons; i++)
			{
				Moon moon = new Moon(Vector2.Zero);
				GenerateImmobileWorldPosition(moon);
				MySector.AddImmobile(moon);
			}
		}

	}
}
