<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderDetails.ascx.cs"
    Inherits="Web2Print.UI.Controls.OrderDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<link href="../LightBox/css/jquery.lightbox-0.5.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    $(document).ready(function () {
        BindEvents();
    });

    function BindEvents() {
        
        $('.gallery a').lightBox({
        
            maxHeight: 500,
            maxWidth: 700
        });
        $(".Fsize13").css("background-color", "#3D3D3D");
        $(".Fsize13").css("color", "White");
    }
</script>
 <asp:Label ID="lblHeader" runat="server" Text="Order Details" CssClass="left_align FileUploadHeaderText_PopUp float_left_simple MesgBoxClass"></asp:Label>
     
       <div id="CancelControl" onclick="$find('mpeOrderDetails').hide();" class="MesgBoxBtnsDisplay rounded_corners5" style="">
        Close
    </div>
<div class="clearBoth">
        &nbsp;
        </div>
        <div class="SolidBorderCS">
        &nbsp;
        </div>
<div class="popupInnerContainer rounded_corners" style="height: 530px; overflow: auto; z-index:0; margin-top:10px;">
    <table border="0" width="100%" cellpadding="4" cellspacing="0">
        <tbody>
            <tr>
                <td>
                    <table border="0" cellpadding="3" cellspacing="0" width="100%">
                        <tbody>
                            <tr>
                                <td class="textAlignRight" style="width: 90px">
                                    <span class="simpleTextBold" id="spnordernum" runat="server">Order Number:</span>
                                </td>
                                <td class="textAlignLeft simpleText">
                                    <asp:Label ID="lblOrderNumber" runat="server" CssClass="txtBold" />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td class="textAlignRight">
                                    <span class="simpleTextBold" id="spnorderstatus" runat="server" >Status:</span>
                                </td>
                                <td class="textAlignLeft simpleText" style="width: 200px">
                                    <asp:Label ID="lblOrderStatus" runat="server" CssClass="txtBold clrOrderDetail"  />
                                   
                                </td>
                            </tr>
                            <tr>
                                <td class="textAlignRight">
                                    <span class="simpleTextBold" id="spnorderdate" runat="server">Order Date:</span>
                                </td>
                                <td class="textAlignLeft simpleText">
                                    <asp:Label ID="lblOrderDate" runat="server" CssClass="txtBold"  />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td class="textAlignRight">
                                    <span class="simpleTextBold" id="spnClientStatus" runat="server" >Status:</span>
                                </td>
                                <td class="textAlignLeft simpleText" style="width: 200px">
                                    <asp:DropDownList ID="AllOrderStatuses" runat="server" CssClass="dropdown rounded_corners5"
                                        Width="107px">
                                    </asp:DropDownList>
                                    <asp:Button ID="btnClientStatusUpdate" runat="server" OnClick="btnClientStatusUpdate_Click" 
                                    CssClass="start_creating_btn rounded_corners5" Width="80px" Text="Update"/>
                                </td>
                            </tr>
                            <tr>
                                <td class="textAlignRight">
                                    <span class="simpleTextBold" id="Span1" runat="server">Placed By:</span>
                                </td>
                                <td class="textAlignLeft simpleText">
                                    <asp:Label ID="lblPlacedBy" runat="server" CssClass="txtBold"  />
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                 <td class="textAlignRight">
                                    <span class="simpleTextBold" id="spndileverydate" runat="server">Delivery Date:</span>
                                </td>
                                <td class="textAlignLeft simpleText">
                                    <asp:Label ID="lblOrderDeliveryStartDate" runat="server" CssClass="txtBold"  />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="cntPLOrderDetails">
                        <asp:GridView ID="grdViewShopCart" SkinID="ProductOrderHistoryGrid" DataKeyNames="ItemID"
                            runat="server" OnRowDataBound="grdViewShopCart_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top">
                                    <ItemTemplate>
                                        <table border="0" cellpadding="2" cellspacing="0">
                                            <tr>
                                                <td valign="middle">
                                                    <input type="hidden" id="txtHdnFileName" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "Attatchment.FileName")%>' />
                                                    <div  class="gallery">
                                                    <a id="lnkPdfFile" runat="server" href='<%# DataBinder.Eval(Container.DataItem, "Attatchment.FolderPath")%>'>
                                                        <asp:Image ID="imgPdf" SkinID="imgArtWorkIcon" runat="server"/></a></div>
                                                </td> 
                                                <td valign="middle">
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product Description" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <div>
                                            <span><strong>
                                                <%# DataBinder.Eval(Container.DataItem, "ProductName")%></strong></span>
                                            <asp:Label ID="lblAddonsHeading" runat="server">with following addons :</asp:Label>
                                        </div>
                                        <asp:BulletedList CssClass="UList" ID="bltSelectedAddonList" runat="server" DisplayMode="Text">
                                        </asp:BulletedList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <span>
                                            <%# DataBinder.Eval(Container.DataItem, "Qty1") %></span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Price" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Qty1BaseCharge1") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="VAT" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblVatStateTax" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalItemPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Qty1GrossTotal") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </td>
            </tr>
            <tr id="PnalTotalBox" runat="server">
                <td align="right">
                    <div class="whitebackground textAlignLeft rounded_corners BillShipAddressesControl simpleText">
                        <div class="padding10">
                            <table border="0" cellpadding="3" cellspacing="0" width="200px">
                                <tr>
                                    <td align="left">
                                        <span id="spnsubtotal" runat="server">Sub Total:</span>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblSubTotal" Text="0" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 80%">
                                        <span id="spnDeliveryoptn" runat="server">Deivery Cost:</span>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblDeliveryCost" Text="0" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 80%">
                                        <span id="spnvattotal" runat="server">Vat Total:</span>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblVatTotal" Text="0" runat="server"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <span class="CartFonts" id="spngrndtotal" runat="server">Grand Total:</span>
                                    </td>
                                    <td>
                                        <asp:Label CssClass="CartFonts txtBold" ID="lblGrandTotal" Text="0" runat="server"/>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <table id="PnlAddresses" runat="server" border="0" cellpadding="0" cellspacing="0"
                            width="100%">
                            <tr>
                                <td>
                                    <%-- Billing Address--%>
                                    <div class="whitebackground textAlignLeft rounded_corners BillShipAddressesControl simpleText">
                                        <div class="product_detail_sup_padding">
                                            <table border="0" cellpadding="2" cellspacing="0" width="100%">
                                                <thead>
                                                    <tr>
                                                        <th colspan="2" style="height: 25px; text-align: left;">
                                                            <asp:Label ID="lblMainControlHeading" runat="server" Text="Billing Address" CssClass="heading_small" />
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 33%">
                                                            <span id="spnbillingname" runat="server">Billing Name:</span>
                                                        </td>
                                                        <td style="width: 67%">
                                                            <asp:Label ID="lblBillingName" runat="server" CssClass="txtBold"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <span id="ltrladdressline1" runat="server">Address line 1:</span>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblAddressLine1" runat="server" CssClass="txtBold" ></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <span id="ltrladdressline2" runat="server">Address line 2:</span>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblAddressLine2" runat="server" CssClass="txtBold"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <span id="ltrlcity" runat="server">City:</span>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCity" runat="server" CssClass="txtBold"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <span id="ltrlstate" runat="server">State:</span>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblState" runat="server" CssClass="txtBold"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <span id="ltrlzipcodeP" runat="server">Zip Code / Post Code:</span>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblZipPostCode" runat="server" CssClass="txtBold"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <span id="ltrlcountry" runat="server">Country:</span>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCountry" runat="server" CssClass="txtBold"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <span id="ltrlcontactnum" runat="server">Contact Number:</span>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblContactNumber" runat="server" CssClass="txtBold"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                </td>
                                <td align="right">
                                    <%-- Shipping Address--%>
                                    <div class="whitebackground textAlignLeft rounded_corners BillShipAddressesControl simpleText">
                                        <div class="product_detail_sup_padding">
                                            <table border="0" cellpadding="2" cellspacing="0" width="100%">
                                                <thead>
                                                    <tr>
                                                        <th colspan="2" style="height: 25px; text-align: left;">
                                                            <asp:Label ID="Label1" runat="server" Text="Shipping Address" CssClass="heading_small" />
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 33%">
                                                            <span id="spnshppingname" runat="server">Shipping Name:</span>
                                                        </td>
                                                        <td style="width: 67%">
                                                            <asp:Label ID="lblShipAddressName" runat="server" CssClass="txtBold"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <span id="spnaddressline2nd" runat="server">Address line 1:</span>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblShipAddLine1" runat="server" CssClass="txtBold"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <span id="spnaddressline2nd2" runat="server">Address line 2:</span>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblShipAddressLine2" runat="server" CssClass="txtBold"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <span id="spncity2" runat="server">City:</span>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblShipAddCity" runat="server" CssClass="txtBold"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <span id="spnstate2" runat="server">State:</span>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblShipState" runat="server" CssClass="txtBold"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <span id="spnzipcoeP2" runat="server">Zip Code / Post Code:</span>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblShipPostCode" runat="server" CssClass="txtBold"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <span id="spncountry2" runat="server">Country:</span>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblShipCountry" runat="server" CssClass="txtBold"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <span id="spncontactnum2" runat="server">Contact Number:</span>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblShipContact" runat="server" CssClass="txtBold"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </tbody>
    </table>
</div>
<asp:Label ID="lblProdDescHeadingText" runat="server" Visible="false"></asp:Label>
<asp:Label ID="lblQuantityHeaderTxt" runat="server" Visible="false"></asp:Label>
<asp:Label ID="lblPriceHeaderTxt" runat="server" Visible="false"></asp:Label>
<asp:Label ID="lblVatHeadingText" runat="server" Visible="false"></asp:Label>
<asp:Label ID="lblTotalHeadingText" runat="server" Visible="false"></asp:Label>
