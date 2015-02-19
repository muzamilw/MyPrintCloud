<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ThemeSite.Master" AutoEventWireup="true"
    CodeBehind="PropertyDetailsAndMarketing.aspx.cs" Inherits="Web2Print.UI.PropertyDetailsAndMarketing" %>

<%--<%@ Register TagName="ImageSlider" TagPrefix="uc1" Src="~/Controls/ImageSlider.ascx" %>--%>

<asp:Content ID="PropertyPageHead" ContentPlaceHolderID="HeadContents" runat="server">
</asp:Content>
<asp:Content ID="PageBanner" ContentPlaceHolderID="PageBanner" runat="server">
</asp:Content>
<asp:Content ID="PropertyPageMainContent" ContentPlaceHolderID="MainContent" runat="server">

    <div id="divShd" class="opaqueLayer">
    </div>
    <div runat="server" id="divbg" visible="false" class="categorydivBG">&nbsp;</div>
    <div class="container content_area">

        <asp:Panel runat="server" ID="smartParentPanel" Style="margin-top: 20px;">
            <asp:Panel runat="server" ID="pnlGENERALINFORMATION" HorizontalAlign="Left" GroupingText="GENERAL INFORMATION" CssClass="SmartFormPanels" stye="border-style: none; text-align:left;">

                <asp:Label runat="server" ID="lblPropName" CssClass="col-md-2 col-lg-2 col-xs-12 smallCheckoutAvenior">Property Name</asp:Label>
                <div class="TTL_Checkout ckecoutWidthAvenior col-md-8 col-lg-8 col-xs-12">
                    <asp:TextBox runat="server" Enabled="false" ID="txtPropName" CssClass="AveniorTxtBoxCheckout"></asp:TextBox>
                </div>

                <asp:Label runat="server" ID="lblPropType" CssClass="col-md-2 col-lg-2 col-xs-12 smallCheckoutAvenior">Property Type</asp:Label>
                <div class="TTL_Checkout ckecoutWidthAvenior col-md-8 col-lg-8 col-xs-12">
                    <asp:TextBox runat="server" Enabled="false" ID="txtPropType" CssClass="AveniorTxtBoxCheckout"></asp:TextBox>
                </div>

                <asp:Label runat="server" ID="lblPropAddress" CssClass="col-md-2 col-lg-2 col-xs-12 smallCheckoutAvenior">Address</asp:Label>
                <div class="TTL_Checkout ckecoutWidthAvenior col-md-8 col-lg-8 col-xs-12">
                    <asp:TextBox runat="server" Enabled="false" ID="txtPropAddress" CssClass="AveniorTxtBoxCheckout"></asp:TextBox>
                </div>

                <asp:Label runat="server" ID="lblPropState" CssClass="col-md-2 col-lg-2 col-xs-12 smallCheckoutAvenior">State</asp:Label>
                <div class="TTL_Checkout ckecoutWidthAvenior col-md-8 col-lg-8 col-xs-12">
                    <asp:TextBox runat="server" Enabled="false" ID="txtPropState" CssClass="AveniorTxtBoxCheckout"></asp:TextBox>
                </div>

            </asp:Panel>
        </asp:Panel>
        <asp:Button ID="btnSaveAndDesign" runat="server" Text="Save and Design" OnClick="btnSaveAndDesign_Click"
            Style="margin-bottom: 20px; float: right;" CssClass="cursor_pointer designNowCS_Corp rounded_corners5 btnMakeThisDesign btnEditDesign" />

    </div>

</asp:Content>
