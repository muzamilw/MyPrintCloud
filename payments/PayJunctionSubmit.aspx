<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ThemeSite.Master" AutoEventWireup="true" CodeBehind="PayJunctionSubmit.aspx.cs" Inherits="Web2Print.UI.payments.PayJunctionSubmit"%>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContents" runat="server">
    <script src="../Scripts/utilities.js"></script>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container content_area">
        <div class="row left_right_padding">
           
            <div id="errorMesgCnt" runat="server" visible="false" style=" background-color:red; border: 1px solid red; padding:10px;" class="rounded_corners">
                                <asp:Label ID="ErrorMEsSummry" runat="server" style="color: White; font-size: 16px; font-weight:bold;"></asp:Label>
                            </div>
            <div class="white-container-lightgrey-border rounded_corners">
                <div class="pad20">
                    <div class="headingsAvenior float_left_simple">
                        <asp:Literal ID="lblHead" runat="server" Text="Please enter your Credit Card details:"></asp:Literal>
                    </div>
                    <br />
                    <div class="cntHalfRightProfile">
                        <div class="smallContctUsAvenior float_left_simple">
                           &nbsp;
                        </div>
                        <div class="TTL CCtxtPanel"> 
                            
						<img src="../images/visa_49x31.jpg" border="0" alt="VISA">
					
						<img src="../images/MASTERCARD.gif" border="0" alt="MASTERCARD">
					
						<img src="../images/AMEX.gif" border="0" alt="AMEX">
					
						<img src="../images/DCCB.gif" border="0" alt="DCCB">
					
					
                        </div>
                        <div class="clearBoth">
                        </div>
                        <br />
                        <div class="smallContctUsAvenior float_left_simple">
                            Card Number
                        </div>
                        <div class="TTL CCtxtPanel"> 
                            <span class="error-message EFirstName" style="display: none;">can't be blank</span>
                            <asp:TextBox ID="txtCardNumber" runat="server" CssClass="newTxtBox"
                                TabIndex="1"></asp:TextBox>
                        </div>
                        <div class="clearBoth">
                        </div>
                        <br />
                        <div class="smallContctUsAvenior float_left_simple">
                            Card Type
                        </div>
                        <div class="TTL CCtxtPanel">
                            <asp:DropDownList ID="ddCCType" runat="server" CssClass="newTxtBox" OnChange="OnCCChanged();" OnSelectedIndexChanged="ddCCType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="clearBoth">
                        </div>
                        <br />
                        <div class="smallContctUsAvenior float_left_simple">
                            CVV Number
                        </div>
                        <div class="TTL CCtxtPanel">
                            
                            <asp:TextBox ID="txtCvvNumber" runat="server" CssClass="smallTxtBox" MaxLength="3" TabIndex="2"></asp:TextBox>
                        </div>
                        <div class="clearBoth">
                        </div>
                        <br />
                        <div class="smallContctUsAvenior float_left_simple">
                            Expiry
                        </div>
                        <div class="TTL CCtxtPanel">
                            <asp:DropDownList ID="ddDate" runat="server" CssClass="smallTxtBox">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddYEar" runat="server" CssClass="smallTxtBox">
                            </asp:DropDownList>
                        </div>
                        <div class="clearBoth">
                        </div>
                        <br />
                        <div class="smallContctUsAvenior float_left_simple">
                            Name on the Card
                        </div>
                        <div class="TTL CCtxtPanel">
                            <asp:TextBox ID="txtNameOnCard" runat="server" CssClass="newTxtBox"
                                TabIndex="4"></asp:TextBox>
                        </div>
                        <div class="clearBoth">
                        </div>
                        <br />
                        <div class="smallContctUsAvenior float_left_simple">
                            Email Address(optional)
                        </div>
                        <div class="TTL CCtxtPanel">
                            <asp:TextBox ID="txtEmailAddress" runat="server" CssClass="newTxtBox"
                                TabIndex="2"></asp:TextBox>
                        </div>
                        <div class="clearBoth">
                        </div>
                        <br />

                        <div class="smallContctUsAvenior float_left_simple">
                            &nbsp;
                        </div>
                        <div class="TTL CCtxtPanel">
                            <asp:Button ID="cmdPayment" runat="server" Text="Pay" CssClass="start_creating_btn rounded_corners5"
                                TabIndex="11" OnClick="cmdPayment_Click" OnClientClick="return ValidateData();" />
                            &nbsp;<asp:Button ID="cmdReset" runat="server" CausesValidation="False" Text="Reset"
                                CssClass="start_creating_btn rounded_corners5" OnClientClick="return ResetData();"
                              />
                        </div>
                    </div>
                </div>
                <div class="clearBoth">
                    &nbsp;
                </div>
            </div>
        </div>
    </div>
    <script>
        function ResetData() {

            $('#<%= txtCardNumber.ClientID %>').val();
            $('#<%= txtCvvNumber.ClientID %>').val();
            $('#<%= txtNameOnCard.ClientID %>').val();
            return false;
        }

        function OnCCChanged() {
            if ($("select[id=MainContent_ddCCType] option:selected").index() == 4) {
                $("#MainContent_txtCvvNumber").attr('maxlength', '4');
            } else {
                $("#MainContent_txtCvvNumber").attr('maxlength', '3');
            }
        }

        function ValidateData() {
            var isValid = true;
            var isDataFilles = 0;

            if ($("select[id=MainContent_ddCCType] option:selected").index() == 0) {
                        isDataFilles = 1;
                        $("#<%= ddCCType.ClientID %>").css("border", "1px solid #b33c12");
             } else {

             }

            if ($("#<%= txtCardNumber.ClientID %>").val() == '') {
                isDataFilles = 1;
                $('.LEmail').css("display", "block");
                $("#<%= txtCardNumber.ClientID %>").parent().addClass("field-with-errors");
            } else {
                $('.LEmail').css("display", "none");
                $("#<%= txtCardNumber.ClientID %>").parent().removeClass("field-with-errors");
            }

            if ($("#<%= txtCvvNumber.ClientID %>").val() == '') {
                isDataFilles = 1;
                $('.LPass').css("display", "block");
                $("#<%= txtCvvNumber.ClientID %>").parent().addClass("field-with-errors");
            } else {
                $('.LPass').css("display", "none");
                $("#<%= txtCvvNumber.ClientID %>").parent().removeClass("field-with-errors");
            }

            if (isDataFilles == 1) {
                return false;
            } else {
                if ($('#<%= txtEmailAddress.ClientID %>').val() == '') {
                    showProgress();
                    return true;
                }
                else{
                    var IsEmailValid = ValidateEmail($('#<%= txtEmailAddress.ClientID %>').val());
                    showProgress();
                    if (IsEmailValid == true) {
                        return true;
                    } else {
                        $("#<%= txtEmailAddress.ClientID %>").css("border", "1px solid #b33c12");
                        return false;
                    }
                }
               
               
            }
        }
    </script>
</asp:Content>
