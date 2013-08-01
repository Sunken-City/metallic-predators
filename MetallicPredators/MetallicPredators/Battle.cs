using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MetallicPredators
{
	public class Battle
	{
		public Player Player1 { get; private set; }
		public Player Enemy { get; private set; }
		private static Random AI = new Random();

		public Battle(Player P1, Player P2)
		{
			this.Player1 = P1;
			this.Enemy = P2;
			Begin();
		}

		public void Begin()
		{
			UI.Write("ROBO-SPACE-BATTLE, COMMENCE!\n");
			SFX.Play("RandomRoboSound");
			while ((Enemy.GetHealth() != 0) && (Player1.GetHealth() != 0))
			{
				this.Fight();
			}
			if ((Enemy.GetHealth() == 0))
			{
				UI.Write("@0V@9I@1C@0T@9O@1R@0Y@9!@1!@0!@9!@1!@0!@9!"); //Victory!!!!!!
				SFX.Play("Skrillex");
				UI.waitForConfirmation();
			}
			else
			{
				UI.Write("@8DEFEAT...");
				SFX.Play("LongExplosion");
				Thread.Sleep(2000);
				UI.waitForConfirmation();
				Program.TitleScreen();
			}
		}

		private void Fight()
		{
			UI.Write(Enemy.GetName() + " Stats:     @0HP: " + Enemy.GetHealth() + "     @1DEF: " + Enemy.GetDefence() + "\n");
			UI.Write(Player1.GetName() + " Stats:     @0HP: " + Player1.GetHealth() + "     @1DEF: " + Player1.GetDefence() + "\n");
			Player1.displayHand();
			//Player's Turn
			int choice = UI.getInput(5);
			try
			{
				Player1.playCard(choice, Player1, Enemy);
			}
			catch (Exception)
			{
				SFX.Play("Hit");
			}
			
			Player1.draw();
			//Enemy's Turn
			Thread.Sleep(250);
			if ((Enemy.GetHealth() != 0))
			{
				try
				{
					Enemy.playCard(AI.Next(5), Enemy, Player1);
				}
				catch (Exception)
				{
					SFX.Play("Hit");
				}
				Enemy.draw();
			}
			Thread.Sleep(250);
		}
	}
}

