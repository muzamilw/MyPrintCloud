using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.DomainModels;

namespace MPC.Models.RequestModels
{
    public class ReportNoteRequestModel
    {
        public IEnumerable<ReportNote> ReportsBanners { get; set; }
    }
}
