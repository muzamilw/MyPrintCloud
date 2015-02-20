<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ThemeSite.Master" AutoEventWireup="true"
    CodeBehind="ProductConfirmSelect.aspx.cs" Inherits="Web2Print.UI.ProductConfirmSelect" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Controls/BreadCrumbMenu.ascx" TagName="BreadCrumbMenu" TagPrefix="uc1" %>
<%@ Register Src="~/Controls/RaveReview.ascx" TagName="RRViews" TagPrefix="uc2" %>
<asp:Content ID="contentHead" ContentPlaceHolderID="HeadContents" runat="server">
    <link href="../LightBox/css/jquery.lightbox-0.5.css" rel="stylesheet" type="text/css" />
    <script src="LightBox/js/jquery.lightbox-0.5.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div id="divShd" class="opaqueLayer">
    </div>
    <div class="container content_area">

        <div class="row left_right_padding">
            <ul class="bonhamsOption navM cursor_pointer">
                <li class="bonhamSelectedOption">1. Select
                </li>
                <li>2. Edit
                </li>


                <li>3. Confirm order & payment
                </li>
                <li>4.  Order summary
                </li>

            </ul>
            <div class="col-md-12 col-lg-12 col-xs-12" style="padding-left: 0px;">
                <uc1:BreadCrumbMenu ID="BreadCumbMenuCategory" runat="server" />
            </div>
            <div class="clearBoth">
                &nbsp;
            </div>
            <div class="signin_heading_div float_left_simple">
                <asp:Label ID="lblProductName" runat="server" CssClass="product_detail_main_heading"></asp:Label>
            </div>
            <div class="clearBoth">
                &nbsp;
            </div>
            <div class="StocksDelivryContainer col-md-8 col-lg-8 col-xs-12 col-sm-12">
                <div id="divStockOptions" runat="server" class="float_left_simple LGBC rounded_corners width730" style="padding: 5px;">
                    <div id="ltrlSelectQty" runat="server" class="SelectQtyContainer textAlignLeft">
                    </div>
                    <div class="white_background rounded_corners divStk">
                        <div class="float_left_simple Width100Percent">
                            <div id="divStock" runat="server" class="Width220pxM250px heght150px spacer10pxtop">
                                <asp:Label ID="lblStockTxt" runat="server" Text="Stock" CssClass="DisplayNoneCSS6 ProdtPricedisplayNoneCs spacer10pxtop checked_design custom_color float_left_simple MTop12px ClearLeft clearRight"></asp:Label><br />
                            </div>
                            <div class="clearBoth">
                                &nbsp;
                            </div>
                            <div id="divAdditionalStockOptions" runat="server" class="spacer10pxtop">
                            </div>
                            <div class="clearBoth">
                                &nbsp;
                            </div>
                            <br />
                            <div class="marginLeft Fsize19 template_designing margnTop10 LandPage-Heading-Cs">
                                <asp:Label ID="lblStockHeaer" runat="server" Text="Refining Options"></asp:Label>
                            </div>
                            <div class="width365pxM250p">

                                <asp:Repeater ID="rptOptionsPricing" runat="server" OnItemDataBound="rptOptionsPricing_ItemDataBound">


                                    <ItemTemplate>
                                        <div class="float_left_simple spacerbottom" style="width: 120px;">
                                            <asp:Image ID="CCImg" runat="server" CssClass="RfgImgCs" />
                                        </div>
                                        <div class="extra_item float_left_simple">
                                            <asp:CheckBox ID="chkAddOnPrice" runat="server" CssClass="checkBoxAdOnPriceCheckBox backgroundIMg"
                                                onclick="calculateAdOnPrices();" />



                                            <asp:Label ID="lbldd" runat="server" AssociatedControlID="chkAddOnPrice" Text='<%# Eval("AddOnName")%>'
                                                CssClass="margin5 colorBlack"></asp:Label>

                                            <div class="extra_pricing float_right">
                                                <asp:Label ID="lblAddOnPrice" runat="server" />
                                                <asp:Label ID="lblAddOnPriceDiscountedPrice" CssClass="custom_color" Visible="false"
                                                    runat="server" Text='<%# Eval("AddOnPrice")%>' />
                                                <input type="hidden" id="txtHiddenProductAddOnID" value='<%# Eval("ProductAddOnID") %>'
                                                    runat="server" />
                                                <input type="hidden" id="txtHiddenIsDiscountedAddOn" value='<%# Eval("IsDiscounted") %>'
                                                    runat="server" />
                                                <input type="hidden" id="txtHiddenDiscountedRate" value='<%# Eval("DiscountRate") %>'
                                                    runat="server" />
                                                <input type="hidden" id="txtHiddenCostCentreID" value='<%# Eval("CostCenterID") %>'
                                                    runat="server" />
                                            </div>

                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("Webstoredesc")%>' CssClass="checkBoxAdOnDesc marginLeft" />
                                            <div class="clearBoth">
                                                &nbsp;
                                            </div>
                                        </div>
                                        <div class="clearBoth">
                                            &nbsp;
                                        </div>
                                        <div class="BorderLG_LP">
                                            &nbsp;
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            <div class="clearBoth">
                                &nbsp;
                            </div>
                        </div>
                        <div class="clearBoth">
                            &nbsp;
                        </div>
                    </div>

                </div>
                <div id="LftbtnContainer" runat="server" class="signin_heading_div float_left_simple UploadContainerCs">
                    <asp:Label ID="ltrlUpFlH" runat="server" Text="Upload artwork design" CssClass="textAlignLeft float_left_simple"></asp:Label>
                    <div id="FormateCont" runat="server" class="FormatContainer" visible="false">
                        <div id="PermFormat" runat="server" class="float_left_simple Fsize12 lbFormat">
                            Permitted Format
                        </div>
                        <div>
                            <img id="imgpdf" runat="server" alt="" src="~/images/pdfFormat.PNG" class="float_left_simple FormatedImgs" />
                            <%--<img id="imgpng" runat="server" alt="" src="~/images/pngFormat.PNG" class="float_left_simple FormatedImgs"
                                 />--%>
                            <img id="imgjpg" runat="server" alt="" src="~/images/jpgFormat.PNG" class="float_left_simple FormatedImgs" />
                            <%--<img id="imggif" runat="server" alt="" src="~/images/gifFormat.PNG" class="float_left_simple FormatedImgs"
                                />--%>
                        </div>
                    </div>
                    <div id="RptContainerDiv" class="float_left_simple LGBC rounded_corners paddingLeft5px H4B UplodArkworkContainer"
                        runat="server">
                        <div class="white_background rounded_corners padding10 SidesContainerCS" style="">
                            <div class="rptContainers">
                                <div onclick="return ShowUpload1();" id="ArtWorkUploadContainer" runat="server" class="artworkarrow">
                                    <img id="ImgArrow" runat="server" alt="" src="~/images/artwork_arrow_icons.png" style="width: 60px;" /><br />
                                    <asp:Label ID="lblSupplyArt" runat="server" Text="Supply your own artwork" Style="font-size: 16px; font-weight: bold;"></asp:Label><br />
                                    <asp:Label ID="lblFilesReady" runat="server" Text="Add multiple files" Style="font-size: 13px;"></asp:Label>
                                </div>
                                <div class="clearBoth">
                                    &nbsp;
                                </div>
                                <asp:Repeater ID="rptTemplte" runat="server" OnItemDataBound="rptTemplte_ItemDataBound">
                                    <ItemTemplate>
                                        <div id="DivSide1" class="LGBC BD_PCS rounded_corners">
                                            <asp:HyperLink ID="hlSide1" runat="server">
                                                <div class="PDTC_LP FI_PCS">
                                                    <asp:Image ID="imgSide1" runat="server" CssClass="full_img_ThumbnailPath_LP" />
                                                </div>
                                            </asp:HyperLink>
                                            <div class="confirm_design LGBC" style="display: none;">
                                                <asp:Label ID="lblFront" runat="server" CssClass="ProdtPricedisplayNoneCs DisplayNoneCSS6"></asp:Label>
                                            </div>
                                            <div class="modify_button_container" style="position: absolute; padding: 0px; margin-top: -27px;">
                                                <div>
                                                    <asp:Button ID="btnSide1Modify" runat="server" CssClass="btn_brown_small" Text="Modify"
                                                        OnClick="lnkBtnSide_Modify_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:Repeater ID="rptUploadFiles" runat="server" OnItemDataBound="rptUploadFiles_ItemDataBound"
                                    OnItemCommand="rptUploadFiles_ItemCommand">
                                    <ItemTemplate>
                                        <div id="DivSide1" class="LGBC BD_PCS rounded_corners">
                                            <div class="DeleteIconPP">
                                                <asp:Button ID="DeleteBtn2" runat="server" CssClass="delete_icon_img " OnClientClick="return confirm('Are you sure you want to delete this Design?');"
                                                    CommandName="DeleteDesign" />
                                            </div>
                                            <asp:HyperLink ID="hlSide1" runat="server">
                                                <div class="PDTC_LP FI_PCS">
                                                    <asp:Image ID="imgSide1" runat="server" CssClass="full_img_ThumbnailPath_LP" />
                                                </div>
                                            </asp:HyperLink>
                                            <div class="confirm_design LGBC height40_LP ">
                                                <asp:Label ID="lblFront" runat="server" CssClass="" Text="front"></asp:Label>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <div class="clearBoth">
                                    &nbsp;
                                </div>
                            </div>
                            <div class="float_left_simple cntuploadfilebtns">
                                <asp:Button ID="UploadFilesBtn" runat="server" Text="Upload files" CssClass="btn_upload_files_Prod_Land rounded_corners5"
                                    OnClientClick="return ShowUpload1();" Visible="false" /><br />
                                <asp:Button ID="btnBactToTemplates" runat="server" Visible="false" Text="Back to templates"
                                    CssClass="btn_BackTo_Tem_Prod_Land rounded_corners5" />
                            </div>
                            <div id="MoreSidesCont" runat="server" class="ViewTemSidesCS" visible="false">
                                <asp:Label ID="ViewMore" runat="server" Text="View more sides"></asp:Label><br />
                                <asp:Label ID="imDesgMode" runat="server" Text="in designer mode"></asp:Label>
                            </div>
                            <asp:Image ID="imgDesignSize" runat="server" CssClass="DesgnFormteImg" />
                        </div>
                    </div>
                </div>

            </div>
            <div class="float_left_simple PriceTableCs col-md-4 col-lg-4 col-xs-12">
                <div class="LGBC rounded_corners Pad5px">
                    <div id="ltrlselecctstock" runat="server" class="SelectQtyContainer">
                        Select your quantity
                    </div>
                    <div class="white_background rounded_corners padding10">
                        <div class="textAlignLeft Mleft10 gallery">
                            <asp:HyperLink ID="lnkPdfFile" runat="server">
                                <asp:Image ID="FinishedGoodImg" runat="server" CssClass="full_img_ThumbnailPath" />
                            </asp:HyperLink>
                        </div>
                        <div>
                            <asp:Label ID="lblQtytxt" runat="server" Text="Quantity" CssClass="checked_design custom_color ProdtPricedisplayNoneCs DisplayNoneCSS6 float_left_simple clearRight MTop12px ClearLeft"></asp:Label>
                            <br />
                            <asp:TextBox ID="RangedQtyTxtBox" runat="server" Visible="false" CssClass="dropdownBorderCS Width60pixel Mleft10 Fsize17"
                                ClientIDMode="Static" onblur="javascript:OnFocusOut();">
                            </asp:TextBox>
                            <asp:Button ID="CalCulatePriceBtn" OnClientClick="CalcutesTextBoxPrice(); return false;"
                                Text="Go" CssClass="go_button rounded_corners5 width35pix" runat="server" Visible="false" />
                            <asp:DropDownList ID="ddQtyOptn" runat="server" CssClass="dropdownBorderCS dropdown190 rounded_corners5 template_designing float_right dropdownQtyListClass clearRight "
                                OnChange="OnQtyDropDownChanged();">
                            </asp:DropDownList>
                        </div>
                        <div class="clearBoth">
                            &nbsp;
                        </div>
                        <br />
                        <div id="Div1" class=" float_left_simple LandPage-Heading-Cs template_designing FloatLeftMargn15 clearBoth Mleft10 spacer20pxbottom"
                            style="padding: 2px;" visible="false" runat="server">
                            <asp:Label ID="lblCfSp" runat="server" CssClass="Fsize13" Text="Confirm spellings and details"></asp:Label>
                        </div>
                        <div id="Div2" class="float_left_simple Mleft10 check_width" visible="false" runat="server">
                            <input id="chkCheckSpelling" runat="server" class="simpleText regular-checkbox" type="checkbox" />
                            <label for="chkAddOnPrice" style="display: inline;">
                            </label>
                        </div>
                        <div class="clearBoth">
                            &nbsp;
                        </div>
                        <div class=" left_align productStockDesc">
                            <asp:Label ID="MainHeadProdtNme" runat="server" CssClass="Fsize18 colorBlack left_align colorDGry"></asp:Label><br />
                            <br />
                            <asp:Label ID="lblStock" runat="server" CssClass="LightGrayLabels"></asp:Label>
                            <asp:Label ID="lblExtra" runat="server" CssClass="LightGrayLabels"></asp:Label>
                        </div>
                        <div class="clearBoth">
                            &nbsp;
                        </div>
                        <div class="float_left_simple grossTotaldiv">
                            <asp:Label ID="lblTotlTxt" runat="server" Text="Total" CssClass="float_left_simple"></asp:Label>
                            <asp:Label ID="lblTaxLabel" runat="server" CssClass="Fsize13 spacer10pxtop float_right marginLeft"></asp:Label>
                            <asp:Label ID="lblGrossTotal" runat="server" CssClass="float_right">$0</asp:Label>

                        </div>
                        <div id="ContainerInStock" runat="server" class="InStockContainer">
                            <asp:Label ID="InStockValue" runat="server"></asp:Label>
                        </div>
                        <div class="clearBoth">
                            &nbsp;
                        </div>
                        <div class="float_right">

                            <asp:Button ID="btnAddToCart" runat="server" OnClientClick="return CheckSpelling();"
                                CssClass="add_to_cart_btn rounded_corners5 MTopM10" OnClick="btnAddToCart_Click"
                                Text="ADD TO CART" /><br />

                        </div>
                        <div class="clearBoth">
                            &nbsp;
                        </div>

                        <div class="clearBoth">
                            &nbsp;
                        </div>
                        <div id="ToogleTable" onclick="TextChange();" style="cursor: pointer; color: #b6d682; font-size: 13px; float: left; margin-top: 15px; margin-bottom: 10px;">
                            <asp:Label ID="ShowPriceMatrix" runat="server" Text="Show price table"></asp:Label>
                        </div>
                        <div class="clearBoth">
                            &nbsp;
                        </div>
                        <div id="HeadingCont" class="" style="display: none;">
                            <div class="confirm_design divtxtalgn MLR">
                                <asp:Literal ID="ltrlQty" runat="server" Visible="false"></asp:Literal>
                            </div>
                            <br />
                            <div class="width50p">
                                <div id="ltrlQuantityDiv" class="Fsize13 divtxtalgn_CS width70pixel float_left_simple"
                                    runat="server" visible="false">
                                    <asp:Literal ID="ltrlQuantity" runat="server" Visible="false"></asp:Literal>
                                </div>
                                <div class="Fsize13 divtxtalgn float_left_simple ddwidth50 marginRight" style="margin-left: -40px;">
                                    <asp:Literal ID="ltrlTo" runat="server" Visible="false"></asp:Literal>
                                </div>
                                <div class="Fsize13 divtxtalgn float_left_simple">
                                    <asp:Literal ID="ltrlFrom" runat="server" Visible="false"></asp:Literal>
                                </div>
                                <div class="divtxtalgn_CS Width40Px float_right">
                                    <asp:Literal ID="LtrlCurrencySymbol" runat="server" Visible="false"></asp:Literal>
                                </div>
                                <div class="clearBoth">
                                    &nbsp;
                                </div>
                            </div>
                        </div>
                        <div class="clearBoth">
                            &nbsp;
                        </div>
                        <div id="tableCont" style="display: none;" class="spacerbottom cntlndngtbl">
                            <asp:Repeater ID="rptPriceMatrix" runat="server" OnItemDataBound="rptPriceMatrix_ItemDataBound">
                                <ItemTemplate>
                                    <div id="container" runat="server">
                                        <div class="divfloat WhtBakg">
                                            <asp:Label ID="lblJasonString" runat="server" CssClass="ClaassJsonObj radioBtnQtyGroupClass"></asp:Label>
                                            <input type="radio" onclick="checkUnCheckRadio(this);" class="radioBtnQtyGroupClassw"
                                                value='<%# Eval("Quantity") %>' id="rbQuantity" name="GrpQuantity" runat="server"
                                                visible="false" />
                                            <input type="hidden" id="txtHiddenPriceMatrixID" class="AccessPriceInScript" value='<%# Eval("PriceMatrixID") %>'
                                                runat="server" />
                                            <input type="hidden" id="txtHiddenIsDiscounted" value='<%# Eval("IsDiscounted") %>'
                                                runat="server" />
                                        </div>
                                        <div id="Quantity" class="LandingPageQty divQNum" runat="server">
                                            <%# Eval("Quantity") %>
                                        </div>
                                        <div id="QtyRangeFrom" class="LandingPageQty divQNum" runat="server">
                                            <%# Eval("QtyRangeFrom")%>
                                        </div>
                                        <div id="QtyRangeTo" class="LandingPageQty divQNum" runat="server">
                                            <%# Eval("QtyRangeTo")%>
                                        </div>
                                        <div id="matrixItemColumn1" runat="server" class="mattPriceMatrixColumn tblBorderTdWidth extra_pricing divtxtflt">
                                            <span id='Matt_PriceMatrixID_<%# Eval("PriceMatrixID")%>' title='<%# Eval("PricePaperType1")%>'>
                                                <asp:Label ID="lblMatt" runat="server" />
                                                <asp:Label ID="lblMattDiscountedPrice" CssClass="custom_colorTS" Visible="false"
                                                    runat="server" Text='<%# Eval("PricePaperType1")%>' />
                                            </span>
                                        </div>
                                        <div id="matrixItemColumn2" runat="server" class="glossyPriceMatrixColumn tblBorderTdWidth extra_pricing divtxtflt">
                                            <span id='Glossy_PriceMatrixID_<%# Eval("PriceMatrixID")%>' title='<%# Eval("PricePaperType2")%>'>
                                                <asp:Label ID="lblGlossy" runat="server" />
                                                <asp:Label ID="lblGlossyDiscountedPrice" CssClass="custom_colorTS" Visible="false"
                                                    runat="server" Text='<%# Eval("PricePaperType2")%>' />
                                            </span>
                                        </div>
                                        <div id="matrixItemColumn3" runat="server" class="premiumMattPriceMatrixColumn tblBorderTdWidth extra_pricing divtxtflt">
                                            <span id='PremiumMatt_PriceMatrixID_<%# Eval("PriceMatrixID")%>' title='<%# Eval("PricePaperType3")%>'>
                                                <asp:Label ID="lblPremiumMatt" runat="server" />
                                                <asp:Label ID="lblPremiumMattDiscountedPrice" CssClass="custom_colorTS" Visible="false"
                                                    runat="server" Text='<%# Eval("PricePaperType3")%>' />
                                            </span>
                                        </div>
                                        <div id="matrixItemColumn4" runat="server" class="Stock4PriceMatrixColumn tblBorderTdWidth extra_pricing divtxtflt">
                                            <span id='4_PriceMatrixID_<%# Eval("PriceMatrixID")%>' title='<%# Eval("PriceStockType4")%>'>
                                                <asp:Label ID="lblForthStock" runat="server" />
                                                <asp:Label ID="lblForthStockDiscountedPrice" CssClass="custom_colorTS" Visible="false"
                                                    runat="server" Text='<%# Eval("PriceStockType4")%>' />
                                            </span>
                                        </div>
                                        <div id="matrixItemColumn5" runat="server" class="Stock5PriceMatrixColumn tblBorderTdWidth extra_pricing divtxtflt">
                                            <span id='5_PriceMatrixID_<%# Eval("PriceMatrixID")%>' title='<%# Eval("PriceStockType5")%>'>
                                                <asp:Label ID="lblFifthStock" runat="server" />
                                                <asp:Label ID="lblFifthStockDiscountedPrice" CssClass="custom_colorTS" Visible="false"
                                                    runat="server" Text='<%# Eval("PriceStockType5")%>' />
                                            </span>
                                        </div>
                                        <div id="matrixItemColumn6" runat="server" class="Stock6PriceMatrixColumn tblBorderTdWidth extra_pricing divtxtflt">
                                            <span id='6_PriceMatrixID_<%# Eval("PriceMatrixID")%>' title='<%# Eval("PriceStockType6")%>'>
                                                <asp:Label ID="lblSixthStock" runat="server" />
                                                <asp:Label ID="lblSixthStockDiscountedPrice" CssClass="custom_colorTS" Visible="false"
                                                    runat="server" Text='<%# Eval("PriceStockType6")%>' />
                                            </span>
                                        </div>
                                        <div id="matrixItemColumn7" runat="server" class="Stock7PriceMatrixColumn tblBorderTdWidth extra_pricing divtxtflt">
                                            <span id='7_PriceMatrixID_<%# Eval("PriceMatrixID")%>' title='<%# Eval("PriceStockType7")%>'>
                                                <asp:Label ID="lblSevenStock" runat="server" />
                                                <asp:Label ID="lblSevenStockDiscountedPrice" CssClass="custom_colorTS" Visible="false"
                                                    runat="server" Text='<%# Eval("PriceStockType7")%>' />
                                            </span>
                                        </div>
                                        <div id="matrixItemColumn8" runat="server" class="Stock8PriceMatrixColumn tblBorderTdWidth extra_pricing divtxtflt">
                                            <span id='8_PriceMatrixID_<%# Eval("PriceMatrixID")%>' title='<%# Eval("PriceStockType8")%>'>
                                                <asp:Label ID="lblEightStock" runat="server" />
                                                <asp:Label ID="lblEightStockDiscountedPrice" CssClass="custom_colorTS" Visible="false"
                                                    runat="server" Text='<%# Eval("PriceStockType8")%>' />
                                            </span>
                                        </div>
                                        <div id="matrixItemColumn9" runat="server" class="Stock9PriceMatrixColumn tblBorderTdWidth extra_pricing divtxtflt">
                                            <span id='9_PriceMatrixID_<%# Eval("PriceMatrixID")%>' title='<%# Eval("PriceStockType9")%>'>
                                                <asp:Label ID="lblNinthStock" runat="server" />
                                                <asp:Label ID="lblNinthStockDiscountedPrice" CssClass="custom_colorTS" Visible="false"
                                                    runat="server" Text='<%# Eval("PriceStockType9")%>' />
                                            </span>
                                        </div>
                                        <div id="matrixItemColumn10" runat="server" class="Stock10PriceMatrixColumn tblBorderTdWidth extra_pricing divtxtflt">
                                            <span id='10_PriceMatrixID_<%# Eval("PriceMatrixID")%>' title='<%# Eval("PriceStockType10")%>'>
                                                <asp:Label ID="lblTenthStock" runat="server" />
                                                <asp:Label ID="lblTenthStockDiscountedPrice" CssClass="custom_colorTS" Visible="false"
                                                    runat="server" Text='<%# Eval("PriceStockType10")%>' />
                                            </span>
                                        </div>
                                        <div id="matrixItemColumn11" runat="server" class="Stock11PriceMatrixColumn tblBorderTdWidth extra_pricing divtxtflt">
                                            <span id='11_PriceMatrixID_<%# Eval("PriceMatrixID")%>' title='<%# Eval("PriceStockType11")%>'>
                                                <asp:Label ID="lblElevenStock" runat="server" />
                                                <asp:Label ID="lblElevenStockDiscountedPrice" CssClass="custom_colorTS" Visible="false"
                                                    runat="server" Text='<%# Eval("PriceStockType11")%>' />
                                            </span>
                                        </div>
                                        <div style="clear: both;">
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
                <div class=" ProductDetailGrayBoxLeftLP rounded_corners float_right ">
                    <div style="background: white; padding: 10px; text-align: left;" class="rounded_corners">
                        <div class="boxCatDes">
                            <div class="mrginBtm">
                                <asp:Label ID="lblSpecHead" CssClass="marginall5 ProductDetailH2" runat="server"
                                    Text="Specifications"></asp:Label>
                            </div>
                            <asp:Label ID="lblCategoryCode" CssClass="marginall5" runat="server" Text=""></asp:Label>
                            <br />
                            <p class="marginall5" style="width: 243px;">
                                <asp:Literal ID="lblCatDes" runat="server" Text=""></asp:Literal>
                            </p>
                            <br />
                            <asp:Label ID="lblRefinngOpt" CssClass="marginall5 ProductDetailH2" runat="server"
                                Text="Refinning Options:" Style="display: none;">
                            </asp:Label>
                            <div class="marginall5 ProductDetailRefOptions" style="display: none;">
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
                                        <asp:Literal ID="Literal1" runat="server" Text="FROM"></asp:Literal>
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
                </div>
            </div>
            <div class="clearBoth">
                &nbsp;
            </div>
            <div class="clearBoth">
                &nbsp;
            </div>
            <div class="height15">
                &nbsp;
            </div>
            <div class="clearBoth">
                &nbsp;
            </div>
            <div class="height15">
                &nbsp;
            </div>
            <%--Hidden Fielsds--%>
            <asp:HiddenField ID="txtHiddenOrderID" Value="0" runat="server" />
            <asp:HiddenField ID="txtHiddenCorpItemTemplateID" Value='0' runat="server" />
            <asp:HiddenField ID="txtHiddenProductName" Value='' runat="server" />
            <asp:HiddenField ID="txtHiddenItemDiscountRate" Value="0" runat="server" />
            <asp:HiddenField ID="txtHiddenProductCategoryID" runat="server" />
            <asp:HiddenField ID="txtHiddenProductItemID" runat="server" />
            <asp:HiddenField ID="txtHiddenProdItemPrice" Value="0" runat="server" />
            <asp:HiddenField ID="txtHiddenAddonTotal" Value="0" runat="server" />
            <asp:HiddenField ID="hfSelectedStockId" Value="0" runat="server" />
            <asp:HiddenField ID="hfSelectedQuantity" Value="0" runat="server" />
            <asp:HiddenField ID="hfSelectedCostCenters" Value="0" runat="server" />
            <asp:HiddenField ID="hfJVSQ" Value="0" runat="server" />
            <asp:HiddenField ID="hfBrokerMarkup" Value="0" runat="server" />
            <asp:HiddenField ID="hfContactMarkup" Value="0" runat="server" />
            <asp:HiddenField ID="hfIndexOfRangedQty" Value="0" runat="server" />
            <asp:HiddenField ID="hfCountOfMatrix" Value="0" runat="server" />
            <asp:HiddenField ID="hfRangedQty" Value="0" runat="server" />
            <input type="hidden" id="hdnTargetCtrlUpload" runat="server" />
            <input type="hidden" id="hdnFileUploadTargetCtrl" runat="server" />
            <input type="hidden" id="hdnLoaderTargetCtrl" runat="server" />
            <asp:HiddenField ID="hfQtyRangdSelectedPrice" Value="0" runat="server" />
            <asp:HiddenField ID="hfQtyRangedCustPer" Value="0" runat="server" />
            <asp:HiddenField ID="hfSTockID" Value="0" runat="server" />
            <asp:HiddenField ID="hfSTockIDOnModify" Value="0" runat="server" />
            <asp:HiddenField ID="hfPriceExVat" Value="0" runat="server" />
            <asp:HiddenField ID="hfdiscountandVatApplied" Value="0" runat="server" />
            <asp:HiddenField ID="hfStockLevel" Value="0" runat="server" />
            <asp:HiddenField ID="hfBackOrderingAllowed" Value="0" runat="server" />
            <asp:HiddenField ID="hfProductVAT" Value="-1" runat="server" />
            <ajaxToolkit:ModalPopupExtender ID="mpeImgBidDisplayPopup" BehaviorID="mpeImgBidDisplayPopup"
                TargetControlID="hdnTargetCtrlUpload" PopupControlID="PnlImdDisplayer" BackgroundCssClass="ModalPopupBG"
                runat="server" Drag="false" PopupDragHandleControlID="draghandlepnl" DropShadow="false" />
            <asp:Panel ID="PnlImdDisplayer" runat="server" CssClass="FileUploaderPopup_Mesgbox LGBC rounded_corners"
                Style="display: none; width: 500px">
                <div style="background: white; height: 450px;">
                    <div class="Width100Percent">
                        <div class="float_left" style="padding-top: 5px;">
                            <asp:Label ID="Label1" runat="server" Text="Image Previewer" CssClass="FileUploadHeaderText"></asp:Label>
                        </div>
                        <div class="exit_container25" onclick="ImgDisplayerUploaderHide();">
                            <div id="btnCancelMessageBox" runat="server" class="exit_popup">
                            </div>
                        </div>
                    </div>
                    <div class="clearBoth">
                        &nbsp;
                    </div>
                    <div class="FileuploadInnerContainer">
                        <br />
                        <img id="imgDisplayBigPicture" class="card_size_image_popUp" alt="" />
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="draghandlepnl" runat="server" Style="display: block;">
            </asp:Panel>
            <%-- Start Modal Popupextender File Uploader--%>
            <ajaxToolkit:ModalPopupExtender ID="mpeUploadYourDesignPopup" BehaviorID="mpeUploadYourDesignPopup"
                TargetControlID="hdnFileUploadTargetCtrl" PopupControlID="PnlUploader" BackgroundCssClass="ModalPopupBG"
                CancelControlID="btnCancelFileUpload" runat="server" Drag="false" DropShadow="false" />
            <asp:Panel ID="PnlUploader" runat="server" Style="display: none;" CssClass="FileUploaderPopup_Mesgbox UploadPanel rounded_corners">

                <div class="UploadGuidLinelbl float_left">
                    <asp:Label ID="lblHeader" runat="server" CssClass=" FileUploadHeaderText lblUploadHeading"
                        Text="Upload Your Design"></asp:Label>
                </div>
                <div id="btnCancelFileUpload" onclick="$find('mpeUploadYourDesignPopup').hide();"
                    class="MesgBoxBtnsDisplay rounded_corners5" style="">
                    Close
                </div>
                <div class="clearBoth">
                    &nbsp;
                </div>
                <div class="SolidBorderCS">
                    &nbsp;
                </div>
                <div id="updateHide" class="InnerUploadContainer">

                    <div id="downloadExmp" runat="server" class="DownloadExmContainer">
                        Download templates
                    </div>
                    <div id="lblDEmple" runat="server" class="DExmlbl">
                    </div>
                    <div id="DownloadIcons" runat="server" style="text-align: left;">
                        <asp:HyperLink ID="ImgIconPs" runat="server" Visible="false" Target="_blank" CssClass="DownloadImages"></asp:HyperLink>
                        <asp:HyperLink ID="ImgIconAi" runat="server" Visible="false" Target="_blank" CssClass="DownloadImages"></asp:HyperLink>
                        <asp:HyperLink ID="ImgIconId" runat="server" Visible="false" Target="_blank" CssClass="DownloadImages"></asp:HyperLink>
                        <asp:HyperLink ID="ImgIconPdf" runat="server" Visible="false" Target="_blank" CssClass="DownloadImages"></asp:HyperLink>
                    </div>
                    <div class="clearBoth">&nbsp;</div>
                    <div id="designFormatC" runat="server">
                        <div class="DesgnFormatelbl">
                            <asp:Label ID="GridDsgnForSizes" runat="server" Text="Design sizes & format" CssClass="Fsize17"></asp:Label>
                        </div>
                        <div class="white_background rounded_corners left_align" style="">

                            <asp:Image ID="ImgeDesgnFormate2" runat="server" CssClass="DesgnFormteImg2" />
                        </div>

                    </div>
                    <div class="guidlinContainer">
                        <asp:Label ID="UpFileGuidl" runat="server" Text="Upload file guidelines" CssClass="Fsize17"></asp:Label>
                    </div>
                    <div class="float_right UploadsGuidLineContainer">
                        <asp:HiddenField ID="hfSideNumber" runat="server"></asp:HiddenField>
                        <asp:FileUpload ID="txtUploadYourDsgn" runat="server" CssClass="file_upload_box210"
                            Style="display: none;" />
                        <asp:Button ID="btnUploadYourFile" runat="server" CssClass="Uplod_PCS rounded_corners5 "
                            OnClientClick="return showLoadingImg();" Text="Upload" />

                        <div id="Div3" runat="server" class="float_left_simple Fsize12" style="width: 175px; text-align: left; margin-bottom: 5px; margin-top: 10px;">
                            Permitted formats
                        </div>
                        <div>
                            <img id="img1" runat="server" alt="" src="~/images/pdfFormat.PNG" class="float_left_simple"
                                width="35" style="margin-right: 5px;" />
                            <%--  <img id="img2" runat="server" alt="" src="~/images/pngFormat.PNG" class="float_left_simple"
                                width="35" style="margin-right: 5px;" />--%>
                            <img id="img3" runat="server" alt="" src="~/images/jpgFormat.PNG" class="float_left_simple"
                                width="35" style="margin-right: 5px;" />
                            <%-- <img id="img4" runat="server" alt="" src="~/images/gifFormat.PNG" class="float_left_simple"
                                width="35" style="margin-right: 5px;" />--%>
                        </div>
                        <div id="Recommandedtxt" runat="server" style="">
                        </div>
                        <div id="PdfFormatInfo" runat="server">
                        </div>
                        <div id="MaxFileSize" runat="server">
                        </div>
                        <div id="logoImgsize" runat="server">
                        </div>
                    </div>
                    <div class="clearBoth">&nbsp;</div>
                    <div class="GLHeadingContainer float_left_simple">
                        <asp:Label ID="lblGL1Heading" runat="server" CssClass="GLHeading"></asp:Label>
                    </div>
                    <div class="GLHeadDescContainer float_left_simple">
                        <%--FirstGLContiner--%>
                        <asp:Literal ID="ltrlGL1Desc" runat="server"></asp:Literal>
                    </div>
                    <div class="clearBoth">&nbsp;</div>
                    <div class="GLHeadingContainer float_left_simple">
                        <asp:Label ID="lblGL2Heading" runat="server" CssClass="GLHeading"></asp:Label>
                    </div>
                    <div class="GLHeadDescContainer float_left_simple">
                        <asp:Literal ID="ltrlGL2Desc" runat="server"></asp:Literal>
                    </div>
                    <div class="clearBoth">&nbsp;</div>
                    <div class="GLHeadingContainer float_left_simple">
                        <asp:Label ID="lblGL3Heading" runat="server" CssClass="GLHeading"></asp:Label>
                    </div>
                    <div class="GLHeadDescContainer float_left_simple">
                        <asp:Literal ID="ltrlGL3Desc" runat="server"></asp:Literal>
                    </div>
                    <div class="clearBoth">&nbsp;</div>
                    <div class="GLHeadingContainer float_left_simple">
                        <asp:Label ID="lblGL4Heading" runat="server" CssClass="GLHeading"></asp:Label>
                    </div>
                    <div class="GLHeadDescContainer float_left_simple">
                        <asp:Literal ID="ltrlGL4Desc" runat="server"></asp:Literal>
                    </div>
                    <div class="clearBoth">&nbsp;</div>
                    <div class="GLHeadingContainer float_left_simple">
                        <asp:Label ID="lblGL5Heading" runat="server" CssClass="GLHeading"></asp:Label>
                    </div>
                    <div class="GLHeadDescContainer float_left_simple" style="width: 380px;">
                        <asp:Literal ID="ltrlGL5Desc" runat="server"></asp:Literal>
                    </div>
                    <div class="clearBoth">&nbsp;</div>
                    <div class="GLHeadingContainer float_left_simple">
                        <asp:Label ID="lblGL6Heading" runat="server" CssClass="GLHeading"></asp:Label>
                    </div>
                    <div class="GLHeadDescContainer float_left_simple">
                        <asp:Literal ID="ltrlGL6Desc" runat="server"></asp:Literal>
                    </div>
                    <div class="clearBoth">&nbsp;</div>
                    <div class="GLHeadingContainer float_left_simple">
                        <asp:Label ID="lblGL7Heading" runat="server" CssClass="GLHeading"></asp:Label>
                    </div>
                    <div class="GLHeadDescContainer float_left_simple">
                        <asp:Literal ID="ltrlGL7Desc" runat="server"></asp:Literal>
                    </div>
                    <div class="clearBoth">&nbsp;</div>
                    <div class="GLHeadingContainer float_left_simple">
                        <asp:Label ID="lblGL8Heading" runat="server" CssClass="GLHeading"></asp:Label>
                    </div>
                    <div class="GLHeadDescContainer float_left_simple">
                        <asp:Literal ID="ltrlGL8Desc" runat="server"></asp:Literal>
                    </div>
                    <div class="clearBoth">&nbsp;</div>

                </div>
                <asp:Button ID="HdUplodFileBtn" runat="server" Style="display: none;" OnClick="btnUploadYourFile_Click" />
            </asp:Panel>
        </div>
        <br />
    </div>
    <uc2:RRViews ID="rr1" runat="server" />
    <br />
    <div class="clearBoth">
        &nbsp;
    </div>
    <br />
    <br />
    <br />
    <script type="text/javascript">

        var productTotal = 0;
        var prouctTotalSavings = 0;
        var addonTotalSavings = 0;
        var selectedQuantity = 1;
        var selectedBtton = null;

        function ShowUpload1() {

            $('#<%=hfSideNumber.ClientID %>').val('1');
            $find('mpeUploadYourDesignPopup').show();
            $(".imgUploading").css("visibility", "hidden");
            return false;
        }

        function showLoadingImg() {
            $('#<%=txtUploadYourDsgn.ClientID %>').click();
            return false;
            //            var HasVal = $('#<%=txtUploadYourDsgn.ClientID %>').val();
            //            if (HasVal == "") {

            //                return false;
            //            } else {
            //                $(".imgUploading").css("margin-bottom", "-8px");
            //                $(".imgUploading").css("visibility", "visible");
            //                return true;
            //            }
        }

        function DisplayFileUploadPopup(btnUploadFile) {

            var result = false;
            result = ValidateFileSelection();
            if (result == false)
                alert("Please select file");
            else {
                ShowProgress();
            }
            return result;
        }

        function showProgress() {
            var shadow = document.getElementById("divShd");
            var bws = getBrowserHeight();
            shadow.style.width = bws.width + "px";
            shadow.style.height = bws.height + "px";
            var left = parseInt((bws.width - 350) / 2);
            var top = parseInt((bws.height - 200) / 2);
            //shadow = null;
            $('#divShd').css("display", "block");
            $('#UpdateProgressUserProfile').css("display", "block");
            return true;
        }


        function getBrowserHeight() {
            var intH = 0;
            var intW = 0;
            if (typeof window.innerWidth == 'number') {
                intH = window.innerHeight;
                intW = window.innerWidth;
            }
            else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
                intH = document.documentElement.clientHeight;
                intW = document.documentElement.clientWidth;
            }
            else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
                intH = document.body.clientHeight;
                intW = document.body.clientWidth;
            }
            return { width: parseInt(intW), height: parseInt(intH) };
        }

        function ValidateFileSelection() {

            var addedFileDivsList = $("div .MultiFile-label");

            if (addedFileDivsList.length > 0)
                return true;
            else
                return false;
        }

        function BindEvents() {
            $('.gallery a').lightBox({
                maxHeight: 500,
                maxWidth: 700
            });
        }
        function TextChange() {
            var HideMesg = '<%=Resources.MyResource.lnkHideTabel %>';
            var ShowTable = '<%=Resources.MyResource.lnkShowTbl %>';
            if ($('#<%= ShowPriceMatrix.ClientID %>').text() == HideMesg) {
                $('#<%= ShowPriceMatrix.ClientID %>').text(ShowTable);
                $('#ToogleTable').css("margin-bottom", "0px");
            }
            else {
                $('#<%= ShowPriceMatrix.ClientID %>').text(HideMesg);
                $('#ToogleTable').css("margin-bottom", "10px");
            }
        }
        $(document).ready(function () {
            $("#ToogleTable").click(function () {
                $('#HeadingCont').slideToggle();
                $('#tableCont').slideToggle();

            });
            BindEvents();
            var isRangedQuantity = '<%= RangedQty %>';
            if (isRangedQuantity == 1) {
                var RangtxtBox = $("#RangedQtyTxtBox").val();
                if (RangtxtBox == "") {
                } else {
                    CalcutesTextBoxPrice();
                }
            } else {
                OnQtyDropDownChanged();
            }

            CreateMultipleUpload();
            SetSelectedCostCenter();
            calculateAdOnPrices();
            ChangeCheckBoxVlueWithQuantityselection();

            $('input:radio[name=StockPaper]:checked').next().removeClass('grayRadiantbtn').addClass('OrangeGradiantbtn');
            $('input:radio[name=StockPaper]:checked').next().css("background-color", "white");

            hideShowPriceColumns($('input:radio[name=StockPaper]:checked').attr("class"));

            var sID = $('input:radio[name=StockPaper]:checked').attr("dataId")

            $("#<%= hfSTockID.ClientID %>").val(sID);


            $('#Panel1').css('position', 'fixed');
            $('#Panel1').css("left", ($(window).width() / 2 - $('#Panel1').width() / 2) + "px");
            $('#Panel1').css("top", ($(window).height() / 2 - $('#Panel1').height() / 2) + "px");

            TextChange(); // show pricing by default
            $('#HeadingCont').slideToggle();
            $('#tableCont').slideToggle();
        });

        function SetSelectedCostCenter() {
            var selectedCostCenters = $('#<%= hfSelectedCostCenters.ClientID %>').val();
            if (selectedCostCenters != null) {
                var names = selectedCostCenters.split('___');
                for (var i = 0; i < names.length; i++) {
                    $('[CostCenterId="' + names[i] + '"]').children().attr('checked', 'checked');
                }
            }
        }

        function clearMsgLabel() {


        }

        function CreateMultipleUpload() {
            //set up the file upload
            $("#" + "<%=txtUploadYourDsgn.ClientID %>").MultiFile(
            {
                max: 9,
                accept: 'pdf,jpg,eps,tiff,tif,csv,xls,xlsx',
                onFileAppend: function (element, value, master_element) {
                    $find('mpeUploadYourDesignPopup').hide();

                    showProgress(); $("#" + "<%=HdUplodFileBtn.ClientID %>").click();
                }
            });
            }

            function FileUploaderShow() {

                $find('mpeUploadYourDesignPopup').show();
            }

            function FileUploaderHide() {

                $find('mpeUploadYourDesignPopup').hide();
                //            $('input:file').MultiFile('reset');

                clearMsgLabel();
            }

            function ImgDisplayerUploaderShow(imgPath) {
                //alert("show me");
                $find('mpeImgBidDisplayPopup').show();
                for (i = 0; i <= 6000; i++) { } //wait
                $("#imgDisplayBigPicture").attr("src", imgPath);

                //return false;

            }

            function ImgDisplayerUploaderHide() {

                $("#imgDisplayBigPicture").attr("src", "/images/wpLoader.gif");
                $find('mpeImgBidDisplayPopup').hide();
            }

            function displayProductItemTotal(price) {

                $("#" + "<%=txtHiddenProdItemPrice.ClientID %>").val(price);
            //            $("#lblProductItemTotal").text(getCurrencySymbol() + (price).toString());
        }

        function displayAddOnTotal(totalPrice) {

            $("#" + "<%=txtHiddenAddonTotal.ClientID %>").val((totalPrice).toString());
            //            $("#lblOptionaltemsTotal").text(getCurrencySymbol() + (totalPrice).toString());
        }

        //product details page script




        function CheckSpelling() {

            var artworkuploaded = '<%= ArtworkdesignUploaded %>';
            var Msg = "<%= Resources.MyResource.ArtWorkAlert %>";
            var numOfRecords = '<%= CanAddToCart %>';
            var RangedMatrix = $("#<%= hfRangedQty.ClientID %>").val();
            if (parseInt(numOfRecords) == 2) {
                if (parseInt(numOfRecords) == 2) {
                    if (RangedMatrix == 1) {
                        var txtbox = $('#<%=RangedQtyTxtBox.ClientID %>').val();
                        if (txtbox == '' || txtbox.replace(/^\s+/, '') == '') {
                            ShowPopup('Message', "please enter quantity to proceed.");
                            return false;
                        } else if (artworkuploaded == 0) {
                            ShowPopupAddToCart('Message', Msg);

                            return false;
                        } else {
                            showProgress();
                            return true;
                        }
                    }
                    else {
                        if (artworkuploaded == 0) {
                            ShowPopupAddToCart('Message', Msg);

                            return false;
                        } else {
                            showProgress();
                            return true;
                        }
                    }

                }
                else if (parseInt(numOfRecords) > 0) {
                    if (parseInt(numOfRecords) == 1) {
                        if (RangedMatrix == 1) {
                            var txtbox = $('#<%=RangedQtyTxtBox.ClientID %>').val();
                            if (txtbox == '' || txtbox.replace(/^\s+/, '') == '') {
                                ShowPopup('Message', "please enter quantity to proceed.");
                                return false;
                            } else {
                                return true;
                                showProgress();
                            }
                        }
                        else {
                            return true;
                            showProgress();
                        }
                    }
                }
                else {
                    var uploaddesignside1 = "<%=Resources.MyResource.uploaddesignside1 %>";
                    ShowPopup('Message', uploaddesignside1);
                    return false;

                }
        }
        if (parseInt(numOfRecords) == 0) {
            var uploaddesignside1 = "<%=Resources.MyResource.uploaddesignside1 %>";
                ShowPopup('Message', uploaddesignside1);
                return false;
            }
            if (RangedMatrix == 1) {
                var txtbox = $('#<%=RangedQtyTxtBox.ClientID %>').val();
                if (txtbox == '' || txtbox.replace(/^\s+/, '') == '') {
                    ShowPopup('Message', "please enter quantity to proceed.");
                    return false;
                }
            }
            if (parseInt(numOfRecords) == 3) {

                ShowPopup('Message', "You cannot place order because item is out of stock.");
                return false;
            }
            if ($("#<%= hfStockLevel.ClientID %>").val() > 0) {
                if ($("#<%= hfBackOrderingAllowed.ClientID %>").val() == 0) {
                    if (RangedMatrix == 1) {
                        var selectedQty = parseInt($("#<%= RangedQtyTxtBox.ClientID %>").val());
                        var StockL = parseInt($("#<%= hfStockLevel.ClientID %>").val());
                        if (selectedQty > StockL) {
                            ShowPopup('Message', "You cannot place order because the selected quantity is more than stock.");
                            return false;
                        }
                    } else {
                        var selectedQty = parseInt($(".dropdownQtyListClass").find('option:selected').text());
                        var StockL = parseInt($("#<%= hfStockLevel.ClientID %>").val());
                        if (selectedQty > StockL) {
                            ShowPopup('Message', "You cannot place order because the selected quantity is more than stock.");
                            return false;
                        }
                    }

                }

            }
        }

        /*Adon Pricess methods*/
        function calculateAdOnPrices() {
            this.displayTotalPrice(productTotal, goThroughAdOnPricesChkBoxesForTotal());
        }
        function goThroughAdOnPricesChkBoxesForTotal() {
            var BMode = '<%=IsBrokerMde %>';

            var checkedPriceTotal = 0;
            var addonSaving = 0;
            var checkBoxJasonData = null;
            var QtyJason = null;
            var itemsList = '';
            var RangedMatrix = $("#<%= hfRangedQty.ClientID %>").val();

            $(".checkBoxAdOnPriceCheckBox input:checkbox").each(function (i, val) {

                $(this).css("height", "18px");
                $(this).css("width", "21px");
                //Gets the Jason Data associated with it
                checkBoxJasonData = jQuery.parseJSON($(this).parent().attr("CheckBoxJasonData"));
                $(this).css("opacity", "0");
                if ($(this).is(':checked')) {
                    $(this).parent().css("background-image", "url('../../images/tick-box.png')");

                    if (checkBoxJasonData != null) {

                        itemsList = itemsList + "<br/> " + $(this).parent().next().text();

                        // check the costcenter if its type is fixed then we will take its setup cost
                        //if it is per quantity type then we will (qty * priceperunitqty) + setupcost

                        var actualPrice = checkBoxJasonData.ActualPrice;

                        if (checkBoxJasonData.Type == 2) {

                            var Quan = $('#<%=hfJVSQ.ClientID %>').val();
                            if (RangedMatrix == 1) {

                                actualPrice = (Quan * actualPrice) + checkBoxJasonData.SetupCost;
                                if (actualPrice < checkBoxJasonData.MinimumCost && checkBoxJasonData.MinimumCost != 0) {
                                    actualPrice = checkBoxJasonData.MinimumCost;
                                    if (BMode == 1) {
                                        var markupB = $("#<%= hfBrokerMarkup.ClientID %>").val();
                                        var markupC = $("#<%= hfContactMarkup.ClientID %>").val();
                                        if (markupB > 0) {
                                            actualPrice = actualPrice + (actualPrice * (markupB / 100));
                                            if (markupC > 0) {
                                                actualPrice = actualPrice + (actualPrice * (markupC / 100));
                                            }
                                            else if (markupC < 0) {
                                                actualPrice = actualPrice + (actualPrice * (markupC / 100));
                                            }
                                        }
                                        else if (markupB < 0) {
                                            actualPrice = actualPrice + (actualPrice * (markupB / 100));
                                            if (markupC > 0) {
                                                actualPrice = actualPrice + (actualPrice * (markupC / 100));
                                            }
                                            else if (markupC < 0) {
                                                actualPrice = actualPrice + (actualPrice * (markupC / 100));
                                            }
                                        }
                                        else if (markupC > 0) {
                                            actualPrice = actualPrice + (selectedPrice * (markupC / 100));
                                        }
                                        else if (markupC < 0) {
                                            actualPrice = actualPrice + (selectedPrice * (markupC / 100));
                                        }
                                    }
                                }
                                //$(this).parent().next().next().children(':first').html(getCurrencySymbol() + actualPrice)
                            }
                            else {
                                actualPrice = (Quan * actualPrice) + checkBoxJasonData.SetupCost;
                                if (actualPrice < checkBoxJasonData.MinimumCost && checkBoxJasonData.MinimumCost != 0) {
                                    actualPrice = checkBoxJasonData.MinimumCost;

                                    if (BMode == 1) {
                                        var markupB = $("#<%= hfBrokerMarkup.ClientID %>").val();
                                        var markupC = $("#<%= hfContactMarkup.ClientID %>").val();
                                        if (markupB > 0) {
                                            actualPrice = actualPrice + (actualPrice * (markupB / 100));
                                            if (markupC > 0) {
                                                actualPrice = actualPrice + (actualPrice * (markupC / 100));
                                            }
                                            else if (markupC < 0) {
                                                actualPrice = actualPrice + (actualPrice * (markupC / 100));
                                            }
                                        }
                                        else if (markupB < 0) {
                                            actualPrice = actualPrice + (actualPrice * (markupB / 100));
                                            if (markupC > 0) {
                                                actualPrice = actualPrice + (actualPrice * (markupC / 100));
                                            }
                                            else if (markupC < 0) {
                                                actualPrice = actualPrice + (actualPrice * (markupC / 100));
                                            }
                                        }
                                        else if (markupC > 0) {
                                            actualPrice = actualPrice + (selectedPrice * (markupC / 100));
                                        }
                                        else if (markupC < 0) {
                                            actualPrice = actualPrice + (selectedPrice * (markupC / 100));
                                        }
                                    }
                                }
                                //$(this).parent().next().next().children(':first').html(getCurrencySymbol() + actualPrice)
                            }

                        }

                        if (checkBoxJasonData.IsDiscounted == false) {
                            checkedPriceTotal = checkedPriceTotal + parseFloat(actualPrice); //Gets Actual non discounted Price
                        }
                        else {
                            var discountPrice = checkBoxJasonData.DiscountedPrice;

                            if (checkBoxJasonData.Type == 2) {
                                var Quan = $('#<%=hfJVSQ.ClientID %>').val();
                                if (RangedMatrix == 1) {

                                    discountPrice = (Quan * discountPrice) + checkBoxJasonData.SetupCost;
                                    if (discountPrice < checkBoxJasonData.MinimumCost && checkBoxJasonData.MinimumCost != 0) {
                                        discountPrice = checkBoxJasonData.MinimumCost;
                                        if (BMode == 1) {
                                            var markupB = $("#<%= hfBrokerMarkup.ClientID %>").val();
                                            var markupC = $("#<%= hfContactMarkup.ClientID %>").val();
                                            if (markupB > 0) {
                                                discountPrice = discountPrice + (discountPrice * (markupB / 100));
                                                if (markupC > 0) {
                                                    discountPrice = discountPrice + (discountPrice * (markupC / 100));
                                                }
                                                else if (markupC < 0) {
                                                    discountPrice = discountPrice + (discountPrice * (markupC / 100));
                                                }
                                            }
                                            else if (markupB < 0) {
                                                discountPrice = discountPrice + (discountPrice * (markupB / 100));
                                                if (markupC > 0) {
                                                    discountPrice = discountPrice + (discountPrice * (markupC / 100));
                                                }
                                                else if (markupC < 0) {
                                                    discountPrice = discountPrice + (discountPrice * (markupC / 100));
                                                }
                                            }
                                            else if (markupC > 0) {
                                                discountPrice = discountPrice + (discountPrice * (markupC / 100));
                                            }
                                            else if (markupC < 0) {
                                                discountPrice = discountPrice + (discountPrice * (markupC / 100));
                                            }
                                        }
                                    }
                                    $(this).parent().next().next().children(':first').html(getCurrencySymbol() + discountPrice);
                                } else {
                                    discountPrice = (Quan * discountPrice) + checkBoxJasonData.SetupCost;
                                    if (discountPrice < checkBoxJasonData.MinimumCost && checkBoxJasonData.MinimumCost != 0) {
                                        discountPrice = checkBoxJasonData.MinimumCost;
                                        if (BMode == 1) {
                                            var markupB = $("#<%= hfBrokerMarkup.ClientID %>").val();
                                            var markupC = $("#<%= hfContactMarkup.ClientID %>").val();
                                            if (markupB > 0) {
                                                discountPrice = discountPrice + (discountPrice * (markupB / 100));
                                                if (markupC > 0) {
                                                    discountPrice = discountPrice + (discountPrice * (markupC / 100));
                                                }
                                                else if (markupC < 0) {
                                                    discountPrice = discountPrice + (discountPrice * (markupC / 100));
                                                }
                                            }
                                            else if (markupB < 0) {
                                                discountPrice = discountPrice + (discountPrice * (markupB / 100));
                                                if (markupC > 0) {
                                                    discountPrice = discountPrice + (discountPrice * (markupC / 100));
                                                }
                                                else if (markupC < 0) {
                                                    discountPrice = discountPrice + (discountPrice * (markupC / 100));
                                                }
                                            }
                                            else if (markupC > 0) {
                                                discountPrice = discountPrice + (discountPrice * (markupC / 100));
                                            }
                                            else if (markupC < 0) {
                                                discountPrice = discountPrice + (discountPrice * (markupC / 100));
                                            }
                                        }
                                    }
                                    $(this).parent().next().next().children(':first').html(getCurrencySymbol() + discountPrice);
                                }

                            }

                            checkedPriceTotal = checkedPriceTotal + parseFloat(discountPrice); //Gets Actual non discounted Price

                            addonSaving = addonSaving + (parseFloat(actualPrice) - parseFloat(discountPrice));
                        }
                    }
                } else {
                    $(this).parent().css("background-image", "url('../../images/square-final.png')");
                }
                checkBoxJasonData = null;
            });

            $('#<%=lblExtra.ClientID %>').html(itemsList);

            addonTotalSavings = addonSaving; //global variable            
            return checkedPriceTotal;
        }

        /*End Adon Pricess methods*/

        function ShowTable() {
            $('#tableCont').css("display", "block");
            $('#HeadingCont').css("display", "block");
        }
        function displayTotalPrice(productPrice, addOnPrice) {
            var productPriceTotal = productPrice;
            var addonPriceTotal = addOnPrice;
            var IsCalculateVat = '<%=IsCalcVat %>';
            var culturee = '<%=IsCurrentCultureEng %>';

            var IsInt = "";
            IsInt = productPrice;

            if (IsInt == 0) {
            } else {
                IsInt = IsInt.toString();
                var indexOfDot = IsInt.indexOf(".");
                var indexOfComma = IsInt.indexOf(",") == -1

                if (indexOfDot == -1 || indexOfComma == -1) {

                    IsInt = -1;
                } else {
                    IsInt = 1;
                }

            }

            if (culturee == 1) {
                this.displayProductItemTotal(productPriceTotal.toFixed(2));
                this.displayAddOnTotal(addonPriceTotal.toFixed(2))
            } else {
                this.displayProductItemTotal(productPriceTotal);
                this.displayAddOnTotal(addonPriceTotal)
            }


            var total = 0;


            if (IsCalculateVat > 0) {
                var isVatApplicable = $("#<%= hfdiscountandVatApplied.ClientID %>").val();

                if (isVatApplicable == 0) { // this hidden feild will have a value 1 in case of non-discount price

                    total = productPriceTotal + addonPriceTotal;
                    if (IsInt == -1) {
                        total = parseInt(productPriceTotal) + addonPriceTotal;
                    } else {
                        total = productPriceTotal + addonPriceTotal;
                    }

                    var productVAt = $("#<%= hfProductVAT.ClientID %>").val();

                    if (productVAt != -1) {// apply ProductVat 
                        total = total + ((total * productVAt) / 100);
                    } else {// apply global settings 
                        total = total + ((total * IsCalculateVat) / 100);
                    }

                } else {// the discounted value case

                    var valueIncludeDiscount = $("#<%= hfPriceExVat.ClientID %>").val();
                    valueIncludeDiscount = parseFloat(valueIncludeDiscount);

                    total = valueIncludeDiscount + addonPriceTotal;

                    if (IsInt == -1) {
                        total = valueIncludeDiscount + addonPriceTotal;
                    } else {
                        total = valueIncludeDiscount + addonPriceTotal;
                    }

                    total = total + ((total * IsCalculateVat) / 100);

                }
            } else {
                total = productPriceTotal + addonPriceTotal;

                if (IsInt == -1) {

                    if (culturee == 1) {
                    } else {
                        productPriceTotal = productPriceTotal.replace(",", ".");
                    }



                    total = parseFloat(productPriceTotal) + addonPriceTotal;
                } else {
                    total = productPriceTotal + addonPriceTotal;
                }

            }


            var CorpUser = '<%=IsUsrCorp %>';
            var Reslt = '<%=PricingShown %>';
            var CorpPricing = '<%=isCorporatePricingshown %>';
            if (CorpUser == 1) {
                if (CorpPricing == 1) {
                    if (IsInt == -1) {
                        $('#<%=lblGrossTotal.ClientID %>').text(getCurrencySymbol() + total.toFixed(2));
                    } else {
                        $('#<%=lblGrossTotal.ClientID %>').text(getCurrencySymbol() + parseFloat(total).toFixed(2));
                    }
                }
                else {
                    $('#<%=lblGrossTotal.ClientID %>').css("display", "none");
                }
            } else {
                if (Reslt == 1) {
                    if (culturee == 1) {

                        if (IsInt == -1) {

                            $('#<%=lblGrossTotal.ClientID %>').text(getCurrencySymbol() + total.toFixed(2));
                        } else {
                            $('#<%=lblGrossTotal.ClientID %>').text(getCurrencySymbol() + parseFloat(total).toFixed(2));
                        }

                    } else {
                        if (IsCalculateVat > 0) {
                        } else {
                            if (isNaN(total) === true) {
                                total = total.replace(",", ".");
                            }
                        }
                        if (IsInt == -1) {

                            $('#<%=lblGrossTotal.ClientID %>').text(getCurrencySymbol() + total.toFixed(2));
                        } else {
                            $('#<%=lblGrossTotal.ClientID %>').text(getCurrencySymbol() + parseFloat(total).toFixed(2));
                        }

                    }

                }
                else {
                    $('#<%=lblGrossTotal.ClientID %>').css("display", "none");
                    //$('.PricingClass').css("display", "none");
                }
            }
        }

        function ChangeCheckBoxVlueWithQuantityselection() {
            var checkBoxJasonData = null;
            var BMode = '<%=IsBrokerMde %>';
            var itemsList = '';
            var Quantitys = $('#<%=hfJVSQ.ClientID %>').val();
            var RangedMatrix = $("#<%= hfRangedQty.ClientID %>").val();
            if (RangedMatrix == 1) {
                if (Quantitys == 0) {
                    Quantitys = 1;
                }
            }

            $(".checkBoxAdOnPriceCheckBox input:checkbox").each(function (i, val) {
                checkBoxJasonData = jQuery.parseJSON($(this).parent().attr("CheckBoxJasonData"));
                if (checkBoxJasonData != null) {

                    itemsList = itemsList + ", " + $(this).parent().next().text();

                    // check the costcenter if its type is fixed then we will take its setup cost
                    //if it is per quantity type then we will (qty * priceperunitqty) + setupcost
                    var actualPrice = checkBoxJasonData.ActualPrice;

                    if (checkBoxJasonData.Type == 2) {
                        actualPrice = (Quantitys * actualPrice) + checkBoxJasonData.SetupCost;
                        if (actualPrice < checkBoxJasonData.MinimumCost && checkBoxJasonData.MinimumCost != 0) {
                            actualPrice = checkBoxJasonData.MinimumCost;
                            if (BMode == 1) {
                                var markupB = $("#<%= hfBrokerMarkup.ClientID %>").val();
                                var markupC = $("#<%= hfContactMarkup.ClientID %>").val();
                                if (markupB > 0) {
                                    actualPrice = actualPrice + (actualPrice * (markupB / 100));
                                    if (markupC > 0) {
                                        actualPrice = actualPrice + (actualPrice * (markupC / 100));
                                    }
                                    else if (markupC < 0) {
                                        actualPrice = actualPrice + (actualPrice * (markupC / 100));
                                    }
                                }
                                else if (markupB < 0) {
                                    actualPrice = actualPrice + (actualPrice * (markupB / 100));
                                    if (markupC > 0) {
                                        actualPrice = actualPrice + (actualPrice * (markupC / 100));

                                    }
                                    else if (markupC < 0) {
                                        actualPrice = actualPrice + (actualPrice * (markupC / 100));
                                    }
                                }
                                else if (markupC > 0) {
                                    actualPrice = actualPrice + (selectedPrice * (markupC / 100));
                                }
                                else if (markupC < 0) {
                                    actualPrice = actualPrice + (selectedPrice * (markupC / 100));
                                }
                            }
                        }

                        // $(this).parent().next().html(getCurrencySymbol() + actualPrice);
                        $(this).parent().next().next().children(':first').html(getCurrencySymbol() + (actualPrice).toFixed(2));
                    }
                }
            });
        }


        function CheckRadioBtn(classname) {
            $('input:radio[class=' + classname + ']').attr('checked', true);
            if ($('input:radio[class=' + classname + ']').is(':checked')) {

                $('input:radio[class=' + classname + ']').next().removeClass('grayRadiantbtn').addClass('OrangeGradiantbtn');

                var name = $('input:radio[class=' + classname + ']').attr("name");

                $('input:radio[name=' + name + ']').each(function () {
                    if ($(this).is(':checked')) {
                        $(this).parent().parent().parent().parent().addClass('white_background');
                        $(this).parent().parent().parent().parent().css("background-color", "white");
                    } else {
                        $(this).next().removeClass('OrangeGradiantbtn').addClass('grayRadiantbtn');
                    }
                });
                var sID = $('input:radio[name=StockPaper]:checked').attr("dataId")

                $("#<%= hfSTockID.ClientID %>").val(sID);

                $('#<%=lblStock.ClientID %>').html($('input:radio[dataId=' + sID + ']').next().text());
                hideShowPriceColumns(classname);
                var RangedMatrix = $("#<%= hfRangedQty.ClientID %>").val();
                if (RangedMatrix == 1) {

                    var RangtxtBox = $("#RangedQtyTxtBox").val();
                    if (RangtxtBox == "") {

                    } else {
                        CalcutesTextBoxPrice();
                    }

                } else {
                    var selectedPaperType = null;
                    if (classname == 'rbStockType1') {
                        selectedPaperType = 'Matt';
                    }
                    else if (classname == 'rbStockType2') {
                        selectedPaperType = 'Glossy';
                    }
                    else if (classname == 'rbStockType3') {
                        selectedPaperType = 'PremiumMatt';
                    }
                    else if (classname == 'rbStockType4') {
                        selectedPaperType = '4';
                    }
                    else if (classname == 'rbStockType5') {
                        selectedPaperType = '5';
                    }
                    else if (classname == 'rbStockType6') {
                        selectedPaperType = '6';
                    }
                    else if (classname == 'rbStockType7') {
                        selectedPaperType = '7';
                    }
                    else if (classname == 'rbStockType8') {
                        selectedPaperType = '8';
                    }
                    else if (classname == 'rbStockType9') {
                        selectedPaperType = '9';
                    }
                    else if (classname == 'rbStockType10') {
                        selectedPaperType = '10';
                    }
                    else if (classname == 'rbStockType11') {
                        selectedPaperType = '11';
                    }
                    calculateProductItemPriceNew(selectedPaperType);
                    ChangeCheckBoxVlueWithQuantityselection();
                }
            }
        }



        function calculateProductItemPriceNew(selectedPaperType) {
            var IsCalculateVat = '<%=IsCalcVat %>';
            var cultureUi = '<%=IsCurrentCultureEng %>';
            var BMode = '<%=IsBrokerMde %>';
            var RangedMatrix = $("#<%= hfRangedQty.ClientID %>").val();
            if (RangedMatrix == 1) {
            }
            else {
                //var selectedRadio = $('.dropdownStockListClass');
                var qtyJsonData = null;
                var selectedVal = $(".dropdownQtyListClass")[0];
                var Qty = $("#<%= ddQtyOptn.ClientID %>");
                var radioQty = $(Qty).children("[selected]");
                var JsonA = $('.ClaassJsonObj')[$(selectedVal).find('option:selected').index()];

                var selectedPrice = null;
                var savings = 0;

                if ($(radioQty).length > 0 || $("#<%= ddQtyOptn.ClientID %>").length > 0) {
                    qtyJsonData = jQuery.parseJSON($(JsonA).attr("QtyJason"));
                    $('#<%=hfJVSQ.ClientID %>').val(qtyJsonData.Quantity);
                    selectedPaperType = selectedPaperType + '_PriceMatrixID_' + qtyJsonData.PriceMatrixID.toString();

                    if (qtyJsonData.IsDiscounted == false) {

                        var cultureUi = '<%=IsCurrentCultureEng %>';

                        if (cultureUi == 1) {
                            selectedPrice = parseFloat($('#' + selectedPaperType).attr("title")); // pick non discounted price
                        } else {

                            selectedPrice = $('#' + selectedPaperType).attr("title"); // pick non discounted price
                        }
                    } else {

                        var actualPrice = parseFloat($('#' + selectedPaperType).attr("title")); // pick non discounted price

                        if (BMode == 1) {
                            var ActualPriceWithMarkups = "";
                            var markupB = $("#<%= hfBrokerMarkup.ClientID %>").val();
                            var markupC = $("#<%= hfContactMarkup.ClientID %>").val();
                            if (markupB > 0) {
                                ActualPriceWithMarkups = actualPrice + (actualPrice * (markupB / 100));
                                if (markupC > 0) {
                                    ActualPriceWithMarkups = ActualPriceWithMarkups + (ActualPriceWithMarkups * (markupC / 100));
                                }
                                else if (markupC < 0) {
                                    ActualPriceWithMarkups = ActualPriceWithMarkups + (ActualPriceWithMarkups * (markupC / 100));
                                }
                            }
                            else if (markupB < 0) {
                                ActualPriceWithMarkups = actualPrice + (actualPrice * (markupB / 100));
                                if (markupC > 0) {
                                    ActualPriceWithMarkups = ActualPriceWithMarkups + (ActualPriceWithMarkups * (markupC / 100));
                                }
                                else if (markupC < 0) {
                                    ActualPriceWithMarkups = ActualPriceWithMarkups + (ActualPriceWithMarkups * (markupC / 100));
                                }
                            }
                            else if (markupC > 0) {
                                ActualPriceWithMarkups = actualPrice + (actualPrice * (markupC / 100));
                            }
                            else if (markupC < 0) {
                                ActualPriceWithMarkups = actualPrice + (actualPrice * (markupC / 100));
                            }
                            else {
                                ActualPriceWithMarkups = actualPrice;
                            }
                            var DiscountedActualPrice = ActualPriceWithMarkups - (ActualPriceWithMarkups * (qtyJsonData.DiscountRate / 100));
                            $("#<%= hfPriceExVat.ClientID %>").val(DiscountedActualPrice);
                        } else {
                            var DiscountedActualPrice = actualPrice - (actualPrice * (qtyJsonData.DiscountRate / 100));

                            $("#<%= hfPriceExVat.ClientID %>").val(DiscountedActualPrice);
                        }

                        //get the discounted Price for this search the span having disconted Price
                        selectedPrice = triverseForDiscountedPrice(selectedPaperType);

                        if (cultureUi == 1) {
                        } else {
                            selectedPrice = selectedPrice.replace(",", ".");
                            selectedPrice = parseFloat(selectedPrice);
                        }
                        //save ammount

                        savings = actualPrice - selectedPrice;
                    }

                    if (qtyJsonData.IsDiscounted == false) {
                        $("#<%= hfdiscountandVatApplied.ClientID %>").val(0);

                        if (BMode == 1) {
                            if (cultureUi == 1) {
                            } else {
                                selectedPrice = selectedPrice.replace(",", ".");
                                selectedPrice = parseFloat(selectedPrice);

                            }

                            var markupB = $("#<%= hfBrokerMarkup.ClientID %>").val();
                            var markupC = $("#<%= hfContactMarkup.ClientID %>").val();

                            if (markupB > 0) {
                                productTotal = selectedPrice + (selectedPrice * (markupB / 100));
                                if (markupC > 0) {
                                    productTotal = productTotal + (productTotal * (markupC / 100));
                                }
                                else if (markupC < 0) {
                                    productTotal = productTotal + (productTotal * (markupC / 100));
                                }
                            }
                            else if (markupB < 0) {
                                productTotal = selectedPrice + (selectedPrice * (markupB / 100));
                                if (markupC > 0) {
                                    productTotal = productTotal + (productTotal * (markupC / 100));
                                }
                                else if (markupC < 0) {
                                    productTotal = productTotal + (productTotal * (markupC / 100));
                                }
                            }
                            else if (markupC > 0) {
                                productTotal = selectedPrice + (selectedPrice * (markupC / 100));
                            }
                            else if (markupC < 0) {
                                productTotal = selectedPrice + (selectedPrice * (markupC / 100));
                            }
                            else {
                                productTotal = selectedPrice;
                            }
                            if (IsCalculateVat > 0) {

                                var ExVatPr = productTotal;
                                $("#<%= hfPriceExVat.ClientID %>").val(ExVatPr);
                                //productTotal = productTotal + ((productTotal * IsCalculateVat) / 100);
                            } else {
                                $("#<%= hfPriceExVat.ClientID %>").val(0);
                            }
                        }
                        else {


                            if (IsCalculateVat > 0) {

                                if (cultureUi == 1) {
                                } else {
                                    selectedPrice = selectedPrice.replace(",", ".");
                                    selectedPrice = parseFloat(selectedPrice);
                                }
                                var ExVatPr = selectedPrice;
                                $("#<%= hfPriceExVat.ClientID %>").val(ExVatPr);
                                // selectedPrice = selectedPrice + ((selectedPrice * IsCalculateVat) / 100);
                            } else {
                                $("#<%= hfPriceExVat.ClientID %>").val(0);
                            }
                            productTotal = selectedPrice; //global variables
                        }
                    } else {
                        $("#<%= hfdiscountandVatApplied.ClientID %>").val(1);

                        productTotal = selectedPrice;
                    }
                    prouctTotalSavings = savings; //global variables

                    this.displayTotalPrice(productTotal, goThroughAdOnPricesChkBoxesForTotal());
                }

            } //calculateProductItemPrice
        }
        ////



        function CalcutesTextBoxPrice() {
            var FirstObjJason = null;
            var LastObjJason = null;
            var CustMarkupPerc = null;
            var Qty = $("#<%= RangedQtyTxtBox.ClientID %>").val();

            if (isNaN(Qty) === true) {
                ShowPopup('Message', "Please enter numeric characters only.");
                return false;
            }
            else if (Qty == 0) {
                ShowPopup('Message', "Please enter correct quantity.");
                return false;
            }
            else if (Qty.toString().indexOf(".") != -1) {
                ShowPopup('Message', "Please enter correct quantity.");
                return false;
            }
            else if (Qty < 0) {
                ShowPopup('Message', "Please enter correct quantity.");
                return false;
            }

            if ($("#<%= hfStockLevel.ClientID %>").val() > 0) {

                if ($("#<%= hfBackOrderingAllowed.ClientID %>").val() == 0) {

                    var selectedQty = parseInt($("#<%= RangedQtyTxtBox.ClientID %>").val());
                    var StockL = parseInt($("#<%= hfStockLevel.ClientID %>").val());
                    if (selectedQty > StockL) {
                        ShowPopup('Message', "You cannot place order because the selected quantity is more than stock.");
                        return false;
                    }
                }


            }

            var RangedMatrix = $("#<%= hfRangedQty.ClientID %>").val();
            var count = $("#<%= hfCountOfMatrix.ClientID %>").val();

            if (RangedMatrix == 1) {
                var selectedPaperType = null;
                var StockIndex = $('input:radio[name=StockPaper]:checked').attr("class"); //  $(selectedStock).find('option:checked');


                if (StockIndex == 'rbStockType1') {
                    selectedPaperType = 'Matt';
                }
                else if (StockIndex == 'rbStockType2') {
                    selectedPaperType = 'Glossy';
                }
                else if (StockIndex == 'rbStockType3') {
                    selectedPaperType = 'PremiumMatt';
                }
                else if (StockIndex == 'rbStockType4') {
                    selectedPaperType = '4';
                }
                else if (StockIndex == 'rbStockType5') {
                    selectedPaperType = '5';
                }
                else if (StockIndex == 'rbStockType6') {
                    selectedPaperType = '6';
                }
                else if (StockIndex == 'rbStockType7') {
                    selectedPaperType = '7';
                }
                else if (StockIndex == 'rbStockType8') {
                    selectedPaperType = '8';
                }
                else if (StockIndex == 'rbStockType9') {
                    selectedPaperType = '9';
                }
                else if (StockIndex == 'rbStockType10') {
                    selectedPaperType = '10';
                }
                else if (StockIndex == 'rbStockType11') {
                    selectedPaperType = '11';
                }
                var BMode = '<%=IsBrokerMde %>';
                var selectedPrice = null;
                var savings = 0;
                var qtyJsonData = null;
                var ActualqtyJsonData = null;

                if (Qty != "") {
                    FirstObjJason = $('.ClaassJsonObj')[0];
                    for (var i = count - 1; i < count; i++) {
                        LastObjJason = $('.ClaassJsonObj')[i];
                    }
                    FirstObjJason = jQuery.parseJSON($(FirstObjJason).attr("QtyJason"));
                    LastObjJason = jQuery.parseJSON($(LastObjJason).attr("QtyJason"));
                    if (Qty < FirstObjJason.QtyRangeFrom || Qty > LastObjJason.QtyRangeTo) {
                        ShowPopup('Message', "Your quantity exceeds the normal amount, please contact us for a special price.");
                        return false;
                    } else {
                        var JsonA = $('.ClaassJsonObj');
                        for (var i = 0; i < count; i++) {
                            JsonA = $('.ClaassJsonObj')[i];
                            qtyJsonData = jQuery.parseJSON($(JsonA).attr("QtyJason"));
                            if (Qty >= qtyJsonData.QtyRangeFrom && Qty <= qtyJsonData.QtyRangeTo) {
                                $("#<%= hfIndexOfRangedQty.ClientID %>").val(i);
                                ActualqtyJsonData = qtyJsonData;
                                $('#<%=hfJVSQ.ClientID %>').val(Qty);
                                break;
                            }
                        }
                        if (ActualqtyJsonData != null) {
                            selectedPaperType = selectedPaperType + '_PriceMatrixID_' + ActualqtyJsonData.PriceMatrixID.toString();

                            if (ActualqtyJsonData.IsDiscounted == false) {
                                $("#<%= hfdiscountandVatApplied.ClientID %>").val(0);
                                selectedPrice = parseFloat($('#' + selectedPaperType).attr("title")); // pick non discounted price
                                if (BMode == 1) {

                                } else {
                                    var IsCalculateVat = '<%=IsCalcVat %>';
                                    if (IsCalculateVat > 0) {
                                        var ExVatPr = selectedPrice;
                                        $("#<%= hfPriceExVat.ClientID %>").val(ExVatPr);
                                        // selectedPrice = selectedPrice + ((selectedPrice * IsCalculateVat) / 100);
                                    } else {
                                        $("#<%= hfPriceExVat.ClientID %>").val(0);
                                    }
                                }
                            } else {
                                $("#<%= hfdiscountandVatApplied.ClientID %>").val(1);
                                var actualPrice = parseFloat($('#' + selectedPaperType).attr("title")); // pick non discounted price
                                if (BMode == 1) {
                                } else {
                                    var DiscountedActualPrice = actualPrice - (actualPrice * (qtyJsonData.DiscountRate / 100));
                                    $("#<%= hfPriceExVat.ClientID %>").val(DiscountedActualPrice);
                                }

                                //get the discounted Price for this search the span having disconted Price
                                selectedPrice = triverseForDiscountedPrice(selectedPaperType);

                                //save ammount
                                savings = actualPrice - selectedPrice;

                            }

                            if (BMode == 1) {
                                var markupB = $("#<%= hfBrokerMarkup.ClientID %>").val();
                                var markupC = $("#<%= hfContactMarkup.ClientID %>").val();

                                if (markupB > 0) {
                                    productTotal = selectedPrice + (selectedPrice * (markupB / 100));
                                    if (markupC > 0) {
                                        CustMarkupPerc = productTotal * (markupC / 100);
                                        productTotal = productTotal + CustMarkupPerc;
                                        $("#<%= hfQtyRangdSelectedPrice.ClientID %>").val(CustMarkupPerc);
                                        $("#<%= hfQtyRangedCustPer.ClientID %>").val(productTotal);

                                    }
                                    if (markupC < 0) {
                                        CustMarkupPerc = productTotal * (markupC / 100);
                                        productTotal = productTotal + CustMarkupPerc;
                                        $("#<%= hfQtyRangdSelectedPrice.ClientID %>").val(CustMarkupPerc);
                                        $("#<%= hfQtyRangedCustPer.ClientID %>").val(productTotal);

                                    }
                                    $("#<%= hfPriceExVat.ClientID %>").val(productTotal);
                                    productTotal = productTotal * Qty;
                                }
                                if (markupB < 0) {
                                    productTotal = selectedPrice + (selectedPrice * (markupB / 100));
                                    if (markupC > 0) {
                                        CustMarkupPerc = productTotal * (markupC / 100);
                                        productTotal = productTotal + CustMarkupPerc;
                                        $("#<%= hfQtyRangdSelectedPrice.ClientID %>").val(CustMarkupPerc);
                                        $("#<%= hfQtyRangedCustPer.ClientID %>").val(productTotal);

                                    }
                                    if (markupC < 0) {
                                        CustMarkupPerc = productTotal * (markupC / 100);
                                        productTotal = productTotal + CustMarkupPerc;
                                        $("#<%= hfQtyRangdSelectedPrice.ClientID %>").val(CustMarkupPerc);
                                        $("#<%= hfQtyRangedCustPer.ClientID %>").val(productTotal);

                                    }
                                    $("#<%= hfPriceExVat.ClientID %>").val(productTotal);
                                    productTotal = productTotal * Qty;
                                }
                                else if (markupC > 0) {
                                    CustMarkupPerc = selectedPrice * (markupC / 100);
                                    productTotal = selectedPrice + CustMarkupPerc;
                                    $("#<%= hfQtyRangdSelectedPrice.ClientID %>").val(CustMarkupPerc);
                                    $("#<%= hfQtyRangedCustPer.ClientID %>").val(productTotal);
                                    $("#<%= hfPriceExVat.ClientID %>").val(productTotal);
                                    productTotal = productTotal * Qty;
                                }
                                else if (markupC < 0) {
                                    CustMarkupPerc = selectedPrice * (markupC / 100);
                                    productTotal = selectedPrice + CustMarkupPerc;
                                    $("#<%= hfQtyRangdSelectedPrice.ClientID %>").val(CustMarkupPerc);
                                    $("#<%= hfQtyRangedCustPer.ClientID %>").val(productTotal);
                                    $("#<%= hfPriceExVat.ClientID %>").val(productTotal);
                                    productTotal = productTotal * Qty;
                                }
                                else {
                                    productTotal = selectedPrice;
                                    $("#<%= hfPriceExVat.ClientID %>").val(productTotal);
                                    productTotal = productTotal * Qty;
                                }
                        productTotal = productTotal;
                        if (ActualqtyJsonData.IsDiscounted == true) {
                            var DiscountedActualPrice = productTotal - (productTotal * (qtyJsonData.DiscountRate / 100));
                            $("#<%= hfPriceExVat.ClientID %>").val(DiscountedActualPrice);
                                } else {

                                }

                                var IsCalculateVat = '<%=IsCalcVat %>';
                                if (IsCalculateVat > 0) {
                                    //productTotal = productTotal + ((productTotal * IsCalculateVat) / 100);
                                }
                            }
                            else {
                                productTotal = selectedPrice;
                                productTotal = productTotal * Qty;
                            }
                            prouctTotalSavings = savings; //global variables

                            this.displayTotalPrice(productTotal, goThroughAdOnPricesChkBoxesForTotal());
                        }
                    }
                }
                else {
                    ShowPopup('Message', "Please enter quantity.");
                    return false;
                }
            }
        }


        function OnQtyDropDownChanged() {

            CheckRadioBtn($('input:radio[name=StockPaper]:checked').attr("class"));
            ChangeCheckBoxVlueWithQuantityselection();
        }

        function OnFocusOut() {

            $("#<%= CalCulatePriceBtn.ClientID %>").click();
        }

        function triverseForDiscountedPrice(selectedPaperType) {

            //var spanDiscounted = $($('#' + selectedPaperType).children()[1]).text().toString().replace(/^[$]/, '');
            var currSymbol = getCurrencySymbol();
            var spanDiscounted = $($('#' + selectedPaperType).children()[1]).text().toString().replace(/^[currSymbol]/, '');
            var culturee = '<%=IsCurrentCultureEng %>';

            if (culturee == 1) {
                spanDiscounted = spanDiscounted.replace(",", "");
                var arr = spanDiscounted.split(currSymbol);
                return parseFloat(arr[1]); //one will hve the numeric value
            } else {
                var arr = spanDiscounted.split(currSymbol);

                return arr[1]; //one will hve the numeric value
            }
            //


        } //triverseForDiscountedPrice

        function hideShowPriceColumns(selectedPaperType) {

            var mattClomuns = $(".mattPriceMatrixColumn");
            var glossyColumns = $(".glossyPriceMatrixColumn");
            var premimumMattColumns = $(".premiumMattPriceMatrixColumn");
            var forthColumn = $(".Stock4PriceMatrixColumn");
            var fifthColumn = $(".Stock5PriceMatrixColumn");
            var SixColumn = $(".Stock6PriceMatrixColumn");
            var SevenColumn = $(".Stock7PriceMatrixColumn");
            var eightColumn = $(".Stock8PriceMatrixColumn");
            var nineColumn = $(".Stock9PriceMatrixColumn");
            var tenthColumn = $(".Stock10PriceMatrixColumn");
            var elevenColumn = $(".Stock11PriceMatrixColumn");

            hideColumns(mattClomuns);
            hideColumns(glossyColumns);
            hideColumns(premimumMattColumns);
            hideColumns(forthColumn);
            hideColumns(fifthColumn);
            hideColumns(SixColumn);
            hideColumns(SevenColumn);
            hideColumns(eightColumn);
            hideColumns(nineColumn);
            hideColumns(tenthColumn);
            hideColumns(elevenColumn);

            if (selectedPaperType == 'rbStockType1') {
                showColumns(mattClomuns);
            }
            else if (selectedPaperType == 'rbStockType2') {
                showColumns(glossyColumns);
            }
            else if (selectedPaperType == 'rbStockType3') {
                showColumns(premimumMattColumns);
            }
            else if (selectedPaperType == 'rbStockType4') {
                showColumns(forthColumn);
            }
            else if (selectedPaperType == 'rbStockType5') {
                showColumns(fifthColumn);
            }
            else if (selectedPaperType == 'rbStockType6') {
                showColumns(SixColumn);
            }
            else if (selectedPaperType == 'rbStockType7') {
                showColumns(SevenColumn);
            }
            else if (selectedPaperType == 'rbStockType8') {
                showColumns(eightColumn);
            }
            else if (selectedPaperType == 'rbStockType9') {
                showColumns(nineColumn);
            }
            else if (selectedPaperType == 'rbStockType10') {
                showColumns(tenthColumn);
            }
            else if (selectedPaperType == 'rbStockType11') {
                showColumns(elevenColumn);
            }
        }

        function showColumns(columnsListObj) {

            $(columnsListObj).css("display", "block");
            //$(columnsListObj).css("visibility", "visible");
        }


        function hideColumns(columnsListObj) {
            $(columnsListObj).css("display", "none");
            //$(columnsListObj).css("visibility", "hidden");
        }

    </script>
</asp:Content>
