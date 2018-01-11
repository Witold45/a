<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateAuction.aspx.cs" Inherits="AHWForm.CreateAuction" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="MainCreateAuction">
        <div class="upperBar">
            <div class="AuctionCreateTitleDiv">
                <asp:Label runat="server" ID="AuctionCreateTitleLabel" Font-Size="X-Large" Font-Bold="True" meta:resourcekey="AuctionCreateTitleLabelResource1"  />
            </div>
        </div>
        <div class="middle">
            <div class="LeftMiddleBar">
                <asp:Label runat="server" ID="AuctionTitleTextBoxLabel" meta:resourcekey="AuctionTitleTextBoxLabelResource1"></asp:Label><br>
                <asp:TextBox runat="server" ID="AuctionTitleTextBox" meta:resourcekey="AuctionTitleTextBoxResource1"></asp:TextBox><br>
                <asp:Label runat="server" ID="MinimalPriceTextBoxLabel" meta:resourcekey="MinimalPriceTextBoxLabelResource1"></asp:Label><br>
                <asp:TextBox runat="server" ID="MinimalPriceTextBox" meta:resourcekey="MinimalPriceTextBoxResource1"></asp:TextBox><br>
                <asp:Label runat="server"  ID="ExpiresInTextBoxLabel" meta:resourcekey="ExpiresInTextBoxLabelResource1"></asp:Label><br>
                <asp:DropDownList runat="server" ID="ExpiresInDropDown" ></asp:DropDownList><br>
                <asp:Label runat="server"  ID="DescriptionTextBoxLabel" meta:resourcekey="DescriptionTextBoxLabelResource1"></asp:Label><br>
                <asp:TextBox runat="server" ID="DescriptionTextBox" meta:resourcekey="DescriptionTextBoxResource1"></asp:TextBox>
            </div>
            <div class="RightMiddleBar">
                <asp:TreeView ID="NewAuctionTreeView" runat="server" Font-Names="Verdana" Font-Size="12px" 
                ForeColor="#F48110" SkinID="NonPostBackTree" Height="296px" Width="415px" SelectedNodeStyle-ForeColor="blue" meta:resourcekey="NewAuctionTreeViewResource1" >
                
                <NodeStyle CssClass="tree_node_text" />
                <ParentNodeStyle CssClass="tree_node_text" />
                <RootNodeStyle CssClass="tree_node_text" />
                <HoverNodeStyle CssClass="tree_node_text" />
                    
                </asp:TreeView>
            </div>
        </div>
        <div class="downBar">
            <asp:TextBox runat="server" TextMode="MultiLine" ID="DescriptionLongTextBox" meta:resourcekey="DescriptionLongTextBoxResource1"></asp:TextBox><br />
            <asp:Button runat="server" ID="PassNewAuctionButton" OnClick="PassNewAuctionButton_Click" meta:resourcekey="PassNewAuctionButtonResource1"/>
            
        </div>
    </div>
</asp:Content>
