using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Repository.Repositories
{
    [Serializable()]
    public class CostCentreVariableRepository : BaseRepository<CostCentreVariable>, ICostCentreVariableRepository
    {

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public CostCentreVariableRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<CostCentreVariable> DbSet
        {
            get
            {
                return db.CostCentreVariables;
            }
        }

        #endregion

        #region public

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
                return db.CostCentreVariables.Where(v => v.VarId == VariableId).FirstOrDefault();


            }
            catch (Exception ex)
            {
                throw new Exception("LoadVariable", ex);
            }
        }

        /// <summary>
        /// MySql Dal function which executes a System/Estimate Variable
        /// </summary>
        /// <param name="Variable">Variable Object</param>
        /// <param name="oConnection">Connection Object</param>
        /// <param name="EstimateID">EstimateID which is going to be used as criteria</param>
        /// <returns>Result as double</returns>
        public double execSystemVariable(CostCentreVariable Variable, long EstimateID)
        {
            string sSqlString = null;
            double dResult = 0;
            try
            {
                sSqlString = db.Database.SqlQuery<string>("select top 1 cast(" + Variable.RefFieldName + " as varchar(100)) from " + Variable.RefTableName + " where " + Variable.CriteriaFieldName + "= " + EstimateID + "", "").FirstOrDefault();


                if ((sSqlString != null))
                {
                    dResult = Convert.ToDouble(sSqlString);
                }
                else
                {
                    throw new Exception("Unable to retreive System Variable Value");
                }

                return dResult;
            }
            catch (Exception ex)
            {
                throw new Exception(sSqlString, ex);
            }

        }

        /// <summary>
        /// MySql Dal function which executes a Simple Variable without any criteria using TOP query
        /// </summary>
        /// <param name="Variable">Variable Object</param>
        /// <param name="oConnection">Connection Object</param>
        /// <returns>Result as double</returns>
        public double execSimpleVariable(CostCentreVariable Variable)
        {
            string sSqlString = null;
            double dResult = 0;
            try
            {
                sSqlString = db.Database.SqlQuery<string>("select top 1 cast(" + Variable.RefFieldName + " as varchar(100)) from " + Variable.RefTableName + "", "").FirstOrDefault();

                if ((sSqlString != null))
                {
                    dResult = Convert.ToDouble(sSqlString);
                }
                else
                {
                    throw new Exception("Unable to retreive System Variable Value");
                }

                return dResult;
            }
            catch (Exception ex)
            {
                throw ex;
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

                if (Convert.ToBoolean(oVariable.IsCriteriaUsed) == true)
                {
                    sSqlString = db.Database.SqlQuery<string>("select top 1 cast(" + oVariable.RefFieldName + " as varchar(100)) from " + oVariable.RefTableName + " where " + oVariable.CriteriaFieldName + "= " + oVariable.Criteria + "", "").FirstOrDefault();


                }
                else
                {
                    sSqlString = db.Database.SqlQuery<string>("select top 1 cast(" + oVariable.RefFieldName + " as varchar(100)) from " + oVariable.RefTableName + "", "").FirstOrDefault();
                }


                //we have received a propper result, continue else raise exception
                if ((sSqlString != null))
                {
                    dResult = Convert.ToDouble(sSqlString);
                }
                else
                {
                    throw new Exception("Unable to retreive System Variable Value");
                }

                return dResult;
            }
            catch (Exception ex)
            {
                throw new Exception("ExecUserVariable", ex);
            }
        }

        /// <summary>
        /// Function checks wheather the Variable Exists or not
        /// </summary>
        /// <param name="VariableID"></param>
        /// <param name="VariableName"></param>
        /// <param name="oconnection"></param>
        /// <returns>Boolean</returns>
        public bool VariableExists(int VariableID, string VariableName)
        {
            try
            {
                int counts = db.CostCentreVariables.Where(v => v.VarId == VariableID && v.Name == VariableName).Count();
                if ((counts > 0))
                {
                    return true;

                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// function returns the variable List
        /// </summary>
        /// <param name="oConnection"></param>
        /// <returns>Datatable</returns>
        public List<CostCentreVariable> returnLoadVariableList()
        {
           
            try
            {
                var query = (from variable in db.CostCentreVariables
                             join Org in db.Organisations on variable.SystemSiteId equals Org.OrganisationId
                             
                             select variable).ToList();

                return query.ToList<CostCentreVariable>();
               
              
               
            }
            catch (Exception ex)
            {
                throw new Exception("returnLoadVariableList", ex);
            }
        }

        /// <summary>
        /// returns variable Category list
        /// </summary>
        /// <param name="oconnection">Data Connection</param>
        /// <returns>Datatable</returns>
        public List<CostCentreVariableType> returnVariableCateogories()
        {
           
            try
            {
                return db.CostCentreVariableTypes.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetVariableNameById(int iVariableId)
        {
            var objVariable = DbSet.Where(c => c.VarId == iVariableId).FirstOrDefault();
            if (objVariable != null)
                return objVariable.Name;
            else
                return string.Empty;
        }

    
        #endregion
    }
}
