using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;
using System;
using AutoMapper;

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
           

            return DbSet.Where(sf => sf.SectionId == (int)SectionEnum.Inventory && sf.OrganisationId == OrganisationId).ToList();
        }

        /// <summary>
        /// Get Section Flag By Section Id
        /// </summary>
        public IEnumerable<SectionFlag> GetSectionFlagBySectionId(long sectionId)
        {
            return DbSet.Where(sf => sf.SectionId == sectionId && sf.OrganisationId == OrganisationId).OrderBy(s => s.FlagName).ToList();
        }

        /// <summary>
        /// Get Defualt Section Flag for Price Matrix in webstore
        /// </summary>
        public int GetDefaultSectionFlagId()
        {
            return DbSet.Where(sf => sf.SectionId == 81 && sf.isDefault == true).Select(id => id.SectionFlagId).FirstOrDefault();
        }

        /// <summary>
        /// Get Section Flags for Customer Price Index Section
        /// </summary>
        public IEnumerable<SectionFlag> GetAllForCustomerPriceIndex()
        {
            return DbSet.Where(sf => sf.SectionId == (int)SectionEnum.CustomerPriceMatrix && sf.OrganisationId == OrganisationId).ToList();
        }
        public IEnumerable<SectionFlag> GetDefaultSectionFlags()
        {
            return DbSet.Where(sf => sf.SectionId == (int)SectionEnum.CustomerPriceMatrix && sf.OrganisationId == OrganisationId && sf.isDefault == true).ToList();
        }
        /// <summary>
        /// Get Section Flags for Campaign
        /// </summary>
        public IEnumerable<SectionFlag> GetAllForCampaign()
        {
            return DbSet.Where(sf => sf.SectionId == (int)SectionEnum.Customers && sf.OrganisationId == OrganisationId).ToList();
        }

        public List<SectionFlag> GetSectionFlagsByOrganisationID(long OID)
        {
            try
            {
                db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;

                Mapper.CreateMap<SectionFlag, SectionFlag>()
               .ForMember(x => x.Section, opt => opt.Ignore());
               

                List<SectionFlag> scFlag = db.SectionFlags.Where(s => s.OrganisationId == OID).ToList();

                List<SectionFlag> oOutputSection = new List<SectionFlag>();

                if (scFlag != null && scFlag.Count > 0)
                {
                    foreach (var item in scFlag)
                    {
                        var omappedItem = Mapper.Map<SectionFlag, SectionFlag>(item);
                        oOutputSection.Add(omappedItem);
                    }
                }
                return oOutputSection;

            }
            catch(Exception ex)
            {
                throw ex;

            }
        }
        public SectionFlag GetSectionFlag(long id)
        {
             return  db.SectionFlags.Where(a => a.SectionFlagId == id).FirstOrDefault();
        }

        /// <summary>
        /// Get Defualt Section Flag for Price Matrix in webstore by organisation Id
        /// </summary>
        public int GetDefaultSectionFlagId(long OrganisationId)
        {
            return DbSet.Where(sf => sf.SectionId == 81 && sf.isDefault == true && sf.OrganisationId == OrganisationId).Select(id => id.SectionFlagId).FirstOrDefault();
        }

        public IEnumerable<SectionFlag> GetAllSectionFlagName()
        {
            return db.SectionFlags.ToList();
        }

        #endregion
    }
}