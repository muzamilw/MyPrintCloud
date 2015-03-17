using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;
using System;
using MPC.Models.Common;
using AutoMapper;

namespace MPC.Repository.Repositories
{
    [Serializable()]
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
        /// Get Matrix and its layout
        /// </summary>
        /// <param name="MatrixID"></param>
        /// <returns></returns>
        public CostCentreMatrix GetMatrix(int MatrixID)
        {
            try
            {


                CostCentreMatrix oMatrix = db.CostCentreMatrices.Where(m => m.MatrixId == MatrixID).FirstOrDefault();

                if (oMatrix != null)
                {
                   oMatrix.items = GetMatrixDetail(MatrixID);
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
        public List<CostCentreMatrix> GetMatrixByOrganisationID(long OrganisationID,out List<CostCentreMatrixDetail> matrixDetail)
        {
            try
            {
               


                List<CostCentreMatrix> matrices = db.CostCentreMatrices.Where(o => o.OrganisationId == OrganisationID).ToList();
                List<CostCentreMatrixDetail> lstMatrixDetail = new List<CostCentreMatrixDetail>();
                if(matrices != null && matrices.Count > 0)
                {
                    foreach(var mat in matrices)
                    {
                        List<CostCentreMatrixDetail> matrixDetails = db.CostCentreMatrixDetails.Where(c => c.MatrixId == mat.MatrixId).ToList();
                        if (matrixDetails != null && matrixDetails.Count > 0)
                        {
                            foreach (var MDetail in matrixDetails)
                            {
                                lstMatrixDetail.Add(MDetail);

                            }
                        }
                    }
                }
                matrixDetail = lstMatrixDetail;
                return matrices;
            }
            catch (Exception ex)
            {
                throw new Exception("GetMatrixDetail", ex);
            }
        }
        #endregion
    }
}
