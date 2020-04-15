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
		//Keep track of the player who touched it. Who knows, two player shenanigans future proofing.
		protected PlayerActor playerRef;
		public PowerupActor(float x, float y, float r) : base(x, y, r) {
		}
		public PowerupActor(Vector2 pos, float r) : base(pos, r) {
		}
		//All powerups have a limited duration and must finish after their expirey.
		public override void Update(GameTime gameTime) {
			if(picked && gameTime.TotalGameTime.TotalSeconds > expirey) {
				PickupEnd();
				GameRoom.gameRoom.RemoveActor(this);
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