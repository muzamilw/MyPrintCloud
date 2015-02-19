<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="marketing-tips.aspx.cs" MasterPageFile="PinkRegister.Master"
    Inherits="Web2Print.UI.marketinbg_tips" %>

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
                <h1 id="pickUpTools" class="toUpper sectionTitle">Marketing<span class="noBold">tips</span></h1>









                <div class="sectionTitle">
                    <h3>Co branding leaflets and drop cards.</h3>
                </div>

                <p class="sectionText">
                    We encourage every partner to announce their new web store quickly and efficiently as soon and as often as possible.
                </p>
                <p class="sectionText">
                    One way is to work with local taxi, restaurants or takeaways businesses.  These specific verticals present an opportunity for our Pink Partners to work with local business who would value help in marketing their own business on a regular basis.  This help could come in the form of you printing occasional drop cards or flyers for free for them in return for co-branding your or pink cards credentials.  
                </p>
                <p class="sectionText">
                    You could offer them a free batch in return for some co-branding on the reverse side.  Of course we would like you to use PinkCards.com as the logo with a simple message, but you could equally use your credentials.
                </p>
                <p class="sectionText">

                    <strong>Who should I market, PinkCards.com or My Business?</strong>
                </p>
                <p class="sectionText">
                    The benefits for using PinkCards’s artwork does have a wider benefit to you as local Pink Partner from an SEO and traffic generation stand point.  Remember when users visit PinkCards.com directly they are prompted to enter in their post code first, which then redirects them to the local store. If all partners adopted this approach, then this equalizes the probability of the correct end users arriving at the nearest store to them.  It also adds value to search engine rankings and propagation.
                </p>
                <p class="sectionText">
                    The benefits of using your own URL or credentials on the reverse side also has direct benefits to the Pink Partner e.g. You phone number would probably be the first point of contact in this instance.  This present a different engagement experience and ensures that your customers do not need to enter in any post code criteria in the first instance.
                </p>
                <p class="sectionText">
                    <strong>Tip Use both co-branding techniques to maximize the opportunities with 2 different verticals and then swap the methods.
                    </strong>
                </p>
                <p class="sectionText">
                    <strong>Example</strong>
                </p>
                <p class="sectionText">
                    Contact a local mini cab or private hire company and offer to GIVE them 100,000 business cards with their details on side 1 and your (or PinkCards.com details) on side 2.  
                </p>
                <p class="sectionText">
                    The taxi company will distribute the cards at their own expense, in their own time, dropping these cards door to door in a targeted area.  
                </p>
                <p class="sectionText">
                    You’ve now created an opportunity to be seen at a very local and personal level for a very small investment.  Measure your responses by asking the taxi company which week they dropped the cards in which territory.  Fine tune and then further market directly to those areas that gave you the most activity with key services.
                </p>
                <p class="sectionText">
                    We are also working closely with numerous cancer related charities and actively promoting PinkCards.com products to their members on a nationwide and global level. In return for their endorsements, the charities will receive a regular revenue stream (donations) from PinkCards.com.
                </p>
                <p class="sectionText">
                    Online, we will be starting some quirky but targeted online campaigns in the social media space soon.
                </p>
                <p class="sectionText">
                    Watch this space for more tips and announcements of up coming events.
                </p>
                <p class="sectionText">
                    Think Pink !
                </p>







               


               
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



