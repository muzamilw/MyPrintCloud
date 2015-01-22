using MPC.Models.Common;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Repository.Repositories
{
    public class CostCentreExecution
    {

        public double TestConnection(long ResourceID)
        {
            try
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

                        result = Convert.ToDouble(reader[0]);
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
                    }
                    reader.Close();
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
        //public CostCentreMatrix GetMatrix(int MatrixID)
        //{
        //    try
        //    {

        //        string connectionString = "Persist Security Info=False;Integrated Security=false;Initial Catalog=MPC;server=www.myprintcloud.com,9998; user id=mpcmissa; password=p@ssw0rd@mis2o14;";

        //        string queryString = "select * from CostCentreQuestion where Id = " + QuestionID;

        //        using (SqlConnection con = new SqlConnection(connectionString))
        //        {
        //            //
        //            // Open the SqlConnection.
        //            //
        //            SqlCommand command = new SqlCommand(queryString, con);
        //            con.Open();
        //            SqlDataReader reader = command.ExecuteReader();
        //            while (reader.Read())
        //            {
        //                QuestionObj.Id = reader.GetInt32(0);

        //                QuestionObj.QuestionString = reader.GetString(1);

        //                QuestionObj.Type = reader.GetInt16(2);

        //                if (QuestionObj.Type == (int)QuestionType.MultipleChoiceQuestion)
        //                {
        //                    List<CostCentreAnswer> AnsObj = new List<CostCentreAnswer>();
        //                    CostCentreAnswer CCObj = null;
        //                    string AnsqueryString = "select * from CostCentreAnswer where QuestionId = " + QuestionID;
        //                    command = new SqlCommand(AnsqueryString, con);
        //                    reader = command.ExecuteReader();
        //                    while (reader.Read())
        //                    {
        //                        CCObj = new CostCentreAnswer();

        //                        CCObj.Id = reader.GetInt32(0);

        //                        CCObj.QuestionId = reader.GetInt32(1);

        //                        CCObj.AnswerString = reader.GetDouble(2);

        //                        AnsObj.Add(CCObj);
        //                    }
        //                    QuestionObj.AnswerCollection = AnsObj.ToList();
        //                }
        //            }
        //            reader.Close();
        //            return QuestionObj;
        //        }
               
        //        CostCentreMatrix oMatrix = db.CostCentreMatrices.Where(m => m.MatrixId == MatrixID).FirstOrDefault();

        //        if (oMatrix != null)
        //        {
        //            oMatrix.items = GetMatrixDetail(MatrixID);
        //        }

        //        return oMatrix;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("GetMatrix", ex);
        //    }
        //}


        ///// <summary>
        ///// Get matrix layout only
        ///// </summary>
        ///// <param name="MatrixID"></param>
        ///// <returns></returns>
        //public List<CostCentreMatrixDetail> GetMatrixDetail(int MatrixID)
        //{
        //    try
        //    {
        //        return db.CostCentreMatrixDetails.Where(d => d.MatrixId == MatrixID).OrderBy(i => i.Id).ToList();

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("GetMatrixDetail", ex);
        //    }
        //}
    }
}
