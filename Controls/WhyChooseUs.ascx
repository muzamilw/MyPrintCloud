<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WhyChooseUs.ascx.cs"
    Inherits="Web2Print.UI.Controls.WhyChooseUs" %>
<%@ Register Src="Subscribe.ascx" TagName="Subscribe" TagPrefix="uc1" %>
<div id="WhyChooseUsHorizontal" runat="server" class="WhyChooseUsHorizontalControl">
    <div class="footer_super_sec VertMiddleAlign">
        <div class="content_area">
            <div class="left_right_padding">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td valign="top">
                            <a href="#" runat="server" id="lnkBtnContactUs1">
                                <asp:Image ID="imgIcon1" runat="server" SkinID="WhychooseUsimgIcon" /></a>
                        </td>
                        <td valign="top">
                            <a href="#" runat="server" id="lnkBtnContactUs2">
                                <asp:Image ID="imgIcon2" runat="server" SkinID="WhychooseUsimgIcon" /></a>
                        </td>
                        <td valign="top">
                            <a href="#" runat="server" id="lnkBtnContactUs3">
                                <asp:Image ID="imgIcon3" runat="server" SkinID="WhychooseUsimgIcon" /></a>
                        </td>
                        <td valign="top">
                            <uc1:Subscribe ID="Subscribe1" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</div>
<div id="whyChosseVertical" runat="server" visible="false">
    <table border="0" cellpadding="2" cellspacing="3">
        <tr>
            <td align="left">
                <span class="heading_XMedium themeFontColor">Why RegisterColor">Why Register</span><br />
                <br />
                <span>By Registering you are able to:</span>
                <br />
                <br />
                <span>.Save your designs</span><br />
                <span>.View and track order history</span><br />
                <span>.Re order quickly</span><br />
            </td>
        </tr>
        <tr>
            <td>
                <a href="#" runat="server" id="lnkVertIcon1">
                    <asp:Image ID="imgVertIcon1" runat="server" SkinID="WhychooseUsimgIcon1" /></a>
            </td>
        </tr>
        <tr>
            <td>
                <a href="#" runat="server" id="lnkVertIcon2">
                    <asp:Image ID="imgVertIcon2" runat="server" SkinID="WhychooseUsimgIcon2" /></a>
            </td>
        </tr>
        <tr>
            <td>
                <a href="#" runat="server" id="lnkVertIcon3">
                    <asp:Image ID="imgVertIcon3" runat="server" SkinID="WhychooseUsimgIcon3" /></a>
            </td>
        </tr>
    </table>
</div>
