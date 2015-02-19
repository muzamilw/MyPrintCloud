<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CategoryListing.ascx.cs"
    Inherits="Web2Print.UI.Controls.CategoryListing" %>
<div class="content_area left_right_padding AllProductsCatList ">
    <div class="LCL CategoryListing">
        <div class="feature_product_main_heading" id="divHeading" runat="server">
            <asp:Literal ID="ltrlProductandServices" runat="server" Text=" Products and Services"></asp:Literal>
        </div>
        <div class="pre_LCLB LCLB rounded_corners">
            <asp:Repeater ID="rptCategoryNames" runat="server" OnItemDataBound="rptCategoryNames_ItemDataBound">
                <ItemTemplate>
                    <div class="product_detail_image_heading_headerPage_SubH">
                        <asp:HyperLink ID="hlFP" runat="server">
                            <asp:Label ID="lblCategoryName" runat="server"  Style="font-size: 14px;
                                font-weight: bolder; text-decoration: none;"></asp:Label><br />
                        </asp:HyperLink>
                    </div>
                    <div class="clearBoth">
                        &nbsp;
                    </div>
                    <asp:Repeater ID="rptCategories" runat="server" OnItemDataBound="rptCategories_ItemDataBound">
                        <ItemTemplate>
                            <div class="category_listing_item">
                                <asp:HyperLink ID="hlFP" runat="server">
                                    <asp:Label ID="lblCategories" runat="server" ></asp:Label><br />
                                </asp:HyperLink>
                            </div>
                            <div class="clearBoth">
                                &nbsp;
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</div>
