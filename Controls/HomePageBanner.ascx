<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HomePageBanner.ascx.cs"
    Inherits="Web2Print.UI.Controls.HomePageBanner" %>
<%@ OutputCache VaryByParam="true" Duration="10" %>
<div id="bannerContainer" class="slider-container" runat="server">
    <script src="../js/slider.js" type="text/javascript"></script>
    <div class="content_area">
        <div class="left_right_padding">
            <div class="page_banner">
                <div class="slider-wrapper">
                    <a href="javascript:prevSlide();" class="slider-prev">«</a>
                    <div id="sliderHomePage" class="slider">
                        <asp:Repeater ID="rptrPageBanners" runat="server" OnItemDataBound="rptrPageBanners_ItemDataBound">
                            <ItemTemplate>
                                <section id="sec" runat="server">
                        <asp:HyperLink ID="hlItemUrl" runat="server" >
                            <asp:Image ID="imgBanner" runat="server" AlternateText=" " CssClass="slider_image" />
                        </asp:HyperLink>
                        <h4>
                            <%#Eval("Heading") %>
                        </h4>
                        <p class="copy">
                           <asp:Label ID="Description" runat="server"></asp:Label>
                            <br />
                            <br />
                        </p>
                        <p class="dashed">
                        &nbsp;
                        </p>
                        <p class="button">
                          

                             <asp:Button ID="btnStartCreating" ToolTip='<%#Eval("Heading") %>' runat="server" CssClass="btn_start_creating rounded_corners5" Text="Start Creating"  />

                            <%--   <asp:HyperLink ID="hlButtonUrl" runat="server" >
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/start-creating-print-product.png" />
                            </asp:HyperLink>--%>
                        </p>
                      </section>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <a href="javascript:nextSlide();" class="slider-next">» </a>
                </div>
            </div>
        </div>
    </div>
</div>
<%--<div id="subscribeDiv" class="content_area">
    <div class="left_right_padding">
        <div class="subscrib_header_item">
            <asp:Button ID="btnGo" runat="server" CssClass="go_button" OnClick="btnGo_Click"
                OnClientClick="return ValidateTopSubscriberEmail();" />
        </div>
        <div class="subscrib_header_item">
            <asp:TextBox ID="txtEmail" runat="server" CssClass="search_textBox rounded_corners5"
                Width="300px" Text="Enter email address..."></asp:TextBox>
        </div>
        <div class="subscrib_header_item">
            <div class="subscribe_label">
                <asp:Literal ID="ltrlsubscribelbl" runat="server" Text="Subscribe to our Newsletter :"></asp:Literal>
            </div>
        </div>
        <div class="clearBoth">
        </div>
    </div>
</div>--%>
<%--<script type="text/javascript">
    $(document).ready(function () {
        $('#<%= txtEmail.ClientID %>').focus(function () {
            $(this).val('');
        });
    });
    function ValidateTopSubscriberEmail() {
        var email = $('#<%= txtEmail.ClientID %>').val().trim();
        return ValidateEmail(email);
    }
</script>--%>
