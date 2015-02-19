<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ThemeSite.Master" AutoEventWireup="true"
    CodeBehind="ContactDetailsWidget.aspx.cs" Inherits="Web2Print.UI.contactDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Controls/QuickLinks.ascx" TagName="QuickLinks" TagPrefix="uc5" %>
<%@ Register Src="Controls/ResetPassword.ascx" TagName="ResetPassword" TagPrefix="uc1" %>
<%@ Register Src="~/Controls/BreadCrumbMenu.ascx" TagName="BreadCrumbMenu" TagPrefix="uc2" %>
<asp:Content ID="ProfileContent1" ContentPlaceHolderID="HeadContents" runat="server">
</asp:Content>
<asp:Content ID="ProfileContent2" ContentPlaceHolderID="PageBanner" runat="server">
</asp:Content>
<asp:Content ID="ProfileContent3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content_area container">
        <div class="left_right_padding row">
            <div id="MessgeToDisply" class="rounded_corners messageBoxNew" runat="server" visible="false">
                <asp:Literal ID="ltrlMessge" runat="server"></asp:Literal>
            </div>
              <div class="signin_heading_div float_left_simple dashboard_heading_signin">
                  <asp:Label ID="lblHead" runat="server" Text="Contact Us page details :"  CssClass="sign_in_heading" ></asp:Label>
            </div>
            <div class="dashBoardRetrunLink">
             <uc2:BreadCrumbMenu ID="BreadCumbMenuCategory" runat="server" WorkMode="MyAccount"
                                MyAccountCurrentPage="My Profile" MyAccountCurrentPageUrl="UserProfile.aspx" />
                     </div>
            <div class="clearBoth">

            </div>
            <div align="center" id="divUserProfile" runat="server">
                <div class="white-container-lightgrey-border rounded_corners">
                    <div class="pad20">
                        <div class="headingsAvenior float_left_simple">
                            
                        </div>
                        <div class="cntdashBoardRetrunLink">
                           
                        </div>
                        <br />

                        <div class="divHalfRightProfile">
                            <div class="smallContctUsAvenior float_left_simple" id="div2" runat="server">
                                Telephone
                            </div>
                            <div class="TTL widthAvenior">
                                <span class="error-message Etelephone" style="display: none;">can't be blank</span><br>
                                <asp:TextBox ID="txtTelephone" runat="server" CssClass="newTxtBox"
                                    TabIndex="1"></asp:TextBox>
                            </div>
                            <div class="clearBoth">
                            </div>
                            <br />
                            <div class="smallContctUsAvenior float_left_simple" id="div3" runat="server">
                                Fax
                            </div>
                            <div class="TTL widthAvenior">
                                <asp:TextBox ID="txtFax" runat="server" CssClass="newTxtBox"
                                    TabIndex="1"></asp:TextBox>

                            </div>
                            <div class="clearBoth">
                            </div>
                            <br />
                            <div class="smallContctUsAvenior float_left_simple" id="div4" runat="server">
                                Email
                            </div>
                            <div class="TTL widthAvenior">
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="newTxtBox"
                                    TabIndex="1"></asp:TextBox>

                            </div>
                            <div class="clearBoth">
                            </div>
                            <br />
                            <div class="smallContctUsAvenior float_left_simple" id="divfname" runat="server">
                                Address 1
                            </div>
                            <div class="TTL widthAvenior">
                                <span class="error-message EAddress1" style="display: none;">can't be blank</span><br>
                                <asp:TextBox ID="txtAddress1" runat="server" CssClass="newTxtBox"
                                    TabIndex="1"></asp:TextBox>
                            </div>
                            <div class="clearBoth">
                            </div>
                            <br />
                            <div class="smallContctUsAvenior float_left_simple" id="divlname" runat="server">
                                Address 2
                            </div>
                            <div class="TTL widthAvenior">
                                <asp:TextBox ID="txtAddress2" runat="server" CssClass="newTxtBox"
                                    TabIndex="2"></asp:TextBox>
                            </div>
                            <div class="clearBoth">
                            </div>
                            <br />
                            <div class="smallContctUsAvenior float_left_simple" id="divemail" runat="server">
                                City
                            </div>
                            <div class="TTL widthAvenior">
                                <asp:TextBox ID="txtCity" runat="server" Enabled="true" CssClass="newTxtBox"
                                    TabIndex="3"></asp:TextBox>
                            </div>
                            <div class="clearBoth">
                            </div>
                            <br />
                            <div class="smallContctUsAvenior float_left_simple" id="divjobtitle" runat="server">
                                State
                            </div>
                            <div class="TTL widthAvenior">
                                <asp:TextBox ID="txtState" runat="server" CssClass="newTxtBox"
                                    TabIndex="4"></asp:TextBox>
                            </div>
                            <div class="clearBoth">
                            </div>
                            <br />
                            <div class="smallContctUsAvenior float_left_simple" id="divphonenumber" runat="server">
                                Zip Code
                            </div>
                            <div class="TTL widthAvenior">
                                <asp:TextBox ID="txtZipCode" runat="server" CssClass="newTxtBox"
                                    TabIndex="5"></asp:TextBox>
                            </div>
                            <div class="clearBoth">
                            </div>
                            <br />
                            <div class="smallContctUsAvenior float_left_simple" id="divcellnumber" runat="server">
                                Country
                            </div>
                            <div class="TTL widthAvenior">
                                <asp:TextBox ID="txtCountry" runat="server" CssClass="newTxtBox"
                                    TabIndex="6"></asp:TextBox>
                            </div>
                            <div class="clearBoth">
                            </div>
                            <br />
                        </div>
                        <div class="clearBoth">
                            &nbsp;
                        </div>
                        <div>
                            <h3 class="headingsAvenior">Enter in your Post code in google to get your Geo co-ordinates for longitude and latitude.
                                    <br />
                                <br />
                                This will help us list you as the nearest parter for web enquiries.
                            </h3>
                        </div>
                        <div class="divHalfRightProfile" >
                            <div class="smallContctUsAvenior float_left_simple" id="divfax" runat="server">
                                Latitude
                            </div>
                            <div class="TTL widthAvenior">
                                <span class="error-message ELatitude" style="display: none;">can't be blank</span><br>
                                <asp:TextBox ID="txtLatitude" runat="server" CssClass="newTxtBox"
                                    TabIndex="7"></asp:TextBox>

                            </div>

                            <div class="clearBoth">
                            </div>
                            <br />
                            <div class="smallContctUsAvenior float_left_simple" id="divcompanyname" runat="server">
                                Longitude
                            </div>
                            <div class="TTL widthAvenior">
                                <span class="error-message ELongitude" style="display: none;">can't be blank</span><br>
                                <asp:TextBox ID="txtLongitude" runat="server" CssClass="newTxtBox"
                                    TabIndex="8"></asp:TextBox>
                            </div>
                            <div class="clearBoth">
                            </div>
                            <br />
                            <div class="smallContctUsAvenior float_left_simple" id="div5" runat="server">
                                &nbsp;
                            </div>
                            <div class="TTL widthAvenior">
                                <asp:Button ID="btnSave" runat="server" CssClass="start_creating_btn rounded_corners5"
                                    Text="Save" OnClick="btnSave_Click" TabIndex="13" Width="100px" OnClientClick="return ValidateBoxes();" />
                                &nbsp;&nbsp;
                            <asp:Button ID="btnCancel" runat="server" CssClass="start_creating_btn rounded_corners5"
                                Text="Cancel" PostBackUrl="~/DashBoard.aspx" TabIndex="15" Width="100px" />


                            </div>
                        </div>
                    </div>


                    <div class="clearBoth">
                    </div>

                </div>
            </div>
        </div>
        <div style="height: 10px">
            <asp:HiddenField ID="hdContactID" runat="server" Value="0" />
            <asp:HiddenField ID="hdDeliveryAddressID" runat="server" Value="0" />
            <asp:HiddenField ID="hdBillingAddressID" runat="server" Value="0" />
            <asp:HiddenField ID="hfIsPageValid"  runat="server" Value="1"/>
        </div>
    </div>
    <br />
    <br />
    <br />
    <asp:Label ID="lblMesgSavedSuccesfully" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblerrorMesge" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblPassChangedSuccesfully" runat="server" Visible="false"></asp:Label>
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            if ($("#<%= hfIsPageValid.ClientID %>").val() == 0) {
                ValidateBoxes();
            }

        });



        function FileUploaderHide() {

            $('input:file').MultiFile('reset');
        }


        function ValidateBoxes() {
            var isValid = true;
            var isDataFilles = 0;

            if ($("#<%= txtTelephone.ClientID %>").val() == '') {
                isDataFilles = 1;
                $('.Etelephone').css("display", "block");
                $("#<%= txtTelephone.ClientID %>").parent().addClass("field-with-errors");
            } else {
                $('.Etelephone').css("display", "none");
                $("#<%= txtTelephone.ClientID %>").parent().removeClass("field-with-errors");
        }
            if ($("#<%= txtAddress1.ClientID %>").val() == '') {
                isDataFilles = 1;
                $('.EAddress1').css("display", "block");
                $("#<%= txtAddress1.ClientID %>").parent().addClass("field-with-errors");

            } else {
                $('.EAddress1').css("display", "none");
                $("#<%= txtAddress1.ClientID %>").parent().removeClass("field-with-errors");
        }
            if ($("#<%= txtLatitude.ClientID %>").val() == '') {
                $('.ELatitude').css("display", "block");
                $("#<%= txtLatitude.ClientID %>").parent().addClass("field-with-errors");

                isDataFilles = 1;
            } else {
                $('.ELatitude').css("display", "none");
                $("#<%= txtLatitude.ClientID %>").parent().removeClass("field-with-errors");

        }
            if ($("#<%= txtLongitude.ClientID %>").val() == '') {
                $('.ELongitude').css("display", "block");
                $("#<%= txtLongitude.ClientID %>").parent().addClass("field-with-errors");

                isDataFilles = 1;
            } else {
                $('.ELongitude').css("display", "none");
                $("#<%= txtLongitude.ClientID %>").parent().removeClass("field-with-errors");

            }

            if (isDataFilles == 1) {
                $("#<%= hfIsPageValid.ClientID %>").val(0);
                isValid = false;
            } else {
                $("#<%= hfIsPageValid.ClientID %>").val(1);
                isValid = true;
            }

            return isValid;
        }

    </script>
</asp:Content>
