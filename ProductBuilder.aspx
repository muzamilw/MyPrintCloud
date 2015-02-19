<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ProductBuilder.aspx.cs" Inherits="Web2Print.UI.ProductBuilder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">

        function element_hover(obj, state) {

            var old_top = "";
            var old_bottom = "";
            var old_color = "";
            if (state == true) {
                old_color = obj.style.backgroundColor;
                obj.style.backgroundColor = "#c2dfeb";
            }
            else {
                obj.style.backgroundColor = old_color;

            }
        }

        function Sethighligted(obj, objtlb) {
            //alert(obj);

            document.getElementById(obj).onmouseover = '';
            document.getElementById(obj).onmouseout = '';

            document.getElementById(obj).style.backgroundColor = "#93B0C3";
            var tg = document.getElementById(objtlb).getElementsByTagName('div');
            //var objelm = obj; //.id;

            for (var i = 0; i < tg.length; i++) {
                //var nextelm = tg.item(i).id;
                if (obj != tg.item(i).id) {
                    var strhtml = tg.item(i).innerHTML
                    strhtml = strhtml.replace(/^\s+|\s+$/g, '');
                    if (strhtml != '') {
                        //alert(tg.item(i).id);
                        tg.item(i).style.backgroundColor = "#e7e7e7";
                        tg.item(i).onmouseover = function ()
                        { element_hover(this, true); };
                        tg.item(i).onmouseout = function ()
                        { element_hover(this, false); };


                    }
                }

            }

        }

        var xmlHttp
        var valss = 0;
        var cids = 0;
        function Getcontents(cid, vals, tbl, divs) {
            xmlHttp = GetXmlHttpObject()
            if (xmlHttp == null) {
                alert("Browser does not support HTTP Request")
                return
            }

            document.getElementById('product_matrix_loader_image').style.display = '';
            var url = "ProductBuilderDetail.aspx"
            url = url + "?cid=" + cid + "&vls=" + vals;
            url = url + "&sid=" + Math.random()
            valss = vals;
            cids = cid;
            xmlHttp.onreadystatechange = stateChanged; //(vals)
            if (tbl != '') {
                Sethighligted(divs, tbl);
            }
            xmlHttp.open("GET", url, true)
            xmlHttp.send(null)
            document.getElementById('product_matrix_loader_image').style.display = 'none';
        }
        function Getpcontents(cid, vals, tbl, divs, pid) {
            xmlHttp = GetXmlHttpObject()
            if (xmlHttp == null) {
                alert("Browser does not support HTTP Request")
                return
            }
            document.getElementById('product_matrix_loader_image').style.display = '';
            var url = "ProductBuilderDetail.aspx"
            url = url + "?cid=" + cid + "&vls=" + vals + "&pid=" + pid;
            url = url + "&sid=" + Math.random()
            valss = vals;
            xmlHttp.onreadystatechange = stateChanged2;
            Sethighligted(divs, tbl);
            xmlHttp.open("GET", url, true)
            xmlHttp.send(null)
            document.getElementById('product_matrix_loader_image').style.display = 'none';
        }
        function stateChanged2() {
            if (xmlHttp.readyState == 4 || xmlHttp.readyState == "complete") {
                // alert(xmlHttp.responseText);
                document.getElementById("productsdive").innerHTML = '';
                document.getElementById("productsdive").innerHTML = xmlHttp.responseText;

            }
        }
        function stateChanged() {
            if (xmlHttp.readyState == 4 || xmlHttp.readyState == "complete") {
                // alert(xmlHttp.responseText);
                var innerhtml = '';
                if (valss == 1) {
                    innerhtml = document.getElementById("MainContent_divPages").innerHTML;
                    innerhtml = innerhtml.replace(/^\s+|\s+$/g, '');
                    //alert(innerhtml);
                    if (innerhtml == '') {
                        document.getElementById("MainContent_divPages").innerHTML = '';
                        document.getElementById("MainContent_divPages").innerHTML = xmlHttp.responseText;

                    }
                    var hidtotal2 = document.getElementById('MainContent_hidtotal2').value;
                    if (hidtotal2 == 1) {
                        var val1 = 2;
                        Getcontents(cids, val1, '', '');

                        //Getcontents(
                    }
                }
                else if (valss == 2) {
                    innerhtml = document.getElementById("MainContent_divColours").innerHTML;
                    innerhtml = innerhtml.replace(/^\s+|\s+$/g, '');
                    if (innerhtml == '') {
                        document.getElementById("MainContent_divColours").innerHTML = '';
                        document.getElementById("MainContent_divColours").innerHTML = xmlHttp.responseText;
                    }
                    var hidtotal3 = document.getElementById('MainContent_hidtotal3').value;
                    if (hidtotal3 == 1) {
                        var val1 = 3;
                        Getcontents(cids, val1, '', '');

                        //Getcontents(
                    }

                }
                else if (valss == 3) {
                    innerhtml = document.getElementById("MainContent_divBookbinding").innerHTML;
                    innerhtml = innerhtml.replace(/^\s+|\s+$/g, '');
                    if (innerhtml == '') {
                        document.getElementById("MainContent_divBookbinding").innerHTML = '';
                        document.getElementById("MainContent_divBookbinding").innerHTML = xmlHttp.responseText;
                    }

                    var hidtotal4 = document.getElementById('MainContent_hidtotal4').value;
                    if (hidtotal4 == 1) {
                        var val1 = 4;
                        Getcontents(cids, val1, '', '');

                        //Getcontents(
                    }

                } else if (valss == 4) {
                    innerhtml = document.getElementById("MainContent_divPaper").innerHTML;
                    innerhtml = innerhtml.replace(/^\s+|\s+$/g, '');
                    if (innerhtml == '') {
                        document.getElementById("MainContent_divPaper").innerHTML = '';
                        document.getElementById("MainContent_divPaper").innerHTML = xmlHttp.responseText;
                    }
                    var hidtotal5 = document.getElementById('MainContent_hidtotal5').value;
                    if (hidtotal5 == 1) {
                        var val1 = 5;
                        Getcontents(cids, val1, '', '');

                        //Getcontents(
                    }
                } else if (valss == 5) {
                    innerhtml = document.getElementById("MainContent_divFinishing").innerHTML;
                    innerhtml = innerhtml.replace(/^\s+|\s+$/g, '');
                    if (innerhtml == '') {
                        document.getElementById("MainContent_divFinishing").innerHTML = '';
                        document.getElementById("MainContent_divFinishing").innerHTML = xmlHttp.responseText;
                    }
                    var hidtotal6 = document.getElementById('MainContent_hidtotal6').value;
                    if (hidtotal6 == 1) {
                        var val1 = 6;
                        Getcontents(cids, val1, '', '');

                        //Getcontents(
                    }
                } else if (valss == 6) {
                    innerhtml = document.getElementById("MainContent_Refinement").innerHTML;
                    innerhtml = innerhtml.replace(/^\s+|\s+$/g, '');
                    if (innerhtml == '') {
                        document.getElementById("MainContent_Refinement").innerHTML = '';
                        document.getElementById("MainContent_Refinement").innerHTML = xmlHttp.responseText;
                    }
                }
                //document.getElementById("divPages").innerHTML=xmlHttp.responseText 
            }
        }

        function GetXmlHttpObject() {
            var xmlHttp = null;
            try {
                // Firefox, Opera 8.0+, Safari
                xmlHttp = new XMLHttpRequest();
            }
            catch (e) {
                //Internet Explorer
                try {
                    xmlHttp = new ActiveXObject("Msxml2.XMLHTTP");
                }
                catch (e) {
                    xmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
                }
            }
            return xmlHttp;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="width: 735px;">
        <img id="product_matrix_loader_image" src="images/builder/matrix-ajax-loader.gif"
            alt="" style="display: none;" />
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr style="height: 40px">
                <td align="left" style="text-align: left;" valign="top">
                    <span class="headingRoundTop"><span id="ProductCategory">Product Builder</span>
                    </span>
                </td>
                <td align="right" style="padding-right: 20px;">
                    <asp:LinkButton ID="btn_back" runat="server" Text="Click here to go back" OnClick="btn_back_Click"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td class="hx_webshop_matrix_table_header" style="width: 100px;">
                                Format
                            </td>
                            <td class="hx_webshop_matrix_table_header" style="width: 100px;">
                                Pages
                            </td>
                            <td class="hx_webshop_matrix_table_header" style="width: 100px;">
                                Colours
                            </td>
                            <td class="hx_webshop_matrix_table_header" style="width: 100px;">
                                Paper
                            </td>
                            <td class="hx_webshop_matrix_table_header" style="width: 100px;">
                                Bookbinding
                            </td>
                            <td class="hx_webshop_matrix_table_header" style="width: 100px;">
                                Finishing
                            </td>
                            <td class="hx_webshop_matrix_table_header" style="width: 100px;">
                                Refinement
                            </td>
                        </tr>
                        <tr>
                            <td id="divformat" valign="top">
                                <%=GETFORMATDIVE()%>
                            </td>
                            <td id="divPages" runat="server" valign="top">
                            </td>
                            <td id="divColours" runat="server" valign="top">
                            </td>
                            <td id="divBookbinding" runat="server" valign="top">
                            </td>
                            <td id="divPaper" runat="server" valign="top">
                            </td>
                            <td id="divFinishing" runat="server" valign="top">
                            </td>
                            <td id="Refinement" runat="server" valign="top">
                            </td>
                        </tr>
                    </table>
                    <br />
                    <div id="productsdive">
                    </div>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hidtotal1" runat="server" Value="0" />
        <asp:HiddenField ID="hidtotal2" runat="server" Value="0" />
        <asp:HiddenField ID="hidtotal3" runat="server" Value="0" />
        <asp:HiddenField ID="hidtotal4" runat="server" Value="0" />
        <asp:HiddenField ID="hidtotal5" runat="server" Value="0" />
        <asp:HiddenField ID="hidtotal6" runat="server" Value="0" />
    </div>
</asp:Content>
