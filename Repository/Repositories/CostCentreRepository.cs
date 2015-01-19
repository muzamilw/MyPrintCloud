﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;
using System;
using System.Data;
using MPC.Models.Common;

namespace MPC.Repository.Repositories
{
    /// <summary>
    /// CostCentre Repository
    /// </summary>
    public class CostCentreRepository : BaseRepository<CostCentre>, ICostCentreRepository
    {
        #region privte
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public CostCentreRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<CostCentre> DbSet
        {
            get
            {
                return db.CostCentres;
            }
        }

        #endregion

        #region public

        /// <summary>
        /// Get All Cost Centres that are not system defined
        /// </summary>
        public IEnumerable<CostCentre> GetAllNonSystemCostCentres()
        {
            return DbSet.Where(costcentre => costcentre.OrganisationId == OrganisationId && costcentre.Type != 1);
        }

        public bool Delete(long CostCentreID)
        {
            try
            {
                db.CostCentres.Remove(db.CostCentres.Where(g => g.CostCentreId == CostCentreID).SingleOrDefault());
                db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Delete", ex);
            }
        }


        public long GetMaxCostCentreID()
        {
            try
            {

                var maxId = db.CostCentres.Max(g => g.CostCentreId);

                maxId += 1;


                return maxId;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateSystemCostCentre(long CostCentreID, int ProfitMarginID, int NominalCodeId, double MinCost, int UserID, string Description, bool DirectCost, bool IsScheduleable)
        {
            try
            {

                var costCentre = db.CostCentres.Where(g => g.CostCentreId == CostCentreID).SingleOrDefault();

                if (costCentre != null)
                {
                    costCentre.DefaultVA = ProfitMarginID;
                    costCentre.nominalCode = NominalCodeId;
                    costCentre.MinimumCost = MinCost;
                    costCentre.LastModifiedBy = UserID.ToString();
                    costCentre.Description = Description;
                    costCentre.IsDirectCost = Convert.ToInt16(DirectCost);
                    costCentre.IsScheduleable = Convert.ToInt16(IsScheduleable);
                }

                if (db.SaveChanges() > 0)
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
                throw new Exception("UpdateSystemCostCentre", ex);
            }
        }

        /// <summary>
        /// returns the costcentre object with summary information
        /// </summary>
        /// <param name="CostCentreID"></param>
        /// <returns></returns>
        public CostCentre GetCostCentreByID(long CostCentreID)
        {

            try
            {

                try
                {
                    return db.CostCentres.Where(c => c.CostCentreId == CostCentreID).SingleOrDefault();

                }
                catch (Exception ex)
                {
                    throw new Exception("GetCostCentreSummaryByID", ex);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("GetCostCentreSummaryByID", ex);
            }

        }

        /// <summary>
        /// returns the costcentre resources as datatable
        /// </summary>
        /// <param name="CostcentreID"></param>
        /// <returns></returns>
        public CostcentreResource GetCostCentreResources(long CostcentreID)
        {
            try
            {

                return db.CostcentreResources.Where(rr => rr.CostCentreId == CostcentreID).SingleOrDefault();

            }
            catch (Exception ex)
            {
                throw new Exception("GetCostCentreResources", ex);
            }
        }

        public List<CostCentreResource> GetCostCentreResourcesWithNames(long CostcentreID)
        {
            try
            {
                var query = from sysUser in db.SystemUsers
                            join resource in db.CostcentreResources on sysUser.SystemUserId equals resource.ResourceId
                            where resource.CostCentreId == CostcentreID
                            select new CostCentreResource()
                            {
                                ResourceId = resource.ResourceId ?? 0,
                                UserName = sysUser.UserName
                            };


                return query.ToList<CostCentreResource>();

            }
            catch (Exception ex)
            {
                throw new Exception("GetCostCentreResources", ex);
            }
        }


        public List<CostCentreType> GetCostCentreTypes(TypeReturnMode ReturnMode)
        {
            try
            {

                if (ReturnMode == TypeReturnMode.All)
                {
                    return db.CostCentreTypes.ToList();// "Select * from tbl_costcentreTypes";
                }
                else if (ReturnMode == TypeReturnMode.System)
                {
                    return db.CostCentreTypes.Where(c => c.IsSystem == 1).ToList(); //"Select * from tbl_costcentreTypes where IsSystem=1";
                }
                else if (ReturnMode == TypeReturnMode.UserDefined)
                {
                    return db.CostCentreTypes.Where(c => c.IsExternal == 1 && c.TypeId != 1).ToList(); //"Select * from tbl_costcentreTypes where IsExternal=1 and typeid <> 1";
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw new Exception("GetCostCentreTypes", ex);
            }
        }

        public List<CostcentreSystemType> GetCostCentreSystemTypes()
        {
            try
            {
                return db.CostcentreSystemTypes.ToList();

            }
            catch (Exception ex)
            {
                throw new Exception("GetCostCentreTypes", ex);
                return null;
            }
        }


        public CostcentreInstruction GetCostCentreWorkInstruction(long CostcentreID)
        {
            try
            {
                return db.CostcentreInstructions.Where(i => i.CostCentreId == CostcentreID).FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw new Exception("GetCostCentreWorkInstruction", ex);
            }
        }

        public List<CostCentreType> ReturnCostCentreCategories()
        {
            try
            {

                return db.CostCentreTypes.Where(c => c.IsSystem == 0 && c.IsExternal == 1).ToList();
                //"Select * from tbl_CostCentreTypes where IsSystem=0 and IsExternal=1";

            }
            catch (Exception ex)
            {
                throw new Exception("ReturnCostCentreCategories", ex);
            }
        }

        public List<CostCentre> GetCostCentreList()
        {
            try
            {
                return db.CostCentres.ToList();
              
            }
            catch (Exception ex)
            {
                throw new Exception("GetCostCentreList", ex);
            }
        }

        public bool CheckCostCentreName(long CostCentreID, string CostCentreName, long OrganisationId)
        {

            try
            {
                long CostCentreId = db.CostCentres.Where(c => c.CostCentreId != CostCentreID && c.Name == CostCentreName && c.OrganisationId == OrganisationId).SingleOrDefault().CostCentreId;

                if (CostCentreId > 0)
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
                throw new Exception("CheckCostCentreName", ex);
            }
        }


      
        public long InsertWorkInstruction(CostcentreInstruction oInstruction)
        {
            try
            {
                CostcentreInstruction oInstructionObj = new CostcentreInstruction();

                oInstructionObj.Instruction = oInstruction.Instruction;
                oInstructionObj.CostCentreId = oInstruction.CostCentreId;

                db.CostcentreInstructions.Add(oInstructionObj);
                if (db.SaveChanges() > 0)
                {
                    return oInstructionObj.InstructionId;
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("", ex);
            }
        }

        public long UpdateWorkInstruction(CostcentreInstruction oInstruction)
        {
            try
            {
                CostcentreInstruction oInstructionObj = db.CostcentreInstructions.Where(w => w.InstructionId == oInstruction.InstructionId).FirstOrDefault();

                oInstructionObj.Instruction = oInstruction.Instruction;
                oInstructionObj.CostCentreId = oInstruction.CostCentreId;

                if (db.SaveChanges() > 0)
                {
                    return oInstructionObj.InstructionId;
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("", ex);
            }
        }

        public bool DeleteWorkInstruction(long InstructionID)
        {
            try
            {
                CostcentreInstruction oInstructionObj = db.CostcentreInstructions.Where(w => w.InstructionId == InstructionID).FirstOrDefault();

                db.CostcentreInstructions.Remove(oInstructionObj);

                if (db.SaveChanges() > 0)
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
                throw new Exception("", ex);
            }
        }

        public long InsertChoice(CostcentreWorkInstructionsChoice ochoice)
        {

            try
            {
                CostcentreWorkInstructionsChoice oChoiceObj = new CostcentreWorkInstructionsChoice();

                oChoiceObj.Choice = ochoice.Choice;
                oChoiceObj.InstructionId = ochoice.InstructionId;

                db.CostcentreWorkInstructionsChoices.Add(oChoiceObj);
                if (db.SaveChanges() > 0)
                {
                    return oChoiceObj.Id;
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("", ex);
            }

        }

        public long UpdateChoice(CostcentreWorkInstructionsChoice ochoice)
        {
            try
            {
                CostcentreWorkInstructionsChoice oChoiceObj = db.CostcentreWorkInstructionsChoices.Where(w => w.Id == ochoice.Id).FirstOrDefault();

                oChoiceObj.Choice = ochoice.Choice;
                oChoiceObj.InstructionId = ochoice.InstructionId;

                if (db.SaveChanges() > 0)
                {
                    return oChoiceObj.Id;
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("", ex);
            }
        }

        public bool DeleteChoice(long ChoiceID)
        {

            try
            {
                CostcentreWorkInstructionsChoice oChoiceObj = db.CostcentreWorkInstructionsChoices.Where(w => w.Id == ChoiceID).FirstOrDefault();

                db.CostcentreWorkInstructionsChoices.Remove(oChoiceObj);

                if (db.SaveChanges() > 0)
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
                throw new Exception("", ex);
            }
        }

        public long InsertCostCentre(CostCentre oCostCentre)
        {
            try
            {

                db.CostCentres.Add(oCostCentre);
                if (db.SaveChanges() > 0)
                {
                    return oCostCentre.CostCentreId;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("InsertUserDefinedCostCentre", ex);
            }
        }

        public bool UpdateCostCentre(CostCentre oCostCentre)
        {
            try
            {

                CostCentre result = db.CostCentres.Where(c => c.CostCentreId == oCostCentre.CostCentreId).FirstOrDefault();

                result = oCostCentre;

                if (db.SaveChanges() > 0)
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
                throw new Exception("InsertUserDefinedCostCentre", ex);
            }
        }

        public CostCentre GetCostCentreSummary(long CostCentreID)
        {
            try
            {
                //returning the object
                return db.CostCentres.Where(c => c.CostCentreId == CostCentreID).FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw new Exception("GetCostCentreSummaryByID", ex);
            }

        }

       
        /// <summary>
        /// Get Code for CostCentre For a Company
        /// </summary>
        /// <returns></returns>
        public List<CostCentre> GetCompleteCodeofAllCostCentres(long OrganisationId)
        {
            try
            {
              var query =  from ccType in db.CostCentreTypes
                           join cc in db.CostCentres on ccType.TypeId equals cc.Type  
                           join Org in db.Organisations on cc.OrganisationId equals Org.OrganisationId
                           where ((ccType.IsExternal ==  1 && ccType.IsSystem == 0) 
                           && Org.OrganisationId == OrganisationId)
                           select new CostCentre()
                           {
                               CostCentreId = cc.CostCentreId,
                               CompleteCode = cc.CompleteCode

                           };
              return query.ToList<CostCentre>();
            }
            catch (Exception ex)
            {
                throw  ex;
            }
        }


        public bool ChangeFlag(int FlagID, long CostCentreID)
        {
            try
            {

                CostCentre oCC = db.CostCentres.Where(c => c.CostCentreId == CostCentreID).FirstOrDefault();
                oCC.FlagId = FlagID;
                if (db.SaveChanges() > 0)
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
                throw new Exception("ChangeFlag", ex);
            }
        }


        public CostCentre GetSystemCostCentre(long SystemTypeID, long OrganisationID)
        {

            try
            {
                return db.CostCentres.Where(c => c.SystemTypeId == SystemTypeID && c.OrganisationId == OrganisationID).FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<CostCentreType> GetCostCentreCategories(long OrganisationId)
        {
            try
            {
                return db.CostCentreTypes.Where(t => t.CompanyId == OrganisationId && t.IsSystem == 0).ToList();

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
                if (db.CostCentres.Where(c => c.Type == CategoryID).Count() > 0)
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
                throw new Exception("IsCostCentreAvailable", ex);
            }
        }
        #endregion

        #region "CostCentre Template"
        public CostCentreTemplate LoadCostCentreTemplate(int TemplateID)
        {
            try
            {

                return db.CostCentreTemplates.Where(t => t.Id == TemplateID).FirstOrDefault();


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public double ExecUserVariable(CostCentreVariable oVariable)
        {
            string sSqlString = null;
            double dResult = 0;
            object temp = null;
            try
            {
                //formatting the query
                System.Data.Entity.Infrastructure.DbRawSqlQuery<string> result = null;
                if (Convert.ToBoolean( oVariable.IsCriteriaUsed) == true)
                {
                    result = db.Database.SqlQuery<string>("select top 1 cast(" + oVariable.RefFieldName + " as varchar(100)) from " + oVariable.RefTableName + " where " + oVariable.CriteriaFieldName + "= " + oVariable.Criteria + "", "");
                    sSqlString = result.FirstOrDefault();
                   
                   
                }
                else
                {
                    result = db.Database.SqlQuery<string>("select top 1 cast(" + oVariable.RefFieldName + " as varchar(100)) from " + oVariable.RefTableName + "", "");
                    sSqlString = result.FirstOrDefault();
                }

               
                //we have received a propper result, continue else raise exception
                if ((result != null))
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

        public double ExecuteUserResource(long ResourceID, ResourceReturnType oCostPerHour)
        {
            try
            {
                if (oCostPerHour == ResourceReturnType.CostPerHour)
                {
                    return Convert.ToDouble(db.SystemUsers.Where(s => s.SystemUserId == ResourceID).Select(c => c.CostPerHour).FirstOrDefault());
                }
                else
                    return 0;

            }
            catch (Exception ex)
            {
                throw new Exception("ExecuteUserResource", ex);
            }
        }

        public double ExecuteUserStockItem(long StockID, StockPriceType StockPriceType, out double PerQtyQty)
        {
            string sSqlString = null;
            object temp = null;

            try
            {

                //[sp_CostCentreExecution_get_StockPriceByCalculationType]



                //expect  out params PerQtyQty
                //if ((temp != null))
                //{
                //    if ((!object.ReferenceEquals(temp, DBNull.Value)))
                //    {
                //        //returen the value
                //        return Convert.ToDouble(temp);
                //    }
                //}

                //temp = oParams(3).Value;

                //if ((temp != null))
                //{
                //    if ((!object.ReferenceEquals(temp, DBNull.Value)))
                //    {
                //        //returen the value
                //        PerQtyQty = oParams(3).Value;
                //    }
                //}
                PerQtyQty = 0;

                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception("ExecuteUserStockItem", ex);
            }
        }
        #endregion

        #region AddressSelect
        public List<CostCentre> GetCorporateDeliveryCostCentersList(long CompanyID)
        {

                var query = from tblCostCenter in db.CostCentres
                            join CorpCostCenter in db.CompanyCostCentres on tblCostCenter.CostCentreId equals (long)CorpCostCenter.CostCentreId
                            where tblCostCenter.Type == (int)CostCenterTypes.Delivery && tblCostCenter.isPublished == true
                            && CorpCostCenter.CompanyId == CompanyID
                            orderby tblCostCenter.MinimumCost
                            select new CostCentre()
                            {

                                CostCentreId = tblCostCenter.CostCentreId,
                                CompletionTime = tblCostCenter.CompletionTime,
                                MinimumCost = tblCostCenter.MinimumCost,
                                Description = tblCostCenter.Description,
                                Name = tblCostCenter.Name,
                                SetupCost = tblCostCenter.DeliveryCharges ?? 0,
                                EstimateProductionTime = tblCostCenter.EstimateProductionTime
                            };


                return query.ToList();

        }
        public List<CostCentre> GetDeliveryCostCentersList()
        {

           
                var query = from tblCostCenter in db.CostCentres
                            where tblCostCenter.Type == (int)CostCenterTypes.Delivery && tblCostCenter.isPublished == true && tblCostCenter.IsDisabled == 0
                            orderby tblCostCenter.MinimumCost
                            select new CostCentre()
                            {
                                CostCentreId = tblCostCenter.CostCentreId,
                                CompletionTime = tblCostCenter.CompletionTime,
                                MinimumCost = tblCostCenter.MinimumCost,
                                Description = tblCostCenter.Description,
                                Name = tblCostCenter.Name,
                                SetupCost = tblCostCenter.DeliveryCharges ?? 0,
                                EstimateProductionTime = tblCostCenter.EstimateProductionTime
                            };


                return query.ToList();
            

        }
        #endregion
    }
}
