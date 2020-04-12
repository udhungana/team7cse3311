using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
namespace Carronade {

	public abstract class BaseRoom {
		public List<Actor> actors { get; private set; }
		protected List<Actor> actorAddQueue { get; private set; }
		protected List<Actor> actorDeleteQueue { get; private set; }
		protected bool actorQueueUpdated = false;
		protected BaseRoom() {
		}
		//Initialize is called whenever the object is created or instanced in the game.
		public virtual void Initialize() {
			actors = new List<Actor>();
			actorAddQueue = new List<Actor>();
			actorDeleteQueue = new List<Actor>();
		}
		public void AddActor(Actor act) {
			actorAddQueue.Add(act);
			actorQueueUpdated = true;
		}
		public void RemoveActor(Actor act) {
			actorDeleteQueue.Add(act);
			actorQueueUpdated = true;
			act.Disable();
		}
		//Actors have the ability to update (their position for instance) or recieve input
		public abstract void Update(GameTime gameTime);
		//After the lights go out we need to update positions.
		public abstract void LateUpdate(GameTime gameTime);
		//All actors will have a draw function but not every actor will necessarily use this.
		public abstract void Draw(SpriteBatch canvas, GameTime gametime);
	}
}
 