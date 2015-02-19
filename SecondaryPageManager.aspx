<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ThemeSite.Master" AutoEventWireup="true"
    CodeBehind="SecondaryPageManager.aspx.cs" Inherits="Web2Print.UI.SecondaryPageManager" %>
    <%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Controls/BreadCrumbMenu.ascx" TagName="BreadCrumbMenu" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContents" runat="server">
    
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content_area container">
        <div class="left_right_padding row">
            <div id="MessgeToDisply" class="rounded_corners" runat="server" visible="false">
                <asp:Literal ID="ltrlMessge" runat="server"></asp:Literal>
            </div>
             <div class="signin_heading_div float_left_simple dashboard_heading_signin">
               <asp:Label ID="lblTitle" runat="server" Text="View and update your webstore secondary pages"
                    CssClass="sign_in_heading"></asp:Label>
            </div>
            <div class="dashBoardRetrunLink">
              <uc1:BreadCrumbMenu ID="BreadCumbMenuCategory" runat="server" WorkMode="MyAccount"
                        MyAccountCurrentPage="Store Banners" MyAccountCurrentPageUrl="BrokerBannerWiget.aspx" />
                     </div>
            <div class="clearBoth">

            </div>
            <div id="BannerSelectionCon" class="white-container-lightgrey-border rounded_corners bannerSelecionPanel"
                 runat="server">
                <div id="Div1" class="Width100Percent" runat="server">
                    <div class="textAlignLeft paddingLeft10px paddingBottom10px product_detail_sub_heading custom_color paddingTop15px">
                        <asp:Label ID="Label2" runat="server" Text="View update secondary pages for your webstore" />
                    </div>
                </div>
                <div class="float_left_simple" style="margin-left: 5px;">
                    <asp:RadioButton ID="lblShowWebStoreSPage" GroupName="RD" runat="server" onclick="HideBannerWiget();"
                        Text="Use Default secondary pages" CssClass="MargnRght10" />
                    <asp:RadioButton ID="lblShowOwnStoreSPage" runat="server" GroupName="RD" onclick="ShowBannerWiget();"
                        Text="Use my own secondary pages uploaded below" />
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
            <div class="rounded_corners" id="SPageContainer" runat="server">
                <div class="cursor_pointer paddingTop5px" id="AddNewContainer">
                    <div onclick="ShowSPagePopUp();" class="float_left">
                        <img alt="" class="add_new" src="images/AddNew.png" title="New Page" /></div>
                    <div class="new_caption" onclick="ShowSPagePopUp();">
                        <asp:Label ID="lblNewText" runat="server" Text="Add new page" ></asp:Label>
                    </div>
                    <div class="clearBoth">
                    </div>
                </div>
               <%-- <div id="ErrorMsgCon" class="white-container-lightgrey-border rounded_corners" style="height: 30px;
                    text-align: center; padding-top: 20px;" runat="server" visible="false">
                    <asp:Label ID="lblErrorMes" runat="server" Text="You have no secondary pages to display."
                        ForeColor="Red" Font-Size="13"></asp:Label>
                </div>--%>
                <div id="ContainerSP" class="ProductOrderContainer" runat="server">
                    <div>
                        <asp:GridView ID="grdViewBrokerSPages" DataKeyNames="PageID" runat="server" AutoGenerateColumns="False"
                            OnRowDataBound="grdViewBrokerSPages_RowDataBound" OnRowCreated="grdViewBrokerSPages_RowCreated"
                            OnRowCommand="grdViewBrokerSPages_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Page Title" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="25%">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkH" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PageTitle")%>'
                                       
                                            />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Page Meta Title" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="50%"
                                    ControlStyle-CssClass="OverFlowX" ItemStyle-Height="10px">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkD" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Meta_Title")%>'
                                            CssClass="paddingLeft20px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Banner Image" HeaderStyle-HorizontalAlign="Left" ItemStyle-Height="10px">
                                    <ItemTemplate>
                                       
                                            <asp:Image ID="ImgBanner" runat="server" Style="width: 200px;" />
                                       
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action(s)" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                    <ItemTemplate>
                                        <div class="textLeftFloating">
                                            <asp:ImageButton ID="lnkBtnviewDetails" runat="server" Text="View Details" CssClass="rounded_corners"
                                                ImageUrl="~/images/View-Detail-icon.png" ToolTip="Click To edit secondary page details" CommandName="UpdateSPageDetails" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PageID")%>'  
                                                Height="23" Width="23" style="margin-right:5px;" />
                                            <asp:ImageButton id="btnEditPageHtml" runat="server" CssClass="rounded_corners" ImageUrl="~/images/edit.png" Height="23" Width="23" Text="Page HTML" ToolTip="Click To edit page html" CommandName="EditHtml" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PageID")%>'  />
                                            <asp:ImageButton ID="lnkBtnDeleteBannerDetails" runat="server" Text="Delete" ToolTip="Click To delete secondary page"
                                                OnClientClick="return confirm('Are you sure you want to delete this page?');"
                                                CssClass="rounded_corners" ImageUrl="~/images/delete.png" Height="28" Width="28"
                                                CommandName="DeleteSPageDetails" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PageID")%>' />
                                           
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div>
                    <ajaxToolkit:ModalPopupExtender ID="mpeSPage" BehaviorID="mpeSPage" TargetControlID="hfTargetPO"
                        PopupControlID="pnlBanners" BackgroundCssClass="ModalPopupBG" runat="server"
                        Drag="true" DropShadow="false" CancelControlID="btnCancel" />
                    <input type="hidden" id="hfTargetPO" runat="server" />
                    <asp:Panel ID="pnlBanners" runat="server" CssClass="FileUploaderPopup_Mesgbox LCLB rounded_corners"
                        Style="display: none; width: 760px; height:615px;">
                        <div style="background: white; padding-top: 15px; padding-bottom: 15px; padding-left: 15px;
                            padding-right: 15px;" class="white_background">
                            <div class="left_align" style=" padding-bottom: 15px;">
                                <div class="float_left_simple spacer10pxtop">
                                    <asp:Label ID="Label1" runat="server" Text="Page Details" CssClass="FileUploadHeaderText  left_align"></asp:Label>
                                </div>
                                <div id="CancelControl" onclick="$find('mpeSPage').hide();" class="MesgBoxBtnsDisplay rounded_corners5">
                                    Close
                                </div>
                                <div class="clear">
                                </div>
                            </div>
                            <div class="Bottom-doted-Cs">
                                <br />
                            </div>
                            <div style="height:500px; overflow-y:scroll; margin-top:10px;">

                            
                            <div class="SP_LeftAlignDivsCs">
                                <asp:Label ID="lblPageCategory" runat="server" Text="Page Category"></asp:Label>
                            </div>
                            <div class="RightAlignDivsCs">
                                <asp:DropDownList ID="dpPageCat" runat="server" CssClass="dropdown200 rounded_corners5"
                                            AutoPostBack="false"></asp:DropDownList>
                            </div>
                            <div class="clear">
                                &nbsp;
                            </div>
                            <div class="SP_LeftAlignDivsCs">
                                <asp:Label ID="lblUploadBanner" runat="server" Text="Upload banner image"></asp:Label>
                                <br />
                                <p style="font-size: 12px;
