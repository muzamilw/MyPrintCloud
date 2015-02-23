﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OxfordTopLevelCategories.ascx.cs" Inherits="Web2Print.UI.Controls.OxfordTopLevelCategories" %>
<div class="container body-container spacer-top ">
    <!--Main Content-->
    <div class="row">
        <!--Left Panel-->
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
                                        <a id="ltrlParentCatName" runat="server" data-toggle="collapse" data-parent="#accordionmenu">Business Cards
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
        <div id="middle-content" class="col-xs-12 col-sm-8 col-md-6 col-lg-6 ">
            <div class="row main-content">
                <div class="col-xs-12">
                    <div class="middle_inner_section">
                        <div class="row" id="home_page">
                            <asp:Repeater ID="seaBlueTopCategories" runat="server" OnItemDataBound="seaBlueTopCategories_ItemDataBound">
                                <ItemTemplate>
                                    <div class="col-sm-12 col-md-6 product-box" itemscope itemtype="http://schema.org/Product">
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
        </div>
        <!--/Center Part-->
        <div class="clearfix visible-sm"></div>
        <!--Right Panel-->
        <div id="sidebar-right" class="col-xs-12 col-sm-12 col-md-3 col-lg-3  sidebar" style="padding-left:0px; padding-right:0px;">
            <div class="row visible-xs">
                <div class="col-xs-12">
                    <button type="button" class="col-xs-12 btn btn-darkblue btn-xs" data-toggle="collapse" data-target="#sidebar2"><i class="fa fa-plus-square fa-2x pull-right"></i></button>
                </div>
            </div>
            <div class="col-xs-12 col-sm-6 col-md-12 col-lg-12 " style="padding-left:0px; padding-right:0px;">
                <div class="panel panel-info" id="contactus_sidebar">
                    <div class="panel-heading">
                        <h3 class="panel-title">Contact us</h3>
                    </div>
                    <div class="panel-body">
                        <div itemscope="" itemtype="http://schema.org/Organization">
                            <h2 itemprop="name" id="companyname" runat="server">Company</h2>
                            <div itemprop="address" itemscope="" itemtype="http://schema.org/PostalAddress">
                                <b>Main address</b>:<br>
                                <span itemprop="streetAddress" id="addressline1" runat="server">Address 1..<br>
                                    Address 2..</span><br>
                                <span itemprop="postalCode" id="cityandCode" runat="server">City and Zip Code</span><br>
                                <span itemprop="addressLocality" id="stateandCountry" runat="server">state &amp; Country</span>
                            </div>
                            Tel:<span itemprop="telephone" id="telno" runat="server">(xxx) xxx-xxxx </span>
                            <br>
                            E-mail: <span itemprop="email" id="emailadd" runat="server">john_doe@company.com</span>
                            </span>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-xs-12 col-sm-6 col-md-12 col-lg-12 " style="padding-left:0px; padding-right:0px;">
                <div class="panel panel-info" id="subscribe_sidebar">
                    <div class="panel-heading">
                        <h3 class="panel-title">Subscribe here</h3>
                    </div>
                    <div class="panel-body">
                        <form id="user_subscribe_1617216226" method="post" action="http://samplestore.onprintshop.com/demo-oxfordblue#" novalidate="novalidate">
                            <div class="form-group">
                                <div class="input-group">
                                    <span class="input-group-addon"><span class="fa fa-envelope"></span></span>
                                    <asp:TextBox  id="txtEmailbox" runat="server" CssClass="form-control btn-rounded" Width="100"></asp:TextBox>
                               
                                </div>
                                <span id="errorMsg" runat="server" class="error-block text-danger"></span>
                                <p class="help-block">Subscribe Here to receive promotional offers.</p>
                            </div>
                            <asp:Button ID="GoSubscribe" runat="server" CssClass="btn btn-primary btn-rounded" Text="Subscribe" OnClick="GoSubscribe_Click" />
                        </form>
                    </div>
                </div>
            </div>
            <div class="col-xs-12 col-sm-6 col-md-12 col-lg-12 " style="padding-left:0px; padding-right:0px;">
                <div class="panel panel-info" id="Div2">
                    <div class="panel-heading">
                        <h3 class="panel-title">Testimonials</h3>
                    </div>
                    <div class="panel-body">
                        <div id="testimonial-sidebar-1002820984" data-interval="6000" class="carousel slide testimonial-panel">
                            <div class="carousel-inner">
                                <div class="item">
                                    <blockquote>
                                        <p>
                                            <br>
                                            I used Company services for my business cards and I must tell that I am much&nbsp; pleased with the quality of printed cards their prompt and professional service. Good luck to your business.<br>
                                            <br>
                                            Best regards,<br>
                                            &nbsp;
                                        </p>
                                        <small>Henry Roberts</small>
                                    </blockquote>
                                </div>
                                <div class="item active">
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
                    <div class="panel-footer"><span class="carousel-controls"><a  class="left" data-slide="prev"><span class="glyphicon glyphicon-chevron-left">&nbsp;</span></a><a  class="right" data-slide="next"><span class="glyphicon glyphicon-chevron-right">&nbsp;</span></a></span></div>
                </div>
            </div>
            <!--id="sidebar2"-->
        </div>
        <!--Right Panel-->
        <div class="col-xs-12 col-sm-12 col-md-3 col-lg-3  hidden" id="alternate2"></div>
    </div>
    <!--/Main Content-->
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
