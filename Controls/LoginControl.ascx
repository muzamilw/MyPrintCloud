<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginControl.ascx.cs"
    Inherits="Web2Print.UI.Controls.LoginControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Panel ID="pnlDefaultButton" runat="server" DefaultButton="cmdLogin">
    <div class="white-container-lightgrey-border  rounded_corners">
        <div class="sign_up_page_box NoPadding">
            <div class="divleftpanel col-md-6 col-lg-6 col-sm-12 col-xs-12">
                <div id="lblSignWithSM" runat="server" class="mainHeadingAveniorCheckout">Sign in using</div>
                <div class="clearBoth">&nbsp;</div>
                <div id="cntSocialLink" runat="server" class="cntSignInWithSocialinks">
                    <em id="emfacebook" runat="server" class="emfb"></em>
                    <asp:Button ID="facebookbtn" runat="server" Text="Sign in with Facebook" OnClientClick="return OpenNewWindow('fb');" CssClass="connectWithfb" /><br />
                    <%-- <asp:Button ID="gPlusbtn" runat="server" OnClientClick="connectWithgPlus(); return false;" CssClass="connectWithgPlus " /><br />--%>
                    <em id="emtwtter" runat="server" class="emTw"></em>
                    <asp:Button ID="twitterBtn" runat="server" Text="Sign in with Twitter" OnClientClick="return OpenNewWindow('tw');" CssClass="connectWithtwitter " />
                     <br />
                <br /> 
                    <div id="cntOrBottom" runat="server" >
                    -OR-
                </div>
                </div>
                <div id="cntOrRight" runat="server" class="visible-lg visible-md hidden-sm hidden-xs">
                    -OR-
                </div>
                <div class="clear">
                </div>
                <div class="mainHeadingAveniorCheckout">
                    <asp:Literal ID="ltrlsigninwithaccount" runat="server" Text="Sign in using "></asp:Literal>
                </div>
                <br />
                 <div id="errorMesgcnt" runat="server" class="errorMesgLoginPage">
                    </div>
                <div class="  Fsize14">
                    <span class="error-message LEmail" style="display: none;">can't be blank</span><br>
                    <asp:TextBox ID="txtEmail" runat="server" Text="Email" CssClass="txtBoxLoginPage txtEmailBox"></asp:TextBox>


                </div>
                <div class="mrginBtm Fsize14">

                    <span class="error-message LPass" style="display: none;">can't be blank</span><br>
                    <asp:TextBox ID="txtPassword" runat="server" AutoCompleteType="None" Text="Password"
                        CssClass="txtBoxLoginPage txtPassBox"></asp:TextBox>
                </div>
                <div id="cntStaySignedin">
                    <asp:CheckBox ID="checkRemberLogin" runat="server" Text="Keep me logged in" />

                </div>
                <div id="divBtnContainer" style="margin-top: 20px;">
                    <div class="divwidth">
                        <asp:LinkButton CssClass="forgetPassLink" ID="lnkForgotPassword" runat="server" Text="Forgot your Password?"
                            PostBackUrl="~/ForgotPassword.aspx"></asp:LinkButton>
                    </div>
                    <div class="divwidthset">
                        <asp:Button ID="cmdLogin" runat="server" OnClick="cmdLogin_Click" OnClientClick="return ValidateLoginUser();"
                            CssClass="start_creating_btn_Login rounded_corners5" /><br />



                    </div>
                    <div class="clear">
                        &nbsp;
                    </div>
                   
                </div>

            </div>
            <div class="col-md-6 col-lg-6 col-xs-12 col-sm-12">
                 <div class="containerRegisternow">
                <asp:Button ID="Button1" runat="server" CssClass="SignUpButton rounded_corners5"
                    PostBackUrl="~/Signup.aspx" OnClick="Button1_Click" />

                <br />
                <br />
            </div>
            <div class="divDontHaveAccont">
                <asp:Label ID="lblRegistering" runat="server" CssClass="mainHeadingAveniorCheckout" Style="font-size: 19px !important; margin-bottom: 0px !important;"
                    Text="By registering you are able to:"></asp:Label>
                <div class="registering_item">
                    <div class="tickImge">
                        &nbsp;
                    </div>
                    <asp:Label ID="ltrlsaveurdesign" runat="server" Text="Save your designs" CssClass="smallAveniorLogin"></asp:Label>
                </div>
                <div class="registering_item">
                    <div class="tickImge">
                        &nbsp;
                    </div>
                    <asp:Label ID="ltrlviewtoh" runat="server" Text="View and track order history" CssClass="smallAveniorLogin"></asp:Label>
                </div>
                <div class="registering_item">
                    <div class="tickImge">
                        &nbsp;
                    </div>
                    <asp:Label ID="ltrlroq" runat="server" Text=" Re-order quickly" CssClass="smallAveniorLogin"></asp:Label>
                </div>
                <div class="registering_item">
                    <div class="tickImge">
                        &nbsp;
                    </div>
                    <asp:Label ID="ltrlsubscribe2Nwzl" runat="server" CssClass="smallAveniorLogin" Text="Subscribe to our Newsletters on new marketing products and services"></asp:Label>
                </div>
                <div class="registering_item">
                    <div class="tickImge">
                        &nbsp;
                    </div>
                    <asp:Label ID="lblTitleRequestQuote" runat="server" CssClass="smallAveniorLogin" Text="Request a quote"></asp:Label>
                </div>
                <div class="registering_item">
                    <div class="tickImge">
                        &nbsp;
                    </div>
                    <asp:Label ID="ltrlNewVoucherPromo" runat="server" CssClass="smallAveniorLogin" Text="Get latest Voucher promotions"></asp:Label>
                </div>
            </div>
            </div>
            <div class="clearBoth">
                &nbsp;
            </div>
        </div>
    </div>
    <div id="status">
    </div>
    <div class="clearBoth">
        &nbsp;
            </div>
