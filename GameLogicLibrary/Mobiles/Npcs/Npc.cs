using Microsoft.Xna.Framework;
using GameLogicLibrary.Mobiles.Behaviors;
using GameLogicLibrary.Mobiles.Ships;

namespace GameLogicLibrary.Mobiles.Npcs
{
	public abstract class Npc : ShipPilot
	{
		public Vector2 HomeLocation;
		protected BehaviorManager _BehaviorManager;

		public Npc(Vector2 location)
			: base(location)
		{
			HomeLocation = location;
		}

		public override void Update(GameTime gameTime)
		{
			if (!Expired)
			{
				_BehaviorManager.Update(gameTime);
			}

			base.Update(gameTime);
		}
	}
}
