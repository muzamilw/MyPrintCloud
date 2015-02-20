<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ThemeSite.Master" AutoEventWireup="true"
    CodeBehind="CategoryProductListing.aspx.cs" Inherits="Web2Print.UI.CategoryProductListing" %>

<%@ Register Src="~/Controls/PinkCardsRelatedItems.ascx" TagName="PinkCardsRelatedItems"
    TagPrefix="uc12" %>
<%@ Register Src="~/Controls/BreadCrumbMenu.ascx" TagName="BreadCrumbMenu" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContents" runat="server">
    <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <link href="Styles/jquery-ui-1.10.0.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content_area">
        <div class="left_right_padding">
            <uc1:BreadCrumbMenu ID="BreadCumbMenuCategory" runat="server" />
            <div class="float_left_simple">
                <asp:Image ID="CategoryImg" CssClass="ImgCategory" runat="server" />
                <div class="float_left_simple LGBC rounded_corners Mleft10 SpecsBoxContainer">
                    <div class="white_background rounded_corners padding10">
                        <asp:Label ID="lblSpecx" runat="server" Text="Specification" CssClass="Fsize17"></asp:Label><br />
                        <div class="SpecDescBoxContainer">
                            <asp:Label ID="lblDesc1" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="clearBoth">
                    &nbsp;
                </div>
                <div class="LGBC rounded_corners Pad5px H4B">
                    <div class="Pad5px white_background">
                        <div class="float_left_simple lblProductBoxContainer">
                            Product
                        </div>
                        <div class="float_left_simple lblPriceBoxContainer">
                            Prices from (ex GST)
                        </div>
                        <div class="clearBoth">
                            &nbsp;
                        </div>
                        <div id="ProductLists" runat="server">
                        </div>
                    </div>
                </div>
            </div>
            <div id="PriceBox" class="float_left_simple LGBC rounded_corners CP_RightContainer"
                style="display: none;">
                <div class="SelectQtyContainer">
                    Select your quantity
                </div>
                <div class="white_background rounded_corners padding10">
                    <asp:Label ID="lblCatName" runat="server" CssClass="Fsize17"></asp:Label>
                    <div class="AvailInContaier">
                        <asp:Label ID="lblAvailablein" runat="server" Text="Available in:" CssClass="Fsize17"></asp:Label><br />
                        <select id="ddStockOptn" runat="server" clientidmode="Static" class="dropdown185 rounded_corners5 dropdownStockListClass spacer10pxtop">
                        </select>
                    </div>
                    <asp:Label ID="lblQtytxt" runat="server" Text="Quantity" CssClass="spanQtylbl checked_design custom_color float_left_simple 
                    clearRight ClearLeft"></asp:Label>
                    <asp:TextBox ID="RangedQtyTxtBox" runat="server" CssClass="dropdownBorderCS RangedQtyBox Mleft10 Fsize17"
                        ClientIDMode="Static" onblur="javascript:OnFocusOut();" Style="display: none;">
                    </asp:TextBox>
                    <asp:Button ID="CalCulatePriceBtn" OnClientClick="CalcutesTextBoxPrice(); return false;"
                        Text="Go" CssClass="goBtnRangedBox rounded_corners5 width35pix" runat="server"
                        Style="display: none;" />
                    <select id="ddQtyOptn" runat="server" clientidmode="Static" class="dropdownBorderCS dropdown156 rounded_corners5 template_designing float_right dropdownQtyListClass"
                        style="display: none;">
                    </select>
                    <div class="clearBoth">
                        &nbsp;
                    </div>
                    <asp:Label ID="lblOrderNow" runat="server" Text="Order now" CssClass="float_left_simple OrdrNowlb"
                        ></asp:Label>
                    <asp:Label ID="lblGrossTotal" runat="server" CssClass="spanGrossTotal">$0</asp:Label>
                    <div class="clearBoth">
                        &nbsp;
                    </div>
                    <asp:Label ID="lblTaxLabel" runat="server" CssClass="Fsize13 H4B float_right marginLeft">ex. VAT</asp:Label>
                    <div class="clearBoth">
                        &nbsp;
                    </div>
                    <asp:Button ID="btnAddToCart" runat="server" OnClientClick="return CheckSpelling();"
                        CssClass="OrderNowBtn rounded_corners5 float_right" Text="ADD TO CART" OnClick="btnOrderNow_Click" />
                    <div class="clearBoth">
                        &nbsp;
                    </div>
                    <div id="FixedQtylbl" class="confirm_design divtxtalgn MLR" style="display: none;">
                        <asp:Literal ID="ltrlQty" runat="server"></asp:Literal>
                    </div>
                    <br />
                    <div id="RangedQtylbls" style="display: none;">
                        <div id="ltrlQuantityDiv" class="Fsize13 divtxtalgn_CS width70pixel float_left_simple"
                            runat="server" style="margin-left: 9px;">
                            <asp:Literal ID="ltrlQuantity" runat="server"></asp:Literal>
                        </div>
                        <div class="clearBoth">
                            &nbsp;</div>
                        <div class="Fsize13 divtxtalgn float_left_simple Rangedlblx50 marginRight" style="margin-left: 10px;">
                            <asp:Literal ID="ltrlTo" runat="server"></asp:Literal></div>
                        <div class="Fsize13 divtxtalgn float_left_simple">
                            <asp:Literal ID="ltrlFrom" runat="server"></asp:Literal></div>
                        <div class="divtxtalgn_CS Width40Px float_right">
                            <asp:Label ID="LtrlCurrencySymbol" runat="server"></asp:Label>
                        </div>
                        <div class="clearBoth">
                            &nbsp;</div>
                    </div>
                    <div class="clearBoth">
                        &nbsp;</div>
                    <div id="tableCont" class="Mleft10 spacerbottom">
                    </div>
                </div>
            </div>
            <div class="clearBoth">
                &nbsp;
            </div>
            <div class="mightLikeContainer">
                <asp:Label ID="lblYoumightLke" runat="server" Visible="false" Text="You might also like:"></asp:Label>
            </div>
            <div>
                <uc12:PinkCardsRelatedItems ID="PinkcardrelateditemsWidget" runat="server" Visible="false" />
            </div>
        </div>
    </div>
    <asp:Label ID="lblProJasonString" runat="server" CssClass="ProJsonObj" Style="display: none;"></asp:Label>
    <asp:Label ID="lblStokJasonString" runat="server" CssClass="StokJsonObj" Style="display: none;"></asp:Label>
    <asp:Label ID="lblMtrixJasonString" runat="server" CssClass="MtrixJsonObj" Style="display: none;"></asp:Label>
    <asp:HiddenField ID="hfRangedQty" Value="0" runat="server" />
    <asp:HiddenField ID="hfCountOfMatrix" Value="0" runat="server" />
    <asp:HiddenField ID="hfBrokerMarkup" Value="0" runat="server" />
    <asp:HiddenField ID="hfContactMarkup" Value="0" runat="server" />
    <asp:HiddenField ID="HiddenItemDiscountRate" Value="0" runat="server" />
    <asp:HiddenField ID="hfQuantity" Value="0" runat="server" />
    <asp:HiddenField ID="hfJVSQ" Value="0" runat="server" />
    <asp:HiddenField ID="hfCurrencySymbol" Value="0" runat="server" />
    <asp:HiddenField ID="hfItemID" Value="0" runat="server" />
    <asp:HiddenField ID="hfSelectedStock" Value="0" runat="server" />
    <asp:HiddenField ID="hfPriceTotal" Value="0" runat="server" />
    <asp:HiddenField ID="hfQtyRangedCustPer" Value="0" runat="server" />
    <asp:HiddenField ID="hfQtyRangdSelectedPrice" Value="0" runat="server" />
    <script type="text/javascript">
        var PreviousItemID = 0;
        var CurrentItemId = 0;
        var StockOption = 0;
        var JsonProduct = $('.ProJsonObj')[0];
        var JsonStock = $('.StokJsonObj')[0];
        var JsonMtrix = $('.MtrixJsonObj')[0];
        var Products = jQuery.parseJSON($(JsonProduct).attr("ProductJason"));
        var Stocks = jQuery.parseJSON($(JsonStock).attr("StockJason"));
        var PriceMatrix = jQuery.parseJSON($(JsonMtrix).attr("PriceMatrixJason"));
        $(document).ready(function () {
            if ($("#RangedQtyTxtBox").val() != null && $("#RangedQtyTxtBox").val() != "") {
                CalcutesTextBoxPrice();
            }
        });

        $("li.ui-widget-content").mouseout(function () {

            $(this).children().next().next().css("display", "none");
        });

        $("li.ui-widget-content").mouseover(function () {

            $(this).children().next().next().css("display", "block");
        });

      
        $(function () {
            $("#selectable").selectable();
        });


        // fires when Ranged text box Focus Outs

        function OnFocusOut() {

           
        }


        function CheckSpelling() {
            
            var RangedMatrix = $("#<%= hfRangedQty.ClientID %>").val();
            
            if (RangedMatrix == 1) {
                var txtbox = $('#<%=RangedQtyTxtBox.ClientID %>').val();
                if (txtbox == '' || txtbox.replace(/^\s+/, '') == '') {
                    ShowPopup('Message', "please enter quantity to proceed.");
                    return false;
                }
            }
        }

        // calcultes the Price when quantity enters in text Box

        function CalcutesTextBoxPrice() {
            var FirstObjJason = null;
            var LastObjJason = null;
            var CustMarkupPerc = null;

            var Qty = $("#<%= RangedQtyTxtBox.ClientID %>").val();
            if (Qty == '') {
                return false;
            }
            if (isNaN(Qty) === true) {
                ShowPopup('Message', "Please enter numeric characters only.");
                return false;
            }
            if (Qty == 0) {
                ShowPopup('Message', "Please enter correct quantity.");
                return false;
            }
            if (Qty.toString().indexOf(".") != -1) {
                ShowPopup('Message', "Please enter correct quantity.");
                return false;
            }

            var CountOfMatrix = 0;
            var lstObjPMID = 0

            $.each(PriceMatrix, function (key, value) {
                if (value.ItemID == CurrentItemId) {
                    if (CountOfMatrix == 0) {

                        FirstObjJason = value;
                    }
                    lstObjPMID = value.PriceMatrixID;
                    CountOfMatrix = CountOfMatrix + 1;
                }
            });

            $.each(PriceMatrix, function (key, value) {

                if (value.PriceMatrixID == lstObjPMID) {
                    LastObjJason = value;
                    return;
                }

            });

            if ($('#<%=hfRangedQty.ClientID %>').val() == "1") {

                var selectedPrice = null;
                var savings = 0;
                var qtyJsonData = null;
                var ActualqtyJsonData = null;


                if (Qty != "") {
                    $('#<%=hfQuantity.ClientID %>').val(Qty);
                    if (Qty < FirstObjJason.QtyRangeFrom || Qty > LastObjJason.QtyRangeTo) {
                        ShowPopup('Message', "Your quantity exceeds the normal amount, please contact us for a special price.");
                        return false;
                    } else {
                        $.each(PriceMatrix, function (key, value) {
                            if (value.ItemID == CurrentItemId) {
                                if (Qty >= value.QtyRangeFrom && Qty <= value.QtyRangeTo) {
                                    ActualqtyJsonData = value;
                                    $('#<%=hfJVSQ.ClientID %>').val(Qty);
                                    return;
                                }
                            }
                        });
                        if (ActualqtyJsonData != null) {
                            if (ActualqtyJsonData.IsDiscounted == false) {

                                switch (StockOption) {
                                    case 1:
                                        selectedPrice = parseFloat(ActualqtyJsonData.PriceType1);
                                        break;
                                    case 2:
                                        selectedPrice = parseFloat(ActualqtyJsonData.PriceType2);
                                        break;
                                    case 3:
                                        selectedPrice = parseFloat(ActualqtyJsonData.PriceType3);
                                        break;
                                    case 4:
                                        selectedPrice = parseFloat(ActualqtyJsonData.PriceType4);
                                        break;
                                    case 5:
                                        selectedPrice = parseFloat(ActualqtyJsonData.PriceType5);
                                        break;
                                    case 6:
                                        selectedPrice = parseFloat(ActualqtyJsonData.PriceType6);
                                        break;
                                    case 7:
                                        selectedPrice = parseFloat(ActualqtyJsonData.PriceType7);
                                        break;
                                    case 8:
                                        selectedPrice = parseFloat(ActualqtyJsonData.PriceType8);
                                        break;
                                    case 9:
                                        selectedPrice = parseFloat(ActualqtyJsonData.PriceType9);
                                        break;
                                    case 10:
                                        selectedPrice = parseFloat(ActualqtyJsonData.PriceType10);
                                        break;
                                    case 11:
                                        selectedPrice = parseFloat(ActualqtyJsonData.PriceType11);
                                        break;
                                }
                            } else {
                                var actualPrice = 0;
                                switch (StockOption) {
                                    case 1:
                                        actualPrice = parseFloat(ActualqtyJsonData.PriceType1);
                                        break;
                                    case 2:
                                        actualPrice = parseFloat(ActualqtyJsonData.PriceType2);
                                        break;
                                    case 3:
                                        actualPrice = parseFloat(ActualqtyJsonData.PriceType3);
                                        break;
                                    case 4:
                                        actualPrice = parseFloat(ActualqtyJsonData.PriceType4);
                                        break;
                                    case 5:
                                        actualPrice = parseFloat(ActualqtyJsonData.PriceType5);
                                        break;
                                    case 6:
                                        actualPrice = parseFloat(ActualqtyJsonData.PriceType6);
                                        break;
                                    case 7:
                                        actualPrice = parseFloat(ActualqtyJsonData.PriceType7);
                                        break;
                                    case 8:
                                        actualPrice = parseFloat(ActualqtyJsonData.PriceType8);
                                        break;
                                    case 9:
                                        actualPrice = parseFloat(ActualqtyJsonData.PriceType9);
                                        break;
                                    case 10:
                                        actualPrice = parseFloat(ActualqtyJsonData.PriceType10);
                                        break;
                                    case 11:
                                        actualPrice = parseFloat(ActualqtyJsonData.PriceType11);
                                        break;
                                }

                                var discountPrecentage = $('#<%=HiddenItemDiscountRate.ClientID %>').val();

                                //get the discounted Price for this search the span having disconted Price
                                selectedPrice = actualPrice - (actualPrice * (discountPrecentage / 100));
                                //save ammount
                                savings = actualPrice - selectedPrice;
                            }
                            var BMode = '<%=IsBrokerMode %>';
                            if (BMode == 1) {
                                var markupB = ActualqtyJsonData.BrokerMarkup;
                                $("#<%= hfBrokerMarkup.ClientID %>").val(markupB);
                                var markupC = ActualqtyJsonData.ContactMarkup;
                                $("#<%= hfContactMarkup.ClientID %>").val(markupC);
                                if (markupB > 0) {
                                    productTotal = selectedPrice + (selectedPrice * (markupB / 100));
                                    if (markupC > 0) {
                                        CustMarkupPerc = productTotal * (markupC / 100);
                                        productTotal = productTotal + CustMarkupPerc;
                                        $("#<%= hfQtyRangdSelectedPrice.ClientID %>").val(CustMarkupPerc);
                                        $("#<%= hfQtyRangedCustPer.ClientID %>").val(productTotal);
                                    }
                                    productTotal = productTotal * Qty;
                                }
                                else if (markupC > 0) {
                                    CustMarkupPerc = selectedPrice * (markupC / 100);
                                    productTotal = selectedPrice + CustMarkupPerc;
                                    $("#<%= hfQtyRangdSelectedPrice.ClientID %>").val(CustMarkupPerc);
                                    $("#<%= hfQtyRangedCustPer.ClientID %>").val(productTotal);
                                    productTotal = productTotal * Qty;
                                }
                                else {
                                    productTotal = selectedPrice;
                                    productTotal = productTotal * Qty;
                                }
                                productTotal = productTotal;
                            }
                            else {
                                productTotal = selectedPrice;
                                productTotal = productTotal * Qty;
                            }
                            prouctTotalSavings = savings; //global variables
                            displayTotalPrice(productTotal);
                            return false;
                        }
                    }
                }
                else {
                    ShowPopup('Message', "Please enter quantity.");
                    return false;
                }
            }

        }

        // Set Grand Total

        function displayTotalPrice(productPrice) {
            $('#<%=hfPriceTotal.ClientID %>').val(productPrice);
            $('#<%=lblGrossTotal.ClientID %>').text(getCurrencySymbol() + (productPrice).toFixed(2));
        }

        // change the price matrix when stocks changes
        $("#ddStockOptn").change(function () {
            var ddSelectedIndex = $("#ddStockOptn option:selected").val();
            var ddSelectedOptionSeq = $("#ddStockOptn option:selected").attr("opSeqId");
            
            var seleSeq = 0;
            $.each(Stocks, function (key, value) {
                if (value.StockId == ddSelectedIndex & value.itemID == CurrentItemId & value.OptionSequence == ddSelectedOptionSeq) {
                    StockOption = value.OptionSequence;
                    $('#<%=hfSelectedStock.ClientID %>').val(value.StockId);

                }
            });
            if (StockOption > 0 & CurrentItemId > 0) {
                FillPriceMatrix(StockOption, CurrentItemId);
                CalcutesTextBoxPrice();
            }
        });


        // change the Total price when quantity changed from dropdown
        $("#ddQtyOptn").change(function () {

            var ddSelectedMatrixIndex = $("#ddQtyOptn option:selected").val();
            $('#<%=hfQuantity.ClientID %>').val($("#ddQtyOptn option:selected").text());
            OnQtyIndexChanged(ddSelectedMatrixIndex);
        });

        // Change the Total when Qty chaned from drop down

        function OnQtyIndexChanged(index) {
            var SelectedPriceRec = null;
            var selectedPrice = 0;
            var savings = 0;
            $.each(PriceMatrix, function (key, value) {
                if (value.PriceMatrixID == index) {
                    SelectedPriceRec = value;
                }
            });
            if (SelectedPriceRec.IsDiscounted == false) {
                switch (StockOption) {
                    case 1:
                        selectedPrice = parseFloat(SelectedPriceRec.PriceType1);
                        break;
                    case 2:
                        selectedPrice = parseFloat(SelectedPriceRec.PriceType2);
                        break;
                    case 3:
                        selectedPrice = parseFloat(SelectedPriceRec.PriceType3);
                        break;
                    case 4:
                        selectedPrice = parseFloat(SelectedPriceRec.PriceType4);
                        break;
                    case 5:
                        selectedPrice = parseFloat(SelectedPriceRec.PriceType5);
                        break;
                    case 6:
                        selectedPrice = parseFloat(SelectedPriceRec.PriceType6);
                        break;
                    case 7:
                        selectedPrice = parseFloat(SelectedPriceRec.PriceType7);
                        break;
                    case 8:
                        selectedPrice = parseFloat(SelectedPriceRec.PriceType8);
                        break;
                    case 9:
                        selectedPrice = parseFloat(SelectedPriceRec.PriceType9);
                        break;
                    case 10:
                        selectedPrice = parseFloat(SelectedPriceRec.PriceType10);
                        break;
                    case 11:
                        selectedPrice = parseFloat(SelectedPriceRec.PriceType11);
                        break;
                }
            } else {
                var actualPrice = 0;
                switch (StockOption) {
                    case 1:
                        actualPrice = parseFloat(SelectedPriceRec.PriceType1);
                        break;
                    case 2:
                        actualPrice = parseFloat(SelectedPriceRec.PriceType2);
                        break;
                    case 3:
                        actualPrice = parseFloat(SelectedPriceRec.PriceType3);
                        break;
                    case 4:
                        actualPrice = parseFloat(SelectedPriceRec.PriceType4);
                        break;
                    case 5:
                        actualPrice = parseFloat(SelectedPriceRec.PriceType5);
                        break;
                    case 6:
                        actualPrice = parseFloat(SelectedPriceRec.PriceType6);
                        break;
                    case 7:
                        actualPrice = parseFloat(SelectedPriceRec.PriceType7);
                        break;
                    case 8:
                        actualPrice = parseFloat(SelectedPriceRec.PriceType8);
                        break;
                    case 9:
                        actualPrice = parseFloat(SelectedPriceRec.PriceType9);
                        break;
                    case 10:
                        actualPrice = parseFloat(SelectedPriceRec.PriceType10);
                        break;
                    case 11:
                        actualPrice = parseFloat(SelectedPriceRec.PriceType11);
                        break;
                }

                var discountPrecentage = $('#<%=HiddenItemDiscountRate.ClientID %>').val();

                //get the discounted Price for this search the span having disconted Price
                selectedPrice = actualPrice - (actualPrice * (discountPrecentage / 100));
                //save ammount
                savings = actualPrice - selectedPrice;

            }
            var BMode = '<%=IsBrokerMode %>';
            if (BMode == 1) {
                var markupB = SelectedPriceRec.BrokerMarkup;
                $("#<%= hfBrokerMarkup.ClientID %>").val(markupB);
                var markupC = SelectedPriceRec.ContactMarkup;
                $("#<%= hfContactMarkup.ClientID %>").val(markupC);
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
            }
            else {
                productTotal = selectedPrice;

            }
            prouctTotalSavings = savings; //global variables

            displayTotalPrice(productTotal);
        }



        // Fill Price Matrix
        function FillPriceMatrix(Option, CItemID) {
            var MatrixHtml = "";
            var TableIndex = 0;
            var SelectedQtyID = 0;
            var Symbol = $('#<%=hfCurrencySymbol.ClientID %>').val();
            if ($('#<%=hfRangedQty.ClientID %>').val() != "1") {
                $("#ddQtyOptn option").each(function () {
                    $(this).remove();
                });
            }

            $.each(PriceMatrix, function (key, value) {
                if (value.ItemID == CItemID) {
                    if ($('#<%=hfRangedQty.ClientID %>').val() == "1") {
                        switch (Option) {
                            case 1:

                                if (MatrixHtml == "") {
                                    if (TableIndex % 2 == 0) {
                                        MatrixHtml = GenerateHtmlForRangedPriceMatrix(value.QtyRangeTo, value.QtyRangeFrom, value.PriceType1, value.IsDiscounted, 1, value.BrokerMarkup, value.ContactMarkup);
                                    } else {
                                        MatrixHtml = GenerateHtmlForRangedPriceMatrix(value.QtyRangeTo, value.QtyRangeFrom, value.PriceType1, value.IsDiscounted, 0, value.BrokerMarkup, value.ContactMarkup);
                                    }
                                    TableIndex = TableIndex + 1;

                                } else {
                                    if (TableIndex % 2 == 0) {
                                        MatrixHtml += GenerateHtmlForRangedPriceMatrix(value.QtyRangeTo, value.QtyRangeFrom, value.PriceType1, value.IsDiscounted, 1, value.BrokerMarkup, value.ContactMarkup);
                                    } else {
                                        MatrixHtml += GenerateHtmlForRangedPriceMatrix(value.QtyRangeTo, value.QtyRangeFrom, value.PriceType1, value.IsDiscounted, 0, value.BrokerMarkup, value.ContactMarkup);
                                    }
                                    TableIndex = TableIndex + 1;
                                }
                                break;
                            case 2:
                                if (MatrixHtml == "") {
                                    if (TableIndex % 2 == 0) {
                                        MatrixHtml = GenerateHtmlForRangedPriceMatrix(value.QtyRangeTo, value.QtyRangeFrom, value.PriceType2, value.IsDiscounted, 1, value.BrokerMarkup, value.ContactMarkup);
                                    } else {
                                        MatrixHtml = GenerateHtmlForRangedPriceMatrix(value.QtyRangeTo, value.QtyRangeFrom, value.PriceType2, value.IsDiscounted, 0, value.BrokerMarkup, value.ContactMarkup);
                                    }
                                    TableIndex = TableIndex + 1;

                                } else {
                                    if (TableIndex % 2 == 0) {
                                        MatrixHtml += GenerateHtmlForRangedPriceMatrix(value.QtyRangeTo, value.QtyRangeFrom, value.PriceType2, value.IsDiscounted, 1, value.BrokerMarkup, value.ContactMarkup);
                                    } else {
                                        MatrixHtml += GenerateHtmlForRangedPriceMatrix(value.QtyRangeTo, value.QtyRangeFrom, value.PriceType2, value.IsDiscounted, 0, value.BrokerMarkup, value.ContactMarkup);
                                    }
                                    TableIndex = TableIndex + 1;
                                }
                                break;
                            case 3:
                                if (MatrixHtml == "") {
                                    if (TableIndex % 2 == 0) {
                                        MatrixHtml = GenerateHtmlForRangedPriceMatrix(value.QtyRangeTo, value.QtyRangeFrom, value.PriceType3, value.IsDiscounted, 1, value.BrokerMarkup, value.ContactMarkup);
                                    } else {
                                        MatrixHtml = GenerateHtmlForRangedPriceMatrix(value.QtyRangeTo, value.QtyRangeFrom, value.PriceType3, value.IsDiscounted, 0, value.BrokerMarkup, value.ContactMarkup);
                                    }
                                    TableIndex = TableIndex + 1;

                                } else {
                                    if (TableIndex % 2 == 0) {
                                        MatrixHtml += GenerateHtmlForRangedPriceMatrix(value.QtyRangeTo, value.QtyRangeFrom, value.PriceType3, value.IsDiscounted, 1, value.BrokerMarkup, value.ContactMarkup);
                                    } else {
                                        MatrixHtml += GenerateHtmlForRangedPriceMatrix(value.QtyRangeTo, value.QtyRangeFrom, value.PriceType3, value.IsDiscounted, 0, value.BrokerMarkup, value.ContactMarkup);
                                    }
                                    TableIndex = TableIndex + 1;
                                }
                                break;
                            case 4:
                                if (MatrixHtml == "") {
                                    if (TableIndex % 2 == 0) {
                                        MatrixHtml = GenerateHtmlForRangedPriceMatrix(value.QtyRangeTo, value.QtyRangeFrom, value.PriceType4, value.IsDiscounted, 1, value.BrokerMarkup, value.ContactMarkup);
                                    } else {
                                        MatrixHtml = GenerateHtmlForRangedPriceMatrix(value.QtyRangeTo, value.QtyRangeFrom, value.PriceType4, value.IsDiscounted, 0, value.BrokerMarkup, value.ContactMarkup);
                                    }
                                    TableIndex = TableIndex + 1;

                                } else {
                                    if (TableIndex % 2 == 0) {
                                        MatrixHtml += GenerateHtmlForRangedPriceMatrix(value.QtyRangeTo, value.QtyRangeFrom, value.PriceType4, value.IsDiscounted, 1, value.BrokerMarkup, value.ContactMarkup);
                                    } else {
                                        MatrixHtml += GenerateHtmlForRangedPriceMatrix(value.QtyRangeTo, value.QtyRangeFrom, value.PriceType4, value.IsDiscounted, 0, value.BrokerMarkup, value.ContactMarkup);
                                    }
                                    TableIndex = TableIndex + 1;
                                }
                                break;
                            case 5:
                                if (MatrixHtml == "") {
                                    if (TableIndex % 2 == 0) {
                                        MatrixHtml = GenerateHtmlForRangedPriceMatrix(value.QtyRangeTo, value.QtyRangeFrom, value.PriceType5, value.IsDiscounted, 1, value.BrokerMarkup, value.ContactMarkup);
                                    } else {
                                        MatrixHtml = GenerateHtmlForRangedPriceMatrix(value.QtyRangeTo, value.QtyRangeFrom, value.PriceType5, value.IsDiscounted, 0, value.BrokerMarkup, value.ContactMarkup);
                                    }
                                    TableIndex = TableIndex + 1;

                                } else {
                                    if (TableIndex % 2 == 0) {
                                        MatrixHtml += GenerateHtmlForRangedPriceMatrix(value.QtyRangeTo, value.QtyRangeFrom, value.PriceType5, value.IsDiscounted, 1, value.BrokerMarkup, value.ContactMarkup);
                                    } else {
                                        MatrixHtml += GenerateHtmlForRangedPriceMatrix(value.QtyRangeTo, value.QtyRangeFrom, value.PriceType5, value.IsDiscounted, 0, value.BrokerMarkup, value.ContactMarkup);
                                    }
                                    TableIndex = TableIndex + 1;
                                }
                                break;
                            case 6:
                                if (MatrixHtml == "") {
                                    if (TableIndex % 2 == 0) {
                                        MatrixHtml = GenerateHtmlForRangedPriceMatrix(value.QtyRangeTo, value.QtyRangeFrom, value.PriceType6, value.IsDiscounted, 1, value.BrokerMarkup, value.ContactMarkup);
                                    } else {
                                        MatrixHtml = GenerateHtmlForRangedPriceMatrix(value.QtyRangeTo, value.QtyRangeFrom, value.PriceType6, value.IsDiscounted, 0, value.BrokerMarkup, value.ContactMarkup);
                                    }
                                    TableIndex = TableIndex + 1;

                                } else {
                                    if (TableIndex % 2 == 0) {
                                        MatrixHtml += GenerateHtmlForRangedPriceMatrix(value.QtyRangeTo, value.QtyRangeFrom, value.PriceType6, value.IsDiscounted, 1, value.BrokerMarkup, value.ContactMarkup);
                                    } else {
                                        MatrixHtml += GenerateHtmlForRangedPriceMatrix(value.QtyRangeTo, value.QtyRangeFrom, value.PriceType6, value.IsDiscounted, 0, value.BrokerMarkup, value.ContactMarkup);
                                    }
                                    TableIndex = TableIndex + 1;
                                }
                                break;
                            case 7:
                                if (MatrixHtml == "") {
                                    if (TableIndex % 2 == 0) {
                                        MatrixHtml = GenerateHtmlForRangedPriceMatrix(value.QtyRangeTo, value.QtyRangeFrom, value.PriceType7, value.IsDiscounted, 1, value.BrokerMarkup, value.ContactMarkup);
                                    } else {
                                        MatrixHtml = GenerateHtmlForRangedPriceMatrix(value.QtyRangeTo, value.QtyRangeFrom, value.PriceType7, value.IsDiscounted, 0, value.BrokerMarkup, value.ContactMarkup);
                                    }
                                    TableIndex = TableIndex + 1;

                                } else {
                                    if (TableIndex % 2 == 0) {
                                        MatrixHtml += GenerateHtmlForRangedPriceMatrix(value.QtyRangeTo, value.QtyRangeFrom, value.PriceType7, value.IsDiscounted, 1, value.BrokerMarkup, value.ContactMarkup);
                                    } else {
                                        MatrixHtml += GenerateHtmlForRangedPriceMatrix(value.QtyRangeTo, value.QtyRangeFrom, value.PriceType7, value.IsDiscounted, 0, value.BrokerMarkup, value.ContactMarkup);
                                    }
                                    TableIndex = TableIndex + 1;
                                }
                                break;
                            case 8:
                                if (MatrixHtml == "") {
                                    if (TableIndex % 2 == 0) {
                                        MatrixHtml = GenerateHtmlForRangedPriceMatrix(value.QtyRangeTo, value.QtyRangeFrom, value.PriceType8, value.IsDiscounted, 1, value.BrokerMarkup, value.ContactMarkup);
                                    } else {
                                        MatrixHtml = GenerateHtmlForRangedPriceMatrix(value.QtyRangeTo, value.QtyRangeFrom, value.PriceType8, value.IsDiscounted, 0, value.BrokerMarkup, value.ContactMarkup);
                                    }
                                    TableIndex = TableIndex + 1;

                                } else {
                                    if (TableIndex % 2 == 0) {
                                        MatrixHtml += GenerateHtmlForRangedPriceMatrix(value.QtyRangeTo, value.QtyRangeFrom, value.PriceType8, value.IsDiscounted, 1, value.BrokerMarkup, value.ContactMarkup);
                                    } else {
                                        MatrixHtml += GenerateHtmlForRangedPriceMatrix(value.QtyRangeTo, value.QtyRangeFrom, value.PriceType8, value.IsDiscounted, 0, value.BrokerMarkup, value.ContactMarkup);
                                    }
                                    TableIndex = TableIndex + 1;
                                }
                                break;
                            case 9:
                                if (MatrixHtml == "") {
                                    if (TableIndex % 2 == 0) {
                                        MatrixHtml = GenerateHtmlForRangedPriceMatrix(value.QtyRangeTo, value.QtyRangeFrom, value.PriceType9, value.IsDiscounted, 1, value.BrokerMarkup, value.ContactMarkup);
                                    } else {
                                        MatrixHtml = GenerateHtmlForRangedPriceMatrix(value.QtyRangeTo, value.QtyRangeFrom, value.PriceType9, value.IsDiscounted, 0, value.BrokerMarkup, value.ContactMarkup);
                                    }
                                    TableIndex = TableIndex + 1;

                                } else {
                                    if (TableIndex % 2 == 0) {
                                        MatrixHtml += GenerateHtmlForRangedPriceMatrix(value.QtyRangeTo, value.QtyRangeFrom, value.PriceType9, value.IsDiscounted, 1, value.BrokerMarkup, value.ContactMarkup);
                                    } else {
                                        MatrixHtml += GenerateHtmlForRangedPriceMatrix(value.QtyRangeTo, value.QtyRangeFrom, value.PriceType9, value.IsDiscounted, 0, value.BrokerMarkup, value.ContactMarkup);
                                    }
                                    TableIndex = TableIndex + 1;
                                }
                                break;
                            case 10:
                                if (MatrixHtml == "") {
                                    if (TableIndex % 2 == 0) {
                                        MatrixHtml = GenerateHtmlForRangedPriceMatrix(value.QtyRangeTo, value.QtyRangeFrom, value.PriceType10, value.IsDiscounted, 1, value.BrokerMarkup, value.ContactMarkup);
                                    } else {
                                        MatrixHtml = GenerateHtmlForRangedPriceMatrix(value.QtyRangeTo, value.QtyRangeFrom, value.PriceType10, value.IsDiscounted, 0, value.BrokerMarkup, value.ContactMarkup);
                                    }
                                    TableIndex = TableIndex + 1;

                                } else {
                                    if (TableIndex % 2 == 0) {
                                        MatrixHtml += GenerateHtmlForRangedPriceMatrix(value.QtyRangeTo, value.QtyRangeFrom, value.PriceType10, value.IsDiscounted, 1, value.BrokerMarkup, value.ContactMarkup);
                                    } else {
                                        MatrixHtml += GenerateHtmlForRangedPriceMatrix(value.QtyRangeTo, value.QtyRangeFrom, value.PriceType10, value.IsDiscounted, 0, value.BrokerMarkup, value.ContactMarkup);
                                    }
                                    TableIndex = TableIndex + 1;
                                }
                                break;
                            case 11:
                                if (MatrixHtml == "") {
                                    if (TableIndex % 2 == 0) {
                                        MatrixHtml = GenerateHtmlForRangedPriceMatrix(value.QtyRangeTo, value.QtyRangeFrom, value.PriceType11, value.IsDiscounted, 1, value.BrokerMarkup, value.ContactMarkup);
                                    } else {
                                        MatrixHtml = GenerateHtmlForRangedPriceMatrix(value.QtyRangeTo, value.QtyRangeFrom, value.PriceType11, value.IsDiscounted, 0, value.BrokerMarkup, value.ContactMarkup);
                                    }
                                    TableIndex = TableIndex + 1;

                                } else {
                                    if (TableIndex % 2 == 0) {
                                        MatrixHtml += GenerateHtmlForRangedPriceMatrix(value.QtyRangeTo, value.QtyRangeFrom, value.PriceType11, value.IsDiscounted, 1, value.BrokerMarkup, value.ContactMarkup);
                                    } else {
                                        MatrixHtml += GenerateHtmlForRangedPriceMatrix(value.QtyRangeTo, value.QtyRangeFrom, value.PriceType11, value.IsDiscounted, 0, value.BrokerMarkup, value.ContactMarkup);
                                    }
                                    TableIndex = TableIndex + 1;
                                }
                                break;
                        }
                    } else {
                        $("#ddQtyOptn").append("<option value='" + value.PriceMatrixID + "'>" + value.Quantity + "</option>");
                        if (SelectedQtyID == 0) {
                            SelectedQtyID = value.PriceMatrixID;
                            $('#<%=hfQuantity.ClientID %>').val(value.Quantity);
                        }
                        switch (Option) {

                            case 1:
                                if (MatrixHtml == "") {
                                    if (TableIndex % 2 == 0) {
                                        MatrixHtml = GenerateHtmlForFixedPriceMatrix(value.Quantity, value.PriceType1, value.IsDiscounted, 1, value.BrokerMarkup, value.ContactMarkup);
                                    } else {
                                        MatrixHtml = GenerateHtmlForFixedPriceMatrix(value.Quantity, value.PriceType1, value.IsDiscounted, 0, value.BrokerMarkup, value.ContactMarkup);
                                    }
                                    TableIndex = TableIndex + 1;

                                } else {
                                    if (TableIndex % 2 == 0) {
                                        MatrixHtml += GenerateHtmlForFixedPriceMatrix(value.Quantity, value.PriceType1, value.IsDiscounted, 1, value.BrokerMarkup, value.ContactMarkup);
                                    } else {
                                        MatrixHtml += GenerateHtmlForFixedPriceMatrix(value.Quantity, value.PriceType1, value.IsDiscounted, 0, value.BrokerMarkup, value.ContactMarkup);
                                    }
                                    TableIndex = TableIndex + 1;
                                }
                                break;
                            case 2:
                                if (MatrixHtml == "") {
                                    if (TableIndex % 2 == 0) {
                                        MatrixHtml = GenerateHtmlForFixedPriceMatrix(value.Quantity, value.PriceType2, value.IsDiscounted, 1, value.BrokerMarkup, value.ContactMarkup);
                                    } else {
                                        MatrixHtml = GenerateHtmlForFixedPriceMatrix(value.Quantity, value.PriceType2, value.IsDiscounted, 0, value.BrokerMarkup, value.ContactMarkup);
                                    }
                                    TableIndex = TableIndex + 1;

                                } else {
                                    if (TableIndex % 2 == 0) {
                                        MatrixHtml += GenerateHtmlForFixedPriceMatrix(value.Quantity, value.PriceType2, value.IsDiscounted, 1, value.BrokerMarkup, value.ContactMarkup);
                                    } else {
                                        MatrixHtml += GenerateHtmlForFixedPriceMatrix(value.Quantity, value.PriceType2, value.IsDiscounted, 0, value.BrokerMarkup, value.ContactMarkup);
                                    }
                                    TableIndex = TableIndex + 1;
                                }
                                break;
                            case 3:
                                if (MatrixHtml == "") {
                                    if (TableIndex % 2 == 0) {
                                        MatrixHtml = GenerateHtmlForFixedPriceMatrix(value.Quantity, value.PriceType3, value.IsDiscounted, 1, value.BrokerMarkup, value.ContactMarkup);
                                    } else {
                                        MatrixHtml = GenerateHtmlForFixedPriceMatrix(value.Quantity, value.PriceType3, value.IsDiscounted, 0, value.BrokerMarkup, value.ContactMarkup);
                                    }
                                    TableIndex = TableIndex + 1;

                                } else {
                                    if (TableIndex % 2 == 0) {
                                        MatrixHtml += GenerateHtmlForFixedPriceMatrix(value.Quantity, value.PriceType3, value.IsDiscounted, 1, value.BrokerMarkup, value.ContactMarkup);
                                    } else {
                                        MatrixHtml += GenerateHtmlForFixedPriceMatrix(value.Quantity, value.PriceType3, value.IsDiscounted, 0, value.BrokerMarkup, value.ContactMarkup);
                                    }
                                    TableIndex = TableIndex + 1;
                                }
                                break;
                            case 4:
                                if (MatrixHtml == "") {
                                    if (TableIndex % 2 == 0) {
                                        MatrixHtml = GenerateHtmlForFixedPriceMatrix(value.Quantity, value.PriceType4, value.IsDiscounted, 1, value.BrokerMarkup, value.ContactMarkup);
                                    } else {
                                        MatrixHtml = GenerateHtmlForFixedPriceMatrix(value.Quantity, value.PriceType4, value.IsDiscounted, 0, value.BrokerMarkup, value.ContactMarkup);
                                    }
                                    TableIndex = TableIndex + 1;

                                } else {
                                    if (TableIndex % 2 == 0) {
                                        MatrixHtml += GenerateHtmlForFixedPriceMatrix(value.Quantity, value.PriceType4, value.IsDiscounted, 1, value.BrokerMarkup, value.ContactMarkup);
                                    } else {
                                        MatrixHtml += GenerateHtmlForFixedPriceMatrix(value.Quantity, value.PriceType4, value.IsDiscounted, 0, value.BrokerMarkup, value.ContactMarkup);
                                    }
                                    TableIndex = TableIndex + 1;
                                }
                                break;
                            case 5:
                                if (MatrixHtml == "") {
                                    if (TableIndex % 2 == 0) {
                                        MatrixHtml = GenerateHtmlForFixedPriceMatrix(value.Quantity, value.PriceType5, value.IsDiscounted, 1, value.BrokerMarkup, value.ContactMarkup);
                                    } else {
                                        MatrixHtml = GenerateHtmlForFixedPriceMatrix(value.Quantity, value.PriceType5, value.IsDiscounted, 0, value.BrokerMarkup, value.ContactMarkup);
                                    }
                                    TableIndex = TableIndex + 1;

                                } else {
                                    if (TableIndex % 2 == 0) {
                                        MatrixHtml += GenerateHtmlForFixedPriceMatrix(value.Quantity, value.PriceType5, value.IsDiscounted, 1, value.BrokerMarkup, value.ContactMarkup);
                                    } else {
                                        MatrixHtml += GenerateHtmlForFixedPriceMatrix(value.Quantity, value.PriceType5, value.IsDiscounted, 0, value.BrokerMarkup, value.ContactMarkup);
                                    }
                                    TableIndex = TableIndex + 1;
                                }
                                break;
                            case 6:
                                if (MatrixHtml == "") {
                                    if (TableIndex % 2 == 0) {
                                        MatrixHtml = GenerateHtmlForFixedPriceMatrix(value.Quantity, value.PriceType6, value.IsDiscounted, 1, value.BrokerMarkup, value.ContactMarkup);
                                    } else {
                                        MatrixHtml = GenerateHtmlForFixedPriceMatrix(value.Quantity, value.PriceType6, value.IsDiscounted, 0, value.BrokerMarkup, value.ContactMarkup);
                                    }
                                    TableIndex = TableIndex + 1;

                                } else {
                                    if (TableIndex % 2 == 0) {
                                        MatrixHtml += GenerateHtmlForFixedPriceMatrix(value.Quantity, value.PriceType6, value.IsDiscounted, 1, value.BrokerMarkup, value.ContactMarkup);
                                    } else {
                                        MatrixHtml += GenerateHtmlForFixedPriceMatrix(value.Quantity, value.PriceType6, value.IsDiscounted, 0, value.BrokerMarkup, value.ContactMarkup);
                                    }
                                    TableIndex = TableIndex + 1;
                                }
                                break;
                            case 7:
                                if (MatrixHtml == "") {
                                    if (TableIndex % 2 == 0) {
                                        MatrixHtml = GenerateHtmlForFixedPriceMatrix(value.Quantity, value.PriceType7, value.IsDiscounted, 1, value.BrokerMarkup, value.ContactMarkup);
                                    } else {
                                        MatrixHtml = GenerateHtmlForFixedPriceMatrix(value.Quantity, value.PriceType7, value.IsDiscounted, 0, value.BrokerMarkup, value.ContactMarkup);
                                    }
                                    TableIndex = TableIndex + 1;

                                } else {
                                    if (TableIndex % 2 == 0) {
                                        MatrixHtml += GenerateHtmlForFixedPriceMatrix(value.Quantity, value.PriceType7, value.IsDiscounted, 1, value.BrokerMarkup, value.ContactMarkup);
                                    } else {
                                        MatrixHtml += GenerateHtmlForFixedPriceMatrix(value.Quantity, value.PriceType7, value.IsDiscounted, 0, value.BrokerMarkup, value.ContactMarkup);
                                    }
                                    TableIndex = TableIndex + 1;
                                }
                                break;
                            case 8:
                                if (MatrixHtml == "") {
                                    if (TableIndex % 2 == 0) {
                                        MatrixHtml = GenerateHtmlForFixedPriceMatrix(value.Quantity, value.PriceType8, value.IsDiscounted, 1, value.BrokerMarkup, value.ContactMarkup);
                                    } else {
                                        MatrixHtml = GenerateHtmlForFixedPriceMatrix(value.Quantity, value.PriceType8, value.IsDiscounted, 0, value.BrokerMarkup, value.ContactMarkup);
                                    }
                                    TableIndex = TableIndex + 1;

                                } else {
                                    if (TableIndex % 2 == 0) {
                                        MatrixHtml += GenerateHtmlForFixedPriceMatrix(value.Quantity, value.PriceType8, value.IsDiscounted, 1, value.BrokerMarkup, value.ContactMarkup);
                                    } else {
                                        MatrixHtml += GenerateHtmlForFixedPriceMatrix(value.Quantity, value.PriceType8, value.IsDiscounted, 0, value.BrokerMarkup, value.ContactMarkup);
                                    }
                                    TableIndex = TableIndex + 1;
                                }
                                break;
                            case 9:
                                if (MatrixHtml == "") {
                                    if (TableIndex % 2 == 0) {
                                        MatrixHtml = GenerateHtmlForFixedPriceMatrix(value.Quantity, value.PriceType9, value.IsDiscounted, 1, value.BrokerMarkup, value.ContactMarkup);
                                    } else {
                                        MatrixHtml = GenerateHtmlForFixedPriceMatrix(value.Quantity, value.PriceType9, value.IsDiscounted, 0, value.BrokerMarkup, value.ContactMarkup);
                                    }
                                    TableIndex = TableIndex + 1;

                                } else {
                                    if (TableIndex % 2 == 0) {
                                        MatrixHtml += GenerateHtmlForFixedPriceMatrix(value.Quantity, value.PriceType9, value.IsDiscounted, 1, value.BrokerMarkup, value.ContactMarkup);
                                    } else {
                                        MatrixHtml += GenerateHtmlForFixedPriceMatrix(value.Quantity, value.PriceType9, value.IsDiscounted, 0, value.BrokerMarkup, value.ContactMarkup);
                                    }
                                    TableIndex = TableIndex + 1;
                                }
                                break;
                            case 10:
                                if (MatrixHtml == "") {
                                    if (TableIndex % 2 == 0) {
                                        MatrixHtml = GenerateHtmlForFixedPriceMatrix(value.Quantity, value.PriceType10, value.IsDiscounted, 1, value.BrokerMarkup, value.ContactMarkup);
                                    } else {
                                        MatrixHtml = GenerateHtmlForFixedPriceMatrix(value.Quantity, value.PriceType10, value.IsDiscounted, 0, value.BrokerMarkup, value.ContactMarkup);
                                    }
                                    TableIndex = TableIndex + 1;

                                } else {
                                    if (TableIndex % 2 == 0) {
                                        MatrixHtml += GenerateHtmlForFixedPriceMatrix(value.Quantity, value.PriceType10, value.IsDiscounted, 1, value.BrokerMarkup, value.ContactMarkup);
                                    } else {
                                        MatrixHtml += GenerateHtmlForFixedPriceMatrix(value.Quantity, value.PriceType10, value.IsDiscounted, 0, value.BrokerMarkup, value.ContactMarkup);
                                    }
                                    TableIndex = TableIndex + 1;
                                }
                                break;
                            case 11:
                                if (MatrixHtml == "") {
                                    if (TableIndex % 2 == 0) {
                                        MatrixHtml = GenerateHtmlForFixedPriceMatrix(value.Quantity, value.PriceType11, value.IsDiscounted, 1, value.BrokerMarkup, value.ContactMarkup);
                                    } else {
                                        MatrixHtml = GenerateHtmlForFixedPriceMatrix(value.Quantity, value.PriceType11, value.IsDiscounted, 0, value.BrokerMarkup, value.ContactMarkup);
                                    }
                                    TableIndex = TableIndex + 1;

                                } else {
                                    if (TableIndex % 2 == 0) {
                                        MatrixHtml += GenerateHtmlForFixedPriceMatrix(value.Quantity, value.PriceType11, value.IsDiscounted, 1, value.BrokerMarkup, value.ContactMarkup);
                                    } else {
                                        MatrixHtml += GenerateHtmlForFixedPriceMatrix(value.Quantity, value.PriceType11, value.IsDiscounted, 1, value.BrokerMarkup, value.ContactMarkup);
                                    }
                                    TableIndex = TableIndex + 1;
                                }
                                break;
                        }
                    }
                }
            });
            if ($('#<%=hfRangedQty.ClientID %>').val() == "1") {
                $("#FixedQtylbl").css("display", "none");
                $("#RangedQtylbls").css("display", "block");
                $('#<%=LtrlCurrencySymbol.ClientID %>').text("Per Unit");
                $('#<%=RangedQtyTxtBox.ClientID %>').css("display", "block");
                $('#<%=CalCulatePriceBtn.ClientID %>').css("display", "block");
                $('#<%=ddQtyOptn.ClientID %>').css("display", "none");

            } else {
                $('#ddQtyOptn').val(SelectedQtyID);
                OnQtyIndexChanged(SelectedQtyID);
                $("#RangedQtylbls").css("display", "none");
                $("#FixedQtylbl").css("display", "block");
                $('#<%=LtrlCurrencySymbol.ClientID %>').text("Price");
                $('#<%=RangedQtyTxtBox.ClientID %>').css("display", "none");
                $('#<%=CalCulatePriceBtn.ClientID %>').css("display", "none");
                $('#<%=ddQtyOptn.ClientID %>').css("display", "block");
            }
            $("#tableCont").html(MatrixHtml);
        }

        function GenerateHtmlForRangedPriceMatrix(valueTo, valueFrom, Price, IsDiscountedPrice, RowStyleVal, BrokerMarkup, ContactMarkup) {
            var html = "";
            var Symbol = $('#<%=hfCurrencySymbol.ClientID %>').val();
            var BMode = '<%=IsBrokerMode %>';
            if (IsDiscountedPrice == 1) {
                var discountP = $('#<%=HiddenItemDiscountRate.ClientID %>').val();
                var DiscountedValue = Price - (Price * (discountP / 100));
               
                if (BMode == 1) {
                    DiscountedValue = CalculateMarkupsOnEachPrice(DiscountedValue, BrokerMarkup, ContactMarkup);
                    Price = CalculateMarkupsOnEachPrice(Price, BrokerMarkup, ContactMarkup);
                }
                if (RowStyleVal == 1) {
                    html = "<div style=background:#f3f3f3;height:30px; ><div class='RangedTbl float_left_simple'>" + valueFrom + "</div><div class='RangedTbl float_left_simple'>" + valueTo + "</div><div class='tblBorderTdWidth strikeThrough extra_pricing divtxtflt'>" + Symbol + Price + "</div><div class='extra_pricing custom_colorTS divtxtflt' >" + Symbol + DiscountedValue.toFixed(2) + "</div></div><div class=clearBoth >&nbsp;</div>";
                } else {
                    html = "<div><div class='RangedTbl float_left_simple'>" + valueFrom + "</div><div class='RangedTbl float_left_simple'>" + valueTo + "</div><div class='tblBorderTdWidth extra_pricing divtxtflt strikeThrough'>" + Symbol + Price.toFixed(2) + "</div><div class='extra_pricing custom_colorTS divtxtflt' >" + Symbol + DiscountedValue.toFixed(2) + "</div></div><div class=clearBoth >&nbsp;</div>";
                }
            } else {
                if (BMode == 1) {
                    Price = CalculateMarkupsOnEachPrice(Price, BrokerMarkup, ContactMarkup);
                   
                }
                if (RowStyleVal == 1) {
                    html = "<div style=background:#f3f3f3;height:30px; ><div class='RangedTbl float_left_simple'>" + valueFrom + "</div><div class='RangedTbl float_left_simple'>" + valueTo + "</div><div class='tblBorderTdWidth extra_pricing divtxtflt'>" + Symbol + Price.toFixed(2) + "</div></div><div class=clearBoth >&nbsp;</div>";
                } else {
                    html = "<div><div class='RangedTbl float_left_simple'>" + valueFrom + "</div><div class='RangedTbl float_left_simple'>" + valueTo + "</div><div class='tblBorderTdWidth extra_pricing divtxtflt'>" + Symbol + Price.toFixed(2) + "</div></div><div class=clearBoth >&nbsp;</div>";
                }
            }
            return html;
        }


        function GenerateHtmlForFixedPriceMatrix(valueQuantity, Price, IsDiscountedPrice, RowStyleVal, BrokerMarkup, ContactMarkup) {

            var html = "";
            var Symbol = $('#<%=hfCurrencySymbol.ClientID %>').val();
            var BMode = '<%=IsBrokerMode %>';

            if (IsDiscountedPrice == 1) {
                var discountP = $('#<%=HiddenItemDiscountRate.ClientID %>').val();
                var DiscountedValue = Price - (Price * (discountP / 100));
                if (BMode == 1) {
                    DiscountedValue = CalculateMarkupsOnEachPrice(DiscountedValue, BrokerMarkup, ContactMarkup);
                    Price = CalculateMarkupsOnEachPrice(Price, BrokerMarkup, ContactMarkup);
                }
                if (RowStyleVal == 1) {
                    html = "<div style=background:#f3f3f3;height:30px; ><div class='RangedTbl float_left_simple'>" + valueQuantity + "</div><div class='tblBorderTdWidth extra_pricing strikeThrough divtxtflt'>" + Symbol + Price.toFixed(2) + "</div><div class='custom_colorTS extra_pricing divtxtflt' >" + Symbol + DiscountedValue.toFixed(2) + "</div></div><div class=clearBoth >&nbsp;</div>";
                } else {
                    html = "<div><div class='RangedTbl float_left_simple'>" + valueQuantity + "</div><div class='tblBorderTdWidth extra_pricing strikeThrough divtxtflt'>" + Symbol + Price.toFixed(2) + "</div><div class='custom_colorTS extra_pricing divtxtflt' >" + Symbol + DiscountedValue.toFixed(2) + "</div></div><div class=clearBoth >&nbsp;</div>";
                }
            } else {
                if (BMode == 1) {
                    Price = CalculateMarkupsOnEachPrice(Price, BrokerMarkup, ContactMarkup);
                }
                if (RowStyleVal == 1) {
                    html = "<div style=background:#f3f3f3;height:30px; ><div class='RangedTbl float_left_simple'>" + valueQuantity + "</div><div class='mattPriceMatrixColumn tblBorderTdWidth extra_pricing divtxtflt'>" + Symbol + Price.toFixed(2) + "</div></div><div class=clearBoth >&nbsp;</div>";
                } else {
                    html = "<div><div class='RangedTbl float_left_simple'>" + valueQuantity + "</div><div class='mattPriceMatrixColumn tblBorderTdWidth extra_pricing divtxtflt'>" + Symbol + Price.toFixed(2) + "</div></div><div class=clearBoth >&nbsp;</div>";
                }
            }
            return html;
        }


        function CalculateMarkupsOnEachPrice(ActualPrice, brokerMrkup, contactMarkup) {
            var BrokerPercentage = 0;
            var CustPercentage = 0;
            var TotalPrice = 0;
            if (brokerMrkup > 0) {
                BrokerPercentage = ActualPrice * (brokerMrkup / 100);
                TotalPrice = ActualPrice + BrokerPercentage;
                if (contactMarkup > 0) {
                    CustPercentage = TotalPrice * (contactMarkup / 100);
                    TotalPrice = TotalPrice + CustPercentage;
                    return TotalPrice;
                }
                else if (contactMarkup < 0) {
                    CustPercentage = TotalPrice * (contactMarkup / 100);
                    TotalPrice = TotalPrice + CustPercentage;
                    return TotalPrice;
                }
                else {
                    return TotalPrice;
                }
            }
            else if (brokerMrkup < 0) {
                BrokerPercentage = ActualPrice * (brokerMrkup / 100);
                TotalPrice = ActualPrice + BrokerPercentage;
                if (contactMarkup > 0) {
                    CustPercentage = TotalPrice * (contactMarkup / 100);
                    TotalPrice = TotalPrice + CustPercentage;
                    return TotalPrice;
                }
                else if (contactMarkup < 0) {
                    CustPercentage = TotalPrice * (contactMarkup / 100);
                    TotalPrice = TotalPrice + CustPercentage;
                    return TotalPrice;
                }
                else {
                    return TotalPrice;
                }
            }
            else if (CPrice > 0) {
                CustPercentage = ActualPrice * (CPrice / 100);
                TotalPrice = CustPercentage + ActualPrice;
                return TotalPrice;
            }
            else if (CPrice < 0) {
                CustPercentage = ActualPrice * (CPrice / 100);
                TotalPrice = CustPercentage + ActualPrice;
                return TotalPrice;
            }
            else {
                return ActualPrice;
            }
        }


        // Fires on Buy Now Button click
        function BuythisProduct(ItemID) {
            CurrentItemId = ItemID;
            $("#ddStockOptn option").each(function () {
                $(this).remove();
            });
            var SelectedStockID = 0;
            $("#<%= hfItemID.ClientID %>").val(ItemID);
            $('#<%=hfRangedQty.ClientID %>').val(0);
            $("#<%= RangedQtyTxtBox.ClientID %>").val('');
            $('#<%=lblGrossTotal.ClientID %>').text(getCurrencySymbol() + "0");
            $.each(Products, function (key, value) {

                if (value.ProductId == ItemID) {
                    $('#<%=lblCatName.ClientID %>').text(value.ProductName);
                    $('#<%=hfRangedQty.ClientID %>').val(value.RangedMatrix);
                    $('#<%=HiddenItemDiscountRate.ClientID %>').val(value.DiscountPercentage);
                    return;
                }
            });
           
            $.each(Stocks, function (key, value) {

                if (value.itemID == ItemID) {
                    $("#ddStockOptn").append("<option value='" + value.StockId + "' opSeqId='" + value.OptionSequence + "'>" + value.StockName + "</option>");
                    if (SelectedStockID == 0) {
                        SelectedStockID = value.StockId;
                        StockOption = value.OptionSequence;
                        $('#<%=hfSelectedStock.ClientID %>').val(value.StockId);
                    }
                }
            });
            $('#ddStockOptn').val(SelectedStockID);
            FillPriceMatrix(StockOption, CurrentItemId);
            $("#PriceBox").css("display", "block");
        }

     
    </script>
</asp:Content>
