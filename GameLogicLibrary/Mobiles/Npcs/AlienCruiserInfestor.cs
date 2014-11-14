using Microsoft.Xna.Framework;
using GameLogicLibrary.Mobiles.Ships;
using GameLogicLibrary.Mobiles.Behaviors;

namespace GameLogicLibrary.Mobiles.Npcs
{
	public class AlienCruiserInfestor : Npc
	{
		public AlienCruiserInfestor(Vector2 location)
			: base(location)
		{
			CurrentShip = new AlienCruiser1();
			_BehaviorManager = new BasicBehaviorManager(this);
		}
	}
}
