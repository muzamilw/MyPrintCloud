<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CorportateProducts.ascx.cs"
    Inherits="Web2Print.UI.Controls.CorportateProducts" %>
<div class="content_area_CP container" runat="server">
    <div class=" left_right_padding row">
        <asp:Repeater ID="rptCorpCategory" runat="server" OnItemDataBound="rptCorpCategory_ItemDataBound">
            <ItemTemplate>
                <div id="MainContainer" class="Corp_Cat_Body rounded_corners" runat="server">
                    <div class="Corp_Cat_Name_Heading Corp_Cat_Display">
                        <asp:Literal ID="lblProductName" runat="server"></asp:Literal>
                    </div>
                    <asp:HyperLink ID="hlProductDetail" runat="server">
                    <div class="Corp_PDTC_TL Corp_FI_TL">
                        <asp:Image ID="imgThumbnail" runat="server" CssClass="Corp_Cat_ThumbnailPath" ImageUrl='<%# Eval("ThumbnailPath","{0}") %>' />
                    </div>
                    </asp:HyperLink>
                    <div class="topcat_desc">
                        <asp:Literal ID="lblDescription1" runat="server"></asp:Literal>
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
