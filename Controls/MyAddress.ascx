<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyAddress.ascx.cs" Inherits="Web2Print.UI.Controls.MyAddress" %>
<%--<div class="popupContainer" style="width:75%;">--%>
<table border="0" align="center" cellpadding="0" cellspacing="0" class="tableDefaultSettings popupTblContainer">
    <tr id="tblheaderRow">
        <td>
            <table class="tableDefaultSettings" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="popupContainerHeader">
                        <asp:Label CssClass="FileUploadHeaderText" ID="lblTitle" runat="server" Text="Add New Address" />
                    </td>
                    <td class="popupContainerHeader textAlignRight">
                        <span class="FileUploadHeaderCloseText" onclick="parent.popupAddressHide();">Close X</span>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="tblInnerContainerRow">
        <td class="tbleInnerContainer">
            <table border="0" cellpadding="2" cellspacing="0" align="center" width="60%">
                <tbody>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Label ID="lblMessage" runat="server" Text="" CssClass="errorMessage" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 33%">
                            <span id="spnaddresstitle" runat="server">Address Title</span>
                        </td>
                        <td style="width: 67%">
                            <input id="txtShipAddressName" type="text" runat="server" class="textBoxCart" />
                            <asp:RequiredFieldValidator ID="rfvShipAddName" ControlToValidate="txtShipAddressName"
                                ValidationGroup="AddressAddUpdGroup" runat="server" CssClass="errorMessage" ErrorMessage="*"
                                ToolTip="Required Field"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span id="spnaddressline1" runat="server">Address line 1</span>
                        </td>
                        <td>
                            <input id="txtShipAddLine1" type="text" runat="server" class="textBoxCart" />
                            <asp:RequiredFieldValidator ID="rfvShipAddressline1" ControlToValidate="txtShipAddLine1"
                                ValidationGroup="AddressAddUpdGroup" runat="server" CssClass="errorMessage" ErrorMessage="*"
                                ToolTip="Required Field"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span id="spnaddressline2" runat="server">Address line 2</span>
                        </td>
                        <td>
                            <input id="txtShipAddressLine2" type="text" runat="server" class="textBoxCart" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span id="spncity" runat="server">City</span>
                        </td>
                        <td>
                            <input id="txtShipAddCity" type="text" runat="server" class="textBoxCart" />
                            <asp:RequiredFieldValidator ID="rfvShipAddCity" ControlToValidate="txtShipAddCity"
                                ValidationGroup="AddressAddUpdGroup" runat="server" CssClass="errorMessage" ErrorMessage="*"
                                ToolTip="Required Field"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span id="spnstate" runat="server">State</span>
                        </td>
                        <td>
                            <input id="txtShipState" type="text" runat="server" class="textBoxCart" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span id="spnzipcodeP" runat="server">Zip Code / Post Code</span>
                        </td>
                        <td>
                            <input id="txtShipPostCode" type="text" runat="server" class="textBoxCart" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span id="spncountry" runat="server">Country</span>
                        </td>
                        <td>
                            <input id="txtShipCountry" type="text" runat="server" class="textBoxCart" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span id="spncontactnum" runat="server">Contact Number</span>
                        </td>
                        <td>
                            <input id="txtShipContact" type="text" runat="server" class="textBoxCart" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkIsDefaultAddr" runat="server" Text="Is Default Billing Address" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkIsDefaulShippingAddr" runat="server" Text="Is Default Delivery Address" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Button Text="Save" ValidationGroup="AddressAddUpdGroup" ID="btnSave" runat="server"
                                OnClick="btnSave_Click" Style="width: 100px" />
                            &nbsp;
                            <input type="button" onclick="parent.popupAddressHide();" style="width: 100px" id="btnCancelPopup"
                                value="Cancel" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </td>
    </tr>
    <tr>
        <td class="ModalFooterRow">
        </td>
    </tr>
</table>
<%--</div>--%>