<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MetroProductCategories.ascx.cs" Inherits="Web2Print.UI.Controls.MetroProductCategories" %>
<div class="container">
    <div class="row">
        <div class="col-sm-12 col-md-6 col-lg-6 col-xs-12">
            <h1 class="MetroallprodHead">Product Categories
            </h1>
            <asp:Repeater ID="rptParent" runat="server" OnItemDataBound="rptParent_ItemDataBound">
                <ItemTemplate>
                    <div id="MainDiv" runat="server" class="MetroProCats">
                        <h2 class="Metrowidth_allPro">
                            <a id="ltrlParentCatName" runat="server" class="MetroParent_Cat_allPro"></a>
                        </h2>

                        <div class="MetrocolContainer">
                            <asp:Repeater ID="rptSubMain" runat="server" OnItemDataBound="dlChild_ItemDataBound">
                                <ItemTemplate>
                                    <div class="Metroallproducts_col">
                                        <asp:BulletedList ID="ParentbulList" runat="server" DisplayMode="HyperLink" Style="list-style: inherit; margin-left: -20px;">
                                        </asp:BulletedList>

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
        </div>
        <div class="col-sm-12 col-md-6 col-lg-6 col-xs-12">
            <h1 class="MetroallprodHead">Location
            </h1>
            <img src="../App_Themes/Metromania/Images/map.png" class="metroMap" />
        </div>
    </div>


</div>
