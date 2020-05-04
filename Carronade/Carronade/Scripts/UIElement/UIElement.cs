using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Carronade {
	//All "interactive" objects in the game will be some form of actor.

	public abstract class UIElement {
		//Decreed by the design of the project, all Actors have these fundamental laws.
		protected Vector2 position;
		public long ID { get; private set; } = -1;
		public static long totalIDs { get; private set; } = 0;
		protected UIElement(Action list) {
			AddListener(list);
		}
		public UIElement(float x, float y, Action list) {
			position = new Vector2(x, y);
			SetID();
			AddListener(list);
		}
		public UIElement(Vector2 pos, Action list) {
			position = pos;
			SetID();
			AddListener(list);
		}
		private void SetID() {
			ID = totalIDs;
			totalIDs++;
		}
		public delegate void Action();
		protected Action OnClick;
		public void AddListener(Action AddListener) {
			if(AddListener != null)
				OnClick += AddListener;
		}
		public void RemoveListener(Action RemoveListener) {
			if (RemoveListener != null)
				OnClick += RemoveListener;
		}
		public abstract void Update(GameTime gameTime);
		public abstract Vector2 GetUpperLeft();
		public abstract Rectangle GetBounds();
	}
}
 