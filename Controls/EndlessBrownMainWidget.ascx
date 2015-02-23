<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EndlessBrownMainWidget.ascx.cs" Inherits="Web2Print.UI.Controls.EndlessBrownMainWidget" %>
<div class="container body-container spacer-top ">
    <!--Main Content-->
    <div class="row">
        <!--Left Panel-->
        <div class="col-xs-12 col-sm-4 col-md-3 col-lg-3  sidebar-offcanvas sidebar" id="sidebar-left" role="navigation" style="padding-left:0px;">
            <div class="panel panel-info" id="verticalmenu_sidebar">
                <div class="panel-heading">
                    <h3 class="panel-title">Product Category</h3>
                </div>
                <div class="panel-body">
                    <div id="cssmenu">
                        <ul class="list-unstyled">
                            <asp:Repeater ID="rptParentCats" runat="server" OnItemDataBound="rptParentCats_ItemDataBound">
                                <ItemTemplate>
                                    <li class="has-sub">
                                        <a id="parentCatLink" runat="server"><i class="fa fa-play-circle"></i><span id="parentCatName" runat="server">Business Cards</span></a>
                                        <ul>
                                            <asp:Repeater ID="rptchildCats" runat="server" OnItemDataBound="rptchildCats_ItemDataBound">
                                                <ItemTemplate>
                                                    <li>
                                                        <a id="subCatLink" runat="server"><i class="fa fa-play-circle"></i><span id="subCatName" runat="server">Standard Business Cards</span></a></li>

                                                </ItemTemplate>
                                            </asp:Repeater>

                                        </ul>
                                    </li>
                                </ItemTemplate>

                            </asp:Repeater>

                        </ul>
                    </div>
                </div>
            </div>
            <div class="panel panel-info" id="testimonial_sidebar">
                <div class="panel-heading">
                    <h3 class="panel-title">Testimonials</h3>
                </div>
                <div class="panel-body">
                    <div id="testimonial-sidebar-2928791" data-interval="6000" class="carousel slide testimonial-panel">
                        <div class="carousel-inner">
                            <div class="item active">
                               <blockquote>
                        <p id="lblRaveReview" runat="server">
                                           
                        </p>
                        <small id="lblReviewBy" runat="server"></small>
                    </blockquote>
                            </div>
                            <div class="item ">
                                <blockquote>
                                    <p>
                                        As a realtor I am in regular need of business cards. I would like to thank you for the artistic work you undertook for printing cards for my real estate business. I appeciate your professional service and fast delivery. Company online printing facility surely does away with all hassles involved in conventional printing.
                                    </p>
                                    <small>Kenneth</small>
                                </blockquote>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel-footer"><span class="carousel-controls"><a class="left" data-slide="prev"><span class="glyphicon glyphicon-chevron-left">&nbsp;</span></a><a class="right" data-slide="next"><span class="glyphicon glyphicon-chevron-right">&nbsp;</span></a></span></div>
            </div>
        </div>
        <div class="col-xs-12 col-sm-4 col-md-3 col-lg-3  hidden" id="alternate"></div>
        <!--/Left Panel-->
        <!--Center Part-->
        <div id="middle-content" class="col-xs-12 col-sm-8 col-md-9 col-lg-9 ">
            <div class="row main-content">
                <div class="col-xs-12">
                    <div id="top-banner" class="carousel slide hidden-xs " data-interval="6000" data-ride="carousel">
                        <ol class="carousel-indicators">
                            <li data-target="#top-banner" data-slide-to="0" class="active"></li>
                            <li data-target="#top-banner" data-slide-to="1" class=""></li>
                        </ol>
                        <div class="carousel-inner">
                            <div class="item text-center active">
                                <img src="../App_Themes/EndlessBrown/Images/banner1.jpg" alt="Banner 1" title="Banner 1"><div class="carousel-caption" style=""></div>
                            </div>
                            <div class="item text-center">
                                <img src="../App_Themes/EndlessBrown/Images/banner2.jpg" alt="banner 2" title="banner 2"><div class="carousel-caption" style=""></div>
                            </div>
                        </div>
                        <a class="left carousel-control" data-slide="prev"><span class="glyphicon glyphicon-chevron-left"></span></a><a class="right carousel-control" data-slide="next"><span class="glyphicon glyphicon-chevron-right"></span></a>
                    </div>
                    <div class="header-content">
                        <div class="panel panel-info">
                            <div class="col-md-6">
                                <h3 class="panel-title">Enjoy Quick and Simple Process for Product Personalization and Order Placement</h3>
                                <p>We allow for quick product designing by browsing our readily available designs. Personalize the available templates as per your specific business and use it. Create an entirely new design that suits to your business branding at create design. You can also upload your design and business credentials. Simply place your order and we deliver it next to your door.</p>
                            </div>
                            <div class="col-md-6">
                                <ul class="nav nav-pills">
                                    <li><a href="javascript:void(0)"><i class="fa fa-folder-open fa-border fa-2x"></i>
                                        <br>
                                        Browse</a></li>
                                    <li>-or-</li>
                                    <li><a href="javascript:void(0)"><i class="fa fa-pencil fa-border fa-2x"></i>
                                        <br>
                                        Create</a></li>
                                    <li>-or-</li>
                                    <li><a href="javascript:void(0)"><i class="fa fa-upload fa-border fa-2x"></i>
                                        <br>
                                        Upload</a></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <div class="middle_inner_section">
                        <div class="row" id="home_page">
                             <asp:Repeater ID="BrownTopCategories" runat="server"  OnItemDataBound="BrownTopCategories_ItemDataBound">
                                <ItemTemplate>
                                    <div class="col-sm-6 col-md-3 product-box" itemscope itemtype="http://schema.org/Product">
                                        <div class="hover-box-shadow">
                                            <p class="text-center">
                                                <a id="ProductUrl" runat="server" href="../en/standard-business-cards/index.htm" class='thumbnail'>
                                                    <img id="productImge" runat="server" src="../themes/seablue//images/product/business-card_thumb.jpg" alt="Standard Business Cards" title="Standard Business Cards" class="img-responsive" itemprop="image" /></a>
                                            </p>
                                            <h3 itemprop="name" id="productName" runat="server">Standard Business Cards</h3>
                                            <div class="product-desc" itemprop="description">
                                                <p id="productDesc" runat="server">Create cards that make the first impression</p>
                                            </div>
                                            <p class="text-center"><a id="prductlinkUrl" runat="server" href="../en/standard-business-cards/index.htm" class='btn btn-info' itemprop="url">View details <i class="fa fa-angle-double-right"></i></a></p>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
            </div>
            <div class="middle-footer-content">
                <p></p>
            </div>
        </div>
        <!--/Center Part-->
    </div>
    <!--/Main Content-->
</div>
<script src="../js/script.js"></script>
