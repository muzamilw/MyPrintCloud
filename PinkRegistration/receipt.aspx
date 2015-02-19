﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="receipt.aspx.cs" MasterPageFile="PinkRegister.Master"
    Inherits="Web2Print.UI.PinkRegReceipt" %>

<%@ Register Src="PinkRegFooter.ascx" TagName="PinkRegFooter" TagPrefix="uc1" %>
<%@ Register Src="~/PinkRegistration/PinkRegFooter.ascx" TagPrefix="uc2" TagName="PinkRegFooter" %>
<%@ Register Src="~/PinkRegistration/Header.ascx" TagPrefix="uc1" TagName="Header" %>

<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="cphHeader">
    <link href="AllSite.css" rel="stylesheet" />
    <link href="PinkSite.css" rel="stylesheet" />
    <uc1:Header runat="server" ID="Header" />
</asp:Content>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainContent">
    <div class="content_area">
        <div class="left_right_padding">
            
            <div class="signin_heading_div PinkRegHead1">
                Want to be a local PinkCards.com supplier
            </div>
            <div style="float: right; padding: 10px; padding-top: 14px; text-decoration: underline;display:none">
                        <a id="lblPrintReceipt" runat="server" href="#" onclick="PrintReceipt();">Print this
                            receipt</a>
                    </div>
            <div class="page_border_div  rounded_corners PinkRegFramePadding">
                <div>
                    <div style="padding-right: 30px;">
                        <div id="SubHead" runat="server" class="PinkRegHead2" visible="true">
                            Your web store is live.
                        </div>
                        <div class="PinkRegHead3">
                            Your receipt has been emailed to :
                            <asp:Literal ID="litEmail" runat="server"></asp:Literal>
                        </div>
                        <br /><br />
                        <p id="OnBoardtxt" runat="server" class="PinkRegP">
                            
                        </p>

                        <%--<div class="PinkRegHead3">
                            Now lets make it pink;d up.
                        </div>--%>

                        <p id="OnBoardSecdPara" runat="server" class="PinkRegP">
                            Your account manager will call you within 24 hours time to explain the next steps.
                        </p>

                        <p class="headingsAvenior">
                            Lets starts marketing collaboratively to generate traffic to your new Web 2 Print store.
                    <%-- </br>
                     i.e.
                     <br />
                            Your company logo available PNG Format 450 x 80 pixels.
                     <br />
                            Main sales phone number
                     <br />
                            Main sales managers name, email address and phone numbers.
                     <br />
                            Production Managers Name, email address and phone numbers.--%>

                        </p>

                        <br />
                        <br />

                        <div id="page" style="width: 960px; margin: auto; border: 0px solid orange; display:none">
                            <div style="padding: 1px 0px 1px 0px;">
                                <div style="width: 100%;">
                                    <div style="background-color: #3d3d3d; padding: 5px; padding-bottom: 10px; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: normal; font-size: 12px; -moz-border-radius: 10px; -webkit-border-radius: 10px; -khtml-border-radius: 10px; border-radius: 10px;">
                                        <div style="padding: 15px; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 13px; line-height: 13px; color: #ffffff; text-align: center">
                                            Order Confirmed
                                        </div>
                                    </div>
                                </div>
                                <div style="background-color: White; padding: 10px; text-align: left; -moz-border-radius: 10px; -webkit-border-radius: 10px; -khtml-border-radius: 10px; border-radius: 10px;">

                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td>
                                                <table border="0" cellpadding="0" cellspacing="8" width="100%">
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>

                                                    <tr>
                                                        <td colspan="1" valign="top">

                                                            <asp:Image ID="Companyimg" runat="server" CssClass="IframeCompanyLogoCs_AddSelectCS" />
                                                            <br />
                                                            <asp:Literal ID="ltrlCompanyName" runat="server"></asp:Literal><br />
                                                            <asp:Label ID="lblAddLine1" runat="server" Style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: normal; font-size: 12px; line-height: 31px;" />
                                                            <asp:Label ID="lblAddLine2" runat="server" Style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: normal; font-size: 12px; line-height: 31px;" /><br />
                                                            <asp:Label ID="lblTown" runat="server" Style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: normal; font-size: 12px; line-height: 31px;" />
                                                            <asp:Label ID="lblState" runat="server" Style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: normal; font-size: 12px; line-height: 31px;" />
                                                            <asp:Label ID="lblZipCode" runat="server" Style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: normal; font-size: 12px; line-height: 31px;" />
                                                            <asp:Label ID="lblCountry" runat="server" Style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: normal; font-size: 12px; line-height: 31px;" />
                                                            
                                                            <div class="clearboth" style="height:1px;">
                                                                &nbsp;
                                                            </div>
                                                            <asp:Label ID="lblTaxName" runat="server"  Style="line-height: 31px;"></asp:Label>
                                                            <asp:Label ID="lblVatNum" runat="server" Style="line-height: 31px;"/>
                                                              <div class="clearboth" style="height:1px;">
                                                                &nbsp;
                                                            </div>
                                                            <asp:Label ID="lblTel" runat="server" Style="color: black; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-weight: bold; font-style: normal; font-weight: normal; font-size: 15px; line-height: 31px; float: left; margin-top: 4px;" />
                                                        </td>
                                                        <td colspan="1">
                                                            <table border="0" cellpadding="3" cellspacing="0" class="pnkRegBilltedTo" width="100%" >
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <span id="spnbilledto" runat="server" style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 12px;">Billed To:</span>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblCustName" runat="server" Style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: normal; font-size: 12px; " />
                                                                    </td>
                                                                    <td></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblBAddressLine1" runat="server" Style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: normal; font-size: 12px; " />
                                                                    </td>
                                                                    <td></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblAddressLine2" runat="server" Style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: normal; font-size: 12px; " />
                                                                    </td>
                                                                    <td></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblBcity" runat="server" Style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: normal; font-size: 12px; " />
                                                                        <asp:Label ID="lblBState" runat="server" Style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: normal; font-size: 12px; " />
                                                                        <asp:Label ID="lblBZipCode" runat="server" Style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: normal; font-size: 12px; " />
                                                                    </td>
                                                                    <td></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblBCountry" runat="server" Style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: normal; font-size: 12px; " />
                                                                    </td>
                                                                    <td></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <span style="font-size: 12px !important;">
                                                                            <asp:Literal ID="lrtlBTel" runat="server" Text="Tel:"></asp:Literal>
                                                                        </span>
                                                                        <asp:Label ID="lblBTel" runat="server" Style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: normal; font-size: 12px; " />
                                                                    </td>
                                                                    <td></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td colspan="1" valign="top" class="pnkregthirdpnl">
                                                            <span id="spnorderdate" runat="server" style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 12px;">Order date: </span>
                                                            <asp:Label ID="lblOrderDate" runat="server" Style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: normal; font-size: 12px;" /><br />
                                                            <span id="spnOrderCode" runat="server" style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 12px; margin-bottom: 4px;">Order Code: </span>
                                                            <asp:Label ID="lblOrderCode" runat="server" Style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: normal; font-size: 12px;" /><br />

                                                            <span id="spnplacedby" runat="server" style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 12px;">Placed by: </span>
                                                            <asp:Label ID="lblPlacedBy" runat="server" Style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: normal; font-size: 12px;" /><br />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br />
                                                <div style="height: 1px; border-bottom: 1px dotted #BEBEBE;">
                                                </div>
                                                <br />
                                            </td>
                                        </tr>
                                        <%--<tr>
                                            <td>
                                                <table border="0" cellpadding="3" cellspacing="0" width="85%">
                                                    <tr>
                                                        <td valign="top">
                                                            <span id="spnspeacilinstruction" runat="server" style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: bold; font-size: 12px;">Comments:</span>
                                                        </td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
                                                            <asp:Label ID="lblSpecialInstructions" runat="server" Style="color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: normal; font-size: 12px; line-height: 16px;" />
                                                        </td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td>
                                                <br />
                                                 <br />
                                              <%--  <div style="height: 1px; border-bottom: 1px dotted #BEBEBE;">
                                                </div>--%>
                                                <div style="padding: 3px; width: 100%;">
                                                    <span id="spnitemordered" runat="server" style="color: #ec008c; font-size: 16px;">Postcodes Reserved:</span>
                                                </div>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <%--shop cart grid--%>
                                                <div id="PnalTotalBox" runat="server" style="width: 100%; color: #333333; font-family: Trebuchet MS, Arial, Helvetica, sans-serif; font-style: normal; font-weight: normal; font-size: 12px; line-height: 27px;">
                                                    <table border="0" cellpadding="3" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td align="left">&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblPostCodes" runat="server" Font-Bold="True" Font-Size="16px"
                                                                    ForeColor="#EC008C"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                <span id="spnsubtotal" runat="server">Total:</span>
                                                            </td>
                                                            <td  style="text-align:right;">
                                                                <asp:Label ID="lblSubTotal" Text="0" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="width: 67%">
                                                                <asp:Label ID="nextCollectionlbl" runat="server"></asp:Label>
                                                            </td>
                                                            <td align="left" style="width: 17%">
                                                                <asp:Label ID="lblTaxLabelTotal" runat="server"></asp:Label>
                                                            </td>
                                                            <td style="width: 15%; text-align:right" align="right">
                                                                <asp:Label ID="lblVatTotal" Text="0" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                            <td align="left">
                                                                <span id="spntotal" runat="server" style="font-weight: bold;">Total:</span>
                                                            </td>
                                                            <td style="text-align:right;">
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

                    </div>

                    <div class="clear">
                    </div>

                </div>
            </div>
        </div>
    </div>
      <script type="text/javascript" language="javascript">

          function PrintReceipt() {

              window.print();
          }

    </script>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="cphFooter">
    <uc2:PinkRegFooter runat="server" ID="PinkRegFooter" />
</asp:Content>

