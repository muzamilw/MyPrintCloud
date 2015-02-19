<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ColorPicker.aspx.cs" Inherits="Web2Print.UI.ColorPicker" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8">
    <link href="Styles/jquery-ui.css" rel="stylesheet" />
    <script src="Scripts/jquery-1.9.1.min.js"></script>
    <script src="Scripts/jquery-ui.min.js"></script>
    <style>
        #red, #green, #blue
        {
            float: left;
            clear: left;
            width: 140px;
            margin: 7px;
            margin-left:0px;
        }
        #swatch
        {
            width: 120px;
            height: 100px;
            margin-top: 18px;
            margin-left: 350px;
            background-image: none;
        }

        #red .ui-slider-range
        {
            background: #ef2929;
        }

        #red .ui-slider-handle
        {
            border-color: #ef2929;
        }

        #green .ui-slider-range
        {
            background: #8ae234;
        }

        #green .ui-slider-handle
        {
            border-color: #8ae234;
        }

        #blue .ui-slider-range
        {
            background: #729fcf;
        }

        #blue .ui-slider-handle
        {
            border-color: #729fcf;
        }

        #selectionArea
        {
          cursor: pointer;
margin-right: 20px;
margin-top: 10px;
width: 60px;
text-align: center;
padding: 5px;
font-size: 14px;
color: white !important;
text-decoration: inherit;
padding: 8px;
border-radius: 5px;
        }
    </style>
    <script>
        function hexFromRGB(r, g, b) {
            var hex = [
              r.toString(16),
              g.toString(16),
              b.toString(16)
            ];
            $.each(hex, function (nr, val) {
                if (val.length === 1) {
                    hex[nr] = "0" + val;
                }
            });
            return hex.join("").toUpperCase();
        }
        function refreshSwatch() {
            var red = $("#red").slider("value"),
              green = $("#green").slider("value"),
              blue = $("#blue").slider("value"),
              hex = hexFromRGB(red, green, blue);
            $("#selectionArea").css("background-color", "#" + hex);
        }
        $(function () {
            $("#red, #green, #blue").slider({
                orientation: "horizontal",
                range: "min",
                max: 255,
                value: 127,
                slide: refreshSwatch,
                change: refreshSwatch
            });
            $("#red").slider("value", 255);
            $("#green").slider("value", 140);
            $("#blue").slider("value", 60);
        });
        function ChangeBaseColor() {
            var val = $("#SelectClor").val();
            parent.ChangeBaseColorq(val, $('#selectionArea').css('background-color'));

        }

        function openCloseBaseTab() {
            if ($("#colorChangercnt").hasClass("right199")) {
                $("#colorChangercnt").removeClass("right199").addClass("right0");
            } else {
                $("#colorChangercnt").removeClass("right0").addClass("right199");
            }
        }
    </script>
</head>
<body class="ui-widget-content" style="border: 0;">
    
    <select id="SelectClor" style="width: 152px; padding: 6px; margin-bottom: 9px;">
        <option value="1">Background color
        </option>
        <option value="2">Text color
        </option>
    </select>
    <div style="height:95px;">
        <div id="red"></div>
        <div id="green"></div>
        <div id="blue"></div>
    </div>

    <br />

    <a id="selectionArea" class="rounded_corners" style="background: #63c2de;" onclick="return ChangeBaseColor();">Apply</a>
</body>
</html>
