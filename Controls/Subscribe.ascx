<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Subscribe.ascx.cs" Inherits="Web2Print.UI.Controls.Subscribe" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<div class="subscribe_sec">
    <div class="subscribe_heading" id="divsubscribe" runat="server">
        <asp:Literal ID="ltrlsubscribetxt" runat="server" Text="Subscribe"></asp:Literal>
    </div>
    <div style="height: 17px;">
        &nbsp;</div>
    <asp:Literal ID="ltrlsubscribe" runat="server" Text="Subscribe and get the latest exclusive"></asp:Literal>
    <br />
    <asp:Literal ID="ltrlofferdelivered" runat="server" Text="offers delivered to your inbox..."></asp:Literal>
    <div class="space3">
        &nbsp;</div>
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="search_textBox rounded_corners5"
                    Text="Enter email address..." ValidationGroup="email" CausesValidation="false"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="btnGo" runat="server" CssClass="go_button rounded_corners5" Text="GO"
                    OnClientClick="return ValidateBottomSubscriberEmail();" OnClick="btnGo_Click"  />
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
</div>
<script type="text/javascript">
    $(document).ready(function () {
        $('#<%= txtEmail.ClientID %>').focus(function () {
            $(this).val('');
        });
    });
    function ValidateBottomSubscriberEmail() {
        var email = $('#<%= txtEmail.ClientID %>').val().trim();
        return ValidateEmail(email);
    }
</script>
