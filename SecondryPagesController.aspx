<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ThemeSite.Master" AutoEventWireup="true"
    CodeBehind="SecondryPagesController.aspx.cs" Inherits="Web2Print.UI.SecondryPagesController" %>

<%@ Register Src="Controls/PageBanner.ascx" TagName="PageBanner" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContents" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBanner" runat="server">
    <div class="content_area left_right_padding SecondaryPageBanner">
        <uc1:PageBanner ID="PageBanner1" runat="server" />
    </div>
</asp:Content>
<asp:Content ID="SecondryPageContentsHolder" ContentPlaceHolderID="MainContent" runat="server">
    <div id="SecondryPageMainContent">
        <div class="content_area container">
            <div class="left_right_padding row">
                <div class="SP_transparent rounded_corners">
                    <div class="product_detail_sup_padding">
                        <h1 class="SecPagesHeading ">
                            <asp:Literal ID="ltlHeading" runat="server"></asp:Literal></h1>
                        <div class="paddingLeft10px">
                            <asp:Label ID="lblPageContents" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <br />
        </div>
    </div>
</asp:Content>
