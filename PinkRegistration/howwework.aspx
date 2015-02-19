<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="howwework.aspx.cs" MasterPageFile="PinkRegister.Master"
    Inherits="Web2Print.UI.howwework" %>

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
                <h1 id="pickUpTools" class="toUpper sectionTitle">How we <span class="noBold">work</span></h1>

              









                <p class="sectionText">
                  We work completely transparently. We focus on generating internet traffic to PinkCards.com on a Town by town basis across the United Kingdom, United States and Australia.
                </p>
                <p class="sectionText">
                    As visitors lands on the PinkCards.com home page they are prompted to enter in their town, city or Post/Zip code. The Pinkcards.com platform gets rebranded to the nearest Pink Partner to them – in effect white labeling the whole site with your branding, pricing and contact details.  
                </p>
                <p class="sectionText">
                    From here on in, every page and action is branded as the Pink Partner right down to the receipt page and payment gateway.  If they chose to skip this step, they will be assigned a Pink Partner territory on the checkout page based on the nearest distance to their billing address.
                </p>
                <p class="sectionText">
                    All orders and payments go directly to our Pink Partners, with no commissions charged and no royalties charged for the use of templates and images.
                </p>
                <p class="sectionText">
                   Customers pay directly into the YOUR (Pink Partner) PayPal account.
                </p>
               
               
                 <div class="button-section">
                <div class="button-wrap">

                    <button class="btn primary" type="submit" name="add" id="Button2" onclick="window.location.href='/pinkregistration/step2.aspx';return false">SIGN UP FREE<i class="chevron chevron-button"></i></button>
                    &nbsp;&nbsp;

                        <button class="btn primary" type="submit" name="add" id="Button3" onclick="window.location.href='/pinkregistration/contactus.aspx';return false">Book Demo<i class="chevron chevron-button"></i></button>

                </div>

            </div>
               


                
                


                 <div class="sectionTitle">
                    <div>
                        <h1 class="toUpper sectionTitle"><span class="noBold">Your "own brand" store gives</span> you</h1>
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
                               Our templates and a great starting point for your customers to personalize using our free intuitive HTML editor.

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

                 <p class="sectionText">
                    We encourage Pink Partners to take all new leads and orders offline and promote their other services as a local print and design specialist.  Let’s face it, that’s how you already work and that’s how your customers prefer it.
                </p>
                <p class="sectionText">
                    This doesn’t mean we’re replacing your existing web site.  Far from it. Simply add a button to your existing web site and point it to your ‘Own brand’ Pink Cards portal area or another domain name for seamless results.
                </p>
                <p class="sectionText">
                    Next step, lets market PinkCards.com and your own brand together.  <a href="/pinkregistration/marketing-tips.aspx">See Marketing tips</a>
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
                        <h1 class="toUpper sectionTitle"><span class="noBold">Reserve your </span> turf</h1>
                    </div>
                </div>



                

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



