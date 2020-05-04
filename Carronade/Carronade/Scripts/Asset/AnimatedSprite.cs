using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Carronade {
	public class AnimatedSprite : Sprite {
		//We don't want our animations to actually run at framerate, so we pentuple the duration a given frame.
		public static readonly int ANIMATION_HANGTIME = 5;
		private Animations anim;
		private int currentFrame = 0;
		private int maxFrames = 0;
		private string currentAnimation = "";
		private Rectangle activeFrame = Rectangle.Empty;
		public AnimatedSprite(int assetID) : base(assetID) {
			anim = (Animations) Asset.GetAsset(assetID);
			SetAnimation(anim.defaultAnim);
		}

		//We reset the animation state for each animation
		public void SetAnimation(string animName) {
			if(!animName.Equals(currentAnimation)) {
				currentFrame = 0;
				currentAnimation = animName;
				maxFrames = anim.GetAnimationLength(animName) * ANIMATION_HANGTIME;
				SetFrame(0);
			}
		}
		public void ResetAnimation() {
			currentFrame = 0;
			SetFrame(0);
		}
		//This let's us loop through animations though that does give an important TODO...
		//TODO: add compatability for animations that aren't intended to be looped.
		public void SetFrame(int frame) {
			if(frame > maxFrames) {
				frame = 0;
			}
			currentFrame = frame;
		}
		//Compensate for the animation dragging out we've done earlier.
		public void GetActiveFrame() {
			activeFrame = anim.GetAnimationBounds(currentAnimation, currentFrame / ANIMATION_HANGTIME);
		}
		//If the game is paused, then we always draw stills and absolutely DO NOT increment the current frame.
		public void DrawStill(SpriteBatch canvas, Vector2 position, float rotation) {
			Vector2 center = new Vector2(text.Width / 2, text.Height / 2);
			GetActiveFrame();
			canvas.Draw(text, position + center, activeFrame, Color.White, rotation, center, 1.0f, SpriteEffects.None, layer);
		}
		public void DrawCenteredStill(SpriteBatch canvas, Vector2 position, float rotation) {
			Vector2 center = new Vector2(activeFrame.Width / 2, activeFrame.Height / 2);
			GetActiveFrame();
			canvas.Draw(text, position, activeFrame, Color.White, rotation, center, 1.0f, SpriteEffects.None, layer);
		}

		//BIG TODO: REDIRECT TO DRAWSTILL DURING PAUSING

		//Draws the current frame and increments the frame counter by one.
		public override void Draw(SpriteBatch canvas, Vector2 position, float rotation) {
			Vector2 center = new Vector2(activeFrame.Width / 2, activeFrame.Height / 2);
			GetActiveFrame();
			canvas.Draw(text, position + center, activeFrame, Color.White, rotation, center, 1.0f, SpriteEffects.None, layer);
			SetFrame(currentFrame + 1);
		}
		//Draws the current frame and increments the frame counter by one.
		public override void DrawCentered(SpriteBatch canvas, Vector2 position, float rotation) {
			Vector2 center = new Vector2(activeFrame.Width/2, activeFrame.Height/2);
			GetActiveFrame();
			canvas.Draw(text, position, activeFrame, Color.White, rotation, center, 1.0f, SpriteEffects.None, layer);
			SetFrame(currentFrame + 1);
		}
	}
}