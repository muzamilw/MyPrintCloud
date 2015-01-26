using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using System.CodeDom.Compiler;
using Microsoft.CSharp;
using Microsoft.VisualBasic;
using System.Reflection;
using Westwind.RemoteLoader;
using MPC.Implementation.WebStoreServices;
using MPC.Interfaces.WebStoreServices;

using Microsoft.Practices.Unity;
using MPC.WebBase.UnityConfiguration;
using UnityDependencyResolver = MPC.WebBase.UnityConfiguration.UnityDependencyResolver;
using System.Web.Mvc;
namespace DynamicCompilation
{
	/// <summary>
	/// Summary description for BasicExecution.
	/// </summary>
	public class BasicExecution : System.Windows.Forms.Form
	{
        private IUnityContainer container;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox txtCode;
		private System.Windows.Forms.TextBox txtAssemblyCode;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button button2;
        private  ICostCentreService _myCompanyService;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;


        private IUnityContainer CreateUnityContainer()
        {
            container = UnityWebActivator.Container;
            RegisterTypes();

            return container;
        }

        private void RegisterTypes()
        {
            MPC.WebBase.TypeRegistrations.RegisterTypes(container);
            MPC.Implementation.TypeRegistrations.RegisterType(container);

        }
        public BasicExecution()
		{
            if (container == null)
            {
                container = CreateUnityContainer();
            }
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();


			this.txtCode.Text = @"

 object[] oParamsArray = new object[6];
            oParamsArray[1] = 1;
            oParamsArray[5] = 2;
            oParamsArray[4] = 1;
            oParamsArray[2] = null;
            oParamsArray[3] = null;
            ExecutionService servicetofire = new ExecutionService();
            double cName = servicetofire.ExecuteQuestion(ref oParamsArray, 6, 335);
MessageBox.Show(""Hello World"" + cName);
return (object) DateTime.Now;
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
			this.txtCode = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.txtAssemblyCode = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.button2 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtCode
			// 
			this.txtCode.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.txtCode.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtCode.Location = new System.Drawing.Point(0, 64);
			this.txtCode.Multiline = true;
			this.txtCode.Name = "txtCode";
			this.txtCode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtCode.Size = new System.Drawing.Size(536, 120);
			this.txtCode.TabIndex = 0;
			this.txtCode.Text = "txtCode";
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.button1.Location = new System.Drawing.Point(8, 8);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(96, 24);
			this.button1.TabIndex = 1;
			this.button1.Text = "E&xecute";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// txtAssemblyCode
			// 
			this.txtAssemblyCode.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.txtAssemblyCode.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtAssemblyCode.Location = new System.Drawing.Point(0, 208);
			this.txtAssemblyCode.Multiline = true;
			this.txtAssemblyCode.Name = "txtAssemblyCode";
			this.txtAssemblyCode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtAssemblyCode.Size = new System.Drawing.Size(536, 304);
			this.txtAssemblyCode.TabIndex = 2;
			this.txtAssemblyCode.Text = "txtAssemblyCode";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(0, 192);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(152, 16);
			this.label1.TabIndex = 3;
			this.label1.Text = "Generated Assembly Source:";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(0, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(152, 14);
			this.label2.TabIndex = 4;
			this.label2.Text = "Source Code to Execute:";
			// 
			// button2
			// 
			this.button2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.button2.Location = new System.Drawing.Point(112, 8);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(120, 24);
			this.button2.TabIndex = 5;
			this.button2.Text = "E&xecute AppDomain";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// BasicExecution
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(536, 509);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button2,
																		  this.label2,
																		  this.label1,
																		  this.txtAssemblyCode,
																		  this.button1,
																		  this.txtCode});
			this.Name = "BasicExecution";
			this.Text = "Basic Dynamic Execution";
			this.Load += new System.EventHandler(this.BasicExecution_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			string lcCode = this.txtCode.Text;

			// *** Must create a fully functional assembly
			lcCode = @"
using System;
using System.IO;
using System.Windows.Forms;
using System.Data.Entity;

namespace MyNamespace {
public class MyClass {

DbContext octx = null;

public object DynamicCode(params object[] Parameters) {
" + lcCode +
"}   }    }";

			ICodeCompiler loCompiler = new CSharpCodeProvider().CreateCompiler();
			CompilerParameters loParameters = new CompilerParameters();

			// *** Start by adding any referenced assemblies
			loParameters.ReferencedAssemblies.Add("System.dll");
			loParameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            loParameters.ReferencedAssemblies.Add("System.Data.Entity.dll");
            loParameters.ReferencedAssemblies.Add("EntityFramework.dll");

            

			// *** Load the resulting assembly into memory
			loParameters.GenerateInMemory = true;
			
			// *** Now compile the whole thing
			CompilerResults loCompiled = loCompiler.CompileAssemblyFromSource(loParameters,lcCode);

			if (loCompiled.Errors.HasErrors) 
			{
				string lcErrorMsg = "";

				// *** Create Error String
				lcErrorMsg = loCompiled.Errors.Count.ToString() + " Errors:";
				for (int x=0;x<loCompiled.Errors.Count;x++) 
					lcErrorMsg = lcErrorMsg  + "\r\nLine: " + loCompiled.Errors[x].Line.ToString() + " - " + 
						loCompiled.Errors[x].ErrorText;		

				MessageBox.Show(lcErrorMsg + "\r\n\r\n" + lcCode,"Compiler Demo",MessageBoxButtons.OK,MessageBoxIcon.Error);

				return;
			}

			this.txtAssemblyCode.Text = lcCode;

			Assembly loAssembly = loCompiled.CompiledAssembly;

			// *** Retrieve an object reference - since this object is 'dynamic' we can't explicitly
			// *** type it so it's of type Object
			object loObject  = loAssembly.CreateInstance("MyNamespace.MyClass");
			if (loObject == null) 
			{
				MessageBox.Show("Couldn't load class.");
				return;
			}

			object[] loCodeParms = new object[1];
			loCodeParms[0] = "West Wind Technologies";
            
			try 
			{
				object loResult = loObject.GetType().InvokeMember("DynamicCode",
	            BindingFlags.InvokeMethod,null,loObject,loCodeParms);

				DateTime ltNow = (DateTime) loResult;
				MessageBox.Show("Method Call Result:\r\n\r\n" + loResult.ToString(),"Compiler Demo",MessageBoxButtons.OK,MessageBoxIcon.Information);
			}
			catch(Exception loError) 
			{
				MessageBox.Show(loError.Message,"Compiler Demo",MessageBoxButtons.OK,MessageBoxIcon.Information);
			}
		}

		private void BasicExecution_Load(object sender, System.EventArgs e)
		{
		
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
           
          //  MessageBox.Show(_myCompanyService.test());
			string lcCode = this.txtCode.Text;
			
			// ** Create an AppDomain
			AppDomainSetup loSetup = new AppDomainSetup();
			loSetup.ApplicationBase = AppDomain.CurrentDomain.BaseDirectory;
			AppDomain loAppDomain = AppDomain.CreateDomain("MyAppDomain",null,loSetup);


			// *** Must create a fully functional assembly
			lcCode = @"
using System;
using System.IO;
using System.Windows.Forms;
using System.Data.Entity;

using System.Reflection;
using Westwind.RemoteLoader;
using MPC.Implementation.WebStoreServices;
namespace MyNamespace {
public class MyClass : MarshalByRefObject,IRemoteInterface  {
DbContext octx = null;
public object Invoke(string lcMethod,object[] Parameters) {
	return this.GetType().InvokeMember(lcMethod,
            BindingFlags.InvokeMethod,null,this,Parameters);
}

public object DynamicCode(params object[] Parameters) {
" + lcCode + 
				"}   }    }";

			ICodeCompiler loCompiler = new CSharpCodeProvider().CreateCompiler();
			CompilerParameters loParameters = new CompilerParameters();

			// *** Start by adding any referenced assemblies
			loParameters.ReferencedAssemblies.Add("System.dll");
			loParameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");
			loParameters.ReferencedAssemblies.Add("Remoteloader.dll");
            loParameters.ReferencedAssemblies.Add("System.Data.Entity.dll");
            loParameters.ReferencedAssemblies.Add("EntityFramework.dll");
            loParameters.ReferencedAssemblies.Add("MPC.Implementation.dll");
            loParameters.ReferencedAssemblies.Add("System.Web.Mvc.dll");
            
			// *** Load the resulting assembly into memory
			loParameters.GenerateInMemory = false;
			loParameters.OutputAssembly = "MyNamespace.dll";
			
			// *** Now compile the whole thing
			CompilerResults loCompiled = loCompiler.CompileAssemblyFromSource(loParameters,lcCode);

			if (loCompiled.Errors.HasErrors) 
			{
				string lcErrorMsg = "";

				// *** Create Error String
				lcErrorMsg = loCompiled.Errors.Count.ToString() + " Errors:";
				for (int x=0;x<loCompiled.Errors.Count;x++) 
					lcErrorMsg = lcErrorMsg  + "\r\nLine: " + loCompiled.Errors[x].Line.ToString() + " - " + 
						loCompiled.Errors[x].ErrorText;		

				MessageBox.Show(lcErrorMsg + "\r\n\r\n" + lcCode,"Compiler Demo",MessageBoxButtons.OK,MessageBoxIcon.Error);

				return;
			}

			this.txtAssemblyCode.Text = lcCode;

			// create the factory class in the secondary app-domain
			RemoteLoaderFactory factory = 
				(RemoteLoaderFactory) loAppDomain.CreateInstance( "RemoteLoader", 
				"Westwind.RemoteLoader.RemoteLoaderFactory" ).Unwrap();

			// with the help of this factory, we can now create a real 'LiveClass' instance
			object loObject = factory.Create( "mynamespace.dll", "MyNamespace.MyClass", null ); 

			// *** Cast the object to the remote interface to avoid loading type info
			IRemoteInterface loRemote = (IRemoteInterface) loObject;

			if (loObject == null) 
			{
				MessageBox.Show("Couldn't load class.");
				return;
			}

			object[] loCodeParms = new object[2];

			loCodeParms[0] = "West Wind Technologies";
            loCodeParms[1] = _myCompanyService;
			try 
			{
				// *** Indirectly call the remote interface
				object loResult = loRemote.Invoke("DynamicCode",loCodeParms);


				DateTime ltNow = (DateTime) loResult;
				MessageBox.Show("Method Call Result:\r\n\r\n" + loResult.ToString(),"Compiler Demo",MessageBoxButtons.OK,MessageBoxIcon.Information);
			}
			catch(Exception loError) 
			{
				MessageBox.Show(loError.Message,"Compiler Demo",MessageBoxButtons.OK,MessageBoxIcon.Information);
			}		

			loRemote = null;
			AppDomain.Unload(loAppDomain);
			loAppDomain = null;
			File.Delete("mynamespace.dll");

		}
	}

	

}
