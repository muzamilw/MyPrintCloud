<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ProductList.aspx.cs" Inherits="Web2Print.UI.ProductList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<asp:UpdatePanel ID="pnlMain" runat="server">
    <ContentTemplate>
    
    <div style="width: 735px;">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr style="height: 40px">
                <td colspan="2" align="left" style="text-align: left;" valign="top">
                    <table width="100%" cellpadding="0" cellspacing="5" border="0" id="tblSectionTitleCustomPrintedLetterhead">
                        <tr style="height:8px">
                            <td colspan="2"></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblCategory" runat="server" Text="Category Name" CssClass="H1"></asp:Label>
                            </td>
                            <td align="right" style="padding-right: 20px;">
                                <a style="" href="javascript:history.back()">
                                    <asp:Image ID="imgback" runat="server" alt="" src="images/btnback.png" border="0"
                                        Height="20px" /></a>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td width="20%">
                </td>
                <td id="productListing" valign="top">
                    <asp:DataList ID="dlProducts" runat="server" RepeatColumns="3" RepeatDirection="Vertical"
                        Width="680px" border="0">
                        <ItemTemplate>
                            <div class="productBox">
                                <asp:ImageButton ID="imgbtnCategory" runat="server" 
                                    ImageUrl='<%#Eval("ImageThumbnail") %>' ToolTip='<%# Eval("CategoryName") %>' 
                                CommandArgument='<%# Eval("SubCategoriesCount") %>' 
                                    CommandName='<%# Eval("ProductCategoryID") %>' 
                                    oncommand="imgbtnCategory_Command" />
                               <br />
                               <asp:LinkButton ID="lnkbtnCategory" runat="server" CommandArgument='<%# Eval("SubCategoriesCount") %>' 
                               CommandName='<%# Eval("ProductCategoryID") %>' 
                                    Text='<%# Eval("CategoryName") %>' CssClass="productTitle" 
                                    oncommand="lnkbtnCategory_Command"></asp:LinkButton>
                               
                                <p class="productDescription">
                                    <%#Eval("Description1")%>
                                </p>
                               
                            </div>
                        </ItemTemplate>
                    </asp:DataList>
                    <table width="680px" border="0" style="display: none;">
                        <tr>
                            <td>
                                <!-- PRODUCT LIST -->
                                <div class="productBox">
                                    <a href="#" class="productIconLink">
                                        <img src="images/products/a4_wall_13.png" alt="" width="160" height="160" /></a>
                                    <a href="#" class="productTitle">A4 Wall Calendar 13 leaves</a>
                                    <p class="productDescription">
                                        Size: 210 × 297 mm - 12 date leaves plus cover printed one side of the sheet, bound
                                        at the head with hanging loop</p>
                                </div>
                                <div class="productBox">
                                    <a href="#" class="productIconLink">
                                        <img src="images/products/a3_wall_13.png" alt="" width="160" height="160" /></a>
                                    <a href="#" class="productTitle">A3 Wall Calendar 13 leaves</a>
                                    <p class="productDescription">
                                        Size: 297 × 420 mm - 12 date leaves plus cover printed one side of the sheet, bound
                                        at the head with hanging loop</p>
                                </div>
                                <div class="productBox">
                                    <a href="#" class="productIconLink">
                                        <img src="images/products/slimline_wall_13.png" alt="" width="160" height="160" /></a>
                                    <a href="#" class="productTitle">Slimline Wall Calendar 13 leaves</a>
                                    <p class="productDescription">
                                        Size: 148 × 420 mm - 12 date leaves plus cover printed one side of the sheet, bound
                                        at the head with hanging loop</p>
                                </div>
                                <div class="productBox">
                                    <a href="#" class="productIconLink">
                                        <img src="images/products/a4_wall_13.png" alt="" width="160" height="160" /></a>
                                    <a href="#" class="productTitle">A4 Wall Calendar 13 leaves</a>
                                    <p class="productDescription">
                                        Size: 210 × 297 mm - 12 date leaves plus cover printed one side of the sheet, bound
                                        at the head with hanging loop</p>
                                </div>
                                <div class="productBox">
                                    <a href="#" class="productIconLink">
                                        <img src="images/products/a3_wall_13.png" alt="" width="160" height="160" /></a>
                                    <a href="#" class="productTitle">A3 Wall Calendar 13 leaves</a>
                                    <p class="productDescription">
                                        Size: 297 × 420 mm - 12 date leaves plus cover printed one side of the sheet, bound
                                        at the head with hanging loop</p>
                                </div>
                                <div class="productBox">
                                    <a href="#" class="productIconLink">
                                        <img src="images/products/slimline_wall_13.png" alt="" width="160" height="160" /></a>
                                    <a href="#" class="productTitle">Slimline Wall Calendar 13 leaves</a>
                                    <p class="productDescription">
                                        Size: 148 × 420 mm - 12 date leaves plus cover printed one side of the sheet, bound
                                        at the head with hanging loop</p>
                                </div>
                                <div class="productBox">
                                    <a href="#" class="productIconLink">
                                        <img src="images/products/a4_wall_13.png" alt="" width="160" height="160" /></a>
                                    <a href="#" class="productTitle">A4 Wall Calendar 13 leaves</a>
                                    <p class="productDescription">
                                        Size: 210 × 297 mm - 12 date leaves plus cover printed one side of the sheet, bound
                                        at the head with hanging loop</p>
                                </div>
                                <div class="productBox">
                                    <a href="#" class="productIconLink">
                                        <img src="images/products/a3_wall_13.png" alt="" width="160" height="160" /></a>
                                    <a href="#" class="productTitle">A3 Wall Calendar 13 leaves</a>
                                    <p class="productDescription">
                                        Size: 297 × 420 mm - 12 date leaves plus cover printed one side of the sheet, bound
                                        at the head with hanging loop</p>
                                </div>
                                <div class="productBox">
                                    <a href="#" class="productIconLink">
                                        <img src="images/products/slimline_wall_13.png" alt="" width="160" height="160" /></a>
                                    <a href="#" class="productTitle">Slimline Wall Calendar 13 leaves</a>
                                    <p class="productDescription">
                                        Size: 148 × 420 mm - 12 date leaves plus cover printed one side of the sheet, bound
                                        at the head with hanging loop</p>
                                </div>
                                <br clear="all" />
                                <!--  /PRODUCT LIST -->
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
           <%-- <tr style="height: 180px">
                <td colspan="2">
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:ImageButton ID="imgBottomLogo" runat="server" ImageUrl="~/Common/images/logo.png"
                        Height="75px" CausesValidation="false" PostBackUrl="~/Default.aspx" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:DataList ID="dlBottomLinks" runat="server" RepeatColumns="7" RepeatDirection="Vertical">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkLinks" runat="server" CssClass="a4" ForeColor="Gray" Text='<%# DataBinder.Eval(Container.DataItem,"TagName")%>'
                                PostBackUrl="#"></asp:LinkButton>
                            &nbsp;&nbsp;&nbsp;
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:DataList>
                </td>
            </tr>--%>
        </table>
    </div>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
