<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ANZResponse.aspx.cs" Inherits="Web2Print.UI.payments.ANZResponse" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type='text/css'>
    <!--
    h1       { font-family:Arial,sans-serif; font-size:20pt; font-weight:600; margin-bottom:0.1em; color:#08185A;}
    h2       { font-family:Arial,sans-serif; font-size:14pt; font-weight:100; margin-top:0.1em; color:#08185A;}
    h2.co    { font-family:Arial,sans-serif; font-size:24pt; font-weight:100; margin-top:0.1em; margin-bottom:0.1em; color:#08185A}
    h3       { font-family:Arial,sans-serif; font-size:16pt; font-weight:100; margin-top:0.1em; margin-bottom:0.1em; color:#08185A}
    h3.co    { font-family:Arial,sans-serif; font-size:16pt; font-weight:100; margin-top:0.1em; margin-bottom:0.1em; color:#FFFFFF}
    body     { font-family:Verdana,Arial,sans-serif; font-size:10pt; background-color:#FFFFFF; color:#08185A}
    th       { font-family:Verdana,Arial,sans-serif; font-size:8pt; font-weight:bold; background-color:#CED7EF; padding-top:0.5em; padding-bottom:0.5em;  color:#08185A}
    tr       { height:25px; }
    .shade   { height:25px; background-color:#CED7EF }
    .title   { height:25px; background-color:#0074C4 }
    td       { font-family:Verdana,Arial,sans-serif; font-size:8pt;  color:#08185A }
    td.red   { font-family:Verdana,Arial,sans-serif; font-size:8pt;  color:#FF0066 }
    td.green { font-family:Verdana,Arial,sans-serif; font-size:8pt;  color:#008800 }
    p        { font-family:Verdana,Arial,sans-serif; font-size:10pt; color:#FFFFFF }
    p.blue   { font-family:Verdana,Arial,sans-serif; font-size:7pt;  color:#08185A }
    p.red    { font-family:Verdana,Arial,sans-serif; font-size:7pt;  color:#FF0066 }
    p.green  { font-family:Verdana,Arial,sans-serif; font-size:7pt;  color:#008800 }
    div.bl   { font-family:Verdana,Arial,sans-serif; font-size:7pt;  color:#0074C4 }
    div.red  { font-family:Verdana,Arial,sans-serif; font-size:7pt;  color:#FF0066 }
    li       { font-family:Verdana,Arial,sans-serif; font-size:8pt;  color:#FF0066 }
    input    { font-family:Verdana,Arial,sans-serif; font-size:8pt;  color:#08185A; background-color:#CED7EF; font-weight:bold }
    select   { font-family:Verdana,Arial,sans-serif; font-size:8pt;  color:#08185A; background-color:#CED7EF; font-weight:bold; }
    textarea { font-family:Verdana,Arial,sans-serif; font-size:8pt;  color:#08185A; background-color:#CED7EF; font-weight:normal; scrollbar-arrow-color:#08185A; scrollbar-base-color:#CED7EF }
    -->
</style>
</head>
<body>


<asp:Panel id="Panel_Debug" runat=server>
<!-- only display these next fields if debug enabled -->
<table>
    <tr>
        <td><asp:Label id=Label_Debug runat="server"/></td>
    </tr>
    <tr>
        <td><asp:Label id=Label_DigitalOrder runat="server"/></td>
    </tr>
</table>
</asp:Panel>

<h1 align="center"><asp:Label id=Label_Title runat="server"/></h1>

<form id="Form1" runat=server>
<table align="center" border="0" width="70%">
    
    <tr class="title">
        <td colspan="2"><p><strong>&nbsp;Transaction Receipt Fields</strong></p></td>
    </tr>
    <tr>
        <td align="right"><strong><i>VPC API Version: </i></strong></td>
        <td><asp:Label id=Label_Version runat="server"/></td>
    </tr>
    <tr class='shade'>
        <td align="right"><strong><i>Command: </i></strong></td>
        <td><asp:Label id=Label_Command runat="server"/></td>
    </tr>
    <tr>
        <td align="right"><strong><em>MerchTxnRef: </em></strong></td>
        <td><asp:Label id=Label_MerchTxnRef runat="server"/></td>
    </tr>
    <tr class="shade">
        <td align="right"><strong><em>Merchant ID: </em></strong></td>
        <td><asp:Label id=Label_MerchantID runat="server"/></td>
    </tr>
    <tr>
        <td align="right"><strong><em>OrderInfo: </em></strong></td>
        <td><asp:Label id=Label_OrderInfo runat="server"/></td>
    </tr>
    <tr class="shade">
        <td align="right"><strong><em>Transaction Amount: </em></strong></td>
        <td><asp:Label id=Label_Amount runat="server"/></td>
    </tr>
    <tr>
        <td colspan="2" align="center">
            <div class='bl'>Fields above are the primary request values.<hr>Fields below are receipt data fields.</div>
        </td>
    </tr>
    <tr class="shade">
        <td align="right"><strong><em>Transaction Response Code: </em></strong></td>
        <td><asp:Label id=Label_TxnResponseCode runat="server"/></td>
    </tr>
    <tr>
        <td align="right"><strong><em>QSI Response Code Description: </em></strong></td>
        <td><asp:Label id=Label_TxnResponseCodeDesc runat="server"/></td>
    </tr>
    <tr class='shade'>
        <td align="right"><strong><i>Message: </i></strong></td>
        <td><asp:Label id=Label_Message runat="server"/></td>
    </tr>
<asp:Panel id="Panel_Receipt" runat=server>
<!-- only display these next fields if not an error -->
    <tr>
        <td align="right"><strong><em>Shopping Transaction Number: </em></strong></td>
        <td><asp:Label id=Label_TransactionNo runat="server"/></td>
    </tr>
    <tr class="shade">
        <td align="right"><strong><em>Batch Number for this transaction: </em></strong></td>
        <td><asp:Label id=Label_BatchNo runat="server"/></td>
    </tr>
    <tr>
        <td align="right"><strong><em>Acquirer Response Code: </em></strong></td>
        <td><asp:Label id=Label_AcqResponseCode runat="server"/></td>
    </tr>
    <tr class="shade">
        <td align="right"><strong><em>Receipt Number: </em></strong></td>
        <td><asp:Label id=Label_ReceiptNo runat="server"/></td>
    </tr>
    <tr>
        <td align="right"><strong><em>Authorization ID: </em></strong></td>
        <td><asp:Label id=Label_AuthorizeID runat="server"/></td>
    </tr>
    <tr class="shade">
        <td align="right"><strong><em>Card Type: </em></strong></td>
        <td><asp:Label id=Label_CardType runat="server"/></td>
    </tr>
</asp:Panel>
    <tr>
        <td colspan="2"><hr/></td>
    </tr>
    <tr class="title">
        <td colspan="2" height="25"><p><strong>&nbsp;Hash Validation</strong></p></td>
    </tr>
    <tr>
        <td align="right"><strong><em>Hash Validated Correctly: </em></strong></td>
        <td><asp:Label id=Label_HashValidation runat="server"/></td>
    </tr>
<asp:Panel id="Panel_StackTrace" runat=server>
<!-- only display these next fields if an stacktrace output exists-->
    <tr>
        <td colspan="2">&nbsp;</td>
    </tr>
    <tr class="title">
        <td colspan="2"><p><strong>&nbsp;Exception Stack Trace</strong></p></td>
    </tr>
    <tr>
        <td colspan="2"><asp:Label id=Label_StackTrace runat="server"/></td>
    </tr>
</asp:Panel>
    <tr>
        <td width="50%">&nbsp;</td>
        <td width="50%">&nbsp;</td>
    </tr>
    <tr>
        <td colspan="2" align="center"><asp:Label id=Label_AgainLink runat="server"/></td>
    </tr>

</table>
</form>

</body>
</html>
