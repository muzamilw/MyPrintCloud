using AutoMapper;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using MPC.Models.ResponseModels;
using MPC.Models.RequestModels;
using MPC.Models.Common;
using System.Web;

namespace MPC.Repository.Repositories
{
    public class ReportRepository : BaseRepository<Report>, IReportRepository
    {

        public ReportRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Report> DbSet
        {
            get
            {
                return db.Reports;
            }
        }
        public List<Report> GetReportsByOrganisationID(long OrganisationID)
        {
            try
            {
               

                return db.Reports.Where(o => o.OrganisationId == OrganisationID).ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public List<ReportCategory> GetReportCategories()
        {
            try
            {
                db.Configuration.LazyLoadingEnabled = false;
                List<ReportCategory> list = db.ReportCategories.ToList();
                List<Report> ReportList = db.Reports.Where(g => g.OrganisationId == OrganisationId).ToList();

                //foreach (var item in list)
                //{
                //    item.Reports = db.Reports.Where(g=>g.CategoryId==)
                //}

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ReportparamResponse> getParamsById(long Id)
        {
            string connectionString = string.Empty;
            SqlConnection oConn = new SqlConnection();

            List<Reportparam> Reportparams = db.Reportparams.Where(g => g.ReportId == Id).ToList();
            List<ReportparamResponse> ReportparamsList = new List<ReportparamResponse>();

            oConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ReportConnectiontring"].ConnectionString);

            oConn.Open();


            foreach (var item in Reportparams)
            {
                ReportparamResponse reportpar = new ReportparamResponse();
                reportpar.param = item;
                // for drop down
               if(item.ControlType == 1){
                  
                   string queryString = "select * from " + item.ComboTableName + " " + item.CriteriaFieldName;

                   SqlCommand command = new SqlCommand(queryString, oConn);
                   SqlDataReader reader = command.ExecuteReader();

                   DataTable dtrpt = new DataTable();

                   dtrpt.Load(reader);

                    DataColumnCollection columns = dtrpt.Columns;
                    DataColumn colId = dtrpt.Columns["YourColumnName"];
                    DataColumn colName = dtrpt.Columns["YourColumnName"];

                    if (columns.Contains("CompanyId")) // company records
                    {
                        reportpar.ComboList = new List<ReportparamComboCollection>();

                        foreach (DataRow row in dtrpt.Rows) // Loop over the rows.
                        {
                           ReportparamComboCollection objCombo = new ReportparamComboCollection();

                           objCombo.ComboId = row[colId].ToString();

                           objCombo.ComboText = row[colName].ToString();

                           reportpar.ComboList.Add(objCombo);
                        }
                    }
   
               }

                
            }
            return ReportparamsList;
        }
        public ReportCategory GetReportCategory(long CategoryId, int IsExternal)
        {
            try
            {
                db.Configuration.LazyLoadingEnabled = false;
                ReportCategory oReportCategory = db.ReportCategories.Where(g => g.CategoryId == CategoryId).SingleOrDefault();
                List<Report> ReportList = db.Reports.Where(g => (g.OrganisationId == OrganisationId || g.OrganisationId == null) && g.IsExternal == IsExternal).ToList();

              

                return oReportCategory;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ReportNote> GetReportNotesByOrganisationID(long OrganisationID)
        {
            try
            {
               

                return db.ReportNotes.Where(c => c.OrganisationId == OrganisationID).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Report GetReportByReportID(long iReportID)
        {
            try
            {


                return db.Reports.Where(c => c.ReportId == iReportID).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<usp_JobCardReport_Result> getJobCardReportResult(long OrganisationID,long OrderID,long ItemID)
        {
            try
            {
                return db.usp_JobCardReport(OrganisationID, OrderID, ItemID).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<usp_OrderReport_Result> getOrderReportResult(long OrganisationID, long OrderID)
        {
            try
            {
                return db.usp_OrderReport(OrganisationID, OrderID).ToList();
               // return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<usp_EstimateReport_Result> getEstimateReportResult(long OrganisationID, long EstimateID)
        {
            try
            {
                return db.usp_EstimateReport(OrganisationID, EstimateID).ToList();
                

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<usp_InvoiceReport_Result> getInvoiceReportResult(long OrganisationID, long InvoiceID)
        {
            try
            {
                return db.usp_InvoiceReport(OrganisationID, InvoiceID).ToList();
               

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       

        public DataTable GetReportDataSourceByReportID(long ReportID, string CriteriaParam)
        {
            string connectionString = string.Empty;
            SqlConnection oConn = new SqlConnection();
            //if (System.Web.HttpContext.Current.Request.Url.Authority == "mpc" || System.Web.HttpContext.Current.Request.Url.Authority == "localhost")
            //{
            //    connectionString = "Persist Security Info=False;Integrated Security=false;Initial Catalog=MPCLive;server=192.168.1.22; user id=sa; password=p@ssw0rd;";
            //   // connectionString = "Persist Security Info=False;Integrated Security=false;Initial Catalog=MPCLive;server=www.myprintcloud.com,9998; user id=mpcmissa; password=p@ssw0rd@mis2o14;";
            //    oConn = new SqlConnection(connectionString);   
            //}
            //else
            //{
            oConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ReportConnectiontring"].ConnectionString);
            //}
             
          
            oConn.Open();
            try
            {
              
                
               // string connectionString = "Persist Security Info=False;Integrated Security=false;Initial Catalog=MPCLive;server=www.myprintcloud.com,9998; user id=mpcmissa; password=p@ssw0rd@mis2o14;";
                string ReportDataSource = string.Empty;
                string ReportTemplate = string.Empty;
                Report report = GetReportByReportID(ReportID);
                if(report != null)
                {
                    if(string.IsNullOrEmpty(CriteriaParam))
                    {

                      
                        string queryString = "select " + report.ReportDataSource  + OrganisationId;

                        SqlCommand command = new SqlCommand(queryString, oConn);
                        SqlDataReader reader = command.ExecuteReader();

                        DataTable dtrpt = new DataTable();

                        dtrpt.Load(reader);

                        return dtrpt;
                     //   System.Data.Entity.Infrastructure.DbRawSqlQuery<Company> result = db.Database.SqlQuery<Company>("select " + report.ReportDataSource);

                      //  var oresult = db.Database.ExecuteSqlCommand("select " + report.ReportDataSource);
                        //foreach(v i in oresult)
                        //{
                              
                        //}
                        //return oresult.;
                       // return oResult;
                       // return null;
                    }
                    else
                    {
                        if (report.ReportDataSource.Contains("where") && CriteriaParam.Contains("where"))
                        {
                            CriteriaParam = CriteriaParam.Replace("where", " and ");
                        }

                        string queryString = "select " + report.ReportDataSource + CriteriaParam + "and cOrganisationId = " + OrganisationId;

                        SqlCommand command = new SqlCommand(queryString, oConn);
                        SqlDataReader reader = command.ExecuteReader();

                        DataTable dtrpt = new DataTable();

                        dtrpt.Load(reader);

                        return dtrpt;
                      

                    }

                    
                }
                else
                {
                    return null;
                }
               // var oResult = null;
                //System.Data.Objects.ObjectResult<string> result = db.ExecuteStoreQuery<string>("select top 1 cast(" + feildname + " as varchar(1000)) from " + tblname + " where " + keyName + "= " + keyValue + "", "");
               
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oConn.Close();
            }
        }




        public List<ReportNote> GetReportNoteByCompanyId(long CompanyId)
        {
            List<ReportNote> reportNotes = new List<ReportNote>();
            reportNotes = db.ReportNotes.Where(c => c.CompanyId == CompanyId && c.ReportCategoryId != (int)ReportCategoryEnum.PurchaseOrders).ToList();

            
            //long CompanyId = db.Companies.Where(c => )

            if (reportNotes == null || reportNotes.Count() == 0)
            {
                reportNotes = CreateDummyReportNotesRecord(CompanyId);
            }

            ReportNote rptNote = db.ReportNotes.Where(c => c.ReportCategoryId == (int)ReportCategoryEnum.PurchaseOrders && c.OrganisationId == OrganisationId && c.CompanyId == null).FirstOrDefault();
            if (rptNote == null)
            {
                string PathFull = "http://" + HttpContext.Current.Request.Url.Host + "/";

                // for purchases
                ReportNote objReportNotePurchase = new ReportNote();


                objReportNotePurchase.isDefault = true;
                objReportNotePurchase.OrganisationId = OrganisationId;
                // objReportNotePurchase.ReportBanner = "MPC_Content/Reports/Banners/Report-Banner.png";
                objReportNotePurchase.BannerAbsolutePath = PathFull;
                objReportNotePurchase.ReportCategoryId = 5;
                objReportNotePurchase.SystemSiteId = 1;
                objReportNotePurchase.UserId = 1;
                db.ReportNotes.Add(objReportNotePurchase);


                db.SaveChanges();
                reportNotes.Add(objReportNotePurchase);
            }
            else
            {
                reportNotes.Add(rptNote);
            }

            return reportNotes;
        }
        public void UpdateReportNotes(List<ReportNote> reportNotes)
        {
        
            if (reportNotes != null && reportNotes.Count > 0)
            {
               foreach(var rpt in reportNotes)
               {
                   ReportNote reportNote = db.ReportNotes.Where(c => c.Id == rpt.Id).FirstOrDefault();
                   if(reportNote != null)
                   {
                       if (!string.IsNullOrEmpty(rpt.ReportBanner))
                         reportNote.ReportBanner = rpt.ReportBanner;
                   }
               }
               db.SaveChanges();
            }

           

        }

        public List<ReportNote> CreateDummyReportNotesRecord(long CompanyId)
        {
            List<ReportNote> lstReportNotes = new List<ReportNote>();

            string PathFull = "http://" + HttpContext.Current.Request.Url.Host + "/";


            ReportNote objReportNoteEstimate = new ReportNote();

            objReportNoteEstimate.CompanyId = CompanyId;
            objReportNoteEstimate.isDefault = true;
            objReportNoteEstimate.OrganisationId = OrganisationId;
            //objReportNoteEstimate.ReportBanner = "MPC_Content/Reports/Banners/Report-Banner.png";
            objReportNoteEstimate.BannerAbsolutePath = PathFull;
            objReportNoteEstimate.ReportCategoryId = 3;
            objReportNoteEstimate.SystemSiteId = 1;
            objReportNoteEstimate.UserId = 1;
            
            db.ReportNotes.Add(objReportNoteEstimate);


            ReportNote objReportNoteOrder = new ReportNote();

            objReportNoteOrder.CompanyId = CompanyId;
            objReportNoteOrder.isDefault = true;
            objReportNoteOrder.OrganisationId = OrganisationId;
           // objReportNoteOrder.ReportBanner = "MPC_Content/Reports/Banners/Report-Banner.png";
            objReportNoteOrder.BannerAbsolutePath = PathFull;
            objReportNoteOrder.ReportCategoryId = 12;
            objReportNoteOrder.SystemSiteId = 1;
            objReportNoteOrder.UserId = 1;
            db.ReportNotes.Add(objReportNoteOrder);

            ReportNote objReportNoteInvoice = new ReportNote();

            objReportNoteInvoice.CompanyId = CompanyId;
            objReportNoteInvoice.isDefault = true;
            objReportNoteInvoice.OrganisationId = OrganisationId;
            //objReportNoteInvoice.ReportBanner = "MPC_Content/Reports/Banners/Report-Banner.png";
            objReportNoteInvoice.BannerAbsolutePath = PathFull;
            objReportNoteInvoice.ReportCategoryId = 13;
            objReportNoteInvoice.SystemSiteId = 1;
            objReportNoteInvoice.UserId = 1;
            db.ReportNotes.Add(objReportNoteInvoice);




            // for delivery
            ReportNote objReportNoteDelivery = new ReportNote();

            objReportNoteDelivery.CompanyId = CompanyId;
            objReportNoteDelivery.isDefault = true;
            objReportNoteDelivery.OrganisationId = OrganisationId;
            //objReportNoteDelivery.ReportBanner = "MPC_Content/Reports/Banners/Report-Banner.png";
            objReportNoteDelivery.BannerAbsolutePath = PathFull;
            objReportNoteDelivery.ReportCategoryId = 6;
            objReportNoteDelivery.SystemSiteId = 1;
            objReportNoteDelivery.UserId = 1;
            db.ReportNotes.Add(objReportNoteDelivery);




            db.SaveChanges();
            lstReportNotes.Add(objReportNoteEstimate);
            lstReportNotes.Add(objReportNoteOrder);
            lstReportNotes.Add(objReportNoteInvoice);
            //lstReportNotes.Add(objReportNotePurchase);
            lstReportNotes.Add(objReportNoteDelivery);

            return lstReportNotes;  
        }


        /// <summary>
        /// SP for PO Report
        /// </summary>
        public List<usp_PurchaseOrderReport_Result> GetPOReport(long PurchaseId)
        {
            try
            {
               return db.usp_PurchaseOrderReport(OrganisationId, PurchaseId).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public List<usp_DeliveryReport_Result> GetDeliveryNoteReport(long deliveryId)
        {
            try
            {
                return db.usp_DeliveryReport(OrganisationId, deliveryId).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ReportEmailResponseModel GetReportEmailBaseData(ReportEmailRequestModel request, string Path)
        {
            try
            {


                string To = string.Empty;
                string cc = string.Empty;
                string subject = string.Empty;
                string signature = string.Empty;

                
                int reportCategory = db.Reports.Where(c => c.ReportId == request.Reportid).Select(c => c.CategoryId).FirstOrDefault();


                SystemUser systemUser = db.SystemUsers.Where(c => c.SystemUserId == request.SignedBy).FirstOrDefault();


                string Email = db.CompanyContacts.Where(x => x.ContactId == request.ContactId).Select(c => c.Email).FirstOrDefault();

                if (reportCategory == (int)ReportCategoryEnum.Invoice)
                {
                    Invoice inv = db.Invoices.Where(c => c.InvoiceId == request.RecordId).FirstOrDefault();
                    
                    subject = inv.InvoiceName + " " + inv.Company.Name + " " + inv.InvoiceCode;
                }
                else if (reportCategory == (int)ReportCategoryEnum.Delivery)
                {
                    DeliveryNote delivery = db.DeliveryNotes.Where(c => c.DeliveryNoteId == request.RecordId).FirstOrDefault();

                    subject =  delivery.Company.Name + " " + delivery.Code;
                }
                else if (reportCategory == (int)ReportCategoryEnum.PurchaseOrders)
                {
                    Purchase purchase = db.Purchases.Where(c => c.PurchaseId == request.RecordId).FirstOrDefault();

                    subject =  purchase.Company.Name + " " + purchase.Code;
                }
                else
                {
                    Estimate order = db.Estimates.Where(c => c.EstimateId == request.RecordId).FirstOrDefault();
                   
                    subject = order.Estimate_Name + " " + order.Company.Name + " " + order.Order_Code;
                }

                To = Email;

              
            
                return new ReportEmailResponseModel
                {
                    Attachment = subject + ".pdf",
                    AttachmentPath = Path,
                    Subject = subject,
                    To = To
                };

             

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
       // GetReportsByOrganisationID
    }
}
