using MPC.Models.Common;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Repository.Repositories
{
    public class CostCentreExecution
    {


        public CostCentreQuestion LoadQuestion(int QuestionID, string connectionString)
        {
            try
            {

                CostCentreQuestion QuestionObj = new CostCentreQuestion();

               // string connectionString = "Persist Security Info=False;Integrated Security=false;Initial Catalog=MPCLive;server=www.myprintcloud.com,9998; user id=mpcmissa; password=p@ssw0rd@mis2o14;";
             //   string connectionString = "Persist Security Info=False;Integrated Security=false;Initial Catalog=" + System.Configuration.ConfigurationManager.AppSettings["CostCentreConnectionStringDBName"] + ";server=" + System.Configuration.ConfigurationManager.AppSettings["CostCentreConnectionStringServerName"] + "; user id=" + System.Configuration.ConfigurationManager.AppSettings["CostCentreConnectionStringUserName"] + "; password=" + System.Configuration.ConfigurationManager.AppSettings["CostCentreConnectionStringPasswordName"] + ";";
                string queryString = "select * from CostCentreQuestion where Id = " + QuestionID;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    //
                    // Open the SqlConnection.
                    //
                    SqlCommand command = new SqlCommand(queryString, con);
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        QuestionObj.Id = reader.GetInt32(0);

                        QuestionObj.QuestionString = reader.GetString(1);

                        QuestionObj.Type = reader.GetInt16(2);

                        QuestionObj.DefaultAnswer = reader.IsDBNull(3) ? "0" : reader.GetString(3);
                        
                    }
                    reader.Close();
                    if (QuestionObj.Type == (int)QuestionType.MultipleChoiceQuestion)
                    {
                        List<CostCentreAnswer> AnsObj = new List<CostCentreAnswer>();
                        CostCentreAnswer CCObj = null;
                        string AnsqueryString = "select * from CostCentreAnswer where QuestionId = " + QuestionID;
                        command = new SqlCommand(AnsqueryString, con);
                        reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            CCObj = new CostCentreAnswer();

                            CCObj.Id = reader.GetInt32(0);

                            CCObj.QuestionId = reader.GetInt32(1);

                            CCObj.AnswerString = reader.GetDouble(2);

                            AnsObj.Add(CCObj);
                        }
                        QuestionObj.AnswerCollection = AnsObj.ToList();
                    }
                    return QuestionObj;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("LoadQuestions", ex);
            }
        }

        /// <summary>
        /// Get Matrix and its layout
        /// </summary>
        /// <param name="MatrixID"></param>
        /// <returns></returns>
        public CostCentreMatrix GetMatrix(int MatrixID, string connectionString)
        {
            try
            {

                // string connectionString = "Persist Security Info=False;Integrated Security=false;Initial Catalog=MPCLive;server=www.myprintcloud.com,9998; user id=mpcmissa; password=p@ssw0rd@mis2o14;";
               // string connectionString = "Persist Security Info=False;Integrated Security=false;Initial Catalog=MPCLive;server=192.168.1.22; user id=sa; password=p@ssw0rd;";
              //  string connectionString = "Persist Security Info=False;Integrated Security=false;Initial Catalog=" + System.Configuration.ConfigurationManager.AppSettings["CostCentreConnectionStringDBName"] + ";server=" + System.Configuration.ConfigurationManager.AppSettings["CostCentreConnectionStringServerName"] + "; user id=" + System.Configuration.ConfigurationManager.AppSettings["CostCentreConnectionStringUserName"] + "; password=" + System.Configuration.ConfigurationManager.AppSettings["CostCentreConnectionStringPasswordName"] + ";";
                string queryString = "select * from CostCentreMatrix where MatrixId = " + MatrixID;

                CostCentreMatrix oMatrix = new CostCentreMatrix();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    //
                    // Open the SqlConnection.
                    //
                    SqlCommand command = new SqlCommand(queryString, con);
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        oMatrix.MatrixId = reader.GetInt32(0);

                        oMatrix.Name = reader.GetString(1);
                        oMatrix.Description = reader.GetString(2);
                        oMatrix.RowsCount = reader.GetInt32(3);
                        oMatrix.ColumnsCount = reader.GetInt32(4);
                        oMatrix.OrganisationId = reader.GetInt32(5);
                        oMatrix.SystemSiteId = reader.GetInt32(6);

                    }
                    reader.Close();
                    List<CostCentreMatrixDetail> matrixDetail = new List<CostCentreMatrixDetail>();
                    CostCentreMatrixDetail oDetail = null;
                    string matrixDetailString = "select * from CostCentreMatrixDetail where MatrixId = " + MatrixID;
                    command = new SqlCommand(matrixDetailString, con);
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        oDetail = new CostCentreMatrixDetail();

                        oDetail.Id = reader.GetInt64(0);

                        oDetail.MatrixId = reader.GetInt32(1);

                        oDetail.Value = reader.GetString(2);

                        matrixDetail.Add(oDetail);
                    }
                    oMatrix.items = matrixDetail.ToList();
                    return oMatrix;
                }


            }
            catch (Exception ex)
            {
                throw new Exception("GetMatrix", ex);
            }
        }

        public double ExecuteUserResource(long ResourceID, ResourceReturnType oCostPerHour, string connectionString)
        {
            try
            {
                if (oCostPerHour == ResourceReturnType.CostPerHour)
                {
                    // string connectionString = "Persist Security Info=False;Integrated Security=false;Initial Catalog=MPCLive;server=www.myprintcloud.com,9998; user id=mpcmissa; password=p@ssw0rd@mis2o14;";
                 //   string connectionString = "Persist Security Info=False;Integrated Security=false;Initial Catalog=" + System.Configuration.ConfigurationManager.AppSettings["CostCentreConnectionStringDBName"] + ";server=" + System.Configuration.ConfigurationManager.AppSettings["CostCentreConnectionStringServerName"] + "; user id=" + System.Configuration.ConfigurationManager.AppSettings["CostCentreConnectionStringUserName"] + "; password=" + System.Configuration.ConfigurationManager.AppSettings["CostCentreConnectionStringPasswordName"] + ";";
                    string queryString = "select CostPerHour from SystemUser where SystemUserId = " + ResourceID;

                    double result = 0;
                    //
                    // In a using statement, acquire the SqlConnection as a resource.
                    //
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        //
                        // Open the SqlConnection.
                        //
                        SqlCommand command = new SqlCommand(queryString, con);
                        con.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {

                            result = Convert.ToDouble(reader.GetDouble(14));
                        }
                        reader.Close();
                        return result;
                    }

                }
                else
                    return 0;


            }
            catch (Exception ex)
            {
                throw new Exception("", ex);
            }

        }

        public double ExecuteUserStockItem(int StockID, StockPriceType StockPriceType, string connectionString, out double Price, out double PerQtyQty)
        {
            try
            {
                ObjectParameter paramPrice = new ObjectParameter("Price", typeof(float));
                ObjectParameter paramQty = new ObjectParameter("PerQtyQty", typeof(float));


                // string connectionString = "Persist Security Info=False;Integrated Security=false;Initial Catalog=MPCLive;server=www.myprintcloud.com,9998; user id=mpcmissa; password=p@ssw0rd@mis2o14;";
                //string connectionString = "Persist Security Info=False;Integrated Security=false;Initial Catalog=" + System.Configuration.ConfigurationManager.AppSettings["CostCentreConnectionStringDBName"] + ";server=" + System.Configuration.ConfigurationManager.AppSettings["CostCentreConnectionStringServerName"] + "; user id=" + System.Configuration.ConfigurationManager.AppSettings["CostCentreConnectionStringUserName"] + "; password=" + System.Configuration.ConfigurationManager.AppSettings["CostCentreConnectionStringPasswordName"] + ";";
                double result = 0;
                //
                // In a using statement, acquire the SqlConnection as a resource.
                //
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    //
                    // Open the SqlConnection.
                    //

                    SqlCommand command = new SqlCommand("sp_CostCentreExecution_get_StockPriceByCalculationType", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@StockID", StockID));
                    command.Parameters.Add(new SqlParameter("@CalculationType", (int)StockPriceType));
                    command.Parameters.Add(new SqlParameter("@returnPrice", paramPrice));
                    command.Parameters.Add(new SqlParameter("@PerQtyQty", paramQty));
                    con.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        paramPrice = (ObjectParameter)reader["returnPrice"];
                        paramQty = (ObjectParameter)reader["PerQtyQty"];
                    }
                    reader.Close();
                    Price = Convert.ToDouble(paramPrice);
                    PerQtyQty = Convert.ToDouble(paramQty);
                    return Price;
                }


                //db.sp_CostCentreExecution_get_StockPriceByCalculationType(StockID, (int)StockPriceType, paramPrice, paramQty);
                //Price = Convert.ToDouble(paramPrice);
                //PerQtyQty = Convert.ToDouble(paramQty);
                //return Price;
            }
            catch (Exception ex)
            {
                throw new Exception("ExecuteUserStockItem", ex);
            }
        }


        /// <summary>
        /// Function which returns the required Variable object
        /// </summary>
        /// <param name="VariableId">VariableID</param>
        /// <param name="oConnection">Open DB connection</param>
        /// <returns>Variable</returns>
        public CostCentreVariable LoadVariable(int VariableId, string connectionString)
        {
            try
            {
                CostCentreVariable oVariable = new CostCentreVariable();

                // string connectionString = "Persist Security Info=False;Integrated Security=false;Initial Catalog=MPCLive;server=www.myprintcloud.com,9998; user id=mpcmissa; password=p@ssw0rd@mis2o14;";
                //string connectionString = "Persist Security Info=False;Integrated Security=false;Initial Catalog=" + System.Configuration.ConfigurationManager.AppSettings["CostCentreConnectionStringDBName"] + ";server=" + System.Configuration.ConfigurationManager.AppSettings["CostCentreConnectionStringServerName"] + "; user id=" + System.Configuration.ConfigurationManager.AppSettings["CostCentreConnectionStringUserName"] + "; password=" + System.Configuration.ConfigurationManager.AppSettings["CostCentreConnectionStringPasswordName"] + ";";

                string queryString = "select * from CostCentreVariable where VarId = " + VariableId;

                //
                // In a using statement, acquire the SqlConnection as a resource.
                //
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    //
                    // Open the SqlConnection.
                    //
                    SqlCommand command = new SqlCommand(queryString, con);
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        oVariable.VarId = reader.GetInt32(0);
                        oVariable.Name = reader.GetString(1);
                        oVariable.RefTableName = reader.IsDBNull(2) == true ? "" : reader.GetString(2);
                        oVariable.RefFieldName = reader.IsDBNull(3) == true ? "" : reader.GetString(3);
                        oVariable.CriteriaFieldName = reader.IsDBNull(4) == true ? "" : reader.GetString(4);
                        oVariable.Criteria = reader.IsDBNull(5) == true ? "" : reader.GetString(5);
                        oVariable.CategoryId = reader.GetInt32(6);
                        oVariable.IsCriteriaUsed = reader.IsDBNull(7) == true ? "" : reader.GetString(7);
                        oVariable.Type = reader.GetInt16(8);
                        oVariable.PropertyType = reader.IsDBNull(9) == true ? 0 : reader.GetInt32(9);  
                        if (!reader.IsDBNull(10))
                        {
                            oVariable.VariableDescription = reader.GetString(10);
                        }
                        if (!reader.IsDBNull(11))
                        {
                            oVariable.VariableValue = reader.GetDouble(11); 
                        }
                        oVariable.SystemSiteId = reader.GetInt32(12);
                       
                    }
                    reader.Close();
                    return oVariable;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("", ex);
            }
        }

        /// <summary>
        /// Executes a variable with criteria defined in it
        /// </summary>
        /// <param name="Variable">VVariable object</param>
        /// <param name="oConnection">DB Connection</param>
        /// <returns>double</returns>
        public double ExecUserVariable(CostCentreVariable oVariable, string connectionString)
        {
            string sSqlString = null;
            double dResult = 0;

            try
            {
                // string connectionString = "Persist Security Info=False;Integrated Security=false;Initial Catalog=MPCLive;server=www.myprintcloud.com,9998; user id=mpcmissa; password=p@ssw0rd@mis2o14;";
              //  string connectionString = "Persist Security Info=False;Integrated Security=false;Initial Catalog=" + System.Configuration.ConfigurationManager.AppSettings["CostCentreConnectionStringDBName"] + ";server=" + System.Configuration.ConfigurationManager.AppSettings["CostCentreConnectionStringServerName"] + "; user id=" + System.Configuration.ConfigurationManager.AppSettings["CostCentreConnectionStringUserName"] + "; password=" + System.Configuration.ConfigurationManager.AppSettings["CostCentreConnectionStringPasswordName"] + ";";
                    string queryString = "";

                    if (Convert.ToBoolean(oVariable.IsCriteriaUsed) == true)
                    {
                        queryString = "select * from " + oVariable.RefTableName + " where " + oVariable.CriteriaFieldName + "= " + oVariable.Criteria + "";


                    }
                    else
                    {
                        sSqlString = "select * from " + oVariable.RefTableName;
                    }

                    double result = 0;
                    //
                    // In a using statement, acquire the SqlConnection as a resource.
                    //
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        //
                        // Open the SqlConnection.
                        //
                        SqlCommand command = new SqlCommand(queryString, con);
                        con.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {

                            result = Convert.ToDouble(reader.GetString(4));
                            break;
                        }
                        reader.Close();
                        return result;
                    }


            }
            catch (Exception ex)
            {
                throw new Exception("", ex);
            }
        }


        public double CalculateLookup(int MethodID, double InPutQuantity, ClickChargeReturnType ReturnValue, string connectionString = "")
        {
            try
            {
                int Sheets = 0;
                double Cost = 0;
                double Price = 0;

                int[] rngFrom = new int[15];
                int[] rngTo = new int[15];
                int[] dblClickSheet = new int[15];
                double[] dblClickCost = new double[15];
                double[] dblClickPrice = new double[15];
                MachineClickChargeZone oModelClickChargeZone = null;
                //Getting the Values for the LookUp
                //-----------Commented by Naveed on 20160718-----------
                //using(MPC.Repository.BaseRepository.BaseDbContext db = new BaseRepository.BaseDbContext())
                //{
                //    oModelClickChargeZone = db.MachineClickChargeZones.Where(a => a.MethodId == MethodID).FirstOrDefault();
                //}
                //------------------------------------------------------
                //Code added replacing above commented code
                if (!string.IsNullOrEmpty(connectionString))
                {
                    oModelClickChargeZone = LoadClickChargeZone(MethodID, connectionString);
                }
                if (oModelClickChargeZone == null)
                    return 0;
                double TimePerChargeableSheets = Convert.ToDouble(oModelClickChargeZone.TimePerHour);

                //Setting the Range and Rate 
                rngFrom[0] = Convert.ToInt32(oModelClickChargeZone.From1);
                rngFrom[1] = Convert.ToInt32(oModelClickChargeZone.From2);
                rngFrom[2] = Convert.ToInt32(oModelClickChargeZone.From3);
                rngFrom[3] = Convert.ToInt32(oModelClickChargeZone.From4);
                rngFrom[4] = Convert.ToInt32(oModelClickChargeZone.From5);
                rngFrom[5] = Convert.ToInt32(oModelClickChargeZone.From6);
                rngFrom[6] = Convert.ToInt32(oModelClickChargeZone.From7);
                rngFrom[7] = Convert.ToInt32(oModelClickChargeZone.From8);
                rngFrom[8] = Convert.ToInt32(oModelClickChargeZone.From9);
                rngFrom[9] = Convert.ToInt32(oModelClickChargeZone.From10);
                rngFrom[10] = Convert.ToInt32(oModelClickChargeZone.From11);
                rngFrom[11] = Convert.ToInt32(oModelClickChargeZone.From12);
                rngFrom[12] = Convert.ToInt32(oModelClickChargeZone.From13);
                rngFrom[13] = Convert.ToInt32(oModelClickChargeZone.From14);
                rngFrom[14] = Convert.ToInt32(oModelClickChargeZone.From15);
                rngTo[0] = Convert.ToInt32(oModelClickChargeZone.To1);
                rngTo[1] = Convert.ToInt32(oModelClickChargeZone.To2);
                rngTo[2] = Convert.ToInt32(oModelClickChargeZone.To3);
                rngTo[3] = Convert.ToInt32(oModelClickChargeZone.To4);
                rngTo[4] = Convert.ToInt32(oModelClickChargeZone.To5);
                rngTo[5] = Convert.ToInt32(oModelClickChargeZone.To6);
                rngTo[6] = Convert.ToInt32(oModelClickChargeZone.To7);
                rngTo[7] = Convert.ToInt32(oModelClickChargeZone.To8);
                rngTo[8] = Convert.ToInt32(oModelClickChargeZone.To9);
                rngTo[9] = Convert.ToInt32(oModelClickChargeZone.To10);
                rngTo[10] = Convert.ToInt32(oModelClickChargeZone.To11);
                rngTo[11] = Convert.ToInt32(oModelClickChargeZone.To12);
                rngTo[12] = Convert.ToInt32(oModelClickChargeZone.To13);
                rngTo[13] = Convert.ToInt32(oModelClickChargeZone.To14);
                rngTo[14] = Convert.ToInt32(oModelClickChargeZone.To15);
                dblClickCost[0] = Convert.ToDouble(oModelClickChargeZone.SheetCost1);
                dblClickCost[1] = Convert.ToDouble(oModelClickChargeZone.SheetCost2);
                dblClickCost[2] = Convert.ToDouble(oModelClickChargeZone.SheetCost3);
                dblClickCost[3] = Convert.ToDouble(oModelClickChargeZone.SheetCost4);
                dblClickCost[4] = Convert.ToDouble(oModelClickChargeZone.SheetCost5);
                dblClickCost[5] = Convert.ToDouble(oModelClickChargeZone.SheetCost6);
                dblClickCost[6] = Convert.ToDouble(oModelClickChargeZone.SheetCost7);
                dblClickCost[7] = Convert.ToDouble(oModelClickChargeZone.SheetCost8);
                dblClickCost[8] = Convert.ToDouble(oModelClickChargeZone.SheetCost9);
                dblClickCost[9] = Convert.ToDouble(oModelClickChargeZone.SheetCost10);
                dblClickCost[10] = Convert.ToDouble(oModelClickChargeZone.SheetCost11);
                dblClickCost[11] = Convert.ToDouble(oModelClickChargeZone.SheetCost12);
                dblClickCost[12] = Convert.ToDouble(oModelClickChargeZone.SheetCost13);
                dblClickCost[13] = Convert.ToDouble(oModelClickChargeZone.SheetCost14);
                dblClickCost[14] = Convert.ToDouble(oModelClickChargeZone.SheetCost15);
                dblClickPrice[0] = Convert.ToDouble(oModelClickChargeZone.SheetPrice1);
                dblClickPrice[1] = Convert.ToDouble(oModelClickChargeZone.SheetPrice2);
                dblClickPrice[2] = Convert.ToDouble(oModelClickChargeZone.SheetPrice3);
                dblClickPrice[3] = Convert.ToDouble(oModelClickChargeZone.SheetPrice4);
                dblClickPrice[4] = Convert.ToDouble(oModelClickChargeZone.SheetPrice5);
                dblClickPrice[5] = Convert.ToDouble(oModelClickChargeZone.SheetPrice6);
                dblClickPrice[6] = Convert.ToDouble(oModelClickChargeZone.SheetPrice7);
                dblClickPrice[7] = Convert.ToDouble(oModelClickChargeZone.SheetPrice8);
                dblClickPrice[8] = Convert.ToDouble(oModelClickChargeZone.SheetPrice9);
                dblClickPrice[9] = Convert.ToDouble(oModelClickChargeZone.SheetPrice10);
                dblClickPrice[10] = Convert.ToDouble(oModelClickChargeZone.SheetPrice11);
                dblClickPrice[11] = Convert.ToDouble(oModelClickChargeZone.SheetPrice12);
                dblClickPrice[12] = Convert.ToDouble(oModelClickChargeZone.SheetPrice13);
                dblClickPrice[13] = Convert.ToDouble(oModelClickChargeZone.SheetPrice14);
                dblClickPrice[14] = Convert.ToDouble(oModelClickChargeZone.SheetPrice15);
                dblClickSheet[0] = Convert.ToInt32(oModelClickChargeZone.Sheets1);
                dblClickSheet[1] = Convert.ToInt32(oModelClickChargeZone.Sheets2);
                dblClickSheet[2] = Convert.ToInt32(oModelClickChargeZone.Sheets3);
                dblClickSheet[3] = Convert.ToInt32(oModelClickChargeZone.Sheets4);
                dblClickSheet[4] = Convert.ToInt32(oModelClickChargeZone.Sheets5);
                dblClickSheet[5] = Convert.ToInt32(oModelClickChargeZone.Sheets6);
                dblClickSheet[6] = Convert.ToInt32(oModelClickChargeZone.Sheets7);
                dblClickSheet[7] = Convert.ToInt32(oModelClickChargeZone.Sheets8);
                dblClickSheet[8] = Convert.ToInt32(oModelClickChargeZone.Sheets9);
                dblClickSheet[9] = Convert.ToInt32(oModelClickChargeZone.Sheets10);
                dblClickSheet[10] = Convert.ToInt32(oModelClickChargeZone.Sheets11);
                dblClickSheet[11] = Convert.ToInt32(oModelClickChargeZone.Sheets12);
                dblClickSheet[12] = Convert.ToInt32(oModelClickChargeZone.Sheets13);
                dblClickSheet[13] = Convert.ToInt32(oModelClickChargeZone.Sheets14);
                dblClickSheet[14] = Convert.ToInt32(oModelClickChargeZone.Sheets15);

                int iInputQty = Convert.ToInt32(InPutQuantity);
                if (iInputQty >= rngFrom[0] && iInputQty <= rngTo[0])
                {
                    Sheets = dblClickSheet[0];
                    Cost = dblClickCost[0];
                    Price = dblClickPrice[0];
                }
                else if (iInputQty >= rngFrom[1] && iInputQty <= rngTo[1])
                {
                    Sheets = dblClickSheet[1];
                    Cost = dblClickCost[1];
                    Price = dblClickPrice[1];
                }
                else if (iInputQty >= rngFrom[2] && iInputQty <= rngTo[2])
                {
                    Sheets = dblClickSheet[2];
                    Cost = dblClickCost[2];
                    Price = dblClickPrice[2];
                }
                else if (iInputQty >= rngFrom[3] && iInputQty <= rngTo[3])
                {
                    Sheets = dblClickSheet[3];
                    Cost = dblClickCost[3];
                    Price = dblClickPrice[3];
                }
                else if (iInputQty >= rngFrom[4] && iInputQty <= rngTo[4])
                {
                    Sheets = dblClickSheet[4];
                    Cost = dblClickCost[4];
                    Price = dblClickPrice[4];
                }
                else if (iInputQty >= rngFrom[5] && iInputQty <= rngTo[5])
                {
                    Sheets = dblClickSheet[5];
                    Cost = dblClickCost[5];
                    Price = dblClickPrice[5];
                }
                else if (iInputQty >= rngFrom[6] && iInputQty <= rngTo[6])
                {
                    Sheets = dblClickSheet[6];
                    Cost = dblClickCost[6];
                    Price = dblClickPrice[6];
                }
                else if (iInputQty >= rngFrom[7] && iInputQty <= rngTo[7])
                {
                    Sheets = dblClickSheet[7];
                    Cost = dblClickCost[7];
                    Price = dblClickPrice[7];
                }
                else if (iInputQty >= rngFrom[8] && iInputQty <= rngTo[8])
                {
                    Sheets = dblClickSheet[8];
                    Cost = dblClickCost[8];
                    Price = dblClickPrice[8];
                }
                else if (iInputQty >= rngFrom[9] && iInputQty <= rngTo[9])
                {
                    Sheets = dblClickSheet[9];
                    Cost = dblClickCost[9];
                    Price = dblClickPrice[9];
                }
                else if (iInputQty >= rngFrom[10] && iInputQty <= rngTo[10])
                {
                    Sheets = dblClickSheet[10];
                    Cost = dblClickCost[10];
                    Price = dblClickPrice[10];
                }
                else if (iInputQty >= rngFrom[11] && iInputQty <= rngTo[11])
                {
                    Sheets = dblClickSheet[11];
                    Cost = dblClickCost[11];
                    Price = dblClickPrice[11];
                }
                else if (iInputQty >= rngFrom[12] && iInputQty <= rngTo[12])
                {
                    Sheets = dblClickSheet[12];
                    Cost = dblClickCost[12];
                    Price = dblClickPrice[12];
                }
                else if (iInputQty >= rngFrom[13] && iInputQty <= rngTo[13])
                {
                    Sheets = dblClickSheet[13];
                    Cost = dblClickCost[13];
                    Price = dblClickPrice[13];
                }
                else if (iInputQty >= rngFrom[14] && iInputQty <= rngTo[14])
                {
                    Sheets = dblClickSheet[14];
                    Cost = dblClickCost[14];
                    Price = dblClickPrice[14];
                }
                //Checking Total Passes in the Press
                

                if (ReturnValue == ClickChargeReturnType.Cost)
                {
                    return Sheets * Cost;
                }
                else
                {
                    return Sheets * Price;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("CalculateLookup", ex);
            }


        }

        public MachineClickChargeZone LoadClickChargeZone(int zoneId, string connectionString)
        {
            try
            {
                MachineClickChargeZone oZone = new MachineClickChargeZone();

                string queryString = "select * from MachineClickChargeZone where Id = " + zoneId;

               using (SqlConnection con = new SqlConnection(connectionString))
                {
                    //
                    // Open the SqlConnection.
                    //
                    SqlCommand command = new SqlCommand(queryString, con);
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        oZone.Id = zoneId;
                        oZone.ZoneName = reader.GetString(82);
                        oZone.From1 = reader.IsDBNull(2) ? 0 : Convert.ToInt64(reader.GetInt64(2));
                        oZone.To1 = reader.IsDBNull(3) ? 0 : Convert.ToInt64(reader.GetInt64(3));
                        oZone.Sheets1 = reader.IsDBNull(4) ? 0 : Convert.ToInt64(reader.GetInt64(4));
                        oZone.SheetCost1 = reader.IsDBNull(5) ? 0 : Convert.ToDouble(reader.GetDouble(5));
                        oZone.SheetPrice1 = reader.IsDBNull(6) ? 0 : Convert.ToDouble(reader.GetDouble(6));

                        oZone.From2 = reader.IsDBNull(7) ? 0 : Convert.ToInt64(reader.GetInt64(7));
                        oZone.To2 = reader.IsDBNull(8) ? 0 : Convert.ToInt64(reader.GetInt64(8));
                        oZone.Sheets2 = reader.IsDBNull(9) ? 0 : Convert.ToInt64(reader.GetInt64(9));
                        oZone.SheetCost2 = reader.IsDBNull(10) ? 0 : Convert.ToDouble(reader.GetDouble(10));
                        oZone.SheetPrice2 = reader.IsDBNull(11) ? 0 : Convert.ToDouble(reader.GetDouble(11));

                        oZone.From3 = reader.IsDBNull(12) ? 0 : Convert.ToInt64(reader.GetInt64(12));
                        oZone.To3 = reader.IsDBNull(13) ? 0 : Convert.ToInt64(reader.GetInt64(13));
                        oZone.Sheets3 = reader.IsDBNull(14) ? 0 : Convert.ToInt64(reader.GetInt64(14));
                        oZone.SheetCost3 = reader.IsDBNull(15) ? 0 : Convert.ToDouble(reader.GetDouble(15));
                        oZone.SheetPrice3 = reader.IsDBNull(16) ? 0 : Convert.ToDouble(reader.GetDouble(16));

                        oZone.From4 = reader.IsDBNull(17) ? 0 : Convert.ToInt64(reader.GetInt64(17));
                        oZone.To4 = reader.IsDBNull(18) ? 0 : Convert.ToInt64(reader.GetInt64(18));
                        oZone.Sheets4 = reader.IsDBNull(19) ? 0 : Convert.ToInt64(reader.GetInt64(19));
                        oZone.SheetCost4 = reader.IsDBNull(20) ? 0 : Convert.ToDouble(reader.GetDouble(20));
                        oZone.SheetPrice4 = reader.IsDBNull(21) ? 0 : Convert.ToDouble(reader.GetDouble(21));

                        oZone.From5 = reader.IsDBNull(22) ? 0 : Convert.ToInt64(reader.GetInt64(22));
                        oZone.To5 = reader.IsDBNull(23) ? 0 : Convert.ToInt64(reader.GetInt64(23));
                        oZone.Sheets5 = reader.IsDBNull(24) ? 0 : Convert.ToInt64(reader.GetInt64(24));
                        oZone.SheetCost5 = reader.IsDBNull(25) ? 0 : Convert.ToDouble(reader.GetDouble(25));
                        oZone.SheetPrice5 = reader.IsDBNull(26) ? 0 : Convert.ToDouble(reader.GetDouble(26));

                        oZone.From6 = reader.IsDBNull(27) ? 0 : Convert.ToInt64(reader.GetInt64(27));
                        oZone.To6 = reader.IsDBNull(28) ? 0 : Convert.ToInt64(reader.GetInt64(28));
                        oZone.Sheets6 = reader.IsDBNull(29) ? 0 : Convert.ToInt64(reader.GetInt64(29));
                        oZone.SheetCost6 = reader.IsDBNull(30) ? 0 : Convert.ToDouble(reader.GetDouble(30));
                        oZone.SheetPrice6 = reader.IsDBNull(31) ? 0 : Convert.ToDouble(reader.GetDouble(31));
                        //-------
                        oZone.From7 = reader.IsDBNull(32) ? 0 : Convert.ToInt64(reader.GetInt64(32));
                        oZone.To7 = reader.IsDBNull(33) ? 0 : Convert.ToInt64(reader.GetInt64(33));
                        oZone.Sheets7 = reader.IsDBNull(34) ? 0 : Convert.ToInt64(reader.GetInt64(34));
                        oZone.SheetCost7 = reader.IsDBNull(35) ? 0 : Convert.ToDouble(reader.GetDouble(35));
                        oZone.SheetPrice7 = reader.IsDBNull(36) ? 0 : Convert.ToDouble(reader.GetDouble(36));

                        oZone.From8 = reader.IsDBNull(37) ? 0 : Convert.ToInt64(reader.GetInt64(37));
                        oZone.To8 = reader.IsDBNull(38) ? 0 : Convert.ToInt64(reader.GetInt64(38));
                        oZone.Sheets8 = reader.IsDBNull(39) ? 0 : Convert.ToInt64(reader.GetInt64(39));
                        oZone.SheetCost8 = reader.IsDBNull(40) ? 0 : Convert.ToDouble(reader.GetDouble(40));
                        oZone.SheetPrice8 = reader.IsDBNull(41) ? 0 : Convert.ToDouble(reader.GetDouble(41));

                        oZone.From9 = reader.IsDBNull(42) ? 0 : Convert.ToInt64(reader.GetInt64(42));
                        oZone.To9 = reader.IsDBNull(43) ? 0 : Convert.ToInt64(reader.GetInt64(43));
                        oZone.Sheets9 = reader.IsDBNull(44) ? 0 : Convert.ToInt64(reader.GetInt64(44));
                        oZone.SheetCost9 = reader.IsDBNull(45) ? 0 : Convert.ToDouble(reader.GetDouble(45));
                        oZone.SheetPrice9 = reader.IsDBNull(46) ? 0 : Convert.ToDouble(reader.GetDouble(46));

                        oZone.From10 = reader.IsDBNull(47) ? 0 : Convert.ToInt64(reader.GetInt64(47));
                        oZone.To10 = reader.IsDBNull(48) ? 0 : Convert.ToInt64(reader.GetInt64(48));
                        oZone.Sheets10 = reader.IsDBNull(49) ? 0 : Convert.ToInt64(reader.GetInt64(49));
                        oZone.SheetCost10 = reader.IsDBNull(50) ? 0 : Convert.ToDouble(reader.GetDouble(50));
                        oZone.SheetPrice10 = reader.IsDBNull(51) ? 0 : Convert.ToDouble(reader.GetDouble(51));

                        oZone.From11 = reader.IsDBNull(52) ? 0 : Convert.ToInt64(reader.GetInt64(52));
                        oZone.To11 = reader.IsDBNull(53) ? 0 : Convert.ToInt64(reader.GetInt64(53));
                        oZone.Sheets11 = reader.IsDBNull(54) ? 0 : Convert.ToInt64(reader.GetInt64(54));
                        oZone.SheetCost11 = reader.IsDBNull(55) ? 0 : Convert.ToDouble(reader.GetDouble(55));
                        oZone.SheetPrice11 = reader.IsDBNull(56) ? 0 : Convert.ToDouble(reader.GetDouble(56));

                        oZone.From12 = reader.IsDBNull(57) ? 0 : Convert.ToInt64(reader.GetInt64(57));
                        oZone.To12 = reader.IsDBNull(58) ? 0 : Convert.ToInt64(reader.GetInt64(58));
                        oZone.Sheets12 = reader.IsDBNull(59) ? 0 : Convert.ToInt64(reader.GetInt64(59));
                        oZone.SheetCost12 = reader.IsDBNull(60) ? 0 : Convert.ToDouble(reader.GetDouble(60));
                        oZone.SheetPrice12 = reader.IsDBNull(61) ? 0 : Convert.ToDouble(reader.GetDouble(61));

                        oZone.From13 = reader.IsDBNull(62) ? 0 : Convert.ToInt64(reader.GetInt64(62));
                        oZone.To13 = reader.IsDBNull(63) ? 0 : Convert.ToInt64(reader.GetInt64(63));
                        oZone.Sheets13 = reader.IsDBNull(64) ? 0 : Convert.ToInt64(reader.GetInt64(64));
                        oZone.SheetCost13 = reader.IsDBNull(65) ? 0 : Convert.ToDouble(reader.GetDouble(65));
                        oZone.SheetPrice13 = reader.IsDBNull(66) ? 0 : Convert.ToDouble(reader.GetDouble(66));

                        oZone.From14 = reader.IsDBNull(67) ? 0 : Convert.ToInt64(reader.GetInt64(67));
                        oZone.To14 = reader.IsDBNull(68) ? 0 : Convert.ToInt64(reader.GetInt64(68));
                        oZone.Sheets14 = reader.IsDBNull(69) ? 0 : Convert.ToInt64(reader.GetInt64(69));
                        oZone.SheetCost14 = reader.IsDBNull(70) ? 0 : Convert.ToDouble(reader.GetDouble(70));
                        oZone.SheetPrice14 = reader.IsDBNull(71) ? 0 : Convert.ToDouble(reader.GetDouble(71));

                        oZone.From15 = reader.IsDBNull(72) ? 0 : Convert.ToInt64(reader.GetInt64(72));
                        oZone.To15 = reader.IsDBNull(73) ? 0 : Convert.ToInt64(reader.GetInt64(73));
                        oZone.Sheets15 = reader.IsDBNull(74) ? 0 : Convert.ToInt64(reader.GetInt64(74));
                        oZone.SheetCost15 = reader.IsDBNull(75) ? 0 : Convert.ToDouble(reader.GetDouble(75));
                        oZone.SheetPrice15 = reader.IsDBNull(76) ? 0 : Convert.ToDouble(reader.GetDouble(76));

                    }
                    reader.Close();
                    return oZone;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("", ex);
            }
        }

    }
}
