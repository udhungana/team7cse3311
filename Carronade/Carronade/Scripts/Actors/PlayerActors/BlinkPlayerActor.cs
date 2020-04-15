using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Carronade {
	//All "interactive" objects in the game will be some form of actor.

	public class BlinkPlayerActor : PlayerActor {
		private Sprite playerSprite;
		private float baseSpeed = 640.0f;
		private double blinkCooldown = -1;
		private float lastRot = 0.0f;
		public BlinkPlayerActor(float x, float y, float r) : base(x, y, r) {

		}
		public BlinkPlayerActor(Vector2 pos, float r) : base(pos, r) {

		}
		public override void Initialize() {
			base.Initialize();
			playerSprite = new Sprite(501);
			GameRoom.gameRoom.player = this;
			SetMaxDamage(30);
		}
		public override void Update(GameTime gameTime) {
			KeyboardState state = Keyboard.GetState();
			double curTime = gameTime.TotalGameTime.TotalSeconds;
			if(blinkCooldown == -1) {
				blinkCooldown = curTime;
			}
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
			if (vel != Vector2.Zero)
				vel.Normalize();
			//jumps forward.
			if(vel != Vector2.Zero) {
				rotation = (float) Math.Atan2(vel.Y, vel.X);
				if (state.IsKeyDown(Keys.LeftShift)) {
					if (curTime > blinkCooldown) {
						Vector2 facing = new Vector2(vel.X * 128, vel.Y * 128);
						position += facing;
						blinkCooldown = curTime + 5.0f;
					}
				}
			} else {
				rotation = lastRot;
			}
			SetVelocity(vel.X * baseSpeed, vel.Y * baseSpeed);
			lastRot = rotation;
		}
		public override void Draw(SpriteBatch canvas) {
			playerSprite.Draw(canvas, position, rotation);
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