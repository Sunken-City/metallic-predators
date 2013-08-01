using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Media;
using System.Diagnostics;

namespace MetallicPredators
{
	//This is the class that binds the engine to the console.
	class ConsoleListener
	{
		public ConsoleListener()
		{
			UI.OnMessage += new EventHandler<EventArgsString>(UI_OnMessage);
			UI.OnInput += new EventHandler<EventArgsChoice>(UI_OnInput);
			UI.PressEnter += new EventHandler<EventArgsString>(UI_PressEnter);
			UI.OnClear += new EventHandler(UI_OnClear);

		}

		void UI_OnClear(object sender, EventArgs e)
		{
			Console.Clear();
		}

		void UI_PressEnter(object sender, EventArgsString e)
		{
			Console.WriteLine("(Press Enter to Continue)");
			Console.ReadLine();
			Console.Clear();
		}

		void UI_OnInput(object sender, EventArgsChoice e)
		{
			makeChoice(e.Value);
		}

		void UI_OnMessage(object sender, EventArgsString e)
		{
			ParseColors(e.ToString());
		}

		~ ConsoleListener()
		{
			UI.OnMessage -= new EventHandler<EventArgsString>(UI_OnMessage);
			UI.OnInput -= new EventHandler<EventArgsChoice>(UI_OnInput);
			UI.PressEnter -= new EventHandler<EventArgsString>(UI_PressEnter);
		}

		public void ParseColors(string line)
		{
			char[] parseable = line.ToCharArray();
			bool isColor = false;
			foreach (char item in parseable)
			{
				if (item == '@')
				{
					isColor = true;
					continue;
				}
				if (isColor)
				{
					changeColor((int)item);
					isColor = false;
					continue;
				}
				else
				{
					Console.Write(item);
				}
			}
			Console.Write("\n");
			Console.ForegroundColor = ConsoleColor.Gray;
		}

		public void changeColor(int colorNumber)
		{
			switch (colorNumber)
			{
				case ((int)'0'):
					Console.ForegroundColor = ConsoleColor.Red;
					break;
				case ((int)'1'):
					Console.ForegroundColor = ConsoleColor.Cyan;
					break;
				case ((int)'2'):
					Console.ForegroundColor = ConsoleColor.Green;
					break;
				case ((int)'3'):
					Console.ForegroundColor = ConsoleColor.DarkMagenta;
					break;
				case ((int)'4'):
					Console.ForegroundColor = ConsoleColor.Yellow;
					break;
				case ((int)'8'):
					Console.ForegroundColor = ConsoleColor.Gray;
					break;
				case ((int)'9'):
					Console.ForegroundColor = ConsoleColor.White;
					break;
			}
		}
		
		public void makeChoice(Choice choice)
		{
			//NOTE: THE CHOICES HAVE TO HAVE A FREE LINE ABOVE AND BELOW THEM IN ORDER TO WORK PROPERLY.
			//ALSO, YOU NEED TO HAVE AT LEAST ONE FREE SPACE BEFORE EACH OF THE CHOICES OR ELSE
			//THE FIRST CHARACTER WILL BE LOST. YES, THIS IS BAD PROGRAMMING, BUT IT'S NOT HIGH ON THE
			//LIST OF PRIORITIES TO FIX, CONSIDERING THAT I'M THE ONLY ONE WRITING THIS.
			Console.SetCursorPosition(0, Console.CursorTop - (choice.getMax() + 2));
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write("\r>");
			Console.CursorVisible = false;
			int position = 0;
			ConsoleKeyInfo key = new ConsoleKeyInfo();
			
			do
			{
				while (Console.KeyAvailable == false)
					Thread.Sleep(50); // Loop until input is entered.

				key = Console.ReadKey(true);
				if (key.Key == ConsoleKey.UpArrow && position > 0)
				{
					Console.Write("\r ");
					Console.SetCursorPosition(0, Console.CursorTop - 1);
					SFX.Play("Selection");
					position--;
					Console.Write("\r>");
				}
				else if (key.Key == ConsoleKey.DownArrow && position < (choice.getMax()))
				{
					Console.Write("\r ");
					Console.SetCursorPosition(0, Console.CursorTop + 1);
					SFX.Play("Selection");
					position++;
					Console.Write("\r>");
				}
			} while (key.Key != ConsoleKey.Enter);
			Console.CursorVisible = true;
			choice.setInt(position);
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.Clear();
			//Console.WriteLine("\rNRNRNRNRNRNRNRNRNRNRNRNRNRNRNRNRNR");
		}
	}
}
