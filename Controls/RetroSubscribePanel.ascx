<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RetroSubscribePanel.ascx.cs" Inherits="Web2Print.UI.Controls.RetroSubscribePanel" %>
<div id="footer" class="footer-content spacer-top">


    <div class="container">
        <p>
            <div class="row">

                <div class="col-xs-12 col-md-6">
                    <%--<div class="panel panel-info">--%>
                        <div id="social_icon" class="panel panel-info">
                            <div class="panel-heading">
                                <h3 class="panel-title">Go Social</h3>
                            </div>
                            <div class="panel-body">
                                <div class="social"><a href="https://www.facebook.com" target="_blank"><i class="fa fa-facebook fa-2x"></i></a><a href="https://www.twitter.com" target="_blank"><i class="fa fa-twitter fa-2x"></i></a><a href="https://www.pinterest.com/" target="_blank"><i class="fa fa-pinterest fa-2x"></i></a><a href="https://plus.google.com/" target="_blank"><i class="fa fa-google-plus fa-2x"></i></a><a href="https://www.linkedin.com/" target="_blank"><i class="fa fa-linkedin fa-2x"></i></a></div>
                            </div>
                        </div>
                   <%-- </div>--%>
                    <div class="panel panel-info" id="Div2">
                        <div class="panel-heading">
                            <h3 class="panel-title" id="lblOurNews" runat="server">Subscribe here</h3>
                        </div>
                        <div class="panel-body">
                                <div class="form-group">
                                    <div class="input-group">
                                        <span class="input-group-addon"><span class="fa fa-envelope"></span></span>
                                        <asp:TextBox id="txtEmailbox" runat="server" CssClass="form-control btn-rounded"></asp:TextBox>
                                     
                                    </div>
                                    <asp:Label ID="lblerrorMsg" runat="server" CssClass="error-block text-danger"></asp:Label>
                                    <p class="help-block" id="Nwsdesc" runat="server"></p>
                                </div>
                                <asp:Button id="btnSendNews" runat="server" Width="100" CssClass="btn btn-primary btn-rounded" Text="Subscribe" OnClick="btnGo_Click"/>
                            
                        </div>
                    </div>
                </div>
                <div class="col-xs-12 col-md-6">
                    <div class="panel panel-info" id="Div3">
                        <div class="panel-heading">
                            <h3 class="panel-title">Testimonials</h3>
                        </div>
                        <div class="panel-body">
                            <div id="testimonial-sidebar-1189832456" data-interval="6000" class="carousel slide testimonial-panel">
                                <div class="carousel-inner">
                                    <div class="item active">
                                        <blockquote>
                                            <p id="lblRaveReview" runat="server">
                                               
                                               
                                            </p>
                                            <small id="lblReviewBy" runat="server">Henry Roberts</small>
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
                        <div class="panel-footer"><span class="carousel-controls"><a href="#testimonial-sidebar-1189832456" class='left' data-slide='prev'><span class="glyphicon glyphicon-chevron-left">&nbsp;</span></a><a href="#testimonial-sidebar-1189832456" class='right' data-slide='next'><span class="glyphicon glyphicon-chevron-right">&nbsp;</span></a></span></div>
                    </div>
                </div>
            </div>
        </p>
    </div>
</div>
