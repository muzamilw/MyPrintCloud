<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignupPaypal.aspx.cs" Inherits="Web2Print.UI.SignupPaypal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1250" />
    <title>Web2Print PayPal</title>
    
</head>
<body>

<form runat="Server">
<asp:ScriptManager ID="ScriptManager2" runat="server">
            <Scripts>

                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="jquery-migrate" />

            </Scripts>
        </asp:ScriptManager>
</form>
    <form id="payForm"  method="post" action="<%Response.Write (URL);%>" >
        
    <input type="hidden" name="cmd" value="<%Response.Write (cmd);%>" />
    <input type="hidden" name="business" value="<%Response.Write (business);%>" />
    <input type="hidden" name="upload" value="1" />
    <input type="hidden" name="custom" value="<%Response.Write (pageOrderID);%>" />
    <input type="hidden" name="no_shipping" value="<%Response.Write (no_shipping);%>" />
    <input type="hidden" name="return" value="<%Response.Write (return_url);%>" />
    <input type="hidden" name="rm" value="<%Response.Write (rm);%>" />
    <input type="hidden" name="notify_url" value="<%Response.Write (notify_url);%>" />
    <input type="hidden" name="cancel_return" value="<%Response.Write (cancel_url);%>" />
    <input type="hidden" name="currency_code" value="<%Response.Write (currency_code);%>" />
    <input type="hidden" name="handling_cart" value="<%Response.Write (handling_cart);%>" />
    <input type="hidden" name="discount_amount_cart" value="0" />
    <input type="hidden" runat="server" id="txtHiddenJason" />

    <div style="text-align:center; height:600px; margin-top:300px; font:arial; font-size:16px;">Please wait while we redirect you to PayPal.</div>
   
    </form>
    <script language="javascript" type="text/javascript">

        $(document).ready(function () {

            var jasonData = jQuery.parseJSON($('#<%=txtHiddenJason.ClientID %>').val());
            var objData = null;
            $(jasonData).each(function (indx) {

                objData = jasonData[indx];

                var priceValue = (objData.UnitPrice).toString();
                var prodName = (objData.ProductName).toString();
                var prodQuantity = (objData.TotalQuantity).toString();

                var ctr0 = $('<input/>').attr({ type: 'hidden', name: 'quantity_' + (indx + 1).toString(), value: prodQuantity.toString() });
                $("#payForm").append(ctr0);

                var ctrl = $('<input/>').attr({ type: 'hidden', name: 'item_name_' + (indx + 1).toString(), value: prodName.toString() });
                $("#payForm").append(ctrl);


                var ctrl2 = $('<input/>').attr({ type: 'hidden', name: 'amount_' + (indx + 1).toString(), value: priceValue.toString() });
                $("#payForm").append(ctrl2);

            });

            document.forms["payForm"].submit();
        });
 
    </script>
</body>
</html>
