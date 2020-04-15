using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
namespace Carronade {

	public class SelectionRoom : BaseRoom {
		private Sprite background;
		public SelectionRoom() : base() {
			Initialize();
		}
		//Initialize is called whenever the object is created or instanced in the game.
		public override void Initialize() {
			base.Initialize();
			background = new Sprite(0);
		}
		public override void Update(GameTime gameTime) {
			BaseRoom room = Game1.mainGame.GetRoom("GameRoom");
			if (room.GetType().Equals(typeof(GameRoom))) {
				GameRoom game = (GameRoom) room;
				KeyboardState state = Keyboard.GetState();

				//Selection Menu Logic. 1-3 for selecting a playertype
				if (state.IsKeyDown(Keys.D1))
					game.SetPlayerType(typeof(BasePlayerActor));
				if (state.IsKeyDown(Keys.D2))
					game.SetPlayerType(typeof(BlinkPlayerActor));
				if (state.IsKeyDown(Keys.D3))
					game.SetPlayerType(typeof(HunkerPlayerActor));
				//Q or W for selecting an Arena and going into the Game Room
				if (state.IsKeyDown(Keys.Q)) {
					game.SetBuild(0);
					game.Reset();
					Game1.mainGame.SwitchRooms("GameRoom");
				}
				if (state.IsKeyDown(Keys.W)) {
					game.SetBuild(1);
					game.Reset();
					Game1.mainGame.SwitchRooms("GameRoom");
				}
				//Consume the keyboard update just incase there's a room transition.
				Game1.mainGame.prevState = state;
			}
		}
		public override void LateUpdate(GameTime gameTime) {
		}
		//There's nothing to draw. Yet.
		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
			spriteBatch.Begin();
			background.Draw(spriteBatch, Vector2.Zero, 0.0f);
			spriteBatch.End();
		}
	}
}
 