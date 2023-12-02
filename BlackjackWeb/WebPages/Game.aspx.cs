using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Blackjack.BusinessClasses;
using Blackjack.Entities;

namespace Blackjack
{
    public partial class Game : System.Web.UI.Page
    {
        List<Player> players;
        GameLogic gameLogic;
        Dealer dealer;

        protected void Page_Load(object sender, EventArgs e)
        {
            // asp button styling
            Stand.Style.Add("background-color", "darkslateblue");
            Stand.Style.Add("border", "none");
            Stand.Style.Add("box-shadow", "0 8px 16px 0 rgba(0,0,0,0.2), 0 6px 20px 0 rgba(0,0,0,0.19)");
            Stand.Style.Add("color", "gold");
            Stand.Style.Add("text-align", "center");
            Stand.Style.Add("padding", "15px 32px");
            Stand.Style.Add("font-size", "16px");
            Stand.Style.Add("display", "inline-block");
            Stand.Style.Add("border-radius", "25px");

            Hit.Style.Add("background-color", "darkslateblue");
            Hit.Style.Add("border", "none");
            Hit.Style.Add("box-shadow", "0 8px 16px 0 rgba(0,0,0,0.2), 0 6px 20px 0 rgba(0,0,0,0.19)");
            Hit.Style.Add("color", "gold");
            Hit.Style.Add("text-align", "center");
            Hit.Style.Add("padding", "15px 32px");
            Hit.Style.Add("font-size", "16px");
            Hit.Style.Add("display", "inline-block");
            Hit.Style.Add("border-radius", "25px");

            ChangeAce.Style.Add("background-color", "darkslateblue");
            ChangeAce.Style.Add("border", "none");
            ChangeAce.Style.Add("box-shadow", "0 8px 16px 0 rgba(0,0,0,0.2), 0 6px 20px 0 rgba(0,0,0,0.19)");
            ChangeAce.Style.Add("color", "gold");
            ChangeAce.Style.Add("text-align", "center");
            ChangeAce.Style.Add("padding", "15px 32px");
            ChangeAce.Style.Add("font-size", "16px");
            ChangeAce.Style.Add("display", "inline-block");
            ChangeAce.Style.Add("border-radius", "25px");

            Bet.Style.Add("background-color", "darkslateblue");
            Bet.Style.Add("border", "none");
            Bet.Style.Add("box-shadow", "0 8px 16px 0 rgba(0,0,0,0.2), 0 6px 20px 0 rgba(0,0,0,0.19)");
            Bet.Style.Add("color", "gold");
            Bet.Style.Add("text-align", "center");
            Bet.Style.Add("padding", "15px 32px");
            Bet.Style.Add("font-size", "16px");
            Bet.Style.Add("display", "inline-block");
            Bet.Style.Add("border-radius", "25px");

            NextRound.Style.Add("background-color", "darkslateblue");
            NextRound.Style.Add("border", "none");
            NextRound.Style.Add("box-shadow", "0 8px 16px 0 rgba(0,0,0,0.2), 0 6px 20px 0 rgba(0,0,0,0.19)");
            NextRound.Style.Add("color", "gold");
            NextRound.Style.Add("text-align", "center");
            NextRound.Style.Add("padding", "15px 32px");
            NextRound.Style.Add("font-size", "16px");
            NextRound.Style.Add("display", "inline-block");
            NextRound.Style.Add("border-radius", "25px");

            NewGame.Style.Add("background-color", "darkslateblue");
            NewGame.Style.Add("border", "none");
            NewGame.Style.Add("box-shadow", "0 8px 16px 0 rgba(0,0,0,0.2), 0 6px 20px 0 rgba(0,0,0,0.19)");
            NewGame.Style.Add("color", "gold");
            NewGame.Style.Add("text-align", "center");
            NewGame.Style.Add("padding", "15px 32px");
            NewGame.Style.Add("font-size", "16px");
            NewGame.Style.Add("display", "inline-block");
            NewGame.Style.Add("border-radius", "25px");

            PlayerBet.Style.Add("text-align", "center");
            PlayerBet.Style.Add("font-family", "Arial, sans-serif");

            MainMenu.Style.Add("background-color", "darkslateblue");
            MainMenu.Style.Add("border", "none");
            MainMenu.Style.Add("box-shadow", "0 8px 16px 0 rgba(0,0,0,0.2), 0 6px 20px 0 rgba(0,0,0,0.19)");
            MainMenu.Style.Add("color", "gold");
            MainMenu.Style.Add("text-align", "center");
            MainMenu.Style.Add("padding", "15px 32px");
            MainMenu.Style.Add("font-size", "16px");
            MainMenu.Style.Add("display", "inline-block");
            MainMenu.Style.Add("border-radius", "25px");

            //if (!IsPostBack)
            //{
            gameLogic = (GameLogic)Session["GameLogic"];
            players = gameLogic.Players;
            dealer = gameLogic.Dealer;
            //PlayerTurnLbl.Text = "Player 1's Turn";
            //LblCurrentPlayerHand.Text = gameLogic.Players[0].PrintHand(); // print the first player's hand
            //CurrentPlayersCount.Text = gameLogic.CheckHandTotal(gameLogic.Players[0]).ToString(); // display the count of the first player's hand
            //GetCardImages(gameLogic.Players[0]); // get the first player's card images

            ShowRoundOverBtns(false); // hide the next round and new game buttons
            this.RefreshList();
        }

