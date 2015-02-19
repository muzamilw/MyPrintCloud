<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShopPayment.aspx.cs" Inherits="Web2Print.UI.ShopPayment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
          <asp:ScriptManager ID="ScriptManager2" runat="server">
            <Scripts>

                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="jquery-migrate" />

            </Scripts>
        </asp:ScriptManager>
    <div>
     <div class="simpleText" >
    <asp:Literal ID="ltrlthnxordersuccessfull" runat="server" Text="Thanks, Your orders is placed for approval successfully!"></asp:Literal> </div>
     <a id="agohome" class="forgetPassLink" runat="server" href="default.aspx"><span id="spngohome" runat="server"></span>Go Home</a>
    </div>
    </form>
</body>
</html>
