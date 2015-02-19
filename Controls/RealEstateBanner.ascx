<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RealEstateBanner.ascx.cs"
    Inherits="Web2Print.UI.Controls.RealEstateBanner" %>

<div class="content_area featuredProductsCarousel" style="width: 100%;">
    <link href="../Scripts/js-image-slider.css" rel="stylesheet" />
    <script src="../Scripts/js-image-slider.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {

            var bannerCount = '<%=this.bannerCount%>';
            var autoMode = true;

            if (bannerCount == "0" || bannerCount == "1") {
                autoMode = false;
            }

            $('.bxslider').bxSlider({
                mode: 'fade',
                adaptiveHeight: 'true',
                auto: autoMode,
                speed: 6600
            });

            $('.bx-wrapper').css(
                {
                    'margin': '0px',
                    'box-shadow': '0px 0px 14px 5px lightgray',
                    'position': 'relative',
                    'z-index': '99'
                });

            $('.bx-controls').css('display', 'none');
        });
    </script>
    <style type="text/css">
        body {
            margin: 0;
            padding: 0;
        }

        .bx-wrapper .bx-viewport {
            border: none;
            left: 0;
        }

        ul.bxslider {
            margin: 0;
            padding: 0;
        }

        div.slide-desc {
            position: absolute;
            padding-top: 2%;
            top: 0%;
            right: 0%;
            width: 25%;
            height: 100%;
            color: white;
            overflow: hidden;
            background-color: rgba(0, 0, 0, .4);
            box-shadow: -12px 0 90px 45px rgba(0, 0, 0, 0.4);
        }

        div.slide-desc-parent {
            width: 310px;
            height: 350px;
            background-color: rgba(0, 0, 0, .4);
            position: absolute;
            top: 1px;
            right: 1px;
            box-shadow: -12px 0 30px 18px rgba(0, 0, 0, 0.4);
        }
    </style>
    <ul class="bxslider">
        <asp:Repeater ID="rptRealEstateBanners" runat="server" OnItemDataBound="rptRealEstateBanners_ItemDataBound">
            <ItemTemplate>
                <li style="width: 100%;">
                    <div class="banner">
                        <asp:HyperLink ID="hlProductDetail" runat="server">
                            <div>
                                <asp:Image ID="imgbanner" runat="server" Style="height: 350px; width: 100%" />
                            </div>
                        </asp:HyperLink>
                        <%--<div class="slide-desc-parent">--%>
                        <div class="slide-desc">
                            <asp:Label ID="lblTitle" runat="server" Style="font-size: 20px;"></asp:Label><br />
                            <div style="margin-right: 20px;">
                                <asp:Literal ID="desc" runat="server"></asp:Literal>
                            </div>
                        </div>
                        <%--  </div>--%>
                    </div>
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>

    <div class="clearBoth">
        &nbsp;
    </div>
</div>

