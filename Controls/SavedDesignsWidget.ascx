<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SavedDesignsWidget.ascx.cs"
    Inherits="Web2Print.UI.Controls.SavedDesignsWidget" %>
<div class="SavedDesignsContainer">
    <div class="SavedDesignsWidget">
        <h2 id="SvdDesignWdgtHeading" class="SvdDesignWdgtHeading" runat="server">Your  <strong>saved designs</strong></h2>

        <asp:Button ID="btnRegister" runat="server" OnClientClick="window.navigate.url='/login.aspx'"
            CssClass="btnRegisterSvdDesign rounded_corners" Text="Sign In / Register" />

        <div class="SavedDesignsWidgetItemCont">
            <asp:Repeater ID="dlProducts1" runat="server" OnItemDataBound="dlAllProducts_ItemDataBound"
                OnItemCommand="dlProducts1_ItemCommand">
                <ItemTemplate>
                    <div class="svdDesgnItem">
                        <div class="pad5">
                            <div class="svdDesgnImgCntr">
                                <asp:LinkButton ID="lbtnGo" runat="server" CommandName="ProcessSaveDesign" CommandArgument='<%# Eval("ItemID") %>'
                                    CssClass="svdDesgnImgLnk" Style="display: block;">
                                    <asp:Image ID="imgThumbnail" CssClass="svdDesignThumb" runat="server" />
                                </asp:LinkButton>
                            </div>
                            <div class="svdDesgnName">
                                <asp:Label ID="lblProductName" runat="server" Text='<%#Eval("ProductName","{0}") %>'
                                    CssClass="themeFontColor"></asp:Label>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <div class="clearBoth">
            </div>
            <div class="lblSavedDesignsNotFound col-xs-12 col-sm-12 col-md-12 col-lg-12">
                <asp:Label ID="lblSavedDesignsNotFound" runat="server"
                    Visible="false"></asp:Label>
            </div>
            <a id="linkSavedDesigns" href="/SavedDesignes.aspx" runat="server" clientidmode="Static">More Saved Designs...</a>
        </div>
    </div>
    <div class="SavedDesignsWidgetPost">
    </div>
     <div class="clearBoth">

        </div>
</div>
