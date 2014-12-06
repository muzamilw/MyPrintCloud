using System;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    /// <summary>
    /// Prefix Repository
    /// </summary>
    public class PrefixRepository : BaseRepository<Prefix>, IPrefixRepository
    {
        #region privte
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public PrefixRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Prefix> DbSet
        {
            get
            {
                return db.Prefixes;
            }
        }

        #endregion

        #region public

        /// <summary>
        /// Returns Next Item Code Prefix and increments the NextItem Value by 1
        /// </summary>
        public string GetNextItemCodePrefix()
        {
            Prefix prefix = DbSet.FirstOrDefault(pfx => pfx.OrganisationId == OrganisationId);
            if (prefix == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, LanguageResources.NoPrefixDefined, OrganisationId));
            }

            string nextPrefix = prefix.ItemPrefix + "-001-" + prefix.ItemNext;

            // Update Item Next
            prefix.ItemNext += 1;

            // Save Changes
            SaveChanges();

            return nextPrefix;
        }

        #endregion


        
    }
}
