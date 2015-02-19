<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ThemeSite.Master"
    CodeBehind="PartnerSignup.aspx.cs" Inherits="Web2Print.UI.PartnerSignup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/BreadCrumbMenu.ascx" TagName="BreadCrumbMenu" TagPrefix="uc2" %>
<%@ Register Src="Controls/WhyChooseUs.ascx" TagName="WhyChooseUs" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContents" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content_area">
        <div class="left_right_padding">
            <uc2:BreadCrumbMenu ID="BreadCumbMenuCategory" runat="server" WorkMode="Signup" MyAccountCurrentPage="Register"
                MyAccountCurrentPageUrl="Signup.aspx" />
            <div class="signin_heading_div">
                <asp:Label ID="lblNewCustomer" runat="server" CssClass="sign_in_heading" Text="New User Registration"></asp:Label>
            </div>
            <div class="page_border_div rounded_corners">
                <div class="sign_up_page_box">
                    <table width="100%">
                        <tr>
                            <td class="signup_left_width" valign="top">
                                <div class="register_message" id="divform2createaccount" runat="server">
                                    Complete the form below to register and create an account...
                                </div>
                                <br />
                                <table width="100%" class="custom_lin_height">
                                    <tr>
                                        <td class="td50" valign="top">
                                            <asp:Label ID="labelFName" runat="server" Text="First Name "></asp:Label>
                                            <br />
                                            <asp:TextBox ID="txtFirstName" runat="server" CssClass="text_box200 rounded_corners5"
                                                MaxLength="100"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName"
                                                ErrorMessage="<%$ Resources:MyResource, PlxenterFname %>" ForeColor="Red" Text="*"></asp:RequiredFieldValidator>
                                            <cc1:ValidatorCalloutExtender ID="vceFirstName" runat="server" TargetControlID="rfvFirstName"
                                                Width="250px">
                                            </cc1:ValidatorCalloutExtender>
                                            <br />
                                            <asp:Label ID="label1" runat="server" Text="Last Name "></asp:Label>
                                            <br />
                                            <asp:TextBox ID="txtLastName" runat="server" CssClass="text_box200 rounded_corners5"
                                                MaxLength="100"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName"
                                                ErrorMessage="<%$ Resources:MyResource, PlxenterLname %>" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <cc1:ValidatorCalloutExtender ID="vceLastName" runat="server" TargetControlID="rfvLastName"
                                                Width="250px">
                                            </cc1:ValidatorCalloutExtender>
                                            <br />
                                            <asp:Label ID="label2" runat="server" Text="E-Mail address "></asp:Label>
                                            <br />
                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="text_box200 rounded_corners5"
                                                MaxLength="255" AutoCompleteType="None"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                                                ErrorMessage="<%$ Resources:MyResource, Plxenteremail %>" ForeColor="Red" Text="*"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="revEmail" runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                ControlToValidate="txtEmail" ForeColor="Red" Text="*" ErrorMessage="<%$ Resources:MyResource, PlxenterVEmail %>"></asp:RegularExpressionValidator>
                                            <cc1:ValidatorCalloutExtender ID="vceEmail" runat="server" TargetControlID="rfvEmail"
                                                Width="250px">
                                            </cc1:ValidatorCalloutExtender>
                                            <cc1:ValidatorCalloutExtender ID="vceValidMail" runat="server" TargetControlID="revEmail"
                                                Width="250px">
                                            </cc1:ValidatorCalloutExtender>
                                            <br />
                                            <asp:Label ID="label3" runat="server" Text="Re type E-Mail Address "></asp:Label>
                                            <br />
                                            <asp:TextBox ID="txtConfirmEmail" runat="server" CssClass="text_box200 rounded_corners5"
                                                MaxLength="255" AutoCompleteType="None"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvConfirmEmail" runat="server" ControlToValidate="txtConfirmEmail"
                                                ErrorMessage="<%$ Resources:MyResource, PlxenterCEmail %>" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <cc1:ValidatorCalloutExtender ID="vceconfirmMail" runat="server" TargetControlID="rfvConfirmEmail"
                                                Width="250px">
                                            </cc1:ValidatorCalloutExtender>
                                            <asp:CompareValidator ID="cvConfirmEmail" runat="server" ControlToCompare="txtEmail"
                                                ControlToValidate="txtConfirmEmail" ErrorMessage="<%$ Resources:MyResource, PlxenterEmailNotS %>"
                                                Text="*" ForeColor="Red"></asp:CompareValidator>
                                            <cc1:ValidatorCalloutExtender ID="vceCompareMail" runat="server" TargetControlID="cvConfirmEmail"
                                                Width="250px">
                                            </cc1:ValidatorCalloutExtender>
                                            <br />
                                            <asp:Label ID="label4" runat="server" Text="Enter Password "></asp:Label>
                                            <br />
                                            <asp:TextBox ID="txtRegPassword" runat="server" TextMode="Password" CssClass="text_box200 rounded_corners5"
                                                AutoCompleteType="None" MaxLength="20"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvRegPassword" runat="server" ControlToValidate="txtRegPassword"
                                                ErrorMessage="<%$ Resources:MyResource, PlxenterPass %>" ForeColor="Red" Text="*"></asp:RequiredFieldValidator>
                                            <cc1:ValidatorCalloutExtender ID="vcePassword" runat="server" TargetControlID="rfvRegPassword"
                                                Width="250px">
                                            </cc1:ValidatorCalloutExtender>
                                            <br />
                                            <asp:Label ID="label5" runat="server" Text="Re type Password "></asp:Label>
                                            <br />
                                            <asp:TextBox ID="txtRegConfirmPass" runat="server" TextMode="Password" CssClass="text_box200 rounded_corners5"
                                                AutoCompleteType="None" MaxLength="20"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvRegConfirmPass" runat="server" ControlToValidate="txtRegConfirmPass"
                                                ErrorMessage="<%$ Resources:MyResource, PlxenterCPass %>" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="cvRegConfirmPass" runat="server" Text="*" ControlToCompare="txtRegPassword"
                                                ControlToValidate="txtRegConfirmPass" ErrorMessage="Password is not same." ForeColor="Red"></asp:CompareValidator>
                                            <cc1:ValidatorCalloutExtender ID="vceConfirmPass" runat="server" TargetControlID="rfvRegConfirmPass"
                                                Width="250px">
                                            </cc1:ValidatorCalloutExtender>
                                            <cc1:ValidatorCalloutExtender ID="vceComparePass" runat="server" TargetControlID="cvRegConfirmPass"
                                                Width="250px">
                                            </cc1:ValidatorCalloutExtender>
                                        </td>
                                        <td class="td50" valign="top">
                                            <asp:Label ID="label6" runat="server" Text="Secret Question "></asp:Label>
                                            <br />
                                            <asp:DropDownList ID="ddlbSecretQuestion" runat="server" CssClass="dropdown rounded_corners5"
                                                Width="215px">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvSecQuestion" runat="server" ControlToValidate="ddlbSecretQuestion"
                                                ErrorMessage="<%$ Resources:MyResource, selectaSQ %>" Text="*" InitialValue="-1"
                                                ForeColor="Red"></asp:RequiredFieldValidator>
                                            <cc1:ValidatorCalloutExtender ID="vceQuestion" runat="server" TargetControlID="rfvSecQuestion"
                                                Width="250px">
                                            </cc1:ValidatorCalloutExtender>
                                            <br />
                                            <asp:Label ID="label7" runat="server" Text="Secret Answer  "></asp:Label>
                                            <br />
                                            <asp:TextBox ID="txtSecretAns" runat="server" CssClass="text_box200 rounded_corners5"
                                                MaxLength="200"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvSecretAns" runat="server" ControlToValidate="txtSecretAns"
                                                ErrorMessage="<%$ Resources:MyResource, PlxenterSQ %>" Text="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <cc1:ValidatorCalloutExtender ID="vceSecretAns" runat="server" TargetControlID="rfvSecretAns"
                                                Width="250px">
                                            </cc1:ValidatorCalloutExtender>
                                            <br />
                                            <br />
                                            <asp:CheckBox ID="chkEmailMeOffers" Text="Send Me Email" runat="server" />
                                            <br />
                                            <asp:CheckBox ID="chkSendMeNewsLetters" Text="Send Me Newsletters" runat="server"
                                                Checked="true" />
                                            <br />
                                            <div class="top80">
                                                &nbsp;</div>
                                            <asp:Button ID="cmdRegister" runat="server" Text="CREATE ACCOUNT" OnClick="cmdRegister_Click"
                                                CssClass="start_creating_btn rounded_corners5" />
                                            <br />
                                            <asp:Label ID="lblSignupMessage" runat="server" CssClass="errorMessage"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="separator">
                                &nbsp;
                            </td>
                            <td class="signup_width" valign="top">
                                <div class="custom_color_heading" id="divwhyregister" runat="server">
                                    Why Register?</div>
                                <div class="space5">
                                    &nbsp;</div>
                                <asp:Label ID="lblRegistering" runat="server" CssClass="registering_label" Text="By registering you are able to:"></asp:Label>
                                <div class="registering_item" id="divsaveurdeisgn" runat="server">
                                    Save your designs
                                </div>
                                <div class="registering_item_separator">
                                    &nbsp;</div>
                                <div class="registering_item" id="divviewntrackoh" runat="server">
                                    View and track order history
                                </div>
                                <div class="registering_item_separator">
                                    &nbsp;</div>
                                <div class="registering_item" id="divreorderq" runat="server">
                                    Re-order quickly
                                </div>
                                <div class="registering_item_separator">
                                    &nbsp;</div>
                                <div class="registering_item" id="divsubs2newsl" runat="server">
                                    Subscribe to our Newsletters on new marketing products<br />
                                    and services
                                </div>
                                <div class="registering_item_separator">
                                    &nbsp;</div>
                                    <div class="registering_item" id="lblTitleRequestQuote" runat="server">
                                    Request a quote
                                </div>
                                <div class="registering_item_separator">
                                    &nbsp;</div>
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
            </div>
            <br />
            <br />
            <br />
        </div>
    </div>
    <script type="text/javascript" language="javascript">
        // Set the focus
        $(document).ready(function () {
            $('#<%=txtFirstName.ClientID %>').focus();
        });
    </script>
</asp:Content>
