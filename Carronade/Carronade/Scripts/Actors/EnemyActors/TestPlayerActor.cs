using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Carronade {
	//All "interactive" objects in the game will be some form of actor.

	public class TestPlayerActor : PlayerActor {
		public static TestPlayerActor player;
		private Sprite playerSprite;
		private float baseSpeed = 512.0f;
		public TestPlayerActor(float x, float y, float r) : base(x, y, r) {

		}
		public TestPlayerActor(Vector2 pos, float r) : base(pos, r) {

		}
		public override void Initialize() {
			base.Initialize();
			playerSprite = new Sprite(4);
			if (player == null)
				player = this;
		}
		public override void Update(GameTime gameTime) {
			KeyboardState state = Keyboard.GetState();
			int buildX = 0;
			int buildY = 0;
			if (state.IsKeyDown(Keys.W))
				buildY -= 1;
			if (state.IsKeyDown(Keys.S))
				buildY += 1;
			if (state.IsKeyDown(Keys.A))
				buildX -= 1;
			if (state.IsKeyDown(Keys.D))
				buildX += 1;
			Vector2 vel = new Vector2(buildX, buildY);
			if(vel != Vector2.Zero)
				vel.Normalize();
			if(vel != Vector2.Zero) {
				rotation = ((float) Math.Atan2(vel.Y, vel.X)) * 180 / (float) Math.PI;
			} else {
				rotation = 0;
			}
			SetVelocity(vel.X * baseSpeed, vel.Y * baseSpeed);
		}
		public override void Draw(SpriteBatch canvas) {
			playerSprite.Draw(canvas, position, (float) Math.PI / 180 * rotation);
		}
		public override Vector2 GetCenterPosition() {
			return GetPosition() + new Vector2(playerSprite.GetBounds().Width/2, playerSprite.GetBounds().Height / 2);
		}
		public override Vector2 GetPosition() {
			return position;
		}
		public override Rectangle GetBounds() {
			return playerSprite.GetBounds();
		}
	}
}