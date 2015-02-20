<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="live-partners.aspx.cs" MasterPageFile="PinkRegister.Master"
    Inherits="Web2Print.UI.testimonials" %>

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
                <h1 id="pickUpTools" class="toUpper sectionTitle">Live  <span class="noBold">Pink Partners</span></h1>





                <p class="sectionText">
                    <br />
                    Click on the logos of some Pink Partners to visit their live stores with their own URLs.

                    <br />
                    Notice they work slightly different!
                    <br />
                    <br />
                </p>

                <style>
                    table.tblpartners td {
                        font-size:14px;
                    }


                     table.tblpartners img {
                        max-height:50px !important;
                    }

                    .thinRow {
                        height:25px;
                    }

                    .thickRow {
                        height:80px !important;
                    }


                </style>
                <asp:Repeater ID="rptPartners" runat="server" OnItemDataBound="rptPartners_ItemDataBound">
                    <ItemTemplate>
                        <div class="tblpartners">
                            <a id="Storelink" runat="server" target="_blank">
                         <asp:Image ID="storeImg" runat="server" CssClass="partnerStoreImg"/> </a>
                            <asp:Label ID="lblStoreName" runat="server"></asp:Label>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <div style="clear:both">

                </div>
               <%-- <table style="text-align:center;font-size:18px"  >
                    <tr>
                        <td> 
                        </td>
                       
                    </tr>
                    
                  <tr class="thinRow">
                        <td>SL6</td>
                        <td>SL2,SL3</td>
                        <td>N3
                           
                        </td>
                    </tr>
                    
                    <tr class="thinRow">
                        <td><a href="http://maidenheadprint.co.uk/">http://maidenheadprint.co.uk/</a></td>
                        <td><a href="http://print-direct.net">http://print-direct.net</a></td>
                        <td>CPI London
                        </td>
                    </tr>
                    <tr class="thickRow">
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td> <a href="http://www.fiona4u.co.uk" target="_blank">
                        <img src="http://pinkcards.com//StoredImages/CustomerImages/4a4081ec-4991-4093-8d37-24943ab28c36_CompanyImage.png" alt="Scuba Print" border="0" style="max-width: 300px;"></a></td>
                        <td> <a href="#" target="_blank">
                        <img src="http://pinkcards.com//StoredImages/CustomerImages/dabb57d6-f7ce-44a1-bc19-366765b0c901_CompanyImage.png" alt="Eazy Print" border="0" style="max-width: 300px;"></a></td>
                        <td><a href="#" target="_blank">
                        <img src="http://pinkcards.com//StoredImages/CustomerImages/2a882afe-92cc-4ec1-ad7d-53e96a231907_CompanyImage.png" alt="Artem Creative Print Limited" border="0" style="max-width: 300px;"></a></td>
                    </tr>
                    <tr class="thinRow">
                        <td>GU16</td>
                        <td>NN18</td>
                        <td>M22</td>
                    </tr>
                    <tr class="thinRow">
                        <td><a href="http://www.fiona4u.co.uk">http://www.fiona4u.co.uk</a></td>
                        <td>Eazy Print</td>
                        <td>Artem Creative Print Limited</td>
                    </tr>
                    <tr class="thickRow">
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td><a href="http://canprint.it" target="_blank">
                        <img src="http://pinkcards.com//StoredImages/CustomerImages/70a09e5d-77ad-4d16-8e97-0c2c7c73825f_CompanyImage.png" alt="Canprint.it" border="0" style="max-width: 300px;"></a></td>
                        <td><a href="#" target="_blank">
                        <img src="http://pinkcards.com//StoredImages/CustomerImages/0c08636d-9e33-4734-8437-dfd1c59179ac_CompanyImage.png" alt="Colour Fast" border="0" style="max-width: 300px;"></a></td>
                        <td><a href="#" target="_blank">
                        <img src="http://pinkcards.com//StoredImages/CustomerImages/3272a4ac-9a99-4c34-858e-db0553dc71a6_CompanyImage.png" alt="Colour Fast" border="0" style="max-width: 300px;"></a></td>


                        
                    </tr>
                    <tr class="thinRow">
                        <td>BT28, BT19</td>
                        <td>BN18,BN2</td>
                        <td>SS6</td>
                    </tr>
                    <tr class="thinRow">
                        <td><a href="http://canprint.it">http://canprint.it</a></td>
                        <td>Colour Fast</td>
                        <td>Printed365</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>--%>


                <p>&nbsp;</p>
                <p>&nbsp;</p>
                <p>&nbsp;</p>
                <p>&nbsp;</p>
                <p>&nbsp;</p>





                
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



