<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CRUDUsers.ascx.cs" Inherits="Web2Print.UI.Controls.CRUDUsers" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
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
<div class="divSearchBar_corp normalTextStyle rounded_corners territory_row_padding_header">
    <div class="user_name_corp_div">
        <asp:Literal ID="ltrlname" runat="server" Text="Name"></asp:Literal>
    </div>
    <div class="territory_corp_div" id="divteritory2" runat="server">
        <asp:Literal ID="ltrlterritory2" runat="server" Text="Territory"></asp:Literal>
    </div>
    <div class="department_corp_div">
        <asp:Literal ID="ltrldepartment2" runat="server" Text="Department"></asp:Literal>
    </div>
    <div class="can_order_corp_div">
        <asp:Literal ID="ltrlcanorder" runat="server" Text="Can Order"></asp:Literal>
    </div>
    <div class="can_approve_corp_div">
        <asp:Literal ID="ltrlcanapprove" runat="server" Text="Can Approve"></asp:Literal>
    </div>
    <div class="order_limit_corp_div">
        <asp:Literal ID="ltrlorderlimit" runat="server" Text="Order Limit"></asp:Literal>
    </div>
</div>
<asp:UpdatePanel ID="upnUser" runat="server" UpdateMode="Always">
    <ContentTemplate>
        <asp:Repeater ID="rptrUsers" runat="server" OnItemDataBound="rptrUsers_ItemDataBound"
            OnItemCommand="rptrUsers_ItemCommand">
            <ItemTemplate>
                <div class="territory_row_padding">
                    <div id="divContainer" runat="server" class="cursor_pointer">
                        <div class="user_name_corp_div">
                            <%# Eval("FirstName")%>&nbsp;</div>
                        <div class="territory_corp_div">
                            <asp:Label ID="lblTerritoryName" runat="server"></asp:Label>&nbsp;
                        </div>
                        <div class="department_corp_div">
                            <asp:Label ID="lblDepartmentName" runat="server"></asp:Label>&nbsp;
                        </div>
                        <div class="can_order_corp_div">
                            <asp:Label ID="lblCanOrder" runat="server"></asp:Label>&nbsp;</div>
                        <div class="can_approve_corp_div">
                            <asp:Label ID="lblCanApprove" runat="server"></asp:Label>&nbsp;</div>
                        <div class="order_limit_corp_div">
                            <asp:Label ID="lblOrderLimit" runat="server" Text='<%# Eval("CreditLimit") %>'></asp:Label>&nbsp;</div>
                    </div>
                    <div class="float_right">
                        <asp:Button ID="btnDelete" runat="server" CssClass="delete_icon_img" OnClientClick="return confirm('Are you sure you want to delete this user?');"
                            ToolTip="Delete User"  CommandName="DeleteRecord"
                            CommandArgument='<%# Eval("ContactID") %>' />
                    </div>
                </div>
                <div class="clearBoth">
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <asp:HiddenField ID="hfContactId" runat="server" />
        <asp:Button ID="btnEditRecord" runat="server" Text="Edit" Style="display: none;"
            OnClick="btnEditRecord_Click" />
    </ContentTemplate>
</asp:UpdatePanel>
<asp:Button ID="btnUserPopup" runat="server" Style="display: none; width: 0px; height: 0px" />
<ajaxToolkit:ModalPopupExtender ID="mpeUser" runat="server" BackgroundCssClass="ModalPopupBG"
    PopupControlID="pnlUser" TargetControlID="btnUserPopup" BehaviorID="mpeUser"
    CancelControlID="btnCancelMessageBox" DropShadow="false">
