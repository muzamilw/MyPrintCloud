<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SeablueSubscribePanel.ascx.cs" Inherits="Web2Print.UI.Controls.SeablueSubscribePanel" %>
   <div id="footer" class="container footer-content spacer-top">
        <div class="container">
            <p>
                <div class="row">
                    <div class="col-xs-12 col-md-4" style="padding-left:0px;">
                        <div class="panel panel-info" id="Div2">
                            <div class="panel-heading">
                                <h3 class="panel-title">Contact us</h3>
                            </div>
                            <div class="panel-body" style="padding-top: 0px;">
                                <div itemscope="" itemtype="http://schema.org/Organization">
                                    <h2 itemprop="name" id="companyname" runat="server">Company</h2>
                                    <div itemprop="address" itemscope="" itemtype="http://schema.org/PostalAddress">
                                        <b>Main address</b>:<br />
                                        <span itemprop="streetAddress" id="addressline1" runat="server">Address 1..<br />
                                            Address 2..</span><br />
                                        <span itemprop="postalCode" id="cityandCode" runat="server">City and Zip Code</span><br />
                                        <span itemprop="addressLocality" id="stateandCountry" runat="server">state &amp; Country</span>
                                    </div>
                                    Tel:<span itemprop="telephone" id="telno" runat="server">(xxx) xxx-xxxx </span>
                                    <br />
                                    E-mail: <span itemprop="email" id="emailadd" runat="server">john_doe@company.com</span><br />
                                    <span itemprop="member" itemscope="" itemtype="http://schema.org/Organization"></span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-12 col-md-4">
                        <div class="panel panel-info" id="Div3">
                            <div class="panel-heading">
                                <h3 class="panel-title">Subscribe here</h3>
                            </div>
                            <div class="panel-body">
                                    <div class="form-group">
                                        <div class="input-group"><span class="input-group-addon"><span class="fa fa-envelope"></span></span>
                                         <asp:TextBox ID="txtEmailbox" runat="server" CssClass="form-control btn-rounded" Width="100"></asp:TextBox> </div>
                                        <span id="errorMsg" runat="server" class="error-block text-danger"></span>
                                        <p id="Nwsdesc" runat="server" class="help-block">Subscribe Here to receive promotional offers.</p>
                                    </div>
                                    <asp:Button ID="btnSendNews" runat="server" Text="Subscribe" CssClass="btn btn-primary btn-rounded" OnClick="btnGo_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-12 col-md-4">
                        <div class="panel panel-info" id="">
                            <div class="panel-heading">
                                <h3 class="panel-title">Go Social</h3>
                            </div>
                            <div class="panel-body">
                                <p>Stay in touch with us:</p>
                                <p><a href="https://www.facebook.com" target="_blank"><i class="fa fa-facebook fa-2x"></i></a><a href="https://www.twitter.com" target="_blank"><i class="fa fa-twitter fa-2x"></i></a><a href="https://www.pinterest.com/" target="_blank"><i class="fa fa-pinterest fa-2x"></i></a><a href="https://plus.google.com/" target="_blank"><i class="fa fa-google-plus fa-2x"></i></a><a href="https://www.linkedin.com/" target="_blank"><i class="fa fa-linkedin fa-2x"></i></a></p>
                            </div>
                        </div>
                    </div>
                </div>
            </p>
        </div>
    </div>