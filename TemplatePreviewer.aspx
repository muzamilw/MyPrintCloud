<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TemplatePreviewer.aspx.cs" Inherits="Web2Print.UI.TemplatePreviewer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="js/p101.js"></script>
    <script src="Scripts/jquery-1.9.1.min.js"></script>
    <link href="Styles/p103.css" rel="stylesheet" />
</head>
<body style="margin: 0px 0px 0px 0px; padding: 0px 0px 0px 0px; width: 979px;height: 419px;border: none;">
       <div id="PreviewerContainer" class="ui-corner-all propertyPanel">
  
       
        <div id="Previewer" class="ui-corner-all">
            <div id="sliderFrame">
            </div>
            <div class="sliderLine sliderLineBtm">
            </div>

        </div>
    </div>

    <script type="text/javascript">
        function getParameterByName(name) {
            name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
            var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
                results = regex.exec(location.search);
            return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
        }
        function k0() {
            var tID = parseInt( getParameterByName("templateID"));

            $("#sliderFrame").html('<div id="slider">  </div> <div id="thumbs"></div> <div style="clear:both;height:0;"></div>');
               
                $('#PreviewerContainer').css("left", (($(window).width() - $('#PreviewerContainer').width()) / 2) + "px");
                $('#PreviewerContainer').css("height", (($(window).height() - 15)) + "px");
                $('#Previewer').css("height", (($(window).height() - 15)) + "px");
                    $('#sliderFrame').css("height", $('#Previewer').height() - 25 + "px");
                    $('#slider').css("height", $('#Previewer').height() - 5 + "px");
                    $('#thumbs').css("height", $('#Previewer').height() - 5+ "px");

                    $.getJSON("designengine/services/TemplatePagesSvc/" + tID,
           function (DT) {
               $.each(DT, function (i, IT) {

                   $("#slider").append('<img src="designengine/designer/products/' + tID + '/p' + IT.PageNo + '.png"  alt="' + IT.PageName + '" />');
                   $("#thumbs").append(' <div id="thumbPage' + IT.ProductPageID + '" class="thumb"><div class="frame"><img src="designengine/designer/products/' + tID + '/p' + IT.PageNo + '.png" class="thumbNailFrame" /></div><div class="thumb-content"><p>' + IT.PageName + '</p></div><div style="clear:both;"></div></div>');

               });
           });

        
        }
        $(document).ready(function () {
            k0();
        });
    </script>
</body>
</html>
