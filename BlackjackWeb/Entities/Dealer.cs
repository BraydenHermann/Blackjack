using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blackjack.BusinessClasses;

namespace Blackjack.Entities
{
    /// <summary>
    /// a class to represent the Dealer in the game
    /// </summary>
    public class Dealer
    {
        private List<Card> hand = new List<Card>();

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

        /// <summary>
        /// constructor for the Dealer class
        /// </summary>
        /// <param name="hand"></param>
        public Dealer(List<Card> hand)
        {
            Hand = hand;
        }

        /// <summary>
        /// return the Dealer's hand as a string
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
        /// return all information about the Dealer
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Hand: " + Hand.ToString();
        }
    }
}
