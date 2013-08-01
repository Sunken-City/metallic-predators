using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetallicPredators
{
	public class Choice
	{
		private static int choice;
		private static int maximum;
		public Choice(int i, int max)
		{
			choice = i;
			maximum = max;
		}

		public int getInt()
		{
			return choice;
		}

		public void setInt(int i)
		{
			choice = i;
		}

		public int getMax()
		{
			return maximum;
		}

		public void setMax(int i)
		{
			maximum = i;
		}
	}
}
