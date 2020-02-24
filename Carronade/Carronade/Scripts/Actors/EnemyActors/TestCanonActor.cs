using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Carronade {
	//All "interactive" objects in the game will be some form of actor.

	public class TestCanonActor : KinematicActor {
		private Vector2 widthHeight;

		private AnimatedSprite canonSprite;
		private Vector2 aim = Vector2.One;
		private Vector2 canonMuzzle = Vector2.Zero;
		private double lastSingleShot = 5.0f;
		private double lastDoubleShot = 7.0f;
		private double lastTripleShot = 10.0f;
		public TestCanonActor(float x, float y, float r) : base(x, y, r) {

		}
		public TestCanonActor(Vector2 pos, float r) : base(pos, r) {

		}
		public override void Initialize() {
			canonSprite = new AnimatedSprite(100);
			widthHeight = new Vector2(Game1.graphics.GraphicsDevice.Viewport.Width, Game1.graphics.GraphicsDevice.Viewport.Height);
			position = new Vector2(widthHeight.X / 2, widthHeight.Y / 2);
		}
		public override void Update(GameTime gameTime) {
			double delta = gameTime.ElapsedGameTime.TotalSeconds;
			Vector2 playPos = TestPlayerActor.player.GetCenterPosition();
			rotation = (float) (Math.Atan2(playPos.Y - position.Y, playPos.X - position.X));
			canonMuzzle = new Vector2(position.X + (float) Math.Cos(rotation) * 64.0f, position.Y + (float) Math.Sin(rotation) * 128.0f);
			if(lastTripleShot < gameTime.TotalGameTime.TotalSeconds) {
				Game1.mainGame.AddActor(new TestEnemyActor(canonMuzzle, (float) (rotation - Math.PI / 8), 64.0f * 13));
				Game1.mainGame.AddActor(new TestEnemyActor(canonMuzzle, rotation, 128.0f * 2));
				Game1.mainGame.AddActor(new TestEnemyActor(canonMuzzle, (float) (rotation + Math.PI / 8), 64.0f * 13));
				lastTripleShot = gameTime.TotalGameTime.TotalSeconds + 5;
				canonSprite.SetAnimation("idle");

			} else if (lastDoubleShot < gameTime.TotalGameTime.TotalSeconds) {
				Game1.mainGame.AddActor(new TestEnemyActor(canonMuzzle, (float)(rotation - Math.PI / 3), 64.0f * 11));
				Game1.mainGame.AddActor(new TestEnemyActor(canonMuzzle, (float)(rotation + Math.PI / 3), 64.0f * 11));
				lastDoubleShot = gameTime.TotalGameTime.TotalSeconds + 2;
				canonSprite.SetAnimation("idle");

			} else if (lastSingleShot < gameTime.TotalGameTime.TotalSeconds) {
				Game1.mainGame.AddActor(new TestEnemyActor(canonMuzzle, rotation, 64.0f * 7));
				lastSingleShot = gameTime.TotalGameTime.TotalSeconds + 0.5f;
				canonSprite.SetAnimation("idle");
			}
			float closestShotTime = (float) Math.Min(lastTripleShot, Math.Min(lastSingleShot, lastDoubleShot));
			if (Math.Min(lastTripleShot, Math.Min(lastSingleShot, lastDoubleShot)) - 1.0f/3.0f < gameTime.TotalGameTime.TotalSeconds) {
				canonSprite.SetAnimation("firing");
			}
		}
		public override void Draw(SpriteBatch canvas) {
			canonSprite.DrawCentered(canvas,position, rotation);
		}
		public override Vector2 GetCenterPosition() {
			return GetPosition();
		}
		public override Vector2 GetPosition() {
			return position;
		}
	}
} 