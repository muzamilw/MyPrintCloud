<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaypalRedirect.aspx.cs" Inherits="Web2Print.UI.PinkRegPayPalRedirect" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1250" />
    <title>Web2Print PayPal</title>
    <script src="js/libs/jquery.min.js"></script>
    
</head>
<body>
   <form id="payForm" method="post" action="<%Response.Write (URL);%>" target="_parent">
   
    <input type="hidden" name="cmd" value="<%Response.Write (cmd);%>" />
    <input type="hidden" name="business" value="<%Response.Write (business);%>" />
    <input type="hidden" name="upload" value="1" />
    <input type="hidden" name="custom" value="<%Response.Write (request_id1);%>" />
    <input type="hidden" name="no_shipping" value="<%Response.Write (no_shipping);%>" />
    <input type="hidden" name="return" value="<%Response.Write (return_url);%>" />
    <input type="hidden" name="rm" value="<%Response.Write (rm);%>" />
    <input type="hidden" name="notify_url" value="<%Response.Write (notify_url);%>" />
    <input type="hidden" name="cancel_return" value="<%Response.Write (cancel_url);%>" />
    <input type="hidden" name="currency_code" value="<%Response.Write (currency_code);%>" />
    <input type="hidden" name="handling_cart" value="<%Response.Write (handling_cart);%>" />
    <!-- Identify the subscription. -->
    <input type="hidden" name="item_name" value="<%Response.Write (Services);%>">
    <!-- Set the initial payment. -->
    <input type="hidden" name="a1" value="<%Response.Write (setupTotal);%>">
    <input type="hidden" name="p1" value="<%Response.Write (trialPeriod);%>">
    <input type="hidden" name="t1" value="<%Response.Write (trialPeriodType);%>">
    <!-- Set the terms of the recurring payments. -->
    <input type="hidden" name="a3" value="<%Response.Write (onwardsTotal);%>">
    <input type="hidden" name="p3" value="<%Response.Write ("1");%>">
    <input type="hidden" name="t3" value="M">
    <!-- Limit the number of billing cycles. -->
    <input type="hidden" name="src" value="1">
    <input type="hidden" name="srt" value="<%Response.Write (BillingCycleMonths);%>">
    <input type="hidden" name="modify" value="<%Response.Write (ModifyMode);%>">


  <input type="hidden" name="sra" value="1">
   
    </form>

    <div style="text-align:center; height:600px; margin-top:300px; font:arial; font-size:16px;">Please wait while we redirect you to PayPal.</div>
    
    
    <script language="javascript" type="text/javascript">

        $(document).ready(function () {

          

            document.forms["payForm"].submit();
        });
 
    </script>
</body>
</html>
