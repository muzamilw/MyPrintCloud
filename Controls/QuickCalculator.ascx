<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QuickCalculator.ascx.cs"
    Inherits="Web2Print.UI.Controls.QuickCalculator" %>
<div class="content_area container left_right_padding PDM7-CS  QCMainContainer">
    <div id="QuickCalcFrame" class=" QuickCalcFrame">
        <div id="QuickCalcFrameLoader" class="QuickCalcFrameLoader">
            <br />
            <img src='<%=ResolveUrl("~/images/asdf.gif") %>' alt="" /><br />
            Loading...
        </div>
        <h1 id="lblQuickCalcHead" class="QuickCalcHead" runat="server">
            Quick Calculator</h1>
        <div class="QuickCalcBar">
            <asp:DropDownList ID="ddlProducts" CssClass="QuickCalcProducts" runat="server" ClientIDMode="Static">
            </asp:DropDownList>
            <span id="lblQuickCalcQty" class="lblQuickCalcQty" runat="server" clientidmode="Static">
                Qty</span>
            <asp:DropDownList ID="ddlQuantity" runat="server" CssClass="QuickCalcQty" ClientIDMode="Static">
            </asp:DropDownList>
            <div class="clear">
            </div>
        </div>
        <div id="QuickQtyRanged">
            <span>Enter Quantity</span>
            <asp:TextBox ID="txtRangedQty" runat="server"></asp:TextBox>
        </div>
        <div id="QuickPriceBox" class="QuickCalcPriceBox">
            <div id="QuickCalcOnlinePrice" class="QuickCalcPriceLbl">
                Online price</div>
            <div id="QuickCalcFrom" class="QuickCalcPriceLbl" style="display:none;">
                &nbsp;from&nbsp;</div>
            <div id="QuickPrice" class="QuickCalcPrice">
            </div>
            <div class="QuickCalcVat">
                <asp:Label ID="lblVat" runat="server"></asp:Label></div>
        </div>
        <button id="AddtoBastkBtn" class="QuickPriceBtn" runat="server" clientidmode="Static" onclick="return GotoDetails()">
            Add to Basket</button>
        <asp:HiddenField ID="hlProductData" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hlPriceData" runat="server" ClientIDMode="Static" />
    </div>
     <div class="float_right Need_aPrice_CS">
        <a href="../RequestQuote.aspx" id="hfReqQ" runat="server">Need a price? </a>
</div>
    <div class="clearBoth">
    &nbsp;
</div>
   
</div>
<div class="clearBoth">
    &nbsp;
