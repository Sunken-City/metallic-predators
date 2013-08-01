using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetallicPredators
{
	public class HealCard : Card
	{
		public HealCard(int val)
		{
			this.value = val;
			this.type  = (int) Types.HEAL;
		}

		public override void Play(Player cardLayer, Player other)
		{
			UI.Write("@8" + cardLayer.GetName() + " repairs its RoboBody using @2" + this.value + "@8 supercapacitors!\n");
			cardLayer.SetHealth(cardLayer.GetHealth() + this.value);
			SFX.Play("Repair");
		}

		public override string ToString()
		{
			return "@2Heal: " + value;
		}
	}
}
