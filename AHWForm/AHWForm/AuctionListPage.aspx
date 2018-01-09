<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AuctionListPage.aspx.cs" Inherits="AHWForm.AuctionListPage" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
   
    <asp:ListView ID="AuctionList" runat="server" ItemType="AHWForm.Models.Auction" SelectMethod="AuctionList_GetData"  DataKeyNames="Id" >

        <EmptyDataTemplate>
            <table runat="server" style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;">
                <tr>
                    <td>No data was returned.</td>
                </tr>
            </table>
        </EmptyDataTemplate>

        <ItemTemplate>
            <tr style="background-color: #FFFBD6;color: #333333;" "  >
                <td>
                    <asp:DynamicControl runat="server" DataField="Title" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="StartPrice" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="EndingPrice" Mode="ReadOnly" />
                </td>
                <td>
                    <asp:DynamicControl runat="server" DataField="DateCreated" Mode="ReadOnly" />
                </td>
                
    
                <td>
                    <asp:HyperLink ID="hpLinkUrl" runat="server" NavigateUrl='<%# Eval("Id", "~/AuctionDetails.aspx?Id={0}") %>' >
                    <asp:DynamicControl runat="server" DataField="Description" Mode="ReadOnly" />
                    </asp:HyperLink >
                </td>
                
            </tr>
        </ItemTemplate>
        <LayoutTemplate>
            <table runat="server">
                <tr runat="server">
                    <td runat="server">
                        <table id="itemPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;">
                            <tr runat="server" style="background-color: #FFFBD6;color: #333333;">
                                <th runat="server">Title</th>
                                <th runat="server">StartPrice</th>
                                <th runat="server">EndingPrice</th>
                                <th runat="server">DateCreated</th>
                                <th runat="server">Description</th>
                            </tr>
                            <tr id="itemPlaceholder" runat="server">
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server">
                    <td runat="server" style="text-align: center;background-color: #FFCC66;font-family: Verdana, Arial, Helvetica, sans-serif;color: #333333;">
                        <asp:DataPager ID="DataPager1" runat="server">
                            <Fields>
                                <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" />
                                <asp:NumericPagerField />
                                <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" />
                            </Fields>
                        </asp:DataPager>
                    </td>
                </tr>
            </table>
        </LayoutTemplate>
    </asp:ListView>
    

</asp:Content>
