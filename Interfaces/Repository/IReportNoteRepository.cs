using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.Repository
{
    public interface IReportNoteRepository : IBaseRepository<ReportNote, long>
    {
        List<ReportNote> GetReportNotesByCompanyId(long CompanyId);
    }
}
