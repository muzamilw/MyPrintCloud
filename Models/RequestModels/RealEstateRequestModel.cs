using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.RequestModels
{
    public class RealEstateRequestModel : GetPagedListRequest
    {
        public string SearchString { get; set; }
        public long CompanyId { get; set; }
    }
}
