using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    [Serializable()]
    /// <summary>
    /// Cost Centre Question Domain Model
    /// </summary>
    public partial class CostCentreQuestion
    {
        public int Id { get; set; }
        public string QuestionString { get; set; }
        public short? Type { get; set; }
        public string DefaultAnswer { get; set; }
        public int CompanyId { get; set; }
        public int SystemSiteId { get; set; }
    }
}
