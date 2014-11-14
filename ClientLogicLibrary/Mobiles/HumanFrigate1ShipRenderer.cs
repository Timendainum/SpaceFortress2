using System.Collections.Generic;
using ClientLogicLibrary.Graphics;
using GameLogicLibrary.Maths;
using GameLogicLibrary.Mobiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ClientLogicLibrary.Mobiles
{
	public class HumanFrigate1ShipRenderer : ShipRenderer
	{
		public HumanFrigate1ShipRenderer(ShipPilot serverPilot)
			: base (serverPilot)
		{
			//Ship sprite
			shipSprite = new AnimatedSprite(_ServerPilot.WorldLocation, _ServerPilot.Size, TaticalScreenTextureManager.GetTexture("ship_human_frigate_1"), new Rectangle(0, 0, 32, 29), "default", 1, 1.0f);

			List<Texture2D> pTextures = new List<Texture2D>();
			pTextures.Add(TaticalScreenTextureManager.GetTexture("white_pixel"));

			particleEmitter1 = new ParticleEmitter(pTextures, emitter1WorldLocation);
			particleEmitter2 = new ParticleEmitter(pTextures, emitter2WorldLocation);


		}

		private AnimatedSprite shipSprite;
		private ParticleEmitter particleEmitter1;
		private Vector2 engine1RelativeEmitterLocation = new Vector2(2, 4);
		private float distanceToEmmitter1;
		private float angleToEmitter1;
		private Vector2 emitter1WorldLocation;

		private ParticleEmitter particleEmitter2;
		private Vector2 engine2RelativeEmitterLocation = new Vector2(2, 26);
		private float distanceToEmmitter2;
		private float angleToEmitter2;
		private Vector2 emitter2WorldLocation;


		public override void Draw(SpriteBatch spriteBatch)
		{
			//before drawing a mobile sprite has to make sure if it has moved that its graphics represent its current state.
			particleEmitter1.Draw(spriteBatch);
			particleEmitter2.Draw(spriteBatch);
			shipSprite.Draw(spriteBatch);
			base.Draw(spriteBatch);
		}


		public override void Update(GameTime gameTime)
		{
			//Update graphics based on server state
			shipSprite.Rotation = _ServerPilot.Rotation;
			shipSprite.WorldLocation = _ServerPilot.WorldLocation;
			shipSprite.Update(gameTime);

			//Emitters
			distanceToEmmitter1 = Vector2.Distance(engine1RelativeEmitterLocation, shipSprite.RelativeCenter);
			angleToEmitter1 = (MathsHelper.DirectInterceptAngle(shipSprite.RelativeCenter, engine1RelativeEmitterLocation) + shipSprite.Rotation) % MathHelper.TwoPi;
			particleEmitter1.EmitterWorldLocation = MathsHelper.RotateAroundCircle(angleToEmitter1, distanceToEmmitter1, shipSprite.RelativeCenter) + shipSprite.WorldLocation;
			particleEmitter1.Update(gameTime);

			distanceToEmmitter2 = Vector2.Distance(engine2RelativeEmitterLocation, shipSprite.RelativeCenter);
			angleToEmitter2 = (MathsHelper.DirectInterceptAngle(shipSprite.RelativeCenter, engine2RelativeEmitterLocation) + shipSprite.Rotation) % MathHelper.TwoPi;
			particleEmitter2.EmitterWorldLocation = MathsHelper.RotateAroundCircle(angleToEmitter2, distanceToEmmitter2, shipSprite.RelativeCenter) + shipSprite.WorldLocation;
			particleEmitter2.Update(gameTime);
			base.Update(gameTime);
		}

		#region helpers
		public override Rectangle GetWorldRectangle()
		{
			return shipSprite.WorldRectangle;
		}
		#endregion
	}
}
