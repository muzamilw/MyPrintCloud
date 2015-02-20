<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReceiptPlain.aspx.cs" Inherits="Web2Print.UI.ReceiptPlain" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        @media print
        {
            /* All your print styles go here */
            .LoginBarContainer, .top_header_ex, .TopMenuH60W100Px, #MainContent_MatchingSet1_MainContainerDiv, #MainContent_relateditemsWidget_controlBodyDiv, #Footer, #ctl20_controlBodyDiv
            {
                display: none !important;
            }
        }
    </style>
</head>
<body>
    <form id="PinkCardOrderReceipt" runat="server">
    <asp:PlaceHolder runat="server" ID="RplainControlsContainer" Visible="false"></asp:PlaceHolder>
    <div id="page" style="width: 1000px; margin: auto; border: 0px solid orange;">
        <div style="padding: 1px 0px 1px 0px;">
            <div style="width: 100%;">
                <div style="font-family: 'Open Sans Condensed', sans-serif; font-style: normal; text-align:left; font-weight: normal; font-size: 20px; -moz-border-radius: 10px;
                    -webkit-border-radius: 10px; -khtml-border-radius: 10px; border-radius: 10px;">
                    <div style="padding: 15px; padding-left:0px; text-align:left; font-family: 'Open Sans Condensed', sans-serif;
                        font-style: normal; font-weight: bold; font-size: 20px; line-height: 13px; color: black;">
                        <asp:Literal ID="ltrlreceipt" runat="server" Text="Receipt"></asp:Literal>
                       </div>
                </div>
            </div>
            <div style="background-color: White; padding: 10px; text-align: left; padding-left:0px; -moz-border-radius: 10px;
                -webkit-border-radius: 10px; -khtml-border-radius: 10px; border-radius: 10px;">
                <table border="0" cellpadding="0" cellspacing="0" width="1000px">
                    <tr>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="8" width="100%" style=" border-collapse:collapse;">
                                <tr id="trWhiteLabel" runat="server">
                                <td width="60%" colspan="2" valign="top">
                                    </td>
                                    <td >
                                        <asp:Image ID="Companyimg" runat="server" Width="235" Height="70"  Style="margin-bottom: 10px; display:none;" /><br />
                                        <table>
                                            <tr>
                                                <td width="255">
                                                    <asp:Label ID="ltrlCompanyName" runat="server" style=" font-size: 18px;"></asp:Label><br />
                                                    <asp:Label ID="lblAddLine1" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                                        font-style: normal; font-weight: bold; font-size: 12px; line-height: 16px;" />
                                                    <asp:Label ID="lblAddLine2" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                                        font-style: normal; font-weight: bold; font-size: 12px; line-height: 16px;" /><br />
                                                    <asp:Label ID="lblTown" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                                        font-style: normal; font-weight: bold; font-size: 12px; line-height: 16px;" />
                                                    <asp:Label ID="lblState" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                                        font-style: normal; font-weight: bold; font-size: 12px; line-height: 16px;" />
                                                    <asp:Label ID="lblZipCode" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                                        font-style: normal; font-weight: bold; font-size: 12px; line-height: 16px;" />
                                                    <asp:Label ID="lblCountry" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                                        font-style: normal; font-weight: bold; font-size: 12px; line-height: 16px;" /><br />
                                                         <div id="ImagePhne" class="TelLogoCs float_left_simple">
                                                    </div>
                                                    <asp:Label ID="lblTel" runat="server" Style="color: black; font-family: 'Open Sans Condensed', sans-serif;
                                                        font-weight: bold; font-style: normal; font-weight: bold; font-size: 18px;
                                                        line-height: 16px; float: left; margin-top: 6px; margin-left:10px;" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    
                                </tr>
                                <tr id="trVatNumb" runat="server" style="height: 30px;">
                                    <td width="60%" colspan="2" >
                                        &nbsp;
                                    </td>
                                    <td style="padding-left: 5px;">
                                    <asp:Label ID="lblTaxName" runat="server"></asp:Label>
                                        <asp:Label ID="lblVatNum" runat="server" Style="color: #333333; font-family: 'Open Sans Condensed', sans-serif;
                                            font-style: normal; font-weight: normal; font-size: 12px; line-height: 16px;" />
                                    </td>
                                </tr>
                                <tr style="border-top: 2px solid #f3f3f3; border-bottom: 2px solid #f3f3f3; -moz-border-radius: 5px; -webkit-border-radius: 5px;
                                    -khtml-border-radius: 5px; border-radius: 5px;">
                                    <td valign="top" colspan="1" style="padding-bottom:10px; padding-left:20px; padding-right:20px; padding-top: 10px;">
                                        <div style="width: 100px; text-align: right; float: left; clear: both;">
                                            <span id="spnorderdate" runat="server" style="color: #333333; font-family: 'Open Sans Condensed', sans-serif;
                                                font-style: normal; font-weight: bold; font-size: 13px; line-height: 1.7; text-align: right;">Order
                                                date: </span>
                                        </div>
                                        <asp:Label ID="lblOrderDate" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                            font-style: normal; font-weight: bold; font-size: 12px; line-height: 25px; margin-left: 5px;" /><br />
                                        <div class="clearBoth">
                                            &nbsp;
                                        </div>
                                        <div style="width: 100px; text-align: right; float: left;">
                                            <span id="spnOrderCode" runat="server" style="color: #333333; font-family: 'Open Sans Condensed', sans-serif;
                                                font-style: normal; font-weight: bold; font-size: 13px; line-height: 1.7; margin-bottom: 4px; text-align: right;">
                                                Order Code: </span>
                                        </div>
                                        <asp:Label ID="lblOrderCode" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                            font-style: normal; font-weight: bold; font-size: 12px; line-height: 25px; margin-left: 5px;" /><br />
                                        <div class="clearBoth">
                                            &nbsp;
                                        </div>
                                        <div style="width: 100px; text-align: right; float: left;">
                                            <span id="spninvoicedate" runat="server" style="color: #333333; font-family: 'Open Sans Condensed', sans-serif;
                                                font-style: normal; font-weight: bold; font-size: 13px; line-height: 1.7; text-align: right;">Invoice
                                                date: </span>
                                        </div>
                                        <asp:Label ID="lblInvoiceDate" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                            font-style: normal; font-weight: bold; font-size: 12px; line-height: 25px; margin-left: 5px;" /><br />
                                        <div class="clearBoth">
                                            &nbsp;
                                        </div>
                                        <div style="width: 100px; text-align: right; float: left;">
                                            <span id="spnplacedby" runat="server" style="color: #333333; font-family: 'Open Sans Condensed', sans-serif;
                                                font-style: normal; font-weight: bold; font-size: 12px; line-height: 1.7; text-align: right;">Placed
                                                by: </span>
                                        </div>
                                        <asp:Label ID="lblPlacedBy" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                            font-style: normal; font-weight: bold; font-size: 12px; line-height: 25px; margin-left: 5px;" /><br />
                                        <div class="clearBoth">
                                            &nbsp;
                                        </div>
                                        <div style="width: 100px; text-align: right; float: left;">
                                            <span id="spnyourref" runat="server" style="color: #333333; font-family: 'Open Sans Condensed', sans-serif;
                                                font-style: normal; font-weight: bold; font-size: 12px; line-height: 1.7; text-align: right;">Your
                                                Ref: </span>
                                        </div>
                                        <asp:Label ID="lblYourRefNum" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                            font-style: normal; font-weight: bold; font-size: 12px; line-height: 25px; margin-left: 5px;" /><br />
                                        <div class="clearBoth">
                                            &nbsp;
                                        </div>
                                    </td>
                                    <td valign="top" colspan="1" style="padding-bottom:10px; padding-left:20px; padding-right:20px; padding-top: 10px;">
                                        <table border="0" cellpadding="3" cellspacing="0" width="100%">
                                            <tr>
                                                <td colspan="2">
                                                    <span id="spnbilledto" runat="server" style="color: #333333; font-family: 'Open Sans Condensed', sans-serif;
                                                        font-style: normal; font-weight: bold; font-size: 13px;">Billed To:</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblCustName" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                                        font-style: normal; font-weight: bold; font-size: 12px; line-height: 16px;" />
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblBAddressLine1" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                                        font-style: normal; font-weight: bold; font-size: 12px; line-height: 10px;" />
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblAddressLine2" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                                        font-style: normal; font-weight: bold; font-size: 12px; line-height: 10px;" />
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblBcity" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                                        font-style: normal; font-weight: bold; font-size: 12px; line-height: 5px;" />
                                                    <asp:Label ID="lblBState" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                                        font-style: normal; font-weight: bold; font-size: 12px; line-height: 5px;" />
                                                    <asp:Label ID="lblBZipCode" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                                        font-style: normal; font-weight: bold; font-size: 12px; line-height: 5px;" />
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblBCountry" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                                        font-style: normal; font-weight: bold; font-size: 12px; line-height: 10px;" />
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <span>
                                                        <asp:Literal ID="lrtlBTel" runat="server" Text="Tel:"></asp:Literal>
                                                    </span>
                                                    <asp:Label ID="lblBTel" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                                        font-style: normal; font-weight: normal; font-size: 12px; line-height: 10px;" />
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td colspan="1" valign="top" style=" padding-top:10px; padding-bottom:10px;">
                                        <table border="0" cellpadding="3" cellspacing="0" width="100%">
                                            <tr>
                                                <td colspan="2" valign="top">
                                                    <span id="spnshippedto" runat="server" style="color: #333333; font-family: 'Open Sans Condensed', sans-serif;
                                                        font-style: normal; font-weight: bold; font-size: 13px;">Shipped To:</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblSCusName" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                                        font-style: normal; font-weight: bold; font-size: 12px; line-height: 16px;" />
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblSAddLine1" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                                        font-style: normal; font-weight: bold; font-size: 12px; line-height: 10px;" />
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblSAddreLine2" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                                        font-style: normal; font-weight: bold; font-size: 12px; line-height: 10px;" />
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblSCity" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                                        font-style: normal; font-weight: bold; font-size: 12px; line-height: 5px;" />
                                                    <asp:Label ID="lblSState" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                                        font-style: normal; font-weight: bold; font-size: 12px; line-height: 5px;" />
                                                    <asp:Label ID="lblSZipCode" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                                        font-style: normal; font-weight: bold; font-size: 12px; line-height: 5px;" />
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblSCountry" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                                        font-style: normal; font-weight: bold; font-size: 12px; line-height: 10px;" />
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Literal ID="ltlShiTel" runat="server" Text="Tel:"></asp:Literal>
                                                    <asp:Label ID="lblSTel" runat="server" Style="color: #A8A8A8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                                        font-style: normal; font-weight: bold; font-size: 12px; line-height: 10px;" />
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table border="0" cellpadding="3" cellspacing="0" width="100%" style="background: #f3f3f3;padding: 20px;">
                                <tr>
                                    <td valign="top" style=" padding-left: 10px; width: 38%;">
                                        <span id="spnspeacilinstruction" runat="server" style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                            font-style: normal; font-weight: bold; font-size: 12px;">Special Instruction:</span>
                                    </td>
                                    <td style="width: 22%;">
                                        <span id="spnestimateddispatchdate" runat="server" style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                            font-style: normal; font-weight: bold; font-size: 12px;">Estimated dispatch date:</span>
                                    </td>
                                    <td>
                                       <span id="spnestimationmethod" runat="server" style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                            font-style: normal; font-weight: bold; font-size: 12px;">Delivery method:</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <asp:Label ID="lblSpecialInstructions" runat="server" Style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                            font-style: normal; font-weight: normal; font-size: 12px; line-height: 16px;" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblEstimatedDispatchDate" runat="server" Style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                            font-style: normal; font-weight: normal; font-size: 12px; line-height: 16px;" />
                                        <%--<span id="spnpickupcollection" runat="server">pickup / collection</span>--%>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblEstimatedDeliveryMethod" runat="server" Style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                            font-style: normal; font-weight: normal; font-size: 12px; line-height: 16px;" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="height: 1px; border-bottom: 2px solid #f3f3f3;">
                            </div>
                            <div style="height: 10px; padding: 3px; width: 100%; margin-top: 5px; font-size: 15px;">
                                <span id="spnitemordered" runat="server">Items Ordered:</span>
                            </div>
                            <br />
                            <div style="padding-bottom: 20px;">
                                <asp:GridView ID="grdViewShopCart" DataKeyNames="ItemID" runat="server" OnRowDataBound="grdViewShopCart_RowDataBound"
                                    SkinID="gridViewNoHeaderSkin">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                            ItemStyle-Width="38%">
                                            <ItemTemplate>
                                                <img src="" runat="server" id="imgProduct" style="width: 40%;" alt="" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="43%">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Label ID="lblItemProductName" runat="server"
                                                        Font-Bold="true" Font-Size="12px"></asp:Label><br />
                                                         <div style=" margin-top: 5px;">
                                                        <asp:Label ID="lblPaPerName" runat="server" Font-Size="12px"></asp:Label></div>
                                                    
                                                        <div style=" margin-top: 5px; margin-bottom: 5px;">
                                                        <asp:Label ID="lblItemDesc" runat="server" Font-Size="12px"></asp:Label></div>
                                                        <br />
                                                    <asp:Label ID="lblAddonsHeading" runat="server" Font-Size="12px">with following addons :</asp:Label>
                                                </div>
                                                <asp:BulletedList Style="list-style-type: none; list-style-position: outside; list-style-image: none;
                                                    padding: 0; margin-left: 0px; color: Gray;" ID="bltSelectedAddonList" runat="server"
                                                    DisplayMode="Text">
                                                </asp:BulletedList>
                                                <br />
                                                <span>Quantity :
                                                    <%# DataBinder.Eval(Container.DataItem, "Qty1") %>
                                                </span>
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
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%--shop cart grid--%>
                            <div id="PnalTotalBox" runat="server" style="width: 98%; color: #333333; 
                                font-style: normal; font-weight: normal; font-size: 12px; line-height: 16px; padding:10px; background-color: #f3f3f3; border: 1px solid #f4f4f4;">
                                <table border="0" cellpadding="3" cellspacing="0" width="100%">
                                    <tr>
                                        <td width="38%">
                                        </td>
                                        <td width="34%">
                                        </td>
                                        <td width="10%" align="left" style="font-weight: normal; font-size: 14px; color: #A8A8A8; line-height: 2; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;">
                                            <span id="spnsubtotal" runat="server">Sub Total:</span>
                                        </td>
                                        <td  width="40%">
                                            <asp:Label ID="lblSubTotal" Text="0" runat="server" style="font-size: 17px;" />
                                        </td>
                                    </tr>
                                    <tr id="tblRowVoucher" runat="server">
                                            <td width="38%"></td>
                                        <td align="left" width="34%">
                                        <asp:Label ID="lblVoucherCode" runat="server" Text= "Discount Code: " style="color: black; font-size: 14px; font-weight:bold;"></asp:Label>
                                        <asp:Label ID="lblDicountName" runat="server" style="color: black;"></asp:Label>
                                        </td>
                                        <td align="left" >
                                            <asp:Label ID="lblVoucherDiscPercentageDisplay" Style="font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                                font-style: normal; font-weight: bold; font-size: 14px; line-height: inherit; color: #67b8db;
                                                color: black !important;" Text="Discount:" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblVoucherCodeDiscAmount" Style="color: black !important;" Text="0"
                                                runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                        </td>
                                        <td></td>
                                        <td align="left" style="font-weight: normal; font-size: 14px; color: #A8A8A8; line-height: 2; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;">
                                            <span id="spndilevry" runat="server">Delivery:</span>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDeliveryCostCenter" Text="0" runat="server" />
                                        </td>
                                    </tr>
                                    <tr id="rowVat" runat="server">
                                    <td>
                                    </td>
                                        <td align="left">
                                        </td>
                                        <td align="left" style="width: 10%; font-weight: normal; font-size: 14px; color: #A8A8A8; line-height: 2; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;">
                                            <asp:Label ID="lblTaxLabelTotal" runat="server"></asp:Label>
                                        </td>
                                        <td style="width: 40%">
                                            <asp:Label ID="lblVatTotal" Text="0" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                    <td></td>
                                        <td>
                                        </td>
                                        <td align="left">
                                            <span id="spntotal" runat="server" style="font-weight: bold; font-size:17px; ">Total:</span>
                                        </td>
                                        <td>
                                            <asp:Label Style="font-weight: bold; font-size:17px;" ID="lblGrandTotal" Text="0" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            
        </div>
        <br />
        <br />
        <br />
    </div>
        <script src="Scripts/jquery-1.9.1.min.js"></script>
   <script>
       function printMe() {
           window.print()
       }
   </script>
    </form>
</body>
</html>
