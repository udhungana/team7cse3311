﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Carronade {
	//All "interactive" objects in the game will be some form of actor.

	public class HunkerPlayerActor : PlayerActor {
		public static BasePlayerActor player;
		private Sprite playerSprite;
		private float baseSpeed = 448.0f;
		private double hunkerEnd = -1;
		private double hunkerStart = -1;
		private float lastRot = 0.0f;
		public HunkerPlayerActor(float x, float y, float r) : base(x, y, r) {

		}
		public HunkerPlayerActor(Vector2 pos, float r) : base(pos, r) {

		}
		public override void Initialize() {
			base.Initialize();
			playerSprite = new Sprite(502);
			GameRoom.gameRoom.player = this;
			SetMaxDamage(50);
		}
		public override void Update(GameTime gameTime) {
			double curTime = gameTime.TotalGameTime.TotalSeconds;
			KeyboardState state = Keyboard.GetState();
			if(hunkerEnd == -1) {
				hunkerStart = curTime + - 1.0d;
				hunkerEnd = curTime;
			}
			//the power of WASD. W and S keys cancel each other out in the same way A and D should.
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

			if (state.IsKeyDown(Keys.LeftShift)) {
				if(curTime < hunkerStart) {
					baseSpeed = 0.0f;
					SetInvuln(true);
				} else if (curTime > hunkerEnd) {
					hunkerStart = curTime + 1.0d;
					hunkerEnd = curTime + 4.0d;
					baseSpeed = 0.0f;
					SetInvuln(true);
				} else {
					baseSpeed = 448.0f;
					SetInvuln(false);
				}
			} else {
				baseSpeed = 448.0f;
				SetInvuln(false);
			}
			Vector2 vel = new Vector2(buildX, buildY);
			if (vel != Vector2.Zero)
				vel.Normalize();
			if (vel != Vector2.Zero) {
				rotation = (float)Math.Atan2(vel.Y, vel.X);
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