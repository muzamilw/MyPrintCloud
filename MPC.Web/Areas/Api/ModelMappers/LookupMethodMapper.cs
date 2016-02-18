using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using APIDomainModels = MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;
using ResponseDomainModels = MPC.Models.ResponseModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class LookupMethodMapper
    {
        public static APIDomainModels.LookupMethod CreateFrom(this DomainModels.LookupMethod source)
        {
            return new APIDomainModels.LookupMethod
            {
                MethodId = source.MethodId,
                Name = source.Name,
                Type = source.Type,
                LockedBy = source.LockedBy,
                OrganisationId = source.OrganisationId,
                FlagId = source.FlagId,
                SystemSiteId = source.SystemSiteId,
                MachineClickChargeZones = source.MachineClickChargeZones == null ? null : source.MachineClickChargeZones.Select(m => m.CreateFrom()).ToList(),
                MachineMeterPerHourLookups = source.MachineMeterPerHourLookups == null ? null : source.MachineMeterPerHourLookups.Select(m => m.CreateFrom()).ToList(),

                MachineGuillotineCalcs = source.MachineGuillotineCalcs == null ? null : source.MachineGuillotineCalcs.Select(m => m.CreateFrom()).ToList(),
                MachineSpeedWeightLookups = source.MachineSpeedWeightLookups == null ? null : source.MachineSpeedWeightLookups.Select(m => m.CreateFrom()).ToList()
                
            };

        }
        public static DomainModels.LookupMethod CreateFrom(this APIDomainModels.LookupMethod source)
        {
            return new DomainModels.LookupMethod
            {
                MethodId = source.MethodId,
                Name = source.Name,
                Type = source.Type,
                LockedBy = source.LockedBy,
                OrganisationId = source.OrganisationId,
                FlagId = source.FlagId,
                SystemSiteId = source.SystemSiteId,

                 MachineClickChargeZones = source.MachineClickChargeZones == null ? null : source.MachineClickChargeZones.Select(m => m.CreateFrom()).ToList(),
                MachineMeterPerHourLookups = source.MachineMeterPerHourLookups == null ? null : source.MachineMeterPerHourLookups.Select(m => m.CreateFrom()).ToList(),

                MachineGuillotineCalcs = source.MachineGuillotineCalcs == null ? null : source.MachineGuillotineCalcs.Select(m => m.CreateFrom()).ToList(),
                MachineSpeedWeightLookups = source.MachineSpeedWeightLookups == null ? null : source.MachineSpeedWeightLookups.Select(m => m.CreateFrom()).ToList()
            };

        }
        public static ResponseDomainModels.LookupMethodResponse CreateFrom(this APIDomainModels.LookupMethodResponse source)
        {
            return new ResponseDomainModels.LookupMethodResponse
            {
                LookupMethodId = source.LookupMethodId,
                LookupMethod = source.LookupMethod == null ? null : source.LookupMethod.CreateFrom(),
                ClickChargeLookup = source.ClickChargeLookup == null ? null : source.ClickChargeLookup.CreateFrom(),
                ClickChargeZone = source.ClickChargeZone == null ? null : source.ClickChargeZone.CreateFrom(),
                GuillotineCalc = source.GuillotineCalc == null ? null : source.GuillotineCalc.CreateFrom(),
                GuilotinePtv = source.GuilotinePtv == null ? null : source.GuilotinePtv.Select(g => g.CreateFrom()),
                MeterPerHourLookup = source.MeterPerHourLookup == null ? null : source.MeterPerHourLookup.CreateFrom(),
                PerHourLookup = source.PerHourLookup == null ? null : source.PerHourLookup.CreateFrom(),
                SpeedWeightLookup = source.SpeedWeightLookup == null ? null : source.SpeedWeightLookup.CreateFrom()



            };

        }
        public static APIDomainModels.LookupMethodResponse CreateFrom(this ResponseDomainModels.LookupMethodResponse source)
        {
            return new APIDomainModels.LookupMethodResponse
            {
                LookupMethod = source.LookupMethod == null ? null : source.LookupMethod.CreateFrom(),
                ClickChargeLookup = source.ClickChargeLookup == null ? null : source.ClickChargeLookup.CreateFrom(),
                ClickChargeZone = source.ClickChargeZone == null ? null : source.ClickChargeZone.CreateFrom(),
                GuillotineCalc = source.GuillotineCalc == null ? null : source.GuillotineCalc.CreateFrom(),
                GuilotinePtv = source.GuilotinePtv == null ? null : source.GuilotinePtv.Select(g=>g.CreateFrom()),
                MeterPerHourLookup = source.MeterPerHourLookup == null ? null : source.MeterPerHourLookup.CreateFrom(),
                PerHourLookup = source.PerHourLookup == null ? null : source.PerHourLookup.CreateFrom(),
                SpeedWeightLookup = source.SpeedWeightLookup == null ? null : source.SpeedWeightLookup.CreateFrom(),
               CurrencySymbol = source.CurrencySymbol



            };

        }
        public static APIDomainModels.LookupMethodListResponse CreateFrom(this ResponseDomainModels.LookupMethodListResponse source)
        {
            return new APIDomainModels.LookupMethodListResponse
            {
                LookupMethods = source.LookupMethods == null ? null : source.LookupMethods.Select(g => g.CreateFrom()),
                CurrencySymbol = source.CurrencySymbol == null ? null : source.CurrencySymbol,
                WeightUnit = source.WeightUnit == null ? null : source.WeightUnit,
                LengthUnit = source.LengthUnit == null ? null : source.LengthUnit
            };

        }

        //public static APIDomainModels.MachineClickChargeLookup CreateFrom(this DomainModels.MachineClickChargeLookup source)
        //{
        //    return new APIDomainModels.MachineClickChargeLookup
        //    {
        //        Id = source.Id,
        //        MethodId = source.MethodId,
        //        SheetCost = source.SheetCost,
        //        Sheets = source.Sheets,
        //        SheetPrice = source.SheetPrice,
        //        TimePerHour = source.TimePerHour
        //    };

        //}
        //public static APIDomainModels.MachineClickChargeZone CreateFrom(this DomainModels.MachineClickChargeZone source)
        //{
        //    return new APIDomainModels.MachineClickChargeZone
        //    {
        //        Id = source.Id,
        //        MethodId = source.MethodId,
        //        From1 = source.From1,
        //        To1 = source.To1,
        //        Sheets1 = source.Sheets1,
        //        SheetCost1 = source.SheetCost1,
        //        SheetPrice1 = source.SheetPrice1,
        //        From2 = source.From2,
        //        To2 = source.To2,
        //        Sheets2 = source.Sheets2,
        //        SheetCost2 = source.SheetCost2,
        //        SheetPrice2 = source.SheetPrice2,
        //        From3 = source.From3,
        //        To3 = source.To3,
        //        Sheets3 = source.Sheets3,
        //        SheetCost3 = source.SheetCost3,
        //        SheetPrice3 = source.SheetPrice3,
        //        From4 = source.From4,
        //        To4 = source.To4,
        //        Sheets4 = source.Sheets4,
        //        SheetCost4 = source.SheetCost4,
        //        SheetPrice4 = source.SheetPrice4,
        //        From5 = source.From5,
        //        To5 = source.To5,
        //        Sheets5 = source.Sheets5,
        //        SheetCost5 = source.SheetCost5,
        //        SheetPrice5 = source.SheetPrice5,
        //        From6 = source.From6,
        //        To6 = source.To6,
        //        Sheets6 = source.Sheets6,
        //        SheetCost6 = source.SheetCost6,
        //        SheetPrice6 = source.SheetPrice6,
        //        From7 = source.From7,
        //        To7 = source.To7,
        //        Sheets7 = source.Sheets7,
        //        SheetCost7 = source.SheetCost7,
        //        SheetPrice7 = source.SheetPrice7,
        //        From8 = source.From8,
        //        To8 = source.To8,
        //        Sheets8 = source.Sheets8,
        //        SheetCost8 = source.SheetCost8,
        //        SheetPrice8 = source.SheetPrice8,
        //        From9 = source.From9,
        //        To9 = source.To9,
        //        Sheets9 = source.Sheets9,
        //        SheetCost9 = source.SheetCost9,
        //        SheetPrice9 = source.SheetPrice9,
        //        From10 = source.From10,
        //        To10 = source.To10,
        //        Sheets10 = source.Sheets10,
        //        SheetCost10 = source.SheetCost10,
        //        SheetPrice10 = source.SheetPrice10,
        //        From11 = source.From11,
        //        To11 = source.To11,
        //        Sheets11 = source.Sheets11,
        //        SheetCost11 = source.SheetCost11,
        //        SheetPrice11 = source.SheetPrice11,
        //        From12 = source.From12,
        //        To12 = source.To12,
        //        Sheets12 = source.Sheets12,
        //        SheetCost12 = source.SheetCost12,
        //        SheetPrice12 = source.SheetPrice12,
        //        From13 = source.From13,
        //        To13 = source.To13,
        //        Sheets13 = source.Sheets13,
        //        SheetCost13 = source.SheetCost13,
        //        SheetPrice13 = source.SheetPrice13,
        //        From14 = source.From14,
        //        To14 = source.To14,
        //        Sheets14 = source.Sheets14,
        //        SheetCost14 = source.SheetCost14,
        //        SheetPrice14 = source.SheetPrice14,
        //        From15 = source.From15,
        //        To15 = source.To15,
        //        Sheets15 = source.Sheets15,
        //        SheetCost15 = source.SheetCost15,
        //        SheetPrice15 = source.SheetPrice15,
        //        isaccumulativecharge = source.isaccumulativecharge,
        //        IsRoundUp = source.IsRoundUp,
        //        TimePerHour = source.TimePerHour
        //    };

        //}
        //public static APIDomainModels.MachineGuillotineCalc CreateFrom(this DomainModels.MachineGuillotineCalc source)
        //{
        //    return new APIDomainModels.MachineGuillotineCalc
        //    {
        //        Id = source.Id,
        //        MethodId = source.MethodId,
        //        PaperWeight1 = source.PaperWeight1,
        //        PaperThroatQty1 = source.PaperThroatQty1,
        //        PaperWeight2 = source.PaperWeight2,
        //        PaperThroatQty2 = source.PaperThroatQty2,
        //        PaperWeight3 = source.PaperWeight3,
        //        PaperThroatQty3 = source.PaperThroatQty3,
        //        PaperWeight4 = source.PaperWeight4,
        //        PaperThroatQty4 = source.PaperThroatQty4,
        //        PaperWeight5 = source.PaperWeight5,
        //        PaperThroatQty5 = source.PaperThroatQty5
        //    };
        //}
        //public static APIDomainModels.MachineGuilotinePtv CreateFrom(this DomainModels.MachineGuilotinePtv source)
        //{
        //    return new APIDomainModels.MachineGuilotinePtv
        //    {
        //        Id = source.Id,
        //        NoofSections = source.NoofSections,
        //        NoofUps = source.NoofUps,
        //        Noofcutswithoutgutters = source.Noofcutswithoutgutters,
        //        Noofcutswithgutters = source.Noofcutswithgutters,
        //        GuilotineId = source.GuilotineId
        //    };
        //}
        //public static APIDomainModels.MachineMeterPerHourLookup CreateFrom(this DomainModels.MachineMeterPerHourLookup source)
        //{
        //    return new APIDomainModels.MachineMeterPerHourLookup
        //    {
        //        Id = source.Id,
        //        MethodId = source.MethodId,
        //        SheetsQty1 = source.SheetsQty1,
        //        SheetsQty2 = source.SheetsQty2,
        //        SheetsQty3 = source.SheetsQty3,
        //        SheetsQty4 = source.SheetsQty4,
        //        SheetsQty5 = source.SheetsQty5,
        //        SheetWeight1 = source.SheetWeight1,
        //        speedqty11 = source.speedqty11,
        //        speedqty12 = source.speedqty12,
        //        speedqty13 = source.speedqty13,
        //        speedqty14 = source.speedqty14,
        //        speedqty15 = source.speedqty15,
        //        SheetWeight2 = source.SheetWeight2,
        //        speedqty21 = source.speedqty21,
        //        speedqty22 = source.speedqty22,
        //        speedqty23 = source.speedqty23,
        //        speedqty24 = source.speedqty24,
        //        speedqty25 = source.speedqty25,
        //        SheetWeight3 = source.SheetWeight3,
        //        speedqty31 = source.speedqty31,
        //        speedqty32 = source.speedqty32,
        //        speedqty33 = source.speedqty33,
        //        speedqty34 = source.speedqty34,
        //        speedqty35 = source.speedqty35,
        //        hourlyCost = source.hourlyCost,
        //        hourlyPrice = source.hourlyPrice
        //    };
        //}
        //public static APIDomainModels.MachinePerHourLookup CreateFrom(this DomainModels.MachinePerHourLookup source)
        //{
        //    return new APIDomainModels.MachinePerHourLookup
        //    {
        //        Id = source.Id,
        //        MethodId = source.MethodId,
        //        SpeedCost = source.SpeedCost,
        //        Speed = source.Speed,
        //        SpeedPrice = source.SpeedPrice
        //    };
        //}
        //public static APIDomainModels.MachineSpeedWeightLookup CreateFrom(this DomainModels.MachineSpeedWeightLookup source)
        //{
        //    return new APIDomainModels.MachineSpeedWeightLookup
        //    {
        //        Id = source.Id,
        //        MethodId = source.MethodId,
        //        SheetsQty1 = source.SheetsQty1,
        //        SheetsQty2 = source.SheetsQty2,
        //        SheetsQty3 = source.SheetsQty3,
        //        SheetsQty4 = source.SheetsQty4,
        //        SheetsQty5 = source.SheetsQty5,
        //        SheetWeight1 = source.SheetWeight1,
        //        speedqty11 = source.speedqty11,
        //        speedqty12 = source.speedqty12,
        //        speedqty13 = source.speedqty13,
        //        speedqty14 = source.speedqty14,
        //        speedqty15 = source.speedqty15,
        //        SheetWeight2 = source.SheetWeight2,
        //        speedqty21 = source.speedqty21,
        //        speedqty22 = source.speedqty22,
        //        speedqty23 = source.speedqty23,
        //        speedqty24 = source.speedqty24,
        //        speedqty25 = source.speedqty25,
        //        SheetWeight3 = source.SheetWeight3,
        //        speedqty31 = source.speedqty31,
        //        speedqty32 = source.speedqty32,
        //        speedqty33 = source.speedqty33,
        //        speedqty34 = source.speedqty34,
        //        speedqty35 = source.speedqty35,
        //        hourlyCost = source.hourlyCost,
        //        hourlyPrice = source.hourlyPrice

        //    };
        //}

        

    }
}