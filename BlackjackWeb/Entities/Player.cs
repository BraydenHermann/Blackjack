using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;
using Blackjack.BusinessClasses;

namespace Blackjack.Entities
{
    /// <summary>
    /// a class to represent a Player in the game
    /// </summary>
    public class Player
    {
        // constants to hold a range for betting
        const int MIN_BET = 2, MAX_BET = 500;

        private string name;
        private int chips, betAmount;
        private List<Card> hand = new List<Card>();
        public bool IsPlaying { get; set; }

        public string Name
        {   get { return name; }
            set
            {
                // validation check
                if (!String.IsNullOrEmpty(value))
                {
                    this.name = value;
                }
                else
                    throw new Exception("Name cannot be null or empty");
            }
        }

        public int Chips
        {   get { return this.chips; }
            set
            {
                // validation check
                if (value >= 0)
                {
                    this.chips = value;
                }
                else
                    throw new Exception("Invalid Chip amount");
            }
        }

        public int BetAmount
        {
            get { return this.betAmount; }
            set
            {
                // validation check
                if (value >= MIN_BET && value <= MAX_BET)
                {
                    this.betAmount = value;
                }
                else
                    throw new Exception("Invalid bet amount");
            }
        }

        public List<Card> Hand
        {   get { return this.hand; }
            set
            {
                // make sure each card in the hand is set
                bool fullHand = false;

                foreach (Card card in this.hand)
                {
                    // if the Card has values set
                    if (!String.IsNullOrEmpty(card.ToString()))
                        fullHand = true;
                    // any Card is not set
                    else
                    {
                        fullHand = false;
                        break; // exit loop in case later Cards are set
                    }
                }

                // validation check
                if (fullHand)
                    this.hand = value;
                //else
                    //throw new Exception("Not all Cards in the Hand have been set");
            }
        }

        // a method to check if the player's deck has an Ace
        public Card HasAce (Player p)
        {
            // check each card in the player's hand
            foreach (Card card in p.hand)
            {
                // if the value is 1 or 11 (an ace)
                if (card.GetFaceValue() == 1 || card.GetFaceValue() == 11)
                {
                    // if the card's ace value hasn't already changed
                    if (card.changedAce == false)
                        return card;
                }
            }

            return null;
        }

        /// <summary>
        /// constructor for the Player class
        /// </summary>
        /// <param name="name"></param>
        /// <param name="chips"></param>
        /// <param name="hand"></param>
        public Player(string name, int chips, List<Card> hand, bool isPlaying)
        {
            Name = name;
            Chips = chips;
            Hand = hand;
            IsPlaying = true; // all players are playing by default to start
        }

        /// <summary>
        /// return the Player's hand as a string
        /// </summary>
        /// <returns></returns>
        public string PrintHand()
        {
            string handTxt = "";

            foreach (Card card in Hand)
            {
                // if the card is an ace
                if (card.Face.ToString() == "Ace1" || card.Face.ToString() == "Ace11")
                    handTxt += "Ace of " + card.Suit.ToString() + " (" + card.GetFaceValue() + ")\n";
                else
                    handTxt += card.ToString() + " (" + card.GetFaceValue() + ")\n";
            }

            return handTxt;
        }

        /// <summary>
        /// return all information about the Player
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Name: " + Name + " Chips: " + Chips + " Bet Amount: " + BetAmount + " \nHand: " + PrintHand();
        }
    }
}
