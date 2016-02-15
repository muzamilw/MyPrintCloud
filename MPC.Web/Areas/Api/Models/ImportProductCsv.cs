using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public class ImportProductCsv
    {
        public string FileName { get; set; }
        public string FileBytes { get; set; }
        public long CompanyId { get; set; }
    }
}