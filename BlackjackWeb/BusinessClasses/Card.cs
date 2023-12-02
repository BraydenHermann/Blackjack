using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.BusinessClasses
{
    /// <summary>
    /// a class simulating a Card object in the a game of Blackjack
    /// </summary>
    public class Card
    {
        //the get and set method for the Suit and Face enums
        public Suit Suit { get; set; }
        public Face Face { get; set; }

        public bool changedAce;

        
        /// <summary>
        /// constructor for the Card object, a Card has a suit and a face
        /// </summary>
        /// <param name="suit">suit of the card, of Suit enum type</param>
        /// <param name="face">face of the card, of Face enum type</param>
        public Card(Suit suit, Face face)
        { 
            Suit = suit;
            Face = face;
            this.changedAce = false;
        }

        /// <summary>
        /// method to get the value of a cards face.
        /// if the card is an ace, the player must decide if they want it to be equal to 11 or 1
        /// </summary>
        /// <param name="face">the face of the Card whose value is being retrieved</param>
        /// <returns>an int containing the value of a face card</returns>
        public int GetFaceValue() {
            int faceValue = 0;
            
            switch (Face)
            {
                case Face.Two:
                    faceValue = 2;
                    break;
                case Face.Three:
                    faceValue = 3;
                    break;
                case Face.Four:
                    faceValue = 4;
                    break;
                case Face.Five:
                    faceValue = 5;
                    break;
                case Face.Six:
                    faceValue = 6;
                    break;
                case Face.Seven:
                    faceValue = 7;
                    break;
                case Face.Eight:
                    faceValue = 8;
                    break;
                case Face.Nine:
                    faceValue = 9;
                    break;
                case Face.Ten:
                    faceValue = 10;
                    break;
                case Face.Jack:
                    faceValue = 10;
                    break;
                case Face.Queen:
                    faceValue = 10;
                    break;
                case Face.King:
                    faceValue = 10;
                    break;
                case Face.Ace1:
                    faceValue = 1;
                    break;
                case Face.Ace11:
                    faceValue = 11;
                    break;
            }
            return faceValue;
        }

        /// <summary>
        /// method to determine the value of the Ace card based upon the players decision
        /// </summary>
        /// <param name="choice">the players choice regarding the value of the Ace card</param>
        /// <returns>an int containing the value of the Ace card</returns>
        public Card ChangeAceValue(Card changeAce)
        {
            // if the card's ace value hasn't already changed
            if (changeAce.changedAce == false)
            {
                if (changeAce.Face == Face.Ace1)
                {
                    changeAce.Face = Face.Ace11;
                }
                else if (changeAce.Face == Face.Ace11)
                {
                    changeAce.Face = Face.Ace1;
                }
                changeAce.changedAce = true;
            }

            return changeAce;
        }

        /// <summary>
        /// return all information about the Card
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Face.ToString() + " of " + Suit.ToString();
        }
    }
}
