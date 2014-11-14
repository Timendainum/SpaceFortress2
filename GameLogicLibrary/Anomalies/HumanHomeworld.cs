using GameLogicLibrary.Factions;
using GameLogicLibrary.Immobiles;
using GameLogicLibrary.Simulation;
using Microsoft.Xna.Framework;

namespace GameLogicLibrary.Anomalies
{
	public class HumanHomeworld : Anomaly
	{
		public HumanHomeworld(Sector mySector, Faction faction)
			: base(mySector, 3000, 3000, faction)
		{
			
		}

		public override void GenerateContents()
		{
			//Add background planet
			MySector.SectorBackgroundObjects.Add(new SectorBackgroundObject(TransformAnomalyToWorld(RelativeCenter), new Vector2(1822, 1817), "planet_human", 0.6f));
			
			//Station
			MySector.CapitalStation = new HumanStation1(Vector2.Zero);
			GenerateImmobileWorldPosition(MySector.CapitalStation);
			MySector.CapitalStation.Name = "Human Capital Station";
			MySector.AddImmobile(MySector.CapitalStation);

			//HumanStation1 otherStation = new HumanStation1(new Vector2(-6500, -6500));
			//otherStation.Name = "Human Seconday Station";
			//AddImmobile(otherStation);

			//Moons and crap
			Immobile moon = new Moon(Vector2.Zero, MoonType.Large);
			GenerateImmobileWorldPosition(moon);
			MySector.AddImmobile(moon);
		}

	}
}
