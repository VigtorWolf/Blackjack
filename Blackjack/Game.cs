using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Blackjack
{
    internal class Game
    {
        private static string dealerCard;
        private static string playerCard;
        private static int dealerTotal;
        private static int playerTotal;
        private static int hitOrStand;
        private static string input;
        private static bool restart = true;

        static void Main(string[] args)
        {
            while (restart == true)
            {
                var deck = new Deck();
                Console.WriteLine("Welcome customer, today we will be playing a game of blackjack, I will be your dealer");
                dealerCard = deck.getCard();
                dealerTotal = getInt(dealerCard);
                Console.WriteLine($"Dealers card: {dealerCard}");
                playerCard = deck.getCard();
                playerTotal = getInt(playerCard);
                Console.WriteLine($"Your card: {playerCard}");
                Console.WriteLine($"Dealer total is: {dealerTotal}");
                Console.WriteLine($"Your total is: {playerTotal}");
                Console.WriteLine("Would you like to STAND or HIT?");

                input = Console.ReadLine().ToLower();

                while (input != "END")
                {
                    if (input == "stand")
                    {
                        hitOrStand = 1;
                    }
                    if (input == "hit")
                    {
                        hitOrStand = 2;
                    }
                    switch (hitOrStand)
                    {
                        case 1:
                            while (dealerTotal < 21 && dealerTotal <= playerTotal)
                            {
                                dealerCard = deck.getCard();
                                Console.WriteLine($"Dealer draws an extra card: {dealerCard}");
                                int newDealerCard = getInt(dealerCard);
                                dealerTotal = dealerTotal + newDealerCard;
                            }
                            if (dealerTotal > playerTotal && dealerTotal < 21 || dealerTotal == 21)
                            {
                                Console.WriteLine($"Dealers total is {dealerTotal} and has decided to stand");
                                Console.WriteLine($"Your total is {playerTotal} the dealer is closest to 21");
                                Console.WriteLine("House wins");
                                input = "END";
                                break;
                            }
                            else
                            {
                                Console.WriteLine($"Dealers total is {dealerTotal}");
                                Console.WriteLine($"Your total is {playerTotal}");
                                Console.WriteLine("You win :)");
                                input = "END";
                                break;
                            }
                        case 2:
                            playerCard = deck.getCard();
                            Console.WriteLine($"Player draws an extra card: {playerCard}");
                            playerTotal = playerTotal + getInt(playerCard);
                            Console.WriteLine($"Your total is {playerTotal}");
                            if (playerTotal == 21)
                            {
                                Console.WriteLine("You win");
                                input = "END";
                                break;
                            }
                            if (playerTotal > 21)
                            {
                                Console.WriteLine("House wins");
                                input = "END";
                                break;
                            }
                            Console.WriteLine("Would you like to STAND or HIT?");
                            input = Console.ReadLine().ToLower();
                            if (input == "stand" || input == "hit")
                            {
                                break;
                            }
                            break;
                    }
                }

                Console.WriteLine("Would you like to play again? YES/NO");
                if (Console.ReadLine().ToLower() != "yes")
                {
                    restart = false;
                }
            }
                int getInt(string subjectString)
            {
                int total = 0;
                if (subjectString.Contains("Jack") || subjectString.Contains("Queen") || subjectString.Contains("King"))
                {
                    total = 10;
                }
                else
                {
                    string resultString = Regex.Match(subjectString, @"\d+").Value;
                    total = Int32.Parse(resultString);
                }
                    if (total == 1 && playerTotal + 11 <= 21 || total == 1 && dealerTotal + 11 <= 21)
                    {
                        total = 11;
                    }
                    return total;
                }
        }
    }
}
