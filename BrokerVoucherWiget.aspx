<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ThemeSite.Master" AutoEventWireup="true"
    CodeBehind="BrokerVoucherWiget.aspx.cs" Inherits="Web2Print.UI.BrokerVoucherWiget" %>

<%@ Register Src="~/Controls/BreadCrumbMenu.ascx" TagName="BreadCrumbMenu" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="VoucherWiget" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content_area">
        <div class="left_right_padding">
            <div class="signin_heading_div">
                <asp:Label ID="lblVouchersHead" runat="server" Text="Store Vouchers" CssClass="sign_in_heading"></asp:Label>
                <div class="dashBoardRetrunLink">
                    <uc1:BreadCrumbMenu ID="BreadCumbMenuCategory" runat="server" WorkMode="MyAccount"
                        MyAccountCurrentPage="Store Banners" MyAccountCurrentPageUrl="BrokerVoucherWiget.aspx" />
                </div>
            </div>
            <div class="cursor_pointer" onclick="AddNewPostBackForUser();">
                <div class="float_left">
                    <asp:ImageButton ID="imgAddNew" CssClass="add_new" ToolTip="Add New Voucher" runat="server"
                        ImageUrl="~/images/AddNew.png" OnClick="imgAddNew_Click" />
                </div>
                <div class="new_caption">
                    <asp:Literal ID="ltrlnew" runat="server" Text="New Voucher Code"></asp:Literal>
                </div>
                <div class="clearBoth">
                </div>
            </div>
            <div class="Width100Percent">
                <div class="divSearchBar paddingBottom10px normalTextStyle rounded_corners">
                    <div style="float: left; padding: 45px 23px 23px 23px;" class="heading_h8">
                        <asp:Literal ID="ltrlSearchOrder" runat="server" Text=" Search Orders"></asp:Literal>
                    </div>
                    <div>
                        <table border="0" cellpadding="5" cellspacing="5" width="85%" style="text-align: left;
                            color: White;">
                            <tr>
                                <td>
                                    <span id="spntdfromdate" runat="server" class="heading_h8">From Date</span>
                                </td>
                                <td>
                                    <span id="spntodate" runat="server" class="heading_h8">To Date</span>
                                </td>
                                <td>
                                    <span id="spntdorderref" runat="server" class="heading_h8">Voucher Name</span>
                                </td>
                                <td>
                                    <span id="spntdorderstatus" runat="server" class="heading_h8">Voucher Status</span>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtValidFromDate" MaxLength="20" CssClass="text_box150 rounded_corners5"
                                        runat="server" Width="100px" />
                                    <ajaxToolkit:CalendarExtender TargetControlID="txtValidFromDate" Format="MM/dd/yyyy"
                                        ID="fromDatePickerExtender" BehaviorID="fromDatePickerExtender" runat="server" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtValidUpToDate" MaxLength="20" CssClass="text_box150 rounded_corners5"
                                        runat="server" Width="100px" />
                                    <ajaxToolkit:CalendarExtender TargetControlID="txtValidUpToDate" Format="MM/dd/yyyy"
                                        ID="toDatePickerExtender" BehaviorID="toDatePickerExtender" runat="server" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtVCode" MaxLength="20" CssClass="text_box150 rounded_corners5"
                                        runat="server" Width="100px" />
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlVoucherStatuses" runat="server" CssClass="dropdown rounded_corners5"
                                        Width="160px" AutoPostBack="true" OnSelectedIndexChanged="ddlVoucherStatuses_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <div class="float_left_simple">
                                        <asp:Button ID="btnSearchVouch" runat="server" Text="Go" CssClass="start_creating_btn rounded_corners5"
                                            Width="90px" OnClick="btnSearchVouch_Click" />
                                    </div>
                                    <div class="float_left_simple left_margin">
                                        <asp:Button ID="btnClear" runat="server" Text="Reset" CssClass="start_creating_btn rounded_corners5"
                                            Width="90px" /></div>
                                    <div class="clearBoth">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="white_back_div rounded_corners">
                    <div class="Pad1020">
                        <div class="Width100Percent">
                            <div class="textAlignCenter paddingBottom10px normalTextStyle">
                                <asp:Label ID="lblMessage" runat="server" Visible="false" />
                            </div>
                        </div>
                        <%--The Main Container--%>
                        <div class="ProductOrderContainer">
                            <asp:GridView ID="grdViewVouchers" SkinID="ProductOrderHistoryGrid" runat="server"
                                OnRowCreated="grid_RowCreated" DataKeyNames="DiscountVoucherID" AutoGenerateColumns="False"
                                OnRowCommand="grdViewVouchers_RowCommand" OnRowDataBound="grdViewVouchers_RowDataBound"
                                OnRowEditing="grdViewVouchers_RowEditing">
                                <Columns>
                                    <asp:TemplateField HeaderText="Voucher Code" HeaderStyle-HorizontalAlign="Left" HeaderStyle-CssClass="paddingLeft20px LGBC ">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkH" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "VoucherCode")%>'
                                                ToolTip="Click To Edit Discount Voucher" CommandName="EditRecord" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DiscountVoucherID")%>'
                                                CssClass="paddingLeft20px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Discount Rate" HeaderStyle-HorizontalAlign="Left" HeaderStyle-CssClass="LGBC ">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblVoucherRate" runat="server" ToolTip="Click To Edit Discount Voucher"
                                                CommandName="EditRecord" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DiscountVoucherID")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Valid From" DataField="ValidFromDate" DataFormatString="{0:MMMM d, yyyy}"
                                        HtmlEncode="false" HeaderStyle-HorizontalAlign="Left" HeaderStyle-CssClass="LGBC " />
                                    <asp:BoundField HeaderText="Valid To" DataField="ValidUptoDate" DataFormatString="{0:MMMM d, yyyy}"
                                        HtmlEncode="false" HeaderStyle-HorizontalAlign="Left" HeaderStyle-CssClass="LGBC " />
                                    <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Left" HeaderStyle-CssClass="LGBC ">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblVoucherStatus" runat="server" ToolTip="Click To Edit Discount Voucher"
                                                CommandName="EditRecord" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DiscountVoucherID")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <div>
                                <%--Modal Popupextender--%>
                                <ajaxToolkit:ModalPopupExtender ID="mpeVoucherDetails" BehaviorID="mpeVoucherDetails"
                                    CancelControlID="btnCancel" TargetControlID="hdnTargetCtrlUpload" PopupControlID="pnlVoucherDetails"
                                    BackgroundCssClass="ModalPopupBG" runat="server" Drag="true" DropShadow="false" />
                                <input type="hidden" id="hdnTargetCtrlUpload" runat="server" />
                                <asp:Panel ID="pnlVoucherDetails" runat="server" CssClass="FileUploaderPopup_Mesgbox LCLB rounded_corners"
                                    Style="display: none; width: 700px;">
                                    <div style="background: white; padding-top: 15px; padding-bottom: 15px; padding-left: 15px;
                                        padding-right: 15px;">
                                        <div class="signin_heading_div">
                                            <asp:Label ID="lblUpdateV" runat="server" Text="Update Voucher" CssClass="sign_in_heading"></asp:Label>
                                        </div>
                                        <div class="Bottom-doted-Cs">
                                            <br />
                                        </div>
                                        <asp:UpdatePanel ID="panelBody" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <table style="margin-left: 40px; padding-bottom: 5px;">
                                                    <tr>
                                                        <td align="right" valign="top">
                                                            <asp:Label ID="lblVocherCode" runat="server" Text="Voucher Code" class="left_label_area"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="bottom">
                                                            <asp:TextBox ID="txtVoucherName" runat="server" CssClass="text_box200 rounded_corners5 left_margin"></asp:TextBox>
                                                            <asp:Label ID="lblVoucherName" runat="server" CssClass="left_margin"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" valign="top">
                                                            <asp:Label ID="lblVoucherRate" runat="server" Text="Discount applied to shopping cart ( excluding delivery and tax)"
                                                                class="left_label_area"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="bottom">
                                                            <asp:TextBox ID="txtVRate" runat="server" CssClass="text_box200 rounded_corners5 left_margin"
                                                                Style="width: 50px"></asp:TextBox>
                                                            <asp:Label ID="lblPer" runat="server" Text="%"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" valign="top">
                                                            <asp:Label ID="lblFromValidD" runat="server" Text="Valid From Date" class="left_label_area"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="bottom">
                                                            <asp:TextBox ID="txtValidFrom" MaxLength="20" CssClass="text_box200 rounded_corners5 left_margin right_control_area"
                                                                runat="server" />
                                                            <ajaxToolkit:CalendarExtender TargetControlID="txtValidFrom" Format="MM/dd/yyyy"
                                                                ID="fromDatePE" BehaviorID="fromDatePE" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" valign="top">
                                                            <asp:Label ID="lblToValidDate" runat="server" Text="Valid To Date" class="left_label_area"></asp:Label>
                                                        </td>
                                                        <td align="left" valign="bottom">
                                                            <asp:TextBox ID="txtValidTo" MaxLength="20" CssClass="text_box200 rounded_corners5 left_margin right_control_area"
                                                                runat="server" />
                                                            <ajaxToolkit:CalendarExtender TargetControlID="txtValidTo" Format="MM/dd/yyyy" ID="ToDatePE"
                                                                BehaviorID="ToDatePE" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                        </td>
                                                        <td align="left">
                                                            <asp:CheckBox ID="checkBox" runat="server" CssClass="left_margin" />
                                                            <asp:Label ID="lblIsEnabled" runat="server" Text="Active"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                        </td>
                                                        <td align="left">
                                                            <asp:Button ID="btnSave" runat="server" CssClass="start_creating_btn rounded_corners5 left_margin"
                                                                Text="Save" OnClientClick="return CheckValues();" OnClick="btnSave_Click" Width="100px" />
                                                            <asp:Button ID="btnCancel" runat="server" CssClass="start_creating_btn rounded_corners5 H4B"
                                                                Text="Cancel" Width="100px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="clear"></div>
    <br />
    <br />
    <br />    <br />
    <asp:HiddenField ID="HfVID" runat="server" Value="0" />
    <script type="text/javascript">
        function AddNewPostBackForUser() {
            __doPostBack('<%=imgAddNew.UniqueID %>', '');
        }

        function CheckValues() {
            var Discount = $('#<%=txtVRate.ClientID %>').val();
            if (isNaN(Discount) === true) {
                ShowPopup('Message', "Please enter numeric value only.");
                return false;
            }
            else {
                return true;
            }
        }
    </script>
</asp:Content>
