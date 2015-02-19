<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ThemeSite.Master" AutoEventWireup="true"
    CodeBehind="NewsLettersSubscriberPanel.aspx.cs" Inherits="Web2Print.UI.NewsLettersSubscriberPanel" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Controls/BreadCrumbMenu.ascx" TagName="BreadCrumbMenu" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContents" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="MainContent" runat="server">
<div class="content_area container" style="margin-bottom: 40px">
    <div class="left_right_padding row">
           <div class="signin_heading_div float_left_simple dashboard_heading_signin">
                <asp:Label ID="NUManager" runat="server" Text="Subscribers Manager" CssClass="sign_in_heading"></asp:Label>
            </div>
            <div class="dashBoardRetrunLink">
               <uc1:breadcrumbmenu id="BreadCumbMenuCategory" runat="server" workmode="MyAccount"
            myaccountcurrentpage="Store Users" myaccountcurrentpageurl="UserManager.aspx" />
                     </div>
            <div class="clearBoth">

            </div>
        <div class="Width100Percent">
        <div class="divSearchBar paddingBottom10px normalTextStyle rounded_corners">
            
            <table style="width: auto; border-collapse:collapse; color:White; text-align:left" cellpadding="5" cellspacing="5">
                <tr>
                    <td>
                        <div style="float: left; padding: 23px 23px 23px 23px;" class="heading_h8">
                            <asp:Literal ID="ltrlSearchrecords" runat="server" Text=" Search Records"></asp:Literal>
                        </div>
                    </td>
                    <td>
                        <asp:TextBox ID="txtsearch" runat="server" CssClass="text_box300 rounded_corners5"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnsearch" runat="server" Text="Go" CssClass="start_creating_btn rounded_corners5"
                            OnClick="btnsearch_Click" style="width:90px; margin-left:20px; margin-right:10px;" />
                    </td>
                    <td>
                        <asp:Button ID="btnReset" runat="server" Text="Reset" style="width:90px;" CssClass="start_creating_btn rounded_corners5"
                            OnClick="btnReset_Click" />
                    </td>
                </tr>
            </table>
        </div>
        </div>
        
        <div class="white_background  rounded_corners">
            <div class="pad10">
                <asp:Label ID="lblmatchingrecord" runat="server" CssClass="matchingTxtclass" />
            </div>
            <div class="clearBoth">
                &nbsp;</div>
            <div class="ListHeaderBG">
                <div class="checked_design" style="width: 300px; float: left; text-align: left; vertical-align: middle;
                    padding: 0px;">
                    <asp:Literal ID="ltrlname" runat="server" Text="Name"></asp:Literal>
                </div>
                <div class="checked_design" style="width: 300px; float: left; text-align: left; vertical-align: middle;
                    padding: 0px;">
                    <asp:Literal ID="ltremail" runat="server" Text="Email"></asp:Literal>
                </div>
                <div class="checked_design" style="width: 160px; float: left; text-align: left; vertical-align: middle;
                    padding: 0px;">
                    <asp:Literal ID="ltrlUserRoles" runat="server" Text="Subscription Date"></asp:Literal>
                </div>
                <div class="clearBoth">
                    &nbsp;
                </div>
            </div>
            <asp:UpdatePanel ID="upnUser" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <asp:Repeater ID="rptrSubscribers" runat="server" OnItemDataBound="rptrSubscribers_ItemDataBound"
                        OnItemCommand="rptrSubscribers_ItemCommand" 
                        onitemcreated="rptrSubscribers_ItemCreated" >
                        
                        <ItemTemplate>
                        

                            <div class="clsdivRepeaterItem" id="divRepeaterItem" runat="server">
                               
                                <div id="divContainer" runat="server" class="cursor_pointer">
                                     <div class="user_email_div checked_design" style="width:300px">
                                            <asp:Label ID="lblname" runat="server" Width="300px"></asp:Label>&nbsp;
                                        </div>
                                        <div class="user_email_div checked_design" style="width:300px">
                                            <asp:Label ID="lblemail" runat="server" Text='<%# Eval("Email")%>' Width="300px"></asp:Label>&nbsp;
                                        </div>
                                        <div class="user_phone_div_card checked_design" style="width:160px">
                                            <asp:Label ID="lblSubScriptionDate" runat="server"></asp:Label>&nbsp;
                                        </div>
                                </div>
                                <%--<div class="float_right" style="padding: 0px 0px 0px 0px;">
                                    <asp:ImageButton ID="btnDelete" runat="server" OnClientClick="return confirm('Are you sure you want to delete this user?');"
                                        CssClass="rounded_corners" ImageUrl="~/images/delete.png" Height="28" Width="28"
                                        ToolTip="Delete User" CommandName="DeleteRecord" CommandArgument='<%# Eval("SubscriberID") %>' />
                                </div>--%>
                            </div>
                            <div class="clearBoth">
                                &nbsp;
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <div class="clearBoth">
                        &nbsp;</div>
                    
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</div>

</asp:Content>
