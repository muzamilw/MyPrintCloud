using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;
using System.Web;
using System.IO;
using GrapeCity.ActiveReports;


namespace MPC.Web.Reports
{
    /// <summary>
    /// Summary description for EstimateReportCostCenters.
    /// </summary>
    public partial class EstimateReportCostCenters : GrapeCity.ActiveReports.SectionReport
    {
        private static SqlConnection oConn;
        private static SqlDataReader oReader;
        private FileStream fs = null;
        private int nEstimateID;
        private int iReportID;
        byte[] rptBytes = null;
        private DataTable dtSubReport = null;
        private SectionReport currSubReport = new SectionReport();
        public EstimateReportCostCenters()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            //nEstimateID = p_nParam;
            //iReportID = reportID;
        }

        private void EstimateReportCostCenters_ReportStart(object sender, EventArgs e)
        {

            try
            {


            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }


    }
}
