<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PersonalInfo.ascx.cs"
    Inherits="Web2Print.UI.Controls.PersonalInfo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<div id="PDetailDiv" class="order_container rounded_corners" runat="server">

    <div class="corpPInfoCnt1 pad17">
        <div class="custom_color confirm_design">
            <asp:Literal ID="ltrlpersonalinfo" runat="server" Text="Personal Details"></asp:Literal>
        </div>
        <br />
        <br />
    </div>
    <div class="corpPInfoCnt2 white_background rounded_corners pad20_corp">
        <table class="tblPersonalInfoCorp" cellspacing="5">
            <tr>
                <td>
                    <asp:Label ID="lblName" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblJobTitle" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Literal ID="ltrlteritory" runat="server" Text="Territory"></asp:Literal>
                </td>
                <td>
                    <asp:Label ID="lblTerritory" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Literal ID="ltrlorderlimit" runat="server" Text="Order Limit"></asp:Literal>
                </td>
                <td>
                    <asp:Label ID="lblOrderLimit" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Literal ID="ltrlloplaced" runat="server" Text="Last order placed"></asp:Literal>
                </td>
                <td>
                    <asp:Label ID="lblLastOrder" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Literal ID="ltrlcplaceorder" runat="server" Text="Can Place Order"></asp:Literal>
                </td>
                <td>
                    <asp:Label ID="lblCanPlaceOrder" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Literal ID="ltrlcanapproveorder" runat="server" Text="Can Approve Orders"></asp:Literal>
                </td>
                <td>
                    <asp:Label ID="lblCanApproveOrders" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div class="corpPInfoCnt3 white_background rounded_corners pad17 memo_message custom_color"
        id="tdcompanyopolicy" runat="server">

        <div class="orederPCnt">
            Ordering Policy Statement
                            <br />
            <asp:TextBox ID="txtPolicy" runat="server" CssClass="rounded_corners5 text_boxPolicy"
                TextMode="MultiLine" TabIndex="11" Enabled="false"></asp:TextBox>
            <br />

            <a id="btnCorporateOrderingPolicy1" runat="server" class="linkOrderingpolicy" onclick="ShowCOPPopUp();">Set corporate ordereing policy
            </a>

            <ajaxToolkit:ModalPopupExtender ID="mpeCorporateOrderingPolicy" runat="server" BackgroundCssClass="ModalPopupBG"
                PopupControlID="pnlCorporateOrderingPolicy" TargetControlID="btnCorporateOrderingPolicy1"
                BehaviorID="mpeCorporateOrderingPolicy" CancelControlID="btnCancelMessageBox"
                DropShadow="false">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="pnlCorporateOrderingPolicy" runat="server" Width="700px" CssClass="FileUploaderPopup rounded_corners"
                ClientIDMode="Static" Style="display: none">
                <div class="Width100Percent">

                    <div class="exit_container">
                        <div id="btnCancelMessageBox" runat="server" class="exit_popup">
                        </div>
                    </div>
                </div>
                <div class="pop_body">
                    <asp:UpdatePanel ID="upnlBody" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            Corporate Ordering Policy
                                    <br />
                            <br />
                            <asp:TextBox ID="txtCorporateOrderingPolicy" runat="server" CssClass="rounded_corners5 text_box"
                                TextMode="MultiLine" Width="550px" Height="80px" TabIndex="11"></asp:TextBox>
                            <br />
                            <br />
                            <div class="address_contol_area1_corp">
                                <div class="float_left">
                                    <asp:Button ID="btnSaveCorporateOrderingPolicy" runat="server" Text="Save" CssClass="start_creating_btn rounded_corners5"
                                        OnClick="btnSaveCorporateOrderingPolicy_Click" />
                                </div>
                                <div class="float_left">
                                    <asp:Button ID="btnCancelCorporateOrderingPolicy" runat="server" Text="Cancel" OnClientClick="HidePnlCorporateOrderingPolicy(); return false;"
                                        CssClass="start_creating_btn rounded_corners5" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </asp:Panel>
            <script type="text/javascript" language="javascript">
                function ShowCOPPopUp() {
                    $find('mpeCorporateOrderingPolicy').show();
                }
                function HidePnlCorporateOrderingPolicy() {


                    $find('mpeCorporateOrderingPolicy').hide();

                }
            </script>
        </div>
        <div class="corporate_logo_small_container">
            <a href='<%=ResolveUrl("~/default.aspx") %>'>
                <asp:Image ID="imgCorpLogo" runat="server" BorderStyle="None" CssClass="corporate_logo_small" /></a>
            <br />
            <asp:Label ID="lblCorporateName" runat="server" CssClass="sign_in_heading" Text="Coporate Name" />
            <br />
        </div>
        <div class="clearBoth">
        </div>
    </div>

    <div class="clearBoth">
    </div>
</div>
