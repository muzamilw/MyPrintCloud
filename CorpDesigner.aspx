<%@ Page Title="" Language="C#"
    AutoEventWireup="true" CodeBehind="CorpDesigner.aspx.cs" Inherits="Web2Print.UI.CorpDesigner" %>

<%@ Register Src="~/Controls/BreadCrumbMenu.ascx" TagName="BreadCrumbMenu" TagPrefix="uc1" %>
<%@ Register Src="~/Controls/OrderStep.ascx" TagName="OrderStep" TagPrefix="uc2" %>
<%@ Register Src="~/Controls/MainHeading.ascx" TagName="MainHeading" TagPrefix="uc3" %>
<%@ Register Src="Controls/TopHeader.ascx" TagName="TopHeader" TagPrefix="uc7" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Template Designer</title>
    <style type="text/css">
        html, body {
            height: 100%;
            overflow: auto;
        }

        body {
            height: 100%;
            padding: 0;
            margin: 0;
        }
    </style>
</head>
<body class="designerBody">
    <form runat="server" id="formCorpDesgner">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="jquery-migrate" />

                <asp:ScriptReference Name="WebUIValidation.js" Assembly="System.Web" />
            </Scripts>
        </asp:ScriptManager>



        <script type="text/javascript">

            var TemplateID = 0;
            var ItemID = 0;
            var CustomerID = 0;
            var ContactID = 0;

            IsCoorporate = 1;

            function gotoLandingPage(PagePath) {

                window.location.href = PagePath;
            }


            function SaveAttachments() {

                $.get("Services/Webstore.svc/data", { templateID: TemplateID, itemID: ItemID, customerID: CustomerID, DesignName: document.getElementById('txtDesignName').value },
                    function (data) {
                        window.location.href = data;
                    });
            }


            function Next() {

                //access the iframe and fire its function.
                designerFrame.save("continue");

                return false;
            }

            //        function ShowPromptBox() {
            //            if (IsDesignModified) {
            //                return confirm("You have unsaved changes. Do you want to leave without saving changes ?");
            //            }
            //        }


            function checkForChanges() {
                var flag = document.getElementById('designerFrame').contentWindow.IsDesignModified;

                if (flag) {
                    return confirm("Template has been modified. Click on \"Save and Preview\" button to save. Do you want to continue without saving? ");
                }
                else
                    return true;
            }

        </script>
    </form>
    <iframe runat="server" id="designerFrame" class="designerFrame" src=""></iframe>
</body>
</html>
