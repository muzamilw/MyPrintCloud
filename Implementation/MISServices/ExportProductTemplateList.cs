using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Web.Configuration;
using System.Web;
using System.IO;
using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.SectionReportModel;


namespace MPC.Web.Reports
{
    /// <summary>
    /// Summary description for EstimateReportCostCenters.
    /// </summary>
    public partial class ExportProductTemplateList : GrapeCity.ActiveReports.SectionReport
    {
        
        public ExportProductTemplateList()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            this.detail.Format += new EventHandler(detail_Format);
        }

        private void detail_Format(object sender, EventArgs e)
        {
            if (File.Exists(txtImagePath.Text))
            {
                Picture1.Image = Image.FromFile(txtImagePath.Text);
            }
            else
            {
                Picture1.Image = null;
            }
                
           // Picture1.Image = new System.Drawing.Bitmap(txtImagePath.Text);
        }

        


    }
}
