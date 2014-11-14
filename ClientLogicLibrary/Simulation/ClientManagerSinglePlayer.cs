using ClientLogicLibrary.Client;
using ClientLogicLibrary.Effects;
using GameLogicLibrary.Mobiles;
using GameLogicLibrary.Simulation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace ClientLogicLibrary.Simulation
{
	public class ClientManagerSinglePlayer : ClientManager
	{
		#region declarations

		public Universe GameUniverse;
		public Player ThePlayer;
		#endregion

		public ClientManagerSinglePlayer()
		{
			
		}

		#region XNA Methods
		public void Init(ContentManager content)
		{
			RandomManager.Init();
			
			CollisionMapManager.Init(content);
			SoundManager.Init(content);

			//Instance new game universe
			GameUniverse = new Universe();

			ThePlayer = new Player(Vector2.Zero, GameUniverse.Human);
			ThePlayer.CurrentSector = GameManager.TheGameManager.GameUniverse.Sectors[0];
			ThePlayer.HomeStation = ThePlayer.CurrentSector.CapitalStation;
		}
		
		public void Update(GameTime gameTime)
		{
			//Update the game state
			GameUniverse.Update(gameTime);
		}
		#endregion

	}
}
