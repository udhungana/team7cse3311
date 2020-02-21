using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Carronade {
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game {
		public static GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		XMLAssetBuilder builder;
		public static Game1 mainGame;
		public List<Sprite> drawSprites;
		public List<Actor> actors;
		public bool unpaused = true;
		public Game1() {
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			drawSprites = new List<Sprite>();
			actors = new List<Actor>();
			builder = new XMLAssetBuilder(Content);
			if (mainGame == null) {
				mainGame = this;
				//It's going to upset a couple of people but for the sake of playing animations as expected 60FPS is the way to go
				IsFixedTimeStep = true;
			}
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize() {
			// TODO: Add your initialization logic here
			base.Initialize();
			graphics.PreferredBackBufferWidth = 80 * 16;
			graphics.PreferredBackBufferHeight = 60 * 16;
			graphics.ApplyChanges();
			actors.Add(new Loading_Actor(0,0,0));
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent() {
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			// TODO: use this.Content to load your game content here
			builder.LoadAssets("loadingscreen");
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// game-specific content.
		/// </summary>
		protected override void UnloadContent() {
			// TODO: Unload any non ContentManager content here
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime) {
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();
			if(unpaused) {
				foreach (var actor in actors) {
					actor.Update(gameTime);
				}
			}
			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime) {
			GraphicsDevice.Clear(Color.CornflowerBlue);
			spriteBatch.Begin();
			foreach (var actor in actors) {
				actor.Draw(spriteBatch);
			}
			spriteBatch.End();
			base.Draw(gameTime);
		}
	}
}
