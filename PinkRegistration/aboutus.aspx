<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="aboutus.aspx.cs" MasterPageFile="PinkRegister.Master"
    Inherits="Web2Print.UI.aboutus" %>

<%@ Register Src="PinkRegFooter.ascx" TagName="PinkRegFooter" TagPrefix="uc1" %>


<%@ Register Src="Header.ascx" TagName="Header" TagPrefix="uc2" %>


<asp:Content ID="Content4" runat="server" ContentPlaceHolderID="HeadContents">
    <link href="PinkSite.css" rel="stylesheet" />
    <script src="/pinkregistration/js/main.js"></script>
    <script src="/pinkregistration/js/video.js"></script>
    <script src="/pinkregistration/js/responsive.js"></script>
    <script src="/pinkregistration/js/libs/iOS.js"></script>
    <script src="/pinkregistration/js/libs/iscroll.js"></script>

</asp:Content>




<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="cphHeader">
    <uc2:Header ID="Header1" runat="server" />
</asp:Content>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainContent">


    <div id="container">
        <div id="main" role="main">

            <div id="tools" class="section clear_fix sized">
                <h1 id="pickUpTools" class="toUpper sectionTitle">Welcome <span class="noBold">to PinkCards.com</span></h1>

                <div class="sectionTitle">
                    <h3>Become a Pink Partner</h3>
                </div>

                <p class="sectionText">
                    We’ve launched an exciting new print portal in the UK that allows customers to order business cards and other print related products online. We would like you to be part of our growing family of friendly local Pink Partners.
                </p>
                <p class="sectionText">
                    We take out all the pain of developing and launching your own print web store by giving you a supercharged ‘white label’ web store specific for the instant print and copy market.
                </p>
                <p class="sectionText">
                    Although we set the prices, templates and products, this doesn’t mean we are a franchise in disguise, far from it.  
                </p>
                <p class="sectionText">
                    The products and prices are carefully chosen and take into account popular buying trends so you don’t have to be price and product conscious.  
                </p>
                <p class="sectionText">
                    Print buyers do not necessarily buy online based on being the cheapest. Many other factors are considered including the supplier being local and has the ability to bespoke any requirement.
                </p>
                <p class="sectionText">
                    You get to do all the print yourself or outsource it to any print supplier you prefer.  There is no commission or royalty fees for artwork to pay.  All we charge is a low monthly fee for our services that you can cancel at any time.
                </p>
                <p class="sectionText">
                    We’re a technology focused software company specialising in web 2 print, print management solutions and online marketing techniques such as traffic generation and SEO.
                </p>
                <p class="sectionText">
                    Our function is to give you a strong, clean and professional web presence, generate you leads and to provide you with the ability to market your OWN DOMAIN name independently without all the hassle of reacting to changing trends in buying habits, managing content, SEO tasks and hosting issues.
                </p>
                <p class="sectionText">
                    We work collaboratively, which means we focus on our specialities whilst market locally by promoting your OWN DOMAIN and PinkCards.com.  From time to time we will announce major nationwide campaigns at our costs to promote PinkCards.com which in turns promotes you.  These campaigns are not just online, our ambition is to make PinkCards.com a name recognised with quality print products like business cards and flyers.
                </p>
                <p class="sectionText">
                    See Marketing Tips for some ideas which you can do yourself in your area.

                </p>



                <div class="sectionTitle">
                    <div>
                        <h1 class="toUpper sectionTitle"><span class="noBold">Sounds</span> Compelling?</h1>
                    </div>
                </div>

                <p class="sectionText">
                    There's more with PinkCards.com, and as a Pink Partner you get your own branded web store which
                </p>

                <ul>


                    <li>
                        <p class="sectionText"><strong class="demi">forwards</strong> you all the leads</p>
                    </li>
                    <li>
                        <p class="sectionText"><strong class="demi">emails</strong> you the order details with artwork attached and</p>
                    </li>
                    <li>
                        <p class="sectionText"><strong class="demi">lets</strong> your customers pay directly into your PayPal account.</p>
                    </li>


                </ul>
                <p class="sectionText">
                    We do all the localisation SEO and generate you traffic for you.
                </p>



                <div class="sectionTitle">
                    <div>
                        <h1 class="toUpper sectionTitle"><span class="noBold">Make yourself</span> known</h1>
                    </div>
                </div>
                <p class="sectionText">
                    We encourage you to take all new leads offline and promote your other services as the local print and design specialist. Let’s face it, that’s how you already work and that’s how your customers prefer it.
                </p>

                <div class="featureSection">
                    <div class="featureRow clear_fix">
                        <div class="featureUnit3Column tools-draw">
                            <h3 class="yellow">Your brand</h3>
                            <p>
                                <span class="bold">Redirect your own branded Web 2 Print store accepting online payments with your domain name.</span><br>
                            </p>
                            <%--<div class="detail grey">Free</div>--%>
                        </div>
                        <div class="featureUnit3Column tools-sketch">
                            <h3 class="red">Shopping Cart</h3>
                            <p>
                                <span class="bold">No commissions, no royalties.
All payments go directly into your PayPal account.
                                </span>
                                <br>
                            </p>
                            <%--<div class="detail grey">In-App Purchase</div>--%>
                        </div>
                        <div class="featureUnit3Column tools-outline">
                            <h3 class="blue">SEO & Marketing</h3>
                            <p>
                                <span class="bold">We do the SEO and Social stuff.
