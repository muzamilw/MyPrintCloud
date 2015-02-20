<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PromotionalBanner.ascx.cs"
    Inherits="Web2Print.UI.Controls.PromotionalBanner" %>
<div class="content_area left_right_padding paddingS4 PromotionalBanner">
    <div id="controlBodyDiv" runat="server" class="PromotionalBannerSub">
        <div class="feature_product_main_heading">
            <asp:Literal ID="ltrlSpecialOffers" runat="server" Text="Special Offers"></asp:Literal>
        </div>
        <asp:Repeater ID="dlSpecialProducts" runat="server" OnItemDataBound="dlSpecialProducts_ItemDataBound">
            <ItemTemplate>
                <div class="BD rounded_corners">
                    <div class="pad5">
                        <div class="LCLB">
                            <asp:HyperLink ID="hlProductDetail" runat="server">
                                <div class="PDTC FI">
                                    <asp:Image ID="imgThumbnail" CssClass="full_img_ThumbnailPath" runat="server" ImageUrl='<%# Eval("ThumbnailPath","{0}") %>' />
                                </div>
                            </asp:HyperLink>
                        </div>
                        <div class="product_detail_image_heading LCLB">
                            <asp:Label ID="lblProductName" runat="server" Text='<%#Eval("ProductName","{0}") %>'
                               ></asp:Label>
                        </div>
                    </div>
                    <div id="PriceCircle" class="blue_cicle_container" runat="server">
                        <div class="BC">
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
        <div class="clear">
        </div>
        <br />
        <br />
    </div>
</div>
