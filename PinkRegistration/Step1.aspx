<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Step1.aspx.cs" MasterPageFile="PinkRegister.Master"
    Inherits="Web2Print.UI.Step1" %>

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

    <div id="toolsVideoShield" class="video-backdrop">
        <div id="toolsVideo" class="video-popup-container"></div>
    </div>
    <div id="container">
        <div id="main" role="main">
            <div id="headerImage">
                <div class="sized">
                    <div style="text-align: left; color: white; padding-top: 100px;"><span style="font-size: 60px; line-height: 1;">FREE </span><span class="bold" style="font-size: 60px;">WEB 2 PRINT</span></div>
                    <div class="sectionLeft">
                        <div class="paper-headline toUpper">
                            <span class="title-word" style="font-size: 35px; line-height: 1.2;">your website </span>
                            <span class="title-word" style="font-size: 35px; line-height: 1.2;">your prices</span>
                            <span class="title-word" style="font-size: 35px; line-height: 1.2;">your cart</span>
                            <span class="title-word" style="font-size: 35px; line-height: 1.2; ">Free Hosting</span>
                        </div>
                        <a id="freeDownload" href="http://itunes.apple.com/us/app/paper-by-fiftythree/id506003812" target="_blank" class="appStore">Available on the App Store</a>
                    </div>
                    <div class="sectionRight">
                        <a class="play-wrap" id="videoPopup" style="opacity: 1;" href="http://www.youtube.com/embed/u5sYhXRMWZM" target="_blank" data-player-url="http://www.youtube.com/embed/u5sYhXRMWZM"><i title="Play Video" class="play">▶</i></a>

                    </div>

                    <div style="float: left; color: white; clear: both;line-height:1.5">
                        <a style="font-size: 22px;color:white" href="/pinkregistration/live-partners.aspx"> Click here to see live pink partners
                      
                                                                         </a></div>
                </div>
            </div>
            <div id="tools" class="section clear_fix sized">
                <h1 id="pickUpTools" class="toUpper sectionTitle">INSTANT<span class="noBold"> WEB PRESENCE.</span></h1>
                <div class="sectionDescription">
                   
                 Generate leads and orders online. 
FREE Web 2 Print Plug-in for your existing web site OR,
Use as a Stand-Alone web site with your URL.


                </div>

                 <div class="button-section">
                <div class="button-wrap">

                   <button class="btn primary" type="submit" name="add" id="Button2" onclick="window.location.href='/pinkregistration/step2.aspx?mode=1';return false">SIGN UP FREE<i class="chevron chevron-button"></i></button>
                    &nbsp;&nbsp;

                        <button class="btn primary" type="submit" name="add" id="Button3" onclick="window.location.href='/pinkregistration/contactus.aspx';return false">Book Demo<i class="chevron chevron-button"></i></button>

                    </div>

                </div>

                <div class="featureSection">
                    <div class="featureRow clear_fix">
                        <div class="featureUnit3Column tools-draw">
                            <h3 class="yellow">SEO & Marketing</h3>
                            <p>
                                <span class="bold">Meta tags & Social Stuff</span><br>
                                Modify meta tags and product descriptions. Place your Facebook and Twitter feeds into your
