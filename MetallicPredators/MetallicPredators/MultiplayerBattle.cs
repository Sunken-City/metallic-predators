using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MetallicPredators
{
	class MultiplayerBattle
	{
		public Player Player1 { get; private set; }
		public Player Enemy { get; private set; }
		private static Random AI = new Random();

		public MultiplayerBattle(Player P1, Player Network)
		{
			Player1 = P1;
			Enemy = Network;
		}

		public void Begin()
		{
			UI.Write("ROBO-SPACE-NETWORK-BATTLE, COMMENCE!\n");
			SFX.Play("RandomRoboSound");
		}

		public void CardPlayed(Card cardPlayed)
		{
			Enemy.playCard(cardPlayed, Enemy, Player1);
		}

		public Card PlayCard()
		{
			//Player's Turn
			int choice = UI.getInput(5);
			Card temp;
			try
			{
				temp = Player1.playCardMultiplayer(choice, Enemy);
			}
			catch (Exception)
			{
				SFX.Play("Hit");
				temp = new AttackCard(0);
			}
			Player1.draw();
			return temp;
		}

		public Card Fight()
		{
			UI.Write(Enemy.GetName() + " Stats:     @0HP: " + Enemy.GetHealth() + "     @1DEF: " + Enemy.GetDefence() + "\n");
			UI.Write(Player1.GetName() + " Stats:     @0HP: " + Player1.GetHealth() + "     @1DEF: " + Player1.GetDefence() + "\n");
			Player1.displayHand();
			return PlayCard();
		}
	}
}