Get your inbox and phones ringing with orders with collaborative marketing.
                                </span>
                                <br>
                            </p>
                            <%--<div class="detail grey">In-App Purchase</div>--%>
                        </div>
                    </div>
                    <div class="featureRow clear_fix">
                        <div class="featureUnit3Column tools-write">
                            <h3 class="brown">Design & Save</h3>
                            <p>
                                <span class="bold">Start a conversation with your visitors by letting them designs for free.</span><br>
                            </p>
                            <%--<div class="detail grey">In-App Purchase</div>--%>
                        </div>
                        <div class="featureUnit3Column tools-color">
                            <h3 class="purple">Templates galore</h3>
                            <p>
                                <span class="bold">Show your diversity with over 6000 templates </span>
                                <br>
                            </p>
                            <%--<div class="detail grey">In-App Purchase</div>--%>
                        </div>
                        <div class="featureUnit3Column tools-mixer">
                            <h3 class="green">Setup is fast and easy</h3>
                            <p>
                                <span class="bold">There's no software to install. Just upload your logo and contact details!</span><br>
                                .
                            </p>
                            <%-- <div class="detail grey">In-App Purchase</div>--%>
                        </div>
                    </div>
                </div>
                <div class="sectionTitle">
                    <div>
                        <h1 class="toUpper sectionTitle"><span class="noBold">So what’s the </span>catch?</h1>
                    </div>
                </div>


                <p class="sectionText">
                    Before we go live with our marketing to start processing leads and orders to you, you’ll first have to satisfy our quality and service criteria. Don’t worry, if you’re already customer focused and delivering high quality digital print then you’ll sail through the criteria.
                </p>
                <p class="sectionText">
                    There is a small subscription fee each month per outer post code. You can only reserve up to 2 outer post codes (territory) per subscription – the Exclusive Pink Partner.
                </p>
                <p class="sectionText">
                    We’re not promising you 100’s of orders a month initially.  The launch, marketing and processes required to adjust to web 2 print takes time. We’re here to work with you to drive more and more business to you at a local level.
                </p>



                <div class="sectionTitle">
                    <div>
                        <h1 class="toUpper sectionTitle"><span class="noBold">catch 22</span> covered</h1>
                    </div>
                </div>


                <p class="sectionText">
                    We’ve started recruiting Pink Partners across the United Kingdom on a first come first reserve basis.  Our SEO, marketing and templates localisation, improvements will commence this summer.  However your stores are live immediately on registration and you can start marketing yourself and process orders online.
                </p>

                <p class="sectionText">
                    We’re also working very closely with national and international cancer related charities to promote PinkCards.com as their partner for printing flyers, posters, business cards and greeting cards.  Every order processed by you will help raise cancer awareness. 
                </p>

                <section class="details">
                    <div class="inner-section">
                        <div class="row satisfaction">
                            <div class="satisfaction-txt">
                                <h2 class="light">Simple Satisfaction</h2>
                                <p>
                            A Pink Partner retail web store is completely free. Step in to the world of web to print and ease your print business into a new way of doing business online. We focus on all the technology, whilst you focus on content and lead generation. 
                        <br />
                            <br />
                            Our aim is simple, To raise cancer awareness and to give you the confidence to upgrade to <a href="http://www.myprintcloud.com" target="_blank">MyPRINTCloud.com</a> if you have the need for Corporate/Private Web to Print stores.
                        </p>
                            </div>
                            <div class="satisfaction-img">
                                <a href="#">
                                    <img src="/pinkregistration/pinkimages//satisfaction_seal.svg?4218" alt="30-day Satisfaction Guarantee" onerror="this.onerror=null; this.src='/pinkregistration/pinkimages/satisfaction_seal.png?4218'" /></a>
                            </div>
                        </div>




                        <div class="sectionTitle">
                            <div>
                                <h1 class="toUpper sectionTitle"><span class="noBold">Reserve your </span>turf</h1>
                            </div>
                        </div>

                        <p class="sectionText">
                            We operate on a first come, first reserve basis. So hurry before one of your competitors snaps up your post code area.
                        </p>




                        <p class="sectionText">
                            Register today and see how your ‘Own Brand’ web store will look and function with PinkCards.com
                        </p>


                    </div>
                </section>
            </div>



            <div class="button-section">
                <div class="button-wrap">

                    <button class="btn primary" type="submit" name="add" id="btnAddCart" onclick="window.location.href='/pinkregistration/step2.aspx';return false">SIGN UP FREE<i class="chevron chevron-button"></i></button>
                    &nbsp;&nbsp;

                        <button class="btn primary" type="submit" name="add" id="Button1" onclick="window.location.href='/pinkregistration/contactus.aspx';return false">Book Demo<i class="chevron chevron-button"></i></button>

                </div>

            </div>

            <div class="banner-section">
                <div class="banner-wrap">
                    <div class="banner two-items">
                        <div style="text-align: center">

                            <h2 class="banner-title-centre">Featured in the press by</h2>
                            <img src="PinkImages/press-feature.png" />
                        </div>
                        <%--<div class="product first-item pencil"><a href="/pencil">
                  <h2 class="banner-title"><strong class="product-name">Pencil.</strong><span class="product-line">Think with your hands.</span></h2>
                  <div class="product-image-wrapper"><img src="/assets/images/pencil/hand-holding-pencil-outward.jpg" alt="Pencil by FiftyThree" class="holding-pencil"></div></a></div>
              <div class="product book"><a href="/book">
                  <h2 class="banner-title"><strong class="product-name">Book.</strong><span class="product-line">Bring ideas to life.</span></h2>
                  <div class="product-image-wrapper"><img src="/assets/images/book/little-books-pile.jpg" alt="Book by FiftyThree" class="little-pile"></div></a></div>--%>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>


<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="cphFooter">

    <uc1:PinkRegFooter ID="PinkRegFooter1" runat="server" />

</asp:Content>



