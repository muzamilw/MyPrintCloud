<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CorporateProductListing.ascx.cs"
    Inherits="Web2Print.UI.Controls.CorporateProductListing" %>
<div class="content_area clearBoth left_right_padding AllProductsCatList">
    <div class="content_area_CPL LCL CategoryListing">
        <div class="pre_LCLB rounded_corners">
            <asp:Label ID="lblProductandServices" runat="server" Text=" Products and Services"></asp:Label>
            <asp:Repeater ID="rptCorpProductsNames" runat="server" 
                OnItemDataBound="rptCorpProductsNames_ItemDataBound" 
                onitemcommand="rptCorpProductsNames_ItemCommand">
                <ItemTemplate>
                    <div class="containerCatProList">
                        <asp:LinkButton ID="hlCP" runat="server">
                            <asp:Label ID="lblCProdName" runat="server" CssClass="spanCatProList"></asp:Label></asp:LinkButton>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</div>
