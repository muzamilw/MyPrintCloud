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
        public string TokenParse(string sText)
        {
            try {
                string ChangedStr = "";
                string[] oBeforeStr = sText.Split('{');
                ChangedStr = oBeforeStr[0];
                if (oBeforeStr.Length > 1) {
                    for (int i = 1; i <= oBeforeStr.Length - 1; i++) {
                        string[] oAfterStr = oBeforeStr[i].Split( '}');
                        if (oAfterStr.Length == 2) {
                            string oReplaceStr = ReplaceTag(oAfterStr[0]);
                            if (!string.IsNullOrEmpty(oReplaceStr)) {
                                if (oAfterStr[1].Split('}').Length > 1)
                                {
                                    throw new Exception("Invalid Calculation String.");
                                    return "";
                                }
                                ChangedStr += oReplaceStr + oAfterStr[1];
                            } else {
                                return "";
                            }
                        } else {
                            throw new Exception("Invalid Calculation String.");
                            return "";
                        }
                    }
                } else {
                    string[] oAfterStr = sText.Split('}');
                    if (oAfterStr.Length > 1) {
                        throw new Exception("Invalid Tag.");
                        return "";
                    }
                }
                return ChangedStr;
            } catch (Exception ex) {
                throw new Exception("Token Parse", ex);
            }
        }
        /// <summary>
        /// Replace Tags which we use in our visual code with the functions.
        /// </summary>
        /// <param name="sText"></param>
        /// <returns></returns>
        public string ReplaceTag(string sText)
        {
            try {
                string[] oSpiltTokens = sText.Split(',');
                // For i As Integer = 0 To oSpiltTokens.Length - 1

                if (oSpiltTokens.Length > 1) {

                    string[] GetID = oSpiltTokens[1].Split('\"');
                    string[] GetReturnValue = null;
                    string[] GetValue = null;

                    switch (oSpiltTokens[0].ToLower()) {
                        

                        case "systemvariable":
                             GetID = oSpiltTokens[1].Split('\"');
                            if (GetID.Length == 3) {
                                return " BLL.CostCentres.CostCentreExecution.ExecuteVariable(ParamsArray ,\"" + GetID[1] + "\") ";
                            } else {
                                throw new Exception("Invalid Calculation String.");
                                return "";
                            }

                            break;
                        case "question":
                             GetID = oSpiltTokens[1].Split('\"');
                            if (GetID.Length == 3) {
                                return " BLL.CostCentres.CostCentreExecution.ExecuteQuestion(ParamsArray,\"" + GetID[1] + "\",CostCentreID) ";
                            } else {
                                throw new Exception("Invalid Calculation String.");
                                return "";
                            }

                            break;
                        case "subcostcentre":

                            GetID = oSpiltTokens[1].Split( '\"');
                            GetReturnValue = oSpiltTokens[3].Split('\"');
                            if (GetID.Length == 3) {
                                return " BLL.CostCentres.CostCentreExecution.ExecuteCostCentre(ParamsArray,\"" + GetID[1] + "\",\"" + GetReturnValue[1] + "\") ";
                            } else {
                                throw new Exception("Invalid Calculation String.");
                                return "";
                            }

                            break;
                        case "resource":

                            GetID = oSpiltTokens[1].Split('\"');
                            GetReturnValue = oSpiltTokens[3].Split( '\"');
                            if (GetID.Length == 3 & GetReturnValue.Length == 3) {
                                return " BLL.CostCentres.CostCentreExecution.ExecuteResource(ParamsArray,\"" + GetID[1] + "\",\"" + GetReturnValue[1] + "\") ";
                            } else {
                                throw new Exception("Invalid Calculation String.");
                                return "";
                            }

                            break;
                        case "stock":

                            GetID = oSpiltTokens[1].Split( '\"');
                            string[] GetName = oSpiltTokens[2].Split( '\"');
                            string[] GetQType = oSpiltTokens[3].Split( '\"');
                            //Like per unit , per package
                            string[] GetQtyType = oSpiltTokens[4].Split( '\"');
                            //Qty,Variable or string
                            GetValue = oSpiltTokens[5].Split( '\"');

                            if (((GetID.Length == 3 & GetName.Length == 3) & (GetQType.Length == 3 & GetQtyType.Length == 3) & GetValue.Length == 3)) {
                                return " BLL.CostCentres.CostCentreExecution.ExecuteStockItem(ParamsArray,\"" + GetID[1] + "\",\"" + GetName[1] + "\",\"" + GetQtyType[1] + "\",\"" + GetValue[1] + "\",\"" + GetQType[1] + "\",cint(CostCentreID)) ";
                            } else {
                                throw new Exception("Invalid Calculation String.");
                                return "";
                            }

                            break;
                        case "matrix":

                            GetID = oSpiltTokens[1].Split( '\"');
                            if (GetID.Length == 3) {
                                return " BLL.CostCentres.CostCentreExecution.ExecuteMatrix(ParamsArray,\"" + GetID[1] + "\",cint(CostCentreID)) ";
                            } else {
                                throw new Exception("Invalid Calculation String.");
                                return "";
                            }

                            break;
                        case "cinput":

                            GetID = oSpiltTokens[1].Split( '\"');
                            string[] GetQuestion = oSpiltTokens[2].Split( '\"');
                            string[] GetTypes = oSpiltTokens[3].Split( '\"');
                            string[] GetInputTypes = oSpiltTokens[4].Split( '\"');
                            GetValue = oSpiltTokens[5].Split('\"');

                            if ((((GetID.Length == 3 & GetQuestion.Length == 3) & (GetTypes.Length == 3 & GetValue.Length == 3)) & GetInputTypes.Length == 3)) {
                                return " BLL.CostCentres.CostCentreExecution.ExecuteInput(ParamsArray,\"" + GetID[1] + "\",\"" + GetQuestion[1] + "\"," + GetTypes[1] + "," + GetInputTypes[1] + ",\"" + GetValue[1] + "\",cint(CostCentreID)) ";
                            } else {
                                throw new Exception("Invalid Calculation String.");
                                return "";
                            }

                            break;
                        default:
                            throw new Exception("Invalid Calculation String.");
                            return "";
                    }
                }
                else
                {
                    return "";
                }
                //Next
            } catch (Exception ex) {
                throw new Exception("", ex);
            }
        }

        public bool Delete(ref CostCentreResource GlobalData, int CostCentreID)
        {

            try
            {
                return _CostCentreRepository.Delete(CostCentreID);

            }
            catch (Exception ex)
            {
                throw new Exception("Delete", ex);
            }
        }
        /// <summary>
        ///     Compile the code with the source frovided (Source provided will be in the form of text string)and generate dll.
        /// </summary>
        ///         ''' 
        public object CompileBinaries(string sOutputPath, string Source, string CompanyName)
        {
            try {
                VBCodeProvider csc = new VBCodeProvider();
                ICodeCompiler icc = csc.CreateCompiler();
                ICodeParser icp = csc.CreateParser();
                string errorString = null;

                //// Set input params for the compiler
                CompilerParameters co = new CompilerParameters();
                co.OutputAssembly = sOutputPath + CompanyName + "UserCostCentres.dll";
                //co.OutputAssembly = "c:\UserCostCentres.dll"

                co.GenerateInMemory = false;


                ///'''Dim assemblyFileName As String
                ///'''assemblyFileName = [Assembly].GetExecutingAssembly().GetName().CodeBase
                ///'''co.ReferencedAssemblies.Add(assemblyFileName.Substring("file:///".Length))
                ///'''co.ReferencedAssemblies.Add(sOutputPath + "CostCentreEngine.dll")
                ///'''
                ///'''

                ///////////////////////////////////
                //// Add available assemblies - this should be enough
                //for /////////the simplest            // applications.
                //System.Reflection.Assembly oAsm = default(System.Reflection.Assembly);

               

                foreach (var oAsm in AppDomain.CurrentDomain.GetAssemblies()) {
                    //If Not (oAsm.GetType Is System.Reflection.Emit.AssemblyBuilder) Then
                    //co.ReferencedAssemblies.Add(oAsm.Location)
                    //End If

                    if ((!object.ReferenceEquals(oAsm.GetType(), typeof(System.Reflection.Emit.AssemblyBuilder)))) {
                        if (oAsm.Location.Length > 0) {
                            co.ReferencedAssemblies.Add(oAsm.Location);
                        }
                    }
                }

                //Dim oFile As New IO.StreamWriter("c:/Test/Test.txt")
                //oFile.Write(Source)
                //oFile.Close()





                //For iCounter As Integer = co.ReferencedAssemblies.Count - 1 To 0 Step -1
                //    If co.ReferencedAssemblies.Item(icounter).Trim() = String.Empty Then
                //        co.ReferencedAssemblies.RemoveAt(icounter)
                //    End If
                //Next

                //co.ReferencedAssemblies.Add("E:\Development\Infinity\Infinity.UI\bin\Infinity.Componetns.CostCentreLoader.dll")
                //co.ReferencedAssemblies.Add("system.data.dll")
                //co.ReferencedAssemblies.Add("system.xml.dll")
                //co.ReferencedAssemblies.Add("system.drawing.dll")

                //co.ReferencedAssemblies.Add(sOutputPath + "ByteFX.Data.dll")
                //////////////////////////////////
                co.IncludeDebugInformation = true;
                //co.TreatWarningsAsErrors = True
                //co.CompilerOptions
                //co.WarningLevel = 1

                //// we want to genereate a DLL
                co.GenerateExecutable = false;

                //here we will have to retrieve the names of all vb files in binaries folder and pass them to compiler for compilation



                //source(0) = "costcentre.vb"
                //source(1) = "assemblyinfo.vb"
                //source(2) = "assemblyinfo.vb"

                //// Run the compiling process

                CompilerResults result = icc.CompileAssemblyFromSource(co, Source);


                //Dim result As CompilerResults = icc.CompileAssemblyFromSourceBatch(co, source)

                if (result.Errors.HasErrors == true) {
                    int iCounter = 0;
                    for (iCounter = 0; iCounter <= result.Errors.Count - 1; iCounter++) {
                        errorString += result.Errors[iCounter].ToString() + Environment.NewLine;
                    }
                    result = null;
                    Source = null;
                    co = null;
                    throw new Exception("Compilation Errors : " + errorString + "<br><br> Output :");
                } else {
                    result = null;
                    Source = null;
                    co = null;
                }
                return null;

            } catch (Exception ex) {
                throw ex;
            }
        }

        /// <summary>
        ///     Compile the code and return the errors.
        /// </summary>
        ///
        public CompilerResults CompileSource(string Source)
        {
            try {
                VBCodeProvider csc = new VBCodeProvider();
                ICodeCompiler icc = csc.CreateCompiler();
                ICodeParser icp = csc.CreateParser();
                string errorString = null;

                //// Set input params for the compiler
                CompilerParameters co = new CompilerParameters();
                //co.OutputAssembly = "c:\UserCostCentres.dll"

                co.GenerateInMemory = true;


                ///'''Dim assemblyFileName As String
                ///'''assemblyFileName = [Assembly].GetExecutingAssembly().GetName().CodeBase
                ///'''co.ReferencedAssemblies.Add(assemblyFileName.Substring("file:///".Length))
                ///'''co.ReferencedAssemblies.Add(sOutputPath + "CostCentreEngine.dll")
                ///'''
                ///'''

                ///////////////////////////////////
                //// Add available assemblies - this should be enough
                //for /////////the simplest            // applications.
                //System.Reflection.Assembly oAsm = default(System.Reflection.Assembly);

                foreach (System.Reflection.Assembly oAsm in AppDomain.CurrentDomain.GetAssemblies()) {
                    //If Not (oAsm.GetType Is System.Reflection.Emit.AssemblyBuilder) Then
                    //co.ReferencedAssemblies.Add(oAsm.Location)
                    //End If

                    if ((!object.ReferenceEquals(oAsm.GetType(), typeof(System.Reflection.Emit.AssemblyBuilder)))) {
                        co.ReferencedAssemblies.Add(oAsm.Location);
                    }

                }

                for (int iCounter = co.ReferencedAssemblies.Count - 1; iCounter >= 0; iCounter += -1) {
                    if (co.ReferencedAssemblies[iCounter].Trim() == string.Empty) {
                        co.ReferencedAssemblies.RemoveAt(iCounter);
                    }
                }

                //co.ReferencedAssemblies.Add("E:\Development\Infinity\Infinity.UI\bin\Infinity.Componetns.CostCentreLoader.dll")
                //co.ReferencedAssemblies.Add("system.data.dll")
                //co.ReferencedAssemblies.Add("system.xml.dll")
                //co.ReferencedAssemblies.Add("system.drawing.dll")

                //co.ReferencedAssemblies.Add(sOutputPath + "ByteFX.Data.dll")
                //////////////////////////////////
                co.IncludeDebugInformation = false;
                //co.TreatWarningsAsErrors = True
                //co.CompilerOptions
                //co.WarningLevel = 1

                //// we want to genereate a DLL
                co.GenerateExecutable = false;

                //here we will have to retrieve the names of all vb files in binaries folder and pass them to compiler for compilation



                //source(0) = "costcentre.vb"
                //source(1) = "assemblyinfo.vb"
                //source(2) = "assemblyinfo.vb"

                //// Run the compiling process

                CompilerResults result = icc.CompileAssemblyFromSource(co, Source);


                //Dim result As CompilerResults = icc.CompileAssemblyFromSourceBatch(co, source)

                return result;
            } catch (Exception ex) {
                throw ex;
            }
        }

        //public long CopyCostCentre(long CostCentreID)
        //{
        //    try {
        //        CostCentre oCostCentre = _CostCentreRepository.GetCostCentreByID(CostCentreID);
        //        oCostCentre.Name = "Copy of (" + oCostCentre.Name + ")";

        //        string[] CompleteCode = null;

        //        CompleteCode = oCostCentre.CompleteCode, "Private Const SetupCost As Double");

        //        if (CompleteCode.Length == 2) {
        //            //oCostCentre.CostCentreID = BLL.CostCentres.CostCentre.GetMaxCostCentreID(g_GlobalData);
        //            oCostCentre.CompleteCode = "Namespace UserCostCentres" + Constants.vbCrLf;
        //            oCostCentre.CompleteCode += "Public Class copyof" + oCostCentre.CodeFileName + Constants.vbCrLf;
        //            oCostCentre.CompleteCode += "Inherits MarshalByRefObject" + Constants.vbCrLf;
        //            oCostCentre.CompleteCode += "Implements Infinity.Model.CostCentres.ICostCentreLoader" + Constants.vbCrLf;
        //            oCostCentre.CompleteCode += "Private Const CostCentreID As String = \"" + (oCostCentre.CostCentreID + 1).ToString + '\"' + Constants.vbCrLf;
        //            oCostCentre.CompleteCode += "Private Const SetupCost As Double";
        //            oCostCentre.CompleteCode += CompleteCode(1);

        //            _CostCentreRepository.InsertCostCentre(oCostCentre);
        //            _CostCentreRepository.UpdateWorkinstructions(oCostCentre.WorkInstructionsList, oCostCentre.CostCentreID, true);

        //            int TotalCount = oCostCentre.CostCentreResources.Rows.Count;
        //            //for (int i = TotalCount - 1; i >= 0; i += -1) {
        //            //    DataRow oRRow = oCostCentre.CostCentreResources.NewRow();
        //            //    oRRow("CostCentreID") = oCostCentre.CostCentreID;
        //            //    oRRow("ResourceID") = oCostCentre.CostCentreResources.Rows(i)("ResourceID");
        //            //    oCostCentre.CostCentreResources.Rows.Add(oRRow);
        //            //}
        //            BLL.CostCentres.CostCentre.ADDUpdateCostCentreResources(oCostCentre.CostCentreResources, g_GlobalData);


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
        //            oCompanyName = oCompanyName.Replace('\"', "");
        //            oCompanyName = oCompanyName.Replace("?", "");
        //            oCompanyName = oCompanyName.Replace("<", "");
        //            oCompanyName = oCompanyName.Replace(">", "");
        //            oCompanyName = oCompanyName.Replace("(", "");
        //            oCompanyName = oCompanyName.Replace(")", "");
        //            oCompanyName = oCompanyName.Replace('{', "");
        //            oCompanyName = oCompanyName.Replace('}', "");
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


        //            foreach (var oRow in oTable.Rows) {
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


        /// <summary>
        ///     Return Cost Centre  Calculation methods datatable having (Fixed,PerHour,Per Quantity,Formula base)
        /// </summary>
        ///         ''' 
        //public DataTable LoadCalculationMethodType()
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



        /// <summary>
        ///     Get Max CostCentreID
        /// </summary>
        ///         ''' 
        public long GetMaxCostCentreID()
        {
            try
            {
                return _CostCentreRepository.GetMaxCostCentreID();
                

            }
            catch (Exception ex)
            {
                throw new Exception("GetMaxCostCentreID", ex);
            }
        }

        /// <summary>
        /// Get CostCentre Model By ID
        /// </summary>
        /// <param name="CostCentreID"></param>
        /// <returns>Costcentre</returns>
        public CostCentre GetCostCentreByID(long CostCentreID)
        {

            try
            {
                return _CostCentreRepository.GetCostCentreByID(CostCentreID);

            }
            catch (Exception ex)
            {
                throw new Exception("GetCostCentreSummaryByID", ex);
            }
        }


        /// <summary>
        /// Get CostCentre Summary it will not return complete costcentre model, Its for summary costcentres
        /// </summary>
        /// <param name="CostCentreID"></param>
        /// <returns>Costcentre</returns>
        public CostCentre GetCostCentreSummaryByID(long CostCentreID)
        {

            try
            {
                return _CostCentreRepository.GetCostCentreByID(CostCentreID);

            }
            catch (Exception ex)
            {
                throw new Exception("GetCostCentreSummaryByID", ex);
            }
        }

        /// <summary>
        ///     It will not return complete costcentre object but it will return System Costcentre Parameters only
        /// </summary>
        ///         ''' 
        public CostCentre GetSystemCostCentre(long SystemTypeID, long OrganisationID)
        {

            try
            {
                return _CostCentreRepository.GetSystemCostCentre(SystemTypeID, OrganisationID);

            }
            catch (Exception ex)
            {
                throw new Exception("GetCostCentreSummaryByID", ex);
            }
        }

        /// <summary>
        ///     Get Cost Centre List
        /// </summary>
        ///         ''' 
        public List<CostCentre> GetCostCentreList()
        {

            try
            {
                return _CostCentreRepository.GetCostCentreList();
            }
            catch (Exception ex)
            {
                throw new Exception("Get CostCentre List", ex);
            }
        }

    

        /// <summary>
        ///     Get Cost Centre Resources table.
        /// </summary>
        ///         ''' 
        public CostcentreResource GetCostCentreResources(long CostCentreID)
        {
            try
            {
                return _CostCentreRepository.GetCostCentreResources(CostCentreID);
            }
            catch (Exception ex)
            {
                throw new Exception("Get Resources", ex);
            }
        }

        /// <summary>
        ///     Update Only System cost centre attributes in the costcentres
        /// </summary>
        ///         ''' 
        public bool UpdateSystemCostCentre(long CostCentreID, int ProfitMarginID, int NominalCodeId, double MinCost, int UserID, string Description, bool DirectCost, bool IsScheduleable)
        {
            try
            {
                return _CostCentreRepository.UpdateSystemCostCentre(CostCentreID, ProfitMarginID, NominalCodeId, MinCost, UserID, Description, DirectCost, IsScheduleable);
            }
            catch (Exception ex)
            {
                throw new Exception("Get Resources", ex);
            }
        }


        /// <summary>
        /// Get Cost Centre Types
        /// </summary>
        /// <param name="oConnection"></param>
        /// <param name="ReturnMode"></param>
        /// <returns></returns>
        public List<CostCentreType> GetCostCentreTypes(TypeReturnMode ReturnMode)
        {
            try
            {
                return _CostCentreRepository.GetCostCentreTypes(ReturnMode);
            }
            catch (Exception ex)
            {
                throw new Exception("", ex);
            }
        }


        /// <summary>
        /// Get Cost Centre System Types
        /// </summary>
        /// <param name="oConnection"></param>
        /// <param name="ReturnMode"></param>
        /// <returns></returns>
        public List<CostcentreSystemType> GetCostCentreSystemTypes()
        {
            try
            {
                return _CostCentreRepository.GetCostCentreSystemTypes();
            }
            catch (Exception ex)
            {
                throw new Exception("", ex);
            }
        }

        //


        /// <summary>
        ///     Load Pre Defined Cost Centre Template in DB
        /// </summary>
        ///         ''' 
        public CostCentreTemplate LoadCostCentreTemplate(int TemplateID)
        {
            try
            {

                return _CostCentreRepository.LoadCostCentreTemplate(TemplateID);
            }
            catch (Exception ex)
            {
                throw new Exception("LoadCostCentreTemplate", ex);
            }
        }

        /// <summary>
        ///     Get WorkInstruction of a costcentre in a dataset
        /// </summary>
        ///         ''' 
        public CostcentreInstruction GetCostCentreWorkInstruction(long CostcentreID)
        {
            try
            {
                return _CostCentreRepository.GetCostCentreWorkInstruction(CostcentreID);
            }
            catch (Exception ex)
            {
                throw new Exception("GetCostCentreWorkInstruction", ex);
            }
        }


        /// <summary>
        ///     Return Cost Centre Categories Datatable
        /// </summary>
        ///         ''' 
        public List<CostCentreType> ReturnCostCentreCategories()
        {
            try
            {
                return _CostCentreRepository.ReturnCostCentreCategories();
            }
            catch (Exception ex)
            {
                throw new Exception("ReturnCostCentreCategories", ex);
            }
        }

        /// <summary>
        ///     Check Cost centre Names
        /// </summary>
        ///         ''' 
        public bool CheckCostCentreName(long CostCentreID, string CostCentreName, long OrganisationId)
        {
            try
            {
                return _CostCentreRepository.CheckCostCentreName(CostCentreID, CostCentreName, OrganisationId);
            }
            catch (Exception ex)
            {
                throw new Exception("Check CostCentre Name", ex);
            }
        }


        /// <summary>
        ///     It will update complete costcentre model.
        /// </summary>
        ///         ''' 
        public bool UpdateUserDefinedCostCentre(CostCentre oCostCentre)
        {
            try
            {
                return _CostCentreRepository.UpdateCostCentre(oCostCentre);
            }
            catch (Exception ex)
            {
                throw new Exception("UpdateUserDefinedCostCentre", ex);
            }
        }

        /// <summary>
        ///     Inserts complete costcentre model.
        /// </summary>
        ///         ''' 
        public long InsertUserDefinedCostCentre(CostCentre oCostCentre)
        {
            try
            {
                return _CostCentreRepository.InsertCostCentre(oCostCentre);
            }
            catch (Exception ex)
            {
                throw new Exception("InsertUserDefineCostCentre", ex);
            }
        }

     

        /// <summary>
        ///     Get Cost Centre resources with names
        /// </summary>
        ///         ''' 
        public List<CostCentreResource> GetCostCentreResourcesWithNames(long CostcentreID)
        {
            try
            {
                return _CostCentreRepository.GetCostCentreResourcesWithNames(CostcentreID);
            }
            catch (Exception ex)
            {
                throw new Exception("GetCostCentreResourcesWithNames", ex);
            }
        }

        /// <summary>
        ///    A Datatable with only code attribute will return a complete code of one site
        /// </summary>
        ///         ''' 
        public List<CostCentre> GetCompleteCodeofAllCostCentres(long OrganisationId)
        {
            try
            {
                return _CostCentreRepository.GetCompleteCodeofAllCostCentres(OrganisationId);
            }
            catch (Exception ex)
            {
                throw new Exception("GetCompleteCodeofAllCostCentres", ex);
            }
        }

        /// <summary>
        ///     Change flag of the costcentre
        /// </summary>
        ///         ''' 
        public bool ChangeFlag(int FlagID, long CostCentreID)
        {
            try
            {
                return _CostCentreRepository.ChangeFlag(FlagID, CostCentreID);
            }
            catch (Exception ex)
            {
                throw new Exception("ChangeFlag", ex);
            }
        }

        public List<CostCentreType> GetCostCentreCategories(long OrganisationId)
        {
            try
            {
                return _CostCentreRepository.GetCostCentreCategories(OrganisationId);
            }
            catch (Exception ex)
            {
                throw new Exception("GetCostCentreCategories", ex);
            }
        }


        public bool IsCostCentreAvailable(int CategoryID)
        {
            try
            {
                return _CostCentreRepository.IsCostCentreAvailable(CategoryID);
            }
            catch (Exception ex)
            {
                throw new Exception("IsCostCentreAvailable", ex);
            }
        }

        public List<CostCentre> GetDeliveryCostCentersList()
        {
            try
            {
                return _CostCentreRepository.GetDeliveryCostCentersList();

            }
            catch (Exception ex)
            {
                throw new Exception("GetDeliveryCostCentersList", ex);
            }
        }

        public List<CostCentre> GetCorporateDeliveryCostCentersList(long CompanyID)
        {
            try
            {
                return _CostCentreRepository.GetCorporateDeliveryCostCentersList(CompanyID);
            }
            catch (Exception ex)
            {
                throw new Exception("GetCorporateDeliveryCostCentersList", ex);
            }
        }


    }
}
