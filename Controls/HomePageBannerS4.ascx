<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HomePageBannerS4.ascx.cs"
    Inherits="Web2Print.UI.Controls.HomePageBannerS4" %>
<%@ OutputCache VaryByParam="true" Duration="10" %>
<div id="bannerContainer" class="" runat="server">
    <script type="text/javascript" src="../Scripts/fancybox/jquery.fancybox-1.3.4.pack.js"></script>
    <link rel="stylesheet" href="../Scripts/fancybox/jquery.fancybox-1.3.4.css" type="text/css" media="screen" />
    
    <script type="text/javascript" src="../Scripts/jquery.jcarousel.lite.js"></script>
    <script src="../Scripts/HomeBannerS4.js" type="text/javascript"></script>

    <div class="body">
        <div id="page">
            <div id="content">
                <div class="billboard">
                    <div class="top-shadow" style="height: 330px; padding: 25px 0 0 0;">
                        <div class="intro">
                            <h1 style="margin-top: 20px; font-size: 30px; margin-bottom: -5px;" class="introHeading">
                               .</h1>
                            <p class="introDescription">
                                .</p>
                            <ul style="margin-top: 34px;">
                                <li class="btn_start_creating_Slider"><a href="" class="order-now btn_start_creating_Slider_txt "> Start Creating
                                    <%--<img src="../App_Themes/S4/Images/sliderImgs/Start-Now-Blue-Button.png" alt="Start Now" />--%></a></li>
                            </ul>
                        </div>
                        
                        <div id="sliderBillBoard">
                            <img style="position: absolute; left: 0; top: 0; z-index: 1; margin: -3px 0 0 6px;"
                                src="../App_Themes/S4/Images/sliderImgs/slider-lens-and-shadow.png" />
                            <a class="video-link" href="http://www.youtube.com/">
                                <img class="video-corner" src="../App_Themes/S4/Images/sliderImgs/VideoCorner.png"
                                    alt="Watch Videos Now" /></a>
                            <div id="slides">
                                <a class="arrowS4 left" id="prev">
                                    <img src="../App_Themes/S4/Images/sliderImgs/Next-Slider-Left-Button.png" alt="Prev" /></a>
                                <a class="arrowS4 right" id="next">
                                    <img src="../App_Themes/S4/Images/sliderImgs/Next-Slider-Right-Button.png" alt="Next" /></a>
                                <ul >
                                <asp:Repeater ID="listImgSlider" runat="server">

                                <ItemTemplate>
                                    <li><asp:Image runat="server" ImageUrl='<%#bind("ImageUrl") %>' class="sliderImgClass" alt="" ID="img1" />
                                        <asp:Label runat="server" id="bndes" CssClass="sliderDescription"  Text='<%#bind("Description") %>'  />
                                        <asp:Label runat="server" id="bnhead" CssClass="sliderTitle"  Text='<%#bind("Heading") %>'  />
                                        <asp:Label runat="server" id="bnrefUrl" CssClass="sliderRefrenceUrl"  Text='<%#bind("ButtonURL") %>'  />
                                        <asp:Label runat="server" id="btnVidUrl" CssClass="sliderVideoUrl"  Text='<%#bind("ItemURL") %>'  />
                                    </li>
                                </ItemTemplate>
                                </asp:Repeater>
                                </ul>

                            </div>
                        </div>
                        <div class="relax">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<div class="clearBoth">
</div>
