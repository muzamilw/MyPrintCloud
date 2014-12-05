using System;

namespace MPC.Models.DomainModels
{
    public class CompanyCMYKColor
    {
        public long ColorId { get; set; }
        public Nullable<long> CompanyId { get; set; }
        public string ColorName { get; set; }
        public string ColorC { get; set; }
        public string ColorM { get; set; }
        public string ColorY { get; set; }
        public string ColorK { get; set; }
        public virtual Company Company{ get; set; }
    }
}
