using System.Collections.Generic;

namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Report Category Domain Model
    /// </summary>
    public class ReportCategory
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<ReportNote> ReportNotes { get; set; }
    }
}
