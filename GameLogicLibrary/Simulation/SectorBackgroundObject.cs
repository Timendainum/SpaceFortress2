using Microsoft.Xna.Framework;

namespace GameLogicLibrary.Simulation
{
	public struct SectorBackgroundObject
	{
		private Vector2 _Location;
		public Vector2 Location
		{
			get
			{
				return _Location;
			}
			set
			{
				_Location = value;
			}
		}
		private Vector2 _Size;
		public Vector2 Size
		{
			get
			{
				return _Size;
			}
			set
			{
				_Size = value;
			}
		}
		private string _TextureName;
		public string TextureName
		{
			get
			{
				return _TextureName;
			}
			set
			{
				_TextureName = value;
			}
		}
		private float _ParalaxFactor;
		public float ParalaxFactor
		{
			get
			{
				return _ParalaxFactor;
			}
			set
			{
				_ParalaxFactor = value;
			}
		}

		public SectorBackgroundObject(Vector2 location, Vector2 size, string textureName, float paralaxFactor)
		{
			_Location = location;
			_Size = size;
			_TextureName = textureName;
			_ParalaxFactor = paralaxFactor;
		}
	}
}
