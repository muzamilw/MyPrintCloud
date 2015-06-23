using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.ResponseModels
{
    public class SmartFormWebstoreResponse
    {
        public long SmartFormId { get; set; }
        public string Name { get; set; }
        public string Heading { get; set; }
        public long? CompanyId { get; set; }
        public long? OrganisationId { get; set; }
    }
}
