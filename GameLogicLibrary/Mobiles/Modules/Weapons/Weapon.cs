

using Microsoft.Xna.Framework;


namespace GameLogicLibrary.Mobiles.Modules.Weapons
{
	public abstract class Weapon : Module
	{
		public float Cooldown { get; protected set; }
		public float CooldownTimer { get; set; }
		public float OffsetTimer = 0f;
		public float Offset = 0f;
		public string FiredSoundEffect { get; protected set; }
		public Vector2 RelativeFirePosition;

		public abstract void Fire(ShipPilot firedBy, Vector2 fireWorldPosition);
		public virtual void Update(GameTime gameTime)
		{
			float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
			CooldownTimer += elapsed;
			OffsetTimer += elapsed;
		}
	}
}
