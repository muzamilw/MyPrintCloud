<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ThemeSpecificBanner.ascx.cs" Inherits="Web2Print.UI.Controls.ThemeSpecificBanner" %>
<div class="container themebannerContainer">
    <div id="top-banner" class="carousel slide hidden-xs " data-interval="6000" data-ride="carousel">
        <ol class="carousel-indicators">
            <li data-target="#top-banner" data-slide-to="0" class="active"></li>
            <li data-target="#top-banner" data-slide-to="1"></li>
        </ol>
        <div class="carousel-inner">
            <div class="item active text-center">
                <img id="banner1Img" runat="server" src="../App_Themes/Classic/Images/banner1.jpg" alt="Banner 1" title="Banner 1" /><div class="carousel-caption" style=""></div>
            </div>
            <div class="item text-center">
                <img id="banner2Img" runat="server" src="../themes/retroresponsive//images/flashgallary/large/banner2.jpg" alt="banner 2" title="banner 2" /><div class="carousel-caption" style=""></div>
            </div>
        </div>
        <a class="left carousel-control" href="#top-banner" data-slide="prev"><span class="glyphicon glyphicon-chevron-left"></span></a><a class="right carousel-control" href="#top-banner" data-slide="next"><span class="glyphicon glyphicon-chevron-right"></span></a>
    </div>
</div>
<script src="../js/script.js"></script>
