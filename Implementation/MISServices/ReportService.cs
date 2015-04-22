using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Implementation.MISServices
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _IReportRepository;
        public ReportService(IReportRepository IReportRepository)
        {
            this._IReportRepository = IReportRepository;
        }

        public ReportCategory GetReportCategory(long CategoryId)
        {
            return _IReportRepository.GetReportCategory(CategoryId);
        }
        public List<ReportCategory> GetReportCategories()
        {
            return _IReportRepository.GetReportCategories();
        }

    }
}