</ajaxToolkit:ModalPopupExtender>
<asp:Panel ID="pnlUser" runat="server" Width="950px" CssClass="FileUploaderPopup_corp LCLB rounded_corners"
    Style="display: none">
    <div style="height:640px;" class="white_background">
        <div class="Width100Percent">
            <div class="float_left">
                <asp:Label ID="lblHeader" runat="server" CssClass="FileUploadHeaderText"></asp:Label>
            </div>
            <div class="exit_container25">
                <div id="btnCancelMessageBox" runat="server" class="exit_popup">
                </div>
            </div>
        </div>
        <div class="pop_body_corp">
            <asp:UpdatePanel ID="upnlBody" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="left_user_popup_area">
                        <div class="address_label_area1 fontSyleBold">
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
                            &nbsp;</div>
                        <div class="address_label_area1">
                            <asp:Literal ID="ltrllname" runat="server" Text="Last Name:"></asp:Literal>
                        </div>
                        <div class="address_contol_area1">
                            <asp:TextBox ID="txtLastName" runat="server" CssClass="text_box200 rounded_corners5"
                                MaxLength="50" TabIndex="2"></asp:TextBox>
                        </div>
                        <div class="user_label_area2 fontSyleBold">
                            <asp:Literal ID="ltrlbusinessaddress" runat="server" Text="Business Address:"></asp:Literal>
                        </div>
                        <div class="user_contol_area2">
                            <asp:DropDownList ID="ddlBusinessAddress" runat="server" CssClass="rounded_corners5 dropdown"
                                Width="210px" AutoPostBack="true" OnSelectedIndexChanged="ddlBusinessAddress_SelectedIndexChanged"
                                TabIndex="15">
                            </asp:DropDownList>
                        </div>
                        <div class="clearBoth">
                            &nbsp;</div>
                        <div class="address_label_area1">
                            <asp:Literal ID="ltrlcellnumber" runat="server" Text="Cell Number:"></asp:Literal>
                        </div>
                        <div class="address_contol_area1">
                            <asp:TextBox ID="txtCellNumber" runat="server" CssClass="text_box200 rounded_corners5"
                                MaxLength="30" TabIndex="3"></asp:TextBox>
                        </div>
                        <div class="user_label_area2 ">
                            <asp:Literal ID="ltrladdress" runat="server" Text="Address"></asp:Literal>
                            :
                        </div>
                        <div class="user_contol_area2">
                            <asp:TextBox ID="txtAddress" runat="server" CssClass="text_box200 rounded_corners5"
                                MaxLength="250" Enabled="False" TabIndex="16"></asp:TextBox>
                        </div>
                        <div class="clearBoth">
                            &nbsp;</div>
                        <div class="address_label_area1">
                            <asp:Literal ID="ltrldirectline" runat="server" Text="Direct Line:"></asp:Literal>
                        </div>
                        <div class="address_contol_area1">
                            <asp:TextBox ID="txtDirectLine" runat="server" CssClass="text_box200 rounded_corners5"
                                MaxLength="30" TabIndex="4"></asp:TextBox>
                        </div>
                        <div class="user_label_area2">
                            <asp:Literal ID="ltrlcity" runat="server" Text="City:"></asp:Literal>
                        </div>
                        <div class="user_contol_area2">
                            <asp:TextBox ID="txtCity" runat="server" CssClass="text_box200 rounded_corners5"
                                MaxLength="30" Enabled="False" TabIndex="17"></asp:TextBox>
                        </div>
                        <div class="clearBoth">
                            &nbsp;</div>
                        <div class="address_label_area1">
                            <asp:Literal ID="ltrlfax" runat="server" Text="Fax:"></asp:Literal>
                        </div>
                        <div class="address_contol_area1">
                            <asp:TextBox ID="txtFax" runat="server" CssClass="text_box200 rounded_corners5" MaxLength="30"
                                TabIndex="5"></asp:TextBox>
                        </div>
                        <div class="user_label_area2">
                            <asp:Literal ID="ltrlstate" runat="server" Text="State:"></asp:Literal>
                        </div>
                        <div class="user_contol_area2">
                            <asp:TextBox ID="txtState" runat="server" Enabled="False" CssClass="text_box200 rounded_corners5 "
                                MaxLength="30" TabIndex="18"></asp:TextBox>
                        </div>
                        <div class="clearBoth">
                            &nbsp;</div>
                        <div class="address_label_area1">
                            <asp:Literal ID="ltrlemaill" runat="server" Text="Email:"></asp:Literal>
                        </div>
                        <div class="address_contol_area1">
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="text_box200 rounded_corners5"
                                MaxLength="30" TabIndex="6"></asp:TextBox>
                        </div>
                        <div class="user_label_area2">
                            <asp:Literal ID="ltrlcreditlimit" runat="server" Text="Credit Limit:"></asp:Literal>
                        </div>
                        <div class="user_contol_area2">
                            <asp:TextBox ID="txtCreditLimit" runat="server" CssClass="text_box200 rounded_corners5 float_input"
                                MaxLength="8" TabIndex="19"></asp:TextBox>
                        </div>
                        <div class="clearBoth">
                            &nbsp;</div>
                        <div class="address_label_area1">
                            <asp:Literal ID="ltrlteritory" runat="server" Text="Territory:"></asp:Literal>
                        </div>
                        <div class="address_contol_area1">
                            <asp:DropDownList ID="ddlTerritory" runat="server" CssClass="rounded_corners5 dropdown"
                                OnSelectedIndexChanged="ddlTerritory_SelectedIndexChanged" AutoPostBack="true"
                                Width="210px" TabIndex="9">
                            </asp:DropDownList>
                        </div>
                        <div class="user_label_area2 ">
                            <asp:Literal ID="ltrldepartment" runat="server" Text="Department:"></asp:Literal>
                        </div>
                        <div class="user_contol_area2">
                            <asp:DropDownList ID="ddlDepartments" runat="server" CssClass="rounded_corners5 dropdown"
                                Width="210px" TabIndex="22">
                            </asp:DropDownList>
                            <%--  <ajaxToolkit:CascadingDropDown ID="ccdDepartments" runat="server" TargetControlID="ddlDepartments"
                            ParentControlID="ddlTerritory" ServiceMethod="GetDepartmentsByTerritoryId" ServicePath="~/Services/WebStoreUtility.asmx"
                            Category="TerritoryId" />--%>
                        </div>
                        <div class="clearBoth">
                            &nbsp;</div>
                        <div class="address_label_area1">
                            <asp:Literal ID="ltrlsecrquestion" runat="server" Text="Secret Question:"></asp:Literal>
                        </div>
                        <div class="address_contol_area1">
                            <asp:DropDownList ID="ddlSecretQuestions" runat="server" CssClass="rounded_corners5 dropdown"
                                Width="210px" TabIndex="7">
                            </asp:DropDownList>
                        </div>
                        <div id="divPass" runat="server" class="user_label_area2 fontSyleBold pass">
                            <asp:Literal ID="ltrlPassword" runat="server" Text="Password:"></asp:Literal>
                        </div>
                        <div id="divPassContr" runat="server" class="user_contol_area2 pass">
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="text_box200 rounded_corners5"
                                MaxLength="200" TextMode="Password" TabIndex="20"></asp:TextBox>
                        </div>
                        <div class="clearBoth">
                            &nbsp;</div>
                        <div class="address_label_area1">
                            <asp:Literal ID="ltrlSecretAnswer" runat="server" Text="Secret Answer:"></asp:Literal>
                        </div>
                        <div class="address_contol_area1">
                            <asp:TextBox ID="txtSecretAnswer" runat="server" CssClass="text_box200 rounded_corners5"
                                MaxLength="200" TabIndex="8"></asp:TextBox>
                        </div>
                        <div id="divConfirmPass" runat="server" class="user_label_area2 fontSyleBold pass">
                            <asp:Literal ID="ltrlconfirmpass" runat="server" Text="Confirm Password:"></asp:Literal>
                        </div>
                        <div id="divConfirmPassContr" runat="server" class="user_contol_area2 pass">
                            <asp:TextBox ID="txtConfirmPassword" TextMode="Password" runat="server" CssClass="text_box200 rounded_corners5"
                                MaxLength="200" TabIndex="21"></asp:TextBox>
                        </div>
                        <div class="clearBoth">
                            &nbsp;</div>
                        <div class="address_label_area1">
                            <asp:Literal ID="ltrlrole" runat="server" Text="Role:"></asp:Literal>
                        </div>
                        <div class="address_contol_area1">
                            <asp:DropDownList ID="ddlRole" runat="server" CssClass="rounded_corners5 dropdown"
                                Width="210px" TabIndex="10">
                            </asp:DropDownList>
                        </div>
                        <div class="user_label_area2 ">
                            &nbsp;</div>
                        <div class="user_contol_area2">
                            <asp:Button ID="btnChangePassword" runat="server" CssClass="start_creating_btn rounded_corners5"
                                OnClientClick="return ShowChangePassword();" Text="Change Password" Visible="false"
                                TabIndex="25" />
                        </div>
                        <div class="clearBoth">
                            &nbsp;</div>
                        <div class="address_label_area1">
                            <asp:Literal ID="ltrlnotes" runat="server" Text="Notes:"></asp:Literal>
                        </div>
                        <div class="left_multiline_text_box">
                            <asp:TextBox ID="txtNotes" runat="server" CssClass="rounded_corners5 text_box" TextMode="MultiLine"
                                Width="550px" Height="40px" TabIndex="11"></asp:TextBox>
                        </div>
                        <div class="clearBoth">
                            &nbsp;</div>
                        <div class="address_label_area1">
                            &nbsp;
                        </div>
                        <div class="left_multiline_text_box">
                            <asp:CheckBox ID="chkCanApprove" runat="server" Text="&nbsp;&nbsp;&nbsp;Can Approve"
                                TabIndex="12" />
                        </div>
                        <div class="right_multiline_text_box">
                            <asp:CheckBox ID="chkCanByCredit" runat="server" Text="&nbsp;&nbsp;&nbsp;Can Pay By Credit Card"
                                TabIndex="13" />
                        </div>
                        <div class="clearBoth">
                            &nbsp;</div>
                        <div class="address_label_area1">
                            &nbsp;
                        </div>
                        <div class="left_multiline_text_box">
                            <asp:CheckBox ID="chkCanOrder" runat="server" Text="&nbsp;&nbsp;&nbsp;Can Place Order"
                                TabIndex="14" />
                        </div>
                        <div class="right_multiline_text_box">
                            <asp:CheckBox ID="chkShowPricesForProduct" runat="server" Text="&nbsp;&nbsp;&nbsp;Show Prices on Products"
                                TabIndex="15" />
                        </div>
                        <div class="clearBoth">
                            &nbsp;</div>
                        <%-- <div class="address_label_area1">
                        &nbsp;
                    </div>
                    
                    <div class="clearBoth">
                        &nbsp;</div>
                    <div class="address_label_area1">
                        &nbsp;
                    </div>
                    
                    <div class="clearBoth">
                        &nbsp;</div>
                    <div class="address_label_area1">
                        &nbsp;
                    </div>--%>
                        <div class="button_contol_area1_corp">
                            <div class="float_left">
                                <asp:Button ID="btnSaveUser" runat="server" Text="Save" OnClientClick="return ValidateUsers();"
                                    CssClass="start_creating_btn rounded_corners5" OnClick="btnSaveUser_Click" TabIndex="23" />
                            </div>
                            <div class="float_left">
                                <asp:Button ID="btnCancelUser" runat="server" Text="Cancel" OnClientClick="HideAddNewUsers(); return false;"
                                    CssClass="start_creating_btn rounded_corners5" TabIndex="24" />
                            </div>
                        </div>
                        <%--<div class="button_label_area2">
                        &nbsp;</div>
                    <div class="user_contol_area2">
                        <asp:Button ID="btnCancelUser" runat="server" Text="Cancel" OnClientClick="HideAddNewUsers(); return false;"
                            CssClass="start_creating_btn rounded_corners5" TabIndex="24" />
                    </div>--%>
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
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSaveUser" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Panel>
<asp:HiddenField ID="hfUserDeleted" runat="server" />
<asp:HiddenField ID="hfUserNotDeleted" runat="server" />
<asp:HiddenField ID="hfUserIsInuse" runat="server" />
<asp:HiddenField ID="hfUserNameExist" runat="server" />
<script type="text/javascript" language="javascript">

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
    function EditUser(contactId) {
        $('#<%= hfContactId.ClientID%>').val(contactId);
        __doPostBack('<%=btnEditRecord.UniqueID %>', '');
    }
    function AddNewPostBackForUser() {
        FileUploaderHide();
        __doPostBack('<%=imgAddNew.UniqueID %>', '');
    }
    //File Uploader Script
    // Set foucs and Multip handler
    $(document).ready(function () {
        $('#<%=txtFirstName.ClientID %>').focus();
        CreateMultipleUpload();
    });

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
