<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Welcome.aspx.cs" Inherits="Blackjack.Welcome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">\
    <div style="text-align:center;padding:20px;font-family:Arial, sans-serif;">
        <strong>OBJECT OF THE GAME</strong><br />
        Each participant attempts to beat the dealer by getting a count as close to 21
        as possible, without going over 21.<hr />

        <strong>CARD VALUES/SCORING</strong><br />
        It is up to each individual player if an ace is worth 1 or 11.<br />
        Face cards are 10 and any other card is its pip value.<hr />

        <strong>BETTING</strong><br />
        Before the deal begins, each player places a bet, in chips, 
        in front of him in the designated area.<br />
        Minimum and maximum limits are established on the betting, 
        and the general limits are from $2 to $500.<br /><br />

        <a href="https://www.bicyclecards.com/how-to-play/blackjack/">Game Rules Source</a>
    </div>
    <br />
    <asp:Panel ID="NumPanel" runat="server">
        <asp:Label ID="LblNum" Text="Number of Players: " runat="server" />
        <asp:TextBox TextMode="Number" runat="server" min="1" max="4" step="1" value="1" ID="PlayerNum" />
    </asp:Panel>
    <asp:Panel ID="BtnPanel" runat="server">
        <asp:Button ID="StartBtn" runat="server" Text="START GAME" OnClick="StartBtn_Click" />
    </asp:Panel>
</asp:Content>

