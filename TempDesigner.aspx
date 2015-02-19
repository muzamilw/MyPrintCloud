<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/DesignerTheme.Master"
    AutoEventWireup="true" CodeBehind="TempDesigner.aspx.cs" Inherits="Web2Print.UI.TempDesigner" %>

<%@ Register Src="~/Controls/OrderStep.ascx" TagName="OrderStep" TagPrefix="uc2" %>
<%@ Register Src="~/Controls/MainHeading.ascx" TagName="MainHeading" TagPrefix="uc3" %>
<%@ Register Src="Controls/TopHeader.ascx" TagName="TopHeader" TagPrefix="uc7" %>
<%@ Register Src="~/Controls/Header.ascx" TagName="PinkHeader" TagPrefix="uc8" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContents" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphHeader" runat="server">
    <uc7:TopHeader ID="TopHeader1" runat="server" />
    <uc8:PinkHeader ID="PinkHeader" runat="server" Visible="false" />
</asp:Content>
<asp:Content ID="ProductDetailsContents" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
 
        var TemplateID = 0;
        var ItemID = 0;
        var CustomerID = 0;
        var ContactID = 0;
        var IsCalledFrom = 0;
        var email1 = "";
        var email2 = "";
        var IsRoundedCorners = false;
      
        function gotoLandingPage(PagePath) {

            window.location.href = PagePath;
        }


        function SaveAttachments() {
            var Culture = $("#<%=hfUICulture.ClientID %>").val();
            var emailParameters = "";
            if (email1 != "") {
                emailParameters += "&pE1=" + email1;
            }
            if (email2 != "") {
                emailParameters += "&pE2=" + email2;
            }
            emailParameters += "&IsRC=" + IsRoundedCorners;
            $.get("Services/Webstore.svc/data", { templateID: TemplateID, itemID: ItemID, customerID: CustomerID, DesignName: document.getElementById('txtDesignName').value, UICulture: Culture,caller:"designer" },
                function (data) {
                    window.location.href = data + "&CategoryId=" + CategoryId + "&ProductName=" + document.getElementById('txtDesignName').value + emailParameters;
                });
        }


        function Next() {
            $(".top_header").css("display","none");
            $(".content_area").css("display", "none");
            $(".top_sub_section").css("display", "none");
            //access the iframe and fire its function.
            var TemplateName = "" + document.getElementById('txtDesignName').value;
//            if ($.browser.mozilla) {
//                designerFrame.contentWindow.save("preview", TemplateName);
//            } else {
//                designerFrame.save("preview", TemplateName);
//            }
            if (!!navigator.userAgent.match(/Trident\/7\./)) {
                designerFrame.save("preview", TemplateName);
            } else {
                //if ($.browser.mozilla) {
                //    designerFrame.contentWindow.save("preview", TemplateName);
                //} else {
                //    designerFrame.save("preview", TemplateName);
                //}
                if ($.browser.mozilla ) {
                    designerFrame.contentWindow.save("preview", TemplateName);
                } else if ( $.browser.chrome)
                {
                    if(designerFrame.contentWindow != undefined) {
                        designerFrame.contentWindow.save("preview", TemplateName);
                    } else {
                        designerFrame.save("preview", TemplateName);
                    }
                }else {
                    designerFrame.save("preview", TemplateName);
                }
            }

            return false;
        }

        function hideHeader() {
            $(".top_header").css("display", "none");
            $(".content_area").css("display", "none");
            $(".top_sub_section").css("display", "none");
            return false;
        }

        function ShowTopBars()
        {
            $(".top_header").css("display", "block");
            $(".top_sub_section").css("display", "block");
            $(".content_area").css("display","block");
        }
       
     
    </script>
    <style>
    html, body
    {
        height:100%;
    }
    </style>
    <div class="content_area container" style="height: auto;">
        <div class="left_right_padding row">
            <div id="TemplateDesignings" class="clsTemplateDesignings">
                <div class="signin_heading_div">
                    <div class="float_left_simple" style="position: absolute;left: 5px;" >
                        <h1>
                        <asp:Label ID="lblHeading" runat="server" CssClass="headingTempDesigner"></asp:Label></h1>
                    </div>
                    <div id="designBarRightPnl"  >
                        <p id="sdLabel" class="sdLabel" runat="server">
                            Save as</p>
                        <asp:TextBox runat="server" ID="txtDesignName" CssClass="txtDesignName text_box rounded_corners5"></asp:TextBox>
                        <button class="btnNext rounded_corners5" onclick="return Next();" runat="server" id="btnNext"  style=" display:none;"> Proof
                        </button>
                       <%-- <asp:Button ID="btnInquiryBreif" runat="server" CssClass="btn_inquiry_files_TS rounded_corners5"
                            CausesValidation="False" Text="Inquiry Brief"
                            OnClick="btnInquiryBreif_Click" style=" display:none;" />--%>
                    </div>
                    <div class="clearBoth">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <iframe  runat="server" id="designerFrame" class="designerFrame" src="" ></iframe>

  <asp:HiddenField ID="hfUICulture" runat="server" /> 
</asp:Content>
