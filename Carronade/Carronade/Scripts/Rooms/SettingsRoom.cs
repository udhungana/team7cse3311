using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
namespace Carronade {

	public class SettingsRoom : BaseRoom {
		private Sprite background;
		public int selection { get; private set; }
		public bool muted { get; private set; } = false;
		private MouseState lastState;
		//Adjustments
		private Vector2 startSelect;
		private Vector2 mutepos;
		private Vector2 exitpos;
		//Interactables
		private Sprite display;
		private UIButton selectL;
		private UIButton selectR;
		private UIButton mute;
		private UIButton exit;
		public SettingsRoom(int select, bool mute) : base() {
			selection = select;
			muted = mute;
			Initialize();
		}
		//A bunch of boring setup functionality
		public override void Initialize() {
			base.Initialize();
			background = new Sprite(0);
			UpdateSprite();
			startSelect = new Vector2(640 - 80 - 32, 160 + 32);
			selectL = new UIButton(startSelect, 3, LowerVolume);
			Vector2 offset = new Vector2(64 + 16 + 64 + 16, 0);
			selectR = new UIButton(startSelect + offset, 4, RaiseVolume);
			mutepos = new Vector2(640 - 32 - 64, 160 + 32 + 64 + 64 + 32);
			if(muted) {
				mute = new UIButton(mutepos, 22, UnmuteSound);
			} else {
				mutepos += new Vector2(128, 0);
				mute = new UIButton(mutepos, 21, MuteSound);
			}
			exitpos = new Vector2(640 - 64, 512);
			exit = new UIButton(exitpos, 9, ExitMenu);
			SetVolume();
		}
		//Grabs the previous room and swaps it over
		public void ExitMenu() {
			Game1.mainGame.SwitchRooms(Game1.mainGame.prevRoom);
		}
		//Mute toggle
		public void MuteSound() {
			muted = true;
			mutepos -= new Vector2(128, 0);
			SoundEffect.MasterVolume = 0.0f;
			mute = new UIButton(mutepos, 22, UnmuteSound);
		}
		public void UnmuteSound() {
			muted = false;
			mutepos += new Vector2(128, 0);
			SoundEffect.MasterVolume = (float)selection / 10;
			Sound.GetSound(1000).Play();
			mute = new UIButton(mutepos, 21, MuteSound);
		}
		//Raises and lowers the volume
		public void LowerVolume() {
			selection = (selection -= 1) < 0 ? 10 : selection;
			SetVolume();
			Sound.GetSound(1000).Play();
		}
		public void SetVolume() {
			SoundEffect.MasterVolume = (float)selection / 10;
			UpdateSprite();
		}
		public void RaiseVolume() {
			selection = (selection += 1) > 10 ? 0 : selection;
			SetVolume();
			Sound.GetSound(1000).Play();
		}
		public void SetAudioLevel(int amnt) {
			selection = amnt;
		}
		public void UpdateSprite() {
			display = new Sprite(10 + selection);
		}
		//Gives the buttons life
		public override void Update(GameTime gameTime) {
			if(!muted) {
				selectL.Update(gameTime);
				selectR.Update(gameTime);
			}
			mute.Update(gameTime);
			exit.Update(gameTime);
		}
		public override void LateUpdate(GameTime gameTime) {
		}
		//Draws all the buttons and adjustors
		public override void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
			spriteBatch.Begin();
			background.Draw(spriteBatch, Vector2.Zero, 0.0f);
			if (!muted) {
				selectL.GetSprite().Draw(spriteBatch, startSelect, 0.0f);
				display.Draw(spriteBatch, startSelect + new Vector2(80, 0), 0.0f);
				selectR.GetSprite().Draw(spriteBatch, startSelect + new Vector2(160, 0), 0.0f);
			}
			mute.GetSprite().Draw(spriteBatch, mutepos, 0.0f);
			exit.GetSprite().Draw(spriteBatch, exitpos, 0.0f);
			spriteBatch.End();
		}
	}
}
 