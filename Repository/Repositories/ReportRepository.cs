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
       // GetReportsByOrganisationID
    }
}
