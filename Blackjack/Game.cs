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
        private static string? dealerCard;
        private static string? playerCard;
        private static int dealerTotal;
        private static int playerTotal;
        private static int hitOrStand;
        private static string? input;
        private static bool restart = true;
        private int winCount;
        private int iteration;
        private int loseCount;
        private int spadesCount;
        private int diamondsCount;
        private int heartsCount;
        private int clubsCount;

        static void Main(string[] args)
        {
            Game game = new Game();
            Console.WriteLine("Would you like to play blackjack? YES/NO");
            if (Console.ReadLine().ToLower() == "yes")
            {
                game.GameStart();
            }
            else if (Console.ReadLine().ToLower() == "test")
            {
                game.Test();
            }
        }

        public void GameStart()
        {
            while (restart == true)
            {
                //IMPORTANT THAT THE Deck IS INSTANSIATED AFTER RESTART.
                //Otherwise the deck continues to shrink until there are no cards left.
                var deck = new Deck();
                Console.WriteLine("Welcome customer, today we will be playing a game of blackjack, I will be your dealer");

                dealerCard = deck.GetRandomCard();
                dealerTotal = DealerGetInt(dealerCard);
                Console.WriteLine($"Dealers card: {dealerCard}");
                playerCard = deck.GetRandomCard();
                playerTotal = PlayerGetInt(playerCard);
                Console.WriteLine($"Your card: {playerCard}");

                Console.WriteLine($"Dealer total is: {dealerTotal}");
                Console.WriteLine($"Your total is: {playerTotal}");

                Console.WriteLine("Would you like to STAND or HIT?");

                //Console reader:
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
                        //Dealer plays:
                        case 1:
                            while (dealerTotal < 21 && dealerTotal <= playerTotal)
                            {
                                dealerCard = deck.GetRandomCard();

                                Console.WriteLine($"Dealer draws an extra card: {dealerCard}");
                                int newDealerCard = DealerGetInt(dealerCard);
                                dealerTotal = dealerTotal + newDealerCard;
                                Console.WriteLine($"Dealer total is: {dealerTotal}");
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

                                Console.WriteLine($"Your total is {playerTotal}");
                                Console.WriteLine("You win :)");
                                input = "END";
                                break;
                            }
                        //Player plays:
                        case 2:
                            playerCard = deck.GetRandomCard();
                            Console.WriteLine($"Player draws an extra card: {playerCard}");
                            playerTotal = playerTotal + PlayerGetInt(playerCard);
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
                //replay function, works as a while loop, not the most efficient solution, but because of the size of the game
                //It doesn't reduce efficiency performancewise.
                Console.WriteLine("Would you like to play again? YES/NO");
                if (Console.ReadLine().ToLower() != "yes")
                {
                    restart = false;
                }
            }
        }

        //The reason the next 2 methods look the same,is because there is a difference on playertotal and dealertotal, it might be worth to send the information from
        //The other method, and reduce this to 1 method instead.
        //This function determines what a king/queen value is, and what an ace should be when drawn relatively to player/dealer total.
        //Maybe it's better if half of this function is in the Deck class instead, but it is important that ace is determined with totals in mind.
        public int PlayerGetInt(string subjectString)
        {
            int total = 0;
            if (subjectString.Contains("Ace"))
            {
                if (playerTotal + 11 <= 21)
                {
                    return total = 11;
                }
                return total = 1;
            }
            if (subjectString.Contains("Jack") || subjectString.Contains("Queen") || subjectString.Contains("King"))
            {
                return total = 10;
            }
            else
            {
                string resultString = Regex.Match(subjectString, @"\d+").Value;
                total = Int32.Parse(resultString);
            }
            return total;
        }

        public int DealerGetInt(string subjectString)
        {
            int total = 0;
            if (subjectString.Contains("Ace"))
            {
                if (dealerTotal + 11 <= 21)
                {
                    return total = 11;
                }
                return total = 1;
            }
            if (subjectString.Contains("Jack") || subjectString.Contains("Queen") || subjectString.Contains("King"))
            {
                return total = 10;
            }
            else
            {
                string resultString = Regex.Match(subjectString, @"\d+").Value;
                total = Int32.Parse(resultString);
            }
            return total;
        }
        public void Test()
        {

            while (iteration < 1000)
            {
                var deck = new Deck();
                Console.WriteLine($"Current iteration: {iteration}");
                dealerCard = deck.GetRandomCard();
                if (dealerCard.Contains("Spades")) { spadesCount++; };
                if (dealerCard.Contains("Diamonds")) { diamondsCount++; };
                if (dealerCard.Contains("Hearts")) { heartsCount++; };
                if (dealerCard.Contains("Clubs")) { clubsCount++; };
                dealerTotal = DealerGetInt(dealerCard);


                playerCard = deck.GetRandomCard();
                if (playerCard.Contains("Spades")) { spadesCount++; };
                if (playerCard.Contains("Diamonds")) { diamondsCount++; };
                if (playerCard.Contains("Hearts")) { heartsCount++; };
                if (playerCard.Contains("Clubs")) { clubsCount++; };
                playerTotal = PlayerGetInt(playerCard);

                while (playerTotal < 17)
                {
                    playerCard = deck.GetRandomCard();
                    if (playerCard.Contains("Spades")) { spadesCount++; };
                    if (playerCard.Contains("Diamonds")) { diamondsCount++; };
                    if (playerCard.Contains("Hearts")) { heartsCount++; };
                    if (playerCard.Contains("Clubs")) { clubsCount++; };
                    playerTotal = PlayerGetInt(playerCard) + playerTotal;
                }
                if (playerTotal == 21)
                {
                    Console.WriteLine("Win");
                    winCount++;
                    iteration++;
                }
                if(playerTotal > 21)
                {
                    Console.WriteLine("Lose");
                    loseCount++;
                    iteration++;
                }
                else
                {
                    while (dealerTotal < 21 && dealerTotal <= playerTotal)
                    {
                        dealerCard = deck.GetRandomCard();
                        if (dealerCard.Contains("spades")) { spadesCount++; };
                        if (dealerCard.Contains("diamonds")) { diamondsCount++; };
                        if (dealerCard.Contains("hearts")) { heartsCount++; };
                        if (dealerCard.Contains("clubs")) { clubsCount++; };
                        int newDealerCard = DealerGetInt(dealerCard);
                        dealerTotal = dealerTotal + newDealerCard;
                    }
                    if (dealerTotal > playerTotal && dealerTotal < 21 || dealerTotal == 21)
                    {
                        Console.WriteLine("Lose");
                        loseCount++;
                        iteration++;
                    }
                    else
                    {
                        Console.WriteLine("Win");
                        winCount++;
                        iteration++;
                    }
                }
            }
            Console.WriteLine($"House wins: {loseCount}, Player wins: {winCount}");
            double winPercentage = (double)winCount / (double)iteration  * 100;
            Console.WriteLine($"Win percentage: {winPercentage}%");
            double spadesPercentage = (double)spadesCount / (double)iteration * 100;
            double diamondsPercentage = (double)diamondsCount / (double)iteration * 100;
            double heartsPercentage = (double)heartsCount / (double)iteration * 100;
            double clubsPercentage = (double)clubsCount / (double)iteration * 100;
            Console.WriteLine($"Spade count: {spadesCount}");
            Console.WriteLine($"Diamonds count: {diamondsCount}");
            Console.WriteLine($"Hearts count: {heartsCount}");
            Console.WriteLine($"Clubs count: {clubsCount}");
        }
    }
}

