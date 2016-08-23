using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;
using System;

namespace MPC.Repository.Repositories
{
    /// <summary>
    /// Markup Repository
    /// </summary>
    public class MarkupRepository : BaseRepository<Markup>, IMarkupRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public MarkupRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Markup> DbSet
        {
            get
            {
                return db.Markups;
            }
        }

        #endregion

        #region Public
        /// <summary>
        /// Get All MarkUp for User Domain Key
        /// </summary>
        public override IEnumerable<Markup> GetAll()
        {
            return DbSet.Where(markup => markup.OrganisationId == OrganisationId).ToList();
        }

        public Markup GetZeroMarkup()
        {
            return db.Markups.FirstOrDefault(c => c.MarkUpRate.Value == 0);
        }

        public List<Markup> GetMarkupsByOrganisationId(long OID)
        {
            try
            {
                db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;
                return db.Markups.Where(c => c.OrganisationId == OID).ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public string GetMarkupNamebyID(long ID)
        {
            return db.Markups.Where(m => m.MarkUpId == ID).Select(e => e.MarkUpName).FirstOrDefault();
        }

        public List<Markup> GetMarkups()
        {
           return db.Markups.Where(c => c.OrganisationId == OrganisationId).ToList();
        }

        public Markup GetDefaultMarkupsByOrganisationId(long OID)
        {
            try
            {
                db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;
                return db.Markups.Where(m => m.OrganisationId == OrganisationId && m.IsDefault == true).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long GetOrganisationDefaultMarkupId()
        {
            var markup = DbSet.FirstOrDefault(m => m.OrganisationId == OrganisationId && m.IsDefault == true);
            return markup != null ? markup.MarkUpId : 0;
        }

        #endregion
    }
}
