<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/ThemeSite.Master" CodeBehind="successfull.aspx.cs" Inherits="Web2Print.UI.successfull" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContents" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageBanner" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <h2 class="title">Provide your additional information</h2>
   <div id="divadditioninfo" runat="server" class="rounded_corners yahooProvider">
   <div   style=" margin-left:30px; height:auto;">
   <div id="flcDiv" runat="server" visible="false">
   <asp:Label ID="lblfname" runat="server" Text="First Name"></asp:Label> <br />
       <asp:TextBox ID="txtFname" runat="server" CssClass="text_box200 rounded_corners5" ></asp:TextBox>
       <br />
        <asp:Label ID="lblLastname" runat="server" Text="Last Name"></asp:Label> <br />
       <asp:TextBox ID="txtLname" runat="server" CssClass="text_box200 rounded_corners5" ></asp:TextBox>
        <br />
        <asp:Label ID="lblCountry" runat="server" Text="Country" ></asp:Label> <br />
        <asp:TextBox ID="txtCountry" runat="server" CssClass="text_box200 rounded_corners5" ></asp:TextBox>
   </div>
       <div id="emaildiv" runat="server" visible="false">
       
      <asp:Label ID="lblEmail" runat="server" Text="*Email"></asp:Label> <br />
       <asp:TextBox ID="txtEmail" runat="server" CssClass="text_box200 rounded_corners5"  ></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvEmailID" runat="server" ControlToValidate="txtEmail"
                                ErrorMessage="*Required." Display="Dynamic" CssClass="" ValidationGroup="Vgsubmit"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ValidationGroup="Vgsubmit" ID="revEmail" runat="server"
                                Display="Dynamic" ValidationExpression="^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$"
                                ControlToValidate="txtEmail" ForeColor="Red" CssClass="" ErrorMessage=" Enter valid email"></asp:RegularExpressionValidator>
        
       </div>

       <div id="Companydiv" runat="server" visible="false">
        
        <asp:Label ID="lblCompany" runat="server" Text="Company Name" ></asp:Label> <br />
        <asp:TextBox ID="txtCompany" runat="server"  CssClass="text_box200 rounded_corners5"  ></asp:TextBox>
       </div> 
        
        Phone<br />
        <asp:TextBox ID="txtPhone" runat="server"  CssClass="text_box200 rounded_corners5" ></asp:TextBox>
        <br />
        <asp:CheckBox ID="chkNewsLettersSubscription" runat="server" Text="Yes, Send me Newsletters" /><br /><br />
        <asp:Button ID="btnSubmit" runat="server"  ValidationGroup="Vgsubmit"   CssClass="start_creating_btn rounded_corners5" Text="Submit"
           onclick="btnSubmit_Click" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnSkip" runat="server" onclick="btnSkip_Click" CssClass="start_creating_btn rounded_corners5" Text="Skip" />

    <asp:Label ID="lblMessage" runat="server" Visible="false" ></asp:Label>
    </div>
   </div>
</asp:Content>