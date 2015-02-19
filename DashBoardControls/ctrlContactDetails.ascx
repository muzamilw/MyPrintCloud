<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlContactDetails.ascx.cs" Inherits="Web2Print.UI.DashBoardControls.ctrlContactDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<link href="/LightBox/css/jquery.lightbox-0.5.css" rel="stylesheet" type="text/css" />
<link href="/Styles/CommonStyles.css" rel="stylesheet" type="text/css" />

 <div class="content_area">
        <div class="left_right_padding">
            
            <div class="signin_heading_div">
                <asp:Label ID="lblTitle" runat="server" Text="Contact Information" CssClass="sign_in_heading"></asp:Label>
            </div>
            <div align="center" id="divUserProfile" runat="server">
                <div class="white_back_div rounded_corners">
                    <div class="pad20">
                        <div class="product_detail_sub_heading custom_color floatleft clearBoth">
                            <asp:Literal ID="lblHead" runat="server" Text="View and update your profile:"></asp:Literal>
                        </div>
                        <br />
                        <div class="normalTextStyle divHalfRightProfile">
                            <div class="TLR" id="divfname" runat="server">
                                First Name
                            </div>
                            <div class="TTL">
                                <asp:TextBox ID="txtFirstName" runat="server" CssClass="text_box200 rounded_corners5"
                                    TabIndex="1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ErrorMessage="<%$ Resources:MyResource, PlxenterFname %>"
                                    Text="*" ForeColor="#cc0000" ControlToValidate="txtFirstName" />
                                <ajaxToolkit:ValidatorCalloutExtender ID="vceFirstName" runat="server" TargetControlID="rfvFirstName"
                                    Width="250px">
                                </ajaxToolkit:ValidatorCalloutExtender>
                            </div>
                            <div class="clearBoth">
                            </div>
                            <br />
                            <div class="TLR" id="divlname" runat="server">
                                Last Name
                            </div>
                            <div class="TTL">
                                <asp:TextBox ID="txtLastName" runat="server" CssClass="text_box200 rounded_corners5"
                                    TabIndex="2"></asp:TextBox>
                            </div>
                            <div class="clearBoth">
                            </div>
                            <br />
                            <div class="TLR" id="divemail" runat="server">
                                Email
                            </div>
                            <div class="TTL">
                                <asp:TextBox ID="txtUserEmail" runat="server" Enabled="false" CssClass="text_box200 rounded_corners5"
                                    TabIndex="3"></asp:TextBox>
                            </div>
                            <div class="clearBoth">
                            </div>
                            <br />
                            <div class="TLR" id="divjobtitle" runat="server">
                                Job Title
                            </div>
                            <div class="TTL">
                                <asp:TextBox ID="txtJobTitle" runat="server" CssClass="text_box200 rounded_corners5"
                                    TabIndex="4"></asp:TextBox>
                            </div>
                            <div class="clearBoth">
                            </div>
                            <br />
                            <div class="TLR" id="divphonenumber" runat="server">
                                Phone Number
                            </div>
                            <div class="TTL">
                                <asp:TextBox ID="txtUserPhone1" runat="server" CssClass="text_box200 rounded_corners5"
                                    TabIndex="5"></asp:TextBox>
                            </div>
                            <div class="clearBoth">
                            </div>
                            <br />
                            <div class="TLR" id="divcellnumber" runat="server">
                                Cell Number
                            </div>
                            <div class="TTL">
                                <asp:TextBox ID="txtUserCellNo" runat="server" CssClass="text_box200 rounded_corners5"
                                    TabIndex="6"></asp:TextBox>
                            </div>
                            <div class="clearBoth">
                            </div>
                            <br />
                            <div class="TLR" id="divfax" runat="server">
                                Fax
                            </div>
                            <div class="TTL">
                                <asp:TextBox ID="txtUserFax" runat="server" CssClass="text_box200 rounded_corners5"
                                    TabIndex="7"></asp:TextBox>
                            </div>
                            <div class="clearBoth">
                            </div>
                            <br />
                            <div class="TLR" id="divcompanyname" runat="server">
                                Company Name
                            </div>
                            <div class="TTL">
                                <asp:TextBox ID="txtCompanyName" runat="server" CssClass="text_box200 rounded_corners5"
                                    TabIndex="8"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvCompanyName" runat="server" ErrorMessage="<%$ Resources:MyResource, plxenterCompanyName %>"
                                    Text="*" ForeColor="#cc0000" ControlToValidate="txtCompanyName" />
                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                    TargetControlID="rfvCompanyName" Width="250px">
                                </ajaxToolkit:ValidatorCalloutExtender>
                            </div>
                            <div class="clearBoth">
                            </div>
                            <br />
                            <div class="TLR" id="divwebsite" runat="server">
                                Website
                            </div>
                            <div class="TTL">
                                <asp:TextBox ID="txtWebSite" runat="server" CssClass="text_box200 rounded_corners5"
                                    TabIndex="9"></asp:TextBox>
                            </div>
                            <div class="clearBoth">
                            </div>
                            <br />
                            <div class="TLR">
                                &nbsp;
                            </div>
                            <div class="TTBL1">
                                <asp:CheckBox ID="chkEmailMeOffers" Text="Notify me of marketing and promotions"
                                    runat="server" TabIndex="10" />
                            </div>
                            <div class="clearBoth">
                            </div>
                            <br />
                            <div class="TLR">
                                &nbsp;
                            </div>
                            <div class="TTBL1">
                                <asp:CheckBox ID="chkSendMeNewsLetters" Text="Subscribe to Newsletters" runat="server"
                                    TabIndex="11" Checked="true" />
                            </div>
                            <div class="clearBoth">
                            </div>
                        </div>
                        <div class="normalTextStyle div_half_right">
                            <%-- <asp:Button ID="Button1" runat="server" CausesValidation="false" CssClass="btn_brown rounded_corners5"
                                OnClientClick="PopupShow(); return false;" Text="Reset Password" TabIndex="14" />--%>
                            <div class="profile_image_container_div">
                                <div class="fontSyleBold" id="divprofilepic" runat="server">
                                    Profile Picture</div>
                                <br />
                                <asp:Image ID="imgContactPicture" runat="server" CssClass="user_image_container" />
                                <br />
                                <br />
                                <asp:FileUpload ID="fuImageUpload" runat="server" CssClass="file_upload_box210 rounded_corners5"
                                    TabIndex="12" />
                                <br />
                                <br />
                                <asp:Button ID="btnUploadFile" runat="server" CssClass="start_creating_btn rounded_corners5"
                                    Text="Upload Image"  Style="display: none;" />
                            </div>
                        </div>
                        <div class="clearBoth">
                        </div>
                        <br />
                        <div class="user_profile_bottom_buttons">
                            <asp:Button ID="btnSave" runat="server" CssClass="start_creating_btn rounded_corners5"
                                Text="Save"  TabIndex="13" Width="100px" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnCancel" runat="server" CssClass="start_creating_btn rounded_corners5"
                                Text="Cancel" PostBackUrl="~/DashBoard.aspx" TabIndex="15" Width="100px" />
                        </div>
                    </div>
                </div>
            </div>
            <div style="height: 10px">
                <asp:HiddenField ID="hdContactID" runat="server" Value="0" />
                <asp:HiddenField ID="hdDeliveryAddressID" runat="server" Value="0" />
                <asp:HiddenField ID="hdBillingAddressID" runat="server" Value="0" />
            </div>
        </div>
        <br />
        <br />
        <br />
    </div>
   
    <asp:Label ID="lblMesgSavedSuccesfully" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblerrorMesge" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblPassChangedSuccesfully" runat="server" Visible="false"></asp:Label>
    <script type="text/javascript" language="javascript">

   
      

    </script>
    <script type="text/javascript" language="javascript">
        // Set foucs and Multip handler
        $(document).ready(function () {
            //$('#<%=txtFirstName.ClientID %>').focus();
            CreateMultipleUpload();
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

    </script>

