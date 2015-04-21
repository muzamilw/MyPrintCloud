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
        public ReportCategory GetReportCategory(long CategoryId)
        {
            try
            {
                db.Configuration.LazyLoadingEnabled = false;
                ReportCategory oReportCategory = db.ReportCategories.Where(g => g.CategoryId == CategoryId).SingleOrDefault();
                List<Report> ReportList = db.Reports.Where(g => g.OrganisationId == OrganisationId).ToList();

              

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

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       // GetReportsByOrganisationID
    }
}
