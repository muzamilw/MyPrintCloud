<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ThemeSite.Master" AutoEventWireup="true"
    MaintainScrollPositionOnPostback="true" CodeBehind="ProductDesignTemplate.aspx.cs"
    Inherits="Web2Print.UI.ProductDesignTemplate1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Controls/BreadCrumbMenu.ascx" TagName="BreadCrumbMenu" TagPrefix="uc1" %>
<%@ Register Src="Controls/WhyChooseUs.ascx" TagName="WhyChooseUs" TagPrefix="uc4" %>
<%@ Register Src="Controls/LoginControl.ascx" TagName="LoginControl" TagPrefix="uc2" %>
<%@ Register Src="Controls/MatchingSet.ascx" TagName="MatchingSet" TagPrefix="uc3" %>
<asp:Content ID="ProductDetailsContents" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../LightBox/css/jquery.lightbox-0.5.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {

            BindEvents();
        });

        function BindEvents() {
            $('.gallery a').lightBox({
                maxHeight: 500,
                maxWidth: 700
            });

            // Bind Show progress
            //   $('.ImgCont').click(showProgress());
        }

        function showProgressbar() {
            showProgress();
        }

        function LoginPopUp() {
            var CheckLogin = '<%=IsLogin %>';
            if (parseInt(CheckLogin) > 0) {
                return true;
            }
            else {
                var returnUrl = '<%=ReturnUrl %>';
                var message = '<div style="text-align:left;padding-left:90px;">Add to Favorites to mark this template as a favorites in your profile, please register or login first</div><br><a href="../Signup.aspx?' + returnUrl + '" style="padding-right:20px;">Register</a><a href="../Login.aspx?' + returnUrl + '">Login</a>';

                ShowPopup('Message', message);
                return false;
            }
        }
        function showProgress() {
            var shadow = document.getElementById("divShd");
            var bws = getBrowserHeight();
            shadow.style.width = bws.width + "px";
            shadow.style.height = bws.height + "px";
            var left = parseInt((bws.width - 350) / 2);
            var top = parseInt((bws.height - 200) / 2);
            //shadow = null;
            $('#divShd').css("display", "block");
            $('#UpdateProgressUserProfile').css("display", "block");
            return true;
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
    <div id="divShd" class="opaqueLayer">
    </div>
    <div class="content_area" style="z-index: 1;">
        <div class="left_right_padding">
            <uc1:BreadCrumbMenu ID="BreadCumbMenuCategory" runat="server" />
            <div class="signin_heading_div_float_left">
                <asp:Label ID="lblMainHeading" runat="server" CssClass="sign_in_heading float_left_simple"></asp:Label>
            </div>
            <div class="clearBoth">
                &nbsp;</div>
            <div class="template_designing">
                <div class="float_left_simple ">
                    <div class="get_in_touch_box_ProdDetail rounded_corners">
                        <div>
                            <table width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td width="13%" valign="top" style="text-align: center;" class="minPriceBox">
                                        <div class="LightGrayLabels PriceHeadingCs">
                                            <asp:Label ID="lblPricetxt" runat="server" Text="Prices"></asp:Label>
                                            <asp:Label ID="lblMinPrincs" runat="server" Text="start from"></asp:Label>
                                            <asp:Label ID="lblMinPriceOFPro" runat="server" CssClass="PRiceCS"></asp:Label>
                                        </div>
                                        <asp:Button ID="btnMoreONProdt" runat="server" CssClass="btn_upload_design rounded_corners5"
                                            Text="Full prices & options" OnClick="MoreAboutProduct_Click" />
                                    </td>
                                    <td rowspan="3" colspan="1" width="1%">
                                        <asp:Image ID="btnarrowImg_SP" runat="server" ImageUrl="~/images/arrow.gif"  />
                                    </td>
                                   <td width="28%" valign="top" class="minPriceBox" style="text-align: left;padding-left:25px;padding-right:7px;">
                                        <div>
                                            <asp:Label ID="ltrlOpt1" runat="server" CssClass="float_left_simple font_size16 LightGrayLabels"></asp:Label></div>
                                        <div>
                                            <img id="imgDownArrow" runat="server" src="images/Down-arrow.PNG" alt="" class="float_right DownArrowCs" /></div>
                                        <br />
                                        <div style="clear:left; margin-top: 10px;"><asp:Label ID="lblSubHeading" runat="server" Text="Select a Template from below"
                                            CssClass="DownArrowHeadingCS"></asp:Label></div>
                                        <asp:Label ID="ltrlClcktempnStart" runat="server" CssClass="LightGrayLabels"></asp:Label><br />
                                        <asp:Label ID="ltrlDesignonline" runat="server" Text="Click on a template to preview or edit"
                                            CssClass="LightGrayLabels"></asp:Label>
                                    </td>
                                    <td rowspan="3" colspan="1" width="1%">
                                       <asp:Image ID="Image1" runat="server" ImageUrl="~/images/arrow.gif"  />
                                    </td>
                                    <td width="28%" valign="top" class="minPriceBox" style="text-align: left;padding-left:25px;padding-right:7px;">
                                        <div>
                                            <asp:Label ID="ltrloption2" runat="server" CssClass="float_left_simple font_size16 LightGrayLabels"></asp:Label></div>
                                        <div>
                                            <img id="imgsupplyarrow" runat="server" src="~/images/artwork_arrow_icons.png" alt=""
                                                class="float_right SupplyAroowCs" /></div>
                                        <br />
                                        <div style="clear:left; margin-top: 10px;">
                                        <asp:Label ID="Label2" runat="server" CssClass="SupplyArrowDescCs"></asp:Label>
                                        </div>
                                        <asp:Label ID="ltrlUploaddesign" runat="server" Text="Upload a design that you have and would" CssClass="LightGrayLabels"></asp:Label>
                                        <br />
                                        <asp:Label ID="Label1" runat="server" Text="like us to print/or modify" CssClass="LightGrayLabels"></asp:Label>
                                        <asp:Button ID="btnUploadDesign" runat="server" CssClass="btn_upload_design rounded_corners5"
                                            CausesValidation="False" Text="Upload Design >" OnClientClick="showProgressbar();" />
                                    </td>
                                     <td rowspan="3" colspan="1" width="1%">
                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/images/arrow.gif"  />
                                    </td>
                                   <td width="28%" valign="top" class="minPriceBox" style="text-align: left;padding-left:25px;padding-right:7px;">
                                        <div>
                                            <asp:Label ID="ltrOpt3" runat="server" CssClass="float_left_simple font_size16 LightGrayLabels"></asp:Label></div>
                                        <div>
                                            <img id="img2" runat="server" src="~/images/artwork_light_icons.png" alt="" class=" float_right lightIconCs" /></div><br />
                                        <div style="clear:left; margin-top: 10px;"><asp:Label ID="lblpopIn" runat="server" Text="We can design your artwork" CssClass="lightIconHeadingCS"></asp:Label></div>
                                        <asp:Label ID="ltrlSeendesignn" runat="server" Text="Call us for a free consultation" CssClass="LightGrayLabels"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <br />
                    <br />
                    <asp:Button ID="DefualtBTn" runat="server" Style="display: none;" />
                    <asp:Panel ID="pnlSearchBox" runat="server" DefaultButton="DefualtBTn" CssClass="search_box rounded_corners"
                        Style="width: 100%; visibility: hidden; height: 1px;">
                        <div class="product_detail_sup_padding">
                            <%-- <div class="search_template_heading">
                            Search templates by...
                        </div>
                        <br />--%>
                            <table width="100%" cellpadding="2">
                                <tr id="Tr1" runat="server" visible="false">
                                    <td>
                                        <asp:Literal ID="ltrlIndusrtyType" runat="server" Text="Industry Type"></asp:Literal>
                                    </td>
                                    <td>
                                        <%--   <asp:DropDownList ID="cmbIndustryTypes" runat="server" CssClass="dropdown160 rounded_corners5">
                                        <asp:ListItem>Select Industry Type</asp:ListItem>
                                    </asp:DropDownList>--%>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblStyle" runat="server" Text="Style" Visible="false"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cmbStyles" runat="server" CssClass="dropdown160 rounded_corners5"
                                            Visible="false" Enabled="false">
                                            <asp:ListItem>Select Style</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Text="Occassion" Visible="false"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="dropdown160 rounded_corners5"
                                            Visible="false" Enabled="false">
                                            <asp:ListItem>Select Occassion</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td colspan="2">
                                        <asp:CheckBox ID="chkBelongsToMatchingSet" runat="server" CssClass="rounded_corners5"
                                            Visible="false" Text="Part of matching set" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="fontSyleBold" style="white-space: nowrap;" width="10%">
                                        <asp:Literal ID="ltrlSearchphrase" runat="server" Text="Search Phrase"></asp:Literal>
                                    </td>
                                    <td width="40%">
                                        <asp:Image ID="searchImg" runat="server" ImageUrl="~/images/toolbar_find.png" CssClass="SearchBtnJs" />
                                        <asp:HiddenField runat="server" ID="txtTemplateDesignerMappedCategoryName" />
                                        <asp:TextBox ID="txtSearchKeywords" runat="server" class="text_box185_ProdDesgn rounded_corners5 textBoxClearJS"
                                            onclick="closebtnAppers();" onKeyDown="javascript:IfEnter();" AutoPostBack="true"
                                            OnTextChanged="btnSearchText_Click" Text=""></asp:TextBox>
                                        <asp:ImageButton ID="closeBtnImg" runat="server" ImageUrl="~/images/close-icon.png"
                                            OnClick="btnReset_Click" CssClass="ClosebtnJS" />
                                        <asp:Button ID="btnSubmit" runat="server" Text="Search" OnClick="btnSearchText_Click"
                                            CssClass="silver_back_button" Style="display: none;" />
                                        <asp:Button ID="btnReset" runat="server" Text="Clear" CssClass="silver_back_button"
                                            OnClick="btnReset_Click" Style="display: none;" />
                                    </td>
                                    <td width="15%">
                                        <asp:Literal ID="ltrlIndusrtyType2" runat="server" Text="Industry Type"></asp:Literal>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cmbIndustryTypes" runat="server" CssClass="dropdown160 rounded_corners5"
                                            AutoPostBack="true" OnSelectedIndexChanged="cmbIndustryTypes_SelectedIndexChanged">
                                            <asp:ListItem>Select Industry Type</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td colspan="3">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:Panel>
                </div>
                <div class="clearBoth">
                    &nbsp;
                </div>
                <div class="paging_design">
                    <div class="float_left total_record_heading">
                        <asp:Label ID="lblTotalRecords" runat="server" Font-Bold="true"></asp:Label>&nbsp;<asp:Literal
                            ID="ltrlMatchesfound" runat="server" Text="matches found"></asp:Literal>
                    </div>
                    <div class="float_right">
                        <table id="tblPaging" runat="server">
                            <tr>
                                <td>
                                    <asp:Literal ID="ltrlShowingPdT" runat="server" Text="Showing"></asp:Literal>
                                    <b>
                                        <asp:Literal ID="PagerCurrentPage" runat="server"></asp:Literal></b> of <b>
                                            <asp:Literal ID="PagerTotalPages" runat="server"></asp:Literal></b> Pages
                                </td>
                                <td>
                                    <asp:Button ID="PagerFirstButton" runat="server" Enabled="false" CssClass="next_button rounded_corners5"
                                        Text="|<" OnClick="PagerFirstButton_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="PagerPreviousButton" runat="server" Enabled="false" CssClass="next_button rounded_corners5"
                                        Text="<" OnClick="PagerPreviousButton_Click" />
                                </td>
                                <td>
                                    <asp:HiddenField runat="server" ID="PagerCurrentPagehd" />
                                    <asp:Button ID="btn1" runat="server" CssClass="selected_next_button rounded_corners5"
                                        Text="1" Visible="false" OnClick="btn1_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btn2" runat="server" CssClass="next_button rounded_corners5" Text="2"
                                        Visible="false" OnClick="btn1_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btn3" runat="server" CssClass="next_button rounded_corners5" Text="3"
                                        Visible="false" OnClick="btn1_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="PagerNextButton" runat="server" Enabled="false" CssClass="next_button rounded_corners5"
                                        Text=">" OnClick="PagerNextButton_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="PagerLastButton" runat="server" Enabled="false" CssClass="next_button rounded_corners5"
                                        Text=">|" OnClick="PagerLastButton_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="clearBoth">
                    &nbsp;</div>
                <asp:DataList ID="dlDesignerTemplates" OnItemDataBound="dlDesignerTemplates_OnItemDataBound"
                    runat="server" RepeatColumns="3" RepeatDirection="Vertical" BorderStyle="None"
                    ShowFooter="False" ShowHeader="False" CellSpacing="1" CellPadding="0" OnItemCommand="dlDesignerTemplates_ItemCommand">
                    <ItemTemplate>
                        <div class="LCLB border_div rounded_corners">
                            <div class="pad5 PDTCWB">
                                <div class="product_design_thumbnail_container_back rounded_corners">
                                    <div class="product_template_design_thumbnail_container">
                                        <asp:LinkButton ID="hlImageCommand" runat="server" CssClass="ImgCont" CommandName="DesignOnline"
                                            CommandArgument='<%#Eval("ProductID","{0}") %>'>
                                            <asp:Image ID="imgPic1" runat="server" ImageUrl='<%#Eval("Thumbnail","{0}") %>' />
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="product_selcection_thumnail_button_container gallery">
                                <asp:HyperLink ID="hlImage1" runat="server" ToolTip="Click to view enlarged design">
                                    <div class="magnifying_glass_image">
                                        <asp:Image ID="imgPic2" runat="server" Style="display: none;" />
                                    </div>
                                </asp:HyperLink>
                            </div>
                            <div class="WHOfDivMS">
                                <asp:HyperLink ID="hlMSet" runat="server" NavigateUrl="#" ToolTip="Click to view Matching Sets">
                                    <asp:Label ID="lbl" runat="server" Text="M" CssClass="MSCLAss"></asp:Label>
                                </asp:HyperLink>
                            </div>
                            <div class="product_selcection_thumnail_button_container_right">
                                <div id="divFavoriteInd" runat="server" class="passive_star" title="Click to Add as Favorite">
                                    &nbsp;
                                </div>
                                <asp:HiddenField ID="hfTemplateId" runat="server" Value='<%#Eval("ProductID") %>' />
                            </div>
                            <div class="clearBoth">
                                &nbsp;</div>
                            <div class="product_detail_image_heading">
                                <asp:Label ID="lblProductName" runat="server" Text='<%#Eval("ProductName","{0}") %>'
                                    CssClass="themeFontColor"></asp:Label>
                            </div>
                        </div>
                    </ItemTemplate>
                    <ItemStyle VerticalAlign="Top" />
                </asp:DataList>
                <asp:Label ID="lblTemplateNotfound" CssClass="simpleText" runat="server" Visible="false"></asp:Label>
                <div class="paging_design">
                    <div class="float_right">
                        <table id="tblPagingB" runat="server">
                            <tr>
                                <td>
                                    <asp:Literal ID="ltrlShowing2" runat="server" Text="Showing"></asp:Literal>
                                    <b>
                                        <asp:Literal ID="ltrCurrentPage" runat="server"></asp:Literal></b> of <b>
                                            <asp:Literal ID="ltrTotalPages" runat="server"></asp:Literal></b> Pages
                                </td>
                                <td>
                                    <asp:Button ID="PagerFirstButtonB" runat="server" Enabled="false" CssClass="next_button rounded_corners5"
                                        Text="|<" OnClick="PagerFirstButton_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btnPreviousB" runat="server" Enabled="false" CssClass="next_button rounded_corners5"
                                        Text="<" OnClick="PagerPreviousButton_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btn1b" runat="server" CssClass="selected_next_button rounded_corners5"
                                        Text="1" Visible="false" OnClick="btn1_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btn2b" runat="server" CssClass="next_button rounded_corners5" Text="2"
                                        Visible="false" OnClick="btn1_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btn3b" runat="server" CssClass="next_button rounded_corners5" Text="3"
                                        Visible="false" OnClick="btn1_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="PagerNextButtonB" runat="server" Enabled="false" CssClass="next_button rounded_corners5"
                                        Text=">" OnClick="PagerNextButton_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="PagerLastButtonB" runat="server" Enabled="false" CssClass="next_button rounded_corners5"
                                        Text=">|" OnClick="PagerLastButton_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="clearBoth">
                    &nbsp;</div>
            </div>
            <%--Hidden Fielsds--%>
            <asp:HiddenField ID="hfSelectedBaseColor" runat="server" Value="0" />
            <asp:HiddenField ID="hfSelectedItem" runat="server" Value="0" />
            <asp:HiddenField ID="hfContactId" runat="server" />
            <asp:HiddenField ID="hfItemId" runat="server" />
            <asp:HiddenField ID="hfCategoryId" runat="server" />
            <asp:HiddenField ID="txtHiddenCorpItemTemplateID" Value='0' runat="server" />
        </div>
        <br />
        <br />
        <br />
    </div>
    <asp:Button ID="btnTemplatePopup" runat="server" Style="display: none; width: 0px;
        height: 0px" />
    <ajaxToolkit:ModalPopupExtender ID="mpeTemplateDesgn" runat="server" BackgroundCssClass="ModalPopupBG"
        PopupControlID="pnlTemplateDesgn" TargetControlID="btnTemplatePopup" BehaviorID="mpeTemplateDesgn"
        CancelControlID="btnCancelMessageBox" DropShadow="false">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlTemplateDesgn" runat="server" Width="717px" Height="488px" CssClass="FileUploaderPopup_Mesgbox LCLB rounded_corners"
        Style="display: none">
        <div class="Width100Percent" style="margin-top: -4px;">
            <div class="exit_container">
                <div id="btnCancelMessageBox" runat="server" class="exit_popup3" onclick="HideBar();">
                </div>
            </div>
        </div>
        <div id="MAinContentdiv" class="pop_body_MesgPopUp_Tepmlate innerBodyTemplateCS">
            <div class="float_left Width100Percent textAlignLeft">
                <asp:Label ID="lblHeader" runat="server" CssClass="FileUploadHeaderText_Tepmlate"></asp:Label>
                <asp:Label ID="lblMoreAbout" CssClass="custom_color left_align cursor_pointer float_right MoreAboutDesignCS"
                    Text="More about this product" runat="server" onclick="AlertPopUP();"></asp:Label>
                <br />
                <br />
                <div class="top_sub_section_bottom_space_Pink">
                    <br />
                </div>
            </div>
            <div>
                <div class="float_left_simple">
                    <asp:Label ID="lblTemplatePageName1" runat="server" CssClass="TemplateName1CS"></asp:Label></div>
                <asp:Label ID="lblTemplatePageName2" runat="server" CssClass="TemplateName2CS"></asp:Label>
            </div>
            <div class="clearBoth">
                &nbsp;
            </div>
            <div class="LCLB BD_Temp">
                <div class="pad5">
                    <div class="PDTCWB_Temp">
                        <div class="PDTC_Temp FI_Temp">
                            <img id="imgTempPage1" alt="" class="full_img_ThumbnailPath_Temp" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="LCLB BD_Temp SecondTemplatediv">
                <div class="pad5">
                    <div class="PDTCWB_Temp">
                        <div class="PDTC_Temp FI_Temp">
                            <img id="imgTempPageN" alt="" class="full_img_ThumbnailPath_Temp" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="clearBoth">
                &nbsp;
            </div>
            <div class="spacer10pxtop">
                <asp:Button ID="btnEditThisDesign" runat="server" Text="Make this pack" CssClass="btn_upload_design textRighFloating rounded_corners5"
                    OnClick="btnPackThisDesgn_Click" OnClientClick="return ShowMessage();" Style="margin-right: 21px;" />
            </div>
            <div class="clearBoth">
                &nbsp;
            </div>
            <div class="left_align" style="color: #9594ab;">
                <asp:Label ID="lrtlMultiFc" runat="server" Text="Multi Back (Choose upto 5 different backs, same price, click on image to preview)"></asp:Label>
            </div>
            <div id="SubContent">
            </div>
        </div>
    </asp:Panel>
    <asp:Button ID="btnMoreAboutThisDesgn" runat="server" OnClick="btnMoreAboutThisDesgn_Click"
        OnClientClick="showProgressbar();" Style="display: none;" />
    <asp:HiddenField ID="hfProductTemplateID" runat="server" />
    <asp:HiddenField ID="hfTempProductName" runat="server" />
    <asp:HiddenField ID="hfEditTempType" runat="server" />
    <asp:HiddenField runat="server" ID="hfFTempName" Value=" " />
    <asp:HiddenField runat="server" ID="hfPageItemID" Value="0" />
    <asp:HiddenField runat="server" ID="hfFTemplateId" Value="0" />
    <asp:HiddenField ID="hfState" runat="server" ClientIDMode="Predictable" />
    <script type="text/javascript">

        $(document).ready(function () {

            $('.passive_star').toggle(function () {
                if (LoginPopUp()) {

                    $(this).removeClass('passive_star').addClass('active_star');
                    $(this).attr('title', 'Click to remove favorite');
                    var templateId = $(this).next().val();
                    SavedFarorite(templateId, true);
                    var FavcountValu = $('.FavCount').text();
                    var ReplceFav = FavcountValu.replace("(", " ");
                    var FinalFavCount = ReplceFav.replace(")", " ");
                    var Count = "(" + (parseInt(FinalFavCount, 10) + 1) + ")";
                    $('.FavCount').html(Count);
                }
            }, function () {
                if (LoginPopUp()) {
                    $(this).removeClass('active_star').addClass('passive_star');
                    $(this).attr('title', 'Click to add favorite');
                    var templateId = $(this).next().val();
                    SavedFarorite(templateId, false);
                    var FavcountValu = $('.FavCount').text();
                    var ReplceFav = FavcountValu.replace("(", " ");
                    var FinalFavCount = ReplceFav.replace(")", " ");
                    var Count = "(" + (parseInt(FinalFavCount, 10) - 1) + ")";
                    $('.FavCount').html(Count);
                }
            }
            );

            $('.active_star').toggle(function () {
                if (LoginPopUp()) {
                    $(this).removeClass('active_star').addClass('passive_star');
                    $(this).attr('title', 'Click to add favorite');
                    var templateId = $(this).next().val();
                    SavedFarorite(templateId, false);
                    var FavcountValu = $('.FavCount').text();
                    var ReplceFav = FavcountValu.replace("(", " ");
                    var FinalFavCount = ReplceFav.replace(")", " ");
                    var Count = "(" + (parseInt(FinalFavCount, 10) - 1) + ")";
                    $('.FavCount').html(Count);
                }
            }, function () {
                if (LoginPopUp()) {
                    $(this).removeClass('passive_star').addClass('active_star');
                    $(this).attr('title', 'Click to remove favorite');
                    var templateId = $(this).next().val();
                    SavedFarorite(templateId, true);
                    var FavcountValu = $('.FavCount').text();
                    var ReplceFav = FavcountValu.replace("(", " ");
                    var FinalFavCount = ReplceFav.replace(")", " ");
                    var Count = "(" + (parseInt(FinalFavCount, 10) + 1) + ")";
                    $('.FavCount').html(Count);
                }
            }
            );

        });

        function SavedFarorite(templateId, isFavorite) {

            var contactId = $('#<%=hfContactId.ClientID %>').val();
            var itemId = $('#<%=hfItemId.ClientID %>').val();
            var categoryId = $('#<%=hfCategoryId.ClientID %>').val();


            if (parseInt(contactId) > 0) {

                Web2Print.UI.Services.WebStoreUtility.AddUpdateFavDesign(templateId, itemId, contactId, categoryId, isFavorite, OnSuccess);
            }
        }
        function OnSuccess(responce) {

        }


        $(document).ready(function () {

            var selectedItemId = $('#<%=hfSelectedItem.ClientID %>').val();
            $('#' + selectedItemId).removeClass().addClass('base_color_active');
        });


        function SelectedColor(colorId, sender) {
            $('#<%=hfSelectedBaseColor.ClientID %>').val(colorId);
            $('#<%=hfSelectedItem.ClientID %>').val(sender.id);
            $('.base_color_active').removeClass().addClass('base_color_passive');
            $(sender).removeClass().addClass('base_color_active');
        }

        function ClearBaseColor() {
            $('.base_color_active').removeClass().addClass('base_color_passive');
            $('#<%=hfSelectedBaseColor.ClientID %>').val('0');
            $('#<%=hfSelectedItem.ClientID %>').val('');
            // __doPostBack('<%=btnSubmit.UniqueID %>', '');
        }


        function openMS(templatename, ProductCategoryId, ItemId) {
            var shadow = document.getElementById("divShd");
            var bws = getBrowserHeight();
            shadow.style.width = bws.width + "px";
            shadow.style.height = bws.height + "px";
            var left = parseInt(($(window).width() - 780) / 2);
            var top = parseInt(($(window).height() - 510) / 2);
            $('#divShd').css("display", "block");
            //shadow = null;
            $('#jqwin').css("position", "fixed");
            $('#jqwin').css("left", left);
            $('#jqwin').css("top", top);
            $('#jqwin').css("background-color", "transparent");
            var html = '<div class="closeBtn" onclick="closeMS()"> </div>';
            $('#jqwin').html(html + '<iframe id="ifrm" style="width:780px; height:510px; padding:5px; padding-bottom:0px; border: none;" class="rounded_corners LCLB"></iframe>').dialog();
            $("#jqwin>#ifrm").attr("src", '../matchingsetpopup.aspx?templatename=' + templatename + "&ProductCategoryId=" + ProductCategoryId + "&ItemID=" + ItemId);
            $('#jqwin').show();
            $(".closeBtn").css("display", "block");
            return false;
        }

        function closeMS() {
            $(".ui-dialog-titlebar-close").click();
            $(".closeBtn").css("display", "none");
            $('#divShd').css("display", "none");
            $('#jqwin').hide();
        }

        function AlertPopUP() {
            $('#<%=btnMoreAboutThisDesgn.ClientID %>').click();
        }
    </script>
    <div id="jqwin" class="FileUploaderPopup_Mesgbox" style="position: fixed;">
        <div class="clear">
        </div>
    </div>
    <script src="http://code.jquery.com/ui/1.9.0/jquery-ui.js"></script>
    <script type="text/javascript">
        function closebtnAppers() {
            if ($('.textBoxClearJS').val() == "") {

            }
            else {
                $('.ClosebtnJS').css("visibility", "visible");
            }
        }
        function IfEnter() {
            if (event.keyCode == 13) {

            } else {
                $('.ClosebtnJS').css("visibility", "visible");
            }
        }
    </script>
    <script type="text/javascript">
        var ItemID = 0;
        var orderId = 0;
        var count = 1;
        function FlowChanges(templatename, ProductId) {
            showProgress();
            $('#<%=hfProductTemplateID.ClientID %>').val(ProductId);
            $('#<%=hfTempProductName.ClientID %>').val(templatename);
            var Html = null;
            var length = null;
            var Flow = '<%= IsFlowChanged %>';
            if (Flow == "2") {
                $.get("/Services/Webstore.svc/resumeproduct?itemid=" + ItemID + "&orderid=" + orderId + "&templateid=" + ProductId,
                function (data) {
                    $('#<%=hfEditTempType.ClientID %>').val(data);
                });
                $('#<%=lblHeader.ClientID %>').html(templatename);

                $.getJSON("http://designerv2.myprintcloud.com/services/TemplatePagesSvc/" + ProductId,
	            function (DT) {
	                length = DT.length;
	                if (length > 2) {
	                    $('#<%=lrtlMultiFc.ClientID %>').css("display", "block");
	                    $('.SecondTemplatediv').css("display", "block");
	                    $('#<%=lblTemplatePageName1.ClientID %>').html(DT[0].PageName);
	                    $('#<%=lblTemplatePageName2.ClientID %>').html(DT[1].PageName);

	                    document.getElementById("imgTempPage1").src = 'http://designerv2.myprintcloud.com//designer/products/' + ProductId + '/p' + 1 + '.png';
	                    document.getElementById("imgTempPageN").src = 'http://designerv2.myprintcloud.com//designer/products/' + ProductId + '/p' + 2 + '.png';
	                    document.getElementById("SubContent").innerHTML = "";
	                    if (length > 6) {
	                        for (i = 2; i <= 6; i++) {

	                            var src = "http://designerv2.myprintcloud.com//designer/products/" + ProductId + "/p" + i + ".png";
	                            document.getElementById("SubContent").innerHTML += "<div><div class='LCLB TemplesDiv MTop15_Temp'><div class=pad5><div class=PDTCWB_Temp><div class='PDTC_TemplesDiv FI_TemplesDiv ImageClick' onclick=ChangeImage('" + src + "') ><img id=TempPage" + i + " src =" + src + " class=full_img_ThumbnailPath_Temp_Div /></div></div></div><div class=TemplateNameNCS ><span id=lblPageName" + i + ">" + DT[i].PageName + "</span></div></div></div>";

	                        }
	                    }
	                    else {
	                        count = 1;
	                        for (i = 2; i <= length; i++) {

	                            var src = "http://designerv2.myprintcloud.com//designer/products/" + ProductId + "/p" + i + ".png";
	                            document.getElementById("SubContent").innerHTML += "<div><div class='LCLB TemplesDiv MTop15_Temp'><div class=pad5><div class=PDTCWB_Temp><div class='PDTC_TemplesDiv FI_TemplesDiv ImageClick' onclick=ChangeImage('" + src + "') ><img id=TempPage" + i + " src =" + src + " class=full_img_ThumbnailPath_Temp_Div /></div></div></div><div class=TemplateNameNCS ><span id=lblPageName" + i + ">" + DT[count].PageName + "</span></div></div></div>";
	                            count = count + 1;

	                        }
	                    }
	                }
	                if (length == "1") {
	                    $('#<%=lrtlMultiFc.ClientID %>').css("display", "none");
	                    $('.SecondTemplatediv').css("display", "none");
	                    $('#<%=lblTemplatePageName1.ClientID %>').html(DT[0].PageName);
	                    document.getElementById("imgTempPage1").src = 'http://designerv2.myprintcloud.com//designer/products/' + ProductId + '/p1' + '.png';
	                    document.getElementById("SubContent").innerHTML = "";
	                }
	                if (length == "2") {
	                    $('#<%=lrtlMultiFc.ClientID %>').css("display", "none");
	                    $('.SecondTemplatediv').css("display", "block");
	                    $('#<%=lblTemplatePageName1.ClientID %>').html(DT[0].PageName);
	                    $('#<%=lblTemplatePageName2.ClientID %>').html(DT[1].PageName);
	                    document.getElementById("imgTempPage1").src = 'http://designerv2.myprintcloud.com//designer/products/' + ProductId + '/p1' + '.png';
	                    document.getElementById("imgTempPageN").src = 'http://designerv2.myprintcloud.com//designer/products/' + ProductId + '/p2' + '.png';
	                    var src = "http://designerv2.myprintcloud.com//designer/products/" + ProductId + "/p" + "2" + ".png";
	                    document.getElementById("SubContent").innerHTML = document.getElementById("SubContent").innerHTML = "<div><div class='LCLB TemplesDiv MTop15_Temp'><div class=pad5><div class=PDTCWB_Temp><div class='PDTC_TemplesDiv FI_TemplesDiv ImageClick' onclick=ChangeImage('" + src + "') ><img id=TempPage" + "2" + " src =" + src + " class=full_img_ThumbnailPath_Temp_Div /></div></div></div><div class=TemplateNameNCS ><span id=lblPageName2>" + DT[1].PageName + "</span></div></div></div>";
	                }
	                $find('mpeTemplateDesgn').show();
	            });

                return false;
            } else {
                showProgressbar();
                return true;
            }
        }

        function ChangeImage(src) {
            var srcOfIMage = src;
            document.getElementById("imgTempPageN").src = srcOfIMage;

        }

        function HideBar() {
            $('#divShd').css("display", "none");
            $('#UpdateProgressUserProfile').css("display", "none");
        }

        function ShowMessage() {
            var type = $('#<%=hfEditTempType.ClientID %>').val();
            // alert(type);
            if (type == "NoTemplate") {
                showProgress();
                return true;
            } else if (type == "SameTemplate") {
                //alert("2");
                ShowPopup2("Alert!", "You already have the same template in designer mode Would you like to resume your design or start from scratch ?");
                return false;
            } else if (type == "SameItem") {
                // alert();
                ShowPopup2("Alert!", "you already have another template in designer mode. Would you like to continue modifiing the design or create new ");
                return false;
            }
            return false;
            //
        }

    </script>
</asp:Content>
