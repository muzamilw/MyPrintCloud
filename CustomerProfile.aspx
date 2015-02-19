<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerProfile.aspx.cs"
    Inherits="Web2Print.UI.CustomerProfile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <style>
        #divShd
        {
            z-index: 999 !important;
        }
    </style>
</head>
<body class="CustomerProfileCS">
    <form id="form1" runat="server">
         <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="jquery-migrate" />

                <asp:ScriptReference Name="WebUIValidation.js" Assembly="System.Web" />
            </Scripts>
        </asp:ScriptManager>
    <div class="white_background pad20" style="z-index:100000 !important;">
                <asp:Label ID="lblCCHeaderTxt" runat="server" CssClass="left_align FileUploadHeaderText_PopUp float_left_simple MesgBoxClass">Customer Details</asp:Label>
        <div class="clearBoth">
        &nbsp;
        </div>
        <div class="SolidBorderCS spacer10pxtop">
        &nbsp;
        </div>
        <div class="rounded_corners" style="height: 472px; overflow: auto; clear: both; background-color: #FFFFFF; height: auto; z-index: 0;">
            <table border="0" width="100%" cellpadding="4" cellspacing="3">
                <tbody>
                    <tr>
                        <td>
                            <div>
                                <table id="PnlAddresses" runat="server" border="0" cellpadding="3" cellspacing="0"
                                    width="100%">
                                    <tr>
                                        <td valign="top">
                                            <%--Company info--%>
                                            <div class="rounded_corners  simpleText" style="text-align:left; padding-left:50px;">
                                                <div class="product_detail_sup_padding rounded_corners " style="text-align:left; padding-left:60px;">
                                                    <div style="float:right;">
                                                    <asp:Image ID="imgCompanyLogo" runat="server" CssClass="widhtCP_CSLogo" style="float:right;" />
                                                    </div>
                                                    <div style="float:left; width:75%">
                                                     <table border="0" cellpadding="2" cellspacing="0" width="100%"">
                                                        <thead>
                                                            <tr>
                                                                <th colspan="2" style="height: 25px; text-align: left;">
                                                                    <asp:Label ID="lblCompanyInfo" runat="server" Text="Company Details" CssClass="heading_small" />
                                                                </th>
                                                            </tr>
                                                           
                                                        </thead>
                                                        <tbody>
                                                            <tr>
                                                                <td class="TLR">
                                                                    <span id="spnCname" runat="server">Company Name:</span>
                                                                </td>
                                                                <td class="labelAlignedleft">
                                                                    <asp:Label ID="lblCName" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TLR">
                                                                    <span id="spnCompAccNo" runat="server">Account No:</span>
                                                                </td>
                                                                 <td class="labelAlignedleft">
                                                                    <asp:Label ID="lblCompAccNo" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TLR">
                                                                    <span id="spnCompURL" runat="server">URL:</span>
                                                                </td>
                                                                   <td class="labelAlignedleft">
                                                                    <asp:Label ID="lblCompURL" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TLR">
                                                                    <span id="spnCompAccOpenDate" runat="server">Account Open Date:</span>
                                                                </td>
                                                                  <td class="labelAlignedleft">
                                                                    <asp:Label ID="lblCompAccOpenDate" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>

                                                             <tr>
                                                                <th colspan="2" style="height: 25px; text-align: left;">
                                                                    <asp:Label ID="Label2" runat="server" Text="Contact Details" CssClass="heading_small" />
                                                                </th>
                                                            </tr>
                                                             
                                                            <tr>
                                                                <td class="TLR">
                                                                    <span id="spnContFName" runat="server">First Name:</span>
                                                                </td>
                                                                  <td class="labelAlignedleft">
                                                                    <asp:Label ID="lblFName" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TLR">
                                                                    <span id="spnContLName" runat="server">Last Name:</span>
                                                                </td>
                                                                   <td class="labelAlignedleft">
                                                                    <asp:Label ID="lblContLName" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TLR">
                                                                    <span id="spnContEmail" runat="server">Email:</span>
                                                                </td>
                                                                  <td class="labelAlignedleft">
                                                                    <asp:Label ID="lblContEmail" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TLR">
                                                                    <span id="spnjobT" runat="server">Job Title:</span>
                                                                </td>
                                                                 <td class="labelAlignedleft">
                                                                    <asp:Label ID="lblContjobT" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TLR">
                                                                    <span id="spnContMob" runat="server">Mobile:</span>
                                                                </td>
                                                                 <td class="labelAlignedleft">
                                                                    <asp:Label ID="lblContMob" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TLR">
                                                                    <span id="spnContFax" runat="server">Fax:</span>
                                                                </td>
                                                                  <td class="labelAlignedleft">
                                                                    <asp:Label ID="lblContFax" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>


                                                            <tr>
                                                                <th colspan="2" style="height: 25px; text-align: left;">
                                                                    <asp:Label ID="Label1" runat="server" Text="Address Details" CssClass="heading_small" />
                                                                </th>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" align="left">
                                                                    <asp:Repeater ID="rptContactAddress" runat="server" OnItemDataBound="rptContactAddress_ItemDataBound">
                                                                        <ItemTemplate>
                                                                            <div id="MainContainer" class="page_border_div rounded_corners simpleText" runat="server" style="width:100%">
                                                                                 <table border="0" cellpadding="2" cellspacing="0" width="100%">
                                                                                  
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td class="TLR">
                                                                                                    <span id="spnbillingname" runat="server">Address:</span>
                                                                                                </td>
                                                                                                <td class="labelAlignedleft">
                                                                                                    <asp:Label ID="lblBillingName" runat="server" Text='<%#Eval("AddressName","{0}") %>'></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="TLR">
                                                                                                    <span id="ltrladdressline1" runat="server">Address line 1:</span>
                                                                                                </td>
                                                                                                <td class="labelAlignedleft">
                                                                                                    <asp:Label ID="lblAddressLine1" runat="server" Text='<%#Eval("Address1","{0}") %>'></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="TLR">
                                                                                                    <span id="ltrladdressline2" runat="server">Address line 2:</span>
                                                                                                </td>
                                                                                                <td class="labelAlignedleft">
                                                                                                    <asp:Label ID="lblAddressLine2" runat="server" Text='<%#Eval("Address2","{0}") %>'></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="TLR">
                                                                                                    <span id="ltrlcity" runat="server">City:</span>
                                                                                                </td>
                                                                                                <td class="labelAlignedleft">
                                                                                                    <asp:Label ID="lblCity" runat="server" Text='<%#Eval("City","{0}") %>'></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="TLR">
                                                                                                    <span id="ltrlstate" runat="server">State:</span>
                                                                                                </td>
                                                                                                <td class="labelAlignedleft">
                                                                                                    <asp:Label ID="lblState" runat="server" Text='<%#Eval("State","{0}") %>'></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="TLR">
                                                                                                    <span id="ltrlzipcodeP" runat="server">Zip Code / Post Code:</span>
                                                                                                </td>
                                                                                                <td class="labelAlignedleft">
                                                                                                    <asp:Label ID="lblZipPostCode" runat="server" Text='<%#Eval("PostCode","{0}") %>'></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="TLR">
                                                                                                    <span id="ltrlcountry" runat="server">Country:</span>
                                                                                                </td>
                                                                                                <td class="labelAlignedleft">
                                                                                                    <asp:Label ID="lblCountry" runat="server" Text='<%#Eval("Country","{0}") %>'></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="TLR">
                                                                                                    <span id="ltrlcontactnum" runat="server">Contact Number:</span>
                                                                                                </td>
                                                                                                <td class="labelAlignedleft">
                                                                                                    <asp:Label ID="lblContactNumber" runat="server" Text='<%#Eval("Tel1","{0}") %>'></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </tbody>
                                                                                    </table>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                </td>
                                                            </tr>




                                                        </tbody>
                                                    </table>
                                                    </div>
                                                    
                                                   
                                                   
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
    </div>
    </form>
</body>
</html>
