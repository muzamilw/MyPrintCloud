<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrUserQuickText.ascx.cs" Inherits="Web2Print.UI.DashBoardControls.ctrUserQuickText" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Controls/ResetPassword.ascx" TagName="ResetPassword" TagPrefix="uc2" %>
<%@ Register Src="~/Controls/BreadCrumbMenu.ascx" TagName="BreadCrumbMenu" TagPrefix="uc1" %>
<%@ Register Src="~/Controls/MainHeading.ascx" TagName="MainHeading" TagPrefix="uc3" %>
<link href="~/LightBox/css/jquery.lightbox-0.5.css" rel="stylesheet" type="text/css" />
<link href="~/Styles/CommonStyles.css" rel="stylesheet" type="text/css" />
 <div class="content_area">
        <div class="left_right_padding">
            <uc1:BreadCrumbMenu ID="BreadCumbMenuCategory" runat="server" WorkMode="MyAccount"
                MyAccountCurrentPage="Template Quick Feild" MyAccountCurrentPageUrl="UserQuickTextInfo.aspx" />
            <div class="signin_heading_div">
                <asp:Label ID="lblTitle" runat="server" Text="Template Quick Feild" CssClass="sign_in_heading"></asp:Label>
            </div>
            <div class="white_back_div rounded_corners">
               
                <div class="pad20">
                 <div class="product_detail_sub_heading custom_color floatleft clearBoth">
                            <asp:Literal ID="lblHead" runat="server" Text="View and update your design details:"></asp:Literal>
                        </div>
                        <br />
                    <div class="normalTextStyle divHalfRightProfileQT">
                        <div class="TLR">
                            <asp:Label ID="label9" runat="server" Text="Your Name "></asp:Label>
                        </div>
                        <div class="TTL">
                            <asp:TextBox ID="txtqtName" runat="server" CssClass="text_box200 rounded_corners5"
                                MaxLength="100" TabIndex="1"></asp:TextBox>
                        </div>
                        <div class="clearBoth">
                        </div>
                        <br />
                        <div class="TLR">
                            <asp:Label ID="label10" runat="server" Text="Job Title "></asp:Label>
                        </div>
                        <div class="TTL">
                            <asp:TextBox ID="txtqtTitle" runat="server" CssClass="text_box200 rounded_corners5"
                                MaxLength="100" TabIndex="2"></asp:TextBox>
                        </div>
                        <div class="clearBoth">
                        </div>
                        <br />
                        <div class="TLR">
                            <asp:Label ID="label11" runat="server" Text="Your Company Name "></asp:Label>
                        </div>
                        <div class="TTL">
                            <asp:TextBox ID="txtqtCompanyName" runat="server" CssClass="text_box200 rounded_corners5"
                                MaxLength="100" TabIndex="3"></asp:TextBox>
                        </div>
                        <div class="clearBoth">
                        </div>
                        <br />
                        <div class="TLR">
                            <asp:Label ID="label15" runat="server" Text="Telephone "></asp:Label>
                        </div>
                        <div class="TTL">
                            <asp:TextBox ID="txtqtPhone" runat="server" CssClass="text_box200 rounded_corners5"
                                MaxLength="100" TabIndex="4"></asp:TextBox>
                        </div>
                        <div class="clearBoth">
                        </div>
                        <br />
                        <div class="TLR">
                            <asp:Label ID="label16" runat="server" Text="Fax "></asp:Label>
                        </div>
                        <div class="TTL">
                            <asp:TextBox ID="txtqtFax" runat="server" CssClass="text_box200 rounded_corners5"
                                MaxLength="100" TabIndex="5"></asp:TextBox>
                        </div>
                        <div class="clearBoth">
                        </div>
                        <br />
                        <div class="TLR">
                            <asp:Label ID="label17" runat="server" Text="Email Address "></asp:Label>
                        </div>
                        <div class="TTL">
                            <asp:TextBox ID="txtqtEmail" runat="server" CssClass="text_box200 rounded_corners5"
                                MaxLength="100" TabIndex="6"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="regExpEmail" runat="server" ControlToValidate="txtqtEmail"
                                ForeColor="#ffffff" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="Email is not valid"></asp:RegularExpressionValidator>
                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                TargetControlID="regExpEmail" Width="250px">
                            </ajaxToolkit:ValidatorCalloutExtender>
                        </div>
                        <div class="clearBoth">
                        </div>
                        <br />
                        <div class="TLR">
                            <asp:Label ID="lblWebAdd" runat="server" Text="Website Address "></asp:Label>
                        </div>
                        <div class="TTL">
                            <asp:TextBox ID="txtqtWebsite" runat="server" CssClass="text_box200 rounded_corners5"
                                MaxLength="100" TabIndex="7"></asp:TextBox>
                        </div>
                        <div class="clearBoth">
                        </div>
                        <br />
                        <div class="TLR">
                            <asp:Label ID="lblAddres" runat="server" Text="Address "></asp:Label>
                        </div>
                        <div class="TTL">
                            <asp:TextBox ID="txtqtAddress1" CssClass="text_box200 rounded_corners5" runat="server"
                                TextMode="MultiLine" Height="50px" MaxLength="3000" TabIndex="8"></asp:TextBox>
                        </div>
                        <div class="clearBoth">
                        </div>
                        <br />
                        <div class="TLR">
                            <asp:Label ID="lblCompnyMesg" runat="server" Text="Company Message "></asp:Label>
                        </div>
                        <div class="TTL">
                            <asp:TextBox ID="txtqtCompMessage" runat="server" CssClass="text_box200 rounded_corners5"
                                TextMode="MultiLine" Height="40px" MaxLength="3000" TabIndex="9"></asp:TextBox>
                        </div>
                        <div class="clearBoth">
                        </div>
                        <br />
                    </div>
                    <div class="clearBoth">
                    </div>
                    <div class="user_profile_bottom_buttonsQT">
                        <div>
                            <asp:Button ID="cmdSave" runat="server" Text="Save" OnClick="cmdSave_Click" CssClass="start_creating_btn rounded_corners5"
                                TabIndex="11" />
                            &nbsp;<asp:Button ID="cmdCancel" runat="server" CausesValidation="False" Text="Cancel"
                                CssClass="start_creating_btn rounded_corners5" PostBackUrl="~/DashBoard.aspx"
                                TabIndex="13" />
                        </div>
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

    </script>
