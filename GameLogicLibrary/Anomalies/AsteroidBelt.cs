using GameLogicLibrary.Factions;
using GameLogicLibrary.Immobiles;
using GameLogicLibrary.Simulation;
using Microsoft.Xna.Framework;
using GameLogicLibrary.Immobiles.Lairs;

namespace GameLogicLibrary.Anomalies
{
	public class AsteroidBelt : Anomaly
	{
		public AsteroidBelt(Sector mySector, Faction faction)
			: base(mySector, 7500, 7500, faction)
		{
			
		}

		public override void GenerateContents()
		{
			//Add background planet
			//if (RandomManager.TheRandom.Next(0,1) == 1)
			//	MySector.SectorBackgroundObjects.Add(new SectorBackgroundObject(TransformAnomalyToWorld(RelativeCenter), new Vector2(870, 870), "planet_green", 0.9f));
			//else
			//	MySector.SectorBackgroundObjects.Add(new SectorBackgroundObject(TransformAnomalyToWorld(RelativeCenter), new Vector2(590, 590), "planet_blue", 0.8f));
			
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

			int numberOfMoons = RandomManager.TheRandom.Next(1, 3);

			for (int i = 0; i < numberOfMoons; i++)
			{
				Moon moon = new Moon(Vector2.Zero);
				GenerateImmobileWorldPosition(moon);
				MySector.AddImmobile(moon);
			}

			//AddImmobile(new Moon(new Vector2(-5500, -5200), MoonType.Small));

			//AddImmobile(new Moon(new Vector2(-4900, -5350), MoonType.Medium));

			//
			//AddImmobile(new Moon(new Vector2(-3500, 1350), MoonType.Medium));

			//AddImmobile(new Moon(new Vector2(-4500, 250), MoonType.Small));

			//AddImmobile());

			//Station
			//MySector.CapitalStation = new HumanStation1(Vector2.Zero);
			//GenerateImmobileWorldPosition(MySector.CapitalStation);
			//MySector.CapitalStation.Name = "Human Capital Station";
			//MySector.AddImmobile(MySector.CapitalStation);

			//HumanStation1 otherStation = new HumanStation1(new Vector2(-6500, -6500));
			//otherStation.Name = "Human Seconday Station";
			//AddImmobile(otherStation);

			//Moons and crap
			//Immobile moon = new Moon(Vector2.Zero, MoonType.Large);
			//GenerateImmobileWorldPosition(moon);
			//MySector.AddImmobile(moon);
		}

	}
}
