<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OlsonFeaturedProducts.ascx.cs" Inherits="Web2Print.UI.Controls.OlsonFeaturedProducts" %>
<div class="recent-posts blocky">
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <!-- Section title -->
                <div class="section-title">
                    <h4>&nbsp;Whats New</h4>
                </div>
                <div class="carousel slide" data-ride="carousel" id="item-carousel">
                    <!-- Wrapper for slides -->
                    <div class="carousel-inner">
                        <div class="item active">
                            <div class="row">
                                <asp:Repeater ID="rptOlsonFeaturedItems" runat="server">
                                    <ItemTemplate>
                                        <div class="col-md-3 col-sm-6">
                                            <!-- single item -->
                                            <div class="s-item">
                                                <!-- Image link -->
                                                <a href="single-item.html">
                                                    <asp:Image ID="imgThumbnail" runat="server" CssClass="full_img_ThumbnailPath" ImageUrl='<%# Eval("ThumbnailPath","{0}") %>' />
                                                </a>
                                                <!-- portfolio caption -->
                                                <div class="s-caption">
                                                    <!-- heading and paragraph -->
                                                    <h4>
                                                        <a href="#" id="headingOfFeaturedPro" runat="server">
                                                            <asp:Label ID="lblFeaturedProH" runat="server" Text='<%#Eval("ProductName","{0}") %>'></asp:Label></a></h4>
                                                    <p id="descOfFPro" runat="server">
                                                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("ProductWebDescription","{0}") %>'></asp:Label>
                                                    </p>
                                                </div>
                                            </div>
                                        </div>

                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                        <div class="item">
                            <div class="row">
                                 <asp:Repeater ID="rptSecFP" runat="server">
                                    <ItemTemplate>
                                        <div class="col-md-3 col-sm-6">
                                            <!-- single item -->
                                            <div class="s-item">
                                                <!-- Image link -->
                                                <a href="single-item.html">
                                                    <asp:Image ID="imgThumbnail" runat="server" CssClass="full_img_ThumbnailPath" ImageUrl='<%# Eval("ThumbnailPath","{0}") %>' />
                                                </a>
                                                <!-- portfolio caption -->
                                                <div class="s-caption">
                                                    <!-- heading and paragraph -->
                                                    <h4>
                                                        <a href="#" id="headingOfFeaturedPro" runat="server">
                                                            <asp:Label ID="lblFeaturedProH" runat="server" Text='<%#Eval("ProductName","{0}") %>'></asp:Label></a></h4>
                                                    <p id="descOfFPro" runat="server">
                                                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("ProductWebDescription","{0}") %>'></asp:Label>
                                                    </p>
                                                </div>
                                            </div>
                                        </div>

                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                    <!-- Controls -->
                    <a class="left c-control" href="#item-carousel" data-slide="prev">
                        <i class="fa fa-chevron-left"></i>
                    </a>
                    <a class="right c-control" href="#item-carousel" data-slide="next">
                        <i class="fa fa-chevron-right"></i>
                    </a>

                </div>
            </div>
        </div>
    </div>
</div>
<script src="../Scripts/bootstrap.min.js"></script>
