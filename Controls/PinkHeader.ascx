<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PinkHeader.ascx.cs"
    Inherits="Web2Print.UI.Controls.PinkHeader" %>
<%@ Register src="QuickCalculator.ascx" tagname="QuickCalculator" tagprefix="uc1" %>
<style>
    .ui-autocomplete-loading
    {
        background: white url('images/pu_loader.gif') right center no-repeat !important;
    }
</style>
<script src="/Scripts/input.watermark.js" type="text/javascript"></script>
<div id="divShd1" class="opaqueLayer1" style="z-index: 999 !important;">
</div>
<div id="divToAnmate" class="animatedDiv FileUploaderPopup_Mesgbox LCLB rounded_corners"
    style="display: none; background-color: Gray; position: fixed; width: 300px;
    height: 100px;">
    <div style="background-color: White; height: 100px;">
        <div class="Width100Percent">
            <div class="exit_container">
                <div id="Div1" onclick="PopupHide();" runat="server" class="exit_popup">
                </div>
            </div>
        </div>
        <div>
            <asp:Button ID="btnfinfstore" runat="server" Text="Find a store near you" CssClass="PinkStoreToFindCS"
                OnClientClick="return showPanel();" />
        </div>
    </div>
</div>
<div class="content_area">
    <div class="left_right_padding">
        <div id="divBroker" class="pink_company_logo_CS borderGray float_left_simple"
            runat="server" visible="false">
            <asp:Image ID="imgBroker" runat="server" ImageUrl="~/App_Themes/S6/Images/VideoPinkTheme.png"
                BorderStyle="None" CssClass="PinkWidth480Px PinkHeight180Px" />
        </div>
        <div id="divMap" class="pink_company_logo_CS borderGray UKMAPImge float_left_simple"
            style="background-color: #F9F9F9;" runat="server">
            <div class="FindYourStoreCss">
                <asp:Literal ID="ltrlFindStore" runat="server" Text="Find a store near you"></asp:Literal>
            </div>
            <asp:TextBox ID="txtPostCodeMain" runat="server" CssClass="txtbox_PostCode rounded_corners"
                ClientIDMode="Static" CausesValidation="false">
            </asp:TextBox>
            <div id="empty-message" class="searchPostCodeErr">
            </div>
            <%--<img src="/App_Themes/S6/Images/pink-float-popup.png" style="position: absolute;
                width: 242px; height: 172px; z-index: 9000; margin-left: -30px; margin-top: -136px;" />--%>
        </div>
        <div id="VideoImge" class="Mleft10 borderGray float_left_simple" runat="server">
            <a href="#" onclick="showVideo()">
                <asp:Image ID="imgPinkVideotheme" runat="server" ImageUrl="~/App_Themes/S6/Images/VideoPinkTheme.png"
                    BorderStyle="None" CssClass="PinkWidth243Px PinkHeight180Px" /></a>
        </div>
        <div class="Mleft10 float_left_simple" >
            
                <asp:Image ID="imgLogo" runat="server" BorderStyle="None" CssClass="PinkWidth243Px PinkHeight180Px" />
        </div>
        <uc1:QuickCalculator ID="QuickCalculator1" runat="server" />
        <div class="clearBoth">
        &nbsp;
        </div>
    </div>
     <div class="clearBoth">
        &nbsp;
        </div>

    <script type="text/javascript">

        $('#txtPostCodeMain').bind("keydown", function (e) {

            if (e.which == 13) { //Enter key
                e.preventDefault(); //Skip default behavior of the enter key
                //showPopUP();

                //var code = $('#txtPostCodeMain').val();
                //showPopUP(code);
                //increaseHeight();
                //window.location.href = "/SearchByPostCode.aspx?PostCode=" + code;
            }

        });




        function PopupHide() {
            $("#divToAnmate").css("display", "none");
            $('#divShd1').css("display", "none");
        }
        function showPanel() {
            $('#divShd1').css("display", "none");
            var code = "";
            $("#divToAnmate").css("display", "none");
            showPopUP(code);
            //increaseHeight();
            return false;
        }




        function showVideo() {
            var popwidth = 565;

            var shadow = document.getElementById("divShd");
            var bws = getBrowserHeight();
            shadow.style.width = bws.width + "px";
            shadow.style.height = bws.height + "px";
            var left = parseInt((bws.width - popwidth) / 2);
            var top = parseInt((bws.height - 315) / 2);


            //        shadow = null;
            $('#divShd').css("display", "block");
            $('#jqwin').css("width", popwidth);
            $('#jqwin').css("height", 320);
            $('#jqwin').css("top", top);
            $('#jqwin').css("left", left);
            var html = '<div class="closeBtn2" onclick="closeMS()"> </div>';
            $('#jqwin').html(html + '<iframe width="560" height="315" src="http://www.youtube.com/embed/u5sYhXRMWZM" frameborder="0" allowfullscreen></iframe>')

            $('#jqwin').show();
            $(".closeBtn2").css("display", "block");


            return true;
        }


        function closeMS() {
            $('#divShd').css("display", "none");
            $(".closeBtn").css("display", "none");
            $('#jqwin').hide();

        }

        $('#txtPostCodeMain').inputWatermark();

        $("#txtPostCodeMain").autocomplete({

            source: function (request, response) {

                $.ajax({

                    url: "../services/webstore.svc/SearchPostCodeorCity?Keyword=" + $("#txtPostCodeMain").val(),

                    success: function (data) {

                        response($.map(data, function (item) {

                            return {

                                label: item.OutPostCode + (item.City ? ", " + item.City : "") + ", " + item.Country,
                                value: item.OutPostCode
                            }
                        }));
                    }

                });
            },

            response: function (event, ui) {
                //alert(ui.content.length);
                // ui.content is the array that's about to be sent to the response callback.
                if (ui.content.length == 0) {
                    $("#empty-message").text("No results found");
                } else {
                    $("#empty-message").empty();
                }
            }
                        ,

            minLength: 2,
            change: function (event, ui) {

                if (ui.item == null || ui.item == undefined) {
                    clearResult();

                }



            },

            select: function (event, ui) {
                if (validateinput()) {
                    var code = $('#txtPostCodeMain').val();
                    showPopUP(code, '/default.aspx');
                    increaseHeight();

                }
            },

            open: function () {

                $(this).removeClass("ui-corner-all").addClass("ui-corner-top");

            },

            close: function () {

                $(this).removeClass("ui-corner-top").addClass("ui-corner-all");

            }

        });



        function clearResult() {
            $("#txtPostCodeMain").val('');
        }


        function validateinput() {



            if ($("#txtPostCodeMain").val() == '' || $("#txtPostCodeMain").val() == 'Enter Postcode') {
                $("#empty-message").text("Please select a valid Postcode to find a store near you");
                return false;
            }
            else {
                $("#empty-message").empty();
                return true;
            }


        }
    </script>
</div>
