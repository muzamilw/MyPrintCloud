<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.ascx.cs"
    Inherits="Web2Print.UI.Controls.ResetPassword" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<div id="divQuestion" runat="server" class="resetPasswordBox">
    <asp:Label ID="lblTitle" runat="server" Text="Reset Password" CssClass="left_align FileUploadHeaderText_PopUp float_left_simple MesgBoxClass"></asp:Label>

    <div id="CancelControl" onclick="window.parent.closeMS(); return false;" class="MesgBoxBtnsDisplay rounded_corners5" style="">
        Close
    </div>
    <div class="clearBoth">
        &nbsp;
    </div>
    <div class="SolidBorderCS">
        &nbsp;
    </div>
    <table id="CurPssCont" class="normalTextStyle tblRestPass" runat="server">
         <tr>
            <td>
             <label id="errorMesg" style="display:none;font-size: 13px;
color: red;"></label>
            </td>
        </tr>
        <tr>
            <td>
                <div class="float_left_simple smallfonAvenior">
                    <asp:Literal ID="ltrlPss" runat="server" Text="Current password:"></asp:Literal>
                </div>
                <div class="float_left_simple cnttxtBox">
                   
                    <asp:TextBox ID="txtCurPass" runat="server" MaxLength="50" CssClass="newTxtBox"
                        TextMode="Password"></asp:TextBox>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <h3 class="headingsAvenior">New password Details</h3>
            </td>
        </tr>
        <tr>
            <td align="right">
                <div class="float_left_simple smallfonAvenior">
                    <asp:Literal ID="ltrlnewpass" runat="server" Text="New Password:"></asp:Literal>
                </div>
                <div class="float_left_simple cnttxtBox">
                    
                    <asp:TextBox ID="txtNewPassword" runat="server" MaxLength="50" CssClass=" newTxtBox"
                        TextMode="Password"></asp:TextBox>
                </div>
                <div class="clearBoth">
                    &nbsp;
                </div>
            </td>
        </tr>
        <tr>
            <td align="right" class="H4B">
                <div class="float_left_simple smallfonAvenior">
                    <asp:Literal ID="ltrlconfirmpass" runat="server" Text="Confirm Password:"></asp:Literal>
                </div>
                <div class="float_left_simple cnttxtBox">
                    
                    <asp:TextBox ID="txtConfirmPassword" runat="server" MaxLength="50" CssClass="newTxtBox"
                        TextMode="Password"></asp:TextBox>
                </div>
                <div class="clearBoth">
                    &nbsp;
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div class="float_left_simple smallfonAvenior">
                    &nbsp;
                </div>
                <div class="float_left_simple cnttxtBox left_align" style="margin-top: 30px;">
                    <asp:Button ID="cmdSave" CssClass="start_creating_btn rounded_corners5" OnClientClick="return Passwordvalidator();"
                        runat="server" Text="Save" OnClick="cmdSave_Click" />
                </div>
            </td>
        </tr>
    </table>
</div>
<asp:HiddenField ID="hfErrorMes" runat="server" Value="1" />
<script type="text/javascript">


    $(document).ready(function () {

        $("#<%=txtCurPass.ClientID %>").focus();

        if ($("#<%= hfErrorMes.ClientID %>").val() == "1") {

        }
        else if ($("#<%= hfErrorMes.ClientID %>").val() == "3") {
            Passwordvalidator();
            $("#<%= hfErrorMes.ClientID %>").val(1);
        } else if ($("#<%= hfErrorMes.ClientID %>").val() == "2") {
            $("#CancelControl").click();
        } else {
            $("#errorMesg").text("Your current password is not valid.");
            $("#errorMesg").css("display", "block");
            $("#<%= txtCurPass.ClientID %>").css("border", "1px solid #b33c12");//.parent().addClass("field-with-errors");
            $("#<%= hfErrorMes.ClientID %>").val(1);
        }
    });



function Passwordvalidator() {
    var isValid = true;
    var isDataFilles = 1;
    var CurrentPass = $("#<%= txtCurPass.ClientID %>").val();
        var passwordnew = $('#<%=txtNewPassword.ClientID %>').val().trim();
        var Confirmpassword = $('#<%=txtConfirmPassword.ClientID %>').val().trim();

        if (CurrentPass.length == 0) {
            isValid = false; isDataFilles = 0;
            //$('.EMCurentPass').css("display", "block");
            $("#<%= txtCurPass.ClientID %>").css("border", "1px solid #b33c12");//.parent().addClass("field-with-errors");
        } else {
            //$('.EMCurentPass').css("display", "none");
            $("#<%= txtCurPass.ClientID %>").css("border", "1px solid #c1c6cc");//.parent().removeClass("field-with-errors");
        }
        if (passwordnew.length == 0) {
            isValid = false; isDataFilles = 0;
            var passwordxreq = "<%= Resources.MyResource.popupPasswordxreq %>";
            //$('.EMPass').css("display", "block");
            $("#<%= txtNewPassword.ClientID %>").css("border", "1px solid #b33c12");//.parent().addClass("field-with-errors");

        } else {
            //$('.EMPass').css("display", "none");
            $("#<%= txtNewPassword.ClientID %>").css("border", "1px solid #c1c6cc");//.parent().removeClass("field-with-errors");
        }
        if (Confirmpassword.length == 0) {
            var Cpasswordxreq = "<%= Resources.MyResource.Cpasswordxreq %>";
            //$('.EMCPass').css("display", "block");
            $("#<%= txtConfirmPassword.ClientID %>").css("border", "1px solid #b33c12");//.parent().addClass("field-with-errors");

            isValid = false; isDataFilles = 0;
        } else {
            //$('.EMCPass').css("display", "none");
            $("#<%= txtConfirmPassword.ClientID %>").css("border", "1px solid #c1c6cc");//.parent().removeClass("field-with-errors");

        }

        if (passwordnew === Confirmpassword) {

        } else {
            $("#errorMesg").css("display", "block");
            $("#errorMesg").text("Password not match.");
            $("#<%= txtNewPassword.ClientID %>").css("border", "1px solid #b33c12");//.parent().addClass("field-with-errors");
         
            $("#<%= txtConfirmPassword.ClientID %>").css("border", "1px solid #b33c12");//.parent().addClass("field-with-errors");
             isValid = false;
             isDataFilles = 0;
         }

         if (isDataFilles == 0) {
             $("#<%=hfErrorMes.ClientID%>").val(3);
        } else {
            $("#<%=hfErrorMes.ClientID%>").val(1);
            return true;
        }
        return false;
    }
</script>
