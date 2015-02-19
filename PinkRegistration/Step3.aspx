<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Step3.aspx.cs" MasterPageFile="PinkRegister.Master"
    Inherits="Web2Print.UI.Step3" %>

<%@ Register Src="PinkRegFooter.ascx" TagName="PinkRegFooter" TagPrefix="uc1" %>
<%@ Register Src="~/PinkRegistration/Header.ascx" TagPrefix="uc1" TagName="Header" %>


<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="cphHeader">
    <link href="AllSite.css" rel="stylesheet" />
    <link href="PinkSite.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainContent">

    <div id="container">
        <uc1:Header runat="server" ID="Header" />
    <section class="shop-nav">
        <div class="inner-section">
            <div class="shop-nav-wrap">
                <nav class="bread">
                    <ul>
                        <li><a href="/Pinkregistration/step2.aspx?mode=1">Register <i class="chevron chevron-text chevron-black chevron-medium"></i></a></li>
                        <li><a class="active" href="#">Subscription</a></li>
                    </ul>
                </nav>
                <nav class="alt" style="display:none">
                    <ul>
                        <%--<li class="status"><a href="#">Order Status</a></li>--%>
                        <li class="cart"><a href="#"><span id="ItemInCardCount" runat="server" class="cart_count">1</span>Item</a></li>
                    </ul>
                </nav>
            </div>
        </div>
    </section>

    <article class="cart">
        <div class="inner-article">
            <div class="row" id="cartform">


                <h1>Your Subscription</h1>

                <fieldset class="empty" style="display: none">
                    <p>There are no items in your cart</p>
                    <a class="btn primary" href="/">Continue shopping<i class="chevron chevron-button"></i></a>
                </fieldset>


                <header>
                    <h6 class="price"><span></span></h6>
                    <h6 class="qty"><span></span></h6>
                    <h6 class="total"><span>Total</span></h6>
                </header>

                <hr>


                <fieldset class="line-item" data-id="380938388">
                    <div class="img">
                        <b></b>
                        <img src="http://ukhome-improvement.co.uk/Images/ukMap.png" width="100" alt="Pencil">
                    </div>
                    <div id="txtPostCodes" class="title" runat="server"></div>
                    <div class="price">
                        <h6></h6>
                        <span id="spnTotal" runat="server" visible="false">£59.95</span>
                    </div>
                    <div class="qty">
                        <h6></h6>
                        <strong id="TotalQty" runat="server" style="right: 0px !important;" visible="false">1</strong>

                    </div>
                    <div class="total">
                        <h6>Total</h6>
                        <strong class="item_total" id="cntItemTotal" runat="server" data-id="380938388">£59.95</strong>
                    </div>
                </fieldset>
                <hr>


                <footer>
                    <div class="cnttxtLeftPnl">
                        <img id="imgFreeT" runat="server" alt="" src="~/PinkRegistration/PinkImages/pinkFreeTrial.png" class="float_left_simple"  style="margin-right:10px;"/>
                        <p style="font-size:19px;font-weight:bold;">Register today and get your Web to print Store Instantly!</p>
                    </div>
                    <div class="cntPriceRightPnl">
                    <div class="total-actions" style="width:100% !important;">
                        <strong>VAT <span class="cart_total" id="spnVat" runat="server">£59.95</span></strong>
                    </div>
                    <div class="total-actions" style="width:100% !important;">
                        <strong>Total <span class="cart_total" id="cntGrandTotal" runat="server">£59.95</span></strong>
                    </div>
                        </div>
                    <div style="clear:both">
                        &nbsp;
                    </div>
                    <div class="checkout-actions">
                        <%--<p>Please read and accept our <a data-dialog="terms" href="#">terms of&nbsp;sale</a>.</p>--%>
                        <fieldset class="agree">
                            <input type="checkbox" id="tcs" class="termsConditionsCS"><label for="tcs" onclick="AcceptTerm();">I accept and agree</label>
                        </fieldset>
                        <fieldset class="checkout">
                            <asp:Button ID="btnNew" class="btn primary" runat="server" Text="Checkout" OnClientClick="return ifAgreed();" OnClick="btnCompleteReg_Click"  Style="text-align:right; margin-right:10px;"/>
                            <button type="submit" class="btn primary" name="checkout" id="btnCheckout" onclick="return ifAgreed();" style="display:none;">Checkout<i class="chevron chevron-button"></i></button>
                        </fieldset>
                    </div>

                </footer>
                <asp:Button ID="PostToPayPal" runat="server" OnClick="btnCompleteReg_Click" 
                    Style=" display:none;" />
                <div class="dialog-padding">


                    <div class="dialog-container dialog-terms" id="terms">
                        <div class="dialog-row">
                            <div class="dialog-header">
                                <p class="title">Terms of Sale</p>
                            </div>
                            <div class="dialog-content scrolling">
                                <p>BY CHECKING THE 'I accept and agree' BOX YOU AGREE TO BE BOUND BY THESE TERMS OF SALE. &nbsp;IF YOU DO NOT AGREE TO ALL OF THESE TERMS, THEN DO NOT CHECK THE 'I accept and agree' BOX OR ORDER PRODUCTS FROM THE WEBSITE. &nbsp;IF YOU CHECK THE 'I accept and agree' BOX, THESE TERMS OF SALE ARE FULLY ACCEPTED BY YOU. &nbsp;If you agree to these terms on behalf of an organization, you hereby represent to FiftyThree (as defined below) that you are authorized to accept these terms on behalf of such organization. IF YOU DO NOT ACCEPT THESE TERMS OF SALE, DO NOT CHECK THE 'I accept and agree' BOX AND DO NOT ORDER PRODUCTS ONLINE FROM FIFTYTHREE.</p>
                                <p>Terms of Sale</p>
                                <p>Please review these terms carefully, as these Terms of Sale (the “Terms”) govern your purchases on our website and constitute a binding legal agreement between you and FiftyThree, Inc., (herein also referred to as “FiftyThree”, “we” or “us”), the owner of the http://www.fiftythree.com website (the “Website”). By ordering products, including products manufactured by FiftyThree or its agents (“Products”), through the Website, you signify your acceptance of these Terms. These Terms set out your rights and obligations with respect to your purchases, including important limitations and exclusions. All changes to these Terms are effective when posted on the Website. THESE TERMS CONTAIN LIMITATIONS ON LIABILITY; see Sections 11 - 13 for additional details.</p>
                                <p>1. Types of Sales. FiftyThree accepts orders for Products through the Website. You may place your order on our Website at any time (subject to any planned or unplanned downtime). All Products ordered from the Website will be delivered to the U.S. or Canadian address you specify. You must pay for the Products online at the time you place the order. If you entered a valid address and your order and payment are accepted by us, your order will be shipped pursuant to the terms below. FiftyThree will send a proof of purchase of the Products to the email address you provide. In addition, FiftyThree will provide the proof of purchase information through the Website after the purchase transaction is complete, so that you may print the information at the time you complete the order if desired. If you have any questions regarding the ordering process, please contact FiftyThree by any means listed in the How To Contact Us section below.</p>
                                <p>2. Prices and Taxes. Prices offered on the Website are quoted in U.S. Dollars. Such prices do not include sales taxes where applicable. You are responsible for any sales, use, excise, value added, or similar sales taxes, customs duties and brokerage fees that may apply to your order. If the amount you pay for an item is incorrect, regardless of whether it is an error in a price posted on this Website or otherwise communicated to you, then we reserve the right, at our sole discretion, to cancel your order, notify you through the contact information you provided during the order process, and refund to you the amount that you paid, regardless of how the error occurred.</p>
                                <p>3. Payment Methods. FiftyThree allows you to make your purchases using any of the payment methods described below. Please read carefully our policies with respect to payment methods before you place your order. You may pay by credit, debit, or check card. When you provide FiftyThree with your card information, and authorize the transaction, FiftyThree will bill your credit card or process the transaction under your debit or check card you provided. Once your order has been accepted by the Website and the amount owed is authorized by the issuing bank of your card, FiftyThree will send you confirmation to the email address you provided indicating that your order has been accepted (“Order Confirmation”). FiftyThree accepts American Express, MasterCard, and Visa (subject to change without notice) with a billing address within the United States or Canada. Debit cards and check cards have daily spending limits that may substantially delay the processing of your order. The Website requires the security code of your card for any online purchase to protect against the unauthorized use of your card by other persons. The security code is an individual three or four digit number specific to your card that may be printed on the face of your card above the embossed account number (if American Express), or on the back of your card, on the signature panel (if Visa or MasterCard). You represent and warrant that you have the right and are authorized to use the credit, debit or check card you present to purchase Products and that the billing and related information you provide is accurate and truthful. If for any reason you have not authorized charges to be made to your credit, debit or check card, or your credit, debit or check card issuer does not pay FiftyThree for charges, FiftyThree reserves the right to immediately suspend or terminate the fulfillment of your order.</p>
                                <p>4. Product Descriptions; All Sales Final. We attempt to describe and display the items offered on the Website as accurately as possible; however, we do not warrant that the descriptions or other content on the Website are accurate, complete, reliable, current or error-free.Unless you are provided with information to the contrary from us and subject to Product Warranties and our Guarantee stated below, all Product sales are final.</p>
                                <p>5. Payment Disputes. Subject to our Guarantee and Warranty stated below, if you dispute any charge for purchases of Products on the Website, you must notify FiftyThree in writing within sixty (60) days of any such charge; failure to so notify FiftyThree shall result in the waiver by you of any claim relating to such disputed charge; and charges shall be calculated solely based on records maintained by FiftyThree.</p>
                                <p>6. Changes. In the event FiftyThree agrees to a modification of a Product purchase transaction, FiftyThree may revise prices, dates of delivery, and warranties with respect thereto.</p>
                                <p>7. Shipment and Delivery. You must pay for Product shipping costs to your designated locations within the ordering process prior to shipment of such Products. Accepted orders will be processed and Products will be shipped to valid physical addresses within the U.S. and Canada. Exact shipping dates are unknown at the time you place the order and may exceed 30 days after your order is accepted. FiftyThree will notify you if any order is estimated to take longer than 90 days to ship from the time the order is placed by you and accepted by FiftyThree. In such circumstances, FiftyThree will include in the notice your options as it relates to the order for Products that take longer than 90 days to ship. Products may be shipped by FiftyThree or direct from one of our independent contractors. All claims of shortages or damages suffered in transit must be submitted directly to the FiftyThree within the Guarantee period stated below. FiftyThree reserves the right to make partial shipments. FiftyThree is not bound to deliver any Products for which you have not provided shipping instructions, which include a valid email and phone number where you may be reached. Products may not be returned without the prior written consent of FiftyThree through our RMA process stated below.</p>
                                <p>8. Inspection. Subject to the Warranty and Guarantee stated below,Products are accepted by you no later than the thirtieth (30th) day following delivery of Products unless the parties otherwise agree in writing; after acceptance is deemed completed, you waive any right to reject Products for nonconformity herewith.</p>
                                <p>9 Applicability of the Privacy Policy. You agree and understand that it is necessary for FiftyThree to collect, process and use the information you submit to FiftyThree in order to sell Products and confirm compliance with applicable laws in respect of your transaction. FiftyThree will protect your information in accordance with its Privacy Policy located at fiftythree.com/legal/privacy (the “Privacy Policy”).</p>
                                <p>10. 30-DAY SATISFACTION GUARANTEE (“Guarantee”). If you are unsatisfied with your Pencil or Battery purchase from FiftyThree for any reason, you have thirty (30) days from the date of purchase to request a refund. &nbsp;&nbsp;</p>
                                <p>
                                    To qualify for a replacement or refund, all the following conditions must be met:
                                    <br>
                                    1. A Return Merchandise Authorization (“RMA”) must be requested from our customer service team within thirty (30) days of your purchase date.&nbsp;To request an RMA, go to: fiftythree.com/product/help<br>
                                    2. The serial number of your Pencil or Battery must be verified by FiftyThree for date of purchase. The serial number can be found in the following locations:<br>
                                    - On the exterior of the original packaging.
                                    <br>
                                    - Within the Paper by FiftyThree App. Please go to: support.fiftythree.com for instructions on how to access the Pencil serial number through Paper.
                                    <br>
                                    - If neither of above is available please contact customer support at: support.fiftythree.com
                                    <br>
                                    3. Once a Pencil or Battery serial number has been submitted to FiftyThree for an RMA request, that product will no longer qualify for any other guarantee or warranty.
                                    <br>
                                    4. Returned Product must be in good physical condition (not physically broken or damaged) and in original packaging.
                                    <br>
                                    5. All accessories originally included with your Product must be included with your return.
                                    <br>
                                    6. The Product must be shipped with a FiftyThree 30-Day Satisfaction Guarantee RMA shipping label.
                                    <br>
                                    7. Only one (1) Pencil or one (1) Battery per RMA request. Only one (1) Pencil or one (1) Battery per RMA shipping package. If more than one (1) Pencil or one (1) Battery is included with the RMA request or shipping package then only one (1) Pencil or one (1) Battery will be replaced or refunded.
                                </p>
                                <p>FiftyThree shall bear the cost of return shipping to FiftyThree only if the Product is shipped with the 30-Day Satisfaction Guarantee RMA shipping label and only if one (1) Pencil or one (1) Battery is shipped per package (per above required conditions).</p>
                                <p>
                                    FiftyThree will offer you one of the following options upon receipt and verification of returned product:
                                    <br>
                                    - A replacement Pencil or Battery. FiftyThree shall bear the cost of shipping a replacement Pencil or Battery to you.
                                    <br>
                                    - A refund for the original purchase price plus applicable taxes (minus original shipping &amp; handling costs). Refunds can only be credited to Visa, MasterCard or American Express credit/debit cards and only in US dollars. Guarantee Terms &amp; Conditions: Shipping and handling charges are not refundable unless FiftyThree determines that: (1) the charges requested are legitimate and reasonable; and (2) the Product is inoperable and/or fails to operate substantially in accordance with the accompanying documentation through no fault of the original purchaser or through no fault of a third person, including the limitations and/or disqualifying actions expressed in the FiftyThree 30-Day Satisfaction Guarantee.
                                </p>
                                <p>If you return Product to FiftyThree (a) without an RMA request or (b) without all parts included in the original package or (c) without the FiftyThree RMA shipping label or (d) with multiple products in one return package; FiftyThree retains the right to either refuse delivery of such return or refuse replacement or refund. Refunds will be processed and paid within two (2) weeks of FiftyThree receipt and verification of return product.</p>
                                <p>11. 1-YEAR LIMITED WARRANTY (“Warranty”)</p>
                                <p>In the event that you purchase a Product, FiftyThree warrants to you that your Pencil will under normal use operate substantially in accordance with the accompanying documentation for a period of one (1) year from date of original purchase. Your sole and exclusive remedy, and FiftyThree’s sole and exclusive responsibility under this warranty will be, at FiftyThree’s option, to replace the defective Product during the one (1) year limited warranty period so that it performs substantially in accordance with the accompanying documentation on the date of the initial purchase. Any replacement may be, at the option of FiftyThree a new or remanufactured Pencil.</p>
                                <p>The forgoing warranty is limited to Pencils and is not applicable to: (i) consumables such as tips and erasers or batteries; (ii) normal wear and tear; (iii) defects or damage caused by misuse, accident (including without limitation; collision, fire and the spillage of food or liquid), neglect, abuse, alteration, unusual stress, modification, improper or unauthorized repair or improper storage (iv) used not in accordance with the documentation; and (v) damage caused by or to the equipment with which the Pencil is used.</p>
                                <p>
                                    To obtain warranty service for any Pencil that is subject to the foregoing warranty, all the following conditions must be met:
                                    <br>
                                    1. An RMA must be requested from our customer service team within one (1) year of your purchase date. To request an RMA, go to: fiftythree.com/product/help<br>
                                    2. The serial number of your Pencil or Battery must be verified by FiftyThree for date of purchase. The serial number can be found in the following locations:
                                    <br>
                                    - On the exterior of the original packaging.
                                    <br>
                                    - Within the Paper by FiftyThree App. Please go to: support.fiftythree.com for instructions on how to access the Pencil serial number through Paper.
                                    <br>
                                    - If neither of above is available please contact customer support at: support.fiftythree.com
                                    <br>
                                    3.Once a Pencil’s serial number has been submitted to FiftyThree for RMA request, that Pencil will no longer qualify for any other guarantee or warranty.
                                    <br>
                                    4. Returned Product must be in good physical condition (not physically broken or damaged) and in original packaging.
                                    <br>
                                    5. All accessories originally included with your Pencil must be included with your return.
                                    <br>
                                    6. The Pencil must be shipped with a FiftyThree 1-Year Limited Warranty RMA shipping label.
                                    <br>
                                    7. Only one (1) Product per RMA request.Only one (1) Product per RMA shipping package. If more than one (1) Product is included with the RMA request or shipping package then only one (1) Product will be replaced.
                                </p>
                                <p>You shall bear the cost of shipping your Pencil to FiftyThree and assume all risk of loss or damage to the Pencil while in transit to FiftyThree. FiftyThree will offer you a replacement Pencil upon receipt and verification of returned product. FiftyThree shall bear the cost of shipping a replacement Pencil to you.</p>
                                <p>Warranty and Remedy Terms &amp; Conditions: If you return your Pencil to FiftyThree (a) without an RMA request or (b) without all parts included in the original package or (c) without the FiftyThree RMA shipping label or (d) with multiple products in one return package; FiftyThree retains the right to either refuse delivery of such return or refuse replacement.</p>
                                <p>Shipping and handling charges are not refundable unless FiftyThree determines that: (1) the charges requested are legitimate and reasonable; and (2) the Pencil is inoperable and/or fails to operate substantially in accordance with the accompanying documentation through no fault of the original purchaser or through no fault of a third person, including the limitations and/or disqualifying actions expressed in the FiftyThree 1-Year Limited Warranty.</p>
                                <p>The limited warranty and remedy extends only to you and is not assignable or transferable to any subsequent purchaser or user.</p>
                                <p>THE LIMITED WARRANTY AND REMEDY SET FORTH ABOVE IS PROVIDED IN LIEU OF ALL OTHER WARRANTIES AND REMEDIES AND FIFTYTHREE HEREBY DISCLAIMS ALL OTHER WARRANTIES AND REMEDIES OF ANY KIND, WHETHER EXPRESS, IMPLIED, STATUTORY OR OTHERWISE, INCLUDING WITHOUT LIMITATION ANY WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR USE OR PURPOSE, NON-INFRINGEMENT, QUALITY AND TITLE. FIFTYTHREE DOES NOT WARRANT THAT THE PRODUCT IS ERROR FREE OR THAT IT WILL FUNCTION WITHOUT INTERRUPTION.</p>
                                <p>To the extent FiftyThree may not, as a matter of applicable law, disclaim certain implied warranties and remedies, the duration of any such implied warranty and remedy shall be limited to the shorter of the one (1) year limited warranty period or the minimum time period permitted under such law. Some jurisdictions do not allow limitations on the duration of implied warranties and remedies, so the above limitation may not apply to you. This limited warranty and remedy gives you specific legal rights, and you may also have other rights that vary from jurisdictions to jurisdictions.</p>
                                <p>12. Limitation of Liability. (A) IN NO EVENT WILL FIFTYTHREE BE LIABLE FOR ANY SPECIAL, INDIRECT, INCIDENTAL, PUNITIVE OR CONSEQUENTIAL DAMAGES OF ANY NATURE WHATSOEVER INCLUDING BUT NOT LIMITED TO LOSS OF PROFITS OR REVENUES, LOSS OF DATA, LOSS OF USE OF THE PRODUCT OR ANY ASSOCIATED EQUIPMENT, COST OF ANY REPLACEMENT GOODS OR SUBSTITUTE EQUIPMENT, LOSS OF USE DURING THE PERIOD THAT THE PRODUCT IS BEING REPAIRED, CLAIMS OF ANY THIRD PARTIES, OR ANY OTHER DAMAGES ARISING FROM FIFTYTHREE’S BREACH OF THIS AGREEMENT, INCLUDING THE LIMITED WARRANTY, OR THE USE OF THE PRODUCT, REGARDLESS OF THE FORM OF ACTION WHETHER IN CONTRACT, TORT (INCLUDING NEGLIGENCE) OR ANY OTHER LEGAL OR EQUITABLE THEORY, EVEN IF FIFTYTHREE HAS BEEN ADVISED OF THE POSSIBILITY OF SUCH DAMAGES. (B) IN NO EVENT WILL FIFTYTHREE’S TOTAL CUMULATIVE LIABILITY EXCEED THE PRICE PAID BY YOU FOR THE PRODUCT PURCHASED BY YOU.</p>
                                <p>Some states do not allow the exclusion or limitation of incidental or consequential or other damages, so the above limitation or exclusion may not apply to you. If you have any questions concerning this statement of limited warranty please email FiftyThree at: pencil@fiftythree.com.</p>
                                <p>13. Indemnification. Except to the extent a claim arises out of FiftyThree’s fraud or willful misconduct, you agree to defend, indemnify and hold harmless FiftyThree, its members, affiliates, partners, and their officers, directors, partners, shareholders agents, licensees and employees (cumulatively “FiftyThree Indemnitees”) from and against all claims, actions, liabilities, losses, expenses, damages and costs, including but not limited to attorney’s fees that may, at any time, arise from or relate to any Products purchased through our Website, including, without limitation, for any causes of action arising from your misuse of the Products.</p>
                                <p>14. Excuse of Performance. FiftyThree shall not be liable for any failure or delay in performance due in whole or in part to any cause beyond the reasonable control of FiftyThree or its contractors, agents or suppliers, including but not limited to utility or transmission failures, power failure, strikes or other labor disturbances, acts of God, acts of war or terror, floods, sabotage, fire, natural or other disasters.</p>
                                <p>15. Governing Law; Dispute Resolution. These Terms are governed by the laws of the State of New York, United States, without giving effect to its conflict of law rules, and you agree to the exclusive jurisdiction and venue of the federal and state courts located in New York County, New York, United States, and waive any objection to such jurisdiction or venue. In the event of a dispute or controversy between FiftyThree and you arising out of or in connection with your purchase, the parties shall attempt, promptly and in good faith, to resolve any such dispute. If we are unable to resolve any such dispute within a reasonable time (not to exceed thirty (30) days), then the parties shall be free to pursue any right or remedy available to them under applicable law. The application of the United Nations Convention on Contracts for the International Sale of Goods does not apply to these Terms.</p>
                                <p>16. Export Control. You may not use or otherwise export or re-export the Products purchased via the Website except as authorized by the laws of the jurisdiction in which the Products were obtained. In particular, but without limitation, the Products may not be exported or re-exported in violation of export laws, including if applicable export or re-export into any US-embargoed countries or to anyone on the US Treasury Department’s list of Specially Designated Nationals or the US Department of Commerce Denied Person’s List or Entity List. By using the Website, you represent that you are not located in any country or on any list where the provision of Product to you would violate applicable law. You also agree that you will not use Products for any purposes prohibited by applicable law.</p>
                                <p>17. General Provisions. These Terms constitute the entire agreement between the parties and supersedes all other communications between the parties relating to the subject matter of the Terms. The Terms may be amended, modified, or supplemented only in a writing signed by both parties. No waiver by either party of a breach hereof shall be deemed to constitute a continuing waiver of any other breach or default or of any other right or remedy, unless such waiver is expressed in writing signed by both parties. The rights that accrue to FiftyThree by virtue of these Terms shall inure to the benefit of FiftyThree’s successors and assigns. Other terms may govern services purchased from FiftyThree. Please contact us through any means listed below or review those terms as presented on the Website or as otherwise provided to you by FiftyThree.</p>
                                <p>18. Notices. FiftyThree may give you all notices (including legal process) that FiftyThree is required to give by any lawful method, including by posting notice on the Website or by sending it to any email or mailing address that you provide to FiftyThree. You agree to check for notices posted on the Website. You agree to send FiftyThree any notice by mailing it toFiftyThree’s address for legal notices which is: FiftyThree, Inc., 110 Reade Street, Floor 4, New York, NY 10013 Attention: Legal Dept.</p>
                                <p>*PLEASE NOTE: All returned Products must follow the RMA return process above, and not shipped to the Notice address in this section.</p>
                                <p>19. How to Contact Us. If you have any questions about any Product or these Terms or would like to learn more about FiftyThree, please write to us at FiftyThree, Inc., 60 Hudson Street, Suite 1810, New York, NY 10013, or email us at pencil@fiftythree.com, or call us at +1 (415) 580-1157 (long distance charges may apply).</p>
                                <p>Terms of Sale: last updated March 12, 2014.</p>
                                <span>
                                    <br>
                                </span>
                            </div>
                            <div class="dialog-buttons">
                                <a href="#" class="print half">Print</a>
                                <a href="#" class="close half">Close</a>
                            </div>
                        </div>
                    </div>

                </div>

            </div>
        </div>
    </article>
     </div>   
    <div style="clear: both;">
        &nbsp;
    </div>
    <br />
    <br />
    <br />
    <uc1:PinkRegFooter ID="PinkFooter" runat="server" />
    <script src="../Scripts/jquery-1.9.1.min.js"></script>
    <script src="../Scripts/jquery-ui-1.9.2.custom.min.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {

            if ($('input:checkbox[class=termsConditionsCS]').is(':checked')) {

               
                $("#<%=btnNew.ClientID %>").removeClass("FadeCS");

            } else {
                $("#<%=btnNew.ClientID %>").addClass("FadeCS");
                
                
            }
        });


        function AcceptTerm() {
            if ($('input:checkbox[class=termsConditionsCS]').is(':checked')) {
               
                $("#<%=btnNew.ClientID %>").addClass("FadeCS");
                
            } else {
                $('input:checkbox[class=termsConditionsCS]').parent().css("border", "none");
                $("#<%=btnNew.ClientID %>").removeClass("FadeCS");
            }
        }


        function ifAgreed() {
            if ($('input:checkbox[class=termsConditionsCS]').is(':checked')) {
                
                $("#<%= PostToPayPal.ClientID%>").click();
             
                return true;


            } else {
                $('input:checkbox[class=termsConditionsCS]').parent().css("border", "4px solid #eb5328");
                $('input:checkbox[class=termsConditionsCS]').parent().css("border-radius", "5px");
                $('input:checkbox[class=termsConditionsCS]').parent().css("padding-left", "5px");
                return false;
            }
        }

    </script>
</asp:Content>

