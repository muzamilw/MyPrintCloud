<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FeaturedProducts.ascx.cs"
    Inherits="Web2Print.UI.Controls.FeaturedProducts" %>
<div class="clearBoth">
    &nbsp;
</div>
<div class="bgS5White featureproductContainer">
    <div class=" featuredProducts content_area">
        <div id="controlBodyDiv" runat="server">
            <div class="feature_product_main_heading hidden-xs hidden-sm" id="divHeading" runat="server" visible="false">
                &nbsp;<asp:Literal ID="ltrlFeaturedProducts" runat="server" Text="Featured Products"></asp:Literal>
            </div>
            <asp:Repeater ID="dlFeaturedProducts" runat="server" OnItemDataBound="dlFeaturedProducts_ItemDataBound">
                <ItemTemplate>
                    <div class="BD_Featured  widthS5 rounded_corners ItemFeatureProduct">
                        <div class="pad5">
                            <div class="cntImgThumbnail ">
                                <asp:HyperLink ID="hlProductDetail" runat="server">
                                    <div class="PDTC FI_feautured FIS5 HeightImgContainerS5">
                                        <asp:Image ID="imgThumbnail" runat="server" CssClass="full_img_ThumbnailPath" ImageUrl='<%# Eval("ThumbnailPath","{0}") %>' />
                                    </div>
                                </asp:HyperLink>
                            </div>
                           <div class="cntfeaturedProdName product_detail_image_heading s5TxtStyle">
                                <asp:Label ID="lblProductName" runat="server" Text='<%#Eval("ProductName","{0}") %>'
                                    ></asp:Label>
                            </div>
                            <div class="cntfeaturedProdDesc">
                                <asp:Label ID="lblFeaturedProDes" runat="server" Text='<%#Eval("ProductWebDescription","{0}") %>'
                                   ></asp:Label>
                            </div>
                        </div>
                        <div id="PriceCircle" class="blue_cicle_container" runat="server">
                            <div class="BC BCS5">
                                <div class="all_padding3">
                                    <div class="paddingTop2px">
                                        &nbsp;</div>
                                    FROM
                                    <br />
                                    <asp:Label runat="server" ID="lblFromMinPrice" Text='<%# Eval("MinPrice") %>' Font-Bold="true"
                                        Font-Size="16px" />
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <div class="clear"></div>
        </div>
    </div><div class="clear"></div>
            <div class="callUsLblFP hidden-xs hidden-sm">
                           Call us for other Great Offers !
        </div>
    <div class="clear"></div>

</div>
