<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginBar.ascx.cs" Inherits="Web2Print.UI.Controls.LoginBar" %>
<div class="clearBoth">
    &nbsp;
</div>
<div id="divShd" class="opaqueLayer">
</div>
<style>
    #divShd
    {
        z-index: 500;
    }

    #jqwin
    {
        z-index: 10001;
        position: fixed;
        background-color: transparent;
        display: none;
        left: 272px;
        top: 28px;
    }
</style>
<div class="PDTCWB LoginBarContainer" id="TopLoginBar" runat="server">
    <div class="container content_area">

        <div class="row left_right_padding">
            <div class="float_left_simple cnttemporary col-md-6  col-sm-6 col-lg-6">
                &nbsp;
            </div>

            <div id="loginbarCon" runat="server" class="login_bar float_right col-sm-6 col-md-6 col-lg-6 col-xs-12" style="padding-right: 0px !important;">
                <ul id="menu-my-account" class="navM">
                    <li style="margin-right: 0px !important;" class="classicthemeLoginMenu metromaniaThemeDisplayN">
                        <a href="/Login.aspx"><i class="fa fa-user usercolorSize"></i></a>
                        <a href="/Signup.aspx"><i class="fa fa-unlock usercolorSize"></i></a>
                    </li>
                    <li id="liSignInRegister" class="sub PinkBtnMyAccS6" runat="server">

                        <asp:HyperLink ID="lnkLogin" runat="server" CssClass="lnkSignInCs" NavigateUrl="~/Login.aspx">Sign In / Register</asp:HyperLink>
                    </li>
                    <li class="ZeroRightMargin BlackBtnCS">
                        <asp:HyperLink ID="hyperlink3" NavigateUrl="~/PinkCardShopCart.aspx" runat="server"
                            CssClass="top_sub_section_links hyperlinkCart ">
                            <i class="fa fa-shopping-cart cartcolorSize classicthemeLoginMenu"></i><span class="carttxtcolorSize classicthemeLoginMenu">Shopping cart</span>
                            <div runat="server" class="lblCartCounter" id="lblCartCount">
                            </div>
                        </asp:HyperLink>
                    </li>
                </ul>
            </div>

            <div id="USerInfoContainer" runat="server" class="float_right">
                <ul class="LogBar">

                    <li class="liUserInfo">

                        <asp:Label ID="lblUserInfo" runat="server" Text="Hi Omer" CssClass="LoginBarColor liUserInfo" />
                    </li>
                    <li class="classicthemeMenu olsonfaIcon MetroMania">
                        <a href="/DashBoard.aspx"><i class="fa fa-cog  loginusercolorSize"></i></a>
                    </li>
                    <li class="classicthemeMenu olsonfaIcon MetroMania">
                        <a href="/SavedDesignes.aspx"><i class="fa fa-pencil-square-o loginusercolorSize"></i></a>
                    </li>
                    <li class="classicthemeMenu olsonfaIcon MetroMania">
                        <a href="/PinkCardShopCart.aspx"><i class="fa fa-shopping-cart loginusercolorSize"></i></a>
                    </li>
                    <li class="liMarginRight responsiveMenu olsonspriteIcon">

                        <div class="ProfileSetingbtn">
                            <asp:HyperLink ID="hyperlink1" NavigateUrl="~/DashBoard.aspx" runat="server" CssClass="top_sub_section_links hyperlinkCart">
                              
                            </asp:HyperLink>
                        </div>
                    </li>
                    <li class="liMarginRight responsiveMenu olsonspriteIcon">
                        <asp:Label ID="Label1" runat="server" Text="Saved Designs" Style="float: left; width: 100px;"
                            CssClass="LoginBarColor" Visible="false" />
                        <div class="SavedDesignBtn">
                            <asp:HyperLink ID="hyperlink2" NavigateUrl="~/SavedDesignes.aspx" runat="server"
                                CssClass="top_sub_section_links hyperlinkCart">

                                <div runat="server" class="lblSDCounter" id="lblSavedDesignCount">
                                </div>
                            </asp:HyperLink>
                        </div>
                    </li>
                    <li class="liMarginRight responsiveMenu olsonspriteIcon">
                        <asp:Label ID="Label2" runat="server" Text="Items in cart" Style="float: left; width: 100px;"
                            CssClass="LoginBarColor" Visible="false" />
                        <div class="CartBtn">
                            <asp:HyperLink ID="hyperlinkCart" NavigateUrl="~/PinkCardShopCart.aspx" runat="server"
                                CssClass="top_sub_section_links hyperlinkCart">

                                <div runat="server" class="lblItemsCartCounter" id="lblCartCounter">
                                </div>
                            </asp:HyperLink>
                        </div>
                    </li>
                    <li>
                        <asp:LinkButton ID="lnkBtnSignOut" runat="server" Text="Log Out" CommandName="SignOut"
                            CausesValidation="false" CssClass="LoginBarColor" OnClick="lnkLogin_Click" />
                    </li>
                </ul>
            </div>
            <div class="clearBoth">
                &nbsp;
            </div>
        </div>
        <div class="clearBoth">
            &nbsp;
        </div>
    </div>
    <div class="clearBoth">
        &nbsp;
    </div>
</div>
<div class="clearBoth">
    &nbsp;
