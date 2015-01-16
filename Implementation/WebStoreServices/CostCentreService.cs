using Microsoft.VisualBasic;
using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Implementation.WebStoreServices
{
    public class CostCentreService : ICostCentreService
    {
        private readonly ICostCentreRepository _CostCentreRepository;
        public CostCentreService(ICostCentreRepository CostCentreRepository)
        {
            this._CostCentreRepository = CostCentreRepository;
        }

        
        /// <summary>
        /// It will parse the string pass to it and parse between those brackets {} and replace tag which is inside of those brackets
        /// </summary>
        /// <param name="sText"></param>
        /// <returns></returns>
        //public static string TokenParse(string sText)
        //{
        //    try {
        //        string ChangedStr = "";
        //        string[] oBeforeStr = Strings.Split(sText, "{");
        //        ChangedStr = oBeforeStr(0);
        //        if (oBeforeStr.Length > 1) {
        //            for (int i = 1; i <= oBeforeStr.Length - 1; i++) {
        //                string[] oAfterStr = Strings.Split(oBeforeStr(i), "}");
        //                if (oAfterStr.Length == 2) {
        //                    string oReplaceStr = ReplaceTag(oAfterStr(0));
        //                    if (!string.IsNullOrEmpty(oReplaceStr)) {
        //                        if (Strings.Split(oAfterStr(1), "}").Length > 1) {
        //                            Interaction.MsgBox("Invalid Calculation String.", , "Infinity");
        //                            return "";
        //                        }
        //                        ChangedStr += oReplaceStr + oAfterStr(1);
        //                    } else {
        //                        return "";
        //                    }
        //                } else {
        //                    Interaction.MsgBox("Invalid Calculation String.", , "Infinity");
        //                    return "";
        //                }
        //            }
        //        } else {
        //            string[] oAfterStr = Strings.Split(sText, "}");
        //            if (oAfterStr.Length > 1) {
        //                Interaction.MsgBox("Invalid Tag");
        //                return "";
        //            }
        //        }
        //        return ChangedStr;
        //    } catch (Exception ex) {
        //        throw new Exception("Token Parse", ex);
        //    }
        //}
        ///// <summary>
        ///// Replace Tags which we use in our visual code with the functions.
        ///// </summary>
        ///// <param name="sText"></param>
        ///// <returns></returns>
        //public static string ReplaceTag(string sText)
        //{
        //    try {
        //        string[] oSpiltTokens = Strings.Split(sText, ",");
        //        // For i As Integer = 0 To oSpiltTokens.Length - 1

        //        if (oSpiltTokens.Length > 1) {
        //            switch (oSpiltTokens(0).ToLower) {

        //                case "systemvariable":
        //                    string[] GetID = Strings.Split(oSpiltTokens(1), "\"");
        //                    if (GetID.Length == 3) {
        //                        return " BLL.CostCentres.CostCentreExecution.ExecuteVariable(ParamsArray ,\"" + GetID(1) + "\") ";
        //                    } else {
        //                        Interaction.MsgBox("Invalid Calculation String.", , "Infinity");
        //                        return "";
        //                    }

        //                    break;
        //                case "question":
        //                    string[] GetID = Strings.Split(oSpiltTokens(1), "\"");
        //                    if (GetID.Length == 3) {
        //                        return " BLL.CostCentres.CostCentreExecution.ExecuteQuestion(ParamsArray,\"" + GetID(1) + "\",CostCentreID) ";
        //                    } else {
        //                        Interaction.MsgBox("Invalid Calculation String.", , "Infinity");
        //                        return "";
        //                    }

        //                    break;
        //                case "subcostcentre":

        //                    string[] GetID = Strings.Split(oSpiltTokens(1), "\"");
        //                    string[] GetReturnValue = Strings.Split(oSpiltTokens(3), "\"");
        //                    if (GetID.Length == 3) {
        //                        return " BLL.CostCentres.CostCentreExecution.ExecuteCostCentre(ParamsArray,\"" + GetID(1) + "\",\"" + GetReturnValue(1) + "\") ";
        //                    } else {
        //                        Interaction.MsgBox("Invalid Calculation String.", , "Infinity");
        //                        return "";
        //                    }

        //                    break;
        //                case "resource":

        //                    string[] GetID = Strings.Split(oSpiltTokens(1), "\"");
        //                    string[] GetReturnValue = Strings.Split(oSpiltTokens(3), "\"");
        //                    if (GetID.Length == 3 & GetReturnValue.Length == 3) {
        //                        return " BLL.CostCentres.CostCentreExecution.ExecuteResource(ParamsArray,\"" + GetID(1) + "\",\"" + GetReturnValue(1) + "\") ";
        //                    } else {
        //                        Interaction.MsgBox("Invalid Calculation String.", , "Infinity");
        //                        return "";
        //                    }

        //                    break;
        //                case "stock":

        //                    string[] GetID = Strings.Split(oSpiltTokens(1), "\"");
        //                    string[] GetName = Strings.Split(oSpiltTokens(2), "\"");
        //                    string[] GetQType = Strings.Split(oSpiltTokens(3), "\"");
        //                    //Like per unit , per package
        //                    string[] GetQtyType = Strings.Split(oSpiltTokens(4), "\"");
        //                    //Qty,Variable or string
        //                    string[] GetValue = Strings.Split(oSpiltTokens(5), "\"");

        //                    if (((GetID.Length == 3 & GetName.Length == 3) & (GetQType.Length == 3 & GetQtyType.Length == 3) & GetValue.Length == 3)) {
        //                        return " BLL.CostCentres.CostCentreExecution.ExecuteStockItem(ParamsArray,\"" + GetID(1) + "\",\"" + GetName(1) + "\",\"" + GetQtyType(1) + "\",\"" + GetValue(1) + "\",\"" + GetQType(1) + "\",cint(CostCentreID)) ";
        //                    } else {
        //                        Interaction.MsgBox("Invalid Calculation String.", , "Infinity");
        //                        return "";
        //                    }

        //                    break;
        //                case "matrix":

        //                    string[] GetID = Strings.Split(oSpiltTokens(1), "\"");
        //                    if (GetID.Length == 3) {
        //                        return " BLL.CostCentres.CostCentreExecution.ExecuteMatrix(ParamsArray,\"" + GetID(1) + "\",cint(CostCentreID)) ";
        //                    } else {
        //                        Interaction.MsgBox("Invalid Calculation String.", , "Infinity");
        //                        return "";
        //                    }

        //                    break;
        //                case "cinput":

        //                    string[] GetID = Strings.Split(oSpiltTokens(1), "\"");
        //                    string[] GetQuestion = Strings.Split(oSpiltTokens(2), "\"");
        //                    string[] GetTypes = Strings.Split(oSpiltTokens(3), "\"");
        //                    string[] GetInputTypes = Strings.Split(oSpiltTokens(4), "\"");
        //                    string[] GetValue = Strings.Split(oSpiltTokens(5), "\"");

        //                    if ((((GetID.Length == 3 & GetQuestion.Length == 3) & (GetTypes.Length == 3 & GetValue.Length == 3)) & GetInputTypes.Length == 3)) {
        //                        return " BLL.CostCentres.CostCentreExecution.ExecuteInput(ParamsArray,\"" + GetID(1) + "\",\"" + GetQuestion(1) + "\"," + GetTypes(1) + "," + GetInputTypes(1) + ",\"" + GetValue(1) + "\",cint(CostCentreID)) ";
        //                    } else {
        //                        Interaction.MsgBox("Invalid Calculation String.", , "Infinity");
        //                        return "";
        //                    }

        //                    break;
        //                default:
        //                    Interaction.MsgBox("Invalid Calculation String.", , "Infinity");
        //                    return "";
        //            }
        //        }
        //        //Next
        //    } catch (Exception ex) {
        //        throw new Exception("", ex);
        //    }
        //}

        //public static bool Delete(ref CostCentreResource GlobalData, int CostCentreID)
        //{

        //    try
        //    {
        //        ////IDAL = DALFactory.CostCentres.CostCentre.Create(GlobalData);
        //        //return IDAL.Delete(CostCentreID);
        //        return _CostCentreRepository.Delete(CostCentreID);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Delete", ex);
        //    }
        //}
        ///// <summary>
        /////     Compile the code with the source frovided (Source provided will be in the form of text string)and generate dll.
        ///// </summary>
        /////         ''' 
        //public static object CompileBinaries(string sOutputPath, string Source, string CompanyName)
        //{
        //    try {
        //        VBCodeProvider csc = new VBCodeProvider();
        //        ICodeCompiler icc = csc.CreateCompiler();
        //        ICodeParser icp = csc.CreateParser();
        //        string errorString = null;

        //        //// Set input params for the compiler
        //        CompilerParameters co = new CompilerParameters();
        //        co.OutputAssembly = sOutputPath + CompanyName + "UserCostCentres.dll";
        //        //co.OutputAssembly = "c:\UserCostCentres.dll"

        //        co.GenerateInMemory = false;


        //        ///'''Dim assemblyFileName As String
        //        ///'''assemblyFileName = [Assembly].GetExecutingAssembly().GetName().CodeBase
        //        ///'''co.ReferencedAssemblies.Add(assemblyFileName.Substring("file:///".Length))
        //        ///'''co.ReferencedAssemblies.Add(sOutputPath + "CostCentreEngine.dll")
        //        ///'''
        //        ///'''

        //        ///////////////////////////////////
        //        //// Add available assemblies - this should be enough
        //        //for /////////the simplest            // applications.
        //        System.Reflection.Assembly oAsm = default(System.Reflection.Assembly);

        //        foreach ( oAsm in AppDomain.CurrentDomain.GetAssemblies) {
        //            //If Not (oAsm.GetType Is System.Reflection.Emit.AssemblyBuilder) Then
        //            //co.ReferencedAssemblies.Add(oAsm.Location)
        //            //End If

        //            if ((!object.ReferenceEquals(oAsm.GetType, typeof(System.Reflection.Emit.AssemblyBuilder)))) {
        //                if (oAsm.Location.Length > 0) {
        //                    co.ReferencedAssemblies.Add(oAsm.Location);
        //                }
        //            }
        //        }

        //        //Dim oFile As New IO.StreamWriter("c:/Test/Test.txt")
        //        //oFile.Write(Source)
        //        //oFile.Close()





        //        //For iCounter As Integer = co.ReferencedAssemblies.Count - 1 To 0 Step -1
        //        //    If co.ReferencedAssemblies.Item(icounter).Trim() = String.Empty Then
        //        //        co.ReferencedAssemblies.RemoveAt(icounter)
        //        //    End If
        //        //Next

        //        //co.ReferencedAssemblies.Add("E:\Development\Infinity\Infinity.UI\bin\Infinity.Componetns.CostCentreLoader.dll")
        //        //co.ReferencedAssemblies.Add("system.data.dll")
        //        //co.ReferencedAssemblies.Add("system.xml.dll")
        //        //co.ReferencedAssemblies.Add("system.drawing.dll")

        //        //co.ReferencedAssemblies.Add(sOutputPath + "ByteFX.Data.dll")
        //        //////////////////////////////////
        //        co.IncludeDebugInformation = true;
        //        //co.TreatWarningsAsErrors = True
        //        //co.CompilerOptions
        //        //co.WarningLevel = 1

        //        //// we want to genereate a DLL
        //        co.GenerateExecutable = false;

        //        //here we will have to retrieve the names of all vb files in binaries folder and pass them to compiler for compilation



        //        //source(0) = "costcentre.vb"
        //        //source(1) = "assemblyinfo.vb"
        //        //source(2) = "assemblyinfo.vb"

        //        //// Run the compiling process

        //        CompilerResults result = icc.CompileAssemblyFromSource(co, Source);


        //        //Dim result As CompilerResults = icc.CompileAssemblyFromSourceBatch(co, source)

        //        if (result.Errors.HasErrors == true) {
        //            int iCounter = 0;
        //            for (iCounter = 0; iCounter <= result.Errors.Count - 1; iCounter++) {
        //                errorString += result.Errors.Item(iCounter).ToString() + Constants.vbCrLf;
        //            }
        //            result = null;
        //            Source = null;
        //            co = null;
        //            throw new Exception("Compilation Errors : " + errorString + "<br><br> Output :");
        //        } else {
        //            result = null;
        //            Source = null;
        //            co = null;
        //        }

        //    } catch (Exception ex) {
        //        throw new Exception(ex.ToString);
        //    }
        //}

        ///// <summary>
        /////     Compile the code and return the errors.
        ///// </summary>
        /////
        //public static CompilerResults CompileSource(string Source)
        //{
        //    try {
        //        VBCodeProvider csc = new VBCodeProvider();
        //        ICodeCompiler icc = csc.CreateCompiler();
        //        ICodeParser icp = csc.CreateParser();
        //        string errorString = null;

        //        //// Set input params for the compiler
        //        CompilerParameters co = new CompilerParameters();
        //        //co.OutputAssembly = "c:\UserCostCentres.dll"

        //        co.GenerateInMemory = true;


        //        ///'''Dim assemblyFileName As String
        //        ///'''assemblyFileName = [Assembly].GetExecutingAssembly().GetName().CodeBase
        //        ///'''co.ReferencedAssemblies.Add(assemblyFileName.Substring("file:///".Length))
        //        ///'''co.ReferencedAssemblies.Add(sOutputPath + "CostCentreEngine.dll")
        //        ///'''
        //        ///'''

        //        ///////////////////////////////////
        //        //// Add available assemblies - this should be enough
        //        //for /////////the simplest            // applications.
        //        System.Reflection.Assembly oAsm = default(System.Reflection.Assembly);

        //        foreach ( oAsm in AppDomain.CurrentDomain.GetAssemblies) {
        //            //If Not (oAsm.GetType Is System.Reflection.Emit.AssemblyBuilder) Then
        //            //co.ReferencedAssemblies.Add(oAsm.Location)
        //            //End If

        //            if ((!object.ReferenceEquals(oAsm.GetType, typeof(System.Reflection.Emit.AssemblyBuilder)))) {
        //                co.ReferencedAssemblies.Add(oAsm.Location);
        //            }

        //        }

        //        for (int iCounter = co.ReferencedAssemblies.Count - 1; iCounter >= 0; iCounter += -1) {
        //            if (co.ReferencedAssemblies.Item(iCounter).Trim() == string.Empty) {
        //                co.ReferencedAssemblies.RemoveAt(iCounter);
        //            }
        //        }

        //        //co.ReferencedAssemblies.Add("E:\Development\Infinity\Infinity.UI\bin\Infinity.Componetns.CostCentreLoader.dll")
        //        //co.ReferencedAssemblies.Add("system.data.dll")
        //        //co.ReferencedAssemblies.Add("system.xml.dll")
        //        //co.ReferencedAssemblies.Add("system.drawing.dll")

        //        //co.ReferencedAssemblies.Add(sOutputPath + "ByteFX.Data.dll")
        //        //////////////////////////////////
        //        co.IncludeDebugInformation = false;
        //        //co.TreatWarningsAsErrors = True
        //        //co.CompilerOptions
        //        //co.WarningLevel = 1

        //        //// we want to genereate a DLL
        //        co.GenerateExecutable = false;

        //        //here we will have to retrieve the names of all vb files in binaries folder and pass them to compiler for compilation



        //        //source(0) = "costcentre.vb"
        //        //source(1) = "assemblyinfo.vb"
        //        //source(2) = "assemblyinfo.vb"

        //        //// Run the compiling process

        //        CompilerResults result = icc.CompileAssemblyFromSource(co, Source);


        //        //Dim result As CompilerResults = icc.CompileAssemblyFromSourceBatch(co, source)

        //        return result;
        //    } catch (Exception ex) {
        //        throw new Exception(ex.ToString);
        //    }
        //}

        //public static void CopyCostCentre(int CostCentreID, ref CostCentreResource g_GlobalData, ref int oCostCentreID)
        //{
        //    try {
        //        CostCentre oCostCentre = BLL.CostCentres.CostCentre.GetCostCentreByID(CostCentreID, g_GlobalData, true);
        //        oCostCentre.Name = "Copy of (" + oCostCentre.Name + ")";

        //        string[] CompleteCode = null;

        //        CompleteCode = Strings.Split(oCostCentre.CompleteCode, "Private Const SetupCost As Double");

        //        if (CompleteCode.Length == 2) {
        //            //oCostCentre.CostCentreID = BLL.CostCentres.CostCentre.GetMaxCostCentreID(g_GlobalData);
        //            oCostCentre.CompleteCode = "Namespace UserCostCentres" + Constants.vbCrLf;
        //            oCostCentre.CompleteCode += "Public Class copyof" + oCostCentre.CodeFileName + Constants.vbCrLf;
        //            oCostCentre.CompleteCode += "Inherits MarshalByRefObject" + Constants.vbCrLf;
        //            oCostCentre.CompleteCode += "Implements Infinity.Model.CostCentres.ICostCentreLoader" + Constants.vbCrLf;
        //            oCostCentre.CompleteCode += "Private Const CostCentreID As String = \"" + (oCostCentre.CostCentreID + 1).ToString + "\"" + Constants.vbCrLf;
        //            oCostCentre.CompleteCode += "Private Const SetupCost As Double";
        //            oCostCentre.CompleteCode += CompleteCode(1);

        //            //BLL.CostCentres.CostCentre.InsertUserDefinedCostCentre(g_GlobalData, oCostCentre);
        //            //BLL.CostCentres.CostCentre.UpdateWorkinstructions(g_GlobalData, oCostCentre.WorkInstructionsList, oCostCentre.CostCentreID, true);

        //            int TotalCount = oCostCentre.CostCentreResources.Rows.Count;
        //            for (int i = TotalCount - 1; i >= 0; i += -1) {
        //                DataRow oRRow = oCostCentre.CostCentreResources.NewRow();
        //                oRRow("CostCentreID") = oCostCentre.CostCentreID;
        //                oRRow("ResourceID") = oCostCentre.CostCentreResources.Rows(i)("ResourceID");
        //                oCostCentre.CostCentreResources.Rows.Add(oRRow);
        //            }
        //            //BLL.CostCentres.CostCentre.ADDUpdateCostCentreResources(oCostCentre.CostCentreResources, g_GlobalData);


        //            CostCentreID = oCostCentre.CostCentreID;

        //            //Get Complete Code of the CostCentre from the DB and Recompile it with the new changes
        //            DataTable oTable = null;//BLL.CostCentres.CostCentre.GetCompleteCodeofAllCostCentres(g_GlobalData, g_GlobalData.CompanyDetails.CompanyID);
        //            DataRow oRow = default(DataRow);


        //            string oSource = "";
        //            string oCompanyName = g_GlobalData.CompanyDetails.CompanyName;

        //            //replacing any unwanted characters in the CompanyNames and create file name
        //            oCompanyName = oCompanyName.Replace(" ", "");
        //            oCompanyName = oCompanyName.Replace("'", "");
        //            oCompanyName = oCompanyName.Replace("+", "");
        //            oCompanyName = oCompanyName.Replace("-", "");
        //            oCompanyName = oCompanyName.Replace("*", "");
        //            oCompanyName = oCompanyName.Replace("/", "");
        //            oCompanyName = oCompanyName.Replace("\"", "");
        //            oCompanyName = oCompanyName.Replace("?", "");
        //            oCompanyName = oCompanyName.Replace("<", "");
        //            oCompanyName = oCompanyName.Replace(">", "");
        //            oCompanyName = oCompanyName.Replace("(", "");
        //            oCompanyName = oCompanyName.Replace(")", "");
        //            oCompanyName = oCompanyName.Replace("{", "");
        //            oCompanyName = oCompanyName.Replace("}", "");
        //            oCompanyName = oCompanyName.Replace("[", "");
        //            oCompanyName = oCompanyName.Replace("]", "");
        //            oCompanyName = oCompanyName.Replace("|", "");
        //            oCompanyName = oCompanyName.Replace("&", "");
        //            oCompanyName = oCompanyName.Replace("^", "");
        //            oCompanyName = oCompanyName.Replace("%", "");
        //            oCompanyName = oCompanyName.Replace("$", "");
        //            oCompanyName = oCompanyName.Replace("#", "");
        //            oCompanyName = oCompanyName.Replace("@", "");
        //            oCompanyName = oCompanyName.Replace("!", "");
        //            oCompanyName = oCompanyName.Replace("`", "");
        //            oCompanyName = oCompanyName.Replace("~", "");
        //            oCompanyName = oCompanyName.Replace("\\", "");
        //            oCompanyName = oCompanyName.Replace(";", "");
        //            oCompanyName = oCompanyName.Replace(":", "");
        //            oCompanyName = oCompanyName.Replace(",", "");
        //            oCompanyName = oCompanyName.Replace(".", "");

        //            oSource += "Imports Infinity" + Constants.vbCrLf;
        //            oSource += "Imports System" + Constants.vbCrLf;
        //            oSource += "Imports System.Data" + Constants.vbCrLf;
        //            oSource += "Imports Microsoft.VisualBasic" + Constants.vbCrLf;
        //            oSource += System.Configuration.ConfigurationSettings.AppSettings("DALProviderNameSpace") + Constants.vbCrLf;
        //            oSource += "Imports Infinity.Bll.CostCentres" + Constants.vbCrLf;
        //            oSource += "imports Infinity.Model.CostCentres" + Constants.vbCrLf;
        //            oSource += "Imports System.Reflection" + Constants.vbCrLf;

        //            oSource += "Namespace UserCostCentres" + Constants.vbCrLf;


        //            foreach ( oRow in oTable.Rows) {
        //                string Str = "";
        //                Str = oRow(1).ToString;
        //                Str = Strings.Replace(Str, "Namespace UserCostCentres", "");
        //                Str = Strings.Replace(Str, "End Namespace", "");

        //                oSource += Constants.vbCrLf + Str + Constants.vbCrLf;

        //            }

        //            oSource += "End Namespace" + Constants.vbCrLf;

        //            //'if compilation fails then delete the code file
        //            System.IO.File CostCentreDLL = default(System.IO.File);
        //            IO.FileStream oFileStream = default(IO.FileStream);

        //            bool IsCompiled = true;

        //            try {
        //                //Compile Code of CostCentres
        //            //	BLL.CostCentres.CostCentre.CompileBinaries(Application.StartupPath.ToString + "\\ccAssembly\\", oSource, oCompanyName);

        //                //Get CostCentre File Open it in Read Mode
        //                oFileStream = CostCentreDLL.OpenRead(Application.StartupPath.ToString + "\\ccAssembly\\" + oCompanyName + "UserCostCentres.dll");

        //                //Get Byte Array of the file and write it in the db
        //                byte[] CostCentreByte = new byte[Convert.ToInt32(oFileStream.Length - 1) + 1];

        //                oFileStream.Read(CostCentreByte, 0, Convert.ToInt32(oFileStream.Length - 1));
        //                BLL.Companies.Company.ChangeCostCentreExecution(g_GlobalData, Now, CostCentreByte, g_GlobalData.CompanyDetails.CompanyID);
        //                CostCentreByte = null;
        //            } catch (Exception ex) {
        //                //    BLL.CostCentres.CostCentre.DeleteCodeFile(sCostCentreFileName, Application.StartupPath.ToString + "\binaries\")
        //                IsCompiled = false;
        //                throw new Exception("Error Compiling Costcentre", ex);

        //            } finally {
        //                CostCentreDLL = null;
        //                oFileStream = null;

        //            }

        //            //Update Registry
        //            Microsoft.win32.RegistryKey Software = default(Microsoft.win32.RegistryKey);
        //            Microsoft.win32.RegistryKey Clydo = default(Microsoft.win32.RegistryKey);
        //            Microsoft.win32.RegistryKey InfinityDesktop = default(Microsoft.win32.RegistryKey);
        //            Microsoft.win32.RegistryKey Client = default(Microsoft.win32.RegistryKey);

        //            Software = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE", true);
        //            Clydo = Software.OpenSubKey("Clydo", true);

        //            if (Clydo == null) {
        //                Clydo = Software.CreateSubKey("Clydo");
        //            }

        //            InfinityDesktop = Clydo.OpenSubKey("Infinity Desktop", true);
        //            if (InfinityDesktop == null) {
        //                InfinityDesktop = Clydo.CreateSubKey("Infinity Desktop");
        //            }

        //            Client = InfinityDesktop.OpenSubKey("Client", true);
        //            if (Client == null) {
        //                Client = InfinityDesktop.CreateSubKey("Client");
        //            }

        //            Client.SetValue("CostCentre Updation Date", Now);

        //        } else {
        //            MessageBox.Show("Invalid Costcentre to copy.", "Infinity", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }

        //    } catch (Exception ex) {
        //        throw new Exception("Copy CC", ex);

        //    }
        //}
        ///// <summary>
        /////     A Data table which will return a datatable of costcentre sequences
        ///// </summary>
        /////         ''' 
        //public static DataTable LoadCostCentreSequence()
        //{
        //    DataTable oDataTable = new DataTable();
        //    DataColumn oColoumnID = new DataColumn("ID");
        //    DataColumn oColoumnName = new DataColumn("Name");

        //    try
        //    {
        //        oDataTable.Columns.Add(oColoumnID);
        //        oDataTable.Columns.Add(oColoumnName);

        //        DataRow oRow = default(DataRow);
        //        oRow = oDataTable.NewRow;
        //        oRow(0) = 1;
        //        oRow(1) = "Pre Press";
        //        oDataTable.Rows.Add(oRow);

        //        oRow = oDataTable.NewRow;
        //        oRow(0) = 2;
        //        oRow(1) = "Post Press";
        //        oDataTable.Rows.Add(oRow);

        //        oRow = oDataTable.NewRow;
        //        oRow(0) = 3;
        //        oRow(1) = "Both";
        //        oDataTable.Rows.Add(oRow);

        //        return oDataTable;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("LoadCostCentreSequence", ex);
        //    }
        //    finally
        //    {
        //        oDataTable = null;
        //        oColoumnID = null;
        //        oColoumnName = null;
        //    }
        //}

        ///// <summary>
        /////     Return Cost Centre  Calculation methods datatable having (Fixed,PerHour,Per Quantity,Formula base)
        ///// </summary>
        /////         ''' 
        //public static DataTable LoadCalculationMethodType()
        //{
        //    DataTable oDataTable = new DataTable();
        //    DataColumn oColoumnID = new DataColumn("ID");
        //    DataColumn oColoumnName = new DataColumn("Name");
        //    try
        //    {
        //        oDataTable.Columns.Add(oColoumnID);
        //        oDataTable.Columns.Add(oColoumnName);

        //        DataRow oRow = default(DataRow);
        //        oRow = oDataTable.NewRow;
        //        oRow(0) = 1;
        //        oRow(1) = "Fixed";
        //        oDataTable.Rows.Add(oRow);

        //        oRow = oDataTable.NewRow;
        //        oRow(0) = 2;
        //        oRow(1) = "Per Hour Selling Rate";
        //        oDataTable.Rows.Add(oRow);

        //        oRow = oDataTable.NewRow;
        //        oRow(0) = 3;
        //        oRow(1) = "Per Quantity Base";
        //        oDataTable.Rows.Add(oRow);

        //        oRow = oDataTable.NewRow;
        //        oRow(0) = 4;
        //        oRow(1) = "Formulae Base ( Advance )";
        //        oDataTable.Rows.Add(oRow);

        //        return oDataTable;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Load Calculation Method Type", ex);
        //    }
        //    finally
        //    {
        //        oDataTable = null;
        //        oColoumnID = null;
        //        oColoumnName = null;
        //    }
        //}



        ///// <summary>
        /////     Get Max CostCentreID
        ///// </summary>
        /////         ''' 
        //public static int GetMaxCostCentreID(ref Model.ApplicationSettings.GlobalData GlobalData)
        //{
        //    try
        //    {
        //        //IDAL = DALFactory.CostCentres.CostCentre.Create(GlobalData);
        //        return 0;//IDAL.GetMaxCostCentreID;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("GetMaxCostCentreID", ex);
        //    }
        //}

        ///// <summary>
        ///// Get CostCentre Model By ID
        ///// </summary>
        ///// <param name="CostCentreID"></param>
        ///// <returns>Costcentre</returns>
        //public static CostCentre GetCostCentreByID(int CostCentreID, ref Model.ApplicationSettings.GlobalData GlobalData, bool Complete = true)
        //{

        //    try
        //    {
        //       // IDAL = DALFactory.CostCentres.CostCentre.Create(GlobalData);
        //        return null; //IDAL.GetCostCentreByID(CostCentreID, Complete);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("GetCostCentreSummaryByID", ex);
        //    }
        //}


        ///// <summary>
        ///// Get CostCentre Summary it will not return complete costcentre model, Its for summary costcentres
        ///// </summary>
        ///// <param name="CostCentreID"></param>
        ///// <returns>Costcentre</returns>
        //public static CostCentre GetCostCentreSummaryByID(int CostCentreID, ref Model.ApplicationSettings.GlobalData GlobalData)
        //{

        //    try
        //    {
        //        //IDAL = DALFactory.CostCentres.CostCentre.Create(GlobalData);
        //        return null;// IDAL.GetCostCentreSummaryByID(CostCentreID);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("GetCostCentreSummaryByID", ex);
        //    }
        //}

        ///// <summary>
        /////     It will not return complete costcentre object but it will return System Costcentre Parameters only
        ///// </summary>
        /////         ''' 
        //public static CostCentre GetSystemCostCentre(int SystemTypeID, ref Model.ApplicationSettings.GlobalData GlobalData)
        //{

        //    try
        //    {
        //       // IDAL = DALFactory.CostCentres.CostCentre.Create(GlobalData);
        //        return null;// IDAL.GetSystemCostCentre(SystemTypeID);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("GetCostCentreSummaryByID", ex);
        //    }
        //}

        ///// <summary>
        /////     Return DataSet of CostCentre
        ///// </summary>
        /////         ''' 
        //public static DataSet GetCostCentreListDataset(ref Model.ApplicationSettings.GlobalData GlobalData, int CategoryID = 0, string SearchString = "", bool FromList = false)
        //{

        //    try
        //    {
        //        IDAL = DALFactory.CostCentres.CostCentre.Create(GlobalData);

        //        return IDAL.GetCostCentreListDataSet(CategoryID, Model.Common.FilterInputString(SearchString), FromList);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Get CostCentre List", ex);
        //    }
        //}

        ///// <summary>
        /////     Get Cost Centre List
        ///// </summary>
        /////         ''' 
        //public static DataTable GetCostCentreList(ref Model.ApplicationSettings.GlobalData GlobalData)
        //{

        //    try
        //    {
        //        IDAL = DALFactory.CostCentres.CostCentre.Create(GlobalData);

        //        return IDAL.GetCostCentreList;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Get CostCentre List", ex);
        //    }
        //}

        ///// <summary>
        /////     Add Update Cost centre Resources table using adapter.
        ///// </summary>
        /////         ''' 
        //public static bool ADDUpdateCostCentreResources(ref DataTable oDataTable, ref Model.ApplicationSettings.GlobalData GlobalData)
        //{
        //    try
        //    {
        //        IDAL = DALFactory.CostCentres.CostCentre.Create(GlobalData);
        //        return IDAL.ADDUpdateCostCentreResources(oDataTable);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("ADD Update costCentre Resources", ex);
        //    }

        //}

        ///// <summary>
        /////     Get Cost Centre Resources table.
        ///// </summary>
        /////         ''' 
        //public static DataTable GetCostCentreResources(int CostCentreID, ref Model.ApplicationSettings.GlobalData GlobalData)
        //{
        //    try
        //    {
        //        IDAL = DALFactory.CostCentres.CostCentre.Create(GlobalData);
        //        return IDAL.GetCostCentreResources(CostCentreID);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Get Resources", ex);
        //    }
        //}

        ///// <summary>
        /////     Update Only System cost centre attributes in the costcentres
        ///// </summary>
        /////         ''' 
        //public static bool UpdateSystemCostCentre(int CostCentreID, int ProfitMarginID, string NominalCode, double MinCost, int UserID, string Description, bool DirectCost, bool IsScheduleable, ref Model.ApplicationSettings.GlobalData GlobalData)
        //{
        //    try
        //    {
        //        IDAL = DALFactory.CostCentres.CostCentre.Create(GlobalData);
        //        return IDAL.UpdateSystemCostCentre(CostCentreID, ProfitMarginID, NominalCode, MinCost, UserID, Description, DirectCost, IsScheduleable);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Get Resources", ex);
        //    }
        //}


        ///// <summary>
        ///// Get Cost Centre Types
        ///// </summary>
        ///// <param name="oConnection"></param>
        ///// <param name="ReturnMode"></param>
        ///// <returns></returns>
        //public static DataTable GetCostCentreTypes(ref Model.ApplicationSettings.GlobalData oGlobalData, Model.CostCentres.TypeReturnMode ReturnMode)
        //{
        //    try
        //    {
        //        IDAL = DALFactory.CostCentres.CostCentre.Create(oGlobalData);
        //        return IDAL.GetCostCentreTypes(ReturnMode);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("", ex);
        //    }
        //}


        ///// <summary>
        ///// Get Cost Centre System Types
        ///// </summary>
        ///// <param name="oConnection"></param>
        ///// <param name="ReturnMode"></param>
        ///// <returns></returns>
        //public static DataTable GetCostCentreSystemTypes(ref Model.ApplicationSettings.GlobalData oGlobalData)
        //{
        //    try
        //    {
        //        IDAL = DALFactory.CostCentres.CostCentre.Create(oGlobalData);
        //        return IDAL.GetCostCentreSystemTypes();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("", ex);
        //    }
        //}

        ////


        ///// <summary>
        /////     Load Pre Defined Cost Centre Template in DB
        ///// </summary>
        /////         ''' 
        //public static Model.CostCentres.CostCentreTemplateDTO LoadCostCentreTemplate(string TemplateID, Model.ApplicationSettings.GlobalData oGlobalData)
        //{
        //    try
        //    {
        //        IDAL = DALFactory.CostCentres.CostCentre.Create(oGlobalData);
        //        return IDAL.LoadCostCentreTemplate(TemplateID);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("LoadCostCentreTemplate", ex);
        //    }
        //}

        ///// <summary>
        /////     Get WorkInstruction of a costcentre in a dataset
        ///// </summary>
        /////         ''' 
        //public static DataSet GetCostCentreWorkInstruction(int CostcentreID, Model.ApplicationSettings.GlobalData oGlobalData)
        //{
        //    try
        //    {
        //        IDAL = DALFactory.CostCentres.CostCentre.Create(oGlobalData);
        //        return IDAL.GetCostCentreWorkInstruction(CostcentreID);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("GetCostCentreWorkInstruction", ex);
        //    }
        //}


        ///// <summary>
        /////     Return Cost Centre Categories Datatable
        ///// </summary>
        /////         ''' 
        //public static DataTable ReturnCostCentreCategories(Model.ApplicationSettings.GlobalData oGlobalData)
        //{
        //    try
        //    {
        //        IDAL = DALFactory.CostCentres.CostCentre.Create(oGlobalData);
        //        return IDAL.ReturnCostCentreCategories;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("ReturnCostCentreCategories", ex);
        //    }
        //}

        ///// <summary>
        /////     Check Cost centre Names
        ///// </summary>
        /////         ''' 
        //public static bool CheckCostCentreName(ref Model.ApplicationSettings.GlobalData oGlobalData, int CostCentreID, string CostCentreName)
        //{
        //    try
        //    {
        //        IDAL = DALFactory.CostCentres.CostCentre.Create(oGlobalData);
        //        return IDAL.CheckCostCentreName(CostCentreID, CostCentreName);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Check CostCentre Name", ex);
        //    }
        //}

        ///// <summary>
        /////     Update Work Instruction by Dataset
        ///// </summary>
        /////         ''' 
        //public static bool UpdateWorkinstructions(ref Model.ApplicationSettings.GlobalData oGlobaldata, ref DataSet oDataset, int CostCentreID, bool IsCopy = false)
        //{
        //    try
        //    {
        //        IDAL = DALFactory.CostCentres.CostCentre.Create(oGlobaldata);
        //        return IDAL.UpdateWorkinstructions(oDataset, CostCentreID, IsCopy);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Check CostCentre Name", ex);
        //    }
        //}

        ///// <summary>
        /////     It will update complete costcentre model.
        ///// </summary>
        /////         ''' 
        //public static bool UpdateUserDefinedCostCentre(ref Model.ApplicationSettings.GlobalData oGlobaldata, Model.CostCentres.CostCentreDTO oCostCentre)
        //{
        //    try
        //    {
        //        IDAL = DALFactory.CostCentres.CostCentre.Create(oGlobaldata);
        //        return IDAL.UpdateUserDefinedCostCentre(oCostCentre);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("UpdateUserDefinedCostCentre", ex);
        //    }
        //}

        ///// <summary>
        /////     Inserts complete costcentre model.
        ///// </summary>
        /////         ''' 
        //public static int InsertUserDefinedCostCentre(ref Model.ApplicationSettings.GlobalData oGlobaldata, Model.CostCentres.CostCentreDTO oCostCentre)
        //{
        //    try
        //    {
        //        IDAL = DALFactory.CostCentres.CostCentre.Create(oGlobaldata);
        //        return IDAL.InsertUserDefinedCostCentre(oCostCentre);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("InsertUserDefineCostCentre", ex);
        //    }
        //}

        ///// <summary>
        /////     It will return only name,Description,Type and file name of the costcentre object
        ///// </summary>
        /////         ''' 
        //public static Model.CostCentres.CostCentreDTO LoadCostCentreHeader(int CostCentreID, ref Model.ApplicationSettings.GlobalData GlobalData)
        //{
        //    try
        //    {
        //        IDAL = DALFactory.CostCentres.CostCentre.Create(GlobalData);
        //        return IDAL.LoadCostCentreHeader(CostCentreID);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("LoadCostCentreHeader", ex);
        //    }
        //}

        ///// <summary>
        /////     Get Cost Centre resources with names
        ///// </summary>
        /////         ''' 
        //public static DataTable GetCostCentreResourcesWithNames(int CostcentreID, ref Model.ApplicationSettings.GlobalData GlobalData)
        //{
        //    try
        //    {
        //        IDAL = DALFactory.CostCentres.CostCentre.Create(GlobalData);
        //        return IDAL.GetCostCentreResourcesWithNames(CostcentreID);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("GetCostCentreResourcesWithNames", ex);
        //    }
        //}

        ///// <summary>
        /////    A Datatable with only code attribute will return a complete code of one site
        ///// </summary>
        /////         ''' 
        //public static DataTable GetCompleteCodeofAllCostCentres(ref Model.ApplicationSettings.GlobalData GlobalData, int CompanyID)
        //{
        //    try
        //    {
        //        IDAL = DALFactory.CostCentres.CostCentre.Create(GlobalData);
        //        return IDAL.GetCompleteCodeofAllCostCentres(CompanyID);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("GetCompleteCodeofAllCostCentres", ex);
        //    }
        //}

        ///// <summary>
        /////     Change flag of the costcentre
        ///// </summary>
        /////         ''' 
        //public static bool ChangeFlag(ref Model.ApplicationSettings.GlobalData GlobalData, int FlagID, int CostCentreID)
        //{
        //    try
        //    {
        //        IDAL = DALFactory.CostCentres.CostCentre.Create(GlobalData);
        //        return IDAL.ChangeFlag(FlagID, CostCentreID);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("ChangeFlag", ex);
        //    }
        //}

        ///// <summary>
        /////   compares the costcentre dates of registry and database, and if there is differnet then gets the new DLL from DB and write it
        ///// </summary>
        /////         ''' 
        //public static int CheckCostCentresVersion(Model.ApplicationSettings.GlobalData GlobalData)
        //{
        //    try
        //    {
        //        IDAL = DALFactory.CostCentres.CostCentre.Create(GlobalData);
        //        return IDAL.CheckCostCentresVersion();

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("CheckCostCentresVersion", ex);
        //    }

        //}

        //public static DataTable GetCostCentreCategories(ref Model.ApplicationSettings.GlobalData GlobalData)
        //{
        //    try
        //    {
        //        IDAL = DALFactory.CostCentres.CostCentre.Create(GlobalData);
        //        return IDAL.GetCostCentreCategories();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("GetCostCentreCategories", ex);
        //    }
        //}

        //public static bool UpdateCostCentreCategories(ref Model.ApplicationSettings.GlobalData GlobalData, ref DataTable oTable)
        //{
        //    try
        //    {
        //        IDAL = DALFactory.CostCentres.CostCentre.Create(GlobalData);
        //        return IDAL.UpdateCostCentreCategories(oTable);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("UpdateCostCentreCategories", ex);
        //    }
        //}

        //public static bool IsCostCentreAvailable(ref Model.ApplicationSettings.GlobalData GlobalData, int CategoryID)
        //{
        //    try
        //    {
        //        IDAL = DALFactory.CostCentres.CostCentre.Create(GlobalData);
        //        return IDAL.IsCostCentreAvailable(CategoryID);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("IsCostCentreAvailable", ex);
        //    }
        //}

    }
}
