<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MetroTestimonial.ascx.cs" Inherits="Web2Print.UI.Controls.MetroTestimonial" %>
<div class="container">


<div class="panel panel-info" id="testimonial_sidebar">
    <div class="panel-heading">
        <h3 class="panel-title">Testimonials</h3>
    </div>
    <div class="panel-body">
        <div id="testimonial-sidebar-124726984" data-interval="6000" class="carousel slide testimonial-panel">
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
    <div class="panel-footer"><span class="carousel-controls"><a href="#testimonial-sidebar-124726984" class='left' data-slide='prev'><span class="glyphicon glyphicon-chevron-left">&nbsp;</span></a><a href="#testimonial-sidebar-124726984" class='right' data-slide='next'><span class="glyphicon glyphicon-chevron-right">&nbsp;</span></a></span></div>
</div>
 <div class="MetroSubscribe">
            <asp:Label ID="lblOurNews" runat="server" Text="Subscribe here" CssClass="NewsLtrHeadingCS"></asp:Label>
        <div class="NewsLtrTxtBxContCS col-md-12 col-lg-12 col-sm-12 col-xs-12">
            <span class="input-group-addon"><span class="fa fa-envelope"></span></span>
                <asp:TextBox ID="txtEmailbox" runat="server" Text="Enter email address..." ValidationGroup="email"
                    CausesValidation="false" CssClass="txtSubscribe-Subscribe SubscribeTxtBoxCS"
                    ClientIDMode="Static">
                </asp:TextBox>
                
            </div>
            <div class="NewsDescCs col-md-12 col-lg-12 col-sm-12 col-xs-12">
                <asp:Label ID="Nwsdesc" runat="server"></asp:Label>
            </div>
         
     <asp:Button ID="btnSendNews" runat="server" OnClientClick="return ValidateBottomSubscriberEmail();"
                    OnClick="btnGo_Click" CssClass="btnSubscribe rounded_corners" Text="Send" /><br />
            <div class="NewsLtrErrDesCS">
                <asp:Label ID="errorMsg" runat="server" CssClass="NewsErrMesgCS"></asp:Label>
            </div>
            <div class="clearBoth">
                &nbsp;
            </div>
        </div>
    </div>
<script src="../js/script.js"></script>
