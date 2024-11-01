﻿using System.Collections.Generic;
using System.Linq;
using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Item Vdp Price Mapper
    /// </summary>
    public static class CostCentreMapper
    {
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static CostCenterResponse CreateFrom(this MPC.Models.ResponseModels.CostCentersResponse source)
        {
            return new CostCenterResponse
            {
                CostCenters = source.CostCenters.Select(s => s.ListViewModelCreateFrom()),
                RowCount = source.RowCount
                
            };
        }
        
        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static CostCentre CreateFrom(this DomainModels.CostCentre source)
        {
            return new CostCentre
            {
                CostCentreId = source.CostCentreId,
                Name = source.Name,
                Type = source.Type,
                Description = source.Description,
                TypeName = source.TypeName!=null?source.TypeName:source.CostCentreType != null ? source.CostCentreType.TypeName : string.Empty,
                CalculationMethodType = source.CalculationMethodType,
                ItemDescription = source.ItemDescription,
                CreatedBy = source.CreatedBy,
                LockedBy = source.LockedBy,
                LastModifiedBy = source.LastModifiedBy,
                MinimumCost = source.MinimumCost,
                SetupCost = source.SetupCost,
                SetupTime = source.SetupTime,
                DefaultVA = source.DefaultVA,
                DefaultVAId = source.DefaultVAId,
                OverHeadRate = source.OverHeadRate,
                HourlyCharge = source.HourlyCharge,
                CostPerThousand = source.CostPerThousand,
                CreationDate = source.CreationDate,
                LastModifiedDate = source.LastModifiedDate,
                PreferredSupplierId = source.PreferredSupplierId,
                CodeFileName = source.CodeFileName,
                nominalCode = source.nominalCode,
                CompletionTime = source.CompletionTime,
                HeaderCode = source.HeaderCode,
                MiddleCode = source.MiddleCode,
                FooterCode = source.FooterCode,
                strCostPlantParsed = source.strCostPlantParsed,
                strCostPlantUnParsed = source.strCostPlantUnParsed,
                strCostLabourParsed = source.strCostLabourParsed,
                strCostLabourUnParsed = source.strCostLabourUnParsed,
                strCostMaterialParsed = source.strCostMaterialParsed,
                strCostMaterialUnParsed = source.strActualCostMaterialUnParsed,
                strPricePlantParsed = source.strPricePlantParsed,
                strPricePlantUnParsed = source.strPricePlantUnParsed,
                strPriceLabourParsed = source.strPriceLabourParsed,
                strPriceLabourUnParsed = source.strPriceLabourUnParsed,
                strPriceMaterialParsed = source.strPriceMaterialParsed,
                strPriceMaterialUnParsed = source.strPriceMaterialUnParsed,
                strActualCostPlantParsed = source.strActualCostPlantParsed,
                strActualCostPlantUnParsed = source.strActualCostPlantUnParsed,
                strActualCostLabourParsed = source.strActualCostLabourParsed,
                strActualCostLabourUnParsed = source.strActualCostLabourUnParsed,
                strActualCostMaterialParsed = source.strActualCostMaterialParsed,
                strActualCostMaterialUnParsed = source.strActualCostMaterialUnParsed,
                strTimeParsed = source.strTimeParsed,
                strTimeUnParsed = source.strTimeUnParsed,
                IsDisabled = source.IsDisabled,
                IsDirectCost = source.IsDirectCost,
                SetupSpoilage = source.SetupSpoilage,
                RunningSpoilage = source.RunningSpoilage,
                NoOfHours = source.NoOfHours,
                PerHourCost = source.PerHourCost,
                PerHourPrice = source.PerHourPrice,
                UnitQuantity = source.UnitQuantity,
                QuantityVariableId = source.QuantityVariableId,
                QuantityQuestionString = source.QuantityQuestionString,
                QuantityQuestionDefaultValue = source.QuantityQuestionDefaultValue,
                QuantityCalculationString = source.QuantityCalculationString,
                CostPerUnitQuantity = source.CostPerUnitQuantity,
                PricePerUnitQuantity = source.PricePerUnitQuantity,
                TimePerUnitQuantity = source.TimePerUnitQuantity,
                TimeRunSpeed = source.TimeRunSpeed,
                TimeNoOfPasses = source.TimeNoOfPasses,
                TimeVariableId = source.TimeVariableId,
                TimeQuestionString = source.TimeQuestionString,
                TimeQuestionDefaultValue = source.TimeQuestionDefaultValue,
                TimeCalculationString = source.TimeCalculationString,
                Priority = source.Priority,
                CostQuestionString = source.CostQuestionString,
                CostDefaultValue = source.CostDefaultValue,
                PriceQuestionString = source.PriceQuestionString,
                PriceDefaultValue = source.PriceDefaultValue,
                EstimatedTimeQuestionString = source.EstimatedTimeQuestionString,
                EstimatedTimeDefaultValue = source.EstimatedTimeDefaultValue,
                Sequence = source.Sequence,
                CompleteCode = source.CompleteCode,
                SystemTypeId = source.SystemTypeId,
                FlagId = source.FlagId,
                IsScheduleable = source.IsScheduleable,
                IsPrintOnJobCard = source.IsPrintOnJobCard,
                WebStoreDesc = source.WebStoreDesc,
                isPublished = source.isPublished,
                EstimateProductionTime = source.EstimateProductionTime,
                MainImageURL = source.MainImageURL,
                ThumbnailImageURL = source.ThumbnailImageURL,
                DeliveryCharges = source.DeliveryCharges,
                XeroAccessCode = source.XeroAccessCode,
                OrganisationId = source.OrganisationId,
                TimeSourceType = source.TimeSourceType,
                QuantitySourceType = source.QuantitySourceType,
                DeliveryServiceType= source.DeliveryServiceType,
                CarrierId=source.CarrierId,
                ImageBytes = source.ImageBytes,
                IsParsed = source.IsParsed,
                CostcentreResources = source.CostcentreResources != null ? source.CostcentreResources.Select(x => x.CreateFrom()).ToList() : null,
                CostcentreInstructions = source.CostcentreInstructions != null? source.CostcentreInstructions.Select(x => x.CreateFrom()).ToList() : null,
                FixedVariables = CostCenterVariables(source)
            };
        }

        /// <summary>
        /// Create From Client Model
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static MPC.Models.DomainModels.CostCentre CreateFrom(this CostCentre source)
        {
            return new MPC.Models.DomainModels.CostCentre
            {
                CostCentreId = source.CostCentreId,
                Name = source.Name,
                Type = source.Type,
                Description = source.Description,
                CalculationMethodType = source.CalculationMethodType,
                ItemDescription = source.ItemDescription,
                CreatedBy = source.CreatedBy,
                LockedBy = source.LockedBy,
                LastModifiedBy = source.LastModifiedBy,
                MinimumCost = source.MinimumCost,
                SetupCost = source.SetupCost,
                SetupTime = source.SetupTime,
                DefaultVA = source.DefaultVA,
                DefaultVAId = source.DefaultVAId,
                OverHeadRate = source.OverHeadRate,
                HourlyCharge = source.HourlyCharge,
                CostPerThousand = source.CostPerThousand,
                CreationDate = source.CreationDate,
                LastModifiedDate = source.LastModifiedDate,
                PreferredSupplierId = source.PreferredSupplierId,
                CodeFileName = source.CodeFileName,
                nominalCode = source.nominalCode,
                CompletionTime = source.CompletionTime,
                HeaderCode = source.HeaderCode,
                MiddleCode = source.MiddleCode,
                FooterCode = source.FooterCode,
                strCostPlantParsed = source.strCostPlantParsed,
                strCostPlantUnParsed = source.strCostPlantUnParsed,
                strCostLabourParsed = source.strCostLabourParsed,
                strCostLabourUnParsed = source.strCostLabourUnParsed,
                strCostMaterialParsed = source.strCostMaterialParsed,
                strCostMaterialUnParsed = source.strActualCostMaterialUnParsed,
                strPricePlantParsed = source.strPricePlantParsed,
                strPricePlantUnParsed = source.strPricePlantUnParsed,
                strPriceLabourParsed = source.strPriceLabourParsed, 
                strPriceLabourUnParsed = source.strPriceLabourUnParsed,
                strPriceMaterialParsed = source.strPriceMaterialParsed,
                strPriceMaterialUnParsed = source.strPriceMaterialUnParsed,
                strActualCostPlantParsed = source.strActualCostPlantParsed,
                strActualCostPlantUnParsed = source.strActualCostPlantUnParsed,
                strActualCostLabourParsed = source.strActualCostLabourParsed,
                strActualCostLabourUnParsed = source.strActualCostLabourUnParsed,
                strActualCostMaterialParsed = source.strActualCostMaterialParsed,
                strActualCostMaterialUnParsed = source.strActualCostMaterialUnParsed,
                strTimeParsed = source.strTimeParsed,
                strTimeUnParsed = source.strTimeUnParsed,
                IsDisabled = source.IsDisabled,
                IsDirectCost = source.IsDirectCost,
                SetupSpoilage = source.SetupSpoilage,
                RunningSpoilage = source.RunningSpoilage,
                NoOfHours = source.NoOfHours,
                PerHourCost = source.PerHourCost,
                PerHourPrice = source.PerHourPrice,
                UnitQuantity = source.UnitQuantity,
                QuantityVariableId = source.QuantityVariableId,
                QuantityQuestionString = source.QuantityQuestionString,
                QuantityQuestionDefaultValue = source.QuantityQuestionDefaultValue,
                QuantityCalculationString = source.QuantityCalculationString,
                CostPerUnitQuantity = source.CostPerUnitQuantity,
                PricePerUnitQuantity = source.PricePerUnitQuantity,
                TimePerUnitQuantity = source.TimePerUnitQuantity,
                TimeRunSpeed = source.TimeRunSpeed,
                TimeNoOfPasses = source.TimeNoOfPasses,
                TimeVariableId = source.TimeVariableId,
                TimeQuestionString = source.TimeQuestionString,
                TimeQuestionDefaultValue = source.TimeQuestionDefaultValue,
                TimeCalculationString = source.TimeCalculationString,
                Priority = source.Priority,
                CostQuestionString = source.CostQuestionString,
                CostDefaultValue = source.CostDefaultValue,
                PriceQuestionString = source.PriceQuestionString,
                PriceDefaultValue = source.PriceDefaultValue,
                EstimatedTimeQuestionString = source.EstimatedTimeQuestionString,
                EstimatedTimeDefaultValue = source.EstimatedTimeDefaultValue,
                Sequence = source.Sequence,
                CompleteCode = source.CompleteCode,
                SystemTypeId = source.SystemTypeId,
                FlagId = source.FlagId,
                IsScheduleable = source.IsScheduleable,
                IsPrintOnJobCard = source.IsPrintOnJobCard,
                WebStoreDesc = source.WebStoreDesc,
                isPublished = source.isPublished,
                EstimateProductionTime = source.EstimateProductionTime,
                MainImageURL = source.MainImageURL,
                ThumbnailImageURL = source.ThumbnailImageURL,
                DeliveryCharges = source.DeliveryCharges,
                XeroAccessCode = source.XeroAccessCode,
                OrganisationId = source.OrganisationId,
                TimeSourceType = source.TimeSourceType,
                QuantitySourceType = source.QuantitySourceType,
                DeliveryServiceType= source.DeliveryServiceType,
                CarrierId = source.CarrierId,
                ImageBytes = source.ImageBytes,
                CostcentreResources = source.CostcentreResources != null ? source.CostcentreResources.Select(x => x.CreateFrom()).ToList() : null,
                CostcentreInstructions = source.CostcentreInstructions != null ? source.CostcentreInstructions.Select(x => x.CreateFrom()).ToList() : null
            };
        }

        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static CostCentre CreateFromDropDown(this DomainModels.CostCentre source)
        {
            return new CostCentre
            {
                CostCentreId = source.CostCentreId,
                Name = source.Name,
                Type = source.Type,
                TypeName = source.CostCentreType != null ? source.CostCentreType.TypeName : string.Empty
            };
        }

        private static List<CostCenterFixedVariable> CostCenterVariables(DomainModels.CostCentre source)
        {
            List<CostCenterFixedVariable> dict = new List<CostCenterFixedVariable>();
            dict.Add(new CostCenterFixedVariable { Id = "1fv" + source.CostCentreId, Name = "Total Cost", VariableString = "{SubCostCentre," + "ID=&quot;" + source.CostCentreId + "&quot;,Name=&quot;" + source.Name + "&quot;,ReturnValue=TotalCost}" });
            dict.Add(new CostCenterFixedVariable { Id = "2fv" + source.CostCentreId, Name = "Total Price", VariableString = "{SubCostCentre," + "ID=&quot;" + source.CostCentreId + "&quot;,Name=&quot;" + source.Name + "&quot;,ReturnValue=TotalPrice}" });
            dict.Add(new CostCenterFixedVariable { Id = "3fv" + source.CostCentreId, Name = "Plant Cost", VariableString = "{SubCostCentre," + "ID=&quot;" + source.CostCentreId + "&quot;,Name=&quot;" + source.Name + "&quot;,ReturnValue=PlantCost}" });
            dict.Add(new CostCenterFixedVariable { Id = "4fv" + source.CostCentreId, Name = "Resource Cost", VariableString = "{SubCostCentre," + "ID=&quot;" + source.CostCentreId + "&quot;,Name=&quot;" + source.Name + "&quot;,ReturnValue=ResourceCost}" });
            dict.Add(new CostCenterFixedVariable { Id = "5fv" + source.CostCentreId, Name = "Stock Cost", VariableString = "{SubCostCentre," + "ID=&quot;" + source.CostCentreId + "&quot;,Name=&quot;" + source.Name + "&quot;,ReturnValue=StockCost}" });
            dict.Add(new CostCenterFixedVariable { Id = "6fv" + source.CostCentreId, Name = "Plant Price", VariableString = "{SubCostCentre," + "ID=&quot;" + source.CostCentreId + "&quot;,Name=&quot;" + source.Name + "&quot;,ReturnValue=PlantPrice}" });
            dict.Add(new CostCenterFixedVariable { Id = "7fv" + source.CostCentreId, Name = "Resource Price", VariableString = "{SubCostCentre," + "ID=&quot;" + source.CostCentreId + "&quot;,Name=&quot;" + source.Name + "&quot;,ReturnValue=ResourcePrice}" });
            dict.Add(new CostCenterFixedVariable { Id = "8fv" + source.CostCentreId, Name = "Stock Price", VariableString = "{SubCostCentre," + "ID=&quot;" + source.CostCentreId + "&quot;,Name=&quot;" + source.Name + "&quot;,ReturnValue=StockPrice}" });
            
            return dict;
        }
        
    }
}