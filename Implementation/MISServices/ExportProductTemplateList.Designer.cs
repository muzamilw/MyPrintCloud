namespace MPC.Web.Reports
{
    /// <summary>
    /// Summary description for EstimateReportCostCenters.
    /// </summary>
    partial class ExportProductTemplateList
    {
        private GrapeCity.ActiveReports.SectionReportModel.PageHeader pageHeader;
        private GrapeCity.ActiveReports.SectionReportModel.Detail detail;
        private GrapeCity.ActiveReports.SectionReportModel.PageFooter pageFooter;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            base.Dispose(disposing);
        }

        #region ActiveReport Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportProductTemplateList));
            this.pageHeader = new GrapeCity.ActiveReports.SectionReportModel.PageHeader();
            this.Shape1 = new GrapeCity.ActiveReports.SectionReportModel.Shape();
            this.Label23 = new GrapeCity.ActiveReports.SectionReportModel.Label();
            this.Label1 = new GrapeCity.ActiveReports.SectionReportModel.Label();
            this.Label26 = new GrapeCity.ActiveReports.SectionReportModel.Label();
            this.Label27 = new GrapeCity.ActiveReports.SectionReportModel.Label();
            this.Label28 = new GrapeCity.ActiveReports.SectionReportModel.Label();
            this.label2 = new GrapeCity.ActiveReports.SectionReportModel.Label();
            this.detail = new GrapeCity.ActiveReports.SectionReportModel.Detail();
            this.txtOrderCode = new GrapeCity.ActiveReports.SectionReportModel.TextBox();
            this.txtOrderPlacedBy = new GrapeCity.ActiveReports.SectionReportModel.TextBox();
            this.txtImagePath = new GrapeCity.ActiveReports.SectionReportModel.TextBox();
            this.txtCompanyName = new GrapeCity.ActiveReports.SectionReportModel.TextBox();
            this.Picture1 = new GrapeCity.ActiveReports.SectionReportModel.Picture();
            this.TextBox4 = new GrapeCity.ActiveReports.SectionReportModel.TextBox();
            this.line1 = new GrapeCity.ActiveReports.SectionReportModel.Line();
            this.pageFooter = new GrapeCity.ActiveReports.SectionReportModel.PageFooter();
            ((System.ComponentModel.ISupportInitialize)(this.Label23)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label26)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label27)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label28)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrderCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrderPlacedBy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtImagePath)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompanyName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picture1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageHeader
            // 
            this.pageHeader.Controls.AddRange(new GrapeCity.ActiveReports.SectionReportModel.ARControl[] {
            this.Shape1,
            this.Label23,
            this.Label1,
            this.Label26,
            this.Label27,
            this.Label28,
            this.label2});
            this.pageHeader.Height = 0.70125F;
            this.pageHeader.Name = "pageHeader";
            // 
            // Shape1
            // 
            this.Shape1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.Shape1.Height = 0.25F;
            this.Shape1.Left = 2.220446E-16F;
            this.Shape1.Name = "Shape1";
            this.Shape1.RoundingRadius = 9.999999F;
            this.Shape1.Top = 0.4199999F;
            this.Shape1.Width = 10.812F;
            // 
            // Label23
            // 
            this.Label23.Height = 0.16F;
            this.Label23.HyperLink = null;
            this.Label23.Left = 0.01900046F;
            this.Label23.Name = "Label23";
            this.Label23.Style = "font-family: Arial; font-size: 8.25pt; font-weight: bold";
            this.Label23.Text = "Parent Category";
            this.Label23.Top = 0.465F;
            this.Label23.Width = 0.9799993F;
            // 
            // Label1
            // 
            this.Label1.Height = 0.3125F;
            this.Label1.HyperLink = null;
            this.Label1.Left = 3.437F;
            this.Label1.Name = "Label1";
            this.Label1.Style = "font-family: Tahoma; font-size: 14.25pt; font-weight: bold; text-decoration: none" +
    "";
            this.Label1.Text = "Product Templates List";
            this.Label1.Top = 0.041F;
            this.Label1.Width = 2.967F;
            // 
            // Label26
            // 
            this.Label26.Height = 0.13F;
            this.Label26.HyperLink = null;
            this.Label26.Left = 2.184F;
            this.Label26.Name = "Label26";
            this.Label26.Style = "font-family: Arial; font-size: 8.25pt; font-weight: bold; ddo-char-set: 0";
            this.Label26.Text = "Category";
            this.Label26.Top = 0.465F;
            this.Label26.Width = 1.179F;
            // 
            // Label27
            // 
            this.Label27.Height = 0.14F;
            this.Label27.HyperLink = null;
            this.Label27.Left = 4.214F;
            this.Label27.Name = "Label27";
            this.Label27.Style = "font-family: Arial; font-size: 8.25pt; font-weight: bold; ddo-char-set: 0";
            this.Label27.Text = "Product Code";
            this.Label27.Top = 0.455F;
            this.Label27.Width = 1.032F;
            // 
            // Label28
            // 
            this.Label28.Height = 0.14F;
            this.Label28.HyperLink = null;
            this.Label28.Left = 8.822001F;
            this.Label28.Name = "Label28";
            this.Label28.Style = "font-family: Arial; font-size: 8.25pt; font-weight: bold; text-align: center; ddo" +
    "-char-set: 0";
            this.Label28.Text = "Thumbnail";
            this.Label28.Top = 0.455F;
            this.Label28.Width = 1.563001F;
            // 
            // label2
            // 
            this.label2.Height = 0.14F;
            this.label2.HyperLink = null;
            this.label2.Left = 6.597F;
            this.label2.Name = "label2";
            this.label2.Style = "font-family: Arial; font-size: 8.25pt; font-weight: bold; ddo-char-set: 0";
            this.label2.Text = "Product Name";
            this.label2.Top = 0.455F;
            this.label2.Width = 1.070007F;
            // 
            // detail
            // 
            this.detail.Controls.AddRange(new GrapeCity.ActiveReports.SectionReportModel.ARControl[] {
            this.txtOrderCode,
            this.txtOrderPlacedBy,
            this.txtImagePath,
            this.txtCompanyName,
            this.Picture1,
            this.TextBox4,
            this.line1});
            this.detail.Height = 2.111112F;
            this.detail.Name = "detail";
            this.detail.Format += new System.EventHandler(this.detail_Format);
            // 
            // txtOrderCode
            // 
            this.txtOrderCode.DataField = "ParentCategory";
            this.txtOrderCode.Height = 0.392F;
            this.txtOrderCode.Left = 0.017F;
            this.txtOrderCode.Name = "txtOrderCode";
            this.txtOrderCode.Style = "font-size: 8.25pt; ddo-char-set: 0";
            this.txtOrderCode.Text = "parent category";
            this.txtOrderCode.Top = 0F;
            this.txtOrderCode.Width = 2.105F;
            // 
            // txtOrderPlacedBy
            // 
            this.txtOrderPlacedBy.DataField = "ProductCode";
            this.txtOrderPlacedBy.Height = 0.392F;
            this.txtOrderPlacedBy.Left = 4.214F;
            this.txtOrderPlacedBy.Name = "txtOrderPlacedBy";
            this.txtOrderPlacedBy.Style = "font-size: 8.25pt; ddo-char-set: 0";
            this.txtOrderPlacedBy.Text = "OrderPlaccedBy";
            this.txtOrderPlacedBy.Top = 0F;
            this.txtOrderPlacedBy.Width = 2.349F;
            // 
            // txtImagePath
            // 
            this.txtImagePath.DataField = "TemplatePath";
            this.txtImagePath.Height = 0.125F;
            this.txtImagePath.Left = 5.291F;
            this.txtImagePath.Name = "txtImagePath";
            this.txtImagePath.Style = "font-size: 8.25pt";
            this.txtImagePath.Text = "path";
            this.txtImagePath.Top = 0.5F;
            this.txtImagePath.Visible = false;
            this.txtImagePath.Width = 0.687F;
            // 
            // txtCompanyName
            // 
            this.txtCompanyName.DataField = "CategoryName";
            this.txtCompanyName.Height = 0.392F;
            this.txtCompanyName.Left = 2.16F;
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.Style = "font-size: 8.25pt; ddo-char-set: 0";
            this.txtCompanyName.Text = "CompanyName";
            this.txtCompanyName.Top = 0F;
            this.txtCompanyName.Width = 2.027F;
            // 
            // Picture1
            // 
            this.Picture1.Height = 2.047F;
            this.Picture1.HyperLink = null;
            this.Picture1.ImageData = ((System.IO.Stream)(resources.GetObject("Picture1.ImageData")));
            this.Picture1.Left = 8.892F;
            this.Picture1.Name = "Picture1";
            this.Picture1.SizeMode = GrapeCity.ActiveReports.SectionReportModel.SizeModes.Stretch;
            this.Picture1.Top = 0F;
            this.Picture1.Width = 1.889999F;
            // 
            // TextBox4
            // 
            this.TextBox4.DataField = "ProductName";
            this.TextBox4.Height = 0.392F;
            this.TextBox4.Left = 6.597F;
            this.TextBox4.Name = "TextBox4";
            this.TextBox4.Style = "font-size: 8.25pt; ddo-char-set: 0";
            this.TextBox4.Text = "OrderPlaccedBy";
            this.TextBox4.Top = 0F;
            this.TextBox4.Width = 2.259F;
            // 
            // line1
            // 
            this.line1.Height = 0F;
            this.line1.Left = 0.242F;
            this.line1.LineWeight = 1F;
            this.line1.Name = "line1";
            this.line1.Top = 2.067F;
            this.line1.Width = 9.875F;
            this.line1.X1 = 0.242F;
            this.line1.X2 = 10.117F;
            this.line1.Y1 = 2.067F;
            this.line1.Y2 = 2.067F;
            // 
            // pageFooter
            // 
            this.pageFooter.Height = 0F;
            this.pageFooter.Name = "pageFooter";
            // 
            // ExportProductTemplateList
            // 
            this.MasterReport = false;
            this.PageSettings.PaperHeight = 11F;
            this.PageSettings.PaperWidth = 8.5F;
            this.PrintWidth = 10.81142F;
            this.Sections.Add(this.pageHeader);
            this.Sections.Add(this.detail);
            this.Sections.Add(this.pageFooter);
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Arial; font-style: normal; text-decoration: none; font-weight: norma" +
            "l; font-size: 10pt; color: Black", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 16pt; font-weight: bold", "Heading1", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-family: Times New Roman; font-size: 14pt; font-weight: bold; font-style: ita" +
            "lic", "Heading2", "Normal"));
            this.StyleSheet.Add(new DDCssLib.StyleSheetRule("font-size: 13pt; font-weight: bold", "Heading3", "Normal"));
            ((System.ComponentModel.ISupportInitialize)(this.Label23)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label26)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label27)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Label28)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrderCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrderPlacedBy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtImagePath)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompanyName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picture1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TextBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private GrapeCity.ActiveReports.SectionReportModel.Label Label23;
        private GrapeCity.ActiveReports.SectionReportModel.Label Label1;
        private GrapeCity.ActiveReports.SectionReportModel.Label Label26;
        private GrapeCity.ActiveReports.SectionReportModel.Label Label27;
        private GrapeCity.ActiveReports.SectionReportModel.Label Label28;
        private GrapeCity.ActiveReports.SectionReportModel.Label label2;
        private GrapeCity.ActiveReports.SectionReportModel.TextBox txtOrderCode;
        private GrapeCity.ActiveReports.SectionReportModel.TextBox txtOrderPlacedBy;
        private GrapeCity.ActiveReports.SectionReportModel.TextBox txtImagePath;
        private GrapeCity.ActiveReports.SectionReportModel.TextBox txtCompanyName;
        private GrapeCity.ActiveReports.SectionReportModel.TextBox TextBox4;
        private GrapeCity.ActiveReports.SectionReportModel.Picture Picture1;
        private GrapeCity.ActiveReports.SectionReportModel.Line line1;
        private GrapeCity.ActiveReports.SectionReportModel.Shape Shape1;
    }
}
