<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PageBannerTheme3.ascx.cs"
    Inherits="Web2Print.UI.Controls.PageBannerTheme3" %>

<div class="content_area">

    <ul id="list-links">
        <asp:Repeater ID="rptheading" runat="server" OnItemDataBound="rptheading_ItemDataBound">
            <ItemTemplate>
                <li><a id="headingAnchor" runat="server">
                    <%#Eval("Heading") %></a> </li>
                <div class="clear">
                </div>
                <br />
            </ItemTemplate>
        </asp:Repeater>
    </ul>
    <ul id="list-images">
        <asp:Repeater ID="rptImg" runat="server" OnItemDataBound="rptImg_ItemDataBound">
            <ItemTemplate>
                <li>
                    <asp:HyperLink ID="hpImg" runat="server">
                        <asp:Image ID="img" AlternateText=" " runat="server" CssClass="widthHeightOfImg" /></asp:HyperLink>
                    <span class="heading">
                        <%#Eval("Heading") %><br />
                    </span><span class="heading2">
                        <%#Eval("Description") %><br />
                    </span>
                    <asp:Button ID="btnStartCreating" ToolTip='<%#Eval("Heading") %>' runat="server"
                        CssClass="btn_start_creating rounded_corners5" Text="Start Creating" />
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
    <div class="clear">
    </div>
</div>
<script type="text/JavaScript">

    $(document).ready(function () {

        // Declare variables
        var width = 570;
        var height = 250;
        var slides = $('#list-images li');
        var numSlides = slides.length;

        // Wrap the slides & set the wrap width - this will be used to animate the slider left/right
        slides.wrapAll('<div id="slide-wrap"></div>').css({ 'float': 'left', 'width': width, 'height': height });
        $('#slide-wrap').css({ width: width * numSlides });

        // Hover function - animate the slides based on index of links
        $('#list-links li a').hover(function () {
            $('#list-links li').removeClass('hover');
            var i = $(this).index('#list-links li a');
            $(this).parent().addClass('hover');
            $('#slide-wrap').stop().animate({ 'marginLeft': (width) * (-i) });
        });
    });
</script>
