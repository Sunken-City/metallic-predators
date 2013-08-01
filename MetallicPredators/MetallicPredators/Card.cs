namespace MetallicPredators
{
	public abstract class Card
	{
		protected int value;
		protected int type;
		protected bool endsTurn = true;

		public enum Types
		{
			ATT,
			DEF,
			HEAL,
			EQUIP,
			TERRAIN
		};

		public Card()
		{

		}

		public abstract void Play(Player cardLayer, Player other);

		public int getType()
		{
			return type;
		}

		public int getValue()
		{
			return value;
		}
	}
}
