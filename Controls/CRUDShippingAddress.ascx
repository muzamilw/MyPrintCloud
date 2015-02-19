<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CRUDShippingAddress.ascx.cs"
    Inherits="Web2Print.UI.Controls.CRUDShippingAddress" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<div class="cursor_pointer" onclick="AddNewPostBack();">
    <div class="float_left">
        <asp:ImageButton ID="imgAddNew" CssClass="add_new" ToolTip="Add New Address" runat="server"
            ImageUrl="~/images/AddNew.png" OnClick="imgAddNew_Click" />
    </div>
    <div class="new_caption">
        <asp:Literal ID="ltrlnewShippingAddress" runat="server" Text="New Shipping Address"></asp:Literal>
    </div>
    <div class="clearBoth">
    </div>
</div>
<div class="divSearchBar_corp normalTextStyle usermanager_padding_header rounded_corners "
    style="padding: 15px 15px 15px 15px;">
    <table style="width: auto; border-collapse: separate; border-spacing: 2px;">
        <tr>
            <td>
                <asp:Literal ID="ltrlSearchrecords" runat="server" Text=" Search Address"></asp:Literal>
            </td>
            <td style="padding-left: 10px;">
                <asp:TextBox ID="txtsearch" runat="server" Width="400px" CssClass="text_box150 rounded_corners5"></asp:TextBox>
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
<div class="white_background rounded_corners textAlignLeft">
    <div class="normalTextStyle territory_row_padding_header">
        <div class="address_title_corp_div checked_design ">
            <asp:Literal ID="ltrladdressstitle" runat="server" Text="Address Title"></asp:Literal>
        </div>
        <div class="address_detil_corp_div checked_design" id="divaddressdeatails" runat="server">
            <asp:Literal ID="ltrladdressdeatails" runat="server" Text="Address Details"></asp:Literal>
        </div>
        <div class="main_contact_corp_div checked_design" id="divmaincontent" runat="server">
            <asp:Literal ID="ltrlmaincontent" runat="server" Text="Main Contact"></asp:Literal>
        </div>
    </div>
    <div class="border_bottom_line">
        &nbsp;
    </div>
    <asp:UpdatePanel ID="upnlShippingAddress" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Repeater ID="rptrShippingAddress" runat="server" OnItemDataBound="rptrShippingAddress_ItemDataBound"
                OnItemCommand="rptrShippingAddress_ItemCommand">
                <ItemTemplate>
                    <div class="territory_row_padding" title="click to edit address">
                        <div id="divContainer" runat="server" class="cursor_pointer">
                            <div class="address_title_corp_div">
                                <%# Eval("AddressName")%>&nbsp;
                            </div>
                            <div class="address_detil_corp_div ">
                                <%# Eval("Address1")%>&nbsp;
                            </div>
                            <div class="main_contact_corp_div">
                                <asp:Label ID="lblDefaultAddress" runat="server"></asp:Label>&nbsp;
                            </div>
                        </div>
                        <div class="float_right">
                            <%--commented by Naveed on 08/12/2014--%>
                            <asp:ImageButton ID="btnDelete" runat="server" OnClientClick="return confirm('Are you sure you want to delete this address?');" style="display:none;"
                                ToolTip="Delete Address" ImageUrl="~/images/delete.png" Height="28" Width="28" CommandName="DeleteRecord" CommandArgument='<%# Eval("AddressID") %>' />
                        </div>
                    </div>
                    <div class="clearBoth">
                    </div>
                    <div class="border_bottom_line">
                        &nbsp;
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <asp:Button ID="btnEditRecord" runat="server" Text="Edit" Style="display: none;"
                OnClick="btnEditRecord_Click" />
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSaveShippingAddress" EventName="Click" />
        </Triggers>
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
<asp:Button ID="btnAddressPopup" runat="server" Style="display: none; width: 0px; height: 0px" />
<ajaxToolkit:ModalPopupExtender ID="mpeAddress" runat="server" BackgroundCssClass="ModalPopupBG"
    PopupControlID="pnlAddress" TargetControlID="btnAddressPopup" BehaviorID="mpeAddress"
    CancelControlID="btnCancelShippingAddress" DropShadow="false">
