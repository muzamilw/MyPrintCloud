using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;
using System;
using MPC.Models.Common;

namespace MPC.Repository.Repositories
{
    public class CostCentreMatrixRepository : BaseRepository<CostCentreMatrix>, ICostCentreMatrixRepository
    {
        
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public CostCentreMatrixRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<CostCentreMatrix> DbSet
        {
            get
            {
                return db.CostCentreMatrices;
            }
        }

        /// <summary>
        /// Create Matrix and its layout
        /// </summary>
        /// <param name="oMatrix"></param>
        /// <returns></returns>
        public int CreateMatrix(CostCentreMatrix oMatrix)
		{
			try {

                db.CostCentreMatrices.Add(oMatrix);
                if (db.SaveChanges() > 0)
                {
                    return oMatrix.MatrixId;
                }
                else 
                {
                    return 0;
                }
				
			} catch (Exception ex) {
				throw new Exception("CreateMatrix", ex);
			}
		}

        ///// <summary>
        ///// Get Complete Matrix List
        ///// </summary>
        ///// <returns></returns>
        //public System.Data.DataTable getCostMatixiesList()
        //{
        //    try
        //    {

        //        "Select tbl_costcentrematrices.* from tbl_costcentrematrices " _
        //        + " INNER JOIN tbl_company_sites ON (tbl_company_sites.CompanySiteID=tbl_costcentrematrices.SystemSiteID) " _
        //        + " where "
        //        string SQL_STRING = SQL_GET_MATRIX_LIST + Common.LoadDepartmentString(g_GlobalData);
        //        return SQLHelper.ExecuteTable(g_GlobalData.AppSettings.ConnectionString, CommandType.Text, SQL_STRING);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("getCostMatixiesList", ex);
        //    }
        //}



        /// <summary>
        /// Executes a Matrix
        /// </summary>
        /// <param name="MatrixID"></param>
        /// <param name="oConnection"></param>
        /// <param name="ExecutionMode"></param>
        /// <param name="QuestionQueue"></param>
        /// <returns>Double</returns>
//        public double ExecuteMatrix(ref object[] oParamsArray, int MatrixID, long CostCentreID)
//{
//    bool bFlag = false;
//    QueueItemDTO QuestionITEM = default(QueueItemDTO);

//    CostCentreExecutionMode ExecutionMode = (CostCentreExecutionMode)oParamsArray[1];
//    ItemSection oItemSection = (ItemSection)oParamsArray[8];
//    int CurrentQuantity = Convert.ToInt32(oParamsArray[5]);
//    int MultipleQutantities = Convert.ToInt32(oParamsArray[4]);
//    ExecutionQueueDTO QuestionQueue = (ExecutionQueueDTO)oParamsArray[2];


//    try {
//        //check if its the visual mode or Execution Mode
//        if (ExecutionMode == CostCentreExecutionMode.ExecuteMode) {
//            //here the questions returned asnwer will ahave been loaded in the queue
//            //retreive the queue answer for this question and use.. :D
//            //use is simple only cast it in double and return..


//            foreach (var item in QuestionQueue.Item) {
//                //matching
//                if (item.ID == MatrixID.ToString & item.ItemType == 4) {
//                    bFlag = true;
//                    QuestionITEM = item;
//                    break; // TODO: might not be correct. Was : Exit For
//                }
//            }

//            //if found question in queue then use its values
//            if (bFlag == true) {
//                //Return CDbl(item.Answer)
//                //multiple qty logic goes here

//                if (CurrentQuantity <= MultipleQutantities) {
//                    switch (CurrentQuantity) {
//                        case 1:
//                            return Convert.ToDouble(QuestionITEM.Qty1Answer);
//                        case 2:
//                            if (Convert.ToDouble(QuestionITEM.Qty2Answer) == 0)
//                            {
//                                return Convert.ToDouble(QuestionITEM.Qty1Answer);
//                            } else {
//                                return Convert.ToDouble(QuestionITEM.Qty2Answer);
//                            }
//                            break;
//                        case 3:
//                            if (Convert.ToDouble(QuestionITEM.Qty3Answer) == 0)
//                            {
//                                return Convert.ToDouble(QuestionITEM.Qty1Answer);
//                            } else {
//                                return Convert.ToDouble(QuestionITEM.Qty3Answer);
//                            }
//                            break;
//                    }
//                } else {
//                    throw new Exception("Invalid  Current Selected Multiple Quantitity");
//                }
//            } else {
//                throw new Exception("Answer not found in Queue");
//            }


//        } else if (ExecutionMode == CostCentreExecutionMode.PromptMode) {
//            //populate the question in the executionQueue
//            //loading the Questions Information for populating in the Queue
//            CostMatixDTO oMatrix = GetMatrix(MatrixID);
//            QuestionQueue.addItem(MatrixID.ToString, oMatrix.Name, CostCentreID, 4, oMatrix.Description, "", "", false, null);
//            oMatrix = null;
//            return 1;
//            //exit normally 
//        }

//    } catch (Exception ex) {
//        throw new Exception("ExecuteMatrix", ex);
//    }

//}


        /// <summary>
        /// Get Matrix and its layout
        /// </summary>
        /// <param name="MatrixID"></param>
        /// <returns></returns>
        public CostMatixDTO GetMatrix(int MatrixID)
        {
            try
            {
                CostMatixDTO oMatrix = new CostMatixDTO();

                CostCentreMatrix result = db.CostCentreMatrices.Where(m => m.MatrixId == MatrixID).FirstOrDefault();
               
                if (result != null)
                {
                    oMatrix.ID = MatrixID;
                    oMatrix.Name = result.Name;
                    oMatrix.Description = result.Description;
                    oMatrix.Rows = result.RowsCount;
                    oMatrix.Columns = result.ColumnsCount;

                    oMatrix.Items = GetMatrixDetail(MatrixID);
                }

                return oMatrix;
            }
            catch (Exception ex)
            {
                throw new Exception("GetMatrix", ex);
            }
        }


        /// <summary>
        /// Get matrix layout only
        /// </summary>
        /// <param name="MatrixID"></param>
        /// <returns></returns>
        public List<CostCentreMatrixDetail> GetMatrixDetail(int MatrixID)
        {
            try
            {
                return db.CostCentreMatrixDetails.Where(d => d.MatrixId == MatrixID).OrderBy(i => i.Id).ToList();
                 
            }
            catch (Exception ex)
            {
                throw new Exception("GetMatrixDetail", ex);
            }
        }
        #endregion
    }
}
