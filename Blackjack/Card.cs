namespace Blackjack
{
    internal class Card
    {
        public Card(int rank, string suit)
        {
            Rank = rank;
            Suit = suit;
        }

        public int Rank { get; }
        public string Suit { get; }
    }
}