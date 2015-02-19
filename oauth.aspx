<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="oauth.aspx.cs" Inherits="Web2Print.UI.oauth" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/jquery-1.9.1.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <asp:HiddenField ID="hfisPostback" runat="server" Value ="0"/>
         <asp:HiddenField ID="redirectUrl" runat="server" />
          <script>
              $(document).ready(function () {
                  if ($("#<%=hfisPostback.ClientID%>").val() == 1) {
                     window.close(); window.opener.location.href = $("#<%=redirectUrl.ClientID%>").val();
                  }
                  
              });


        </script>
    </form>
</body>
</html>
