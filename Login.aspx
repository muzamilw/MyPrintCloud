<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ThemeSite.Master"
	CodeBehind="Login.aspx.cs" Inherits="Web2Print.UI.Login" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/BreadCrumbMenu.ascx" TagName="BreadCrumbMenu" TagPrefix="uc2" %>
<%@ Register Src="~/Controls/QuickLinks.ascx" TagName="QuickLinks" TagPrefix="uc5" %>
<%@ Register Src="~/Controls/PromotionalBanner.ascx" TagName="PromotionalBanners"
	TagPrefix="uc6" %>
<%@ Register Src="~/Controls/LoginControl.ascx" TagName="LoginControl" TagPrefix="uc7" %>
<asp:Content ID="Logincontents1" ContentPlaceHolderID="HeadContents" runat="server">
      <style>
        input[type="checkbox"]
        {
            display: none;
            outline: none !important;
            -webkit-transition: background-color;
            -moz-transition: background-color;
            -o-transition: background-color;
            -ms-transition: background-color;
            transition: background-color;
        }

            input[type="checkbox"] + label
            {
                display: inline-block !important;
                padding: 6px 0 6px 45px;
                line-height: 25px;
                background-image: url("//cdn.shopify.com/s/files/1/0245/8513/t/7/assets/checkbox_sprite.png?4214");
                background-image: none,url("//cdn.shopify.com/s/files/1/0245/8513/t/7/assets/checkbox_sprite.svg?4214");
                background-position: -108px 0;
                background-repeat: no-repeat;
                -webkit-background-size: 143px 143px;
                -moz-background-size: 143px 143px;
                background-size: 143px 143px;
                overflow: visible;
                outline: none;
                -webkit-user-select: none;
                -moz-user-select: none;
                -ms-user-select: none;
                user-select: none;
                cursor: pointer;
                cursor: pointer;
                color: #66615b;
                outline: none !important;
                font-size: 17px;
            }

                input[type="checkbox"]:hover + label, input[type="checkbox"] + label:hover, input[type="checkbox"]:hover + label:hover
                {
                    background-position: -72px -36px;
                    color: #403d39;
                }

            input[type="checkbox"]:checked + label
            {
                background-position: -36px -72px;
                color: #403d39;
            }

                input[type="checkbox"]:checked:hover + label, input[type="checkbox"]:checked + label:hover, input[type="checkbox"]:checked:hover + label:hover
                {
                    background-position: 0 -108px;
                    color: #403d39;
                }
    </style>
</asp:Content>
<asp:Content ID="Logincontents2" ContentPlaceHolderID="PageBanner" runat="server">
</asp:Content>
<asp:Content ID="Logincontents3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container content_area">
        <div class="row left_right_padding">
            <div class="signin_heading_div col-md-12 col-lg-12 col-xs-12">
                <asp:Label ID="lblTitle" runat="server" Text="Sign In" CssClass="sign_in_heading"></asp:Label>
            </div>

            <uc7:LoginControl ID="lgnctrl" runat="server" />
        </div>
        <div class="clearBoth">
            &nbsp;
        </div>
        <br />
        <br />
        <br />

	</div>
	  
</asp:Content>
