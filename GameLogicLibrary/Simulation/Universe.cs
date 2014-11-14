using GameLogicLibrary.Simulation;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameLogicLibrary.Factions;

namespace GameLogicLibrary.Simulation
{
	public class Universe : Simulation
	{
		public List<Sector> Sectors = new List<Sector>();

		public readonly FactionHostile Hostile = new FactionHostile();
		public readonly TeamHuman Human = new TeamHuman();

		public Universe()
		{
			Sectors.Add(new Sector(this, Human));
		}

		public override void Update(GameTime gameTime)
		{
			Human.Update(gameTime);

			foreach (Sector sector in Sectors)
			{
				sector.Update(gameTime);
			}
		}
	}
}
