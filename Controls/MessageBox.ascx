<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MessageBox.ascx.cs"
    Inherits="Web2Print.UI.Controls.MessageBox" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Button ID="btnMessageBox" runat="server" Style="display: none; width: 0px; height: 0px" />
<ajaxToolkit:ModalPopupExtender ID="mpeMessageBox" runat="server" BackgroundCssClass="ModalPopupBG"
    PopupControlID="pnlMessageBox" TargetControlID="btnMessageBox" BehaviorID="mpeMessageBox"
    CancelControlID="btnCancelMessageBox" DropShadow="false">
</ajaxToolkit:ModalPopupExtender>
<asp:Panel ID="pnlMessageBox" runat="server" Width="500px" CssClass="FileUploaderPopup_Mesgbox LCLB rounded_corners"
    Style="display: none">
    <div class="white_background" style="padding:20px;">
   <div class="Width100Percent popUpsDisply">
        <div class="exit_container_PopUpMesg">
            <div id="btnCancelMessageBox" runat="server" class="exit_popup">
            </div>
        </div>
    </div>
    <asp:Label ID="lblHeader" runat="server" CssClass="left_align FileUploadHeaderText_PopUp float_left_simple MesgBoxClass"></asp:Label>
     
       <div id="CancelControl" onclick="$find('mpeMessageBox').hide();" class="MesgBoxBtnsDisplay rounded_corners5" style="">
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
        <div id="OkBtn" class="padding_top_bottom_10 popUpsDisply">
            <asp:Button ID="btnOkMessageBox" runat="server" Text="OK" OnClientClick="$find('mpeMessageBox').hide(); return false;"
                CssClass="start_creating_btn rounded_corners5" />
        </div>
    </div>
    </div>
</asp:Panel>
 
<script type="text/javascript" language="javascript">
    function ShowPopup(header, message) {
        $('#<%= lblHeader.ClientID %>').html(header);
        $('#<%= lblMessage.ClientID %>').html(message);
        $find('mpeMessageBox').show();
    }
</script>