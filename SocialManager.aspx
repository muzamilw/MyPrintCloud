<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ThemeSite.Master" AutoEventWireup="true" CodeBehind="SocialManager.aspx.cs" Inherits="Web2Print.UI.SocialManager" %>

<%@ Register Src="~/Controls/BreadCrumbMenu.ascx" TagName="BreadCrumbMenu" TagPrefix="uc2" %>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content_area container">
        <div class="left_right_padding row">
            <div id="MessgeToDisply" class="rounded_corners" runat="server" visible="false">
                <asp:Literal ID="ltrlMessge" runat="server"></asp:Literal>
            </div>
             <div class="signin_heading_div float_left_simple dashboard_heading_signin">
                <asp:Label ID="lblTitle" runat="server" Text="Social Links" CssClass="sign_in_heading"></asp:Label>

            </div>
            <div class="dashBoardRetrunLink">
                <uc2:BreadCrumbMenu ID="BreadCumbMenuCategory" runat="server" WorkMode="MyAccount"
                                MyAccountCurrentPage="My Profile" MyAccountCurrentPageUrl="UserProfile.aspx" />
                     </div>
            <div class="clearBoth">

            </div>
            <div align="center" id="divUserProfile" runat="server">
                <div class="white-container-lightgrey-border rounded_corners">
                    <div class="pad20">
                        <div class="headingsAvenior">
                            <asp:Literal ID="lblHead" runat="server" Text="Add your app keys:"></asp:Literal>
                        </div>
                        <br />
                        <div class="cntSocialHalfRightProfile" style="float: left; /* width: 725px; */
text-align: left; padding-left: 60px; margin-top: 20px;">
                            <div class="smallContctUsAvenior float_left_simple" id="divfname" runat="server">
                                Facebook App Id
                            </div>
                            <div class="TTL widthAvenior">

                                <asp:TextBox ID="txtfbID" runat="server" CssClass="newTxtBox"
                                    TabIndex="1"></asp:TextBox>

                            </div>
                            <div style="float: left; color: #8888A8; font-family: 'Arial'; font-weight:bold; font-size: 12px; margin-top: 15px; margin-left: 9px; ">
                                <a href="https://developers.facebook.com/" target="_blank">Don't have app on Facebook? </a>
                                
                            </div>
                            <div class="clearBoth">
                            </div>
                            <br />
                            <div class="smallContctUsAvenior float_left_simple" id="divlname" runat="server">
                                Facebook App Secret
                            </div>
                            <div class="TTL widthAvenior">
                                <asp:TextBox ID="txtfbKey" runat="server" CssClass="newTxtBox"
                                    TabIndex="2"></asp:TextBox>
                            </div>
                            <div class="clearBoth">
                            </div>
                            <br />
                            <div class="smallContctUsAvenior float_left_simple" id="divemail" runat="server">
                                Twitter API key
                            </div>
                            <div class="TTL widthAvenior">
                                <asp:TextBox ID="txtTwitterID" runat="server" CssClass="newTxtBox"
                                    TabIndex="3"></asp:TextBox>
                            </div>
                             <div style="float: left; color: #8888A8; font-family: 'Arial'; font-weight:bold; font-size: 12px; margin-top: 15px; margin-left: 9px;">
                                <a href="https://dev.twitter.com/" target="_blank">Don't have app on Twitter? </a>
                                <br />
                                
                            </div>
                            <div class="clearBoth">
                            </div>
                            <br />
                            <div class="smallContctUsAvenior float_left_simple" id="divjobtitle" runat="server">
                                Twitter API Secret
                            </div>
                            <div class="TTL widthAvenior">
                                <asp:TextBox ID="txtTwitterKey" runat="server" CssClass="newTxtBox"
                                    TabIndex="4"></asp:TextBox>
                            </div>
                            <div class="clearBoth">
                            </div>
                            <br />
                            <div class="smallContctUsAvenior float_left_simple" id="div1" runat="server">
                            </div>
                            <div class="TTL widthAvenior">
                                <asp:Button ID="btnSave" runat="server" CssClass="start_creating_btn rounded_corners5"
                                    Text="Save" OnClick="btnSave_Click" TabIndex="13" Width="100px" />
                                &nbsp;&nbsp;
                            <asp:Button ID="btnCancel" runat="server" CssClass="start_creating_btn rounded_corners5"
                                Text="Cancel" PostBackUrl="~/DashBoard.aspx" TabIndex="15" Width="100px" />
                            </div>
                        </div>
                       
                        <div class="clearBoth">
                        </div>
                        <br />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