</div>
<script type="text/javascript">

    var PriceData = null;
    var ProductData = null;
    //var currencySymbol = '$';
    var SelectedProductID = 0;
    var SelectedProduct = null;

    $(document).ready(function () {


        //Loading Products data from service

        $.getJSON("/services/Webstore.svc/GetProducts?cID=" + cID + "&mode=" + smode,
		        function (xdata) {
		            ProductData = xdata;

		            //loading pricing data from service
		            $.getJSON("/services/Webstore.svc/GetProductPrices?cID=" + cID + "&mode=" + smode,
		                function (xdata) {
		                    PriceData = xdata;
		                    //getting the selected index and loading the Qty/Price
		                    makeInitSelection();
		                    ShowProductData();
		                    $('#QuickCalcFrameLoader').hide();

		                });
		        });
    });


    function makeInitSelection() {

        $.each(ProductData, function (i, p) {


            //populating the produccts dropdown
            $("#ddlProducts").append("<option value='" + p.ItemID + "'>" + p.ProductName + "</option>");

            if (p.IsSelectedBizCard == 1) {
                SelectedProductID = p.ItemID;
                SelectedProduct = p;
                return;
            }
        });

        $('#ddlProducts').val(SelectedProductID);
    }






    function ShowProductData() {

        SelectedProductID = $('#ddlProducts').val();
        SelectedProduct = null;

        //getting the selected product

        $.each(ProductData, function (i, p) {
            
            if (p.ItemID == SelectedProductID) {
                if (p.ContactCompanyID == cID) {
                    bIsOverridedPriceFound = true;
                }

                if (bIsOverridedPriceFound) {
                    if (p.ContactCompanyID == cID) {
                        SelectedProduct = p;
                        return;
                    }


                } else {
                    if (p.ContactCompanyID == undefined) {
                        SelectedProduct = p;
                        return;
                    }

                }
                
            }

        });


        if (SelectedProduct.IsQtyRanged == false) { //matrix drop down to be displayyed

            $('#ddlQuantity').show();
            $('#lblQuickCalcQty').show();
            $('#QuickQtyRanged').hide();


        }
        else {
            $('#ddlQuantity').hide();
            $('#QuickQtyRanged').hide();
            $('#lblQuickCalcQty').hide();


        }

        var counter = 0;
        var bIsOverridedPriceFound = false;
        $.each(PriceData, function (i, p) {

            if (p.ItemID == SelectedProductID) {
                if (p.ContactCompanyID == cID) {
                    bIsOverridedPriceFound = true;
                }

                if (bIsOverridedPriceFound) {
                    if (p.ContactCompanyID == cID) {
                        $("#ddlQuantity").append("<option value='" + p.PriceMatrixID + "'>" + p.Quantity + "</option>");
                    }


                } else {
                    if (p.ContactCompanyID == undefined) {
                        $("#ddlQuantity").append("<option value='" + p.PriceMatrixID + "'>" + p.Quantity + "</option>");
                    }

                }
                

                if (counter == 0) {

                    ShowPrice(p.PricePaperType1, SelectedProduct.BrokerMarkup, SelectedProduct.ContactMarkup, SelectedProduct.IsQtyRanged, p.QtyRangeFrom);
                }
                counter++;
            }

        });
        
    }


    function ShowSelectedQtyPrice() {

        var SelectedMatrixID = $('#ddlQuantity').val();

        $.each(PriceData, function (i, p) {

            if (p.PriceMatrixID == SelectedMatrixID) {


                ShowPrice(p.PricePaperType1, SelectedProduct.BrokerMarkup, SelectedProduct.ContactMarkup, SelectedProduct.IsQtyRanged, p.QtyRangeFrom);
                return;
            }

        });

    }


    function ShowPrice(BasePrice, M1, M2, IsQtyRanged, PriceFrom) {
        var Tax = '<%=TaxValue %>';
        var FromClause = '';
        if (IsQtyRanged == true) {
            BasePrice = BasePrice / PriceFrom;
            $('#QuickCalcOnlinePrice').html("Online Unit price");
            $("#QuickCalcFrom").css("display", "block");
        }
        else {
            $("#QuickCalcFrom").css("display", "none");
            $('#QuickCalcOnlinePrice').html("Online price");
        }

        if (Tax > 0) {

            BasePrice = BasePrice + (BasePrice * Tax / 100);
        }

        if (M1 != 0) {
            BasePrice = BasePrice * M1 / 100 + BasePrice;

        }

        if (M2 != 0) {
            BasePrice = BasePrice * M2 / 100 + BasePrice;

        }


        $('#QuickPrice').html(currencySymbol + '' + BasePrice.toFixed(2));

    }


    function GotoDetails() {

        window.location.href = SelectedProduct.NavPath.substring(1, SelectedProduct.NavPath.length);
        //alert(SelectedProduct.NavPath.substring(1, SelectedProduct.NavPath.length));


        return false;
    }

    $('#ddlProducts').change(function (sender) {
        $('select[id$=ddlQuantity] > option').remove();

        ShowProductData();

    });

    $('#ddlQuantity').change(function (sender) {


        ShowSelectedQtyPrice();

    });

    function showVideo() {
        var popwidth = 565;

        var shadow = document.getElementById("divShd");
        var bws = getBrowserHeight();
        shadow.style.width = bws.width + "px";
        shadow.style.height = bws.height + "px";
        var left = parseInt((bws.width - popwidth) / 2);
        var top = parseInt((bws.height - 315) / 2);


        //        shadow = null;
        $('#divShd').css("display", "block");
        $('#jqwin').css("width", popwidth);
        $('#jqwin').css("height", 320);
        $('#jqwin').css("top", top);
        $('#jqwin').css("left", left);
        var html = '<div class="closeBtn2" onclick="closeMS()"> </div>';
        $('#jqwin').html(html + '<iframe width="560" height="315" src="http://www.youtube.com/embed/u5sYhXRMWZM" frameborder="0" allowfullscreen></iframe>')

        $('#jqwin').show();
        $(".closeBtn2").css("display", "block");


        return true;
    }
</script>
