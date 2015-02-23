<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ThemeSite.Master" AutoEventWireup="true"
    CodeBehind="BrokerBannerWiget.aspx.cs" Inherits="Web2Print.UI.BrokerBannerWiget" %>

<%@ Register Src="Controls/ProductDetail.ascx" TagName="ProductDetail" TagPrefix="uc1" %>
<%@ Register Src="~/Controls/BreadCrumbMenu.ascx" TagName="BreadCrumbMenu" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content6" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content_area container">
        <div class="left_right_padding row">

            <div id="MessgeToDisply" class="rounded_corners" runat="server" visible="false">
                <asp:Literal ID="ltrlMessge" runat="server"></asp:Literal>
            </div>
               <div class="signin_heading_div float_left_simple dashboard_heading_signin">
               <asp:Label ID="lblTitle" runat="server" Text="View and update your webstore banners" CssClass="sign_in_heading"></asp:Label>
                  
            </div>
            <div class="dashBoardRetrunLink">
              <uc1:BreadCrumbMenu ID="BreadCumbMenuCategory" runat="server" WorkMode="MyAccount"
                        MyAccountCurrentPage="Store Banners" MyAccountCurrentPageUrl="BrokerBannerWiget.aspx" />
                     </div>
            <div class="clearBoth">

            </div>
          
            <div id="BannerSelectionCon" class="white-container-lightgrey-border rounded_corners bannerSelecionPanel" runat="server">
                <div class="Width100Percent" runat="server">
                    <div class="textAlignLeft paddingLeft10px paddingBottom10px product_detail_sub_heading custom_color paddingTop15px">
                        <asp:Label ID="Label2" runat="server" Text="View update banners for your webstore" />
                    </div>
                </div>
                <div class="float_left_simple" style="margin-left: 5px;">
                    <asp:RadioButton ID="lblShowWebStoreBanner" GroupName="RD" runat="server" onclick="HideBannerWiget();"
                        Text="Use Default banners" CssClass="MargnRght10" />
                    <asp:RadioButton ID="lblShowOwnStoreBanner" runat="server" GroupName="RD" onclick="ShowBannerWiget();"
                        Text="Use my own banners uploaded below" />
                </div>
                <div class="float_right" style="margin-right: 240px;">
                    <asp:Button ID="btnSaveRadioBtnChanges" runat="server" CssClass="start_creating_btn rounded_corners5 MTopM10 marginRight"
                        Text="Save" Width="100px" OnClick="btnSaveRadioBtnChanges_Click" />
                </div>
            </div>
            <br />
            <div class="clearBoth">
                <br />
            </div>
            <div class="rounded_corners" id="BannerContainer" runat="server">
                <div class="cursor_pointer paddingTop5px" id="AddNewContainer">
                    <div onclick="ShowBannerPopUp();" class="float_left">
                        <img alt="" class="add_new" src="images/AddNew.png" title="New Banner" />
                    </div>
                    <div class="new_caption" onclick="ShowBannerPopUp();">
                        <asp:Label ID="lblNewBannersText" runat="server" Text="New Banner"></asp:Label>
                    </div>
                    <div class="clearBoth">
                    </div>
                </div>
                <%--The Main Container--%>
                <div class="ProductOrderContainer">
                    <div>
                        <asp:GridView ID="grdViewBrokerBanners" DataKeyNames="PageBannerID" runat="server"
                            AutoGenerateColumns="False" OnRowDataBound="grdViewBrokerBanners_RowDataBound" OnRowCreated="grdViewBrokerBanners_RowCreated"
                            OnRowCommand="grdViewBrokerBanners_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Banner Heading" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="15%" HeaderStyle-CssClass="paddingLeft20px">
                                    <ItemTemplate>

                                        <asp:LinkButton ID="lnkH" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Heading")%>' ToolTip="Click To edit banner details"
                                            CommandName="UpdateBannerDetails" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PageBannerID")%>' CssClass="paddingLeft20px" />

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Description" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="50%" ControlStyle-CssClass="OverFlowX" ItemStyle-Height="10px">
                                    <ItemTemplate>

                                        <asp:LinkButton ID="lnkD" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Description")%>' ToolTip="Click To edit banner details"
                                            CommandName="UpdateBannerDetails" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PageBannerID")%>' CssClass="paddingLeft20px" />

                                    </ItemTemplate>
                                </asp:TemplateField>





                                <asp:TemplateField HeaderText="Banner Image" HeaderStyle-HorizontalAlign="Left" ItemStyle-Height="10px">
                                    <ItemTemplate>

                                        <asp:LinkButton ID="lnkI" runat="server" ToolTip="Click To edit banner details"
                                            CommandName="UpdateBannerDetails" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PageBannerID")%>'>
                                            <asp:Image ID="ImgBanner" runat="server" Style="width: 200px;" />
                                        </asp:LinkButton>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action(s)" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                    <ItemTemplate>
                                        <div class="textLeftFloating">
                                            <asp:ImageButton ID="lnkBtnDeleteBannerDetails" runat="server" Text="Delete" ToolTip="Click To delete banner" OnClientClick="return confirm('Are you sure you want to delete this banner?');"
                                                CssClass="rounded_corners" ImageUrl="~/images/delete.png" Height="28" Width="28" CommandName="DeleteBannerDetails" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PageBannerID")%>' />
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div>
                <ajaxToolkit:ModalPopupExtender ID="mpeBanner" BehaviorID="mpeBanner" TargetControlID="hfTargetPO"
                    PopupControlID="pnlBanners" BackgroundCssClass="ModalPopupBG" runat="server"
                    Drag="true" DropShadow="false" CancelControlID="btnCancel" />
                <input type="hidden" id="hfTargetPO" runat="server" />
                <asp:Panel ID="pnlBanners" runat="server" CssClass="FileUploaderPopup_Mesgbox LCLB rounded_corners"
                    Style="display: none; width: 700px;">
                    <div style="background: white; padding-top: 15px; padding-bottom: 15px; padding-left: 15px; padding-right: 15px;" class="white_background">
                        <div class="Pad5px left_align">
                            <asp:Label ID="Label1" runat="server" Text="Home Page Banner" CssClass="sign_in_heading left_align"></asp:Label>
                        </div>
                        <div class="Bottom-doted-Cs">
                            <br />
                        </div>
                        <div id="sizeExccedsMsg" runat="server" style="padding: 10px; font-size: 14px; color: red; border: 1px solid red; margin-bottom: 10px; border-radius: 5px;"
                            visible="false">
                            your upload banner size exceeds.
                        </div>
                        <div class="LeftAlignDivsCs">
                            <asp:Label ID="lblUploadBanner" runat="server" Text="Upload banner image"></asp:Label><br />
                            <asp:Label ID="lblbannerSize" runat="server" CssClass="bannersizeLimit"></asp:Label>
                        </div>
                        <div class="RightAlignDivsCs">
                            <asp:Image ID="imgBanner" runat="server" Style="width: 200px; margin-bottom: 10px;" />
                            <asp:FileUpload ID="fuImageUpload" runat="server" CssClass="file_upload_box210" />
                        </div>
                        <div class="LeftAlignDivsCs">
                            <asp:Label ID="lblHeading" runat="server" Text="Banner Heading"></asp:Label>
                        </div>
                        <div class="RightAlignDivsCs">
                            <asp:TextBox ID="txtBannerHeading" runat="server" CssClass="text_box334 rounded_corners5"></asp:TextBox>
                        </div>
                        <div class="LeftAlignDivsCs">
                            <asp:Label ID="lblDescp" runat="server" Text="Description"></asp:Label>
                        </div>
                        <div class="RightAlignDivsCs">
                            <textarea id="txtAreaDesc" rows="2" cols="40" class="text_box334 rounded_corners5" runat="server">
                            </textarea>
                        </div>
                        <div class="LeftAlignDivsCs">
                            <asp:Label ID="lblITemUrl" runat="server" Text="Banner image URL"></asp:Label><br />
                            <asp:Label ID="Label3" runat="server" AssociatedControlID="txtItemURl" Text="(Cut and paste the full URL of the page you want this banner to link to.)" CssClass="lblToolTip" />
                        </div>
                        <div class="RightAlignDivsCs">

                            <asp:TextBox ID="txtItemURl" runat="server" CssClass="text_box334 rounded_corners5"></asp:TextBox>

                        </div>
                        <div class="clear"></div>

                        <div class="LeftAlignDivsCs">
                            &nbsp;
                        </div>
                        <div class="RightAlignDivsCs">
                            <br />
                            <asp:Button ID="btnSave" runat="server" CssClass="start_creating_btn rounded_corners5 H4B"
                                Text="Save" Width="100px" OnClick="btnSave_Click" />&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnCancel" runat="server" CssClass="start_creating_btn rounded_corners5 H4B"
                                Text="Cancel" Width="100px" OnClientClick="ResetHiddenData();" />
                        </div>
                        <div class="clearBoth">
                            &nbsp;
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
    <br />
    <br />
    <br />
    <asp:HiddenField ID="hfBannerID" runat="server" />
    <asp:HiddenField ID="hfChangeScenarioToSaveNewBanner" runat="server" Value="0" />
    <asp:Button ID="Hiddenbtn" runat="server" OnClick="Hiddenbtn_Click" Style="display: none;" />
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {

            var isPinkBroker = '<%=isPinkCardsBroker %>';
            if (isPinkBroker == 1) {
                var Count = '<%=BannerCount %>';
                if (Count >= 1) {
                    $('#AddNewContainer').css("display", "none");
                } else {
                    $('#AddNewContainer').css("display", "block");
                }
            } else {
                var Count = '<%=BannerCount %>';
                if (Count == 5) {
                    $('#AddNewContainer').css("display", "none");
                } else {
                    $('#AddNewContainer').css("display", "block");
                }
            }


            $("#<%= grdViewBrokerBanners.ClientID%> tr").children(":first").css("border-top-left-radius", 10, "-moz-border-top-left-radius", 10,
             "-webkit-border-top-left-radius", 10, "-khtml-border-top-left-radius", 10);

            $("#<%= grdViewBrokerBanners.ClientID%> tr").children(":first").next().next().next().css("border-top-right-radius", 10, "-moz-border-top-right-radius", 10,
              "-webkit-border-top-right-radius", 10, "-khtml-border-top-right-radius", 10);

            $("#" + "<%=fuImageUpload.ClientID %>").MultiFile(
              {
                  max: 1,
                  accept: 'jpg,jpeg,png',
                  afterFileSelect: function (element, value, master_element) {

                  },
                  afterFileRemove: function (element, value, master_element) {

                  }
              });
        });

        function ShowBannerWiget() {
            var LoadBanners = '<%=BannerAvailable %>';
            if (LoadBanners == 0) {
                $('#<%=BannerContainer.ClientID %>').css("display", "block");
                $('#<%=Hiddenbtn.ClientID %>').click();
            } else {
                $('#<%=BannerContainer.ClientID %>').css("display", "block");
            }
        }

        function ShowBannerPopUp() {

            $('#<%=imgBanner.ClientID %>').css('display', 'none');
            $('#<%=txtBannerHeading.ClientID %>').val('');
            $('#<%=txtAreaDesc.ClientID %>').val('');
            $('#<%=txtItemURl.ClientID %>').val('');
            $('#<%=btnSave.ClientID %>').val('Save');
            $('#<%=hfChangeScenarioToSaveNewBanner.ClientID %>').val('1');
            $find('mpeBanner').show();
        }

        function HideBannerWiget() {
            $('#<%=BannerContainer.ClientID %>').css("display", "none");
        }

        function ResetHiddenData() {
            $('#<%=hfChangeScenarioToSaveNewBanner.ClientID %>').val('0');
        }
    </script>
</asp:Content>
