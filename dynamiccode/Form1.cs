using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;


using Westwind.wwScripting;
using MPC.Interfaces.WebStoreServices;

namespace DynamicCompilation
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button btnParse;
		private System.Windows.Forms.Button cmdBasic;
		private System.Windows.Forms.GroupBox groupBox1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
        private readonly ICostCentreService _myCompanyService;
		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.button1 = new System.Windows.Forms.Button();
			this.btnParse = new System.Windows.Forms.Button();
			this.cmdBasic = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(64, 64);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(100, 25);
			this.button1.TabIndex = 0;
			this.button1.Text = "wwScripting";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// btnParse
			// 
			this.btnParse.Location = new System.Drawing.Point(64, 104);
			this.btnParse.Name = "btnParse";
			this.btnParse.Size = new System.Drawing.Size(100, 25);
			this.btnParse.TabIndex = 3;
			this.btnParse.Text = "Parse Template";
			this.btnParse.Click += new System.EventHandler(this.btnParse_Click);
			// 
			// cmdBasic
			// 
			this.cmdBasic.Location = new System.Drawing.Point(64, 24);
			this.cmdBasic.Name = "cmdBasic";
			this.cmdBasic.Size = new System.Drawing.Size(100, 25);
			this.cmdBasic.TabIndex = 5;
			this.cmdBasic.Text = "Basic Execution";
			this.cmdBasic.Click += new System.EventHandler(this.cmdBasic_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Location = new System.Drawing.Point(16, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(184, 136);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(216, 157);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.cmdBasic,
																		  this.btnParse,
																		  this.button1,
																		  this.groupBox1});
			this.Name = "Form1";
			this.Text = "Dynamic Code Samples";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
            Application.Run(new BasicExecution());
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			wwScriptingForm  oForm = new wwScriptingForm();
			oForm.Show();
		}

		private void btnParse_Click(object sender, System.EventArgs e)
		{
			wwAspScriptingForm oForm = new wwAspScriptingForm();
			oForm.Show();
		}

		private void cmdBasic_Click(object sender, System.EventArgs e)
		{
            BasicExecution oForm = new BasicExecution();
			oForm.Show();
		}

	}
}
