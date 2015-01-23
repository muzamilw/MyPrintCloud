using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using Westwind.wwScripting;


namespace DynamicCompilation
{
	/// <summary>
	/// Summary description for wwScriptingForm.
	/// </summary>
	public class wwScriptingForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ComboBox txtLanguage;
		private System.Windows.Forms.TextBox txtCode;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtGeneratedCode;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button cmdExecute;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public wwScriptingForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.txtLanguage.SelectedIndex = 0;

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
				if(components != null)
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
			this.txtLanguage = new System.Windows.Forms.ComboBox();
			this.txtCode = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtGeneratedCode = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.cmdExecute = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtLanguage
			// 
			this.txtLanguage.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtLanguage.Items.AddRange(new object[] {
															 "C#",
															 "VB"});
			this.txtLanguage.Location = new System.Drawing.Point(8, 24);
			this.txtLanguage.Name = "txtLanguage";
			this.txtLanguage.Size = new System.Drawing.Size(128, 21);
			this.txtLanguage.TabIndex = 9;
			// 
			// txtCode
			// 
			this.txtCode.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.txtCode.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtCode.Location = new System.Drawing.Point(0, 72);
			this.txtCode.Multiline = true;
			this.txtCode.Name = "txtCode";
			this.txtCode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtCode.Size = new System.Drawing.Size(672, 272);
			this.txtCode.TabIndex = 8;
			this.txtCode.Text = "";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(144, 16);
			this.label1.TabIndex = 10;
			this.label1.Text = "Script Language:";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(0, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(144, 16);
			this.label2.TabIndex = 11;
			this.label2.Text = "Script Code to run:";
			// 
			// txtGeneratedCode
			// 
			this.txtGeneratedCode.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.txtGeneratedCode.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtGeneratedCode.Location = new System.Drawing.Point(0, 368);
			this.txtGeneratedCode.Multiline = true;
			this.txtGeneratedCode.Name = "txtGeneratedCode";
			this.txtGeneratedCode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtGeneratedCode.Size = new System.Drawing.Size(672, 208);
			this.txtGeneratedCode.TabIndex = 12;
			this.txtGeneratedCode.Text = "";
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.Location = new System.Drawing.Point(0, 352);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(144, 16);
			this.label3.TabIndex = 13;
			this.label3.Text = "Generated Code:";
			// 
			// cmdExecute
			// 
			this.cmdExecute.Location = new System.Drawing.Point(144, 24);
			this.cmdExecute.Name = "cmdExecute";
			this.cmdExecute.Size = new System.Drawing.Size(136, 24);
			this.cmdExecute.TabIndex = 14;
			this.cmdExecute.Text = "E&xecute Code (simple)";
			this.cmdExecute.Click += new System.EventHandler(this.cmdExecute_Click);
			// 
			// wwScriptingForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(672, 573);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.cmdExecute,
																		  this.label3,
																		  this.txtGeneratedCode,
																		  this.label2,
																		  this.label1,
																		  this.txtLanguage,
																		  this.txtCode});
			this.Name = "wwScriptingForm";
			this.Text = "wwScripting Class Execution";
			this.Load += new System.EventHandler(this.wwScriptingForm_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{

		}

		private void wwScriptingForm_Load(object sender, System.EventArgs e)
		{
			this.txtCode.Text = 
				@"
string cHello;
cHello = ""What's this?"";
MessageBox.Show(cHello,""Compiler Demo"");
return DateTime.Now.ToString();
";
	
		}

		private void cmdExecute_Click(object sender, System.EventArgs e)
		{
			wwScripting loScript = new wwScripting(this.txtLanguage.Text);
			loScript.lSaveSourceCode = true;

			// loScript.CreateAppDomain("WestWind");  // force into AppDomain

			loScript.AddAssembly("system.windows.forms.dll","System.Windows.Forms");
			//loScript.AddAssembly("system.web.dll","System.Web");
			//loScript.AddNamespace("System.Net");

			string lcCode = this.txtCode.Text;
			int x = 100;
			string lcResult = (string) loScript.ExecuteCode(lcCode,"rick strahl",(int) x,(decimal) 10 );

//*** Execute full method or mutliple methods on the same object
//			string lcResult = (string) loScript.ExecuteMethod(lcCode,"Test","rick strahl",(int) x);
//			lcResult =  (string) loScript.CallMethod(loScript.oObjRef,"Test2","rick strahl",(int) x);

			this.txtGeneratedCode.Text = loScript.cSourceCode;

			if (loScript.bError)
				MessageBox.Show(loScript.cErrorMsg + "\r\n\r\n" + loScript.cSourceCode,
					            "Compile ",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
			else
				MessageBox.Show(lcResult,"Code Result");


			loScript.Dispose();
		}
	}
}
