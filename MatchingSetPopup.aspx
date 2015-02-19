<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MatchingSetPopup.aspx.cs"
    Inherits="Web2Print.UI.MatchingSetPopup" %>

<%@ Register Src="Controls/MatchingSet.ascx" TagName="MatchingSet" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Matching Sets</title>
</head>
<body>
    <form id="form1" runat="server">
         <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="jquery-migrate" />

                <asp:ScriptReference Name="WebUIValidation.js" Assembly="System.Web" />
            </Scripts>
        </asp:ScriptManager>
    <div style="background-color: White; min-height: 493px; max-width: 780px; padding: 5px;
        padding-top: 7px;">
        <uc1:MatchingSet ID="MS" runat="server" />
    </div>
    </form>
</body>
</html>
