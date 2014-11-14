using System.Collections.Generic;
using GameLogicLibrary.Mobiles;
using Microsoft.Xna.Framework.Graphics;

namespace ClientLogicLibrary.Mobiles
{
	public class ClientBasicNpc : ClientNpc
	{
		public ClientBasicNpc(ShipPilot serverPilot)
			: base(serverPilot)
		{

		}
	}
}
