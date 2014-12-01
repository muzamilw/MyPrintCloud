﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class CostCentre
    {
        public long CostCentreId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public string LockedBy { get; set; }
        public string LastModifiedBy { get; set; }
        public Nullable<double> MinimumCost { get; set; }
        public Nullable<double> SetupCost { get; set; }
        public Nullable<int> SetupTime { get; set; }
        public Nullable<double> DefaultVA { get; set; }
        public int DefaultVAId { get; set; }
        public Nullable<double> OverHeadRate { get; set; }
        public Nullable<double> HourlyCharge { get; set; }
        public Nullable<double> CostPerThousand { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public Nullable<System.DateTime> LastModifiedDate { get; set; }
        public Nullable<int> PreferredSupplierId { get; set; }
        public string CodeFileName { get; set; }
        public Nullable<int> nominalCode { get; set; }
        public Nullable<int> CompletionTime { get; set; }
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
        public short IsDisabled { get; set; }
        public short IsDirectCost { get; set; }
        public int SetupSpoilage { get; set; }
        public double RunningSpoilage { get; set; }
        public int CalculationMethodType { get; set; }
        public Nullable<int> NoOfHours { get; set; }
        public Nullable<double> PerHourCost { get; set; }
        public Nullable<double> PerHourPrice { get; set; }
        public Nullable<int> UnitQuantity { get; set; }
        public Nullable<int> QuantitySourceType { get; set; }
        public Nullable<int> QuantityVariableId { get; set; }
        public string QuantityQuestionString { get; set; }
        public double QuantityQuestionDefaultValue { get; set; }
        public string QuantityCalculationString { get; set; }
        public Nullable<double> CostPerUnitQuantity { get; set; }
        public Nullable<double> PricePerUnitQuantity { get; set; }
        public Nullable<double> TimePerUnitQuantity { get; set; }
        public int TimeRunSpeed { get; set; }
        public int TimeNoOfPasses { get; set; }
        public Nullable<int> TimeSourceType { get; set; }
        public Nullable<int> TimeVariableId { get; set; }
        public string TimeQuestionString { get; set; }
        public double TimeQuestionDefaultValue { get; set; }
        public string TimeCalculationString { get; set; }
        public Nullable<int> Priority { get; set; }
        public string CostQuestionString { get; set; }
        public Nullable<double> CostDefaultValue { get; set; }
        public string PriceQuestionString { get; set; }
        public Nullable<double> PriceDefaultValue { get; set; }
        public string EstimatedTimeQuestionString { get; set; }
        public Nullable<int> EstimatedTimeDefaultValue { get; set; }
        public int Sequence { get; set; }
        public string CompleteCode { get; set; }
        public string ItemDescription { get; set; }
        public int CompanyId { get; set; }
        public Nullable<int> SystemTypeId { get; set; }
        public Nullable<int> FlagId { get; set; }
        public short IsScheduleable { get; set; }
        public int SystemSiteId { get; set; }
        public short IsPrintOnJobCard { get; set; }
        public string WebStoreDesc { get; set; }
        public Nullable<bool> isPublished { get; set; }
        public Nullable<decimal> EstimateProductionTime { get; set; }
        public string MainImageURL { get; set; }
        public string ThumbnailImageURL { get; set; }
        public Nullable<bool> isOption1 { get; set; }
        public Nullable<bool> isOption2 { get; set; }
        public Nullable<bool> isOption3 { get; set; }
        public string TextOption1 { get; set; }
        public string TextOption2 { get; set; }
        public string TextOption3 { get; set; }
        public Nullable<int> CCIDOption1 { get; set; }
        public Nullable<int> CCIDOption2 { get; set; }
        public Nullable<int> CCIDOption3 { get; set; }
        public Nullable<double> SetupCharge2 { get; set; }
        public Nullable<double> SetupCharge3 { get; set; }
        public Nullable<double> MinimumCost2 { get; set; }
        public Nullable<double> MinimumCost3 { get; set; }
        public Nullable<double> PricePerUnitQuantity2 { get; set; }
        public Nullable<double> PricePerUnitQuantity3 { get; set; }
        public Nullable<int> QuantityVariableID2 { get; set; }
        public Nullable<int> QuantityVariableID3 { get; set; }
        public Nullable<int> QuantitySourceType2 { get; set; }
        public Nullable<int> QuantitySourceType3 { get; set; }
        public string QuantityQuestionString2 { get; set; }
        public string QuantityQuestionString3 { get; set; }
        public Nullable<double> QuantityQuestionDefaultValue2 { get; set; }
        public Nullable<double> QuantityQuestionDefaultValue3 { get; set; }
        public Nullable<int> DefaultVAId2 { get; set; }
        public Nullable<int> DefaultVAId3 { get; set; }
        public Nullable<bool> IsPrintOnJobCard2 { get; set; }
        public Nullable<bool> IsPrintOnJobCard3 { get; set; }
        public Nullable<bool> IsDirectCost2 { get; set; }
        public Nullable<bool> IsDirectCost3 { get; set; }
        public Nullable<int> PreferredSupplierID2 { get; set; }
        public Nullable<int> PreferredSupplierID3 { get; set; }
        public Nullable<decimal> EstimateProductionTime2 { get; set; }
        public Nullable<decimal> EstimateProductionTime3 { get; set; }
        public Nullable<double> DeliveryCharges { get; set; }
        public Nullable<bool> isFromMIS { get; set; }
        public string XeroAccessCode { get; set; }
        public Nullable<long> OrganisationId { get; set; }

        public virtual ICollection<ItemAddonCostCentre> ItemAddonCostCentres { get; set; }
    }
}
