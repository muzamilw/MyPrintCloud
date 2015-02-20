<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HomePageSlider.ascx.cs" Inherits="Web2Print.UI.Controls.HomePageSlider" %>

<%@ Register Src="~/Controls/RSSWidget.ascx" TagName="MatchingSet" TagPrefix="uc1" %>
<%@ Register Src="~/Controls/SavedDesignsWidget.ascx" TagName="MatchingSet" TagPrefix="uc2" %>
<%@ Register Src="~/Controls/EditorsChoice.ascx" TagName="MatchingSet" TagPrefix="uc3" %>
<%@ Register Src="~/Controls/DesignTutorials.ascx" TagName="MatchingSet" TagPrefix="uc4" %>
<%@ Register Src="~/Controls/FeaturedProducts.ascx" TagName="featuredProducts" TagPrefix="uc5" %>
<div class="clearBoth HomePageSliderHeadingBG"></div>

<div class="mainDivRelativeContainer " style="position: relative; padding-left: 0px;
padding-right: 0px;">
    <div class="MenuSliderContainer">
        <ul class="tabItemMenuUL hidden-xs">

                          <li class="tabItemMenu" onclick="showTab(1);">Editor picks
                            <br />
                            <div class="tabMaskContainer tabControl1" style="margin-left: 28px;">
                                <div class="tabMask"></div>
                            </div>
                        </li>
                        <li class="tabItemMenu" onclick="showTab(2);">My designs
                            <br />
                            <div class="tabMaskContainer tabControl2" style="margin-left: 60px;">
                                <div class="tabMask"></div>
                            </div>
                        </li>
                        <li class="tabItemMenu" onclick="showTab(3);">Charity blog
                            <br />
                            <div class="tabMaskContainer tabControl3" style="margin-left: 30px;">
                                <div class="tabMask"></div>
                            </div>
                        </li>
                        <li class="tabItemMenu" onclick="showTab(4);">Design tutorials
                            <br />
                            <div class="tabMaskContainer tabControl4" style="margin-left: 42px;">
                                <div class="tabMask"></div>
                            </div>
                        </li>
                        <li class="tabItemMenu" onclick="showTab(5);">Special offers
                            <br />
                            <div class="tabMaskContainer tabControl5" style="margin-left: 42px;">
                                <div class="tabMask"></div>
                            </div>
                        </li>
                    </ul>
                </div>
    <div class="MenuSliderContainerContainer top_sub_section">
        <div class="SliderContainer">
             
                <script>

                    var cItem = 1;
                    var CenterLeft = 0;

                   
                    $(document).ready(function () {
                        

                        if ($(window).width() >= 961) {
                            
                        //    CenterLeft = ($(window).width() - $(".control1").width()) / 2;

                        //    $(".control" + cItem).css("left", CenterLeft + "px");

                            $(".mainDivRelativeContainer").css("height", $(".control" + 1).height() + 80 + "px");
                            CenterLeft = ($(window).width() - $(".control1").width()) / 2;
                            $(".controldiv").css("left", CenterLeft + "px");
                            $(".tabControl" + cItem).css("visibility", "visible");
                        } else {
                            $(".control1").css("display", "block");
                            $(".control2").css("display", "block");
                            $(".control5").css("display", "block");
                            $(".control3").css("display", "block");
                            $(".control4").css("display", "block");
                            $(".control1").css("left", "0px !important");
                            $(".control2").css("left", "0px !important");
                            $(".control3").css("left", "0px !important");
                            $(".control4").css("left", "0px !important");
                            $(".control5").css("left", "0px !important");
                            $(".control1").css("clear", "both");
                            $(".control2").css("clear", "both");
                            $(".control5").css("clear", "both");
                            $(".control3").css("clear", "both");
                            $(".control4").css("clear", "both");
                        }

                    });

                function showTab(index) {
                    $(".tabControl" + index).css("visibility", "visible");
                    $(".tabControl" + cItem).css("visibility", "hidden");
                    if (cItem != index) {
                        if (index > cItem) {
                            var left = 0 - $(".control1").width();
                            var left2 = $(window).width() + $(".control1").width();
                            $(".control" + index).css("display", "block");
                            $(".control" + index).css("opacity", "0");
                            $(".control" + index).css("left", left2 + "px");
                            $(".control" + index).animate({
                                opacity: 1,
                                left: CenterLeft
                            }, 1000, function () {
                                if (index == 3) {
                                    $(".mainDivRelativeContainer").css("height", $(".control" + index).height() + 80 + "px");
                                } else {
                                    $(".mainDivRelativeContainer").css("height", $(".control" + index).height() + 80 + "px");
                                }
                            });
                            $(".control" + cItem).animate({
                                opacity: 0,
                                left: left
                            }, 1000, function () {
                                $(".control" + cItem).css("display", "none");
                                $(".control" + cItem).css("opacity", "1");
                                $(".control" + cItem).css("left", CenterLeft + "px");
                                cItem = index;

                            });
                            $('html, body').animate({
                                scrollTop: ($(".mainDivRelativeContainer").offset().top - 25)
                            }, 500);
                          
                        } else {
                            var left = $(window).width() + $(".control1").width();
                            var left2 = 0 - $(".control1").width();
                            $(".control" + index).css("display", "block");
                            $(".control" + index).css("opacity", "0");
                            $(".control" + index).css("left", left2 + "px");
                            $(".control" + index).animate({
                                opacity: 1,
                                left: CenterLeft
                            }, 1000, function () {
                                if (index == 3) {
                                    $(".mainDivRelativeContainer").css("height", $(".control" + index).height() + 80 + "px");
                                } else {
                                    $(".mainDivRelativeContainer").css("height", $(".control" + index).height() + 80 + "px");
                                }
                            });
                            $(".control" + cItem).animate({
                                opacity: 0,
                                left: left
                            }, 1000, function () {
                                $(".control" + cItem).css("display", "none");
                                $(".control" + cItem).css("opacity", "1");
                                $(".control" + cItem).css("left", CenterLeft + "px");
                                cItem = index;

                            });
                            $('html, body').animate({
                                scrollTop: ($(".mainDivRelativeContainer").offset().top - 25)
                            }, 500);
                        }
                    }
                   
                }

            </script>
            <div id="tab-slider container">

                <div class="divsliderControlsContainer row">
                    <div class="control1 controldiv" style="display: block;">
                        <div class="hidden-lg hidden-md visible-xs visible-sm col-xs-12 editoSliderHeadings" style="font-size: 33px; text-align: left;  color: white;">
                            Editor picks
                        </div>
                        <div>
                            
                            <uc3:MatchingSet ID="MatchingSet2" runat="server" />
                        </div>
                    </div>
                    <div class="control2 controldiv   " style="display: none; ">
                        <div class="hidden-lg hidden-md visible-xs visible-sm editoSliderHeadings" style="font-size: 33px; text-align: left; padding-left: 13px; color: white;">
                            My designs
                        </div>
                        <div>
                            <uc2:MatchingSet ID="MatchingSet1" runat="server" />
                        </div>
                    </div>
                    <div class="control3 controldiv  " style="display: none;">
                        <div class="hidden-lg hidden-md visible-xs visible-sm editoSliderHeadings" style="font-size: 33px; text-align: left; padding-left: 13px; color: white;">
                            Charity blog
                        </div>
                        <div>
                            <uc1:MatchingSet ID="EC" runat="server" />
                        </div>
                    </div>
                    <div class="control4 controldiv  " style="display: none;">
                        <div class="hidden-lg hidden-md visible-xs visible-sm editoSliderHeadings" style="font-size: 33px; text-align: left; padding-left: 13px; color: white;">
                            Design tutorials
                        </div>
                        <div>
                            <uc4:MatchingSet ID="MatchingSet3" runat="server" />
                        </div>
                    </div>
                    <div class="control5 controldiv  " style="display: none;">
                        <div class="hidden-lg hidden-md visible-xs visible-sm editoSliderHeadings" style="font-size: 33px; text-align: left; padding-left: 13px; color: white;">
                            Special offers
                        </div>
                        <div>
                            <uc5:featuredProducts ID="MatchingSet4" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<div class="clearBoth"></div>
