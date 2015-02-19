<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MessageBoxTwo.ascx.cs" Inherits="Web2Print.UI.Controls.MessageBoxTwo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Button ID="btnMessageBox" runat="server" Style="display: none; width: 0px; height: 0px" />
<ajaxToolkit:ModalPopupExtender ID="mpeMessageBox2" runat="server" BackgroundCssClass="ModalPopupBG"
    PopupControlID="pnlMessageBox" TargetControlID="btnMessageBox" BehaviorID="mpeMessageBox2"
    CancelControlID="btnCancelMessageBox" DropShadow="false">
</ajaxToolkit:ModalPopupExtender>
<asp:Panel ID="pnlMessageBox" runat="server" Width="500px" CssClass="FileUploaderPopup_Mesgbox LCLB rounded_corners"
    Style="display: none">
    <div class="white_background" style="padding:20px;">
    <div class="Width100Percent">
        <div class="exit_container_PopUpMesg popUpsDisply">
            <div id="btnCancelMessageBox" runat="server" class="exit_popup">
            </div>
        </div>
    </div>
    <asp:Label ID="lblHeader" runat="server" CssClass="left_align FileUploadHeaderText_PopUp float_left_simple MesgBoxClass"></asp:Label>
    
       <div id="CancelControl" onclick="$find('mpeMessageBox2').hide();" class="MesgBoxBtnsDisplay rounded_corners5">
        Close
    </div>
    <div class="clearBoth">
        &nbsp;
        </div>
        <div class="SolidBorderCS">
        &nbsp;
        </div>
    <div class="pop_body_MesgPopUp">
        <br /> 
        <div class="inner">
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </div>
        <br />
        <div class="padding_top_bottom_10 center_align">
            <asp:Button ID="btnOkMessageBox" runat="server" Text="Resume" OnClientClick="SelectedResume();  return false;"
                CssClass="start_creating_btn rounded_corners5" />
            <asp:Button ID="Button1" runat="server" Text="Create New" OnClientClick="SelectedNew(); return false;"
                CssClass="start_creating_btn rounded_corners5" />
        </div>
    </div>
    </div>
</asp:Panel>
<asp:Button ID="btnYesNO" runat="server" Style="display: none; width: 0px; height: 0px" />
<ajaxToolkit:ModalPopupExtender ID="MpeMessgBox" runat="server" BackgroundCssClass="ModalPopupBG"
    PopupControlID="pnlMBox" TargetControlID="btnYesNO" BehaviorID="MpeMessgBox"
    CancelControlID="btnCancel" DropShadow="false">
</ajaxToolkit:ModalPopupExtender>
<asp:Panel ID="pnlMBox" runat="server" Width="500px" CssClass="FileUploaderPopup_Mesgbox LCLB rounded_corners"
    Style="display: none">
     <div class="white_background" style="padding:20px;">
          <asp:Label ID="lblHeader1" runat="server" CssClass="left_align FileUploadHeaderText_PopUp float_left_simple MesgBoxClass"></asp:Label>
         <div onclick="$find('MpeMessgBox').hide();" class="MesgBoxBtnsDisplay rounded_corners5">
        Close
    </div>
    <div class="clearBoth">
        &nbsp;
        </div>
         <div class="SolidBorderCS">
        &nbsp;
        </div>
    <div class="pop_body_MesgPopUp">
        <br />
        <div class="inner">
            <asp:Label ID="lblMesgDes" runat="server"></asp:Label>
        </div>
        <br />
        <div class="padding_top_bottom_10 center_align">
            <asp:Button ID="btnContinue" runat="server" Text="Ok" OnClientClick="SelectedOK();  return false;"
                CssClass="start_creating_btn rounded_corners5" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="start_creating_btn_LetWait rounded_corners5" />
        </div>
    </div>
    </div>
</asp:Panel>

<asp:HiddenField ID="hfBtnId" runat="server" Value="0" />

<ajaxToolkit:ModalPopupExtender ID="mpeMessageBoxPriceMatrix" runat="server" BackgroundCssClass="ModalPopupBG"
    PopupControlID="pnlPriceMatrix" TargetControlID="btnYesNO" BehaviorID="mpeMessageBoxPriceMatrix"
    CancelControlID="btnCancel" DropShadow="false">
</ajaxToolkit:ModalPopupExtender>
<asp:Panel ID="pnlPriceMatrix" runat="server" Width="500px" CssClass="FileUploaderPopup_Mesgbox LCLB rounded_corners"
    Style="display: none">
     <div class="white_background" style="padding:20px;">
          <asp:Label ID="lblHeaderReset" runat="server" CssClass="left_align FileUploadHeaderText_PopUp float_left_simple MesgBoxClass"></asp:Label>
         <%--<div onclick="$find('mpeMessageBoxPriceMatrix').hide();" class="MesgBoxBtnsDisplay rounded_corners5">
        Close
    </div>--%>
    <div class="clearBoth">
        &nbsp;
        </div>
         <div class="SolidBorderCS">
        &nbsp;
        </div>
    <div class="pop_body_MesgPopUp">
        <br />
        <div class="inner">
            <asp:Label ID="lblMessageReset" runat="server"></asp:Label>
        </div>
        <br />
        <div class="padding_top_bottom_10 center_align">
            <asp:Button ID="Button3" runat="server" Text="Ok" OnClientClick="SelectedOKReset();  return false;"
                CssClass="start_creating_btn_LetWait rounded_corners5" />
            <asp:Button ID="Button4" runat="server" Text="Cancel" CssClass="start_creating_btn_LetWait rounded_corners5" />
        </div>
    </div>
    </div>
