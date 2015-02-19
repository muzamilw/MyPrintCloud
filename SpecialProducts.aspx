<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ThemeSite.Master" AutoEventWireup="true"
    CodeBehind="SpecialProducts.aspx.cs" Inherits="Web2Print.UI.SpecialProducts" %>

<%@ Register Src="Controls/PromotionalBanner.ascx" TagName="PromotionalBanner" TagPrefix="uc1" %>
<asp:Content ID="CatPageHead" ContentPlaceHolderID="HeadContents" runat="server">
</asp:Content>
<asp:Content ID="PageBanner" ContentPlaceHolderID="PageBanner" runat="server">
</asp:Content>
<asp:Content ID="CatPageMainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content_area">
        <div class="left_right_padding">
            <uc1:PromotionalBanner ID="PromotionalBanner1" runat="server" BehaveAsPage="true" />
        </div>
    </div>
</asp:Content>
