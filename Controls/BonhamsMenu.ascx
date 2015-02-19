<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BonhamsMenu.ascx.cs" Inherits="Web2Print.UI.Controls.BonhamsMenu" %>

<div id="bonhamsControl" runat="server" class="content_area container">
    <div class="left_right_padding">
        <ul class="bonhamsMenu navM cursor_pointer">
            <li>
                <a href="/Default.aspx">Home</a>

            </li>
            <li>
                <a href="/UserProfile.aspx">Edit My Details</a>

            </li>


            <li>
                <a href="/SavedDesignes.aspx">Saved Projects</a>

            </li>
            <li>
                <a id="orderRedirectLink" runat="server" href="/Default.aspx">Order History</a>

            </li>
            <li>
                <a href="/Dashboard.aspx">Open Catelogue</a>

            </li>
            <li>
                <a href="/PinkCardShopCart.aspx">My Shopping Basket</a>

            </li>
            <li>
                <asp:LinkButton ID="lnkBtnSignOut" runat="server" Text="Log Out"
                    CausesValidation="false" OnClick="LogOut_Click" />
            </li>
        </ul>
        <ul id="bonhamUserStatus" class="navM" style="width: 1000px; padding-left: 0px;">
            <li style="width: 12%; ">
                 <asp:Label ID="loggedInUser" runat="server" Text="Logged in Mr William Smith"></asp:Label>
            </li>
            <li style="width: 28%; margin-right: 0px;">
                <asp:Label ID="savedDesnglbl" runat="server" Text="You have 4 Saved Project(s)"></asp:Label>

            </li>



            <li style="width: 29%; margin-right: 0px;">

                <asp:Label ID="cartItemsCount" runat="server" Text="0 Items in your basket(s)"></asp:Label>
            </li>

            <li style="width: 29%; margin-right: 0px;">
                <asp:Label ID="cartBalance" runat="server" Text="Balance $0.00"></asp:Label>

            </li>

        </ul>
        
        <div class="clearBoth">
        </div>
    </div>
</div>