Free Web 2 Print store.


                            </p>
                            <%--<div class="detail grey">Free</div>--%>
                        </div>
                        <div class="featureUnit3Column tools-sketch">
                            <h3 class="red">Templates</h3>
                            <p>
                                <span class="bold">Royalty FREE Templates</span><br>
                               Our templates are a great starting point for your customers to personalize using our free intuitive HTML editor.

                            </p>
                            <%--<div class="detail grey">In-App Purchase</div>--%>
                        </div>
                        <div class="featureUnit3Column tools-outline">
                            <h3 class="blue">Pink Partner</h3>
                            <p>
                                <span class="bold">Get local leads fed to you.</span><br>
                                We promote PinkCards.com and prompt visitors to enter in their town, city or Post/Zip code.  We then re-direct them to the nearest pink partner.

                            </p>
                            <%--<div class="detail grey">In-App Purchase</div>--%>
                        </div>
                    </div>
                    
                </div>
                <div class="sectionTitle">
                    <div>
                        <h1 class="toUpper sectionTitle"><span class="noBold">Faster, better,</span><br /> Local.</h1>
                    </div>
                </div>
                <div class="featureSection">
                    <div class="featureRow clear_fix">
                        <div class="featureUnit3Column">
                            <img src="PinkImages/Multiple.png">
                            <p>
                                <span class="bold toUpper"> Pick up in Store</span><br>
                               
                               Our research suggests that customers want to order online, but want to build a relationship with a local print and design business.

                            </p>
                        </div>
                        <div class="featureUnit3Column">
                            <img src="PinkImages/Rubber.png">
                            <p>
                                <span class="bold toUpper">Upsell other services</span><br>
                                All registered users and customers 
contact details from your store are directly
sent to you.  Build new relationships
and upsell other products and services. 

                            </p>
                        </div>
                        <div class="featureUnit3Column">
                            <img src="PinkImages/Cube.png">
                            <p>
                                <span class="bold toUpper">Completely Free</span><br />
                               We do not charge you for this service and you do not need to enter in your credit card details to register.  Register today and start customizing your free web2print store.

                            </p>
                        </div>
                    </div>
                </div>
            </div>
            <div id="interface" class="section clear_fix sized">
                <div class="sectionHead"></div>
                <div class="sectionLeft">
                    <div class="relative">
                        <div id="paperInterfaceCarousel" style="overflow: hidden;">
                            <div class="content" style="-webkit-transition: -webkit-transform 0ms; transition: -webkit-transform 0ms; -webkit-transform-origin: 0px 0px; -webkit-transform: translate3d(0px, 0px, 0px);"></div>
                        </div>
                        <div id="interfaceDemp"></div>
                        <div class="buttons clear_fix">
                            <div class="carouselButton carouselButtonActive"></div>
                            <div class="carouselButton"></div>
                            <div class="carouselButton"></div>
                            <div class="carouselButton"></div>
                        </div>
                    </div>
                </div>
                <div class="sectionRight">
                    <h1 class="toUpper sectionTitle"><span class="noBold">Take on the</span><br>
                        big boys.</h1>
                    <p>
                        Look as big and as good as the big print portals online.
Go further and start marketing your online presence
locally and see how simple it is to win business online.

                    </p>
                </div>
                <div class="featureSection">
                    <div class="featureRow clear_fix">
                        <div class="featureUnit3Column">
                            <img src="PinkImages/Sharpner.png">
                            <p>
                                <span class="bold toUpper">Targeted SEO for your area</span><br />
                                Our dedicated team of SEO and
Social marketers work around the
clock to capture and direct people 
who are searching online to order
popular  print items. 
We also target businesses in your area
to order stationery online.

                            </p>
                        </div>
                        <div class="featureUnit3Column">
                            <img src="PinkImages/Compass.png">
                            <p>
                                <span class="bold toUpper">Market as a local business</span><br />
                                Do your bit in completing the
circle to win new business online. 
Work with a local mini-cab operator
and promote your brand
door to door with minimal effort.
Let them literally drive business
to your new web to print store.

                            </p>
                        </div>
                        <div class="featureUnit3Column">
                            <img src="PinkImages/Filo.png">
                            <p>
                                <span class="bold toUpper">Organize & assess </span>
                                <br />
                                Download Orders and Artwork from your Admin Panel. Measure your investment and review the key analytics 
