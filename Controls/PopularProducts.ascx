<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PopularProducts.ascx.cs"
    Inherits="Web2Print.UI.Controls.PopularProducts" %>
<div class="content_area left_right_padding paddingS4 PopularProducts">
    <div id="controlBodyDiv" runat="server" class="PopularProductsSub">
        <div class="feature_product_main_heading">
            <asp:Literal ID="ltrlPopularProducts" runat="server" Text=" Popular Products"></asp:Literal>
        </div>
        <asp:Repeater ID="dlPopularProducts" runat="server" OnItemDataBound="dlPopularProducts_ItemDataBound">
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
                        <div class="product_detail_image_heading">
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
    </div>
</div>
