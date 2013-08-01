using System.Collections.Generic;

namespace MetallicPredators
{
	public class Player
	{
		private int health = 10;
		private int defence = 10;
		private string name = "NRNRNR";
		private Deck myDeck;
		private List<Card> hand;

		public Player()
		{
			myDeck = new Deck();
			hand = new List<Card>(5);
			for (int i = 0; i < 5; i++)
			{
				draw();
			}
		}

		public Player(string newName)
		{
			name = newName;
		}

		public Player(int deckScore)
		{
			myDeck = new Deck(deckScore);
			hand = new List<Card>(5);
			for (int i = 0; i < 5; i++)
			{
				draw();
			}
		}

		public Player(string n, int deckScore)
		{
			name = n;
			myDeck = new Deck(deckScore);
			hand = new List<Card>(5);
			for (int i = 0; i < 5; i++)
			{
				draw();
			}
		}

		public Player(int initialHealth, int initialDefence, string n, int deckScore)
		{
			name = n;
			health = initialHealth;
			defence = initialDefence;
			myDeck = new Deck(deckScore);
			hand = new List<Card>(5);
			for (int i = 0; i < 5; i++)
			{
				draw();
			}
		}

		public int GetHealth()
		{
			return health;
		}

		public int GetDefence()
		{
			return defence;
		}

		public string GetName()
		{
			return name;
		}

		public void SetName(string n)
		{
			name = n;
		}

		public void SetHealth(int h)
		{
			health = h;
		}

		public void SetDefence(int d)
		{
			defence = d;
		}

		public Card getCard(int index)
		{
			return hand[index];
		}

		public void playCard(int index, Player cardLayer, Player other)
		{
			hand[index].Play(cardLayer, other);
			discard(index);
		}

		public Card playCardMultiplayer(int index, Player other)
		{
			Card card = hand[index];

			card.Play(this, other);
			discard(index);

			return card;
		}

		public void playCard(Card cardPlayed, Player cardLayer, Player other)
		{
			cardPlayed.Play(cardLayer, other);
		}

		public void discard(int index)
		{
			hand.RemoveAt(index);
		}

		public void draw()
		{
			if (hand.Count >= 5)
			{
				UI.Write("Too many cards in hand! CLEARLY, YOU ARE CHEATING!");
				return;
			}
			if (myDeck.deckEmpty())
			{
				UI.Write("Deck Empty! Nothing left to draw!");
				hand.Add(new NullCard(0));
				return;
			}
			else
			{
				hand.Add(myDeck.draw());
			}
		}

		public void displayHand()
		{
			UI.Write("Your Hand:\n{");
			foreach (Card item in hand)
			{
				UI.Write("     " + item.ToString());
			}
			UI.Write("}");
		}

		public void displayDeck()
		{
			myDeck.displayDeck();
		}
	}
}
