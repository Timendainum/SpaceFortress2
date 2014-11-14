
namespace GameLogicLibrary.Mobiles.Modules.Shields
{
	public abstract class Shield : Module
	{
		public int RegenerationRate { get; protected set; }
		private int _ShieldPoints;
		public int ShieldPoints
		{
			get
			{
				return _ShieldPoints;
			}
			protected set
			{
				_ShieldPoints = value;
			}
		}
	}
}
