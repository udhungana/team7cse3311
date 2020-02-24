using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Carronade {
	public class Sprite {
		protected int AIDRef = -1;
		protected Texture2D text;
		protected float layer = 0;
		public enum DrawType {REGULAR, ALWAYS_ANIM, ANIM};
		protected DrawType type = DrawType.REGULAR;
		public Sprite(int assetID) {
			Asset asset = Asset.GetAsset(assetID);
			text = asset.GetAssetReference<Texture2D>();
			AIDRef = assetID;
		}
		public Rectangle GetBounds() {
			return text.Bounds;
		}
		public virtual void DrawRaw(SpriteBatch canvas, Vector2 position, float rotation, Vector2 origin) {
			canvas.Draw(text, position, null, Color.White, rotation, origin, 1.0f, SpriteEffects.None, layer);
		}
		//We shift the origin to the center and then compensate for the change so we can rotate about the center.
		public virtual void Draw(SpriteBatch canvas, Vector2 position, float rotation) {
			Vector2 center = new Vector2(text.Width / 2, text.Height / 2);
			canvas.Draw(text, position + center, null, Color.White, rotation, center, 1.0f, SpriteEffects.None, layer);
		}
		public virtual void DrawCentered(SpriteBatch canvas, Vector2 position, float rotation) {
			Vector2 center = new Vector2(text.Width / 2, text.Height / 2);
			canvas.Draw(text, position, null, Color.White, rotation, center, 1.0f, SpriteEffects.None, layer);
		}
	}
}