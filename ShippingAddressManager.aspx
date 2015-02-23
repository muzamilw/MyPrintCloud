<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ThemeSite.Master" AutoEventWireup="true" CodeBehind="ShippingAddressManager.aspx.cs" Inherits="Web2Print.UI.ShippingAddressManager" %>

<%@ Register Src="~/Controls/BreadCrumbMenu.ascx" TagName="BreadCrumbMenu" TagPrefix="uc1" %>
<%@ Register src="Controls/CRUDShippingAddress.ascx" tagname="CRUDShippingAddress" tagprefix="uc1" %>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">
 <div class="content_area container">
        <div class="left_right_padding row">
       <div class="signin_heading_div float_left_simple dashboard_heading_signin">
                <asp:Label ID="lblPageTitle" CssClass="sign_in_heading" runat="server" Text="Billing & Shipping Addresses"></asp:Label>
                  
            </div>
            <div class="dashBoardRetrunLink">
           <uc1:BreadCrumbMenu ID="BreadCumbMenuCategory" runat="server" WorkMode="MyAccount"
                MyAccountCurrentPage="Billing and Shipping Addresses" MyAccountCurrentPageUrl="ShippingAddressManager.aspx" />
                     </div>
            <div class="clearBoth">

            </div>
           
            <div class="paddingTop15px">
                <uc1:CRUDShippingAddress ID="CRUDShippingAddress1" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>
