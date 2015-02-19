<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HomePageBannerTransitions.ascx.cs"
    Inherits="Web2Print.UI.Controls.HomePageBannerTransitions" %>
<%@ OutputCache VaryByParam="true" Duration="10" %>

<link href="../Styles/js-image-slider.css" rel="stylesheet" />
<script src="../scripts/js-image-slider.js" type="text/javascript"></script>

<script type="text/javascript">
    $(document).ready(function () {
        $('.bxslider').bxSlider({
           
        }
        );
    });
</script>



<div class="content_area container" id="bannerContainer" runat="server" clientidmode="Static">
<%--    <div id="bsliderFrame">
    <div id="bribbon"></div>
        <div id="bslider" class="postionofImge" style="background-position:center;">--%>
    <ul class="bxslider">
            <asp:Repeater ID="rptrPageBanners" runat="server" OnItemDataBound="rptrPageBanners_ItemDataBound">
                <ItemTemplate>
                    <li>
                    <asp:HyperLink ID="hlItemUrl" runat="server" NavigateUrl='<%#Eval("ButtonUrl") %>' ClientIDMode="Predictable">
                        <asp:Image ID="imgBanner" runat="server" ClientIDMode="Predictable" />
                    </asp:HyperLink>
                        </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>      <%-- </div>
    </div>
</div>--%>
    </div>

<%--AlternateText='<%#Eval("Heading") %>'--%>
