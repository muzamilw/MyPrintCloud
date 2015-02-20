<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsletteerSubscriber.aspx.cs"
    Inherits="Web2Print.UI.NewsletteerSubscriber" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
   
</head>
<body style="background-color:White;background-image:none;">
    <form id="form1" runat="server">
         <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="jquery-migrate" />

                <asp:ScriptReference Name="WebUIValidation.js" Assembly="System.Web" />
            </Scripts>
        </asp:ScriptManager>
    <div style="padding: 30px;" class="white_background rounded_corners textAlignLeft">
        <asp:Label ID="lblSignUp" runat="server" Style="color: #CCCDDA; font-size: 22px;
            margin-top: 3px; float: left;"></asp:Label>
        <asp:Label ID="lblOurNews" runat="server" Style="font-size: 22px;
            margin-top: 3px; float: left;"></asp:Label>
       
        <div class="clearBoth">
            &nbsp;
        </div>
       <div style="margin-top: 30px; margin-bottom: 15px; text-align:left; font-size:14px;">
            <asp:Label ID="Nwsdesc" runat="server"></asp:Label>
        </div>
      <%--  <asp:Label ID="lblEmAdd" runat="server" Text="Your Email Address" Style="text-align: left;
            font-size: 22px;">
        </asp:Label>--%>
        <div style="margin-top: 20px; margin-bottom: 15px;">
            <asp:TextBox ID="txtEmailbox" runat="server" Text="Enter email address..." ValidationGroup="email" CausesValidation="false" Style="width: 400px; height: 25px; margin-right: 20px;
                float: left; margin-top: 5px;">
            </asp:TextBox>
            <div onclick="ValidateBottomSubscriberEmail();" class="rounded_corners button arrow">
                Subscribe
            </div><div class="clearBoth">
            &nbsp;
        </div>
            
        </div>
        <div>
        <asp:Label ID="errorMsg" runat="server" style="font-size:14px; color:Red; margin-top:5px;"></asp:Label>
        </div>
        <div class="clearBoth">
            &nbsp;
        </div><%--
        <div style="margin-top: 15px; margin-bottom: 10px; text-align:left;">
           <asp:Label ID="Label1" runat="server" Font-Size="12px" 
                Text="Did you know we can deliver to you locally for free*! Or why not collect yourself and pop in for a chat on other great marketing intiatives."></asp:Label>
        </div>--%>
    </div>
    <asp:Button ID="btnGo" runat="server" style="display:none;" OnClick="btnGo_Click"  />
     <script type="text/javascript">
         $(document).ready(function () {
             $('#<%= txtEmailbox.ClientID %>').focus(function () {
                 $(this).val('');
             });
         });
         function ValidateBottomSubscriberEmail() {

             var email = $('#<%= txtEmailbox.ClientID %>').val().trim();
             if (ValidateEmail(email)) {
                 $('#<%=errorMsg.ClientID %>').html('');
                 $('#<%=btnGo.ClientID %>').click();
             }
         }
         function ValidateEmail(email) {
             var isValid = true;
             if (email == '') {
                 var emailxreq = "<%= Resources.MyResource.emailxreq %>";
                 $('#<%=errorMsg.ClientID %>').css("display", "block");
                 $('#<%=errorMsg.ClientID %>').html(emailxreq);
                 isValid = false;
             }
             else {
                 var re = new RegExp("^[A-Za-z0-9](([_\\.\\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\\.\\-]?[a-zA-Z0-9]+)*)\\.([A-Za-z]{2,})$");
                 if (!re.test(email)) {
                     var PlxenterVEmail = "<%= Resources.MyResource.PlxenterVEmail %>";
                     $('#<%=errorMsg.ClientID %>').html(PlxenterVEmail);
                     isValid = false;
                 }
             }
             return isValid;
         }
    </script>
    </form>
</body>
</html>
