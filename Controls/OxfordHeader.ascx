<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OxfordHeader.ascx.cs" Inherits="Web2Print.UI.Controls.OxforHeader" %>
<header class="navbar navbar-default topnav" id="TopHeaderContainer" runat="server" role="navigation">
    <div class="container">
        <div class="navbar-header col-md-3 col-lg-3 col-xs-12 col-sm-12">
            <button type="button" class="navbar-toggle btn btn-darkblue hidden-lg hidden-md" id="darkCollapseBtn" data-toggle="collapse" data-target=".navbar-collapse" ><span class="fa fa-bars fa-fw" ></span></button>
            <%--<button type="button" class="btn btn-darkblue leftbar-toggle pull-left" data-toggle="offcanvas" onclick="showSideBar();"><span class="fa fa-th-large fa-fw" onclick="showSideBar();"></span></button>--%>
            <a class="navbar-brand" href='<%=ResolveUrl("~/default.aspx") %>'>
                <img class="img-responsive" alt="Company" id="imgLogo" runat="server"></a>
        </div>
        <div class="navbar-collapse col-md-9 col-lg-9 col-xs-12 col-sm-12" id="oxfordCollapedMenucnt" runat="server">

            <ul class="nav navbar-nav navbar-right">
                
                <li>
                    <a href="../AllProducts.aspx" class="dropdown-toggle" data-toggle="dropdown"><i class="fa fa-list"></i><span>Products </span></a>

                </li>
                <li><a id="lnkRequestQuoteHeader" runat="server" href="../RequestQuote.aspx" class="" target="_self"><i class="fa fa-pencil"></i><span>Request a Quote </span></a></li>
                <li><a href="../ContactUs.aspx" class="" target="_self"><i class="fa fa-envelope-o"></i><span>Contact Us </span></a></li>
                <li><a id="psop7" href="../Login.aspx" title="" class="top_login_popup"><i class="fa fa-user"></i><span>Login / Register </span></a></li>

            </ul>
        </div>
    </div>
</header>
 <a  onclick="LoginPopUP();" class="demoButton">
    Ask for demo
</a>
    <div id="jqwin" class="FileUploaderPopup_Mesgbox" style="position: fixed; z-index:1000;">
       
         <div onclick="closeBOX();" class="exit_page_CP MesgBoxBtnsDisplay rounded_corners5" style="">Close</div>
      <iframe id="ifrm" style="height:610px; padding:5px; padding-bottom:0px; border: none; z-index:99999999 !important;" class="rounded_corners LCLB" src="../AskForDemo.aspx" ></iframe>
    </div>
<script>
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

    $("#darkCollapseBtn").click(function () {

        $("#MainContent_ctl00_oxfordCollapedMenucnt").slideToggle();
    });
    function LoginPopUP() {

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