        // a method to refresh the list of each player's info
        public void RefreshList()
        {
            tblPlayers.Rows.Clear(); // clear previous player data

            TableRow headers = new TableRow();
            headers.Font.Bold = true;
            TableCell name = new TableCell{ Text = "Name", };
            headers.Cells.Add(name);
            TableCell chips = new TableCell { Text = "Chips", };
            headers.Cells.Add(chips);
            TableCell betAmount = new TableCell { Text = "Bet Amount", };
            headers.Cells.Add(betAmount);
            TableCell hand = new TableCell { Text = "Hand", };
            headers.Cells.Add(hand);
            TableCell count = new TableCell { Text = "Count", };
            headers.Cells.Add(count);
            tblPlayers.Rows.Add(headers);

            //for loop to create a table that will contain player information
            //dynamically creates rows based on how many players there are that will contain player info
            for (int r = 0; r < players.Count; r++)
            {
                TableRow tr = new TableRow();

                // player name
                TableCell tc1 = new TableCell { Text = players[r].Name };
                tr.Cells.Add(tc1);

                // player chips
                TableCell tc2 = new TableCell { Text = players[r].Chips.ToString() };
                tr.Cells.Add(tc2);

                // player bet amount
                TableCell tc3 = new TableCell { Text = players[r].BetAmount.ToString() };
                tr.Cells.Add(tc3);

                // player hand
                TableCell tc4 = new TableCell { Text = players[r].PrintHand() };
                tr.Cells.Add(tc4);

                // player count
                TableCell tc5 = new TableCell
                {
                    Text = gameLogic.CheckHandTotal(gameLogic.Players[r]).ToString() // display the count of the current player's hand
                };

                // if the player busted, display the count in red
                if (Int32.Parse(tc5.Text) > 21)
                {
                    tc1.ForeColor = Color.Red;
                    //tc1.Font.Bold = true;

                    tc2.ForeColor = Color.Red;
                    //tc2.Font.Bold = true;

                    tc3.ForeColor = Color.Red;
                    //tc3.Font.Bold = true;

                    tc4.ForeColor = Color.Red;
                    //tc4.Font.Bold = true;

                    tc5.ForeColor = Color.Red;
                    //tc5.Font.Bold = true;
                }
                // if the player has exactly 21
                else if (Int32.Parse(tc5.Text) == 21)
                {
                    tc1.ForeColor = Color.Green;
                    //tc1.Font.Bold = true;

                    tc2.ForeColor = Color.Green;
                    //tc2.Font.Bold = true;

                    tc3.ForeColor = Color.Green;
                    //tc3.Font.Bold = true;

                    tc4.ForeColor = Color.Green;
                    //tc4.Font.Bold = true;

                    tc5.ForeColor = Color.Green;
                    //tc5.Font.Bold = true;
                }

                // hide or show the change ace button depending on if the current player has aces
                int currentPlayer = this.GetTurn();

                // if it's not the Dealer's turn
                if (currentPlayer != 999) {
                    Card ace = players[currentPlayer].HasAce(players[currentPlayer]);

                    // if the current player has an ace
                    if (ace != null)
                        ChangeAce.Visible = true;
                    else
                        ChangeAce.Visible = false;

                    // highlight the current player's row
                    if (tc1.Text == players[currentPlayer].Name)
                    {
                        tc1.ForeColor = Color.LightGoldenrodYellow;
                        tc2.ForeColor = Color.LightGoldenrodYellow;
                        tc3.ForeColor = Color.LightGoldenrodYellow;
                        tc4.ForeColor = Color.LightGoldenrodYellow;
                        tc5.ForeColor = Color.LightGoldenrodYellow;
                    }
                }

                tr.Cells.Add(tc5);
                tblPlayers.Rows.Add(tr);

                LblDealerHand.Text = gameLogic.Dealer.PrintHand();
                DealerCount.Text = "Count: " + gameLogic.CheckHandTotal(dealer).ToString(); // display the count of the Dealer's hand

                this.GetTurn();
            }
        }
        protected void Stand_Click(object sender, EventArgs e)
        {
            gameLogic = (GameLogic)Session["GameLogic"]; // make sure its the same gameLogic instance
            
            GameMessage.Text = "";
            int currentPlayer = this.GetTurn();

            // if it's not the Dealer's turn
            if (currentPlayer != 999)
            {
                //subtract the default bet amount in case it wasn't changed
                int betAmount = Int32.Parse(PlayerBet.Text);
                gameLogic.MakeBet(players[currentPlayer], betAmount);

                players[currentPlayer].IsPlaying = false;
                this.NextPlayer(); // it's now the next Player's turn
            }

            PlayerBet.Text = "2"; // set the default bet back to 2
            this.RefreshList();
            Session["GameLogic"] = gameLogic;
        }

