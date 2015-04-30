using GrapeCity.ActiveReports;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.Common;
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
        private readonly IOrganisationRepository organisationRepository;
        private readonly IReportRepository ReportRepository;
        public ReportService(IReportRepository IReportRepository, IOrganisationRepository organisationRepository, IReportRepository ReportRepository)
        {
            if (IReportRepository == null)
            {
                throw new ArgumentNullException("IReportRepository");
            }
            if (organisationRepository == null)
            {
                throw new ArgumentNullException("organisationRepository");
            }
            if (ReportRepository == null)
            {
                throw new ArgumentNullException("ReportRepository");
            }
            this._IReportRepository = IReportRepository;
            this.organisationRepository = organisationRepository;
            this.ReportRepository = ReportRepository;
        }

        public ReportCategory GetReportCategory(long CategoryId, int IsExternal)
        {
            return _IReportRepository.GetReportCategory(CategoryId, IsExternal);
        }
        public List<ReportCategory> GetReportCategories()
        {
            return _IReportRepository.GetReportCategories();
        }
        public SectionReport GetReport(int iReportID, long itemid)
        {
            //, long iRecordID, ReportType type, long OrderID
            string sFilePath = string.Empty;
            try
            {
                long OrganisationID = 0;
                Organisation org = organisationRepository.GetOrganizatiobByID();
                if (org != null)
                {
                    OrganisationID = org.OrganisationId;
                }
                Report currentReport = ReportRepository.GetReportByReportID(iReportID);
                SectionReport currReport = new SectionReport();
                if (currentReport.ReportId > 0)
                {
                    byte[] rptBytes = null;
                    rptBytes = System.Text.Encoding.Unicode.GetBytes(currentReport.ReportTemplate);
                 
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(rptBytes);
                 
                    ms.Position = 0;

                 
                    currReport.LoadLayout(ms);

                    if (itemid > 0)
                    {
                        currReport.DataSource = ReportRepository.getOrderReportResult(OrganisationID, itemid);
                    }
                    else
                    {
                        currReport.DataSource = ReportRepository.GetReportDataSourceByReportID(iReportID, "");
                    }

                  
                    //DataTable dataSourceList = ReportRepository.GetReportDataSourceByReportID(iReportID, CriteriaParam);
                    //currReport.DataSource = dataSourceList;

                     // List<usp_OrderReport_Result> rptOrderSource = ReportRepository.getOrderReportResult(OrganisationID, 0);
                       
                    


                }
                return currReport;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        //public List<usp_OrderReport_Resuilt> GetOrderReportSource()
        //{

        //}

    }
}
