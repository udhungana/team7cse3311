using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Carronade {
	//All "interactive" objects in the game will be some form of actor.

	public abstract class PlayerActor : KinematicActor {
		private int damageLevel = 0;
		private int maxDamage = 0;
		public bool invuln { get; private set; } = false;
		public PlayerActor(float x, float y, float r) : base(x, y, r) {

		}
		public PlayerActor(Vector2 pos, float r) : base(pos, r) {

		}
		#region HEALTH FUNCTIONS
		//It's easier to do a lot of math involving health if it's keeping track of how much DAMAGE the player has taken as opposed to how much HEALTH is remaining
		public int GetDamageLevel() {
			return damageLevel;
		}
		public int GetMaxDamage() {
			return maxDamage;
		}
		public float GetPercentMaxHealth() {
			if (damageLevel == 0)
				return 1.0f;
			return 1.0f - ((float)damageLevel) / ((float) (maxDamage));
		}
		public override void Initialize() {
			SetVelocity(0,0);
		}
		public void SetMaxDamage(int amount) {
			maxDamage = amount;
		}
		public void Damage(int amount) {
			if (!invuln) {
				damageLevel += amount;
				//The damage taken can NEVER exceed the maxDamage, that's the point
				damageLevel = Math.Min(damageLevel, maxDamage);
				if (damageLevel == maxDamage)
					Perish();
			}
		}
		public void Heal(int amount) {
			damageLevel -= amount;
			//A player can NEVER have negative damage taken.
			damageLevel = Math.Max(damageLevel, 0);
		}
		public void SetInvuln(bool inv) {
			invuln = inv;
		}
		//Then
		public void Perish() {
			//There is no previous highscore so just throw the player a bone.
			if (Game1.mainGame.highScore == 0)
				Game1.mainGame.highScore = GameRoom.gameRoom.Score;
			Game1.mainGame.SwitchRooms("MainRoom");
		}
		#endregion
		public override void Update(GameTime gameTime) {
			
		}
		//Collision detection!
		public override void LateUpdate(GameTime gameTime) {
			Vector2 playPos = GetCenterPosition();
			foreach (var actor in GameRoom.gameRoom.actors) {
				Type t = actor.GetType();
				//Spend as little time as humanly possible comparing the player to every actor type that exists in the room.
				if (t.IsSubclassOf(typeof(EnemyActor))) {
					EnemyActor enem = (EnemyActor) actor;
					Vector2 enemPos = enem.GetCenterPosition();
					float xDist = playPos.X - enemPos.X;
					float yDist = playPos.Y - enemPos.Y;
					//Collisions treat both objects as circle. Not the most accurate hit detection but computationally cheap and easier to write.
					if (Math.Sqrt((xDist * xDist + yDist * yDist)) < 32) {
						Damage(enem.damage);
						if (invuln) {
							enem.OnKilled();
							Sound.GetSound(1004).CreateInstance().Play();
						} else {
							enem.OnImpact();
							Sound.GetSound(1003).CreateInstance().Play();
						}
					}
				} else if(t.IsSubclassOf(typeof(PowerupActor))) {
					PowerupActor pow = (PowerupActor) actor;
					if (pow.picked)
						continue;
					Vector2 powPos = pow.GetCenterPosition();
					float xDist = playPos.X - powPos.X;
					float yDist = playPos.Y - powPos.Y;
					if (Math.Sqrt((xDist * xDist + yDist * yDist)) < 32) {
						pow.OnPickup(this, gameTime);
					}
				} else
					continue;
			}
			base.LateUpdate(gameTime);
			//Out of bounds correction
			if (playPos.X < -GetBounds().Width / 2)
				position = new Vector2(-GetBounds().Width / 2, position.Y);
			else if (playPos.X > Game1.mainGame.ViewPort.Width)
				position = new Vector2(Game1.mainGame.ViewPort.Width - GetBounds().Width / 2, position.Y);
			if (playPos.Y < -GetBounds().Height / 2)
				position = new Vector2(position.X, -GetBounds().Height / 2);
			else if (playPos.Y > Game1.mainGame.ViewPort.Height)
				position = new Vector2(position.X, Game1.mainGame.ViewPort.Height - GetBounds().Height / 2);
		}
	}
}