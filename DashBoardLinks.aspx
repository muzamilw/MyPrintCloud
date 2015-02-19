<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DashBoardLinks.aspx.cs" Inherits="Web2Print.UI.DashBoardLinks" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
</head>
<body style=" background-color:#172b32;">
    <form id="form1" runat="server" >
   <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="jquery-migrate" />

                <asp:ScriptReference Name="WebUIValidation.js" Assembly="System.Web" />
            </Scripts>
        </asp:ScriptManager>
    <div style="background-color: White; min-height: 477px; max-width: 100%; padding: 5px;
        padding-top: 7px; border-top: 10px solid #20A5CD; -moz-border-top-left-radius: 5px;
    -webkit-border-top-left-radius: 5px;
    -khtml-border-top-left-radius: 5px;
    border-top-left-radius: 5px; border-top-right-radius: 5px; box-shadow: 0 8px 6px -6px #666;">
       <div id="divControl" runat="server">
       <asp:PlaceHolder ID="plcHolder" runat="server"></asp:PlaceHolder>
       </div>
    </div>
  
    </form>
</body>
</html>
