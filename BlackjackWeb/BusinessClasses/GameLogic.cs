using Blackjack.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.BusinessClasses
{
    class GameLogic
    {
        //the list of players that will be playing the game
        private List<Player> players = new List<Player>();

        public List<Player> Players { get; set; }

        //deck object
        public Deck Deck { get; set; } = new Deck();

        public Dealer Dealer { get; set; } = new Dealer(new List<Card>());

        //a constant containing the number to win the game
        const int TWENTY_ONE = 21;

        //a contant containing the 3:2 ratio for a Player's payout
        const double PAYOUT = 1.5;

        /// <summary>
        /// constructor for the GameLogic class
        /// </summary>
        /// <param name="players">the list of players playing the game</param>
        public GameLogic(List<Player> players)
        {
            foreach (Player player in players) {
                this.players.Add(player);
            }
            Players = players;
        }

        /// <summary>
        /// a method for one player to draw a card
        /// </summary>
        /// <param name="player"></param>
        public void DealCard(Player player)
        {
            player.Hand.Add(Deck.DrawCard());
        }

        /// <summary>
        /// a method for the dealer to draw a card
        /// </summary>
        /// <param name="player"></param>
        public void DealCard(Dealer dealer)
        {
            dealer.Hand.Add(Deck.DrawCard());
        }

        /// <summary>
        /// a method to check the total value of a hand
        /// </summary>
        /// <returns>an int containing the total of each players hand</returns>
        public List<int> CheckHandTotal() {
            List<int> handsTotal = new List<int>();
            
            foreach (Player player in players)
            {
                int handTotal = 0;
                foreach (Card card in player.Hand)
                {
                    handTotal += card.GetFaceValue();
                }
                handsTotal.Add(handTotal);
            }

            return handsTotal;
        }

        /// <summary>
        /// a method to check to hand total of a specific Player
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public int CheckHandTotal(Player player)
        {
            int handTotal = 0;
            foreach (Card card in player.Hand)
            {
                /*if (card.Face == Face.Ace)
                {
                    //prompt user for choice regarding an ace, either 1 or 11
                    handTotal += card.GetFaceValue(true);
                }*/
                handTotal += card.GetFaceValue();
            }

            return handTotal;
        }

        /// <summary>
        /// a method to check to hand total of the Dealer
        /// </summary>
        /// <param name="dealer"></param>
        /// <returns></returns>
        public int CheckHandTotal(Dealer dealer)
        {
            int handTotal = 0;
            foreach (Card card in dealer.Hand)
            {
                /*if (card.Face == Face.Ace)
                {
                    //prompt user for choice regarding an ace
                    handTotal += card.GetFaceValue(true);
                }*/
                // TO-DO Have the Dealer dynamically pick if the Ace is 1 or 11
                handTotal += card.GetFaceValue();
            }

            return handTotal;
        }

        /// <summary>
        /// a method to check if a hand is bust
        /// </summary>
        /// <returns>a list of boolean for each player indicating true if the hand is bust or false if it is not</returns>
        public List<bool> CheckIfBust()
        {
            List<bool> isBust = new List<bool>();
            List<int> handsTotal = CheckHandTotal();
            foreach (int handTotal in handsTotal)
            {
                if (handTotal > TWENTY_ONE)
                {
                    isBust.Add(true);
                }
                else {
                    isBust.Add(false);
                }
            }
            
            return isBust;
        }

        // a method to check if a specific player's hand is a bust
        public bool CheckIfBust(Player player)
        {
            int handTotal = 0;

            // add each card's face value to the total
            foreach (Card card in player.Hand)
            {
                handTotal += card.GetFaceValue();
            }

            // if the total exceeds 21
            if (handTotal > TWENTY_ONE)
            {
                player.IsPlaying = false;
                return true; // bust
            }
            else
                return false; // not a bust
        }

        /// <summary>
        /// a method to simulate dealing cards to a player at the beginning of the game.
        /// NOTE: DealCard method is to be used when starting a new game, DrawCard is a method from the Deck class
        /// and should be used when a player draws a card on each of their turns
        /// </summary>
        public void DealCard() {
            // deal 2 cards to each player
            foreach (Player player in players) {
                for (int i = 0; i < 2; i++) {
                    player.Hand.Add(Deck.DrawCard());
                }
            }
            // deal 2 cards to the dealer
            for (int i = 0; i < 2; i++)
            {
                Dealer.Hand.Add(Deck.DrawCard());
            }
        }

        /// <summary>
        /// a method for a Player to place a bet
        /// would be called on each player that is player and obtaining their desired bet amount
        /// </summary>
        /// <param name="player"></param>
        /// <param name="chips"></param>
        public bool MakeBet(Player player, int bet)
        {
            // if the player has enough chips to place the bet
            if ((player.Chips - bet) >= 0)
            {
                player.BetAmount = bet;
                player.Chips -= bet; // subtract their chips
                return true; // player completed the bet
            }

            return false; // player could not complete the bet
        }

        /// <summary>
        /// reset each player's attributes
        /// </summary>
        public void NewGame()
        {
            Deck = new Deck();
            foreach (Player player in players)
            {
                player.IsPlaying = true;
                player.Chips = 50;
                player.BetAmount = 2;
                player.Hand = new List<Card>();
            }
            Dealer = new Dealer(new List<Card>()); // reset Dealer's Hand
            DealCard();
        }

        // reset each player's deck, but keep the chips
        public void NextRound()
        {
            Deck = new Deck();
            foreach (Player player in players)
            {
                player.IsPlaying = true;
                player.BetAmount = 2;
                player.Hand = new List<Card>();
            }
            Dealer = new Dealer(new List<Card>()); // reset Dealer's Hand
            DealCard();
        }

        public Player CheckWinner()
        {
            Dictionary<Player, int> potentialWinners = new Dictionary<Player, int>();

            for (int b=0; b<players.Count; b++)
                // for some reason, the last potential winner is not getting added to dictionary
                // if one busts, the loops comes back here and says the player who busted wins
                // if a player has 21 and a player busts, dealer wins
            {
                int handTotal = this.CheckHandTotal(players[b]);

                // if the player did not bust
                if (!this.CheckIfBust(players[b]))
                {
                    potentialWinners.Add(players[b], handTotal);
                }
                /*
                if (isBust[b] == false)
                {
                    potentialWinners.Add(players[b], handsTotal[b]);
                }*/
            }

            // if there's only 1 potential winner
            if (potentialWinners.Count == 1)
                return potentialWinners.ElementAt(0).Key; // return that 1 Player

            // if there a few possible winners
            else if (potentialWinners.Count > 1)
            {
                int[] difference = new int[potentialWinners.Count];

                // compare the differences each Player is from 21
                for (int d=0; d<potentialWinners.Count; d++)
                {
                    difference[d] = TWENTY_ONE - potentialWinners.ElementAt(d).Value;
                }

                // get the minimum value in the difference
                int minDiff = difference.Min();


                //int minCount = 0; // counter to check how many players have the minimum difference

                /*
                HashSet<int> uniqueElements = new HashSet<int>();
                bool hasDuplicates = false;

                foreach (int element in difference)
                {
                    if (!uniqueElements.Add(element))
                    {
                        hasDuplicates = true;
                        break;
                    }
                }

                // if multiple players have the same difference, the dealer wins
                if (hasDuplicates)
                {
                    return null; // Dealer wins
                }
                /*
                // check if multiple players have the minimum difference value (meaning a tie)
                for (int d=0; d<difference.Length; d++)
                {
                    if (d == minDiff)
                        minCount++;
                }
                */

                /*
                // if multiple players have the same difference, the dealer wins
                if (minCount > 1)
                    return null; // Dealer wins
                */
                // if only one player has the minimum difference
                //else
                //{
                    // winner is the player who's difference from 21 is the smallest
                    Player winner = players[Array.IndexOf(difference, minDiff)];

                    // the player must be closer to 21 than the Dealer
                    if (CheckHandTotal(winner) > CheckHandTotal(Dealer))
                    {
                        // give payout to the winner
                        winner.Chips += (int)Math.Round(winner.BetAmount * PAYOUT);

                        // return the winner, who has the smallest difference
                        return winner;
                    }
                    else
                        return null; // Dealer wins
                //}
            }

            // there are no potential winners
            else
                return null; // Dealer wins
        }

        public override string ToString()
        {
            string players = "";

            foreach (Player player in this.players)
            {
                players += player.ToString() + ", ";
            }
            return "Players: " + players;
        }
    }
}
