<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ThemeSite.Master" AutoEventWireup="true"
    CodeBehind="ContactUs.aspx.cs" Inherits="Web2Print.UI.ContactUs" %>

<%@ Register Src="Controls/WhyChooseUs.ascx" TagName="WhyChooseUs" TagPrefix="uc7" %>
<%@ Register Src="Controls/PageBanner.ascx" TagName="PageBanner" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBanner" runat="server">
</asp:Content>
<asp:Content ID="SecondryPageContentsHolder" ContentPlaceHolderID="MainContent" runat="server">
        <script type="text/javascript" language="javascript">
            var isGeoCode = false;
        </script>
    <div id="SecondryPageMainContent" runat="server">
        <div class="content_area container">
            <div class="left_right_padding row">
                <div class="contact_us_heading">
                    <asp:Label ID="lblPageTitle" runat="server"></asp:Label>
                </div>
                <div class="white-container-lightgrey-border textAlignLeft rounded_corners">
                    <div class="cont_us_pad">
                        <div class="contactusLeftPanl">
                              <div id="ContactImage" runat="server" class="contact_item_image float_left">
                                        &nbsp;
                                    </div>
                                    <div class="float_left cntContactImagetxt">
                                        <asp:Label ID="lblMobileText" runat="server" Text="Tel: " Visible="false" style="display:none;"></asp:Label>
                                        <asp:Label ID="lblMobile" runat="server" Visible="false"></asp:Label><br />
                                       
                                        <asp:Label ID="lblFaxTxt" runat="server" Text="Fax : " Visible="false"></asp:Label>
                                        <asp:Label ID="lblFax" runat="server" Visible="false"></asp:Label>
                                    </div>
                                   <%-- <br />--%>
                                    <div class="dashboard_vertical_item_separator">
                                        &nbsp;</div>
                                    <div id="EmailImage" runat="server"  class="email_item_image float_left">
                                        &nbsp;
                                    </div>
                                    <div class="float_left cntEmailImageCntctUS" style="">
                                        <asp:Label ID="lblSales" runat="server" Text="Email" Visible="false"></asp:Label>
                                        <asp:Label ID="lblSalesEmail" runat="server" Visible="false"></asp:Label><br />
                                    </div>
                                    <div class="dashboard_vertical_item_separator">
                                        &nbsp;</div>
                                    <div id="HomeImage" runat="server" class="home_item_image float_left">
                                        &nbsp;
                                    </div>
                                    <div class="float_left cntAddressContactUS">
                                        <asp:Label ID="lblCompanyName" runat="server" Visible="false"></asp:Label>
                                        <br />
                                        <asp:Label ID="lblAddressLine1" runat="server" Visible="false"></asp:Label><br />
                                        <asp:Label ID="lblAddressLine2" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="LblCity" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblState" runat="server" Visible="false"></asp:Label><br />
                                        <asp:Label ID="lblCountry" runat="server" Visible="false"></asp:Label><br />
                                        <asp:Label ID="lblZipCode" runat="server" Visible="false"></asp:Label>
                                    </div>
                        </div>
                        <div class="contactusRightPanl">
                             <div id="imgmapContainer" class="MapPanel rounded_corners" runat="server">
                                        <asp:Image ID="imgGoogleImage" runat="server" Visible="false" />
                                    </div>
                                     <div id="gMapContainer" class="rounded_corners cntgMapContactUs"  runat="server">
                                        <div id="gMap" runat="server" class="rounded_corners gMapContactUs" visible="false" clientidmode="Static"></div>
                                     </div>
                        </div>
                     <div class="clearBoth">

                     </div>
                    </div>
                </div>
              
                <div id="contactusform" class="white-container-lightgrey-border textAlignLeft rounded_corners">
                    <div class="get_in_touch_heading">
                        <asp:Literal ID="ltrlgetintouch" runat="server" Text=" Get in touch..."></asp:Literal>
                    </div>
                    <div class="get_in_touch_sub_padding">
                        <table width="100%" cellpadding="5" cellspacing="5">
                            <tr>
                                <td>
                                    <asp:Literal ID="ltrlyourname" runat="server" Text="Your name"></asp:Literal>
                                </td>
                                <td>
                                    <div style="display:none"><asp:Literal ID="ltrlnoi" runat="server" Text="Nature of Enquiry"></asp:Literal></div>
                                    <asp:Literal ID="ltrlenquiry" runat="server" Text="Enquiry"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtName" runat="server" CssClass="text_box400 rounded_corners5"></asp:TextBox>
                                </td>
                                <td rowspan="5" valign="top">
                                    <div style="display:none"><asp:DropDownList ID="ddlEnqiryNature" runat="server" CssClass="dropdown rounded_corners5"
                                        Width="200px">
                                        <asp:ListItem Text="General" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Need a Quote"></asp:ListItem>
                                        <asp:ListItem Text="Design inquiry"></asp:ListItem>
                                        <asp:ListItem Text="Book studio/ design time"></asp:ListItem>
                                        <asp:ListItem Text="Status on delivery"></asp:ListItem>
                                    </asp:DropDownList></div>

                                    <asp:TextBox ID="txtEnquiry" runat="server" TextMode="MultiLine" CssClass="text_box400 rounded_corners5" ClientIDMode="Static" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Literal ID="ltrlcompany" runat="server" Text="Company name"></asp:Literal>
                                </td>
                                
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtCompany" runat="server" CssClass="text_box400 rounded_corners5"></asp:TextBox>
                                </td>
                                
                            </tr>
                            <tr>
                                <td>
                                    <asp:Literal ID="ltrlemail" runat="server" Text="Your email"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="text_box400 rounded_corners5"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td align="right">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="start_creating_btn rounded_corners5"
                                        OnClientClick="return ValidateSubmit();" Text="Submit" OnClick="btnSubmit_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
       
    </div>
     <script src="https://maps.googleapis.com/maps/api/js?v=3.exp&sensor=false"></script>
     <script>
         var geocoder;
         var map;
         function initialize() {
            var latlng;
             geocoder = new google.maps.Geocoder();
             if (isGeoCode) {
                 latlng = new google.maps.LatLng(51.507674, -0.166196);
             }
             else {

                 latlng = new google.maps.LatLng(lat, long);
             }
             var mapOptions = {
                 zoom: 14,
                 center: latlng,
                 mapTypeId: google.maps.MapTypeId.ROADMAP,
                 mapTypeControl: true,
                 mapTypeControlOptions: {
                     style: google.maps.MapTypeControlStyle.HORIZONTAL_BAR,
                     position: google.maps.ControlPosition.BOTTOM_CENTER
                 },
                 panControl: true,
                 panControlOptions: {
                     position: google.maps.ControlPosition.TOP_RIGHT
                 },
                 zoomControl: true,
                 zoomControlOptions: {
                     style: google.maps.ZoomControlStyle.LARGE,
                     position: google.maps.ControlPosition.LEFT_CENTER
                 },
                 scaleControl: true,
                 scaleControlOptions: {
                     position: google.maps.ControlPosition.TOP_LEFT
                 },
                 streetViewControl: true,
                 streetViewControlOptions: {
                     position: google.maps.ControlPosition.LEFT_TOP
                 }

             }




             map = new google.maps.Map(document.getElementById('gMap'), mapOptions);

//             var marker = new google.maps.Marker({
//                 position: latlng,
//                 map: map,
//                 title: info
//             });


             var infowindow = new google.maps.InfoWindow({
                 content: info
             });

//             var marker = new google.maps.Marker({
//                 position: latlng,
//                 map: map,
//                 title: 'Click for more details'
//             });


//             google.maps.event.addListener(marker, 'click', function () {
//                 infowindow.open(map, marker);
//             });



             if (isGeoCode) {

                
                 codeAddress(addressline);


             }
             
         }

         function codeAddress(address) {


             geocoder.geocode({ 'address': address }, function (results, status) {
                 if (status == google.maps.GeocoderStatus.OK) {
                     map.setCenter(results[0].geometry.location);
                     var infowindow = new google.maps.InfoWindow({
                         content: info
                     });

                     var marker = new google.maps.Marker({
                         position: results[0].geometry.location,
                         map: map,
                         title: 'Click for more details'
                     });
                     google.maps.event.addListener(marker, 'click', function () {
                         infowindow.open(map, marker);
                     }); ;
                 } else {
                     alert('Geocode was not successful for the following reason: ' + status);
                 }
             });
         }

         $(document).ready(function () {
             //google.maps.event.addDomListener(window, 'load', initialize);
             initialize();
         });

    </script>


    <script type="text/javascript" language="javascript">
        function ValidateSubmit() {

            var isValid = true;
            var email = $('#<%=txtEmail.ClientID %>').val().trim();
            isValid = ValidateEmail(email);

            if (isValid) {
                if ($('#<%=txtEnquiry.ClientID %>').val().trim() == '') {
                    ShowPopup('Message', 'Inquiry detail is required.');
                    isValid = false;
                }
            }

            return isValid;
        }


    </script>
</asp:Content>
