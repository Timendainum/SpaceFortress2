using GameLogicLibrary.Mobiles;

namespace ClientLogicLibrary.Mobiles
{
	public class ClientPlayer : ClientShipPilot
	{
		public Player ServerPlayer;
		

		public ClientPlayer(ShipPilot serverObject)
			: base (serverObject)
		{
			ServerPlayer = (Player)serverObject;
			
		}
	}
}
