using GameLogicLibrary.Factions;
using GameLogicLibrary.Immobiles;
using GameLogicLibrary.Simulation;
using Microsoft.Xna.Framework;

namespace GameLogicLibrary.Anomalies
{
	public class HabitablePlanet : Anomaly
	{
		public HabitablePlanet(Sector mySector, Faction faction)
			: base(mySector, 3000, 3000, faction)
		{
			
		}

		public override void GenerateContents()
		{
			int rand = RandomManager.TheRandom.Next(0, 3);
			switch (rand)
			{
				case 0:
					MySector.SectorBackgroundObjects.Add(new SectorBackgroundObject(TransformAnomalyToWorld(RelativeCenter), new Vector2(350, 349), "planet_jungle_small", 0.85f));
					break;
				case 1:
					MySector.SectorBackgroundObjects.Add(new SectorBackgroundObject(TransformAnomalyToWorld(RelativeCenter), new Vector2(800, 798), "planet_shad_small", 0.65f));
					break;
				case 2:
					MySector.SectorBackgroundObjects.Add(new SectorBackgroundObject(TransformAnomalyToWorld(RelativeCenter), new Vector2(450, 450), "planet_yellowblue_small", 0.75f));
					break;
			}
			
			
			//Station
			if (RandomManager.TheRandom.Next(0, 2) == 1)
			{
				Station newStation = new HumanStation1(Vector2.Zero);
				GenerateImmobileWorldPosition(newStation);
				MySector.CapitalStation.Name = "Human Station";
				MySector.AddImmobile(newStation);
			}


			//Moons and crap
			Immobile moon = new Moon(Vector2.Zero, MoonType.Large);
			GenerateImmobileWorldPosition(moon);
			MySector.AddImmobile(moon);
		}

	}
}
