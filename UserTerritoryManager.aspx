<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ThemeSite.Master" AutoEventWireup="true" CodeBehind="UserTerritoryManager.aspx.cs" Inherits="Web2Print.UI.UserTerritoryManager" %>
<%@ Register src="Controls/CRUDTerritory.ascx" tagname="CRUDTerritory" tagprefix="uc1" %>
<%@ Register Src="~/Controls/BreadCrumbMenu.ascx" TagName="BreadCrumbMenu" TagPrefix="uc2" %>

<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">
 <div class="content_area ">
        <div class="left_right_padding">
        <uc2:BreadCrumbMenu ID="BreadCumbMenuCategory" runat="server" WorkMode="MyAccount"
                MyAccountCurrentPage="Store Territories" MyAccountCurrentPageUrl="UserTerritoryManager.aspx" />
            <div class="contact_us_heading">
                <asp:Label ID="lblPageTitle" runat="server" Text="Territory Manager"></asp:Label>
            </div>
            <div class=" PaddingB50 ">
                <uc1:CRUDTerritory ID="CRUDTerritory1" runat="server" />
            </div>
        </div>
    </div>
    <br />
</asp:Content>