using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
namespace Carronade {

	public class SelectionRoom : BaseRoom {
		private Sprite background;
		private enum SelectedPlayer {BASE, BLINK, HUNKER};
		private int selection = 0;
		private Sprite displayBack;
		private Sprite playerSprite;
		private Vector2 startSelect;
		private UIButton selectL;
		private UIButton selectR;
		private UIButton select1;
		private UIButton select2;
		public SelectionRoom() : base() {
			Initialize();
		}
		//Sets up the main menu
		public override void Initialize() {
			base.Initialize();
			background = new Sprite(0);
			displayBack = new Sprite(5);
			playerSprite = new Sprite(500);
			startSelect = new Vector2(640 - 80 - 32, 128 + 64 + 6);
			selectL = new UIButton(startSelect, 3, PlayerSelectL);
			Vector2 offset = new Vector2(64 + 16 + 64 + 16, 0);
			selectR = new UIButton(startSelect + offset, 4, PlayerSelectR);
			select1 = new UIButton(new Vector2(320, 640), 6, SelectRoomOne);
			select2 = new UIButton(new Vector2(640, 640), 7, SelectRoomTwo);
		}
		//Swaps between the available players
		public void PlayerSelectR() {
			selection = (++selection) % 3;
		}
		public void PlayerSelectL() {
			selection = (--selection) < 0 ? 2 : selection;
		}
		//Does the actual switching
		public void PlayerSelect(GameRoom game) {
			switch(selection) {
				case 0:
					playerSprite = new Sprite(500);
					game.SetPlayerType(typeof(BasePlayerActor));
					break;
				case 1:
					playerSprite = new Sprite(501);
					game.SetPlayerType(typeof(BlinkPlayerActor));
					break;
				case 2:
					playerSprite = new Sprite(502);
					game.SetPlayerType(typeof(HunkerPlayerActor));
					break;
			}
		}
		//Switches to the first room
		public void SelectRoomOne() {
			BaseRoom room = Game1.mainGame.GetRoom("GameRoom");
			if (room.GetType().Equals(typeof(GameRoom))) {
				GameRoom game = (GameRoom)room;
				game.SetBuild(0);
				game.Reset();
				Game1.mainGame.SwitchRooms("GameRoom");
			}
		}
		//Switches to the last room
		public void SelectRoomTwo() {
			BaseRoom room = Game1.mainGame.GetRoom("GameRoom");
			if (room.GetType().Equals(typeof(GameRoom))) {
				GameRoom game = (GameRoom)room;
				game.SetBuild(1);
				game.Reset();
				Game1.mainGame.SwitchRooms("GameRoom");
			}
		}
		//Updates all the buttons
		public override void Update(GameTime gameTime) {
			BaseRoom room = Game1.mainGame.GetRoom("GameRoom");
			if (room.GetType().Equals(typeof(GameRoom))) {
				GameRoom game = (GameRoom) room;
				KeyboardState state = Keyboard.GetState();
				selectL.Update(gameTime);
				selectR.Update(gameTime);
				PlayerSelect(game);
				select1.Update(gameTime);
				select2.Update(gameTime);
				Game1.mainGame.prevState = state;
			}
		}
		public override void LateUpdate(GameTime gameTime) {
		}
		//Draws all the buttons
		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
			spriteBatch.Begin();
			background.Draw(spriteBatch, Vector2.Zero, 0.0f);
			selectL.GetSprite().Draw(spriteBatch, startSelect, 0.0f);
			displayBack.Draw(spriteBatch, startSelect + new Vector2(80, 0), 0.0f);
			playerSprite.Draw(spriteBatch, startSelect + new Vector2(80 + 16, 16), 0.0f);
			selectR.GetSprite().Draw(spriteBatch, startSelect + new Vector2(160, 0), 0.0f);
			select1.GetSprite().Draw(spriteBatch, new Vector2(320, 640), 0.0f);
			select2.GetSprite().Draw(spriteBatch, new Vector2(640, 640), 0.0f);
			spriteBatch.End();
		}
	}
}
 