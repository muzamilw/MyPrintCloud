<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SeaBlueTopLevelCategories.ascx.cs" Inherits="Web2Print.UI.Controls.SeaBlueTopLevelCategories" %>
<div id="middle-content" class="container">
    <div class="row main-content">
        <div class="col-xs-12">
            <div class="middle_inner_section">
                <div class="row" id="home_page">
                    <asp:Repeater id="seaBlueTopCategories" runat="server" OnItemDataBound="seaBlueTopCategories_ItemDataBound">
                        <ItemTemplate>
                            <div class="col-sm-12 col-md-3 product-box" itemscope itemtype="http://schema.org/Product">
                                <div class="hover-box-shadow">
                                    <p class="text-center">
                                        <a id="ProductUrl" runat="server" href="../en/standard-business-cards/index.htm" class='thumbnail'>
                                            <img id="productImge" runat="server" src="../themes/seablue//images/product/business-card_thumb.jpg" alt="Standard Business Cards" title="Standard Business Cards" class="img-responsive" itemprop="image" /></a>
                                    </p>
                                    <h3 itemprop="name" id="productName" runat="server">Standard Business Cards</h3>
                                    <div class="product-desc" itemprop="description">
                                        <p id="productDesc" runat="server">Create cards that make the first impression</p>
                                    </div>
                                    <p class="text-center"><a id="prductlinkUrl" runat="server" href="../en/standard-business-cards/index.htm" class='btn btn-info' itemprop="url">View details <i class="fa fa-angle-double-right"></i></a></p>
                                </div>
                            </div>
                        </ItemTemplate>

                    </asp:Repeater>

                </div>
            </div>
        </div>
    </div>
</div>
