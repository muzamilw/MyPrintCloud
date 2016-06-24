using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
     public class MachineClickChargeZone
    {
        public long Id { get; set; }
        public long? MethodId { get; set; }
        public long? From1 { get; set; }
        public long? To1 { get; set; }
        public long? Sheets1 { get; set; }
        public double? SheetCost1 { get; set; }
        public double SheetPrice1 { get; set; }
        public long? From2 { get; set; }
        public long? To2 { get; set; }
        public long? Sheets2 { get; set; }
        public double? SheetCost2 { get; set; }
        public double SheetPrice2 { get; set; }
        public long? From3 { get; set; }
        public long? To3 { get; set; }
        public long? Sheets3 { get; set; }
        public double? SheetCost3 { get; set; }
        public double SheetPrice3 { get; set; }
        public long? From4 { get; set; }
        public long? To4 { get; set; }
        public long? Sheets4 { get; set; }
        public double? SheetCost4 { get; set; }
        public double SheetPrice4 { get; set; }
        public long? From5 { get; set; }
        public long? To5 { get; set; }
        public long? Sheets5 { get; set; }
        public double? SheetCost5 { get; set; }
        public double SheetPrice5 { get; set; }
        public long? From6 { get; set; }
        public long? To6 { get; set; }
        public long? Sheets6 { get; set; }
        public double? SheetCost6 { get; set; }
        public double SheetPrice6 { get; set; }
        public long? From7 { get; set; }
        public long? To7 { get; set; }
        public long? Sheets7 { get; set; }
        public double? SheetCost7 { get; set; }
        public double SheetPrice7 { get; set; }
        public long? From8 { get; set; }
        public long? To8 { get; set; }
        public long? Sheets8 { get; set; }
        public double? SheetCost8 { get; set; }
        public double SheetPrice8 { get; set; }
        public long? From9 { get; set; }
        public long? To9 { get; set; }
        public long? Sheets9 { get; set; }
        public double? SheetCost9 { get; set; }
        public double SheetPrice9 { get; set; }
        public long? From10 { get; set; }
        public long? To10 { get; set; }
        public long? Sheets10 { get; set; }
        public double? SheetCost10 { get; set; }
        public double SheetPrice10 { get; set; }
        public long? From11 { get; set; }
        public long? To11 { get; set; }
        public long? Sheets11 { get; set; }
        public double? SheetCost11 { get; set; }
        public double SheetPrice11 { get; set; }
        public long? From12 { get; set; }
        public long? To12 { get; set; }
        public long? Sheets12 { get; set; }
        public double? SheetCost12 { get; set; }
        public double SheetPrice12 { get; set; }
        public long? From13 { get; set; }
        public long? To13 { get; set; }
        public long? Sheets13 { get; set; }
        public double? SheetCost13 { get; set; }
        public double SheetPrice13 { get; set; }
        public long? From14 { get; set; }
        public long? To14 { get; set; }
        public long? Sheets14 { get; set; }
        public double? SheetCost14 { get; set; }
        public double SheetPrice14 { get; set; }
        public long? From15 { get; set; }
        public long? To15 { get; set; }
        public long? Sheets15 { get; set; }
        public double? SheetCost15 { get; set; }
        public double SheetPrice15 { get; set; }
        public short? isaccumulativecharge { get; set; }
        public short? IsRoundUp { get; set; }
        public double? TimePerHour { get; set; }
        public bool? IsCostCenterZone { get; set; }
        public long? OrganisationId { get; set; }
        public string ZoneName { get; set; }
        public string VariableString { get; set; }

        public virtual LookupMethod LookupMethod { get; set; }
    }
}