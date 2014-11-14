using System.Collections.Generic;
using GameLogicLibrary.Mobiles;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ClientLogicLibrary.Mobiles
{
    public abstract class ClientNpc : ClientShipPilot
	{
		public ClientNpc(ShipPilot serverPilot)
			: base(serverPilot)
		{
			
		}
	}
}
