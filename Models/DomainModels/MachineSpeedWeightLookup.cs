
namespace MPC.Models.DomainModels
{
    public class MachineSpeedWeightLookup
    {
        public long Id { get; set; }
        public long? MethodId { get; set; }
        public long? SheetsQty1 { get; set; }
        public long? SheetsQty2 { get; set; }
        public long? SheetsQty3 { get; set; }
        public long? SheetsQty4 { get; set; }
        public long? SheetsQty5 { get; set; }
        public long? SheetWeight1 { get; set; }
        public long? speedqty11 { get; set; }
        public long? speedqty12 { get; set; }
        public long? speedqty13 { get; set; }
        public long? speedqty14 { get; set; }
        public long? speedqty15 { get; set; }
        public long? SheetWeight2 { get; set; }
        public long? speedqty21 { get; set; }
        public long? speedqty22 { get; set; }
        public long? speedqty23 { get; set; }
        public long? speedqty24 { get; set; }
        public long? speedqty25 { get; set; }
        public long? SheetWeight3 { get; set; }
        public long? speedqty31 { get; set; }
        public long? speedqty32 { get; set; }
        public long? speedqty33 { get; set; }
        public long? speedqty34 { get; set; }
        public long? speedqty35 { get; set; }
        public double? hourlyCost { get; set; }
        public double hourlyPrice { get; set; }
        public virtual LookupMethod LookupMethod { get; set; }
    }
}
