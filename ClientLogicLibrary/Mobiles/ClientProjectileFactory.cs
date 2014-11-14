using System.Collections.Generic;
using GameLogicLibrary.Mobiles.Modules.Weapons;
using Microsoft.Xna.Framework.Graphics;

namespace ClientLogicLibrary.Mobiles
{
	public static class ClientProjectileFactory
	{
		public static ClientProjectile Create(Projectile proj)
		{
			if (proj is PlasmaBlast)
				return new ClientPlasmaBlast((PlasmaBlast)proj);
			if (proj is OrbBlast)
				return new ClientOrbBlast((OrbBlast)proj);
			if (proj is Sabot)
				return new ClientSabot((Sabot)proj);
			if (proj is Blast)
				return new ClientBlast((Blast)proj);
			return null;
		}
	}
}
