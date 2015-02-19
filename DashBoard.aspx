<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ThemeSite.Master"
    CodeBehind="DashBoard.aspx.cs" Inherits="Web2Print.UI.DashBoard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Controls/PromotionalBanner.ascx" TagName="PromotionalBanners"
    TagPrefix="uc6" %>
<%@ Register Src="Controls/WhyChooseUs.ascx" TagName="WhyChooseUs" TagPrefix="uc3" %>
<%@ Register Src="Controls/MatchingSet.ascx" TagName="MatchingSet" TagPrefix="uc4" %>
<asp:Content ID="DashBoardContent3" ContentPlaceHolderID="MainContent" runat="server">
    <div id="divShd" class="opaqueLayer" style="z-index: 999 !important;">
    </div>
    <div class="container content_area">
        <div class="row left_right_padding">
            <div id="BCTitle" class="signin_heading_div col-md-12 col-lg-12 col-xs-12" runat="server" visible="false">
                <asp:Label ID="lblORderManagment" runat="server" Text="Order Managment" CssClass="sign_in_heading"></asp:Label>
            </div>
            <div class="clearboth">

            </div>
            <div id="BCDIV" class="white-container-lightgrey-border rounded_corners" runat="server">
                <div class="padding50">
                    <asp:Repeater ID="rptBrokerCorpDasHBItems" runat="server" OnItemDataBound="rptBrokerCorpDasHBItems_ItemDataBound">
                        <ItemTemplate>
                            <div class="cntDashboardItems" >
                                <asp:HyperLink ID="hyperlinkToNavigateBC" runat="server">
                                    <div id="dashIcons" runat="server" class="dashboard_logo_div dashboardIcons contact_log MarginBottom30px">
                                        
                                    </div>
                                    
                                   <%-- <asp:Image ID="ImageLogoBC" runat="server" CssClass="dashboard_logo_div contact_log MarginBottom30px" />--%>
                                    <div id="NavigationDiv" class="dashboard_logo_des_div MarginBottom30px" runat="server">
                                        <div class="dashboard_item_main_heading">
                                            <asp:Literal ID="lblBCName" runat="server"></asp:Literal>
                                        </div>
                                        <div class="dashboard_item_detail">
                                            <asp:Literal ID="lblBCDescription" runat="server"></asp:Literal>
                                        </div>
                                    </div>
                                </asp:HyperLink>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <div class="clearBoth">
                        &nbsp;
                    </div>
                </div>
                <%-- <div class="clearBoth">
                        &nbsp;
                 </div>--%>
                <div class="divRowAnalytics rounded_corners" runat="server" id="divAnalytics">
                    <div class="cntdashboardcountersselect">
                        <span class="heading_h8 txtStatistics">Analytics</span>
                        <asp:DropDownList ID="ddlAnaylitcs" runat="server" CssClass="dropdown rounded_corners5"
                            Style="margin-left: 10px; margin-top: 3px;" Width="160px" OnSelectedIndexChanged="ddlAnaylitcs_SelectedIndexChanged"
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </div>
                    <div class="cntdashboardcounters">
                        <div class="divdashboardcounter">
                            <asp:Label ID="lblOrdersCount" runat="server" class="dashboardcounters" Text="0" />&nbsp;
                            <span id="spnOrders" runat="server" class="labeldashboardcounter">Orders Checked out</span>
                        </div>
                        <div class="divdashboardcounter">
                            <asp:Label ID="lblRegUserCount" runat="server" class="dashboardcounters" Text="0" />&nbsp;
                            <span id="spnRegUsers" runat="server" class="labeldashboardcounter">Registered Users</span>
                        </div>
                        <div class="divdashboardcounter">
                            <asp:Label ID="lblSubscribersCount" runat="server" class="dashboardcounters" Text="0" />&nbsp;
                            <span id="spnSubscribers" runat="server" class="labeldashboardcounter">Subscribers</span>
                        </div>
                        <div class="divdashboardcounter">
                            <asp:Label ID="lblRFQCount" runat="server" class="dashboardcounters" Text="0" />&nbsp;
                            <span id="spnRFQs" runat="server" class="labeldashboardcounter">RFQs</span>
                        </div>
                    </div>
                    <div class="clearBoth">
                    </div>
                </div>
            </div>
            <div class="spacer10pxtop">
                &nbsp;
            </div>
            <div id="SPtitle" class="signin_heading_div col-md-12 col-lg-12 col-xs-12" runat="server" visible="false">
                <asp:Label ID="Label2" runat="server" Text="Store Preferences" CssClass="sign_in_heading"></asp:Label>
            </div>
            <div id="SPDIV" class="white-container-lightgrey-border rounded_corners" runat="server">
                <div id="Store_Pre_Left_Panel" runat="server" class="padding50">
                    <asp:Repeater ID="rptStorePreferences" runat="server" OnItemDataBound="rptStorePreferences_ItemDataBound">
                        <ItemTemplate>
                            <div class="cntDashboardItemsSP">
                                <asp:HyperLink ID="hyperlinkToNavigateSP" runat="server">
                                    <div id="dashIconsSp" runat="server" class="dashboard_logo_div dashboardIcons contact_log MarginBottom30px">
                                        
                                    </div>
                                   <%-- <asp:Image ID="ImageLogoSP" runat="server" CssClass="dashboard_logo_div contact_log MarginBottom30px" />--%>
                                    <div id="NavigationDiv" class="dashboard_logo_des_div MarginBottom30px" runat="server">
                                        <div class="dashboard_item_main_heading">
                                            <asp:Literal ID="lblSPName" runat="server"></asp:Literal>
                                        </div>
                                        <div class="dashboard_item_detail">
                                            <asp:Literal ID="lblSPDescription" runat="server"></asp:Literal>
                                        </div>
                                    </div>
                                </asp:HyperLink>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <div class="clearBoth">
                        &nbsp;
                    </div>
                </div>
                <div id="AvailableSoonCont" runat="server" class="white-container-lightgrey-border rounded_corners bkGrey"
                    style="width: 170px; float: left; margin-top: 50px;">
                    <p style="color: #666666; font-size: 16px;">
                        Coming Soon</p>
                    <div style="margin-top: 25px; line-height: 7px; color: #666666; font-size: 13px;">
                        <p>
                            Google analytics</p>
                        
                        <p>
                            Improved SEO
                        </p>
                   
                        <p>
                            Greeting Cards wizard</p>
                        <p>
                            Picture Book wizard</p>
                        <p>
                            Canvas Print wizard</p>
                        <p>
                            More Templates</p>
                        <p>
                            Editors choice</p>
                        <p>
                            Marketing Tips</p>
                    </div>
                </div>
                <div class="clearBoth">
                    &nbsp;
                </div>
            </div>
            <div class="spacer10pxtop">
                &nbsp;
            </div>
            <div class="signin_heading_div col-md-12 col-lg-12 col-xs-12">
                <asp:Label ID="lblTitle" runat="server" Text="Dashboard" CssClass="sign_in_heading"></asp:Label>
            </div>
            <div id="cntDashboardItems" runat="server" class="white-container-lightgrey-border rounded_corners">
                <div class="padding50">
                    <asp:Repeater ID="rptRetailDashboardItem" runat="server" OnItemDataBound="rptRetailDashboardItem_ItemDataBound">
                        <ItemTemplate>
                            <div  class="cntDashboardItems">
                                <asp:HyperLink ID="hyperlinkToNavigate" runat="server">

                                    <%--<asp:Image ID="ImageLogo" runat="server" CssClass="dashboard_logo_div contact_log MarginBottom30px" />--%>
                                     <div id="dashIconsrp" runat="server" class="dashboard_logo_div dashboardIcons contact_log MarginBottom30px">
                                        
                                    </div>
                                    <div id="NavigationDiv" class="dashboard_logo_des_div MarginBottom30px" runat="server">
                                        <div class="dashboard_item_main_heading">
                                            <asp:Literal ID="lblName" runat="server"></asp:Literal>
                                            <asp:Label ID="StoreBrandtxt" runat="server" Visible="false" CssClass="dashboard_item_main_heading"></asp:Label>
                                        </div>
                                        <div class="dashboard_item_detail">
                                            <asp:Literal ID="lblDescription" runat="server"></asp:Literal>
                                            <asp:Image ID="imgBrandName" runat="server"  Visible="false" />
                                        </div>
                                    </div>
                                   <div class="clearBoth">

                                   </div>
                                    
                                </asp:HyperLink>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <div class="clearBoth">
                        &nbsp;
                    </div>
                </div>
            </div>
            <div class="spacer10pxtop">
                &nbsp;
            </div>
           
        </div>
        <br />
        <br />
        <br />
    </div>
   <ajaxToolkit:ModalPopupExtender ID="mpeMessageBoxDeleteAccount" runat="server" BackgroundCssClass="ModalPopupBG"
    PopupControlID="pnlDeleteAcc" TargetControlID="btnYesNO" BehaviorID="mpeMessageBoxDeleteAccount"
    CancelControlID="btnNo" DropShadow="false">
