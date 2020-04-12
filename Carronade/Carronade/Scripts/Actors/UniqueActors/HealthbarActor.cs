using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Carronade {
	//All "interactive" objects in the game will be some form of actor.

	public class HealthbarActor : Actor {
		private Sprite backSprite;
		private Sprite goldBack;
		private Sprite frontSprite;
		private Sprite[] tubeSprites = new Sprite[10];
		private Vector2 barOffset;
		private Vector2 tubeOffset;
		private int[] slotValues = new int[8];
		public HealthbarActor(float x, float y, float r) : base(x, y, r) {

		}
		public HealthbarActor(Vector2 pos, float r) : base(pos, r) {

		}
		public override void Initialize() {
			backSprite = new Sprite(200);
			frontSprite = new Sprite(201);
			goldBack = new Sprite(202);
			for (int i = 0; i < 10; i++)
				tubeSprites[i] = new Sprite(220 + i);
			position = new Vector2(Game1.mainGame.ViewPort.Width / 2, Game1.mainGame.ViewPort.Height / 2);
			position -= new Vector2(80, 8);
			barOffset = new Vector2(2, 2);
			tubeOffset = new Vector2(16, -20);
		}
		public override void Update(GameTime gameTime) {
			int score = GameRoom.gameRoom.Score;
			int divisor = 10000000;
			int modulator = 100000000;
			for (int i = 0; i < 8; i++) {
				int workingScore = score;
				workingScore %= modulator;
				slotValues[i] = workingScore / divisor;
				divisor /= 10;
				modulator /= 10;
			}
		}
		public override void LateUpdate(GameTime gameTime) {
		}
		public override void Draw(SpriteBatch canvas) {
			PlayerActor player;
			if (GameRoom.gameRoom.player != null) {
				player = GameRoom.gameRoom.player;
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
			Vector2 subOffset = new Vector2(0, 0);
			for (int i = 0; i < 8; i++) {
				tubeSprites[slotValues[i]].Draw(canvas, position + tubeOffset + subOffset, 0.0f);
				subOffset.X += 16;
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