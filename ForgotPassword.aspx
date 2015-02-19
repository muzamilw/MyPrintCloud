<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ThemeSite.Master"
    CodeBehind="ForgotPassword.aspx.cs" Inherits="Web2Print.UI.ForgotPassword" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="Controls/ResetPassword.ascx" TagName="ResetPassword" TagPrefix="uc1" %>
<asp:Content ID="ForgotPwdContent1" ContentPlaceHolderID="HeadContents" runat="server">
</asp:Content>
<asp:Content ID="ForgotPwdContent2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content_area container">
        <div class="left_right_padding row">
            <div class="signin_heading_div">
                <asp:Label ID="lblMainHeading" runat="server" Text="Password Request / Reset" CssClass="sign_in_heading"></asp:Label>
            </div>
            <div class="page_border_div rounded_corners">
                <div class="forgotPasswordContainer">

                
                <div class="custom_color_heading14" id="spnforgotPassord" runat="server">
                    Please enter your registered email address below. Your password will be mailed to
                                        your email address.
                </div>
                <div class="smallContctUsAvenior float_left_simple" id="spnemailAddress" runat="server">
                    Email Address
                </div>
                <div class="TTL widthAvenior">
                    <span class="error-message EFirstName" style="display: none;">can't be blank</span><br>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="newTxtBox"
                        TabIndex="1"></asp:TextBox>
                    <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>
                </div>
                <div class="clearBoth">
                    &nbsp;
                </div>
                <div class="smallContctUsAvenior float_left_simple" id="Div1" runat="server">
                  
                </div>
                <div class="TTL widthAvenior">
                  <asp:Label ID="lblMessage" runat="server" CssClass="errorMessage" Text=""></asp:Label>
                </div>
                <div class="clearBoth">
                    &nbsp;
                </div>
                <div class="smallContctUsAvenior float_left_simple">
                </div>
                <div class="TTL widthAvenior" style="margin-top:20px;">
                    <asp:Button ID="cmdNext" runat="server" Text="SEND PASSWORD" OnClick="cmdNext_Click" Visible="true"
                        CssClass="start_creating_btn rounded_corners5" OnClientClick="return ValidateFPass();" />
                    &nbsp;
                                                    <asp:LinkButton CssClass="forgetPassLink" ID="lnkReturnLogin" runat="server" Text="Return to home Page"
                                                        CausesValidation="false"></asp:LinkButton>
                </div>
                <div class="clearBoth">
                </div>
                    </div>
            </div>
        </div>
        <br />
        <br />
        <br />
    </div>
    <asp:HiddenField ID="hfPsswordSent" runat="server" />
    <asp:HiddenField ID="hfAnswerIncorrect" runat="server" />
    <asp:HiddenField ID="hfEmailNotSent" runat="server" />
    <asp:HiddenField ID="hfEmailNorRegistered" runat="server" />
    <script>
        function ValidateFPass() {
          var isValid = true;
          if ($('#<%= txtEmail.ClientID %>').val() == '') {
              $('.LEmail').css("display", "block");
              $('.LEmail').text("Please enter email address.");
              $("#<%= txtEmail.ClientID %>").css("border-color", "#b33c12");
              return false;
          } else {

          
           var IsEmailValid = ValidateEmail($('#<%= txtEmail.ClientID %>').val());
            if (IsEmailValid == true) {

                isValid = true;
            } else {
                $('.LEmail').css("display", "block");
                $('.LEmail').text("Please enter valid email address.");
                $("#<%= txtEmail.ClientID %>").parent().addClass("field-with-errors");
                isValid = false;
            }

            return isValid;
          }
        }


    </script>
</asp:Content>
