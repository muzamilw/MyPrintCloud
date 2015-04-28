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
            try
            {

                db.CostCentreMatrices.Add(oMatrix);
                if (db.SaveChanges() > 0)
                {
                    return oMatrix.MatrixId;
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
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
        public IEnumerable<CostCentreMatrixDetail> GetByMatrixId(int MatrixId)
        {
            try
            {
                return db.CostCentreMatrixDetails.Where(c => c.MatrixId == MatrixId).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("GetMatrixDetail", ex);
            }
        }
        public CostCentreMatrix Add(CostCentreMatrix Matrix, IEnumerable<CostCentreMatrixDetail> MatrixDetail)
        {
            CostCentreMatrix oMatrix = new CostCentreMatrix();
            oMatrix.Description = Matrix.Description;
            oMatrix.Name = Matrix.Name;
            oMatrix.RowsCount = Matrix.RowsCount;
            oMatrix.ColumnsCount = Matrix.ColumnsCount;
            oMatrix.OrganisationId = Convert.ToInt32(OrganisationId);
            db.CostCentreMatrices.Add(oMatrix);
            if (db.SaveChanges() > 0)
            {

                foreach (var element in MatrixDetail)
                {
                    CostCentreMatrixDetail item = new CostCentreMatrixDetail();
                    item.Value = element.Value;
                    item.MatrixId = oMatrix.MatrixId;
                    db.CostCentreMatrixDetails.Add(item);

                }
                if (db.SaveChanges() > 0)
                {
                    return oMatrix;
                }
         }

            return null;


        }
        public bool DeleteMatrixById(int MatrixId)
        {
            IEnumerable<CostCentreMatrixDetail> MatrixDetailsList = db.CostCentreMatrixDetails.Where(g => g.MatrixId == MatrixId).ToList();
            db.CostCentreMatrixDetails.RemoveRange(MatrixDetailsList);
            db.SaveChanges();
            db.CostCentreMatrices.Remove(db.CostCentreMatrices.Where(g => g.MatrixId == MatrixId).SingleOrDefault());
            if (db.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        
        }
        public CostCentreMatrix Update(CostCentreMatrix Matrix, IEnumerable<CostCentreMatrixDetail> MatrixDetail)
        {
            CostCentreMatrix oMatrix = db.CostCentreMatrices.Where(g => g.MatrixId == Matrix.MatrixId).SingleOrDefault();
            if (oMatrix != null)
            {
                oMatrix.Description = Matrix.Description;
                oMatrix.Name = Matrix.Name;
                oMatrix.RowsCount = Matrix.RowsCount;
                oMatrix.ColumnsCount = Matrix.ColumnsCount;
                //oMatrix.items = MatrixDetail.ToList();


                int i = 0;
                List<CostCentreMatrixDetail> MatrixDetail2 = db.CostCentreMatrixDetails.Where(g => g.MatrixId == Matrix.MatrixId).ToList();

                foreach (var item in MatrixDetail2)
                {
                    if (i < MatrixDetail.Count())
                    {
                        item.Value = MatrixDetail.ElementAt(i).Value;
                        i++;
                    }
                    else
                    {
                        db.CostCentreMatrixDetails.Remove(item);
                    }

                }
                while (i < MatrixDetail.Count())
                {
                    CostCentreMatrixDetail item = new CostCentreMatrixDetail();
                    item.Value = MatrixDetail.ElementAt(i).Value;
                    item.MatrixId = Matrix.MatrixId;
                    db.CostCentreMatrixDetails.Add(item);
                    i++;
                }







            }


            if (db.SaveChanges() > 0)
            {
                return oMatrix;
            }
            else
            {
                return null;
            }

        }

        public List<CostCentreMatrix> GetMatrixByOrganisationID(long OrganisationID, out List<CostCentreMatrixDetail> matrixDetail)
        {
            try
            {



                List<CostCentreMatrix> matrices = db.CostCentreMatrices.Where(o => o.OrganisationId == OrganisationID).ToList();
                List<CostCentreMatrixDetail> lstMatrixDetail = new List<CostCentreMatrixDetail>();
                if (matrices != null && matrices.Count > 0)
                {
                    foreach (var mat in matrices)
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
