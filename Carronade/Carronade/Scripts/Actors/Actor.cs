using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Carronade {
	//All "interactive" objects in the game will be some form of actor.
	
	public abstract class Actor {
		protected Vector2 position;
		protected float rotation;
		protected float layer;
		protected Actor() {
			Initialize();
		}
		public Actor(float x, float y, float r) : this() {
			position = new Vector2(x, y);
			rotation = r;
		}
		public Actor(Vector2 pos) : this() {
			position = pos;
		}
		//Initialize is called whenever the object is created or instanced in the game.
		public abstract void Initialize();
		//Actors have the ability to update (their position for instance) or recieve input
		public abstract void Update(GameTime gameTime);
		//All actors will have a draw function but not every actor will necessarily use this.
		public abstract void Draw(SpriteBatch canvas);
	}
}
 