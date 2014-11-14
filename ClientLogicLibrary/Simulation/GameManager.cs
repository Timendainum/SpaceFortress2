
namespace ClientLogicLibrary.Simulation
{
	public static class GameManager
	{
		public static ClientManagerSinglePlayer TheGameManager;
		
		public static void StartNewSinglePlayerGame()
		{
			EndGame();
			TheGameManager = new ClientManagerSinglePlayer();
		}

		public static void EndGame()
		{
			TheGameManager = null;
		}
	}
}
