<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ThemeSite.Master" AutoEventWireup="true"
    CodeBehind="Receipt.aspx.cs" Inherits="Web2Print.UI.Receipt" %>

<%@ Register Src="~/Controls/BreadCrumbMenu.ascx" TagName="BreadCrumbMenu" TagPrefix="uc1" %>
<%@ Register Src="~/Controls/MainHeading.ascx" TagName="MainHeading" TagPrefix="uc3" %>
<%@ Register Src="~/Controls/WhyChooseUs.ascx" TagName="WhyChooseUs" TagPrefix="uc7" %>
<%@ Register Src="~/Controls/OrderDetails.ascx" TagName="OrderDetailsControl" TagPrefix="uc9" %>
<%@ Register Src="~/Controls/RelatedItems.ascx" TagName="RelatedItems" TagPrefix="uc11" %>
<%@ Register Src="Controls/MatchingSet.ascx" TagName="MatchingSet" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContents" runat="server">

    <style type="text/css">
        @media print
        {
            /* All your print styles go here */
            .LoginBarContainer, .top_header_ex, .TopMenuH60W100Px, #MainContent_MatchingSet1_MainContainerDiv, #MainContent_relateditemsWidget_controlBodyDiv, #Footer
            {
                display: none !important;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="MainContent" runat="server">
    <div id="page" class="container content_area" style="margin: auto; border: 0px solid orange; margin-top: 20px;">

        <div class="row left_right_padding">
            <ul class="bonhamsOption navM cursor_pointer">
                <li>1. Select
                </li>
                <li>2. Edit
                </li>


                <li>3. Confirm order & payment
                </li>
                <li class="bonhamSelectedOption">4. Order summary
                </li>

            </ul>
            <div class="col-md-12 col-lg-12 col-xs-12 receiptcontainer">

                <div style="font-family: 'Open Sans Condensed', sans-serif; font-style: normal; text-align: left; font-weight: normal; -moz-border-radius: 10px; -webkit-border-radius: 10px; -khtml-border-radius: 10px; border-radius: 10px; font-size: large;">
                    <div class="orderReceiptTitle">
                        <asp:Literal ID="ltrlreceipt" runat="server" Text="Receipt"></asp:Literal>
                        &nbsp;<asp:Label ID="lblReceiptNumber" runat="server" />
                    </div>
                    <div style="float: right; padding: 10px; padding-top: 14px; text-decoration: underline;">
                        <a id="lblPrintReceipt" runat="server" href="#" onclick="PrintReceipt();">Print this
                            receipt</a>
                    </div>
                    <div class="clearBoth">
                        &nbsp;
                    </div>
                </div>
            </div>
            <div class="receiptArea">
                <div style="width: 100%; display: none; text-align: center; font-size: 16px; font-weight: bold; color: #C72965;">
                    <asp:Literal ID="ltrlthNKU" runat="server" Text="Thank you"></asp:Literal>
                </div>
                <div style="width: 100%; display: none; text-align: center; font-size: 16px; font-weight: bold; color: #C72965;">
                    <asp:Literal ID="ltrlConfrmMesgd" runat="server" Text="Your Order is complete and some one will call you to confirm all details and delivery"></asp:Literal>
                </div>
                <div class="col-md-8 col-lg-8 col-xs-12 receiptWhitelblRightcnt">
                    &nbsp;
                    <asp:Image ID="ImagePinkcard" runat="server" Style="display: none;" CssClass="pink_company_logo floatright" />
                    <asp:Label ID="lblPoweredBy" runat="server" Text="Powered By" Style="color: #C72965; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-weight: bold; font-style: normal; font-weight: bold; display: none; font-size: 13px; line-height: 16px; float: right; margin-top: 25px;" />
                </div>
                <div id="trWhiteLabel" runat="server" class="col-md-4 col-lg-4 col-xs-12 receiptWhitelblleftcnt">
                    <asp:Image ID="Companyimg" runat="server" CssClass="IframeCompanyLogoCs_AddSelectCS"
                        Visible="false" /><br />
                    <asp:Label ID="ltrlCompanyName" runat="server" Style="font-size: 18px; font-weight: bold;"></asp:Label><br />
                    <asp:Label ID="lblAddLine1" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 12px; line-height: 16px;" />
                    <asp:Label ID="lblAddLine2" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 12px; line-height: 16px;" /><br />
                    <asp:Label ID="lblTown" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 12px; line-height: 16px;" />
                    <asp:Label ID="lblState" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 12px; line-height: 16px;" />
                    <asp:Label ID="lblZipCode" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 12px; line-height: 16px;" />
                    <asp:Label ID="lblCountry" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 12px; line-height: 16px;" /><br />
                    <div id="ImagePhne" class="TelLogoCs float_left_simple">
                    </div>
                    <asp:Label ID="lblTel" runat="server" Style="color: black; font-family: 'Open Sans Condensed', sans-serif; font-weight: bold; font-style: normal; font-weight: bold; font-size: 18px; line-height: 16px; float: left; margin-top: 6px; margin-left: 10px;" />
                </div>
                <div class=" col-md-8 col-lg-8 col-xs-12 receiptWhitelblRightcnt">
                    &nbsp;
                </div>
                <div id="trVatNumb" runat="server" style="height: 30px;" class="col-md-4 col-lg-4 col-xs-12 receiptWhitelblleftcnt">
                    <asp:Label ID="lblTaxName" runat="server" Style="font-weight: bold;"></asp:Label>
                    <asp:Label ID="lblVatNum" runat="server" CssClass="receiptVatNum" Style="font-weight: bold;" />
                </div>
                <div class="clearBoth" style="border-top: 2px solid #f3f3f3;">
                </div>
                <div class="col-md-4 col-lg-4 col-xs-12 receiptbillingShippingcnt">
                    <div style="" class="shopReceiptOrderDetails">
                        <span id="spnorderdate" runat="server" class="spnOrderHeadings">Order date: </span>
                    </div>
                    <asp:Label ID="lblOrderDate" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 12px; line-height: 34px; margin-left: 5px;" />
                    <div class="clearBoth">
                        &nbsp;
                    </div>
                    <div class="shopReceiptOrderDetails">
                        <span id="spnOrderCode" runat="server" class="spnOrderHeadings">Order Code: </span>
                    </div>
                    <asp:Label ID="lblOrderCode" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 12px; line-height: 34px; margin-left: 5px;" />
                    <div class="clearBoth">
                        &nbsp;
                    </div>
                    <div class="shopReceiptOrderDetails">
                        <span id="spninvoicedate" runat="server" class="spnOrderHeadings">Invoice date: </span>
                    </div>
                    <asp:Label ID="lblInvoiceDate" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 12px; line-height: 34px; margin-left: 5px;" />
                    <div class="clearBoth">
                        &nbsp;
                    </div>
                    <div class="shopReceiptOrderDetails">
                        <span id="spnplacedby" runat="server" class="spnOrderHeadings">Placed by: </span>
                    </div>
                    <asp:Label ID="lblPlacedBy" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 12px; line-height: 34px; margin-left: 5px;" />
                    <div class="clearBoth">
                        &nbsp;
                    </div>
                    <div class="shopReceiptOrderDetails">
                        <span id="spnyourref" runat="server" class="spnOrderHeadings">Your Ref: </span>
                    </div>
                    <asp:Label ID="lblYourRefNum" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 12px; line-height: 34px; margin-left: 5px;" />
                    <div class="clearBoth">
                        &nbsp;
                    </div>
                </div>
                <div class="col-md-4 col-lg-4 col-xs-12 receiptbillingShippingcnt" style="line-height: 30px;">
                    <span id="spnbilledto" runat="server" style="" class="shopReceiptBillToHeading">Billed To:</span><br />
                    <asp:Label ID="lblCustName" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 12px;" /><br />
                    <asp:Label ID="lblBAddressLine1" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 12px;" /><br />
                    <asp:Label ID="lblAddressLine2" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 12px;" /><br />
                    <asp:Label ID="lblBcity" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 12px;" />
                    <asp:Label ID="lblBState" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 12px;" />
                    <asp:Label ID="lblBZipCode" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 12px;" /><br />

                    <asp:Label ID="lblBCountry" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 12px;" /><br />
                    <span>
                        <asp:Literal ID="lrtlBTel" runat="server" Text="Tel:"></asp:Literal>
                    </span>
                    <asp:Label ID="lblBTel" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: normal; font-size: 12px;" />
                </div>
                <div class="col-md-4 col-lg-4 col-xs-12 receiptShippingDeliverycnt" style="line-height: 30px;">
                    <span id="spnshippedto" runat="server" class="shopReceiptBillToHeading">Shipped To:</span><br />
                    <asp:Label ID="lblSCusName" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 12px;" /><br />
                    <asp:Label ID="lblSAddLine1" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 12px;" /><br />
                    <asp:Label ID="lblSAddreLine2" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 12px;" /><br />
                    <asp:Label ID="lblSCity" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 12px;" />
                    <asp:Label ID="lblSState" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 12px;" />
                    <asp:Label ID="lblSZipCode" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 12px;" /><br />
                    <asp:Label ID="lblSCountry" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 12px;" /><br />
                    <asp:Literal ID="ltlShiTel" runat="server" Text="Tel:"></asp:Literal>
                    <asp:Label ID="lblSTel" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 12px;" />
                </div>
                <div class="clearBoth">
                </div>
                <div class="trspclInstr col-md-4 col-lg-4 col-xs-12 receiptbillingShippingcnt  spclcntHeight">
                    <span id="spnspeacilinstruction" runat="server" style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: normal; font-size: 12px; margin-left: 22px;">Special Instruction:</span><br />
                    <asp:Label ID="lblSpecialInstructions" runat="server" Style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: normal; font-size: 12px; line-height: 16px;" />
                </div>
                <div class="trspclInstr col-md-4 col-lg-4 col-xs-12 receiptbillingShippingcnt">
                    <span id="spnestimateddispatchdate" runat="server" style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 12px;">Estimated dispatch date:</span><br />
                    <asp:Label ID="lblEstimatedDispatchDate" runat="server" Style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: normal; font-size: 12px; line-height: 16px; margin-left: 0px;" />
                </div>
                <div class="trspclInstr col-md-4 col-lg-4 col-xs-12 receiptShippingDeliverycnt">
                    <span id="spnestimationmethod" runat="server" style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 12px;">Delivery method:</span><br />
                    <asp:Label ID="lblEstimatedDeliveryMethod" runat="server" Style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: normal; font-size: 12px; line-height: 16px;" />
                </div>
                <div class="clearBoth">
                </div>
                <div style="height: 35px; padding: 3px; margin-top: 5px; font-size: 15px; font-weight: bold" class="col-md-12 col-lg-12 col-xs-12">
                    <span id="spnitemordered" runat="server">Items Ordered:</span>
                </div>
                <div style="padding-bottom: 20px;" class="col-md-12 col-lg-12 col-xs-12">
                    <asp:GridView ID="grdViewShopCart" DataKeyNames="ItemID" runat="server" OnRowDataBound="grdViewShopCart_RowDataBound"
                        SkinID="gridViewNoHeaderSkin">
                        <Columns>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="32%">
                                <ItemTemplate>
                                    <img src="" runat="server" id="imgProduct" style="width: 40%;" alt="" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="43%">
                                <ItemTemplate>
                                    <div>
                                        <span style="font-weight: bold;">Quantity :
                                                    <%# DataBinder.Eval(Container.DataItem, "Qty1") %>
                                        </span>
                                        <br />
                                        <br />
                                        <asp:Label ID="lblItemProductName" runat="server" Font-Bold="true" Font-Size="12px"></asp:Label><br />
                                        <div class="">
                                            <asp:Label ID="lblPaPerName" runat="server" Font-Size="12px"></asp:Label>
                                        </div>
                                        <div class=" spacer5pxbottom">
                                            <asp:Label ID="lblItemDesc" runat="server" Font-Size="12px"></asp:Label>
                                        </div>
                                        <br />
                                        <asp:Label ID="lblAddonsHeading" runat="server" Font-Size="12px">with following addons :</asp:Label>
                                    </div>
                                    <asp:BulletedList Style="list-style-type: none; list-style-position: outside; list-style-image: none; padding: 0; margin-left: 0px; color: Gray;"
                                        ID="bltSelectedAddonList" runat="server"
                                        DisplayMode="Text">
                                    </asp:BulletedList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Qty1BaseCharge1") %>' />
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
                </div>
                <div class="clearBoth">
                </div>
                <div id="PnalTotalBox" runat="server" class="col-md-12 col-lg-12 col-xs-12" style="color: #333333; font-style: normal; font-weight: normal; font-size: 12px; line-height: 16px; padding: 10px; background-color: #f3f3f3; border: 1px solid #f4f4f4;">
                    <table border="0" cellpadding="3" cellspacing="0" width="100%">
                        <tr>
                            <td class="srFirstTd"></td>
                            <td class="srScdTd"></td>
                            <td class="srThirdTd" align="left"  style="font-weight: normal; font-size: 14px; color: #A8A8A8; line-height: 2; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;">
                                <span id="spnsubtotal" runat="server">Sub Total:</span>
                            </td>
                            <td class="srForthtd" >
                                <asp:Label ID="lblSubTotal" Text="0" runat="server" Style="font-size: 17px; color: Black;" />
                            </td>
                        </tr>
                        <tr id="tblRowVoucher" runat="server">
                            <td class="srFirstTd" ></td>
                            <td align="left" >
                                <asp:Label ID="lblVoucherCode" runat="server" Text="Voucher Code: " Style="color: black; font-size: 14px; font: bold;"></asp:Label>
                                <asp:Label ID="lblDicountName" runat="server" Style="color: black;"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:Label ID="lblVoucherDiscPercentageDisplay" Style="font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 14px; line-height: inherit; color: #67b8db; color: black !important;"
                                    Text="Discount:" runat="server" />
                            </td>
                            <td >
                                <asp:Label ID="lblVoucherCodeDiscAmount" Style="color: black !important;" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td ></td>
                            <td align="left"></td>
                            <td align="left"  style="font-weight: normal; font-size: 14px; color: #A8A8A8; line-height: 2; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;">
                                <span id="spndilevry" runat="server">Delivery:</span>
                            </td>
                            <td >
                                <asp:Label ID="lblDeliveryCostCenter" Text="0" runat="server" />
                            </td>
                        </tr>
                        <tr id="rowVat" runat="server">
                            <td ></td>
                            <td align="left"></td>
                            <td align="left"  style="font-weight: normal; font-size: 14px; color: #A8A8A8; line-height: 2; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;">
                                <asp:Label ID="lblTaxLabelTotal" text="VAT" runat="server"></asp:Label>
                            </td>
                            <td width="20%">
                                <asp:Label ID="lblVatTotal" Text="0" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td ></td>
                            <td ></td>
                            <td align="left">
                                <span id="spntotal" runat="server" style="font-weight: bold; color: Black; font-size: 17px;">Total:</span>
                            </td>
                            <td >
                                <asp:Label Style="font-weight: bold; font-size: 17px; color: Black;" ID="lblGrandTotal"
                                    Text="0" runat="server" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="clearBoth">
                </div>
            </div>
            <br />
            <uc2:MatchingSet ID="MatchingSet1" runat="server" Visible="false" />
            <br />
            <uc11:RelatedItems ID="relateditemsWidget" runat="server" Visible="false" />
        </div>
        <br />
        <br />
        <br />
    </div>
    <iframe id="receiptIframe1" runat="server" style="display: none;"></iframe>
    <script type="text/javascript" language="javascript">
        function PrintReceipt() {
            document.getElementById('MainContent_receiptIframe1').contentWindow.printMe();
        }


    </script>
</asp:Content>
