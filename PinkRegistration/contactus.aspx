<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="contactus.aspx.cs" Inherits="Web2Print.UI.PinkRegistration.contactus" MasterPageFile="~/PinkRegistration/PinkRegister.Master" %>
<%@ Register TagPrefix="recaptcha" Namespace="Recaptcha" Assembly="Recaptcha" %>
<%@ Register src="Header.ascx" tagname="Header" tagprefix="uc1" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="cphHeader">

    <script src="/pinkregistration/js/main.js"></script>
    <script src="/pinkregistration/js/video.js"></script>
    <script src="/pinkregistration/js/responsive.js"></script>
    <script src="/pinkregistration/js/libs/iOS.js"></script>
    <script src="/pinkregistration/js/libs/iscroll.js"></script>
    <link href="/pinkregistration/RegistrationSite.css" rel="stylesheet" />
    <link href="../Styles/jquery-ui.css" rel="stylesheet" />
    <style>
        .ui-autocomplete-loading {
            background: white url('../images/pu_loader.gif') right center no-repeat;
        }
    </style>
    <uc1:Header ID="Header1" runat="server" />
&nbsp;
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainContent">
    <div id="container" class="step1">
      
        <div id="main">
            <div id="content">

                 <div id="msgbox" class="messagebox" style="display:none;">Thank you for contacting us. Someone will be in contact</div>

                <div id="outsideukdiv" class="messagebox" runat="server" visible="false">We have launched in the UK and soon will be launching in other countries.  Register your interest below in becoming a Country or Pink Partner outside the UK.</div>
                <h1 style="color:#eb5e28;margin-left:193px;margin-bottom:20px;">Book a Demo</h1>
                <div style="margin: 0; padding: 0; display: inline" runat="server" id="contactform">
                   
                    <div id="email">
                        <label for="order_email">Your Contact Email</label>
                        <div>
                            <span class="error-message EMEmail" style="display: none;">can't be blank</span><br>
                            <input id="txtEmail" name="checkout[email]" size="30" tabindex="1" value="" runat="server" x-autocompletetype="email" />
                        </div>
                        <p id="email_correction" style="display: none;"></p>
                    </div>
                    <!-- begin addresses -->
                    <div class="group clearfix" id="addresses">
                        <div class="gleft">
                            <h3>Personal Information</h3>
                            <table cellspacing="0" cellpadding="0" class="form">
                                <tbody>
                                    <tr>
                                        <td class="lbl">
                                            <label for="txtFirstName">Your Name</label></td>
                                        <td>
                                            <div>
                                                <span class="error-message EMName" style="display: none;">can't be blank</span><br>
                                                <input class="short" id="txtFirstName" name="billing_address[first_name]" size="30" tabindex="2" type="text" value="" runat="server" x-autocompletetype="section-billing given-name">
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lbl">
                                            <label for="txtPhone">Phone</label></td>
                                        <td>
                                            <div>
                                                <span class="error-message EMPhone" style="display: none;">can't be blank</span><br>
                                                <input class="short" id="txtPhone" name="billing_address[last_name]" size="30" tabindex="3" type="text" value="" runat="server" x-autocompletetype="section-billing surname">
                                            </div>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="gleft" id="cntCompanyInfo">
                            
                            <table cellspacing="0" cellpadding="0" class="form" id="billing">
                                <tbody>
                                    <tr>
                                        <td class="lbl">
                                            <label for="txtCompanyName">Company Name</label></td>
                                        <td>
                                            <div>
                                                <span class="error-message EMCompanyName" style="display: none;">can't be blank</span><br>
                                                <input class="short" id="txtCompanyName" name="billing_address[first_name]" size="30" tabindex="4" type="text" value="" runat="server" x-autocompletetype="section-billing given-name" >
                                            </div>
                                        </td>
                                    </tr>
                                    
                                </tbody>
                            </table>
                        </div>

                        <div class="gleft" id="Div1">
                            
                            <table cellspacing="0" cellpadding="0" class="form" id="Table1">
                                <tbody>
                                    <tr>
                                        <td class="lbl">
                                            <label for="txtCompanyName">Enquiry Details</label></td>
                                        <td>
                                            <div>
                                                <span class="error-message EMCompanyName" style="display: none;">can't be blank</span><br>
                                                <textarea class="short" id="txtInquiry" name="billing_address[first_name]" size="30" tabindex="5" type="text" value="" runat="server" x-autocompletetype="section-billing given-name" ></textarea>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Verification text (required)</td>
                                        <td><recaptcha:RecaptchaControl
    ID="recaptcha"
    runat="server"
    PublicKey="6LeXrfASAAAAAEV_Ml9tMccObqz1IZvf-jA2Vjgd "
    PrivateKey="6LeXrfASAAAAADCfcMPxk2aKoTdK-prlfftBJn-1 "
    /></td>
                                    </tr>
                                    
                                </tbody>
                            </table>
                        </div>
                       

                       
                        <div style="clear: both"></div>
                    </div>
                    <div id="buttons">
                        <asp:Button ID="btnContinue" runat="server" Text="Submit" CssClass="btnContinueReg" OnClientClick="return ContinueRegistration();" OnClick="btnCompleteReg_Click" TabIndex="12" />
                    </div>
                </div>
                <asp:Button ID="btnOnPostBack" runat="server" Style="display: none;" OnClick="btnCompleteReg_Click" />
                <asp:HiddenField ID="isFileUploded" runat="server" Value="0" />
                <asp:HiddenField ID="hfContactEmailEror" runat="server" Value="0" />
                 <asp:HiddenField ID="hfSuccessful" runat="server" Value="0" />
            </div>
            <div style="clear: both;">&nbsp;</div>
            
        </div>
    </div>
    <script src="../Scripts/jquery-1.9.1.min.js"></script>
    <script src="../Scripts/jquery-ui-1.9.2.custom.min.js"></script>
    <script src="../Scripts/jquery.MultiFile.js"></script>
    <script type="text/javascript">



        function ContinueRegistration() {
            var isDataFilled = 0;
            var scrolElement = "";
            var ContactEmail = $("#<%= txtEmail.ClientID %>").val();
            if (ContactEmail == "") {
                isDataFilled = 1;
                if (scrolElement == "") {
                    scrolElement = "email";
                }
                $('.EMEmail').css("display", "block");
                $("#<%= txtEmail.ClientID %>").parent().addClass("field-with-errors");
            } else {
                if (isEmailValid(ContactEmail)) {
                    $('.EMEmail').css("display", "none");
                    $("#<%= txtEmail.ClientID %>").parent().removeClass("field-with-errors");
                } else {
                    isDataFilled = 1;
                    if (scrolElement == "") {
                        scrolElement = "email";
                    }
                    $('.EMEmail').css("display", "block");
                    $('.EMEmail').text("Please enter valid email address.");
                    $("#<%= txtEmail.ClientID %>").parent().addClass("field-with-errors");
                }
            }

            var FrstEmail = $("#<%= txtFirstName.ClientID %>").val();

            if (FrstEmail == "") {
                isDataFilled = 1;
                if (scrolElement == "") {
                    scrolElement = "addresses";
                }
                $('.EMName').css("display", "block");
                $("#<%= txtFirstName.ClientID %>").parent().addClass("field-with-errors");
            } else {
                $('.EMName').css("display", "none");
                $("#<%= txtFirstName.ClientID %>").parent().removeClass("field-with-errors");
            }

            if ($("#<%= txtPhone.ClientID %>").val() == "") {
                isDataFilled = 1;
                if (scrolElement == "") {
                    scrolElement = "addresses";
                }
                $('.EMPhone').css("display", "block");
                $("#<%= txtPhone.ClientID %>").parent().addClass("field-with-errors");
            } else {
                $('.EMPhone').css("display", "none");
                $("#<%= txtPhone.ClientID %>").parent().removeClass("field-with-errors");
            }


            if ($("#<%= txtCompanyName.ClientID %>").val() == "") {
                isDataFilled = 1;
                if (scrolElement == "") {
                    scrolElement = "cntCompanyInfo";
                }
                $('.EMCompanyName').css("display", "block");
                $("#<%= txtCompanyName.ClientID %>").parent().addClass("field-with-errors");
            } else {
                $('.EMCompanyName').css("display", "none");
                $("#<%= txtCompanyName.ClientID %>").parent().removeClass("field-with-errors");
            }



            if (isDataFilled == 0) {

                $("#<%= btnOnPostBack.ClientID %>").click();

                return true;
            } else {
                event.preventDefault();
                $('html, body').animate({
                    scrollTop: $("#" + scrolElement + "").offset().top
                }, 500);

                return false;
            }
        }


        function isEmailValid(email) {
            var isValid = true;
            if (email == '') {
                isValid = false;
            }
            else {
                var re = new RegExp("^[A-Za-z0-9](([_\\.\\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\\.\\-]?[a-zA-Z0-9]+)*)\\.([A-Za-z]{2,})$");
                if (!re.test(email)) {

                    isValid = false;
                }
            }
            return isValid;
        }


        $(document).ready(function () {
            //alert($("#<%= hfSuccessful.ClientID %>").val());
            if ($("#<%= hfContactEmailEror.ClientID %>").val() == "1") {
                $('.EMEmail').text("Provided email address is already in use, please use another email address.");
                $('.EMEmail').css("display", "block");
                $("#<%= txtEmail.ClientID %>").parent().addClass("field-with-errors");
            } else if ($("#<%= hfSuccessful.ClientID %>").val() == "1") {
                $("#msgbox").css("display", "block");
                $("#msgbox").html("Your inquiry has been sent. An account manager will call you soon.");
                $("#<%= hfSuccessful.ClientID %>").val(0);
            } else if ($("#<%= hfSuccessful.ClientID %>").val() == "2") {
                $("#msgbox").css("display", "block");
                $("#msgbox").html("An error occured. Please try again.");
                $("#<%= hfSuccessful.ClientID %>").val(0);
            } else if ($("#<%= hfSuccessful.ClientID %>").val() == "3") {
                $("#msgbox").css("display", "block");
                $("#msgbox").html("An error just occurred and has been notified to the webmaster");
                $("#<%= hfSuccessful.ClientID %>").val(0);
            } else if ($("#<%= hfSuccessful.ClientID %>").val() == "4") {
                $("#msgbox").css("display", "block");
                $("#msgbox").html("The verification text you entered does not match. Please enter the correct verification text");
                $("#<%= hfSuccessful.ClientID %>").val(0);
            }

        });




                      

    </script>
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="cphFooter">
    <div class="clearBoth">
        &nbsp;
    </div>
</asp:Content>

