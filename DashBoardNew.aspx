<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ThemeSite.Master" AutoEventWireup="true"
    CodeBehind="DashBoardNew.aspx.cs" Inherits="Web2Print.UI.DashBoardNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContents" runat="server">
    <link href="/Styles/DashboardCarousal.css" rel="stylesheet" type="text/css" />
   
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content_area" style=" background-color: #373C40;">
        <div class="left_right_padding">
            <div style="height: 25px; background-color: #3c3838; width: 600px; float: right;
                margin-right: 190px; border-bottom-left-radius: 10px; border-bottom-right-radius: 10px;
                padding-top: 5px;">
                <asp:Label ID="POlbl" runat="server" Text="pending orders" Style="font-size: 12px;
                    color: #9b9b9b; text-transform: uppercase; margin-right: 10px;"></asp:Label><asp:Label
                        ID="POCount" runat="server" Style="margin-right: 10px; color: Red;"></asp:Label>
                <asp:Label ID="Label2" runat="server" Text="saved designs" Style="font-size: 12px;
                    color: #9b9b9b; text-transform: uppercase; margin-right: 10px;"></asp:Label><asp:Label
                        ID="SDCount" runat="server" Style="margin-right: 10px; color: Red;"></asp:Label>
                <asp:Label ID="Label1" runat="server" Text="favourite designs" Style="font-size: 12px;
                    color: #9b9b9b; text-transform: uppercase; margin-right: 10px;"></asp:Label><asp:Label
                        ID="FDCount" runat="server" Style="margin-right: 10px; color: Red;"></asp:Label>
                <asp:HyperLink ID="hyperlinkCart" NavigateUrl="~/PinkCardShopCart.aspx" runat="server"
                    Style="font-size: 12px; color: #9b9b9b; text-transform: uppercase;">
                    <asp:Label ID="logout" runat="server" Text="Logout"></asp:Label>
                </asp:HyperLink>
            </div>
            <div class="clearBoth">
                &nbsp;
            </div>
            <div id="tabs" class="spacer10pxtop">
                <div id="DashBoardsTab" runat="server" clientidmode="Predictable"  class="TabsWidth cursor_pointer UnSelectedTabColor" onclick="tabify('Dashboard','MainContent_DashBoardsTab')">
                    <a id="lnkdashboard" runat="server">Dashboard</a>
                </div>
                <div id="CrmEstTab" runat="server" class="TabsWidth cursor_pointer UnSelectedTabColor" onclick="tabify('Estimates','MainContent_CrmEstTab')">
                    <a id="lnkCrmEst" runat="server">CRM & Estimating</a>
                </div>
                <div id="OrdMngTab" runat="server" class="TabsWidth cursor_pointer UnSelectedTabColor" onclick="tabify('Orders','MainContent_OrdMngTab')">
                    <a id="lnkOrdMng" runat="server">Order Management</a>
                </div>
                <div id="POInkTab" runat="server" class="TabsWidth cursor_pointer UnSelectedTabColor" onclick="tabify('Invoicing','MainContent_POInkTab')">
                    <a id="lnkPOInk" runat="server">PO & Invoicing</a>
                </div>
                <div id="ProTempTab" runat="server" class="TabsWidth cursor_pointer UnSelectedTabColor" onclick="tabify('Products','MainContent_ProTempTab')">
                    <a id="lnkProTmp" runat="server">Products & Templates</a>
                </div>
                <div id="MAstStrTab" runat="server" class="TabsWidth cursor_pointer UnSelectedTabColor" onclick="tabify('Store','MainContent_MAstStrTab')">
                    <a id="lnkMstStore" runat="server">Master Store</a>
                </div>
                <div id="AccSettingsTab" runat="server" class="TabsWidth cursor_pointer UnSelectedTabColor" onclick="tabify('Settings','MainContent_AccSettingsTab')">
                    <a id="lnkAccSet" runat="server">Settings</a>
                </div>
                
                <div class="clearBoth">
                    &nbsp;
                </div>
                <div id="tabs1" class="displayBlock">
                    <div id="mcts1">
                        <asp:Repeater ID="rptDashboardSlider" runat="server" 
                            OnItemDataBound="rptDashboardSlider_ItemDataBound" >
                            <ItemTemplate>
                             <asp:HyperLink ID="hlContainer" runat="server" NavigateUrl="#">
                                <div id="ImgContainer" runat="server" style="float: left; text-align: center; padding: 10px; background-color: #2c8ba3; width: 150px;
                                    height: 120px; -moz-border-radius: 5px; -webkit-border-radius: 5px; -khtml-border-radius: 5px;
                                    border-radius: 5px;">
                                    <asp:Image ID="imgIcon" runat="server" Style="width: 40px; margin-top: 10px; margin-bottom: 30px;" />
                                    <br />
                                    <asp:Label ID="lblIcon" runat="server" Style="color: #6ed5f3; font-size: 12px;"></asp:Label>
                                    
                                </div>
                                 </asp:HyperLink>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
            <div class="clearBoth">
                &nbsp;</div>
                <div id="ContentArea" style="background-color:white; padding:10px;">
                <iframe id="iframe" style="width:100%; min-height:500px; border:none;">
                </iframe>
                </div>
                <div class="clearBoth">
                &nbsp;</div>
        </div>
    </div>
    
    <script src="js/thumbnail-slider.js" type="text/javascript"></script>
    
    <script type="text/javascript">
        var rptDashboardItems = 0;
        var SelectedTabsItems = null;
        var numItems = 0;
        $(document).ready(function () {
            var count = '<%=count %>';
            if (count == 2) {
                $('.TabsWidth').css("width", "480px");
            }
            else {
                var tabwidth = 940 / count;
                $('.TabsWidth').css("width", tabwidth);
            }

            $("#MainContent_DashBoardsTab").removeClass("UnSelectedTabColor");
            $("#MainContent_DashBoardsTab").addClass("SelectedTabColor");


        });
        function tabify(id, btnName) {

            $("#MainContent_DashBoardsTab").removeClass("SelectedTabColor");
            $("#MainContent_CrmEstTab").removeClass("SelectedTabColor");
            $("#MainContent_OrdMngTab").removeClass("SelectedTabColor");
            $("#MainContent_POInkTab").removeClass("SelectedTabColor");
            $("#MainContent_ProTempTab").removeClass("SelectedTabColor");
            $("#MainContent_MAstStrTab").removeClass("SelectedTabColor");
            $("#MainContent_AccSettingsTab").removeClass("SelectedTabColor");

            $("#MainContent_DashBoardsTab").addClass("UnSelectedTabColor");
            $("#MainContent_CrmEstTab").addClass("UnSelectedTabColor");
            $("#MainContent_OrdMngTab").addClass("UnSelectedTabColor");
            $("#MainContent_POInkTab").addClass("UnSelectedTabColor");
            $("#MainContent_ProTempTab").addClass("UnSelectedTabColor");
            $("#MainContent_MAstStrTab").addClass("UnSelectedTabColor");
            $("#MainContent_AccSettingsTab").addClass("UnSelectedTabColor");

            ///
            $("#" + btnName).removeClass("UnSelectedTabColor");
            $("#" + id).removeClass("displayNone");
            $("#" + id).addClass("displayBlock");
            $("#" + btnName).addClass("SelectedTabColor");
            
            $('#mcts1  a').each(function (i) {
				$(this).css("display","none");

            });
            $(".bbtn" + id).css("display", "block");

            $(".navPrev").css("display", "block");
            $(".navNext").css("display", "block");

            numItems = $(".bbtn" + id).length;
            if (numItems < 5) {
                $(".navPrev").css("display", "none");
                $(".navNext").css("display", "none");
            }
        }

        function ShowContent(ID, PageNav) {
            $('#mcts1  a div').each(function (i) {
                $(this).css("background", "#2c8ba3");
            });
            $(".selectedItem" + ID + " div").css("background", "#8fb732");
            $("#iframe").attr("src", "/DashBoardLinks.aspx?uCode=" + PageNav);
            

         }
    </script>
</asp:Content>
