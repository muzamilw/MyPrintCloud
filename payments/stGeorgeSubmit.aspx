<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="stGeorgeSubmit.aspx.cs" Inherits="Web2Print.UI.payments.stGeorgeSubmit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>HPP POST</title>
    <script src="../Scripts/jquery-1.9.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui-1.9.0.js" type="text/javascript"></script>
        <script src="../Scripts/utilities.js" type="text/javascript"></script>
    <script type="text/javascript" language="JavaScript">

        function validate(form) {
            if ((form.customerReference.value == '') | (form.amount.value == '')) {
                alert("Please enter required fields");
            } else {
                form.submit();
            }
        }

        function SubmitForm() {
            var form = document.getElementById("paymentForm");
            if ((form.customerReference.value == '') | (form.amount.value == '')) {
                alert("Please enter required fields");
            } else {
                form.submit();
            }
        }
        $(document).ready(function () { showLoader(); });

        function showLoader() {
            var shadow = parent.document.getElementById("divShd");
            var bws = getBrowserHeight();
            shadow.style.width = bws.width + "px";
            shadow.style.height = bws.height + "px";
            var left = parseInt((bws.width - 500) / 2);
            var top = parseInt((bws.height - 200) / 2);
            //shadow = null;
            $('#' + shadow.id).css("display", "block");

            $('#UpdateProgressUserProfile').css("display", "block");

            //window.location.reload(true);
        }
    </script>
</head>
<body>
    <div align="center">
        
        <form name="frmstGeorgeSubmit" runat="server" id ="paymentForm"  method="POST">
            <div id="divShd" class="opaqueLayer">
    </div>
             <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdateProgress ID="UpdateProgressUserProfile" runat="server">
            <ProgressTemplate>
                <asp:Panel ID="Panel2" CssClass="loader" runat="server">
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
            </ProgressTemplate>
        </asp:UpdateProgress>

        <table style="display:none;">
            <tr>
                <td>
                    <b>Customer Reference: *</b>
                </td>
                <td>
                    <input type="text" runat="server" id="customerReference" name="customerReference" maxlength="50" size="25" />
                </td>
            </tr>
            <tr>
                <td>
                    <b>Cardholder's Name:</b>
                </td>
                <td>
                    <input type="text" name="cardHolderName" maxlength="50" size="25" />
                </td>
            </tr>
            <tr>
                <td>
                    <b>Description:</b>
                </td>
                <td>
                    <input type="text" name="description" maxlength="255" size="25" />
                </td>
            </tr>
            <tr>
                <td>
                    <b>E-mail Address:</b>
                </td>
                <td>
                    <input type="text" name="email" maxlength="50" size="25" />
                </td>
            </tr>
            <tr>
                <td>
                    <b>Amount: *</b>
                </td>
                <td>
                    <input type="text" id="amount" runat="server" name="amount" maxlength="11" size="11" />
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    * Required Field
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <input type="button" runat="server" id="btnSubmit" value="Continue" onclick="validate(this.form)">
                </td>
            </tr>
        </table>
        </form>
    </div>
</body>
</html>
