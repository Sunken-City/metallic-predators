using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetallicPredators
{
	public class EventArgsString : EventArgs
	{
		string value;
		public string Value { get { return value; } }
		public EventArgsString(string value) { this.value = value; }
		public override string ToString() { return value; }
	}

	public class EventArgsChoice : EventArgs
	{
		Choice value;
		public Choice Value { get { return value; } }
		public EventArgsChoice(Choice value) { this.value = value; }
		public override string ToString() { return value.getInt().ToString(); }
	}
}
