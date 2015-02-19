<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs"
    Inherits="Web2Print.UI.test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script src="Scripts/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="http://www.google.com/jsapi?key=ABQIAAAALDWeTDQHOJCbCf0JnUqL8BT2yXp_ZAY8_ufC3CFXhHIE1NvwkxQA7AE8xB9MyWgHECPY2qimOp7BUQ"></script>
    <script src="scripts/clientLocation.js" type="text/javascript"></script>
    <script type="text/javascript">
        function $g(id) {
            return document.getElementById(id);
        }

        function displayLocation(latitudeEl, longitudeEl, cityEl, regionEl, countryEl, country_codeEl) {
            var Continent = { "AD": "Europe", "AE": "Asia", "AF": "Asia", "AG": "North America", "AI": "North America", "AL": "Europe", "AM": "Asia", "AN": "North America", "AO": "Africa", "AQ": "Antarctica", "AR": "South America", "AS": "Australia", "AT": "Europe", "AU": "Australia", "AW": "North America", "AZ": "Asia", "BA": "Europe", "BB": "North America", "BD": "Asia", "BE": "Europe", "BF": "Africa", "BG": "Europe", "BH": "Asia", "BI": "Africa", "BJ": "Africa", "BM": "North America", "BN": "Asia", "BO": "South America", "BR": "South America", "BS": "North America", "BT": "Asia", "BW": "Africa", "BY": "Europe", "BZ": "North America", "CA": "North America", "CC": "Asia", "CD": "Africa", "CF": "Africa", "CG": "Africa", "CH": "Europe", "CI": "Africa", "CK": "Australia", "CL": "South America", "CM": "Africa", "CN": "Asia", "CO": "South America", "CR": "North America", "CU": "North America", "CV": "Africa", "CX": "Asia", "CY": "Asia", "CZ": "Europe", "DE": "Europe", "DJ": "Africa", "DK": "Europe", "DM": "North America", "DO": "North America", "DZ": "Africa", "EC": "South America", "EE": "Europe", "EG": "Africa", "EH": "Africa", "ER": "Africa", "ES": "Europe", "ET": "Africa", "FI": "Europe", "FJ": "Australia", "FK": "South America", "FM": "Australia", "FO": "Europe", "FR": "Europe", "GA": "Africa", "GB": "Europe", "GD": "North America", "GE": "Asia", "GF": "South America", "GG": "Europe", "GH": "Africa", "GI": "Europe", "GL": "North America", "GM": "Africa", "GN": "Africa", "GP": "North America", "GQ": "Africa", "GR": "Europe", "GS": "Antarctica", "GT": "North America", "GU": "Australia", "GW": "Africa", "GY": "South America", "HK": "Asia", "HN": "North America", "HR": "Europe", "HT": "North America", "HU": "Europe", "ID": "Asia", "IE": "Europe", "IL": "Asia", "IM": "Europe", "IN": "Asia", "IO": "Asia", "IQ": "Asia", "IR": "Asia", "IS": "Europe", "IT": "Europe", "JE": "Europe", "JM": "North America", "JO": "Asia", "JP": "Asia", "KE": "Africa", "KG": "Asia", "KH": "Asia", "KI": "Australia", "KM": "Africa", "KN": "North America", "KP": "Asia", "KR": "Asia", "KW": "Asia", "KY": "North America", "KZ": "Asia", "LA": "Asia", "LB": "Asia", "LC": "North America", "LI": "Europe", "LK": "Asia", "LR": "Africa", "LS": "Africa", "LT": "Europe", "LU": "Europe", "LV": "Europe", "LY": "Africa", "MA": "Africa", "MC": "Europe", "MD": "Europe", "ME": "Europe", "MG": "Africa", "MH": "Australia", "MK": "Europe", "ML": "Africa", "MM": "Asia", "MN": "Asia", "MO": "Asia", "MP": "Australia", "MQ": "North America", "MR": "Africa", "MS": "North America", "MT": "Europe", "MU": "Africa", "MV": "Asia", "MW": "Africa", "MX": "North America", "MY": "Asia", "MZ": "Africa", "NA": "Africa", "NC": "Australia", "NE": "Africa", "NF": "Australia", "NG": "Africa", "NI": "North America", "NL": "Europe", "NO": "Europe", "NP": "Asia", "NR": "Australia", "NU": "Australia", "NZ": "Australia", "OM": "Asia", "PA": "North America", "PE": "South America", "PF": "Australia", "PG": "Australia", "PH": "Asia", "PK": "Asia", "PL": "Europe", "PM": "North America", "PN": "Australia", "PR": "North America", "PS": "Asia", "PT": "Europe", "PW": "Australia", "PY": "South America", "QA": "Asia", "RE": "Africa", "RO": "Europe", "RS": "Europe", "RU": "Europe", "RW": "Africa", "SA": "Asia", "SB": "Australia", "SC": "Africa", "SD": "Africa", "SE": "Europe", "SG": "Asia", "SH": "Africa", "SI": "Europe", "SJ": "Europe", "SK": "Europe", "SL": "Africa", "SM": "Europe", "SN": "Africa", "SO": "Africa", "SR": "South America", "ST": "Africa", "SV": "North America", "SY": "Asia", "SZ": "Africa", "TC": "North America", "TD": "Africa", "TF": "Antarctica", "TG": "Africa", "TH": "Asia", "TJ": "Asia", "TK": "Australia", "TM": "Asia", "TN": "Africa", "TO": "Australia", "TR": "Asia", "TT": "North America", "TV": "Australia", "TW": "Asia", "TZ": "Africa", "UA": "Europe", "UG": "Africa", "US": "North America", "UY": "South America", "UZ": "Asia", "VC": "North America", "VE": "South America", "VG": "North America", "VI": "North America", "VN": "Asia", "VU": "Australia", "WF": "Australia", "WS": "Australia", "YE": "Asia", "YT": "Africa", "ZA": "Africa", "ZM": "Africa", "ZW": "Africa" };
            var cloc = new ClientLocation.Location(google.loader.ClientLocation);
            if (latitudeEl) latitudeEl.innerHTML = cloc.latitude;
            if (longitudeEl) longitudeEl.innerHTML = cloc.longitude;
            if (cityEl) cityEl.innerHTML = cloc.address.city;
            if (regionEl) regionEl.innerHTML = cloc.address.region;
            if (country) country.innerHTML = cloc.address.country;
            if (country_codeEl) country_codeEl.innerHTML = cloc.address.country_code;
            $("#<%=locationCode.ClientID%>").val(cloc.address.country_code);
            if (cloc.address.country_code == '') {
                            
            } else {
                var continent = Continent[cloc.address.country_code];
                $("#<%=locationContinent.ClientID%>").val(continent);
               
            }
        }

        function init() {
            displayLocation($g("latitude"), $g("longitude"), $g("city"), $g("region"), $g("country"), $g("country_code"));
            if ($("#<%=hfRedirected.ClientID%>").val() == 0) {
                  $("#setCookieAndRedirect").click();
            }
        }


        var ClientLocation = {};

        ClientLocation.Address = function () {
            /// <field name="city" type="String" />
            /// <field name="region" type="String" />
            /// <field name="country" type="String" />
            /// <field name="country_code" type="String" />
            /// <returns type="ClientLocation.Address"/>
            if (arguments.length > 0) {
                this.city = arguments[0].city;
                this.region = arguments[0].region;
                this.country = arguments[0].country;
                this.country_code = arguments[0].country_code;
                return;
            }
            else {
                this.city = "";
                this.region = "";
                this.country = "";
                this.country_code = "";
            }

        }
        ClientLocation.Location = function () {
            /// <field name="latitude" type="Number" />
            /// <field name="longitude" type="Number" />
            /// <field name="address" type="ClientLocation.Address" />
            if (arguments.length > 0) {
                console.log(arguments[0]);
                this.latitude = arguments[0].latitude;
                this.longitude = arguments[0].longitude;
                this.address = arguments[0].address;

            }
            else {
                this.latitude = 0;
                this.longitude = 0;
                this.address = undefined;
            }

        }
    </script>
    
</head>
<body onload="init()">
    <form id="form1" runat="server">
    <div>
        latitude : <span id="latitude"></span>
        <br />
        longitude : <span id="longitude"></span>
        <br />
        city : <span id="city"></span>
        <br />
        region : <span id="region"></span>
        <br />
        country : <span id="country"></span>
        <br />
        country_code : <span id="country_code"></span>
        <br />
    </div>
         <asp:HiddenField ID="locationCode" runat="server" Value="0" />
        <asp:HiddenField ID="locationContinent" runat="server" Value="0" />
        <asp:HiddenField ID="hfRedirected" runat="server" Value="0" />
        <asp:Button ID="setCookieAndRedirect" runat="server" OnClick="Button1_Click" ClientIDMode="Static" style="display:none;" />
    </form>
   
</body> 
</html>
