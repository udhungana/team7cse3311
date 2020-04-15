using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Carronade {
	//All "interactive" objects in the game will be some form of actor.

	public class AcceleratedShot : EnemyActor {
		private Sprite enemySprite;
		private float baseSpeed = 16.0f;
		private double birthTime = -1;
		public AcceleratedShot(float x, float y, float r, float spd) : base(x, y, r) {
			baseSpeed = spd;	
		}
		public AcceleratedShot(Vector2 pos, float r, float spd) : base(pos, r) {
			baseSpeed = spd;
		}
		public override void Initialize() {
			enemySprite = new Sprite(402);
			Vector2 facing = new Vector2(((float) Math.Cos(rotation)) * baseSpeed, ((float) Math.Sin(rotation)) * baseSpeed);
			//Console.WriteLine(string.Format("{0} - {1}:{2}", facing, Math.Cos(Math.PI / 180 * rotation), Math.Sin(Math.PI / 180 * rotation)));
			SetVelocity(facing);
			SetDamage(20);
		}
		//gotta go fast
		//Accelerated arrows go faster the longer they're alive.
		//TODO: Raycast into the player
		public override void Update(GameTime gameTime) {
			double curTime = gameTime.TotalGameTime.TotalSeconds;
			if (birthTime == -1)
				birthTime = curTime;
			double timeDifference = curTime - birthTime;
			float scaledSpeed = baseSpeed + 1024 * (float) (timeDifference * timeDifference);
			Vector2 facing = new Vector2(((float) Math.Cos(rotation)) * scaledSpeed, ((float)Math.Sin(rotation)) * scaledSpeed);
			SetVelocity(facing);
		}
		public override void LateUpdate(GameTime gameTime) {
			base.LateUpdate(gameTime);
			Vector2 pos = GetCenterPosition();
			if (pos.X < -GetBounds().Width / 2)
				OnKilled();
			else if (pos.X > Game1.mainGame.ViewPort.Width)
				OnKilled();
			if (pos.Y < -GetBounds().Height / 2)
				OnKilled();
			else if (pos.Y > Game1.mainGame.ViewPort.Height)
				OnKilled();
		}
		public override void Draw(SpriteBatch canvas) {
			enemySprite.DrawCentered(canvas, position, rotation);
		}
		public override Vector2 GetCenterPosition() {
			return GetPosition() + new Vector2(enemySprite.GetBounds().Width / 2, enemySprite.GetBounds().Height / 2);
		}
		public override Vector2 GetPosition() {
			return position;
		}
		public override Rectangle GetBounds() {
			return enemySprite.GetBounds();
		}
	}
}