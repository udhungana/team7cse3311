using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Carronade {
	//All "interactive" objects in the game will be some form of actor.

	public class InvulnPowerUpActor : PowerupActor {
		private Sprite shieldSprite;
		private float baseSpeed = 16.0f;
		public InvulnPowerUpActor(float x, float y, float r, float spd) : base(x, y, r) {
			baseSpeed = spd;	
		}
		public InvulnPowerUpActor(Vector2 pos, float r, float spd) : base(pos, r) {
			baseSpeed = spd;
		}
		public override void Initialize() {
			shieldSprite = new Sprite(301);
			SetDuration(3);
			Vector2 facing = new Vector2(((float) Math.Cos(rotation)) * baseSpeed, ((float) Math.Sin(rotation)) * baseSpeed);
			SetVelocity(facing);
		}
		public override void OnPickup(PlayerActor player, GameTime gameTime) {
			base.OnPickup(player, gameTime);
			playerRef.SetInvuln(true);
		}
		public override void PickupEnd() {
			playerRef.SetInvuln(false);
			base.PickupEnd();
		}
		public override void Draw(SpriteBatch canvas) {
			if (!picked)
				shieldSprite.DrawCentered(canvas, position, 0.0f);
		}
		public override Vector2 GetCenterPosition() {
			return GetPosition() + new Vector2(shieldSprite.GetBounds().Width / 2, shieldSprite.GetBounds().Height / 2);
		}
		public override Vector2 GetPosition() {
			return position;
		}
		public override Rectangle GetBounds() {
			return shieldSprite.GetBounds();
		}
	}
}