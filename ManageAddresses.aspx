<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ThemeSite.Master" AutoEventWireup="true"
    CodeBehind="ManageAddresses.aspx.cs" Inherits="Web2Print.UI.ManageAddresses" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Controls/BreadCrumbMenu.ascx" TagName="BreadCrumbMenu" TagPrefix="uc1" %>
<%@ Register Src="~/Controls/MainHeading.ascx" TagName="MainHeading" TagPrefix="uc3" %>
<%@ Register Src="~/Controls/CustomPager.ascx" TagName="CustomPagerControl" TagPrefix="uc8" %>
<%@ Register Src="~/Controls/MyAddress.ascx" TagName="ManageAddressControl" TagPrefix="uc10" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContents" runat="server">
    <style type="text/css">
        .ImgApprove
        {
            width: 32px;
            height: 32px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBanner" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content_area">
        <div class="left_right_padding">
            <uc1:BreadCrumbMenu ID="BreadCumbMenuCategory" runat="server" WorkMode="MyAccount"
                MyAccountCurrentPage="My Addresses" MyAccountCurrentPageUrl="ManageAddresses.aspx" />
            <div class="signin_heading_div">
                <asp:Label ID="lblTitle" runat="server" Text="My Addresses" CssClass="sign_in_heading"></asp:Label>
            </div>
            <asp:UpdatePanel ID="updPanelManageAddresses" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <div><%--
                        <div class="PageHtmlLabel">
                            <asp:Label ID="lblPageContents" runat="server" />
                        </div>--%>
                        <div class="Width100Percent">
                            <div class="textAlignRight paddingBottom10px normalTextStyle">
                                <asp:Button ID="btnAddNew" runat="server" Text="Add New" OnClick="btnAddNew_Click"
                                    CssClass="start_creating_btn rounded_corners5" />
                            </div>
                        </div>
                        <%--The Main Container--%>
                        <div class="ProductOrderContainer">
                            <div>
                                <asp:GridView ID="grdViewMyAddresses" DataKeyNames="AddressID" runat="server" AutoGenerateColumns="False"
                                    OnRowCommand="grdViewMyAddresses_RowCommand" OnRowDataBound="grdViewMyAddresses_RowDataBound">
                                    <Columns>
                                        <asp:BoundField HeaderText="Address Title" DataField="AddressName" HeaderStyle-HorizontalAlign="Left" />
                                        <asp:TemplateField HeaderText="Is Default Billing Address?" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Image ID="imgIsDefaultAddress" runat="server" SkinID="imgTickApproveIcon" ToolTip="Default Address"
                                                    Visible="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Is Default Delivery Address?" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Image ID="imgIsDefaultShippingAddress" runat="server" SkinID="imgTickApproveIcon"
                                                    ToolTip="Default Address" Visible="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Button ID="btnAddNewAddress" CommandName="EditAddress" runat="server" Text="Edit"
                                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "AddressID")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <uc8:CustomPagerControl ID="grdViewMyAddressesPager" runat="server" />
                                <div class="Width100Percent">
                                    <div class="textAlignCenter paddingBottom10px normalTextStyle">
                                        <asp:Label ID="lblMessage" runat="server" Text="No Record Found!" />
                                    </div>
                                </div>
                            </div>
                            <div>
                                <%--Modal Popupextender--%>
                                <%--   <asp:UpdatePanel UpdateMode="Conditional" runat="server" ID="updFileUploadPanel">
                <ContentTemplate>--%>
                                <asp:Panel ID="pnlAddressPopup" runat="server" Style="display: none; width: 60%;">
                                    <uc10:ManageAddressControl ID="MyAddressControl" runat="server" />
                                </asp:Panel>
                                <ajaxToolkit:ModalPopupExtender ID="mpeMyAddr" BehaviorID="mpeMyAddr" TargetControlID="hdnTargetCtrlUpload"
                                    PopupControlID="pnlAddressPopup" BackgroundCssClass="ModalPopupBG" runat="server"
                                    Drag="true" DropShadow="false" />
                                <input type="hidden" id="hdnTargetCtrlUpload" runat="server" />
                                <%--       </ContentTemplate>
            </asp:UpdatePanel>--%>
                                <%--end Modal Popupextender--%>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <br />
        <br />
        <br />
    </div>
    <script type="text/javascript" language="javascript">

        function popupAddressHide() {

            $find('mpeMyAddr').hide();
        }

        function popupAddressShow() {

            $find('mpeMyAddr').show();
        }

        

    </script>
</asp:Content>
