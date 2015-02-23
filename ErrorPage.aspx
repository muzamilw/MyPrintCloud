<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ThemeSite.Master" AutoEventWireup="true"
    CodeBehind="ErrorPage.aspx.cs" Inherits="Web2Print.UI.ErrorPage" %>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <div class="content_area">
        <div class="left_right_padding">
            <center>
                <h1>
                    <asp:Label ID="lblErrorMessage" runat="server"></asp:Label></h1>
            </center>
        </div>
    </div>
</asp:Content>
