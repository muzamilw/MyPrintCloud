<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ThemeSite.Master" AutoEventWireup="true" Async="true"
    CodeBehind="PinkCardsShopCartAddSelect.aspx.cs" Inherits="Web2Print.UI.PinkCardsShopCartAddSelect" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Controls/OrderStep.ascx" TagName="OrderStep" TagPrefix="uc2" %>
<%@ Register Src="~/Controls/QuickLinks.ascx" TagName="QuickLinks" TagPrefix="uc5" %>
<%@ Register Src="~/Controls/WhyChooseUs.ascx" TagName="WhyChooseUs" TagPrefix="uc7" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContents" runat="server">
    <style>
        input[type="checkbox"]
        {
            display: none;
            outline: none !important;
            -webkit-transition: background-color;
            -moz-transition: background-color;
            -o-transition: background-color;
            -ms-transition: background-color;
            transition: background-color;
        }

            input[type="checkbox"] + label
            {
                display: inline-block !important;
                padding: 6px 0 6px 45px;
                line-height: 25px;
                background-image: url("//cdn.shopify.com/s/files/1/0245/8513/t/7/assets/checkbox_sprite.png?4214");
                background-image: none,url("//cdn.shopify.com/s/files/1/0245/8513/t/7/assets/checkbox_sprite.svg?4214");
                background-position: -108px 0;
                background-repeat: no-repeat;
                -webkit-background-size: 143px 143px;
                -moz-background-size: 143px 143px;
                background-size: 143px 143px;
                overflow: visible;
                outline: none;
                -webkit-user-select: none;
                -moz-user-select: none;
                -ms-user-select: none;
                user-select: none;
                cursor: pointer;
                cursor: pointer;
                color: #66615b;
                outline: none !important;
                font-size: 17px;
            }

                input[type="checkbox"]:hover + label, input[type="checkbox"] + label:hover, input[type="checkbox"]:hover + label:hover
                {
                    background-position: -72px -36px;
                    color: #403d39;
                }

            input[type="checkbox"]:checked + label
            {
                background-position: -36px -72px;
                color: #403d39;
            }

                input[type="checkbox"]:checked:hover + label, input[type="checkbox"]:checked + label:hover, input[type="checkbox"]:checked:hover + label:hover
                {
                    background-position: 0 -108px;
                    color: #403d39;
                }
        .rounded_corners5
        {
            margin-bottom: 0px;
        }
    </style>
    <script src="Scripts/utilities.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="MainContent" runat="server">
   <div id="divShd" class="opaqueLayer">
    </div>
    <div class="container content_area">

        <div class="row left_right_padding">
            <ul class="bonhamsOption navM cursor_pointer">
                <li >1. Select
                </li>
                <li >2. Edit
                </li>


                <li class="bonhamSelectedOption">3. Confirm order & payment
                </li>
                <li>4. Order summary
                </li>

            </ul>
            <div id="MessgeToDisply" class="rounded_corners col-md-12 col-lg-12 col-xs-12" runat="server" visible="false">
                <asp:Literal ID="ltrlMessge" runat="server"></asp:Literal>
            </div>
            <div class="signin_heading_div col-md-12 col-lg-12 col-xs-12">
                <asp:Label ID="lblCheckout" runat="server" Text="Checkout" CssClass="sign_in_heading"></asp:Label>
            </div>
            <div class="clearBoth">
                &nbsp;
            </div>
            <br />
            <div id="maincntCheckout" class="check_out_right_div col-md-12 col-lg-12 col-xs-12">
                <div class="clearBoth mainHeadingAveniorCheckout">
                    <asp:Label ID="Label2" runat="server" Text="Special Instructions" />
                </div>

                <div class="TTL_Checkout ckecoutWidthAvenior col-md-12 col-lg-12 col-xs-12">
                    <asp:Label ID="ltrlcontacttelnum" runat="server" Text="Contact Tel Number" CssClass="smallCheckoutAvenior col-md-2 col-lg-2 col-xs-12 "></asp:Label>

                    <input id="txtInstContactTelNumber" type="text" runat="server" class="AveniorTxtBoxCheckout col-md-2 col-lg-2 col-xs-12 "
                        onblur="ChangeTelNumber();" />

                </div>
                <div class="clearBoth">
                </div>

                <div class="col-md-12 col-lg-12 col-xs-12 ckecoutWidthAvenior TTL_Checkout">
                    <asp:Label ID="ltrlnotes" runat="server" Text="Notes" CssClass="col-md-2 col-lg-2 col-xs-12 checkoutNoteslbl"></asp:Label>

                    <asp:TextBox TextMode="MultiLine" ID="txtInstNotes" runat="server" Height="80px"
                        CssClass="AveniorTxtBoxCheckout" Columns="10" onblur="ChangeNotesTxt();" />
                </div>
                <div class="clearBoth">
                </div>

                <div id="CorporateBox" class="col-md-12 col-lg-12 col-xs-12 TTL_Checkout ckecoutWidthAvenior" visible="false" runat="server">
                    <asp:Label ID="ltrlcheckoutfirstdiv" runat="server" CssClass="col-md-2 col-lg-2 col-xs-12 smallCheckoutAvenior" Text="Your Ref / PO"></asp:Label>

                    <input id="txtYourRefNumber" type="text" runat="server" class="AveniorTxtBoxCheckout" maxlength="50"
                        onblur="ChangeRefPO();" />

                </div>
                <div id="NormalBox" class="col-md-12 col-lg-12 col-xs-12 TTL_Checkout ckecoutWidthAvenior" visible="false" runat="server">
                    <asp:Label ID="Label4" runat="server" CssClass="col-md-2 col-lg-2 col-xs-12 smallCheckoutAvenior" Text="Your Ref / PO"></asp:Label>

                    <input id="txtYourRefNumberRetail" type="text" runat="server" class="AveniorTxtBoxCheckout" maxlength="50"
                        onblur="ChangeRefPO();" />

                </div>
                <div class="clearBoth">
                    &nbsp;
                </div>
                <br />
                <br />

                   <div class="clearBoth mainHeadingAveniorCheckout col-md-12 col-lg-12 col-xs-12">
                    <asp:Label ID="Label1"  runat="server" Text="Choose Delivery Option" />
                </div>
                <div>
                    <div class="clearBoth smallCheckoutAvenior float_left_simple">
                        <asp:Literal ID="Literal3" runat="server" Text=" "></asp:Literal>
                    </div>
                    <div class="col-md-12 col-lg-12 col-xs-12 TTL_Checkout ckecoutWidthAvenior">
                        <asp:Label ID="Label3"  runat="server" Text="Choose Delivery" CssClass="smallCheckoutAvenior col-md-2 col-lg-2 col-xs-12"></asp:Label>

                         <asp:DropDownList ID="ddlDeliveyCostCenter" runat="server" CssClass="checkoutDropDown"
                        
                        />
                    </div>
                    <div class="clearBoth">
                    </div>
                   
                    <div id="pickUpaddressContainer" runat="server" class="col-md-12 col-lg-12 col-xs-12 TTL_Checkout ckecoutWidthAvenior">
                        <asp:Label ID="Label5"  runat="server" Text="Pickup Address" CssClass="smallCheckoutAvenior col-md-2 col-lg-2 col-xs-12"></asp:Label>

                        <asp:TextBox TextMode="MultiLine" ID="txtPickUpAddressDetail" runat="server" Height="80px" 
                        CssClass="AveniorTxtBoxCheckout" Columns="10" Enabled="false" />
                    
                    </div>
                   
                </div>
                <div class="clearBoth">
                    &nbsp;
                </div>
                <br />
                <br />
               <%-- billing shipping address panel --%>
                <div class="clearBoth mainHeadingAveniorCheckout col-md-12 col-lg-12 col-xs-12">
                    <asp:Label ID="Label6" runat="server" Text="Shipping Address" />
                </div>
                <div id="divDeliveryAddress">

                    <div id="ddshippingAdd" runat="server" class="col-md-12 col-lg-12 col-xs-12 TTL_Checkout ckecoutWidthAvenior">
                        <asp:Label ID="ltrlchoosedeliveryadd" runat="server" Text=" Choose Delivery Address" CssClass="col-md-2 col-lg-2 col-xs-12 smallCheckoutAvenior"></asp:Label>

                        <asp:DropDownList ID="ddlDelivery" runat="server" AutoPostBack="true" CssClass="checkoutDropDown"
                            OnSelectedIndexChanged="ddlDelivery_SelectedIndexChanged" />
                    </div>
                    <div class="clearBoth">
                    </div>

                    <div class="col-md-12 col-lg-12 col-xs-12 TTL_Checkout ckecoutWidthAvenior">
                        <asp:Label ID="ltrlshippingname" runat="server" Text="Shipping Name" CssClass="smallCheckoutAvenior col-md-2 col-lg-2 col-xs-12"></asp:Label>

                        <input id="txtShipAddressName" type="text" runat="server" class=" AveniorTxtBoxCheckout" maxlength="100" />
                        <asp:Label ID="Label7" CssClass="tooltipCheckoutAvenior" runat="server" Text="e.g. Office Address"></asp:Label>

                    </div>

                    <div class="clearBoth">
                    </div>


                    <div class="col-md-12 col-lg-12 col-xs-12 TTL_Checkout ckecoutWidthAvenior">
                        <asp:Label ID="ltrladdline2nd" runat="server" Text=" Address line 1" CssClass="smallCheckoutAvenior col-md-2 col-lg-2 col-xs-12"></asp:Label>

                        <input id="txtShipAddLine1" type="text" runat="server" class=" AveniorTxtBoxCheckout" maxlength="255"
                            onblur="ChangeShippToAddLin1();" />

                    </div>
                    <div class="clearBoth">
                    </div>

                    <div class="col-md-12 col-lg-12 col-xs-12 TTL_Checkout ckecoutWidthAvenior">
                        <asp:Label ID="ltrladdressline2nd2" runat="server" Text=" Address line 2" CssClass="smallCheckoutAvenior col-md-2 col-lg-2 col-xs-12"></asp:Label>


                        <input id="txtShipAddressLine2" type="text" runat="server" class="AveniorTxtBoxCheckout" maxlength="255"
                            onblur="ChangeShippToAddLin2();" />
                    </div>
                    <div class="clearBoth">
                    </div>

                    <div class="col-md-12 col-lg-12 col-xs-12 TTL_Checkout ckecoutWidthAvenior">
                        <asp:Label ID="ltrlcity2" runat="server" Text="City" CssClass="smallCheckoutAvenior col-md-2 col-lg-2 col-xs-12"></asp:Label>


                        <input id="txtShipAddCity" type="text" runat="server" class=" AveniorTxtBoxCheckout" maxlength="100"
                            onblur="ChangeShippToCity();" />

                    </div>
                    <div class="clearBoth">
                    </div>

                    <div class="col-md-12 col-lg-12 col-xs-12 TTL_Checkout ckecoutWidthAvenior">
                        <asp:Label ID="ltrlzipcodeP2" runat="server" Text="Zip Code / Post Code" CssClass="smallCheckoutAvenior col-md-2 col-lg-2 col-xs-12"></asp:Label>

                        <input id="txtShipPostCode" type="text" runat="server" class="AveniorTxtBoxCheckout float_left_simple" maxlength="30"
                            onblur="ChangeShippToPostCode();" /><img id="lodingImg" runat="server" alt=""
                                src="~/images/wpLoader.gif" style="display: none; float: left; width: 20px; margin-top: 7px; margin-left: 0px;" />

                    </div>
                    <div class="clearBoth">
                    </div>

                    <div class="col-md-12 col-lg-12 col-xs-12 TTL_Checkout ckecoutWidthAvenior">
                        <asp:Label ID="ltrlcountry2" runat="server" Text="Country" CssClass="smallCheckoutAvenior col-md-2 col-lg-2 col-xs-12"></asp:Label>
                         <asp:DropDownList ID="ddShippingCountry" runat="server" CssClass="checkoutDropDown"
                        AutoPostBack="true" OnSelectedIndexChanged="ShippingDropDown_SelectedIndexChanged"
                        />
                    </div>
                      <div class="clearBoth">
                    </div>
                    
                    <div  class="col-md-12 col-lg-12 col-xs-12 TTL_Checkout ckecoutWidthAvenior">
                        <asp:Label ID="lblStatedd" runat="server" Text="State" CssClass="smallCheckoutAvenior col-md-2 col-lg-2 col-xs-12"></asp:Label>
                         <asp:DropDownList ID="ddShippingState" runat="server" CssClass="checkoutDropDown"
                 
                        />
                    </div>
                      <div class="clearBoth">
                    </div>
                    <div class="col-md-12 col-lg-12 col-xs-12 TTL_Checkout ckecoutWidthAvenior">
                        <asp:Label ID="ltrlcontactnum2" runat="server" Text="Contact Number" CssClass="smallCheckoutAvenior col-md-2 col-lg-2 col-xs-12"></asp:Label>

                        <input id="txtShipContact" type="text" runat="server" class="AveniorTxtBoxCheckout" maxlength="60" />
                    </div>
                    <div class="clearBoth">
                        &nbsp;
                    </div>

                </div>
                    
                </div>
                <div class="clearBoth">
                    &nbsp;
                </div>
                <br />
                <br />
                <div class="col-md-12 col-lg-12 col-xs-12 TTL_Checkout ckecoutWidthAvenior">
                    <asp:CheckBox ID="chkBoxDeliverySameAsBilling" Text="Bill items to the above shipping address" runat="server"
                        Checked="true" onclick="BillingSameAsDeliveryScriptCheckBox();" OnCheckedChanged="chkBoxDeliverySameAsBilling_CheckedChanged" AutoPostBack="true" />
                </div>
                <div class="clear">
                </div>
                <br />
                <br />
                <br />

                <div id="divBilling">
                    <div class="clearBoth mainHeadingAveniorCheckout col-md-12 col-lg-12 col-xs-12">
                        <asp:Label ID="Label8" runat="server" Text="Billing Address" />
                    </div>

                    <div id="ddBillingAdd" runat="server" class="col-md-12 col-lg-12 col-xs-12 TTL_Checkout ckecoutWidthAvenior">
                        <asp:Label ID="lblBillingAddress" runat="server" Text="Choose Billing Address" CssClass="col-md-2 col-lg-2 col-xs-12 smallCheckoutAvenior"></asp:Label>
                        <asp:DropDownList ID="ddlBilling" runat="server" AutoPostBack="true" CssClass="checkoutDropDown"
                            OnSelectedIndexChanged="ddlBilling_SelectedIndexChanged" />
                    </div>
                    <div class="clearBoth">
                    </div>


                    <div class=" col-md-12 col-lg-12 col-xs-12 TTL_Checkout ckecoutWidthAvenior">
                        <asp:Label ID="ltrlBillingname" runat="server" Text="Billing Name" CssClass="smallCheckoutAvenior col-md-2 col-lg-2 col-xs-12"></asp:Label>

                        <input id="txtBillingName" type="text" runat="server" class="AveniorTxtBoxCheckout" maxlength="100" />
                        <asp:Label ID="Label9" runat="server" Text="e.g. Office Address" CssClass="tooltipCheckoutAvenior"></asp:Label>
                    </div>
                    <div class="clearBoth">
                    </div>


                    <div class=" col-md-12 col-lg-12 col-xs-12 TTL_Checkout ckecoutWidthAvenior">
                        <asp:Label ID="ltrladdressline1" runat="server" Text="Address line 1" CssClass="smallCheckoutAvenior col-md-2 col-lg-2 col-xs-12"></asp:Label>

                        <input id="txtAddressLine1" type="text" runat="server" class="AveniorTxtBoxCheckout" maxlength="255"
                            onblur="ChangeAddresLinne1();" />

                    </div>
                    <div class="clearBoth">
                    </div>


                    <div class=" col-md-12 col-lg-12 col-xs-12 TTL_Checkout ckecoutWidthAvenior">
                        <asp:Label ID="ltrladdressline2" runat="server" Text=" Address line 2" CssClass="smallCheckoutAvenior col-md-2 col-lg-2 col-xs-12"></asp:Label>

                        <input id="txtAddressLine2" type="text" runat="server" class="AveniorTxtBoxCheckout" maxlength="255"
                            onblur="ChangeAddresLinne2();" />
                    </div>
                    <div class="clearBoth">
                    </div>

                    <div class=" col-md-12 col-lg-12 col-xs-12 TTL_Checkout ckecoutWidthAvenior">
                        <asp:Label ID="ltrlcity" runat="server" Text="City" CssClass="smallCheckoutAvenior col-md-2 col-lg-2 col-xs-12"></asp:Label>

                        <input id="txtCity" type="text" runat="server" class="AveniorTxtBoxCheckout" maxlength="100"
                            onblur="ChangeCityShippBill();" />

                    </div>
                    <div class="clearBoth">
                    </div>

                    <div class=" col-md-12 col-lg-12 col-xs-12 TTL_Checkout ckecoutWidthAvenior">

                        <asp:Label ID="ltrlzipcodeP" runat="server" Text="Zip Code / Post Code" CssClass="smallCheckoutAvenior col-md-2 col-lg-2 col-xs-12"></asp:Label>

                        <input id="txtZipPostCode" type="text" runat="server" class="AveniorTxtBoxCheckout " maxlength="30"
                            onblur="ChangePostCodeShippBill();" />

                    </div>
                    <div class="clearBoth">
                    </div>

                 
                    <div class="col-md-12 col-lg-12 col-xs-12 TTL_Checkout ckecoutWidthAvenior">
                        <asp:Label ID="ltrlcountry"  runat="server" Text="Country" CssClass="smallCheckoutAvenior col-md-2 col-lg-2 col-xs-12"></asp:Label>
                         <asp:DropDownList ID="BillingCountryDropDown" AutoPostBack="true" runat="server" CssClass="checkoutDropDown" OnSelectedIndexChanged="BillingCountryDropDown_SelectedIndexChanged"
                   
                        />
                    </div>
                    <div class="clearBoth">
                    </div>
                       <div class="col-md-12 col-lg-12 col-xs-12 TTL_Checkout ckecoutWidthAvenior">
                        <asp:Label ID="ltrlstate" runat="server" Text="Country" CssClass="smallCheckoutAvenior col-md-2 col-lg-2 col-xs-12"></asp:Label>
                         <asp:DropDownList ID="ddBillingState" runat="server" CssClass="checkoutDropDown"
                       
                        />
                    </div>
                    <div class="clearBoth">
                    </div>
                    <div class="col-md-12 col-lg-12 col-xs-12 TTL_Checkout ckecoutWidthAvenior">
                        <asp:Label ID="ltrlcontactnum" runat="server" Text="Contact Number" CssClass="smallCheckoutAvenior col-md-2 col-lg-2 col-xs-12"></asp:Label>

                        <input id="txtContactNumber" type="text" runat="server" class="AveniorTxtBoxCheckout" maxlength="60" />
                    </div>
 
                    
                </div>
            <div style="clear: both; font-size: 18px; color: rgb(179, 60, 18); font-family: 'Avenir Next LT W01 Demi', Helvetica, Arial, sans-serif; height: auto; width: 80%; text-align: left;">
                Please make sure your shipping and billing addresses are correct. Addresses cannot be changed once an order is submitted and your order may be canceled if we cannot confirm either address.
            </div>
            <br />
            <br />
            <div class="check_out_third_div">
                <div class="confirm_order_detail_div">
                    <div style="min-height: 290px">
                        <asp:Panel ID="pnlConfirmOrder" runat="server">
                            <div class="mainHeadingAveniorCheckout">
                                <asp:Literal ID="ltrlconfirmurorderdetails" runat="server" Text="Confirm your order details..."></asp:Literal>
                            </div>
                            <br />
                            <asp:GridView ID="grdViewShopCart" DataKeyNames="ItemID" runat="server" SkinID="grdViewCheckoutCartView" CellSpacing="2" CssClass="width100p"
                                OnRowDataBound="grdViewShopCart_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Quantity" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="PaddingToCells">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalItemQuantity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Qty1") %>'
                                                Font-Bold="true" CssClass="Fsize14" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Product Description" HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="PaddingToCells"
                                        ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemProductName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>'
                                                Font-Bold="true" CssClass="Fsize14"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblPapErName" runat="server"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblAddons" runat="server"></asp:Label>
                                            <asp:BulletedList CssClass="UList" ID="bltSelectedAddonList" runat="server" DisplayMode="Text"
                                                Visible="false">
                                            </asp:BulletedList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Price" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="PaddingToCells">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemPrice" runat="server" CssClass="Fsize14" Text='<%# DataBinder.Eval(Container.DataItem, "Qty1BaseCharge1") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="VAT" HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVatStateTax" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false" HeaderText="Total" HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalItemPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Qty1GrossTotal") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <br />
                            <div id="PnalTotalBox" runat="server">
                                <table border="0" cellpadding="3" cellspacing="0" width="100%" class="left_align">
                                    <tr>
                                        <td width="30%">&nbsp;
                                        </td>
                                        <td width="30%">&nbsp;
                                        </td>
                                        <td width="20%" align="left" class="LightGrayLabels" id="tdSubtotal" runat="server">
                                            <span id="spnsubtotal" runat="server">Sub Total:</span>
                                        </td>
                                        <td align="right" width="20%">
                                            <asp:Label ID="lblSubTotal" Text="0" runat="server" CssClass="Fsize15 colorBlack"
                                                Style="margin-right: 13px;" />
                                        </td>
                                    </tr>
                                    <tr id="tblRowVoucher" runat="server">
                                        <td width="30%" align="left">&nbsp;
                                        </td>
                                        <td width="30%">&nbsp;
                                        </td>
                                        <td width="20%" align="left">
                                            <asp:Label ID="lblVoucherDiscPercentageDisplay" Style="font-family: 'Lato', Calibri, Arial, sans-serif; font-style: normal; font-weight: bold; font-size: 14px; line-height: 13px; color: rgb(102,102,102); color: #FB8D00 !important;"
                                                Text="Discount:" runat="server" />
                                        </td>
                                        <td align="right" width="20%">
                                            <asp:Label ID="lblVoucherCodeDiscAmount" Style="margin-right: 13px; color: #FB8D00 !important;"
                                                Text="0" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="30%" align="left">&nbsp;
                                        </td>
                                        <td width="30%">&nbsp;
                                        </td>
                                        <td width="20%" align="left" class=" LightGrayLabels" id="tddelivery" runat="server">
                                            <span id="spndelivery" runat="server">Delivery:</span>
                                        </td>
                                        <td align="right" width="20%">
                                            <asp:Label ID="lblDeliveryCostCenter" Text="0" runat="server" CssClass="LightGrayLabels"
                                                Style="margin-right: 13px;" />
                                        </td>
                                    </tr>
                                    <tr id="rowVat" runat="server">
                                        <td width="30%" align="left"></td>
                                        <td width="30%"></td>
                                        <td width="20%" align="left" class="LightGrayLabels">
                                            <asp:Label ID="lblTaxLabel" runat="server"></asp:Label>
                                        </td>
                                        <td align="right" width="20%">
                                            <asp:Label ID="lblVatTotal" Text="0" CssClass="LightGrayLabels" runat="server" Style="margin-right: 13px;" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="30%">&nbsp;
                                        </td>
                                        <td width="30%">&nbsp;
                                        </td>
                                        <td width="20%" align="left" class="fontSyleBold Fsize15" id="tdgrandtotal" runat="server">
                                            <span class="CartFonts colorBlack" id="spngrandtotal" runat="server">Total:</span>
                                        </td>
                                        <td align="right" width="20%">
                                            <asp:Label CssClass="CartFonts colorBlack Fsize15" ID="lblGrandTotal" Text="0" runat="server"
                                                Style="margin-right: 13px;" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                    </div>
                    <div id="DivShippTo" class="black_sub_heading textAlignLeft paddingLeft10px" style="display: none;"
                        runat="server">
                        <asp:Label ID="ltrlShipTo" runat="server" Text="Bill to & Ship to:"></asp:Label>
                    </div>
                    <div id="innerContainerShippTo" class="textAlignLeft AddressesContainer" style="display: none;"
                        runat="server">
                        <asp:Label ID="lblShipToAddLin1" runat="server" class="lbAddressLinesWithPadding" />
                        <asp:Label ID="lblShipToAddLin2" runat="server" class="lbAddressLines" /><br />
                        <asp:Label ID="lblShipToCity" runat="server" class="lbAddressLinesWithPadding" /><br />
                        <asp:Label ID="lblShipTostate" runat="server" class="lbAddressLinesWithPadding" />&nbsp;
                                <asp:Label ID="lblShipToCountry" runat="server" class="lbAddressLines" />&nbsp;
                                <asp:Label ID="lblShipToZipCode" runat="server" class="lbAddressLines" /><br />
                    </div>
                    <br />
                    <div id="BillTOShippTO" class="black_sub_heading textAlignLeft paddingLeft10px" runat="server">
                        <asp:Label ID="ltrlBillShippTo" runat="server" Text="Bill to:"></asp:Label>

                    </div>
                    <div id="BillTOShippTOContainer" class="textAlignLeft AddressesContainer" runat="server">
                        <asp:Label ID="lblAddLine1BilSip" runat="server" class="lbAddressLinesWithPadding" />
                        <asp:Label ID="lbladdLine2BilSip" runat="server" class="lbAddressLines" /><br />
                        <asp:Label ID="lblCityBilSip" runat="server" class="lbAddressLinesWithPadding" /><br />
                        <asp:Label ID="lblStateBilSip" runat="server" class="lbAddressLinesWithPadding" />&nbsp;
                                <asp:Label ID="lblCountryBilSip" runat="server" class="lbAddressLines" />&nbsp;
                                <asp:Label ID="lblPostCdeBilSip" runat="server" class="lbAddressLines" /><br />
                    </div>
                    <br />
                    <div class="textAlignLeft paddingLeft10px">
                        <asp:Label ID="lblContactNotxt" runat="server" Text="Contact No:" CssClass="black_sub_heading"></asp:Label>
                        <asp:Label ID="lblContactNo" runat="server" CssClass="ContactNumberlb" />
                    </div>
                    <br />
                    <div id="DivRefPO" style="display: none;" class="textAlignLeft paddingLeft10px">
                        <asp:Label ID="lblRefPOtxt" runat="server" Text="Your Ref/PO is:" CssClass="black_sub_heading"></asp:Label><asp:Label
                            ID="lblRefPo" runat="server" CssClass="POlb"></asp:Label>
                    </div>
                    <br />
                    <div id="DivSpclNotes" style="display: none;" class="textAlignLeft paddingLeft10px">
                        <asp:Label ID="lblSpecialNoteTxt" runat="server" Text="Special instructions(notes):"
                            Style="color: #C72965;" CssClass="black_sub_heading"></asp:Label><br />
                        <br />
                        <asp:Label ID="lblNotesDetail" runat="server" CssClass="SpecialNotesContainer" />
                    </div>
                    <div class="pad10">
                        <div class="float_right marginLeft directDepositcs">
                            <asp:Button ID="btnDirectDeposit" CssClass="start_creating_btn_SApp rounded_corners5" UseSubmitBehavior="true"
                                runat="server" OnClick="btnDirectDeposit_Click" Visible="false" Text="Pay Via Direct Deposite"
                                ClientIDMode="Static" OnClientClick="return ValidateCheckout();" />
                        </div>
                    </div>
                    <div class="clearBoth">
                    </div>
                    <div class="pad10">
                        <div class="float_right marginLeft" >

                            <asp:Button ID="btnConfirmOrder" CssClass="start_creating_btn_SApp rounded_corners5" UseSubmitBehavior="true"
                                runat="server" OnClick="btnConfirmOrder_Click" Enabled="true" Text="Continue"
                                ClientIDMode="Static" OnClientClick="return ValidateCheckout();" />
                        </div>
                        <div class="float_right">
                            <asp:Button Text="BACK TO SHOPPING" ID="btnShopCart" CssClass="btn_brown rounded_corners5"
                                runat="server" OnClick="btnShopCart_Click" CausesValidation="False" />
                        </div>
                        <div class="clearBoth">
                            &nbsp;
                        </div>
                    </div>
                </div>
                <br />
                <br />
            </div>
            <div class="clearBoth">
                &nbsp;
            </div>
            <div class="col-md-6 col-lg-6 col-xs-12 cntcheckoutSummary">

                <div class="super_admin_message" id="DivretailMesg" runat="server" visible="false">
                    <asp:Literal ID="ltrlSomeOmeCallU" runat="server" Text=" Some one will call you"></asp:Literal>
                    <asp:Literal ID="ltrlconfirmDetails" runat="server" Text=" to reconfirm details."></asp:Literal>
                </div>
                <div class="super_admin_message" id="divAdminMessage" runat="server" visible="false">
                    <asp:Literal ID="ltrlorderwillbed" runat="server" Text=" Order will be delivered after "></asp:Literal>

                    <asp:Label ID="lblSupAdminName" runat="server"></asp:Label>

                    <asp:Literal ID="ltrlapprovethisorder" runat="server" Text=" approves this order."></asp:Literal>
                </div>
                <div class="clearBoth">
                    &nbsp;
                </div>
                <div class="super_admin_message" runat="server" id="divWarnnigMEsg">
                    <asp:Literal ID="ltrlWarnnigMEsg" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="col-md-6 col-lg-6 col-xs-12 cntcheckoutSummary">

                <asp:Label ID="lblYourOrderPRoMesg" runat="server" Text="Your order being processed by:" CssClass="mainHeadingAveniorCheckout" Style="display: none; height: auto !important;"></asp:Label>
                <div class="SelectedBroker nContainer rounded_corners" style="display: none;">
                    <table style="width: 100%;">
                        <tr id="container" class="PostCodetableRow">
                            <td class="tdBrokerLogo">
                                <div class="CkOutBrokerLlogo MargnRght10">
                                    <img id="imgLogo" style="border: 0px; max-width: 180px; max-height: 70px;" class="brokerLogoImg" />
                                </div>
                                <div>
                                    <asp:Label ID="BrokerAddLine1" runat="server" Style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: normal; font-size: 12px; line-height: 16px;" />
                                    <asp:Label ID="BrokerAddLine2" runat="server" Style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: normal; font-size: 12px; line-height: 16px;" /><br />
                                    <asp:Label ID="BrokerTown" runat="server" Style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: bold; font-weight: normal; font-size: 14px;" />,
                                    <asp:Label ID="BrokerState" runat="server" Style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: bold; font-weight: normal; font-size: 14px;" />,
                                    <asp:Label ID="BrokerCountry" runat="server" Style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: bold; font-weight: normal; font-size: 14px;" /><br />
                                    <asp:Label ID="BrokerFax" runat="server" Style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: bold; font-weight: normal; font-size: 14px;" />
                                    <br />
                                    <br />
                                    <div class="SearchPostCodeTeleImg" runat="server" id="telephoneIcon">
                                    </div>
                                    <div id="BrokerTeleNo" class="srchpstcodeTel">
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="errorMesgBroker nContainer rounded_corners" style="display: none;">
                </div>
            </div>

            <div class="textAlignCenter">
                <asp:Label ID="lblMsg" runat="server" Text="Your Cart is empty!" Visible="false" />
                <asp:Label ID="lblStatus" runat="server" Text="" CssClass="errorMessage" Visible="false" />
            </div>
            <asp:HiddenField ID="txthdnDeliveryAddressID" runat="server" Value="0" />
            <asp:HiddenField ID="txthdnBillingAddressID" runat="server" Value="0" />
            <asp:HiddenField ID="txthdnBillingDefaultAddress" runat="server" Value="false" />
            <asp:HiddenField ID="txthdnBillingDefaultShippingAddress" runat="server" Value="false" />
            <asp:HiddenField ID="txthdnDeliveryDefaultAddress" runat="server" Value="false" />
            <asp:HiddenField ID="txthdnDeliveryDefaultShippingAddress" runat="server" Value="false" />
            <asp:HiddenField ID="txtVoucherDiscountRate" runat="server" Value="0" />
            <asp:HiddenField ID="hfIsCorporate" runat="server" />
            <asp:HiddenField ID="hfBrokerContactCompany" runat="server" Value="0" />
            <asp:HiddenField ID="hfISBrokerSelected" runat="server" Value="0" />
            <asp:HiddenField ID="hfPageIsValid" runat="server" Value="1" />
            <asp:HiddenField ID="hfIsDefaultBilling" runat="server" Value="0" />
            <br />
            <br />
            <br />
            <asp:Label ID="lblQuantityHeaderTxt" runat="server" Visible="false"></asp:Label>
            <asp:Label ID="lblProdDescHeadingText" runat="server" Visible="false"></asp:Label>
            <asp:Label ID="lblPriceHeaderTxt" runat="server" Visible="false"></asp:Label>
            <asp:Label ID="lblVatHeadingText" runat="server" Visible="false"></asp:Label>
            <asp:Label ID="lblTotalHeadingText" runat="server" Visible="false"></asp:Label>

        </div>
    </div>
    
    <br />
    <br />
    <script src="Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">


        $(document).ready(function () {
            window.onkeydown = keydown;
            initialize();
            var ShipTO = '<%=Resources.MyResource.ltrlShipTo %>';

            if ($("#<%=chkBoxDeliverySameAsBilling.ClientID %>")[0].checked == true) {


                $('#divBilling').css('display', 'none');
                $('#<%=BillTOShippTO.ClientID %>').css('display', 'none');
                $('#<%=BillTOShippTOContainer.ClientID %>').css('display', 'none');
                $('#<%=DivShippTo.ClientID %>').css('display', 'block');
                $('#<%=innerContainerShippTo.ClientID %>').css('display', 'block');
            }
            else {

                $('#divDeliveryAddress').css('display', 'block');
                $('#<%=DivShippTo.ClientID %>').css('display', 'block');
                $('#<%=innerContainerShippTo.ClientID %>').css('display', 'block');
                $("#<%=ltrlShipTo.ClientID %>").html("Ship to:");
            }
            LoadBrokerByPostCode();
            if ($("#<%= hfPageIsValid.ClientID %>").val() == "0") {
                ValidateCheckout();
            }

        });

        function keydown(e) {
            if (e.which == 13) {
                e.preventDefault();
                return false;
            }
        }

        function showProgressBar() {
            if (Page_ClientValidate()) {

                showProgress();
            }
            else {

                return false;
            }
        }




    <%-- Billing Address--%>


        var txtBillingName = null;
        var txtAddressLine1 = null;
        var txtAddressLine2 = null;
        var txtCity = null;
        var txtState = null;
        var txtZipPostCode = null;
        var txtCountry = null;
        var txtContactNumber = null;
        var ddlBilling = null;
        var ddlDelivery = null;

        function initialize() {

            ddlBilling = $("#" + "<%=ddlBilling.ClientID %>");
            ddlDelivery = $("#" + "<%=ddlDelivery.ClientID %>");

            txtBillingName = $("#" + "<%=txtBillingName.ClientID %>");
            txtAddressLine1 = $("#" + "<%=txtAddressLine1.ClientID %>")
            txtAddressLine2 = $("#" + "<%=txtAddressLine2.ClientID %>");
            txtCity = $("#" + "<%=txtCity.ClientID %>");

            txtZipPostCode = $("#" + "<%=txtZipPostCode.ClientID %>");

            txtContactNumber = $("#" + "<%=txtContactNumber.ClientID %>");

        }


        function SetNewControlValues() {
            ddlBilling.val($("#" + "<%=ddlDelivery.ClientID %>").val());
            txtBillingName.val($("#" + "<%=txtShipAddressName.ClientID %>").val());
            txtAddressLine1.val($("#" + "<%=txtShipAddLine1.ClientID %>").val());
            txtAddressLine2.val($("#" + "<%=txtShipAddressLine2.ClientID %>").val());
            txtCity.val($("#" + "<%=txtShipAddCity.ClientID %>").val());

            txtZipPostCode.val($("#" + "<%=txtShipPostCode.ClientID %>").val());

            txtContactNumber.val($("#" + "<%=txtShipContact.ClientID %>").val());

        }
        function EnableTextBoxes() {

            $("#<%= txtBillingName.ClientID %>").prop("disabled", "");
            $("#<%= txtAddressLine1.ClientID %>").prop("disabled", "");
            $("#<%= txtAddressLine2.ClientID %>").prop("disabled", "");
            $("#<%= txtCity.ClientID %>").prop("disabled", "");

            $("#<%= txtZipPostCode.ClientID %>").prop("disabled", "");
            $("#<%= txtContactNumber.ClientID %>").prop("disabled", "");
        }
        function BillingSameAsDeliveryScriptCheckBox() {

            var ShipTO = '<%=Resources.MyResource.ltrlShipTo %>';
            var BillShippTO = '<%=Resources.MyResource.lnkBillShipto %>';
            if ($("#" + "<%=chkBoxDeliverySameAsBilling.ClientID %>")[0].checked == true) {
                //  alert("c");
                //   $("#MainContent_chkBoxDeliverySameAsBilling").attr("checked", "checked");
                $('#<%=BillTOShippTO.ClientID %>').css('display', 'none');
                $('#<%=BillTOShippTOContainer.ClientID %>').css('display', 'none');
                $("#<%=ltrlShipTo.ClientID %>").html(BillShippTO);
                SetNewControlValues();


                $('#divBilling').css('display', 'none');
            }
            else {
                //  
                // $("#MainContent_chkBoxDeliverySameAsBilling").removeattr("checked");
                if ($("#<%= hfIsDefaultBilling.ClientID %>").val() != "1") {
                    EnableTextBoxes();
                }
                var ShipTO = '<%=ShipToTxt %>';
                $('#<%=BillTOShippTO.ClientID %>').css('display', 'block');
                $('#<%=BillTOShippTOContainer.ClientID %>').css('display', 'block');
                $("#<%=ltrlShipTo.ClientID %>").html(ShipTO + "");
                $("#<%=lblAddLine1BilSip.ClientID %>").html("");
                $("#<%=lbladdLine2BilSip.ClientID %>").html("");
                $("#<%=lblCityBilSip.ClientID %>").html("");
                $("#<%=lblStateBilSip.ClientID %>").html("");
                $("#<%=lblCountryBilSip.ClientID %>").html("");
                $("#<%=lblPostCdeBilSip.ClientID %>").html("");
                //                //Set Selected Value to add new Mode

                if (ddlBilling.val() != "0") {
                    //ddlBilling.val("0");

                    //txtBillingName.val("");
                    //txtAddressLine1.val("");
                    //txtAddressLine2.val("");
                    //txtCity.val("");
                    //txtState.val("");
                    //txtZipPostCode.val("");
                    //txtCountry.val("");
                    //txtContactNumber.val("");
                    //reset hidden fields as well
                    //$("#" + "<%=txthdnBillingAddressID.ClientID %>").val("0");
                    // $("#" + "<%=txthdnBillingDefaultAddress.ClientID %>").val("false");
                    // $("#" + "<%=txthdnBillingDefaultShippingAddress.ClientID %>").val("false");

                    $('#divBilling').css('display', 'block');
                } else {
                    $('#divBilling').css('display', 'block');
                }

            }


        }

        function billingSameAsDeliveryScript() {

            if (ddlBilling.val() == ddlDelivery.val()) {

                SetNewControlValues();
            }
            else {

            }
        }

        function getthecheckbox() {
            BillingSameAsDeliveryScriptCheckBox();
        }


        function ChangeAddresLinne1() {
            var val = $('#<%=txtAddressLine1.ClientID %>').val();
            $('#<%=lblAddLine1BilSip.ClientID %>').html(val);
        }

        function ChangeCountryShippBill() {
            // set from drop down
        }
        function ChangePostCodeShippBill() {
            var val = $('#<%=txtZipPostCode.ClientID %>').val();
            $('#<%=lblPostCdeBilSip.ClientID %>').html(val);
        }
        function ChangestateShippBill() {
            // get value from drop down
        }
        function ChangeCityShippBill() {
            var val = $('#<%=txtCity.ClientID %>').val();
            $('#<%=lblCityBilSip.ClientID %>').html(val);
        }
        function ChangeAddresLinne2() {
            var val = $('#<%=txtAddressLine2.ClientID %>').val();
            $('#<%=lbladdLine2BilSip.ClientID %>').html(val);
        }


        function ChangeShippToAddLin1() {
            var val = $('#<%=txtShipAddLine1.ClientID %>').val();
            $('#<%=lblShipToAddLin1.ClientID %>').html(val);
        }

        function ChangeShippToAddLin2() {
            var val = $('#<%=txtShipAddressLine2.ClientID %>').val();
            $('#<%=lblShipToAddLin2.ClientID %>').html(val);
        }
        function ChangeShippToCity() {
            var val = $('#<%=txtShipAddCity.ClientID %>').val();
            $('#<%=lblShipToCity.ClientID %>').html(val);
        }
        function ChangeShippToState() {
            // get state from drop down
        }
        function ChangeShippToPostCode() {
            var val = $('#<%=txtShipPostCode.ClientID %>').val();
             $('#<%=lblShipToZipCode.ClientID %>').html(val);
             var ISPinkCard = '<%=IsPinkCards %>';
             if (ISPinkCard == 1) {
                 var PostCode = $("#<%=txtShipPostCode.ClientID %>").val();
                if (PostCode == '') {
                } else {
                    $('#<%=lodingImg.ClientID %>').css("display", "block");
                    $.ajax({

                        url: "../services/Webstore.svc/GetNearestBrokerStoreByPostCode?PostCode=" + PostCode,

                        success: function (data) {
                            $('#<%=lblYourOrderPRoMesg.ClientID %>').css("display", "block");
                             if (data == null) {
                                 $('.SelectedBroker').css("display", "block");
                                 $('.SelectedBroker').html("No results found.");
                             } else {
                                 $('#<%=hfBrokerContactCompany.ClientID %>').val(data.ContactCompanyID);
                                $('.brokerLogoImg').attr('src', data.Logo + "");
                                $('#<%=BrokerAddLine1.ClientID %>').text(data.Address1);
                                $('#<%=BrokerAddLine2.ClientID %>').text(data.Address2);
                                $('#<%=BrokerTown.ClientID %>').text(data.City);
                                $('#<%=BrokerState.ClientID %>').text(data.State);
                                $('#<%=BrokerCountry.ClientID %>').text(data.Country);
                                $("#BrokerTeleNo").html(data.HomeContact);
                                $('.SelectedBroker').css("display", "block");
                            }

                             $('#<%=lodingImg.ClientID %>').css("display", "none");
                         }
                     });
                 }
             }
         }
         function ChangeShippToCounntry() {
             //set country text from drop down
         }
         function ChangeTelNumber() {
             var val = $('#<%=txtInstContactTelNumber.ClientID %>').val();
            $('#<%=lblContactNo.ClientID %>').html(val);
        }
        function ChangeRefPO() {
            var val = $('#<%=txtYourRefNumber.ClientID %>').val();
             if (val == "") {
                 $('#DivRefPO').css('display', 'none');
                 $('#<%=lblRefPo.ClientID %>').html("");
            }
            else {
                $('#DivRefPO').css('display', 'block');
                $('#<%=lblRefPo.ClientID %>').html(val);
            }

            var val = $('#<%=txtYourRefNumberRetail.ClientID %>').val();
             if (val == "") {
                 $('#DivRefPO').css('display', 'none');
                 $('#<%=lblRefPo.ClientID %>').html("");
            }
            else {
                $('#DivRefPO').css('display', 'block');
                $('#<%=lblRefPo.ClientID %>').html(val);
            }

        }
        function ChangeNotesTxt() {
            var val = $('#<%=txtInstNotes.ClientID %>').val();
                if (val == "") {
                    $('#DivSpclNotes').css('display', 'none');
                    $('#<%=lblNotesDetail.ClientID %>').html("");
                 }
                 else {
                     $('#DivSpclNotes').css('display', 'block');
                     $('#<%=lblNotesDetail.ClientID %>').html(val);
                }

            }


            $("#<%=txtShipPostCode.ClientID %>").keydown(function (e) {
            if (e.which == 13) {

                LoadBrokerByPostCode();
                e.preventDefault();
            }
        });



        function ValidateShippingAddressCheckout() {
            var isDataFilled = 0;

            if ($("#<%= txtInstContactTelNumber.ClientID %>").val() == "") {
                isDataFilled = 1;
                $('.EContactTelNo').css("display", "block");
                $("#<%= txtInstContactTelNumber.ClientID %>").css("border", "1px solid #b33c12");//.parent().addClass("field-with-errors");
            } else {
                $('.EContactTelNo').css("display", "none");
                $("#<%= txtInstContactTelNumber.ClientID %>").css("border", "1px solid #c1c6cc");//.parent().removeClass("field-with-errors");
            }

            if ($("#<%= txtYourRefNumber.ClientID %>").val() == "") {
                isDataFilled = 1;
                $('.ECorporateRefNo').css("display", "block");
                $("#<%= txtYourRefNumber.ClientID %>").css("border", "1px solid #b33c12");//.parent().addClass("field-with-errors");
            } else {
                $('.ECorporateRefNo').css("display", "none");
                $("#<%= txtYourRefNumber.ClientID %>").css("border", "1px solid #c1c6cc");//.parent().removeClass("field-with-errors");
            }

            if ($("#<%= txtShipAddressName.ClientID %>").val() == "") {
                isDataFilled = 1;
                $('.EShipAddresNameNo').css("display", "block");
                $("#<%= txtShipAddressName.ClientID %>").css("border", "1px solid #b33c12");//.parent().addClass("field-with-errors");
            } else {
                $('.EShipAddresNameNo').css("display", "none");
                $("#<%= txtShipAddressName.ClientID %>").css("border", "1px solid #c1c6cc");//.parent().removeClass("field-with-errors");
            }

            if ($("#<%= txtShipAddLine1.ClientID %>").val() == "") {
                isDataFilled = 1;
                $('.EShipAddres1').css("display", "block");
                $("#<%= txtShipAddLine1.ClientID %>").css("border", "1px solid #b33c12");//.parent().addClass("field-with-errors");
            } else {
                $('.EShipAddres1').css("display", "none");
                $("#<%= txtShipAddLine1.ClientID %>").css("border", "1px solid #c1c6cc");//.parent().removeClass("field-with-errors");
            }
            if ($("#<%= txtShipAddCity.ClientID %>").val() == "") {
                isDataFilled = 1;
                $('.EShipAddresCity').css("display", "block");
                $("#<%= txtShipAddCity.ClientID %>").css("border", "1px solid #b33c12");//.parent().addClass("field-with-errors");
            } else {
                $('.EShipAddresCity').css("display", "none");
                $("#<%= txtShipAddCity.ClientID %>").css("border", "1px solid #c1c6cc");//.parent().removeClass("field-with-errors");
            }

            if ($("#<%= ddShippingCountry.ClientID %>").val() == -1) {
                isDataFilled = 1;
                $("#<%= ddShippingCountry.ClientID %>").css("border", "1px solid #b33c12");
            } else {
                $("#<%= ddShippingCountry.ClientID %>").css("border", "1px solid #c1c6cc");
            }

            if ($("#<%= ddlDeliveyCostCenter.ClientID %>").val() == -1) {
                isDataFilled = 1;
                $("#<%= ddlDeliveyCostCenter.ClientID %>").css("border", "1px solid #b33c12");
            } else {
                $("#<%= ddlDeliveyCostCenter.ClientID %>").css("border", "1px solid #c1c6cc");
            }



            if ($("#<%= ddShippingState.ClientID %>").val() == -1) {
                isDataFilled = 1;
                $("#<%= ddShippingState.ClientID %>").css("border", "1px solid #b33c12");
            } else {
                $("#<%= ddShippingState.ClientID %>").css("border", "1px solid #c1c6cc");
            }


            if ($("#<%= ddBillingState.ClientID %>").val() == -1) {
                isDataFilled = 1;
                $("#<%= ddBillingState.ClientID %>").css("border", "1px solid #b33c12");
            } else {
                $("#<%= ddBillingState.ClientID %>").css("border", "1px solid #c1c6cc");
            }

            if ($("#<%= BillingCountryDropDown.ClientID %>").val() == -1) {
                isDataFilled = 1;
                $("#<%= BillingCountryDropDown.ClientID %>").css("border", "1px solid #b33c12");
            } else {
                $("#<%= BillingCountryDropDown.ClientID %>").css("border", "1px solid #c1c6cc");
            }


            if ($("#<%= txtShipPostCode.ClientID %>").val() == "") {
                isDataFilled = 1;
                $('.EShipAddresPostCode').css("display", "block");
                $("#<%= txtShipPostCode.ClientID %>").css("border", "1px solid #b33c12");//.parent().addClass("field-with-errors");
            } else {
                $('.EShipAddresPostCode').css("display", "none");
                $("#<%= txtShipPostCode.ClientID %>").css("border", "1px solid #c1c6cc");//.parent().removeClass("field-with-errors");
            }


            return isDataFilled;
        }

        function ValidateBillingAddressCheckout() {
            var isDataFilled = 0;

            if ($("#<%= txtBillingName.ClientID %>").val() == "") {
                isDataFilled = 1;
                $('.EBillAddresName').css("display", "block");
                $("#<%= txtBillingName.ClientID %>").css("border", "1px solid #b33c12");//.parent().addClass("field-with-errors");
            } else {
                $('.EBillAddresName').css("display", "none");
                $("#<%= txtBillingName.ClientID %>").css("border", "1px solid #c1c6cc");//.parent().removeClass("field-with-errors");
            }

            if ($("#<%= txtAddressLine1.ClientID %>").val() == "") {
                isDataFilled = 1;
                $('.EBillAddres1').css("display", "block");
                $("#<%= txtAddressLine1.ClientID %>").css("border", "1px solid #b33c12");//.parent().addClass("field-with-errors");
            } else {
                $('.EBillAddres1').css("display", "none");
                $("#<%= txtAddressLine1.ClientID %>").css("border", "1px solid #c1c6cc");//.parent().removeClass("field-with-errors");
            }

            if ($("#<%= txtCity.ClientID %>").val() == "") {
                isDataFilled = 1;
                $('.EBillAddresCity').css("display", "block");
                $("#<%= txtCity.ClientID %>").css("border", "1px solid #b33c12");//.parent().addClass("field-with-errors");
            } else {
                $('.EBillAddresCity').css("display", "none");
                $("#<%= txtCity.ClientID %>").css("border", "1px solid #c1c6cc");//.parent().removeClass("field-with-errors");
            }

            if ($("#<%= txtZipPostCode.ClientID %>").val() == "") {
                isDataFilled = 1;
                $('.EBillAddresZipCode').css("display", "block");
                $("#<%= txtZipPostCode.ClientID %>").css("border", "1px solid #b33c12");//.parent().addClass("field-with-errors");
            } else {
                $('.EBillAddresZipCode').css("display", "none");
                $("#<%= txtZipPostCode.ClientID %>").css("border", "1px solid #c1c6cc");//.parent().removeClass("field-with-errors");
            }
            if ($("#<%= BillingCountryDropDown.ClientID %>").val() == -1) {
                isDataFilled = 1;
                $("#<%= BillingCountryDropDown.ClientID %>").css("border", "1px solid #b33c12");
            } else {
                $("#<%= BillingCountryDropDown.ClientID %>").css("border", "1px solid #c1c6cc");
            }
            if ($("#<%= ddBillingState.ClientID %>").val() == -1) {
                isDataFilled = 1;
                $("#<%= ddBillingState.ClientID %>").css("border", "1px solid #b33c12");
            } else {
                $("#<%= ddBillingState.ClientID %>").css("border", "1px solid #c1c6cc");
            }
            return isDataFilled;
        }

        function ValidateCheckout() {
            var isDataFilled = 0;
            var isValid = true;

            var isContainBlanksInShipping = ValidateShippingAddressCheckout();

            if ($("#<%=chkBoxDeliverySameAsBilling.ClientID %>")[0].checked == true) {
                if (isContainBlanksInShipping == 1) {
                    $("#<%= hfPageIsValid.ClientID %>").val(0);
                    isValid = false;
                    event.preventDefault();
                    $('html, body').animate({
                        scrollTop: $("#maincntCheckout").offset().top
                    }, 500);
                } else {
                    $("#<%= hfPageIsValid.ClientID %>").val(1);
                    isValid = true;
                }
            } else {
                var iContainsBlankInBilling = ValidateBillingAddressCheckout();

                if (iContainsBlankInBilling == 1) {
                    $("#<%= hfPageIsValid.ClientID %>").val(0);
                    isValid = false;
                    event.preventDefault();
                    $('html, body').animate({
                        scrollTop: $("#maincntCheckout").offset().top
                    }, 500);
                } else if (isContainBlanksInShipping == 1) {
                    $("#<%= hfPageIsValid.ClientID %>").val(0);
                    isValid = false;
                    event.preventDefault();
                    $('html, body').animate({
                        scrollTop: $("#maincntCheckout").offset().top
                    }, 500);
                } else {
                    $("#<%= hfPageIsValid.ClientID %>").val(1);
                    isValid = true;
                    showProgress();
                }
        }

        if (event.keyCode == undefined) {
        } else {
            if (event.keyCode == 13) { //if this is enter key
                event.preventDefault();
                isValid = false;
                $("#<%= hfPageIsValid.ClientID %>").val(0);
            } else {
                $("#<%= hfPageIsValid.ClientID %>").val(1);
            }
        }
        if (isValid == false) {

        } else {
            showProgress();
        }
        return isValid;
    }
    function LoadBrokerByPostCode() {
        var ISPinkCard = '<%=IsPinkCards %>';
        if (ISPinkCard == 1) {
            var PostCode = $("#<%=txtShipPostCode.ClientID %>").val();
            $('#<%=lodingImg.ClientID %>').css("display", "block");

            $.ajax({

                url: "../services/Webstore.svc/GetNearestBrokerStoreByPostCode?PostCode=" + PostCode,

                success: function (data) {
                    $('#<%=lblYourOrderPRoMesg.ClientID %>').css("display", "block");

                    if (data.ContactCompanyID == undefined) {
                        $('.errorMesgBroker').css("display", "block");
                        $('.SelectedBroker').css("display", "none");
                        $('.errorMesgBroker').html("No results found.");

                    } else {
                        $('#<%=hfBrokerContactCompany.ClientID %>').val(data.ContactCompanyID);

                            $('.brokerLogoImg').attr('src', data.Logo + "");
                            $('#<%=BrokerAddLine1.ClientID %>').text(data.Address1);
                            $('#<%=BrokerAddLine2.ClientID %>').text(data.Address2);
                            $('#<%=BrokerTown.ClientID %>').text(data.City);
                            $('#<%=BrokerState.ClientID %>').text(data.State);
                            $('#<%=BrokerCountry.ClientID %>').text(data.Country);
                            $("#BrokerTeleNo").html(data.HomeContact);
                            $('.SelectedBroker').css("display", "block");
                            $('.errorMesgBroker').css("display", "none");
                        }

                    $('#<%=lodingImg.ClientID %>').css("display", "none");
                }
            });

        }
        $('#<%=hfBrokerContactCompany.ClientID %>').val(0);
    }

    </script>
    
</asp:Content>
