namespace FileSystemWatcher
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
            this.btnWatchFile = new System.Windows.Forms.Button();
            this.txtRootPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnImportDam = new System.Windows.Forms.Button();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.txtOrg = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtStore = new System.Windows.Forms.TextBox();
            this.btnGenerateThumbnails = new System.Windows.Forms.Button();
            this.cmdRenameFiles = new System.Windows.Forms.Button();
            this.btnShortenFilenames = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnWatchFile
            // 
            this.btnWatchFile.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnWatchFile.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnWatchFile.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnWatchFile.Location = new System.Drawing.Point(478, 5);
            this.btnWatchFile.Name = "btnWatchFile";
            this.btnWatchFile.Size = new System.Drawing.Size(119, 23);
            this.btnWatchFile.TabIndex = 5;
            this.btnWatchFile.Text = "Start Watching";
            this.btnWatchFile.UseVisualStyleBackColor = false;
            this.btnWatchFile.Visible = false;
            this.btnWatchFile.Click += new System.EventHandler(this.btnWatchFile_Click);
            // 
            // txtRootPath
            // 
            this.txtRootPath.Location = new System.Drawing.Point(88, 34);
            this.txtRootPath.Name = "txtRootPath";
            this.txtRootPath.Size = new System.Drawing.Size(371, 20);
            this.txtRootPath.TabIndex = 6;
            this.txtRootPath.Text = "E:\\damdata";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Root Path";
            // 
            // btnImportDam
            // 
            this.btnImportDam.Location = new System.Drawing.Point(478, 34);
            this.btnImportDam.Name = "btnImportDam";
            this.btnImportDam.Size = new System.Drawing.Size(110, 23);
            this.btnImportDam.TabIndex = 8;
            this.btnImportDam.Text = "Import DAM";
            this.btnImportDam.UseVisualStyleBackColor = true;
            this.btnImportDam.Click += new System.EventHandler(this.btnImportDam_Click);
            // 
            // txtStatus
            // 
            this.txtStatus.AcceptsReturn = true;
            this.txtStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatus.Location = new System.Drawing.Point(88, 63);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtStatus.Size = new System.Drawing.Size(971, 316);
            this.txtStatus.TabIndex = 9;
            // 
            // txtOrg
            // 
            this.txtOrg.Location = new System.Drawing.Point(88, 8);
            this.txtOrg.Name = "txtOrg";
            this.txtOrg.Size = new System.Drawing.Size(100, 20);
            this.txtOrg.TabIndex = 10;
            this.txtOrg.Text = "1651";
            this.txtOrg.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Organisation Id";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(320, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Store Id";
            // 
            // txtStore
            // 
            this.txtStore.Location = new System.Drawing.Point(370, 8);
            this.txtStore.Name = "txtStore";
            this.txtStore.Size = new System.Drawing.Size(89, 20);
            this.txtStore.TabIndex = 12;
            this.txtStore.Text = "33474";
            this.txtStore.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnGenerateThumbnails
            // 
            this.btnGenerateThumbnails.Location = new System.Drawing.Point(889, 34);
            this.btnGenerateThumbnails.Name = "btnGenerateThumbnails";
            this.btnGenerateThumbnails.Size = new System.Drawing.Size(173, 23);
            this.btnGenerateThumbnails.TabIndex = 14;
            this.btnGenerateThumbnails.Text = "Generate Thumbnails";
            this.btnGenerateThumbnails.UseVisualStyleBackColor = true;
            this.btnGenerateThumbnails.Click += new System.EventHandler(this.btnGenerateThumbnails_Click);
            // 
            // cmdRenameFiles
            // 
            this.cmdRenameFiles.Location = new System.Drawing.Point(594, 34);
            this.cmdRenameFiles.Name = "cmdRenameFiles";
            this.cmdRenameFiles.Size = new System.Drawing.Size(173, 23);
            this.cmdRenameFiles.TabIndex = 15;
            this.cmdRenameFiles.Text = "Rename Files";
            this.cmdRenameFiles.UseVisualStyleBackColor = true;
            this.cmdRenameFiles.Click += new System.EventHandler(this.cmdRenameFiles_Click);
            // 
            // btnShortenFilenames
            // 
            this.btnShortenFilenames.Location = new System.Drawing.Point(773, 34);
            this.btnShortenFilenames.Name = "btnShortenFilenames";
            this.btnShortenFilenames.Size = new System.Drawing.Size(110, 23);
            this.btnShortenFilenames.TabIndex = 16;
            this.btnShortenFilenames.Text = "Shorten FileNames";
            this.btnShortenFilenames.UseVisualStyleBackColor = true;
            this.btnShortenFilenames.Click += new System.EventHandler(this.btnShortenFilenames_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1071, 391);
            this.Controls.Add(this.btnShortenFilenames);
            this.Controls.Add(this.cmdRenameFiles);
            this.Controls.Add(this.btnGenerateThumbnails);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtStore);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtOrg);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.btnImportDam);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtRootPath);
            this.Controls.Add(this.btnWatchFile);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnWatchFile;
        private System.Windows.Forms.TextBox txtRootPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnImportDam;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.TextBox txtOrg;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtStore;
        private System.Windows.Forms.Button btnGenerateThumbnails;
        private System.Windows.Forms.Button cmdRenameFiles;
        private System.Windows.Forms.Button btnShortenFilenames;
    }
}

