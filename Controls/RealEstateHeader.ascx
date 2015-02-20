<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RealEstateHeader.ascx.cs"
    Inherits="Web2Print.UI.Controls.RealEstateHeader" %>

<div class="content_area realEstateHeader">
    <link href="../Styles/RealEstate_StyleSheet.css" rel="stylesheet" />
    <script src="jquery-2.1.3.js"></script>
    <script>
        $(document).ready(function () {
            $(".DropDown").hide();
            $(".HeaderBarLeft-ChildDiv").hover(function () {
                $(".DropDown").show();
            });
            $(".HeaderBarLeft-ChildDiv").mouseout(function () {
                $(".DropDown").hide();
            });
            $(".DropDown").mouseover(function () {
                $(".DropDown").show();
            });
            $(".DropDown").mouseout(function () {
                $(".DropDown").hide();
            });
            $("#hoverPopup").hover(function () {
                $(".DropDown").show();
            });
            $("#hoverPopup").mouseout(function () {
                $(".DropDown").show();
            });
        });
    </script>

    <%------------------------------------- Starting Parent Div (Contact) ------------------------------------%>
    <div class="Headaer-Parent-Div">
        <div class="HeaderBarLeft-Div">
            <div class="HeaderBarLeft-ChildDiv">
                <label id="hoverPopup" style="font-family: Arial; font-size: 18px; color: black;">Profile</label>
            </div>
            <div class="DropDown">
                <div style="height: 125px;">
                    <div class="UserImage" style="float: left; margin: 20px 0px 20px 20px;">
                        <asp:Image ID="imgProfileImage" runat="server" src="../Styles/images/ProfileImage.png" width="80" height="90" />
                    </div>
                    <div class="UserName" style="margin-top: 20px;">
                        <table style="height: 90px; width: 180px;">
                            <tr>
                                <td>
                                    <asp:Label ID="lblLogginAs" runat="server" Font-Bold="False" Font-Names="arial" Font-Size="15px" ForeColor="#666666" Text="Logged in as"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblUsername" runat="server" Font-Names="arial" Font-Size="20px" ForeColor="#333333" Text="John Simth"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div>
                    <hr />
                </div>

                <div>
                    <table style="height: 30px;">
                        <tr>
                            <td style="padding-left: 20px;">
                                <img src="../Styles/images/Settings.png" width="30" height="30" />
                            </td>
                            <td style="padding-left: 20px;">

                                <asp:HyperLink ID="lnkSettings" runat="server" Font-Names="arial" Font-Size="15px" ForeColor="#666666" NavigateUrl="~/DashBoard.aspx">Settings</asp:HyperLink>

                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <hr />
                </div>

                <div>
                    <table style="height: 30px;">
                        <tr>
                            <td style="padding-left: 20px;">
                                <img src="../Styles/images/my-jobs.png" width="30" height="30" />
                            </td>
                            <td style="padding-left: 20px;">
                                <%--<asp:HyperLink ID="" runat="server" Font-Names="arial" Font-Size="15px" ForeColor="#666666" NavigateUrl="~/DashBoard.aspx">Jobs</asp:HyperLink>--%>
                                <asp:Label ID="lblJobs" runat="server" Font-Bold="False" Font-Names="arial" Font-Size="15px" ForeColor="#666666" Text="Jobs"></asp:Label>

                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <hr />
                </div>
                <div>
                    <table style="height: 30px;">
                        <tr>
                            <td style="padding-left: 20px;">
                                <img src="../Styles/images/logout.png" width="30" height="30" />
                            </td>
                            <td style="padding-left: 20px;">

                                <asp:LinkButton ID="btnLogout" runat="server" Font-Names="arial" Font-Size="15px" ForeColor="#666666" OnClick="btnLogout_Click">Logout</asp:LinkButton>

                            </td>
                        </tr>
                    </table>
                    <br />
                </div>

            </div>

        </div>

        <div class="HeaderBarLogo-Div">
            <img src="../Styles/images/RealEstateLogo.png" />
        </div>
        <div class="HeaderBarRight-Div" align="right">
            <%--<div class="HeaderBarRight-ChildDiv">
                <asp:TextBox ID="txtSearch" runat="server" Font-Names="myraid pro" Width="200px" Height="20px" placeHolder="Search" BackColor="#F8CABA" ForeColor="black" Style="padding-left: 10px;"></asp:TextBox>
            </div>--%>
        </div>
        <%------------------------------------- Ending Parent Div (Contact) ------------------------------------%>
    </div>
    <div class="HeaderBarRight-ChildDiv">
        <asp:TextBox ID="txtSearch" runat="server" Font-Names="myraid pro" Width="280px" Height="20px" placeHolder="Search" BackColor="#F8CABA" ForeColor="black" Style="padding-left: 10px;"></asp:TextBox>
    </div>
</div>
