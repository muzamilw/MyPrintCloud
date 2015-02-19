<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InquiryDetails.aspx.cs" Inherits="Web2Print.UI.InquiryDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <style>
        #divShd
        {
            z-index: 999 !important;
        }
    </style>
</head>
<body class="CustomerProfileCS">
    <form id="form1" runat="server">
   <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="jquery-migrate" />

                <asp:ScriptReference Name="WebUIValidation.js" Assembly="System.Web" />
            </Scripts>
        </asp:ScriptManager>
         <div id="divShd" class="opaqueLayer"> </div> 
           <asp:Panel ID="UpdateProgressUserProfile" CssClass="loader" runat="server">
                    <div id="lodingDiv" class="FUPUp LCLB">
                        <div style="background-color: White; height: 100px;">
                            <br />
                            <div id="loaderimageDiv" style="padding-top: 15px;">
                                <img src='<%=ResolveUrl("~/images/asdf.gif") %>' alt="" />
                            </div>
                            <br />
                            <div id="lodingBar" style="text-align: center;">
                                Loading please wait....
                            </div>
                        </div>
                    </div>
                </asp:Panel>
    <div >
    <div class="iframeContent white_background pad20" style="z-index:100000 !important; height:auto; min-height:560px; display:none;">
                <asp:Label ID="lblInquiryHeaderTxt" runat="server" CssClass="left_align FileUploadHeaderText_PopUp float_left_simple MesgBoxClass"></asp:Label>
        <div class="clearBoth">
        &nbsp;
        </div>
        <div class="SolidBorderCS spacer10pxtop">
        &nbsp;
        </div>
        <div class="rounded_corners " style="height: 472px; overflow: auto; clear: both; background-color: #FFFFFF; height: auto; z-index: 0;">
        <%-- <div style="float:right; clear:both;">
                <p id="lblDownloadAttachment" runat="server" class="download_attachments">Download Attachment</p>
             <asp:ImageButton ID="lnkDownloadIconPopup" runat="server" Text="View Details" CssClass="rounded_corners " ImageUrl="~/images/download-icon.png" ToolTip="Click to download Inquiry attachments" Height="40" Width="45" OnClick="lnkDownloadIconPopup_Click" />
         </div>--%>
            
              
            
             <table border="0" width="100%" cellpadding="4" cellspacing="3">
                <tbody>
                    <tr>
                        <td>
                            <div>
                                <table id="PnlAddresses" runat="server" border="0" cellpadding="3" cellspacing="0"
                                    width="100%">
                                    <tr>
                                        <td valign="top" >
                                            <%--Company info--%>
                                            <%--<div class="page_border_div rounded_corners  simpleText" style="text-align:left; padding-left:50px;">--%>
                                                <div class="product_detail_sup_padding rounded_corners " style="text-align:left; padding-left:60px;">
                                                    <%--<div style="float:right;">
                                                    <asp:Image ID="imgCompanyLogo" runat="server" CssClass="widhtCP_CSLogo" style="float:right;" />
                                                    </div>--%>
                                                    <div style="float:left; width:75%">
                                                     <table border="0" cellpadding="2" cellspacing="0" width="100%"">
                                                        <thead>
                                                            <tr>
                                                                <th colspan="2" style="height: 25px; text-align: left;">
                                                                    <asp:Label ID="lblCompanyInfo" runat="server" Text="Inquiry Detail" CssClass="heading_small" />
                                                                </th>
                                                            </tr>
                                                           
                                                        </thead>
                                                        <tbody>
                                                              <tr>
                                                                <td class="TLR">
                                                                    <span id="spnInquiryTitle" runat="server">Inquiry Title:</span>
                                                                </td>
                                                                <td class="labelAlignedleft">
                                                                    <asp:Label ID="lblInquiryTitle" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TLR">
                                                                    <span id="spnInquirCode" runat="server">Inquiry Code:</span>
                                                                </td>
                                                                <td class="labelAlignedleft">
                                                                    <asp:Label ID="lblInquiryCode" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                             <tr>
                                                                <td class="TLR">
                                                                    <span id="spnAttachment" runat="server">Inquiry Attachment:</span>
                                                                </td>
                                                                <td class="labelAlignedleft">
                                                                   <asp:LinkButton ID="lnkDownloadAttachment" runat="server" OnClick="lnkDownloadAttachment_Click">Click to Download Attachments</asp:LinkButton>
                                                                </td> 
                                                            </tr>

                                                            
                                                            <tr>
                                                                <th colspan="2" style="height: 25px; text-align: left;">
                                                                    <asp:Label ID="Label2" runat="server" Text="Contact Detail" CssClass="heading_small" />
                                                                </th>
                                                            </tr>
                                                            <tr>
                                                                <td class="TLR">
                                                                    <span id="spnCname" runat="server">First Name:</span>
                                                                </td>
                                                                <td class="labelAlignedleft">
                                                                    <asp:Label ID="lblCName" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                             <tr>
                                                                <td class="TLR">
                                                                    <span id="spnLName" runat="server">Last Name:</span>
                                                                </td>
                                                                <td class="labelAlignedleft">
                                                                    <asp:Label ID="lblLName" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TLR">
                                                                    <span id="spnEmail" runat="server">Email:</span>
                                                                </td>
                                                                 <td class="labelAlignedleft">
                                                                    <asp:Label ID="lblEmail" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="TLR">
                                                                    <span id="spnPhone" runat="server">Mobile:</span>
                                                                </td>
                                                                   <td class="labelAlignedleft">
                                                                    <asp:Label ID="lblPhone" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                          
                                                            <tr>
                                                                <th colspan="2" style="height: 25px; text-align: left;">
                                                                    <asp:Label ID="Label1" runat="server" Text="Inquiry Item Detail" CssClass="heading_small" />
                                                                </th>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" align="left">
                                                                    <asp:Repeater ID="rptInquiryItemDetails" runat="server" OnItemDataBound="rptInquiryDetails_ItemDataBound">
                                                                        <ItemTemplate>
                                                                            <div id="MainContainer" class="page_border_div rounded_corners simpleText" runat="server" style="width:100%;">
                                                                                 <table border="0" cellpadding="2" cellspacing="0" width="100%">
                                                                                       
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td class="TLR">
                                                                                                    <span id="spnTitlename" runat="server">Title:</span>
                                                                                                </td>
                                                                                                <td class="labelAlignedleft">
                                                                                                    <asp:Label ID="lblInquiryItemTitle" runat="server" Text='<%#Eval("Title","{0}") %>'></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="TLR">
                                                                                                    <span id="ltrlNotes"  runat="server">Notes:</span>
                                                                                                </td>
                                                                                                <td class="labelAlignedleft">
                                                                                                    <div id="notesDivi" class="notesDiv"><%#Eval("Notes","{0}") %></div>
                                                                                                   <%-- <asp:Label ID="lblNotes" runat="server" Text='<%#Eval("Notes","{0}") %>'></asp:Label>--%>
                                                                                                  <%-- <p id="pNotes" runat="server" ti ='<%#Eval("Notes","{0}") %>'></p>--%>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td class="TLR">
                                                                                                    <span id="ltrlDeliveryDate" runat="server">Delivery Date:</span>
                                                                                                </td>
                                                                                                <td class="labelAlignedleft">
                                                                                                    <asp:Label ID="lblDeliveryDate"  runat="server" Text='<%#Eval("DeliveryDate", "{0:D}") %>'></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                          
                                                                                        </tbody>
                                                                                    </table>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                </td>
                                                            </tr>




                                                        </tbody>
                                                    </table>
                                                    </div>
                                                    
                                                   
                                                   
                                                </div>
                                            <%--</div>--%>
                                        </td>
                                       <%--   <td>
                                        </td>
                                       <td valign="top">
                                            Contact Info
                                            <div class="page_border_div rounded_corners BillShipAddressesControl_CP simpleText">
                                                <div class="product_detail_sup_padding">
                                                    <table border="0" cellpadding="2" cellspacing="0" width="100%">
                                                        <thead>
                                                            <tr>
                                                                <th colspan="2" style="height: 25px; text-align: left;">
                                                                    <asp:Label ID="lblContactInfo" runat="server" Text="Contact Details" CssClass="heading_small" />
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                       
                                                    </table>
                                                </div>
                                            </div>
                                        </td>--%>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
          
        </div>
        <asp:HiddenField ID="hdnInquiryID" runat="server" Visible="false" />
    </div>
        </div>
    </form>
    <script>

        function showProgress() {

            $('#loaderimageDiv').css("display", "block");
            $("#lodingBar").html("Loading please wait....");
            var shadow = document.getElementById("divShd");
            var bws = getBrowserHeight();
            shadow.style.width = bws.width + "px";
            shadow.style.height = bws.height + "px";
            var left = parseInt((bws.width - 350) / 2);
            var top = parseInt((bws.height - 200) / 2);
            $('#divShd').css("display", "block");
            $('#UpdateProgressUserProfile').css("left", ($('.CustomerProfileCS').width() - $('#UpdateProgressUserProfile').width())/2);
            $('#UpdateProgressUserProfile').css("display", "block");
            return true;
        }
        function hideProgress() {
            $('#divShd').css("display", "none");
            $('#UpdateProgressUserProfile').css("display", "none");
            return true;
        }
        // A $( document ).ready() block.
        $(document).ready(function () {
            showProgress();
        });
        $(window).load(function () {
            // run code
            hideProgress();
            $('.iframeContent').css("display", "block");
        });
        function getBrowserHeight() {
            var intH = 0;
            var intW = 0;
            if (typeof window.innerWidth == 'number') {
                intH = window.innerHeight;
                intW = window.innerWidth;
            }
            else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
                intH = document.documentElement.clientHeight;
                intW = document.documentElement.clientWidth;
            }
            else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
                intH = document.body.clientHeight;
                intW = document.body.clientWidth;
            }
            return { width: parseInt(intW), height: parseInt(intH) };
        }
    </script>
</body>
</html>
