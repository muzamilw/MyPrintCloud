<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/EmptyTheme.Master" AutoEventWireup="true" CodeBehind="SpriteEditor.aspx.cs" Inherits="Web2Print.UI.tools.SpriteEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContents" runat="server">
    <link href="MIS.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

 <div id="header">
       
        <asp:Button ID="btnRestore" CssClass="BtnAlignRight rounded_corners5" runat="server"
            Text="Restore to Original Version" OnClientClick="return window.confirm('Are you sure to restore the sprite image?');" OnClick="btnRestore_Click" />
    </div>
   
    <div id="content">
      <div class="lblMessage">
            <asp:Label ID="Label1" runat="server" EnableViewState="False"></asp:Label>
        </div>
        <div class="paddedContent boxsizingBorder">



           <div class="marginSmall floatLeft">Upload New Sprite Image</div>
           <asp:FileUpload ID="FileUpload1" runat="server" CssClass="marginSmall floatLeft" />
            <asp:Button ID="btnSave" CssClass="marginSmall floatLeft rounded_corners5" runat="server"
            Text="Upload" onclick="btnSave_Click" />
            
            <div class="clear"></div>
            <div class="marginSmall floatLeft">Current Sprite Image (sprint.png)</div>
            <div class="clear"></div>
            <asp:Image runat="server" ID="imgSprite" BorderWidth="1px" />


         

        </div>
        <script>
            function checkForChanges() {

                return true;
            }
    </script>
    </div>
</asp:Content>
