using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetallicPredators
{
	public class EquipCard : Card
	{
		private int value;
		private const int type = (int) Types.EQUIP;
		private const bool endsTurn = true;
		private string name;

		public EquipCard(int val)
		{
			value = val;
		}

		public override void Play(Player cardLayer, Player other)
		{
			UI.Write("@8" + cardLayer.GetName() + " equips @3" + this.name + "@8 jigawatts!\n");
			cardLayer.SetDefence(cardLayer.GetDefence() + this.value);
			SFX.Play("Defence");
		}

		public int getvalue()
		{
			return value;
		}

		public override string ToString()
		{
			return "@1Defence: " + value;
		}
	}
}
