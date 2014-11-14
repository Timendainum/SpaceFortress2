using System.Collections.Generic;
using ClientLogicLibrary.Mobiles;
using GameLogicLibrary.Mobiles;
using Microsoft.Xna.Framework.Graphics;
using GameLogicLibrary.Simulation;
using GameLogicLibrary.Mobiles.Npcs;

namespace ClientLogicLibrary.Mobiles
{
	public static class ClientMobileFactory
	{
		public static ClientMobile Create(Mobile mob)
		{
			if (mob is AlienFighterInfestor)
				return new ClientBasicNpc((AlienFighterInfestor)mob);
			if (mob is AlienFrigateInfestor)
				return new ClientBasicNpc((AlienFrigateInfestor)mob);
			if (mob is AlienCruiserInfestor)
				return new ClientBasicNpc((AlienCruiserInfestor)mob);
			//if (mob is Ufo)
			//	return new ClientUfo((Ufo)mob, textures);
			return null;
		}
	}
}
