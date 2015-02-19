<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ThemeSite.Master"
    CodeBehind="Signup.aspx.cs" Inherits="Web2Print.UI.Signup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="Controls/WhyChooseUs.ascx" TagName="WhyChooseUs" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContents" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container content_area">

        <div class="row left_right_padding">

            <div class="signin_heading_div  col-md-12 col-lg-12 col-xs-12">
                <asp:Label ID="lblNewCustomer" runat="server" CssClass="sign_in_heading" Text="New User Registration"></asp:Label>
            </div>
            <div class="page_border_div rounded_corners col-md-12 col-lg-12 col-xs-12">
                <div class="sign_up_page_box">
                    <div class="SignUpLeftPanel col-md-6 col-lg-6 col-xs-12">


                        <div id="lblAlreadyHveAcc" runat="server" class="mainHeadingAveniorCheckout">Register using</div>
                        <div class="clearBoth">&nbsp;</div>
                        <div id="cntSocialBtns" runat="server" class="cntSignInWithSocialinks">
                            <em id="emfaceb" runat="server" class="emfb"></em>
                            <asp:Button ID="facebookbtn" runat="server" Text="Sign up with Facebook" OnClientClick="return OpenNewWindow('fb');" CssClass="connectWithfb " /><br />

                            <em id="emtwitte" runat="server" class="emTw"></em>
                            <asp:Button ID="twitterBtn" Text="Sign up with Twitter" runat="server" OnClientClick="return OpenNewWindow('tw');" CssClass="connectWithtwitter " />
                            <br />
                            <br />
                            <div id="cntOrBottom" runat="server">
                                -OR-
                            </div>
                        </div>
                        <div class="clearBoth">&nbsp;</div>
                        <div id="cntCompanyHeading" runat="server" class="mainHeadingAveniorCheckout">Sign Up</div>
                        <div id="cntAlertMesg" runat="server" class="errorMessage mainHeadingAveniorCheckout" style="display: none;">Complete your registration below</div>
                        <br />
                        <asp:Label ID="lblSignupMessage" runat="server" CssClass="errorMessage"></asp:Label>

                        <div>
                            <div id="SignUpBox" class="cntRegisterFN" style="">
                                
                                <asp:TextBox ID="txtFirstName" runat="server" CssClass="txtBoxLoginPage" Text="First Name"
                                    MaxLength="50"></asp:TextBox>
                            </div>

                            <div class="cntRegisterLN" style="">
                                
                                <asp:TextBox ID="txtLastName" runat="server" CssClass="txtBoxLoginPage" Text="Last Name"
                                    MaxLength="50"></asp:TextBox>
                            </div>
                            <div class="clearBoth">&nbsp;</div>
                        </div>



                        <div class="Mtop15P">
                            
                            <asp:TextBox ID="txtPhone" runat="server" CssClass="txtBoxLoginPage" Text="Phone"
                                MaxLength="30"></asp:TextBox>
                        </div>
                        <div class="Mtop15P">
                            
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="txtBoxLoginPage" Text="Email"
                                MaxLength="100" AutoCompleteType="None"></asp:TextBox>


                        </div>
                        <div class="Mtop15P">
                           
                            <asp:TextBox ID="txtRegPassword" runat="server" CssClass="txtBoxLoginPage" Text="Password"
                                AutoCompleteType="None" MaxLength="20"></asp:TextBox>
                        </div>
                        <br />
                        <asp:Button ID="cmdRegister" runat="server" Text="CREATE ACCOUNT" OnClick="cmdRegister_Click" Style=""
                            CssClass="start_creating_btn_CretAcc registerAccountBtn rounded_corners5" OnClientClick="return ValidateUSer();" />
                        <div class="clear">
                            &nbsp;
                        </div>

                    </div>
                    <div class="signup_width col-md-6 col-lg-6 col-xs-12">



                        <div class="BackgroundColor">
                            <div class="custom_color_heading" id="divwhyregister" runat="server">
                                Why Register?
                            </div>
                            <div class="space5">
                                &nbsp;
                            </div>
                            <asp:Label ID="lblRegistering" runat="server" CssClass="registering_label" Text="By registering you are able to:"></asp:Label>
                            <div class="registering_item_register" id="divsaveurdeisgn" runat="server">
                                <div class="tickImge">&nbsp;</div>
                                <asp:Literal ID="ltS" runat="server" Text="Save your designs"></asp:Literal>
                            </div>
                            <div class="registering_item_separator">
                                &nbsp;
                            </div>
                            <div class="registering_item_register" runat="server">
                                <div class="tickImge">&nbsp;</div>
                                <asp:Literal ID="divviewntrackoh" runat="server" Text="View and track order history"></asp:Literal>
                            </div>
                            <div class="registering_item_separator">
                                &nbsp;
                            </div>
                            <div class="registering_item_register" runat="server">
                                <div class="tickImge">&nbsp;</div>
                                <asp:Literal ID="divreorderq" runat="server" Text="Re-order quickly"></asp:Literal>
                            </div>
                            <div class="registering_item_separator">
                                &nbsp;
                            </div>
                            <div class="registering_item_register" runat="server">
                                <div class="tickImge">&nbsp;</div>
                                <asp:Literal ID="divsubs2newsl" runat="server" Text="Subscribe to our Newsletters on new marketing products"></asp:Literal>

                            </div>
                            <div class="registering_item_separator">
                                &nbsp;
                            </div>
                            <div class="registering_item_register" runat="server">
                                <div class="tickImge">&nbsp;</div>
                                <asp:Literal ID="lblTitleRequestQuote" runat="server" Text="Request a quote"></asp:Literal>
                            </div>
                            <div class="registering_item_separator">
                                &nbsp;
                            </div>
                        </div>
                    </div>
                    <div class="clearBoth">
                    </div>
                </div>
                <br />
            </div>
            <br />
            <br />
            <br />
        </div>
    </div>
    <asp:HiddenField ID="hfNameforAuthorization" runat="server" Value="0" />
    <asp:HiddenField ID="hfisSigupWithSM" runat="server" Value="0" />
    <asp:HiddenField ID="hfisRegWithTwitter" runat="server" Value="0" />
    <asp:HiddenField ID="hfisRegisterUserValid" runat="server" Value="1" />
    <script type="text/javascript" language="javascript">
        // Set the focus
        $(document).ready(function () {
            if ($("#<%= hfisSigupWithSM.ClientID %>").val() == 1) {
                $('html, body').animate({
                    scrollTop: $("#SignUpBox").offset().top
                }, 500);
            }
            if ($("#<%= hfisRegisterUserValid.ClientID %>").val() == 0) {
                $("#<%= hfisRegisterUserValid.ClientID %>").val(1);
                validateRegistrationProcess();
            } else if ($("#<%= hfisRegisterUserValid.ClientID %>").val() == 2) {
                $("#<%=lblSignupMessage.ClientID%>").css("display", "block");
                $("#<%=lblSignupMessage.ClientID%>").text("Please enter valid email.");
                $("#<%= txtEmail.ClientID %>").css("border", "1px solid #b33c12");//.parent().addClass("field-with-errors");
            }
            if ($('#<%= txtRegPassword.ClientID %>').val() === 'Password') {
            } else {
                $('#<%= txtRegPassword.ClientID %>').attr("type", "password");
            }
        });

        $('#<%= txtFirstName.ClientID %>').focus(function () {
            if ($('#<%= txtFirstName.ClientID %>').val() === 'First Name') {

            $('#<%= txtFirstName.ClientID %>').val('');
            }
    });

        $('#<%= txtFirstName.ClientID %>').blur(function () {
            if ($('#<%= txtFirstName.ClientID %>').val() === '') {

                $('#<%= txtFirstName.ClientID %>').val('First Name');
            }
        });

        $('#<%= txtLastName.ClientID %>').focus(function () {
            if ($('#<%= txtLastName.ClientID %>').val() === 'Last Name') {

                $('#<%= txtLastName.ClientID %>').val('');
            }
        });

        $('#<%= txtLastName.ClientID %>').blur(function () {
            if ($('#<%= txtLastName.ClientID %>').val() === '') {

                $('#<%= txtLastName.ClientID %>').val('Last Name');
            }
        });

        $('#<%= txtPhone.ClientID %>').focus(function () {
            if ($('#<%= txtPhone.ClientID %>').val() === 'Phone') {

                $('#<%= txtPhone.ClientID %>').val('');
            }
        });

        $('#<%= txtPhone.ClientID %>').blur(function () {
            if ($('#<%= txtPhone.ClientID %>').val() === '') {

                $('#<%= txtPhone.ClientID %>').val('Phone');
            }
        });

        $('#<%= txtEmail.ClientID %>').focus(function () {
            if ($('#<%= txtEmail.ClientID %>').val() === 'Email') {

                $('#<%= txtEmail.ClientID %>').val('');
            }
        });

        $('#<%= txtEmail.ClientID %>').blur(function () {
            if ($('#<%= txtEmail.ClientID %>').val() === '') {

                $('#<%= txtEmail.ClientID %>').val('Email');
            }
        });

        $('#<%= txtRegPassword.ClientID %>').focus(function () {
            if ($('#<%= txtRegPassword.ClientID %>').val() === 'Password') {
                $('#<%= txtRegPassword.ClientID %>').attr("type", "password");
                $('#<%= txtRegPassword.ClientID %>').val('');
            }
        });

        $('#<%= txtRegPassword.ClientID %>').blur(function () {
            if ($('#<%= txtRegPassword.ClientID %>').val() === '') {
                $('#<%= txtRegPassword.ClientID %>').removeAttr("type");
                $('#<%= txtRegPassword.ClientID %>').val('Password');
            } else {
                $('#<%= txtRegPassword.ClientID %>').attr("type", "password");
            }
        });

        function ValidateUSer() {
            var isValid = true;
            var isDataFilles = 0;

            isDataFilles = validateRegistrationProcess();
            if (isDataFilles == 1) {
                $("#<%= hfisRegisterUserValid.ClientID %>").val(0);
                isValid = false;
            } else {
                var IsEmailValid = ValidateEmail($('#<%= txtEmail.ClientID %>').val());
                if (IsEmailValid == true) {
                    $("#<%= hfisRegisterUserValid.ClientID %>").val(1);
                    isValid = true;
                } else {
                    $("#<%= hfisRegisterUserValid.ClientID %>").val(2);
                    isValid = false;
                    $("#<%=lblSignupMessage.ClientID%>").css("display", "block");
                    $("#<%=lblSignupMessage.ClientID%>").text("Please enter valid email.");
                    $("#<%= txtEmail.ClientID %>").css("border", "1px solid #b33c12");//.parent().addClass("field-with-errors");
                }

            }

            return isValid;
        }

        function validateRegistrationProcess() {
            var isDataFilles = 0;

            if ($("#<%= txtFirstName.ClientID %>").val() == '' || $("#<%= txtFirstName.ClientID %>").val() == 'First Name') {
                isDataFilles = 1;
                //$('.UFName').css("display", "block");
                $("#<%= txtFirstName.ClientID %>").css("border", "1px solid #b33c12");//.parent().addClass("field-with-errors");
            } else {
                //$('.UFName').css("display", "none");
                $("#<%= txtFirstName.ClientID %>").css("border", "1px solid #c1c6cc");//.parent().removeClass("field-with-errors");
            }

            if ($("#<%= txtLastName.ClientID %>").val() == '' || $("#<%= txtLastName.ClientID %>").val() == 'Last Name') {
                isDataFilles = 1;
                //$('.ULName').css("display", "block");
                $("#<%= txtLastName.ClientID %>").css("border", "1px solid #b33c12");//.parent().addClass("field-with-errors");
            } else {
                //$('.ULName').css("display", "none");
                $("#<%= txtLastName.ClientID %>").css("border", "1px solid #c1c6cc");//.parent().removeClass("field-with-errors");
            }


            if ($("#<%= txtPhone.ClientID %>").val() == '' || $("#<%= txtPhone.ClientID %>").val() == 'Phone') {
                isDataFilles = 1;
                //$('.UPhone').css("display", "block");
                $("#<%= txtPhone.ClientID %>").css("border", "1px solid #b33c12");//.parent().addClass("field-with-errors");
            } else {
                //$('.UPhone').css("display", "none");
                $("#<%= txtPhone.ClientID %>").css("border", "1px solid #c1c6cc");//.parent().removeClass("field-with-errors");
            }

            if ($("#<%= txtEmail.ClientID %>").val() == '' || $("#<%= txtEmail.ClientID %>").val() == 'Email') {
                isDataFilles = 1;
                //$('.UEmail').css("display", "block");
                $("#<%= txtEmail.ClientID %>").css("border", "1px solid #b33c12");//.parent().addClass("field-with-errors");
            } else {
                //$('.UEmail').css("display", "none");
                $("#<%= txtEmail.ClientID %>").css("border", "1px solid #c1c6cc");//.parent().removeClass("field-with-errors");
            }

            if ($("#<%= txtRegPassword.ClientID %>").val() == '' || $("#<%= txtRegPassword.ClientID %>").val() == 'Password') {
                isDataFilles = 1;
                //$('.UPass').css("display", "block");
                $("#<%= txtRegPassword.ClientID %>").css("border", "1px solid #b33c12");//.parent().addClass("field-with-errors");
            } else {
                //$('.UPass').css("display", "none");
                $("#<%= txtRegPassword.ClientID %>").css("border", "1px solid #c1c6cc");//.parent().removeClass("field-with-errors");
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
</asp:Content>
