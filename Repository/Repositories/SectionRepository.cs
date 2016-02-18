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
            // Estimate=1, Invoices=13, Purchases=7,Delivery=10, Job Production=4]
            IEnumerable<Section> sections = null;
            sections = DbSet.Where(s => s.SectionId == 1 || s.SectionId == 13 || s.SectionId == 7 || s.SectionId == 10 || s.SectionId == 4).OrderBy(s => s.SectionName).ToList();
            if(sections != null && sections.Count() > 0)
            {
                foreach(var sec in sections)
                {


                    sec.PhraseFields = sec.PhraseFields.Where(c => c.OrganisationId == OrganisationId).ToList();
                  
                }

               //sections = sections.ToList().ForEach(c => c.PhraseFields.Where(p => p.OrganisationId == OrganisationId);
            }
            return sections;
        }



        /// <summary>
        /// Get Sections By Parent Id
        /// </summary>
        public IEnumerable<Section> GetSectionsByParentId(long parentId)
        {
            // to get sections which are using
            List<int> SectionIds = new List<int>();
           
            SectionIds.Add(1);
            SectionIds.Add(3);
            SectionIds.Add(4);
            SectionIds.Add(7);
            SectionIds.Add(10);
            SectionIds.Add(13);
            SectionIds.Add(17);
            SectionIds.Add(20);
            SectionIds.Add(23);
            SectionIds.Add(43);
             SectionIds.Add(54);
             SectionIds.Add(57);
             SectionIds.Add(58);

            return DbSet.Where(s => s.ParentId == parentId && SectionIds.Contains(s.SectionId)).OrderBy(s => s.SectionName).ToList();
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
