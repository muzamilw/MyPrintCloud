<%@ Page Title="Quick text fields for designs" Language="C#" MasterPageFile="~/MasterPages/ThemeSite.Master" AutoEventWireup="true"
    CodeBehind="UserQuickTextInfo.aspx.cs" Inherits="Web2Print.UI.UserQuickTextInfo" %>

<%@ Register Src="Controls/ResetPassword.ascx" TagName="ResetPassword" TagPrefix="uc2" %>
<%@ Register Src="~/Controls/BreadCrumbMenu.ascx" TagName="BreadCrumbMenu" TagPrefix="uc1" %>
<%@ Register Src="~/Controls/MainHeading.ascx" TagName="MainHeading" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContents" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBanner" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content_area container">
        <div class="left_right_padding row">
            <div class="signin_heading_div float_left_simple dashboard_heading_signin">
              <asp:Label ID="lblTitle" runat="server" Text="Template Quick Feild" CssClass="sign_in_heading"></asp:Label>

            </div>
            <div class="dashBoardRetrunLink">
            <uc1:BreadCrumbMenu ID="BreadCumbMenuCategory" runat="server" WorkMode="MyAccount"
                            MyAccountCurrentPage="Template Quick Feild" MyAccountCurrentPageUrl="UserQuickTextInfo.aspx" />
                     </div>
            <div class="clearBoth">

            </div>
            <div class="white-container-lightgrey-border rounded_corners">

                <div class="pad20">
                    <div class="headingsAvenior float_left_simple">
                        <asp:Literal ID="lblHead" runat="server" Text="View and update your template credentials (Quick Text)"></asp:Literal>
                    </div>
                    <br />
                    <div class="normalTextStyle divHalfRightProfile">
                        <div class="smallContctUsAvenior float_left_simple">
                            <asp:Label ID="label9" runat="server" Text="Your Name "></asp:Label>
                        </div>
                        <div class="TTL widthAvenior">
                            <asp:TextBox ID="txtqtName" runat="server" CssClass="newTxtBox"
                                MaxLength="200" TabIndex="1"></asp:TextBox>
                        </div>
                        <div class="clearBoth">
                        </div>
                        <br />
                        <div class="smallContctUsAvenior float_left_simple">
                            <asp:Label ID="label10" runat="server" Text="Job Title "></asp:Label>
                        </div>
                        <div class="TTL widthAvenior">
                            <asp:TextBox ID="txtqtTitle" runat="server" CssClass="newTxtBox"
                                MaxLength="50" TabIndex="2"></asp:TextBox>
                        </div>
                        <div class="clearBoth">
                        </div>
                        <br />
                        <div class="smallContctUsAvenior float_left_simple">
                            <asp:Label ID="label11" runat="server" Text="Your Company Name "></asp:Label>
                        </div>
                        <div class="TTL widthAvenior">
                            <asp:TextBox ID="txtqtCompanyName" runat="server" CssClass="newTxtBox"
                                MaxLength="200" TabIndex="3"></asp:TextBox>
                        </div>
                        <div class="clearBoth">
                        </div>
                        <br />
                        <div class="smallContctUsAvenior float_left_simple">
                            <asp:Label ID="label15" runat="server" Text="Telephone "></asp:Label>
                        </div>
                        <div class="TTL widthAvenior">
                            <asp:TextBox ID="txtqtPhone" runat="server" CssClass="newTxtBox"
                                MaxLength="20" TabIndex="4"></asp:TextBox>
                        </div>
                        <div class="clearBoth">
                        </div>
                        <br />
                        <div class="smallContctUsAvenior float_left_simple">
                            <asp:Label ID="label16" runat="server" Text="Fax "></asp:Label>
                        </div>
                        <div class="TTL widthAvenior">
                            <asp:TextBox ID="txtqtFax" runat="server" CssClass="newTxtBox"
                                MaxLength="20" TabIndex="5"></asp:TextBox>
                        </div>
                        <div class="clearBoth">
                        </div>
                        <br />
                        <div class="smallContctUsAvenior float_left_simple">
                            <asp:Label ID="label17" runat="server" Text="Email Address "></asp:Label>
                        </div>
                        <div class="TTL widthAvenior">
                            <asp:TextBox ID="txtqtEmail" runat="server" CssClass="newTxtBox"
                                MaxLength="200" TabIndex="6"></asp:TextBox>
                        </div>
                        <div class="clearBoth">
                        </div>
                        <br />
                        <div class="smallContctUsAvenior float_left_simple">
                            <asp:Label ID="lblWebAdd" runat="server" Text="Website Address "></asp:Label>
                        </div>
                        <div class="TTL widthAvenior">
                            <asp:TextBox ID="txtqtWebsite" runat="server" CssClass="newTxtBox"
                                MaxLength="200" TabIndex="7"></asp:TextBox>
                        </div>
                        <div class="clearBoth">
                        </div>
                        <br />
                        <div class="smallContctUsAvenior float_left_simple">
                            <asp:Label ID="lblAddres" runat="server" Text="Address "></asp:Label>
                        </div>
                        <div class="TTL widthAvenior">
                            <asp:TextBox ID="txtqtAddress1" CssClass="newTxtBox" runat="server"
                                TextMode="MultiLine" Height="50px" MaxLength="255" TabIndex="8"></asp:TextBox>
                        </div>
                        <div class="clearBoth">
                        </div>
                        <br />
                        <div class="smallContctUsAvenior float_left_simple">
                            <asp:Label ID="lblCompnyMesg" runat="server" Text="Company Message "></asp:Label>
                        </div>
                        <div class="TTL widthAvenior">
                            <asp:TextBox ID="txtqtCompMessage" runat="server" CssClass="newTxtBox"
                                TextMode="MultiLine" MaxLength="1000" TabIndex="9"></asp:TextBox>
                        </div>
                        <div class="clearBoth">
                        </div>
                        <br />

                        <div class="smallContctUsAvenior float_left_simple">
                            <asp:Label ID="lblMobileNo" runat="server" Text="Mobile number"></asp:Label>
                        </div>
                        <div class="TTL widthAvenior">
                            <asp:TextBox ID="txtMobileNo" runat="server" CssClass="newTxtBox"
                                MaxLength="30" TabIndex="9"></asp:TextBox>
                        </div>
                        <div class="clearBoth">
                        </div>
                        <br />
                        <div class="smallContctUsAvenior float_left_simple">
                            <asp:Label ID="lblTwitterID" runat="server" Text="Twitter ID"></asp:Label>
                        </div>
                        <div class="TTL widthAvenior">
                            <asp:TextBox ID="txtTwitterID" runat="server" CssClass="newTxtBox"
                                MaxLength="150" TabIndex="9"></asp:TextBox>
                        </div>
                        <div class="clearBoth">
                        </div>
                        <br />
                        <div class="smallContctUsAvenior float_left_simple">
                            <asp:Label ID="LblFBURL" runat="server" Text="Facebook ID"></asp:Label>
                        </div>
                        <div class="TTL widthAvenior">
                            <asp:TextBox ID="txtFBID" runat="server" CssClass="newTxtBox"
                                MaxLength="150" TabIndex="9"></asp:TextBox>
                        </div>
                        <div class="clearBoth">
                        </div>
                        <br />
                        <div class="smallContctUsAvenior float_left_simple">
                            <asp:Label ID="lblLinkedIN" runat="server" Text="LinkedIn ID"></asp:Label>
                        </div>
                        <div class="TTL widthAvenior">
                            <asp:TextBox ID="txtLinkedInUrl" runat="server" CssClass="newTxtBox"
                                MaxLength="150" TabIndex="9"></asp:TextBox>
                        </div>
                        <div class="clearBoth">
                        </div>
                        <br />
                        <div class="smallContctUsAvenior float_left_simple">
                            <asp:Label ID="lblOtherID" runat="server" Text="Other ID"></asp:Label>
                        </div>
                        <div class="TTL widthAvenior">
                            <asp:TextBox ID="txtOtherId" runat="server" CssClass="newTxtBox"
                                MaxLength="150" TabIndex="9"></asp:TextBox>
                        </div>
                        <div class="clearBoth">
                        </div>
                        <br />

                        <div class="smallContctUsAvenior float_left_simple">
                            &nbsp;
                        </div>
                        <div class="TTL widthAvenior">
                            <asp:Button ID="cmdSave" runat="server" Text="Save" CssClass="start_creating_btn rounded_corners5"
                                TabIndex="11" OnClick="cmdSave_Click" OnClientClick="return QuickValidate();" />
                            &nbsp;<asp:Button ID="cmdCancel" runat="server" CausesValidation="False" Text="Cancel"
                                CssClass="start_creating_btn rounded_corners5" PostBackUrl="~/DashBoard.aspx"
                                TabIndex="13" />
                        </div>
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
    <asp:Label ID="lblMesgSavedSuccesfully" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblerrorMesge" runat="server" Visible="false"></asp:Label>
    <script type="text/javascript" language="javascript">
        // Set foucs and Multip handler


        function displayFileUploadPopup(btnUploadFile) {
            var result = false;
            result = validateFileSelection();
            if (result == false)
                alert("Please select file");
            else {
            }
            return result;
        }

        function validateFileSelection() {


            var addedFileDivsList = $("div .MultiFile-label");

            if (addedFileDivsList.length > 0)
                return true;
            else
                return false;
        }
    </script>
    <script type="text/javascript" language="javascript">

        function QuickValidate() {
            var IsEmailValid = ValidateEmail($('#<%= txtqtEmail.ClientID %>').val());
            if (IsEmailValid == false) {
                ShowPopup('Message', "please enter valid email to proceed.");
                return false;
            }
        }

        $('#<%= txtqtEmail.ClientID %>').blur(function () {
            if ($('#<%= txtqtEmail.ClientID %>').val() === '') {
             } else {
                 var IsEmailValid = ValidateEmail($('#<%= txtqtEmail.ClientID %>').val());
                if (IsEmailValid == false) {
                    ShowPopup('Message', "please enter valid email to proceed.");
                }
            }
         });
    </script>
</asp:Content>
