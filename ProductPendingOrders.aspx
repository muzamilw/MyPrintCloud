<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ThemeSite.Master" AutoEventWireup="true"
    CodeBehind="ProductPendingOrders.aspx.cs" Inherits="Web2Print.UI.ProductPendingOrders" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Controls/BreadCrumbMenu.ascx" TagName="BreadCrumbMenu" TagPrefix="uc1" %>
<%@ Register Src="~/Controls/MainHeading.ascx" TagName="MainHeading" TagPrefix="uc3" %>
<%@ Register Src="~/Controls/QuickLinks.ascx" TagName="QuickLinks" TagPrefix="uc5" %>
<%@ Register Src="~/Controls/CustomPager.ascx" TagName="CustomPagerControl" TagPrefix="uc8" %>
<%@ Register Src="~/Controls/OrderDetails.ascx" TagName="OrderDetailsControl" TagPrefix="uc9" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContents" runat="server">
    <script src="Scripts/utilities.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div id="divShd" class="opaqueLayer">
    </div>
    <div class="content_area container">
        <div class="left_right_padding row">
             <div class="signin_heading_div float_left_simple dashboard_heading_signin">
               <asp:Label ID="lblTitle" runat="server" Text="Pending Orders For Approval" CssClass="sign_in_heading"></asp:Label></div>
            
             <div class="dashBoardRetrunLink">
                      <uc1:BreadCrumbMenu ID="BreadCumbMenuCategory" runat="server" WorkMode="MyAccount"
                MyAccountCurrentPage="Pending Orders" MyAccountCurrentPageUrl="ProductPendingOrders.aspx" />
                </div>
            <div class="clearBoth">

            </div>
            
          
            <div class="dashboard_heading_signin">
                
            <div class="white_back_div rounded_corners">
                <div class="Width100Percent">
                    <div class="textAlignCenter paddingBottom10px normalTextStyle">
                        <asp:Label ID="lblMessage" runat="server" Text="No Pending Orders" />
                    </div>
                </div>
                <%--The Main Container--%>
                <div class="ProductOrderContainer">
                    <div>
                        <asp:GridView ID="grdViewPendingOrders" DataKeyNames="OrderID" runat="server" AutoGenerateColumns="False"
                            OnRowCommand="grdViewPendingOrders_RowCommand" OnRowCreated="grdViewPendingOrders_RowCreated"
                            OnRowDataBound="grdViewPendingOrders_RowDataBound">
                            <Columns>
                                <asp:BoundField HeaderText="Order Number" DataField="OrderCode" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Your PO reference" DataField="YourRef" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="10%" />
                                <asp:BoundField HeaderText="Order Date" DataField="OrderDate" DataFormatString="{0:MMMM d, yyyy}"
                                    HtmlEncode="false" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Delivery Date" DataField="DeliveryDate" DataFormatString="{0:MMMM d, yyyy}"
                                    HtmlEncode="false" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Status" DataField="StatusName" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Order Created By" DataField="CustomerName" HeaderStyle-HorizontalAlign="Left" />
                                <asp:TemplateField HeaderText="Action(s)" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <div class="textLeftFloating paddingRight3px">
                                            <asp:ImageButton ID="lnkBtnViewOrderDetails" runat="server" CommandName="ViewOrderDetails" CssClass="rounded_corners" ImageUrl="~/images/View-Detail-icon.png" ToolTip="Click to view order details" Height="28" Width="28"
                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "OrderID")%>' Text="View" /></div>
                                        <div class="textLeftFloating">
                                            <asp:ImageButton Visible="false" ID="lnkBtnApproverOrder" runat="server" CommandName="ApproveOrder" CssClass="rounded_corners" ImageUrl="~/images/accept.png" Height="28" Width="28"
                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "OrderID")%>' Text="Approve"
                                                ToolTip="Click To Approve" />
                                            <asp:ImageButton Visible="false" ID="lnkBtnRejectOrder" OnClientClick="return confirmApproverAction(false, '');"  CssClass="rounded_corners" ImageUrl="~/images/reject.png" Height="28" Width="28"
                                                runat="server" CommandName="RejectOrder" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "OrderID")%>'
                                                Text="Reject" ToolTip="Click To Reject" />
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <uc8:CustomPagerControl ID="grdViewPendingOrdersPager" runat="server" />
                    </div>
                    <div>
                        <asp:Panel ID="pnlOrderDetails" runat="server" CssClass="FileUploaderPopup_Mesgbox LCLB rounded_corners"
                            Style="display: none; width: 920px;">
                            <div class="white_background paddingBottom10px paddingTop5px">
                                <uc9:OrderDetailsControl ID="orderDetailsControl" runat="server" />
                            </div>
                        </asp:Panel>
                        <ajaxToolkit:ModalPopupExtender ID="mpeOrderDetails" BehaviorID="mpeOrderDetails"
                            TargetControlID="hdnTargetCtrlUpload" PopupControlID="pnlOrderDetails" BackgroundCssClass="ModalPopupBG"
                            runat="server" Drag="true" DropShadow="false" />
                        <input type="hidden" id="hdnTargetCtrlUpload" runat="server" />
                    </div>
                    <div>
                        <ajaxToolkit:ModalPopupExtender ID="mpePurchaseOrder" BehaviorID="mpePurchaseOrder"
                            TargetControlID="hfTargetPO" PopupControlID="pnlPurchaseOrder" BackgroundCssClass="ModalPopupBG"
                            runat="server" Drag="true" DropShadow="false" CancelControlID="btnCancel" />
                        <input type="hidden" id="hfTargetPO" runat="server" />
                        <asp:Panel ID="pnlPurchaseOrder" runat="server" CssClass="FileUploaderPopup_Mesgbox LCLB rounded_corners"
                            Style="display: none; width: 400px;">
                            <div style="background: white; padding-top: 5px; padding-bottom: 5px;" class="white_background">
                                <div class="Pad5px">
                                    <asp:Label ID="Label1" runat="server" Text="Alert" CssClass="sign_in_heading"></asp:Label>
                                </div>
                                <div class="TLR">
                                    <asp:Label ID="lblPO" runat="server" Text="Please enter PO:"></asp:Label>
                                </div>
                                <div class="TTL">
                                    <asp:TextBox ID="txtPO" runat="server" CssClass="text_box200 rounded_corners5"></asp:TextBox>
                                </div>
                                <div class="clearBoth">
                                    &nbsp;
                                </div>
                                <div>
                                    <asp:Button ID="btnSave" runat="server" CssClass="start_creating_btn rounded_corners5 H4B Mlft90px"
                                        Text="Save" Width="100px" OnClientClick="return CheckifEmpty();" OnClick="btnSave_click" />&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnCancel" runat="server" CssClass="start_creating_btn rounded_corners5 H4B"
                                        Text="Cancel" Width="100px" />
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
                </div>
        </div>
        <asp:HiddenField ID="hfOrderIDTOApprove" runat="server" />
    </div>
    <br />
    <br />
    <br />
    <asp:Label ID="lblOrderNumHeadingTxt" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblPOHeadingTxt" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblOrderDateHeadingTxt" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblDelivDateHeadingTxt" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblStatusHeadingTxt" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblOdrCreatedByHeadingTxt" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblActionsheaderTxt" runat="server" Visible="false"></asp:Label>
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {
            $("#<%= grdViewPendingOrders.ClientID%> tr").children(":first").css("border-top-left-radius", 10, "-moz-border-top-left-radius", 10,
             "-webkit-border-top-left-radius", 10, "-khtml-border-top-left-radius", 10);

            $("#<%= grdViewPendingOrders.ClientID%> tr").children(":first").next().next().next().next().next().next().css("border-top-right-radius", 10, "-moz-border-top-right-radius", 10,
              "-webkit-border-top-right-radius", 10, "-khtml-border-top-right-radius", 10);

        });

        function popupHide() {

            $find('mpeOrderDetails').hide();
        }


        function confirmApproverAction(isApprove, orderId) {
            var Approveresult = null;
            var Rejectresult = null;

            if (isApprove) {
                Approveresult = confirm('Are you sure you want to approve order?');
                $('#<%=hfOrderIDTOApprove.ClientID %>').val(orderId);
                $find('mpePurchaseOrder').show();
                return false;
            }
            else {
                Rejectresult = confirm('Are you sure you want to reject order?');
                if (Rejectresult == false) {
                    return false;
                }
                else {
                    return true;
                }

            }
        }

        function CheckifEmpty() {
            if ($('#<%=txtPO.ClientID %>').val().trim() == '') {
                ShowPopup('Message', 'Please enter PO to proceed.');
                return false;
            }
            else {
                return true;
                showProgress();
            }
        }
    </script>
</asp:Content>
