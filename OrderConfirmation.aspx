<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ThemeSite.Master" AutoEventWireup="true" CodeBehind="OrderConfirmation.aspx.cs" Inherits="Web2Print.UI.OrderConfirmation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContents" runat="server">
</asp:Content>

<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container content_area">
        <div class="row left_right_padding">
            <div id="MessgeToDisply" class="rounded_corners col-md-12 col-lg-12 col-xs-12" runat="server" visible="false">
                <asp:Literal ID="ltrlMessge" runat="server"></asp:Literal>
            </div>
           
                <h2>Review Your Order

                </h2>

            <div class="white-container-lightgrey-border textAlignLeft rounded_corners">
                <div class="col-md-4 col-lg-4 col-xs-12 receiptbillingShippingcnt" style="line-height: 30px;">
                    <span runat="server" style="" class="shopReceiptBillToHeading">PickUpAddress :</span><br />
                    <asp:Label ID="PickupAddressName" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 12px;" /><br />
                    <asp:Label ID="pickUpAddressLine1" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 12px;" /><br />
                    <asp:Label ID="pickUpAddress2" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 12px;" /><br />
                    <asp:Label ID="pickUpAddressCity" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 12px;" />
                    <asp:Label ID="pickUpAddressState" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 12px;" />
                    <asp:Label ID="pickUPAddressCountry" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 12px;" /><br />

                    <asp:Label ID="pickUpAddressZip" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 12px;" /><br />
                    <span>
                        <asp:Literal runat="server" Text="Tel:"></asp:Literal>
                    </span>
                    <asp:Label ID="pickUpAddressContact" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: normal; font-size: 12px;" />
                </div>
                <div class="col-md-4 col-lg-4 col-xs-12 receiptbillingShippingcnt" style="line-height: 30px;">
                    <span id="Span1" runat="server" style="" class="shopReceiptBillToHeading">Billing Address :</span><br />
                    <asp:Label ID="AddressName" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 12px;" /><br />
                    <asp:Label ID="AddressLine" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 12px;" /><br />
                    <asp:Label ID="Addressline2" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 12px;" /><br />
                    <asp:Label ID="lblBCity" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 12px;" />
                    <asp:Label ID="lblBState" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 12px;" />
                    <asp:Label ID="lblBCountry" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 12px;" /><br />
                    <asp:Label ID="lblBZipCode" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 12px;" /><br />

                    
                    <span>
                        <asp:Literal ID="lrtlBTel" runat="server" Text="Tel:"></asp:Literal>
                    </span>
                    <asp:Label ID="lblBTel" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: normal; font-size: 12px;" />
                </div>
                <div class="col-md-4 col-lg-4 col-xs-12 receiptbillingShippingcnt" style="line-height: 30px;">
                    <span id="Span2" runat="server" style="" class="shopReceiptBillToHeading">Shipping Address:</span><br />
                    <asp:Label ID="ShippingAddressName" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 12px;" /><br />
                    <asp:Label ID="shippingAddressline1" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 12px;" /><br />
                    <asp:Label ID="shippingAddressline2" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 12px;" /><br />
                    <asp:Label ID="shippingCity" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 12px;" />
                    <asp:Label ID="shippingState" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 12px;" />
                    <asp:Label ID="shippingCountry" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 12px;" /><br />
                    <asp:Label ID="shippingZipCode" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 12px;" /><br />

                    
                    <span>
                        <asp:Literal ID="Literal2" runat="server" Text="Tel:"></asp:Literal>
                    </span>
                    <asp:Label ID="shippingTel" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: normal; font-size: 12px;" />
                </div>

            

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
                                        <td width="20%" align="left" class="LightGrayLabels" id="td1" runat="server">
                                            <span id="spnsubtotal" runat="server">Sub Total:</span>
                                        </td>
                                        <td align="right" width="20%">
                                            <asp:Label ID="lblSubTotal" Text="0" runat="server" CssClass="Fsize15 colorBlack"
                                                Style="margin-right: 13px;" />
                                        </td>
                                    </tr>
                                    <tr id="Tr1" runat="server">
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
                                        <td width="20%" align="left" class=" LightGrayLabels" id="td2" runat="server">
                                            <span id="spndelivery" runat="server">Delivery:</span>
                                        </td>
                                        <td align="right" width="20%">
                                            <asp:Label ID="lblDeliveryCostCenter" Text="0" runat="server" CssClass="LightGrayLabels"
                                                Style="margin-right: 13px;" />
                                        </td>
                                    </tr>
                                    <tr id="Tr2" runat="server">
                                        <td width="30%" align="left"></td>
                                        <td width="30%"></td>
                                        <td width="20%" align="left" class="LightGrayLabels">
                                            <asp:Label ID="lblTaxLabel" Text="VAT" runat="server"></asp:Label>
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
                                        <td width="20%" align="left" class="fontSyleBold Fsize15" id="td3" runat="server">
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
                    <div class="pad10">
                        <div class="float_right marginLeft directDepositcs">
                            <asp:Button ID="btnDirectDeposit" CssClass="start_creating_btn_SApp rounded_corners5" UseSubmitBehavior="true"
                                runat="server" OnClick="btnDirectDeposit_Click" Visible="false" Text="Pay Via Direct Deposite"
                                ClientIDMode="Static" />
                        </div>
                    </div>
                    <div class="clearBoth">
                    </div>
                    <div class="pad10">
                        <div class="float_right marginLeft">

                            <asp:Button ID="btnPlaceOrder" CssClass="start_creating_btn_SApp rounded_corners5" UseSubmitBehavior="true"
                                runat="server" OnClick="btnPlaceOrder_Click" Enabled="true"
                                ClientIDMode="Static" />
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
            </div>
                </div>
        </div>
    </div>


</asp:Content>
