<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TumbasFeaturedCarousal.ascx.cs" Inherits="Web2Print.UI.Controls.TumbasFeaturedCarousal" %>
<%@ Register Src="~/Controls/SubscribeToNewsletter.ascx" TagPrefix="uc1" TagName="SubscribeToNewsletter" %>

<div class="outter">

    <!-- BEGIN NEWSLETTER & RECENT BLOG -->
    <section class="newsblog">
        <div class="container">
            <div class="row">
                <div class="col-md-3 col-sm-5">
                    <div class="block newsletter">
                        <h3 style="margin-bottom: 10px !important; margin-top: 0px !important; font-weight: bold;">Newsletter</h3>
                        <uc1:SubscribeToNewsletter runat="server" ID="SubscribeToNewsletter" />

                    </div>
                </div>
                <div class="col-md-9 col-sm-7">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="box-title">
                                <h2>FEATURED PRODUCTS</h2>
                            </div>
                        </div>
                    </div>
                    <div class="row recent-blog">
                        <div class="col-md-12">
                            <div class="slide-recent-blog">
                                <div class="col-md-12">
                                    <div class="postf">
                                        <figure>
                                            <a href="javascript:;">
                                                <img src="https://dl.dropboxusercontent.com/u/112963099/Develpixel/Demo/tumbas/boxed/images/mini-blog-1.jpg" alt="">
                                            </a>
                                        </figure>
                                        <div class="content">
                                            <h2 class="post-title">
                                                <a href="javascript:;">Proin gravida nibh vel velit auctor aliquetnean</a>
                                            </h2>
                                            <span class="post-meta">2015 August 11
                                            </span>
                                            <div class="post-content">
                                                <p>Sollicitudin, lorem quis bibendum auctor, nisi elit consequat ipsum...</p>
                                                <a href="javascript:;" class="read-more">Read more &rarr;
                                                </a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="postf">
                                        <figure>
                                            <a href="javascript:;">
                                                <img src="https://dl.dropboxusercontent.com/u/112963099/Develpixel/Demo/tumbas/boxed/images/mini-blog-3.jpg" alt="">
                                            </a>
                                        </figure>
                                        <div class="content">
                                            <h2 class="post-title">
                                                <a href="javascript:;">Proin gravida nibh vel velit auctor aliquetnean</a>
                                            </h2>
                                            <span class="post-meta">2015 August 11
                                            </span>
                                            <div class="post-content">
                                                <p>Sollicitudin, lorem quis bibendum auctor, nisi elit consequat ipsum...</p>
                                                <a href="javascript:;" class="read-more">Read more &rarr;
                                                </a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="postf">
                                        <figure>
                                            <a href="javascript:;">
                                                <img src="https://dl.dropboxusercontent.com/u/112963099/Develpixel/Demo/tumbas/boxed/images/mini-blog-2.jpg" alt="">
                                            </a>
                                        </figure>
                                        <div class="content">
                                            <h2 class="post-title">
                                                <a href="javascript:;">Proin gravida nibh vel velit auctor aliquetnean</a>
                                            </h2>
                                            <span class="post-meta">2015 August 11
                                            </span>
                                            <div class="post-content">
                                                <p>Sollicitudin, lorem quis bibendum auctor, nisi elit consequat ipsum...</p>
                                                <a href="javascript:;" class="read-more">Read more &rarr;
                                                </a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="postf">
                                        <figure>
                                            <a href="javascript:;">
                                                <img src="https://dl.dropboxusercontent.com/u/112963099/Develpixel/Demo/tumbas/boxed/images/mini-blog-1.jpg" alt="">
                                            </a>
                                        </figure>
                                        <div class="content">
                                            <h2 class="post-title">
                                                <a href="javascript:;">Proin gravida nibh vel velit auctor aliquetnean</a>
                                            </h2>
                                            <span class="post-meta">2015 August 11
                                            </span>
                                            <div class="post-content">
                                                <p>Sollicitudin, lorem quis bibendum auctor, nisi elit consequat ipsum...</p>
                                                <a href="javascript:;" class="read-more">Read more &rarr;
                                                </a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="postf">
                                        <figure>
                                            <a href="javascript:;">
                                                <img src="https://dl.dropboxusercontent.com/u/112963099/Develpixel/Demo/tumbas/boxed/images/mini-blog-3.jpg" alt="">
                                            </a>
                                        </figure>
                                        <div class="content">
                                            <h2 class="post-title">
                                                <a href="javascript:;">Proin gravida nibh vel velit auctor aliquetnean</a>
                                            </h2>
                                            <span class="post-meta">2015 August 11
                                            </span>
                                            <div class="post-content">
                                                <p>Sollicitudin, lorem quis bibendum auctor, nisi elit consequat ipsum...</p>
                                                <a href="javascript:;" class="read-more">Read more &rarr;
                                                </a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--/.slider-recent-blog -->
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <!-- END NEWSLETTER & LATEST BLOG -->


</div>
<script src="../Scripts/jquery-1.9.1.min.js"></script>
<script src="../js/Tumbas.featured.carousel.js"></script>
<script src="../js/owl.carousel.js"></script>
