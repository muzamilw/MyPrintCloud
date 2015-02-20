<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SubscribeToNewsletter.ascx.cs"
    Inherits="Web2Print.UI.Controls.SubscribeToNewsletter" %>

<asp:Panel runat="server" ID="btnSubscribeNewsletter" DefaultButton="btnSendNews" CssClass="content_area_NewsLetter ">
    <div class="PnlSubscribeNewsletter container row">
        <div class="rounded_corners subscribe_sec" style="border: 2px solid #f3f3f3;
            background-image: url('images/SubscribeBoxBackground.jpg'); background-repeat: no-repeat;">
            <asp:Label ID="lblOurNews" runat="server" CssClass="NewsLtrHeadingCS"></asp:Label>
            <div class="NewsDescCs col-md-6 col-lg-6 col-sm-6 col-xs-12">
                <asp:Label ID="Nwsdesc" runat="server"></asp:Label>
            </div>
            <div class="NewsLtrTxtBxContCS col-md-6 col-lg-6 col-sm-6 col-xs-12">
                <asp:TextBox ID="txtEmailbox" runat="server" Text="Enter email address..." ValidationGroup="email"
                    CausesValidation="false" CssClass="txtSubscribe-Subscribe SubscribeTxtBoxCS"
                    ClientIDMode="Static">
                </asp:TextBox>
                <asp:Button ID="btnSendNews" runat="server" OnClientClick="return ValidateBottomSubscriberEmail();"
                    OnClick="btnGo_Click" CssClass="btnSubscribe rounded_corners" Text="Send" /><br />
            </div>
            <div class="NewsLtrErrDesCS">
                <asp:Label ID="errorMsg" runat="server" CssClass="NewsErrMesgCS"></asp:Label>
            </div>
            <div class="clearBoth">
                &nbsp;
            </div>
        </div>
        <div class="clearBoth">
            &nbsp;
        </div>
    </div>
    <script type="text/javascript">


        $(document).ready(function () {
            $("#txtEmailbox").attr("placeholder", "Enter email address...");

            var addEvent = function (elem, type, fn) {
                if (elem.addEventListener) elem.addEventListener(type, fn, false);
                else if (elem.attachEvent) elem.attachEvent('on' + type, fn);
            },
            textField = document.getElementById('txtEmailbox'),
            placeholder = "Enter email address...";
            addEvent(textField, 'focus', function () {
                if (this.value === placeholder) this.value = '';
            });
            addEvent(textField, 'blur', function () {
                if (this.value === '') this.value = placeholder;
            });
        });


        $('#<%= txtEmailbox.ClientID %>').keydown(function (event) {
            if (event.keyCode == 13) {
                $('#<%= btnSendNews.ClientID %>').click();
                return false;
            }
        });



        function ValidateBottomSubscriberEmail() {

            var email = $('#<%= txtEmailbox.ClientID %>').val().trim();
            if (ValidateEmail(email)) {
                $('#<%=errorMsg.ClientID %>').html('');
                return true;
            }
            else {
                return false;
            }
        }
        function ValidateEmail(email) {
            var isValid = true;
            if (email == '') {
                var emailxreq = "<%= Resources.MyResource.emailxreq %>";
                $('#<%=errorMsg.ClientID %>').css("display", "block");
                $('#<%=errorMsg.ClientID %>').html(emailxreq);
                isValid = false;
            }
            else {
                var re = new RegExp("^[A-Za-z0-9](([_\\.\\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\\.\\-]?[a-zA-Z0-9]+)*)\\.([A-Za-z]{2,})$");
                if (!re.test(email)) {
                    var PlxenterVEmail = "<%= Resources.MyResource.PlxenterVEmail %>";
                    $('#<%=errorMsg.ClientID %>').html(PlxenterVEmail);
                    isValid = false;
                }
            }
            return isValid;
        }
    </script>
</asp:Panel>
