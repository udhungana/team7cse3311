using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Carronade
{
	//All "interactive" objects in the game will be some form of actor.

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
	}
}