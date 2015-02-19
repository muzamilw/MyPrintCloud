<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CRUDDepartment.ascx.cs"
    Inherits="Web2Print.UI.Controls.CRUDDepartment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<div onclick="ShowAddNewDepartment();" class="cursor_pointer">
    <div class="float_left">
        <img alt="" class="add_new" src="images/AddNew.png" title="Add New Department" /></div>
    <div class="new_caption">
        <asp:Label ID="lblNewDepartText" runat="server" Text="New Department"></asp:Label>
    </div>
    <div class="clearBoth">
    </div>
</div>
<div class="divSearchBar_corp normalTextStyle rounded_corners territory_row_padding_header textAlignLeft">
    <div class="territory_code_div_grid">
        <asp:Literal ID="ltrldepartcode" runat="server" Text=" Department Code"></asp:Literal>
    </div>
    <div class="territory_corp_div_grid">
        <asp:Literal ID="ltrldepartname" runat="server" Text="Department Name"></asp:Literal>
    </div>
</div>
<div class="white_background rounded_corners textAlignLeft MTOp5 Broker_Panel_padding">
 <div class="textAlignCenter paddingBottom10px normalTextStyle">
                                <asp:Label ID="lblMessage" runat="server" Text="No Record Found!"  Visible="false"/>
                            </div>
    <asp:UpdatePanel ID="upnDepartments" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:Repeater ID="rptrDepartments" runat="server" OnItemDataBound="rptrDepartments_ItemDataBound"
                OnItemCommand="rptrDepartments_ItemCommand">
                <ItemTemplate>
                    <div class="territory_row_padding dottedBorderStyle">
                        <div id="divContainer" runat="server" class="cursor_pointer">
                            <div class="territory_corp_div_grid">
                                <%# Eval("DepartmentCode")%>&nbsp;</div>
                            <div class="territory_corp_div_grid">
                                <%# Eval("DepartmentName")%>&nbsp;
                            </div>
                        </div>
                        <div class="float_right">
                            <asp:Button ID="btnDelete" runat="server" CssClass="delete_icon_img" OnClientClick="return confirm('Are you sure you want to delete this department?');"
                                ToolTip="Delete Department" CommandName="DeleteRecord" CommandArgument='<%# Eval("DepartmentID") %>' />
                        </div>
                    </div>
                    <div class="clearBoth">
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </ContentTemplate>
        <%--<Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnSaveDepartment" EventName="Click" />
    </Triggers>--%>
    </asp:UpdatePanel>
</div>
<%-- Popup--%>
<asp:Button ID="btnDepartmentPopup" runat="server" Style="display: none; width: 0px;
    height: 0px" />
<ajaxToolkit:ModalPopupExtender ID="mpeDepartment" runat="server" BackgroundCssClass="ModalPopupBG"
    PopupControlID="pnlDepartment" TargetControlID="btnDepartmentPopup" BehaviorID="mpeDepartment"
    CancelControlID="btnCancelMessageBox" DropShadow="false">
</ajaxToolkit:ModalPopupExtender>
<asp:Panel ID="pnlDepartment" runat="server" Width="500px" CssClass="FileUploaderPopup_Mesgbox_corp LCLB rounded_corners"
    Style="display: none">
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
                    <asp:Literal ID="ltrldepartcode2" runat="server" Text="Department Code:"></asp:Literal>
                </div>
                <div class="right_control_area">
                    <asp:TextBox ID="txtDepartmentCode" runat="server" Width="188px" CssClass="text_box rounded_corners5"
                        MaxLength="200"></asp:TextBox>
                </div>
                <div class="left_label_area">
                    <asp:Literal ID="ltrldepartname2" runat="server" Text="Department Name:"></asp:Literal>
                </div>
                <div class="right_control_area">
                    <asp:TextBox ID="txtDepartmentName" runat="server" Width="188px" CssClass="text_box rounded_corners5"
                        MaxLength="200"></asp:TextBox>
                </div>
                <div class="left_label_area">
                    <asp:Literal ID="ltrlteritory" runat="server" Text="Territory:"></asp:Literal>
                </div>
                <div class="right_control_area">
                    <asp:DropDownList ID="ddlTerritories" runat="server" CssClass="dropdown200 rounded_corners5">
                    </asp:DropDownList>
                </div>
                <br />
                <br />
                <div class="left_label_area">
                    &nbsp;</div>
                <div class="right_control_area">
                    <div class="float_left">
                        <asp:Button ID="btnSaveDepartment" runat="server" Text="Save" OnClientClick="return ValidateDepartment();"
                            CssClass="start_creating_btn rounded_corners5" OnClick="btnSaveDepartment_Click" /></div>
                    <div class="float_left">
                        <asp:Button ID="btnCancelDepartment" runat="server" Text="Cancel" OnClientClick="HideAddNewDepartment(); return false;"
                            CssClass="start_creating_btn rounded_corners5" /></div>
                </div>
                <div class="clearBoth">
                    <asp:HiddenField ID="hfDepartmentId" runat="server" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Panel>
<asp:HiddenField ID="hfDeprtDeleted" runat="server" />
<asp:HiddenField ID="hfDeprtNotDeleted" runat="server" />
<asp:HiddenField ID="hfDeprtIsInuse" runat="server" />
<asp:HiddenField ID="hfDeprtNameExist" runat="server" />
<asp:HiddenField ID="hfDeprtCodeExist" runat="server" />
<script type="text/javascript" language="javascript">

    // Department
    function ShowAddNewDepartment() {
        // Clear the fields first
        $('#<%=hfDepartmentId.ClientID %>').val('');
        $('#<%=txtDepartmentCode.ClientID %>').val('');
        $('#<%=txtDepartmentName.ClientID %>').val('');

        var firstTerritory = $('#<%=ddlTerritories.ClientID %> option:first').val();
        $('#<%=ddlTerritories.ClientID %>').val(firstTerritory);

        $find('mpeDepartment').show();
        $('#<%=txtDepartmentCode.ClientID %>').focus();
    }
    function HideAddNewDepartment() {
        $find('mpeDepartment').hide();
    }
    function EditDepartment(departmentId, departmentCode, departmentName, territory) {
        $('#<%=hfDepartmentId.ClientID %>').val(departmentId);
        $('#<%=txtDepartmentCode.ClientID %>').val(departmentCode);
        $('#<%=txtDepartmentName.ClientID %>').val(departmentName);

        $('#<%=ddlTerritories.ClientID %>').val(territory);
        $find('mpeDepartment').show();
    }
    function ValidateDepartment() {
        if ($('#<%=txtDepartmentName.ClientID %>').val().trim() == '') {
            ShowPopup('Message', 'Please enter department name');
            return false;
        }
        else {
            return true;
        }
    }
</script>
