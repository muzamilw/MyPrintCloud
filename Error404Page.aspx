<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error404Page.aspx.cs" Inherits="Web2Print.UI.Error404Page"
    MasterPageFile="~/MasterPages/ThemeSite.Master" %>
    <%@ Register Src="Controls/AllProducts.ascx" TagName="AllProducts" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainContent">
    <style>
        BODY
        {
            background-color: #d4d4d4 !important;
        }
        
        
    </style>

    <div class="content_area">
        <div id="divnoresourcefound" runat="server" class="Text404">
        No Resource Found - 404
       
    </div>
    <img alt="404 - Resource not found" src="images/error404_main.jpg" class="Error404Img" />
    </div>

        <div class="content_area">
        <div class="left_right_padding">
            <uc1:AllProducts ID="AllProducts12" runat="server" />
        </div>
    </div>
</asp:Content>
