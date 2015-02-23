<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ItemRelatedItems.ascx.cs"
    Inherits="Web2Print.UI.Controls.ItemsRelatedItems" %>
<div id="controlBodyDiv" runat="server" class="row">
    <div class="feature_product_main_heading col-md-12 col-lg-12 col-xs-12">
        <asp:Label ID="lblHeading" runat="server"></asp:Label>
        <asp:Label ID="lblYoumightLke" runat="server"></asp:Label>
    </div>
    <asp:Repeater ID="dlRelatedItems" runat="server"   OnItemDataBound="dlRelatedItems_ItemDataBound">
        <ItemTemplate>
            <div class=" col-md-3 col-lg-3 col-xs-12 BD rounded_corners">
                <div class="pad5">
                    <div class="LCLB">
                        <asp:HyperLink ID="hlProductDetail" runat="server">
                            <div class="PDTC FI">
                                <asp:Image ID="imgThumbnail" runat="server" CssClass="full_img_ThumbnailPath" ImageUrl='<%# Eval("ThumbnailPath","{0}") %>' />
                            </div>
                        </asp:HyperLink>
                    </div>
                    <div class="product_detail_image_heading_IRI">
                        <asp:Label ID="lblProductName" runat="server" Text='<%#Eval("ProductName","{0}") %>' CssClass="themeFontColor"></asp:Label>
                    </div>
                    <div class="product_detail_image_Pricing">
                    <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("MinPrice") %>' ></asp:Label>
                    </div>
                </div>
                <div id="PriceCircle" class="blue_cicle_container DisplayNoneCSS6" runat="server">
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
    <div class="clearBoth">
        &nbsp;
    </div>
</div>
