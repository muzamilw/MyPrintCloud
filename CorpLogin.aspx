<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ThemeSite.Master" AutoEventWireup="true"
    ValidateRequest="false" CodeBehind="CorpLogin.aspx.cs" Inherits="Web2Print.UI.CorpLogin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContents" runat="server">
    <link href="../Styles/RealEstate_StyleSheet.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <div class="content_area container">
        <div class="left_right_padding row">

            <div class="page_border_div rounded_corners">
                <div class="sign_up_page_boxCorp">
                    <div class="custom_color_heading" id="divsigninwithacc" runat="server">
                        Sign in to my account
                    </div>
                    <br />
                    <div class="custom_lin_height" id="divSigninAccBox">
                        <div id="errorMesgcnt" runat="server" class="errorMesgLoginPage">
                        </div>
                        <asp:Label ID="divemailaddress" runat="server" Text="Email Address" CssClass="Fsize17"></asp:Label>
                        <div id="divEmailSection">
                            <div id="divEmailMessage" class="emailMessage">
                                <br />
                                <label id="lblEmailMessage" class="Message_Username">Type the email address of the account you would like to use with RealEstate Compaigns</label>
                                <br />
                                <br />
                            </div>

                            <div class="  Fsize14">
                                <span class="error-message LEmail" style="display: none;">can't be blank</span>
                                <asp:TextBox ID="txtEmail" runat="server" Width="300px" AutoCompleteType="None" CssClass="txtBoxLoginPage" TabIndex="1"></asp:TextBox>
                            </div>
                            <div id="LinkButtons_Div_Username">
                                <%--<asp:LinkButton ID="LinkButton1" runat="server" Font-Names="Arial" Font-Size="10px" ForeColor="#999999">forgot your username</asp:LinkButton>
                                &nbsp;|&nbsp;--%>
                                <asp:HyperLink ID="lnkRERegistration1" runat="server" Font-Names="Arial" Font-Size="10px" ForeColor="#999999">sign up today</asp:HyperLink>

                                <asp:Label ID="lblCantBeBlank" CssClass="Validator_Username" runat="server"></asp:Label>
                                <br />
                                <br />
                                <button type="button" id="btnNextUsername" class="ButtonsRealEstateLoginUsername" style="float: left;">Next</button>
                            </div>
                        </div>
                        <div id="divPasswordSection">
                            <asp:Label ID="lblPassword" runat="server" Text="Password" CssClass="Fsize17 float_left_simple"></asp:Label>

                            <asp:HyperLink CssClass="forgetPassLink float_left_simple  hypPassword" ID="lnkForgotPassword" runat="server" Text="Forgot your Password?" TabIndex="4"></asp:HyperLink>

                            <div class="mrginBtm Fsize14" style="margin-bottom: 15px;">
                                <div id="divPasswordMessage" style="display: none; width: 100%">
                                    <br />
                                    <label id="Label1" class="Message_PAssword">Type the password of the account email you used on previous screen to use with RealEstate Compaigns</label>
                                    <br />
                                </div>

                            </div>
                            <div id="divLoginPassword">
                                <span class="error-message LPass" style="display: none;">can't be blank</span>
                                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="300px" AutoCompleteType="None"
                                    CssClass="txtBoxLoginPage" TabIndex="2"></asp:TextBox>
                            </div>
                            <div id="LinkButtons_Div">
                                &nbsp;
                                <asp:HyperLink ID="lnkREForgotPassword" runat="server" Font-Names="Arial" Font-Size="10px" ForeColor="#999999">forgot your password</asp:HyperLink>
                                &nbsp;|&nbsp;
                                <asp:HyperLink ID="lnkRERegistration" runat="server" Font-Names="Arial" Font-Size="10px" ForeColor="#999999">sign up today</asp:HyperLink>

                                <asp:Label ID="lblCantBeBlankPasssword" CssClass="Validator_Password" runat="server"></asp:Label>
                            </div>
                            <%-------------------------------------Validator RealEstateUsername-----------------------------------------%>

                            <div class="clearBoth"></div>
                            <div class=" btnCorpLLogin">
                                <asp:Button ID="cmdLogin" runat="server" Text="Sign in" OnClick="cmdLogin_Click" CssClass="start_creating_btn_CorpLogin rounded_corners5"
                                    ValidationGroup="vgSubmit" TabIndex="3" OnClientClick="return validateLoginProcess();" />
                            </div>

                            <div class="hypNotRegistered">
                                <asp:Label ID="lblNotStoreUser" runat="server" Style="color: #a8a8a8;"></asp:Label>
                                <asp:HyperLink CssClass="forgetPassLink" ID="lnkregistration" runat="server" Text="Sign up" TabIndex="5"></asp:HyperLink>
                            </div>
                        </div>
                    </div>
                    <div id="RealEstateFooterLinks" style="float: right; margin-right: 20px;">
                        <div id="divFooterLinks" style="visibility: hidden; float: left;">
                            <asp:HyperLink Font-Names="Arial" Font-Size="10px" ForeColor="#999999" ID="lnkContactUs" runat="server" Text="contact us" TabIndex="5"></asp:HyperLink>
                            &nbsp;|&nbsp;      
                        </div>
                        <asp:HyperLink Font-Names="Arial" Font-Size="10px" ForeColor="#999999" ID="hplinkTermsCondition" runat="server" Text="terms of business" TabIndex="5"></asp:HyperLink>
                        &nbsp;|&nbsp;        
                        <asp:HyperLink Font-Names="Arial" Font-Size="10px" ForeColor="#999999" ID="hlPrivacyPolicy" runat="server" Text="privacy statement" TabIndex="5"></asp:HyperLink>

                    </div>
                    <div class="clear"></div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" language="javascript">
        // Set the fous
        $(document).ready(function () {
            $('#<%=txtEmail.ClientID %>').focus();

            var isLoginSteps = '<%= Convert.ToString(ConfigurationManager.AppSettings["IsLoginSteps"]) %>';

            if (isLoginSteps == 1) {
                $("#<%= divsigninwithacc.ClientID %>").css("display", "none");
                $("#<%= divemailaddress.ClientID %>").text("Sign in").removeClass("Fsize17").addClass("LoginTitle");
                $("#<%= divemailaddress.ClientID %>").attr('style', 'font-size: 25px !important;');
                $(".emailMessage").css("display", "block");
                $(".Fsize14").css("width", "60%");
                $("#<%= txtEmail.ClientID %>").attr("PlaceHolder", "Type your email address here");
                $("#<%= txtEmail.ClientID %>").attr("Width", "").css("width", "");
                $("#<%= txtEmail.ClientID %>").css("width", "93% !important");
                $("#LinkButtons_Div_Username").css("display", "block");
                $("#divPasswordSection").css("display", "none");
                $(".page_border_div").css("padding-left", "200px");
                $(".page_border_div").css("padding-bottom", "20px");


                //$(".LoginTitle").css("visibility", "visible");
                //$(".Message_Username").css("visibility", "visible");
                //$("#LinkButtons_Div_Username").css("visibility", "visible");
                //$(".Validator_Username").css("visibility", "visible");
                //$(".ButtonsRealEstateLoginUsername").css("visibility", "visible");
            }
            else {
                //$(".Message_PAssword").css("visibility", "hidden");
                //$("#LinkButtons_Div").css("visibility", "hidden");
                //$(".Validator_Password").css("visibility", "hidden");

            }



        });

        $('#btnNextUsername').click(function () {
            var name = $("#<%=txtEmail.ClientID%>").val(); //get value
            if ((name == null) || (name == '')) {

                $("#<%=lblCantBeBlank.ClientID%>").html("CAN'T BE BLANK");
            }
            else {
                var emailaddress = $("#<%=txtEmail.ClientID%>").val();
                if (validateEmail(emailaddress)) {
                    $("#divPasswordSection").css("display", "block");
                    $("#LinkButtons_Div").css("display", "block");
                    $("#divEmailSection").css("display", "none");
                    $("#<%=lblPassword.ClientID%>").css("display", "none");
                    $("#<%=lnkForgotPassword.ClientID%>").css("display", "none");
                    $("#divPasswordMessage").css("display", "block");
                    $("#<%= txtPassword.ClientID %>").attr("PlaceHolder", "Type your password here");
                    $("#<%= txtPassword.ClientID %>").attr("Width", "").css("width", "");
                    $("#<%= txtPassword.ClientID %>").css("width", "57%");
                    $("#<%= cmdLogin.ClientID %>").removeClass("start_creating_btn_CorpLogin").addClass("ButtonsRealEstateLoginUsername");
                    $(".mrginBtm").css("width", "");
                    $("#<%= cmdLogin.ClientID %>").val("sign-in");

                    $("#divLoginPassword").css("width", "60% !important");
                    $(".hypNotRegistered").css("display", "none");
                    $("#divFooterLinks").css("visibility", "visible");
                    $("#<%= cmdLogin.ClientID %>").removeClass("rounded_corners5");
                    $('#<%=txtPassword.ClientID %>').focus();
                }
                else {
                    $("#<%=lblCantBeBlank.ClientID%>").html("INVALID EMAIL");
                }

            }
        });
        function validateEmail(sEmail) {
            var filter = /^[\w\-\.\+]+\@[a-zA-Z0-9\.\-]+\.[a-zA-z0-9]{2,4}$/;
            if (filter.test(sEmail)) {
                return true;
            }
            else {
                return false;
            }
        }

        function validateLoginProcess() {
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
            if (isDataFilles == 1) {
                return false;
            } else {
                var IsEmailValid = ValidateEmail($('#<%= txtEmail.ClientID %>').val());
                if (IsEmailValid == true) {

                    return true;

                } else {
                    $('.LEmail').css("display", "block");
                    $('.LEmail').text("Please enter valid email.");
                    $("#<%= txtEmail.ClientID %>").parent().addClass("field-with-errors");
                      return false;
                  }
              }
          }

    </script>
</asp:Content>
