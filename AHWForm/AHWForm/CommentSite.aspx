﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CommentSite.aspx.cs" Inherits="AHWForm.CommentSite" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ListView ID="CommentList" runat="server" DataKeyNames="Id">
        <EmptyDataTemplate>
            <table runat="server" style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;">
                <tr>
                    <td>No data was returned.</td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <ItemTemplate>
            <tr style="background-color: #FFFBD6;color: #333333;" "  >
                <%--<td><%# Eval("Id") %></td>--%>
                <td><asp:HyperLink runat="server" NavigateUrl='<%# Eval("AuctionUrl") %>' ><%# Eval("AuctionTitle") %></asp:HyperLink></td>
                <td><asp:HyperLink runat="server" NavigateUrl='<%# Eval("BuyerUrl") %>' ><%# Eval("BuyerName") %></asp:HyperLink></td>
                <td><asp:HyperLink runat="server" NavigateUrl='<%# Eval("SellerUrl") %>' ><%# Eval("SellerName") %></asp:HyperLink></td>
                <td><%# Eval("Rate") %></td>
                <td><%# Eval("Description") %></td>    
            </tr>
        </ItemTemplate>
        <LayoutTemplate>
            <table runat="server">
                <tr runat="server">
                    <td runat="server">
                        <table id="itemPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;">
                            <tr runat="server" style="background-color: #FFFBD6;color: #333333;">

                                <th runat="server">Title</th>

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
