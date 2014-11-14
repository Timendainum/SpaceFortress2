using System.Collections.Generic;
using GameLogicLibrary.Mobiles.Ships;
using Microsoft.Xna.Framework.Graphics;
using GameLogicLibrary.Mobiles;

namespace ClientLogicLibrary.Mobiles
{
	public static class ShipRendererFactory
	{
		public static ShipRenderer Create(ShipPilot serverPilot)
		{
			if (serverPilot.CurrentShip is HumanFighter)
				return new HumanFighterShipRenderer(serverPilot);
			if (serverPilot.CurrentShip is HumanFrigate1)
				return new HumanFrigate1ShipRenderer(serverPilot);
			if (serverPilot.CurrentShip is HumanCruiser1)
				return new HumanCruiser1ShipRenderer(serverPilot);
			if (serverPilot.CurrentShip is HumanBattleship1)
				return new HumanBattleship1ShipRenderer(serverPilot);
			if (serverPilot.CurrentShip is HumanCapitalship1)
				return new HumanCapitalship1ShipRenderer(serverPilot);
			if (serverPilot.CurrentShip is AlienFighter)
				return new AlienFighterShipRenderer(serverPilot);
			if (serverPilot.CurrentShip is AlienFrigate1)
				return new AlienFrigate1ShipRenderer(serverPilot);
			if (serverPilot.CurrentShip is AlienCruiser1)
				return new AlienCruiser1ShipRenderer(serverPilot);
			return null;
		}
	}
}
