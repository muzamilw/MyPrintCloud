<%@ Page Title="" Language="C#" MasterPageFile="~/PinkRegistration/PinkRegister.Master"
    AutoEventWireup="true" CodeBehind="OldStep1.aspx.cs" Inherits="Web2Print.UI.PinkRegistration.OldStep1" %>

<%@ Register Src="PinkRegFooter.ascx" TagName="PinkRegFooter" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainContent">
    <div id="divShd" class="opaqueLayer" style="z-index: 999 !important;">
    </div>
    <div class="content_area">
        <div class="left_right_padding">
            <a class="signin_heading_div PinkRegHead1 PinkRegBack" href="/pinkregistration/faqs.aspx">
                FAQs - Learn More</a>
            <div class="signin_heading_div PinkRegHead1">
                Want to be a local Pinkcards.com supplier?
            </div>
            <div class="page_border_div  rounded_corners PinkRegFramePadding">
                <div class="one_half ">
                    <div style="padding-right: 30px;">
                        <div id="SubHead" runat="server" class="PinkRegHead2" visible="true">
                            FREE Trial - PinkCards.com
                        </div>
                        <div class="PinkRegHead3">
                            We need YOU to print and process our online orders soon!
                        </div>
                        <p class="PinkRegP">
                            Register today and see how your ‘Own Brand’ web store will look and function with
                            Pinkcards.com
                        </p>
                        Your ‘Own Brand’ web store gives you :
                        <div class="PinkRegHead3">
                            Benefits
                        </div>
                        <ul class="PinkRegUL">
                            <li>Over 30 products and 6000 professionally designed templates.</li>
                            <li>Free listing on Pinkcards.com until 1st September.</li>
                            <li>The ability to replace your existing web site or a secondary website element.</li>
                            <li>Win and retain new customers online.</li>
                            <li>Promote and upsell other products and services you do.</li>
                            <li>Exclusivity as the Pink partner in your post code territory.</li>
                        </ul>
                        <div class="PinkRegHead3">
                            Features
                        </div>
                        <ul class="PinkRegUL">
                            <li>Shopping cart and your payment gateway configuration.</li>
                            <li>Set your own online store prices for products and services.</li>
                            <li>Set your own colour theme and home page banners.</li>
                            <li>Administrator panel; Order history, Visitor CRM details.</li>
                            <li>View Pending Orders ; Change order and Production statuses.</li>
                            <li>Set store Policies, Refining and Delivery options.</li>
                            <li>Create Discount / Voucher codes for promotions.</li>
                            <li>100% cloud based, integrated MIS features.</li>
                        </ul>
                        <p class="PinkRegP">
                            Get your friends, customers and colleagues to design and process orders for business
                            cards all the way to checkout (no billing information will be asked). Make sure
                            they select the correct town or postcode to find your Free Web Store. All order
                            details and artwork will be forwarded to you by email and be listed in you admin
                            panel.
                        </p>
                        <p class="PinkRegP">
                            Your Free Web 2 print store will be automatically disabled on 1st January 2014.
                        </p>
                        <p class="PinkRegP">
                            Let us know what you think and how we can improve the experience - email your suggestions
                            to info@pinkcards.com.
                        </p>
                        <div class="PinkRegHead2">
                            <a href="/PinkRegistration/faqs.aspx" style="color: #ec008c; text-decoration: underline;">
                                FAQs - Learn More</a>
                        </div>
                        <div class="PinkRegHead3">
                            Coming soon…
                        </div>
                        <div class="PinkRegHead3">
                            Upgrade and go live with your 'Own Brand' and your own URL address
                        </div>
                        <div class="left_right_padding paddingS4 PopularProducts">
                            <div id="" class="">
                                <div style="border-radius: 5px; border: 1px solid rgb(231, 231, 231); height: 51px;
                                    text-align: center; padding-top: 15px; margin-top: -10px; margin-bottom: 0px;
                                    background-color: rgb(246, 246, 246); -moz-border-radius: 5px; -webkit-border-radius: 5px;
                                    -khtml-border-radius: 5px;">
                                    Type in <a href="http://www.print-direct.net" style="color: Blue; text-decoration: underline;"
                                        target="_blank">www.print-direct.net</a>&nbsp; and see a live store. This company
                                    is also registered for free in PinkCards.com under the postcode area SL3
                                </div>
                            </div>
                        </div>
                        <p class="PinkRegP">
                            From the 1st September 2013, Registered users will be able to upgrade to a Pink
                            partner status by purchasing, at least 1, available exclusive post code territory.
                        </p>
                        <p class="PinkRegP">
                            Pink partners must satisfy our Quality and Servicing guidelines (see FAQs page)
                            to have exclusive listings on PinkCards.com for their Pink territories.
                        </p>
                    </div>
                </div>
                <div class="one_half">
                    <div style="padding-left: 30px;">
                        <a href="http://www.pinkcards.com" target="_blank">
                            <img class="PinkImgAlignRight" src="../App_Themes/S6/Images/screenshot.png" /></a>
                        <div class="clear" style="margin-top: 15px;">
                        </div>
                        <div class="PinkRegHead3">
                            Register for Free and access your Web 2 Print store today.
                        </div>
                        <p class="PinkRegP">
                            Includes a FREE listing on PinkCards.com until 1st September 2013.
                        </p>
                        <p class="PinkRegP">
                            I have read and agree with the terms & conditions
                            <asp:CheckBox ID="chkEarlyBird2" runat="server" ValidationGroup="1" ClientIDMode="Static" />
                        </p>
                        <asp:Button ID="Button1" Text="Register" runat="server" ClientIDMode="Static" CssClass="start_creating_btn_prodDetail_orng rounded_corners5 marginBtm10 padding0imp"
                            CausesValidation="false" OnClientClick="return validate('chkEarlyBird2');" OnClick="Button1_Click" />
                        <%-- <div class="PinkRegHead3">
                            OR
                        </div>
                        <div class="PinkRegHead3">
                            Early bird offer
                        </div>
                        <p class="PinkRegP">
                            Reserve now and secure your post code area. As an added incentive we’ll give you
                            the first 3 months for free and a further saving of £114 for the next 6 months.</p>
                        <p class="PinkRegP">
                            I have read and agree with the terms & conditions
                            <asp:CheckBox ID="chkEarlyBird" runat="server" ClientIDMode="Static" />
                        </p>
                        <asp:Button ID="btnEditThisDesign" Text="Register" runat="server" ClientIDMode="Static"
                            CssClass="start_creating_btn_prodDetail_orng rounded_corners5 marginBtm10 padding0imp"
                            CausesValidation="false" OnClientClick=" return validate('chkEarlyBird');" OnClick="btnEditThisDesign_Click" />--%>
                    </div>
                </div>
                <div class="clear">
                </div>
                <uc1:PinkRegFooter ID="PinkRegFooter1" runat="server" />
                <script type="text/javascript">
                    function ChangeCSS() {
                        window.parent.increaseHeight();
                    }


                    function validate(chk) {



                        if ($('#' + chk).is(':checked')) {
                            return true;
                        }
                        else {
                            alert('Please accept terms and conditions to continue');
                            return false;
                        }


                    }



                    function showPopUP(postCode) {
                        var popwidth = 550;

                        var shadow = document.getElementById("divShd");
                        var bws = getBrowserHeight();
                        shadow.style.width = bws.width + "px";
                        shadow.style.height = bws.height + "px";
                        var left = parseInt((bws.width - popwidth) / 2);
                        var top = parseInt((bws.height - 450) / 2);


                        //        shadow = null;
                        $('#divShd').css("display", "block");
                        $('#jqwin').css("width", popwidth);
                        $('#jqwin').css("height", 455);
                        $('#jqwin').css("top", top);
                        $('#jqwin').css("left", left);
                        var html = '<div class="closeBtn2" onclick="closeMS()"> </div>';
                        $('#jqwin').html(html + '<iframe id="ifrm" width="' + popwidth + '" height="100%" border="0" style="width:' + popwidth + 'px;height:100%;border: none; background-color: white; padding-left: 3px;" class=""></iframe>')
                        $('#ifrm').attr('src', '/PinkRegistration/Pink_Quality_Service_Checklist.htm');
                        $('#jqwin').show();
                        $(".closeBtn2").css("display", "block");


                        return true;
                    }

                    function closeMS() {
                        $('#divShd').css("display", "none");
                        $(".closeBtn").css("display", "none");
                        $('#jqwin').hide();
                        $('#ifrm').attr('src', 'about:blank');
                    }
                </script>
                <div id="jqwin" class="FileUploaderPopup_Mesgbox rounded_corners IframeCss LCLB">
                </div>
                <style>
                    #jqwin
                    {
                        /* height: 100%;--%> */
                        z-index: 99999;
                        position: absolute;
                        display: none;
                    }
                </style>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="cphFooter">
    <div class="clearBoth">
        &nbsp;
    </div>
    <br />
    <br />
</asp:Content>