</div>
<div id="DemoSiteLink"  runat="server" visible="false">
<div id="colorChangercnt" class="hidden-sm hidden-xs right199" >
    <div id="open-close" onclick="openCloseBaseTab();">
        <i class="fa fa-cog" style="color: white !important;"></i>
    </div>
   <h6>Choose color</h6>
    <iframe id="Iframe1" style="height: 190px; width:180px; padding-bottom: 0px; border: none; z-index: 1000000 !important;" class="rounded_corners LCLB" src="../ColorPicker.aspx"></iframe>

</div>

    <a  onclick="askfordemo();" class="demoButton">Ask for demo
</a>
<div id="jqwin" class="FileUploaderPopup_Mesgbox" style="position: fixed; z-index: 1000;">

    <div onclick="closeBOX();" class="exit_page_CP MesgBoxBtnsDisplay rounded_corners5" style="">Close</div>
    <iframe id="ifrm" style="height: 610px; padding: 5px; padding-bottom: 0px; border: none; z-index: 1000000 !important;" class="rounded_corners LCLB" src="../AskForDemo.aspx"></iframe>
</div>
</div>

<script>

    function ChangeBaseColorq(menuVal, BackClr) {
        try {
            if (menuVal == 1) {
                // change background color 
                $(".BodyColorDynamic").css("background-color", BackClr);
                $(".bodycolor").removeClass("bodycolor").addClass('BodyColorDynamic').css("background-color", BackClr);
            } else {
                var css = BackClr;
                var cssImp = BackClr;
                cssImp = cssImp + " " + "!important";
                $("p,div,a,span").css("color", cssImp);
                $("p,div,a,span").css("color", css);
            }
        }
        catch (err) {
            console.log(err.message);
        }
    }

    function openCloseBaseTab() {
        if ($("#colorChangercnt").hasClass("right199")) {
            $("#colorChangercnt").removeClass("right199").addClass("right0");
        } else {
            $("#colorChangercnt").removeClass("right0").addClass("right199");
        }
    }
    function getBrowserHeight() {
        var intH = 0;
        var intW = 0;
        if (typeof window.innerWidth == 'number') {
            intH = window.innerHeight;
            intW = window.innerWidth;
        }
        else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
            intH = document.documentElement.clientHeight;
            intW = document.documentElement.clientWidth;
        }
        else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
            intH = document.body.clientHeight;
            intW = document.body.clientWidth;
        }
        return { width: parseInt(intW), height: parseInt(intH) };
    }
    $(document).ready(function () {
      
        var shadow = document.getElementById("divShd");
        var bws = getBrowserHeight();
        shadow.style.width = bws.width + "px";
        shadow.style.height = bws.height + "px";
        var left = parseInt(($(window).width() - 809) / 2);
        var top = parseInt(($(window).height() - 610) / 2);

        $('#jqwin').css("left", left);
        $('#jqwin').css("top", top);
        $('#ifrm').css("width", "809px");
        if ($(window).width() < 360) {
            var left = parseInt(($(window).width() - $(window).width()) / 2);
            var top = parseInt(($(window).height() - 610) / 2);
            $('#jqwin').css("left", left);
            $('#jqwin').css("top", top);
            $('#ifrm').css("width", "320px");
        } else if ($(window).width() < 481) {

            var left = parseInt(($(window).width() - $(window).width()) / 2);
            var top = parseInt(($(window).height() - 610) / 2);
            $('#jqwin').css("left", left);
            $('#jqwin').css("top", top);
            $('#ifrm').css("width", "400px");
        } else if ($(window).width() < 641) {

            var left = parseInt(($(window).width() - $(window).width()) / 2);
            var top = parseInt(($(window).height() - 610) / 2);
            $('#jqwin').css("left", left);
            $('#jqwin').css("top", top);
            $('#ifrm').css("width", "460px");
        } else {
        
        }
      
    });

    $(window).resize(function () {
        var shadow = document.getElementById("divShd");
        var bws = getBrowserHeight();
        shadow.style.width = bws.width + "px";
        shadow.style.height = bws.height + "px";
        var left = parseInt(($(window).width() - 809) / 2);
        var top = parseInt(($(window).height() - 610) / 2);
        $('#jqwin').css("left", left);
        $('#jqwin').css("top", top);
        $('#ifrm').css("width", "809px");
        if ($(window).width() < 360) {
            var left = parseInt(($(window).width() - $(window).width()) / 2);
            var top = parseInt(($(window).height() - 610) / 2);
            $('#jqwin').css("left", left);
            $('#jqwin').css("top", top);
            $('#ifrm').css("width", "320px");
        } else if ($(window).width() < 481) {

            var left = parseInt(($(window).width() - $(window).width()) / 2);
            var top = parseInt(($(window).height() - 610) / 2);
            $('#jqwin').css("left", left);
            $('#jqwin').css("top", top);
            $('#ifrm').css("width", "400px");
        } else if ($(window).width() < 641) {

            var left = parseInt(($(window).width() - $(window).width()) / 2);
            var top = parseInt(($(window).height() - 610) / 2);
            $('#jqwin').css("left", left);
            $('#jqwin').css("top", top);
            $('#ifrm').css("width", "460px");
        } else {
          
        }
    });

    function askfordemo() {

        $('#divShd').css("display", "block");

        $('#jqwin').css("display", "block");
        $(".closeBtn_CP").css("display", "block");

        return false;
    }

    function closeBOX() {
        $(".closeBtn_CP").css("display", "none");
        $('#divShd').css("display", "none");
        $('#jqwin').css("display", "none");
    }

</script>
