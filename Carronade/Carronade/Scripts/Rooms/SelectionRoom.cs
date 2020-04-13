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
				if (state.IsKeyDown(Keys.D1))
					game.SetPlayerType(typeof(BasePlayerActor));
				if (state.IsKeyDown(Keys.D2))
					game.SetPlayerType(typeof(BlinkPlayerActor));
				if (state.IsKeyDown(Keys.D3))
					game.SetPlayerType(typeof(HunkerPlayerActor));
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
				Game1.mainGame.prevState = state;
			}
		}
		//After the lights go out we need to update positions.
		public override void LateUpdate(GameTime gameTime) {
		}
		//All actors will have a draw function but not every actor will necessarily use this.
		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
			spriteBatch.Begin();
			background.Draw(spriteBatch, Vector2.Zero, 0.0f);
			spriteBatch.End();
		}
	}
}
 