</ajaxToolkit:ModalPopupExtender>
<asp:Panel ID="pnlAddress" runat="server" Width="730px" Height="405px" CssClass="FileUploaderPopup_Mesgbox_Shipp LCLB rounded_corners"
    Style="display: block">
    <div class="Width100Percent">
        <div class="float_left">
            <asp:Label ID="lblHeader" runat="server" CssClass="FileUploadHeaderText"></asp:Label>
        </div>
    </div>
    <div class="pop_body_MesgPopUp" style="padding: 25px;">
        <asp:UpdatePanel ID="upnlBody" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="address_label_area1 ">
                    <asp:Literal runat="server" ID="ltrlAddressName" Text="Address Name:"></asp:Literal>
                </div>
                <div class="address_contol_area1">
                    <asp:TextBox ID="txtAddressName" runat="server" CssClass="text_box200 rounded_corners5"
                        MaxLength="100"></asp:TextBox>
                </div>
                <div class="address_label_area2">
                    <asp:Literal runat="server" ID="ltrlFAO" Text="FAO:"></asp:Literal>
                </div>
                <div class="address_contol_area2">
                    <asp:TextBox ID="txtFAO" runat="server" CssClass="text_box200 rounded_corners5" MaxLength="50"></asp:TextBox>
                </div>
                <div class="clearBoth">
                    &nbsp;
                </div>
                <div class="address_label_area1 ">
                    <asp:Literal ID="literalAddress1" runat="server" Text="Address 1:"></asp:Literal>
                </div>
                <div class="address_contol_area1">
                    <asp:TextBox ID="txtAddress1" runat="server" CssClass="text_box200 rounded_corners5"
                        MaxLength="250"></asp:TextBox>
                </div>
                <div class="address_label_area2">
                    <asp:Literal ID="ltlReference" Text="Reference:" runat="server"></asp:Literal>
                </div>
                <div class="address_contol_area2">
                    <asp:TextBox ID="txtReference" runat="server" CssClass="text_box200 rounded_corners5"
                        MaxLength="50"></asp:TextBox>
                </div>
                <div class="clearBoth">
                    &nbsp;
                </div>
                <div class="address_label_area1">
                    <asp:Literal runat="server" ID="ltrlAddress2" Text="Address 2:"></asp:Literal>
                </div>
                <div class="address_contol_area1">
                    <asp:TextBox ID="txtAddress2" runat="server" CssClass="text_box200 rounded_corners5"
                        MaxLength="250"></asp:TextBox>
                </div>
                <div class="address_label_area2">
                    <asp:Literal ID="literalTel1" runat="server" Text="Tel 1:"></asp:Literal>
                </div>
                <div class="address_contol_area2">
                    <div class="float_left">
                        <asp:TextBox ID="txtTel1" runat="server" Width="100" CssClass="text_box rounded_corners5"
                            MaxLength="30"></asp:TextBox>
                    </div>
                    <div class="label_area_ext">
                        <asp:Literal runat="server" ID="literalExt1" Text="Ext 1:"></asp:Literal>
                    </div>
                    <div class="float_left">
                        <asp:TextBox ID="txtExt1" runat="server" Width="40" CssClass="text_box rounded_corners5"
                            MaxLength="7"></asp:TextBox>
                    </div>
                </div>
                <div class="clearBoth">
                    &nbsp;
                </div>
                <div class="address_label_area1">
                    <asp:Literal runat="server" ID="ltrlAddress3" Text="Address 3:"></asp:Literal>

                </div>
                <div class="address_contol_area1">
                    <asp:TextBox ID="txtAddress3" runat="server" CssClass="text_box200 rounded_corners5"
                        MaxLength="250"></asp:TextBox>
                </div>
                <div class="address_label_area2">
                    <asp:Literal ID="ltrlTel2" runat="server" Text="Tel 2:"></asp:Literal>

                </div>
                <div class="address_contol_area2">
                    <div class="float_left">
                        <asp:TextBox ID="txtTel2" runat="server" Width="100" CssClass="text_box rounded_corners5"
                            MaxLength="30"></asp:TextBox>
                    </div>
                    <div class="label_area_ext">
                        <asp:Literal ID="LiteralExt2" runat="server" Text="Ext 2:"></asp:Literal>
                    </div>
                    <div class="float_left">
                        <asp:TextBox ID="txtExt2" runat="server" Width="40" CssClass="text_box rounded_corners5"
                            MaxLength="7"></asp:TextBox>
                    </div>
                </div>
                <div class="clearBoth">
                    &nbsp;
                </div>
                <div class="address_label_area1 ">
                    <asp:Literal ID="LiteralCity" runat="server" Text="City:"></asp:Literal>

                </div>
                <div class="address_contol_area1">
                    <asp:TextBox ID="txtCity" runat="server" CssClass="text_box200 rounded_corners5"
                        MaxLength="100"></asp:TextBox>
                </div>
                <div class="address_label_area2">

                    <asp:Literal ID="LiteralFax" runat="server" Text="Fax:"></asp:Literal>
                </div>
                <div class="address_contol_area2">
                    <asp:TextBox ID="txtFax" runat="server" CssClass="text_box200 rounded_corners5"
                        MaxLength="30"></asp:TextBox>
                </div>
                <div class="clearBoth">
                    &nbsp;
                </div>
                <div class="address_label_area1">

                    <asp:Literal ID="LiteralCountry" runat="server" Text="Country:"></asp:Literal>
                </div>
                <div class="address_contol_area1">
                   

                    <asp:DropDownList ID="Country" runat="server" CssClass="text_box200 rounded_corners5" OnSelectedIndexChanged="Country_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>

                </div>
                <div class="address_label_area2">

                    <asp:Literal ID="Literal1" runat="server" Text="Latitude:"></asp:Literal>
                </div>
                <div class="address_contol_area2">
                    <asp:TextBox ID="txtLatitude" runat="server" CssClass="text_box200 rounded_corners5"
                        MaxLength="100"></asp:TextBox>
                </div>
                <div class="clearBoth">
                    &nbsp;
                </div>
                <div class="address_label_area1">

                    <asp:Literal ID="LiteralState" runat="server" Text="State:"></asp:Literal>
                </div>
                <div class="address_contol_area1">
                    <asp:DropDownList ID="StateDropdown" runat="server" CssClass="text_box200 rounded_corners5"></asp:DropDownList>

                </div>
                
                <div class="address_label_area2">

                    <asp:Literal ID="ltrlLongitude" runat="server" Text="Longitude:"></asp:Literal>
                </div>
                <div class="address_contol_area2">
                    <asp:TextBox ID="txtLongitude" runat="server" CssClass="text_box200 rounded_corners5"
                        MaxLength="100"></asp:TextBox>
                </div>
                <div class="clearBoth">
                    &nbsp;
                </div>
                <div class="address_label_area1">
                    <asp:Literal ID="LiteralPostalCode" runat="server" Text="Postal Code:"></asp:Literal>

                </div>
                <div class="address_contol_area1">
                    <asp:TextBox ID="txtPostalCode" runat="server" CssClass="text_box200 rounded_corners5"
                        MaxLength="30"></asp:TextBox>
                </div>
                <div class="address_label_area2">
                    <asp:Literal ID="ltrTerritory" runat="server" Text="Territory:"></asp:Literal>
                </div>
                <div class="address_contol_area2">

                    <asp:DropDownList ID="ddlTerritory" runat="server" CssClass="rounded_corners5 dropdown"
                        Width="210px" TabIndex="9">
                    </asp:DropDownList>
                </div>



                <div class="clearBoth">
                    &nbsp;
                </div>
                <div class="address_label_area1">
                </div>
                <div class="address_contol_area1_corp">

                    <asp:Button ID="btnSaveShippingAddress" runat="server" Text="Save" OnClientClick="return ValidateShippingAddress();"
                        CssClass="start_creating_btn rounded_corners5" OnClick="btnSaveShippingAddress_Click" Style="float: left;" />

                    <asp:Button ID="btnCancelShippingAddress" runat="server" Text="Cancel" OnClientClick="HideAddNewShippingAddress(); return false;"
                        CssClass="start_creating_btn rounded_corners5" Style="float: left; margin-left: 5px;" />
                </div>
                <div class="clearBoth">
                    <asp:HiddenField ID="hfAddressId" runat="server" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Panel>
