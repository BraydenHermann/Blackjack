using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Blackjack.Entities;
using System.Text;
using Blackjack.BusinessClasses;

namespace Blackjack
{
    public partial class Welcome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //asp button styling
            StartBtn.Style.Add("background-color", "darkslateblue");
            StartBtn.Style.Add("border", "none");
            StartBtn.Style.Add("box-shadow", "0 8px 16px 0 rgba(0,0,0,0.2), 0 6px 20px 0 rgba(0,0,0,0.19)");
            StartBtn.Style.Add("color", "gold");
            StartBtn.Style.Add("text-align", "center");
            StartBtn.Style.Add("padding", "15px 32px");
            StartBtn.Style.Add("font-size", "16px");
            StartBtn.Style.Add("display", "inline-block");
            StartBtn.Style.Add("border-radius", "25px");

            BtnPanel.Style.Add("text-align", "center");
            BtnPanel.Style.Add("padding", "10px");

            NumPanel.Style.Add("text-align", "center");
            LblNum.Style.Add("font-family", "Arial, sans-serif");

            PlayerNum.Style.Add("margin", "20px");
            PlayerNum.Style.Add("width", "30px");
        }

        protected void StartBtn_Click(object sender, EventArgs e)
        {
            //string queryString = "PlayerNum=" + PlayerNum.Text;
            int playerCount = int.Parse(PlayerNum.Text);

            List<Player> players = new List<Player>();

            for (int i = 0; i < playerCount; i++)
            {
                int playerNum = i + 1;
                List<Card> hand = new List<Card>();
                players.Add(new Player("Player " + playerNum.ToString(), 50, hand, false));
            }
            players[0].IsPlaying = true;

            GameLogic gameLogic = new GameLogic(players);
            gameLogic.NewGame();
            Session["GameLogic"] = gameLogic;

            Response.Redirect("/WebPages/Game.aspx?", false);
        }
    }
}