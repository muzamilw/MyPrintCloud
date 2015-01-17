﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;
using System;

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


        #endregion
    }
}