<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Game.aspx.cs" Inherits="Blackjack.Game" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="GameIF">
        <div>
            <asp:Label Text="Dealer's Hand:" runat="server" Font-Bold="true" /><br />
            <asp:Label ID="LblDealerHand" runat="server" Text=""></asp:Label>
            <asp:Image ID="DealerCard" runat="server" /><br />
            <asp:Label ID="DealerCount" runat="server" Font-Bold="true" />
        </div>
        <br />
        <asp:Button ID="Stand" runat="server" Text="Stand" OnClick="Stand_Click" />
        <asp:Button ID="Hit" runat="server" Text="Hit" OnClick="Hit_Click" />
        <asp:Button ID="ChangeAce" runat="server" Text="Change Ace to 11" OnClick="Change_Ace" />
        <br />
        <asp:Button ID="NextRound" runat="server" Text="Next Round" OnClick="Next_Round" />
        <asp:Button ID="NewGame" runat="server" Text="New Game" OnClick="New_Game" /> 
        <br />
        <asp:Button ID="Bet" runat="server" Text="Bet" OnClick="Bet_Click" />
        <asp:TextBox ID="PlayerBet" TextMode="Number" runat="server" min="2" value="2" step="1"/>
        <br />
        <br /><br />
        <asp:Label ID="GameMessage" runat="server" Text="" Font-Bold="True" ForeColor="Red"></asp:Label><br /><br />
        <!--
        <asp:Label ID="PlayerTurnLbl" runat="server" Text="" Font-Bold="True"></asp:Label><br />
        <br />
        Current Player's Hand:<br />
        <asp:Label ID="LblCurrentPlayerHand" runat="server" Text=""></asp:Label><br />
        <asp:Image ID="CurrentPlayersHand" runat="server" /><br />
        -->
        <asp:Label ID="CurrentPlayersCount" runat="server" Font-Bold="True"/>
        <br /><br />
        <div class="GameTurnTracker">
            <asp:Table ID="tblPlayers" runat="server" Width="50%" HorizontalAlign="Center"></asp:Table>
            <asp:Table ID="tblCardImages" runat="server" Width="50%" HorizontalAlign="Center"></asp:Table>
        </div>
        <br />
        <asp:Button ID="MainMenu" CssClass="GameBtns=" runat="server" Text="Main Menu" OnClick="Main_Menu" />
    </div>
</asp:Content>
