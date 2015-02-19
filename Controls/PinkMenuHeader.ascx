<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PinkMenuHeader.ascx.cs"
    Inherits="Web2Print.UI.Controls.PinkMenuHeader" %>

<div id="PanelNormalUsers" class="top_sub_section TopMenuH60W100Px" runat="server"
    visible="true">
    <script src="/Scripts/jquery-ui-1.9.2.custom.min.js" type="text/javascript"></script>
    <div class="content_area">
        <div class="left_right_padding">
            <div id="divMenu" runat="server" class="menu">
                <ul id="menu-main-menu" class="nav neg_left_margin_5 cursor_pointer">
                    <li><a id="anchorHomeImage" runat="server" visible="false">
                        <div id="btnHome" runat="server" class="homeIconImge" visible="false">
                        </div>
                    </a></li>
                    <li><a id="lnkHomePage" href="~/Default.aspx" runat="server" class="top_sub_section_links HomeIconBox">
                      <img id="DefaultHomeIcon" runat="server"  width="25" alt="" src="~/App_Themes/S6/Images/home.png"/> </a></li>
                    <li id="liBussinCards" runat="server"><a id="lnkBussiesCard" runat="server" class="top_sub_section_links marginRght">Business
                        Cards </a></li>
                        <li id="featuredProd" runat="server">
                            <asp:Repeater ID="rptFeaturesProducts" runat="server" OnItemDataBound="rptFeaturesProducts_ItemDataBound">
                                <ItemTemplate>
                                <asp:HyperLink ID="hplnkFProducts" runat="server" class="float_left_simple top_sub_section_links white marginRght"></asp:HyperLink>
                                </ItemTemplate>
                            </asp:Repeater>
                        </li>
                    <li id="AllProMenuLi" runat="server"><a id="lnkProducts" href="~/AllProducts.aspx"
                        runat="server" class="top_sub_section_links marginRght">All Products</a> </li>
                    <%--<li id="liInquiry" runat="server">
                        <asp:HyperLink ID="hlRequestQuote" runat="server" CssClass="top_sub_section_links marginRght marginLeft"
                            NavigateUrl="~/RequestQuote.aspx">Request A Quote</asp:HyperLink></li>
                    <li class="sub"><a id="LetUsHelp" href="#" runat="server" class="top_sub_section_links marginRght "
                        onclick="return false;"></a>
                        <asp:BulletedList ID="BLQL" runat="server" CssClass="uSecPageList top_sub_section W100PerH60Px"
                            ViewStateMode="Enabled" DisplayMode="HyperLink">
                        </asp:BulletedList>
                    </li>
                    <li id="lifindStore" runat="server" visible="false">
                        <a href="#" class="top_sub_section_links marginLeft marginRght" onclick="showPopUP('','/default.aspx')">Find Your Store</a>
                    </li>--%>
                   
                </ul>
            </div>
            <div id="divCorporateUser" visible="false" runat="server" class="corportate_header_container">
                <div class="t_hr_ce_item_home">
                    <asp:HyperLink ID="Hyphm" runat="server" CssClass="top_sub_section_links_corporate"
                        NavigateUrl="~/Default.aspx">Home</asp:HyperLink>
                </div>
                <div class="t_hr_ce_item_home">
                    <asp:HyperLink ID="HyperLink1" runat="server" CssClass="top_sub_section_links_corporate"
                        NavigateUrl="~/RequestQuote.aspx">Request A Quote</asp:HyperLink>
                </div>
                <div class="t_hr_ce_item_home">
                    <asp:HyperLink ID="hlContactUs" runat="server" CssClass="top_sub_section_links_corporate"
                        NavigateUrl="#">Contact Us</asp:HyperLink>
                </div>
                <div class="t_hr_ce_item_home">
                    <asp:HyperLink ID="hlAboutUs" runat="server" CssClass="top_sub_section_links_corporate"
                        NavigateUrl="#">About Us</asp:HyperLink></div>
                <div class="clearBoth">
                    &nbsp;</div>
            </div>
            <div id="ContactUSCOntainer" runat="server" class="float_right Fsize15" style=" width: 115px; margin-top: -5px;" >
            <ul class="nav neg_left_margin_5 cursor_pointer">
             <li id="liContcUS" runat="server">
                        <asp:HyperLink ID="hyprContUs" runat="server" CssClass="top_sub_section_links marginLeft"
                            NavigateUrl="~/ContactUs.aspx">Contact Us</asp:HyperLink>
                    </li></ul>
            </div>
            <%-- <div class="login_bar">
                <ul id="menu-my-account" class="nav">
                    <li id="liMyAccount" class="sub PinkBtnMyAccS6" runat="server"><a id="lnkMyAccount" runat="server" href="~/DashBoard.aspx" class="top_sub_section_links">
                        <asp:Label ID="lblUserInfo" runat="server" Text="" CssClass="lnkSignInCs" /></a>
                        <ul id="ulMyAccount" class="uSecPageList W100PerH60Px" runat="server"
                            visible="false">
                            <li class="paddLeft"><a id="anchorDashBoard" runat="server" href="~/DashBoardNew.aspx">
                                Dashboard</a>
                            </li>
                            <li class="PaddingLeft35PxS6">
                                <asp:LinkButton ID="lnkBtnSignOut" runat="server" Text="Sign Out" CommandName="SignOut"
                                    Visible="false" CausesValidation="false" CssClass="top_sub_section_links" OnClick="lnkLogin_Click" />
                            </li>
                        </ul>
                    </li>
                   
                    <li id="liSignInRegister" class="PinkBtnS6" runat="server">
                        <asp:HyperLink ID="lnkLogin" runat="server" CssClass="lnkSignInCs" NavigateUrl="~/Login.aspx"
                            Visible="false">Sign In / Register</asp:HyperLink>
                    </li>
                    <li class="ZeroRightMargin BlackBtnCS">
                    
                    <asp:HyperLink ID="hyperlinkCart" NavigateUrl="~/PinkCardShopCart.aspx" runat="server" CssClass="top_sub_section_links hyperlinkCart">
                        <asp:Image ID="imgCart" runat="server" SkinID="imgCart" />
                        <div runat="server" class="lblCartCounter" id="lblCartCounter"></div>
                    </asp:HyperLink>
                    </li>
                </ul>
            </div>--%>
        </div>
        <div style="clear: both;">
        </div>
    </div>
    <div class="top_sub_section_bottom_space">
        <br />
    </div>
