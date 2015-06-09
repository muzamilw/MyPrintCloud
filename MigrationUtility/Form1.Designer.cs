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
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // output
            // 
            this.output.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.output.Location = new System.Drawing.Point(0, 149);
            this.output.Multiline = true;
            this.output.Name = "output";
            this.output.Size = new System.Drawing.Size(1317, 522);
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
            this.btnCorporateStoreImport.Location = new System.Drawing.Point(438, 105);
            this.btnCorporateStoreImport.Name = "btnCorporateStoreImport";
            this.btnCorporateStoreImport.Size = new System.Drawing.Size(122, 23);
            this.btnCorporateStoreImport.TabIndex = 2;
            this.btnCorporateStoreImport.Text = "Corporate Store Import";
            this.btnCorporateStoreImport.UseVisualStyleBackColor = true;
            this.btnCorporateStoreImport.Click += new System.EventHandler(this.btnCorporateStoreImport_Click);
            // 
            // txtCorpStoreId
            // 
            this.txtCorpStoreId.Location = new System.Drawing.Point(321, 107);
            this.txtCorpStoreId.Name = "txtCorpStoreId";
            this.txtCorpStoreId.Size = new System.Drawing.Size(100, 20);
            this.txtCorpStoreId.TabIndex = 3;
            this.txtCorpStoreId.Text = "10492";
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
            this.button1.Location = new System.Drawing.Point(12, 12);
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
            this.txtTargetRetailStoreId.Text = "63395";
            // 
            // rdRetailStoreTargetNew
            // 
            this.rdRetailStoreTargetNew.AutoSize = true;
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
            this.rdRetailStoreTargetExisting.Checked = true;
            this.rdRetailStoreTargetExisting.Location = new System.Drawing.Point(117, 19);
            this.rdRetailStoreTargetExisting.Name = "rdRetailStoreTargetExisting";
            this.rdRetailStoreTargetExisting.Size = new System.Drawing.Size(192, 17);
            this.rdRetailStoreTargetExisting.TabIndex = 9;
            this.rdRetailStoreTargetExisting.TabStop = true;
            this.rdRetailStoreTargetExisting.Text = "Existing Retail Store. Enter Store ID";
            this.rdRetailStoreTargetExisting.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdRetailStoreTargetExisting);
            this.groupBox1.Controls.Add(this.btnImportRetail);
            this.groupBox1.Controls.Add(this.rdRetailStoreTargetNew);
            this.groupBox1.Controls.Add(this.txtTargetRetailStoreId);
            this.groupBox1.Location = new System.Drawing.Point(12, 54);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(652, 45);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1317, 671);
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
    }
}

