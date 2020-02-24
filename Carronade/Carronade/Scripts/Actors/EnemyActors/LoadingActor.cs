using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Carronade {
	//All "interactive" objects in the game will be some form of actor.
	
	public class LoadingActor : Actor {
		private Vector2 widthHeight;
		private Sprite loadingText;
		private Sprite loadingImage;
		private AnimatedSprite testAnim;
		private int spacer = 0;
		protected LoadingActor() {
			Initialize();
		}
		public LoadingActor(float x, float y, float r) : this() {
			position = new Vector2(x, y);
			rotation = r;
		}
		//Initialize is called whenever the object is created or instanced in the game.
		public override void Initialize() {
			widthHeight = new Vector2(Game1.graphics.GraphicsDevice.Viewport.Width, Game1.graphics.GraphicsDevice.Viewport.Height);
			rotation = 0;
			loadingText = new Sprite(0);
			loadingImage = new Sprite(1);
			testAnim = new AnimatedSprite(2);
			testAnim.SetFrame(0);
		}
		public override void Update(GameTime gameTime) {
			double delta = gameTime.ElapsedGameTime.TotalSeconds;
			rotation = (rotation + 360 * (float) (System.Math.PI / 180d * delta)) % 360.0f;
		}
		//All actors will have a draw function but not every actor will necessarily use this.
		public override void Draw(SpriteBatch canvas) {
			Vector2 imgPos = new Vector2(widthHeight.X - loadingImage.GetBounds().Width, widthHeight.Y - loadingText.GetBounds().Height);
			Vector2 textPos = new Vector2(imgPos.X - loadingText.GetBounds().Width, widthHeight.Y - loadingText.GetBounds().Height);
			loadingText.Draw(canvas, textPos, 0);
			loadingImage.Draw(canvas, imgPos, rotation);
			testAnim.Draw(canvas, Vector2.Zero, 0);
		}
		public override Vector2 GetCenterPosition() {
			return GetPosition();
		}
		public override Vector2 GetPosition() {
			return position;
		}
	}
}