<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ThemeSite.Master" AutoEventWireup="true"
    CodeBehind="RealEstateProducts.aspx.cs" Inherits="Web2Print.UI.RealEstateProducts" %>

<asp:Content ID="PropertyPageHead" ContentPlaceHolderID="HeadContents" runat="server">
</asp:Content>
<asp:Content ID="PageBanner" ContentPlaceHolderID="PageBanner" runat="server">
</asp:Content>
<asp:Content ID="PropertyPageMainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="divShd" class="opaqueLayer">
    </div>
    <div runat="server" id="divbg" visible="false" class="categorydivBG">&nbsp;</div>
    <div class="container content_area">

        <div id="pnlProductList" runat="server" class="padd_bottom_30 ">
            <asp:Repeater ID="rptProducts" runat="server" OnItemDataBound="rptProducts_ItemDataBound">
                <ItemTemplate>
                    <div id="Maincontainer" class="CAT_Body rounded_corners" runat="server">
                        <div class="LCLB">
                            <asp:HyperLink ID="hlCategory" runat="server">
                                <div class="PDTC_CAT FI_CAT">
                                    <asp:Image ID="imgThumbnail" CssClass="CAT_ThumbnailPath" runat="server" ImageUrl='<%# Eval("ThumbnailPath","{0}") %>' />
                                </div>
                            </asp:HyperLink>
                        </div>
                        <div class="product_detail_image_heading_CAT">
                            <asp:Label ID="lblProductName" runat="server" Text='<%#Eval("ProductName","{0}") %>'></asp:Label>
                        </div>
                        <div class="topcat_desc_CAT">
                            <asp:Literal ID="lblDescription1" runat="server"></asp:Literal>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <div class="clearBoth">
            </div>
        </div>

    </div>
</asp:Content>
