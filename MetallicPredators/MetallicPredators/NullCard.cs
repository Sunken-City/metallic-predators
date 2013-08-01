using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetallicPredators
{
	class NullCard : Card
	{
		public NullCard(int val)
		{
			this.value = val;
			this.type  = 9;
		}

		public override void Play(Player cardLayer, Player other)
		{
			UI.Write("@8" + cardLayer.GetName() + " malfunctions and blasts itself!\n");
			cardLayer.SetHealth(cardLayer.GetHealth() - 1);
			SFX.Play("Death");
		}

		public override string ToString()
		{
			return "@9EMPTY";
		}
	}
}
