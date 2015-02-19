<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ThemeSite.Master" AutoEventWireup="true"
    CodeBehind="RequestQuote.aspx.cs" Inherits="Web2Print.UI.RequestQuote" Culture="en-US" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content_area container">
        <div class="left_right_padding row">
            <div class="signin_heading_div">
                <asp:Label ID="lblTitle" runat="server" CssClass="sign_in_heading">Request A Quote</asp:Label>
            </div>


            <div class="get_in_touch_box_RFQ rounded_corners" id="divUserInfo" runat="server" visible="false"> 
                <div class="smallContctUsAvenior float_left_simple">
                    <asp:Literal ID="ltrlYourName" runat="server" Text=" First Name :"></asp:Literal>
                    <div id="divYourNameRq" class="error_star" style="display: none;">
                        *
                    </div>
                </div>
                <div class="TTL widthAvenior">
                    <asp:TextBox ID="txtBoxUserName" runat="server" CssClass="newTxtBox"
                        MaxLength="100"></asp:TextBox>
                </div>
                <div class="clearBoth">
                </div>
                <div class="smallContctUsAvenior float_left_simple">
                    <asp:Literal ID="ltrlLastName" runat="server" Text=" Last Name :"></asp:Literal>
                    <div id="ltrlLastNameRQ" class="error_star" style="display: none;">
                        *
                    </div>
                </div>
                <div class="TTL widthAvenior">
                    <asp:TextBox ID="txtBoxLastName" runat="server" CssClass="newTxtBox"
                        MaxLength="100"></asp:TextBox>
                </div>
                <div class="clearBoth">
                </div>
                <div class="smallContctUsAvenior float_left_simple">
                    <asp:Literal ID="ltrlYourEmail" runat="server" Text=" Your Email :"></asp:Literal>
                    <div id="divEmailRq" class="error_star" style="display: none;">
                        *
                    </div>
                </div>
                <div class="TTL widthAvenior">
                    <asp:TextBox ID="txtBoxUserEmail" runat="server" CssClass="newTxtBox"
                        MaxLength="100"></asp:TextBox>
                </div>
                <div class="clearBoth">
                </div>
                <div class="smallContctUsAvenior float_left_simple">
                    <asp:Literal ID="Literal1" runat="server" Text=" Phone :"></asp:Literal>
                    <div id="div1" class="error_star" style="display: none;">
                        *
                    </div>
                </div>
                <div class="TTL widthAvenior">
                    <asp:TextBox ID="txtPhone" runat="server" CssClass="newTxtBox"
                        MaxLength="100"></asp:TextBox>
                </div>
                <div class="clearBoth">
                </div>
                <br />
                <br />
                <div class="clearBoth">
                </div>
            </div>


            <div class="get_in_touch_box_RFQ rounded_corners">
                <div class="smallContctUsAvenior float_left_simple">
                    <asp:Literal ID="ltrlinquirytitle" runat="server" Text=" Inquiry Title :"></asp:Literal>
                    <div id="InquiryTitleInd2" class="error_star" style="display: none;">
                        *
                    </div>
                </div>
                <div class="TTL widthAvenior">
                    <asp:TextBox ID="txtInquiryTitle" runat="server" CssClass="newTxtBox"
                        MaxLength="100"></asp:TextBox>
                </div>
                <div class="clearBoth">
                </div>
                <div class="smallContctUsAvenior float_left_simple">
                    <asp:Literal ID="ltrlinquiryAttachement" runat="server" Text="Inquiry Attachment's"></asp:Literal>
                </div>
                <div class="TTL widthAvenior">
                    <asp:FileUpload ID="fuImageUpload" runat="server" CssClass="newTxtBox" />
                </div>
                <br />
                <br />
                <div class="clearBoth">
                </div>
            </div>
            <div class="height15">
                &nbsp;
            </div>
            <div id="1" style="display: none;">
                <div class="get_in_touch_box_RFQ rounded_corners">
                    <div>
                        <div class="smallContctUsAvenior float_left_simple">
                            <asp:Literal ID="ltrlitemtitle" runat="server" Text="Item Title :"></asp:Literal>
                            <div id="ItemTitleInd1" class="error_star" style="display: none;">
                                *
                            </div>
                        </div>
                        <div class="TTL widthAvenior">
                            <asp:TextBox ID="txtTitle1" runat="server" CssClass="newTxtBox"
                                MaxLength="100"></asp:TextBox>
                        </div>
                        <div class="clear smallContctUsAvenior float_left_simple">
                            <asp:Literal ID="ltrlitemnotes" runat="server" Text="Item Notes :"></asp:Literal>
                        </div>
                        <div class="TTL widthAvenior">
                            <asp:TextBox ID="txtItemNote1" runat="server" TextMode="MultiLine" Height="100px"
                                CssClass="newTxtBox"></asp:TextBox>
                        </div>
                        <div class="clear smallContctUsAvenior float_left_simple">
                            <asp:Literal ID="ltrldeliverydate" runat="server" Text=" Desired Delivery :"></asp:Literal>
                        </div>
                        <div class="TTL widthAvenior">
                            <asp:TextBox ID="txtInquiryDate1" runat="server" CssClass="newTxtBox"
                                ReadOnly="false"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="ceDateInquiry1" runat="server" TargetControlID="txtInquiryDate1">
                            </ajaxToolkit:CalendarExtender>
                        </div>
                        <div class="clearBoth">
                        </div>
                    </div>
                </div>
                <div class="height15">
                    &nbsp;
                </div>
            </div>
            <div id="2" style="display: none;">
                <div class="get_in_touch_box_RFQ rounded_corners">
                    <div>
                        <div class="smallContctUsAvenior float_left_simple">
                            <asp:Literal ID="ltrlitemtitle2" runat="server" Text=" Item Title :"></asp:Literal>
                            <div id="ItemTitleInd2" class="error_star" style="display: none;">
                                *
                            </div>
                        </div>
                        <div class="TTL widthAvenior">
                            <asp:TextBox ID="txtTitle2" runat="server" CssClass="newTxtBox"
                                MaxLength="100"></asp:TextBox>
                        </div>
                        <div class="exit_container_RQ">
                            <div class="exit_popup" onclick="Remove(2);" title="Click to remove item.">
                            </div>
                        </div>
                        <div class="clear smallContctUsAvenior float_left_simple">
                            <asp:Literal ID="ltrlitemnotes2" runat="server" Text="Item Notes :"></asp:Literal>
                        </div>
                        <div class="TTL widthAvenior">
                            <asp:TextBox ID="txtItemNote2" runat="server" TextMode="MultiLine" Height="100px"
                                CssClass="newTxtBox"></asp:TextBox>
                        </div>
                        <div class="clear smallContctUsAvenior float_left_simple">
                            <asp:Literal ID="ltrldeliverydate2" runat="server" Text="Desired Delivery :"></asp:Literal>
                        </div>
                        <div class="TTL widthAvenior">
                            <asp:TextBox ID="txtInquiryDate2" runat="server" CssClass="newTxtBox"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="ceDateInquiry2" runat="server" TargetControlID="txtInquiryDate2">
                            </ajaxToolkit:CalendarExtender>
                        </div>
                        <div class="clearBoth">
                        </div>
                    </div>
                </div>
                <div class="height15">
                    &nbsp;
                </div>
            </div>
            <div id="3" style="display: none;">
                <div class="get_in_touch_box_RFQ rounded_corners">
                    <div>
                        <div class="smallContctUsAvenior float_left_simple">
                            <asp:Literal ID="ltrlitemtitle3" runat="server" Text=" Item Title :"></asp:Literal>
                            <div id="ItemTitleInd3" class="error_star" style="display: none;">
                                *
                            </div>
                        </div>
                        <div class="TTL widthAvenior">
                            <asp:TextBox ID="txtTitle3" runat="server" CssClass="newTxtBox"
                                MaxLength="100"></asp:TextBox>
                        </div>
                        <div class="exit_container">
                            <div class="exit_popup" onclick="Remove(3);" title="Click to remove item.">
                            </div>
                        </div>
                        <div class="clear smallContctUsAvenior float_left_simple">
                            <asp:Literal ID="ltrlitemnotes3" runat="server" Text=" Item Notes :"></asp:Literal>
                        </div>
                        <div class="TTL widthAvenior">
                            <asp:TextBox ID="txtItemNote3" runat="server" TextMode="MultiLine" Height="100px"
                                CssClass="newTxtBox"></asp:TextBox>
                        </div>
                        <div class="clear smallContctUsAvenior float_left_simple">
                            <asp:Literal ID="ltrldeliverydate3" runat="server" Text=" Desired Delivery :"></asp:Literal>
                        </div>
                        <div class="TTL widthAvenior">
                            <asp:TextBox ID="txtInquiryDate3" runat="server" CssClass="newTxtBox"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="ceDateInquiry3" runat="server" TargetControlID="txtInquiryDate3">
                            </ajaxToolkit:CalendarExtender>
                        </div>
                        <div class="clearBoth">
                        </div>
                    </div>
                </div>
                <div class="height15">
                    &nbsp;
                </div>
            </div>

            <div class="quote_control_sec_btnsDiv float_left">

                <div class="quote_send_button">
                    <asp:Button ID="btnSend" runat="server" CssClass="start_creating_btn rounded_corners5"
                        OnClientClick="return Validate();" Text="Send" OnClick="btnSend_Click" />
                </div>
                <div class="quote_cancel_button">
                    <asp:Button ID="btnCancel" runat="server" CssClass="start_creating_btn rounded_corners5"
                        Text="Cancel" PostBackUrl="~/DashBoard.aspx" />
                </div>
                <div class="clearBoth">
                </div>
            </div>
            <div class="quote_lable_sec_btnDiv float_right">
                <div id="divAddNew" class="cursor_pointer" onclick="AddNew();">
                    <div class="float_left">
                        <img alt="" class="add_new" src="images/AddNew.png" title="Add New" />
                    </div>
                    <div class="new_caption">
                        <asp:Literal ID="ltrladdNewitem" runat="server" Text="Add New Item"></asp:Literal>
                    </div>
                    <div class="clearBoth">
                    </div>
                </div>
            </div>
            <div class="clearBoth">
            </div>
            <br />
            <br />
            <br />
        </div>
        <asp:HiddenField ID="hfNoOfRec" runat="server" Value="1" />
         <asp:HiddenField ID="hfIsUserLoggedIn" runat="server" Value="false" />
    </div>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            ShowGUI();
        });

        function Validate() {
            var isValid = true;
            var currentRec = parseInt($('#<%=hfNoOfRec.ClientID %>').val());
            if (currentRec >= 1) {
                if (!ValidateRec1())
                    isValid = false;
            }
            if (currentRec >= 2) {
                if (!ValidateRec2())
                    isValid = false;
            }
            if (currentRec >= 3) {
                if (!ValidateRec3())
                    isValid = false;
            }
            return isValid;
        }
        function ShowGUI() {
            var numofRec = $('#<%=hfNoOfRec.ClientID%>').val();
            if (numofRec >= 1) {
                $('#1').css('display', 'block');
            }
            if (numofRec >= 2) {
                $('#2').css('display', 'block');
            }
            if (numofRec >= 3) {
                $('#3').css('display', 'block');
                $('#divAddNew').css('display', 'none');
            }
        }

        function AddNew() {
            var isValid = true;
            isValid = Validate();
            if (isValid) {
                var currentRec = parseInt($('#<%=hfNoOfRec.ClientID %>').val());
                var numOfRec = currentRec + 1;
                $('#<%=hfNoOfRec.ClientID%>').val(numOfRec); ShowGUI();
            }
        }
        function Remove(recNo) {
            //Remove the view and Show the add new button
            var Areusureuwnt2remove = "<%= Resources.MyResource.Areusureuwnt2remove %>";
            if (confirm(Areusureuwnt2remove)) {
                $('#' + recNo).css('display', 'none');
                $('#divAddNew').css('display', 'block');
                $('#<%=hfNoOfRec.ClientID %>').val((recNo - 1));
            }
        }
        function ValidateRec1() {
            var errorMessage = '';
            var isValid = true;
            if ($('#<%=hfIsUserLoggedIn.ClientID %>').val() == "false") {
                if ($('#<%=txtBoxUserName.ClientID %>').val().trim() == '') {
                    var inquirytitlexreq = "<%= Resources.MyResource.yourNameRequired %>";
                    errorMessage = errorMessage + inquirytitlexreq + "<br>";
                    $('#divYourNameRq').css('display', 'block');
                    isValid = false;
                }
                else {
                    $('#divYourNameRq').css('display', 'none');
                }
                if ($('#<%=txtBoxLastName.ClientID %>').val().trim() == '') {
                    var inquirytitlexreq = "<%= Resources.MyResource.yourNameRequiredLast %>";
                    errorMessage = errorMessage + inquirytitlexreq + "<br>";
                    $('#ltrlLastNameRQ').css('display', 'block');
                    isValid = false;
                }
                else {
                    $('#ltrlLastNameRQ').css('display', 'none');
                }
                //yourNameRequiredLast
                if ($('#<%=txtBoxUserEmail.ClientID %>').val().trim() == '') {
                    var inquirytitlexreq = "<%= Resources.MyResource.yourEmailRequired %>";
                    errorMessage = errorMessage + inquirytitlexreq + "<br>";
                    $('#divEmailRq').css('display', 'block');
                    isValid = false;
                }
                else {
                    $('#divEmailRq').css('display', 'none');
                }
            }
            if ($('#<%=txtInquiryTitle.ClientID %>').val().trim() == '') {
                var inquirytitlexreq = "<%= Resources.MyResource.inquirytitlexreq %>";
                errorMessage = errorMessage + inquirytitlexreq + "<br>";
                $('#InquiryTitleInd').css('display', 'block');
                isValid = false;
            }
            else {
                $('#InquiryTitleInd').css('display', 'none');
            }
            if ($('#<%=txtTitle1.ClientID %>').val().trim() == '') {
                var itemtitle1xreq = "<%= Resources.MyResource.itemtitle1xreq %>";
                errorMessage = errorMessage + itemtitle1xreq + "<br>";
                $('#ItemTitleInd1').css('display', 'block');
                isValid = false;
            }
            else {
                $('#ItemTitleInd1').css('display', 'none');
            }
            if (!isValid) {
                ShowPopup('Message', errorMessage);
            }
            return isValid;
        }
        function ValidateRec2() {
            var errorMessage = '';
            var isValid = true;
            if ($('#<%=txtTitle2.ClientID %>').val().trim() == '') {
                var itemtitle2xreq = "<%= Resources.MyResource.itemtitle2xreq %>";
                errorMessage = errorMessage + itemtitle2xreq + "<br>";
                $('#ItemTitleInd2').css('display', 'block');
                isValid = false;
            }
            else {
                $('#ItemTitleInd2').css('display', 'none');
            }
            if (!isValid) {
                ShowPopup('Message', errorMessage);
            }
            return isValid;
        }
        function ValidateRec3() {
            var errorMessage = '';
            var isValid = true;
            if ($('#<%=txtTitle3.ClientID %>').val().trim() == '') {
                var itemtitle3xreq = "<%= Resources.MyResource.itemtitle3xreq %>";
                errorMessage = errorMessage + itemtitle3xreq + "<br>";
                $('#ItemTitleInd3').css('display', 'block');
                isValid = false;
            }
            else {
                $('#ItemTitleInd3').css('display', 'none');
            }
            if (!isValid) {
                ShowPopup('Message', errorMessage);
            }
            return isValid;
        }
    </script>
    <script type="text/javascript" language="javascript">
        // Set foucs and Multip handler
        $(document).ready(function () {
            if ($('#<%=hfIsUserLoggedIn.ClientID %>').val() == "true") {
                $('#<%=txtInquiryTitle.ClientID %>').focus();
            } else {
                $('#<%=txtBoxUserName.ClientID %>').focus();
            }
            CreateMultipleUpload();
        });


        function CreateMultipleUpload() {
            //set up the file upload
            $("#" + "<%=fuImageUpload.ClientID %>").MultiFile(
              {
                  max: 4,
                  accept: 'psd,pdd,bmp,rle,dib,gif,eps,jpg,jpeg,jpe,pcx,pdf,pdp,pct,pict,pxr,png,raw,sct,tiff,tif,svc,bmp,doc,docx,xls,ppt,pptx,xlsx',
                  afterFileSelect: function (element, value, master_element) {

                  },
                  afterFileRemove: function (element, value, master_element) {

                  }
              });
        }


        function FileUploaderHide() {

            $('input:file').MultiFile('reset');
        }

        $('#<%= txtBoxUserEmail.ClientID %>').blur(function () {
            if ($('#<%= txtBoxUserEmail.ClientID %>').val() === '') {
                ShowPopup('Message', "please enter email to proceed.");
        } else {
                var IsEmailValid = ValidateEmail($('#<%= txtBoxUserEmail.ClientID %>').val());
                if (IsEmailValid == false) {
                    ShowPopup('Message', "please enter valid email to proceed.");
                }
        }
        });
    </script>
</asp:Content>
