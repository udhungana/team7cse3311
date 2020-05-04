using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
namespace Carronade {

	public class MainRoom : BaseRoom {
		private Sprite background;
		private Sprite logo;
		private Vector2 offset;
		private Vector2 playOffset;
		private MouseState lastState;

		private UIButton playButton;
		private UIButton editButton;
		private UIButton exitButton;
		public MainRoom() : base() {
			Initialize();
		}
		//Initialize is called whenever the object is created or instanced in the game.
		public override void Initialize() {
			base.Initialize();
			background = new Sprite(0);
			logo = new Sprite(1);
			offset = new Vector2(Game1.mainGame.ViewPort.Width/2, 100);
			playButton = new UIButton(2, SelectRoom);
			playOffset = new Vector2(Game1.mainGame.ViewPort.Width / 2 - playButton.GetBounds().Width / 2, 256);
			playButton.SetPosition(playOffset);
			editButton = new UIButton(playOffset + new Vector2(0, 128), 8, EditRoom);
			exitButton = new UIButton(playOffset + new Vector2(0, 256), 9, Exit);
		}
		//Actors have the ability to update (their position for instance) or recieve input
		public override void Update(GameTime gameTime) {
			//Mouse should only be visible on the Main Menu.
			if (!Game1.mainGame.IsMouseVisible)
				Game1.mainGame.IsMouseVisible = true;
			if (Keyboard.GetState().IsKeyDown(Keys.Escape))
				Game1.mainGame.ExitGame();
			playButton.Update(gameTime);
			editButton.Update(gameTime);
			exitButton.Update(gameTime);
		}
		//Go to selection room
		public void SelectRoom() {
			Game1.mainGame.SwitchRooms("SelectionRoom");
		}
		//Go to settings room
		public void EditRoom() {
			Game1.mainGame.SwitchRooms("SettingsRoom");
		}
		//Gets out of dodge
		public void Exit() {
			Game1.mainGame.ExitGame();
		}
		public override void LateUpdate(GameTime gameTime) {
		}
		//Nothing much to draw.
		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
			spriteBatch.Begin();
			background.Draw(spriteBatch, Vector2.Zero, 0.0f);
			logo.DrawCentered(spriteBatch, offset, 0.0f);
			playButton.GetSprite().Draw(spriteBatch, playOffset, 0.0f);
			editButton.GetSprite().Draw(spriteBatch, playOffset + new Vector2(0, 128), 0.0f);
			exitButton.GetSprite().Draw(spriteBatch, playOffset + new Vector2(0, 256), 0.0f);
			spriteBatch.End();
		}
	}
}
 