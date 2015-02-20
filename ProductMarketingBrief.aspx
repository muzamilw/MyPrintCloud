<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ThemeSite.Master" AutoEventWireup="true"
    CodeBehind="ProductMarketingBrief.aspx.cs" Inherits="Web2Print.UI.ProductMarketingBrief" %>

<%@ Register Src="~/Controls/BreadCrumbMenu.ascx" TagName="BreadCrumbMenu" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContents" runat="server">
    
    <script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content_area">
        <div class="left_right_padding">
            <uc1:BreadCrumbMenu ID="BreadCumbMenuCategory" runat="server" />
            <div class="signin_heading_div float_left_simple">
                <asp:Image ID="imgLght" runat="server" ImageUrl="~/images/artwork_light_icons.png"
                    Width="40px" />
            </div>
            <div class="signin_heading_div float_left_simple" style="margin-top: 3px;">
                <asp:Label ID="lblInqueryBreif" runat="server" Text="Inquiry Breif" Style="font-size: 22px;"></asp:Label>
            </div>
            <div class="clearBoth">
                &nbsp;
            </div>
            <div class="MB_MsgSm">
                <asp:Label ID="lblSummeryMesg" runat="server">
                </asp:Label>
            </div>
            <div class="clearBoth">
                &nbsp;
            </div>
            <div id="welcomeSummeryMEsg" runat="server" class="LGBC rounded_corners paddingLeft5px Mleft10"
                style="display: none; padding: 5px;">
                <div class="white_background rounded_corners MB_P5o">
                    <asp:Label ID="WlSumMesg" CssClass="MB_ThksMsg" runat="server"></asp:Label>
                    <asp:LinkButton CssClass="spanRetURl" ID="lnkReturnLogin" runat="server" Text="Return to home Page"
                        CausesValidation="false"></asp:LinkButton>
                </div>
            </div>
            <div id="LeftPanel" class="float_left_simple MB_Width780" runat="server">
                <asp:Repeater ID="rptQuestionsInquiryBreif" runat="server" OnItemDataBound="rptQuestionsInquiryBreif_ItemDataBound">
                    <ItemTemplate>
                        <div class="float_left_simple LGBC rounded_corners paddingLeft5px Mleft10 MB_MainCont"
                            runat="server">
                            <div class="white_background rounded_corners MB_InnerCont">
                                <div id="questionCont" class="MB_QtnsCont" runat="server">
                                </div>
                                <div id="answersCont" runat="server" class="MB_AnsCont">
                                </div>
                                <div class="clearBoth">
                                    &nbsp;
                                </div>
                                <div class="MB_AddInfoCOnt">
                                    <asp:Label ID="AddInfo" runat="server" Text="Additional Info" CssClass="MB_AddInfoColor"></asp:Label>
                                    <textarea id="txtAddAns" class="rounded_corners MB_txtbxInfo" runat="server" 
                                        cols="10" rows="3"></textarea>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <div class="clearBoth">
                    &nbsp;
                </div>
                <div id="MBUploadContainerDisplay" runat="server" class="float_left_simple LGBC rounded_corners paddingLeft5px Mleft10 MB_MainCont">
                    <div class="white_background rounded_corners MB_InnerCont">
                        <div class="MB_QtnsCont">
                            Upload files
                        </div>
                        <div class="MB_UploadContainer">
                            <asp:Label ID="UpFileGuidl" runat="server" Text="Upload file guidelines" CssClass="Fsize17"></asp:Label><br />
                            <br />
                            <div class="MB_GLContainer">
                                <asp:Label ID="lblGL4Heading" runat="server" Text="File formats:"></asp:Label>
                                <asp:Literal ID="ltrlGL4Desc" runat="server" Text="PNG, GIF, PDF and JPG file formats."></asp:Literal>
                            </div>
                            <div class="MB_GLContainer">
                                <asp:Label ID="lblGL6Heading" runat="server" Text="File size:"></asp:Label>
                                <asp:Literal ID="ltrlGL6Desc" runat="server" Text="File greater than 4 mb are not permitted for upload."></asp:Literal>
                            </div>
                        </div>
                        <div class="MB_fileUploadContainer">
                            <asp:FileUpload ID="fuImageUpload" runat="server" CssClass="file_upload_box185" />
                        </div>
                    </div>
                </div>
                <div class="clearBoth">
                    &nbsp;
                </div>
            </div>
            <div id="RightPanel" runat="server" class="MB_RightCont float_left_simple">
            <asp:Image id="CatImge" runat="server" CssClass="MB_ProductImage float_left_simple"/>
            <div  class="float_left_simple LGBC rounded_corners paddingLeft5px Mleft10 MB_InnerRightCont">
                <div class="white_background rounded_corners MB_RightInnerPnl">
                    <div class="MB_GrayBack">
                        <asp:Label ID="lblReplyBacktome" runat="server" Text="Reply back to me by" CssClass="Fsize17"></asp:Label>
                        <div class="left_align">
                            <asp:Label ID="lblReplyemailtxt" runat="server" Text="email" CssClass="MB_EMailtxtColor"></asp:Label></div>
                        <asp:Label ID="lblEMailAdd" runat="server"></asp:Label><br />
                        <div style="margin-top: 10px; text-align: left;">
                            <asp:Label ID="lblTelText" runat="server" Text="Tel:" CssClass="MB_EMailtxtColor"></asp:Label></div>
                        <asp:Label ID="lblPhoneNo" runat="server"></asp:Label><br />
                        <asp:HyperLink ID="hpChange" Text="change" NavigateUrl="#" runat="server" Font-Underline="true"
                            Visible="false" ForeColor="#73a8ac" Style="float: right; cursor: pointer; clear: both;"></asp:HyperLink>
                        <div class="clearBoth">
                            &nbsp;
                        </div>
                    </div>
                    <div style="margin: 5px; margin-top: 15px;">
                        <asp:Button ID="SendINqBtn" runat="server" Text="Send" CssClass="MB_SendINqBtn rounded_corners5"
                            OnClientClick="return GetAllinputElementIDS();" OnClick="SendINqBtn_Click"  />
                    </div>
                </div>
            </div>
            <div class="clearBoth">
                &nbsp;
            </div>
            </div>
            <div class="clearBoth">
                &nbsp;
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hfIdsList" runat="server" />
    <asp:HiddenField ID="hfGroupsCount" runat="server" Value="0" />
    <asp:HiddenField ID="hfTotalGroups" runat="server" />
    <asp:HiddenField ID="hfInqueryMesg" runat="server" />
    <asp:HiddenField ID="hfHaveNoAns" runat="server" />
    <asp:HiddenField ID="hfAttachedFileNames" runat="server" />
    <script type="text/javascript">

        $(document).ready(function () {
            CreateMultipleUpload();
        });

        function CreateMultipleUpload() {
            //set up the file upload
            $("#" + "<%=fuImageUpload.ClientID %>").MultiFile(
              {
                  max: 4,
                  accept: 'jpg,png,pdf,gif,jpeg',
                  afterFileSelect: function (element, value, master_element) {

                  },
                  afterFileRemove: function (element, value, master_element) {

                  }
              });
        }

        function ShowUploader() {
            $("#" + "<%=fuImageUpload.ClientID %>").click();
        }

        function ISUpload() {
            if ($("#" + "<%=fuImageUpload.ClientID %>").val() == '') {
                ShowPopup('Message', 'Please select atleast 1 file to upload.');
                return false;
            } else {
                return true;
            }
        }
        $(function () {
            $('.radio').buttonset();
        });

        function TickRadio(RadioClassName) {
            $('input:radio[class=' + RadioClassName + ']').attr('checked', true);
            if ($('input:radio[class=' + RadioClassName + ']').is(':checked')) {

                $('input:radio[class=' + RadioClassName + ']').next().removeClass('grayRadiantbtn').addClass('OrangeGradiantbtn');

                var name = $('input:radio[class=' + RadioClassName + ']').attr("name");

                $('input:radio[name=' + name + ']').each(function () {
                    if ($(this).is(':checked')) {
                        $(this).parent().parent().parent().parent().addClass('white_background');
                        $(this).parent().parent().parent().parent().css("background-color", "white");
                    } else {
                        $(this).next().removeClass('OrangeGradiantbtn').addClass('grayRadiantbtn');
                    }
                });
            }
        }

        function TickCheckBox(CheckBoxClassName) {
            if ($('input:checkbox[class=' + CheckBoxClassName + ']').is(':checked')) {
                
                $('input:checkbox[class=' + CheckBoxClassName + ']').attr('checked', false);
                $('input:checkbox[class=' + CheckBoxClassName + ']').next().removeClass('OrangeGradiantbtn').addClass('grayRadiantbtn');
            
            } else {
                
                $('input:checkbox[class=' + CheckBoxClassName + ']').attr('checked', true);
                $('input:checkbox[class=' + CheckBoxClassName + ']').next().removeClass('grayRadiantbtn').addClass('OrangeGradiantbtn');
                $('input:checkbox[class=' + CheckBoxClassName + ']').parent().parent().parent().parent().addClass('white_background');
                $('input:checkbox[class=' + CheckBoxClassName + ']').parent().parent().parent().parent().css("background-color", "white");

             }
        }

        function GetAllinputElementIDS() {
            var selectedradioBtnsCount = 0;
            var groupCount = $("#<%=hfGroupsCount.ClientID %>").val();
            var txtVal = "";
            var idList = "";
            var AdditionalAnsList = "";

            var AllAnswers = $("#<%=hfHaveNoAns.ClientID %>").val();

            if (AllAnswers != '') {
                var EachAnswer = AllAnswers.split(',');
                $.each(EachAnswer, function (i, element) {
                    var TypeOfAns = $('.' + element + '').attr("Type");
                    if ($('.' + element + '').children().html() == null) {
                        if (AdditionalAnsList == "") {
                            AdditionalAnsList = AdditionalAnsList + "Q" + $('.' + element + '').parent().prev().text() + " \n " + "Additional Information :  " + $('.' + element + '').parent().next().next().children().next().val();
                        } else {
                            AdditionalAnsList = AdditionalAnsList + "|" + "Q" + $('.' + element + '').parent().prev().text() + " \n " + "Additional Information :  " + $('.' + element + '').parent().next().next().children().next().val();

                        }
                    } else {
                        if (TypeOfAns == 1) { //case of checkbox 
                            
                            var isCheckedd = 0;
                            var AnswerList = "";
                            var GroupName = $('.' + element + '').children().children().attr("name");
                            $('input:checkbox[name=' + GroupName + ']').each(function () {
                                if ($(this).is(':checked')) {
                                    if (AnswerList == "") {
                                        AnswerList = $(this).next().text();
                                    } else {
                                        AnswerList = AnswerList + ", " + $(this).next().text();
                                    }
                                    if (isCheckedd == 0) {
                                        isCheckedd = 1;
                                        selectedradioBtnsCount = selectedradioBtnsCount + 1;
                                    }
                                }
                            });
                            if (AdditionalAnsList == "") {
                                AdditionalAnsList = AdditionalAnsList + "Q" + $('.' + element + '').parent().prev().text() + " \n " + "Selected Answer : " + AnswerList + " \n " + "Additional Information :  " + $('.' + element + '').parent().next().next().children().next().val();
                            } else {
                                AdditionalAnsList = AdditionalAnsList + "|" + "Q" + $('.' + element + '').parent().prev().text() + " \n " + "Selected Answer : " + AnswerList + " \n " + "Additional Information :  " + $('.' + element + '').parent().next().next().children().next().val();
                            }

                        } else { // case of radio 
                            $('input[type=radio]').each(function () {
                                if ($(this).is(':checked')) {
                                    var ClassOFSelectedRadio = $(this).parent().parent().attr("class");

                                    if (ClassOFSelectedRadio == element) {
                                        selectedradioBtnsCount = selectedradioBtnsCount + 1;
                                        if (AdditionalAnsList == "") {
                                            AdditionalAnsList = AdditionalAnsList + "Q" + $(this).parent().parent().parent().prev().text() + " \n " + "Selected Answer : " + $(this).next().text() + " \n " + "Additional Information :  " + $(this).parent().parent().parent().next().next().children().next().val();
                                        } else {
                                            AdditionalAnsList = AdditionalAnsList + "|" + "Q" + $(this).parent().parent().parent().prev().text() + " \n " + "Selected Answer : " + $(this).next().text() + " \n " + "Additional Information :  " + $(this).parent().parent().parent().next().next().children().next().val();
                                        }
                                        return;
                                    }
                                }
                            });
                        }
                    }
                });
            }

           
            if (selectedradioBtnsCount < groupCount) {
                ShowPopup('Message', 'Please select an option in the highlighted questions to submit your inquiry.');
                var AllGroups = $("#<%=hfTotalGroups.ClientID %>").val();
                var EachGroup = AllGroups.split(',');
                $.each(EachGroup, function (i, element) {

                    var i = 0;
                    var id = "";
                    var SelecedRadioNotHighlighted = $('input[type="radio"][name="' + element + '"]:checked').attr("id");
                    if (SelecedRadioNotHighlighted == undefined) {
                        $('input:radio[name=' + element + ']').each(function () {
                            $(this).parent().parent().parent().parent().removeClass('white_background');
                            $(this).parent().parent().parent().parent().css("background-color", "#ECAAAA");
                        });
                        if (i == 0 & id != "") {

                        }
                    }
                    var SelecedCheckBoxNotHighlighted = $('input[type="checkbox"][name="' + element + '"]:checked').attr("id");
                    if (SelecedCheckBoxNotHighlighted == undefined) {
                        $('input:checkbox[name=' + element + ']').each(function () {
                            $(this).parent().parent().parent().parent().removeClass('white_background');
                            $(this).parent().parent().parent().parent().css("background-color", "#ECAAAA");
                        });
                        if (i == 0 & id != "") {

                        }
                    }
                });
                return false;
            } else {
                $("#<%=hfInqueryMesg.ClientID %>").val(AdditionalAnsList);
                return true;
            }
        }
    </script>
</asp:Content>
