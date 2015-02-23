<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Footer.ascx.cs" Inherits="Web2Print.UI.Controls.Footer" %>
<%@ Register Src="QuickLinks.ascx" TagName="QuickLinks" TagPrefix="uc1" %>
<%@ Register Src="CategoryListing.ascx" TagName="CategoryListing" TagPrefix="uc2" %>
<div id="Footer" class="clearBoth">
    <div id="SPagesContainer" class="spclColor" runat="server">
        <div class="GreyBackS6">
            <div class="container heightauto content_area">
                <div class="row left_right_padding">
                    <div class="space5">
                        &nbsp;
                    </div>
                    <div class="footer_portion themeFontColor">
                        <uc1:QuickLinks ID="QuickLinks1" runat="server" />
                    </div>
                    <div class="space5">
                        &nbsp;
                    </div>
                    <div class="clearBoth">
                        &nbsp;
                    </div>
                </div>
                <div class="clearBoth">
                        &nbsp;
                    </div>
            </div>
        </div>
    </div>
    <div id="FooterContainer" class="PDTCWB" runat="server">
        <div class="container content_area">
            <div class="row left_right_padding">
                <div class="footer_sub">
                    <div class="footer_sub_height">
                       <div class="txtAlignLeft footerLogoContainer col-xs-12 col-sm-2 col-md-3 col-lg-3">

                       
                            <a href='<%=ResolveUrl("~/default.aspx") %>' >
                                <asp:Image ID="imgfooterLogo" CssClass="footerLogoBranding" runat="server" BorderStyle="None" /></a>
                       </div>
                        <div class="txtAlignLeft col-md-6 col-sm-6 col-xs-12 col-lg-6 payment_sec NoPadding">
                            <div class="copyTerms">
                                <asp:Literal ID="ltrlcopyrite" runat="server" Text="Copyrights 2014. All Rights Reserved."></asp:Literal>
                            <a id="hplinkTermsCondition" class="small_gray_hyperlink S6FooterMLCs hyperTermCond" runat="server"
                                href="#">Terms & Conditions</a>
                            </div>
                            
                            <div class="powerPrivacy">
                                 <asp:HyperLink ID="hlPweredBy" runat="server" 
                                CssClass="poweredby" Target="_blank"></asp:HyperLink>
                                <asp:HyperLink ID="hlPrivacyPolicy" runat="server" CssClass="small_gray_hyperlink hyPrivacyP">Privacy Policy</asp:HyperLink>
                            </div>
                        </div>
                        <div class="txtAlignLeft col-xs-12 col-md-3 col-sm-4  col-lg-3 social_icons" runat="server" id="pnlFollowUs">
                            <asp:Label ID="lblFollowusText" Text="Follow Us :" Font-Bold="true" runat="server"
                                CssClass="S6DisplayNone" />
                            <div class="space3 S6DisplayNone">
                                &nbsp;
                            </div>
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td valign="top">
                                        <asp:HyperLink ID="linkFollowUs1" runat="server" Target="_blank">
                                            <asp:Image ID="imgFollowUs1" runat="server" CssClass="imgFollowUs" />
                                        </asp:HyperLink>
                                    </td>
                                    <td valign="top">
                                        <asp:HyperLink ID="linkFollowUs2" runat="server" Target="_blank">
                                            <asp:Image ID="imgFollowUs2" runat="server" CssClass="imgFollowUs" />
                                        </asp:HyperLink>
                                    </td>
                                    <td valign="top">
                                        <asp:HyperLink ID="linkFollowUs3" runat="server" Target="_blank">
                                            <asp:Image ID="imgFollowUs3" runat="server" CssClass="imgFollowUs" />
                                        </asp:HyperLink>
                                    </td>
                                    <td valign="top">
                                        <asp:HyperLink ID="linkFollowUs4" runat="server" Target="_blank">
                                            <asp:Image ID="imgFollowUs4" runat="server" CssClass="imgFollowUs" />
                                        </asp:HyperLink>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="clearBoth">
                            &nbsp;
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="clearBoth">
        &nbsp;
    </div>
</div>
<div class="bottom_sec S6DisplayNone clearBoth"></div>
