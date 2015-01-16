using System.Collections.Generic;
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

        //public bool Delete(long CostCentreID)
        //{
        //    try
        //    {
        //        db.CostCentres.Remove(db.CostCentres.Where(g => g.CostCentreId == CostCentreID));
        //        db.SaveChanges();

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Delete", ex);
        //    }
        //}


        //public long GetMaxCostCentreID()
        //{
        //    string sSqlString = null;
        //    object temp = null;
        //    int vID = 0;
        //    try
        //    {

        //        var maxId = db.CostCentres.Max(g => g.CostCentreId);

        //        maxId += 1;


        //        return maxId;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public bool UpdateSystemCostCentre(long CostCentreID, int ProfitMarginID, string NominalCode, double MinCost, int UserID, string Description, bool DirectCost, bool IsScheduleable)
        //{
        //    try
        //    {

        //        var costCentre = db.CostCentres.Where(g => g.CostCentreId == CostCentreID).SingleOrDefault();

        //        if (costCentre != null)
        //        {
        //            costCentre.DefaultVA = ProfitMarginID;
        //            costCentre.nominalCode = NominalCode;
        //            costCentre.MinimumCost = MinCost;
        //            costCentre.LastModifiedBy = UserID;
        //            costCentre.Description = Description;
        //            costCentre.IsDirectCost = Convert.ToInt16(DirectCost);
        //            costCentre.IsScheduleable = Convert.ToInt16(IsScheduleable);
        //        }

        //        db.SaveChanges();

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("UpdateSystemCostCentre", ex);
        //    }
        //}

        ///// <summary>
        ///// returns the costcentre object with summary information
        ///// </summary>
        ///// <param name="CostCentreID"></param>
        ///// <returns></returns>
        //public CostCentre GetCostCentreByID(long CostCentreID, bool Complete)
        //{

        //    try
        //    {

        //        try
        //        {
        //            return db.CostCentres.Where(c => c.CostCentreId == CostCentreID).SingleOrDefault();

        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("GetCostCentreSummaryByID", ex);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("GetCostCentreSummaryByID", ex);
        //    }

        //}

        ///// <summary>
        ///// returns the costcentre resources as datatable
        ///// </summary>
        ///// <param name="CostcentreID"></param>
        ///// <returns></returns>
        //public CostcentreResource GetCostCentreResources(long CostcentreID)
        //{
        //    try
        //    {

        //        return db.CostcentreResource.Where(rr => rr.CostCentreId == CostcentreID).SingleOrDefault();

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("GetCostCentreResources", ex);
        //    }
        //}

        //public List<CostCentreResource> GetCostCentreResourcesWithNames(long CostcentreID)
        //{
        //    try
        //    {
        //        var query = from sysUser in db.SystemUser
        //                    join resource in db.CostcentreResource on sysUser.SystemUserId equals resource.ResourceId
        //                    where resource.CostCentreId = CostcentreID
        //                    select new CostCentreResource()
        //                    {
        //                        ResourceId = resource.ResourceId,
        //                        UserName = sysUser.UserName
        //                    };


        //        return query.ToList<CostCentreResource>();

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("GetCostCentreResources", ex);
        //    }
        //}

        ///// <summary>
        ///// returns the CostCentres List according to the PArameters
        ///// If CategoryID = 0 then load all costcentres
        ///// </summary>
        ///// <param name="CategoryID"></param>
        ///// <param name="SearchString"></param>
        ///// <returns></returns>
        ////public System.Data.DataSet GetCostCentreListDataSet(int CategoryID = 0, string SearchString = "", bool FromList = false)
        ////{
        ////    try
        ////    {
        ////        DataSet oGroupsDS = new DataSet();
        ////        DataRow oRow = default(DataRow);
        ////        DataRow oRow2 = default(DataRow);

        ////        string strQry1 = null;
        ////        string strQry2 = null;

        ////        if (FromList == true)
        ////        {
        ////            strQry1 = "Select distinct TypeID,TypeName from tbl_costcentretypes where IsSystem=0 and IsExternal=1 and CompanyID=" + g_GlobalData.UserSettings.OrganizationID.ToString;
        ////            strQry2 = "SELECT tbl_costcentres.CostCentreID,tbl_costcentres.Name,tbl_costcentres.Type,tbl_costcentres.Description,tbl_costcentres.CodeFileName,tbl_costcentres.SetupSpoilage,tbl_costcentres.RunningSpoilage,tbl_costcentres.Sequence,CompanySiteName as Site,tbl_costcentres.FlagID,tbl_costcentres.IsDisabled FROM tbl_costcentretypes " + " INNER JOIN tbl_costcentres ON (tbl_costcentretypes.TypeID = tbl_costcentres.Type) " + " INNER JOIN tbl_company_sites ON (tbl_company_sites.CompanySiteID=tbl_costcentres.SystemSiteID) " + " where isFromMIS = 0 and ( tbl_costcentres.Name like '%" + SearchString + "%'  or tbl_costcentres.Description like '%" + SearchString + "%' )  and (tbl_costcentretypes.IsExternal =  1 and   tbl_costcentretypes.IsSystem =0 ) and " + Common.LoadDepartmentString(g_GlobalData) + " Order by tbl_costcentres.Name ASC";


        ////        }
        ////        else if (CategoryID == 0 & SearchString == string.Empty)
        ////        {
        ////            strQry1 = "Select distinct TypeID,'Category ('+TypeName + ')' as TypeName from tbl_costcentretypes where IsSystem=0 and IsExternal=1 and CompanyID=" + g_GlobalData.UserSettings.OrganizationID.ToString;
        ////            strQry2 = "SELECT tbl_costcentres.CostCentreId,tbl_costcentres.Name,tbl_costcentres.Type,tbl_costcentres.Description,tbl_costcentres.CodeFileName,tbl_costcentres.SetupSpoilage,tbl_costcentres.RunningSpoilage,tbl_costcentres.Sequence,CompanySiteName as Site,tbl_costcentres.FlagID FROM tbl_costcentretypes " + " INNER JOIN tbl_costcentres ON (tbl_costcentretypes.TypeID = tbl_costcentres.Type) " + " INNER JOIN tbl_company_sites ON (tbl_company_sites.CompanySiteID=tbl_costcentres.SystemSiteID) " + " where isFromMIS = 0 and (tbl_costcentretypes.IsExternal =  1 and   tbl_costcentretypes.IsSystem =0 ) and " + Common.LoadDepartmentString(g_GlobalData) + " Order by tbl_costcentres.Name ASC";


        ////        }
        ////        else if (CategoryID == 0 & SearchString != string.Empty)
        ////        {
        ////            strQry1 = "Select distinct tbl_costcentretypes.TypeID,'Category ('+tbl_costcentretypes.TypeName + ')' as TypeName from tbl_costcentretypes INNER JOIN tbl_costcentres ON (tbl_costcentres.Type = tbl_costcentretypes.TypeID)  where IsSystem=0 and IsExternal=1  and ( tbl_costcentres.Name like '%" + SearchString + "%'  or tbl_costcentres.Description like '%" + SearchString + "%' )";
        ////            strQry2 = "SELECT tbl_costcentres.CostCentreId,tbl_costcentres.Name,tbl_costcentres.Type,tbl_costcentres.Description,tbl_costcentres.CodeFileName,tbl_costcentres.SetupSpoilage,tbl_costcentres.RunningSpoilage,tbl_costcentres.Sequence,CompanySiteName as Site,tbl_costcentres.FlagID FROM tbl_costcentretypes " + " INNER JOIN tbl_costcentres ON (tbl_costcentretypes.TypeID = tbl_costcentres.Type) " + " INNER JOIN tbl_company_sites ON (tbl_company_sites.CompanySiteID=tbl_costcentres.SystemSiteID) " + " where isFromMIS = 0 and (tbl_costcentretypes.IsExternal =  1 and   tbl_costcentretypes.IsSystem =0  and ( tbl_costcentres.Name like '%" + SearchString + "%'  or tbl_costcentres.Description like '%" + SearchString + "%' )" + ") and " + Common.LoadDepartmentString(g_GlobalData) + " Order by tbl_costcentres.Name ASC";

        ////        }
        ////        else if (CategoryID != 0 & SearchString == string.Empty)
        ////        {
        ////            strQry1 = "Select distinct TypeID,'Category ('+TypeName + ')' as TypeName from tbl_costcentretypes INNER JOIN tbl_costcentres ON (tbl_costcentretypes.TypeID = tbl_costcentres.Type) where IsSystem=0 and IsExternal=1 and tbl_costcentres.Type=" + CategoryID.ToString;
        ////            strQry2 = "SELECT tbl_costcentres.CostCentreId,tbl_costcentres.Name,tbl_costcentres.Type,tbl_costcentres.Description,tbl_costcentres.CodeFileName,tbl_costcentres.SetupSpoilage,tbl_costcentres.RunningSpoilage,tbl_costcentres.Sequence,CompanySiteName as Site,tbl_costcentres.FlagID FROM tbl_costcentretypes " + " INNER JOIN tbl_costcentres ON (tbl_costcentretypes.TypeID = tbl_costcentres.Type) " + " INNER JOIN tbl_company_sites ON (tbl_company_sites.CompanySiteID=tbl_costcentres.SystemSiteID) " + " where isFromMIS = 0 and (tbl_costcentretypes.IsExternal =  1 and   tbl_costcentretypes.IsSystem =0  and tbl_costcentres.Type=" + CategoryID.ToString + ") and " + Common.LoadDepartmentString(g_GlobalData) + " Order by tbl_costcentres.Name ASC";

        ////        }
        ////        else if (CategoryID != 0 & SearchString != string.Empty)
        ////        {
        ////            strQry1 = "Select distinct tbl_costcentretypes.TypeID,'Category ('+tbl_costcentretypes.TypeName + ')' as TypeName from tbl_costcentretypes INNER JOIN tbl_costcentres ON (tbl_costcentres.Type = tbl_costcentretypes.TypeID)  where IsSystem=0 and IsExternal=1 and tbl_costcentres.type=" + CategoryID.ToString + "  and ( tbl_costcentres.Name like '%" + SearchString + "%'  or tbl_costcentres.Description like '%" + SearchString + "%' )";
        ////            strQry2 = "SELECT tbl_costcentres.CostCentreId,tbl_costcentres.Name,tbl_costcentres.Type,tbl_costcentres.Description,tbl_costcentres.CodeFileName,tbl_costcentres.SetupSpoilage,tbl_costcentres.RunningSpoilage,tbl_costcentres.Sequence,CompanySiteName as Site,tbl_costcentres.FlagID FROM tbl_costcentretypes " + " INNER JOIN tbl_costcentres ON (tbl_costcentretypes.TypeID = tbl_costcentres.Type) " + " INNER JOIN tbl_company_sites ON (tbl_company_sites.CompanySiteID=tbl_costcentres.SystemSiteID) " + " where isFromMIS = 0 and (tbl_costcentretypes.IsExternal =  1 and   tbl_costcentretypes.IsSystem =0 and tbl_costcentres.type=" + CategoryID.ToString + " and ( tbl_costcentres.Name like '%" + SearchString + "%'  or tbl_costcentres.Description like '%" + SearchString + "%' )" + ") and " + Common.LoadDepartmentString(g_GlobalData) + " Order by tbl_costcentres.Name ASC";
        ////        }

        ////        oGroupsDS.Tables.Add(SQLHelper.ExecuteTable(g_GlobalData.AppSettings.ConnectionString, CommandType.Text, strQry1));
        ////        oGroupsDS.Tables.Add(SQLHelper.ExecuteTable(g_GlobalData.AppSettings.ConnectionString, CommandType.Text, strQry2));

        ////        //oGroupsDS.Tables.Add(SQLHelper.ExecuteTable(g_GlobalData.AppSettings.ConnectionString, CommandType.Text, SQL_GetCostCentreTypes))
        ////        //parm(0) = New SqlParameter(PARAM_CompanyID, SqlDbType.Int)
        ////        //parm(0).Value = g_GlobalData.CompanyDetails.CompanyID

        ////        //oGroupsDS.Tables.Add(SQLHelper.ExecuteTable(g_GlobalData.AppSettings.ConnectionString, CommandType.Text, SQL_GetGroupDetails, parm))

        ////        DataColumn dc1 = default(DataColumn);
        ////        DataColumn dc2 = default(DataColumn);
        ////        dc1 = oGroupsDS.Tables(0).Columns("TypeID");
        ////        dc2 = oGroupsDS.Tables(1).Columns("Type");
        ////        DataRelation dRelation = new DataRelation("sequences", dc1, dc2, false);
        ////        oGroupsDS.Relations.Add(dRelation);

        ////        return oGroupsDS;
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        throw new Exception("GetCostCentreListDataSet", ex);
        ////    }
        ////}


        ////public bool ADDUpdateCostCentreResources(ref System.Data.DataTable oDataTable)
        ////{
        ////    try
        ////    {
        ////         oDataTable.AsEnumerable().FirstOrDefault().
        ////        string Sstring = "Select * from tbl_costcentre_resources where CostCentreID=0";
        ////        SQLHelper.ExcuteAdapter(g_GlobalData.AppSettings.ConnectionString, oDataTable.AsEnumerable()., Sstring, CommandType.Text);
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        throw new Exception("ADD Update Costcentre Resources", ex);
        ////    }
        ////}


        //public List<CostCentreType> GetCostCentreTypes(TypeReturnMode ReturnMode)
        //{
        //    try
        //    {

        //        if (ReturnMode == ReturnMode.All)
        //        {
        //            return db.CostCentreTypes.ToList();// "Select * from tbl_costcentreTypes";
        //        }
        //        else if (ReturnMode == ReturnMode.System)
        //        {
        //            return db.CostCentreTypes.Where(c => c.IsSystem == 1).ToList(); //"Select * from tbl_costcentreTypes where IsSystem=1";
        //        }
        //        else if (ReturnMode == ReturnMode.UserDefined)
        //        {
        //            return db.CostCentreTypes.Where(c => c.IsExternal == 1 && c.TypeId != 1).ToList(); //"Select * from tbl_costcentreTypes where IsExternal=1 and typeid <> 1";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("GetCostCentreTypes", ex);
        //    }
        //}

        //public List<CostcentreSystemType> GetCostCentreSystemTypes()
        //{
        //    try
        //    {
        //        return db.CostcentreSystemType.ToList();

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("GetCostCentreTypes", ex);
        //    }
        //}


        //public CostcentreInstruction GetCostCentreWorkInstruction(long CostcentreID)
        //{
        //    try
        //    {
        //        return db.CostcentreInstruction.Where(i => i.CostCentreId == CostcentreID).FirstOrDefault();

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("GetCostCentreWorkInstruction", ex);
        //    }
        //}

        //public List<CostCentreType> ReturnCostCentreCategories()
        //{
        //    string sSqlString = null;
        //    try
        //    {

        //        return db.CostCentreType.Where(c => c.IsSystem == 0 && c.IsExternal == 1).ToList();
        //        //"Select * from tbl_CostCentreTypes where IsSystem=0 and IsExternal=1";

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("ReturnCostCentreCategories", ex);
        //    }
        //}

        //public List<CostCentre> GetCostCentreList()
        //{
        //    string sSqlString = null;
        //    try
        //    {
        //        return db.CostCentre.ToList();
        //        // "SELECT tbl_costcentres.CostCentreID,tbl_costcentres.Name,tbl_costcentres.Description,tbl_costcentres.Type,strCostPlantUnParsed,strCostLabourUnParsed,strCostMaterialUnParsed,strPricePlantUnParsed,strPriceLabourUnParsed,strPriceMaterialUnParsed,strActualCostPlantUnParsed,strActualCostLabourUnParsed,strActualCostMaterialUnParsed,strTimeUnParsed,QuantityCalculationString,TimeCalculationString FROM tbl_costcentres ";


        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("GetCostCentreList", ex);
        //    }
        //}

        //public bool CheckCostCentreName(long CostCentreID, string CostCentreName, long OrganisationId)
        //{

        //    try
        //    {
        //        long CostCentreId = db.CostCentres.Where(c => c.CostCentreId != CostCentreID && c.Name == CostCentreName && c.OrganisationId == OrganisationId).SingleOrDefault();

        //        if (CostCentreId > 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("CheckCostCentreName", ex);
        //    }
        //}

        //public bool UpdateWorkinstructions(ref DataSet oDataset, long CostCentreID, bool IsCopy)
        //{
        //    SqlConnection oConnection = new SqlConnection(g_GlobalData.AppSettings.ConnectionString);
        //    try {
        //        oConnection.Open();
        //        DataRow oParentRow = default(DataRow);
        //        DataRow oChildRow = default(DataRow);

        //        if ((oDataset == null) == false) {

        //            if ((oDataset.Tables(0) == null) == false) {
        //                foreach ( oParentRow in oDataset.Tables(0).Rows) {
        //                    Model.CostCentres.InstructionDTO oIstrction = new Model.CostCentres.InstructionDTO();
        //                    if (oParentRow.RowState == DataRowState.Added | IsCopy == true) {
        //                        oIstrction.CostCentreID = CostCentreID;
        //                        oIstrction.Instruction = oParentRow("Instruction").ToString;
        //                        oIstrction.InstructionID = InsertWorkInstruction(oIstrction, ref oConnection);

        //                    } else if (oParentRow.RowState == DataRowState.Modified) {
        //                        oIstrction.CostCentreID = CostCentreID;
        //                        oIstrction.Instruction = oParentRow("Instruction").ToString;
        //                        oIstrction.InstructionID = oParentRow("InstructionID");
        //                        UpdateWorkInstruction(oIstrction, ref oConnection);
        //                    } else if (oParentRow.RowState == DataRowState.Deleted) {
        //                        if (Information.IsDBNull(oParentRow("InstructionID", DataRowVersion.Original)) == false) {
        //                            oIstrction.InstructionID = Convert.ToInt32(oParentRow("InstructionID", DataRowVersion.Original));
        //                            DeleteWorkInstruction(oIstrction.InstructionID, ref oConnection);
        //                        }

        //                    } else {
        //                        oIstrction.InstructionID = oParentRow("InstructionID");

        //                    }

        //                    if (oParentRow.RowState != DataRowState.Deleted) {
        //                        foreach ( oChildRow in oParentRow.GetChildRows(oDataset.Relations(0))) {
        //                            Model.CostCentres.InstructionOptionDTO oChoice = default(Model.CostCentres.InstructionOptionDTO);
        //                            if (oChildRow.RowState == DataRowState.Added | IsCopy == true) {
        //                                oChoice = new Model.costcentres.InstructionOptionDTO(0, oChildRow("Choice").ToString, oIstrction.InstructionID);
        //                                InsertChoice(oChoice, ref oConnection);
        //                            } else if (oChildRow.RowState == DataRowState.Modified) {
        //                                oChoice = new Model.costcentres.InstructionOptionDTO(oChildRow("ID"), oChildRow("Choice").ToString, oIstrction.InstructionID);
        //                                InsertChoice(oChoice, ref oConnection);
        //                            }
        //                        }
        //                    }
        //                }

        //            }
        //        }

        //    } catch (Exception ex) {
        //        throw new Exception("UpdateWorkinstructions", ex);
        //    } finally {
        //        if (oConnection.State != ConnectionState.Closed) {
        //            oConnection.Close();
        //        }
        //    }
        //}


        //public long InsertWorkInstruction(CostcentreInstruction oInstruction)
        //{
        //    try
        //    {
        //        CostcentreInstruction oInstructionObj = new CostcentreInstruction();

        //        oInstructionObj.Instruction = oInstruction.Instruction;
        //        oInstructionObj.CostCentreId = oInstruction.CostCentreId;

        //        db.CostcentreInstruction.Add(oInstructionObj);
        //        if (db.SaveChanges() > 0)
        //        {
        //            return oInstructionObj.InstructionId;
        //        }
        //        else
        //        {
        //            return 0;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("", ex);
        //    }
        //}

        //public long UpdateWorkInstruction(CostcentreInstruction oInstruction)
        //{
        //    try
        //    {
        //        CostcentreInstruction oInstructionObj = db.CostcentreInstruction.Where(w => w.InstructionId == oInstruction.InstructionId).FirstOrDefault();

        //        oInstructionObj.Instruction = oInstruction.Instruction;
        //        oInstructionObj.CostCentreId = oInstruction.CostCentreId;

        //        if (db.SaveChanges() > 0)
        //        {
        //            return oInstructionObj.InstructionId;
        //        }
        //        else
        //        {
        //            return 0;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("", ex);
        //    }
        //}

        //public bool DeleteWorkInstruction(long InstructionID)
        //{
        //    try
        //    {
        //        CostcentreInstruction oInstructionObj = db.CostcentreInstruction.Where(w => w.InstructionId == oInstruction.InstructionId).FirstOrDefault();

        //        db.CostcentreInstruction.Remove(oInstructionObj);

        //        if (db.SaveChanges() > 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("", ex);
        //    }
        //}

        //public long InsertChoice(CostcentreWorkInstructionsChoice ochoice)
        //{

        //    try
        //    {
        //        CostcentreWorkInstructionsChoice oChoiceObj = new CostcentreWorkInstructionsChoice();

        //        oChoiceObj.Choice = ochoice.Choice;
        //        oChoiceObj.InstructionId = ochoice.InstructionId;

        //        db.CostcentreWorkInstructionsChoice.Add(oChoiceObj);
        //        if (db.SaveChanges() > 0)
        //        {
        //            return oChoiceObj.Id;
        //        }
        //        else
        //        {
        //            return 0;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("", ex);
        //    }

        //}

        //public long UpdateChoice(CostcentreWorkInstructionsChoice ochoice)
        //{
        //    try
        //    {
        //        CostcentreWorkInstructionsChoice oChoiceObj = db.CostcentreWorkInstructionsChoice.Where(w => w.Id == ochoice.Id).FirstOrDefault();

        //        oChoiceObj.Choice = ochoice.Choice;
        //        oChoiceObj.InstructionId = ochoice.InstructionId;

        //        if (db.SaveChanges() > 0)
        //        {
        //            return oChoiceObj.Id;
        //        }
        //        else
        //        {
        //            return 0;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("", ex);
        //    }
        //}

        //public bool DeleteChoice(long ChoiceID)
        //{

        //    try
        //    {
        //        CostcentreWorkInstructionsChoice oChoiceObj = db.CostcentreWorkInstructionsChoice.Where(w => w.Id == ChoiceID).FirstOrDefault();

        //        db.CostcentreWorkInstructionsChoice.Remove(oChoiceObj);

        //        if (db.SaveChanges() > 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("", ex);
        //    }
        //}

        //public long InsertCostCentre(CostCentre oCostCentre)
        //{
        //    try
        //    {

        //        db.CostCentres.Add(oCostCentre);
        //        if (db.SaveChanges() > 0)
        //        {
        //            return oCostCentre.CostCentreId;
        //        }
        //        else
        //        {
        //            return 0;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("InsertUserDefinedCostCentre", ex);
        //    }
        //}

        //public bool UpdateUserDefinedCostCentre(CostCentre oCostCentre)
        //{
        //    try
        //    {

        //        CostCentre result = db.CostCentres.Where(c => c.CostCentreId == oCostCentre.CostCentreId).FirstOrDefault();

        //        result = oCostCentre;

        //        if (db.SaveChanges() > 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("InsertUserDefinedCostCentre", ex);
        //    }
        //}

        //public CostCentre GetCostCentreSummaryByID(long CostCentreID)
        //{
        //    try
        //    {
        //        //returning the object
        //        return db.CostCentres.Where(c => c.CostCentreId == CostCentreID).FirstOrDefault();

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("GetCostCentreSummaryByID", ex);
        //    }

        //}

        //public CostCentre LoadCostCentreHeader(long CostCentreID)
        //{
        //    try
        //    {
        //        var query = from cc in db.CostCentres
        //                    where cc.CostCentreId == CostCentreID
        //                    select new CostCentreHeader()
        //                    {
        //                        ID = cc.CostCentreId,
        //                        Name = cc.Name,
        //                        Description = cc.Description,
        //                        Type = cc.Type,
        //                        CodeFileName = cc.CodeFileName
        //                    };
        //        return query.FirstOrDefault<CostCentre>();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("LoadCostCentreHeader", ex);
        //    }
        //}

        ///// <summary>
        ///// Get Code for CostCentre For a Company
        ///// </summary>
        ///// <returns></returns>
        //public List<CostCentreCompletionCode> GetCompleteCodeofAllCostCentres(long OrganisationId)
        //{tbl_costcentres.CostCentreID,tbl_costcentres.CompleteCode
        //    try
        //    {
        //      var query =  from ccType in db.CostCentreTypes
        //                   join cc in db.CostCentres on ccType.TypeId equals cc.Type  
        //                   join Org in db.Organisations on cc.OrganisationId equals Org.OrganisationId
        //                   where ((ccType.IsExternal =  1 && ccType.IsSystem =0) 
        //                   && Org.OrganisationId = OrganisationId)
        //                   select new CostCentreCompletionCode()
        //                   {
        //                       ID = cc.CostCentreId,
        //                       CompletionCode = cc.CompleteCode

        //                   };
        //      return query.ToList<CostCentreCompletionCode>();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Get Complete Code", ex);
        //    }
        //}


        //public bool ChangeFlag(int FlagID, long CostCentreID)
        //{
        //    try
        //    {

        //        CostCentre oCC = db.CostCentres.Where(c => c.CostCentreId == CostCentreID).FirstOrDefault();
        //        oCC.FlagId == FlagID;
        //        if (db.SaveChanges() > 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("ChangeFlag", ex);
        //    }
        //}

        ///// <summary>
        ///// compares the costcentre dates of registry and database, and if there is differnet then gets the new DLL from DB and write it
        ///// </summary>
        ///// <returns></returns>
        //public int CheckCostCentresVersion()
        //{
        //    Microsoft.win32.RegistryKey Software = default(Microsoft.win32.RegistryKey);
        //    Microsoft.win32.RegistryKey Clydo = default(Microsoft.win32.RegistryKey);
        //    Microsoft.win32.RegistryKey InfinityDesktop = default(Microsoft.win32.RegistryKey);
        //    Microsoft.win32.RegistryKey Client = default(Microsoft.win32.RegistryKey);
        //    try
        //    {
        //        //get the system date of 
        //        //select CostCentreUpdationDate from tbl_companies where CompanyID =@CompanyID

        //        object oTemp = null;
        //        DateTime DbDateTime = default(DateTime);
        //        DateTime regDateTime = default(DateTime);

        //        SqlParameter[] param = new SqlParameter[1];
        //        param(0) = new SqlParameter(PARAM_CompanyID, SqlDbType.Int);
        //        param(0).Value = g_GlobalData.CompanyDetails.CompanyID;

        //        oTemp = SQLHelper.ExecuteScalar(g_GlobalData.AppSettings.ConnectionString, CommandType.StoredProcedure, MYSQL_GetCostCentreUpdationDate, param);

        //        if ((oTemp != null))
        //        {
        //            if ((!object.ReferenceEquals(oTemp, DBNull.Value)))
        //            {
        //                DbDateTime = (DateTime)oTemp;
        //            }
        //            else
        //            {
        //                DbDateTime = DateTime.Now;
        //            }
        //        }
        //        else
        //        {
        //            DbDateTime = DateTime.Now;
        //        }

        //        //getting registry values
        //        Software = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE", true);
        //        Clydo = Software.OpenSubKey("Clydo", true);

        //        if (Clydo == null)
        //        {
        //            Clydo = Software.CreateSubKey("Clydo");
        //        }

        //        InfinityDesktop = Clydo.OpenSubKey("Infinity Desktop", true);
        //        if (InfinityDesktop == null)
        //        {
        //            InfinityDesktop = Clydo.CreateSubKey("Infinity Desktop");
        //        }

        //        Client = InfinityDesktop.OpenSubKey("Client", true);
        //        if (Client == null)
        //        {
        //            Client = InfinityDesktop.CreateSubKey("Client");
        //        }

        //        if (Convert.ToString(Client.GetValue("CostCentre Updation Date", Now)) == string.Empty)
        //        {
        //            regDateTime = DateTime.Now;
        //        }
        //        else
        //        {
        //            try
        //            {
        //                regDateTime = (DateTime)Client.GetValue("CostCentre Updation Date", DateTime.Now);
        //            }
        //            catch (Exception ex)
        //            {
        //                //we encountered some exception regarding datetime conversion.
        //                Client.SetValue("CostCentre Updation Date", DateTime.Now);
        //                regDateTime = DateTime.Now;
        //            }
        //        }

        //        string oCompanyName = g_GlobalData.CompanyDetails.CompanyID.ToString;

        //        if (DateDiff(DateInterval.Second, regDateTime, DbDateTime) != 0 | File.Exists(g_GlobalData.AppSettings.ApplicationStartupPath + "\\ccAssembly\\" + oCompanyName + "UserCostCentres.dll") == false)
        //        {
        //            //we need to get the latest version of DLL
        //            //another query which will fetch the information form the same table

        //            SqlParameter[] @params = new SqlParameter[1];
        //            @params(0) = new SqlParameter(PARAM_CompanyID, SqlDbType.Int);
        //            @params(0).Value = g_GlobalData.CompanyDetails.CompanyID;

        //            oTemp = SQLHelper.ExecuteScalar(g_GlobalData.AppSettings.ConnectionString, CommandType.StoredProcedure, MYSQL_GetCostCentreDLL, @params);
        //            //the above object contains the DLL file, which we have to write :S

        //            //co.OutputAssembly = sOutputPath + CompanyName + "UserCostCentres.dll"

        //            FileStream fs = new FileStream(g_GlobalData.AppSettings.ApplicationStartupPath + "\\ccAssembly\\" + oCompanyName + "UserCostCentres.dll", FileMode.Create);
        //            BinaryWriter bw = new BinaryWriter(fs);

        //            byte[] bytearr = null;
        //            StreamReader oreader = default(StreamReader);

        //            bw.Write(Convert.ToByte(oTemp));

        //            bw.Close();
        //            fs.Close();

        //            Client.SetValue("CostCentre Updation Date", DbDateTime);

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("", ex);
        //    }
        //    finally
        //    {
        //        Software.Close();
        //        Client.Close();
        //        InfinityDesktop.Close();
        //        Clydo.Close();
        //    }

        //}


        //public CostCentre GetSystemCostCentre(long SystemTypeID, long OrganisationID)
        //{

        //    try
        //    {
        //        return db.CostCentres.Where(c => c.SystemTypeId == SystemTypeID && c.OrganisationId == OrganisationID).FirstOrDefault();

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("GetSystemCostCentre", ex);
        //    }
        //}


        //public List<CostCentreType> GetCostCentreCategories(long OrganisationId)
        //{
        //    try
        //    {
        //        return db.CostCentreType.Where(t => t.CompanyId == OrganisationId && t.IsSystem == 0).ToList();
               
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("GetCostCentreCategories", ex);
        //    }
        //}

        ////public bool UpdateCostCentreCategories(System.Data.DataTable oTable)
        ////{
        ////    SqlConnection oConnection = new SqlConnection(g_GlobalData.AppSettings.ConnectionString);
        ////    try {
        ////        DataRow oParentRow = default(DataRow);

        ////        foreach ( oParentRow in oTable.Rows) {
        ////            if (oParentRow.RowState == DataRowState.Added) {
        ////                SqlParameter[] parm = new SqlParameter[2];
        ////                parm(0) = new SqlParameter("@TypeName", SqlDbType.VarChar);
        ////                parm(0).Value = oParentRow("TypeName");

        ////                parm(1) = new SqlParameter("@CompanyID", SqlDbType.Int);
        ////                parm(1).Value = g_GlobalData.UserSettings.OrganizationID;

        ////                SQLHelper.ExecuteNonQuery(oConnection, CommandType.StoredProcedure, "sp_costcentre_add_categories", parm);

        ////            } else if (oParentRow.RowState == DataRowState.Modified) {
        ////                SqlParameter[] parm = new SqlParameter[2];
        ////                parm(0) = new SqlParameter("@TypeName", SqlDbType.VarChar);
        ////                parm(0).Value = oParentRow("TypeName");

        ////                parm(1) = new SqlParameter("@TypeID", SqlDbType.Int);
        ////                parm(1).Value = oParentRow("TypeID");

        ////                SQLHelper.ExecuteNonQuery(oConnection, CommandType.StoredProcedure, "sp_costcentre_update_categories", parm);

        ////            } else if (oParentRow.RowState == DataRowState.Deleted) {
        ////                if (Information.IsDBNull(oParentRow("TypeID", DataRowVersion.Original)) == false) {
        ////                    SqlParameter[] parm = new SqlParameter[1];
        ////                    parm(0) = new SqlParameter("@TypeID", SqlDbType.Int);
        ////                    parm(0).Value = Convert.ToInt32(oParentRow("TypeID", DataRowVersion.Original));

        ////                    SQLHelper.ExecuteNonQuery(oConnection, CommandType.StoredProcedure, "sp_costcentre_delete_categories", parm);
        ////                }
        ////            }
        ////        }
        ////        return true;
        ////    } catch (Exception ex) {
        ////        throw new Exception("UpdateCostCentreCategories", ex);
        ////    } finally {
        ////        if (oConnection.State != ConnectionState.Closed) {
        ////            oConnection.Close();
        ////        }
        ////    }
        ////}

        //public bool IsCostCentreAvailable(int CategoryID)
        //{
        //    try
        //    {
        //        if (db.CostCentres.Where(c => c.Type == CategoryID).Count() > 0)
        //        {
        //            return true;
        //        }
        //        else 
        //        {
        //            return false;
        //        }
               
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("IsCostCentreAvailable", ex);
        //    }
        //}
        //#endregion

        //#region "CostCentre Template"
        //public CostCentreTemplate LoadCostCentreTemplate(string TemplateID)
        //{
        //    try
        //    {

        //        return db.CostCentreTemplate.Where(t => t.Id == (int)TemplateID).FirstOrDefault();


        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public double ExecUserVariable(CostCentreVariable oVariable)
        //{
        //    string sSqlString = null;
        //    double dResult = 0;
        //    object temp = null;
        //    try
        //    {
        //        //formatting the query
        //        System.Data.Entity.Infrastructure.DbRawSqlQuery<string> result = null;
        //        if (oVariable.IsCriteriaUsed == true)
        //        {
        //            result = db.Database.SqlQuery<string>("select top 1 cast(" + oVariable.RefFieldName + " as varchar(100)) from " + oVariable.RefTableName + " where " + oVariable.CriteriaFieldName + "= " + oVariable.Criteria; + "", "");
        //            sSqlString = result.FirstOrDefault();
        //            return oResult;
                   
        //        }
        //        else
        //        {
        //            result = db.Database.SqlQuery<string>("select top 1 cast(" + oVariable.RefFieldName + " as varchar(100)) from " + oVariable.RefTableName + "", "");
        //            sSqlString = result.FirstOrDefault();
        //        }

               
        //        //we have received a propper result, continue else raise exception
        //        if ((result != null))
        //        {
        //            dResult = Convert.ToDouble(sSqlString);
        //        }
        //        else
        //        {
        //            throw new Exception("Unable to retreive System Variable Value");
        //        }

        //        return dResult;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("ExecUserVariable", ex);
        //    }
        //}

        //public double ExecuteUserResource(long ResourceID, ResourceReturnType oCostPerHour)
        //{
        //    string sSqlString = null;
        //    object temp = null;
        //    try
        //    {
        //        if (oCostPerHour == ResourceReturnType.CostPerHour)
        //        {
        //            return db.SystemUsers.Where(s => s.SystemUserId == ResourceID).Select(c => c.CostPerHour).FirstOrDefault();
                    
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("ExecuteUserResource", ex);
        //    }
        //}

        //public double ExecuteUserStockItem(long StockID, StockPriceType StockPriceType, ref double PerQtyQty)
        //{
        //    string sSqlString = null;
        //    object temp = null;

        //    try
        //    {

        //        // can we call a procedure here ask sir mz

        //        SqlParameter[] oParams = new SqlParameter[4];
        //        oParams(0) = new SqlParameter("@StockID", SqlDbType.Int);
        //        oParams(0).Value = StockID;

        //        oParams(1) = new SqlParameter("@CalculationType", SqlDbType.Int);
        //        oParams(1).Value = Convert.ToInt32(StockPriceType);

        //        oParams(2) = new SqlParameter("@returnPrice", SqlDbType.Float);
        //        oParams(2).Direction = ParameterDirection.Output;

        //        oParams(3) = new SqlParameter("@PerQtyQty", SqlDbType.Float);
        //        oParams(3).Direction = ParameterDirection.Output;



        //        //If StockPriceType = StockPriceType.PerPack Then
        //        //    sSqlString = "sp_CostCentreExecution_get_StockPriceByCalculationType"

        //        //ElseIf StockPriceType = StockPriceType.PerUnit Then
        //        //    sSqlString = "select (unitRate + if ( itemprocessingcharge  > 0 , itemprocessingcharge, 0) )  as price from tbl_stockitems where ItemID=" + StockID.ToString
        //        //End If

        //        SQLHelper.ExecuteScalar(g_GlobalData.AppSettings.ConnectionString, CommandType.StoredProcedure, "sp_CostCentreExecution_get_StockPriceByCalculationType", oParams);

        //        temp = oParams(2).Value;



        //        if ((temp != null))
        //        {
        //            if ((!object.ReferenceEquals(temp, DBNull.Value)))
        //            {
        //                //returen the value
        //                return Convert.ToDouble(temp);
        //            }
        //        }

        //        temp = oParams(3).Value;

        //        if ((temp != null))
        //        {
        //            if ((!object.ReferenceEquals(temp, DBNull.Value)))
        //            {
        //                //returen the value
        //                PerQtyQty = oParams(3).Value;
        //            }
        //        }



        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("ExecuteUserStockItem", ex);
        //    }
        //}
        #endregion
    }
}
