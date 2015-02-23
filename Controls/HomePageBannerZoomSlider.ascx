<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HomePageBannerZoomSlider.ascx.cs"
    Inherits="Web2Print.UI.Controls.HomePageBannerZoomSlider" %>
<%@ OutputCache VaryByParam="true" Duration="10" %>
<script src="../scripts/zoomSlider.js" type="text/javascript"></script>

<div class="content_area" id="bannerContainer" runat="server" clientidmode="Static">
    <div id="wrapper">
        <div id="zoom-slider">
            <asp:Repeater ID="rptrPageBanners" runat="server" OnItemDataBound="rptrPageBanners_ItemDataBound">
                <ItemTemplate>
                    <asp:HyperLink ID="hlItemUrl" runat="server" NavigateUrl='<%#Eval("ButtonUrl") %>' ClientIDMode="Predictable">
                        <asp:Image ID="imgBanner" runat="server"  ClientIDMode="Predictable" />
                    </asp:HyperLink>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</div>



<%--AlternateText='<%#Eval("Heading") %>'--%>