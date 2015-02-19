<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ThemeSite.Master" AutoEventWireup="true"
    CodeBehind="CustomersVisitorsWiget.aspx.cs" Inherits="Web2Print.UI.CustomersVisitorsWiget" %>
    <%@ Register Src="~/Controls/BreadCrumbMenu.ascx" TagName="BreadCrumbMenu" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContents" runat="server">
    <script src="Scripts/utilities.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="MainContent" runat="server">
    <div id="divShd" class="opaqueLayer">
    </div>
    <div class="content_area container">
        <div class="left_right_padding row">
        <div class="signin_heading_div float_left_simple dashboard_heading_signin">
               <asp:Label ID="lblTitle" runat="server" Text="Visitors Of Your Store" CssClass="sign_in_heading"></asp:Label>
                  
            </div>
            <div class="dashBoardRetrunLink">
               <uc1:BreadCrumbMenu ID="BreadCumbMenuCategory" runat="server" WorkMode="MyAccount"
                MyAccountCurrentPage="Visitors list" MyAccountCurrentPageUrl="CustomersVisitorsWiget.aspx" />
                     </div>
            <div class="clearBoth">

            </div>
          
            <div class="divSearchBar paddingBottom10px normalTextStyle rounded_corners ">
                <table style="width: 75%; border-collapse: collapse; color: White; text-align: left"
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
                            <asp:Button ID="btnsearch" runat="server" Text="Go" CssClass="start_creating_btn rounded_corners5" OnClick="btnsearch_Click"
                                 Style="width: 90px;" />
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
                        <asp:GridView ID="grdViewVisitorsList" DataKeyNames="ContactID" runat="server" AutoGenerateColumns="False"
                            OnRowDataBound="grdViewVisitorsList_RowDataBound" OnRowCommand="grdViewVisitorsList_RowCommand" OnRowCreated="grdViewVisitorsList_RowCreated">
                            <Columns>
                                <asp:BoundField HeaderText="First Name" DataField="FirstName" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="20%" />
                                <asp:BoundField HeaderText="Last Name" DataField="LastName" HeaderStyle-HorizontalAlign="Left"
                                    ControlStyle-CssClass="OverFlowX" ItemStyle-Width="20%" ItemStyle-Height="10px" />
                                <asp:BoundField HeaderText="Company Name" DataField="CompanyName" HeaderStyle-HorizontalAlign="Left"
                                    ControlStyle-CssClass="OverFlowX" ItemStyle-Width="20%" ItemStyle-Height="10px" />
                                <asp:BoundField HeaderText="Email" DataField="Email" HeaderStyle-HorizontalAlign="Left"
                                    ControlStyle-CssClass="OverFlowX" ItemStyle-Width="20%" ItemStyle-Height="10px" />
                                <asp:BoundField HeaderText="Company Contact No" DataField="HomeTel1" HeaderStyle-HorizontalAlign="Left"
                                    ControlStyle-CssClass="OverFlowX" ItemStyle-Width="18%" ItemStyle-Height="10px" />
                                <asp:TemplateField HeaderText="Action(s)" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                    <ItemTemplate>
                                        <div class="textLeftFloating paddingRight3px">
                                            <asp:ImageButton ID="lnkBtnviewDetails" runat="server" Text="View Details" CssClass="rounded_corners" ImageUrl="~/App_Themes/S6/Images/Glass.png" ToolTip="Click to view order details" Height="28" Width="28" /></div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
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
     

        function ViewContactCompanyDetails(ContactID) {
            
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
