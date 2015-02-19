<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CorporateAdminHome.ascx.cs"
    Inherits="Web2Print.UI.Controls.CorporateAdminHome" %>
<%@ Register Src="PersonalInfo.ascx" TagName="PersonalInfo" TagPrefix="uc1" %>
<%@ Register Src="VerticalCorporateCategoryList.ascx" TagName="VerticalCorporateCategoryList"
    TagPrefix="uc2" %>
<%@ Register Src="CorportateProducts.ascx" TagName="CorportateProducts" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<div class="content_area container left_right_padding CorproateAdminHome">
    
    <div class="height15">
        &nbsp;
    </div>
    <uc1:PersonalInfo ID="PersonalInfo1" runat="server" Visible="false" />
    <div class="height15">
        &nbsp;
    </div><br />
</div>
