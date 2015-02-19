<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ThemeSite.Master" AutoEventWireup="true"
    CodeBehind="OrderingPolicy.aspx.cs" Inherits="Web2Print.UI.OrderingPolicy" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Controls/BreadCrumbMenu.ascx" TagName="BreadCrumbMenu" TagPrefix="uc1" %>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content_area ">
        <div class="left_right_padding">
            <uc1:BreadCrumbMenu ID="BreadCumbMenuCategory" runat="server" WorkMode="MyAccount"
                MyAccountCurrentPage="Ordering Policy" MyAccountCurrentPageUrl="OrderingPolicy.aspx" />
            <div class="contact_us_heading">
                <asp:Label ID="lblPageTitle" runat="server" Text="Ordering Policy"></asp:Label>
            </div>
            <div class=" PaddingB50 ">
                <div class="order_container rounded_corners">
                    <table width="100%" cellpadding="5" cellspacing="5">
                        
                        <tr>
                            <%-- <td width="19%" align="left" valign="top" class="pad20">
                    <div class="custom_color confirm_design">
                        <asp:Literal ID="ltrlorderpolicy" runat="server" Text="Ordering Policy"></asp:Literal>
                    </div>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                </td>--%>
                            <td width="80%" align="left" valign="top" class="white_background rounded_corners pad20 fontSyleBold">
                                <div id="Div1" class="Width100Percent" runat="server">
                                    <div class="textAlignLeft paddingLeft10px paddingBottom10px product_detail_sub_heading custom_color paddingTop15px">
                                        <asp:Label ID="Label2" runat="server" Text="Change and control order processing behind the scenes:" />
                                    </div>
                                </div>
                                <div class="float_left">
                                    <asp:CheckBox ID="chkPORequired" runat="server" Text="&nbsp;&nbsp;&nbsp;PO Number is required when ordering" />
                                    <br />
                                    <br />
                                    <asp:Button ID="btnApplySetting" runat="server" Text="Apply Setting" CssClass="start_creating_btn rounded_corners5"
                                        OnClick="btnApplySetting_Click" />
                                    <ajaxToolkit:ModalPopupExtender ID="mpeCorporateOrderingPolicy" runat="server" BackgroundCssClass="ModalPopupBG"
                                        PopupControlID="pnlCorporateOrderingPolicy" TargetControlID="btnCorporateOrderingPolicy1"
                                        BehaviorID="mpeCorporateOrderingPolicy" CancelControlID="btnCancelMessageBox"
                                        DropShadow="false">
                                    </ajaxToolkit:ModalPopupExtender>
                                    <asp:Panel ID="pnlCorporateOrderingPolicy" runat="server" Width="700px" CssClass="FileUploaderPopup rounded_corners"
                                        ClientIDMode="Static" Style="display: none">
                                        <div class="Width100Percent">
                                            <div class="float_left">
                                                <asp:Label ID="lblHeader" runat="server" CssClass="FileUploadHeaderText"></asp:Label>
                                            </div>
                                            <div class="exit_container">
                                                <div id="btnCancelMessageBox" runat="server" class="exit_popup">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="pop_body">
                                            <asp:UpdatePanel ID="upnlBody" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    Ordering Policy
                                                    <br />
                                                    <br />
                                                    <asp:TextBox ID="txtCorporateOrderingPolicy" runat="server" CssClass="rounded_corners5 text_box"
                                                        TextMode="MultiLine" Width="550px" Height="80px" TabIndex="11"></asp:TextBox>
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
                                </div>
                                <div class="float_right">
                                    <asp:LinkButton runat="server" Text="Set Store Ordering Policy" ID="btnCorporateOrderingPolicy1"></asp:LinkButton>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" language="javascript">
        function HidePnlCorporateOrderingPolicy() {
            $find('mpeCorporateOrderingPolicy').hide();
        }
    </script>
</asp:Content>
<%--<asp:Content ID="Content6" ContentPlaceHolderID="cphFooter" runat="server">
</asp:Content>--%>
