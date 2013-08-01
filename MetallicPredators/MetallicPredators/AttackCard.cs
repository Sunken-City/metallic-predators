using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetallicPredators
{
	public class AttackCard : Card
	{
		public AttackCard(int val)
		{
			this.value = val;
			this.type = (int)Types.ATT;
		}

		public override void Play(Player cardLayer, Player other)
		{
			if (other.GetDefence() == 0)
			{
				UI.Write("@8" + cardLayer.GetName() + " blasts " + other.GetName() + ", who takes @0" + this.value + "@8 damage!\n");
				other.SetHealth(other.GetHealth() - this.value);
				SFX.Play("Explosion");
			}
			//If the attack is greater than the current defence...
			else if (other.GetDefence() <= this.value)
			{
				UI.Write("@8" + cardLayer.GetName() + " shoots down " + other.GetName() + "'s Laser Grid!\n");
				other.SetDefence(0);
				SFX.Play("LaserGridDown");
			}
			//Otherwise...
			else if (other.GetDefence() > this.value)
			{
				UI.Write("@8" + cardLayer.GetName() + " blasts " + other.GetName() + "'s Laser Grid for @0" + this.value + "@8 damage!\n");
				//...Just subtract from the defence the damage to take.
				other.SetDefence(other.GetDefence() - this.value);
				SFX.Play("Gun");
			}
			//We can't have negative health!
			if (other.GetHealth() < 0)
			{
				other.SetHealth(0);
			}
		}

		public override string ToString()
		{
			return "@0Attack: " + value;
		}
	}
}
