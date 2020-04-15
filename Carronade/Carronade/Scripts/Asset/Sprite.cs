using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Carronade {
	public class Sprite {
		//Sprites refer to loaded in Assets by their ID. Look up an Asset by ID and then refer to it. Makes all Assets copy REFERENCES and not Texture2D data themselves.
		protected int AIDRef = -1;
		protected Texture2D text;
		//Draw layer, so we can organize where sprites appear on the Z axis
		protected float layer = 0;
		//TODO: Implement DrawType so AnimatedSprites are still during game pauses.
		public enum DrawType {REGULAR, ALWAYS_ANIM, ANIM};
		protected DrawType type = DrawType.REGULAR;

		//TODO?: Validate Assets as a given type
		public Sprite(int assetID) {
			Asset asset = Asset.GetAsset(assetID);
			text = asset.GetAssetReference<Texture2D>();
			AIDRef = assetID;
		}
		public Rectangle GetBounds() {
			return text.Bounds;
		}
		public void SetLayer(float depth) {
			layer = depth;
		}
		//If we want more control
		public virtual void DrawRaw(SpriteBatch canvas, Vector2 position, Rectangle clip, float rotation, Vector2 origin) {
			canvas.Draw(text, position, clip, Color.White, rotation, origin, 1.0f, SpriteEffects.None, layer);
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