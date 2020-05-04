using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Carronade {
	//All "interactive" objects in the game will be some form of actor.

	public class UIButton : UIElement {
		//Decreed by the design of the project, all Actors have these fundamental laws.
		private Sprite currentSprite;
		private Sprite mainSprite;
		private MouseState lastState;
		public UIButton(int sprite, Action list=null) : base(list) {
			position = Vector2.Zero;
			SetSprite(sprite);
		}
		public UIButton(float x, float y, int sprite, Action list=null) : base(x, y, list) {
			SetSprite(sprite);
		}
		public UIButton(Vector2 pos, int sprite, Action list=null) : base(pos, list) {
			SetSprite(sprite);
		}
		public void SetPosition(Vector2 pos) {
			position = pos;
		}
		public void SetSprite(int assetID) {
			mainSprite = new Sprite(assetID);
			currentSprite = mainSprite;
		}
		public override void Update(GameTime gameTime) {
			MouseState mouseClick = Mouse.GetState();
			Vector2 ul = GetUpperLeft();
			Rectangle bound = GetBounds();
			//TODO: Create a proper button UI class (Actor?)
			if (Mouse.GetState().X >= ul.X && Mouse.GetState().Y >= ul.Y) {
				if (Mouse.GetState().X <= ul.X + bound.Width && Mouse.GetState().Y <= ul.Y + bound.Height) {
					if (mouseClick.LeftButton == ButtonState.Pressed && lastState.LeftButton != ButtonState.Pressed) {
						if(OnClick.GetInvocationList().Length > 0) {
							OnClick();
						}
					}
				}
			}
			lastState = mouseClick;
		}
		public override Vector2 GetUpperLeft() {
			return position;
		}
		public override Rectangle GetBounds() {
			if(currentSprite != null)
				return currentSprite.GetBounds();
			return Rectangle.Empty;
		}
		public Sprite GetSprite() {
			return currentSprite;
		}
	}
}
 