<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QuickLinks.ascx.cs"
    Inherits="Web2Print.UI.Controls.QuickLinks" %>

<div id="FirstSect" runat="server" class="footer_left_portion_division col-md-3 col-sm-6 col-xs-12 col-lg-3 themeFontColor float_left_simple">
    <div class="quicklinkHeading"><asp:Label ID="lblQuickLinks1" runat="server" Text="Label" CssClass="heading_small_orange Fsize12 themeFontColor" Style="font-weight:bolder; text-transform: uppercase;"></asp:Label></div>
    <asp:BulletedList CssClass=" uSecPageList themeFontColor" ID="lstQuickLinks1" runat="server" DisplayMode="HyperLink">
    </asp:BulletedList>
</div>


<div id="SecSect" runat="server" class="footer_left_portion_division col-md-3 col-sm-6 col-xs-12 col-lg-3 themeFontColor float_left_simple">
    <div class="quicklinkHeading"><asp:Label ID="lblQuickLinks2" runat="server" Text="Label" CssClass="heading_small_orange themeFontColor Fsize12" Style="font-weight:bolder; text-transform: uppercase;"></asp:Label></div>
    <asp:BulletedList CssClass="uSecPageList  themeFontColor" ID="lstQuickLinks2" runat="server" DisplayMode="HyperLink">
    </asp:BulletedList>
</div>
<div id="ThirdSect" runat="server" class="footer_left_portion_division col-md-3 col-sm-6 col-xs-12 col-lg-3 themeFontColor float_left_simple">
    <div class="quicklinkHeading"><asp:Label ID="lblQuickLinks3" runat="server" Text="Label" CssClass="heading_small_orange themeFontColor Fsize12" Style="font-weight:bolder; text-transform: uppercase;"></asp:Label></div>
    <asp:BulletedList CssClass="uSecPageList  themeFontColor" ID="lstQuickLinks3" runat="server" DisplayMode="HyperLink">
    </asp:BulletedList>
</div>
<div class="footer_left_portion_division col-md-3 col-xs-12 col-sm-6  col-lg-3  float_left_simple">
    <a href="/ContactUs.aspx"><asp:Label ID="lblContactInfo" runat="server" Text="CONTACT INFO" CssClass="heading_small_orange ColorContUsQL  Fsize12" Style="font-weight:bolder; text-transform: uppercase;"></asp:Label></a>
    <asp:BulletedList CssClass="uSecPageList ColorContUsQL  lineHeightCUList" ID="lstContctInfo" runat="server" DisplayMode="Text">
    </asp:BulletedList>
    <div id="Div1" class="uSecPageList CreditCardCS" style=" width:200px; margin-top:20px;"></div>
  
   
</div>
<%--<div class="footer_left_portion_division_PM float_left_simple">
    <asp:Label ID="lblPymentMethod" runat="server" Text="PAYMENT METHOD" CssClass="heading_small_orange PaymentMethodQL themeFontColor Fsize12" Style="font-weight:bolder; text-transform: uppercase;"></asp:Label>
    <div id="paymentImg" class="uSecPageList CreditCardCS" style=" width:200px; margin-top:20px;"></div>
    
</div>--%>
<div class="clearBoth">&nbsp;</div>
