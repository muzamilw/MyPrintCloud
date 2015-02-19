<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="Web2Print.UI.Controls.Header" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<div id="divShd" class="opaqueLayer" style="z-index: 999 !important;">
</div>
<div id="divHEaderMenu" runat="server" class="top_sub_section TopMenuH60W100Px">
<div id="div1" runat="server" class="float_left_simple corportate_header_container">
            <ul id="Ul2" class="nav neg_left_margin_5 cursor_pointer">
            <li><a id="A2" href="~/Default.aspx" runat="server" class="top_sub_section_links HomeIconBox">
                      <img id="Img1" runat="server"  width="25" alt="" src="~/App_Themes/S6/Images/home.png"/> </a></li>
                      </ul>
                      </div>
                      <div id="Div2" runat="server" class="float_right Fsize15" style=" width: 115px; margin-top: -5px;" >
            <ul class="nav neg_left_margin_5 cursor_pointer">
             <li id="li1" runat="server">
                        <asp:HyperLink ID="HyperLink1" runat="server" CssClass="top_sub_section_links marginLeft"
                            NavigateUrl="~/ContactUs.aspx">Contact Us</asp:HyperLink>
                    </li></ul>
            </div>
</div>
<div id="PanelNormalUsersHeader" class="top_sub_section TopMenuH60W100Px" runat="server"
    visible="false">
    <script src="/Scripts/jquery-ui-1.9.2.custom.min.js" type="text/javascript"></script>
    <div class="content_area">
        <div class="left_right_padding">
            <div id="divMenu" runat="server" class="menu" visible="false">
                <ul id="menu-main-menu" class="nav neg_left_margin_5 cursor_pointer">
                    <li><a id="anchorHomeImage" runat="server" visible="false">
                        <div id="btnHome" runat="server" class="homeIconImge" visible="false">
                        </div>
                    </a></li>
                    <li><a id="lnkHomePage" href="~/Default.aspx" runat="server" class="top_sub_section_links marginRght">
                        Home </a></li>



                    <li id="AllProMenuLi" class="sub" runat="server"> <a id="lnkProducts" href="~/AllProducts.aspx"
                        runat="server" class="top_sub_section_links marginRght">All Products</a>






                        <ul class="allProduct">
                            <li class="allProduct BSBC" id="allProductsMenu" runat="server">
                                <div id="controlBodyDiv" runat="server" class="FP_container LCLB rounded_corners">
                                    <div class="product_detail_image_heading_headerPage themeFontColor">
                                        &nbsp;<asp:Literal ID="ltrlFeaturedProducts" runat="server" Text="Featured Products"></asp:Literal>
                                    </div>
                                    <div class="border_bottom_line">
                                        &nbsp;</div>
                                    <asp:Repeater ID="dlFeaturedProducts" runat="server" OnItemDataBound="dlFeaturedProducts_ItemDataBound">
                                        <ItemTemplate>
                                            <div class="product_detail_image_heading_headerPage_SubH">
                                                <asp:HyperLink ID="hlFP" runat="server">
                                                    <asp:Label ID="lblProductName" runat="server" Text='<%#Eval("ProductName","{0}") %>'
                                                        CssClass="themeFontColor"></asp:Label><br />
                                                </asp:HyperLink>
                                            </div>
                                            <div class="clearBoth">
                                                &nbsp;
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                                <div class="DoubleDataListClass" style="float: left;">
                                    <asp:DataList ID="dlAllProdReapter" runat="server" CssClass="DataListOfAllProducts"
                                        RepeatDirection="Vertical" CellPadding="0" CellSpacing="20" BorderStyle="None"
                                        ShowFooter="False" ShowHeader="False" OnItemDataBound="dlAllProdReapter_ItemDataBound">
                                        <ItemTemplate>
                                            <div>
                                                <asp:Label ID="lblLetter" runat="server" CssClass="themeFontColor"></asp:Label>
                                            </div>
                                            <div class="border_bottom_line">
                                                &nbsp;</div>
                                            <asp:DataList ID="dlAllProd" runat="server" CssClass="DataListOfdlAllProd" RepeatColumns="1"
                                                RepeatDirection="Vertical" CellPadding="0" CellSpacing="0" BorderStyle="None"
                                                ShowFooter="False" ShowHeader="False" OnItemDataBound="dlAllProd_ItemDataBound">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hlMenuList" runat="server" CssClass="colorOfLinks">
                                                            

                                                    </asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </div>
                                <div class="clearBoth">
                                    &nbsp;
                                </div>
                            </li>
                        </ul>
                    </li>
                    <li id="liInquiry" runat="server">
                        <asp:HyperLink ID="hlRequestQuote" runat="server" CssClass="top_sub_section_links marginRght marginLeft"
                            NavigateUrl="~/RequestQuote.aspx">Request A Quote</asp:HyperLink></li>
                    <li class="sub"><a id="LetUsHelp" href="#" runat="server" class="top_sub_section_links marginRght "
                        onclick="return false;"></a>
                        <asp:BulletedList ID="BLQL" runat="server" CssClass="uSecPageList top_sub_section W100PerH60Px"
                            ViewStateMode="Enabled" DisplayMode="HyperLink">
                        </asp:BulletedList>
                    </li>
                    <li id="liContactUs" runat="server">
                        <asp:HyperLink ID="hypContUs" runat="server" CssClass="top_sub_section_links marginRght marginLeft"
                            NavigateUrl="~/ContactUs.aspx">Contact Us</asp:HyperLink></li>
                </ul>
            </div>
            <div id="divCorporateUser" runat="server" class="float_left_simple corportate_header_container">
            <ul id="Ul1" class="nav neg_left_margin_5 cursor_pointer">
            <li><a id="A1" href="~/Default.aspx" runat="server" class="top_sub_section_links HomeIconBox">
                      <img id="DefaultHomeIcon" runat="server"  width="25" alt="" src="~/App_Themes/S6/Images/home.png"/> </a></li>
                      </ul>
               <%-- <div class="t_hr_ce_item_home">
                    <asp:HyperLink ID="HypHome" runat="server" CssClass="top_sub_section_links_corporate"
                        NavigateUrl="~/Default.aspx">Home</asp:HyperLink>
                </div>
                <div class="t_hr_ce_item_home">
                    <asp:HyperLink ID="HyperLink1" runat="server" CssClass="top_sub_section_links_corporate"
                        NavigateUrl="~/RequestQuote.aspx">Request A Quote</asp:HyperLink>
                </div>
                <div class="t_hr_ce_item_home">
                    <asp:HyperLink ID="hlContactUs" runat="server" CssClass="top_sub_section_links_corporate"
                        NavigateUrl="#">Contact Us</asp:HyperLink>
                </div>
                <div class="t_hr_ce_item_home">
                    <asp:HyperLink ID="hlAboutUs" runat="server" CssClass="top_sub_section_links_corporate"
                        NavigateUrl="#">About Us</asp:HyperLink></div>
                <div class="clearBoth">
                    &nbsp;</div>--%>
            </div>
            <div id="ContactUSCOntainer" runat="server" class="float_right Fsize15" style=" width: 115px; margin-top: -5px;" >
            <ul class="nav neg_left_margin_5 cursor_pointer">
             <li id="liContcUS" runat="server">
                        <asp:HyperLink ID="hyprContUs" runat="server" CssClass="top_sub_section_links marginLeft"
                            NavigateUrl="~/ContactUs.aspx">Contact Us</asp:HyperLink>
                    </li></ul>
            </div>
         <%--   <div class="login_bar" >
                <ul id="menu-my-account" class="nav">
                    <li class="clore">
                        <asp:Label ID="lblUserInfo" runat="server" Text="" CssClass="Fsize13 top_sub_section_links" />
                    </li>
                    <li id="myAccStar" runat="server" class="ZeroRightMargin">
                        <img id="myAccStarImg" runat="server" alt="" src="~/images/star.png" class="Width20Percent" />
                    </li>
                    <li class="sub"><a id="lnkMyAccount" runat="server" href="~/DashBoard.aspx" class="top_sub_section_links">
                        My Account </a>--%>
                       <%-- <ul id="ulMyAccount" class="uSecPageList top_sub_section W100PerH60Px" runat="server"
                            visible="false">
                            <li class="paddLeft"><a id="anchDashBoard" runat="server" href="~/DashBoardNew.aspx">
                                Dashboard</a>
                            </li>
                            <li class="paddLeft"><a id="anchorContctDetail" runat="server" href="~/UserProfile.aspx">
                                Contact Details</a></li>
                            <li class="paddLeft"><a id="anchorQuickTxt" runat="server" href="~/UserQuickTextInfo.aspx">
                                Images,Logos,Quick text</a></li>
                            <li id="ChangePassli" class="paddLeft" runat="server"><a id="anchorChngePass" runat="server"
                                href="~/UserProfile.aspx">Change Password</a></li>
                            <li class="paddLeft"><a id="anchorShopingCrt" runat="server" href="~/ShopCart.aspx">
                                Shopping Cart</a></li>
                            <li class="paddLeft"><a id="anchorOrderHistory" runat="server" href="~/ProductsOrdersHistory.aspx">
                                Order History</a></li>
                            <li id="liOrderingPoli" runat="server" class="paddLeft" visible="false"><a id="anchorOrdering"
                                runat="server" href="~/OrderingPolicy.aspx">Ordering Policy</a></li>
                            <li id="liBrokerPRod" runat="server" class="paddLeft" visible="false"><a id="anchorBrokerProd"
                                runat="server" href="~/BrokerAdminProducts.aspx">Products Manager</a></li>
                            <li id="literritory" runat="server" class="paddLeft" visible="false"><a id="anchorterritory"
                                runat="server" href="~/UserTerritoryManager.aspx">Territory Manager</a></li>
                            <li id="lidepartments" runat="server" class="paddLeft" visible="false"><a id="anchordepartments"
                                runat="server" href="~/UserDepartments.aspx">Departments Manager</a></li>
                            <li id="liBrokerVoucher" runat="server" class="paddLeft" visible="false"><a id="anchorVouc"
                                runat="server" href="~/BrokerVoucherWiget.aspx">Voucher Manager</a></li>
                            <li id="liAddressManager" runat="server" class="paddLeft"><a id="anchorAddressMgr"
                                runat="server" href="~/ShippingAddressManager.aspx">Address Manager </a></li>
                            <li id="liPendingAppr" class="paddLeft" runat="server"><a id="anchorPendingAppr"
                                runat="server">Pending Approvals</a></li>
                            <li><a id="lblMyFavCount" runat="server" class="ColorRedMLef FavCount" href="#"></a>
                                <a id="anchorMyFav" runat="server" href="~/FavContactDesigns.aspx">My Favorites</a></li>
                            <li><a id="CountOfSavedDesign" runat="server" class="ColorRedMLef FavCount"></a><a
                                id="anchorSavDesgn" runat="server" href="~/SavedDesignes.aspx">Saved Designs</a></li>
                            <li id="liOrderProduct" runat="server"><a id="CountOrderProduct" runat="server" class="ColorRedMLef FavCount"
                                href="#"></a><a id="anchorOrderProduct" runat="server">Orders in Production</a></li>
                            <li id="liUserManager" runat="server" visible="false"><a id="CountUserManager" runat="server"
                                class="ColorRedMLef FavCount"></a><a id="anchorUserMgr" runat="server" href="~/UserManager.aspx">
                                    User Manager </a></li>
                        </ul>--%>
             <%--       </li>
                    <li>
                        <asp:LinkButton ID="lnkBtnSignOut" runat="server" Text="Sign Out" CommandName="SignOut"
                            Visible="false" CausesValidation="false" CssClass="top_sub_section_links" OnClick="lnkLogin_Click" />
                    </li>
                    <li>
                        <asp:HyperLink ID="lnkLogin" runat="server" CssClass="top_sub_section_links " NavigateUrl="~/Login.aspx"
                            Visible="false">Sign In / Register</asp:HyperLink>
                    </li>
                    <li class="ZeroRightMargin">
                        <asp:Image ID="imgCart" runat="server" SkinID="imgCart" />
                        <asp:LinkButton ID="lnkBtnViewMyCart" runat="server" Text="Cart(0)" CausesValidation="false"
                            OnClick="lnkBtnViewMyCart_Click" Style="vertical-align: top" CssClass="top_sub_section_links" />
                    </li>
                </ul>
            </div>--%>
        </div>
        <div style="clear: both;">
        </div>
    </div>
    <div class="top_sub_section_bottom_space">
        <br />
    </div>
</div>