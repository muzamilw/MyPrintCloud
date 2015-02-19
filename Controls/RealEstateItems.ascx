<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RealEstateItems.ascx.cs"
    Inherits="Web2Print.UI.Controls.RealEstateItems" %>

<%--<div class="bkTopCategories">--%>
<div class="content_area  featuredProductsCarousel" style="width: 100%; height: 200px; box-shadow: inset 0 -10px 10px -10px gray, inset 0  10px 10px -10px gray;position: relative; z-index: 50;">

    <script src="../Scripts/SmartFormJCarousel.js"></script>
    <link href="../Styles/ListingPropertiesCarousel.css" rel="stylesheet" />
    <style type="text/css">
        .activeCampaigns {
            width: 25%;
            height: 175px;
            background-color: #e9501d;
            float: right;
            padding-top: 25px;
            text-align: center;
            color: white;
        }

        .campaignsHeading {
            font-size: 25px;
            font-family: Myriad Pro;
        }

        .campaignsLabel {
            font-size: 15px;
            font-family: Myriad Pro;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $('#mycarousel').jcarousel();
        });
    </script>
    <div>
        <div style="float: left; width: 75%; z-index: 0; position: relative; top: -30px;">
            <ul id="mycarousel" class="jcarousel-skin-tango" style="background-color: white; margin-top: 30px;">
                <asp:Repeater ID="rptRealEstateProperties" runat="server" OnItemDataBound="rptRealEstateProperties_ItemDataBound">
                    <ItemTemplate>
                        <li style="padding-right: 10px;">
                            <div>
                                <asp:HyperLink ID="hlProductDetail" runat="server">
                                    <div style="height: 120px;">
                                        <asp:Image ID="imgThumbnail" runat="server" Style="height: 100px; width: 150px;" />
                                    </div>
                                </asp:HyperLink>
                                <div>
                                    <asp:Label ID="lblbotmCatName" runat="server" Style="font-family: Myriad Pro; font-size: 14px; color: #e9501d;"></asp:Label>
                                </div>
                            </div>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
        <div class="activeCampaigns" style="z-index: 1; position: relative;">
            <div>
                <label class="campaignsHeading">Active Campaigns</label>
            </div>
            <div style="padding-top:15px;">
                <img src="../Styles/images/SmartFormCart.png" /><br />
            </div>
            <div style="padding-top:20px;">
                <label class="campaignsLabel">You have </label>
                <asp:Label ID="lblCartItemCount" runat="server">-</asp:Label>
                <label class="campaignsLabel"> items  |  </label>
                <asp:HyperLink ID="btnCheckout" runat="server" CssClass="campaignsLabel" NavigateUrl="~/PinkCardsShopCartAddSelect.aspx">Check out</asp:HyperLink>
            </div>
        </div>
    </div>
    <div class="clearBoth">
        &nbsp;
    </div>
</div>
