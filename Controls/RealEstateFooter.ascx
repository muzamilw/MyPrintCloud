<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RealEstateFooter.ascx.cs"
    Inherits="Web2Print.UI.Controls.RealEstateFooter" %>

<div class="content_area" style="width:100%;box-shadow: 0 -4px 22px -1px lightgray; position: relative; z-index: 1000;">
    <link href="../Styles/RealEstate_StyleSheet.css" rel="stylesheet" />

    <script>
        $(document).ready(function () {
            var Width = $(window).width();

            if (Width > 1025) {
                Width = Width - 20;
                document.getElementById("Parent-Div").style.width = Width + "px";
            }
        });
    </script>
    <%------------------------------------- Starting Parent Div (Contact) ------------------------------------%>
    <div class="Parent-Div" id="Parent-Div" style="width:100%; height:170px;">

        <%------------------------------------- Starting Main Div (Contact) ------------------------------------%>
        <div class="Contact-Main-Div" style="width:30%; margin-right: 10px;">
            <div class="controlDivStyle">
                <asp:TextBox ID="txtName" CssClass="controlStyle" Height="20px" runat="server" PlaceHolder="Name" Style="padding-left: 10px; padding-right: 10px;" BackColor="#E6E6E6" Font-Names="myraid pro" Font-Size="11pt" ForeColor="#a5a6a5"></asp:TextBox>
            </div>

            <div class="controlDivStyle">
                <asp:TextBox ID="txtEmail" CssClass="controlStyle" Height="20px" runat="server" PlaceHolder="E-mail Address" Style="padding-left: 10px; padding-right: 10px;" BackColor="#E6E6E6" Font-Names="myraid pro" Font-Size="11pt" ForeColor="#a5a6a5"></asp:TextBox>
            </div>

            <div class="controlDivStyle">
                <asp:TextBox ID="txtTelephone" CssClass="controlStyle" Height="20px" runat="server" PlaceHolder="Telephone Number" Style="padding-left: 10px; padding-right: 10px;" BackColor="#E6E6E6" Font-Names="myraid pro" Font-Size="11pt" ForeColor="#a5a6a5"></asp:TextBox>
            </div>

            <div class="controlDivStyle">
                <asp:TextBox ID="txtCompany" CssClass="controlStyle" Height="20px" runat="server" PlaceHolder="Company Address" Style="padding-left: 10px; padding-right: 10px;" BackColor="#E6E6E6" Font-Names="myraid pro" Font-Size="11pt" ForeColor="#a5a6a5"></asp:TextBox>
            </div>

        </div>
        <%------------------------------------- Ending Main Div (Contact) ------------------------------------%>

        <%------------------------------------- Starting Main Div (Message) ------------------------------------%>
        <div class="Message-Main-Div">


            <div style="margin-bottom: 2px;">
                <asp:TextBox ID="txtEnquiry" CssClass="controlStyle" TextMode="MultiLine" runat="server" Height="100px" PlaceHolder="Write Yor Message here ..." Style="padding-left: 10px;" BackColor="#E6E6E6" Font-Names="myraid pro" Font-Size="11pt" ForeColor="#a5a6a5"></asp:TextBox>
            </div>

            <div>
                <div style="box-shadow: 3px 3px 8px black; width: 60px; margin-top: 10px; margin-bottom: 10px; float: right;">
                    <asp:Button ID="btnSubmit" runat="server" Text="Send" Height="25px" Width="60px" BackColor="#F12D27" BorderStyle="None" Font-Names="Arial" Font-Size="12pt" ForeColor="White" OnClick="btnSubmit_Click" />
                </div>
            </div>

            <%------------------------------------- Ending Main Div (Message) ------------------------------------%>
        </div>

        <%------------------------------------- Starting SocialMedia Div (Message) ------------------------------------%>
        <div class="SocialMedia-Main-Div">

            <div>
                <img src="../Styles/images/linkedin.png" width="33px" height="34px" />
            </div>
            <div>
                <img src="../Styles/images/Twitter.png" width="33px" height="34px" />
            </div>
            <div>
                <img src="../Styles/images/Facebook.png" width="33px" height="34px" />
            </div>
            <div>
                <img src="../Styles/images/googlePlus.png" width="33px" height="34px" />
            </div>

            <%------------------------------------- Ending SocialMedia Div (Message) ------------------------------------%>
        </div>


        <%------------------------------------- Starting SocialMedia Message Div (Message) ------------------------------------%>
        <div class="SocialMedia-Message-Main-Div">

            <div>
                <asp:TextBox ID="txtSocialMediaMessage" CssClass="controlStyle" TextMode="MultiLine" runat="server" Height="100px" Style="padding-left: 10px; padding-right: 10px;" BackColor="#E6E6E6" Font-Names="myraid pro" Font-Size="11pt" ForeColor="#a5a6a5"></asp:TextBox>
            </div>

            <%------------------------------------- Starting SocialMedia Message Div (Message) ------------------------------------%>
        </div>


        <%------------------------------------- Ending Parent Div (Contact) ------------------------------------%>
    </div>

</div>
