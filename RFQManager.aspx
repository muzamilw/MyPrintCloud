<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ThemeSite.Master" AutoEventWireup="true"
    CodeBehind="RFQManager.aspx.cs" Inherits="Web2Print.UI.RFQManager" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Controls/BreadCrumbMenu.ascx" TagName="BreadCrumbMenu" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContents" runat="server">
  

     
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
     <div id="divShd" class="opaqueLayer">
    </div>
    <div class="content_area container">
        <div class="left_right_padding row">
            <div class="signin_heading_div float_left_simple dashboard_heading_signin">
              <asp:Label ID="lblTitle" runat="server" Text="RFQ's for your store" CssClass="sign_in_heading"></asp:Label>
            </div>
            <div class="dashBoardRetrunLink">
               <uc1:BreadCrumbMenu ID="BreadCumbMenuCategory" runat="server" WorkMode="MyAccount"
                        MyAccountCurrentPage="Visitors list" MyAccountCurrentPageUrl="CustomersVisitorsWiget.aspx" />
                     </div>
            <div class="clearBoth">

            </div>
           
            <div class="divSearchBar paddingBottom10px normalTextStyle rounded_corners">
                <table style="width: auto; border-collapse: collapse; color: White; text-align: left"
                    cellpadding="5" cellspacing="5">
                    <tr>
                        <td>
                            <div style="float: left; padding: 23px 23px 23px 23px;" class="heading_h8">
                                <asp:Literal ID="ltrlSearchrecords" runat="server" Text=" Search Records"></asp:Literal>
                            </div>
                        </td>
                        <td>
                            <asp:TextBox ID="txtsearch" runat="server" CssClass="text_box300 rounded_corners5"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnsearch" runat="server" Text="Go" CssClass="start_creating_btn rounded_corners5"
                                OnClick="btnsearch_Click" Style="width: 90px; margin-right:10px; margin-left:20px;" />
                        </td>
                        <td>
                            <asp:Button ID="btnReset" runat="server" Text="Reset" Style="width: 90px;" CssClass="start_creating_btn rounded_corners5"
                                OnClick="btnReset_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="white_back_div rounded_corners">
                <div class="pad10">
                    <asp:Label ID="lblmatchingrecord" runat="server" CssClass="matchingTxtclass" />
                </div>
                <div class="ProductOrderContainer" id="BannerContainer">
                    <div>
                        <asp:GridView ID="grdViewRFQList" DataKeyNames="InquiryID" runat="server" AutoGenerateColumns="False"
                            OnRowDataBound="grdViewRFQList_RowDataBound" OnRowCommand="grdViewRFQList_RowCommand"
                            OnRowCreated="grdViewVisitorsList_RowCreated">
                            <Columns>
                                <asp:BoundField HeaderText="Inquiry Code" DataField="InquiryCode" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="15%" />
                                <asp:BoundField HeaderText="Title" DataField="InquiryTitle" HeaderStyle-HorizontalAlign="Left"
                                    ControlStyle-CssClass="OverFlowX" ItemStyle-Width="19%" ItemStyle-Height="10px" />
                                  <asp:BoundField HeaderText="From" DataField="ContactName" HeaderStyle-HorizontalAlign="Left"
                                    ControlStyle-CssClass="OverFlowX" ItemStyle-Width="16%" ItemStyle-Height="10px" />
                                 <asp:BoundField HeaderText="Email" DataField="Email" HeaderStyle-HorizontalAlign="Left"
                                    ControlStyle-CssClass="OverFlowX" ItemStyle-Width="16%" ItemStyle-Height="10px" />
                                 <asp:BoundField HeaderText="Phone" DataField="Phone" HeaderStyle-HorizontalAlign="Left"
                                    ControlStyle-CssClass="OverFlowX" ItemStyle-Width="16%" ItemStyle-Height="10px" />
                                <asp:BoundField HeaderText="Created Date" DataField="InquiryCreatedDate" HeaderStyle-HorizontalAlign="Left" DataFormatString="{0:MMMM d, yyyy}"
                                    ControlStyle-CssClass="OverFlowX" ItemStyle-Width="22%" ItemStyle-Height="10px" />
                               <%-- <asp:BoundField HeaderText="Required Date" DataField="InquiryRequiredDate" HeaderStyle-HorizontalAlign="Left"
                                    ControlStyle-CssClass="OverFlowX" ItemStyle-Width="22%" ItemStyle-Height="10px" />--%>
                                
                                <asp:TemplateField HeaderText="Action(s)" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <div class="textLeftFloating paddingRight3px">
                                            <asp:ImageButton ID="lnkBtnviewDetails" runat="server" Text="View Details" CssClass="rounded_corners"
                                                ImageUrl="~/images/View-Detail-icon.png" ToolTip="Click to view Inquiry details" CommandName="ViewInquiryItems" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "InquiryID")%>' 
                                                Height="28" Width="28" />
                                                <asp:ImageButton ID="lnkDownloadIcon" runat="server" Text="View Details" CssClass="rounded_corners"
                                                ImageUrl="~/images/download-icon.png" ToolTip="Click to download Inquiry attachments" CommandName="DownloadInquiryItems" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "InquiryID")%>' 
                                                Height="28" Width="28" />
                                                </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div>
             <ajaxToolkit:ModalPopupExtender ID="mpeInquiryItems" BehaviorID="mpeInquiryItems" TargetControlID="hfInquiryItems"
                    PopupControlID="pnlInquiryItems" BackgroundCssClass="ModalPopupBG" runat="server"
                    Drag="true" DropShadow="false" CancelControlID="CancelControl" />
                <input type="hidden" id="hfInquiryItems" runat="server" />
                <asp:Panel ID="pnlInquiryItems" runat="server" CssClass="FileUploaderPopup_Mesgbox LCLB rounded_corners"
                    Style="display: none; width: 740px; min-height:200px;">
                    <div style="background: white; padding-top: 15px; padding-bottom: 15px; padding-left: 15px;
                        padding-right: 15px; min-height:200px;" class="white_background">
                        <div class="left_align" style="padding-bottom: 5px;">
                            <div class="float_left_simple spacer10pxtop">
                                <asp:Label ID="Label1" runat="server" Text="Inquiry Items Detail" CssClass="Fsize17 left_align"></asp:Label>
                            </div>
                          
                            <div id="CancelControl" class="MesgBoxBtnsDisplay rounded_corners5">

                                Close
                            </div>
                            <div class="clear">
                            </div>
                        </div>
                        <div class="Bottom-doted-Cs">
                            <br />
                        </div>
                        <div class="Mtop15P">
                        <asp:GridView ID="grdViewRFQItemsList" DataKeyNames="InquiryID" runat="server" AutoGenerateColumns="False"
                            OnRowDataBound="grdViewRFQItemsList_RowDataBound" 
                            OnRowCreated="grdViewRFQItemsList_RowCreated">
                            <Columns>
                                <asp:BoundField HeaderText="Title" DataField="Title" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="20%" />
                                <asp:BoundField HeaderText="Notes" DataField="Notes" HeaderStyle-HorizontalAlign="Left"
                                    ControlStyle-CssClass="OverFlowX" ItemStyle-Width="20%" ItemStyle-Height="10px" />
                                <asp:BoundField HeaderText="Delivery Date" DataField="DeliveryDate" HeaderStyle-HorizontalAlign="Left" DataFormatString="{0:MMMM d, yyyy}"
                                    ControlStyle-CssClass="OverFlowX" ItemStyle-Width="20%" ItemStyle-Height="10px" />
                            </Columns>
                        </asp:GridView>
                              <div class="float_right_simple spacer10pxtop">
                                  <p id="lblDownloadAttachment" runat="server" class="lbl_Download_Attachment">Download Attachment</p>
                                  
                                 <asp:ImageButton ID="lnkDownloadIconPopup" runat="server" Text="View Details" CssClass="rounded_corners download_artwork_popup_simple_margin"
                                                ImageUrl="~/images/download-icon.png" ToolTip="Click to download Inquiry attachments" OnClick="lnkDownloadIconPopup_Click"
                                                Height="40" Width="45" />
                            </div>
                            
                    </div>
                        <div class="clearBoth">

                        </div>
                    </div>
                </asp:Panel>
                <asp:HiddenField ID="hdnInqueryID" Visible="false" runat="server" />
            </div>
        </div>
    </div>
    <br />
    <br />
    <br />
     <div id="jqwin" class="FileUploaderPopup_Mesgbox" style="position: fixed; z-index:1000;">
        <div class="clear">
        </div>
    </div>
    <script type="text/javascript" language="javascript">
        function ViewInqueryDetails(InquiryID) {
            
            var shadow = document.getElementById("divShd");
            var bws = getBrowserHeight();
            shadow.style.width = bws.width + "px";
            shadow.style.height = bws.height + "px";
            var left = parseInt(($(window).width() - 809) / 2);
            var top = parseInt(($(window).height() - 610) / 2);
            $('#divShd').css("z-index", "500");
            $('#divShd').css("display", "block");
            //shadow = null;
            $('#jqwin').css("position", "fixed");
            $('#jqwin').css("z-index", "10001");
            $('#jqwin').css("left", left);
            $('#jqwin').css("top", top);
            $('#jqwin').css("background-color", "transparent");
          
            var html = '<div onclick="closeMS();" class="exit_page_CP MesgBoxBtnsDisplay rounded_corners5" style="">Close</div>';
            $('#jqwin').html(html + '<iframe id="ifrm" style="width:809px; height:610px; padding:5px; padding-bottom:0px; border: none; z-index:1000000 !important;" class="rounded_corners LCLB"></iframe>').dialog();
            $("#jqwin>#ifrm").attr("src", '../InquiryDetails.aspx?InquiryID=' + InquiryID);
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
