<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ThemeSite.Master" AutoEventWireup="true"
    CodeBehind="BrokerAdminProducts.aspx.cs" Inherits="Web2Print.UI.BrokerAdminProducts" %>
<%@ Register Src="~/Controls/BreadCrumbMenu.ascx" TagName="BreadCrumbMenu" TagPrefix="uc1" %>
<%@ Register Src="Controls/ProductDetail.ascx" TagName="ProductDetail" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContents" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content_area container">
        <div class="left_right_padding row">
        
            <div class="signin_heading_div float_left_simple dashboard_heading_signin">
                <asp:Label ID="lblPageTitle" CssClass="sign_in_heading" runat="server" Text="Published Products"></asp:Label>
                  
            </div>
            <div class="dashBoardRetrunLink">
             <uc1:BreadCrumbMenu ID="BreadCumbMenuCategory" runat="server" WorkMode="MyAccount"
                MyAccountCurrentPage="Published Products" MyAccountCurrentPageUrl="BrokerAdminProducts.aspx" />
                     </div>
            <div class="clearBoth">

            </div>
            <div class=" PaddingB50 ">
                <uc1:ProductDetail ID="ProductDetail1" runat="server" />
            </div>
        </div>
    </div>
    <br />
</asp:Content>
