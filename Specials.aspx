<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Specials.aspx.cs"
    Inherits="Web2Print.UI.Specials" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="pnlMain" runat="server">
        <ContentTemplate>
            <div align="left" style="width: 740px">
                <div style="width: 740px;">
                    <br />
                    <asp:Label ID="lblTitle" runat="server" Text="Special Offers" CssClass="H1"></asp:Label>
                    <br />
                </div>
                <table>
                    
                    <tr valign="top" style="width: 100%">
                        <td colspan="3"><br />
                            <asp:DataList ID="dlCategoryList" runat="server" RepeatColumns="3" RepeatDirection="Vertical">
                                <ItemTemplate>
                                    
                                        <a href="#" class="productIconLink">
                                            <img src="images/products/a4_wall_13.png" alt="" width="160" height="160" /></a>
                                        <a href="#" class="productTitle">
                                            <%# Eval("CategoryName")%></a>
                                    
                                </ItemTemplate>
                                <ItemStyle Width="250px" HorizontalAlign="Center" Height="230px" />
                            </asp:DataList>
                        </td>
                    </tr>
                    <tr>
                        <td height="10px">
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
