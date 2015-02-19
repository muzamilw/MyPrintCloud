<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ThemeSite.Master" AutoEventWireup="true"
    CodeBehind="Category.aspx.cs" Inherits="Web2Print.UI.Category" %>

<%@ Register Src="~/Controls/BreadCrumbMenu.ascx" TagName="BreadCrumbMenu" TagPrefix="uc1" %>
<%@ Register Src="~/Controls/MainHeading.ascx" TagName="MainHeading" TagPrefix="uc3" %>
<asp:Content ID="CatPageHead" ContentPlaceHolderID="HeadContents" runat="server">
</asp:Content>
<asp:Content ID="PageBanner" ContentPlaceHolderID="PageBanner" runat="server">
</asp:Content>
<asp:Content ID="CatPageMainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="divShd" class="opaqueLayer">
    </div>
    <div runat="server" id="divbg" visible="false" class="categorydivBG">&nbsp;</div>
    <div class="container content_area">

        <div class="left_right_padding row">
            <uc1:BreadCrumbMenu ID="BreadCumbMenuCategory" runat="server" />
            <div class="signin_heading_div Categorytitle col-md-12 col-lg-12 col-xs-12">
                <h1>
                    <asp:Label ID="lblTitle" runat="server" CssClass="sign_in_catheading"></asp:Label>
                </h1>

            </div>
            <div id="productCategoryContainer" class="col-md-12 col-lg-12 col-xs-12">
                <asp:Label ID="lblCategoryHeader" runat="server" class="product_detail_sub_heading custom_color floatleft clearBoth MarginBottom30px"
                    Text="Select a product category below" Visible="false"></asp:Label>
            </div>
            <div class="cntParentCategDesc col-md-12 col-lg-12 col-xs-12">
                <asp:Label ID="lblParentCategoryDesc" runat="server"></asp:Label>
            </div>
            <div id="CategoryDisplay responsivePad15"><%--style="width: 1000px;"--%>
                <div class="rounded_corners clearBoth">
                    <div id="pnlCatList" runat="server" class="padd_bottom_30 ">
                        <asp:Repeater ID="dlCategories" runat="server" OnItemDataBound="dlCategories_ItemDataBound">
                            <ItemTemplate>
                                <div id="Maincontainer" class="CAT_Body rounded_corners" runat="server">
                                    <div class="LCLB">
                                        <asp:HyperLink ID="hlCategory" runat="server">
                                            <div class="PDTC_CAT FI_CAT">
                                                <asp:Image ID="imgThumbnail" CssClass="CAT_ThumbnailPath" runat="server" ImageUrl='<%# Eval("ThumbnailPath","{0}") %>' />
                                            </div>
                                        </asp:HyperLink>
                                    </div>
                                    <div class="product_detail_image_heading_CAT">
                                        <asp:Label ID="lblProductName" runat="server" Text='<%#Eval("CategoryName","{0}") %>'></asp:Label>
                                    </div>
                                    <div class="topcat_desc_CAT">
                                        <asp:Literal ID="lblDescription1" runat="server"></asp:Literal>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <div class="clearBoth">

                        </div>
                    </div>
                    <div id="pnlAllProductTopLevel" class="padd_bottom_30 floatleft clear" runat="server"
                        visible="false">
                        <div class="col-md-12 col-lg-12 col-xs-12">
                            <asp:Label ID="lblProductHeader" runat="server" class="product_detail_sub_heading custom_color floatleft clear marginBtm10"
                                Text="Select a product below"></asp:Label>
                        </div>
                        <div class="clearBoth">
                            &nbsp;
                        </div>
                        <div id="cntProductList" runat="server" ><%--style="width: 1000px;"--%>
                            <asp:Repeater ID="dlTopLevel" runat="server" OnItemDataBound="dlTopLevel_ItemDataBound"
                                OnItemCommand="dlTopLevel_ItemCommand">
                                <ItemTemplate>
                                    <div class="BD_CatProducts rounded_corners">
                                        <div class="pad5">
                                            <div class="LCLB">

                                                <asp:HyperLink ID="hlProductDetail" runat="server">
                                                    <div class="PDTC_CatProd FI_CatProd">

                                                        <asp:Image ID="imgProduct" runat="server" CssClass="full_img_ThumbnailPath_CatPro" />

                                                        <asp:ImageButton ID="imgThumbnail" CssClass="full_img_ThumbnailPath_CatPro" runat="server" AlternateText="" OnClientClick=""
                                                            CommandName="DesignOnline" CommandArgument='' />
                                                    </div>
                                                </asp:HyperLink>
                                            </div>
                                            <div class="product_detail_image_heading_IRI">
                                                <asp:Label ID="lblProductName" runat="server" Text='<%#Eval("ProductName","{0}") %>'></asp:Label>
                                            </div>
                                            <div class="product_detail_image_Pricing">
                                                <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("MinPrice") %>'></asp:Label>
                                            </div>
                                            <table id="productPricetbl" class="productPricetbl" runat="server" width="80%" cellpadding="5" cellspacing="0" style="">
                                                <tr>
                                                    <td class="Metroprod_table_cell prod_table_cell procu_detail_grid_cell wdthWrp"
                                                        runat="server" style="text-align: left;">Quantity
                                                    </td>
                                                    <td id="tdStockOpt1" runat="server" class="prod_table_cell Metroprod_table_cell procu_detail_grid_cell wdthWrp"
                                                        style="text-align: right;">
                                                        <asp:Label ID="lblStockOpt1" runat="server" Text="" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td id="tdQty1" class="product_detail_item_cell procu_detail_grid_cell" style="text-align: left;">

                                                        <asp:Label runat="server" ID="spanQuantity"></asp:Label>
                                                    </td>
                                                    <td id="tdPrice1" runat="server" class="product_detail_item_cell procu_detail_grid_cell"
                                                        style="text-align: right;">
                                                        <div id="matrixItemColumn1" runat="server">

                                                            <asp:Label ID="lblPrice1" runat="server" />
                                                            <asp:Label ID="lblDiscountedPrice1" CssClass="custom_colorTS" Visible="false"
                                                                runat="server" />

                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr id="SecPricetr" runat="server">
                                                    <td id="tdQty2" class="product_detail_item_cell procu_detail_grid_cell" style="text-align: left;">

                                                        <asp:Label runat="server" ID="spanQuantity2"></asp:Label>
                                                    </td>
                                                    <td id="tdPrice2" runat="server" class="product_detail_item_cell procu_detail_grid_cell"
                                                        style="text-align: right;">
                                                        <div id="matrixItemColumn2" runat="server">

                                                            <asp:Label ID="lblPrice2" runat="server" />
                                                            <asp:Label ID="lblDiscountedPrice2" CssClass="custom_colorTS" Visible="false"
                                                                runat="server" />

                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr class="trMoreDetailOfProductLnk">
                                                    <td style="text-align: left;">
                                                        <p style="font-weight: bolder; font-style: italic; margin-top: 0px;">
                                                            <a id="lnkPRoductdetail" runat="server">more...</a>
                                                        </p>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div id="PriceCircle" class="blue_cicle_container DisplayNoneCSS6" runat="server">
                                            <div class="BC_CatProd">
                                                <div class="all_padding3">
                                                    <div class="paddingTop2px">
                                                        &nbsp;
                                                    </div>
                                                    FROM
                                                <br />
                                                    <asp:Label runat="server" ID="lblFromMinPrice" Text='<%# Eval("MinPrice") %>' Font-Bold="true"
                                                        Font-Size="16px" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <asp:HiddenField ID="hfIds" runat="server" />
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>

                        <div id="PopupCorpQuickText" class="FileUploaderPopup_Mesgbox  rounded_corners">
                            <asp:Label ID="lblHeader" runat="server" CssClass="left_align FileUploadHeaderText_PopUp float_left_simple MesgBoxClass lblPopupCorpHeading" Text="Select & Edit User Profile"></asp:Label>
                            <div id="CancelControl" onclick="hidePopup()" class="MesgBoxBtnsDisplay rounded_corners5">
                                Close
                            </div>
                            <div class="clearBoth">
                                &nbsp;
                            </div>
                            <div class="SolidBorderCS">
                                &nbsp;
                            </div>
                            <div class="corpUFcontainer">
                                <div class="c1UFContainer">

                                    <div class="variableItemContainer" id="dropDownContainer" runat="server">
                                        <div class="radioBtnCorp">
                                            <input type="radio" id="btnMyProfile" name="profile" value="My profile" checked="checked" />
                                            <span>My profile</span>
                                            <br />
                                        </div>
                                        <div class="radioBtnCorp">
                                            <input type="radio" id="btnOtherProfile" name="profile" value="Other profile" /><span>Other profile</span>
                                            <asp:DropDownList ID="dropBoxSelectUser" runat="server" OnChange="onUserProfileChange();" CssClass="dropBoxSelectUserCorp"></asp:DropDownList>
                                            <%--<select id="dropBoxSelectUser" runat="server" class="InputQtxtTS">
                                            <option>option1</option>
                                        </select>--%>
                                            <br />
                                        </div>
                                        <br />
                                        <a class="lblShowProfile" style="display: none;">Show Profile <i class="arrow-down"></i></a>
                                    </div>
                                    <div class="profileContainer" runat="server" id="profileContainer" style="display:block;">
                                        <div class="variableItemContainer">
                                            <span>First Name</span>
                                            <br />
                                            <input type="text" maxlength="500" class="InputQtxtTS txtFName" runat="server" id="txtFName" />
                                        </div>
                                        <div class="variableItemContainer">
                                            <span>Middle Name</span>
                                            <br />
                                            <input type="text" maxlength="500" class="InputQtxtTS txtMname" runat="server" id="txtMname" />
                                        </div>
                                        <div class="variableItemContainer">
                                            <span>Last Name</span>
                                            <br />
                                            <input type="text" maxlength="500" class="InputQtxtTS txtLname" runat="server" id="txtLname" />
                                        </div>
                                        <div class="variableItemContainer">
                                            <span>Title</span>
                                            <br />
                                            <input type="text" maxlength="500" class="InputQtxtTS txtTitle" runat="server" id="txtTitle" />
                                        </div>
                                        <div class="variableItemContainer">
                                            <span>Home Tel 1</span>
                                            <br />
                                            <input type="text" maxlength="500" class="InputQtxtTS txtHomeTel1" runat="server" id="txtHomeTel1" />
                                        </div>
                                        <div class="variableItemContainer">
                                            <span>Home Tel 2</span>
                                            <br />
                                            <input type="text" maxlength="500" class="InputQtxtTS txtHomeTel2" runat="server" id="txtHomeTel2" />
                                        </div>
                                        <div class="variableItemContainer">
                                            <span>Home Extension 1</span>
                                            <br />
                                            <input type="text" maxlength="500" class="InputQtxtTS txtHomeExt1" runat="server" id="txtHomeExt1" />
                                        </div>
                                        <div class="variableItemContainer">
                                            <span>Home Extension 2</span>
                                            <br />
                                            <input type="text" maxlength="500" class="InputQtxtTS txtHomeExt2" runat="server" id="txtHomeExt2" />
                                        </div>
                                        <div class="variableItemContainer">
                                            <span>Mobile</span>
                                            <br />
                                            <input type="text" maxlength="500" class="InputQtxtTS txtMobile" runat="server" id="txtMobile" />
                                        </div>
                                        <div class="variableItemContainer">
                                            <span>Pager</span>
                                            <br />
                                            <input type="text" maxlength="500" class="InputQtxtTS txtPager" runat="server" id="txtPager" />
                                        </div>
                                        <div class="variableItemContainer">
                                            <span>Email</span>
                                            <br />
                                            <input type="text" maxlength="500" class="InputQtxtTS txtEmail" runat="server" id="txtEmail" disabled="disabled" />
                                        </div>
                                        <div class="variableItemContainer">
                                            <span>FAX</span>
                                            <br />
                                            <input type="text" maxlength="500" class="InputQtxtTS txtFax" runat="server" id="txtFax" />
                                        </div>
                                        <div class="variableItemContainer">
                                            <span>Job Title</span>
                                            <br />
                                            <input type="text" maxlength="500" class="InputQtxtTS txtJtitle" runat="server" id="txtJtitle" />
                                        </div>

                                        <div class="variableItemContainer">
                                            <span>Linkedin URL</span>
                                            <br />
                                            <input type="text" maxlength="500" class="InputQtxtTS txtLinkedInURL" runat="server" id="txtLinkedInURL" />
                                        </div>

                                        <div class="variableItemContainer">
                                            <span>Skype ID</span>
                                            <br />
                                            <input type="text" maxlength="500" class="InputQtxtTS txtSkypeID" runat="server" id="txtSkypeID" />
                                        </div>
                                        <div class="variableItemContainer">
                                            <span>Facebook URL</span>
                                            <br />
                                            <input type="text" maxlength="500" class="InputQtxtTS txtFbID" runat="server" id="txtFbID" />
                                        </div>
                                        <div class="variableItemContainer">
                                            <span>Twitter URL</span>
                                            <br />
                                            <input type="text" maxlength="500" class="InputQtxtTS txtTwitterURL" runat="server" id="txtTwitterURL" />
                                        </div>
                                        <div class="variableItemContainer">
                                            <span>URL</span>
                                            <br />
                                            <input type="text" maxlength="500" class="InputQtxtTS txtURL" runat="server" id="txtURL" />
                                        </div>
                                         <div class="variableItemContainer">
                                            <span>PO Box Address </span>
                                            <br />
                                            <input type="text" maxlength="500" class="InputQtxtTS txtPOBox" runat="server" id="txtPOBox" />
                                        </div>
                                         <div class="variableItemContainer">
                                            <span>Corporate Unit </span>
                                            <br />
                                            <input type="text" maxlength="500" class="InputQtxtTS txtCunit" runat="server" id="txtCunit" />
                                        </div>
                                         <div class="variableItemContainer">
                                            <span>Office Trading Name </span>
                                            <br />
                                            <input type="text" maxlength="500" class="InputQtxtTS txtOfcTrading" runat="server" id="txtOfcTrading" />
                                        </div>
                                         <div class="variableItemContainer">
                                            <span>Contractor Name </span>
                                            <br />
                                            <input type="text" maxlength="500" class="InputQtxtTS txtContractorName" runat="server" id="txtContractorName" />
                                        </div>
                                         <div class="variableItemContainer">
                                            <span>BPay CRN </span>
                                            <br />
                                            <input type="text" maxlength="500" class="InputQtxtTS txtBpay" runat="server" id="txtBpay" />
                                        </div>
                                         <div class="variableItemContainer">
                                            <span>ABN </span>
                                            <br />
                                            <input type="text" maxlength="500" class="InputQtxtTS txtABN" runat="server" id="txtABN" />
                                        </div>
                                         <div class="variableItemContainer">
                                            <span>ACN </span>
                                            <br />
                                            <input type="text" maxlength="500" class="InputQtxtTS txtACN" runat="server" id="txtACN" />
                                        </div>
                                         <div class="variableItemContainer">
                                            <span>Additional Field 1 </span>
                                            <br />
                                            <input type="text" maxlength="500" class="InputQtxtTS txtAf1" runat="server" id="txtAf1" />
                                        </div>
                                         <div class="variableItemContainer">
                                            <span>Additional Field 2</span>
                                            <br />
                                            <input type="text" maxlength="500" class="InputQtxtTS txtAf2" runat="server" id="txtAf2" />
                                        </div>
                                         <div class="variableItemContainer">
                                            <span>Additional Field 3</span>
                                            <br />
                                            <input type="text" maxlength="500" class="InputQtxtTS txtAf3" runat="server" id="txtAf3" />
                                        </div>
                                         <div class="variableItemContainer">
                                            <span>Additional Field 4</span>
                                            <br />
                                            <input type="text" maxlength="500" class="InputQtxtTS txtAf4" runat="server" id="txtAf4" />
                                        </div>
                                         <div class="variableItemContainer">
                                            <span>Additional Field 5</span>
                                            <br />
                                            <input type="text" maxlength="500" class="InputQtxtTS txtAf5" runat="server" id="txtAf5" />
                                        </div>
                                    </div>



                                </div>
                                <div class="c2UFContainer">
                                    <div class="variablesContainer c2UFVarContainer">

                                        <div class="clearBoth"></div>


                                        <asp:Label ID="label1" runat="server" Text="Template Quick Text Info." CssClass=" ufContainerHeading  MesgBoxClass lblPopupCorpHeading"></asp:Label>
                                        <div class="clearBoth"></div>
                                        <div class="variableItemContainer">
                                            <span>Full Name</span>
                                            <br />
                                            <input type="text" maxlength="500" class="InputQtxtTS txtQname" runat="server" id="txtQname" />
                                        </div>
                                        <div class="variableItemContainer">
                                            <span>Title</span>
                                            <br />
                                            <input type="text" maxlength="500" class="InputQtxtTS txtQTitle" runat="server" id="txtQTitle" />
                                        </div>
                                        <div class="variableItemContainer">
                                            <span>Company Name</span>
                                            <br />
                                            <input type="text" maxlength="500" class="InputQtxtTS txtQCompany" runat="server" id="txtQCompany" />
                                        </div>
                                        <div class="variableItemContainer">
                                            <span>Address line 1</span>
                                            <br />
                                            <input type="text" maxlength="500" class="InputQtxtTS txtAddress" runat="server" id="txtAddress" />
                                        </div>
                                        <div class="variableItemContainer">
                                            <span>Telephone / Other</span>
                                            <br />
                                            <input type="text" maxlength="500" class="InputQtxtTS txtQtel" runat="server" id="txtQtel" />
                                        </div>
                                        <div class="variableItemContainer">
                                            <span>Fax / Other</span>
                                            <br />
                                            <input type="text" maxlength="500" class="InputQtxtTS txtQFax" runat="server" id="txtQFax" />
                                        </div>
                                        <div class="variableItemContainer">
                                            <span>Email address</span>
                                            <br />
                                            <input type="text" maxlength="500" class="InputQtxtTS txtQEmail" runat="server" id="txtQEmail" />
                                        </div>
                                        <div class="variableItemContainer">
                                            <span>Website address</span>
                                            <br />
                                            <input type="text" maxlength="500" class="InputQtxtTS txtQWebsite" runat="server" id="txtQWebsite" />
                                        </div>
                                        <div class="variableItemContainer">
                                            <span>Company Message</span>
                                            <br />
                                            <input type="text" maxlength="500" class="InputQtxtTS txtQMessage" runat="server" id="txtQMessage" />
                                        </div>
                                       
                                    </div>
                                     <asp:Button ID="Button1" runat="server" Text="Save" CssClass="cursor_pointer designNowCS_Corp rounded_corners5 btnMakeThisDesign saveDesinCorpCat" OnClick="Button1_Click" />

                                </div>

                                <div class="c3UFContainer c3UFContainerHeight">
                                    <div class="variablesContainer">
                                        <img class=" imgDetailCorp" id="imgProductCPanel" style="margin-top: 30px;" />
                                    </div>

                                    <asp:Button ID="btnEditThisDesign" runat="server" Text="Design Now" CssClass="cursor_pointer designNowCS_Corp rounded_corners5 btnMakeThisDesign btnEditDesign" OnClick="btnEditThisDesign_Click" />
                                    <asp:Button ID="btnOrderNow" runat="server" Text="Generate Proof" CssClass="cursor_pointer designNowCS_Corp rounded_corners5 btnMakeThisDesign btnOrderNow" OnClick="btnOrderNow_Click" OnClientClick=" GeneratePreview(); return false;" />
                                </div>
                                <div class="clearBoth"></div>
                            </div>
                        </div>

                        <div id="PopupProofScreen" class="FileUploaderPopup_Mesgbox  rounded_corners">
                            <asp:Label ID="Label2" runat="server" CssClass="left_align FileUploadHeaderText_PopUp float_left_simple MesgBoxClass lblPopupCorpHeading" Text="Proof "></asp:Label>
                            <div id="Div1" onclick="hidePopup2()" class="MesgBoxBtnsDisplay rounded_corners5">
                                Close
                            </div>
                            <div class="clearBoth">
                                &nbsp;
                            </div>
                            <div class="SolidBorderCS">
                                &nbsp;
                            </div>
                            <div>
                                <iframe class="iframeProofContainer"></iframe>
                            </div>
                            <div id="previewProofing">
                                <div class="divTxtProofing">
                                    <div class="ConfirmPopupProof">
                                        <label>
                                            Confirm spelling and details</label>
                                        <input id="chkCheckSpelling" name="chkCheckSpelling" class="simpleText regular-checkbox"
                                            type="checkbox" />
                                        <label for="chkCheckSpelling" style="display: inline;">
                                        </label>
                                    </div>

                                </div>
                                <div class="SolidBorderCS">
                                    
                                </div>
                                <asp:Button ID="Button2" runat="server" Text="Next" CssClass="cursor_pointer designNowCS_Corp rounded_corners5 btnMakeThisDesign btnOrderNow" OnClick="Button2_Click" OnClientClick="  return verifySpellings();" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="clear marginBtm10">
        </div>
        <br />
        <br />
        <input type="hidden" runat="server" id="hfContactID" />
        <input type="hidden" runat="server" id="hfCompanyID" />
        <input type="hidden" runat="server" id="hfCorpType" />
        <input type="hidden" runat="server" id="hfTemplateProduct" />
        <input type="hidden" runat="server" id="hfCurItem" />
        <input type="hidden" runat="server" id="hfProductCatID" />
        <input type="hidden" runat="server" id="hfSPType" value="default" />
        <input type="hidden" runat="server" id="hfPrintCropMarks" />
        <input type="hidden" runat="server" id="hfSvcRespone" />
    </div>
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
    <script type="text/javascript">
        var mode = 2;
        var CuritemID = 0;
        var CurTemplateID = 0;
        function verifySpellings() {
            if ($("#chkCheckSpelling").is(':checked')) {
                return true;
            } else {
                alert("Please Confirm spellings and details!");
                return false;
            }
        }
        function GeneratePreview() {

            var curContactID = $('#<%= hfContactID.ClientID %>').val();
            if ($("#btnOtherProfile").is(':checked')) {
                curContactID = $('#<%= dropBoxSelectUser.ClientID %>').val();
            }
            var CurrentContact = {
                ContactID: curContactID,
                quickFullName: $(".txtQname").val(),
                quickTitle: $(".txtQTitle").val(),
                quickCompanyName: $(".txtQCompany").val(),
                quickAddress1: $(".txtAddress").val(),
                quickPhone: $(".txtQtel").val(),
                quickFax: $(".txtQFax").val(),
                quickEmail: $(".txtQEmail").val(),
                quickWebsite: $(".txtQWebsite").val(),
                quickCompMessage: $(".txtQMessage").val(),
                //contact 
                FirstName: $(".txtFName").val(),
                MiddleName: $(".txtMname").val(),
                LastName: $(".txtLname").val(),
                Title: $(".txtTitle").val(),
                HomeTel1: $(".txtHomeTel1").val(),
                HomeTel2: $(".txtHomeTel2").val(),
                HomeExtension1: $(".txtHomeExt1").val(),
                HomeExtension2: $(".txtHomeExt2").val(),
                Mobile: $(".txtMobile").val(),
                Pager: $(".txtPager").val(),
                Email: $(".txtEmail").val(),
                FAX: $(".txtFax").val(),
                JobTitle: $(".txtJtitle").val(),
                LinkedinURL: $(".txtLinkedInURL").val(),
                SkypeID: $(".txtSkypeID").val(),
                FacebookURL: $(".txtFbID").val(),
                TwitterURL: $(".txtTwitterURL").val(),
                URL: $(".txtURL").val(),
                POBoxAddress: $(".txtPOBox").val(),
                CorporateUnit: $(".txtCunit").val(),
                OfficeTradingName: $(".txtOfcTrading").val(),
                ContractorName: $(".txtContractorName").val(),
                BPayCRN: $(".txtBpay").val(),
                ABN: $(".txtABN").val(),
                AdditionalField1: $(".txtAf1").val(),
                AdditionalField2: $(".txtAf2").val(),
                AdditionalField3: $(".txtAf3").val(),
                AdditionalField4: $(".txtAf4").val(),
                AdditionalField5: $(".txtAf5").val(),
                ACN: $(".txtACN").val()
            }

            StartLoader();
            var obSt = {
                ContactID: $('#<%= hfContactID.ClientID %>').val(),
                CustomerID: $('#<%= hfCompanyID.ClientID %>').val(),
                OldItemID: CuritemID,
                templateID: CurTemplateID,
                printCropMarks: $('#<%= hfPrintCropMarks.ClientID %>').val(),
                objContact: CurrentContact
            }
            var jsonObjects = JSON.stringify(obSt, null, 2);
            var to;

            to = "/services/Webstore.svc/processOrder/";
            var options = {
                type: "POST",
                url: to,
                data: jsonObjects,
                contentType: "text/plain;",
                dataType: "json",
                async: true,
                complete: function (httpresp, returnstatus) {
                    StopLoader();
                    OnPreviewSuccess(httpresp.responseText);
                }
            };
            var returnText = $.ajax(options).responseText;
            return false;
        }
        function OnPreviewSuccess(responce) {
            if (responce != "") {
                $('#UpdateProgressUserProfile').css("display", "none");
                if (responce != "error") {
                    $('#<%= hfSvcRespone.ClientID %>').val(responce);
                    var arr = responce.split('_');
                    $(".iframeProofContainer").attr("src", "../TemplatePreviewer.aspx?templateID=" + arr[1]); $('#divShd').css("display", "block");
                    $("#PopupProofScreen").css("left", ($(window).width() - $("#PopupProofScreen").width()) / 2 + "px");
                    $("#PopupProofScreen").css("top", ($(window).height() - $("#PopupProofScreen").height()) / 2 + "px");
                    $("#PopupProofScreen").css("display", "block");
                    $('#divShd').css("display", "block");
                } else {
                    alert("error while processing order");
                }
            }
        }
        function StartLoader() {
            $('#divShd').css("z-index", "15000 !important"); $('#UpdateProgressUserProfile').css("z-index", "15000");
            $('#divShd').css("display", "block");

            $('#loaderimageDiv').css("display", "block");
            $("#lodingBar").html("Loading please wait....");
            var shadow = document.getElementById("divShd");
            var bws = getBrowserHeight();
            shadow.style.width = bws.width + "px";
            shadow.style.height = bws.height + "px";
            var left = parseInt((bws.width - 350) / 2);
            var top = parseInt((bws.height - 200) / 2);
            $('#UpdateProgressUserProfile').css("display", "block");
        }
        function StopLoader() {
            if (mode != 1) {
                $('#divShd').css("display", "none");
            }
            $('#UpdateProgressUserProfile').css("display", "none");
            $('#divShd').css("z-index", "500 !important");
        }
        function FunctionPopUp(itemId, imgUrl, pCatID, designMode, templateID) {
            CuritemID = itemId;
            CurTemplateID = templateID;
            $('#<%= hfCurItem.ClientID %>').val(itemId);
            $('#<%= hfProductCatID.ClientID %>').val(pCatID);
            var shadow = document.getElementById("divShd");
            var bws = getBrowserHeight();
            shadow.style.width = bws.width + "px";
            shadow.style.height = bws.height + "px";
            var left = parseInt(($(window).width() - 809) / 2);
            var top = parseInt(($(window).height() - 610) / 2);
            $('#divShd').css("z-index", "500");
            $('#divShd').css("display", "block");
            //shadow = null;
            $("#PopupCorpQuickText").css("left", ($(window).width() - $("#PopupCorpQuickText").width()) / 2 + "px");
            $("#PopupCorpQuickText").css("top", ($(window).height() - $("#PopupCorpQuickText").height()) / 2 + "px");
            $("#PopupCorpQuickText").css("display", "block");
            $("#imgProductCPanel").attr("src", imgUrl);

            if (designMode == 1) {
                $('.btnEditDesign').css("display", "inline-block");
                $('.btnOrderNow').css("display", "none");
            } else {
                $('.btnOrderNow').css("display", "inline-block");
                $('.btnEditDesign').css("display", "none");
            }
            // alert(itemId);
        }
        $("#btnMyProfile").click(function () {
            var thisCheck = $(this);
            if (thisCheck.is(':checked')) {
                $('#<%= hfSPType.ClientID %>').val("default");
                $(".dropBoxSelectUserCorp").css("display", "none");
                var contactID = $('#<%= hfContactID.ClientID %>').val();
                StartLoader();
                $.getJSON("/services/Webstore.svc/getContact?customerID=" + contactID,
                          function (xdata) {
                              OnSuccess(xdata);
                          });
            }
            else {
                $(".dropBoxSelectUserCorp").css("display", "inline-block");
                $('#<%= hfSPType.ClientID %>').val("other");
            }
        });
        $("#btnOtherProfile").click(function () {
            var thisCheck = $(this);
            if (thisCheck.is(':checked')) {
                $(".dropBoxSelectUserCorp").css("display", "inline-block");
         
                $('#<%= hfSPType.ClientID %>').val("other");
                }
                else {
                    $('#<%= hfSPType.ClientID %>').val("default");
                  
                    $(".dropBoxSelectUserCorp").css("display", "none");
                }
            });
            //$(".lblShowProfile").click(function () {
            //    if ($(".lblShowProfile").html() == 'Show Profile <i class="arrow-down"></i>') {
            //        $(".profileContainer").css("display", "block");
            //        $(".lblShowProfile").html('Hide Profile <i class="arrow-up"></i>');
            //    } else {
            //        $(".profileContainer").css("display", "none");
            //        $(".lblShowProfile").html('Show Profile <i class="arrow-down"></i>');
            //    }
            //});
            $(document).ready(function () {
                var mode = $('#<%= hfContactID.ClientID %>').val();
                var contactID = $('#<%= hfContactID.ClientID %>').val();
                if ($('#<%= hfTemplateProduct.ClientID %>').val() == "true") {
                    StartLoader();
                    $.getJSON("/services/Webstore.svc/getContact?customerID=" + contactID,
                              function (xdata) {
                                  OnSuccess(xdata);
                              });
                }
            });
            function hidePopup() {
                $("#PopupCorpQuickText").css("display", "none");
                $('#divShd').css("display", "none");
            }
            function hidePopup2() {
                $("#PopupProofScreen").css("display", "none");
            }
            function onUserProfileChange() {
                var contactID = $('#<%= dropBoxSelectUser.ClientID %>').val();
            StartLoader();
            $.getJSON("/services/Webstore.svc/getContact?customerID=" + contactID,
                      function (xdata) {
                          OnSuccess(xdata);
                      });
        }
        function OnSuccess(responce) {
            if (responce != "") {
                var objCorp = responce;
                StopLoader();
                mode = 1;
                $(".txtQname").val(objCorp.quickFullName);
                $(".txtQTitle").val(objCorp.quickTitle);
                $(".txtQCompany").val(objCorp.quickCompanyName);
                $(".txtAddress").val(objCorp.quickAddress1);
                $(".txtQtel").val(objCorp.quickPhone);
                $(".txtQFax").val(objCorp.quickFax);
                $(".txtQEmail").val(objCorp.quickEmail);
                $(".txtQWebsite").val(objCorp.quickWebsite);
                $(".txtQMessage").val(objCorp.quickCompMessage);
                //contact 
                $(".txtFName").val(objCorp.FirstName);
                $(".txtMname").val(objCorp.MiddleName);
                $(".txtLname").val(objCorp.LastName);
                $(".txtTitle").val(objCorp.Title);
                $(".txtHomeTel1").val(objCorp.HomeTel1);
                $(".txtHomeTel2").val(objCorp.HomeTel2);
                $(".txtHomeExt1").val(objCorp.HomeExtension1);
                $(".txtHomeExt2").val(objCorp.HomeExtension2);
                $(".txtMobile").val(objCorp.Mobile);
                $(".txtPager").val(objCorp.Pager);
                $(".txtEmail").val(objCorp.Email);
                $(".txtFax").val(objCorp.FAX);
                $(".txtJtitle").val(objCorp.JobTitle);
                $(".txtLinkedInURL").val(objCorp.LinkedinURL);
                $(".txtSkypeID").val(objCorp.SkypeID);
                $(".txtFbID").val(objCorp.FacebookURL);
                $(".txtTwitterURL").val(objCorp.TwitterURL);
                $(".txtURL").val(objCorp.URL);


                $(".txtPOBox").val(objCorp.POBoxAddress);
                $(".txtCunit").val(objCorp.CorporateUnit);
                $(".txtOfcTrading").val(objCorp.OfficeTradingName);
                $(".txtContractorName").val(objCorp.ContractorName);
                $(".txtBpay").val(objCorp.BPayCRN);
                $(".txtABN").val(objCorp.ABN);
                $(".txtAf1").val(objCorp.AdditionalField1);
                $(".txtAf2").val(objCorp.AdditionalField2);
                $(".txtAf3").val(objCorp.AdditionalField3);
                $(".txtAf4").val(objCorp.AdditionalField4);
                $(".txtAf5").val(objCorp.AdditionalField5);
                $(".txtACN").val(objCorp.ACN);
            }
        }
        function ProprtySelectionPopup(ItemID) {
            var popwidth = 835;

            var shadow = document.getElementById("divShd");
            var bws = getBrowserHeight();
            shadow.style.width = bws.width + "px";
            shadow.style.height = bws.height + "px";
            var left = parseInt((bws.width - popwidth) / 2);
            var top = parseInt((bws.height - 480) / 2);

            $('#divShd').css("display", "block");
            $('#jqwin').css("width", popwidth);
            $('#jqwin').css("height", 480);
            $('#jqwin').css("top", top);
            $('#jqwin').css("left", left);
            var html = ''; // '<div class="closeBtn2" onclick="closeMS()"> </div>';
            $('#jqwin').html(html + '<iframe id="ifrm" width="' + (popwidth - 20) + '" height="100%" border="0" style="width:' + (popwidth - 20) + 'px;height:100%;border: none; " class=""></iframe>')
            $('#ifrm').attr('src', "/RealEstatePropertyPopup.aspx?ItemID=" + ItemID);
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
    </script>
</asp:Content>
