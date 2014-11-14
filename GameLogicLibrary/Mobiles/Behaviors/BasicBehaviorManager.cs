using GameLogicLibrary.Mobiles.Npcs;
using Microsoft.Xna.Framework;
using GameLogicLibrary.Mobiles.Modules.Weapons;
using GameLogicLibrary.Maths;
using GameLogicLibrary.Simulation;

namespace GameLogicLibrary.Mobiles.Behaviors
{
	public class BasicBehaviorManager : BehaviorManager
	{
		public BasicBehaviorManager(Npc theNpc)
			: base(theNpc)
		{
			CurrentBehavior = new WanderAround(theNpc, _rand);
		}

		public override void Update(GameTime gameTime)
		{
			float wanderDistance = 3000f;
			float minDistance = 800f;
			float distanceToHome = Vector2.Distance(TheNpc.WorldCenter, TheNpc.HomeLocation);
			float interceptAngle;
			Player player = null;
			float minShootDistance = 800f;
			float minChaseDistance = 2500f;
			float distanceToTarget = float.MaxValue;
			float minCloseDistance = 0f;

			//See if there is a player in the sector to shoot at
			if (TheNpc.CurrentSector.Players.Count > 0)
			{
				player = TheNpc.CurrentSector.Players[0];
				distanceToTarget = Vector2.Distance(TheNpc.WorldCenter, player.WorldCenter);
				minCloseDistance = player.CollisionRadius + 200f;
				if (player != null && !player.Expired && distanceToTarget < minChaseDistance)
				{
					CurrentTarget = player;
					
				}
			}
			else
				CurrentTarget = null;

			//Keep weapons rotated at target
			if (distanceToTarget < minChaseDistance)
			{
				interceptAngle = MathsHelper.DirectInterceptAngle(TheNpc.WorldCenter, CurrentTarget.WorldCenter);
				foreach (TurretWeapon weapon in TheNpc.CurrentShip.TurretWeapons)
				{
					weapon.RotateTo = interceptAngle;
				}
			}

			//Decide which behavior to use ---------------------------------------------------------------
			
			//If we're really close, try to get in behind the player
			if (distanceToTarget <= minCloseDistance)
			{
				//try to get behind the target
				float angle = CurrentTarget.Rotation - MathHelper.Pi;
				float distance = minCloseDistance + 20f;
				Vector2 targetPosition = MathsHelper.RotateAroundCircle(angle, distance, CurrentTarget.WorldCenter);

				if (CurrentBehavior is GoToWorldLocation)
				{
					GoToWorldLocation cb = (GoToWorldLocation)CurrentBehavior;
					cb.Destination = targetPosition;
				} else
					CurrentBehavior = new GoToWorldLocation(TheNpc, _rand, targetPosition);
			}
			//if player is nearby then shoot him
			else if (distanceToTarget <= minShootDistance)
			{
				if (!(CurrentBehavior is FireAtTarget))
					CurrentBehavior = new FireAtTarget(TheNpc, _rand, CurrentTarget);
			}
			//Chase the player
			else if (distanceToTarget <= minChaseDistance)
			{
				if (CurrentBehavior is GoToWorldLocation)
				{
					GoToWorldLocation cb = (GoToWorldLocation)CurrentBehavior;
					cb.Destination = CurrentTarget.WorldCenter;
				} else
					CurrentBehavior = new GoToWorldLocation(TheNpc, _rand, CurrentTarget.WorldCenter);
			}
			//If I'm wandering and I'm to far away, go home
			else if ((CurrentBehavior is WanderAround) && distanceToHome > wanderDistance)
			{
				if (CurrentBehavior is GoToWorldLocation)
				{
					GoToWorldLocation cb = (GoToWorldLocation)CurrentBehavior;
					cb.Destination = TheNpc.HomeLocation;
				} else
					CurrentBehavior = new GoToWorldLocation(TheNpc, _rand, TheNpc.HomeLocation);
			}
			//Wander around
			else if ((CurrentBehavior is WanderAround) && distanceToHome < wanderDistance)
			{
				if (!(CurrentBehavior is WanderAround))
					CurrentBehavior = new WanderAround(TheNpc, _rand);
			}
			else
				CurrentBehavior = new WanderAround(TheNpc, _rand);
			
			base.Update(gameTime);
		}
	}
}