color: gray;">(1000 pixels width)</p>
                            </div>
                            <div class="RightAlignDivsCs">
                                <asp:Image ID="imgBanner" runat="server" Style="width: 200px; margin-bottom:10px;"  />
                                <asp:FileUpload ID="fuImageUpload" runat="server" CssClass="file_upload_box210" />
                            </div>
                            <div class="SP_LeftAlignDivsCs">
                                <asp:Label ID="lblHeading" runat="server" Text="Title"></asp:Label>
                            </div>
                            <div class="RightAlignDivsCs">
                                <asp:TextBox ID="txtSPageTitle" runat="server" CssClass="text_box334 rounded_corners"></asp:TextBox>
                            </div>
                            <div class="SP_LeftAlignDivsCs">
                                <asp:Label ID="lblSPageMtitle" runat="server" Text="Enter in meta title"></asp:Label>
                            </div>
                            <div class="RightAlignDivsCs">
                                <asp:TextBox ID="txtMetatitle" runat="server" CssClass="text_box334 rounded_corners"></asp:TextBox>
                            </div>
                            <div class="SP_LeftAlignDivsCs">
                                <asp:Label ID="lblDescCont" runat="server" Text="Enter in the description contents"></asp:Label>
                            </div>
                            <div class="RightAlignDivsCs">
                                <textarea id="txtAreaDescContent" rows="2" cols="40" class="text_box334 rounded_corners" runat="server">
                            </textarea>
                            </div>
                            <div class="clear">
                                &nbsp;
                            </div>
                            <div class="left_align" style=" padding-bottom: 15px;">
                                <asp:Label ID="Label3" runat="server" Text="Page Keywords" CssClass="txtBold Fsize17 left_align"></asp:Label>
                            </div>
                            <div class="Bottom-doted-Cs">
                                <br />
                            </div>
                            <div class="SP_LeftAlignDivsCs">
                                <asp:Label ID="lblPageKeywords" runat="server" Text="Enter in the keywords list"></asp:Label>
                            </div>
                            <div class="RightAlignDivsCs">
                                <textarea id="txtPageKaywords" rows="4" cols="80" class="text_box334 rounded_corners" runat="server">
                            </textarea>
                            </div>
                            <%--<asp:Button ID="LoadKeywords" runat="server" Text="Load keywords" CssClass="start_creating_btn rounded_corners5 H4B" OnClientClick="LoadDefaultKeywords(); return false;" />--%>
                            <div class="clear">
                                &nbsp;
                            </div>
                            
                            <div class="left_align" style=" padding-bottom: 15px;">
                                <asp:Label ID="Label4" runat="server" Text="Meta Details" CssClass="txtBold Fsize17 left_align"></asp:Label>
                            </div>
                            <div class="Bottom-doted-Cs">
                                <br />
                            </div>
                            <div class="SP_LeftAlignDivsCs">
                                <asp:Label ID="lblCatContent" runat="server" Text="Enter in the category content"></asp:Label>
                            </div>
                            <div class="RightAlignDivsCs">
                                <asp:TextBox ID="txtCatContent" runat="server" CssClass="text_box334 rounded_corners"></asp:TextBox>
                            </div>
                            <div class="SP_LeftAlignDivsCs">
                                <asp:Label ID="lblRobotContent" runat="server" Text="Enter in robot content"></asp:Label>
                            </div>
                            <div class="RightAlignDivsCs">
                                <asp:TextBox ID="txtrobotContent" runat="server" CssClass="text_box334 rounded_corners"></asp:TextBox>
                            </div>
                            <div class="SP_LeftAlignDivsCs">
                                <asp:Label ID="lblAuthContent" runat="server" Text="Enter in the author content"></asp:Label>
                            </div>
                            <div class="RightAlignDivsCs">
                                <asp:TextBox ID="txtAuthContent" runat="server" CssClass="text_box334 rounded_corners"></asp:TextBox>
                            </div>
                            <div class="SP_LeftAlignDivsCs">
                                <asp:Label ID="lblLangContent" runat="server" Text="Enter in the language content"></asp:Label>
                            </div>
                            <div class="RightAlignDivsCs">
                                <asp:TextBox ID="txtlangContent" runat="server" CssClass="text_box334 rounded_corners"></asp:TextBox>
                            </div>
                            <div class="SP_LeftAlignDivsCs">
                                <asp:Label ID="lblRevisContent" runat="server" Text="Enter in the revisit after content"></asp:Label>
                            </div>
                            <div class="RightAlignDivsCs">
                                <asp:TextBox ID="txtReVistContent" runat="server" CssClass="text_box334 rounded_corners"></asp:TextBox>
                            </div>
                            <div class="clear">
                                &nbsp;
                            </div>
                            <div class="SP_LeftAlignDivsCs">
                                &nbsp;
                            </div>
                            <div class="RightAlignDivsCs">
                                <asp:Button ID="btnSave" runat="server" CssClass="start_creating_btn rounded_corners5 H4B"
                                    Text="Save" Width="100px" OnClick="btnSave_Click" />&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnCancel" runat="server" CssClass="start_creating_btn rounded_corners5 H4B"
                                    Text="Cancel" Width="100px" OnClientClick="ResetHiddenData();" />
                            </div>
                            <div class="clearBoth">
                                &nbsp;
                            </div>
                                </div>
                        </div>
                    </asp:Panel>
                </div>
                <div>
                    <ajaxToolkit:ModalPopupExtender ID="mpeCkEditor" BehaviorID="mpeCkEditor" TargetControlID="hfCKE"
                        PopupControlID="PnlCKE" BackgroundCssClass="ModalPopupBG" runat="server" Drag="true"
                        DropShadow="false" CancelControlID="btnCloseCKE" />
                    <input type="hidden" id="hfCKE" runat="server" />
                    <asp:Panel ID="PnlCKE" runat="server" CssClass="FileUploaderPopup_Mesgbox LCLB rounded_corners"
                        Style="display: none; width: 750px;">
                        <div style="background: white; padding-top: 15px; padding-bottom: 15px; padding-left: 15px;
                            padding-right: 15px;" class="white_background">
                            <div class="left_align" style="padding-bottom: 5px;">
                                <div class="float_left_simple spacer10pxtop">
                                    <asp:Label ID="Label5" runat="server" Text="Edit Page Html" CssClass="Fsize17 left_align"></asp:Label>
                                </div>
                                <div id="btnCloseCKE" onclick="$find('mpeSPage').hide();" class="MesgBoxBtnsDisplay rounded_corners5">
                                    Close
                                </div>
                                <div class="clear">
                                </div>
                            </div>
                            <div class="Bottom-doted-Cs">
                                <br />
                            </div>
                            <div id="lodingBar" style="text-align: center; display: inline-block; height:415px;">
                                <CKEditor:CKEditorControl ID="txtPageEditor" BasePath="../tools/ckeditor" 
                                    Width="720" runat="server"></CKEditor:CKEditorControl>
                            </div>
                            <br />
                            <asp:Button ID="btnSavePageHtml" runat="server" Text="Save" CssClass="start_creating_btn rounded_corners5 float_right MargnRght10"
                                OnClick="btnSavePageHtml_Click" />
                            <div class="clearBoth">
                                &nbsp;
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hfChangeScenarioToSaveNewPage" runat="server" Value="0" />
    <asp:HiddenField ID="hfSPageID" runat="server" />
    <asp:HiddenField ID="hfDefaultKeywords" runat="server" />
    <asp:Button ID="HiddenbtnSPages" runat="server" OnClick="HiddenbtnSPages_Click" Style="display: none;" />
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {


            $("#<%= grdViewBrokerSPages.ClientID%> tr").children(":first").css("border-top-left-radius", 10, "-moz-border-top-left-radius", 10,
             "-webkit-border-top-left-radius", 10, "-khtml-border-top-left-radius", 10);

            $("#<%= grdViewBrokerSPages.ClientID%> tr").children(":first").next().next().next().css("border-top-right-radius", 10, "-moz-border-top-right-radius", 10,
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

        function ShowSPagePopUp() {

            $('#<%=imgBanner.ClientID %>').css('display', 'none');
            $('#<%=txtSPageTitle.ClientID %>').val('');
            $('#<%=txtMetatitle.ClientID %>').val('');
            $('#<%=txtAreaDescContent.ClientID %>').val('');
                 
            $('#<%=txtPageKaywords.ClientID %>').val('');
            $('#<%=txtCatContent.ClientID %>').val('');
            $('#<%=txtrobotContent.ClientID %>').val('');

            $('#<%=txtAuthContent.ClientID %>').val('');
            $('#<%=txtlangContent.ClientID %>').val('');
            $('#<%=txtReVistContent.ClientID %>').val('');


            $('#<%=btnSave.ClientID %>').val('Save');
            $('#<%=hfChangeScenarioToSaveNewPage.ClientID %>').val('1');
            $find('mpeSPage').show();
        }
        function ResetHiddenData() {
            $('#<%=hfChangeScenarioToSaveNewPage.ClientID %>').val('0');
        }
//        function LoadDefaultKeywords() {
//            var Val = $('#<%=hfDefaultKeywords.ClientID %>').val();
//            $('#<%=txtPageKaywords.ClientID %>').text(Val);
//            return false;
//        }
        function HideBannerWiget() {
            $('#<%=SPageContainer.ClientID %>').css("display", "none");
        }
        function ShowBannerWiget() {
            var LoadPages = '<%=SPagesAvailable %>';
            if (LoadPages == 0) {
                $('#<%=SPageContainer.ClientID %>').css("display", "block");
                $('#<%=HiddenbtnSPages.ClientID %>').click();
            } else {
                $('#<%=SPageContainer.ClientID %>').css("display", "block");
            }

        }
    </script>
</asp:Content>