        protected void Hit_Click(object sender, EventArgs e)
        {
            gameLogic = (GameLogic)Session["GameLogic"]; // make sure its the same gameLogic instance
            
            GameMessage.Text = "";
            int currentPlayer = this.GetTurn();

            // if it's not the Dealer's turn
            if (currentPlayer != 999)
            {
                
                // if the current player did not already bust
                if (!gameLogic.CheckIfBust(players[currentPlayer]))
                {
                
                    gameLogic.DealCard(players[currentPlayer]); // deal a card to the current player

                    // if the current player busted after being dealt a card
                    if (gameLogic.CheckIfBust(players[currentPlayer]))
                    {
                        //subtract the default bet amount if it wasn't changed
                        int betAmount = Int32.Parse(PlayerBet.Text);
                        gameLogic.MakeBet(players[currentPlayer], betAmount);

                        GameMessage.ForeColor = Color.Red;
                        GameMessage.Text = players[currentPlayer].Name + " BUSTED!";
                        players[currentPlayer].IsPlaying = false;
                        this.NextPlayer();
                    }
                }
                else
                {
                    GameMessage.ForeColor = Color.Red;
                    GameMessage.Text = players[currentPlayer].Name + " BUSTED!";
                    players[currentPlayer].IsPlaying = false;
                    this.NextPlayer();
                }
            }
            //else
                //gameLogic.DealCard(dealer); // deal a card to the Dealer

            this.RefreshList();
            Session["GameLogic"] = gameLogic;
        }

        // start a new round
        protected void Next_Round(object sender, EventArgs e)
        {
            gameLogic = (GameLogic)Session["GameLogic"]; // make sure its the same gameLogic instance

            GameMessage.Text = "";
            gameLogic.NextRound(); // start a new round
            this.RefreshList();
            Session["GameLogic"] = gameLogic;
        }

        // start a new game
        protected void New_Game(object sender, EventArgs e)
        {
            gameLogic = (GameLogic)Session["GameLogic"]; // make sure its the same gameLogic instance

            GameMessage.Text = "";
            gameLogic.NewGame(); // start a new game
            this.RefreshList();
            Session["GameLogic"] = gameLogic;
        }

        // place a bet
        protected void Bet_Click(object sender, EventArgs e)
        {
            gameLogic = (GameLogic)Session["GameLogic"]; // make sure its the same gameLogic instance
            
            GameMessage.Text = "";
            int currentPlayer = this.GetTurn();
            int playerBet = int.Parse(PlayerBet.Text);

            // if it's not the Dealer's turn
            if (currentPlayer != 999)
            {
                // if the current player did not already bust
                if (!gameLogic.CheckIfBust(players[currentPlayer]))
                {
                    // if the player did not have enough chips to make the bet
                    if (!gameLogic.MakeBet(players[currentPlayer], playerBet))
                        GameMessage.ForeColor = Color.Red;
                        GameMessage.Text = players[currentPlayer].Name + " does not have enough chips!";
                }
                else
                {
                    GameMessage.ForeColor = Color.Red;
                    GameMessage.Text = players[currentPlayer].Name + " BUSTED!";
                    ShowRoundOverBtns(true); // show the next round and new game buttons
                }
            }
            PlayerBet.Text = "2";

            this.RefreshList();
            Session["GameLogic"] = gameLogic;
        }