</asp:Panel>

<ajaxToolkit:ModalPopupExtender ID="mpeMessageBoxPriceMatrixSave" runat="server" BackgroundCssClass="ModalPopupBG"
    PopupControlID="pnlSave" TargetControlID="btnYesNO" BehaviorID="mpeMessageBoxPriceMatrixSave"
    CancelControlID="btnCancel" DropShadow="false">
</ajaxToolkit:ModalPopupExtender>
<asp:Panel ID="pnlSave" runat="server" Width="500px" CssClass="FileUploaderPopup_Mesgbox LCLB rounded_corners"
    Style="display: none">
     <div class="white_background" style="padding:20px;">
          <asp:Label ID="lblHeaderSave" runat="server" CssClass="left_align FileUploadHeaderText_PopUp float_left_simple MesgBoxClass"></asp:Label>
         <%--<div onclick="$find('mpeMessageBoxPriceMatrixSave').hide();" class="MesgBoxBtnsDisplay rounded_corners5">
        Close
    </div>--%>
    <div class="clearBoth">
        &nbsp;
        </div>
         <div class="SolidBorderCS">
        &nbsp;
        </div>
    <div class="pop_body_MesgPopUp">
        <br />
        <div class="inner">
            <asp:Label ID="lblMessageSave" runat="server"></asp:Label>
        </div>
        <br />
        <div class="padding_top_bottom_10 center_align">
            <asp:Button ID="Button5" runat="server" Text="No" OnClientClick="SelectedOKSave();  return false;"
                CssClass="start_creating_btn_LetWait rounded_corners5" />
            <asp:Button ID="Button6" runat="server" Text="Yes"  OnClientClick="SelectedYesSave(); return false;" CssClass="start_creating_btn_LetWait rounded_corners5" />
        </div>
    </div>
    </div>
</asp:Panel>

<script type="text/javascript" language="javascript">
    function ShowPopupReset(header, message) {
        $('#<%= lblHeaderReset.ClientID %>').html(header);
         $('#<%= lblMessageReset.ClientID %>').html(message);
        $find('mpeMessageBoxPriceMatrix').show();
    }

    function ShowPopupSaveChanges(header, message) {
        $('#<%= lblHeaderSave.ClientID %>').html(header);
        $('#<%= lblMessageSave.ClientID %>').html(message);
        $find('mpeMessageBoxPriceMatrixSave').show();
    } 
    function ShowPopup2(header, message) {
        $('#<%= lblHeader.ClientID %>').html(header);
        $('#<%= lblMessage.ClientID %>').html(message);
        $find('mpeMessageBox2').show();
        
    }
    function ShowPopupAddToCart(header, message) {
        $('#<%= lblHeader1.ClientID %>').html(header);
        $('#<%= lblMesgDes.ClientID %>').html(message);
        $find('MpeMessgBox').show();
    }
    
    function SelectedResume() {
        $('#MainContent_hfState').val("resume");
        $find('mpeMessageBox2').hide();
        __doPostBack('ctl00$MainContent$btnEditThisDesign', '');
    }
    function SelectedOK() {
        $('#MainContent_hfState').val("ok");
        $find('mpeMessageBox2').hide();
        __doPostBack('ctl00$MainContent$btnAddToCart', '');
    }
    function SelectedNew() {
        $find('mpeMessageBox2').hide();
         $('#MainContent_hfState').val("new");
        __doPostBack('ctl00$MainContent$btnEditThisDesign', '');
    }
  

    function SelectedOKReset() {
        //$('#MainContent_hfReset').val(1);
        $find('mpeMessageBoxPriceMatrix').hide();
       // alert($('#btnResetPriceMatrix'));
        __doPostBack('btnResetPriceMatrix', '');
    }
    function SelectedOKSave() {
        //$('#MainContent_hfReset').val(1);
        $find('mpeMessageBoxPriceMatrixSave').hide();
        // alert($('#btnResetPriceMatrix'));
        __doPostBack('btnSaveAdditionalMarkup', '');
    }
    function SelectedYesSave() {
        //$('#MainContent_hfReset').val(1);
        $find('mpeMessageBoxPriceMatrixSave').hide();
        // alert($('#btnResetPriceMatrix'));
        __doPostBack('btnClosePopup', '');
    }
 
</script>