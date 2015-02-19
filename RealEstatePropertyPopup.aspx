<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RealEstatePropertyPopup.aspx.cs" Inherits="Web2Print.UI.RealEstatePropertyPopup" %>
<%@ Register Src="Controls/RealEstateItems.ascx" TagName="RealEstateItems" TagPrefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <base target="_parent" />
</head>
<body class="bodyResetPassword" >
    <form id="form1" runat="server" style="width:95%;">
<%--        <div class="left_align FileUploadHeaderText_PopUp float_left_simple MesgBoxClass">
            Select Property
        </div>--%>
    <div id="CancelControl" onclick="window.parent.closeMS(); return false;" class="MesgBoxBtnsDisplay rounded_corners5" style="margin-right: -35px;">
        Close
    </div>
         <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="jquery-migrate" />

                <asp:ScriptReference Name="WebUIValidation.js" Assembly="System.Web" />
            </Scripts>
        </asp:ScriptManager>
   
        <uc1:RealEstateItems ID="ctrlRealEstateItems" runat="server" />
    
    </form>
</body>
</html>
