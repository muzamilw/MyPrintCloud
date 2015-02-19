<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopLevelCategoryWiget.ascx.cs"
    Inherits="Web2Print.UI.Controls.TopLevelCategoryWiget" %>

<div class="bkTopCategories">
<div id="cntBackgroundBanner" runat="server" class="bgS5White BannerBakImage">
</div>

<div class="container content_area clearBoth " style="padding-left:0px; padding-right:0px;">
    <div class="row left_right_padding cntTopLevelCategory">

        <asp:Repeater ID="rptTopLevelCategory" runat="server" OnItemDataBound="rptTopLevelCategory_ItemDataBound">
            <ItemTemplate>

                <div id="MainContainer" class="Top_Cat_Body  rounded_corners" runat="server">
                    <div class="LCLB">
                       
                        <asp:HyperLink ID="hlProductDetail" runat="server">
                            <div class="PDTC_TL FI_TL H4B_TL">
                                <asp:Image ID="imgThumbnail" runat="server" CssClass="Top_Cat_ThumbnailPath" ImageUrl='<%# Eval("ThumbnailPath","{0}") %>' />
                            </div>
                        </asp:HyperLink>
                        <div class="product_detail_image_heading_S5 s5TxtStyle themeFontColor Top_Cat_Display">
                        <asp:Literal ID="lblbotmCatName" runat="server"></asp:Literal>
                    </div>
                        <div class="topcat_desc displayNone_TLS5">
                            <asp:Literal ID="lblDescription1" runat="server"></asp:Literal>
                        </div>
                    </div>
                    
                </div>


            </ItemTemplate>
        </asp:Repeater>

        <div class="clearBoth">
            &nbsp;
        </div>

    </div>
  
</div>
<div class="clearBoth">
    &nbsp;
</div>
    </div>
