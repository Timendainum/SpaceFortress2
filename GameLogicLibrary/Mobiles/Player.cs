using GameLogicLibrary.Mobiles.Ships;
using Microsoft.Xna.Framework;
using GameLogicLibrary.Immobiles;
using GameLogicLibrary.Factions;

namespace GameLogicLibrary.Mobiles
{
	public class Player : ShipPilot
	{
		#region declarations
		public Station HomeStation;
		#endregion

		#region constructor
		public Player(Vector2 location, Faction faction)
			: base(location)
		{
			OwnerFaction = faction;
			CurrentShip = new NullShip();
			//CurrentShip = new HumanCruiser1();
		}
		#endregion
	
	}
}
