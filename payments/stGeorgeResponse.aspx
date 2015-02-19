<%@ page language="C#" autoeventwireup="true" codebehind="stGeorgeResponse.aspx.cs" EnableViewStateMac="false" inherits="Web2Print.UI.payments.stGeorgeResponse" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:panel id="Panel_StackTrace" runat="server">
<!-- only display these next fields if an stacktrace output exists-->
        <table>
    <tr>
        <td colspan="2">&nbsp;</td>
    </tr>
    <tr class="title">
        <td colspan="2"><p><strong>&nbsp;Exception Stack Trace</strong></p></td>
    </tr>
    <tr>
        <td colspan="2"><asp:Label id=Label_StackTrace runat="server"/></td>
    </tr>
            </table>
</asp:panel>
        </div>
    </form>
</body>
</html>