<asp:HiddenField ID="hfAddressDeleted" runat="server" />
<asp:HiddenField ID="hfAddressNotDeleted" runat="server" />
<asp:HiddenField ID="hfAddressIsInuse" runat="server" />
<asp:HiddenField ID="hfAddressNameExist" runat="server" />
<script type="text/javascript" language="javascript">

    // Shipping Address
    function ShowAddNewShippingAddress() {
        $find('mpeAddress').show();
    }
    function HideAddNewShippingAddress() {
        $find('mpeAddress').hide();
    }
    function EditAddress(addressId) {
        $('#<%=hfAddressId.ClientID %>').val(addressId);
        __doPostBack('<%=btnEditRecord.UniqueID %>', '');
    }
    function ValidateShippingAddress() {
        if ($('#<%=txtAddressName.ClientID %>').val().trim() == '') {
            var Plxenteraddressname = "<%= Resources.MyResource.Plxenteraddressname %>";
            ShowPopup('Message', Plxenteraddressname);
            return false;
        }
        else if ($('#<%=txtAddress1.ClientID %>').val().trim() == '') {
            var Plxenteraddress1 = "<%= Resources.MyResource.Plxenteraddress1 %>";
            ShowPopup('Message', Plxenteraddress1);
            return false;
        }
        else if ($('#<%=txtCity.ClientID %>').val().trim() == '') {
            var Plxentercity = "<%= Resources.MyResource.Plxentercity %>";
            ShowPopup('Message', Plxentercity);
            return false;
        }
        else {
            return true;
        }
}
function AddNewPostBack() {
    __doPostBack('<%=imgAddNew.UniqueID %>', '');
    }
</script>
