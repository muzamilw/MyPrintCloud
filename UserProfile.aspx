<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ThemeSite.Master" AutoEventWireup="true"
    CodeBehind="UserProfile.aspx.cs" Inherits="Web2Print.UI.UserProfile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Controls/QuickLinks.ascx" TagName="QuickLinks" TagPrefix="uc5" %>
<%@ Register Src="Controls/ResetPassword.ascx" TagName="ResetPassword" TagPrefix="uc1" %>
<%@ Register Src="~/Controls/BreadCrumbMenu.ascx" TagName="BreadCrumbMenu" TagPrefix="uc2" %>
<asp:Content ID="ProfileContent1" ContentPlaceHolderID="HeadContents" runat="server">
    <style>
        input[type="checkbox"]
        {
            display: none;
            outline: none !important;
            -webkit-transition: background-color;
            -moz-transition: background-color;
            -o-transition: background-color;
            -ms-transition: background-color;
            transition: background-color;
        }

            input[type="checkbox"] + label
            {
                display: inline-block !important;
                padding: 6px 0 6px 45px;
                line-height: 25px;
                background-image: url("//cdn.shopify.com/s/files/1/0245/8513/t/7/assets/checkbox_sprite.png?4214");
                background-image: none,url("//cdn.shopify.com/s/files/1/0245/8513/t/7/assets/checkbox_sprite.svg?4214");
                background-position: -108px 0;
                background-repeat: no-repeat;
                -webkit-background-size: 143px 143px;
                -moz-background-size: 143px 143px;
                background-size: 143px 143px;
                overflow: visible;
                outline: none;
                -webkit-user-select: none;
                -moz-user-select: none;
                -ms-user-select: none;
                user-select: none;
                cursor: pointer;
                cursor: hand;
                color: #66615b;
                outline: none !important;
            }

                input[type="checkbox"]:hover + label, input[type="checkbox"] + label:hover, input[type="checkbox"]:hover + label:hover
                {
                    background-position: -72px -36px;
                    color: #403d39;
                }

            input[type="checkbox"]:checked + label
            {
                background-position: -36px -72px;
                color: #403d39;
            }

                input[type="checkbox"]:checked:hover + label, input[type="checkbox"]:checked + label:hover, input[type="checkbox"]:checked:hover + label:hover
                {
                    background-position: 0 -108px;
                    color: #403d39;
                }
    </style>
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
               <asp:Label ID="lblHead" runat="server" Text="View and update your profile:" CssClass="sign_in_heading" ></asp:Label>

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
                        <div class="cntHalfRightProfile">
                            <div class="smallContctUsAvenior float_left_simple" id="divfname" runat="server">
                                First Name
                            </div>
                            <div class="TTL widthAvenior">
                                <span class="error-message EFirstName" style="display: none;">can't be blank</span>
                                <asp:TextBox ID="txtFirstName" runat="server" CssClass="newTxtBox" MaxLength="50"
                                    TabIndex="1"></asp:TextBox>
                            </div>
                            <div class="clearBoth">
                            </div>
                            <br />
                            <div class="smallContctUsAvenior float_left_simple" id="divlname" runat="server">
                                Last Name
                            </div>
                            <div class="TTL widthAvenior">
                                <asp:TextBox ID="txtLastName" runat="server" CssClass="newTxtBox" MaxLength="50"
                                    TabIndex="2"></asp:TextBox>
                            </div>
                            <div class="clearBoth">
                            </div>
                            <br />
                            <div class="smallContctUsAvenior float_left_simple" id="divemail" runat="server">
                                Email
                            </div>
                            <div class="TTL widthAvenior">
                                <asp:TextBox ID="txtUserEmail" runat="server" Enabled="false" CssClass="newTxtBox" MaxLength="100"
                                    TabIndex="3"></asp:TextBox>
                            </div>
                            <div class="clearBoth">
                            </div>
                            <br />
                            <div class="smallContctUsAvenior float_left_simple" id="divjobtitle" runat="server">
                                Job Title
                            </div>
                            <div class="TTL widthAvenior">
                                <asp:TextBox ID="txtJobTitle" runat="server" CssClass="newTxtBox" MaxLength="50"
                                    TabIndex="4"></asp:TextBox>
                            </div>
                            <div class="clearBoth">
                            </div>
                            <br />
                            <div class="smallContctUsAvenior float_left_simple" id="divphonenumber" runat="server">
                                Phone Number
                            </div>
                            <div class="TTL widthAvenior">
                                <asp:TextBox ID="txtUserPhone1" runat="server" CssClass="newTxtBox" MaxLength="30"
                                    TabIndex="5"></asp:TextBox>
                            </div>
                            <div class="clearBoth">
                            </div>
                            <br />
                            <div class="smallContctUsAvenior float_left_simple" id="divcellnumber" runat="server">
                                Cell Number
                            </div>
                            <div class="TTL widthAvenior">
                                <asp:TextBox ID="txtUserCellNo" runat="server" CssClass="newTxtBox" MaxLength="30"
                                    TabIndex="6"></asp:TextBox>
                            </div>
                            <div class="clearBoth">
                            </div>
                            <br />
                            <div class="smallContctUsAvenior float_left_simple" id="divfax" runat="server">
                                Fax
                            </div>
                            <div class="TTL widthAvenior">
                                <asp:TextBox ID="txtUserFax" runat="server" CssClass="newTxtBox" MaxLength="30"
                                    TabIndex="7"></asp:TextBox>
                            </div>
                            <div class="clearBoth">
                            </div>
                            <br />
                            <div id="CompanyNameOfBroker" runat="server">
                                <div class="smallContctUsAvenior float_left_simple" id="divcompanyname" runat="server">
                                    Company Name
                                </div>
                                <div class="TTL widthAvenior">
                                    <span class="error-message ECompanyName" style="display: none;">can't be blank</span><br>
                                    <asp:TextBox ID="txtCompanyName" runat="server" CssClass="newTxtBox" MaxLength="100"
                                        TabIndex="8"></asp:TextBox>

                                </div>
                                <div class="clearBoth">
                                </div>
                                <br />
                            </div>
                            <div id="WebsiteUrlOfBroker" runat="server">
                                <div class="smallContctUsAvenior float_left_simple" id="divwebsite" runat="server">
                                    Website
                                </div>
                                <div class="TTL widthAvenior">
                                    <asp:TextBox ID="txtWebSite" runat="server" CssClass="newTxtBox" MaxLength="150"
                                        TabIndex="9"></asp:TextBox>
                                </div>
                                <div class="clearBoth">
                                </div>
                                <br />
                            </div>
                            <div id="coorporateUserFields" runat="server" visible="false">
                                <div >
                                    <div class="smallContctUsAvenior float_left_simple" id="divtxtPOBox" runat="server">
                                        PO Box Address
                                    </div>
                                    <div class="TTL widthAvenior">
                                        <asp:TextBox ID="txtPOBox" runat="server" CssClass="newTxtBox" MaxLength="500"
                                            TabIndex="9"></asp:TextBox>
                                    </div>
                                    <div class="clearBoth">
                                    </div>
                                    <br />
                                </div>
                                <div >
                                    <div class="smallContctUsAvenior float_left_simple" id="divtxtCunit" runat="server">
                                        Business/Corporate Unit
                                    </div>
                                    <div class="TTL widthAvenior">
                                        <asp:TextBox ID="txtCunit" runat="server" CssClass="newTxtBox" MaxLength="500"
                                            TabIndex="9"></asp:TextBox>
                                    </div>
                                    <div class="clearBoth">
                                    </div>
                                    <br />
                                </div>
                                <div >
                                    <div class="smallContctUsAvenior float_left_simple" id="divtxtOfcTrading" runat="server">
                                        Office Trading Name
                                    </div>
                                    <div class="TTL widthAvenior">
                                        <asp:TextBox ID="txtOfcTrading" runat="server" CssClass="newTxtBox" MaxLength="500"
                                            TabIndex="9"></asp:TextBox>
                                    </div>
                                    <div class="clearBoth">
                                    </div>
                                    <br />
                                </div>
                                <div >
                                    <div class="smallContctUsAvenior float_left_simple" id="divtxtContractorName" runat="server">
                                        Contractor Name
                                    </div>
                                    <div class="TTL widthAvenior">
                                        <asp:TextBox ID="txtContractorName" runat="server" CssClass="newTxtBox" MaxLength="500"
                                            TabIndex="9"></asp:TextBox>
                                    </div>
                                    <div class="clearBoth">
                                    </div>
                                    <br />
                                </div>
                                <div >
                                    <div class="smallContctUsAvenior float_left_simple" id="divtxtBpay" runat="server">
                                        Bpay CRN
                                    </div>
                                    <div class="TTL widthAvenior">
                                        <asp:TextBox ID="txtBpay" runat="server" CssClass="newTxtBox" MaxLength="500"
                                            TabIndex="9"></asp:TextBox>
                                    </div>
                                    <div class="clearBoth">
                                    </div>
                                    <br />
                                </div>
                                <div >
                                    <div class="smallContctUsAvenior float_left_simple" id="divtxtABN" runat="server">
                                        ABN
                                    </div>
                                    <div class="TTL widthAvenior">
                                        <asp:TextBox ID="txtABN" runat="server" CssClass="newTxtBox" MaxLength="500"
                                            TabIndex="9"></asp:TextBox>
                                    </div>
                                    <div class="clearBoth">
                                    </div>
                                    <br />
                                </div>
                                <div >
                                    <div class="smallContctUsAvenior float_left_simple" id="divtxtACN" runat="server">
                                        ACN
                                    </div>
                                    <div class="TTL widthAvenior">
                                        <asp:TextBox ID="txtACN" runat="server" CssClass="newTxtBox" MaxLength="500"
                                            TabIndex="9"></asp:TextBox>
                                    </div>
                                    <div class="clearBoth">
                                    </div>
                                    <br />
                                </div>
                                <div>
                                    <div class="smallContctUsAvenior float_left_simple" id="divtxtAf1" runat="server">
                                        Additional Field 1
                                    </div>
                                    <div class="TTL widthAvenior">
                                        <asp:TextBox ID="txtAf1" runat="server" CssClass="newTxtBox" MaxLength="500"
                                            TabIndex="9"></asp:TextBox>
                                    </div>
                                    <div class="clearBoth">
                                    </div>
                                    <br />
                                </div>
                                <div >
                                    <div class="smallContctUsAvenior float_left_simple" id="divtxtAf2" runat="server">
                                        Additional Field 2
                                    </div>
                                    <div class="TTL widthAvenior">
                                        <asp:TextBox ID="txtAf2" runat="server" CssClass="newTxtBox" MaxLength="500"
                                            TabIndex="9"></asp:TextBox>
                                    </div>
                                    <div class="clearBoth">
                                    </div>
                                    <br />
                                </div>
                                <div >
                                    <div class="smallContctUsAvenior float_left_simple" id="divtxtAf3" runat="server">
                                        Additional Field 3
                                    </div>
                                    <div class="TTL widthAvenior">
                                        <asp:TextBox ID="txtAf3" runat="server" CssClass="newTxtBox" MaxLength="500"
                                            TabIndex="9"></asp:TextBox>
                                    </div>
                                    <div class="clearBoth">
                                    </div>
                                    <br />
                                </div>
                                <div >
                                    <div class="smallContctUsAvenior float_left_simple" id="divtxtAf4" runat="server">
                                        Additional Field 4
                                    </div>
                                    <div class="TTL widthAvenior"> 
                                        <asp:TextBox ID="txtAf4" runat="server" CssClass="newTxtBox" MaxLength="500"
                                            TabIndex="9"></asp:TextBox>
                                    </div>
                                    <div class="clearBoth">
                                    </div>
                                    <br />
                                </div>
                                <div >
                                    <div class="smallContctUsAvenior float_left_simple" id="divtxtAf5" runat="server">
                                        Additional Field 5
                                    </div>
                                    <div class="TTL widthAvenior">
                                        <asp:TextBox ID="txtAf5" runat="server" CssClass="newTxtBox" MaxLength="500"
                                            TabIndex="9"></asp:TextBox>
                                    </div>
                                    <div class="clearBoth">
                                    </div>
                                    <br />
                                </div>


                            </div>
                            <div class="smallContctUsAvenior float_left_simple" id="div2" runat="server">
                                Profile Picture
                            </div>
                            <div class="TTL widthAvenior">
                                <asp:FileUpload ID="fuImageUpload" runat="server" CssClass="newTxtBox"
                                    TabIndex="12" />
                            </div>
                            <div class="clearBoth">
                            </div>
                            <br />
                            <div class="smallContctUsAvenior float_left_simple" id="div3" runat="server">
                            </div>
                            <div class="TTL widthAvenior">
                                <asp:Image ID="imgContactPicture" runat="server" CssClass="user_image_container float_left_simple" />
                                <asp:Button ID="btnUploadFile" runat="server" CssClass="btnUploadImage_UP float_left_simple rounded_corners5"
                                    Text="Upload Image" OnClick="btnUploadFile_Click" Style="display: none;" />
                                <div class="clearBoth">
                                </div>
                            </div>
                            <div class="clearBoth">
                            </div>
                            <br />
                            <div class="smallContctUsAvenior float_left_simple">
                                &nbsp;
                            </div>
                            <div class="TTL widthAvenior">
                                <asp:CheckBox ID="chkEmailMeOffers" Text="Notify me of marketing and promotions"
                                    runat="server" TabIndex="10" />
                            </div>
                            <div class="clearBoth">
                            </div>
                            <br />
                            <div class="smallContctUsAvenior float_left_simple">
                                &nbsp;
                            </div>
                            <div class="TTL widthAvenior">
                                <asp:CheckBox ID="chkSendMeNewsLetters" Text="Subscribe to Newsletters" runat="server"
                                    TabIndex="11" Checked="true" />
                            </div>
                            <div class="clearBoth">
                            </div>
                            <br />
                            <br />
                            <div class="smallContctUsAvenior float_left_simple">
                                &nbsp;
                            </div>
                            <div class="TTL widthAvenior">
                                <asp:Button ID="btnSave" runat="server" CssClass="start_creating_btn rounded_corners5"
                                    Text="Save" OnClick="btnSave_Click" TabIndex="13" Width="100px" OnClientClick="return ValidateUSer();" />
                                &nbsp;&nbsp;
                            <asp:Button ID="btnCancel" runat="server" CssClass="start_creating_btn rounded_corners5"
                                Text="Cancel" PostBackUrl="~/DashBoard.aspx" TabIndex="15" Width="100px" />
                            </div>
                        </div>
                        <div class="clearBoth">
                        </div>
                        <br />
                    </div>
                </div>
            </div>
            <div style="height: 10px">
                <asp:HiddenField ID="hdContactID" runat="server" Value="0" />
                <asp:HiddenField ID="hdDeliveryAddressID" runat="server" Value="0" />
                <asp:HiddenField ID="hdBillingAddressID" runat="server" Value="0" />
                <asp:HiddenField ID="hfisUserValid" runat="server" Value="1" />
            </div>
        </div>
        <br />
        <br />
        <br />
    </div>
    <asp:Button ID="btnPopup" runat="server" Style="display: none; width: 0px; height: 0px"
        Text="TEST" />
    <ajaxToolkit:ModalPopupExtender ID="mdlResetPwdPopup" runat="server" BackgroundCssClass="ModalPopupBG"
        PopupControlID="pnlPopup" TargetControlID="btnPopup" BehaviorID="mdlResetPwdPopup"
        DropShadow="false">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlPopup" runat="server" Width="600px" CssClass="FileUploaderPopup_Mesgbox LCLB rounded_corners"
        Style="display: none">
        <div style="background-color: White;">
            <div class="Width100Percent">
                <div class="float_left">
                    <asp:Label ID="lblHeader" runat="server" CssClass="FileUploadHeaderText" Text="Reset Password"></asp:Label>
                </div>
                <div class="exit_container">
                    <div id="Div1" onclick="PopupHide();" runat="server" class="exit_popup">
                    </div>
                </div>
            </div>
            <div class="pop_body_RP">
                <br />
                <div class="inner">
                    <uc1:ResetPassword ID="ctrlResetPassword" runat="server" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Label ID="lblMesgSavedSuccesfully" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblerrorMesge" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblPassChangedSuccesfully" runat="server" Visible="false"></asp:Label>
    <script type="text/javascript" language="javascript">

        function PopupShow() {

            $find('mdlResetPwdPopup').show();
        }



    </script>
    <script type="text/javascript" language="javascript">
        // Set foucs and Multip handler
        $(document).ready(function () {

            CreateMultipleUpload();

            if ($("#<%= hfisUserValid.ClientID %>").val() == 0) {
                ValidateUSer();
            }
        });

        function CreateMultipleUpload() {
            //set up the file upload
            $("#" + "<%=fuImageUpload.ClientID %>").MultiFile(
              {
                  max: 1,
                  accept: 'jpg,bmp,png',
                  afterFileSelect: function (element, value, master_element) {
                      $('#<%=btnUploadFile.ClientID %>').css('display', 'block');
                  },
                  afterFileRemove: function (element, value, master_element) {
                      $('#<%=btnUploadFile.ClientID %>').css('display', 'none');
                  }
              });
              }


              function FileUploaderHide() {

                  $('input:file').MultiFile('reset');
              }

              function ValidateUSer() {
                  var isValid = true;
                  var isDataFilles = 0;

                  if ($("#<%= txtFirstName.ClientID %>").val() == '') {
                      isDataFilles = 1;
                      $('.EFirstName').css("display", "block");
                      $("#<%= txtFirstName.ClientID %>").parent().addClass("field-with-errors");
                  } else {
                      $('.EFirstName').css("display", "none");
                      $("#<%= txtFirstName.ClientID %>").parent().removeClass("field-with-errors");
            }
            if ($("#<%= txtCompanyName.ClientID %>").val() == '') {
                      isDataFilles = 1;
                      $('.ECompanyName').css("display", "block");
                      $("#<%= txtCompanyName.ClientID %>").parent().addClass("field-with-errors");

                  } else {
                      $('.ECompanyName').css("display", "none");
                      $("#<%= txtCompanyName.ClientID %>").parent().removeClass("field-with-errors");
            }

            if (isDataFilles == 1) {
                $("#<%= hfisUserValid.ClientID %>").val(0);
                isValid = false;
            } else {
                $("#<%= hfisUserValid.ClientID %>").val(1);
                isValid = true;
            }

            return isValid;
        }


    </script>
</asp:Content>
