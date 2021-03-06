﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework.Input;
namespace Carronade {
	//All "interactive" objects in the game will be some form of actor.

	public class TestCanonActor : KinematicActor {
		private Vector2 widthHeight;

		private AnimatedSprite canonSprite;
		private Vector2 aim = Vector2.One;
		private Vector2 canonMuzzle = Vector2.Zero;
		//The way timings work in this game is based off when an action LAST occured and checking the differences
		private double lastSingleShot = -1.0f;
		private double lastDoubleShot = -1.0f;
		private double lastTripleShot = -1.0f;
		private double lastPowerUp = -1.0f;
		private Random rand = new Random();
		public TestCanonActor(float x, float y, float r) : base(x, y, r) {

		}
		public TestCanonActor(Vector2 pos, float r) : base(pos, r) {

		}
		public override void Initialize() {
			canonSprite = new AnimatedSprite(100);
			canonSprite.SetLayer(0.5f);
		}
		public override void Update(GameTime gameTime) {
			//Sets the starting times.
			double curTime = gameTime.TotalGameTime.TotalSeconds;
			if (lastSingleShot < 0) {
				lastSingleShot = gameTime.TotalGameTime.TotalSeconds + 5.0f;
				lastDoubleShot = gameTime.TotalGameTime.TotalSeconds + 7.0f;
				lastTripleShot = gameTime.TotalGameTime.TotalSeconds + 10.0f;
				lastPowerUp = gameTime.TotalGameTime.TotalSeconds + 12.0f;
			}
			//Aims at the player. Always.
			Vector2 playPos = GameRoom.gameRoom.player.GetCenterPosition();
			rotation = (float) (Math.Atan2(playPos.Y - position.Y, playPos.X - position.X));
			canonMuzzle = new Vector2(position.X + (float) Math.Cos(rotation) * 128.0f, position.Y + (float) Math.Sin(rotation) * 128.0f);
			//Every triple shot, fire off 3 kinds of enemies.
			if(lastTripleShot < curTime) {
				GameRoom.gameRoom.AddActor(new TurnShot(canonMuzzle, (float) (rotation - Math.PI / 8), 64.0f * 8));
				GameRoom.gameRoom.AddActor(new AcceleratedShot(canonMuzzle, rotation, 128.0f * 2));
				GameRoom.gameRoom.AddActor(new MineShot(canonMuzzle, rotation, 64.0f * 9));
				GameRoom.gameRoom.AddActor(new TurnShot(canonMuzzle, (float) (rotation + Math.PI / 8), 64.0f * 8));
				lastTripleShot = gameTime.TotalGameTime.TotalSeconds + 5;
				canonSprite.SetAnimation("idle");
				Sound.GetSound(1001).CreateInstance().Play();
				//Every double, fire off 2
			} else if (lastDoubleShot < curTime) {
				GameRoom.gameRoom.AddActor(new AcceleratedShot(canonMuzzle, (float)(rotation - Math.PI / 3), 64.0f * 2));
				GameRoom.gameRoom.AddActor(new AcceleratedShot(canonMuzzle, (float)(rotation + Math.PI / 3), 64.0f * 2));
				GameRoom.gameRoom.AddActor(new MineShot(canonMuzzle, rotation, 64.0f * 7));
				lastDoubleShot = gameTime.TotalGameTime.TotalSeconds + 2;
				canonSprite.SetAnimation("idle");
				Sound.GetSound(1001).CreateInstance().Play();
				//Every single, fire off one.
			} else if (lastSingleShot < curTime) {
				GameRoom.gameRoom.AddActor(new StraightShot(canonMuzzle, rotation, 64.0f * 7));
				lastSingleShot = gameTime.TotalGameTime.TotalSeconds + 0.5f;
				canonSprite.SetAnimation("idle");
				Sound.GetSound(1001).CreateInstance().Play();
			}
			//Powerups are a coinflip for now
			if (lastPowerUp < curTime) {
				double chance = rand.NextDouble();
				if (chance < 0.5) {
					GameRoom.gameRoom.AddActor(new HealthPowerUpActor(canonMuzzle, rotation, 64.0f * 4));
				} else {
					GameRoom.gameRoom.AddActor(new InvulnPowerUpActor(canonMuzzle, rotation, 64.0f * 4));
				}
				lastPowerUp = curTime + 6.0f;
				canonSprite.SetAnimation("idle");
				Sound.GetSound(1002).CreateInstance().Play();
			}
			float closestShotTime = (float) Math.Min(lastTripleShot, Math.Min(lastSingleShot, lastDoubleShot));
			//Plays an animation
			if (Math.Min(lastTripleShot, Math.Min(lastSingleShot, lastDoubleShot)) - 1.0f/3.0f < curTime) {
				canonSprite.SetAnimation("firing");
			}
		}
		public override void Draw(SpriteBatch canvas) {
			canonSprite.DrawCentered(canvas, position, rotation);
		}
		public override void DrawStill(SpriteBatch canvas) {
			canonSprite.DrawCenteredStill(canvas, position, rotation);
		}
		public override Vector2 GetCenterPosition() {
			return GetPosition();
		}
		public override Vector2 GetPosition() {
			return position;
		}
		public override Rectangle GetBounds() {
			return Rectangle.Empty;
		}
	}
} 