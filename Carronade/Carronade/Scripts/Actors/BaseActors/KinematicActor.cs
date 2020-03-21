using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Carronade {
	//All "interactive" objects in the game will be some form of actor.
	
	public abstract class KinematicActor : Actor {
		protected Vector2 velocity = Vector2.Zero;
		public override void Initialize() {
		}
		public KinematicActor(float x, float y, float r) : base(x, y, r) {

		}
		public KinematicActor(Vector2 pos, float r) : base(pos, r) {

		}
		public void SetXVel(float x) {
			velocity.X = x;
		}
		public void SetYVel(float y) {
			velocity.Y = y;
		}
		public void SetVelocity(float x, float y) {
			SetVelocity(new Vector2(x, y));
		}
		public void SetVelocity(Vector2 vel) {
			velocity = vel;
		}
		public override void LateUpdate(GameTime gameTime) {
			if (gameTime == null)
				throw new Exception("Something went wrong with GameTime");
			Vector2 deltaVel = new Vector2(velocity.X * (float) gameTime.ElapsedGameTime.TotalSeconds, velocity.Y * (float) gameTime.ElapsedGameTime.TotalSeconds);
			position += deltaVel;
			//Console.WriteLine(string.Format("{0} : {1} - {2}", position, velocity, deltaVel));
		}
	}
}