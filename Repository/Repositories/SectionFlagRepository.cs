﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.Common;
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
            return DbSet.Where(sf => sf.SectionId == (int)SectionEnum.Inventory).ToList();
        }

        /// <summary>
        /// Get Section Flag By Section Id
        /// </summary>
        public IEnumerable<SectionFlag> GetSectionFlagBySectionId(long sectionId)
        {
            return DbSet.Where(sf => sf.SectionId == sectionId).ToList();
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
            return DbSet.Where(sf => sf.SectionId == (int)SectionEnum.CustomerPriceMatrix).ToList();
        }

        /// <summary>
        /// Get Section Flags for Campaign
        /// </summary>
        public IEnumerable<SectionFlag> GetAllForCampaign()
        {
            return DbSet.Where(sf => sf.SectionId == (int)SectionEnum.Customers && sf.OrganisationId == OrganisationId).ToList();
        }

        /// <summary>
        /// Get Base data for orders
        /// </summary>
        public IEnumerable<SectionFlag> GetFlagsForOrders()
        {
            return DbSet.Where(flag => flag.SectionId == 54).ToList();
        }
        #endregion
    }
}