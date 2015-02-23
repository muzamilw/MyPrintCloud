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


        public CostCentreQuestion LoadQuestion(int QuestionID)
        {
            try
            {

                CostCentreQuestion QuestionObj = new CostCentreQuestion();

                string connectionString = "Persist Security Info=False;Integrated Security=false;Initial Catalog=MPC;server=www.myprintcloud.com,9998; user id=mpcmissa; password=p@ssw0rd@mis2o14;";

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
        public CostCentreMatrix GetMatrix(int MatrixID)
        {
            try
            {

                string connectionString = "Persist Security Info=False;Integrated Security=false;Initial Catalog=MPC;server=www.myprintcloud.com,9998; user id=mpcmissa; password=p@ssw0rd@mis2o14;";

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

        public double ExecuteUserResource(long ResourceID, ResourceReturnType oCostPerHour)
        {
            try
            {
                if (oCostPerHour == ResourceReturnType.CostPerHour)
                {
                    string connectionString = "Persist Security Info=False;Integrated Security=false;Initial Catalog=MPC;server=www.myprintcloud.com,9998; user id=mpcmissa; password=p@ssw0rd@mis2o14;";

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

        public double ExecuteUserStockItem(int StockID, StockPriceType StockPriceType, out double Price, out double PerQtyQty)
        {
            try
            {
                ObjectParameter paramPrice = new ObjectParameter("Price", typeof(float));
                ObjectParameter paramQty = new ObjectParameter("PerQtyQty", typeof(float));


                string connectionString = "Persist Security Info=False;Integrated Security=false;Initial Catalog=MPC;server=www.myprintcloud.com,9998; user id=mpcmissa; password=p@ssw0rd@mis2o14;";

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
        public CostCentreVariable LoadVariable(int VariableId)
        {
            try
            {
                CostCentreVariable oVariable = new CostCentreVariable();

                string connectionString = "Persist Security Info=False;Integrated Security=false;Initial Catalog=MPC;server=www.myprintcloud.com,9998; user id=mpcmissa; password=p@ssw0rd@mis2o14;";

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
                        oVariable.RefTableName = reader.GetString(2);
                        oVariable.RefFieldName = reader.GetString(3);
                        oVariable.CriteriaFieldName = reader.GetString(4);
                        oVariable.Criteria = reader.GetString(5);
                        oVariable.CategoryId = reader.GetInt32(6);
                        oVariable.IsCriteriaUsed = reader.GetString(7);
                        oVariable.Type = reader.GetInt16(8);
                        oVariable.PropertyType = reader.GetInt32(9);
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
        public double ExecUserVariable(CostCentreVariable oVariable)
        {
            string sSqlString = null;
            double dResult = 0;

            try
            {
                    string connectionString = "Persist Security Info=False;Integrated Security=false;Initial Catalog=MPC;server=www.myprintcloud.com,9998; user id=mpcmissa; password=p@ssw0rd@mis2o14;";

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
    }
}
