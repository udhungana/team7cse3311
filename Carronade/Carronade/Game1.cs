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
		SpriteBatch spriteBatch;

		//Globally accessible fields for use in a singleton pattern.
		public static GraphicsDeviceManager graphics { get; private set; }
		public static Game1 mainGame { get; private set; }

		public KeyboardState prevState;
		private BaseRoom activeRoom;
		private Dictionary<string, BaseRoom> rooms;

		XMLAssetBuilder builder;
		public Viewport ViewPort { get {
				return graphics.GraphicsDevice.Viewport;
			} }

		public Game1() {
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			builder = new XMLAssetBuilder(Content);
			if (mainGame == null) {
				mainGame = this;
				//It's going to upset a couple of people but for the sake of playing animations as expected 60FPS is the way to go
				IsFixedTimeStep = true;
				activeRoom = null;
				rooms = new Dictionary<string, BaseRoom>();
			}
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize() {
			base.Initialize();
			//Set the resolution
			graphics.PreferredBackBufferWidth = 80 * 16;
			graphics.PreferredBackBufferHeight = 60 * 16;
			graphics.ApplyChanges();
			//Give the pixel art sharper edges
			GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
			//Create all the rooms for room transitioning.
			rooms.Add("GameRoom", new GameRoom());
			rooms.Add("MainRoom", new MainRoom());
			rooms.Add("SelectionRoom", new SelectionRoom());
			//Start at the main menu.
			activeRoom = rooms["MainRoom"];
		}
		//The following function names make the intent and functionality of a room self-explanatory.
		public BaseRoom GetActiveRoom() {
			return activeRoom;
		}
		public BaseRoom GetRoom(string room) {
			if (rooms.ContainsKey(room)) {
				return rooms[room];
			}
			return null;
		}
		public void SwitchRooms(string room) {
			if(rooms.ContainsKey(room)) {
				activeRoom = rooms[room];
			}
		}
		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent() {
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);
			// Loads each category of assets for the sake of organization. If there's a breakdown in one file, it's easier to pinpoint
			builder.LoadAssets("loadingscreen");
			builder.LoadAssets("players");
			builder.LoadAssets("enemies");
			builder.LoadAssets("canon");
			builder.LoadAssets("healthbar");
			builder.LoadAssets("powerups");
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
			//Only one room can be actively updating and drawing at a time.
			if (activeRoom != null)
				activeRoom.Update(gameTime);
			//For keypresses between rooms.
			prevState = Keyboard.GetState();
			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime) {
			if (activeRoom != null)
				activeRoom.Draw(spriteBatch, gameTime);
		}
	}
}
