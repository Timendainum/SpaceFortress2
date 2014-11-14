using ClientLogicLibrary.Simulation;
using GameLogicLibrary.Immobiles;

namespace ClientLogicLibrary.Immobiles
{

	public abstract class ClientImmobile : ClientEntity
	{
		public Immobile ServerImmobile { get; set; }
	}
}
