using GrapeCity.ActiveReports;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MPC.Implementation.MISServices
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _IReportRepository;
        private readonly IOrganisationRepository organisationRepository;
        private readonly IReportRepository ReportRepository;
        private readonly ICompanyRepository CompanyRepository;
        private readonly IExportReportHelper ExportReportHelper;
        public ReportService(IReportRepository IReportRepository, IOrganisationRepository organisationRepository, IReportRepository ReportRepository, ICompanyRepository CompanyRepository, IExportReportHelper ExportReportHelper)
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
            this.ExportReportHelper = ExportReportHelper;
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

        public IEnumerable<ReportNote> Update(IEnumerable<ReportNote> reportNotes)
        {
          
            List<ReportNote> notes = new List<ReportNote>();
            foreach(var rptNote in reportNotes)
            {
            
                rptNote.ReportBanner = SaveReportNoteImage(rptNote);
                notes.Add(rptNote);
            }
            ReportRepository.UpdateReportNotes(notes);

           // costcenter.ThumbnailImageURL = ReportRepository.UPdate
            //_costCenterRepository.Update(costcenter);
            //SaveCostCentre(costcenter, org.OrganisationId, sOrgName, false);
            return reportNotes;
        }

        private string SaveReportNoteImage(ReportNote rptNote)
        {
            if (rptNote.EstimateBannerBytes != null)
            {
                string base64 = rptNote.EstimateBannerBytes.Substring(rptNote.EstimateBannerBytes.IndexOf(',') + 1);
                base64 = base64.Trim('\0');
                byte[] data = Convert.FromBase64String(base64);

                string directoryPath = HttpContext.Current.Server.MapPath("~/MPC_Content/Reports/Banners/" + organisationRepository.OrganisationId + "/" + rptNote.CompanyId);

                if (directoryPath != null && !Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                string savePath = directoryPath + "\\Estimate.png";
                File.WriteAllBytes(savePath, data);
                int indexOf = savePath.LastIndexOf("MPC_Content", StringComparison.Ordinal);
                savePath = savePath.Substring(indexOf, savePath.Length - indexOf);
                return savePath;
            }
            if (rptNote.OrderBannerBytes != null)
            {
                string base64 = rptNote.OrderBannerBytes.Substring(rptNote.OrderBannerBytes.IndexOf(',') + 1);
                base64 = base64.Trim('\0');
                byte[] data = Convert.FromBase64String(base64);

                string directoryPath = HttpContext.Current.Server.MapPath("~/MPC_Content/Reports/Banners/" + organisationRepository.OrganisationId + "/" + rptNote.CompanyId);

                if (directoryPath != null && !Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                string savePath = directoryPath + "\\Order.png";
                File.WriteAllBytes(savePath, data);
                int indexOf = savePath.LastIndexOf("MPC_Content", StringComparison.Ordinal);
                savePath = savePath.Substring(indexOf, savePath.Length - indexOf);
                return savePath;
            }
            if (rptNote.InvoiceBannerBytes != null)
            {
                string base64 = rptNote.InvoiceBannerBytes.Substring(rptNote.InvoiceBannerBytes.IndexOf(',') + 1);
                base64 = base64.Trim('\0');
                byte[] data = Convert.FromBase64String(base64);

                string directoryPath = HttpContext.Current.Server.MapPath("~/MPC_Content/Reports/Banners/" + organisationRepository.OrganisationId + "/" + rptNote.CompanyId);

                if (directoryPath != null && !Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                string savePath = directoryPath + "\\Invoice.png";
                File.WriteAllBytes(savePath, data);
                int indexOf = savePath.LastIndexOf("MPC_Content", StringComparison.Ordinal);
                savePath = savePath.Substring(indexOf, savePath.Length - indexOf);
                return savePath;
            }
            if (rptNote.PurchaseBannerBytes != null)
            {
                string base64 = rptNote.PurchaseBannerBytes.Substring(rptNote.PurchaseBannerBytes.IndexOf(',') + 1);
                base64 = base64.Trim('\0');
                byte[] data = Convert.FromBase64String(base64);

                string directoryPath = HttpContext.Current.Server.MapPath("~/MPC_Content/Reports/Banners/" + organisationRepository.OrganisationId + "/" + rptNote.CompanyId);

                if (directoryPath != null && !Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                string savePath = directoryPath + "\\Purchase.png";
                File.WriteAllBytes(savePath, data);
                int indexOf = savePath.LastIndexOf("MPC_Content", StringComparison.Ordinal);
                savePath = savePath.Substring(indexOf, savePath.Length - indexOf);
                return savePath;
            }
            if (rptNote.DeliveryBannerBytes != null)
            {
                string base64 = rptNote.DeliveryBannerBytes.Substring(rptNote.DeliveryBannerBytes.IndexOf(',') + 1);
                base64 = base64.Trim('\0');
                byte[] data = Convert.FromBase64String(base64);

                string directoryPath = HttpContext.Current.Server.MapPath("~/MPC_Content/Reports/Banners/" + organisationRepository.OrganisationId + "/" + rptNote.CompanyId);

                if (directoryPath != null && !Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                string savePath = directoryPath + "\\Delivery.png";
                File.WriteAllBytes(savePath, data);
                int indexOf = savePath.LastIndexOf("MPC_Content", StringComparison.Ordinal);
                savePath = savePath.Substring(indexOf, savePath.Length - indexOf);
                return savePath;
            }
            return null;
        }
        public List<ReportparamResponse> getParamsById(long Id)
        {
            return new List<ReportparamResponse>();
        }

        public ReportEmailResponseModel GetReportEmailBaseData(ReportEmailRequestModel request)
        {
            //int type = 0;
            //if(type == ReportType.Internal)
            //    type

          string Path =  ExportReportHelper.ExportPDF((int)request.Reportid, request.RecordId, request.ReportType, request.OrderId, request.CriteriaParam);

          return ReportRepository.GetReportEmailBaseData(request, Path);

        }
    }
}
