<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Step2.aspx.cs" MasterPageFile="PinkRegister.Master"
    Inherits="Web2Print.UI.Step2" %>
<%@ Register TagPrefix="recaptcha" Namespace="Recaptcha" Assembly="Recaptcha" %>
<%@ Register Src="PinkRegFooter.ascx" TagName="PinkRegFooter" TagPrefix="uc1" %>
<%@ Register Src="~/PinkRegistration/Header.ascx" TagPrefix="uc1" TagName="Header" %>

<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="cphHeader">
    <link href="RegistrationSite.css" rel="stylesheet" />
    <link href="../Styles/jquery-ui.css" rel="stylesheet" />
    <style>
        .ui-autocomplete-loading
        {
            background: white url('../images/pu_loader.gif') right center no-repeat;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainContent">
    <div id="container" class="step1">
        <uc1:Header runat="server" ID="Header" />
        <div id="main">
            <div id="content">
                <div id="msgbox" class="messageboxReg" style="display:none;"></div>
                <div style="margin: 0; padding: 0; display: inline">
                    <div id="email">
                        <label for="order_email" class="username"></label>
                        <div>
                            <span class="error-message EMEmail" style="display: none;">can't be blank</span><br>
                            <input id="txtEmail" name="checkout[email]" size="30" tabindex="1" value="" runat="server" x-autocompletetype="email" />
                            <p style="text-align: right; font-size: 12px !important;">
                                (this email address will become your username on PinkCards.com)
                            </p>
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
                            <h3>Company Information</h3>
                            <table cellspacing="0" cellpadding="0" class="form" id="billing">
                                <tbody>
                                    <tr>
                                        <td class="lbl">
                                            <label for="txtCompanyName">Company Name</label></td>
                                        <td>
                                            <div>
                                                <span class="error-message EMCompanyName" style="display: none;">can't be blank</span><br>
                                                <input class="short" id="txtCompanyName" name="billing_address[first_name]" size="30" tabindex="4" type="text" value="" runat="server" x-autocompletetype="section-billing given-name">
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lbl">
                                            <label for="txtAddress">Address 1</label></td>
                                        <td>
                                            <div>
                                                <span class="error-message EMAddress" style="display: none;">can't be blank</span><br>
                                                <input class="short" id="txtAddress" name="billing_address[last_name]" size="30" tabindex="5" type="text" value="" runat="server" x-autocompletetype="section-billing surname">
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lbl">
                                            <label for="txtCity">Town/ City</label></td>
                                        <td>
                                            <div>
                                                <span class="error-message EMCity" style="display: none;">can't be blank</span><br>
                                                <input class="short" id="txtCity" name="billing_address[company]" size="30" tabindex="6" type="text" value="" runat="server" x-autocompletetype="section-billing organization">
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lbl">
                                            <label for="txtState">State</label></td>
                                        <td>
                                            <div>
                                                <span class="error-message EMCountry" style="display: none;">can't be blank</span><br>
                                                <input id="txtState" name="billing_address[address1]" size="30" tabindex="7" type="text" value="" runat="server" x-autocompletetype="section-billing address-line1">
                                            </div>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="lbl">
                                            <label for="txtPostCode">Zip/Post Code</label></td>
                                        <td style="width: 260px">
                                            <div>
                                                <span class="error-message EMZipCode" style="display: none;">can't be blank</span><br>
                                                <input id="txtPostCode" name="billing_address[address2]" size="30" tabindex="8" type="text" value="" runat="server" x-autocompletetype="section-billing address-line2">
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                           <td>Verification text</td>
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
                        <%-- <div class="gleft" id="cntCLogo" style="display:none">
                            <h3>Your Company Logo (optional)</h3>
                            <table cellspacing="0" cellpadding="0" class="form">
                                <tbody>
                                    <tr>
                                        <td class="lbl">
                                            <label for="FileLogo">Upload Logo</label></td>
                                        <td>
                                            <div>
                                                <span class="error-message EMFileUpload" style="display: none;">Please select file.</span><br>
                                                <asp:FileUpload ID="FileLogo" runat="server" ClientIDMode="Static" TabIndex="9" />
                                            </div>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>


                        </div>--%>

                        <div class="gleft" id="cntPCodess">
                          
              <%-- <p style="text-align: left; font-size: 12px !important; padding-top: 40px;">* No payment will be taken on Registration.</p>--%>
                        </div>
                        <div style="clear: both"></div>
                        
                    </div>
                    <div id="buttons">
                        <asp:Button ID="btnContinue" runat="server" Text="Create Store Now" CssClass="btnContinueReg" OnClientClick="return ContinueRegistration();" OnClick="btnCompleteReg_Click" TabIndex="12" />

                    </div>
                </div>
                <asp:Button ID="btnOnPostBack" runat="server" Style="display: none;" OnClick="btnCompleteReg_Click" />
                <asp:HiddenField ID="isFileUploded" runat="server" Value="0" />
                <asp:HiddenField ID="hfContactEmailEror" runat="server" Value="0" />
                <asp:HiddenField ID="hfisValidPC1" runat="server" Value="0" />
                <asp:HiddenField ID="hfisValidPC2" runat="server" Value="0" />
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

                if ($("#<%= txtPostCode.ClientID %>").val() == "") {
                isDataFilled = 1;
                if (scrolElement == "") {
                    scrolElement = "cntCompanyInfo";
                }
                $('.EMZipCode').css("display", "block");
                $("#<%= txtPostCode.ClientID %>").parent().addClass("field-with-errors");
            } else {
                $('.EMZipCode').css("display", "none");
                $("#<%= txtPostCode.ClientID %>").parent().removeClass("field-with-errors");
                }

                if ($("#<%= txtState.ClientID %>").val() == "") {
                isDataFilled = 1;
                if (scrolElement == "") {
                    scrolElement = "cntCompanyInfo";
                }
                $('.EMCountry').css("display", "block");
                $("#<%= txtState.ClientID %>").parent().addClass("field-with-errors");
            } else {
                $('.EMCountry').css("display", "none");
                $("#<%= txtState.ClientID %>").parent().removeClass("field-with-errors");
                }

                if ($("#<%= txtCity.ClientID %>").val() == "") {
                isDataFilled = 1;
                if (scrolElement == "") {
                    scrolElement = "cntCompanyInfo";
                }
                $('.EMCity').css("display", "block");
                $("#<%= txtCity.ClientID %>").parent().addClass("field-with-errors");
            } else {
                $('.EMCity').css("display", "none");
                $("#<%= txtCity.ClientID %>").parent().removeClass("field-with-errors");
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

                if ($("#<%= txtAddress.ClientID %>").val() == "") {
                isDataFilled = 1;
                if (scrolElement == "") {
                    scrolElement = "cntCompanyInfo";
                }
                $('.EMAddress').css("display", "block");
                $("#<%= txtAddress.ClientID %>").parent().addClass("field-with-errors");
            } else {
                $('.EMAddress').css("display", "none");
                $("#<%= txtAddress.ClientID %>").parent().removeClass("field-with-errors");
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

                function CreateMultipleUpload() {
                    //set up the file upload
                    $("#FileLogo").MultiFile(
                      {
                          max: 1,
                          accept: 'gif,jpg,jpeg,jpe,bmp',
                          afterFileSelect: function (element, value, master_element) {
                              $("#<%= isFileUploded.ClientID %>").val("1");
                              },
                              afterFileRemove: function (element, value, master_element) {
                                  $("#<%= isFileUploded.ClientID %>").val("0");
                              }
                          });

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

                              if ($("#<%= hfContactEmailEror.ClientID %>").val() == "1") {
                                  $('.EMEmail').text("Provided email address is already in use, please use another email address.");
                                  $('.EMEmail').css("display", "block");
                                  $("#<%= txtEmail.ClientID %>").parent().addClass("field-with-errors");
                              } else if ($("#<%= hfContactEmailEror.ClientID %>").val() == "2") {
                                  $("#msgbox").css("display", "block");
                                  $("#msgbox").html("The verification text you entered does not match. Please enter the correct verification text");
                                  
                              }
                              CreateMultipleUpload();
                          });




                         

    </script>

</asp:Content>

