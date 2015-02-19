<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchByPostCode.aspx.cs"
    Inherits="Web2Print.UI.SearchByPostCode" %>

<%@ Register Src="Controls/Footer.ascx" TagName="Footer" TagPrefix="uc3" %>
<%@ Register Src="~/Controls/SearchPostCode.ascx" TagPrefix="uc3" TagName="SearchPostCode" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style>
        html, body
        {
            height: 100% !important;
        }
        
        .ui-autocomplete-loading
        {
            background: white url('images/pu_loader.gif') right center no-repeat !important;
        }
        
        
        
        H1
        {
            font-size: 21px;
            color: #555555;
            font-family: Arial;
        }
        
        H2
        {
            font-size: 21px;
            color: #555555;
            font-family: Arial;
        }
        
        
        p
        {
            font-size: 17px;
            color: #555555;
            font-family: Arial;
        }
        
        UL
        {
            font-size: 17px;
            color: #555555;
            font-family: Arial;
            list-style-type: none;
            list-style-image: url('/images/tick_bullet.png');
        }
        
        LI
        {
            line-height: 1.5;
        }
    </style>
   
  
    <link href="/Styles/jquery-ui-1.9.2.custom.min.css" rel="stylesheet" type="text/css" />
    
</head>
<body class="searchpopbody">
    <form id="postcode" runat="server">
        <asp:ScriptManager ID="ScriptManager2" runat="server">
            <Scripts>
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="jquery-migrate" />



                <asp:ScriptReference Path="/Scripts/jquery-ui-1.9.2.custom.min.js" />
                <asp:ScriptReference Path="/Scripts/utilities.js" />
                <asp:ScriptReference Path="/Scripts/input.watermark.js" />


                

                <asp:ScriptReference Name="WebUIValidation.js" Assembly="System.Web" />
            </Scripts>
        </asp:ScriptManager>
    <div id="divShd" class="opaqueLayer">
        <div id="loader" class="searchpostcodeLoader">
            <br />
            <img src='<%=ResolveUrl("~/images/asdf.gif") %>' alt="" /><br />
            Searching...
        </div>
    </div>
    
    <uc3:SearchPostCode runat="server" ID="SearchPostCode" />
    
    </form>
</body>
</html>
