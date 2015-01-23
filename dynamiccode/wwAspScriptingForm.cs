using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms; 

using Westwind.wwScripting;

namespace DynamicCompilation
{
	/// <summary>
	/// Summary description for wwAspScriptingForm.
	/// </summary>
	public class wwAspScriptingForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button cmdExecute;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtGeneratedCode;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox txtLanguage;
		private System.Windows.Forms.TextBox txtCode;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public wwAspScriptingForm()	
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
this.txtCode.Text = @"<%@ Assembly name=""System.Windows.Forms.dll""%>
<%@ Import namespace=""System.Windows.Forms""%>
<html>
<body>
<% 
MessageBox.Show(""Hello World"");
int x = 0;
string lcOutput = """";

for (x=1;x<10;x++) {
   lcOutput = lcOutput + x.ToString() + ""\r\n"";
}

Response.Write(lcOutput);
 %>
The time is: <%= DateTime.Now.ToString() %>
</body>
</html>
";
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
			this.cmdExecute = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.txtGeneratedCode = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.txtLanguage = new System.Windows.Forms.ComboBox();
			this.txtCode = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// cmdExecute
			// 
			this.cmdExecute.Location = new System.Drawing.Point(144, 24);
			this.cmdExecute.Name = "cmdExecute";
			this.cmdExecute.Size = new System.Drawing.Size(136, 24);
			this.cmdExecute.TabIndex = 21;
			this.cmdExecute.Text = "E&xecute Code (simple)";
			this.cmdExecute.Click += new System.EventHandler(this.cmdExecute_Click);
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.Location = new System.Drawing.Point(0, 344);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(144, 16);
			this.label3.TabIndex = 20;
			this.label3.Text = "Generated Code:";
			// 
			// txtGeneratedCode
			// 
			this.txtGeneratedCode.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.txtGeneratedCode.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtGeneratedCode.Location = new System.Drawing.Point(0, 360);
			this.txtGeneratedCode.Multiline = true;
			this.txtGeneratedCode.Name = "txtGeneratedCode";
			this.txtGeneratedCode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtGeneratedCode.Size = new System.Drawing.Size(632, 264);
			this.txtGeneratedCode.TabIndex = 19;
			this.txtGeneratedCode.Text = "";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(0, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(144, 16);
			this.label2.TabIndex = 18;
			this.label2.Text = "Script Code to run:";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(144, 16);
			this.label1.TabIndex = 17;
			this.label1.Text = "Script Language:";
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
			this.txtLanguage.TabIndex = 16;
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
			this.txtCode.Size = new System.Drawing.Size(632, 264);
			this.txtCode.TabIndex = 15;
			this.txtCode.Text = "";
			// 
			// wwAspScriptingForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(632, 621);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.cmdExecute,
																		  this.label3,
																		  this.txtGeneratedCode,
																		  this.label2,
																		  this.label1,
																		  this.txtLanguage,
																		  this.txtCode});
			this.Name = "wwAspScriptingForm";
			this.Text = "wwASPScripting Dynamic Scripting Demo";
			this.ResumeLayout(false);

		}
		#endregion

		private void cmdExecute_Click(object sender, System.EventArgs e)
		{
			wwASPScripting oASP = new wwASPScripting();
			string lcCode  = oASP.ParseScript(this.txtCode.Text);
			
			this.txtGeneratedCode.Text = lcCode;

			wwScripting loScript = oASP.oScript;

			// *** You can do this with <%@ Assembly %> and <@%= Namespace %> directives.
			//loScript.AddAssembly("system.windows.forms.dll","System.Windows.Forms");
			
			loScript.lSaveSourceCode = true;
			//loScript.CreateAppDomain("wwScriptDomain");
			
			string  lcResult = (string) loScript.ExecuteCode(lcCode);

			if (loScript.bError)
				MessageBox.Show(loScript.cErrorMsg + "\r\n\r\n" + loScript.cSourceCode);
			else 
			{
				MessageBox.Show(lcResult,"Script Output");
				MessageBox.Show(loScript.cSourceCode,"Generated Assembly Source Code");
			}

			loScript.Dispose();
		}
	}
}