</asp:Panel>
<asp:HiddenField ID="hfisLoginUserValid" runat="server" Value="1" />
<asp:HiddenField ID="hfRegister" runat="server" />
<script type="text/javascript" language="javascript">
    // Set the fous

    $(document).ready(function () {
        if ($("#<%= hfisLoginUserValid.ClientID %>").val() == 0) {
            $("#<%= hfisLoginUserValid.ClientID %>").val(1);
            validateRegistrationProcess();
        } else if ($("#<%= hfisLoginUserValid.ClientID %>").val() == 2) {
            $('.LEmail').css("display", "block");
            $('.LEmail').text("Please enter valid email.");
            $("#<%= txtEmail.ClientID %>").parent().addClass("field-with-errors");
          }
        if ($('#<%= txtPassword.ClientID %>').val() === 'Password') {
        } else {
            $('#<%= txtPassword.ClientID %>').attr("type", "password");
        }
        if ($('#<%= txtEmail.ClientID %>').val() === '') {

            $('#<%= txtEmail.ClientID %>').val("Email");
        }
    });



    $('#<%= txtEmail.ClientID %>').focus(function () {
        if ($('#<%= txtEmail.ClientID %>').val() === 'Email') {
            $('#<%= txtEmail.ClientID %>').val('');
        }
    });
    $('#<%= txtEmail.ClientID %>').blur(function () {
        if ($('#<%= txtEmail.ClientID %>').val() === '') {

            $('#<%= txtEmail.ClientID %>').val("Email");
        }
    });

    $('#<%= txtPassword.ClientID %>').focus(function () {
        if ($('#<%= txtPassword.ClientID %>').val() === 'Password') {
            $('#<%= txtPassword.ClientID %>').attr("type", "password");
            $('#<%= txtPassword.ClientID %>').val('');
        }
    });

    $('#<%= txtPassword.ClientID %>').blur(function () {
        if ($('#<%= txtPassword.ClientID %>').val() === '') {
            $('#<%= txtPassword.ClientID %>').removeAttr("type");
            $('#<%= txtPassword.ClientID %>').val('Password');
        } else {
            $('#<%= txtPassword.ClientID %>').attr("type", "password");
        }
    });


    function Validate() {
        var isValid = true;
        var email = $('#<%=txtEmail.ClientID %>').val().trim();
        isValid = ValidateEmail(email);
        if (isValid) {

            var password = $('#<%=txtPassword.ClientID %>').val().trim();
            if (password == '') {
                var popupPasswordxreq = "<%= Resources.MyResource.popupPasswordxreq %>";

                ShowPopup('Message', popupPasswordxreq, false);

                isValid = false;
            }
        }
        return isValid;
    }

    function ValidateLoginUser() {
        var isValid = true;
        var isDataFilles = 0;

        isDataFilles = validateRegistrationProcess();
        if (isDataFilles == 1) {
            $("#<%= hfisLoginUserValid.ClientID %>").val(0);
            isValid = false;
        } else {
            var IsEmailValid = ValidateEmail($('#<%= txtEmail.ClientID %>').val());
            if (IsEmailValid == true) {
                $("#<%= hfisLoginUserValid.ClientID %>").val(1);
                    isValid = true;
                } else {
                    $("#<%= hfisLoginUserValid.ClientID %>").val(2);
                    isValid = false;
                    $('.LEmail').css("display", "block");
                    $('.LEmail').text("Please enter valid email.");
                    $("#<%= txtEmail.ClientID %>").parent().addClass("field-with-errors");
                }

            }
            $("#<%= hfisLoginUserValid.ClientID %>").val(1);
        return isValid;
    }

    function validateRegistrationProcess() {
        var isDataFilles = 0;

        if ($("#<%= txtEmail.ClientID %>").val() == '' || $("#<%= txtEmail.ClientID %>").val() == 'Email') {
                isDataFilles = 1;
                $('.LEmail').css("display", "block");
                $("#<%= txtEmail.ClientID %>").parent().addClass("field-with-errors");
            } else {
                $('.LEmail').css("display", "none");
                $("#<%= txtEmail.ClientID %>").parent().removeClass("field-with-errors");
            }

            if ($("#<%= txtPassword.ClientID %>").val() == '' || $("#<%= txtPassword.ClientID %>").val() == 'Password') {
                isDataFilles = 1;
                $('.LPass').css("display", "block");
                $("#<%= txtPassword.ClientID %>").parent().addClass("field-with-errors");
            } else {
                $('.LPass').css("display", "none");
                $("#<%= txtPassword.ClientID %>").parent().removeClass("field-with-errors");
            }
            return isDataFilles;
        }


        function OpenNewWindow(ProviderName) {
            var w = 620;
            var h = 360;
            var left = (screen.width / 2) - (w / 2);
            var top = (screen.height / 2) - (h / 2);
            window.open("/oauth.aspx?Provider=" + ProviderName, "_blank", "toolbar=yes, scrollbars=yes, resizable=yes, width=" + w + ", height=" + h + ", top=" + top + ", left=" + left);
            return false;
        }
</script>
