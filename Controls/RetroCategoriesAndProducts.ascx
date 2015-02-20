<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RetroCategoriesAndProducts.ascx.cs" Inherits="Web2Print.UI.Controls.RetroCategoriesAndProducts" %>

<div class="container body-container spacer-top ">
    <div class="row">
        <div class="col-xs-12 col-sm-4 col-md-3 col-lg-3  sidebar-offcanvas sidebar" id="sidebar-left" role="navigation">
            <div class="panel panel-info" id="togglemenu_sidebar">
                <div class="panel-heading">
                    <h3 class="panel-title">Product Category
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="panel-group" id="accordionmenu">
                        <asp:Repeater ID="rptRetroPCats" runat="server" OnItemDataBound="rptRetroPCats_ItemDataBound">
                            <ItemTemplate>
                                <div id="parentCatsContainer" runat="server" class="panel panel-default">
                                    <div class="panel-heading">
                                        <a id="ltrlParentCatName" runat="server" data-toggle="collapse" data-parent="#accordionmenu" >Business Cards
					                </a>
                                    </div>
                                    <div id="sidebar_menu_collapse1" class="sideMenuItems panel-collapse collapse ">
                                        <div class="panel-body">
                                            <ul class="submenu list-unstyled">
                                                <asp:Repeater ID="rptSubMenuCats" runat="server" OnItemDataBound="rptSubMenuCats_ItemDataBound">
                                                    <ItemTemplate>
                                                        <li><a id="ltrlSubCatName" runat="server" href="../en/standard-business-cards/index.htm"><i class="fa fa-play-circle"></i><span>Standard Business Cards</span></a></li>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>

                    </div>
                </div>
            </div>

        </div>
        <div class="col-xs-12 col-sm-4 col-md-3 col-lg-3  hidden" id="alternate"></div>
        <!--/Left Panel-->
        <!--Center Part-->
        <div id="middle-content" class="col-xs-12 col-sm-8 col-md-9 col-lg-9 retrohome">
            <div class="row main-content">

                <div class="col-md-12 col-xs-12">
                    <div class="page-header">
                        <h1>FEATURED PRODUCTS</h1>
                    </div>
                    <div class="middle_inner_section">
                        <div class="row" id="home_page">
                            <asp:Repeater ID="featuredProRpt" runat="server" OnItemDataBound="featuredProRpt_ItemDataBound">
                                <ItemTemplate>
                                    <div class="col-sm-12 col-md-4 product-box" itemscope itemtype="http://schema.org/Product">
                                        <div class="hover-box-shadow">
                                            <p class="text-center">
                                                <a href="../en/standard-business-cards/index.htm" class='thumbnail'>
                                                    <img id="imgFP" runat="server" src="../themes/retroresponsive//images/product/business-card_thumb.jpg" alt="Standard Business Cards" title="Standard Business Cards" class="img-responsive" itemprop="image" /></a>
                                            </p>
                                            <h3 id="h3FP" runat="server" itemprop="name">Standard Business Cards</h3>
                                            <div   class="product-desc" itemprop="description">
                                                <p id="paraDescTitle" runat="server">Creative Business Cards to Impress Clients!</strong></p>
                                                <p id="paraDes" runat="server">Create cards that make the first impression</p>
                                                <p class="text-center"><a id="viewDetaillink" runat="server" href="../en/standard-business-cards/index.htm" class='btn btn-info' itemprop="url">View details <i class="fa fa-angle-double-right"></i></a></p>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>

                            
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-xs-12 col-sm-12 col-md-3 col-lg-3  hidden" id="alternate2"></div>
    </div>
</div>
<script>
    $(document).ready(function () {
        sideBarClick(0)
    });

    function sideBarClick(dataid) {
        $('.sideMenuItems').each(function () {

            $(this).removeClass('in');
        });
        var idOfCatPro = $('a[data-id=' + dataid + ']').attr("id");
        if ($("#" + idOfCatPro + "").parent().next().hasClass('in')) {
            $("#" + idOfCatPro + "").parent().next().removeClass('in');
        } else {
            $("#" + idOfCatPro + "").parent().next().addClass('in');
        }

    }

</script>
