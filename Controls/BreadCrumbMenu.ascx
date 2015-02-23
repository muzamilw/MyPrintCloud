<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BreadCrumbMenu.ascx.cs"
    Inherits="Web2Print.UI.Controls.BreadCrumbMenu" %>
<div class="BreadCrumbMenu left_align">


    <h1>
        <asp:Repeater ID="dlBreadCrumbMenu" runat="server"
            OnItemDataBound="dlBreadCrumbMenu_ItemDataBound1">
            <ItemTemplate>

                <a id="aLinkItem" runat="server" title='<%# Eval("CategoryName") %>' class="left_align">
                    <%# Eval("CategoryName") %></a>
                <asp:Label CssClass="link_active left_align" ID="lblTextItem" runat="server" Visible="false"
                    Text='<%# Eval("CategoryName") %>'></asp:Label><asp:Label ID="lblVerticalLine" runat="server" CssClass="VerticalLineBreadCrum">&nbsp;&nbsp;&raquo;&nbsp;&nbsp;</asp:Label>

            </ItemTemplate>
        </asp:Repeater>
    </h1>
    <div class="clearBoth"></div>
</div>
