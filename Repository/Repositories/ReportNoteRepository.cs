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
    public class ReportNoteRepository : BaseRepository<ReportNote>, IReportNoteRepository
    {
        public ReportNoteRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<ReportNote> DbSet
        {
            get
            {
                return db.ReportNotes;
            }
        }

        public List<ReportNote> GetReportNotesByCompanyId(long CompanyId)
        {
            try
            {
                return db.ReportNotes.Where(c => c.CompanyId == CompanyId).ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
