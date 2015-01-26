using System;
using System.IO;
using System.Text;
using System.CodeDom.Compiler;
using System.Collections.Specialized;
using System.Collections;
using Microsoft.CSharp;
using System.Reflection;
using System.Windows.Forms;

class CSharpScriptApp 
{
	public static void Main(String[] args)
	{
		try {
			CSharpScriptApp o = new CSharpScriptApp(args[0]);
		} catch {}
	
		return;
	}

	// Constructor of the app's main class
	public CSharpScriptApp(String filename)
	{
		RunCSharpCompiler(filename);
	}

	// Attempts to compile the CS code in the specified file
	protected void RunCSharpCompiler(String csfile)
	{
		ICodeCompiler icc =  (new CSharpCodeProvider()).CreateCompiler();
		CompilerParameters cp = new CompilerParameters();

		cp.ReferencedAssemblies.Add("system.dll"); 
		cp.ReferencedAssemblies.Add("system.windows.forms.dll"); 
		cp.ReferencedAssemblies.Add("system.xml.dll"); 
		cp.ReferencedAssemblies.Add("system.data.dll"); 
	

		// OutputAssembly overwrites GenerateInMemory
		//cp.CompilerOptions = "/target:winexe"; 
		cp.GenerateExecutable = true;
		cp.GenerateInMemory = true;

		// Get the source code from the file and adds main declarations
		StreamReader sr = new StreamReader(csfile);
		String strCsSource = sr.ReadToEnd();
		sr.Close();

		StringBuilder sb = new StringBuilder("");
		sb.Append("using System;");
		sb.Append("using System.Windows.Forms;");
		sb.Append("using System.IO;");
		sb.Append("using System.Xml;");
		sb.Append("using System.Data;");
		sb.Append("using System.Data.SqlClient;");

		sb.Append("namespace Expoware { class ExpowareApp {");
		sb.Append("public static void Main() {");
		sb.Append(strCsSource);
		sb.Append("}}}");

		CompilerResults cr = icc.CompileAssemblyFromSource(cp, sb.ToString());
		Assembly a = cr.CompiledAssembly;

		try {
			Object o = a.CreateInstance("ExpowareApp");
			MethodInfo mi = a.EntryPoint;
			mi.Invoke(o, null);
		}
		catch {}
	}
}