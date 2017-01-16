using Microsoft.VisualBasic;
using MPC.Interfaces.Common;
using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace MPC.Implementation.WebStoreServices
{

  
    public class CostCentreService : ICostCentreService
    {
        private readonly ICostCentreRepository _CostCentreRepository;
        private readonly ICostCentreVariableRepository _CostCentreVariableRepository;
        private readonly ICostCentreQuestionRepository _CostCentreQuestionRepository;
        private readonly ICostCentreMatrixRepository _CostCentreMatrixRepository;
        private readonly IOrganisationRepository organisationRepository;

        public CostCentreService(ICostCentreRepository CostCentreRepository,
            ICostCentreVariableRepository CostCentreVariableRepository, ICostCentreQuestionRepository CostCentreQuestionRepository
            , ICostCentreMatrixRepository CostCentreMatrixRepository, IOrganisationRepository organisationRepository)
        {
            if (organisationRepository == null)
            {
                throw new ArgumentNullException("organisationRepository");
            }
            this._CostCentreRepository = CostCentreRepository;
            this._CostCentreVariableRepository = CostCentreVariableRepository;
            this._CostCentreQuestionRepository = CostCentreQuestionRepository;
            this._CostCentreMatrixRepository = CostCentreMatrixRepository;
            this.organisationRepository = organisationRepository;
        }


        public void CompileCostCentreTest()
        {

            List<CostCentre> oCostCentresSource = this.GetCompleteCodeofAllCostCentres(1);
            string oSource = "";
            
            //oSource += "Imports MPC" + Environment.NewLine;
            oSource += "Imports System" + Environment.NewLine;
            oSource += "Imports System.Data" + Environment.NewLine;
            oSource += "Imports Microsoft.VisualBasic" + Environment.NewLine;
            //oSource += System.Configuration.ConfigurationSettings.AppSettings("DALProviderNameSpace") + Environment.NewLine;
            oSource += "Imports MPC.Implementation.WebStoreServices" + Environment.NewLine;
            oSource += "imports MPC.Models.DomainModels" + Environment.NewLine;
            oSource += "Imports System.Reflection" + Environment.NewLine;
            oSource += "Namespace UserCostCentres" + Environment.NewLine;


            //if (IsNewCostCentre == true) {
            //    string Str = sCode.ToString;
            //    Str = Str, "Namespace UserCostCentres", "");

            //    Str = Str, "End Namespace", "");

            //    oSource += Environment.NewLine + Str + Environment.NewLine;

            //}



            foreach (var oCostCentre in oCostCentresSource)
            {
                string Str = "";
                Str = oCostCentre.CompleteCode;


                Str = Str.Replace("Namespace UserCostCentres", "");
                Str = Str.Replace("End Namespace", "");

                oSource += Environment.NewLine + Str + Environment.NewLine;

            }

            oSource += "End Namespace" + Environment.NewLine;

            //'if compilation fails then delete the code file

            System.IO.FileStream oFileStream;
            string oCompanyName = "Test";

            //replacing any unwanted characters in the CompanyNames and create file name

            bool IsCompiled = true;

            try
            {
                //Compile Code of CostCentres
                this.CompileBinaries(HttpContext.Current.Server.MapPath("/") + "\\ccAssembly\\", oSource, oCompanyName);

                //Get CostCentre File Open it in Read Mode
                oFileStream = System.IO.File.OpenRead(HttpContext.Current.Server.MapPath("/") + "\\ccAssembly\\" + oCompanyName + "UserCostCentres.dll");

                //Get Byte Array of the file and write it in the db
                //byte[] CostCentreByte = new byte[Convert.ToInt32(oFileStream.Length - 1) + 1];

                //oFileStream.Read(CostCentreByte, 0, Convert.ToInt32(oFileStream.Length - 1));

                //CostCentreByte = null;
            }
            catch (Exception ex)
            {
                //    BLL.CostCentres.CostCentre.DeleteCodeFile(sCostCentreFileName, Application.StartupPath.ToString + "\binaries\")
                IsCompiled = false;
                throw new Exception("Error Compiling Costcentre", ex);

            }
            finally
            {
                //CostCentreDLL = null;
                oFileStream = null;

            }
            if (IsCompiled == false)
            {
                return;
            }



        }

        public void SaveCostCentre(long _CostCentreID, long OrganisationId, string OrganisationName)
        {
            


            //creating a costcentre code file and updating it and compile it.
            bool  IsNewCostCentre = false;
            CostCentreTemplate oTemplate = _CostCentreRepository.LoadCostCentreTemplate(2);
            string Header, Footer, Middle;
            double SetupCost = 0.0d;
            int SetupTime = 0;
            double MinCost = 0.0d;
            double DefaultProfitMargin = 0.0d;
         //   string sCostPlant = "_CostCentreService.ExecuteQuestion(ParamsArray, \"13\", CostCentreID)";
              
            string sCostPlant = TokenParse("EstimatedPlantCost = {SystemVariable, ID=\"1\",Name=\"Number of unique Inks used on Side 1\"} + {question, ID=\"52\",caption=\"yes no\"} + {matrix, ID=\"19\",Name=\"Super Formula Matrix\"} + {question, ID=\"34\",caption=\"How many sections to fold?\"} + {question, ID=\"51\",caption=\"Multiple Options\"} "); 
    //="EstimatedPlantCost =  BLL.CostCentres.CostCentreExecution.ExecuteVariable(ParamsArray ,"1")  *  BLL.CostCentres.CostCentreExecution.ExecuteQuestion(ParamsArray,"13",CostCentreID) ";
            string sCostLabour ="EstimatedLabourCost = 0";
            string sCostStock ="EstimatedMaterialCost = 0";
            string sTime ="EstimatedTime = 0";
            string sPricePlant ="QuotedPlantPrice = 0";
            string sPriceLabour ="QuotedLabourPrice = 0";
            string sPriceStock ="QuotedMaterialPrice = 0";
            string sActualPlantCost ="";
            string sActualStockCost ="";
            string sActualLabourCost ="";
            StringBuilder sCode = new StringBuilder();



            //#Zone " Compiling CostCentre "

            {
                char spacechar = '\0';
                spacechar = (char)95;
                //replacing any unwanted characters in the costcentrename and create file name
                
                Header = oTemplate.Header.Substring(0, oTemplate.Header.IndexOf( "''<cost>") - 2);
                Middle = oTemplate.Middle.Substring(0, oTemplate.Middle.IndexOf( "''<price>") - 2);
                Footer = oTemplate.Footer;

                //'getting new Cost centreID from Database

                if (_CostCentreID == 0)
                {
                    _CostCentreID = this.GetMaxCostCentreID();
                    IsNewCostCentre = true;
                }


                //'replacing the CostCentre name and ID in the header string
                Header = Header.Replace( "ccname", "CLS_" + _CostCentreID.ToString());
                Header = Header.Replace( "<ccid>", _CostCentreID.ToString());

                //replacing the attribs of cost centre
                Header = Header.Replace( "<ccsc>", SetupCost.ToString());
                Header = Header.Replace( "<ccst>", SetupTime.ToString());
                Header = Header.Replace( "<ccmc>", MinCost.ToString());
                Header = Header.Replace("<ccva>", DefaultProfitMargin.ToString());

                //'now making a code file string

                sCode.Append(Header);
                sCode.Append(Environment.NewLine + "''<cost>" + Environment.NewLine);
                sCode.Append(Environment.NewLine + "''<plant>" + Environment.NewLine);
                sCode.Append(sCostPlant);
                sCode.Append(Environment.NewLine + "''</plant>" + Environment.NewLine);
                sCode.Append(Environment.NewLine + "''<labour>" + Environment.NewLine);
                sCode.Append(sCostLabour);
                sCode.Append(Environment.NewLine + "''</labour>" + Environment.NewLine);
                sCode.Append(Environment.NewLine + "''<material>" + Environment.NewLine);
                sCode.Append(sCostStock);
                sCode.Append(Environment.NewLine + "''</material>" + Environment.NewLine);
                sCode.Append(Environment.NewLine + "''<time>" + Environment.NewLine);
                sCode.Append(sTime);
                sCode.Append(Environment.NewLine + "''</time>" + Environment.NewLine);

                sCode.Append(Environment.NewLine + "''</cost>" + Environment.NewLine);

                sCode.Append(Middle);

                sCode.Append(Environment.NewLine + "''<price>" + Environment.NewLine);
                sCode.Append(Environment.NewLine + "''<plant>" + Environment.NewLine);
                sCode.Append(sPricePlant);
                sCode.Append(Environment.NewLine + "''</plant>" + Environment.NewLine);
                sCode.Append(Environment.NewLine + "''<labour>" + Environment.NewLine);
                sCode.Append(sPriceLabour);
                sCode.Append(Environment.NewLine + "''</labour>" + Environment.NewLine);
                sCode.Append(Environment.NewLine + "''<material>" + Environment.NewLine);
                sCode.Append(sPriceStock);
                sCode.Append(Environment.NewLine + "''</material>" + Environment.NewLine);
                sCode.Append(Environment.NewLine + "''</price>" + Environment.NewLine);


                //process the footer here..
                //since footer also contains the ActualCOST area, we have to remove that AREA
                //and replace it with the new ACTUAL COST code

                //our code is between ''<actualcost>   and  ''</actualcost>
                int iStart = 0;
                int iStart2 = 0;
                int iLength = 0;
                string sActualCostString = null;

                sActualCostString = "''<plant>" + sActualPlantCost + "''</plant>" + "''</material>" + sActualStockCost + "''</material>" + "''</labour>" + sActualLabourCost + "''</labour>";

                iStart = Footer.IndexOf("''<actualcost>") + 14;
                iStart2 = Footer.IndexOf("''</actualcost>");
                Footer.Remove(iStart, iStart2 - iStart);
                Footer.Insert(iStart, sActualCostString);

                sCode.Append(Footer);


                CostCentre oCostCentre = GetCostCentreByID(_CostCentreID);


                oCostCentre.CodeFileName = "CLS_" + _CostCentreID.ToString();
                string oSource = "";

                //Get Complete Code of the CostCentre from the DB and Recompile it with the new changes
                List<CostCentre> oAllCostCentresCode = GetCompleteCodeofAllCostCentres(OrganisationId);


                //oSource += "Imports Infinity" + Environment.NewLine;
                oSource += "Imports System" + Environment.NewLine;
                oSource += "Imports System.Data" + Environment.NewLine;
                oSource += "Imports Microsoft.VisualBasic" + Environment.NewLine;
                //oSource += System.Configuration.ConfigurationSettings.AppSettings("DALProviderNameSpace") + Environment.NewLine;
                oSource += "Imports MPC.Implementation.WebStoreServices" + Environment.NewLine;
                oSource += "imports MPC.Models.DomainModels" + Environment.NewLine;
                oSource += "imports MPC.Models.Common" + Environment.NewLine;
                oSource += "Imports System.Reflection" + Environment.NewLine;
                oSource += "Imports Microsoft.Practices.Unity" + Environment.NewLine;
                oSource += "Imports ICostCentreService = MPC.Interfaces.WebStoreServices.ICostCentreService" + Environment.NewLine;
                oSource += "Namespace UserCostCentres" + Environment.NewLine;


                if (IsNewCostCentre == true)
                {
                    string Str = sCode.ToString();
                    Str = Str.Replace("Namespace UserCostCentres", "");

                    Str = Str.Replace("End Namespace", "");

                    oSource += Environment.NewLine + Str + Environment.NewLine;

                }

                if (oAllCostCentresCode != null)
                {
                    foreach (var oOtherCostCentre in oAllCostCentresCode)
                    {
                        string Str = "";
                        if (_CostCentreID == oOtherCostCentre.CostCentreId)
                        {
                            Str = sCode.ToString();
                        }
                        else
                        {
                            Str = oOtherCostCentre.CompleteCode;
                        }


                        Str = Str.Replace("Namespace UserCostCentres", "");
                        Str = Str.Replace("End Namespace", "");

                        oSource += Environment.NewLine + Str + Environment.NewLine;

                    }
                }
                else
                {
                    string Str = "";
                    Str = sCode.ToString();
                    Str = Str.Replace("Namespace UserCostCentres", "");
                    Str = Str.Replace("End Namespace", "");
                    oSource += Environment.NewLine + Str + Environment.NewLine;
                }

                oSource += "End Namespace" + Environment.NewLine;

                //'if compilation fails then delete the code file

                System.IO.FileStream oFileStream; ;
                //string oCompanyName = OrganisationName;

                //replacing any unwanted characters in the CompanyNames and create file name

                bool IsCompiled = true;

                try
                {
                    //Compile Code of CostCentres
                    CompileBinaries(HttpContext.Current.Server.MapPath("/") + "\\ccAssembly\\", oSource, OrganisationName);

                    //Get CostCentre File Open it in Read Mode
                    oFileStream = System.IO.File.OpenRead(HttpContext.Current.Server.MapPath("/") + "\\ccAssembly\\" + OrganisationName + "UserCostCentres.dll");

                    //Get Byte Array of the file and write it in the db
                    byte[] CostCentreByte = new byte[Convert.ToInt32(oFileStream.Length - 1) + 1];

                    oFileStream.Read(CostCentreByte, 0, Convert.ToInt32(oFileStream.Length - 1));

                    CostCentreByte = null;
                }
                catch (Exception ex)
                {
                    //    BLL.CostCentres.CostCentre.DeleteCodeFile(sCostCentreFileName, Application.StartupPath.ToString + "\binaries\")
                    IsCompiled = false;
                    throw new Exception("Error Compiling Costcentre", ex);

                }
                finally
                {

                    oFileStream = null;

                }

                oCostCentre.CompleteCode = sCode.ToString();

                _CostCentreRepository.UpdateCostCentre(oCostCentre);

            }




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
                                return " _CostCentreService.ExecuteVariable(ParamsArray ,\"" + GetID[1] + "\") ";
                            } else {
                                throw new Exception("Invalid Calculation String.");
                                return "";
                            }

                            break;
                        case "question":
                             GetID = oSpiltTokens[1].Split('\"');
                            if (GetID.Length == 3) {
                                return " _CostCentreService.ExecuteQuestion(ParamsArray,\"" + GetID[1] + "\",CostCentreID) ";
                            } else {
                                throw new Exception("Invalid Calculation String.");
                                return "";
                            }

                            break;
                        case "subcostcentre":

                            GetID = oSpiltTokens[1].Split( '\"');
                            GetReturnValue = oSpiltTokens[3].Split('\"');
                            if (GetID.Length == 3) {
                                return " _CostCentreService.ExecuteCostCentre(ParamsArray,\"" + GetID[1] + "\",\"" + GetReturnValue[1] + "\") ";
                            } else {
                                throw new Exception("Invalid Calculation String.");
                                return "";
                            }

                            break;
                        case "resource":

                            GetID = oSpiltTokens[1].Split('\"');
                            GetReturnValue = oSpiltTokens[3].Split( '\"');
                            if (GetID.Length == 3 & GetReturnValue.Length == 3) {
                                return " _CostCentreService.ExecuteResource(ParamsArray,\"" + GetID[1] + "\",\"" + GetReturnValue[1] + "\") ";
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
                                return " _CostCentreService.ExecuteUserStockItem(ParamsArray,\"" + GetID[1] + "\",\"" + GetName[1] + "\",\"" + GetQtyType[1] + "\",\"" + GetValue[1] + "\",\"" + GetQType[1] + "\",cint(CostCentreID)) ";
                            } else {
                                throw new Exception("Invalid Calculation String.");
                                return "";
                            }

                            break;
                        case "matrix":

                            GetID = oSpiltTokens[1].Split( '\"');
                            if (GetID.Length == 3) {
                                return " _CostCentreService.ExecuteMatrix(ParamsArray,\"" + GetID[1] + "\",cint(CostCentreID)) ";
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
                                return " _CostCentreService.ExecuteInput(ParamsArray,\"" + GetID[1] + "\",\"" + GetQuestion[1] + "\"," + GetTypes[1] + "," + GetInputTypes[1] + ",\"" + GetValue[1] + "\",cint(CostCentreID)) ";
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
        /// Get Organisation from db
        /// </summary>
        public Organisation GetOrganisation(long costCentreId)
        {
            CostCentre costCentre = GetCostCentreByID(costCentreId);
            if (costCentre == null || !costCentre.OrganisationId.HasValue)
            {
                return null;
            }

            return organisationRepository.GetOrganizatiobByID(costCentre.OrganisationId.Value);
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

                    if ((!object.ReferenceEquals(oAsm.GetType(), typeof(System.Reflection.Emit.AssemblyBuilder))) && !oAsm.IsDynamic)
                    {
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

                foreach (System.Reflection.Assembly oAsm in AppDomain.CurrentDomain.GetAssemblies())
                {
                    //If Not (oAsm.GetType Is System.Reflection.Emit.AssemblyBuilder) Then
                    //co.ReferencedAssemblies.Add(oAsm.Location)
                    //End If

                    if ((!object.ReferenceEquals(oAsm.GetType(), typeof(System.Reflection.Emit.AssemblyBuilder))) && !oAsm.IsDynamic)
                    {
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
        //            oCostCentre.CompleteCode = "Namespace UserCostCentres" + Environment.NewLine;
        //            oCostCentre.CompleteCode += "Public Class copyof" + oCostCentre.CodeFileName + Environment.NewLine;
        //            oCostCentre.CompleteCode += "Inherits MarshalByRefObject" + Environment.NewLine;
        //            oCostCentre.CompleteCode += "Implements Infinity.Model.CostCentres.ICostCentreLoader" + Environment.NewLine;
        //            oCostCentre.CompleteCode += "Private Const CostCentreID As String = \"" + (oCostCentre.CostCentreID + 1).ToString + '\"' + Environment.NewLine;
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

        //            oSource += "Imports Infinity" + Environment.NewLine;
        //            oSource += "Imports System" + Environment.NewLine;
        //            oSource += "Imports System.Data" + Environment.NewLine;
        //            oSource += "Imports Microsoft.VisualBasic" + Environment.NewLine;
        //            oSource += System.Configuration.ConfigurationSettings.AppSettings("DALProviderNameSpace") + Environment.NewLine;
        //            oSource += "Imports Infinity.Bll.CostCentres" + Environment.NewLine;
        //            oSource += "imports Infinity.Model.CostCentres" + Environment.NewLine;
        //            oSource += "Imports System.Reflection" + Environment.NewLine;

        //            oSource += "Namespace UserCostCentres" + Environment.NewLine;


        //            foreach (var oRow in oTable.Rows) {
        //                string Str = "";
        //                Str = oRow(1).ToString;
        //                Str = Str, "Namespace UserCostCentres", "");
        //                Str = Str, "End Namespace", "");

        //                oSource += Environment.NewLine + Str + Environment.NewLine;

        //            }

        //            oSource += "End Namespace" + Environment.NewLine;

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


        ///// <summary>
        ///// Get CostCentre Summary it will not return complete costcentre model, Its for summary costcentres
        ///// </summary>
        ///// <param name="CostCentreID"></param>
        ///// <returns>Costcentre</returns>
        //public CostCentre GetCostCentreSummaryByID(long CostCentreID)
        //{

        //    try
        //    {
        //        return _CostCentreRepository.GetCostCentreByID(CostCentreID);

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
        //public CostCentre GetSystemCostCentre(long SystemTypeID, long OrganisationID)
        //{

        //    try
        //    {
        //        return _CostCentreRepository.GetSystemCostCentre(SystemTypeID, OrganisationID);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("GetCostCentreSummaryByID", ex);
        //    }
        //}

        ///// <summary>
        /////     Get Cost Centre List
        ///// </summary>
        /////         ''' 
        //public List<CostCentre> GetCostCentreList()
        //{

        //    try
        //    {
        //        return _CostCentreRepository.GetCostCentreList();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Get CostCentre List", ex);
        //    }
        //}

    

        ///// <summary>
        /////     Get Cost Centre Resources table.
        ///// </summary>
        /////         ''' 
        //public CostcentreResource GetCostCentreResources(long CostCentreID)
        //{
        //    try
        //    {
        //        return _CostCentreRepository.GetCostCentreResources(CostCentreID);
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
        //public bool UpdateSystemCostCentre(long CostCentreID, int ProfitMarginID, int NominalCodeId, double MinCost, int UserID, string Description, bool DirectCost, bool IsScheduleable)
        //{
        //    try
        //    {
        //        return _CostCentreRepository.UpdateSystemCostCentre(CostCentreID, ProfitMarginID, NominalCodeId, MinCost, UserID, Description, DirectCost, IsScheduleable);
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
        //public List<CostCentreType> GetCostCentreTypes(TypeReturnMode ReturnMode)
        //{
        //    try
        //    {
        //        return _CostCentreRepository.GetCostCentreTypes(ReturnMode);
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
        //public List<CostcentreSystemType> GetCostCentreSystemTypes()
        //{
        //    try
        //    {
        //        return _CostCentreRepository.GetCostCentreSystemTypes();
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
        //public CostCentreTemplate LoadCostCentreTemplate(int TemplateID)
        //{
        //    try
        //    {

        //        return _CostCentreRepository.LoadCostCentreTemplate(TemplateID);
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
        //public CostcentreInstruction GetCostCentreWorkInstruction(long CostcentreID)
        //{
        //    try
        //    {
        //        return _CostCentreRepository.GetCostCentreWorkInstruction(CostcentreID);
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
        //public List<CostCentreType> ReturnCostCentreCategories()
        //{
        //    try
        //    {
        //        return _CostCentreRepository.ReturnCostCentreCategories();
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
        //public bool CheckCostCentreName(long CostCentreID, string CostCentreName, long OrganisationId)
        //{
        //    try
        //    {
        //        return _CostCentreRepository.CheckCostCentreName(CostCentreID, CostCentreName, OrganisationId);
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
        //public bool UpdateUserDefinedCostCentre(CostCentre oCostCentre)
        //{
        //    try
        //    {
        //        return _CostCentreRepository.UpdateCostCentre(oCostCentre);
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
        //public long InsertUserDefinedCostCentre(CostCentre oCostCentre)
        //{
        //    try
        //    {
        //        return _CostCentreRepository.InsertCostCentre(oCostCentre);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("InsertUserDefineCostCentre", ex);
        //    }
        //}

     

        ///// <summary>
        /////     Get Cost Centre resources with names
        ///// </summary>
        /////         ''' 
        //public List<CostCentreResource> GetCostCentreResourcesWithNames(long CostcentreID)
        //{
        //    try
        //    {
        //        return _CostCentreRepository.GetCostCentreResourcesWithNames(CostcentreID);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("GetCostCentreResourcesWithNames", ex);
        //    }
        //}

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

        ///// <summary>
        /////     Change flag of the costcentre
        ///// </summary>
        /////         ''' 
        //public bool ChangeFlag(int FlagID, long CostCentreID)
        //{
        //    try
        //    {
        //        return _CostCentreRepository.ChangeFlag(FlagID, CostCentreID);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("ChangeFlag", ex);
        //    }
        //}

        //public List<CostCentreType> GetCostCentreCategories(long OrganisationId)
        //{
        //    try
        //    {
        //        return _CostCentreRepository.GetCostCentreCategories(OrganisationId);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("GetCostCentreCategories", ex);
        //    }
        //}


        //public bool IsCostCentreAvailable(int CategoryID)
        //{
        //    try
        //    {
        //        return _CostCentreRepository.IsCostCentreAvailable(CategoryID);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("IsCostCentreAvailable", ex);
        //    }
        //}
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

    //    public double ExecuteVariable(ref object[] oParamsArray, int VariableID)
    //    {
    //        double functionReturnValue = 0;
    //        try
    //        {
    //            CostCentreExecutionMode ExecutionMode = (CostCentreExecutionMode)oParamsArray[1];
    //            ItemSection oItemSection = (ItemSection)oParamsArray[8];
    //            int CurrentQuantity = Convert.ToInt32(oParamsArray[5]);

    //            //if its Queue populating mode then return 0
    //            if (ExecutionMode == CostCentreExecutionMode.PromptMode)
    //            {
    //                return 0;

    //                //its porpper execution mode
    //            }
    //            else if (ExecutionMode == CostCentreExecutionMode.ExecuteMode)
    //            {

    //                CostCentreVariable oVariable;
    //                //First we have to fetch the Variable object which contains the information
    //                oVariable = _CostCentreVariableRepository.LoadVariable(VariableID);

    //                //now check the type of the variable.
    //                //type 1 = system variable
    //                //type 2 = Customized Variable
    //                //type 3 = CostCentre Variable

    //                // in this type the Criteria will be used that will be

    //                if (oVariable.Type == 1)
    //                {
    //                    switch (oVariable.PropertyType)
    //                    {

    //                        case (int)VariableProperty.Side1Inks:
    //                            functionReturnValue = Convert.ToDouble(oItemSection.Side1Inks);
    //                            break;
    //                        case (int)VariableProperty.Side2Inks:
    //                            functionReturnValue = Convert.ToDouble(oItemSection.Side1Inks);

    //                            break;
    //                        case (int)VariableProperty.PrintSheetQty_ProRata:

    //                            switch (CurrentQuantity)
    //                            {
    //                                case 1:
    //                                    functionReturnValue = Convert.ToDouble(oItemSection.PrintSheetQty1);
    //                                    break;
    //                                case 2:
    //                                    if (oItemSection.PrintSheetQty2 == 0)
    //                                    {
    //                                        functionReturnValue = Convert.ToDouble(oItemSection.PrintSheetQty1);
    //                                    }
    //                                    else
    //                                    {
    //                                        functionReturnValue = Convert.ToDouble(oItemSection.PrintSheetQty2);
    //                                    }
    //                                    break;
    //                                case 3:
    //                                    if (oItemSection.PrintSheetQty3 == 0)
    //                                    {
    //                                        functionReturnValue = Convert.ToDouble(oItemSection.PrintSheetQty1);
    //                                    }
    //                                    else
    //                                    {
    //                                        functionReturnValue = Convert.ToDouble(oItemSection.PrintSheetQty3);
    //                                    }
    //                                    break;
    //                            }

    //                            break;
    //                        case (int)VariableProperty.PressSpeed_ProRata:

    //                            switch (CurrentQuantity)
    //                            {
    //                                case 1:
    //                                    functionReturnValue = Convert.ToDouble(oItemSection.PressSpeed1);
    //                                    break;
    //                                case 2:
    //                                    if (oItemSection.PressSpeed2 == 0)
    //                                    {
    //                                        functionReturnValue = Convert.ToDouble(oItemSection.PressSpeed1);
    //                                    }
    //                                    else
    //                                    {
    //                                        functionReturnValue = Convert.ToDouble(oItemSection.PressSpeed2);
    //                                    }
    //                                    break;
    //                                case 3:
    //                                    if (oItemSection.PressSpeed3 == 0)
    //                                    {
    //                                        functionReturnValue = Convert.ToDouble(oItemSection.PressSpeed1);
    //                                    }
    //                                    else
    //                                    {
    //                                        functionReturnValue = Convert.ToDouble(oItemSection.PressSpeed3);
    //                                    }
    //                                    functionReturnValue = Convert.ToDouble(oItemSection.PressSpeed3);
    //                                    break;
    //                                //Case 4
    //                                //    ExecuteVariable = oItemSection.PressSpeed4
    //                                //Case 5
    //                                //    ExecuteVariable = oItemSection.PressSpeed5
    //                            }

    //                            break;
    //                        //case (int)VariableProperty.ColourHeads:
    //                        //    if ((oItemSection.Press != null)) //  ask sir nv to add referece in press to itemsection
    //                        //    {
    //                        //        functionReturnValue = oItemSection.Press.ColourHeads;
    //                        //    }
    //                        //    else
    //                        //    {
    //                        //        functionReturnValue = 0;
    //                        //    }

    //                        //    break;

    //                        case (int)VariableProperty.ImpressionQty_ProRata:
    //                            switch (CurrentQuantity)
    //                            {
    //                                case 1:
    //                                    functionReturnValue = Convert.ToDouble(oItemSection.ImpressionQty1);
    //                                    break;
    //                                case 2:
    //                                    if (oItemSection.ImpressionQty2 == 0)
    //                                    {
    //                                        functionReturnValue = Convert.ToDouble(oItemSection.ImpressionQty1);
    //                                    }
    //                                    else
    //                                    {
    //                                        functionReturnValue = Convert.ToDouble(oItemSection.ImpressionQty2);
    //                                    }
    //                                    break;
    //                                case 3:
    //                                    if (oItemSection.ImpressionQty3 == 0)
    //                                    {
    //                                        functionReturnValue = Convert.ToDouble(oItemSection.ImpressionQty1);
    //                                    }
    //                                    else
    //                                    {
    //                                        functionReturnValue = Convert.ToDouble(oItemSection.ImpressionQty3);
    //                                    }

    //                                    break;
    //                                //Case 4
    //                                //    ExecuteVariable = oItemSection.ImpressionQty4
    //                                //Case 5
    //                                //    ExecuteVariable = oItemSection.ImpressionQty5
    //                            }

    //                            break;
    //                        case (int)VariableProperty.PressHourlyCharge:
    //                            functionReturnValue = oItemSection.PressHourlyCharge ?? 0;

    //                            break;
    //                        //case (int)VariableProperty.MinInkDuctqty:
    //                        //    if (oItemSection.Press == null)
    //                        //    {
    //                        //        functionReturnValue = 0;
    //                        //    }
    //                        //    else
    //                        //    {
    //                        //        functionReturnValue = oItemSection.Press.MinInkDuctqty;

    //                        //    }

    //                        //    break;

    //                        //case (int)VariableProperty.MakeReadycharge:
    //                        //    if (oItemSection.Press == null)
    //                        //    {
    //                        //        functionReturnValue = 0;
    //                        //    }
    //                        //    else
    //                        //    {
    //                        //        functionReturnValue = oItemSection.Press.MakeReadyCost;
    //                        //    }
    //                        //    break;
    //                        case (int)VariableProperty.PrintChargeExMakeReady_ProRata:
    //                            switch (CurrentQuantity)
    //                            {
    //                                case 1:
    //                                    functionReturnValue = Convert.ToDouble(oItemSection.ImpressionQty1);
    //                                    break;
    //                                case 2:
    //                                    functionReturnValue = Convert.ToDouble(oItemSection.ImpressionQty2);
    //                                    break;
    //                                case 3:
    //                                    functionReturnValue = Convert.ToDouble(oItemSection.ImpressionQty3);
    //                                    break;
    //                                //Case 4
    //                                //    ExecuteVariable = oItemSection.ImpressionQty4
    //                                //Case 5
    //                                //    ExecuteVariable = oItemSection.ImpressionQty5
    //                            }

    //                            break;
    //                        case (int)VariableProperty.PaperGsm:
    //                            functionReturnValue = oItemSection.PaperGsm ?? 0;

    //                            break;
    //                        case (int)VariableProperty.SetupSpoilage:
    //                            functionReturnValue = Convert.ToDouble(oItemSection.SetupSpoilage);

    //                            break;
    //                        case (int)VariableProperty.RunningSpoilage:
    //                            functionReturnValue = Convert.ToDouble(oItemSection.RunningSpoilage);

    //                            break;
    //                        case (int)VariableProperty.PaperPackPrice:
    //                            functionReturnValue = oItemSection.PaperPackPrice ?? 0;

    //                            break;
    //                        case (int)VariableProperty.AdditionalPlateUsed:
    //                            functionReturnValue = Convert.ToDouble(oItemSection.AdditionalPlateUsed);

    //                            break;
    //                        case (int)VariableProperty.AdditionalFilmUsed:
    //                            functionReturnValue = Convert.ToDouble(oItemSection.AdditionalFilmUsed);

    //                            break;
    //                        case (int)VariableProperty.ItemGutterHorizontal:
    //                            functionReturnValue = oItemSection.ItemGutterHorizontal ?? 0;

    //                            break;
    //                        case (int)VariableProperty.ItemGutterVertical:
    //                            functionReturnValue = oItemSection.ItemGutterVertical ?? 0;

    //                            break;
    //                        case (int)VariableProperty.PTVRows:
    //                            functionReturnValue = Convert.ToDouble(oItemSection.PTVRows);

    //                            break;
    //                        case (int)VariableProperty.PTVColoumns:
    //                            functionReturnValue = Convert.ToDouble(oItemSection.PTVColoumns);

    //                            break;
    //                        case (int)VariableProperty.PrintViewLayoutLandScape:
    //                            functionReturnValue = Convert.ToDouble(oItemSection.PrintViewLayoutLandScape);

    //                            break;
    //                        case (int)VariableProperty.PrintViewLayoutPortrait:
    //                            functionReturnValue = Convert.ToDouble(oItemSection.PrintViewLayoutPortrait);

    //                            break;
    //                        case (int)VariableProperty.PrintToView:
    //                            if (oItemSection.PrintViewLayout.Value == (int)PrintViewOrientation.Landscape)
    //                            {
    //                                functionReturnValue = Convert.ToDouble(oItemSection.PrintViewLayoutLandScape);
    //                            }
    //                            else
    //                            {
    //                                functionReturnValue = Convert.ToDouble(oItemSection.PrintViewLayoutPortrait);
    //                            }

    //                            break;
    //                        case (int)VariableProperty.FilmQty:
    //                            functionReturnValue = Convert.ToDouble(oItemSection.FilmQty);

    //                            break;
    //                        case (int)VariableProperty.PlateQty:
    //                            functionReturnValue = Convert.ToDouble(oItemSection.Side1PlateQty + oItemSection.Side2PlateQty);
    //                            break;
    //                        //case (int)VariableProperty.GuilotineMakeReadycharge:
    //                        //    if (oItemSection.Guilotine == null)
    //                        //    {
    //                        //        functionReturnValue = 0;
    //                        //    }
    //                        //    else
    //                        //    {
    //                        //        functionReturnValue = oItemSection.Guilotine.MakeReadyCost;
    //                        //    }

    //                        //    break;
    //                        //case (int)VariableProperty.GuilotineChargePerCut:
    //                        //    if (oItemSection.Guilotine == null)
    //                        //    {
    //                        //        functionReturnValue = 0;
    //                        //    }
    //                        //    else
    //                        //    {
    //                        //        functionReturnValue = oItemSection.Guilotine.CostPerCut;
    //                        //    }

    //                        //    break;
    //                        case (int)VariableProperty.GuillotineFirstCut:
    //                            functionReturnValue = Convert.ToDouble(oItemSection.GuillotineFirstCut);

    //                            break;
    //                        case (int)VariableProperty.GuillotineSecondCut:
    //                            functionReturnValue = Convert.ToDouble(oItemSection.GuillotineSecondCut);

    //                            break;
    //                        case (int)VariableProperty.FinishedItemQty_ProRata:
    //                            switch (CurrentQuantity)
    //                            {
    //                                case 1:
    //                                    functionReturnValue = Convert.ToDouble(oItemSection.Qty1);
    //                                    break;
    //                                case 2:
    //                                    if (oItemSection.Qty2 == 0)
    //                                    {
    //                                        functionReturnValue = Convert.ToDouble(oItemSection.Qty1);
    //                                    }
    //                                    else
    //                                    {
    //                                        functionReturnValue = Convert.ToDouble(oItemSection.Qty2);
    //                                    }
    //                                    break;
    //                                case 3:
    //                                    if (oItemSection.Qty3 == 0)
    //                                    {
    //                                        functionReturnValue = Convert.ToDouble(oItemSection.Qty1);
    //                                    }
    //                                    else
    //                                    {
    //                                        functionReturnValue = Convert.ToDouble(oItemSection.Qty3);
    //                                    }

    //                                    break;
    //                                //Case 4
    //                                //    ExecuteVariable = oItemSection.Qty1
    //                                //Case 5
    //                                //    ExecuteVariable = oItemSection.Qty1
    //                            }

    //                            break;
    //                        //case (int)VariableProperty.TotalSections:
    //                        //    functionReturnValue = oItemSection.TotalSection;

    //                        //    break;
    //                        case (int)VariableProperty.PaperWeight_ProRata:

    //                            switch (CurrentQuantity)
    //                            {
    //                                case 1:
    //                                    functionReturnValue = oItemSection.PaperWeight1 ?? 0;
    //                                    break;
    //                                case 2:
    //                                    if (oItemSection.PaperWeight2 == 0)
    //                                    {
    //                                        functionReturnValue = oItemSection.PaperWeight1 ?? 0;
    //                                    }
    //                                    else
    //                                    {
    //                                        functionReturnValue = oItemSection.PaperWeight2 ?? 0;
    //                                    }
    //                                    break;
    //                                case 3:
    //                                    if (oItemSection.PaperWeight3 == 0)
    //                                    {
    //                                        functionReturnValue = oItemSection.PaperWeight1 ?? 0;
    //                                    }
    //                                    else
    //                                    {
    //                                        functionReturnValue = oItemSection.PaperWeight3 ?? 0;
    //                                    }
    //                                    break;
    //                                //Case 4
    //                                //    ExecuteVariable = oItemSection.PaperWeight4
    //                                //Case 5
    //                                //    ExecuteVariable = oItemSection.PaperWeight5
    //                            }

    //                            break;
    //                        case (int)VariableProperty.PrintSheetQtyIncSpoilage_ProRata:
    //                            switch (CurrentQuantity)
    //                            {
    //                                case 1:
    //                                    functionReturnValue = Convert.ToDouble(oItemSection.PrintSheetQty1 + oItemSection.SetupSpoilage + (oItemSection.PrintSheetQty1 * oItemSection.RunningSpoilage / 100));
    //                                    break;
    //                                case 2:
    //                                    functionReturnValue = Convert.ToDouble(oItemSection.PrintSheetQty2 + oItemSection.SetupSpoilage + (oItemSection.PrintSheetQty2 * oItemSection.RunningSpoilage / 100));
    //                                    break;
    //                                case 3:
    //                                    functionReturnValue = Convert.ToDouble(oItemSection.PrintSheetQty3 + oItemSection.SetupSpoilage + (oItemSection.PrintSheetQty3 * oItemSection.RunningSpoilage / 100));
    //                                    break;
    //                                //Case 4
    //                                //    ExecuteVariable = oItemSection.PrintSheetQty4 + oItemSection.SetupSpoilage + (oItemSection.PrintSheetQty4 * oItemSection.RunningSpoilage / 100)
    //                                //Case 5
    //                                //    ExecuteVariable = oItemSection.PrintSheetQty5 + oItemSection.SetupSpoilage + (oItemSection.PrintSheetQty5 * oItemSection.RunningSpoilage / 100)
    //                            }

    //                            break;

    //                        case (int)VariableProperty.FinishedItemQtyIncSpoilage_ProRata:
    //                            switch (CurrentQuantity)
    //                            {
    //                                case 1:
    //                                    functionReturnValue = Convert.ToDouble(oItemSection.FinishedItemQty1);
    //                                    break;
    //                                case 2:
    //                                    if (oItemSection.FinishedItemQty2 == 0)
    //                                    {
    //                                        functionReturnValue = Convert.ToDouble(oItemSection.FinishedItemQty1);
    //                                    }
    //                                    else
    //                                    {
    //                                        functionReturnValue = Convert.ToDouble(oItemSection.FinishedItemQty2);
    //                                    }
    //                                    break;
    //                                case 3:
    //                                    if (oItemSection.FinishedItemQty3 == 0)
    //                                    {
    //                                        functionReturnValue = Convert.ToDouble(oItemSection.FinishedItemQty1);
    //                                    }
    //                                    else
    //                                    {
    //                                        functionReturnValue = Convert.ToDouble(oItemSection.FinishedItemQty3);
    //                                    }
    //                                    break;
    //                                //Case 4
    //                                //    ExecuteVariable = oItemSection.FinishedItemQty4
    //                                //Case 5
    //                                //    ExecuteVariable = oItemSection.FinishedItemQty5
    //                            }

    //                            break;
    //                        case (int)VariableProperty.NoOfSides:
    //                            if (Convert.ToBoolean(oItemSection.IsDoubleSided) == true)
    //                            {
    //                                functionReturnValue = 2;
    //                            }
    //                            else
    //                            {
    //                                functionReturnValue = 1;
    //                            }

    //                            break;
    //                        //case (int)VariableProperty.PressSizeRatio:
    //                        //    if (oItemSection.Press == null)
    //                        //    {
    //                        //        functionReturnValue = 0;
    //                        //    }
    //                        //    else
    //                        //    {
    //                        //        functionReturnValue = oItemSection.Press.PressSizeRatio;
    //                        //    }

    //                        //    break;

    //                        //case (int)VariableProperty.SectionPaperWeightExSelfQty_ProRata:
    //                        //    switch (CurrentQuantity)
    //                        //    {
    //                        //        case 1:
    //                        //            functionReturnValue = oItemSection.SectionPaperWeightExSelfQty1;
    //                        //            break;
    //                        //        case 2:
    //                        //            if (oItemSection.SectionPaperWeightExSelfQty2 == 0)
    //                        //            {
    //                        //                functionReturnValue = oItemSection.SectionPaperWeightExSelfQty1;
    //                        //            }
    //                        //            else
    //                        //            {
    //                        //                functionReturnValue = oItemSection.SectionPaperWeightExSelfQty2;
    //                        //            }
    //                        //            break;
    //                        //        case 3:
    //                        //            if (oItemSection.SectionPaperWeightExSelfQty3 == 0)
    //                        //            {
    //                        //                functionReturnValue = oItemSection.SectionPaperWeightExSelfQty1;
    //                        //            }
    //                        //            else
    //                        //            {
    //                        //                functionReturnValue = oItemSection.SectionPaperWeightExSelfQty3;
    //                        //            }
    //                        //            break;
    //                        //        //Case 4
    //                        //        //    ExecuteVariable = oItemSection.SectionPaperWeightExSelfQty4
    //                        //        //Case 5
    //                        //        //    ExecuteVariable = oItemSection.SectionPaperWeightExSelfQty5
    //                        //    }

    //                        //    break;

    //                        case (int)VariableProperty.WashupQty:
    //                            functionReturnValue = Convert.ToDouble(oItemSection.WashupQty);

    //                            break;
    //                        case (int)VariableProperty.MakeReadyQty:
    //                            functionReturnValue = Convert.ToDouble(oItemSection.MakeReadyQty);

    //                            break;
    //                        default:
    //                            functionReturnValue = 0;

    //                            break;
    //                    }


    //                }
    //                else if (oVariable.Type == 2)
    //                {
    //                    return _CostCentreVariableRepository.ExecUserVariable(oVariable);
    //                }
    //                else if (oVariable.Type == 3)
    //                {
    //                    return oVariable.VariableValue ?? 0;
    //                }


    //                oVariable = null;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new Exception("ExecuteVariable", ex);
    //        }
    //        return functionReturnValue;

    //    }



        //public double ExecuteResource(ref object[] oParamsArray, long ResourceID, string ReturnValue)
        //{
        //    double functionReturnValue = 0;

        //    try
        //    {
        //        CostCentreExecutionMode ExecutionMode = (CostCentreExecutionMode)oParamsArray[1];

        //        // If execution mode is for populating the Queue then return 0
        //        if (ExecutionMode == CostCentreExecutionMode.PromptMode)
        //        {
        //            return 0;

        //            //if its execution mode then

        //        }
        //        else if (ExecutionMode == CostCentreExecutionMode.ExecuteMode)
        //        {
        //            if (ReturnValue == "costperhour")
        //            {
        //                MPC.Repository.Repositories.CostCentre obj = new MPC.Repository.Repositories.CostCentre();

        //                functionReturnValue = obj.TestConnection(); // _CostCentreRepository.ExecuteUserResource(ResourceID, ResourceReturnType.CostPerHour);
        //                obj = null;
        //            }
        //            else
        //            {
        //                functionReturnValue = 0;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("ExecuteResource", ex);
        //    }
        //    return functionReturnValue;
        //}


        //public double ExecuteUserStockItem(int StockID, StockPriceType StockPriceType, out double Price ,out double PerQtyQty)
        //{
        //    try
        //    {
        //        return _CostCentreRepository.ExecuteUserStockItem(StockID, StockPriceType,out Price ,out PerQtyQty);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("ExecuteUserStockItem", ex);
        //    }
        //}
        ///// <summary>
        ///// Question Execution Function
        ///// </summary>
        ///// <param name="QuestionID"></param>
        ///// <param name="oConnection"></param>
        ///// <param name="ExecutionMode"></param>
        ///// <param name="QuestionQueue"></param>
        ///// <returns></returns>
        //public double ExecuteQuestion(ref object[] oParamsArray, int QuestionID, long CostCentreID)
        //{

        //    CostCentreExecutionMode ExecutionMode = (CostCentreExecutionMode)oParamsArray[1];
        //    ItemSection oItemSection = (ItemSection)oParamsArray[8];
        //    int CurrentQuantity = Convert.ToInt32(oParamsArray[5]);
        //    int MultipleQutantities = Convert.ToInt32(oParamsArray[4]);
        //    List<QuestionQueueItem> QuestionQueue = oParamsArray[2] as List<QuestionQueueItem>;



        //    bool bFlag = false;
        //    QuestionQueueItem QuestionItem = null;
        //    try
        //    {
        //        //check if its the visual mode or Execution Mode
        //        if (ExecutionMode == CostCentreExecutionMode.ExecuteMode)
        //        {
        //            //here the questions returned asnwer will ahave been loaded in the queue
        //            //retreive the queue answer for this question and use.. :D
        //            //use is simple only cast it in double and return..



        //            foreach (QuestionQueueItem item in QuestionQueue)
        //            {
        //                //matching
        //                if (item.ID == QuestionID & item.CostCentreID == CostCentreID)
        //                {
        //                    bFlag = true;
        //                    QuestionItem = item;
        //                    break; // TODO: might not be correct. Was : Exit For
        //                }
        //            }

        //            //if found question in queue then use its values
        //            if (bFlag == true)
        //            {
        //                //Return CDbl(item.Answer)
        //                //if MultipleQutantities
        //                //new multiple qty logic goes here

        //                if (CurrentQuantity <= MultipleQutantities)
        //                {
        //                    switch (CurrentQuantity)
        //                    {
        //                        case 1:
        //                            return Convert.ToDouble(QuestionItem.Qty1Answer);
        //                        case 2:
        //                            if (QuestionItem.Qty2Answer == 0)
        //                            {
        //                                return Convert.ToDouble(QuestionItem.Qty1Answer);
        //                            }
        //                            else
        //                            {
        //                                return Convert.ToDouble(QuestionItem.Qty2Answer);
        //                            }
        //                            break;
        //                        case 3:
        //                            if (Convert.ToDouble(QuestionItem.Qty3Answer) == 0)
        //                            {
        //                                return Convert.ToDouble(QuestionItem.Qty1Answer);
        //                            }
        //                            else
        //                            {
        //                                return Convert.ToDouble(QuestionItem.Qty3Answer);
        //                            }

        //                            break;
        //                    }
        //                }
        //                else
        //                {
        //                    throw new Exception("Invalid  Current Selected Multiple Quantitity");
        //                }
        //            }
        //            else
        //            {
        //                throw new Exception("Answer not find in Queue");
        //            }

        //        }
        //        else if (ExecutionMode == CostCentreExecutionMode.PromptMode)
        //        {
        //            //populate the question in the executionQueue
        //            //loading the Questions Information for populating in the Queue
        //            CostCentreQuestion ovariable = _CostCentreQuestionRepository.LoadQuestion(Convert.ToInt32(QuestionID));
        //            QuestionItem = new QuestionQueueItem(QuestionID, ovariable.QuestionString, CostCentreID, ovariable.Type.Value, ovariable.QuestionString, ovariable.DefaultAnswer, "", false, 0, 0, 0, 0, 0, 0,0,ovariable.AnswerCollection);
        //            QuestionQueue.Add(QuestionItem);
        //            ovariable = null;

        //            //exit normally 
        //        }
        //        return 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("ExecuteQuestion", ex);
        //    }

        //}

        ///// <summary>
        ///// Executes a Matrix
        ///// </summary>
        ///// <param name="MatrixID"></param>
        ///// <param name="oConnection"></param>
        ///// <param name="ExecutionMode"></param>
        ///// <param name="QuestionQueue"></param>
        ///// <returns>Double</returns>
        //public double ExecuteMatrix(ref object[] oParamsArray, int MatrixID, long CostCentreID)
        //{
        //    bool bFlag = false;


        //    CostCentreExecutionMode ExecutionMode = (CostCentreExecutionMode)oParamsArray[1];
        //    ItemSection oItemSection = (ItemSection)oParamsArray[8];
        //    int CurrentQuantity = Convert.ToInt32(oParamsArray[5]);
        //    int MultipleQutantities = Convert.ToInt32(oParamsArray[4]);
        //    List<QuestionQueueItem> QuestionQueue = oParamsArray[2] as List<QuestionQueueItem>;
        //    QuestionQueueItem QuestionItem = null;

        //    try
        //    {
        //        //check if its the visual mode or Execution Mode
        //        if (ExecutionMode == CostCentreExecutionMode.ExecuteMode)
        //        {
        //            //here the questions returned asnwer will ahave been loaded in the queue
        //            //retreive the queue answer for this question and use.. :D
        //            //use is simple only cast it in double and return..


        //            foreach (var item in QuestionQueue)
        //            {
        //                //matching
        //                if (item.ID == MatrixID & item.ItemType == 4)
        //                {
        //                    bFlag = true;
        //                    QuestionItem = item;
        //                    break; // TODO: might not be correct. Was : Exit For
        //                }
        //            }

        //            //if found question in queue then use its values
        //            if (bFlag == true)
        //            {
        //                //Return CDbl(item.Answer)
        //                //multiple qty logic goes here

        //                if (CurrentQuantity <= MultipleQutantities)
        //                {
        //                    switch (CurrentQuantity)
        //                    {
        //                        case 1:
        //                            return Convert.ToDouble(QuestionItem.Qty1Answer);
        //                        case 2:
        //                            if (Convert.ToDouble(QuestionItem.Qty2Answer) == 0)
        //                            {
        //                                return Convert.ToDouble(QuestionItem.Qty1Answer);
        //                            }
        //                            else
        //                            {
        //                                return Convert.ToDouble(QuestionItem.Qty2Answer);
        //                            }
        //                            break;
        //                        case 3:
        //                            if (Convert.ToDouble(QuestionItem.Qty3Answer) == 0)
        //                            {
        //                                return Convert.ToDouble(QuestionItem.Qty1Answer);
        //                            }
        //                            else
        //                            {
        //                                return Convert.ToDouble(QuestionItem.Qty3Answer);
        //                            }
        //                            break;
        //                    }
        //                }
        //                else
        //                {
        //                    throw new Exception("Invalid  Current Selected Multiple Quantitity");
        //                }
        //            }
        //            else
        //            {
        //                throw new Exception("Answer not found in Queue");
        //            }


        //        }
        //        else if (ExecutionMode == CostCentreExecutionMode.PromptMode)
        //        {
        //            //populate the question in the executionQueue
        //            //loading the Questions Information for populating in the Queue
        //            CostCentreMatrix oMatrix = _CostCentreMatrixRepository.GetMatrix(MatrixID);
        //           QuestionItem = new QuestionQueueItem(MatrixID, oMatrix.Name, CostCentreID, 4, oMatrix.Description, "", "", false,0,0,0,0,0, oMatrix.RowsCount, oMatrix.ColumnsCount);
        //           QuestionQueue.Add(QuestionItem);
        //            oMatrix = null;
                  
        //            //exit normally 
        //        }
        //        return 1;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("ExecuteMatrix", ex);
        //    }

        //}

        public List<CostCentre> GetCorporateDeliveryCostCentersList(long CompanyID)
        {

            try
            {
                return _CostCentreRepository.GetCorporateDeliveryCostCentersList(CompanyID);
            }
            catch (Exception ex)
            {
                throw ex;
            }



        }




        //public CostCentre GetCostCentersByID(long costCenterID)
        //{
        //    try
        //    {
        //        return _CostCentreRepository.GetCostCentersByID(costCenterID);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}
        //public string test() 
        //{
        //    return "Hello";
        //}
    }  



    


}
