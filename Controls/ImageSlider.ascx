<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImageSlider.ascx.cs"
    Inherits="Web2Print.UI.Controls.ImageSlider" %>


<div class="content_area  featuredProductsCarousel">

    <script src="../Scripts/SmartFormJCarousel.js"></script>
    <link href="../Styles/SamrtFormCarousel.css" rel="stylesheet" />

    <script type="text/javascript">
        $(function () {
            $('#mycarousel').jcarousel();
        });
    </script>
    <ul id="mycarousel" class="jcarousel-skin-tango">
        <asp:Repeater ID="rptrPageSlider" runat="server" OnItemDataBound="rptrPageSlider_ItemDataBound">
            <ItemTemplate>
                <li style="width: 250px; padding-right: 10px;">
                    <div>
                        <asp:Image ID="imgBanner" runat="server" alt="" Style="height: 250px; width: 250px;" />
                    </div>
                </li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>