        // change ace
        protected void Change_Ace(object sender, EventArgs e)
        {
            gameLogic = (GameLogic)Session["GameLogic"]; // make sure its the same gameLogic instance

            GameMessage.Text = "";
            int currentPlayer = this.GetTurn();

            Card ace = players[currentPlayer].HasAce(players[currentPlayer]);

            // if the current player has an ace
            if (ace != null)
            {
                // go through each card in the player's deck
                foreach (Card card in players[currentPlayer].Hand)
                {
                    // if the card is the same as the old ace
                    if (card == ace)
                    {
                        /*
                        int newAceValue = ace.ChangeAceValue(ace).GetFaceValue(); // get the value of the new ace

                        // if the player will bust after changing the ace value
                        if ((gameLogic.CheckHandTotal(gameLogic.Players[currentPlayer]) + newAceValue) > 21)
                        {
                            GameMessage.Text = players[currentPlayer].Name + " Will Bust if Ace Value Changes";
                        }
                        */
                        // if the player will not bust after changing the ace value
                        //else
                        //{
                            card.ChangeAceValue(card);
                            GameMessage.ForeColor = Color.Green;
                            GameMessage.Text = players[currentPlayer].Name + "'s Ace Value Changed";
                        //}
                    }
                }
            }

            else
            {
                GameMessage.ForeColor = Color.Red;
                GameMessage.Text = players[currentPlayer].Name + " Does Not Have Aces Available to Change";
            }

            this.RefreshList();
            Session["GameLogic"] = gameLogic;
        }
        
        // a method to hide the stand, hit change ace and bet buttons when false, but show the next round and new game buttons when true
        public void ShowRoundOverBtns (bool show)
        {
            // hide the stand, hit, change ace and bet buttons and show the next round and new game buttons
            if (show == true)
            {
                Stand.Visible = false;
                Hit.Visible = false;
                ChangeAce.Visible = false;
                Bet.Visible = false;
                PlayerBet.Visible = false;

                NextRound.Visible = true;
                NewGame.Visible = true;
            }

            // hide the next round and new game buttons and show the stand, hit, change ace and bet buttons
            else
            {
                Stand.Visible = true;
                Hit.Visible = true;
                ChangeAce.Visible = true;
                Bet.Visible = true;
                PlayerBet.Visible = true;

                NextRound.Visible = false;
                NewGame.Visible = false;
            }
        }

        /// <summary>
        /// a method that returns which Player is currently playing
        /// </summary>
        /// <returns></returns>
        public int GetTurn()
        {
            gameLogic = (GameLogic)Session["GameLogic"]; // make sure its the same gameLogic instance

            // for each player in the game
            for (int p=0; p<players.Count; p++)
            {
                if (players[p].IsPlaying)
                {
                    PlayerTurnLbl.Text = "Player " + (p+1).ToString() + "'s Turn";
                    LblCurrentPlayerHand.Text = players[p].PrintHand();
                    CurrentPlayersCount.Text = "Player " + (p+1) + "'s Turn - \nCount: " + gameLogic.CheckHandTotal(players[p]).ToString(); // display the count of the current player's hand
                    //this.GetCardImages(gameLogic.Players[p]); // get the current player's card images

                    return p; // returns which player is playing
                }
            }
            return 999; // it was none of the Player's turn, so use this value to say it's the Dealer's turn
        }

        // go back to the main meny
        protected void Main_Menu(object sender, EventArgs e)
        {
            Response.Redirect("/WebPages/Welcome.aspx?", false);
        }

        /*
        public void GetCardImages(Player p)
        {
            TableRow tr = new TableRow(); // we only need 1 row to show all cards

            // create an array of table cells to match the size of the player's hand
            TableCell[] handLength = new TableCell[p.Hand.Count()];

            // create an array of image paths to match each card in the player's hand
            String[] cardImage = new String[handLength.Length];

            // for each card in the player's hand
            // for loop to create a table that will contain the players' hand images
            // dynamically creates cells based on how many cards there are
            for (int c = 0; c < handLength.Length; c++)
            {
                //tr.Cells.Add(handLength[c]);
                //handLength[c].Text = p.Hand[c].GetFaceValue().ToString();
                //cardImage[c] = gameLogic.Players[p].Hand[c];//p.Hand<c>
                // cell11.Text = string.Format("<img src='images/close_icon.png' />");

                //handLength[c].Text = string.Format("<img src='images/" + 
                //    p.Hand[c].ToString() + ".png' />");
                //handLength[c].Text = p.Hand[c].ToString();

                //cardImage[c] = p.Hand[c].ToString();
                //handLength[c].Text = cardImage[c];
                //handLength[c].Text = p.Hand[c].ToString();

            }
            tblCardImages.Rows.Add(tr);
        }*/

