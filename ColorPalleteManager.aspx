 <%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ColorPalleteManager.aspx.cs"
    Inherits="Web2Print.UI.ColorPalleteManager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    <script src="js/colorpicker.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui-1.9.2.custom.min.js" type="text/javascript"></script>
    <link href="Styles/colorpicker.css" rel="stylesheet" type="text/css" />
    <link href="Styles/layout.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="colorPallete" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="jquery-migrate" />

                <asp:ScriptReference Name="WebUIValidation.js" Assembly="System.Web" />
            </Scripts>
        </asp:ScriptManager>
    <div style="background-color: White; height: 409px; width: 710px; padding: 5px; padding-top: 7px;
        overflow: hidden;">
        <div class="signin_heading_div">
            <div class="float_left_simple ">
                <asp:Label ID="lblAddNP" runat="server" Text="Add new pallete for your store:" CssClass="sign_in_heading"></asp:Label></div>
            <div class="clearBoth">
            </div>
        </div>
        <div>
            <asp:Image ID="HomePageImage" runat="server" CssClass="float_right spacer10pxtop spacerbottom"
                Width="430px" Height="200px" style=" margin-right: 20px;" />
        </div>
        <div class="clearBoth">
        </div>
        <div class="  paddingLeft10px paddingBottom10px product_detail_sub_heading custom_color Cp_label_area1 paddingTop15px">
            <asp:Label ID="lblPleseEPN" runat="server" Text="Please enter Pallete Name:" />
        </div>
        <div class="address_contol_area1">
            <asp:TextBox ID="txtPalleteName" runat="server" CssClass="text_box334"></asp:TextBox>
        </div>
        <div class="clearBoth">
            &nbsp;</div>
        <div class="paddingLeft10px paddingBottom10px product_detail_sub_heading Cp_label_area1 custom_color paddingTop15px float_left_simple">
            <asp:Label ID="lblAllP" runat="server" Text="Choose pallete colors:" />
        </div>
        <div class="float_left_simple" style="width: 500px; text-align: left;">
            <p class="float_left_simple MargnRght10" style="margin-top: 10px;">
                1.</p>
            <div id="customWidget" class="MargnRght10 float_left_simple">
                <div id="colorSelector" class="colorSelector2 ">
                    <div id="Color1" runat="server">
                    </div>
                </div>
                <div class="colorpickerHolder2">
                </div>
            </div>
            <p class="float_left_simple MargnRght10" style="margin-top: 10px;">
                2.</p>
            <div id="customWidget1" class="MargnRght10 float_left_simple">
                <div id="colorSelector1" class="colorSelector2">
                    <div id="Color2" runat="server" clientidmode="Static">
                    </div>
                </div>
                <div class="colorpickerHolder2">
                </div>
            </div>
            <p class="float_left_simple MargnRght10" style="margin-top: 10px;">
                3.</p>
            <div id="customWidget2" class="MargnRght10 float_left_simple">
                <div id="colorSelector2" class="colorSelector2">
                    <div id="Color3" runat="server" clientidmode="Static">
                    </div>
                </div>
                <div class="colorpickerHolder2">
                </div>
            </div>
            <p class="float_left_simple MargnRght10" style="margin-top: 10px;">
                4.</p>
            <div id="customWidget3" class="MargnRght10 float_left_simple">
                <div id="colorSelector3" class="colorSelector2">
                    <div id="Color4" runat="server" clientidmode="Static">
                    </div>
                </div>
                <div class="colorpickerHolder2">
                </div>
            </div>
            <p class="float_left_simple MargnRght10" style="margin-top: 10px;">
                5.</p>
            <div id="customWidget4" class="MargnRght10 float_left_simple">
                <div id="colorSelector4" class="colorSelector2">
                    <div id="Color5" runat="server" clientidmode="Static">
                    </div>
                </div>
                <div class="colorpickerHolder2">
                </div>
            </div>
            <p class="float_left_simple MargnRght10" style="margin-top: 10px;">
                6.</p>
            <div id="customWidget5" class="MargnRght10 float_left_simple">
                <div id="colorSelector5" class="colorSelector2">
                    <div id="Color6" runat="server" clientidmode="Static">
                    </div>
                </div>
                <div class="colorpickerHolder2">
                </div>
            </div>
            <p class="float_left_simple MargnRght10" style="margin-top: 10px;">
                7.</p>
            <div id="customWidget6" class="MargnRght10 float_left_simple">
                <div id="colorSelector6" class="colorSelector2">
                    <div id="Color7" runat="server" clientidmode="Static" style=" background-color: White;">
                    </div>
                </div>
                <div class="colorpickerHolder2">
                </div>
            </div>
        </div>
        <div class="clearBoth">
            <br />
        </div>
        <div style="width: 260px;" class="float_right">
            <asp:Button ID="btnSave" runat="server" CssClass="start_creating_btn rounded_corners5 MTopM10 marginRight"
                Text="Save" Width="100px" OnClick="btnSave_Click" />
            <asp:Button ID="btnCancel" runat="server" CssClass="start_creating_btn rounded_corners5 H4B"
                Text="Cancel" Width="100px" OnClick="btnCancel_Click" />
        </div>
        <div class="clearBoth">
            <br />
        </div>
    </div>
    <asp:HiddenField ID="hfcolor1" runat="server" />
    <asp:HiddenField ID="hfcolor2" runat="server" />
    <asp:HiddenField ID="hfcolor3" runat="server" />
    <asp:HiddenField ID="hfcolor4" runat="server" />
    <asp:HiddenField ID="hfcolor5" runat="server" />
    <asp:HiddenField ID="hfcolor6" runat="server" />
    <asp:HiddenField ID="hfcolor7" runat="server" />
    <script type="text/javascript">
        // 1 color.
        $('#colorSelector').ColorPicker({
            color: '',
            onShow: function (colpkr) {
                $(colpkr).fadeIn(500);
                return false;
            },
            onHide: function (colpkr) {
                $(colpkr).fadeOut(500);
                return false;
            },
            onChange: function (hsb, hex, rgb) {
                $('#<%=Color1.ClientID %>').css("background-color", "#" + hex);
                $('#<%=hfcolor1.ClientID %>').val('#' + hex);

            }
        });
        //// second color 
        $('#colorSelector1').ColorPicker({
            color: '#0000ff',
            onShow: function (colpkr) {
                $(colpkr).fadeIn(500);
                return false;
            },
            onHide: function (colpkr) {
                $(colpkr).fadeOut(500);
                return false;
            },
            onChange: function (hsb, hex, rgb) {
                $('#colorSelector1 div').css('backgroundColor', '#' + hex);
                $('#<%=hfcolor2.ClientID %>').val('#' + hex);
            }
        });
        ////// 3rd color 
        $('#colorSelector2').ColorPicker({
            color: '#0000ff',
            onShow: function (colpkr) {
                $(colpkr).fadeIn(500);
                return false;
            },
            onHide: function (colpkr) {
                $(colpkr).fadeOut(500);
                return false;
            },
            onChange: function (hsb, hex, rgb) {
                $('#colorSelector2 div').css('backgroundColor', '#' + hex);
                $('#<%=hfcolor3.ClientID %>').val('#' + hex);
            }
        });
        ////// 4th color 
        $('#colorSelector3').ColorPicker({
            color: '#0000ff',
            onShow: function (colpkr) {
                $(colpkr).fadeIn(500);
                return false;
            },
            onHide: function (colpkr) {
                $(colpkr).fadeOut(500);
                return false;
            },
            onChange: function (hsb, hex, rgb) {
                $('#colorSelector3 div').css('backgroundColor', '#' + hex);
                $('#<%=hfcolor4.ClientID %>').val('#' + hex);
            }
        });
        ////// 5th color 
        $('#colorSelector4').ColorPicker({
            color: '#0000ff',
            onShow: function (colpkr) {
                $(colpkr).fadeIn(500);
                return false;
            },
            onHide: function (colpkr) {
                $(colpkr).fadeOut(500);
                return false;
            },
            onChange: function (hsb, hex, rgb) {
                $('#colorSelector4 div').css('backgroundColor', '#' + hex);
                $('#<%=hfcolor5.ClientID %>').val('#' + hex);
            }
        });
        ////// 6th color 
        $('#colorSelector5').ColorPicker({
            color: '#0000ff',
            onShow: function (colpkr) {
                $(colpkr).fadeIn(500);
                return false;
            },
            onHide: function (colpkr) {
                $(colpkr).fadeOut(500);
                return false;
            },
            onChange: function (hsb, hex, rgb) {
                $('#colorSelector5 div').css('backgroundColor', '#' + hex);
                $('#<%=hfcolor6.ClientID %>').val('#' + hex);
            }
        });
        ////// 7th color 
        $('#colorSelector6').ColorPicker({
            color: '#0000ff',
            onShow: function (colpkr) {
                $(colpkr).fadeIn(500);
                return false;
            },
            onHide: function (colpkr) {
                $(colpkr).fadeOut(500);
                return false;
            },
            onChange: function (hsb, hex, rgb) {
                $('#colorSelector6 div').css('backgroundColor', '#' + hex);
                $('#<%=hfcolor7.ClientID %>').val('#' + hex);
            }
        });
    </script>
    </form>
</body>
</html>
