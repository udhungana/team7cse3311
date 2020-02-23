using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Carronade {
	//All "interactive" objects in the game will be some form of actor.

	public class TestEnemyActor : KinematicActor {
		private Sprite enemySprite;
		private float baseSpeed = 32.0f;
		public TestEnemyActor(float x, float y, float r) : base(x, y, r) {

		}
		public TestEnemyActor(Vector2 pos, float r) : base(pos, r) {

		}
		public override void Initialize() {
			base.Initialize();
			enemySprite = new Sprite(3);
			double radRot = Math.PI / 180 * rotation;
			Vector2 facing = new Vector2(((float) Math.Cos(radRot)) * baseSpeed, ((float) Math.Sin(radRot)) * baseSpeed);
			Console.WriteLine(string.Format("{0} - {1}:{2}", facing, Math.Cos(Math.PI / 180 * rotation), Math.Sin(Math.PI / 180 * rotation)));
			SetVelocity(facing);
		}
		public override void Update(GameTime gameTime) {
			base.Update(gameTime);
		}
		public override void Draw(SpriteBatch canvas) {
			enemySprite.Draw(canvas, position, (float) Math.PI / 180 * rotation);
		}
	}
}