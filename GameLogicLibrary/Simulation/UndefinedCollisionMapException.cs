using System;
using System.Runtime.Serialization;

namespace GameLogicLibrary.Simulation
{
	public class UndefinedCollisionMapException : Exception
	{
		public Entity Entity1 { get; private set; }
		public Entity Entity2 { get; private set; }
		
		
		public UndefinedCollisionMapException()
		{
			
		}

		public UndefinedCollisionMapException(Entity e1, Entity e2)
		{
			Entity1 = e1;
			Entity2 = e2;
		}

		public UndefinedCollisionMapException(string message)
			: base(message)
		{
			
		}
		public UndefinedCollisionMapException(string message, Exception innerException)
			: base(message, innerException)
		{
			
		}
		protected UndefinedCollisionMapException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			
		}
         
	}
}
