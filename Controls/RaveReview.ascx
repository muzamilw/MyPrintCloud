<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RaveReview.ascx.cs"
    Inherits="Web2Print.UI.Controls.RaveReview" %>
<div class="clearBoth">
</div>
<div class="container RaveReview content_area">
    <div id="controlBodyDiv" runat="server" class="row">
        <div class="p_dl_s_box_60 rounded_corners ligtGreyBack col-md-12 col-lg-12 col-xs-12 ">
            <div class="cntRRBox">

                <img src="../images/Speech.png" />


                <p class="p_box_600_width paraRaveReview">
                    <asp:Label ID="lblRaveReview" runat="server" CssClass="raveReviewText"></asp:Label><br />
                    <asp:Label ID="lblReviewBy" runat="server" CssClass="MLeft145 float_right  raveReviewName"></asp:Label>
                </p>

                <img class="flipedImg" src="../images/Speech.png" />

                <div class="clearBoth">
                    &nbsp;
                </div>
            </div>

        </div>

    </div>
</div>
