using GrapeCity.ActiveReports;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.ResponseModels;
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
        private readonly ICompanyRepository CompanyRepository;
        public ReportService(IReportRepository IReportRepository, IOrganisationRepository organisationRepository, IReportRepository ReportRepository, ICompanyRepository CompanyRepository)
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
            if (CompanyRepository == null)
            {
                throw new ArgumentNullException("CompanyRepository");
            }
            this._IReportRepository = IReportRepository;
            this.organisationRepository = organisationRepository;
            this.ReportRepository = ReportRepository;
            this.CompanyRepository = CompanyRepository;
        }

        public ReportCategory GetReportCategory(long CategoryId)
        {
            return _IReportRepository.GetReportCategory(CategoryId);
        }
        public List<ReportCategory> GetReportCategories()
        {
            return _IReportRepository.GetReportCategories();
        }
        public SectionReport GetReport(int iReportID)
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
                  

                        List<usp_OrderReport_Result> rptOrderSource = ReportRepository.getOrderReportResult(OrganisationID, 46847);
                        currReport.DataSource = rptOrderSource;
                    


                }
                return currReport;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

       
        public List<StoresListResponse> GetStoreNameByOrganisationId()
        {
            return CompanyRepository.GetStoresNameByOrganisationId();
        }

        public List<ReportNote> GetReportNoteByCompanyID(long CompanyID)
        {
            return ReportRepository.GetReportNoteByCompanyId(CompanyID); 
        }

        //public ReportNote Update(ReportNote reportNote)
        //{
        //    //Organisation org = _organisationRepository.GetOrganizatiobByID();
        //  //  string sOrgName = org.OrganisationName.Replace(" ", "").Trim();
        //    //reportNote.BannerAbsolutePath = SaveCostCenterImage(costcenter);
        //    //_costCenterRepository.Update(costcenter);
        //    //SaveCostCentre(costcenter, org.OrganisationId, sOrgName, false);
        //    //return costcenter;
        //}

        //private string SaveReportNoteImage(ReportNote reportNote)
        //{
        //    if (reportNote != null)
        //    {
        //        string base64 = costcenter.ImageBytes.Substring(costcenter.ImageBytes.IndexOf(',') + 1);
        //        base64 = base64.Trim('\0');
        //        byte[] data = Convert.FromBase64String(base64);

        //        string directoryPath = HttpContext.Current.Server.MapPath("~/MPC_Content/CostCentres/" + _costCenterRepository.OrganisationId + "/" + costcenter.CostCentreId);

        //        if (directoryPath != null && !Directory.Exists(directoryPath))
        //        {
        //            Directory.CreateDirectory(directoryPath);
        //        }
        //        string savePath = directoryPath + "\\thumbnail.png";
        //        File.WriteAllBytes(savePath, data);
        //        int indexOf = savePath.LastIndexOf("MPC_Content", StringComparison.Ordinal);
        //        savePath = savePath.Substring(indexOf, savePath.Length - indexOf);
        //        return savePath;
        //    }
        //    return null;
        //}
        //public List<usp_OrderReport_Result> GetOrderReportSource()
        //{

        //}

    }
}
