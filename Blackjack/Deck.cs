using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    internal class Deck
    {
        
        private Card[] cards;
        Random rnd = new Random();

        public Deck()   
        {
            cards =
                new[] { "Spades", "Hearts", "Clubs", "Diamonds", }
                    .SelectMany(
                        suit => Enumerable.Range(1, 13),
                        (suit, rank) => new Card(rank, suit))
                    .ToArray();
        }
        public string getCard()
        {
            int i = rnd.Next(cards.Length);
            var drawnCard = cards[i];
            string suit = drawnCard.Suit;
            var rank = drawnCard.Rank;
            cards = cards.Where((source, index) => index != i).ToArray();
            if (rank == 11)
            {
                return "Jack of " + suit;
            }
            if (rank == 12)
            {
                return "Queen of " + suit;
            }
            if (rank == 13)
            {
                return "King of " + suit;
            }
            return rank + " of " + suit;
        }
    }
}
