<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CRUDTerritory.ascx.cs"
    Inherits="Web2Print.UI.Controls.CRUDTerritory" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<div onclick="ShowAddNewTerritory();" class="cursor_pointer">
    <div class="float_left">
        <img alt="" class="add_new" src="images/AddNew.png" title="Add New Territory" /></div>
    <div class="new_caption">
        <asp:Literal ID="ltrlnewTerritory" runat="server" Text="New Territory"></asp:Literal>
    </div>
    <div class="clearBoth">
    </div>
</div>
<div class="divSearchBar_corp normalTextStyle rounded_corners territory_row_padding_header textAlignLeft height17px">
    <div class="territory_code_div_grid">
        <asp:Literal ID="ltrlterritorycode" runat="server" Text="Territory Code"></asp:Literal>
    </div>
    <div class="territory_corp_div_grid">
        <asp:Literal ID="ltrlterritoryname" runat="server" Text="Territory Name"></asp:Literal>
    </div>
</div>
<div class="white_background rounded_corners textAlignLeft MTOp5 Broker_Panel_padding">
    <div class="Width100Percent">
        <div class="textAlignCenter paddingBottom10px normalTextStyle">
            <asp:Label ID="lblMessage" runat="server" Text="No Record Found!" Visible="false" />
        </div>
    </div>
    <asp:UpdatePanel ID="upTerritories" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:Repeater ID="rprTerritories" runat="server" OnItemCommand="rprTerritories_ItemCommand"
                OnItemDataBound="rprTerritories_ItemDataBound">
                <ItemTemplate>
                    <div class="territory_row_padding dottedBorderStyle" title="Click to edit Territory">
                        <div id="divContainer" runat="server" class="cursor_pointer">
                            <div class="territory_code_div_grid">
                                <%# Eval("TerritoryCode")%>&nbsp;</div>
                            <div class="territory_corp_div_grid">
                                <%# Eval("TerritoryName")%>&nbsp;</div>
                        </div>
                        <div class="float_right" style=" margin-top: -10px;">
                            <asp:ImageButton ID="btnDelete" runat="server" OnClientClick="return confirm('Are you sure you want to delete this territory?');" CssClass="rounded_corners" ImageUrl="~/images/delete.png" Height="28" Width="28"
                                ToolTip="Delete Territory" CommandName="DeleteRecord" CommandArgument='<%# Eval("TerritoryID") %>' /></div>
                    </div>
                    <div class="clearBoth">
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </ContentTemplate>
        <%-- <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnSaveTerritory" EventName="Click" />
    </Triggers>--%>
    </asp:UpdatePanel>
</div>
<%-- Popup--%>
<asp:Button ID="btnTerritoryPopup" runat="server" Style="display: none; width: 0px;
    height: 0px" />
<ajaxToolkit:ModalPopupExtender ID="mpeTerritory" runat="server" BackgroundCssClass="ModalPopupBG"
    PopupControlID="pnlTerritory" TargetControlID="btnTerritoryPopup" BehaviorID="mpeTerritory"
    CancelControlID="btnCancelMessageBox" DropShadow="false">
</ajaxToolkit:ModalPopupExtender>
<asp:Panel ID="pnlTerritory" runat="server" Width="500px" CssClass="FileUploaderPopup_Mesgbox_corp LCLB rounded_corners"
    Style="display: block">
    <div class="Width100Percent">
        <div class="float_left">
            <asp:Label ID="lblHeader" runat="server" CssClass="FileUploadHeaderText"></asp:Label>
        </div>
        <div class="exit_container">
            <div id="btnCancelMessageBox" runat="server" class="exit_popup3">
            </div>
        </div>
    </div>
    <div class="pop_body_MesgPopUp">
        <asp:UpdatePanel ID="upnlBody" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="left_label_area">
                    <asp:Literal ID="ltrlterritorycode2" runat="server" Text="Territory Code:"></asp:Literal>
                </div>
                <div class="right_control_area">
                    <asp:TextBox ID="txtTerritoryCode" runat="server" CssClass="text_box150 rounded_corners5"
                        MaxLength="200"></asp:TextBox>
                </div>
                <div class="left_label_area">
                    <asp:Literal ID="ltrlterritoryname2" runat="server" Text="Territory Name:"></asp:Literal>
                </div>
                <div class="right_control_area">
                    <asp:TextBox ID="txtTerritoryName" runat="server" CssClass="text_box150 rounded_corners5"
                        MaxLength="200"></asp:TextBox>
                </div>
                <br />
                <br />
                <div class="left_label_area">
                    &nbsp;</div>
                <div class="right_control_area">
                    <div class="float_left">
                        <asp:Button ID="btnSaveTerritory" runat="server" Text="Save" OnClientClick="return ValidateTerritory();"
                            CssClass="start_creating_btn rounded_corners5" OnClick="btnSaveTerritory_Click" /></div>
                    <div class="float_left">
                        <asp:Button ID="btnCancelTerritory" runat="server" Text="Cancel" OnClientClick="HideAddNewTerritory(); return false;"
                            CssClass="start_creating_btn rounded_corners5" /></div>
                </div>
                <div class="clearBoth">
                    <asp:HiddenField ID="hfTerritoryId" runat="server" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Panel>
<asp:HiddenField ID="hfterrtoryDeleted" runat="server" />
<asp:HiddenField ID="hfterrtoryNotDeleted" runat="server" />
<asp:HiddenField ID="hfterroryIsInuse" runat="server" />
<asp:HiddenField ID="hfTeroryNameExist" runat="server" />
<asp:HiddenField ID="hfTeroryCodeExist" runat="server" />
<script type="text/javascript" language="javascript">
    //Territory

    function ShowAddNewTerritory() {
        // Clear the fields first
        $('#<%=hfTerritoryId.ClientID %>').val('');
        $('#<%=txtTerritoryCode.ClientID %>').val('');
        $('#<%=txtTerritoryName.ClientID %>').val('');

        $find('mpeTerritory').show();
        $('#<%=txtTerritoryCode.ClientID %>').focus();
    }
    function HideAddNewTerritory() {
        $find('mpeTerritory').hide();
    }
    function EditTerritory(territoryId, territoryCode, territoryName) {
        $('#<%=hfTerritoryId.ClientID %>').val(territoryId);
        $('#<%=txtTerritoryCode.ClientID %>').val(territoryCode);
        $('#<%=txtTerritoryName.ClientID %>').val(territoryName);

        $find('mpeTerritory').show();
    }
    function ValidateTerritory() {
        if ($('#<%=txtTerritoryName.ClientID %>').val().trim() == '') {
            var Plxenterterritoryname = "<%= Resources.MyResource.Plxenterterritoryname %>";
            ShowPopup('Message', Plxenterterritoryname);
            return false;
        }
        else {
            return true;
        }
    }
</script>
