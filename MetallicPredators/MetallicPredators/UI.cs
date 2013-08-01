using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetallicPredators
{
	//Provides the layer of abstraction between the UI and the Engine
	class UI
	{
		//public delegate void MessageEventHandler(object sender, string message);
		public static event EventHandler<EventArgsString> OnMessage;
		public static event EventHandler<EventArgsString> PressEnter;
		public static event EventHandler<EventArgsChoice> OnInput;
		public static event EventHandler OnClear;
		private static string input;
		private static Choice choice;

		public static void Write(string message)
		{
			if (OnMessage != null)
			{
				EventArgsString argument = new EventArgsString(message);
				OnMessage(null, argument);
			}
		}

		public static int getInput(int numChoices)
		{
			choice = new Choice(0, (numChoices - 1));
			if (OnInput != null)
			{
				EventArgsChoice argument = new EventArgsChoice(choice);
				OnInput(null, argument);
				return choice.getInt();
			}
			else
			{
				return 0;
			}
		}

		public static void Clear()
		{
			EventArgs e = new EventArgs();
			OnClear(null, e);
		}

		public static void waitForConfirmation()
		{
			if (PressEnter != null)
			{
				EventArgsString argument = new EventArgsString("");
				PressEnter(null, argument);
			}
		}

		static void ConsoleListener_OnInput(object sender, EventArgsString e)
		{
			input = e.ToString();
		}
	}
}