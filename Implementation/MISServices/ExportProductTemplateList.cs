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
            string sFileJpg = txtImagePath.Text;
            string sFilePng = txtImagePath.Text.Replace(".jpg", ".png");

            Image updatedImage = null;
            if (File.Exists(sFileJpg))
            {
                updatedImage = Image.FromFile(sFileJpg);
            }
            else if (File.Exists(sFilePng))
            {
                updatedImage = Image.FromFile(sFilePng);
            }
            
            int sourceWidth = updatedImage != null ? updatedImage.Width : 1;
            int sourceHeight = updatedImage != null? updatedImage.Height : 1;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = (Picture1.Width / sourceWidth);
            nPercentH = (Picture1.Height / sourceHeight);
            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = System.Convert.ToInt16((Picture1.Width - (sourceWidth * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentW;
                destY = System.Convert.ToInt16((Picture1.Height - (sourceHeight * nPercent)) / 2);
            }

            float destWidth = (sourceWidth * nPercent);
            float destHeight = (sourceHeight * nPercent);
            Picture1.Width = destWidth;
            Picture1.Height = destHeight;


            if (File.Exists(sFileJpg))
            {
                Picture1.Image = Image.FromFile(sFileJpg);
            }
            else if (File.Exists(sFilePng))
            {
                Picture1.Image = Image.FromFile(sFilePng);
            }
            else
            {
                Picture1.Image = null;
            }
                
           // Picture1.Image = new System.Drawing.Bitmap(txtImagePath.Text);
        }

    }
}
