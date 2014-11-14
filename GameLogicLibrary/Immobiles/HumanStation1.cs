using GameLogicLibrary.Simulation;
using Microsoft.Xna.Framework;
using GameLogicLibrary.Mobiles.Ships;
using GameLogicLibrary.Mobiles.Modules.Armors;
using GameLogicLibrary.Mobiles.Modules.Engines;
using GameLogicLibrary.Mobiles.Modules.Generators;
using GameLogicLibrary.Mobiles.Modules.Shields;
using GameLogicLibrary.Mobiles.Modules.Weapons;

namespace GameLogicLibrary.Immobiles
{
	public class HumanStation1 : Station
	{
		float rotationSpeed = 0.01f;
		
		public HumanStation1(Vector2 location)
			: base(location)
		{
			Size = new Vector2(800, 800);
			DockingRange = 600f;
			CollisionRadius = 400;
			CollisionMap = CollisionMapManager.GetTexture("human_station1");

			//Add some ships and mod
			ShipStorage.Add(new HumanFighter());
			ShipStorage.Add(new HumanFighter());
			ShipStorage.Add(new HumanFighter());
			ShipStorage.Add(new HumanFighter());
			ShipStorage.Add(new HumanFighter());
			ShipStorage.Add(new HumanFighter());
			ShipStorage.Add(new HumanFighter());
			ShipStorage.Add(new HumanFrigate1());
			ShipStorage.Add(new HumanFrigate1());
			ShipStorage.Add(new HumanFrigate1());
			ShipStorage.Add(new HumanFrigate1());
			ShipStorage.Add(new HumanCruiser1());
			ShipStorage.Add(new HumanBattleship1());
			ShipStorage.Add(new HumanCapitalship1());
			ShipStorage.Add(new HumanCruiser1());
			ShipStorage.Add(new HumanBattleship1());
			ShipStorage.Add(new HumanCapitalship1());
			ShipStorage.Add(new HumanCruiser1());
			ShipStorage.Add(new HumanBattleship1());
			ShipStorage.Add(new HumanCapitalship1());

			ModuleStorage.Add(new BasicArmor());
			ModuleStorage.Add(new BasicArmor());
			ModuleStorage.Add(new BasicArmor());
			ModuleStorage.Add(new LightArmor());
			ModuleStorage.Add(new LightArmor());
			ModuleStorage.Add(new LightArmor());
			ModuleStorage.Add(new MediumArmor());
			ModuleStorage.Add(new MediumArmor());
			ModuleStorage.Add(new MediumArmor());
			ModuleStorage.Add(new HeavyArmor());
			ModuleStorage.Add(new HeavyArmor());
			ModuleStorage.Add(new HeavyArmor());

			ModuleStorage.Add(new BasicEngine());
			ModuleStorage.Add(new BasicEngine());
			ModuleStorage.Add(new BasicEngine());
			ModuleStorage.Add(new LightEngine());
			ModuleStorage.Add(new LightEngine());
			ModuleStorage.Add(new LightEngine());
			ModuleStorage.Add(new MediumEngine());
			ModuleStorage.Add(new MediumEngine());
			ModuleStorage.Add(new MediumEngine());
			ModuleStorage.Add(new HeavyEngine());
			ModuleStorage.Add(new HeavyEngine());
			ModuleStorage.Add(new HeavyEngine());

			ModuleStorage.Add(new BasicGenerator());
			ModuleStorage.Add(new BasicGenerator());
			ModuleStorage.Add(new BasicGenerator());
			ModuleStorage.Add(new LightGenerator());
			ModuleStorage.Add(new LightGenerator());
			ModuleStorage.Add(new LightGenerator());
			ModuleStorage.Add(new MediumGenerator());
			ModuleStorage.Add(new MediumGenerator());
			ModuleStorage.Add(new MediumGenerator());
			ModuleStorage.Add(new HeavyGenerator());
			ModuleStorage.Add(new HeavyGenerator());
			ModuleStorage.Add(new HeavyGenerator());

			ModuleStorage.Add(new BasicShield());
			ModuleStorage.Add(new BasicShield());
			ModuleStorage.Add(new BasicShield());
			ModuleStorage.Add(new LightShield());
			ModuleStorage.Add(new LightShield());
			ModuleStorage.Add(new LightShield());
			ModuleStorage.Add(new MediumShield());
			ModuleStorage.Add(new MediumShield());
			ModuleStorage.Add(new MediumShield());
			ModuleStorage.Add(new HeavyShield());
			ModuleStorage.Add(new HeavyShield());
			ModuleStorage.Add(new HeavyShield());

			ModuleStorage.Add(new SpinalOrbGun());
			ModuleStorage.Add(new SpinalOrbGun());
			ModuleStorage.Add(new SpinalOrbGun());
			ModuleStorage.Add(new SpinalPlasmaGun());
			ModuleStorage.Add(new SpinalPlasmaGun());
			ModuleStorage.Add(new SpinalPlasmaGun());
			ModuleStorage.Add(new TurretPlasmaGun());
			ModuleStorage.Add(new TurretPlasmaGun());
			ModuleStorage.Add(new TurretPlasmaGun());
			ModuleStorage.Add(new TurretRailGun());
			ModuleStorage.Add(new TurretRailGun());
			ModuleStorage.Add(new TurretRailGun());
			ModuleStorage.Add(new SpinalOrbGun());
			ModuleStorage.Add(new SpinalOrbGun());
			ModuleStorage.Add(new SpinalOrbGun());
			ModuleStorage.Add(new SpinalPlasmaGun());
			ModuleStorage.Add(new SpinalPlasmaGun());
			ModuleStorage.Add(new SpinalPlasmaGun());
			ModuleStorage.Add(new TurretPlasmaGun());
			ModuleStorage.Add(new TurretPlasmaGun());
			ModuleStorage.Add(new TurretPlasmaGun());
			ModuleStorage.Add(new TurretRailGun());
			ModuleStorage.Add(new TurretRailGun());
			ModuleStorage.Add(new TurretRailGun());
			ModuleStorage.Add(new SpinalOrbGun());
			ModuleStorage.Add(new SpinalOrbGun());
			ModuleStorage.Add(new SpinalOrbGun());
			ModuleStorage.Add(new SpinalPlasmaGun());
			ModuleStorage.Add(new SpinalPlasmaGun());
			ModuleStorage.Add(new SpinalPlasmaGun());
			ModuleStorage.Add(new TurretPlasmaGun());
			ModuleStorage.Add(new TurretPlasmaGun());
			ModuleStorage.Add(new TurretPlasmaGun());
			ModuleStorage.Add(new TurretRailGun());
			ModuleStorage.Add(new TurretRailGun());
			ModuleStorage.Add(new TurretRailGun());


		}

		public override void Update(GameTime gameTime)
		{
			if (!Expired)
			{
				float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

				Rotation += (rotationSpeed * elapsed);
			}
			base.Update(gameTime);
		}

	}
}
