using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Carronade
{
	//All "interactive" objects in the game will be some form of actor.

	public abstract class PowerupActor : KinematicActor
	{
		public bool picked { get; private set; } = false;
		protected double expirey = -1;
		protected PlayerActor playerRef;
		public PowerupActor(float x, float y, float r) : base(x, y, r) {
		}
		public PowerupActor(Vector2 pos, float r) : base(pos, r) {
		}
		public override void Update(GameTime gameTime) {
			if(picked && gameTime.TotalGameTime.TotalSeconds > expirey) {
				PickupEnd();
				Game1.mainGame.RemoveActor(this);
			}
		}
		public void SetDuration(double length) {
			expirey = length;
		}
		public virtual void PickupEnd() {
		}
		public virtual void OnPickup(PlayerActor player, GameTime gameTime) {
			playerRef = player;
			picked = true;
			expirey += gameTime.TotalGameTime.TotalSeconds;
		}
	}
}