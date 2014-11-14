using System.Collections.Generic;
using ClientLogicLibrary.Graphics;
using GameLogicLibrary.Maths;
using GameLogicLibrary.Mobiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ClientLogicLibrary.Mobiles
{
	public class HumanCapitalship1ShipRenderer : ShipRenderer
	{
		public HumanCapitalship1ShipRenderer(ShipPilot serverPilot)
			: base (serverPilot)
		{
			//Ship sprite
			shipSprite = new AnimatedSprite(_ServerPilot.WorldLocation, _ServerPilot.Size, TaticalScreenTextureManager.GetTexture("ship_human_capital_1"), new Rectangle(0, 0, 256, 286), "default", 1, 1.0f);

			List<Texture2D> pTextures = new List<Texture2D>();
			pTextures.Add(TaticalScreenTextureManager.GetTexture("white_pixel"));

			particleEmitter1 = new ParticleEmitter(pTextures, emitter1WorldLocation);
			particleEmitter2 = new ParticleEmitter(pTextures, emitter2WorldLocation);
			particleEmitter3 = new ParticleEmitter(pTextures, emitter3WorldLocation);
			particleEmitter4 = new ParticleEmitter(pTextures, emitter4WorldLocation);
			particleEmitter5 = new ParticleEmitter(pTextures, emitter5WorldLocation);
			particleEmitter6 = new ParticleEmitter(pTextures, emitter6WorldLocation);

		}

		private AnimatedSprite shipSprite;
		private ParticleEmitter particleEmitter1;
		private Vector2 engine1RelativeEmitterLocation = new Vector2(95, 30);
		private float distanceToEmmitter1;
		private float angleToEmitter1;
		private Vector2 emitter1WorldLocation;

		private ParticleEmitter particleEmitter2;
		private Vector2 engine2RelativeEmitterLocation = new Vector2(95, 256);
		private float distanceToEmmitter2;
		private float angleToEmitter2;
		private Vector2 emitter2WorldLocation;

		private ParticleEmitter particleEmitter3;
		private Vector2 engine3RelativeEmitterLocation = new Vector2(86, 62);
		private float distanceToEmmitter3;
		private float angleToEmitter3;
		private Vector2 emitter3WorldLocation;

		private ParticleEmitter particleEmitter4;
		private Vector2 engine4RelativeEmitterLocation = new Vector2(86, 224);
		private float distanceToEmmitter4;
		private float angleToEmitter4;
		private Vector2 emitter4WorldLocation;

		private ParticleEmitter particleEmitter5;
		private Vector2 engine5RelativeEmitterLocation = new Vector2(48, 94);
		private float distanceToEmmitter5;
		private float angleToEmitter5;
		private Vector2 emitter5WorldLocation;

		private ParticleEmitter particleEmitter6;
		private Vector2 engine6RelativeEmitterLocation = new Vector2(48, 188);
		private float distanceToEmmitter6;
		private float angleToEmitter6;
		private Vector2 emitter6WorldLocation;


		public override void Draw(SpriteBatch spriteBatch)
		{
			//before drawing a mobile sprite has to make sure if it has moved that its graphics represent its current state.
			particleEmitter1.Draw(spriteBatch);
			particleEmitter2.Draw(spriteBatch);
			particleEmitter3.Draw(spriteBatch);
			particleEmitter4.Draw(spriteBatch);
			particleEmitter5.Draw(spriteBatch);
			particleEmitter6.Draw(spriteBatch);
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

			distanceToEmmitter3 = Vector2.Distance(engine3RelativeEmitterLocation, shipSprite.RelativeCenter);
			angleToEmitter3 = (MathsHelper.DirectInterceptAngle(shipSprite.RelativeCenter, engine3RelativeEmitterLocation) + shipSprite.Rotation) % MathHelper.TwoPi;
			particleEmitter3.EmitterWorldLocation = MathsHelper.RotateAroundCircle(angleToEmitter3, distanceToEmmitter3, shipSprite.RelativeCenter) + shipSprite.WorldLocation;
			particleEmitter3.Update(gameTime);

			distanceToEmmitter4 = Vector2.Distance(engine4RelativeEmitterLocation, shipSprite.RelativeCenter);
			angleToEmitter4 = (MathsHelper.DirectInterceptAngle(shipSprite.RelativeCenter, engine4RelativeEmitterLocation) + shipSprite.Rotation) % MathHelper.TwoPi;
			particleEmitter4.EmitterWorldLocation = MathsHelper.RotateAroundCircle(angleToEmitter4, distanceToEmmitter4, shipSprite.RelativeCenter) + shipSprite.WorldLocation;
			particleEmitter4.Update(gameTime);

			distanceToEmmitter5 = Vector2.Distance(engine5RelativeEmitterLocation, shipSprite.RelativeCenter);
			angleToEmitter5 = (MathsHelper.DirectInterceptAngle(shipSprite.RelativeCenter, engine5RelativeEmitterLocation) + shipSprite.Rotation) % MathHelper.TwoPi;
			particleEmitter5.EmitterWorldLocation = MathsHelper.RotateAroundCircle(angleToEmitter5, distanceToEmmitter5, shipSprite.RelativeCenter) + shipSprite.WorldLocation;
			particleEmitter5.Update(gameTime);

			distanceToEmmitter6 = Vector2.Distance(engine6RelativeEmitterLocation, shipSprite.RelativeCenter);
			angleToEmitter6 = (MathsHelper.DirectInterceptAngle(shipSprite.RelativeCenter, engine6RelativeEmitterLocation) + shipSprite.Rotation) % MathHelper.TwoPi;
			particleEmitter6.EmitterWorldLocation = MathsHelper.RotateAroundCircle(angleToEmitter6, distanceToEmmitter6, shipSprite.RelativeCenter) + shipSprite.WorldLocation;
			particleEmitter6.Update(gameTime);


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