on how well your store is
performing over a week, month
or year.

                            </p>
                        </div>
                    </div>
                </div>
            </div>
            <%--<div id="sharing" class="section clear_fix sized">
          <div class="sectionHead"></div>
          <div class="sectionLeft">
            <div id="sharingCarousel">
              <div id="tumblrTheme" style="overflow: hidden;">
                <div class="content" style="-webkit-transition: -webkit-transform 0ms; transition: -webkit-transform 0ms; -webkit-transform-origin: 0px 0px; -webkit-transform: translate3d(0px, 0px, 0px);"></div>
              </div>
              <div class="buttons clear_fix">
                <div class="carouselButton carouselButtonActive"></div>
                <div class="carouselButton"></div>
                <div class="carouselButton"></div>
                <div class="carouselButton"></div>
              </div>
            </div><a id="tumblrLogo" href="http://www.tumblr.com/app/paper" target="_blank">Tumblr</a>
          </div>
          <div class="sectionRight">
            <h1 class="toUpper sectionTitle"><span class="noBold">Paper<br>the</span><br>web.</h1>
            <p><span class="bold toUpper">Share your ideas instantly.</span> Stream pages to Tumblr, send them over email, or share pages with your friends on Facebook and Twitter.</p>
            <p><span class="bold toUpper">Tumblr theme.</span> Get our free<strong><a href="http://www.tumblr.com/theme/36202" target="_blank"> Paper Stacks</a></strong> theme on Tumblr, built from the ground up for touch. Paper Stacks is a beautiful new way to showcase ideas, made for Paper and inspired by our community of creators. Present pages in paper stacks and feature your favorites. Swipe images aside to reveal the next one. Curate your own gallery. Let your passion shine.</p>
          </div>
        </div>
        <div id="buzz" class="section clear_fix sized">
          <div class="sectionHead buzzHead"></div><a href="http://search.itunes.apple.com/WebObjects/MZContentLink.woa/wa/link?path=AppStoreBestof2012" class="buzzUnit">
            <div class="logo apple-app-of-the-year">Apple App of the Year</div>
            <blockquote>Ideal for jotting down notes, sketches, or your next big idea, this phenomenal app sets a new standard with its clean design and thoughtful implementation of pen on paper.</blockquote></a><a href="http://awards.ixda.org/entry/2013/paper-fiftythree" class="buzzUnit">
            <div class="logo ixda">2013 IxDA Interaction Awards Best in Category Expressing</div>
            <blockquote>One of the challenges is to get the technology out of the way. It is one of the reasons why I really like Paper because all the stuff is stripped away.</blockquote></a><a href="http://techcrunch.com/events/crunchies-2012/winners/" class="buzzUnit">
            <div class="logo crunchies">Crunchies 2012 Best Design</div>
            <blockquote>It’s a beautiful app. You guys are incredibly deserving of this award.</blockquote></a><a href="https://developer.apple.com/wwdc/awards/#paper" class="buzzUnit">
            <div class="logo apple-design-award">Apple Design Award</div>
            <blockquote>Designed from the ground up for touch, Paper by FiftyThree was picked as an Apple Design Award winner because of its excellent concept and execution on iPad and for the magical feeling people get when they draw with it for the first time.</blockquote></a><a href="http://www.theverge.com/2012/3/29/2909537/paper-drawing-ipad-app-fiftythree-brains-behind-courier" class="buzzUnit">
            <div class="logo theverge">The Verge</div>
            <blockquote>Most developers provide paper apps cluttered with hundreds of tweaks and settings. In Paper’s case, that goal is to provide a digital pad of paper, the likes of which we’ve been hoping for since the iPad launch—an app free of icons and folders and settings and menus.</blockquote></a><a href="http://www.fastcodesign.com/1669392/ex-microsofties-unveil-paper-an-ipad-app-inspired-by-the-microsoft-courier" class="buzzUnit">
            <div class="logo fastco">Fast Company</div>
            <blockquote>By paring down options, it sneaks in a niche that’s simpler and more satisfying to use than the heavy dashboards of both art and note-taking apps.</blockquote></a>
        </div>--%>

            <div class="button-section">
                <div class="button-wrap">

                   <button class="btn primary" type="submit" name="add" id="btnAddCart" onclick="window.location.href='/pinkregistration/step2.aspx?mode=1';return false">SIGN UP FREE<i class="chevron chevron-button"></i></button>
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



