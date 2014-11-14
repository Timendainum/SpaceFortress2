using Microsoft.Xna.Framework;
using GameLogicLibrary.Mobiles.Ships;
using GameLogicLibrary.Mobiles.Behaviors;

namespace GameLogicLibrary.Mobiles.Npcs
{
	public class AlienFrigateInfestor : Npc
	{
		public AlienFrigateInfestor(Vector2 location)
			: base(location)
		{
			CurrentShip = new AlienFrigate1();
			_BehaviorManager = new BasicBehaviorManager(this);
		}
	}
}
