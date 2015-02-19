<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PinkReceipt.aspx.cs" Inherits="Web2Print.UI.PinkReceipt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="PinkCardOrderReceipt" runat="server">
    <div id="page" style="width: 960px; margin: auto; border: 0px solid orange;">
        <div style="padding: 1px 0px 1px 0px;">
            <div style="width: 100%;">
                <div style="background-color: #3d3d3d; padding: 5px; padding-bottom: 10px; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                    font-style: normal; font-weight: normal; font-size: 12px; -moz-border-radius: 10px;
                    -webkit-border-radius: 10px; -khtml-border-radius: 10px; border-radius: 10px;">
                    <div style="padding: 15px; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                        font-style: normal; font-weight: bold; font-size: 13px; line-height: 13px; color: #ffffff;">
                        <asp:Literal ID="ltrlreceipt" runat="server" Text="Receipt"></asp:Literal>
                        &nbsp;<asp:Label ID="lblReceiptNumber" runat="server" /></div>
                </div>
            </div>
            <div style="background-color: White; padding: 10px; text-align: left; -moz-border-radius: 10px;
                -webkit-border-radius: 10px; -khtml-border-radius: 10px; border-radius: 10px;">
                <div style="width: 100%; text-align: center; font-size: 16px; font-weight: bold;
                    color: #C72965;">
                    <asp:Literal ID="ltrlthNKU" runat="server" Text="Thank You"></asp:Literal>
                &nbsp;for Payment</div>
                <div style="width: 100%; text-align: center; font-size: 16px; font-weight: bold;
                    color: #C72965;">
                    <asp:Literal ID="ltrlConfrmMesgd" runat="server" Text="Your Registration is complete and your account manager will call you within 24 hours and
                            activate your Pinkcards.com store"></asp:Literal>
                </div>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="8" width="100%">
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td colspan="1" valign="top">

                                        <asp:Image ID="Companyimg" runat="server" CssClass="IframeCompanyLogoCs_AddSelectCS" />
                                        <br />
                                         <asp:Literal ID="ltrlCompanyName" runat="server"></asp:Literal><br />
                                                    <asp:Label ID="lblAddLine1" runat="server" Style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                                        font-style: normal; font-weight: normal; font-size: 12px; line-height: 16px;" />
                                                    <asp:Label ID="lblAddLine2" runat="server" Style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                                        font-style: normal; font-weight: normal; font-size: 12px; line-height: 16px;" /><br />
                                                    <asp:Label ID="lblTown" runat="server" Style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                                        font-style: normal; font-weight: normal; font-size: 12px; line-height: 16px;" />
                                                    <asp:Label ID="lblState" runat="server" Style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                                        font-style: normal; font-weight: normal; font-size: 12px; line-height: 16px;" />
                                                    <asp:Label ID="lblZipCode" runat="server" Style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                                        font-style: normal; font-weight: normal; font-size: 12px; line-height: 16px;" />
                                                    <asp:Label ID="lblCountry" runat="server" Style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                                        font-style: normal; font-weight: normal; font-size: 12px; line-height: 16px;" />
                                                        <br />
                                                        
                                                    <asp:Label ID="lblTel" runat="server" Style="color: #207DB8; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                                        font-weight: bold; font-style: normal; font-weight: normal; font-size: 15px;
                                                        line-height: 16px; float: left; margin-top: 4px;" />
                                                        <br />
                                        <asp:Label ID="lblTaxName" runat="server"></asp:Label>
                                        <asp:Label ID="lblVatNum" runat="server" Style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                            font-style: normal; font-weight: normal; font-size: 12px; line-height: 16px;" />
                                    </td>
                                    <td colspan="1">
                                        <table border="0" cellpadding="3" cellspacing="0" width="100%" style="margin-left: -4px;
                                            margin-top: 4px;">
                                            <tr>
                                                <td colspan="2">
                                                    <span id="spnbilledto" runat="server" style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                                        font-style: normal; font-weight: bold; font-size: 12px;">Billed To:</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblCustName" runat="server" Style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                                        font-style: normal; font-weight: normal; font-size: 12px; line-height: 16px;" />
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblBAddressLine1" runat="server" Style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                                        font-style: normal; font-weight: normal; font-size: 12px; line-height: 16px;" />
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblAddressLine2" runat="server" Style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                                        font-style: normal; font-weight: normal; font-size: 12px; line-height: 16px;" />
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblBcity" runat="server" Style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                                        font-style: normal; font-weight: normal; font-size: 12px; line-height: 16px;" />
                                                    <asp:Label ID="lblBState" runat="server" Style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                                        font-style: normal; font-weight: normal; font-size: 12px; line-height: 16px;" />
                                                    <asp:Label ID="lblBZipCode" runat="server" Style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                                        font-style: normal; font-weight: normal; font-size: 12px; line-height: 16px;" />
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblBCountry" runat="server" Style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                                        font-style: normal; font-weight: normal; font-size: 12px; line-height: 16px;" />
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <span>
                                                        <asp:Literal ID="lrtlBTel" runat="server" Text="Tel:"></asp:Literal>
                                                    </span>
                                                    <asp:Label ID="lblBTel" runat="server" Style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                                        font-style: normal; font-weight: normal; font-size: 12px; line-height: 16px;" />
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td colspan="1" valign="top">
                                                                                <span id="spnorderdate" runat="server" style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                            font-style: normal; font-weight: bold; font-size: 12px;">Registrtion date: </span>
                                        <asp:Label ID="lblOrderDate" runat="server" Style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                            font-style: normal; font-weight: normal; font-size: 12px; line-height: 16px;" /><br />
                                        <span id="spnOrderCode" runat="server" style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                            font-style: normal; font-weight: bold; font-size: 12px; margin-bottom: 4px;">Registration
                                            Code: </span>
                                        <asp:Label ID="lblOrderCode" runat="server" Style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                            font-style: normal; font-weight: normal; font-size: 12px; line-height: 10px;" /><br />
                                       
                                        <span id="spnplacedby" runat="server" style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                            font-style: normal; font-weight: bold; font-size: 12px;">Placed by: </span>
                                        <asp:Label ID="lblPlacedBy" runat="server" Style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                            font-style: normal; font-weight: normal; font-size: 12px; line-height: 16px;" /><br /></td>
                                </tr>
                            </table>
                            <br />
                            <div style="height: 1px; border-bottom: 1px dotted #BEBEBE;">
                            </div>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table border="0" cellpadding="3" cellspacing="0" width="85%">
                                <tr>
                                    <td valign="top">
                                        <span id="spnspeacilinstruction" runat="server" style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                            font-style: normal; font-weight: bold; font-size: 12px;">Comments:</span>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <asp:Label ID="lblSpecialInstructions" runat="server" Style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                            font-style: normal; font-weight: normal; font-size: 12px; line-height: 16px;" />
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                            <div style="height: 1px; border-bottom: 1px dotted #BEBEBE;">
                            </div>
                            <div style=" padding: 3px; width: 100%;">
                                <span id="spnitemordered" runat="server" style="color:#ec008c;font-size:16px;">Postcodes Reserved:</span>
                            </div>
                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%--shop cart grid--%>
                            <div id="PnalTotalBox" runat="server" style="width: 100%; color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif;
                                font-style: normal; font-weight: normal; font-size: 12px; line-height: 16px;">
                                <table border="0" cellpadding="3" cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td align="left">
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                <asp:Label ID="lblPostCodes" runat="server" Font-Bold="True" Font-Size="16px" 
                                                ForeColor="#EC008C"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <span id="spnsubtotal" runat="server">Total:</span>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblSubTotal" Text="0" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="width: 68%">
                                        </td>
                                        <td align="left" style="width: 17%">
                                            <asp:Label ID="lblTaxLabelTotal" runat="server"></asp:Label>
                                        </td>
                                        <td style="width: 15%">
                                            <asp:Label ID="lblVatTotal" Text="0" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td align="left">
                                            <span id="spntotal" runat="server" style="font-weight: bold;">Total:</span>
                                        </td>
                                        <td>
                                            <asp:Label Style="font-weight: bold;" ID="lblGrandTotal" Text="0" runat="server" />
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
    </form>
</body>
</html>
