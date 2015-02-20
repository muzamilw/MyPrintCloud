<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/MasterPages/ThemeSiteNew.master"
    AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Web2Print.UI._Default"
    MaintainScrollPositionOnPostback="true" ValidateRequest="false" EnableEventValidation="false" %>
  

<asp:Content ID="cMainContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:PlaceHolder runat="server" ID="pcControlsContainer"></asp:PlaceHolder>
    <asp:HiddenField ID="hfWebStorePanel" runat="server" />
    <asp:HiddenField ID="HfHome" runat="server" />
  
</asp:Content>

