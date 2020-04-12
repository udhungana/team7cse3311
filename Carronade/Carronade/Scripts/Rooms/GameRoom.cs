using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
namespace Carronade {

	public class GameRoom : BaseRoom {
		public static GameRoom gameRoom { get; private set; }

		public PlayerActor player;
		private KeyboardState previousState;
		private bool unpaused = true;

		private Sprite background;
		public int Score { get; private set; }
		public double TimeStart { get; private set; } = -1;
		public GameRoom() : base() {
			Initialize();
		}
		//Initialize is called whenever the object is created or instanced in the game.
		public override void Initialize() {
			base.Initialize();
			background = new Sprite(0);
			//actors.Add(new LoadingActor(0,0,0));
			AddActor(new BasePlayerActor(600, 600, 0));
			AddActor(new TestCanonActor(0, 0, 0));
			AddActor(new HealthbarActor(0, 0, 0));
			if (gameRoom == null) {
				gameRoom = this;
			}
		}
		public void IncrementScore(int amount) {
			Score += amount;
		}
		public void Reset() {
			foreach (var actor in actors) {
				actor.Disable();
				RemoveActor(actor);
			}
			AddActor(new BlinkPlayerActor(600, 600, 0));
			AddActor(new TestCanonActor(0, 0, 0));
			AddActor(new HealthbarActor(0, 0, 0));
			Score = 0;
			TimeStart = -1;
		}
		//Actors have the ability to update (their position for instance) or recieve input
		public override void Update(GameTime gameTime) {
			if (Game1.mainGame.IsMouseVisible)
				Game1.mainGame.IsMouseVisible = false;
			if (TimeStart < 0)
				TimeStart = gameTime.TotalGameTime.TotalSeconds;
			KeyboardState state = Keyboard.GetState();
			// If they hit esc, exit
			if (state.IsKeyDown(Keys.P))
				Game1.mainGame.SwitchRooms("MainRoom");
			if (state.IsKeyDown(Keys.R) & !previousState.IsKeyDown(
				Keys.R)) {
				Reset();
			}
			if (actorQueueUpdated) {
				foreach (var actor in actorDeleteQueue) {
					actors.Remove(actor);
				}
				foreach (var actor in actorAddQueue) {
					actors.Add(actor);
					actor.Initialize();
					actor.Enable();
				}
				actorAddQueue.Clear();
				actorDeleteQueue.Clear();
				actorQueueUpdated = false;
			}
			if (unpaused) {
				if (gameTime != null) {
					foreach (var actor in actors) {
						if (actor.IsEnabled())
							actor.Update(gameTime);
					}
					LateUpdate(gameTime);
				}
			}
			previousState = state;
			double sTime = Math.Floor(TimeStart * 100) / 100;
			double eTime = Math.Floor(gameTime.TotalGameTime.TotalSeconds * 100) / 100;
			double dTime = eTime - sTime;
			if (dTime % 1.0d == 0.0d)
				Score += (int)(dTime);

		}
		//After the lights go out we need to update positions.
		public override void LateUpdate(GameTime gameTime) {
			foreach (var actor in actors) {
				if (actor.IsEnabled())
					actor.LateUpdate(gameTime);
			}
		}
		//All actors will have a draw function but not every actor will necessarily use this.
		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
			spriteBatch.Begin();
			background.Draw(spriteBatch, Vector2.Zero, 0);
			foreach (var actor in actors) {
				if (actor.IsEnabled())
					actor.Draw(spriteBatch);
			}
			spriteBatch.End();
		}
	}
}
 