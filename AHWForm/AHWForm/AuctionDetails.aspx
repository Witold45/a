<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AuctionDetails.aspx.cs" Inherits="AHWForm.AuctionDetails" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <div class="MainContainer">
        <div class="UpperInfo">
            <div class="Title">
                <asp:Label ID="AuctionTitle" runat="server"  Font-Size="34pt" meta:resourcekey="AuctionTitleResource1" />
            </div>
            <div class="Photo">

            </div>
            <div class="BasicInfo">
                <div class="Price">
                    <asp:Label ID="Price" runat="server" meta:resourcekey="PriceResource1" />
                </div>

                <div class="BidButton">
                    <asp:Button ID="Bid" runat="server" OnClick="Bid_Click" meta:resourcekey="BidResource1" />
                </div>

                <div class="Creator">
                    <div class="CreatorName">
                        <asp:Label ID="CreatorName" runat="server" meta:resourcekey="CreatorNameResource1"/>
                    </div>
                    
                </div>

                <div class="Description">
                    <asp:Label ID="AuctionShortDescription" runat="server" meta:resourcekey="AuctionShortDescriptionResource1"/>
                </div>
            </div>
        </div>
        <div class="MiddleInfo">
            <div class="AuctionText">
                <asp:TextBox runat="server" ID="AuctionLongDescription" TextMode="MultiLine" meta:resourcekey="AuctionLongDescriptionResource1" />
            </div>
        </div>
        <div class="LowerInfo">
            <div class="Bidders">

            </div>
        </div>
    </div>


</asp:Content>
