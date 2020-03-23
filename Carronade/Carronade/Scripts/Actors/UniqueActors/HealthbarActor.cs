using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Carronade {
	//All "interactive" objects in the game will be some form of actor.

	public class HealthbarActor : Actor {
		private Sprite backSprite;
		private Sprite goldBack;
		private Sprite frontSprite;
		private Vector2 barOffset;
		public HealthbarActor(float x, float y, float r) : base(x, y, r) {

		}
		public HealthbarActor(Vector2 pos, float r) : base(pos, r) {

		}
		public override void Initialize() {
			backSprite = new Sprite(200);
			frontSprite = new Sprite(201);
			goldBack = new Sprite(202);
			position = new Vector2(Game1.mainGame.ViewPort.Width / 2, Game1.mainGame.ViewPort.Height / 2);
			position -= new Vector2(80, 8);
			barOffset = new Vector2(2, 2);
		}
		public override void Update(GameTime gameTime) {
		}
		public override void LateUpdate(GameTime gameTime) {
		}
		public override void Draw(SpriteBatch canvas) {
			PlayerActor player;
			if (Game1.mainGame.player != null) {
				player = Game1.mainGame.player;
				if(player.invuln)
					goldBack.Draw(canvas, position, 0.0f);
				else
					backSprite.Draw(canvas, position, 0.0f);
				Rectangle crop;
				float totalSize = 156;
				totalSize *= player.GetPercentMaxHealth();
				crop = new Rectangle(0, 0, (int) totalSize, 12);
				frontSprite.DrawRaw(canvas, position + barOffset, crop, 0.0f, Vector2.Zero);
			} else {
				backSprite.Draw(canvas, position, 0.0f);
			}
		}
		public override Vector2 GetCenterPosition() {
			return GetPosition();
		}
		public override Vector2 GetPosition() {
			return position;
		}
		public override Rectangle GetBounds() {
			return Rectangle.Empty;
		}
	}
} 