<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PinkFooter.ascx.cs"
    Inherits="Web2Print.UI.Controls.PinkFooter" %>
<div id="FooterContainer" class="footer_sub_sec_back" runat="server">
    <div class="content_area">
        <div class="left_right_padding">
            <div style="width: 1000px;">
                <div class="footer_sub_height">
                    <div id="BrokerLogoContainer" runat="server" onclick="showVideo();" class="cursor_pointer float_left_simple BrokerLogoContainerHomePage">
                        <img id="ImgYoutube" runat="server" alt="" src="~/images/Youtube.jpg" class="YouTubeImgCs" />
                    </div>
                    <div class="float_left_simple SocialLinksCS" runat="server" id="pnlFollowUs" style="padding: 3px;">
                        <asp:Label ID="lblFollowusText" Text="Follow Us :" Font-Bold="true" runat="server"
                            CssClass="S6DisplayNone" />
                        <div class="space3 S6DisplayNone">
                            &nbsp;</div>
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td valign="top">
                                    <asp:HyperLink ID="linkFollowUs1" runat="server" Target="_blank">
                                        <asp:Image ID="imgFollowUs1" runat="server" CssClass="imgFollowUs" /></asp:HyperLink>
                                </td>
                                <td valign="top">
                                    <asp:HyperLink ID="linkFollowUs2" runat="server" Target="_blank">
                                        <asp:Image ID="imgFollowUs2" runat="server" CssClass="imgFollowUs" /></asp:HyperLink>
                                </td>
                                <td valign="top">
                                    <asp:HyperLink ID="linkFollowUs3" runat="server" Target="_blank">
                                        <asp:Image ID="imgFollowUs3" runat="server" CssClass="imgFollowUs" /></asp:HyperLink>
                                </td>
                                <td valign="top">
                                    <asp:HyperLink ID="linkFollowUs4" runat="server" Target="_blank">
                                        <asp:Image ID="imgFollowUs4" runat="server" CssClass="imgFollowUs" /></asp:HyperLink>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="loginbarCon" runat="server" class="login_bar float_right">
                        <ul id="menu-my-account" class="nav">
                            <li>
                                <asp:Label ID="lblUserInfo" runat="server" Text="" CssClass="TopWigetHeadingClr" /></li>
                            <li id="liMyAccount" class="sub PinkBtnMyAccS6" runat="server">
                                <div>
                                    <a id="lnkMyAccount" runat="server" href="~/DashBoard.aspx" class="top_sub_section_links top_section_MACC_CS">
                                        <asp:Label ID="lblMYA" runat="server" Text="My Account" CssClass="lnkSignInCs" /></a>
                                </div>
                            </li>
                            <li>
                                <asp:LinkButton ID="lnkBtnSignOut" runat="server" Text="Log Out" CommandName="SignOut" style=" margin-right: 75px;"
                                    Visible="false" CausesValidation="false" CssClass="TopWigetHeadingClr" OnClick="lnkLogin_Click" />
                            </li>
                            <li id="liSignInRegister" class="sub PinkBtnMyAccS6" runat="server" >
                                <asp:HyperLink ID="lnkLogin" runat="server" CssClass="lnkSignInCs" NavigateUrl="~/Login.aspx"
                                    Visible="false" >Sign In / Register</asp:HyperLink>
                            </li>
                            <li class="ZeroRightMargin BlackBtnCS">
                                <asp:HyperLink ID="hyperlinkCart" NavigateUrl="~/PinkCardShopCart.aspx" runat="server"
                                    CssClass="top_sub_section_links hyperlinkCart"> 
                                    <asp:Image ID="imgCart" runat="server" SkinID="imgCart" />
                                    <div runat="server" class="lblCartCounter" id="lblCartCounter">
                                    </div>
                                </asp:HyperLink>
                            </li>
                        </ul>
                    </div>
                    <div class="clearBoth">
                        &nbsp;</div>
                </div>
            </div>
        </div>
    </div>
</div>
<script type="text/javascript">
    function showVideo() {
        var popwidth = 565;

        var shadow = document.getElementById("divShd");
        var bws = getBrowserHeight();
        shadow.style.width = bws.width + "px";
        shadow.style.height = bws.height + "px";
        var left = parseInt((bws.width - popwidth) / 2);
        var top = parseInt((bws.height - 315) / 2);


        //        shadow = null;
        $('#divShd').css("display", "block");
        $('#jqwin').css("width", popwidth);
        $('#jqwin').css("height", 320);
        $('#jqwin').css("top", top);
        $('#jqwin').css("left", left);
        var html = '<div class="closeBtn2" onclick="closeMS()"> </div>';
        $('#jqwin').html(html + '<iframe width="560" height="315" src="http://www.youtube.com/embed/u5sYhXRMWZM" frameborder="0" allowfullscreen></iframe>')

        $('#jqwin').show();
        $(".closeBtn2").css("display", "block");


        return true;
    }
</script>
