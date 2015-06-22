using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    /// <summary>
    /// Section Repository
    /// </summary>
    public class SectionRepository : BaseRepository<Section>, ISectionRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public SectionRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Section> DbSet
        {
            get
            {
                return db.Sections;
            }
        }

        #endregion

        #region Public
        /// <summary>
        /// Get All Sections
        /// </summary>
        public override IEnumerable<Section> GetAll()
        {
            return DbSet.ToList();
        }

        /// <summary>
        /// Get All Sections For Phrase Library
        /// </summary>
        public IEnumerable<Section> GetSectionsForPhraseLibrary()
        {
            return DbSet.OrderBy(s => s.SecOrder).ToList();
        }

        

        /// <summary>
        /// Get Sections By Parent Id
        /// </summary>
        public IEnumerable<Section> GetSectionsByParentId(long parentId)
        {
            return DbSet.Where(s => s.ParentId == parentId).OrderBy(s => s.SecOrder).ToList();
        }

        /// <summary>
        /// Get Campaign Sections 
        /// </summary>
        public IEnumerable<Section> GetCampaignSections()
        {
            return DbSet.Where(s => s.CampaignEmailVariables.Any(c => c.SectionId == s.SectionId)).OrderBy(s => s.SecOrder).ToList();
        }

        #endregion
    }
}
