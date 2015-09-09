using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPC.Models.DomainModels
{
    [Serializable()]
    public class CostCentre
    {
        public long CostCentreId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public int? CreatedBy { get; set; }
        public string LockedBy { get; set; }
        public string LastModifiedBy { get; set; }
        public double? MinimumCost { get; set; }
        public double? SetupCost { get; set; }
        public int? SetupTime { get; set; }
        public double? DefaultVA { get; set; }
        public int DefaultVAId { get; set; }
        public double? OverHeadRate { get; set; }
        public double? HourlyCharge { get; set; }
        public double? CostPerThousand { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int? PreferredSupplierId { get; set; }
        public string CodeFileName { get; set; }
        public int? nominalCode { get; set; }
        public int? CompletionTime { get; set; }
        public string HeaderCode { get; set; }
        public string MiddleCode { get; set; }
        public string FooterCode { get; set; }
        public string strCostPlantParsed { get; set; }
        public string strCostPlantUnParsed { get; set; }
        public string strCostLabourParsed { get; set; }
        public string strCostLabourUnParsed { get; set; }
        public string strCostMaterialParsed { get; set; }
        public string strCostMaterialUnParsed { get; set; }
        public string strPricePlantParsed { get; set; }
        public string strPricePlantUnParsed { get; set; }
        public string strPriceLabourParsed { get; set; }
        public string strPriceLabourUnParsed { get; set; }
        public string strPriceMaterialParsed { get; set; }
        public string strPriceMaterialUnParsed { get; set; }
        public string strActualCostPlantParsed { get; set; }
        public string strActualCostPlantUnParsed { get; set; }
        public string strActualCostLabourParsed { get; set; }
        public string strActualCostLabourUnParsed { get; set; }
        public string strActualCostMaterialParsed { get; set; }
        public string strActualCostMaterialUnParsed { get; set; }
        public string strTimeParsed { get; set; }
        public string strTimeUnParsed { get; set; }
        public bool IsDisabled { get; set; }
        public short IsDirectCost { get; set; }
        public int SetupSpoilage { get; set; }
        public double RunningSpoilage { get; set; }
        public int CalculationMethodType { get; set; }
        public int? NoOfHours { get; set; }
        public double? PerHourCost { get; set; }
        public double? PerHourPrice { get; set; }
        public int? UnitQuantity { get; set; }
        public int? QuantitySourceType { get; set; }
        public int? QuantityVariableId { get; set; }
        public string QuantityQuestionString { get; set; }
        public double QuantityQuestionDefaultValue { get; set; }
        public string QuantityCalculationString { get; set; }
        public double? CostPerUnitQuantity { get; set; }
        public double? PricePerUnitQuantity { get; set; }
        public double? TimePerUnitQuantity { get; set; }
        public int TimeRunSpeed { get; set; }
        public int TimeNoOfPasses { get; set; }
        public int? TimeSourceType { get; set; }
        public int? TimeVariableId { get; set; }
        public string TimeQuestionString { get; set; }
        public double TimeQuestionDefaultValue { get; set; }
        public string TimeCalculationString { get; set; }
        public int? Priority { get; set; }
        public string CostQuestionString { get; set; }
        public double? CostDefaultValue { get; set; }
        public string PriceQuestionString { get; set; }
        public double? PriceDefaultValue { get; set; }
        public string EstimatedTimeQuestionString { get; set; }
        public int? EstimatedTimeDefaultValue { get; set; }
        public int Sequence { get; set; }
        public string CompleteCode { get; set; }
        public string ItemDescription { get; set; }
        public int? SystemTypeId { get; set; }
        public int? FlagId { get; set; }
        public short IsScheduleable { get; set; }
        public int SystemSiteId { get; set; }
        public short IsPrintOnJobCard { get; set; }
        public string WebStoreDesc { get; set; }
        public bool? isPublished { get; set; }
        public decimal? EstimateProductionTime { get; set; }
        public string MainImageURL { get; set; }
        public string ThumbnailImageURL { get; set; }
        public bool? isOption1 { get; set; }
        public bool? isOption2 { get; set; }
        public bool? isOption3 { get; set; }
        public string TextOption1 { get; set; }
        public string TextOption2 { get; set; }
        public string TextOption3 { get; set; }
        public int? CCIDOption1 { get; set; }
        public int? CCIDOption2 { get; set; }
        public int? CCIDOption3 { get; set; }
        public double? SetupCharge2 { get; set; }
        public double? SetupCharge3 { get; set; }
        public double? MinimumCost2 { get; set; }
        public double? MinimumCost3 { get; set; }
        public double? PricePerUnitQuantity2 { get; set; }
        public double? PricePerUnitQuantity3 { get; set; }
        public int? QuantityVariableID2 { get; set; }
        public int? QuantityVariableID3 { get; set; }
        public int? QuantitySourceType2 { get; set; }
        public int? QuantitySourceType3 { get; set; }
        public string QuantityQuestionString2 { get; set; }
        public string QuantityQuestionString3 { get; set; }
        public double? QuantityQuestionDefaultValue2 { get; set; }
        public double? QuantityQuestionDefaultValue3 { get; set; }
        public int? DefaultVAId2 { get; set; }
        public int? DefaultVAId3 { get; set; }
        public bool? IsPrintOnJobCard2 { get; set; }
        public bool? IsPrintOnJobCard3 { get; set; }
        public bool? IsDirectCost2 { get; set; }
        public bool? IsDirectCost3 { get; set; }
        public int? PreferredSupplierID2 { get; set; }
        public int? PreferredSupplierID3 { get; set; }
        public decimal? EstimateProductionTime2 { get; set; }
        public decimal? EstimateProductionTime3 { get; set; }
        public double? DeliveryCharges { get; set; }
        public bool? isFromMIS { get; set; }
        public string XeroAccessCode { get; set; }
        public long? OrganisationId { get; set; }
        public int? DeliveryType { get; set; }
        public string DeliveryServiceType { get; set; }
        public long? CarrierId { get; set; }
        [NotMapped]
        public bool IsParsed { get; set; }
        [NotMapped]
        public string ImageBytes { get; set; }
        [NotMapped]
        public string TypeName { get; set; }
        public virtual CostCentreType CostCentreType { get; set; }
        public virtual ICollection<SectionCostcentre> SectionCostcentres { get; set; }
        public virtual ICollection<ItemAddonCostCentre> ItemAddonCostCentres { get; set; }
        public virtual ICollection<CostcentreInstruction> CostcentreInstructions { get; set; }
        public virtual ICollection<CostcentreResource> CostcentreResources { get; set; }
        public virtual ICollection<CompanyCostCentre> CompanyCostCentres { get; set; }
    }
}
