<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ThemeSite.Master" AutoEventWireup="true"
    CodeBehind="PinkCardShopCart.aspx.cs" Inherits="Web2Print.UI.PinkCardShopCart" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Controls/OrderStep.ascx" TagName="OrderStep" TagPrefix="uc2" %>
<%@ Register Src="~/Controls/WhyChooseUs.ascx" TagName="WhyChooseUs" TagPrefix="uc7" %>
<%@ Register Src="~/Controls/RelatedItems.ascx" TagName="RelatedItems" TagPrefix="uc11" %>
<%@ Register Src="Controls/MatchingSet.ascx" TagName="MatchingSet" TagPrefix="uc3" %>
<%@ Register Src="~/Controls/RaveReview.ascx" TagName="RaveReview" TagPrefix="uc4" %>
<asp:Content ID="Content6" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            BindEvents();
        });

        function BindEvents() {
            $('.gallery a').lightBox({

                maxHeight: 500,
                maxWidth: 700
            });
        }

    </script>
    <div id="divShd" class="opaqueLayer">
    </div>
    <div class="container content_area">
        <div class="row left_right_padding">
               <ul class="bonhamsOption navM cursor_pointer">
                <li >1. Select
                </li>
                <li class="bonhamSelectedOption">2. Edit
                </li>


                <li>3. Confirm order & payment
                </li>
                <li>4. Order summary
                </li>

            </ul>
            <div class="signin_heading_div textAlignLeft" style="padding-bottom: 0px !important;">
                <table width="1000px">
                    <tr>
                        <td width="500px">
                            <asp:Label ID="lblShoppingCart" runat="server" Text="Shopping cart" CssClass="sign_in_heading"></asp:Label>
                        </td>
                        <td width="220px"></td>
                        <td width="180px"></td>
                    </tr>
                    <tr id="ErrorDisplyMes" runat="server" style="display: none; width: 1000px;">
                        <td colspan="3" width="1000px">
                            <div style="background-color: red; border: 1px solid red; padding: 10px;" class="rounded_corners">
                                <asp:Label ID="ErrorMEsSummry" runat="server" Style="color: White; font-size: 16px; font-weight: bold;"></asp:Label>
                            </div>
                        </td>

                    </tr>
                </table>
            </div>
            <div class="get_in_touch_box rounded_corners">

                <div id="ShopingCartDisplay">
                    <div class="textAlignCenter">
                        <asp:Label ID="lblMsg2" runat="server" Text="Your Cart is empty!" CssClass="cartFontStyle"
                            Visible="false" />
                        <asp:Label ID="lblStatus" runat="server" Text="" CssClass="errorMessage" Visible="false" />
                    </div>
                    <asp:Label ID="lblMsg" runat="server" Text="Your Cart is empty!" CssClass="cartFontStyle_MsgBox S_C_UP_VD_MesgBox"
                        Visible="false" />
                    <button id="Button1" runat="server" onclick="return EmailProofClick()" class="start_creating_btn_EmailProof float_right rounded_corners5">
                    </button>
                    <asp:GridView ID="grdViewShopCart" DataKeyNames="ItemID" runat="server" SkinID="grdViewShoppingCartView"
                        OnRowDataBound="grdViewShopCart_RowDataBound" OnRowCommand="grdViewShopCart_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" ItemStyle-CssClass="PadTop5 shopCartFirstCol">
                                <ItemTemplate>
                                    <div class="cart_grid_image_container gallery">
                                        <asp:HyperLink ID="lnkPdfFile" runat="server">
                                            <asp:Image ID="imgToShow" runat="server" CssClass="image_stretcher" />
                                        </asp:HyperLink>
                                    </div>
                                    <div class="BtnTemplateModifyContainer">
                                        <asp:Button ID="lnkBtnRedesignTemplate" Visible="false" CommandName="RedesignTemplate"
                                            runat="server" CssClass="silver_back_button_modify" Text="Modify" />

                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="29%" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemProductName" runat="server" Text='<%# Eval("ProductName")%>'
                                            CssClass="heading_16"></asp:Label><asp:Label id="CopyTxt" runat="server" CssClass="heading_16"  style="color:red !important;"></asp:Label>
                                        <br />
                                        <asp:Label ID="lblProductCategory" runat="server" CssClass="ItemdetailDesc" ></asp:Label>
                                        <br />
                                        <br />
                                        <asp:Label ID="lblSlectedStockName" runat="server" CssClass="ItemdetailDesc" ></asp:Label>
                                        <br />
                                        <br />
                                        <asp:Label ID="lblselectedAddOnList" runat="server" CssClass="ItemdetailDesc"></asp:Label>
                                        <br />
                                        <br />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                    ItemStyle-Width="5%" ItemStyle-CssClass="PadTop5">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemQuantity" Font-Bold="true" CssClass="fntQtyPrice" runat="server" Text='<%# Eval("Qty1") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Price" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                    ItemStyle-Width="20%" ItemStyle-CssClass="PadTop5 priceth">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemPrice" Font-Bold="true" CssClass="fntQtyPrice" runat="server" style="text-align:right !important;"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                                     ItemStyle-Width="20%" >
                                    <ItemTemplate>
                                        
                                       <a ID="linkRemoveCartItem" runat="server" class="silver_back_button_remove removeBtnForeColor" 
                                       >Remove</a>
                                        <asp:Button ID="btnCopyProduct" runat="server" CssClass="silver_back_button" Text="Copy in cart"
                                            CommandName="copyProduct" CommandArgument='<%# Eval("ItemID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <br />
                    
                    <br />
                    <div class="cnttotalPayables">
                        <div class="cntsubHeadings">
                            <asp:Label ID="ltrlsubtotal" runat="server" Text="Sub Total" CssClass=" LightGrayLabels displayClear"></asp:Label>
                            <asp:Label ID="lblVoucherDiscPercentageDisplay" Text="Discount" runat="server" ForeColor="Red"
                                Visible="false" CssClass=" LightGrayLabels displayClear" />
                            <asp:Label ID="lblTotalSavingText" runat="server" Text="Total Saving" ForeColor="Red"
                                Visible="false" CssClass=" LightGrayLabels displayClear"></asp:Label>
                            <asp:Label ID="ltrlDelivery" runat="server" Text="Delivery" CssClass="LightGrayLabels displayClear"></asp:Label>
                            <asp:Label ID="lblTaxLabel" runat="server" CssClass="LightGrayLabels displayClear"></asp:Label>
                            <asp:Label ID="lblTotalpayable" runat="server" Text="Total Payable" CssClass="colorBlack total_payable"></asp:Label>
                        </div>
                        <div class="cnttotals" id="cntRightPricing1" runat="server">
                            <asp:Label ID="lblSubTotal" Text="0" runat="server" CssClass="Fsize14 colorBlack displayClear" />
                            <asp:Label ID="lblVoucherCodeDiscAmount" Text="0" runat="server" ForeColor="Red"
                                Visible="false" CssClass=" LightGrayLabels displayClear" />
                            <asp:Label ID="lblTotalSaving" Text="0" runat="server" ForeColor="Red" Visible="false"
                                CssClass=" LightGrayLabels displayClear" />
                            <asp:Label ID="lblDeliveryCostCenter" Text="0" runat="server" CssClass="LightGrayLabels displayClear" />
                            <asp:Label ID="lblVatTotal" Text="0" runat="server" CssClass="LightGrayLabels displayClear" />
                            <asp:Label ID="lblGrandTotal" Text="0" runat="server" CssClass="colorBlack total_payable" Style="float: right;" />
                        </div>
                        <div class="clearBoth">
                        </div>
                    </div>
                    <div class="cntdispatchDelivery">
                        <div class="tdDispatchDate">
                            <div class="estimated_dispactch_sec MLeft17 LightGrayLabels">
                                <asp:Label ID="lblEstimatedDispatch" Text="Estimated Dispatch" runat="server" />
                            </div>
                            <div class="estimated_dispatch_time MLeft17">
                                <asp:Label ID="lblDeliveryCostCenterDateText" CssClass="heading_h5 colorBlack fontSyleBold"
                                    Text="" runat="server" />&nbsp;
                            </div>
                        </div>
                        <div class="tdDelivery">
                            <div class="estimated_dilvery_sec  LightGrayLabels">
                                <asp:Literal ID="ltrlestimated" runat="server" Text="Delivery options"></asp:Literal>
                            </div>
                            <div class="dropdown_sec">
                             
                                <asp:Panel ID="pnlVoucherCodes" runat="server" DefaultButton="btnAutoPostBackDiscountVoucherCode">
                                    <asp:Label ID="lblRedeemHeading" runat="server" Text="Voucher codes" CssClass="LightGrayLabels marginLeft marginRight"></asp:Label>
                                    <asp:TextBox ID="txtDiscountVoucherCode" runat="server" CssClass="text_box115 rounded_corners5" />
                                    <asp:Button ID="btnAutoPostBackDiscountVoucherCode" ClientIDMode="Static" runat="server"
                                        CssClass="btn_blue_back_small rounded_corners5" Text="Apply" OnClick="btnAutoPostBackDiscountVoucherCode_Click" />
                                    <asp:Button ID="btnAutoPostBackRemoveVoucherCode" ClientIDMode="Static" runat="server"
                                        CssClass="btn_blue_back_small_Remove rounded_corners5" Text="Remove voucher code"
                                        Visible="false" OnClick="btnAutoPostBackRemoveVoucherCode_Click" />
                                </asp:Panel>
                            </div>
                        </div>
                        <div class="clearBoth">
                        </div>
                    </div>
                    <div class="clearBoth">
                        &nbsp;
                    </div>
                </div>
                <asp:Button Text="CHECKOUT" ValidationGroup="ChkOutGroup" ID="btnCheckout" runat="server"
                    OnClientClick="return CanCheckOut(this);" CssClass="start_creating_btn_checkout float_right  rounded_corners5"
                    OnClick="btnCheckout_Click" /><br />
                <asp:Button ID="btnContinueShopping" CausesValidation="false" runat="server" OnClick="btnContinueShopping_Click"
                    CssClass="btn_brown  rounded_corners5" Text="CONTINUE SHOPPING" Style="float: right;" />

                <div class="clearBoth">
                    &nbsp;
                </div>
            </div>
            <div class="clearBoth">
                &nbsp;
            </div>
            <div style="padding-top: 5px; text-align: left; font-size: 18px; line-height: 4;">
                <asp:Label ID="lblYoumightLke" runat="server" Visible="false"></asp:Label>
            </div>

            <ajaxToolkit:ModalPopupExtender ID="mpeImgBidDisplayPopup" BehaviorID="mpeImgBidDisplayPopup"
                TargetControlID="hdnTargetCtrlUpload" PopupControlID="PnlImdDisplayer" BackgroundCssClass="ModalPopupBG"
                runat="server" Drag="false" PopupDragHandleControlID="draghandlepnlShopCart"
                DropShadow="false" />
            <asp:Panel ID="PnlImdDisplayer" runat="server" CssClass="FileUploaderPopup_Mesgbox LCLB rounded_corners"
                Style="display: none; width: 500px">
                <div style="background: white; height: 450px;">
                    <div class="Width100Percent">
                        <div class="float_left">
                            <asp:Label ID="lblImgPreviewer" runat="server" CssClass="FileUploadHeaderText">Image Previewer</asp:Label>
                        </div>
                        <div class="exit_container25" onclick="ImgDisplayerUploaderHide();">
                            <div id="btnCancelMessageBox" runat="server" class="exit_popup">
                            </div>
                        </div>
                    </div>
                    <div class="clearBoth">
                        &nbsp;
                    </div>
                    <div class="FileuploadInnerContainer">
                        <div class="PDTC FI">
                            <img id="imgDisplayBigPicture" class="card_size_image" alt="" />
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="draghandlepnlShopCart" runat="server" Style="display: block;">
            </asp:Panel>
            <div class="clearBoth">
                &nbsp;
            </div>


            <uc3:MatchingSet ID="MatchingSet1" runat="server" Visible="false" />
            <uc11:RelatedItems ID="relateditemsWidget" runat="server" Visible="false" />
            <br />
            <asp:HiddenField ID="hfNumOfRec" runat="server" />
            <asp:HiddenField ID="txthdnDeliveryAddressID" runat="server" Value="0" />
            <asp:HiddenField ID="txthdnBillingAddressID" runat="server" Value="0" />
            <asp:HiddenField ID="txthdnBillingDefaultAddress" runat="server" Value="false" />
            <asp:HiddenField ID="txthdnBillingDefaultShippingAddress" runat="server" Value="false" />
            <asp:HiddenField ID="txthdnDeliveryDefaultAddress" runat="server" Value="false" />
            <asp:HiddenField ID="txthdnDeliveryDefaultShippingAddress" runat="server" Value="false" />
            <asp:HiddenField ID="txtVoucherDiscountRate" runat="server" Value="0" />
            <asp:HiddenField ID="hdnTargetCtrlUpload" runat="server" />
            <asp:HiddenField ID="hfIsVoucherDisabled" runat="server" />
            <asp:HiddenField ID="hfIsUserLoggedIn" runat="server" />
            <asp:HiddenField ID="hfRedirectURL" runat="server" />
            <asp:HiddenField ID="hfProofEmail1" runat="server" />
            <asp:HiddenField ID="hfProofEmail2" runat="server" />
            <asp:HiddenField ID="hfOrderID" runat="server" />
            <asp:HiddenField ID="hfContactID" runat="server" />
            <asp:HiddenField ID="hfCompanyID" runat="server" />
            <asp:HiddenField ID="hfIsRemoveCart" runat="server" Value="0" />
            <asp:HiddenField ID="hfRemovalItemid" runat="server" Value="" />
            <uc4:RaveReview ID="RRV" runat="server" />

            <div class="clearBoth">
                &nbsp;
            </div>
            <br />
        </div>
    </div>
    <div class="opaqueLayer">
    </div>
    <div id="popupSendProofs" class="popupSendProofs">
        <div id="MainContent_Div1" onclick="return closePopUpClick()" class="exit_popup exitPopupProofs" style="display: none;">
        </div>
        <label class="boldTxt">
            Enter the email address(s) to send all proofs</label>
        <br />
        <br />
        <table class="normalTextStyle" width="400px" cellpadding="2">
            <tr>
                <td align="right">
                    <asp:Literal ID="ltrlEmail1" runat="server" Text="E-mail Address 1"></asp:Literal>
                </td>
                <td class="tdtextbox">
                    <input type="email" name="userEmail1" id="userEmail1" class="text_box200 rounded_corners5">
                    <br />
                </td>
            </tr>
            <tr>
                <td align="right" class="H4B">
                    <asp:Literal ID="ltrlconfirmpass" runat="server" Text="E-mail Address 2"></asp:Literal>
                </td>
                <td class="tdtextbox">
                    <input type="email" name="userEmail2" id="userEmail2" class="text_box200 rounded_corners5">
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div class="reset_password_button_div2">
                        <button id="btnSendEmail" class="start_creating_btn rounded_corners5 btnSendProofs"
                            onclick="return EmailItems()">
                            Send Email</button>
                        &nbsp;
                        <button id="btnCancel" class="start_creating_btn rounded_corners5" onclick="return closePopUpClick()">
                            Close
                        </button>
                    </div>
                </td>
            </tr>
        </table>
        <p class="proofSuccess" style="display: none;" id="paraEmail" runat="server">
            Proofs sent.
        </p>
    </div>
    <asp:Label ID="lblDesgnHeaderTxt" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblProductHeaderTxt" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblQuantityHeaderTxt" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblPriceHeaderTxt" runat="server" Visible="false"></asp:Label>
    <asp:Button ID="btnYesNO" runat="server" Style="display: none; width: 0px; height: 0px" />
    <ajaxToolkit:ModalPopupExtender ID="MPERemoveCartItem" runat="server" BackgroundCssClass="ModalPopupBG"
        PopupControlID="pnlRemoveCartItem" TargetControlID="btnYesNO" BehaviorID="MPERemoveCartItem"
        CancelControlID="btnCancalRemoval" DropShadow="false">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlRemoveCartItem" runat="server" Width="500px" CssClass="FileUploaderPopup_Mesgbox LCLB rounded_corners"
        Style="display: none">
        <div class="white_background" style="padding: 20px;">
            <asp:Label ID="lblHeaderRC" runat="server" CssClass="left_align FileUploadHeaderText_PopUp float_left_simple MesgBoxClass">Message</asp:Label>
            <div onclick="$find('MPERemoveCartItem').hide();" class="MesgBoxBtnsDisplay rounded_corners5">
                Close
            </div>
            <div class="clearBoth">
                &nbsp;
            </div>
            <div class="SolidBorderCS">
                &nbsp;
            </div>
            <div class="pop_body_MesgPopUp">
                <br />
                <div class="inner">
                    <asp:Label ID="lblMesdDescRC" runat="server" Text="Are you sure you want to remove this item from your shopping cart?"></asp:Label>
                </div>
                <br />
                <div class="padding_top_bottom_10 center_align">
                    <asp:Button OnClientClick="hideRemovePanel();" OnClick="Unnamed_Click" CssClass="start_creating_btn rounded_corners5 " Text="Remove" runat="server" />


                    <asp:Button ID="btnCancalRemoval" runat="server" Text="Cancel" CssClass="start_creating_btn_LetWait rounded_corners5 " OnClientClick="return resetCartItemId();" />


                </div>
            </div>
        </div>
    </asp:Panel>
    <%-- <uc7:WhyChooseUs ID="WhyChooseUs1" runat="server" IsDisplayText="false" />--%>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("#<%= grdViewShopCart.ClientID%> tr").children(":first").next().next().next().next().css("border", "none");
            $("#<%= grdViewShopCart.ClientID%> tr").children(":first").next().next().next().next().css("background-color", "white !important");
            $("#<%= grdViewShopCart.ClientID%> tr").children(":first").next().next().next().next().css("color", "white !important");
           
        });


        function resetCartItemId() {
            $("#<%= hfRemovalItemid.ClientID%>").val("");
            return false;
        }

        function EmailProofClick() {
            if ($('#<%=hfIsUserLoggedIn.ClientID %>').val() == "true") {

                $(".opaqueLayer").css("height", $(window).height() + "px");
                $(".opaqueLayer").css("width", $(window).width() + "px");
                $(".opaqueLayer").css("display", "block");
                $("#userEmail1").val($('#<%=hfProofEmail1.ClientID %>').val());
                $("#userEmail2").val($('#<%=hfProofEmail2.ClientID %>').val());
                $("#popupSendProofs").css("display", "block");
                $('.popupSendProofs').css("left", (($(window).width() - $('.popupSendProofs').width()) / 2) + "px");
                $('.popupSendProofs').css("top", (($(window).height() - $('.popupSendProofs').height()) / 2) + "px");
                return false;
            } else {
                window.location.href = $('#<%=hfRedirectURL.ClientID %>').val();
                return false;
            }
        }
        function EmailItems() {
            var ModeOfStore = '<%=Mode %>';
            var BrokerCompanyId = '<%=BrokerCompanyID %>';
            var email1 = $("#userEmail1").val();
            var email2 = $("#userEmail2").val();
            var orderID = $('#<%=hfOrderID.ClientID %>').val();
            var ContactID = $('#<%=hfContactID.ClientID %>').val();
            var ComapanyID = $('#<%=hfCompanyID.ClientID %>').val();
            var filter = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
            if (email1 != "") {
                if (!filter.test(email1)) {
                    alert("Please enter a valid email address in address 1");
                    return false;
                }
            }
            if (email2 != "") {
                if (!filter.test(email2)) {
                    alert("Please enter a valid email address in address 2");
                    return false;
                }
            }
            if (email2 == "" && email1 == "") {
                alert("Please enter atleast one email address");
                return false;
            }
            $.get("Services/Webstore.svc/sendProofs", { orderID: orderID, Email1: email1, Email2: email2, ContactID: ContactID, ContactCompanyID: ComapanyID, StoreMode: ModeOfStore, BrokerContactCompanyID: BrokerCompanyId },
                function (data) {
                    //alert(data);
                    if (data == true) {
                        $(".proofSuccess").css("display", "block");
                    }
                    //window.location.href = data + "&CategoryId=" + CategoryId + "&ProductName=" + document.getElementById('txtDesignName').value + emailParameters;
                });
            return false;
        }
        function ShowPopRemoveItem(id) {
            
                $("#<%=hfRemovalItemid.ClientID%>").val(id);
                $find('MPERemoveCartItem').show();
                return false;
        }

        function hideRemovePanel() {
            $find('MPERemoveCartItem').hide();
            showProgress();
            return true;
        }

        function closePopUpClick() {
            $("#popupSendProofs").css("display", "none");
            $(".opaqueLayer").css("display", "none");
            return false;
        }
        function CanCheckOut(sender) {
            if (Page_ClientValidate()) {

                var numOfRec = '<%=NumberOfRecords %>';
                var zeroItemincart = '<%=ZeroItems %>';
                if (parseInt(numOfRec) > 0) {
                    if (zeroItemincart == 0) {
                        showProgress();
                        AvoidReClickOnPostBack(sender);
                        return true;
                    } else {
                        ShowPopup('Message', 'Please remove the items of price zero.');
                        return false;
                    }

                }
                else {
                    ShowPopup('Message', 'No item(s) to checkout');
                    return false;
                }
            } else {
                return false;
            }
        }
        function confirmDeletion(message, checkBox) {
            if ($('#' + checkBox).is(':checked')) {
                var result = confirm(message);
                if (result == false)
                    return false;
                else
                    return true;
            }
            else
                return true;
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

        function ImgDisplayerUploaderShow(imgPath) {
            //alert("show me");
            $find('mpeImgBidDisplayPopup').show();
            for (i = 0; i <= 6000; i++) { } //wait
            $("#imgDisplayBigPicture").attr("src", imgPath);

            //return false;

        }

        function ImgDisplayerUploaderHide() {

            // $("#imgDisplayBigPicture").attr("src", "/images/wpLoader.gif");
            $find('mpeImgBidDisplayPopup').hide();
        }

    </script>
</asp:Content>
