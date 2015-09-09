<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Register Assembly="GrapeCity.ActiveReports.Web.v7, Version=7.1.7470.0, Culture=neutral, PublicKeyToken=cc4967777c49a3ff"
    Namespace="GrapeCity.ActiveReports.Web" TagPrefix="ActiveReportsWeb" %>
<head id="Head1" runat="server">

    <link rel="stylesheet" type="text/css" href="../../Content/ReportViewerSite.css" />
</head>
<script runat="server">
    void Page_Load()
    {
        ARWebViewer.Report = ViewBag.Report;
        
    }

   void printReport()
    {
       
    }
</script>
<body>
     <%-- <input type="button" id="printReport" value="Print Report" style="width: 100px;height: 30px;color: white;" onclick="window.print()" />
    <div id="myDiv" style="margin-left:60px;">--%>
         <ActiveReportsWeb:WebViewer ID="ARWebViewer" runat="server"  Height="700px" Width="900px"   ViewerType="AcrobatReader">
    </ActiveReportsWeb:WebViewer>
    <%--</div>--%>
   
</body>



