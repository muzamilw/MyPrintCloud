<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="TermsandConditions.aspx.cs" Inherits="Web2Print.UI.TermsandConditions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div align="left" style="width:740px">
    <div style="width: 740px;">
    <asp:Label ID="lblTitle" runat="server" Text="Terms and Conditions" CssClass="H1"></asp:Label>
    </div>
    <%--<div style="vertical-align: bottom; text-align: center">
            <asp:Label ID="lblTemp" runat="server" Height="800px"></asp:Label>
            <table>
                <tr>
                    <td align="center">
                        <asp:ImageButton ID="imgBottomLogo" runat="server" ImageUrl="~/Common/images/logo.png"
                            Height="75px" CausesValidation="false" PostBackUrl="~/Default.aspx" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DataList ID="dlBottomLinks" runat="server" RepeatColumns="7" RepeatDirection="Vertical">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkLinks" runat="server" CssClass="a4" ForeColor="Gray" Text='<%# DataBinder.Eval(Container.DataItem,"TagName")%>'
                                    PostBackUrl="#"></asp:LinkButton>
                                &nbsp;&nbsp;&nbsp;
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:DataList>
                    </td>
                </tr>
            </table>
        </div>--%>
</div>
</asp:Content>