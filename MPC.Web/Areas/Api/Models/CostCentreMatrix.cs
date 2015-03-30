using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public class CostCentreMatrix
    {
        public int MatrixId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RowsCount { get; set; }
        public int ColumnsCount { get; set; }
        public int OrganisationId { get; set; }
        public int SystemSiteId { get; set; }
        public string VariableString { get; set; }
    }
}