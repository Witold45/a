<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AuctionDetails.aspx.cs" Inherits="AHWForm.AuctionDetails" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <div class="MainContainer">
        <div class="UpperInfo">
            <div class="Title">
                <asp:Label ID="AuctionTitle" runat="server"  Font-Size="34pt" meta:resourcekey="AuctionTitleResource1" />
            </div>
            <div class="PhotoAndRightBar">
                <div class="Photo">

            </div>
                <div class="BasicInfo">
                <div class="Price">
                    <asp:Label ID="PriceLabel" runat="server" Font-Size="24pt" meta:resourcekey="PriceLabel"/>
                    <asp:Label ID="Price" runat="server" Font-Size="24pt" meta:resourcekey="PriceResource1" />
                    <asp:Label ID="Currency" runat="server" Font-Size="24pt"  meta:resourcekey="Currency"/>
                </div>

                <div class="Creator">
                    <div class="CreatorName">
                        <asp:Label ID="CreatorNameLabel" runat="server" Font-Size="24pt" meta:resourcekey="UserNameLabel"/>
                        <asp:Label ID="CreatorName" runat="server" Font-Size="24pt" meta:resourcekey="CreatorNameResource1"/>
                    </div>
                    
                </div>

                <div class="Description">
                    <asp:Label ID="AuctionShortDescription" Font-Size="16pt" runat="server" meta:resourcekey="AuctionShortDescriptionResource1"/>
                </div>

                <div class="DateCreated">
                    <asp:Label ID="AuctionCreated" runat="server" Font-Size="24pt" meta:resourcekey="AuctionCreated"/>
                    <asp:Label ID="AuctionCreatedLabel" Font-Size="24pt" runat="server" />
                </div>

                 <div class="DateExpires">
                    <asp:Label ID="AuctionExpires" runat="server" Font-Size="24pt" meta:resourcekey="AuctionEndsAt"/>
                    <asp:Label ID="AuctionExpiresLabel" Font-Size="24pt" runat="server" />
                </div>

                <div class="BidButtonContainer">
                    <asp:Button ID="Bid" runat="server" CssClass="BidButton" OnClick="Bid_Click" meta:resourcekey="BidResource1" />
                </div>
            </div>
            </div>
        </div>
        <div class="MiddleInfo">
            <%--<div class="AuctionText">--%>
                <asp:TextBox runat="server"  ID="AuctionLongDescription" CssClass="AuctionText" TextMode="MultiLine" ReadOnly="true" />
            <%--</div>--%>
        </div>
        <div class="LowerInfo">
            <div class="Bidders">

            </div>
        </div>
    </div>


</asp:Content>
