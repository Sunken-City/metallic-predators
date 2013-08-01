using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace MetallicPredators
{
	public class Deck
	{
		//Note: The bottom index of the deck is the top of the deck.
		private List<Card> cardDeck;
		private List<Card> discardPile;


		private static int GetSeed()
		{
			long id = (long)Process.GetCurrentProcess().Id;
			long ticks = DateTime.Now.Ticks;

			return (int)(id & ticks);
		}

		private static Random rand = new Random(GetSeed());
		//Array index of the last card, starts off as -1 to symbolize no cards.
		private int top = -1;
		public Deck()
		{
			cardDeck = new List<Card>(30);
			discardPile = new List<Card>(30);
			//Starting at 1, because an attack that does 0 damage is silly.
			for (int i = 1; i <= 10; i++)
			{
				cardDeck.Add(new AttackCard(i));
				top++;
				cardDeck.Add(new DefenceCard(i));
				top++;
				cardDeck.Add(new HealCard(i));
				top++;
			}
			this.shuffle();
		}

		public Deck(int deckScore)
		{
			cardDeck = new List<Card>(30);
			discardPile = new List<Card>(30);
			generateDeck(deckScore);
			this.shuffle();
		}

		public Card draw()
		{
			//Look at the top card of the deck
			Card card = cardDeck[top--];
			//Take it off of the deck
			cardDeck.Remove(card);
			//Return the card
			return card;
		}

		public void shuffle()
		{
			int n = cardDeck.Count;
			while (n > 1)
			{
				n--;
				int randomIndex = rand.Next(n + 1);
				Card temp = cardDeck[randomIndex];
				cardDeck[randomIndex] = cardDeck[n];
				cardDeck[n] = temp;
			}  
		}

		public bool deckEmpty()
		{
			return cardDeck.Count <= 0;
		}

		public int cardsLeft()
		{
			return cardDeck.Count;
		}

		public void generateDeck(int maxPoints)
		{
			int currPoints = 0;
			int type;
			int value;
			int defenceCount = 0;
			int healCount = 0;
			while ((currPoints != maxPoints)&&(top < 29))
			{
				type = rand.Next(3);
				value = rand.Next(10) + 1;
				if ((currPoints + ((type+1) * value)) <= maxPoints)
				{
					switch (type)
					{
						case (0):
							cardDeck.Add(new AttackCard(value));
							top++;
							currPoints = currPoints + (1 * value);
							break;
						case (1):
							if (defenceCount < 5)
							{
								cardDeck.Add(new DefenceCard((value/2) + 1));
								top++;
								defenceCount++;
								currPoints = currPoints + (2 * value);
							}
							break;
						case (2):
							if (healCount < 7)
							{
								cardDeck.Add(new HealCard(value));
								healCount++;
								top++;
								currPoints = currPoints + (3 * value);
							}
							break;
					}
				}
			}
			if (top < 29)
			{
				cardDeck.Clear();
				top = -1;
				generateDeck(maxPoints);
			}
		}

		public void displayDeck()
		{
			UI.Write("Your Deck:\n{");
			int cardCounter = 0;
			foreach (Card item in cardDeck)
			{
				UI.Write("     " + item.ToString());
				cardCounter++;
			}
			while (cardCounter < 30)
			{
				UI.Write("     @9EMPTY");
				cardCounter++;
			}
			UI.Write("}");
		}

		public void discard(Card discarded)
		{
			discardPile.Add(discarded);
		}
	}
}
