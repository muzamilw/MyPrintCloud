<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ThemeSite.Master"
    CodeBehind="SavedDesignes.aspx.cs" Inherits="Web2Print.UI.SavedDesignes" %>

<%@ Register Src="~/Controls/BreadCrumbMenu.ascx" TagName="BreadCrumbMenu" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="controlBodyDiv" runat="server">
        <div class="container content_area">
            <div class="row left_right_padding">
                <div class="signin_heading_div float_left_simple dashboard_heading_signin">
                 <asp:Label ID="lblCategoriesLinks1" runat="server" CssClass="sign_in_heading">My saved designs</asp:Label>
            </div>
               <div class="dashBoardRetrunLink">
                      <uc1:BreadCrumbMenu ID="BreadCumbMenuCategory" runat="server" WorkMode="MyAccount"
                            MyAccountCurrentPage="Saved Design" MyAccountCurrentPageUrl="SavedDesignes.aspx" />
                </div>
            <div class="clearBoth">

            </div>
             
                <div class="padd_bottom_30 white-container-lightgrey-border rounded_corners">
                   
                   <asp:Repeater ID="dlProducts1" runat="server" 
                        OnItemDataBound="dlAllProducts_ItemDataBound" OnItemCommand="dlProducts1_ItemCommand">
                        <ItemTemplate>
                            <div class="BDSaved rounded_corners">
                                <div class="pad5">
                                    <div class="PDTCWB">
                                        <div class="PDTC_SD FISaved" style="display: block;">
                                            <asp:Image ID="imgThumbnail" CssClass="svdDesignImgThumb" runat="server" />
                                        </div>
                                    </div>
                                    <div class="LCLB product_detail_image_heading rounded_corners">
                                        <asp:Label ID="lblProductName" runat="server" Text='<%#Eval("ProductName","{0}") %>'></asp:Label>
                                    </div>
                                </div>
                                <div id="Div1" class="cntReorderDesign">
                                    <asp:ImageButton ID="ImageButton1" runat="server" CssClass=" btnReorderSavedDesigns"
                                        ImageUrl = "~/images/repeat.png"
                                        ToolTip="Reorder saved design" CommandName="ProcessSaveDesign" CommandArgument='<%# Eval("ItemID") %>' />
                                </div>
                                <div id="SavedDiv" class=" btnDeleteSavedDesignsContainer">
                                    <asp:ImageButton ID="DeleteSavedDesgn" runat="server" CssClass="btnDeleteSavedDesigns"
                                        ImageUrl="~/images/delete.png"  Visible="false" OnClientClick="return confirm('Are you sure you want to delete this Saved design?');"
                                        ToolTip="Delete saved design" CommandName="DeleteSavedDesgn" CommandArgument='<%# Eval("ItemID") %>' />
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                   
                    <asp:Label ID="lblProductNotFound" CssClass="simpleTextLarge" runat="server" Text="You have No saved templates"></asp:Label>
                <div class="clearBoth">
                    &nbsp;
                </div>
                </div>
                  <div class="clearBoth">
                         </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $('.feature_image').click(ShowProgress);
        });
    </script>
</asp:Content>
