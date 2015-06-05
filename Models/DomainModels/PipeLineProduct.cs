using System.Collections.Generic;

namespace MPC.Models.DomainModels
{
    /// <summary>
    /// PipeLine Product Domain Model
    /// </summary>
    public class PipeLineProduct
    {
        public int ProductId { get; set; }
        public string Description { get; set; }
        public virtual ICollection<InquiryItem> InquiryItems { get; set; } 
    }
}
