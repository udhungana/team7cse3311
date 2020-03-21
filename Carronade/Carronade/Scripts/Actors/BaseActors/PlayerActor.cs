using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Carronade {
	//All "interactive" objects in the game will be some form of actor.

	public abstract class PlayerActor : KinematicActor {
		private float damageLevel = 0;
		private float maxDamage = 0;
		public PlayerActor(float x, float y, float r) : base(x, y, r) {

		}
		public PlayerActor(Vector2 pos, float r) : base(pos, r) {

		}
		public override void Initialize() {
			base.Initialize();
			SetVelocity(0,0);
		}
		public void SetMaxDamage(float amount) {
			maxDamage = amount;
		}
		public void Damage(float amount) {
			damageLevel += amount;
			damageLevel = Math.Min(damageLevel, maxDamage);
			if (damageLevel == maxDamage)
				Perish();
		}
		public void Heal(float amount) {
			damageLevel -= amount;
			damageLevel = Math.Max(damageLevel, 0);
		}
		public void Perish() {

		}
		public override void LateUpdate(GameTime gameTime) {
			Vector2 playPos = GetCenterPosition();
			foreach (var actor in Game1.mainGame.actors) {
				Type t = actor.GetType();
				if (Equals(t, typeof(EnemyActor))) {
					//16 + 15
					Vector2 enemPos = actor.GetCenterPosition();
					float xDist = playPos.X - enemPos.X;
					float yDist = playPos.Y - enemPos.Y;
					if (Math.Sqrt((xDist * xDist + yDist * yDist)) < 32) {
						Game1.mainGame.RemoveActor(actor);
					}
				} else
					continue;
			}
			base.LateUpdate(gameTime);
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