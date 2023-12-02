using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.BusinessClasses
{
    /// <summary>
    /// a class simulating a deck of cards
    /// </summary>
    public class Deck
    {
        //initialization of the list of Cards and the list of Drawn cards
        private List<Card> cards = new List<Card>();

        public List<Card> Cards
        {
            get { return this.cards; }
            set
            {
                // make sure each card in the hand is set
                bool fullHand = false;

                foreach (Card card in this.cards)
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
                    this.cards = value;
                else
                    throw new Exception("Not all Cards in the Hand have been set");
            }
        }
        public List<Card> DrawnTracker { get; set; } = new List<Card>();

        /// <summary>
        /// constructor for the Deck class
        /// </summary>
        /// <param name="cards">the deck of cards</param>
        public Deck()
        {
            MakeDeck();
        }

        /// <summary>
        /// a method to make a deck of cards
        /// </summary>
        public void MakeDeck() {
            // for each suit
            for (int suitNum = 0; suitNum < Enum.GetNames(typeof(Suit)).Length; suitNum++) {
                // for each possible face value (minus 11 to avoid duplicate aces)
                for (int faceNum = 0; faceNum < (Enum.GetNames(typeof(Face)).Length - 1); faceNum++) {
                    Card card = new Card((Suit)suitNum, (Face)faceNum );
                    cards.Add(card);
                }
            }
        }

        /// <summary>
        /// a method to simulate drawing a random card from a deck of cards
        /// </summary>
        /// <param name="deck">the deck containing a list of Card objects</param>
        /// <returns>a unique drawn card</returns>
        public Card DrawCard() {
            Card drawnCard = null;

            // if cards have been drawn
            if (DrawnTracker.Count != 0)
            {
                bool isDrawn = true;
                while (isDrawn) {
                    Random r = new Random();
                    Card temp = Cards[r.Next(Cards.Count)];

                    // if the card was not already drawn
                    if (!DrawnTracker.Contains(temp))
                    {
                        drawnCard = temp;
                        DrawnTracker.Add(drawnCard);
                        isDrawn = false;
                    }
                    else
                    {
                        isDrawn = true;
                    }
                }
            }

            // if no card have been drawn yet
            else
            {
                Random r = new Random();
                drawnCard = Cards[r.Next(Cards.Count)];
                DrawnTracker.Add(drawnCard);
            }
            return drawnCard;

        }
        /// <summary>
        /// a method to print out the contents of the deck. Used for debugging purposes
        /// </summary>
        public string PrintDeck() {
            string deckString = null;
            foreach (Card card in Cards)
            {
                deckString += (card.ToString() + " Value: " + card.GetFaceValue() + " || ");
            }
            return deckString;
        }

        public override string ToString()
        {
            return "Cards: " + Cards.ToString();
        }
    }
}
