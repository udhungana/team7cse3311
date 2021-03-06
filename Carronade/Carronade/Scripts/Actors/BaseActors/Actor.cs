﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Carronade {
	//All "interactive" objects in the game will be some form of actor.

	public abstract class Actor {
		//Decreed by the design of the project, all Actors have these fundamental laws.
		protected Vector2 position;
		protected float rotation;
		protected float layer;
		protected bool enabled = false;
		public long ID { get; private set; } = -1;
		public static long totalIDs { get; private set; } = 0;
		protected Actor() {
			//System.Console.WriteLine("Actor Created at {0} r:{1}", position, rotation);
		}
		public Actor(float x, float y, float r) {
			position = new Vector2(x, y);
			rotation = r;
			SetID();
		}
		public Actor(Vector2 pos, float r) {
			position = pos;
			rotation = r;
			SetID();
		}
		private void SetID() {
			ID = totalIDs;
			totalIDs++;
		}
		public void Disable() {
			enabled = false;
		}
		public void Enable() {
			enabled = true;
		}
		public bool IsEnabled() {
			return enabled;
		}
		//Initialize is called whenever the object is created or instanced in the game.
		public abstract void Initialize();
		//Actors have the ability to update (their position for instance) or recieve input
		public abstract void Update(GameTime gameTime);
		//After the lights go out we need to update positions.
		public abstract void LateUpdate(GameTime gameTime);
		//All actors will have a draw function but not every actor will necessarily use this.
		public abstract void Draw(SpriteBatch canvas);
		//For most still sprites it'll just be the same as the base.
		public virtual void DrawStill(SpriteBatch canvas) {
			Draw(canvas);
		}
		public abstract Vector2 GetPosition();
		public abstract Rectangle GetBounds();
		public abstract Vector2 GetCenterPosition();
	}
}
 