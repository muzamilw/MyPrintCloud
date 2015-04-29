<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Register Assembly="GrapeCity.ActiveReports.Web.v7, Version=7.1.7470.0, Culture=neutral, PublicKeyToken=cc4967777c49a3ff"
    Namespace="GrapeCity.ActiveReports.Web" TagPrefix="ActiveReportsWeb" %>
<head id="Head1" runat="server">
    <title>ReportViewer</title>

    <link rel="stylesheet" type="text/css" href="../../Content/Site.css"/>
</head>
<script runat="server">
    void Page_Load()
    {
        ARWebViewer.Report = ViewBag.Report;
        
    }

   
</script>
<body>
<hr />
  <ActiveReportsWeb:WebViewer ID="ARWebViewer" runat="server" style="margin-left:50px;margin-top:50px" Height="700px" Width="900px">
       </ActiveReportsWeb:WebViewer>
</body>