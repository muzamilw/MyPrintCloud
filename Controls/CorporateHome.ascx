<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CorporateHome.ascx.cs"
    Inherits="Web2Print.UI.Controls.CorporateHome" %>
<%@ Register Src="PersonalInfo.ascx" TagName="PersonalInfo" TagPrefix="uc1" %>
<%@ Register Src="CorportateProducts.ascx" TagName="CorportateProducts" TagPrefix="uc2" %>
<div class="content_area container left_right_padding paddingS4">
    <div class="signin_heading_div">
        <asp:Label ID="lblTitle" runat="server" CssClass="sign_in_heading" Visible="false"></asp:Label>
    </div>
   
    <div class="height15">
        &nbsp;</div>
    <uc1:PersonalInfo ID="PersonalInfo1" runat="server" />
</div>
