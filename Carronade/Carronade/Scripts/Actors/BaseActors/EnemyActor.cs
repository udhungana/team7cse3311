using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Carronade
{
	//All "interactive" objects in the game will be some form of actor.
	//All enemies share some common traits.
	public abstract class EnemyActor : KinematicActor
	{
		public int damage {get; private set;} = 10;
		public EnemyActor(float x, float y, float r) : base(x, y, r) {
		}
		public EnemyActor(Vector2 pos, float r) : base(pos, r) {
		}
		public void SetDamage(int amount) {
			damage = amount;
		}
		//Slamming into the player vs...
		public virtual void OnImpact() {
			GameRoom.gameRoom.RemoveActor(this);
		}
		//Being killed (Slamming into an invincible player, dying of old age, into the wall*
		public virtual void OnKilled() {
			GameRoom.gameRoom.IncrementScore(damage);
			GameRoom.gameRoom.RemoveActor(this);
		}
	}
}