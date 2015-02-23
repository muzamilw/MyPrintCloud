<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FeaturedProductCarousel.ascx.cs"
    Inherits="Web2Print.UI.Controls.FeaturedProductCarousel" %>
<div class="content_area  featuredProductsCarousel">
    <div id="controlBodyDiv" runat="server" class="width960">
        <div>
           
            <script src="../Scripts/jquery.jcarousel.lite.js" type="text/javascript"></script>
            <script src="../Scripts/jquery.lavalamp.js" type="text/javascript"></script>
            <script src="../Scripts/jquery.tools.js" type="text/javascript"></script>
            <script src="../Scripts/mainFeaturedGood.js" type="text/javascript"></script>
        </div>
        <div class="container-carousel">
            <div id="products">
                <div>
                    <div class="RelativePosition">
                        <a class="prev-btn" href="javascript:;"></a><a class="next-btn" href="javascript:;">
                        </a>
                        <div class="wrapper">
                            <ul id="mycarousel" class="jcarousel-skin-tango CarouseList">
                                <asp:Repeater ID="dlFeaturedProducts" runat="server" OnItemDataBound="dlFeaturedProducts_ItemDataBound">
                                    <ItemTemplate>
                                        <li class="LCLB BD rounded_corners transparentBk height220px">
                                            <asp:HyperLink ID="hlProductDetail" runat="server">
                                                <div class="pad5">
                                                    <div class="PDTCWB transparentBk">
                                                        <div class="PDTC FI transparentBk">
                                                            <asp:Image ID="imgThumbnail" runat="server" CssClass="full_img_ThumbnailPath" ImageUrl='<%# Eval("ThumbnailPath","{0}") %>' />
                                                        </div>
                                                    </div>
                                                    <div class="product_detail_image_heading padding0">
                                                        <asp:Label ID="lblProductName" runat="server" Text='<%#Eval("ProductName","{0}") %>'
                                                            CssClass="titleFproductCarousel"></asp:Label>
                                                        <br />
                                                        <div class="clearBoth">
                                                        </div>
                                                        <div id="PriceCircle" class="" runat="server">
                                                            <div class="BCCarousel">
                                                                <div class="all_padding3 hideLineBreaks">
                                                                    <div class="paddingTop2px">
                                                                        &nbsp;</div>
                                                                    FROM
                                                                    <br />
                                                                    <asp:Label runat="server" CssClass=" font10px" ID="lblFromMinPrice" Text='<%# Eval("MinPrice") %>' />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:HyperLink>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
