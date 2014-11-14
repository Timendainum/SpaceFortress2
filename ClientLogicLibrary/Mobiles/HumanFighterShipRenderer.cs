using System.Collections.Generic;
using ClientLogicLibrary.Graphics;
using GameLogicLibrary.Maths;
using GameLogicLibrary.Mobiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ClientLogicLibrary.Mobiles
{
	public class HumanFighterShipRenderer : ShipRenderer
	{
		private AnimatedSprite shipSprite;
		private ParticleEmitter particleEmitter1;
		private Vector2 engine1RelativeEmitterLocation = new Vector2(1, 8);
		private float distanceToEmmitter1;
		private float angleToEmitter1;
		private Vector2 emitter1WorldLocation;


		public HumanFighterShipRenderer(ShipPilot serverPilot)
			: base (serverPilot)
		{
			//Ship sprite
			shipSprite = new AnimatedSprite(_ServerPilot.WorldLocation, _ServerPilot.Size, TaticalScreenTextureManager.GetTexture("ship_human_fighter"), new Rectangle(0, 0, 23, 16), "default", 1, 1.0f);

			List<Texture2D> pTextures = new List<Texture2D>();
			pTextures.Add(TaticalScreenTextureManager.GetTexture("white_pixel"));

			particleEmitter1 = new ParticleEmitter(pTextures, emitter1WorldLocation);
		}

		


		public override void Draw(SpriteBatch spriteBatch)
		{
			//before drawing a mobile sprite has to make sure if it has moved that its graphics represent its current state.
			particleEmitter1.Draw(spriteBatch);
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