</ajaxToolkit:ModalPopupExtender>
<asp:Panel ID="pnlDeleteAcc" runat="server" Width="500px" CssClass="FileUploaderPopup_Mesgbox LCLB rounded_corners"
    Style="display: none">
     <div class="white_background" style="padding:20px;">
          <asp:Label ID="lblHeaderSave" runat="server" CssClass="left_align FileUploadHeaderText_PopUp float_left_simple MesgBoxClass" Text="Alert!"></asp:Label>
     
    <div class="clearBoth">
        &nbsp;
        </div>
         <div class="SolidBorderCS">
        &nbsp;
        </div>
    <div class="pop_body_MesgPopUp">
        <br />
        <div class="inner">
            <asp:Label ID="lblMessageDeleteStore" runat="server" Text="R u sure you want to delete your account?"></asp:Label>
        </div>
        <br />
        <div class="padding_top_bottom_10 center_align">
            <asp:Button ID="btnNo" runat="server" Text="Cancel"
                CssClass="start_creating_btn_LetWait rounded_corners5" />
            <asp:Button ID="btnYes" runat="server" Text="Yes, delete my account"  CssClass="start_creating_btn_LetWait rounded_corners5" OnClick="deleteStore_Click" />
        </div>
    </div>
    </div>
</asp:Panel>
    <asp:Button ID="btnYesNO" runat="server" Style="display: none; width: 0px; height: 0px" />
    <div id="jqwin" class="FileUploaderPopup_Mesgbox rounded_corners IframeCss ">
    </div>
    <style>
        #jqwin
        {
            /* height: 100%;--%> */
            z-index: 999;
            position: absolute;
            display: none;
        }
    </style>
    <script type="text/javascript" language="javascript">

        function ShowConfirmationPopup() {
            $find('mpeMessageBoxDeleteAccount').show();

        }

        function ChangePassPopupShow() {
            var popwidth = 681;

            var shadow = document.getElementById("divShd");
            var bws = getBrowserHeight();
            shadow.style.width = bws.width + "px";
            shadow.style.height = bws.height + "px";
            var left = parseInt((bws.width - popwidth) / 2);
            var top = parseInt((bws.height - 385) / 2);


            //        shadow = null;
            $('#divShd').css("display", "block");
            $('#jqwin').css("width", popwidth);
            $('#jqwin').css("height", 385);
            $('#jqwin').css("top", top);
            $('#jqwin').css("left", left);
            var html = ''; // '<div class="closeBtn2" onclick="closeMS()"> </div>';
            $('#jqwin').html(html + '<iframe id="ifrm" width="' + (popwidth - 20) + '" height="100%" border="0" style="width:' + (popwidth - 20) + 'px;height:100%;border: none; " class=""></iframe>')
            $('#ifrm').attr('src', '/ResetPassword.aspx');
            $('#jqwin').show();
            $(".closeBtn2").css("display", "block");

            //            $find('mdlResetPwdPopup').show();
            return false;
        }
        function closeMS() {
            $('#divShd').css("display", "none");
            $(".closeBtn").css("display", "none");
            $('#jqwin').hide();
            $('#ifrm').attr('src', 'about:blank');
        }
        function getBrowserHeight() {
            var intH = 0;
            var intW = 0;
            if (typeof window.innerWidth == 'number') {
                intH = window.innerHeight;
                intW = window.innerWidth;
            }
            else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
                intH = document.documentElement.clientHeight;
                intW = document.documentElement.clientWidth;
            }
            else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
                intH = document.body.clientHeight;
                intW = document.body.clientWidth;
            }
            return { width: parseInt(intW), height: parseInt(intH) };
        }
    </script>
</asp:Content>
