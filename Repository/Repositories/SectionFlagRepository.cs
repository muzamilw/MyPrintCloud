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
    /// Section Flag Repository
    /// </summary>
    public class SectionFlagRepository : BaseRepository<SectionFlag>, ISectionFlagRepository
    {

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public SectionFlagRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<SectionFlag> DbSet
        {
            get
            {
                return db.SectionFlags;
            }
        }

        #endregion

        #region Public
        /// <summary>
        /// Get All Section Flags
        /// </summary>
        public override IEnumerable<SectionFlag> GetAll()
        {
            return DbSet.ToList();
        }

        /// <summary>
        /// Get Section Flag For Inventory
        /// </summary>
        public IEnumerable<SectionFlag> GetSectionFlagForInventory()
        {
            return DbSet.Where(sf => sf.SectionId == sf.Section.SectionId && sf.Section.SectionName == "Inventory").ToList();
        }

        /// <summary>
        /// Get Section Flag By Section Id
        /// </summary>
        public IEnumerable<SectionFlag> GetSectionFlagBySectionId(long SectionId)
        {
            return DbSet.Where(sf => sf.SectionId == SectionId).ToList();
        }

        /// <summary>
        /// Get Defualt Section Flag for Price Matrix in webstore
        /// </summary>
        public int GetDefaultSectionFlagId()
        {
            return DbSet.Where(sf => sf.SectionId == 81 && sf.isDefault == true).Select(id => id.SectionFlagId).FirstOrDefault();
        }

      
        #endregion
    }
}
