<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ThemeSite.Master" AutoEventWireup="true"
    CodeBehind="Orders.aspx.cs" Inherits="Web2Print.UI.Orders" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Controls/BreadCrumbMenu.ascx" TagName="BreadCrumbMenu" TagPrefix="uc1" %>
<%@ Register Src="~/Controls/MainHeading.ascx" TagName="MainHeading" TagPrefix="uc3" %>
<%@ Register Src="~/Controls/QuickLinks.ascx" TagName="QuickLinks" TagPrefix="uc5" %>
<%@ Register Src="~/Controls/CustomPager.ascx" TagName="CustomPagerControl" TagPrefix="uc8" %>
<%@ Register Src="~/Controls/OrderDetails.ascx" TagName="OrderDetailsControl" TagPrefix="uc9" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContents" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBanner" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content_area container">
        <div class="left_right_padding row">
           
            <div class="signin_heading_div float_left_simple dashboard_heading_signin">
                <asp:Label ID="HistryBtn" runat="server" Text="Order History" CssClass="sign_in_heading"></asp:Label>
            </div>
               <div class="dashBoardRetrunLink">
                    <uc1:BreadCrumbMenu ID="BreadCrumbMenu1" runat="server" WorkMode="MyAccount"
                         MyAccountCurrentPage="My Order History" MyAccountCurrentPageUrl="ProductsOrdersHistory.aspx"  />
                </div>
            <div class="clearBoth">

            </div>
            <div>
                <div class="Width100Percent">
                    <div class="divSearchBar paddingBottom10px normalTextStyle rounded_corners">
                        <div class="heading_h8 cntFilteringCritariaLeft">
                            <asp:Literal ID="ltrlSearchOrder" runat="server" Text=" Search Orders"></asp:Literal>
                        </div>
                        <div>
                            <table border="0" cellpadding="5" cellspacing="5" width="85%" style="text-align: left;
                                color: White;">
                                <tr>
                                <td>
                                        <span id="spntdorderref" runat="server" class="heading_h8">Customer or Order Ref</span>
                                    </td>
                                    <td>
                                        <span id="spntdfromdate" runat="server" class="heading_h8">From Date</span>
                                    </td>
                                    <td>
                                        <span id="spntodate" runat="server" class="heading_h8">To Date</span>
                                    </td>
                                    
                                    <td>
                                        <span id="spntdorderstatus" runat="server" class="heading_h8">Order Status</span>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                <td>
                                        <asp:TextBox ID="txtOrderRefrence" MaxLength="20" CssClass="text_box150 rounded_corners5"
                                            runat="server" Width="100px" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFromDate" MaxLength="20" CssClass="text_box150 rounded_corners5"
                                            runat="server" Width="100px"  ReadOnly="false"/>
                                        <ajaxToolkit:CalendarExtender TargetControlID="txtFromDate" Format="MM/dd/yyyy" ID="fromDatePickerExtender"
                                            BehaviorID="fromDatePickerExtender" runat="server" />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtToDate" MaxLength="20" CssClass="text_box150 rounded_corners5"
                                            runat="server" Width="100px"  ReadOnly="false"/>
                                        <ajaxToolkit:CalendarExtender TargetControlID="txtToDate" Format="MM/dd/yyyy" ID="toDatePickerExtender"
                                            BehaviorID="toDatePickerExtender" runat="server" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlOrderStatuses" runat="server" CssClass="dropdown rounded_corners5"
                                            Width="160px">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <div class="float_left_simple">
                                            <asp:Button ID="btnSearchOrders" runat="server" CssClass="start_creating_btn rounded_corners5"
                                                OnClick="btnSearchOrders_Click" Width="60px" />
                                        </div>
                                        <div class="float_left_simple left_margin">
                                            <asp:Button ID="btnClear" runat="server" CssClass="start_creating_btn rounded_corners5"
                                                OnClick="btnClear_Click" CausesValidation="False" Width="60px" /></div>
                                         <div class="float_left_simple left_margin">
                                            <asp:Button ID="btnDownloadOrdersListInExcell" Text="Download" runat="server" CssClass="start_creating_btn rounded_corners5"
                                                OnClick="btnDownloadOrdersListInExcell_Click" CausesValidation="False" Width="90px"/></div>
                                        <div class="clearBoth">
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="white_back_div rounded_corners">
                    <div class="PadingTop textAlignLeft">
                        <asp:Label ID="lblTxtOfRest" runat="server" CssClass="matchingTxtclass" Visible="false" />
                    </div>
                    <div class="Pad1020">
                        <div class="Width100Percent">
                            <div class="textAlignCenter paddingBottom10px normalTextStyle">
                                <asp:Label ID="lblMessage" runat="server" Text="No Record Found!" />
                            </div>
                        </div>
                        <%--The Main Container--%>
                        <div class="ProductOrderContainer">
                            <asp:GridView ID="grdViewOrderhistory" SkinID="ProductOrderHistoryGrid" DataKeyNames="OrderID" 
                                runat="server" AutoGenerateColumns="False" OnRowCommand="grdViewOrderhistory_RowCommand"
                                OnRowDataBound="grdViewOrderhistory_RowDataBound">
                                <Columns>
                                    <asp:BoundField HeaderText="Order Number" DataField="OrderCode" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="10%"/>
                                     <asp:TemplateField HeaderText="Order By" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="hpCompanyContactlnk" runat="server" CssClass="CChpyrCs" CommandName="ViewContactDetails"
                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem, "OrderID")%>'></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="PO ref." DataField="YourRef" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%" />
                                    <asp:BoundField HeaderText="Order Date" DataField="OrderDate" DataFormatString="{0:MMMM d, yyyy}"
                                        HtmlEncode="false" HeaderStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="Delivery Date" DataField="DeliveryDate" DataFormatString="{0:MMMM d, yyyy}"
                                        HtmlEncode="false" HeaderStyle-HorizontalAlign="Left" />
                                    <asp:BoundField HeaderText="Order Status" DataField="StatusName" HeaderStyle-HorizontalAlign="Left" ControlStyle-ForeColor="#F770df" />
                                   
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <div class="textLeftFloating paddingRight3px">
                                                <asp:ImageButton ID="lnkBtnViewOrderDetails" runat="server" CssClass="rounded_corners" ImageUrl="~/images/View-Detail-icon.png" ToolTip="Click to view order details" Height="28" Width="28" CommandName="ViewOrderDetails"
                                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "OrderID")%>' Text="View" />
                                            </div>
                                            <div class="textLeftFloating">
                                                <asp:ImageButton ID="imgBtnReOrderTheCurrentOrder" runat="server"  ToolTip="Download Order information, Job Cards and Production ready artwork" ImageUrl="~/images/download-icon.png" Height="28" Width="28" OnClientClick="showProgress();"
                                                    CssClass="ImgBtnDownloadFile" CommandName="download" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "OrderID")%>' />
                                            </div>
                                               <div class="textLeftFloating">
                                                <asp:ImageButton ID="lnkButnReOrder" runat="server" Text="ReOrder"
                                                    CssClass="rounded_corners" ImageUrl="~/images/repeat.png" Height="28" Width="28"
                                                    ToolTip="Click to reorder " CommandName="ReOrder" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "OrderID")%>' />
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <uc8:CustomPagerControl ID="grdViewOrdHistoryPager" runat="server" />
                            <div>
                                <%--Modal Popupextender--%>
                                <ajaxToolkit:ModalPopupExtender ID="mpeOrderDetails" BehaviorID="mpeOrderDetails"
                                    TargetControlID="hdnTargetCtrlUpload" PopupControlID="pnlOrderDetails" BackgroundCssClass="ModalPopupBG"
                                    runat="server" Drag="true" DropShadow="false" />
                                <input type="hidden" id="hdnTargetCtrlUpload" runat="server" />
                                <asp:Panel ID="pnlOrderDetails" runat="server" CssClass="FileUploaderPopup_Mesgbox LCLB rounded_corners"
                                    Style="display: none; width: 920px;">
                                    <div style=" background:white; padding-top:5px; padding-bottom:5px;">
                                        <uc9:OrderDetailsControl ID="orderDetailsControl" runat="server" />
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <br />
        <br />
    </div>
    <asp:Label ID="lblOrderNumHeadingTxt" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblPOHeadingTxt" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblOrderDateHeadingTxt" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblDelivDateHeadingTxt" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblStatusHeadingTxt" runat="server" Visible="false"></asp:Label>
    <script type="text/javascript" language="javascript">

        function popupHide() {

            $find('mpeOrderDetails').hide();
        }

        function ContactDetailsAction(ContactID) {
            var shadow = document.getElementById("divShd");
            var bws = getBrowserHeight();
            shadow.style.width = bws.width + "px";
            shadow.style.height = bws.height + "px";
            var left = parseInt(($(window).width() - 809) / 2);
            var top = parseInt(($(window).height() - 510) / 2);
            $('#divShd').css("display", "block");
            //shadow = null;
            $('#jqwin').css("position", "fixed");
            $('#jqwin').css("left", left);
            $('#jqwin').css("top", top);
            $('#jqwin').css("background-color", "transparent");
            var html = '<div class="closeBtn_CP" onclick="closeMS()"> </div>';
            $('#jqwin').html(html + '<iframe id="ifrm" style="width:809px; height:510px; padding:5px; padding-bottom:0px; border: none;" class="rounded_corners LCLB"></iframe>').dialog();
            $("#jqwin>#ifrm").attr("src", '../CustomerProfile.aspx?ContactID=' + ContactID);
            $('#jqwin').show();
            $(".closeBtn_CP").css("display", "block");

            return false;
        }
        function closeMS() {
            $(".ui-dialog-titlebar-close").click();
            $(".closeBtn_CP").css("display", "none");
            $('#divShd').css("display", "none");
            $('#jqwin').hide();
        }


    </script>
</asp:Content>
