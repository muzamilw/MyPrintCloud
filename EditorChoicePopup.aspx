﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditorChoicePopup.aspx.cs"
    Inherits="Web2Print.UI.EditorsChoicePopUp" %>

<%@ Register Src="Controls/HomePageSlider.ascx" TagName="MatchingSet" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Editor's Pick</title>
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
    <div >
        <uc1:MatchingSet ID="EC" runat="server" />
    </div>
    </form>
</body>
</html>
