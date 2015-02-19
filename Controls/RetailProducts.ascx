<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RetailProducts.ascx.cs"
    Inherits="Web2Print.UI.Controls.RetailProducts" %>
<div class="content_area left_right_padding paddingS4 RProdClearB">
    <div id="controlBodyDiv" runat="server">
        <div class="feature_product_main_heading">
            &nbsp;<asp:Literal ID="ltrlFeaturedProducts" runat="server" Text="Choose a Design"></asp:Literal>
        </div>
        <asp:Repeater ID="rpProducts" runat="server" OnItemDataBound="RetailProducts_ItemDataBound">
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
                                CssClass="themeFontColor"></asp:Label>
                        </div>
                    </div>
                    <div id="PriceCircle" class="blue_cicle_container" runat="server">
                        <div class="BC">
                            <div class="all_padding3">
                                <div class="paddingTop2px">
                                    &nbsp;</div>
                                FROM
                                <br />
                                <asp:Label runat="server" ID="lblFromMinPrice" Font-Bold="true" Font-Size="16px" />
                            </div>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>
<div class="clearBoth">
</div>
