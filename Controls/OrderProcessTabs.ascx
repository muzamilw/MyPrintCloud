<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderProcessTabs.ascx.cs" Inherits="Web2Print.UI.Controls.OrderProcessTabs" %>

<%--The Control is hided for while--%>
<div id="OrderProcessTabsContainer" style="visibility:hidden; display:none;">
        <div class="textLeftFloating">
            <asp:LinkButton ID="lnkBtnDescriptionTab" Enabled="false" runat="Server" CssClass="imgBtnDescOrderProcessTab" /></div>
        <div class="textLeftFloating">
            <asp:LinkButton ID="lnkBtnTemplateDesignTab" Enabled="false" runat="Server" CssClass="imgBtnTemplateDesignTab" /></div>
        <div class="textLeftFloating">
            <asp:LinkButton ID="lnkBtnConfirmSelectionTab" Enabled="false" runat="Server" CssClass="imgBtnConfirmSelectionTab" /></div>
        <div class="textLeftFloating">
            <asp:LinkButton ID="lnkBtnBillingShippingTab" Enabled="false" runat="Server" CssClass="imgBtnBillingShippingTab" /></div>
        <div class="textLeftFloating">
            <asp:LinkButton ID="lnkBtnCheckOutTab" Enabled="false" runat="Server" CssClass="imgBtnCheckoutTab" /></div>
</div>
