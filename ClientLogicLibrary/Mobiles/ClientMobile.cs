using ClientLogicLibrary.Simulation;
using GameLogicLibrary.Simulation;

namespace ClientLogicLibrary.Mobiles
{
	public abstract class ClientMobile : ClientEntity
	{
		public Mobile ServerMobile { get; set; }
	}
}
