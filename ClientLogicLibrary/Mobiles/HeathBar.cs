using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameLogicLibrary.Mobiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ClientLogicLibrary.Graphics;
using GameLogicLibrary.Mobiles.Ships;
using GameLogicLibrary.Maths;
using GameLogicLibrary.Simulation;

namespace ClientLogicLibrary.Mobiles
{
	public class HeathBar
	{
		private ShipPilot _shipPilot;
		private Texture2D _texture;
		private Rectangle _display = new Rectangle(0,0,75,40);
		private Vector2 _displayRelativeCenter;
		private Rectangle _healthBar = new Rectangle(0,41,73,11);
		private Rectangle _shield = new Rectangle(1,1,73,11);
        private Vector2 _shieldRelativeCenter;
		private Rectangle _armor = new Rectangle(1,15,73,11);
        private Vector2 _armorRelativeCenter;
		private Rectangle _structure = new Rectangle(1,23,73,11);
        private Vector2 _structureRelativeCenter;

		private Ship _ship;
		private Vector2 _shipWorldCenter;

		private Vector2 healthBarWorldCenter;
        private Vector2 shieldWorldCenter;
        private Vector2 armorWorldCenter;
        private Vector2 structureWorldCenter;

		private Rectangle _currentShield = new Rectangle(1, 1, 73, 11);
		private Rectangle _currentArmor = new Rectangle(1, 15, 73, 11);
		private Rectangle _currentStructure = new Rectangle(1, 23, 73, 11);

		
		public HeathBar(ShipPilot serverPilot)
		{
			_shipPilot = serverPilot;
			_texture = TaticalScreenTextureManager.GetTexture("healthbar_small");
			_displayRelativeCenter = new Vector2(_display.Width / 2, _display.Height / 2);
			_shieldRelativeCenter = new Vector2(_shield.Width / 2, _shield.Height / 2);
			_armorRelativeCenter = new Vector2(_armor.Width / 2, _armor.Height / 2);
			_structureRelativeCenter = new Vector2(_structure.Width / 2, _structure.Height / 2);
			
		}

		public void Update(GameTime gameTime)
		{
			//update positional variables
			_ship = _shipPilot.CurrentShip;
			_shipWorldCenter = _shipPilot.WorldCenter;
			healthBarWorldCenter = MathsHelper.RotateAroundCircle(MathHelper.PiOver2, _ship.CollisionRadius + 20,  _shipWorldCenter);
			shieldWorldCenter = MathsHelper.RotateAroundCircle(MathHelper.PiOver2, _ship.CollisionRadius + 25 -19, _shipWorldCenter);
			armorWorldCenter = MathsHelper.RotateAroundCircle(MathHelper.PiOver2, _ship.CollisionRadius + 25 - 5, _shipWorldCenter);
			structureWorldCenter = MathsHelper.RotateAroundCircle(MathHelper.PiOver2, _ship.CollisionRadius + 25 + 7, _shipWorldCenter);

			_currentShield = new Rectangle(_healthBar.X, _healthBar.Y, (int)(_healthBar.Width * _ship.ShieldCurrentPercent), _healthBar.Height);
			_currentArmor = new Rectangle(_healthBar.X, _healthBar.Y, (int)(_healthBar.Width * _ship.ArmorCurrentPercent), _healthBar.Height);
			_currentStructure = new Rectangle(_healthBar.X, _healthBar.Y, (int)(_healthBar.Width * _ship.StructureCurrentPercent), _healthBar.Height);

		}

		public void Draw(SpriteBatch spriteBatch)
		{
			//draw shield
			spriteBatch.Draw(
						_texture,
						Camera.TransformWorldToCamera(shieldWorldCenter),
						_currentShield,
						Color.Blue,
						0f,
						_shieldRelativeCenter,
						1f,
						SpriteEffects.None,
						0.0f);
			//draw armor
			spriteBatch.Draw(
						_texture,
						Camera.TransformWorldToCamera(armorWorldCenter),
						_currentArmor,
						Color.DarkGoldenrod,
						0f,
						_armorRelativeCenter,
						1f,
						SpriteEffects.None,
						0.0f);
		//draw struct
			spriteBatch.Draw(
						_texture,
						Camera.TransformWorldToCamera(structureWorldCenter),
						_currentStructure,
						Color.SlateGray,
						0f,
						_structureRelativeCenter,
						1f,
						SpriteEffects.None,
						0.0f);
		//draw meter
			spriteBatch.Draw(
						_texture,
						Camera.TransformWorldToCamera(healthBarWorldCenter),
						_display,
						Color.White,
						0f,
						_displayRelativeCenter,
						1f,
						SpriteEffects.None,
						0.0f);
		
		}
	}
}
