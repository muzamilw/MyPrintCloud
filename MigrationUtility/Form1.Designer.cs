﻿namespace MigrationUtility
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
            this.SuspendLayout();
            // 
            // output
            // 
            this.output.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.output.Location = new System.Drawing.Point(0, 90);
            this.output.Multiline = true;
            this.output.Name = "output";
            this.output.Size = new System.Drawing.Size(1317, 581);
            this.output.TabIndex = 0;
            // 
            // btnImportRetail
            // 
            this.btnImportRetail.Location = new System.Drawing.Point(24, 27);
            this.btnImportRetail.Name = "btnImportRetail";
            this.btnImportRetail.Size = new System.Drawing.Size(122, 23);
            this.btnImportRetail.TabIndex = 1;
            this.btnImportRetail.Text = "Retail Store Import";
            this.btnImportRetail.UseVisualStyleBackColor = true;
            // 
            // btnCorporateStoreImport
            // 
            this.btnCorporateStoreImport.Location = new System.Drawing.Point(363, 27);
            this.btnCorporateStoreImport.Name = "btnCorporateStoreImport";
            this.btnCorporateStoreImport.Size = new System.Drawing.Size(122, 23);
            this.btnCorporateStoreImport.TabIndex = 2;
            this.btnCorporateStoreImport.Text = "Corporate Store Import";
            this.btnCorporateStoreImport.UseVisualStyleBackColor = true;
            // 
            // txtCorpStoreId
            // 
            this.txtCorpStoreId.Location = new System.Drawing.Point(245, 29);
            this.txtCorpStoreId.Name = "txtCorpStoreId";
            this.txtCorpStoreId.Size = new System.Drawing.Size(100, 20);
            this.txtCorpStoreId.TabIndex = 3;
            // 
            // btnStoreWidgetExport
            // 
            this.btnStoreWidgetExport.Location = new System.Drawing.Point(805, 26);
            this.btnStoreWidgetExport.Name = "btnStoreWidgetExport";
            this.btnStoreWidgetExport.Size = new System.Drawing.Size(212, 23);
            this.btnStoreWidgetExport.TabIndex = 4;
            this.btnStoreWidgetExport.Text = "Get Store Widget JSON";
            this.btnStoreWidgetExport.UseVisualStyleBackColor = true;
            this.btnStoreWidgetExport.Click += new System.EventHandler(this.btnStoreWidgetExport_Click);
            // 
            // txtStoreId
            // 
            this.txtStoreId.Location = new System.Drawing.Point(699, 30);
            this.txtStoreId.Name = "txtStoreId";
            this.txtStoreId.Size = new System.Drawing.Size(100, 20);
            this.txtStoreId.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1317, 671);
            this.Controls.Add(this.txtStoreId);
            this.Controls.Add(this.btnStoreWidgetExport);
            this.Controls.Add(this.txtCorpStoreId);
            this.Controls.Add(this.btnCorporateStoreImport);
            this.Controls.Add(this.btnImportRetail);
            this.Controls.Add(this.output);
            this.Name = "Form1";
            this.Text = "Form1";
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
    }
}

