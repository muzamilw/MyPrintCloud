<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AskForDemo.aspx.cs" Inherits="Web2Print.UI.AskForDemo" %>

<%@ Register TagPrefix="recaptcha" Namespace="Recaptcha" Assembly="Recaptcha" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="//maxcdn.bootstrapcdn.com/font-awesome/4.1.0/css/font-awesome.min.css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
        <div id="pop_up">
            <div class="col-lg-12 col-xs-12 askPopUpBak" >
                <div class="page-header AskPad5" style="text-align: left; padding-top: 15px;">
                    <h1>Evaluate Demo Yourself</h1>
                </div>
                <div class="spacer-top spacer-bottom">
                    <h4><b>Evaluate MyPrintCloud Platinum W2P Solution Before Making Decision</b></h4>
                </div>
                <div class="row">
                    <div class="col-lg-9 col-xs-6">
                        <p style="text-align: left;padding-left: 7px;">
                                                     
                            Get access to Administration Area of OnPrintShop Solution for FREE!!!<br>
                            <br>
                             We are confident that our solution is Intuitive &amp; Simple to use with least IT skills.<br>
                            <br>
                            Don't take it for our word, Experience it yourself<br>
                            <br>
                            <span style=""><i class="fa fa-check-square"></i>&nbsp;&nbsp;<span style="line-height: 42px;">15 Days FREE Access to ALL Features.</span></span><br>
                        </p>
                    </div>
                    <div class="col-lg-3 col-xs-6" style="text-align: right;">
                        <img src="assets/demo.jpg" /><br>
                        <br>
                        <!--<button class="btn btn-warning" value="Ask For Demo" name="Ask For Demo">Ask For Demo <i class="fa fa-arrow-right"></i> </button><br/><br/>-->
                    </div>
                </div>
                <div>
                    <span class="panel-footer" style="border-top: 0px; padding: 0px; background: none;"><a style="text-decoration: none;">Simply Enter details below and receive an email to Access your Demo Account.</a></span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </div>
                <div id="Frmpopupap"></div>
                <div class="blog" style="padding: 5px;">
                    <div class="row">
                        <div class="col-lg-6 col-xs-6">
                            <div class="input-group">
                                <input id="firstname" runat="server" class=" form-control" type="text" value="" size="100" data-msg-required="First name is required" data-rule-required="true" placeholder="Full Name" name="firstname">
                                <span class="input-group-require">* </span>
                            </div>
                            <span class="error-block text-danger"></span>
                        </div>
                        <div class="col-lg-6 col-xs-6">
                            <div class="input-group">
                                <input id="emailid" runat="server" class=" form-control" type="text" value="" size="100" data-msg-required="Email is required" data-rule-required="true" placeholder="Email ID" name="emailid">
                                <span class="input-group-require">* </span>
                            </div>
                            <div class="input-group">
                                <span class="error-block text-danger"></span>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-6 col-xs-6">
                            <div class="input-group">
                                <input id="phone" runat="server" class=" form-control" value="" type="text" size="100" data-msg-required="Phone no is required." data-rule-required="true" placeholder="Phone No." name="phone">
                                <span class="input-group-require">* </span>
                            </div>
                            <div class="input-group">
                                <span class="error-block text-danger"></span>
                            </div>
                        </div>
                        <div class="col-lg-6 col-xs-6">
                            <div class="input-group">
                                <select id="country" class="form-control" name="country" data-rule-required="true" data-msg-required="Please Select Country.">
                                    <option value="" selected="selected">Select country</option>
                                    <option value="Afghanistan">Afghanistan</option>
                                    <option value="Albania">Albania</option>
                                    <option value="Algeria">Algeria</option>
                                    <option value="American">American Samoa</option>
                                    <option value="Andorra">Andorra</option>
                                    <option value="Angola">Angola</option>
                                    <option value="Anguilla">Anguilla</option>
                                    <option value="Antarctica">Antarctica</option>
                                    <option value="Antigua and Barbuda">Antigua and Barbuda</option>
                                    <option value="Argentina">Argentina</option>
                                    <option value="Armenia">Armenia</option>
                                    <option value="Aruba">Aruba</option>
                                    <option value="Australia">Australia</option>
                                    <option value="Austria">Austria</option>
                                    <option value="Azerbaijan">Azerbaijan</option>
                                    <option value="Bahamas">Bahamas</option>
                                    <option value="Bahrain">Bahrain</option>
                                    <option value="Bangladesh">Bangladesh</option>
                                    <option value="Barbados">Barbados</option>
                                    <option value="Belarus">Belarus</option>
                                    <option value="Belgium">Belgium</option>
                                    <option value="Belize">Belize</option>
                                    <option value="Benin">Benin</option>
                                    <option value="Bermuda">Bermuda</option>
                                    <option value="Bhutan">Bhutan</option>
                                    <option value="Bolivia">Bolivia</option>
                                    <option value="Bosnia and Herzegowina">Bosnia and Herzegowina</option>
                                    <option value="Botswana">Botswana</option>
                                    <option value="Bouvet Island">Bouvet Island</option>
                                    <option value="Brazil">Brazil</option>
                                    <option value="British Indian Ocean Territory">British Indian Ocean Territory</option>
                                    <option value="Brunei Darussalam">Brunei Darussalam</option>
                                    <option value="Bulgaria">Bulgaria</option>
                                    <option value="Burkina Faso">Burkina Faso</option>
                                    <option value="Burundi">Burundi</option>
                                    <option value="Cambodia">Cambodia</option>
                                    <option value="Cameroon">Cameroon</option>
                                    <option value="Canada">Canada</option>
                                    <option value="Cape Verde">Cape Verde</option>
                                    <option value="Cayman Islands">Cayman Islands</option>
                                    <option value="Central African Republic">Central African Republic</option>
                                    <option value="Chad">Chad</option>
                                    <option value="Chile">Chile</option>
                                    <option value="China">China</option>
                                    <option value="Christmas Island">Christmas Island</option>
                                    <option value="Cocos (Keeling) Islands">Cocos (Keeling) Islands</option>
                                    <option value="Colombia">Colombia</option>
                                    <option value="Comoros">Comoros</option>
                                    <option value="Congo">Congo</option>
                                    <option value="Cook Islands">Cook Islands</option>
                                    <option value="Costa Rica">Costa Rica</option>
                                    <option value="Cote D'Ivoire">Cote D'Ivoire</option>
                                    <option value="Croatia">Croatia</option>
                                    <option value="Cuba">Cuba</option>
                                    <option value="Cyprus">Cyprus</option>
                                    <option value="Czech Republic">Czech Republic</option>
                                    <option value="Denmark">Denmark</option>
                                    <option value="Djibouti">Djibouti</option>
                                    <option value="Dominica">Dominica</option>
                                    <option value="Dominican Republic">Dominican Republic</option>
                                    <option value="East Timor">East Timor</option>
                                    <option value="Ecuador">Ecuador</option>
                                    <option value="Egypt">Egypt</option>
                                    <option value="El Salvador">El Salvador</option>
                                    <option value="Equatorial Guinea">Equatorial Guinea</option>
                                    <option value="Eritrea">Eritrea</option>
                                    <option value="Estonia">Estonia</option>
                                    <option value="Ethiopia">Ethiopia</option>
                                    <option value="Falkland Islands (Malvinas)">Falkland Islands (Malvinas)</option>
                                    <option value="Faroe Islands">Faroe Islands</option>
                                    <option value="Fiji">Fiji</option>
                                    <option value="Finland">Finland</option>
                                    <option value="France">France</option>
                                    <option value="France, Metropolitan">France, Metropolitan</option>
                                    <option value="French Guiana">French Guiana</option>
                                    <option value="French Polynesia">French Polynesia</option>
                                    <option value="French Southern Territories">French Southern Territories</option>
                                    <option value="Gabon">Gabon</option>
                                    <option value="Gambia">Gambia</option>
                                    <option value="Georgia">Georgia</option>
                                    <option value="Germany">Germany</option>
                                    <option value="Ghana">Ghana</option>
                                    <option value="Gibraltar">Gibraltar</option>
                                    <option value="Greece">Greece</option>
                                    <option value="Greenland">Greenland</option>
                                    <option value="Grenada">Grenada</option>
                                    <option value="Guadeloupe">Guadeloupe</option>
                                    <option value="Guam">Guam</option>
                                    <option value="Guatemala">Guatemala</option>
                                    <option value="Guernsey">Guernsey</option>
                                    <option value="Guinea">Guinea</option>
                                    <option value="Guinea-bissau">Guinea-bissau</option>
                                    <option value="Guyana">Guyana</option>
                                    <option value="Haiti">Haiti</option>
                                    <option value="Heard and Mc Donald Islands">Heard and Mc Donald Islands</option>
                                    <option value="Honduras">Honduras</option>
                                    <option value="Hong Kong">Hong Kong</option>
                                    <option value="Hungary">Hungary</option>
                                    <option value="Iceland">Iceland</option>
                                    <option value="India">India</option>
                                    <option value="Indonesia">Indonesia</option>
                                    <option value="Iran (Islamic Republic of)">Iran (Islamic Republic of)</option>
                                    <option value="Iraq">Iraq</option>
                                    <option value="Ireland">Ireland</option>
                                    <option value="Israel">Israel</option>
                                    <option value="Italy">Italy</option>
                                    <option value="Jamaica">Jamaica</option>
                                    <option value="Japan">Japan</option>
                                    <option value="Jersey">Jersey</option>
                                    <option value="Jordan">Jordan</option>
                                    <option value="Kazakhstan">Kazakhstan</option>
                                    <option value="Kenya">Kenya</option>
                                    <option value="Kiribati">Kiribati</option>
                                    <option value="Korea, Republic of">Korea, Republic of</option>
                                    <option value="Kuwait">Kuwait</option>
                                    <option value="Kyrgyzstan">Kyrgyzstan</option>
                                    <option value="Latvia">Latvia</option>
                                    <option value="Lebanon">Lebanon</option>
                                    <option value="Lesotho">Lesotho</option>
                                    <option value="Liberia">Liberia</option>
                                    <option value="Libyan Arab Jamahiriya">Libyan Arab Jamahiriya</option>
                                    <option value="Liechtenstein">Liechtenstein</option>
                                    <option value="Lithuania">Lithuania</option>
                                    <option value="Luxembourg">Luxembourg</option>
                                    <option value="Macau">Macau</option>
                                    <option value="Madagascar">Madagascar</option>
                                    <option value="Malawi">Malawi</option>
                                    <option value="Malaysia">Malaysia</option>
                                    <option value="Maldives">Maldives</option>
                                    <option value="Mali">Mali</option>
                                    <option value="Malta">Malta</option>
                                    <option value="Marshall Islands">Marshall Islands</option>
                                    <option value="Martinique">Martinique</option>
                                    <option value="Mauritania">Mauritania</option>
                                    <option value="Mauritius">Mauritius</option>
                                    <option value="Mayotte">Mayotte</option>
                                    <option value="Mexico">Mexico</option>
                                    <option value="Micronesia, Federated States of">Micronesia, Federated States of</option>
                                    <option value="Moldova, Republic of">Moldova, Republic of</option>
                                    <option value="Monaco">Monaco</option>
                                    <option value="Mongolia">Mongolia</option>
                                    <option value="Montenegro">Montenegro</option>
                                    <option value="Montserrat">Montserrat</option>
                                    <option value="Morocco">Morocco</option>
                                    <option value="Mozambique">Mozambique</option>
                                    <option value="Myanmar">Myanmar</option>
                                    <option value="Namibia">Namibia</option>
                                    <option value="Nauru">Nauru</option>
                                    <option value="Nepal">Nepal</option>
                                    <option value="Netherlands">Netherlands</option>
                                    <option value="Netherlands Antilles">Netherlands Antilles</option>
                                    <option value="New Caledonia">New Caledonia</option>
                                    <option value="New Zealand">New Zealand</option>
                                    <option value="Nicaragua">Nicaragua</option>
                                    <option value="Niger">Niger</option>
                                    <option value="Nigeria">Nigeria</option>
                                    <option value="Niue">Niue</option>
                                    <option value="Norfolk Island">Norfolk Island</option>
                                    <option value="Northern Mariana Islands">Northern Mariana Islands</option>
                                    <option value="Norway">Norway</option>
                                    <option value="Oman">Oman</option>
                                    <option value="Pakistan">Pakistan</option>
                                    <option value="Palau">Palau</option>
                                    <option value="Panama">Panama</option>
                                    <option value="Papua New Guinea">Papua New Guinea</option>
                                    <option value="Paraguay">Paraguay</option>
                                    <option value="Peru">Peru</option>
                                    <option value="Philippines">Philippines</option>
                                    <option value="Pitcairn">Pitcairn</option>
                                    <option value="Poland">Poland</option>
                                    <option value="Portugal">Portugal</option>
                                    <option value="Puerto Rico">Puerto Rico</option>
                                    <option value="Qatar">Qatar</option>
                                    <option value="Reunion">Reunion</option>
                                    <option value="Romania">Romania</option>
                                    <option value="Russian Federation">Russian Federation</option>
                                    <option value="Rwanda">Rwanda</option>
                                    <option value="Saint Kitts and Nevis">Saint Kitts and Nevis</option>
                                    <option value="Saint Lucia">Saint Lucia</option>
                                    <option value="Samoa">Samoa</option>
                                    <option value="San Marino">San Marino</option>
                                    <option value="Sao Tome and Principe">Sao Tome and Principe</option>
                                    <option value="Saudi Arabia">Saudi Arabia</option>
                                    <option value="Senegal">Senegal</option>
                                    <option value="Serbia">Serbia</option>
                                    <option value="Seychelles">Seychelles</option>
                                    <option value="Sierra Leone">Sierra Leone</option>
                                    <option value="Singapore">Singapore</option>
                                    <option value="Slovakia (Slovak Republic)">Slovakia (Slovak Republic)</option>
                                    <option value="Slovenia">Slovenia</option>
                                    <option value="Solomon Islands">Solomon Islands</option>
                                    <option value="Somalia">Somalia</option>
                                    <option value="South Africa">South Africa</option>
                                    <option value="Spain">Spain</option>
                                    <option value="Sri Lanka">Sri Lanka</option>
                                    <option value="St. Helena">St. Helena</option>
                                    <option value="St. Pierre and Miquelon">St. Pierre and Miquelon</option>
                                    <option value="Sudan">Sudan</option>
                                    <option value="Suriname">Suriname</option>
                                    <option value="Svalbard and Jan Mayen Islands">Svalbard and Jan Mayen Islands</option>
                                    <option value="Swaziland">Swaziland</option>
                                    <option value="Sweden">Sweden</option>
                                    <option value="Switzerland">Switzerland</option>
                                    <option value="Syrian Arab Republic">Syrian Arab Republic</option>
                                    <option value="Taiwan">Taiwan</option>
                                    <option value="Tajikistan">Tajikistan</option>
                                    <option value="Tanzania, United Republic of">Tanzania, United Republic of</option>
                                    <option value="Thailand">Thailand</option>
                                    <option value="Togo">Togo</option>
                                    <option value="Tokelau">Tokelau</option>
                                    <option value="Tonga">Tonga</option>
                                    <option value="Trinidad and Tobago">Trinidad and Tobago</option>
                                    <option value="Tunisia">Tunisia</option>
                                    <option value="Turkey">Turkey</option>
                                    <option value="Turkmenistan">Turkmenistan</option>
                                    <option value="Turks and Caicos Islands">Turks and Caicos Islands</option>
                                    <option value="Tuvalu">Tuvalu</option>
                                    <option value="Uganda">Uganda</option>
                                    <option value="Ukraine">Ukraine</option>
                                    <option value="United Arab Emirates">United Arab Emirates</option>
                                    <option value="United Kingdom">United Kingdom</option>
                                    <option value="United States">United States</option>
                                    <option value="United States Minor Outlying Islands">United States Minor Outlying Islands</option>
                                    <option value="Uruguay">Uruguay</option>
                                    <option value="Uzbekistan">Uzbekistan</option>
                                    <option value="Vanuatu">Vanuatu</option>
                                    <option value="Vatican City State (Holy See)">Vatican City State (Holy See)</option>
                                    <option value="Venezuela">Venezuela</option>
                                    <option value="Vietnam">Vietnam</option>
                                    <option value="Virgin Islands (British)">Virgin Islands (British)</option>
                                    <option value="Virgin Islands (U.S.)">Virgin Islands (U.S.)</option>
                                    <option value="Wallis and Futuna Islands">Wallis and Futuna Islands</option>
                                    <option value="Western Sahara">Western Sahara</option>
                                    <option value="Yemen">Yemen</option>
                                    <option value="Yugoslavia">Yugoslavia</option>
                                    <option value="Zaire">Zaire</option>
                                    <option value="Zambia">Zambia</option>
                                    <option value="Zimbabwe">Zimbabwe</option>
                                </select>
                                <span class="input-group-require">* </span>
                            </div>
                            <div class="input-group">
                                <span class="error-block text-danger"></span>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-12 col-xs-12">
                            <!--<p>Tell us about your specific requirements or business challenges.
							   Experts at OnPrintShop will analyze them and consult you over a personalized demo.<br/><br/>-->
                            <textarea id="enquiry" class="form-control" value="" cols="50" rows="2" placeholder="Share your business requirements or challenges.." tabindex="0" name="enquiry" style="margin-bottom: 10px;"></textarea>
                            <div class="input-group">
                                <div class="spacer-top"></div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12 col-xs-12">
                            <div class="row">
                                <div class="col-lg-3 col-sm-12" style="height: 128px; margin-bottom:10px;">
                                    <div class="pull-left spacer-right">
                                        <recaptcha:RecaptchaControl
                                            ID="recaptcha"
                                            runat="server"
                                            PublicKey="6LeXrfASAAAAAEV_Ml9tMccObqz1IZvf-jA2Vjgd "
                                            PrivateKey="6LeXrfASAAAAADCfcMPxk2aKoTdK-prlfftBJn-1 " />
                                    </div>
                                    <div>
                                        <br>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-sm-12">

                                    <div class="input-group">
                                        <span id="ValidCaptcha" runat="server" class="error-block" style="color: #a94442;"></span>
                                    </div>
                                </div>
                                <input type="hidden" id="captcha" name="captcha" value="0">
                                <div class="col-lg-12 pull-right">
                                    <asp:Button ID="btnSubmit" runat="server" OnClientClick="return validateData();" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-primary" />
                                    <input type="hidden" value="1" name="Submit_x">
                                    <input type="hidden" value="1" name="Submit_y">
                                    <a class="btn btn btn-link btn-xs" onclick="resetData();">Reset</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="text-right">
                    <a class="btn btn-link btn-xs" onclick="closeFancybox();">Don't Ask Again</a>
                </div>
                <br>
                <div class="text-danger">
                    <small>
                        <p style="text-align: justify;">
                            Disclaimer: Our demo store is evaluated and tested by multiple users at a time and all data reset every Monday. Chances are that your store setup configuration might be changed and you might find irrelevant test data or settings.  
					   <br>
                            <br>
                            We would recommend that before evaluating the solution on your own, request a personalized demo and then explore the free demo, which has been very successful for our existing clients and they have referred us to many more clients. 
                        </p>
                    </small>
                </div>
            </div>
        </div>
        <script src="Scripts/jquery-1.9.1.js"></script>
        <script>
            function closeFancybox() {
                window.parent.location.reload();
            }
            function resetData() {

                $("#<%=emailid.ClientID%>").val('');
                $("#<%=firstname.ClientID%>").val('');
                $("#<%=phone.ClientID%>").val('');
            }

            function validateData() {
                var emailtxt = $("#<%=emailid.ClientID%>").val();
                var isValid = true;
                if (emailtxt == '') {
                    $("#<%=emailid.ClientID%>").css("border", "1px solid red");
                    isValid = false;
                }
                else {
                    var re = new RegExp("^[A-Za-z0-9](([_\\.\\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\\.\\-]?[a-zA-Z0-9]+)*)\\.([A-Za-z]{2,})$");
                    if (!re.test(emailtxt)) {
                        $("#<%=emailid.ClientID%>").css("border", "1px solid red");
                    isValid = false;
                }
            }
            return isValid;
        }
        </script>
    </form>
</body>
</html>
