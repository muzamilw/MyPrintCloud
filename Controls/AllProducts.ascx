<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AllProducts.ascx.cs"
    Inherits="Web2Print.UI.Controls.AllProducts" %>
<h1 class="allprodHead">
    <asp:Literal runat="server" ID="ltAllProductsHeading" Text="All Products"></asp:Literal>
</h1>
<asp:Repeater ID="rptParent" runat="server" OnItemDataBound="rptParent_ItemDataBound">

    <ItemTemplate>

        <div id="MainDiv" runat="server" class="white-container-lightgrey-border rounded_corners">

            <h2 class="width_allPro">
                <a id="ltrlParentCatName" runat="server" class="Parent_Cat_allPro"></a>
            </h2>



            <div class="colContainer">
                <asp:Repeater ID="rptSubMain" runat="server" OnItemDataBound="dlChild_ItemDataBound">
                    <ItemTemplate>
                        <div class="allproducts_col">
                            <asp:Repeater ID="rptrColumnCat" runat="server" OnItemDataBound="rptrColumnCat_ItemDataBound">
                                <ItemTemplate>
                                    <div class="allproducts_col_cat">
                                        <div class="allproducts_colCat_cont">
                                            <asp:HyperLink ID="linkcat" CssClass="SubCatLink" runat="server" Text='<% # bind("CategoryName")  %>'></asp:HyperLink>
                                            <asp:Image ID="subcatimg" runat="server" style="float:right; height:33px; margin-top: -15px;" />
                                        </div>
                                        <asp:BulletedList ID="bulList" runat="server" DisplayMode="HyperLink">
                                        </asp:BulletedList>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <div class="clearBoth">

                </div>
            </div>
            <div class="clearBoth">
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>
<%--<div>
                <asp:Repeater ID="rptSubMain" runat="server" OnItemDataBound="dlChild_ItemDataBound">
                    <ItemTemplate>
                        
                        <div style="width: 100px;">
                             <a id="ancherChildCatName" runat="server" class="Child_Cat_allPro"></a>
                        </div>
                        <asp:Repeater ID="rptrColumn" runat="server" OnItemDataBound="rptSubChilds_ItemDataBound">
                            <ItemTemplate>
                                <div class="all-products-col-v1">
                                    <asp:Repeater ID="rptrColumnCat" runat="server" OnItemDataBound="rptrColumnCat_ItemDataBound">
                                        <ItemTemplate>
                                            <asp:BulletedList ID="bulList" runat="server" DisplayMode="HyperLink"></asp:BulletedList>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ItemTemplate>
                </asp:Repeater>
            </div>--%>