using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Carronade {
	//All "interactive" objects in the game will be some form of actor.

	public class TurnShot : EnemyActor {
		private Sprite enemySprite;
		private float baseSpeed = 16.0f;
		public TurnShot(float x, float y, float r, float spd) : base(x, y, r) {
			baseSpeed = spd;	
		}
		public TurnShot(Vector2 pos, float r, float spd) : base(pos, r) {
			baseSpeed = spd;
		}
		public override void Initialize() {
			enemySprite = new Sprite(401);
			Vector2 facing = new Vector2(((float) Math.Cos(rotation)) * baseSpeed, ((float) Math.Sin(rotation)) * baseSpeed);
			//Console.WriteLine(string.Format("{0} - {1}:{2}", facing, Math.Cos(Math.PI / 180 * rotation), Math.Sin(Math.PI / 180 * rotation)));
			SetVelocity(facing);
			SetDamage(5);
		}
		//Funny this Enemy type
		public override void Update(GameTime gameTime) {
			Vector2 playPos = GameRoom.gameRoom.player.GetCenterPosition();
			Vector2 offset = new Vector2(GameRoom.gameRoom.player.GetBounds().Width / 2, GameRoom.gameRoom.player.GetBounds().Height / 2);
			playPos += offset;
			Vector2 pos = GetCenterPosition();
			//By nature of ATAN2, the arrow sometime does a whole 360 to collide into the player.
			float angleToPlayer = (float)(Math.Atan2(playPos.Y - pos.Y, playPos.X - pos.X));
			float directionToPlayer = angleToPlayer - rotation;
			if (Math.Abs(directionToPlayer) > Math.PI)
				directionToPlayer -= Math.Sign(directionToPlayer) * (float) Math.PI * 2;

			//Denies PERFECT tracking of the player
			rotation += directionToPlayer/10;
			Vector2 facing = new Vector2(((float) Math.Cos(rotation)) * baseSpeed, ((float)Math.Sin(rotation)) * baseSpeed);
			SetVelocity(facing);
		}
		public override void LateUpdate(GameTime gameTime) {
			base.LateUpdate(gameTime);
			Vector2 pos = GetCenterPosition();
			if (pos.X < -GetBounds().Width / 2)
				OnKilled();
			else if (pos.X > Game1.mainGame.ViewPort.Width)
				OnKilled();
			if (pos.Y < -GetBounds().Height / 2)
				OnKilled();
			else if (pos.Y > Game1.mainGame.ViewPort.Height)
				OnKilled();
		}
		public override void Draw(SpriteBatch canvas) {
			enemySprite.DrawCentered(canvas, position, rotation);
		}
		public override Vector2 GetCenterPosition() {
			return GetPosition() + new Vector2(enemySprite.GetBounds().Width / 2, enemySprite.GetBounds().Height / 2);
		}
		public override Vector2 GetPosition() {
			return position;
		}
		public override Rectangle GetBounds() {
			return enemySprite.GetBounds();
		}
	}
}