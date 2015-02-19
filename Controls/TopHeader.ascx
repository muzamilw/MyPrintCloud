<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopHeader.ascx.cs" Inherits="Web2Print.UI.Controls.TopHeader" %>
<script src="/Scripts/TransparentPopup.js" type="text/javascript"></script>
<script type="text/javascript" src="http://www.google.com/jsapi?key=ABQIAAAALDWeTDQHOJCbCf0JnUqL8BT2yXp_ZAY8_ufC3CFXhHIE1NvwkxQA7AE8xB9MyWgHECPY2qimOp7BUQ"></script>
<div class="clearBoth">
    &nbsp;
</div>

<div id="TopHeaderContainer" class="top_header top_header_ex " runat="server">

    <div class="container content_area">
        <div class="row left_right_padding">
            <div class="padding_top_bottom_topheader">
                <div class="company_logo col-md-3 col-lg-3 col-xs-12 col-sm-4">
                    <a href='<%=ResolveUrl("~/default.aspx") %>'>
                        <asp:Image ID="imgLogo" runat="server" SkinID="imgLogotop" BorderStyle="None" ClientIDMode="Static" /></a>
                    <asp:HyperLink ID="hlPweredBy" runat="server" NavigateUrl="http://www.myprintcloud.com/"
                        CssClass="MLF top_header" Target="_blank">
                        <asp:Literal runat="server" ID="ltrlPoweredBy" Text="Powered by"></asp:Literal>
                        &nbsp; MyPrintCloud
                    </asp:HyperLink>
                </div>
                <div style="float: right; padding-right: 0px; padding-left: 0px;" class="col-md-9 col-lg-9 col-xs-12 col-sm-8 cntrightMenu">

                    <div id="divTopHeaderMenu" class="TopHeaderMenu float_left_simple" runat="server">

                        <asp:HyperLink ID="lnkAllProducts" runat="server" NavigateUrl="~/Allproducts.aspx" CssClass="iconHover">All Products</asp:HyperLink><a id="A1" runat="server" class="piplineClr">&nbsp;|&nbsp;</a>
                        <%-- <i id="RfqIcon" runat="server" class="fa fa-pencil iconHover classicthemeMenu"></i>--%><a id="lnkRequestQuote" runat="server" onclick="UserRFQVerification();" style="cursor: pointer;" class="iconHover">Request a Quote</a><a id="rfqPipeline" runat="server" class="piplineClr">&nbsp;|&nbsp;</a>
                        <a id="lnkNearstStore" runat="server" style="cursor: pointer;" onclick="OpenNearestStorePopUP(); return false;" visible="false">Find nearest store</a><a id="fasPipeline" runat="server" visible="false" class="piplineClr">&nbsp;|&nbsp;</a>

                        <asp:HyperLink ID="lnkContactUs" runat="server" NavigateUrl="~/ContactUs.aspx" CssClass="iconHover">Contact Us</asp:HyperLink>&nbsp;&nbsp;&nbsp;&nbsp;
                    </div>
                    <div class="phone_number_sec ">
                        <div class="TeleImg" runat="server" id="telephoneIcon">
                        </div>
                        <div id="CompanyName" visible="false" runat="server" class="HeaderCompnyNameCs">
                            <asp:Label ID="ltrlCompanyName" runat="server" CssClass="BrokerCompanyNameCs"></asp:Label>
                            <asp:Label ID="ltrlCity" runat="server" CssClass="BrokerCompanyCityCs"></asp:Label>
                        </div>
                        <div id="lblPoneNumber" runat="server" class="lblPhoneNo">
                        </div>

                    </div>
                    <div id="cntresponsiveMenu" runat="server">


                        <div id="CollapseBtn" class="float_right">
                            <a class="icon-align-justify">
                               
                                <%--<img src="../App_Themes/responsive/Images/collapseBtnImg.PNG" class="collapseMenu" />--%>
                            </a>
                        </div>
                        <div class="clearBoth">
                        </div>

                    </div>
                </div>
                <div class="clearBoth">
                </div>
                <div id="collapsedMenu" style="display: none;" class="containerCollapsedMenu col-md-12 col-lg-12 col-xs-12 col-sm-12">
                    <ul id="ulCollapseMenu" style="">
                        <li >
                            <asp:HyperLink ID="pullDownMenuAllPro" runat="server" NavigateUrl="~/Allproducts.aspx">All Products</asp:HyperLink>
                        </li>
                        <li>
                            <a id="pullDownMenuRFQ" runat="server" onclick="UserRFQVerification();" style="cursor: pointer;">Request a Quote</a>
                        </li>
                        <li id="liFindPartnerStore" runat="server" visible="false">
                            <a id="A3" runat="server" style="cursor: pointer;" onclick="OpenNearestStorePopUP(); return false;">Find nearest store</a>
                        </li>
                        <li>
                            <asp:HyperLink ID="pullDownMenuCU" runat="server" NavigateUrl="~/ContactUs.aspx">Contact Us</asp:HyperLink>

                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
</div>
 
<script type="text/javascript">

    //$(document).ready(function () {
    var isPink = '<%=isPinkCards%>';
    if (isPink == 1) {

        //do the geo location detection
        //var cloc = new ClientLocation.Location(google.loader.ClientLocation);
        //alert(cloc.address.country);
        show_dialog('/searchbypostcode.aspx?PostCode=&ReturnPath=', 'opaqueLayer', 'warn');
    }



    //});

    $("#CollapseBtn").click(function () {

        $("#collapsedMenu").slideToggle();
        if (hideOrShow == 0) {
            $("#MainContent_ctl01_TopLoginBar").css("height", "300px;");
            hideOrShow = 1;
        } else {
            $("#MainContent_ctl01_TopLoginBar").css("height", "75px;");
            hideOrShow = 0;
        }

    });

    function UserRFQVerification() {
        var isRefqValid = '<%=isBrokeronRFQ %>';
        if (isRefqValid == 1) {
            ShowPopup('Alert', "You need to select a store before requesting a qoute.");
            return false;
        } else if (isRefqValid == 0) {
            window.location.href = 'RequestQuote.aspx';
        } else {

        }
    }

    function OpenNearestStorePopUP() {
        //show_dialog('/searchbypostcode.aspx?PostCode=&ReturnPath=', 'opaqueLayer');
        show_dialog('/searchbypostcode.aspx?PostCode=&ReturnPath=', 'opaqueLayer', 'warn');
        // displaypostcodenavbar();
        return false;
    }

    var ClientLocation = {};
    ClientLocation.Address = function () {
        /// <field name="city" type="String" />
        /// <field name="region" type="String" />
        /// <field name="country" type="String" />
        /// <field name="country_code" type="String" />
        /// <returns type="ClientLocation.Address"/>
        if (arguments.length > 0) {
            this.city = arguments[0].city;
            this.region = arguments[0].region;
            this.country = arguments[0].country;
            this.country_code = arguments[0].country_code;
            return;
        }
        else {
            this.city = "";
            this.region = "";
            this.country = "";
            this.country_code = "";
        }

    }
    ClientLocation.Location = function () {
        /// <field name="latitude" type="Number" />
        /// <field name="longitude" type="Number" />
        /// <field name="address" type="ClientLocation.Address" />
        if (arguments.length > 0) {

            this.latitude = arguments[0].latitude;
            this.longitude = arguments[0].longitude;
            this.address = arguments[0].address;

        }
        else {
            this.latitude = 0;
            this.longitude = 0;
            this.address = undefined;
        }

    }

   
</script>
