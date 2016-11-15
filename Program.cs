using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerFace
{
    class Card : IComparable
    {
        // card input is not provided
        public Card() { }
        // card has a suit and a rank input
        public Card(char aSuit, int aRank)
        {
            suit = aSuit;
            rank = aRank;
        }

        public char suit; // c or h or d or s
        public int rank; // 2-10, j = 11, q = 12, k = 13, a = 14

        public int CompareTo(object obj)
        {
            Card c = obj as Card;
            return rank - c.rank;
        }

        //public Card(string str)
        //{
        //    str = str.ToUpper();
        //    foreach(char c in str)
        //    {
        //        switch (c)
        //        {
        //            case 'C':
        //            case 'H':
        //            case 'S':
        //            case 'D':
        //                suit = c;
        //                break;
        //            case 'T'
        //                break;
        //            case '2-9':
        //                rank = c - '0';
        //                break;
        //        }
        //    }
        //    //if no rank or suit after forloop is done
        //    if(rank == 0)
        //        Console.WriteLine("You forgot the rank from " + str);
        //    if (suit == 0)
        //        Console.WriteLine("You forgot the suit from " + str);
        //}

        // could write function to check validity
        //bool IsValid()
        //{
        //    // example
        //    return suit != '\0' && rank >= 2 && rank <= 14;
        //}

    }

    class Program
    {
        static void Main(string[] args)
        {
            // Optimize: Card[] hand = GetHand()

            Card[] hand = new Card[5]; // Placeholder for 5 cards

            hand[0] = new Card('H', 10);
            hand[1] = new Card('H', 10);
            hand[2] = new Card('S', 10);
            hand[3] = new Card('H', 9);
            hand[4] = new Card('H', 9);

            // Bonus: create function to build hand randomly

            Array.Sort(hand);

            for (int k = 0; k < hand.Length; k++)
            {
                Console.WriteLine("Hand " + k + ":" + hand[k].rank + " of " + hand[k].suit);
            }

            // Check for combinations

            if (IsStraightFlush(hand))
            {
                Console.WriteLine("Straight Flush!");
            }
            else if (IsFourOfAKind(hand))
            {
                Console.WriteLine("Four of a kind!");
            }
            else if (IsFullHouse(hand))
            {
                Console.WriteLine("Full House!");
            }
            else if (IsFlush(hand))
            {
                Console.WriteLine("Flush!");
            }
            else if (IsStraight(hand))
            {
                Console.WriteLine("Straight!");
            }
            else if (IsThreeOfAKind(hand))
            {
                Console.WriteLine("Three of a kind!");
            }
            else if (IsTwoPair(hand))
            {
                Console.WriteLine("Two Pairs!");
            }
            else if (IsPair(hand))
            {
                Console.WriteLine("Pair!");
            }
            else
            {
                Card highCard = hand[hand.Length - 1];
                Console.WriteLine("Card High: " + highCard.rank + " of " + highCard.suit);
            }

            Console.Read();
        }

        // Write function to get hand inputs from user
        //static Card[] GetHand(string[] args)
        //{
        //    Card[] hand = new Card[5];
        //    int index = 0;

        //    foreach (string a in args)
        //    {
        //        // handle too many cards
        //        if (index >= 5)
        //            break;
        //        // handle too few
        //        // handle incorrect input
        //        Card c = new Card(a);
        //        hand[index++] = c;
        //    }

        //    while(index < 5)
        //    {
        //        hand[index++] = Deal();
        //    } 
        //}

        //// static Card[] deck = new Card[52];
        //static Card[] deck = null;
        //static int dealIndex = 0;

        //static Card Deal()
        //{
        //    if (deck == null || dealIndex >= 41) ;
        //    {
        //        deck = new Card[52];
        //        int index = 0;
        //        // TODO: fill deck with one of each card
        //        // shuffle deck (randomize)
        //    }
        //    // deal
        //    return deck[dealIndex++];
        //}

        static bool IsStraightFlush(Card[] hand)
        {
            // a sequence of cards all one suit 
            return IsFlush(hand) && IsStraightFlush(hand);
        }

        static bool IsFlush(Card[] hand)
        {
            // cards all match suit of card [0]

            for (int i = 1; i < hand.Length; i++)
            {
                //Console.WriteLine("Flush First: " + hand[0].suit + " i: " + i + ", suit: " + hand[i].suit);
                if (hand[0].suit != hand[i].suit)
                {
                    return false;
                }
            }
            return true;
        }

        static bool IsStraight(Card[] hand)
        {
            // assume cards in hand are sorted, check if in sequence 
            for (int i = 0; i < hand.Length - 1; i++)
            {
                //Console.WriteLine("Straight: " + i + ":" + hand[i].rank + ", i+ " + hand[i + 1].rank);
                if (hand[i].rank + 1 != hand[i + 1].rank)
                {
                    //Console.WriteLine("not straight");
                    return false;
                }
            }
            return true;

        }

        static bool IsFullHouse(Card[] hand)
        {
            return IsThreeOfAKind(hand) && IsTwoPair(hand);
        }
        static bool IsFourOfAKind(Card[] hand)
        {
            int counter = 0;
            Console.WriteLine("Three length: " + hand.Length);

            // assume cards in hand are sorted, check if 4 match 
            for (int i = 0; i < hand.Length - 1; i++)
            {
                if (hand[i].rank != hand[i + 1].rank)
                {
                    counter++;
                    //Console.WriteLine("no match: " + counter + ", Rank: " + hand[i].rank);
                    if (counter == 2)
                    {
                        return false;
                    }
                }

                if (hand[i].rank == hand[i + 1].rank)
                {
                    counter++;
                    //Console.WriteLine("match: " + counter + "Rank: " + hand[i].rank);
                    if (counter == 4)
                        return true;
                }
                //Console.WriteLine("4ofKind: " + counter + hand[i].rank);
            }
            return false;
        }

        static bool IsThreeOfAKind(Card[] hand)
        {
            //int falseCounter = 0;
            int trueCounter = 1;

            for(int i = 0; i < hand.Length - 1; i++)
            {
                //if (hand[i].rank != hand[i + 1].rank)
                //{
                //    falseCounter++;
                //    Console.WriteLine("no match: " + falseCounter + ", Rank: " + hand[i].rank);
                //    if (falseCounter == 3)
                //    {
                //        return false;
                //    }
                //}
                Console.WriteLine("initial counter: " + trueCounter + ", Rank: " + hand[i].rank);

                if (hand[i].rank == hand[i + 1].rank)
                {
                    trueCounter++;
                    Console.WriteLine("match: " + trueCounter + ", Rank: " + hand[i].rank);
                    if (trueCounter == 3)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        static bool IsTwoPair(Card[] hand)
        {
            int counter = 0;

            for (int i = 0; i < hand.Length - 1; i++)
            {
                if (hand[i].rank == hand[i + 1].rank)
                {
                    counter++;
                    i++;
                    //Console.WriteLine("Count: " + counter + ", i: " + i);
                }

                if (counter == 2)
                    return true;
            }
            return false;
        }

        static bool IsPair(Card[] hand)
        {
            for (int i = 0; i < hand.Length - 1; i++)
            {
                if (hand[i].rank == hand[i + 1].rank)
                    return true;
            }
            return false;
        }
    }
}
