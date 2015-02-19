<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/EmptyTheme.Master" AutoEventWireup="true" CodeBehind="CssEditor.aspx.cs" Inherits="Web2Print.UI.tools.CssEditor" ValidateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContents" runat="server">
    <link href="MIS.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

  <div id="header">
        <asp:Button ID="btnSave" CssClass="BtnAlignRight rounded_corners5" runat="server"
            Text="Save" onclick="btnSave_Click" />
        <asp:Button ID="btnRestore" CssClass="BtnAlignRight rounded_corners5" runat="server"
            Text="Restore to Original Version" OnClientClick="return window.confirm('Are you sure to restore the CSS file to original version? any changes made will be lost.');" OnClick="btnRestore_Click" />
    </div>
   
    <div id="content">
      <div class="lblMessage">
            <asp:Label ID="Label1" runat="server" EnableViewState="false"></asp:Label>
        </div>
        <div class="paddedContent boxsizingBorder">
            <asp:TextBox TextMode="MultiLine" runat="server" ID="txtCss" CssClass="fullwidthTextBox"></asp:TextBox>

        </div>
        <script>
         function checkForChanges() {
            
                return true;
        }
    </script>
    </div>
</asp:Content>
