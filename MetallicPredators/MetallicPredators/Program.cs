using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MetallicPredators
{
	class Program
	{
		public static void StoryLine(string line)
		{
			UI.Write(line);
			SFX.Play("Hit");
			Thread.Sleep(250);
		}
		public static void Story()
		{
			StoryLine("THE SPACEYEAR IS 3X85.");
			StoryLine("HUMANS HAVE GROWN BORED OF HUMANITY, AND HAVE DECIDED TO BECOME ROBOTS.");
			StoryLine("THEY QUICKLY TIRED OF BEING ROBOTS, AND BECAME ROBOT ANIMALS.");
			StoryLine("YOU ARE THE LAST MANDROID.");
			StoryLine("YOU MUST DEFEAT ALL OF THE OTHER ROBOTS.");
			StoryLine("GOOD LUCK.");
			UI.waitForConfirmation();
			UI.Write("PLEASE GIVE YOUR MANDROID A NAME:");
			string name = Console.ReadLine();
			Player p1 = new Player(name, 200);
			Player p2 = new Player("Titanium Laser Snake", 200);
			Battle b = new Battle(p1, p2);
			p1 = new Player(name, 300);
			p2 = new Player("Rocket-Powered Velociraptor", 300);
			b = new Battle(p1, p2);
			p1 = new Player(name, 400);
			p2 = new Player("Omega Death Mantis", 400);
			b = new Battle(p1, p2);
			p1 = new Player(name, 500);
			p2 = new Player("Nuke-Launching Baby Seal", 500);
			b = new Battle(p1, p2);
			StoryLine("YOU HAVE DEFEATED EVERY ROBOT IN THE UNIVERSE.");
			StoryLine("CONGRATULATIONS. YOU WIN.");
			UI.waitForConfirmation();
			TitleScreen();
		}

		public static void PlayMultiplayer()
		{
			UI.Clear();
			UI.Write("HOST OR GUEST?");
			UI.Write("");
			UI.Write("     HOST");
			UI.Write("     GUEST");
			UI.Write("");
			int choice = UI.getInput(2);
			if (choice == 0)
			{
				Multiplayer.hostConnect();
			}
			else if (choice == 1)
			{
				Multiplayer.clientConnect();
			}
		}

		public static void TitleScreen()
		{
			UI.Write(@"___  ___ _____ _____ ___   _      _     _____ _____  ");
			UI.Write(@"|  \/  ||  ___|_   _/ _ \ | |    | |   |_   _/  __ \ ");
			UI.Write(@"| .  . || |__   | |/ /_\ \| |    | |     | | | /  \/ ");
			UI.Write(@"| |\/| ||  __|  | ||  _  || |    | |     | | | |     ");
			UI.Write(@"| |  | || |___  | || | | || |____| |_____| |_| \__/\ ");
			UI.Write(@"\_|  |_/\____/  \_/\_| |_/\_____/\_____/\___/ \____/ ");
			UI.Write(@"____________ ___________  ___ _____ ___________  _____ ");
			UI.Write(@"| ___ \ ___ \  ___|  _  \/ _ \_   _|  _  | ___ \/  ___|");
			UI.Write(@"| |_/ / |_/ / |__ | | | / /_\ \| | | | | | |_/ /\ `--. ");
			UI.Write(@"|  __/|    /|  __|| | | |  _  || | | | | |    /  `--. \");
			UI.Write(@"| |   | |\ \| |___| |/ /| | | || | \ \_/ / |\ \ /\__/ /");
			UI.Write(@"\_|   \_| \_\____/|___/ \_| |_/\_/  \___/\_| \_|\____/ ");
			UI.Write("");
			SFX.Play("UFO");
			Thread.Sleep(1000);
			UI.Write("");
			UI.Write("     @91 PLAYER GAME");
			UI.Write("     @92 PLAYER GAME");
			UI.Write("");
			int choice = UI.getInput(2);
			if (choice == 0)
			{
				Story();
			}
			else if (choice == 1)
			{
				PlayMultiplayer();
			}
		}

		static void Main(string[] args)
		{
			ConsoleListener listener = new ConsoleListener();
			TitleScreen();
		}
	}
}
