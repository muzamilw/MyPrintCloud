using System.Collections.Generic;
using MPC.Models.DomainModels;
using MPC.Models.Common;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Interfaces.Repository
{
    /// <summary>
    /// Cost Centre Repository Interface
    /// </summary>
    public interface ICostCentreRepository : IBaseRepository<CostCentre, long>
    {
        /// <summary>
        /// Get All Cost Centres that are not system defined
        /// </summary>
        IEnumerable<CostCentre> GetAllNonSystemCostCentres();
        /// <summary>
        /// Function from infinity
        /// </summary>
        /// <param name="CostCentreID"></param>
        /// <returns></returns>
        bool Delete(long CostCentreID);
        long GetMaxCostCentreID();
        bool UpdateSystemCostCentre(long CostCentreID, int ProfitMarginID, int NominalCodeId, double MinCost, int UserID, string Description, bool DirectCost, bool IsScheduleable);
        CostCentre GetCostCentreByID(long CostCentreID);
        CostcentreResource GetCostCentreResources(long CostcentreID);
        List<CostCentreResource> GetCostCentreResourcesWithNames(long CostcentreID);
        List<CostCentreType> GetCostCentreTypes(TypeReturnMode ReturnMode);
        List<CostcentreSystemType> GetCostCentreSystemTypes();
        CostcentreInstruction GetCostCentreWorkInstruction(long CostcentreID);
        List<CostCentreType> ReturnCostCentreCategories();
        List<CostCentre> GetCostCentreList();
        IEnumerable<CostCentre> GetAllCompanyCentersForOrderItem();
        bool CheckCostCentreName(long CostCentreID, string CostCentreName, long OrganisationId);
        long InsertWorkInstruction(CostcentreInstruction oInstruction);
        long UpdateWorkInstruction(CostcentreInstruction oInstruction);
        bool DeleteWorkInstruction(long InstructionID);
        long InsertChoice(CostcentreWorkInstructionsChoice ochoice);
        long UpdateChoice(CostcentreWorkInstructionsChoice ochoice);
        bool DeleteChoice(long ChoiceID);
        long InsertCostCentre(CostCentre oCostCentre);
        bool UpdateCostCentre(CostCentre oCostCentre);
        CostCentre GetCostCentreSummary(long CostCentreID);
        List<CostCentre> GetCompleteCodeofAllCostCentres(long OrganisationId);
        bool ChangeFlag(int FlagID, long CostCentreID);
        CostCentre GetSystemCostCentre(long SystemTypeID, long OrganisationID);
        List<CostCentreType> GetCostCentreCategories(long OrganisationId);
        bool IsCostCentreAvailable(int CategoryID);
        CostCentreTemplate LoadCostCentreTemplate(int TemplateID);
        double ExecUserVariable(CostCentreVariable oVariable);
        double ExecuteUserResource(long ResourceID, ResourceReturnType oCostPerHour);
        double ExecuteUserStockItem(int StockID, StockPriceType StockPriceType, out double Price, out double PerQtyQty);
        CostCentersResponse GetUserDefinedCostCenters(CostCenterRequestModel request);

        List<CostCentre> GetDeliveryCostCentersList();

        List<CostCentre> GetCorporateDeliveryCostCentersList(long CompanyID);

        CostCentre GetCostCentersByID(long costCenterID);
        IEnumerable<CostCentre> GetAllCompanyCentersByOrganisationId();

        List<CostCentre> GetCostCentersByOrganisationID(long OrganisationID, out List<CostCenterChoice> CostCentreChoices);

        CostCenterVariablesResponseModel GetCostCenterVariablesTree(int id);
        IEnumerable<CostCentre> GetAllDeliveryCostCentersForStore();

        CostCenterBaseResponse GetBaseData();

        List<CostCentreType> GetCostCentreTypeByOrganisationID(long OID);
        CostCentreResponse GetAllNonSystemCostCentresForProduct(GetCostCentresRequest request);

        List<CostCentre> GetCostCentresforxml(List<long> CostCenterIDs);
        /// <summary>
        /// Get web order cost centre
        /// </summary>
        CostCentre GetWebOrderCostCentre(long OrganisationId);

        CostCentre GetFirstCostCentreByOrganisationId(long organisationId);


        List<CostCentre> GetAllCostCentresForRecompiling(long OrganisationId);
        CostCentre GetGlobalWebOrderCostCentre(long OrganisationId);
        long GetCostCentreIdByName(string costCenterName);
    }
}
