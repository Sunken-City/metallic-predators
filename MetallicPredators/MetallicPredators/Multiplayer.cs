using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;

namespace MetallicPredators
{
	class Multiplayer
	{

		public static void hostConnect()
		{
			UI.Write("PLEASE GIVE YOUR MANDROID A NAME:");
			string name = Console.ReadLine();


			IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 6040);

			Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

			try
			{
				listener.Bind(localEndPoint);
				listener.Listen(10);

				UI.Write("Waiting for a player...");
				// Program is suspended while waiting for an incoming connection.
				Socket handler = listener.Accept();

				//Match has been found, begin battle.
				UI.Write("MATCH FOUND! PREPARE FOR BATTLE!");
				Thread.Sleep(2000);
				UI.Clear();

				HostBattle(handler, name);

				handler.Shutdown(SocketShutdown.Both);
				handler.Close();
				Program.TitleScreen();

			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}

			Console.WriteLine("\nPress ENTER to continue...");
			Console.Read();
		}

		public static void clientConnect()
		{
			UI.Write("PLEASE GIVE YOUR MANDROID A NAME:");
			string name = Console.ReadLine();

			UI.Write("Enter server IP:");
			string server = Console.ReadLine();

			// Connect to a remote device.
			try
			{
				IPAddress ipAddress = IPAddress.Parse(server);
				IPEndPoint remoteEP = new IPEndPoint(ipAddress, 6040);

				Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

				try
				{
					sender.Connect(remoteEP);

					//Match has been found, begin battle.
					UI.Write("MATCH FOUND! PREPARE FOR BATTLE!");
					Thread.Sleep(2000);
					UI.Clear();

					ClientBattle(sender, name);

					// Release the socket.
					sender.Shutdown(SocketShutdown.Both);
					sender.Close();
					Program.TitleScreen();

				}
				catch (ArgumentNullException ane)
				{
					Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
				}
				catch (SocketException se)
				{
					Console.WriteLine("SocketException : {0}", se.ToString());
				}
				catch (Exception e)
				{
					Console.WriteLine("Unexpected exception : {0}", e.ToString());
				}

			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}
		}

		public static void HostBattle(Socket handler, string name)
		{
			//Recieve the name first
			string newName = null;
			byte[] bytes;
			int bytesRec;
			while (true)
			{
				bytes = new byte[1024];
				bytesRec = handler.Receive(bytes);
				newName += Encoding.ASCII.GetString(bytes, 0, bytesRec);
				if (newName.IndexOf("<EOF>") > -1)
				{
					break;
				}
			}

			//Now send the other name
			byte[] message = Encoding.ASCII.GetBytes(name + "<EOF>");
			int bytesSent = handler.Send(message);

			do
			{
				ServerGameLoop(handler, name, newName);

				if (!PromptForReplay(handler))
					break;

			} while (true);
		}

		private static void ServerGameLoop(Socket handler, string name, string newName)
		{
			Player Host = new Player(name, 200);
			Player Client = new Player(newName.Substring(0, newName.IndexOf("<EOF>")));
			MultiplayerBattle scuffle = new MultiplayerBattle(Host, Client);
			scuffle.Begin();

			//-----------------------------------------------------------------------
			//This is the magic, the game loop.

			while ((Client.GetHealth() != 0) && (Host.GetHealth() != 0))
			{
				SendCard(handler, scuffle.Fight());
				
				if ((Client.GetHealth() != 0) && (Host.GetHealth() != 0))
				{
					scuffle.CardPlayed(RecieveCard(handler));
				}
			}
			TestForVictory(Client);
		}

		public static void ClientBattle(Socket sender, string name)
		{
			//Send the name first.
			byte[] message = Encoding.ASCII.GetBytes(name + "<EOF>");
			int bytesSent = sender.Send(message);

			//Now get the other name
			string newName = null;
			byte[] bytes;
			int bytesRec;
			while (true)
			{
				bytes = new byte[1024];
				bytesRec = sender.Receive(bytes);
				newName += Encoding.ASCII.GetString(bytes, 0, bytesRec);
				
				if (newName.IndexOf("<EOF>") > -1)
				{
					break;
				}
			}

			do
			{
				ClientGameLoop(sender, name, newName);
				
				if (!PromptForReplay(sender))
					break;

			} while (true);
		}

		private static bool PromptForReplay(Socket sender)
		{
			UI.Write("Play again?");
			UI.Write("");
			UI.Write("     YES");
			UI.Write("     NO");
			UI.Write("");
			int choice = UI.getInput(2);

			if (choice == 1)
			{
				sender.Shutdown(SocketShutdown.Both);	
			}

			return choice == 0;
		}

		private static void ClientGameLoop(Socket sender, string name, string newName)
		{
			Player Client = new Player(name, 200);
			Player Host = new Player(newName.Substring(0, newName.IndexOf("<EOF>")));
			MultiplayerBattle scuffle = new MultiplayerBattle(Client, Host);
			scuffle.Begin();
			//-----------------------------------------------------------------------
			//This is the magic, the game loop.

			while (Client.GetHealth() != 0 && Host.GetHealth() != 0)
			{
				scuffle.CardPlayed(RecieveCard(sender));
				
				if (Client.GetHealth() != 0 && Host.GetHealth() != 0)
				{
					SendCard(sender, scuffle.Fight());
				}
			}
			TestForVictory(Host);
		}

		private static void TestForVictory(Player player)
		{
			if (player.GetHealth() == 0)
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
			}
		}

		public static void SendCard(Socket sender, Card played)
		{
			string cardInfo = played.getType() + "#" + played.getValue() + "<EOF>"; 
			byte[] message = Encoding.ASCII.GetBytes(cardInfo);
			int bytesSent = sender.Send(message);
		}

		public static Card RecieveCard(Socket handler)
		{

			UI.Write("Please wait for your opponent to play a card.");
			Card recievedCard;
			string cardInfo = null;
			byte[] bytes;
			int bytesRec;
			while (true)
			{
				bytes = new byte[1024];
				bytesRec = handler.Receive(bytes);
				cardInfo += Encoding.ASCII.GetString(bytes, 0, bytesRec);
				if (cardInfo.IndexOf("<EOF>") > -1)
				{
					break;
				}
			}
			//Everything between # and <
			int value = Convert.ToInt32((cardInfo.Substring(cardInfo.IndexOf("#") + 1, cardInfo.IndexOf("<") - cardInfo.IndexOf("#") - 1)));
			switch (cardInfo[0])
			{
				case ('0'):
					recievedCard = new AttackCard(value);
					break;
				case ('1'):
					recievedCard = new DefenceCard(value);
					break;
				case ('2'):
					recievedCard = new HealCard(value);
					break;
				default:
					recievedCard = new AttackCard(0);
					break;
			}
			return recievedCard;
		}
	}
}
