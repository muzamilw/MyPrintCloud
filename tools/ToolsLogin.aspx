<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ToolsLogin.aspx.cs" Inherits="Web2Print.UI.tools.ToolsLogin" MasterPageFile="~/MasterPages/ThemeSite.Master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainContent">
    <div class="content_area container">
        <div class="left_right_padding">
            <div class="signin_heading_div">
                <asp:Label ID="lblSignIN" runat="server" Text="Sign in to access secure pages" CssClass="sign_in_heading"></asp:Label>
            </div>

            <div class="white-container-lightgrey-border  rounded_corners">
                <div class="cntMainTools">

                    <div class="custom_lin_height mrginBtm indentLvL2 Fsize14">
                        <div class="cntSignInWidth float_left_simple">
                        <asp:Label ID="ltrlEmailAddress" runat="server" Text="User name"></asp:Label></div>
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="text_box rounded_corners5 txtPassBox float_left_simple"></asp:TextBox>
                    </div>
                    <div class="clearBoth">
                        &nbsp;
                    </div>
                    <div class="custom_lin_height indentLvL2 Fsize14">
                        <div class="cntSignInWidth float_left_simple">
                        <asp:Label ID="lblPassword" runat="server" Text="Password" CssClass="spanPassword"></asp:Label></div>

                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="float_left_simple text_box rounded_corners5 txtPassBox"></asp:TextBox>
                    </div>
                     <div class="clearBoth">
                        &nbsp;
                    </div>
                    <asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click"
                        Text="Login" CssClass="start_creating_btn rounded_corners5 ToolsLoginBtn" />
                    <div class="clearBoth">
                        &nbsp;
                    </div>
                    <div class="cntToolsErrorMesg" style="">

                        <asp:Label ID="lblErrorMesg" runat="server" Style="color: red; font-size: 13px;" Visible="false"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <br />

    </div>


</asp:Content>


