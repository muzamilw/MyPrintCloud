<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ThemeSite.Master" AutoEventWireup="true"
    CodeBehind="ProductDetails.aspx.cs" Inherits="Web2Print.UI.ProductDetails" %>

<%@ Register Src="/Controls/BreadCrumbMenu.ascx" TagName="BreadCrumbMenu" TagPrefix="uc1" %>
<%@ Register Src="/Controls/WhyChooseUs.ascx" TagName="WhyChooseUs" TagPrefix="uc2" %>
<%@ Register Src="/Controls/ItemRelatedItems.ascx" TagName="RelatedItems" TagPrefix="uc3" %>
<%@ Register Src="Controls/MatchingSet.ascx" TagName="MatchingSet" TagPrefix="uc4" %>
<%@ Register Src="~/Controls/RaveReview.ascx" TagName="RRViews" TagPrefix="uc5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContents" runat="server">
    <link href="/Styles/jquery.rating.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/js-image-slider.css" rel="stylesheet" type="text/css" media="screen" />
    <link href="/Styles/jquery-ui-1.10.0.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBanner" runat="server">
</asp:Content>
<asp:Content ID="ProductDetailsContents" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
        <Scripts>
            <asp:ScriptReference Path="/Scripts/js-image-slider.js" />
            <asp:ScriptReference Path="/Scripts/utilities.js" />
            <asp:ScriptReference Path="/Scripts/jquery-ui-1.9.0.js" />
        </Scripts>
    </asp:ScriptManagerProxy>


    <div id="divShd" class="opaqueLayer">
    </div>

    <div class="container content_area">

        <script src="/Scripts/jquery.rating.js" type="text/javascript"></script>
        <script type="text/javascript">
            $(".ratingDisabled").rating({ showCancel: false, disabled: true });
        </script>
        <div class="left_right_padding">
            <uc1:BreadCrumbMenu ID="BreadCumbMenuCategory" runat="server" />

            <div class=" rounded_corners PDContainer">
                <div id="SliderContainer" runat="server" class="row productdetailSliderContainer rounded_corners bkWhite">
                 
                    <div class=" col-md-3 col-lg-3 col-xs-12 col-sm-4 productdetailBannerleftPanel">
                        <div class="productDetailPageHeading">
                            <asp:Label ID="lblProducName" runat="server"></asp:Label>
                        </div>
                        <div class="divBottomBtns">

                            <div id="divFaourites" class=" " runat="server">
                                <asp:Button ID="btnEditThisDesign" Text="Design Now" runat="server" ClientIDMode="Predictable"
                                    CssClass="start_creating_btn_prodDetail_orng rounded_corners5 marginBtm10 padding0imp"
                                    OnClientClick="return ShowMessage(); " OnClick="btnEditThisDesign_Click" />
                                <asp:Label runat="server" ID="lblTemplateCount" CssClass="lblTemplateCount"></asp:Label>
                                <div class="clear">
                                </div>
                            </div>
                            <asp:Button ID="btnStartCreating" Text="Other designs" runat="server" CausesValidation="false"
                                CssClass="start_creating_btn_prodDetail_blk rounded_corners5 marginBtm10" OnClick="btnStartCreating_Click"
                                OnClientClick="showProgress();" />

                        </div>
                        <div id="finishedGoodSpecs" runat="server" class="box_specFinisedGood">
                            <div class="mrginBtm">
                                <asp:Label ID="Label1" CssClass="marginall5 ProductDetailH2" runat="server"
                                    Text="Specifications"></asp:Label>
                            </div>
                            <asp:Label ID="lblCategoryCode2" CssClass="marginall5" runat="server" Text=""></asp:Label>
                            <br />
                            <p class="marginall5">
                                <asp:Literal ID="ltrlCatDesc2" runat="server" Text=""></asp:Literal>
                            </p>
                            <br />
                            <asp:Label ID="lblRefiningOpt2" CssClass="marginall5 ProductDetailH2" runat="server"
                                Text="Refining Options:">
                            </asp:Label>
                            <div class="marginall5 ProductDetailRefOptions">
                                <asp:BulletedList ID="BulletedListExtras2" runat="server" CssClass="ProductDetailRefOptionsUL">
                                </asp:BulletedList>
                            </div>
                        </div>
                        <asp:Button ID="btnUploadDesign" runat="server" CausesValidation="false" Text="Upload files"
                            CssClass="btn_upload_design_Prod_details  rounded_corners5 padding0imp" OnClientClick="showProgress(); " />
                         <div id="ContainerInStock" runat="server" class="InStockContainer">
                           <asp:Label ID="InStockValue" runat="server"></asp:Label>
                           </div>
                    </div>
                    <div id="bannnerContainer" runat="server" style="display: none;">
                        <asp:Image ID="templBannerImg" runat="server" Style="width: 670px; height: 420px;" />
                    </div>
                    <div class=" col-md-7 col-lg-6 col-xs-12 col-sm-4 product_detail_Img_div" runat="server">
                   
                        <div id="divImgTabs" class="height490" runat="server" clientidmode="Static">
                        </div>
                        <div class="clearBoth">
                        </div>
                    </div>
                    <div id="ProductrightContainter" runat="server" class="col-md-2 col-lg-3 col-xs-12 col-sm-4 deatilTemplateInfocnt">
                        <asp:Label ID="txtempltNameHEading" runat="server" Text="Template name"></asp:Label><br />
                        <asp:Label ID="lblTemplateName" runat="server" CssClass="ProductDetailH1" /><br />
                        <br />
                        <select class="ratingDisabled input keepwhitespace" id="ratingControlUser" runat="server">
                            <option value="0" selected="selected">Template Rating </option>
                            <option value="1">Template Rating </option>
                            <option value="2">Template Rating</option>
                            <option value="3">Template Rating</option>
                            <option value="4">Template Rating</option>
                        </select>
                       

                        <div id="divFavoriteInd" runat="server" class="passive_star margin0-sm margin0Imp" title="Click to Add as Favorite">
                            &nbsp;
                        </div>
                        <p class="spanMarkCount spanFavouriteItem margin0-sm" id="spanFavouriteItem" runat="server">
                            Mark as favorite
                        </p>
                        <div class="font11px" runat="server" id="divLetUsDes" style="margin-top: 30px;">
                            <br />
                            <asp:Label ID="Label5" runat="server" Text="Lets us design it for you" CssClass="product_detail_sub_heading colorBlack"></asp:Label><br />
                            <asp:Literal ID="Literal8" runat="server" Text="Seen a design concept you like but want to discuss with us first? Give us a call."></asp:Literal>
                            <br />
                            <br />
                        </div>
                    </div>
                    <div class="clearBoth">
                    </div>
                    <div style="float: right; margin-right: 20px;">
                        <div id="cntgpShareLink" runat="server" style="float: right !important;" class="ShareGoogPluss" onclick="ShareOnGooglePl();">
                        </div>
                        <div id="cntLinkinShareBtn" runat="server" onclick="ShareOnLinkIn();" class="ShareLinkInBtn" style="float: right;">
                        </div>
                        <div id="cnttwitterSharelnk" runat="server" style="float: right; margin-top: 14px;">
                            <em id="emSeTweet" runat="server" class="emShareTweet" onclick="OpenNewWindowToSahre();"></em>
                            <a class="TweetsShareBtn" onclick="OpenNewWindowToSahre();" data-related="jasoncosta" data-lang="en" data-size="large" data-count="none"></a>



                        </div>
                        <div id="cntfacebookSharelnk" runat="server" style="float: right;" class="ShareFbBtn" onclick="OpenNewWindowToSahreWithFaceBook();">
                        </div>
                        <!-- Place this tag after the last share tag. -->
                        <script type="text/javascript">
                            (function () {
                                var po = document.createElement('script'); po.type = 'text/javascript'; po.async = true;
                                po.src = 'https://apis.google.com/js/platform.js';
                                var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(po, s);
                            })();
                        </script>
                        <div>
                            <script src="//platform.linkedin.com/in.js" type="text/javascript">
            lang: en_US
                            </script>
                        </div>

                    </div>
                    <div class="clearBoth">
                        &nbsp;
                    </div>
                </div>
            </div>
            <div class="clearBoth" style="margin-bottom: 10px;">
                &nbsp;
            </div>
            <div class="row">
                <div class="product_detail_main_heading paddingBottom10">
                    <asp:Label ID="lblCategoryMainHeading" runat="server" />
                </div>
                <div class=" rounded_corners Background-sm  col-md-4 col-lg-4 col-xs-12 ProductDetailGrayBoxLeft">
                  
                    <div id="templateGoodSpecs" runat="server" class="Background-lg">
                       
                        <div class="box_catCode">
                            <div class="mrginBtm">
                                <asp:Label ID="lblSpecHead" CssClass="marginall5 ProductDetailH2" runat="server"
                                    Text="Specifications"></asp:Label>
                            </div>
                            <asp:Label ID="lblCategoryCode" CssClass="marginall5" runat="server" Text=""></asp:Label>
                            <br />
                            <p class="marginall5" style="width: 285px;">
                                <asp:Literal ID="lblCatDes" runat="server" Text=""></asp:Literal>
                            </p>
                            <br />
                            <asp:Label ID="lblRefinngOpt" CssClass="marginall5 ProductDetailH2" runat="server"
                                Text="Refinning Options:">
                            </asp:Label>
                            <div class="marginall5 ProductDetailRefOptions">
                                <asp:BulletedList ID="BulletedListExtras" runat="server" CssClass="ProductDetailRefOptionsUL">
                                </asp:BulletedList>
                            </div>
                        </div>
                        <div class="box_prodPrice">
                            <div id="PriceCircle" class="blue_cicle_container" runat="server">
                                <div class="productNewClasscircle">
                                    <div class="all_padding3">
                                        <div class="paddingTop2px">
                                            &nbsp;
                                        </div>
                                        <asp:Literal ID="ltrlfrom" runat="server" Text="FROM"></asp:Literal>
                                        <br />
                                        <asp:Label runat="server" ID="lblFromMinPrice" Text='<%# Eval("MinPrice") %>' Font-Bold="true"
                                            Font-Size="16px" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class=" clear">
                        </div>
                    </div>
                    <div id="handyInfoFinisedGood" runat="server" class="white_background rounded_corners" style="padding: 10px;">
                        <asp:Label ID="btnIdeas2" runat="server" Text="Specification" CssClass="ProductDetailH2"></asp:Label>
                        <div class="rounded_corners paddingLeft10px paddingBottom10px handyInfocnt">
                            <br />
                            <asp:Label ID="lblHintsandTips2" runat="server" Text="sdre" CssClass="show_as_it_is" />
                        </div>
                    </div>

                </div>
                <input type="hidden" id="txtIsQuantityRanged" runat="server" />
                <div class="col-md-8 col-lg-8 col-xs-12 ProductDetailGrayBoxRight" style="padding: 0px !important;">
                    
                    <div style="margin-bottom: 10px;"
                        class="float_left_simple rounded_corners cntwidthProductDetailOnlinePrice cntBackgorndPD cntProductDetailWidth">
                  
                        <div class="prodWhiteBack">
                            <div id="PricingTblDiv" runat="server">
                                <table>
                                    <tr>
                                        <td valign="top" class="width45p">
                                            <div class="mrginBtm" style="text-align:left;">
                                                <asp:Label ID="lblProdSelectOption" CssClass="ProductDetailH2" runat="server" Text="Online Prices" />
                                            </div>
                                            <table class="cntwidthProductDetailOnlinePrice" cellpadding="5" cellspacing="0">
                                                <asp:Repeater ID="rptPriceMatrix" runat="server" OnItemDataBound="rptPriceMatrix_ItemDataBound">
                                                    <HeaderTemplate>
                                                        <tr>
                                                            <td class="product_detail_HeaderCell procu_detail_grid_cell wdthWrp" id="divQuantity"
                                                                runat="server">Quantity
                                                            </td>
                                                            <td id="tdHeadText1" runat="server" class="product_detail_HeaderCell procu_detail_grid_cell wdthWrp"
                                                                style="text-align: right;">
                                                                <asp:Label ID="lblHeaderText1" runat="server" Text="" />
                                                            </td>
                                                            <td id="tdHeadText2" runat="server" class="product_detail_HeaderCell procu_detail_grid_cell wdthWrp"
                                                                style="text-align: right;">
                                                                <asp:Label ID="lblHeaderText2" runat="server" Text="" />
                                                            </td>
                                                            <td id="tdHeadText3" runat="server" class="product_detail_HeaderCell procu_detail_grid_cell wdthWrp"
                                                                style="text-align: right;">
                                                                <asp:Label ID="lblHeaderText3" runat="server" Text="" />
                                                            </td>
                                                            <td id="tdHeadText4" runat="server" class="product_detail_HeaderCell procu_detail_grid_cell wdthWrp"
                                                                style="text-align: right;">
                                                                <asp:Label ID="lblHeaderText4" runat="server" Text="" />
                                                            </td>
                                                            <td id="tdHeadText5" runat="server" class="product_detail_HeaderCell procu_detail_grid_cell wdthWrp"
                                                                style="text-align: right;">
                                                                <asp:Label ID="lblHeaderText5" runat="server" Text="" />
                                                            </td>
                                                            <td id="tdHeadText6" runat="server" class="product_detail_HeaderCell procu_detail_grid_cell wdthWrp"
                                                                style="text-align: right;">
                                                                <asp:Label ID="lblHeaderText6" runat="server" Text="" />
                                                            </td>
                                                            <td id="tdHeadText7" runat="server" class="product_detail_HeaderCell procu_detail_grid_cell wdthWrp"
                                                                style="text-align: right;">
                                                                <asp:Label ID="lblHeaderText7" runat="server" Text="" />
                                                            </td>
                                                            <td id="tdHeadText8" runat="server" class="product_detail_HeaderCell procu_detail_grid_cell wdthWrp"
                                                                style="text-align: right;">
                                                                <asp:Label ID="lblHeaderText8" runat="server" Text="" />
                                                            </td>
                                                            <td id="tdHeadText9" runat="server" class="product_detail_HeaderCell procu_detail_grid_cell wdthWrp"
                                                                style="text-align: right;">
                                                                <asp:Label ID="lblHeaderText9" runat="server" Text="" />
                                                            </td>
                                                            <td id="tdHeadText10" runat="server" class="product_detail_HeaderCell procu_detail_grid_cell wdthWrp"
                                                                style="text-align: right;">
                                                                <asp:Label ID="lblHeaderText10" runat="server" Text="" />
                                                            </td>
                                                            <td id="tdHeadText11" runat="server" class="product_detail_HeaderCell procu_detail_grid_cell wdthWrp"
                                                                style="text-align: right;">
                                                                <asp:Label ID="lblHeaderText11" runat="server" Text="" />
                                                            </td>
                                                        </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="product_detail_item_cell procu_detail_grid_cell">
                                                                <input type="hidden" id="txtHiddenPriceMatrixID" value='<%# Eval("PriceMatrixID") %>'
                                                                    runat="server" />
                                                                <input type="hidden" id="txtHiddenIsDiscounted" value='<%# Eval("IsDiscounted") %>'
                                                                    runat="server" />
                                                                <span runat="server" id="spanFixedQuantity">
                                                                    <%# Eval("Quantity") %></span> <span runat="server" id="spanRangedQuantity">
                                                                        <%# Eval("QtyRangeFrom")%>
                                                                        -
                                                                        <%# Eval("QtyRangeTo")%></span>
                                                            </td>
                                                            <td id="td1" runat="server" class="product_detail_item_cell procu_detail_grid_cell"
                                                                style="text-align: right;">
                                                                <div id="matrixItemColumn1" runat="server">
                                                                    <span id='Matt_PriceMatrixID_<%# Eval("PriceMatrixID")%>' title='<%# Eval("PricePaperType1")%>'>
                                                                        <asp:Label ID="lblMatt" runat="server" />
                                                                        <asp:Label ID="lblMattDiscountedPrice" CssClass="custom_colorTS" Visible="false"
                                                                            runat="server" Text='<%# Eval("PricePaperType1")%>' />
                                                                    </span>
                                                                </div>
                                                            </td>
                                                            <td id="td2" runat="server" class="product_detail_item_cell procu_detail_grid_cell"
                                                                style="text-align: right;">
                                                                <div id="matrixItemColumn2" runat="server">
                                                                    <span id='Glossy_PriceMatrixID_<%# Eval("PriceMatrixID")%>' title='<%# Eval("PricePaperType2")%>'>
                                                                        <asp:Label ID="lblGlossy" runat="server" />
                                                                        <asp:Label ID="lblGlossyDiscountedPrice" CssClass="custom_colorTS" Visible="false"
                                                                            runat="server" Text='<%# Eval("PricePaperType2")%>' />
                                                                    </span>
                                                                </div>
                                                            </td>
                                                            <td id="td3" runat="server" class="product_detail_item_cell procu_detail_grid_cell"
                                                                style="text-align: right;">
                                                                <div id="matrixItemColumn3" runat="server">
                                                                    <span id='PremiumMatt_PriceMatrixID_<%# Eval("PriceMatrixID")%>' title='<%# Eval("PricePaperType3")%>'>
                                                                        <asp:Label ID="lblPremiumMatt" runat="server" />
                                                                        <asp:Label ID="lblPremiumMattDiscountedPrice" CssClass="custom_colorTS" Visible="false"
                                                                            runat="server" Text='<%# Eval("PricePaperType3")%>' />
                                                                    </span>
                                                                </div>
                                                            </td>
                                                            <td id="td4" runat="server" class="product_detail_item_cell procu_detail_grid_cell"
                                                                style="text-align: right;">
                                                                <div id="matrixItemColumn4" runat="server" class="Stock4PriceMatrixColumn tblBorderTdWidth extra_pricing divtxtflt">
                                                                    <span id='4_PriceMatrixID_<%# Eval("PriceMatrixID")%>' title='<%# Eval("PriceStockType4")%>'>
                                                                        <asp:Label ID="lblForthStock" runat="server" />
                                                                        <asp:Label ID="lblForthStockDiscountedPrice" CssClass="custom_colorTS" Visible="false"
                                                                            runat="server" Text='<%# Eval("PriceStockType4")%>' />
                                                                    </span>
                                                                </div>
                                                            </td>
                                                            <td id="td5" runat="server" class="product_detail_item_cell procu_detail_grid_cell"
                                                                style="text-align: right;">
                                                                <div id="matrixItemColumn5" runat="server" class="Stock5PriceMatrixColumn tblBorderTdWidth extra_pricing divtxtflt">
                                                                    <span id='5_PriceMatrixID_<%# Eval("PriceMatrixID")%>' title='<%# Eval("PriceStockType5")%>'>
                                                                        <asp:Label ID="lblFifthStock" runat="server" />
                                                                        <asp:Label ID="lblFifthStockDiscountedPrice" CssClass="custom_colorTS" Visible="false"
                                                                            runat="server" Text='<%# Eval("PriceStockType5")%>' />
                                                                    </span>
                                                                </div>
                                                            </td>
                                                            <td id="td6" runat="server" class="product_detail_item_cell procu_detail_grid_cell"
                                                                style="text-align: right;">
                                                                <div id="matrixItemColumn6" runat="server" class="Stock6PriceMatrixColumn tblBorderTdWidth extra_pricing divtxtflt">
                                                                    <span id='6_PriceMatrixID_<%# Eval("PriceMatrixID")%>' title='<%# Eval("PriceStockType6")%>'>
                                                                        <asp:Label ID="lblSixthStock" runat="server" />
                                                                        <asp:Label ID="lblSixthStockDiscountedPrice" CssClass="custom_colorTS" Visible="false"
                                                                            runat="server" Text='<%# Eval("PriceStockType6")%>' />
                                                                    </span>
                                                                </div>
                                                            </td>
                                                            <td id="td7" runat="server" class="product_detail_item_cell procu_detail_grid_cell"
                                                                style="text-align: right;">
                                                                <div id="matrixItemColumn7" runat="server" class="Stock7PriceMatrixColumn tblBorderTdWidth extra_pricing divtxtflt">
                                                                    <span id='7_PriceMatrixID_<%# Eval("PriceMatrixID")%>' title='<%# Eval("PriceStockType7")%>'>
                                                                        <asp:Label ID="lblSevenStock" runat="server" />
                                                                        <asp:Label ID="lblSevenStockDiscountedPrice" CssClass="custom_colorTS" Visible="false"
                                                                            runat="server" Text='<%# Eval("PriceStockType7")%>' />
                                                                    </span>
                                                                </div>
                                                            </td>
                                                            <td id="td8" runat="server" class="product_detail_item_cell procu_detail_grid_cell"
                                                                style="text-align: right;">
                                                                <div id="matrixItemColumn8" runat="server" class="Stock8PriceMatrixColumn tblBorderTdWidth extra_pricing divtxtflt">
                                                                    <span id='8_PriceMatrixID_<%# Eval("PriceMatrixID")%>' title='<%# Eval("PriceStockType8")%>'>
                                                                        <asp:Label ID="lblEightStock" runat="server" />
                                                                        <asp:Label ID="lblEightStockDiscountedPrice" CssClass="custom_colorTS" Visible="false"
                                                                            runat="server" Text='<%# Eval("PriceStockType8")%>' />
                                                                    </span>
                                                                </div>
                                                            </td>
                                                            <td id="td9" runat="server" class="product_detail_item_cell procu_detail_grid_cell"
                                                                style="text-align: right;">
                                                                <div id="matrixItemColumn9" runat="server" class="Stock9PriceMatrixColumn tblBorderTdWidth extra_pricing divtxtflt">
                                                                    <span id='9_PriceMatrixID_<%# Eval("PriceMatrixID")%>' title='<%# Eval("PriceStockType9")%>'>
                                                                        <asp:Label ID="lblNinthStock" runat="server" />
                                                                        <asp:Label ID="lblNinthStockDiscountedPrice" CssClass="custom_colorTS" Visible="false"
                                                                            runat="server" Text='<%# Eval("PriceStockType9")%>' />
                                                                    </span>
                                                                </div>
                                                            </td>
                                                            <td id="td10" runat="server" class="product_detail_item_cell procu_detail_grid_cell"
                                                                style="text-align: right;">
                                                                <div id="matrixItemColumn10" runat="server" class="Stock10PriceMatrixColumn tblBorderTdWidth extra_pricing divtxtflt">
                                                                    <span id='10_PriceMatrixID_<%# Eval("PriceMatrixID")%>' title='<%# Eval("PriceStockType10")%>'>
                                                                        <asp:Label ID="lblTenthStock" runat="server" />
                                                                        <asp:Label ID="lblTenthStockDiscountedPrice" CssClass="custom_colorTS" Visible="false"
                                                                            runat="server" Text='<%# Eval("PriceStockType10")%>' />
                                                                    </span>
                                                                </div>
                                                            </td>
                                                            <td id="td11" runat="server" class="product_detail_item_cell procu_detail_grid_cell"
                                                                style="text-align: right;">
                                                                <div id="matrixItemColumn11" runat="server" class="Stock11PriceMatrixColumn tblBorderTdWidth extra_pricing divtxtflt">
                                                                    <span id='11_PriceMatrixID_<%# Eval("PriceMatrixID")%>' title='<%# Eval("PriceStockType11")%>'>
                                                                        <asp:Label ID="lblElevenStock" runat="server" />
                                                                        <asp:Label ID="lblElevenStockDiscountedPrice" CssClass="custom_colorTS" Visible="false"
                                                                            runat="server" Text='<%# Eval("PriceStockType11")%>' />
                                                                    </span>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </table>
                                            <div class="product_detail_note">
                                                <asp:Label ID="lblTaxLabel" runat="server" CssClass="lblTxtProdDet"></asp:Label>
                                            </div>
                                            <div class="clearBoth">
                                                &nbsp;
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div id="SpecCont" runat="server" style="margin-bottom: 10px;"
                        class="cntProductDetailWidth cntwidthProductDetailDesc float_left_simple rounded_corners cntBackgorndPD">
             
                        <div class="whitebackground">
                         
                            <asp:Label ID="btnProductDetails" runat="server" Text="Specification" CssClass="ProductDetailH2"></asp:Label>
                            <div class="displayBlock rounded_corners paddingLeft10px paddingBottom10px productHandyDesc" >
                                <br />
                                <asp:Label ID="lblProductSpecifications" runat="server" Text="" CssClass="show_as_it_is" />
                                <br />
                            </div>
                        </div>
                    </div>
                    <div id="HintTipsCont" runat="server" style="margin-bottom: 10px;"
                        class="cntProductDetailWidth float_left_simple rounded_corners cntwidthProductDetailDesc cntBackgorndPD">
                     
                        <div class="whitebackground">
                           
                            <asp:Label ID="btnIdeas" runat="server" Text="Specification" CssClass="ProductDetailH2"></asp:Label>
                            <div class="rounded_corners paddingLeft10px paddingBottom10px productHandyDesc">
                                <br />
                                <asp:Label ID="lblHintsandTips" runat="server" Text="sdre" CssClass="show_as_it_is" />
                            </div>
                        </div>
                    </div>
                    <div id="LyoutGridCont" runat="server" 
                        class="cntProductDetailWidth handyInfocnt float_left_simple cntwidthProductDetailDesc rounded_corners">
                      
                        <div class="whitebackground">
                         
                            <asp:Label ID="btnLayoutGrid" runat="server" Text="Specification" CssClass="ProductDetailH2"></asp:Label>
                            <div class="rounded_corners paddingLeft10px paddingBottom10px productHandyDesc" >
                                <br />
                                <asp:Label ID="lblDescriptionOfLayout" runat="server" CssClass="show_as_it_is"></asp:Label>
                                <br />
                                <br />
                                <div class="float_left_simple">
                                    <asp:Image ID="GridImg" runat="server" CssClass="ImageHW" Visible="false" />
                                </div>
                                <div class="float_left_simple MLftTop">
                                    <asp:HyperLink ID="ImgIconPs" runat="server" Visible="false" Target="_blank"></asp:HyperLink>
                                    <asp:HyperLink ID="ImgIconAi" runat="server" Visible="false" Target="_blank"></asp:HyperLink>
                                    <asp:HyperLink ID="ImgIconId" runat="server" Visible="false" Target="_blank"></asp:HyperLink>
                                    <asp:HyperLink ID="ImgIconPdf" runat="server" Visible="false" Target="_blank"></asp:HyperLink>
                                </div>
                                <div class="clearBoth">
                                    &nbsp;
                                </div>
                            </div>
                        </div>
                    </div>
            
                </div>
                <div class="clearBoth">
                    &nbsp;
                </div>
                <uc4:MatchingSet ID="MatchingSet1" runat="server" Visible="false" />
                <uc3:RelatedItems ID="itemsRelatedItemsWidget" runat="server" />
            </div>


            <div class="clearBoth">
                &nbsp;
            </div>


            <br />



        </div>
        <div class="clearBoth">
            &nbsp;
        </div>
        <div class="rounded_corners">
            <div class="product_detail_sup_padding">
                <div id="tabs" class="float_left_simple width93per" style="text-align: left !important;">
                    <ul class="MLeft40 MBtm marginbtm5Imp" style="display: none;">
                        <%-- <li id="btnProductDetails" runat="server" clientidmode="Predictable" class="width84per ui-state-active ui-corner-top PaddingtopCentreAlign "
                            onclick="tabify('tabs1','MainContent_btnProductDetails')">Product Details</li>--%>
                        <%-- <li id="btnIdeas" runat="server" clientidmode="Predictable" class="width84per ui-state-default ui-corner-top PaddingtopCentreAlign"
                            onclick="tabify('tabs2','MainContent_btnIdeas')">Ideas</li>--%>
                        <%-- <li runat="server" id="btnLayoutGrid" clientidmode="Predictable" class="width84per ui-state-default ui-corner-top PaddingtopCentreAlign"
                            onclick="tabify('tabs3','MainContent_btnLayoutGrid')">Layout Grid</li>--%>
                        <li runat="server" id="btnHowToVideo" clientidmode="Predictable" class="width84per ui-state-default ui-corner-top PaddingtopCentreAlign"
                            onclick="tabify('tabs4','MainContent_btnHowToVideo')">Video </li>
                        <li runat="server" id="btnMatchingSets" clientidmode="Predictable" class="width84per ui-state-default ui-corner-top "
                            onclick="tabify('tabs5','MainContent_btnMatchingSets')">Matching Sets </li>
                    </ul>
                    <%--<div id="tabs1" runat="server" clientidmode="Static" class="displayBlock rounded_corners paddingLeft10px paddingBottom10px"
                        style="background-color: rgb(243, 244, 244);">
                        <br />--%>
                    <%--<asp:Label ID="lblProductSpecifications" runat="server" Text="" CssClass="show_as_it_is" />--%>
                    <%--   <br />
                    </div>
                    <div id="tabs2" runat="server" clientidmode="Static" class="displayNone rounded_corners paddingLeft10px paddingBottom10px"
                        style="background-color: rgb(243, 244, 244);">
                        <br />--%>
                    <%--<asp:Label ID="lblHintsandTips" runat="server" Text="" CssClass="show_as_it_is" />--%>
                    <%--</div>--%>
                    <%--  <div id="tabs3" runat="server" clientidmode="Static" class="displayNone rounded_corners paddingLeft10px paddingBottom10px"
                        style="background-color: rgb(243, 244, 244);">
                        <br />
                        <asp:Label ID="lblDescriptionOfLayout" runat="server" CssClass="show_as_it_is"></asp:Label>
                        <br />
                        <br />
                        <div class="float_left_simple">
                            <asp:Image ID="GridImg" runat="server" CssClass="ImageHW" Visible="false" />
                        </div>
                        <div class="float_left_simple MLftTop">
                            <asp:HyperLink ID="ImgIconPs" runat="server" Visible="false" Target="_blank"></asp:HyperLink>
                            <asp:HyperLink ID="ImgIconAi" runat="server" Visible="false" Target="_blank"></asp:HyperLink>
                            <asp:HyperLink ID="ImgIconId" runat="server" Visible="false" Target="_blank"></asp:HyperLink>
                            <asp:HyperLink ID="ImgIconPdf" runat="server" Visible="false" Target="_blank"></asp:HyperLink>
                        </div>
                        <div class="clearBoth">
                            &nbsp;
                        </div>
                    </div>--%>
                    <%--  <div id="tabs4" runat="server" clientidmode="Static" class="displayNone rounded_corners paddingLeft10px paddingBottom10px"
                        style="background-color: rgb(243, 244, 244);">
                        <br />
                        <asp:Label ID="lblDescriptionOfVideo" runat="server" CssClass="width30p"></asp:Label>
                    </div>
                     <div id="tabs5" runat="server" clientidmode="Static" class="displayNone rounded_corners">
                            <div runat="server" id="ifrCon" style="background-color: rgb(243, 244, 244);">
                                <iframe id="ifrm" style="width: 93%; height: 335px; padding: 5px; padding-bottom: 5px;
                                    padding-top: 5px; border: none;" class="rounded_corners LCLB" src="empty.htm">
                                </iframe>
                                <br />
                                <br />
                            </div>
                        </div>--%>
                </div>
            </div>
            <div class="clearBoth">
                &nbsp;
            </div>
        </div>
        <div class="clearBoth">
            &nbsp;
        </div>
        <br />
       

        <div class="clearBoth">
            &nbsp;
        </div>
        <br />
        <%--<asp:Label ID="lblPageContents" runat="server" />--%>
        <asp:Label ID="lblMsgDisp" runat="server" Visible="false" />
        <%--Hidden Fielsds--%>
        <asp:HiddenField ID="txtHiddenCorpItemTemplateID" Value='0' runat="server" />
        <asp:HiddenField ID="txtHiddenProductName" Value='' runat="server" />
        <asp:HiddenField ID="txtHiddenItemDiscountRate" runat="server" />
        <asp:HiddenField ID="txtHiddenProductCategoryID" runat="server" />
        <asp:HiddenField ID="txtHiddenProductItemID" runat="server" />
        <asp:HiddenField ID="txtHiddenProdItemPrice" Value="0" runat="server" />
        <asp:HiddenField ID="txtHiddenAddonTotal" Value="0" runat="server" />
        <asp:HiddenField ID="txtNoOfPages" Value='' runat="server" />
        <asp:HiddenField ID="txtTemplateID" Value='' runat="server" />
        <asp:HiddenField ID="hfContactId" runat="server" />
        <asp:HiddenField ID="hfItemId" runat="server" />
        <asp:HiddenField ID="hfCategoryId" runat="server" />
        <asp:HiddenField ID="hfTemplateName" runat="server" />
        <asp:HiddenField ID="hfEditTempType" runat="server" />
        <asp:HiddenField ID="hfState" runat="server" ClientIDMode="Predictable" />
        <asp:HiddenField ID="hfBrokerMarkup" runat="server" Value="0" />
        <asp:HiddenField ID="hfContactMarkup" runat="server" Value="0" />
        <asp:HiddenField ID="hfFbShareLink" runat="server" Value="0" />
    </div>
     <uc5:RRViews ID="pnlRViews" runat="server" />
    <br />
    <br />
    <script type="text/javascript">




        function displayFileUploadPopup(btnUploadFile) {

            var result = false;
            result = validateFileSelection();
            if (result == false)
                alert("Please select file");
            else {

                var overLay = $("#lblFileProgressOverlay");
                $(overLay).addClass("overlay");
                $("#lblFileLoader").css('visibility', "visible");
            }


            return result;

        }

        function validateFileSelection() {

            //debugger;
            var addedFileDivsList = $("div .MultiFile-label");

            if (addedFileDivsList.length > 0)
                return true;
            else
                return false;
        }
    </script>
    <script type="text/javascript" language="javascript">

        var productTotal = 0;

        var prouctTotalSavings = 0;
        var addonTotalSavings = 0;

        function checkUnCheckRadio(radionButton) {
            var radiosList = $(".radioBtnQtyGroupClass");

            if (radiosList != null) {

                $(radiosList).each(function (indx, objVal) {

                    //debugger;
                    if ($(this).attr("id") != $(radionButton).attr("id")) {
                        $(this).attr("checked", false);
                    }
                });
            }

            //Finally make selecten changes
            makePaperSelectection();
        } //checkUnCheckRadio



        /*Adon Pricess methods*/
        function calculateAdOnPrices() {

            //Set the AddonTotals on Global Variable
            this.displayTotalPrice(productTotal, goThroughAdOnPricesChkBoxesForTotal());
        }

        function goThroughAdOnPricesChkBoxesForTotal() {


            var checkedPriceTotal = 0;
            var addonSaving = 0;
            var checkBoxJasonData = null;

            $(".checkBoxAdOnPriceCheckBox").each(function (i, val) {

                if ($(this).attr("checked") == "checked") {
                    //Gets the Jason Data associated with it
                    checkBoxJasonData = jQuery.parseJSON($(this).attr("CheckBoxJasonData"));

                    if (checkBoxJasonData != null) {

                        if (checkBoxJasonData.IsDiscounted == false) {

                            checkedPriceTotal = checkedPriceTotal + parseFloat(checkBoxJasonData.ActualPrice); //Gets Actual non discounted Price
                        }
                        else {
                            checkedPriceTotal = checkedPriceTotal + parseFloat(checkBoxJasonData.DiscountedPrice); //Gets Actual non discounted Price
                            addonSaving = addonSaving + (parseFloat(checkBoxJasonData.ActualPrice) - parseFloat(checkBoxJasonData.DiscountedPrice));
                        }
                    }
                }
                checkBoxJasonData = null;
            });

            addonTotalSavings = addonSaving; //global variable            
            return checkedPriceTotal;
        }

        /*End Adon Pricess methods*/


        function displayTotalPrice(productPrice, addOnPrice) {

            //debugger;
            var productPriceTotal = productPrice;
            var addonPriceTotal = addOnPrice;


            this.displayProductItemTotal(productPriceTotal.toFixed(2));
            this.displayAddOnTotal(addonPriceTotal.toFixed(2))

            var total = productPriceTotal + addonPriceTotal;
            $("#lblGrossTotal").text(getCurrencySymbol() + (total).toFixed(2));

            //Dispaly the savings as well
            $("#lblYouHaveSave").text('You have saved ' + getCurrencySymbol() + (prouctTotalSavings + addonTotalSavings).toFixed(2));
        }


        function displayProductItemTotal(price) {


        }

        function displayAddOnTotal(totalPrice) {



        }


        function makePaperSelectection() {

            var paperTypeRadioList = $(".radioBtnPaperTypeClass");

            if (paperTypeRadioList !== null && paperTypeRadioList.length > 0) {
                //get the checked paper radio
                //debugger;
                var selectedRadio = findSelectedPaperTypeRadio(paperTypeRadioList);

                if ($(selectedRadio).length > 0) {
                    if ($(selectedRadio).attr("id").search(/rbPaperPremiumMatt/) != -1) {
                        calculateProductItemPrice('PremiumMatt');
                    }
                    else if ($(selectedRadio).attr("id").search(/rbPaperMatt/) != -1) {
                        calculateProductItemPrice('Matt');
                    }
                    else if ($(selectedRadio).attr("id").search(/rbPaperGlossy/) != -1) {

                        calculateProductItemPrice('Glossy');
                    }
                }
            }
        } //makePaperSelectection



        function ShowMessage() {
            var type = $('#<%=hfEditTempType.ClientID %>').val();
            // alert(type);
            if (type == "NoTemplate") {
                showProgress();
                return true;
            } else if (type == "SameTemplate") {
                //alert("2");
                ShowPopup2("Alert!", "You already have the same template in designer mode Would you like to resume your design or start from scratch ?");
                return false;
            } else if (type == "SameItem") {
                // alert();
                ShowPopup2("Alert!", "you already have another template in designer mode. Would you like to continue modifiing the design or create new ");
                return false;
            }
            return false;
            //
        }
        function calculateProductItemPrice(selectedPaperType) {

            //debugger;
            var qtyJsonData = null;
            var radioQty = this.findSelectedQtyRadio();
            var selectedPrice = null;
            var savings = 0;

            if ($(radioQty).length > 0) {
                qtyJsonData = jQuery.parseJSON($(radioQty).attr("QtyJason"));

                selectedPaperType = selectedPaperType + '_PriceMatrixID_' + qtyJsonData.PriceMatrixID.toString();

                if (qtyJsonData.IsDiscounted == false)
                    selectedPrice = parseFloat($('#' + selectedPaperType).attr("title")); // pick non discounted price
                else {

                    var actualPrice = parseFloat($('#' + selectedPaperType).attr("title")); // pick non discounted price
                    //get the discounted Price for this search the span having disconted Price
                    selectedPrice = triverseForDiscountedPrice(selectedPaperType);
                    //save ammount
                    savings = actualPrice - selectedPrice;
                }

                //alert(savings);
                //debugger;
                productTotal = selectedPrice; //global variables
                prouctTotalSavings = savings; //global variables
                this.displayTotalPrice(productTotal, goThroughAdOnPricesChkBoxesForTotal());
            }
        } //calculateProductItemPrice


        function triverseForDiscountedPrice(selectedPaperType) {

            //debugger;
            //var spanDiscounted = $($('#' + selectedPaperType).children()[1]).text().toString().replace(/^[$]/, '');
            var currSymbol = getCurrencySymbol();
            var spanDiscounted = $($('#' + selectedPaperType).children()[1]).text().toString().replace(/^[currSymbol]/, '');

            var arr = spanDiscounted.split(currSymbol);

            return parseFloat(arr[1]); //one will hve the numeric value

        } //triverseForDiscountedPrice

        function findSelectedPaperTypeRadio(paperTypeRadioList) {

            var selectedPaperRadio = null;

            $(paperTypeRadioList).each(function (indx, obj) {
                if ($(this).attr("checked") == "checked" && selectedPaperRadio == null) {

                    selectedPaperRadio = $(this);
                }
            });

            return selectedPaperRadio;

        } //findSelectedPaperTypeRadio

        function LoginPopUp() {
            var CheckLogin = '<%=IsLogin %>';
            if (parseInt(CheckLogin) > 0) {
                return true;
            }
            else {
                var returnUrl = '<%=ReturnUrl %>';
                var message = '<div style="text-align:left;padding-left:90px;">Add to Favorites to mark this template as a favorites in your profile, please register or login first</div><br><a href="../Signup.aspx?' + returnUrl + '" style="padding-right:20px;">Register</a><a href="../Login.aspx?' + returnUrl + '">Login</a>';

                ShowPopup('Message', message);
                return false;
            }
        }
        function findSelectedQtyRadio() {

            var radiosList = $(".radioBtnQtyGroupClass");
            var selectedQty = null;

            if (radiosList != null) {
                $(radiosList).each(function (indx, objVal) {
                    if ($(this).attr("checked") == "checked" && selectedQty == null) {

                        selectedQty = $(this);
                    }
                });
            }

            return selectedQty;
        }

    </script>
    <script type="text/javascript">

        $(document).ready(function () {

            CreateMultipleUpload();
            makePaperSelectection();
            calculateAdOnPrices();
        });

        function clearMsgLabel() {


        }

        function CreateMultipleUpload() {
            //set up the file upload

        }

        function FileUploaderShow() {

            $find('mpeUploadYourDesignPopup').show();
        }

        function FileUploaderHide() {

            $find('mpeUploadYourDesignPopup').hide();
            $('input:file').MultiFile('reset');

            clearMsgLabel();
        }




    </script>

    <script>!function (d, s, id) { var js, fjs = d.getElementsByTagName(s)[0]; if (!d.getElementById(id)) { js = d.createElement(s); js.id = id; js.src = "https://platform.twitter.com/widgets.js"; fjs.parentNode.insertBefore(js, fjs); } }(document, "script", "twitter-wjs");</script>

    <div id="fb-root"></div>
    <script>(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/en_US/sdk.js#xfbml=1&appId=1421343758131537&version=v2.0";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));</script>
    <script type="text/javascript">
        function OpenNewWindowToSahre() {
            var WindHref = window.location.href;
            while (WindHref.indexOf(";") > 0) {
                WindHref = WindHref.replace(";", "%3B");

            }

            var w = 620;
            var h = 360;
            var left = (screen.width / 2) - (w / 2);
            var top = (screen.height / 2) - (h / 2);
            window.open("https://twitter.com/intent/tweet?original_referer=" + WindHref + "&related=jasoncosta&text=-%20UK&tw_p=tweetbutton&url=" + WindHref, "_blank", "toolbar=yes, scrollbars=yes, resizable=yes, width=" + w + ", height=" + h + ", top=" + top + ", left=" + left);
            return false;
        }

        function OpenNewWindowToSahreWithFaceBook() {

            var WindHref = $("#<%=hfFbShareLink.ClientID%>").val();
            while (WindHref.indexOf(";") > 0) {
                WindHref = WindHref.replace(";", "%3B");

            }
            WindHref = WindHref.replace(":", "%3A");
            while (WindHref.indexOf("/") > 0) {
                WindHref = WindHref.replace("/", "%2F");

            }
            while (WindHref.indexOf("=") > 0) {
                WindHref = WindHref.replace("=", "%3D");

            }
            while (WindHref.indexOf("&") > 0) {
                WindHref = WindHref.replace("&", "%26");

            }
            WindHref = WindHref.replace("?", "%3F");
            //var htm = "<div class='clearfix UIShareStage_Preview'><div class='lfloat _ohe'><div class=UIShareStage_Image><img class='UIShareStage_Image img' src=;w=154&amp;h=154&amp;url=https%3A%2F%2Ffbstatic-a.akamaihd.net%2Frsrc.php%2Fv2%2Fy6%2Fr%2FYQEGe6GxI_M.png&amp;cfs=1&amp;upscale alt=></div></div><div class='UIShareStage_ShareContent _42ef'><div class=UIShareStage_Title><span dir=ltr>Social Plugins</span></div><div class=UIShareStage_Subtitle>developers.facebook.com</div><div class=UIShareStage_Summary><p class=UIShareStage_BottomMargin>Social plugins let you see what your friends have liked, commented on or shared on sites across the web.</p></div></div></div>";
            var w = 620;
            var h = 360;
            var left = (screen.width / 2) - (w / 2);
            var top = (screen.height / 2) - (h / 2);
            window.open("https://www.facebook.com/sharer.php?app_id=1421343758131537&sdk=joey&u=" + WindHref + "&display=popup", "_blank", "toolbar=yes, scrollbars=yes, resizable=yes, width=" + w + ", height=" + h + ", top=" + top + ", left=" + left);
            return false;
        }

        function ShareOnLinkIn() {

            var WindHref = $("#<%=hfFbShareLink.ClientID%>").val();
            while (WindHref.indexOf(";") > 0) {
                WindHref = WindHref.replace(";", "%3B");

            }
            WindHref = WindHref.replace(":", "%3A");
            while (WindHref.indexOf("/") > 0) {
                WindHref = WindHref.replace("/", "%2F");

            }
            while (WindHref.indexOf("=") > 0) {
                WindHref = WindHref.replace("=", "%3D");

            }
            while (WindHref.indexOf("&") > 0) {
                WindHref = WindHref.replace("&", "%26");

            }
            WindHref = WindHref.replace("?", "%3F");
            //var htm = "<div class='clearfix UIShareStage_Preview'><div class='lfloat _ohe'><div class=UIShareStage_Image><img class='UIShareStage_Image img' src=;w=154&amp;h=154&amp;url=https%3A%2F%2Ffbstatic-a.akamaihd.net%2Frsrc.php%2Fv2%2Fy6%2Fr%2FYQEGe6GxI_M.png&amp;cfs=1&amp;upscale alt=></div></div><div class='UIShareStage_ShareContent _42ef'><div class=UIShareStage_Title><span dir=ltr>Social Plugins</span></div><div class=UIShareStage_Subtitle>developers.facebook.com</div><div class=UIShareStage_Summary><p class=UIShareStage_BottomMargin>Social plugins let you see what your friends have liked, commented on or shared on sites across the web.</p></div></div></div>";
            var w = 620;
            var h = 360;
            var left = (screen.width / 2) - (w / 2);
            var top = (screen.height / 2) - (h / 2);
            window.open("http://www.linkedin.com/cws/share?url=" + WindHref + "&original_referer=" + WindHref + "&token=&isFramed=false&lang=en_US&_ts=1401271443305.631", "_blank", "toolbar=yes, scrollbars=yes, resizable=yes, width=" + w + ", height=" + h + ", top=" + top + ", left=" + left);
            return false;
        }


        function ShareOnGooglePl() {

            var WindHref = $("#<%=hfFbShareLink.ClientID%>").val();
            while (WindHref.indexOf(";") > 0) {
                WindHref = WindHref.replace(";", "%3B");

            }
            WindHref = WindHref.replace(":", "%3A");
            while (WindHref.indexOf("/") > 0) {
                WindHref = WindHref.replace("/", "%2F");

            }
            while (WindHref.indexOf("=") > 0) {
                WindHref = WindHref.replace("=", "%3D");

            }
            while (WindHref.indexOf("&") > 0) {
                WindHref = WindHref.replace("&", "%26");

            }
            WindHref = WindHref.replace("?", "%3F");
            //var htm = "<div class='clearfix UIShareStage_Preview'><div class='lfloat _ohe'><div class=UIShareStage_Image><img class='UIShareStage_Image img' src=;w=154&amp;h=154&amp;url=https%3A%2F%2Ffbstatic-a.akamaihd.net%2Frsrc.php%2Fv2%2Fy6%2Fr%2FYQEGe6GxI_M.png&amp;cfs=1&amp;upscale alt=></div></div><div class='UIShareStage_ShareContent _42ef'><div class=UIShareStage_Title><span dir=ltr>Social Plugins</span></div><div class=UIShareStage_Subtitle>developers.facebook.com</div><div class=UIShareStage_Summary><p class=UIShareStage_BottomMargin>Social plugins let you see what your friends have liked, commented on or shared on sites across the web.</p></div></div></div>";
            var w = 620;
            var h = 360;
            var left = (screen.width / 2) - (w / 2);
            var top = (screen.height / 2) - (h / 2);
            window.open("https://plus.google.com/share?url=" + WindHref, "_blank", "toolbar=yes, scrollbars=yes, resizable=yes, width=" + w + ", height=" + h + ", top=" + top + ", left=" + left);
            return false;
        }

    </script>


    <script type="text/javascript">
        //$(window).load(function () {
        //    // adjusting navigation icons 
        //    var hiddenFieldID1 = "input[id$=txtNoOfPages]";
        //    var pages = $(hiddenFieldID1).val()
        //    var width = pages * 22;
        //    width = width + "px";
        //    $(".navBulletsWrapper").css("width", width);

        //});



        $(document).ready(function () {

            var hiddenFieldID1 = "input[id$=txtTemplateID]";
            var templateId = $(hiddenFieldID1).val()
            var itemId = $('#<%=hfItemId.ClientID %>').val();
            var templatename = $('#<%=hfTemplateName.ClientID %>').val();
            var categoryId = $('#<%=hfCategoryId.ClientID %>').val();

            //                tabify('tabs1', 'MainContent_btnProductDetails')
            //  $("#ifrm").attr("src", 'matchingsetpopup.aspx?templatename=' + templatename + "&ProductCategoryId=" + categoryId + "&ItemID=" + itemId);
            var type = $('#<%=hfEditTempType.ClientID %>').val();
            if (type != "FinishedGood") {
                $('.passive_star').toggle(function () {
                    if (LoginPopUp()) {

                        $(this).removeClass('passive_star').addClass('active_star');
                        $(this).attr('title', 'Click to remove favorite');
                        $(".spanFavouriteItem").html("Unmark as favorite");
                        //var templateId = $(this).next().val();
                        SavedFarorite(templateId, true);
                        var FavcountValu = $('.FavCount').text();
                        var ReplceFav = FavcountValu.replace("(", " ");
                        var FinalFavCount = ReplceFav.replace(")", " ");
                        var Count = "(" + (parseInt(FinalFavCount, 10) + 1) + ")";
                        $('.FavCount').html(Count);
                    }
                }, function () {
                    if (LoginPopUp()) {
                        $(this).removeClass('active_star').addClass('passive_star');
                        $(this).attr('title', 'Click to add favorite');
                        //var templateId = $(this).next().val();
                        $(".spanFavouriteItem").html("Mark as favorite");
                        SavedFarorite(templateId, false);
                        var FavcountValu = $('.FavCount').text();
                        var ReplceFav = FavcountValu.replace("(", " ");
                        var FinalFavCount = ReplceFav.replace(")", " ");
                        var Count = "(" + (parseInt(FinalFavCount, 10) - 1) + ")";
                        $('.FavCount').html(Count);
                    }
                }
            );

                $('.active_star').toggle(function () {
                    if (LoginPopUp()) {
                        $(this).removeClass('active_star').addClass('passive_star');
                        $(this).attr('title', 'Click to add favorite');
                        //var templateId = $(this).next().val();
                        $(".spanFavouriteItem").html("Mark as favorite");
                        SavedFarorite(templateId, false);
                        var FavcountValu = $('.FavCount').text();
                        var ReplceFav = FavcountValu.replace("(", " ");
                        var FinalFavCount = ReplceFav.replace(")", " ");
                        var Count = "(" + (parseInt(FinalFavCount, 10) - 1) + ")";
                        $('.FavCount').html(Count);
                    }
                }, function () {
                    if (LoginPopUp()) {
                        $(this).removeClass('passive_star').addClass('active_star');
                        $(this).attr('title', 'Click to remove favorite');
                        $(".spanFavouriteItem").html("Unmark as favorite");
                        //var templateId = $(this).next().val();
                        SavedFarorite(templateId, true);
                        var FavcountValu = $('.FavCount').text();
                        var ReplceFav = FavcountValu.replace("(", " ");
                        var FinalFavCount = ReplceFav.replace(")", " ");
                        var Count = "(" + (parseInt(FinalFavCount, 10) + 1) + ")";
                        $('.FavCount').html(Count);
                    }
                }
                );
            }
            var type = $('#<%=hfEditTempType.ClientID %>').val();
            if (type != "FinishedGood") {
                $(".ratingDisabled").rating({ showCancel: false, disabled: true });
                //                tabify("tabs5", "MainContent_btnMatchingSets");
                $(".width93per").css("width", "98%");
            } else if (type == "FinishedGood") {
                $(".width93per").css("width", "98%");
            }

        });



        function SavedFarorite(templateId, isFavorite) {

            var contactId = $('#<%=hfContactId.ClientID %>').val();
            var itemId = $('#<%=hfItemId.ClientID %>').val();
            var categoryId = $('#<%=hfCategoryId.ClientID %>').val();

            //alert(templateId + "  " + itemId + " " + categoryId);
            if (parseInt(contactId) > 0) {

                Web2Print.UI.Services.WebStoreUtility.AddUpdateFavDesign(templateId, itemId, contactId, categoryId, isFavorite, OnSuccess);
            }
        }

        function OnSuccess(responce) {

        }





        //        function tabify(id, btnName) {
        //            //alert(btnName);
        //            $("#MainContent_btnProductDetails").removeClass("ui-state-active");
        //            $("#MainContent_btnIdeas").removeClass("ui-state-active");
        //            $("#MainContent_btnLayoutGrid").removeClass("ui-state-active");
        //            $("#MainContent_btnHowToVideo").removeClass("ui-state-active");
        //            $("#MainContent_btnMatchingSets").removeClass("ui-state-active");

        //            $("#MainContent_btnProductDetails").addClass("ui-state-default");
        //            $("#MainContent_btnIdeas").addClass("ui-state-default");
        //            $("#MainContent_btnLayoutGrid").addClass("ui-state-default");
        //            $("#MainContent_btnHowToVideo").addClass("ui-state-default");
        //            $("#MainContent_btnMatchingSets").addClass("ui-state-default");

        //            $("#tabs1").removeClass("displayBlock");
        //            $("#tabs2").removeClass("displayBlock");
        //            $("#tabs3").removeClass("displayBlock");
        //            $("#tabs4").removeClass("displayBlock");
        //            //   $("#tabs5").removeClass("displayBlock");

        //            $("#tabs1").addClass("displayNone");
        //            $("#tabs2").addClass("displayNone");
        //            $("#tabs3").addClass("displayNone");
        //            $("#tabs4").addClass("displayNone");
        //            //  $("#tabs5").addClass("displayNone");


        //            $("#" + btnName).removeClass("ui-state-default");
        //            $("#" + id).removeClass("displayNone");
        //            $("#" + id).addClass("displayBlock");
        //            $("#" + btnName).addClass("ui-state-active");

        //        }
    </script>
    </div>
</asp:Content>
