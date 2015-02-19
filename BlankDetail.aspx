<%@ Page Title="" Language="C#" MasterPageFile="MasterPages/ThemeSite.Master" AutoEventWireup="true"
    CodeBehind="BlankDetail.aspx.cs" Inherits="Web2Print.UI.BlankDetail" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <div style="height: 185px;font-size: 20px;" id="divsorrypaypal" runat="server">

        Sorry! Your Paypal payment was not successful.
        <br />
        <br /><asp:Literal ID="ltrlcliclk" runat="server" Text="Click"></asp:Literal>
       
        <asp:HyperLink ID="hlShopingCart" runat="server">Here</asp:HyperLink><asp:Literal ID="ltrlgoback2cart" runat="server" Text="to go back to your cart."></asp:Literal>
        </div>

</asp:Content>
