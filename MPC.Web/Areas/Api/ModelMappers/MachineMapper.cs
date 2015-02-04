﻿using System.Linq;
using DomainModels = MPC.Models.DomainModels;
using DomainResponseModel = MPC.Models.ResponseModels;
using MPC.Models.ResponseModels;
using MPC.Models.DomainModels;

using APIDomainModels = MPC.MIS.Areas.Api.Models;
namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Machine WebApi Mapper
    /// </summary>
    public static class MachineMapper
    {

        #region Public

        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static APIDomainModels.MachineSearchResponse CreateFrom(this DomainResponseModel.MachineSearchResponse source)
        {
            return new APIDomainModels.MachineSearchResponse
            {
                Machines = source.Machines != null ? source.Machines.Select(machine => machine.CreateFrom()).ToList() : null,
                TotalCount = source.TotalCount
            };
        }

        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static APIDomainModels.LookupMethod LookupMethodMapper(this DomainModels.LookupMethod source)
        {
            return new APIDomainModels.LookupMethod
            {
                MethodId=source.MethodId,
                Name=source.Name,
                Type = source.Type,
                LockedBy = source.LockedBy,
                CompanyId = source.CompanyId,
                FlagId = source.FlagId,
                SystemSiteId = source.SystemSiteId

                

            };

        }

        public static APIDomainModels.Machine CreateFrom(this DomainModels.Machine source)
        {
            return new APIDomainModels.Machine
            {
                MachineId = source.MachineId,
                MachineName = source.MachineName,
                MachineCatId = source.MachineCatId,
                ColourHeads = source.ColourHeads,
                isPerfecting = source.isPerfecting,
                SetupCharge = source.SetupCharge,
                WashupPrice = source.WashupPrice,
                WashupCost = source.WashupCost,
                MinInkDuctqty = source.MinInkDuctqty,
                worknturncharge = source.worknturncharge,
                MakeReadyCost = source.MakeReadyCost,
                DefaultFilmId = source.DefaultFilmId,
                DefaultPlateId = source.DefaultPlateId,
                DefaultPaperId = source.DefaultPaperId,
                isfilmused = source.isfilmused,
                isplateused = source.isplateused,
                ismakereadyused = source.ismakereadyused,
                iswashupused = source.iswashupused,
                maximumsheetweight = source.maximumsheetweight,
                maximumsheetheight = source.maximumsheetheight,
                maximumsheetwidth = source.maximumsheetwidth,
                minimumsheetheight = source.minimumsheetheight,
                minimumsheetwidth = source.minimumsheetwidth,
                gripdepth = source.gripdepth,
                gripsideorientaion = source.gripsideorientaion,
                gutterdepth = source.gutterdepth,
                headdepth = source.headdepth,
                Va = source.Va,
                PressSizeRatio = source.PressSizeRatio,
                Description = source.Description,
                Priority = source.Priority,
                DirectCost = source.DirectCost,
                MinimumCharge = source.MinimumCharge,
                CostPerCut = source.CostPerCut,
                PricePerCut = source.PricePerCut,
                IsAdditionalOption = source.IsAdditionalOption,
                IsDisabled = source.IsDisabled,
                LockedBy = source.LockedBy,
                CylinderSizeId = source.CylinderSizeId,
                MaxItemAcrossCylinder = source.MaxItemAcrossCylinder,
                Web1MRCost = source.Web1MRCost,
                Web1MRPrice = source.Web1MRPrice,
                Web2MRCost = source.Web2MRPrice,
                Web2MRPrice = source.Web2MRPrice,
                ReelMRCost = source.ReelMRCost,
                ReelMRPrice = source.ReelMRPrice,
                IsMaxColorLimit = source.IsMaxColorLimit,
                PressUtilization = source.PressUtilization,
                MakeReadyPrice = source.MakeReadyPrice,
                InkChargeForUniqueColors = source.InkChargeForUniqueColors,
                CompanyId = source.CompanyId,
                FlagId = source.FlagId,
                IsScheduleable = source.IsScheduleable,
                SystemSiteId = source.SystemSiteId,
                SpoilageType = source.SpoilageType,
                SetupTime = source.SetupTime,
                TimePerCut = source.TimePerCut,
                MakeReadyTime = source.MakeReadyTime,
                WashupTime = source.WashupTime,
                ReelMakereadyTime = source.ReelMakereadyTime,
                LookupMethodId=source.LookupMethodId
                //MachineInkCoverages = source.MachineInkCoverages,
                //MachineResources = source.MachineResources
            };
        }

        #endregion

    }
}