</div>
<div style="clear: both;">
</div>
<asp:HiddenField ID="hfforstrings" runat="server" />
<div id="jqwin" class="FileUploaderPopup_Mesgbox rounded_corners IframeCss LCLB">
</div>
<style>
    #jqwin
    {
        /* height: 100%;--%> */
        z-index: 99999;
        position: absolute;
        display: none;
    }
</style>
<script type="text/javascript">

    $(document).ready(function () {
        var Mode = '<%=IsPinkCardBrokerMode %>';

        if (Mode == 1) {
            var popwidth = 700;
            var shadow = document.getElementById("divShd");
            var bws = getBrowserHeight();
            shadow.style.width = bws.width + "px";
            shadow.style.height = bws.height + "px";
            var left = parseInt((bws.width - popwidth) / 2);
            var top = parseInt((bws.height - 325) / 2);

            $('#divShd').css("display", "block");
            $('#jqwin').css("width", popwidth);
            $('#jqwin').css("height", "225px");
            $('#jqwin').css("top", top);
            $('#jqwin').css("left", left);

           
            var html = ' <div onclick="closeMS();" class="MesgBoxBtnsDisplay_subscriber rounded_corners">Close</div>';
            $('#jqwin').html(html + '<iframe id="ifrm" width="' + popwidth + '" height="100%" border="0" style="width:' + popwidth + 'px;height:100%;border: none; background-color: white;" class=""></iframe>')
            $('#ifrm').attr('src', '/NewsletteerSubscriber.aspx');
            $('#jqwin').show();

            var ytop = parseInt((bws.height - 325) / 2);

            $('#jqwin').animate({ top: ytop }, 'slow');
        } else if (Mode == 2) {
            //showPopUP('', '/default.aspx');
        }
    });

    function showPopUP(postCode, ReturnPath) {
        var popwidth = 570;

        var shadow = document.getElementById("divShd");
        var bws = getBrowserHeight();
        shadow.style.width = bws.width + "px";
        shadow.style.height = bws.height + "px";
        var left = parseInt((bws.width - popwidth) / 2);
        var top = parseInt((bws.height - 450) / 2);


        //        shadow = null;
        $('#divShd').css("display", "block");
        $('#jqwin').css("width", popwidth);
        $('#jqwin').css("height", 455);
        $('#jqwin').css("top", top);
        $('#jqwin').css("left", left);
        var html = '<div class="closeBtn2" onclick="closeMS()"> </div>';
        $('#jqwin').css("position", "fixed");
        $('#jqwin').html(html + '<iframe id="ifrm" width="' + popwidth + '" height="100%" border="0" style="width:' + popwidth + 'px;height:100%;border: none; background-color: white; padding-left: 3px;" class=""></iframe>')
        $('#ifrm').attr('src', '/searchbypostcode.aspx?PostCode=' + postCode + '&ReturnPath=' + ReturnPath);
        $('#jqwin').show();
        $(".closeBtn2").css("display", "block");

        $('#jqwin').animate({ top: top }, 'slow');
        return true;
    }


    function closeMS() {
        $('#divShd').css("display", "none");
        $(".closeBtn").css("display", "none");
        $('#jqwin').hide();
        $('#ifrm').attr('src', 'about:blank');
    }




    function increaseHeight() {

        var bws = getBrowserHeight();
        var ytop = parseInt((bws.height - 420) / 2);

        $('#jqwin').animate({ top: ytop }, 'slow');
        $('#jqwin').animate({ height: "420px" }, 'slow');
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

    function confirmLogout(message) {

        var result = confirm(message);
        if (result == false)
            return false;
        else
            return true;
    }


    function openMS(templatename, ProductCategoryId, ItemId) {
        //window.open('../matchingsetpopup.aspx?templatename=' + templatename, 'MS', 'height=500,width=800,resizeable=1');

        //var d = $('#jqwin').html('<iframe id="ifrm"></iframe>');

        //d.dialog();


    }

    
</script>
