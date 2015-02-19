﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ThemeSite.Master" AutoEventWireup="true"
    CodeBehind="UserManager.aspx.cs" Inherits="Web2Print.UI.UserManager" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Controls/BreadCrumbMenu.ascx" TagName="BreadCrumbMenu" TagPrefix="uc1" %>
<%@ Register Src="~/Controls/CustomPager.ascx" TagName="CustomPagerControl" TagPrefix="uc8" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContents" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content_area container" style="margin-bottom: 40px">
        <div class="left_right_padding row">
              <div class="signin_heading_div float_left_simple dashboard_heading_signin">
                <asp:Label ID="HistryBtn" runat="server" Text="User Manager" CssClass="sign_in_heading"></asp:Label>
            </div>
               <div class="dashBoardRetrunLink">
                    <uc1:BreadCrumbMenu ID="BreadCumbMenuCategory" runat="server" WorkMode="MyAccount"
                MyAccountCurrentPage="Store Users" MyAccountCurrentPageUrl="UserManager.aspx" />
                </div>
            <div class="clearBoth">

            </div>
            <div class="cursor_pointer" onclick="AddNewPostBackForUser();">
                <div class="float_left">
                    <asp:ImageButton ID="imgAddNew" CssClass="add_new" ToolTip="Add New User" runat="server"
                        ImageUrl="~/images/AddNew.png" OnClick="imgAddNew_Click" />
                </div>
                <div class="new_caption">
                    <asp:Literal ID="ltrlnew" runat="server" Text="New"></asp:Literal>
                </div>
                <div class="clearBoth">
                </div>
            </div>
            <div class="divSearchBar_corp normalTextStyle usermanager_padding_header rounded_corners "
                style="padding: 15px 15px 15px 15px;">
                <table style="width: auto; border-collapse: separate; border-spacing: 2px;">
                    <tr>
                        <td>
                            <asp:Literal ID="ltrlSearchrecords" runat="server" Text=" Search Records"></asp:Literal>
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:TextBox ID="txtsearch" runat="server" Width="200px" CssClass="text_box150 rounded_corners5"></asp:TextBox>
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:Button ID="btnsearch" runat="server" Text="Go" CssClass="start_creating_btn rounded_corners5"
                                OnClick="btnsearch_Click" />
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="start_creating_btn rounded_corners5"
                                OnClick="btnReset_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="PadingTop white_background  rounded_corners">
                <div class="PadingTop textAlignLeft">
                    <asp:Label ID="lblmatchingrecord" runat="server" CssClass="matchingTxtclass" />
                </div>
                <div class="clearBoth">
                    &nbsp;
                </div>
                <div class="normalTextStyle rounded_corners territory_row_padding_header">
                    <div class="user_name_corp_div checked_design">
                        <asp:Literal ID="ltrlname" runat="server" Text="Name"></asp:Literal>
                    </div>
                    <div class="user_email_div checked_design">
                        <asp:Literal ID="ltremail" runat="server" Text="Email"></asp:Literal>
                    </div>
                    <div class="territory_corp_div checked_design" id="divteritory2" runat="server">
                        <asp:Literal ID="ltrlterritory2" runat="server" Text="Territory"></asp:Literal>
                    </div>
                    <div class="order_limit_corp_div checked_design">
                        <asp:Literal ID="ltrlorderlimit" runat="server" Text="Order Limit"></asp:Literal>
                    </div>
                    <div class="can_order_corp_div checked_design">
                        <asp:Literal ID="ltrlcanorder" runat="server" Text="Can Order"></asp:Literal>
                    </div>
                    <div class="user_phone_div checked_design">
                        <asp:Literal ID="ltrphone" runat="server" Text="Can pay by credit card"></asp:Literal>
                    </div>
                    <div class="user_phone_div checked_design">
                        <asp:Literal ID="ltrlUserRoles" runat="server" Text="Role"></asp:Literal>
                    </div>
                </div>
                <asp:UpdatePanel ID="upnUser" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <asp:Repeater ID="rptrUsers" runat="server" OnItemDataBound="rptrUsers_ItemDataBound"
                            OnItemCommand="rptrUsers_ItemCommand">
                            <ItemTemplate>
                                <div class="territory_row_padding" title="Click to edit record">
                                    <div id="divContainer" runat="server" class=" dottedBorderStyle" style="border-top: 1px dotted #CCCCCC;">
                                        <div class="user_name_corp_div checked_design">
                                            <%# Eval("FirstName")%>&nbsp;
                                        </div>
                                        <div class="user_email_div checked_design">
                                            <asp:Label ID="lblemail" runat="server" Text='<%# Eval("Email")%>' Width="160px"></asp:Label>&nbsp;
                                        </div>
                                        <div class="territory_corp_div checked_design">
                                            <asp:Label ID="lblTerritoryName" runat="server"></asp:Label>&nbsp;
                                        </div>
                                        <div class="order_limit_corp_div checked_design">
                                            <asp:Label ID="lblOrderLimit" runat="server" Text='<%# Eval("CreditLimit") %>'></asp:Label>&nbsp;
                                        </div>
                                        <div class="can_order_corp_div checked_design">
                                            <asp:Label ID="lblCanOrder" runat="server"></asp:Label>&nbsp;
                                        </div>
                                        <div class="user_phone_div_card checked_design">
                                            <asp:Label ID="lblPayByCredirCard" runat="server"></asp:Label>&nbsp;
                                        </div>
                                        <div class="user_phone_div_card checked_design" style="width:100px;">
                                            <asp:Label ID="lblUserRoles" runat="server"></asp:Label>&nbsp;
                                        </div>
                                    </div>
                                    <div class="float_right" style="padding: 7px 0px 0px 0px;">
                                        <asp:ImageButton ID="imgBtnEdit" runat="server" CssClass="rounded_corners" ImageUrl="~/images/edit.png" Height="24" Width="24"
                                            ToolTip="Click to edit record" CommandName="EditRecord" CommandArgument='<%# Eval("ContactID") %>' />
                                        <asp:ImageButton ID="btnDelete" runat="server" OnClientClick="return confirm('Are you sure you want to remove this user? The order history of this user will also be removed..');" CssClass="rounded_corners" ImageUrl="~/images/delete.png" Height="28" Width="28"
                                            ToolTip="Delete User" CommandName="DeleteRecord" CommandArgument='<%# Eval("ContactID") %>' />
                                    </div>
                                </div>
                                <div class="clearBoth">
                                    &nbsp;
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <div class="clearBoth">
                            &nbsp;
                        </div>
                        </div>
                        <asp:HiddenField ID="hfContactId" runat="server" Value="0" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div id="PnlPagerControl" class="Width100Percent" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" class="Width100Percent textAlignRight">
                    <tr>
                        <td>
                            <div id="PagerRighArea">
                                <div class="divTblLayout Width100Percent">
                                    <div class="tblRow">
                                        <div class="divCell">
                                            <asp:LinkButton ID="lnkBtnPreviouBullet" runat="server" CssClass="imgPreviousBullet"
                                                OnClick="lnkBtnPreviouBullet_Click" Visible="false" />
                                        </div>
                                        <div class="divCell">
                                            <div id="PagerPageDisplayInfo">
                                                <asp:Label ID="lblPageInfoDisplay" CssClass="simpleText" runat="server" Text="1 of 1"
                                                    Visible="false" />
                                            </div>
                                        </div>
                                        <div class="divCell">
                                            <asp:LinkButton ID="lnkBtnNextBullet" runat="server" CssClass="imgNextBullet" OnClick="lnkBtnNextBullet_Click"
                                                Visible="false" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <asp:Button ID="btnUserPopup" runat="server" Style="display: none; width: 0px; height: 0px" />
            <ajaxToolkit:ModalPopupExtender ID="mpeUser" runat="server" BackgroundCssClass="ModalPopupBG"
                PopupControlID="pnlUser" TargetControlID="btnUserPopup" BehaviorID="mpeUser"
                CancelControlID="btnCancelUser" DropShadow="false">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="pnlUser" runat="server" Width="950px" CssClass="FileUploaderPopup_corp LCLB rounded_corners"
                Style="display: none">
                <div class="white_background">
                     <asp:UpdatePanel ID="upnlBody" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                    <div class="SolidBorderCS left_align pad20 mrginBtm">
                        <asp:Label ID="lblHeader" runat="server" CssClass="FileUploadHeaderText"></asp:Label>
                    </div>
                    <div class="pop_body_corp">
                       
                                <div class="left_user_popup_area">
                                    <div class="address_label_area1">
                                        <asp:Literal ID="ltrlfname" runat="server" Text="First Name:"></asp:Literal>
                                    </div>
                                    <div class="address_contol_area1">
                                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="text_box200 rounded_corners5"
                                            MaxLength="50" TabIndex="1"></asp:TextBox>
                                    </div>
                                    <div class="user_label_area2">
                                        <asp:Literal ID="ltrljobtitle" runat="server" Text="Job Title:"></asp:Literal>
                                    </div>
                                    <div class="user_contol_area2">
                                        <asp:TextBox ID="txtJobTitle" runat="server" CssClass="text_box200 rounded_corners5"
                                            MaxLength="50" TabIndex="14"></asp:TextBox>
                                    </div>
                                    <div class="clearBoth">
                                        &nbsp;
                                    </div>
                                    <div class="address_label_area1">
                                        <asp:Literal ID="ltrllname" runat="server" Text="Last Name:"></asp:Literal>
                                    </div>
                                    <div class="address_contol_area1">
                                        <asp:TextBox ID="txtLastName" runat="server" CssClass="text_box200 rounded_corners5"
                                            MaxLength="50" TabIndex="2"></asp:TextBox>
                                    </div>
                                    <div class="user_label_area2">
                                        <asp:Literal ID="ltrldirectline" runat="server" Text="Direct Line"></asp:Literal>

                                    </div>
                                    <div class="user_contol_area2">
                                        <asp:TextBox ID="txtDirectLine" runat="server" CssClass="text_box200 rounded_corners5"
                                            MaxLength="30" TabIndex="4"></asp:TextBox>

                                    </div>
                                    <div class="clearBoth">
                                        &nbsp;
                                    </div>
                                    <div class="address_label_area1">
                                        <asp:Literal ID="ltrlcellnumber" runat="server" Text="Cell Number:"></asp:Literal>
                                    </div>
                                    <div class="address_contol_area1">
                                        <asp:TextBox ID="txtCellNumber" runat="server" CssClass="text_box200 rounded_corners5"
                                            MaxLength="30" TabIndex="3"></asp:TextBox>
                                    </div>
                                    <div class="user_label_area2 ">
                                        <asp:Literal ID="ltrlfax" runat="server" Text="Fax:"></asp:Literal>

                                    </div>
                                    <div class="user_contol_area2">
                                        <asp:TextBox ID="txtFax" runat="server" CssClass="text_box200 rounded_corners5" MaxLength="30"
                                            TabIndex="5"></asp:TextBox>

                                    </div>
                                    <div class="clearBoth">
                                        &nbsp;
                                    </div>
                                    <div class="address_label_area1">
                                        <asp:Literal ID="ltrlemaill" runat="server" Text="Email:"></asp:Literal>

                                    </div>
                                    <div class="address_contol_area1">
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="text_box200 rounded_corners5"
                                            MaxLength="30" TabIndex="6"></asp:TextBox>

                                    </div>

                                    <div class="user_label_area2">
                                        <asp:Literal ID="ltrlcreditlimit" runat="server" Text="Credit Limit"></asp:Literal>

                                    </div>
                                    <div class="user_contol_area2">
                                        <asp:TextBox ID="txtCreditLimit" runat="server" CssClass="text_box200 rounded_corners5 float_input"
                                            MaxLength="8" TabIndex="19"></asp:TextBox>

                                    </div>

                                    <div class="address_label_area1">
                                        <asp:Literal ID="ltrlsecrquestion" runat="server" Text="Secret Question:"></asp:Literal>

                                    </div>
                                    <div class="address_contol_area1">
                                        <asp:DropDownList ID="ddlSecretQuestions" runat="server" CssClass="rounded_corners5 dropdown"
                                            Width="210px" TabIndex="7">
                                        </asp:DropDownList>

                                    </div>

                                    <div class="user_label_area2">
                                        <asp:Literal ID="ltrlSecretAnswer" runat="server" Text="Secret Answer:"></asp:Literal>

                                    </div>
                                    <div class="user_contol_area2">
                                        <asp:TextBox ID="txtSecretAnswer" runat="server" CssClass="text_box200 rounded_corners5"
                                            MaxLength="200" TabIndex="8"></asp:TextBox>

                                    </div>
                                    <div class="clearBoth">
                                        &nbsp;
                                    </div>




                                    <div class="address_label_area1 height30px">
                                        <asp:Literal ID="ltrlteritory" runat="server" Text="Territory:"></asp:Literal>
                                    </div>
                                    <div class="address_contol_area1 height30px">
                                        <asp:DropDownList ID="ddlTerritory" runat="server" CssClass="rounded_corners5 dropdown"
                                            OnSelectedIndexChanged="ddlTerritory_SelectedIndexChanged" AutoPostBack="true"
                                            Width="210px" TabIndex="9">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="user_label_area2">
                                        <asp:Literal ID="ltrlrole" runat="server" Text="Role:"></asp:Literal>
                                    </div>
                                    <div class="user_contol_area2">
                                        <asp:DropDownList ID="ddlRole" runat="server" CssClass="rounded_corners5 dropdown"
                                            Width="210px" TabIndex="10">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="clearBoth">
                                        &nbsp;
                                    </div>
                                    <div class="address_label_area1">
                                        <asp:Literal ID="ltrlbusinaddress" runat="server" Text="Billing Address"></asp:Literal>
                                    </div>
                                    <div class="address_contol_area1 height30px">
                                        <asp:DropDownList ID="ddlBusinessAddress" runat="server" CssClass="rounded_corners5 dropdown"
                                            Width="210px" AutoPostBack="true" OnSelectedIndexChanged="ddlBusinessAddress_SelectedIndexChanged"
                                            TabIndex="15">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="user_label_area2">
                                        <asp:Literal ID="Literal1" runat="server" Text="Shipping Address"></asp:Literal>
                                    </div>
                                    <div class="user_contol_area2">
                                        <asp:DropDownList ID="ddlShippingAddress" runat="server" CssClass="rounded_corners5 dropdown"
                                            Width="210px" AutoPostBack="true" OnSelectedIndexChanged="ddlShippingAddress_SelectedIndexChanged"
                                            TabIndex="15">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="address_label_area1">
                                        <asp:Literal ID="ltrlBilladdress" runat="server" Text="Address Detail"></asp:Literal>


                                    </div>
                                    <div class="address_contol_area1">
                                        <asp:TextBox ID="txtAddress" runat="server" CssClass="text_box200 rounded_corners5" TextMode="MultiLine"
                                            MaxLength="250" Enabled="False" TabIndex="16" Height="60px"></asp:TextBox>

                                    </div>
                                    <div class="user_label_area2">
                                        <asp:Literal ID="ltrlShipAddrs" runat="server" Text="Address Detail"></asp:Literal>
                                    </div>
                                    <div class="user_contol_area2">
                                        <asp:TextBox ID="txtShippingAddress" runat="server" CssClass="text_box200 rounded_corners5"
                                            MaxLength="30" Enabled="False" TabIndex="17"  Height="60px" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    <div class="clearBoth">
                                        &nbsp;
                                    </div>
                                    <div class="address_label_area1">
                                        <asp:Literal ID="ltrlnotes" runat="server" Text="Notes:"></asp:Literal>
                                    </div>
                                    <div class="left_multiline_text_box" style="padding-left: 0px;">
                                        <asp:TextBox ID="txtNotes" runat="server" CssClass="rounded_corners5 text_box" TextMode="MultiLine"
                                            Width="550px" Height="40px" TabIndex="11"></asp:TextBox>
                                    </div>
                                    <div class="clearBoth">
                                        &nbsp;
                                    </div>
                                    <div class="address_label_area1">
                                        &nbsp;
                                    </div>
                                    <div class="left_multiline_text_box">
                                        <asp:CheckBox ID="chkCanOrder" runat="server" Text="&nbsp;&nbsp;&nbsp;Can Place Order"
                                            TabIndex="12" />

                                    </div>
                                    <div class="right_multiline_text_box">
                                        <asp:CheckBox ID="chkCanByCredit" runat="server" Text="&nbsp;&nbsp;&nbsp;Can Pay By Credit Card"
                                            TabIndex="13" />
                                    </div>
                                    <div class="clearBoth">
                                        &nbsp;
                                    </div>
                                    <div class="address_label_area1">
                                        &nbsp;
                                    </div>
                                    <div class="left_multiline_text_box">
                                        <asp:CheckBox ID="chkShowPricesForProduct" runat="server" Text="&nbsp;&nbsp;&nbsp;Show Prices on Products"
                                            TabIndex="14" />
                                    </div>
                                    <div class="right_multiline_text_box">
                                        <asp:CheckBox ID="chkHasWebAccess" runat="server" Text="&nbsp;&nbsp;&nbsp;Has Web Access"
                                            TabIndex="12" />
                                    </div>
                                    <div class="clearBoth">
                                        &nbsp;
                                    </div>
                                    <div class="button_contol_area1_corp">
                                        <div class="float_left" style="padding-left: 0px;">
                                            <asp:Button ID="btnSaveUser" runat="server" Text="Save" OnClientClick="return ValidateUsers();"
                                                CssClass="start_creating_btn rounded_corners5" OnClick="btnSaveUser_Click" TabIndex="23" />
                                        </div>
                                        <div class="float_left">
                                            <asp:Button ID="btnCancelUser" runat="server" Text="Cancel" OnClientClick="HideAddNewUsers(); return false;"
                                                CssClass="start_creating_btn rounded_corners5" TabIndex="24" />
                                        </div>
                                    </div>
                                    <div class="clearBoth">
                                        &nbsp;
                                    </div>
                                </div>
                                <div class="right_user_popup_area">
                                    <div class="user_contact_popup_image_container">
                                        <asp:Image ID="imgContactPicture" runat="server" CssClass="user_image_container" />
                                        <br />
                                        <br />
                                        <asp:FileUpload ID="fuImageUpload" runat="server" Width="200px" CssClass="file_upload_box rounded_corners5"
                                            TabIndex="23" />
                                        <br />
                                        <br />
                                    </div>
                                    <asp:Button ID="btnChangePassword" runat="server" CssClass="start_creating_btn rounded_corners5"
                                        OnClientClick="return ShowChangePassword();" Text="Change Password" Visible="false"
                                        TabIndex="25" Style="width: 145px; float: right; margin-right: 25px;" />
                                    <div id="divPass" runat="server" class="pass" style="text-align: left; margin-left: 5px;">
                                        <asp:Literal ID="ltrlPassword" runat="server" Text="Password:"></asp:Literal>
                                    </div>
                                    <div id="divPassContr" runat="server" class="user_contol_area2 pass">
                                        <asp:TextBox ID="txtPassword" runat="server" CssClass="text_box200 rounded_corners5"
                                            MaxLength="200" TextMode="Password" TabIndex="20"></asp:TextBox>
                                    </div>
                                    <div class="clearBoth">
                                        &nbsp;
                                    </div>
                                    <div id="divConfirmPass" runat="server" class="pass" style="text-align: left; margin-left: 5px;">
                                        <asp:Literal ID="ltrlconfirmpass" runat="server" Text="Confirm Password:"></asp:Literal>
                                    </div>
                                    <div id="divConfirmPassContr" runat="server" class="user_contol_area2 pass">
                                        <asp:TextBox ID="txtConfirmPassword" TextMode="Password" runat="server" CssClass="text_box200 rounded_corners5"
                                            MaxLength="200" TabIndex="21"></asp:TextBox>
                                    </div>
                                    <div class="clearBoth">
                                        &nbsp;
                                    </div>

                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnSaveUser" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <div class="clearBoth">
                        &nbsp;
                    </div>
                </div>
            </asp:Panel>
            <asp:HiddenField ID="hfUserDeleted" runat="server" />
            <asp:HiddenField ID="hfUserNotDeleted" runat="server" />
            <asp:HiddenField ID="hfUserIsInuse" runat="server" />
            <asp:HiddenField ID="hfUserNameExist" runat="server" />
        </div>
    </div>
    </div>
    <script type="text/javascript">
        //File Uploader Script
        // Set foucs and Multip handler
        $(document).ready(function () {
            $('#<%=txtFirstName.ClientID %>').focus();
            CreateMultipleUpload();
        });

        function ShowChangePassword() {
            $('.pass').css('display', 'block');
            $('#<%=btnChangePassword.ClientID %>').css('display', 'none');
            return false;
        }
        // Users
        function ShowAddNewUsers() {
            ShowChangePassword();
            $find('mpeUser').show();
        }
        function HideAddNewUsers() {
            $find('mpeUser').hide();
        }
        function ValidateUsers() {
            if ($('#<%=txtFirstName.ClientID %>').val().trim() == '') {
                var PlxenterFname = "<%= Resources.MyResource.PlxenterFname %>";
                ShowPopup('Message', PlxenterFname);
                return false;
            }
            else if ($('#<%=txtSecretAnswer.ClientID %>').val().trim() == '') {
                var PlxenterSecretAns = "<%= Resources.MyResource.PlxenterSecretAns %>";
                ShowPopup('Message', PlxenterSecretAns);
                return false;
            }
        if ($('#<%=txtEmail.ClientID %>').val().trim() != '') {

                var str = $('#<%=txtEmail.ClientID %>').val().trim();
                var re = new RegExp("^\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$", "");
                var isValid = re.test(str);
                if (!isValid) {
                    var PlxenterVEmail = "<%= Resources.MyResource.PlxenterVEmail %>";
                    ShowPopup('Message', PlxenterVEmail);
                    return false;
                }
            }
            if ($('#<%=btnChangePassword.ClientID %>').css('display') == 'none') {
                if ($('#<%=txtPassword.ClientID %>').val().trim() == '') {
                    var PlxenterPass = "<%= Resources.MyResource.PlxenterPass %>";
                    ShowPopup('Message', PlxenterPass);
                    return false;
                }
                else if ($('#<%=txtConfirmPassword.ClientID %>').val().trim() == '') {
                    var PlxenterCPass = "<%= Resources.MyResource.PlxenterCPass %>";
                    ShowPopup('Message', PlxenterCPass);
                    return false;
                }
                else if ($('#<%=txtPassword.ClientID %>').val().trim() != $('#<%=txtConfirmPassword.ClientID %>').val().trim()) {
                    var CPassNotM = "<%= Resources.MyResource.CPassNotM %>";
                    ShowPopup('Message', 'Confirm Password not matched');
                    return false;
                }
    }

    return true;

}

        function AddNewPostBackForUser() {
            FileUploaderHide();
            __doPostBack('<%=imgAddNew.UniqueID %>', '');
        }

        function CreateMultipleUpload() {
            //set up the file upload
            $("#" + "<%=fuImageUpload.ClientID %>").MultiFile(
              {
                  max: 1,
                  accept: 'jpg,bmp,png',
                  afterFileSelect: function (element, value, master_element) {

                  },
                  afterFileRemove: function (element, value, master_element) {

                  }
              });
        }


        function FileUploaderHide() {
            $('input:file').MultiFile('reset');
        }

    </script>
</asp:Content>
