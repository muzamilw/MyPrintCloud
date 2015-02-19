<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ThemeSite.Master"
    CodeBehind="FavContactDesigns.aspx.cs" Inherits="Web2Print.UI.FavContactDesigns" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Controls/BreadCrumbMenu.ascx" TagName="BreadCrumbMenu" TagPrefix="uc1" %>
<%@ Register Src="Controls/WhyChooseUs.ascx" TagName="WhyChooseUs" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContents" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBanner" runat="server">
</asp:Content>
<asp:Content ID="ProductDetailsContents" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="LightBox/js/jquery.lightbox-0.5.js"></script>
    <link rel="stylesheet" type="text/css" href="LightBox/css/jquery.lightbox-0.5.css"
        media="screen" />
    <!-- / fim dos arquivos utilizados pelo jQuery lightBox plugin -->
    <!-- Ativando o jQuery lightBox plugin -->
    <script type="text/javascript">
        $(document).ready(function () {
            BindEvents();
        });

        function BindEvents() {
            $('.gallery a').lightBox({
                maxHeight: 500,
                maxWidth: 700
            });

            // Bind Show progress
            $('.ImgCont').click(ShowProgress);
        }

    </script>
    <div class="container content_area">
        <div class="row left_right_padding">
              <div class="signin_heading_div float_left_simple dashboard_heading_signin">
                <asp:Label ID="lblFavDesgn" runat="server" CssClass="sign_in_heading">My favorite designs</asp:Label>
            </div>
               <div class="dashBoardRetrunLink">
                  
                <uc1:BreadCrumbMenu ID="BreadCumbMenuCategory" runat="server" WorkMode="MyAccount"
                MyAccountCurrentPage="Fav Design" MyAccountCurrentPageUrl="FavContactDesigns.aspx" />
                </div>
            <div class="clearBoth">

            </div>
       
            <div class="padd_bottom_30 white-container-lightgrey-border rounded_corners">
                <div class="paging_design">
                
                    <div class="float_right">
                        <table id="tblPaging" runat="server">
                            <tr>
                                <td id="tdshowing" runat="server">
                                    Showing <b>
                                        <asp:Literal ID="PagerCurrentPage" runat="server"></asp:Literal></b> of <b>
                                            <asp:Literal ID="PagerTotalPages" runat="server"></asp:Literal></b> Pages
                                </td>
                                <td>
                                    <asp:Button ID="PagerFirstButton" runat="server" Enabled="false" CssClass="next_button rounded_corners5"
                                        Text="|<" OnClick="PagerFirstButton_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="PagerPreviousButton" runat="server" Enabled="false" CssClass="next_button rounded_corners5"
                                        Text="<" OnClick="PagerPreviousButton_Click" />
                                </td>
                                <td>
                                    <asp:HiddenField runat="server" ID="PagerCurrentPagehd" />
                                    <asp:Button ID="btn1" runat="server" CssClass="selected_next_button rounded_corners5"
                                        Text="1" Visible="false" OnClick="btn1_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btn2" runat="server" CssClass="next_button rounded_corners5" Text="2"
                                        Visible="false" OnClick="btn1_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btn3" runat="server" CssClass="next_button rounded_corners5" Text="3"
                                        Visible="false" OnClick="btn1_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="PagerNextButton" runat="server" Enabled="false" CssClass="next_button rounded_corners5"
                                        Text=">" OnClick="PagerNextButton_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="PagerLastButton" runat="server" Enabled="false" CssClass="next_button rounded_corners5"
                                        Text=">|" OnClick="PagerLastButton_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="clearBoth">
                    &nbsp;</div>
                <asp:Repeater ID="dlDesignerTemplates" OnItemDataBound="dlDesignerTemplates_OnItemDataBound"
               runat="server" 
                    OnItemCommand="dlDesignerTemplates_ItemCommand">
                    <ItemTemplate>
                        <div class="LCLB BD_FavCntct rounded_corners">
                            <div class="pad5 ">
                                <div class="PDTCWB">
                                    <asp:LinkButton ID="hlImageCommand" runat="server" CausesValidation="false" CommandName="DesignOnline"
                                        CommandArgument='<%# Eval("ProductID")%>'>
                                        <div class="PDTC FI_FavCntct">
                                            <asp:Image ID="imgPic1" runat="server" CssClass="full_img_ThumbnailPath" ImageUrl='<%#Eval("Thumbnail","{0}") %>'
                                                ImageAlign="Middle" /></div>
                                    </asp:LinkButton>
                                </div>
                            </div>
                            <div class="product_selcection_thumnail_button_container gallery">
                                <asp:HyperLink ID="hlImage1" runat="server">
                                    <div class="magnifying_glass_image_Fav">
                                        <asp:Image ID="imgPic2" runat="server" Style="display: none;" />
                                    </div>
                                </asp:HyperLink>
                            </div>
                            <div class="product_selcection_thumnail_button_container_right">
                                <div id="divFavoriteInd" runat="server" class="passive_star" title="Click to Add as Favorite">
                                    &nbsp;
                                </div>
                                <asp:HiddenField ID="hfFavDesignId" runat="server" />
                            </div>
                            <div class="clearBoth">
                                &nbsp;</div>
                            <div class="product_detail_image_heading">
                                <asp:Label ID="lblProductName" runat="server" Text='<%#Eval("ProductName","{0}") %>'
                                    CssClass="themeFontColor"></asp:Label>
                                <asp:Label ID="lblProductCatName" runat="server"></asp:Label>
                            </div>
                        </div>
                    </ItemTemplate>
                  
                </asp:Repeater>
                <div class="textAlignCenter ">
                <asp:Label ID="lblTemplateNotfound" CssClass="NoFavTxt" runat="server" Visible="false"></asp:Label></div>
                <div class="paging_design">
                    <div class="float_right">
                        <table id="tblPagingB" runat="server">
                            <tr>
                                <td>
                                    Showing <b>
                                        <asp:Literal ID="ltrCurrentPage" runat="server"></asp:Literal></b> of <b>
                                            <asp:Literal ID="ltrTotalPages" runat="server"></asp:Literal></b> Pages
                                </td>
                                <td>
                                    <asp:Button ID="PagerFirstButtonB" runat="server" Enabled="false" CssClass="next_button rounded_corners5"
                                        Text="|<" OnClick="PagerFirstButton_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btnPreviousB" runat="server" Enabled="false" CssClass="next_button rounded_corners5"
                                        Text="<" OnClick="PagerPreviousButton_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btn1b" runat="server" CssClass="selected_next_button rounded_corners5"
                                        Text="1" Visible="false" OnClick="btn1_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btn2b" runat="server" CssClass="next_button rounded_corners5" Text="2"
                                        Visible="false" OnClick="btn1_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btn3b" runat="server" CssClass="next_button rounded_corners5" Text="3"
                                        Visible="false" OnClick="btn1_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="PagerNextButtonB" runat="server" Enabled="false" CssClass="next_button rounded_corners5"
                                        Text=">" OnClick="PagerNextButton_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="PagerLastButtonB" runat="server" Enabled="false" CssClass="next_button rounded_corners5"
                                        Text=">|" OnClick="PagerLastButton_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="clearBoth">
                    &nbsp;</div>
            </div>
        </div>
      
    </div>
   
    <script type="text/javascript">
        function reloadPage() {
            window.location.reload(true);
        }

        $(document).ready(function () {
            $('.passive_star').toggle(function () {
                $(this).removeClass('passive_star').addClass('active_star');
                $(this).attr('title', 'Click to remove favorite');
                var favDesignId = $(this).next().val();
                SaveFavorite(favDesignId, true);
                reloadPage();
            }, function () {
                $(this).removeClass('active_star').addClass('passive_star');
                $(this).attr('title', 'Click to add favorite');
                var favDesignId = $(this).next().val();
                SaveFavorite(favDesignId, false);
                reloadPage();
            }
            );

            $('.active_star').toggle(function () {
                $(this).removeClass('active_star').addClass('passive_star');
                $(this).attr('title', 'Click to add favorite');
                var favDesignId = $(this).next().val();
                SaveFavorite(favDesignId, false);
                reloadPage();
            }, function () {
                $(this).removeClass('passive_star').addClass('active_star');
                $(this).attr('title', 'Click to remove favorite');
                var favDesignId = $(this).next().val();
                SaveFavorite(favDesignId, true);
                reloadPage();
            }
            );

        });
        function SaveFavorite(favDesignId, isFavorite) {
            Web2Print.UI.Services.WebStoreUtility.UpdateFavoriteDesign(favDesignId, isFavorite, OnSuccess);
        }
        function OnSuccess(responce) {

        }
    </script>
</asp:Content>
