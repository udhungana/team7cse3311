using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Carronade {
	//All "interactive" objects in the game will be some form of actor.

	public class TestEnemyActor : KinematicActor {
		private Sprite enemySprite;
		private float baseSpeed = 16.0f;
		public TestEnemyActor(float x, float y, float r, float spd) : base(x, y, r) {
			baseSpeed = spd;	
		}
		public TestEnemyActor(Vector2 pos, float r, float spd) : base(pos, r) {
			baseSpeed = spd;
		}
		public override void Initialize() {
			base.Initialize();
			enemySprite = new Sprite(3);
			Vector2 facing = new Vector2(((float) Math.Cos(rotation)) * baseSpeed, ((float) Math.Sin(rotation)) * baseSpeed);
			//Console.WriteLine(string.Format("{0} - {1}:{2}", facing, Math.Cos(Math.PI / 180 * rotation), Math.Sin(Math.PI / 180 * rotation)));
			SetVelocity(facing);
		}
		public override void Update(GameTime gameTime) {
			base.Update(gameTime);
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
	}
}