using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetallicPredators
{
	public class DefenceCard : Card
	{
		public DefenceCard(int val)
		{
			this.value = val;
			this.type =  (int) Types.DEF;
		}

		public override void Play(Player cardLayer, Player other)
		{
			UI.Write("@8" + cardLayer.GetName() + " strengthens his Laser Grid by @1" + this.value + "@8 jigawatts!\n");
			cardLayer.SetDefence(cardLayer.GetDefence() + this.value);
			SFX.Play("Defence");
		}

		public override string ToString()
		{
			return "@1Defence: " + value;
		}
	}
}
