<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeeklyReport.aspx.cs" Inherits="Web2Print.UI.WeeklyReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="WeeklyReportForm" runat="server">
    <div id="page" style="width: 700px; margin: auto; margin-top: 10px; margin-bottom: 10px;
        padding: 2px 20px 2px 20px; background-color: White; height: 750px;">
        <div style="padding-top: 30px; border: 2px solid #cccccc; height: 700px; padding-right: 10px;
            -moz-border-radius: 10px; -webkit-border-radius: 10px; -khtml-border-radius: 10px;
            border-radius: 10px;">
            <div id="btnLogoContainerDiv">
                <div id="logoDiv">
                    <asp:Image ID="imgLogoPrintManagment" runat="server" Style="float: left; height: 70px;
                        width: 270px;" BorderStyle="None"/>
                    
                </div>
                <div style="float: right;">
                <asp:HyperLink ID="hyperlinkimgelogin" runat="server" Style="background-repeat: repeat-x; height: 30px;
                         cursor: pointer; margin-top: 40px; -moz-border-radius: 5px;
                        -webkit-border-radius: 5px; -khtml-border-radius: 5px; border-radius: 5px; border-width: 0px;" >
                        </asp:HyperLink>
                </div>
            </div>
            <div style="clear: both; height: 0px;">
                &nbsp;
            </div>
            <div id="orangeDiv" style="background-color: Orange; width: 697px; height: 5px; border-style: none;
                margin-top: 20px;">
                &nbsp;
            </div>
            <div id="txtContainerDiv">
                <div id="msgDiv" style="text-align: left; padding-left: 25px; padding-top: 10px;">
                    <asp:Label ID="lblHi" runat="server" Style="font-weight: bold; font-size: medium;">Hi</asp:Label>
                    <asp:Label ID="lblName" runat="server" Style="font-weight: bold; font-size: medium;">mpc2,</asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="lblmsg" runat="server">Here is this week's summery of your bussiness:</asp:Label>
                    <asp:Label ID="lblStrtDate" runat="server">StrtDate -</asp:Label>
                    <asp:Label ID="lblEndDate" runat="server">EndDate,</asp:Label>
                    <asp:Label ID="lblYear" runat="server">year</asp:Label>
                </div>
                <br />
                <div id="horizontalSepImgDiv">
                    <asp:Image ID="horiznSepImge" runat="server" Style="background-repeat: repeat-x;
                        width: 660px; margin-left: 25px; margin-top: 10px;" />
                </div>
                <div id="leftDivPanel" style="background-color: #DBF0FF; width: 300px; margin-left: 25px;
                    text-align: center; float: left; -moz-border-radius: 10px; -webkit-border-radius: 10px;
                    -khtml-border-radius: 10px; border-radius: 10px;">
                    <div style="height: 25px; text-align: left; font-weight: bold; font-size: small;
                        padding-top: 10px; padding-left: 10px;">
                        New Orders Last Week:
                    </div>
                    <div>
                        <asp:Image ID="blueSepImageDiv" runat="server" Style="background-repeat: repeat-x;
                            width: 300px; height: 5px;" />
                    </div>
                    <div style="height: 25px; text-align: left; font-weight: bold; font-size: small;
                        padding-top: 10px; padding-left: 10px;">
                        <asp:Label ID="lblNewOrders" runat="server"></asp:Label>
                    </div>
                </div>
                <div id="rightDivPanel" style="background-color: #DBF0FF; width: 300px; margin-left: 25px;
                    text-align: center; float: right; -moz-border-radius: 10px; -webkit-border-radius: 10px;
                    -khtml-border-radius: 10px; border-radius: 10px;">
                    <div style="height: 25px; text-align: left; font-weight: bold; font-size: small;
                        padding-top: 10px; padding-left: 10px;">
                        Total value of Jobs in Production
                    </div>
                    <div>
                        <asp:Image ID="blueSeprtrImgDiv" runat="server" Style="background-repeat: repeat-x;
                            width: 300px; height: 5px;" />
                    </div>
                    <div style="height: 25px; text-align: left; font-weight: bold; font-size: small;
                        padding-top: 10px; padding-left: 10px;">
                        <asp:Label ID="lblJobProductn" runat="server"></asp:Label>
                    </div>
                </div>
                <div style="clear: both; height: 0px;">
                    &nbsp;
                </div>
                <div style="float: left;">
                    <asp:Image ID="shapedImg" runat="server" Style="width: 220px; height: 35px; margin-left: 25px;
                        margin-top: 25px;" />
                    <div style="margin-left: 30px; text-align: left; font-weight: bold; font-size: small;">
                        <asp:Label ID="lblInvoiced" runat="server"></asp:Label>
                    </div>
                </div>
                <div style="float: left; margin-top: 20px; margin-left: 10px;">
                    <div style="text-align: left; font-weight: bold; font-size: small; padding-top: 10px;
                        padding-left: 10px; margin-bottom: 3px;">
                        Being putting off your invoicing?
                        <br />
                        Invoice your orders/ jobs now before you deliver.
                    </div>
                    <div>
                        <asp:HyperLink ID="hyperlinkbtnSeeOrderImg" runat="server" Style="background-repeat: repeat-x; height: 30px; float: left;
                         cursor: pointer; margin-top: 10px; border-width: 0px;">
                        </asp:HyperLink>
                    </div>
                </div>
                <div style="clear: both; height: 0px;">
                    &nbsp;
                </div>
                <div style="float: left;">
                    <asp:Image ID="shapedImg2" runat="server" Style="width: 220px; height: 35px; margin-left: 25px;
                        margin-top: 25px;" />
                    <div style="margin-left: 30px; text-align: left; font-weight: bold; font-size: small;">
                        <asp:Label ID="lblEstimation" runat="server"></asp:Label>
                    </div>
                </div>
                <div style="float: left; margin-top: 20px; margin-left: 10px;">
                    <div style="text-align: left; font-weight: bold; font-size: small; padding-top: 10px;
                        padding-left: 10px; margin-bottom: 3px;">
                        Did you remember to call back perspective work?
                        <br />
                        Review all your estimates and close that deal.
                    </div>
                    <div>
                     <asp:HyperLink ID="hyperlinkbtnEstimateImg" runat="server" Style="background-repeat: repeat-x; height: 30px; float: left;
                         cursor: pointer; margin-top: 10px; border-width: 0px;" >
                     </asp:HyperLink>
                    </div>
                </div>
                <div style="clear: both; height: 0px;">
                    &nbsp;
                </div>
                <div style="float: left;">
                    <asp:Image ID="shapedImg3" runat="server" Style="width: 220px; height: 35px; margin-left: 25px;
                        margin-top: 25px;" />
                    <div style="margin-left: 30px; text-align: left; font-weight: bold; font-size: small;">
                        <asp:Label ID="lblRegUser" runat="server"></asp:Label>
                    </div>
                </div>
                <div style="float: left; margin-top: 20px; margin-left: 10px;">
                    <div style="text-align: left; font-weight: bold; font-size: small; padding-top: 10px;
                        padding-left: 10px; margin-bottom: 3px;">
                        Remember to contact your new visitors and shoppers
                        <br />
                        from your retail store front.
                        <br />
                        See all registered users.Why not create an email campaign?
                    </div>
                    <div>
                        <asp:HyperLink ID="hyperLinkbtnServiceImg" runat="server" Style="background-repeat: repeat-x; height: 30px; float: left;
                         cursor: pointer; margin-top: 10px; border-width: 0px;" >
                        </asp:HyperLink>
                    </div>
                </div>
                <div style="clear: both; height: 0px;">
                    &nbsp;
                </div>
                <div style="background-color: #fff5d2; border: 2px solid #f4e0af; margin-top: 25px;
                    margin-left: 30px; width: 650px; height: 30px; text-align: center; font-size: small;
                    font-weight: bold; padding-top: 15px; -moz-border-radius: 10px; -webkit-border-radius: 10px;
                    -khtml-border-radius: 10px; border-radius: 10px;">
                    Want to see more reports like this? Log into your account and <a href="#" id="ReportTab"
                        runat="server" style="color: Blue;">Visits the Reports tab</a>
                </div>
            </div>
        </div>
        <div>
            <asp:Image ID="ReportShadowImgDiv" runat="server" Style="background-repeat: repeat-x;
                width: 640px; height: 25px; margin-left: 25px;" />
        </div>
    </div>
    </form>
</body>
</html>
