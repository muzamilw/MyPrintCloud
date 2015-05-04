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
                return db.usp_JobCardReport(OrganisationId, OrderID, ItemID).ToList();

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
                return db.usp_OrderReport(OrganisationId, OrderID).ToList();
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
                return db.usp_EstimateReport(OrganisationId, EstimateID).ToList();
                

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
               return db.usp_InvoiceReport(OrganisationId, InvoiceID).ToList();
               

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
            if (System.Web.HttpContext.Current.Request.Url.Authority == "mpc" || System.Web.HttpContext.Current.Request.Url.Authority == "localhost")
            {
                connectionString = "Persist Security Info=False;Integrated Security=false;Initial Catalog=MPCLive;server=192.168.1.22; user id=sa; password=p@ssw0rd;";
                oConn = new SqlConnection(connectionString);   
            }
            else
            {
                oConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["BaseDbContext"].ConnectionString);
            }
             
          
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
            return db.ReportNotes.Where(c => c.CompanyId == CompanyId).ToList();
        }
       // GetReportsByOrganisationID
    }
}
