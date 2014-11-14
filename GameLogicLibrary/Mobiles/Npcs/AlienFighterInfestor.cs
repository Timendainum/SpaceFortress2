using Microsoft.Xna.Framework;
using GameLogicLibrary.Mobiles.Ships;
using GameLogicLibrary.Mobiles.Behaviors;

namespace GameLogicLibrary.Mobiles.Npcs
{
	public class AlienFighterInfestor : Npc
	{
		public AlienFighterInfestor(Vector2 location)
			: base(location)
		{
			CurrentShip = new AlienFighter();
			_BehaviorManager = new BasicBehaviorManager(this);
		}
	}
}
