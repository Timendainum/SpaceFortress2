using System.Collections.Generic;
using ClientLogicLibrary.Immobiles;
using GameLogicLibrary.Immobiles;
using Microsoft.Xna.Framework.Graphics;
using GameLogicLibrary.Simulation;
using GameLogicLibrary.Immobiles.Lairs;

namespace ClientLogicLibrary.Immobiles
{
	public static class ClientImmobileFactory
	{
		public static ClientImmobile Create(Immobile immobile)
		{
			if (immobile is Moon)
				return new ClientMoon((Moon)immobile);
			if (immobile is Crosshairs)
				return new ClientCrosshairs((Crosshairs)immobile);
			if (immobile is HumanStation1)
				return new ClientHumanStation1((HumanStation1)immobile);
			if (immobile is AlienLair1)
				return new ClientAlienLair1((AlienLair1)immobile);
			return null;
		}
	}
}
