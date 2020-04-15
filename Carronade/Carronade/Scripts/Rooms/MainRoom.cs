using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
namespace Carronade {

	public class MainRoom : BaseRoom {
		private Sprite background;
		private Sprite logo;
		private Sprite play;
		private Vector2 offset;
		private Vector2 playOffset;
		private MouseState lastState;
		public MainRoom() : base() {
			Initialize();
		}
		//Initialize is called whenever the object is created or instanced in the game.
		public override void Initialize() {
			base.Initialize();
			background = new Sprite(0);
			logo = new Sprite(1);
			play = new Sprite(2);
			offset = new Vector2(Game1.mainGame.ViewPort.Width/2, 100);
			playOffset = new Vector2(Game1.mainGame.ViewPort.Width / 2 - play.GetBounds().Width/2, 256);
		}
		//Actors have the ability to update (their position for instance) or recieve input
		public override void Update(GameTime gameTime) {
			//Mouse should only be visible on the Main Menu.
			if(!Game1.mainGame.IsMouseVisible)
				Game1.mainGame.IsMouseVisible = true;
			if (Keyboard.GetState().IsKeyDown(Keys.Escape))
				Game1.mainGame.Exit();
			MouseState mouseClick = Mouse.GetState();
			//TODO: Create a proper button UI class (Actor?)
			if (Mouse.GetState().X >= playOffset.X && Mouse.GetState().Y >= playOffset.Y) {
				if(Mouse.GetState().X <= playOffset.X + play.GetBounds().Width && Mouse.GetState().Y <= playOffset.Y + play.GetBounds().Height) {
					if (mouseClick.LeftButton == ButtonState.Pressed) {
						Game1.mainGame.SwitchRooms("SelectionRoom");
					}
				}
			}
			lastState = mouseClick;
		}
		public override void LateUpdate(GameTime gameTime) {
		}
		//Nothing much to draw.
		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
			spriteBatch.Begin();
			background.Draw(spriteBatch, Vector2.Zero, 0.0f);
			logo.DrawCentered(spriteBatch, offset, 0.0f);
			play.Draw(spriteBatch, playOffset, 0.0f);
			spriteBatch.End();
		}
	}
}
 