using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Carronade {
	//All "interactive" objects in the game will be some form of actor.

	public class HealthPowerUpActor : PowerupActor {
		private Sprite crossSprite;
		private float baseSpeed = 16.0f;
		public HealthPowerUpActor(float x, float y, float r, float spd) : base(x, y, r) {
			baseSpeed = spd;	
		}
		public HealthPowerUpActor(Vector2 pos, float r, float spd) : base(pos, r) {
			baseSpeed = spd;
		}
		public override void Initialize() {
			crossSprite = new Sprite(300);
			SetDuration(0);
			Vector2 facing = new Vector2(((float) Math.Cos(rotation)) * baseSpeed, ((float) Math.Sin(rotation)) * baseSpeed);
			SetVelocity(facing);
		}
		//i need healing
		public override void OnPickup(PlayerActor player, GameTime gameTime) {
			base.OnPickup(player, gameTime);
			playerRef.Heal(20);
		}
		public override void Draw(SpriteBatch canvas) {
			if(!picked)
				crossSprite.DrawCentered(canvas, position, 0.0f);
		}
		public override Vector2 GetCenterPosition() {
			return GetPosition() + new Vector2(crossSprite.GetBounds().Width / 2, crossSprite.GetBounds().Height / 2);
		}
		public override Vector2 GetPosition() {
			return position;
		}
		public override Rectangle GetBounds() {
			return crossSprite.GetBounds();
		}
	}
}