        /// <summary>
        /// a method to make it the next Player's turn
        /// </summary>
        public void NextPlayer()
        {
            gameLogic = (GameLogic)Session["GameLogic"]; // make sure its the same gameLogic instance

            int currentPlayer = this.GetTurn();
            //players[currentPlayer].IsPlaying = false; // current player is no longer playing

            // if not every player is standing, continue rounds
            bool allStanding = true;

            foreach (Player p in players)
            {
                // if any players do not have any chips
                /*
                if (!gameLogic.MakeBet(players[currentPlayer], playerBet))
                {
                    p.IsPlaying = false; // player can no longer play
                }*/

                // if any player is playing
                if (p.IsPlaying == true)
                    allStanding = false; // not all players are standing

                // no players are playing
                //else
                    //allStanding = true;
            }

            // round continues
            if (!allStanding)
            {
                // if not all players have played
                if (currentPlayer + 1 <= players.Count)
                {
                    // it is no one else's turn
                    //foreach (Player p in players)
                    //{
                        //p.IsPlaying = false;
                    //}
                    currentPlayer += 1; // next player's turn
                    //players[currentPlayer].IsPlaying = true;
                    //players[currentPlayer - 1].IsPlaying = false;
                }

                // if it was the Dealer's turn
                else if (currentPlayer == 999)
                {
                    //// it is no one else's turn
                    //foreach (Player p in players)
                    //{
                        //p.IsPlaying = false;
                    //}
                    currentPlayer = 0; // now it's the first Player's turn
                    //players[currentPlayer].IsPlaying = true;
                    //players[players.Count - 1].IsPlaying = false;
                }
                //else
                //{
                    //foreach (Player p in gameLogic.Players)
                    //{
                        //p.IsPlaying = false;
                    //}
                //}
            }
            // no one is playing anymore, round ends
            else
            {
                // Dealer draws Cards while value < 16
                while (gameLogic.CheckHandTotal(dealer) < 16)
                {
                    gameLogic.DealCard(dealer);
                    //dealer.Hand.Add(gameLogic.Deck.DrawCard());
                    DealerCount.Text = "Count: " + gameLogic.CheckHandTotal(dealer).ToString(); // display the count of the Dealer's hand
                }

                // no Player won, Dealer wins
                if (gameLogic.CheckWinner() == null)
                {
                    GameMessage.ForeColor = Color.Red;
                    GameMessage.Text = "DEALER WINS!";
                    this.RefreshList();

                    /*
                    NextRound.Visible = true; // enable the next round button
                    NewGame.Visible = true; // enable the new game button
                    */

                    ShowRoundOverBtns(true); // show the next round and new game buttons
                }

                // if a Player won
                else
                {
                    Player winner = gameLogic.CheckWinner();
                    GameMessage.ForeColor = Color.Green;
                    GameMessage.Text = winner.Name + " WINS!";

                    foreach (Player p in players)
                    {
                        if(p.Name == winner.Name)
                        {
                            p.Chips = winner.Chips;
                        }
                    }

                    this.RefreshList();

                    /*
                    NextRound.Visible = true; // enable the next round button
                    NewGame.Visible = true; // enable the new game button
                    */

                    ShowRoundOverBtns(true); // show the next round and new game buttons
                }
            }
            Session["GameLogic"] = gameLogic;
        }

        /// <summary>
        /// a method to keep track of the turns, if it is the players turn, 
        /// then set the CurrentPlayersHand image to the current players hand
        /// </summary>
        public void TurnTracker()
        {
            /*
            int i = 0;
            foreach (TableRow row in tblPlayers.Rows)
            {
                row.Style.Add("background", "white");
            }
            foreach (Player player in players) {
                
                if (player.IsPlaying) {
                    CurrentPlayersHand = new Image();//image of players hand                   
                    foreach (TableRow row in tblPlayers.Rows) {
                        if (row.Cells[0].Equals(player.Name))
                        {
                            row.Style.Add("background", "yellow");
                        }
                    }

                }
                i++;
            }

            int index = 0;

            foreach (Player player in players)
            {
                if (player.IsPlaying)
                    index = players.IndexOf(player);
            }
            
            foreach (TableRow row in tblPlayers.Rows)
            {
            for (int row = 1; row < tblPlayers.Rows.Count; row++) {
                tblPlayers.Rows[row].Style.Add("background", "white");
                tblPlayers.Rows[index - 1].Style.Add("background", "yellow");
            } */
        }
    }
}