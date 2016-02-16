namespace MigrationUtility
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.output = new System.Windows.Forms.TextBox();
            this.btnImportRetail = new System.Windows.Forms.Button();
            this.btnCorporateStoreImport = new System.Windows.Forms.Button();
            this.txtCorpStoreId = new System.Windows.Forms.TextBox();
            this.btnStoreWidgetExport = new System.Windows.Forms.Button();
            this.txtStoreId = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtTargetRetailStoreId = new System.Windows.Forms.TextBox();
            this.rdRetailStoreTargetNew = new System.Windows.Forms.RadioButton();
            this.rdRetailStoreTargetExisting = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.txtMPCContentBasePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPinkCardsStoredImagesBasePath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnContentPath = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.txtOrganisationId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.txtDesignerBasePath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // output
            // 
            this.output.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.output.Location = new System.Drawing.Point(0, 243);
            this.output.Multiline = true;
            this.output.Name = "output";
            this.output.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.output.Size = new System.Drawing.Size(1251, 429);
            this.output.TabIndex = 0;
            // 
            // btnImportRetail
            // 
            this.btnImportRetail.Location = new System.Drawing.Point(432, 17);
            this.btnImportRetail.Name = "btnImportRetail";
            this.btnImportRetail.Size = new System.Drawing.Size(122, 23);
            this.btnImportRetail.TabIndex = 1;
            this.btnImportRetail.Text = "Retail Store Import";
            this.btnImportRetail.UseVisualStyleBackColor = true;
            this.btnImportRetail.Click += new System.EventHandler(this.btnImportRetail_Click);
            // 
            // btnCorporateStoreImport
            // 
            this.btnCorporateStoreImport.Location = new System.Drawing.Point(444, 175);
            this.btnCorporateStoreImport.Name = "btnCorporateStoreImport";
            this.btnCorporateStoreImport.Size = new System.Drawing.Size(122, 23);
            this.btnCorporateStoreImport.TabIndex = 2;
            this.btnCorporateStoreImport.Text = "Corporate Store Import";
            this.btnCorporateStoreImport.UseVisualStyleBackColor = true;
            this.btnCorporateStoreImport.Click += new System.EventHandler(this.btnCorporateStoreImport_Click);
            // 
            // txtCorpStoreId
            // 
            this.txtCorpStoreId.Location = new System.Drawing.Point(321, 177);
            this.txtCorpStoreId.Name = "txtCorpStoreId";
            this.txtCorpStoreId.Size = new System.Drawing.Size(100, 20);
            this.txtCorpStoreId.TabIndex = 3;
            this.txtCorpStoreId.Text = "9781";
            // 
            // btnStoreWidgetExport
            // 
            this.btnStoreWidgetExport.Location = new System.Drawing.Point(1015, 12);
            this.btnStoreWidgetExport.Name = "btnStoreWidgetExport";
            this.btnStoreWidgetExport.Size = new System.Drawing.Size(212, 23);
            this.btnStoreWidgetExport.TabIndex = 4;
            this.btnStoreWidgetExport.Text = "Get Store Widget JSON";
            this.btnStoreWidgetExport.UseVisualStyleBackColor = true;
            this.btnStoreWidgetExport.Click += new System.EventHandler(this.btnStoreWidgetExport_Click);
            // 
            // txtStoreId
            // 
            this.txtStoreId.Location = new System.Drawing.Point(909, 15);
            this.txtStoreId.Name = "txtStoreId";
            this.txtStoreId.Size = new System.Drawing.Size(100, 20);
            this.txtStoreId.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(0, -1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(162, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Base Data Settings Import";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtTargetRetailStoreId
            // 
            this.txtTargetRetailStoreId.Location = new System.Drawing.Point(315, 19);
            this.txtTargetRetailStoreId.Name = "txtTargetRetailStoreId";
            this.txtTargetRetailStoreId.Size = new System.Drawing.Size(100, 20);
            this.txtTargetRetailStoreId.TabIndex = 7;
            this.txtTargetRetailStoreId.Text = "0";
            // 
            // rdRetailStoreTargetNew
            // 
            this.rdRetailStoreTargetNew.AutoSize = true;
            this.rdRetailStoreTargetNew.Checked = true;
            this.rdRetailStoreTargetNew.Location = new System.Drawing.Point(6, 20);
            this.rdRetailStoreTargetNew.Name = "rdRetailStoreTargetNew";
            this.rdRetailStoreTargetNew.Size = new System.Drawing.Size(105, 17);
            this.rdRetailStoreTargetNew.TabIndex = 8;
            this.rdRetailStoreTargetNew.TabStop = true;
            this.rdRetailStoreTargetNew.Text = "New Retail Store";
            this.rdRetailStoreTargetNew.UseVisualStyleBackColor = true;
            // 
            // rdRetailStoreTargetExisting
            // 
            this.rdRetailStoreTargetExisting.AutoSize = true;
            this.rdRetailStoreTargetExisting.Location = new System.Drawing.Point(117, 19);
            this.rdRetailStoreTargetExisting.Name = "rdRetailStoreTargetExisting";
            this.rdRetailStoreTargetExisting.Size = new System.Drawing.Size(192, 17);
            this.rdRetailStoreTargetExisting.TabIndex = 9;
            this.rdRetailStoreTargetExisting.Text = "Existing Retail Store. Enter Store ID";
            this.rdRetailStoreTargetExisting.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtStatus);
            this.groupBox1.Controls.Add(this.rdRetailStoreTargetExisting);
            this.groupBox1.Controls.Add(this.btnImportRetail);
            this.groupBox1.Controls.Add(this.rdRetailStoreTargetNew);
            this.groupBox1.Controls.Add(this.txtTargetRetailStoreId);
            this.groupBox1.Location = new System.Drawing.Point(12, 124);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(733, 45);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(572, 20);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(155, 20);
            this.txtStatus.TabIndex = 10;
            // 
            // txtMPCContentBasePath
            // 
            this.txtMPCContentBasePath.Location = new System.Drawing.Point(201, 49);
            this.txtMPCContentBasePath.Name = "txtMPCContentBasePath";
            this.txtMPCContentBasePath.Size = new System.Drawing.Size(565, 20);
            this.txtMPCContentBasePath.TabIndex = 11;
            this.txtMPCContentBasePath.Text = "D:\\wwwRoot\\australia.myprintcloud.com\\mis\\MPC_Content";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "MPC Content BasePath";
            // 
            // txtPinkCardsStoredImagesBasePath
            // 
            this.txtPinkCardsStoredImagesBasePath.Location = new System.Drawing.Point(201, 81);
            this.txtPinkCardsStoredImagesBasePath.Name = "txtPinkCardsStoredImagesBasePath";
            this.txtPinkCardsStoredImagesBasePath.Size = new System.Drawing.Size(565, 20);
            this.txtPinkCardsStoredImagesBasePath.TabIndex = 13;
            this.txtPinkCardsStoredImagesBasePath.Text = "D:\\wwwRoot\\SunnyLand\\mis\\StoredImages";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "OLD StoredImages BasePath";
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.HelpRequest += new System.EventHandler(this.folderBrowserDialog1_HelpRequest);
            // 
            // btnContentPath
            // 
            this.btnContentPath.Location = new System.Drawing.Point(772, 47);
            this.btnContentPath.Name = "btnContentPath";
            this.btnContentPath.Size = new System.Drawing.Size(37, 23);
            this.btnContentPath.TabIndex = 15;
            this.btnContentPath.Text = "...";
            this.btnContentPath.UseVisualStyleBackColor = true;
            this.btnContentPath.Click += new System.EventHandler(this.btnContentPath_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(772, 79);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(37, 23);
            this.button2.TabIndex = 16;
            this.button2.Text = "...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtOrganisationId
            // 
            this.txtOrganisationId.Location = new System.Drawing.Point(201, 23);
            this.txtOrganisationId.Name = "txtOrganisationId";
            this.txtOrganisationId.Size = new System.Drawing.Size(100, 20);
            this.txtOrganisationId.TabIndex = 17;
            this.txtOrganisationId.Text = "447";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(50, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "OrganisationID";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(444, 215);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(122, 23);
            this.button3.TabIndex = 19;
            this.button3.Text = "Import Inventory";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // txtDesignerBasePath
            // 
            this.txtDesignerBasePath.Location = new System.Drawing.Point(201, 107);
            this.txtDesignerBasePath.Name = "txtDesignerBasePath";
            this.txtDesignerBasePath.Size = new System.Drawing.Size(565, 20);
            this.txtDesignerBasePath.TabIndex = 20;
            this.txtDesignerBasePath.Text = "D:\\wwwRoot\\SunnyLand\\DesignEngine\\designer\\products";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(168, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "OLD DesignerProducts Base Path";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1251, 672);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDesignerBasePath);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtOrganisationId);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnContentPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPinkCardsStoredImagesBasePath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMPCContentBasePath);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtStoreId);
            this.Controls.Add(this.btnStoreWidgetExport);
            this.Controls.Add(this.txtCorpStoreId);
            this.Controls.Add(this.btnCorporateStoreImport);
            this.Controls.Add(this.output);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox output;
        private System.Windows.Forms.Button btnImportRetail;
        private System.Windows.Forms.Button btnCorporateStoreImport;
        private System.Windows.Forms.TextBox txtCorpStoreId;
        private System.Windows.Forms.Button btnStoreWidgetExport;
        private System.Windows.Forms.TextBox txtStoreId;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtTargetRetailStoreId;
        private System.Windows.Forms.RadioButton rdRetailStoreTargetNew;
        private System.Windows.Forms.RadioButton rdRetailStoreTargetExisting;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtMPCContentBasePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPinkCardsStoredImagesBasePath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnContentPath;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtOrganisationId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox txtDesignerBasePath;
        private System.Windows.Forms.Label label4;
    }
}

