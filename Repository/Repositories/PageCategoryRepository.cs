﻿using Microsoft.Practices.Unity;
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
    public class PageCategoryRepository : BaseRepository<PageCategory>, IPageCategoryRepository
    {
        public PageCategoryRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<PageCategory> DbSet
        {
            get
            {
                return db.PageCategories;
            }
        }

        public List<PageCategory> GetCmsSecondaryPageCategories()
        {
            return db.PageCategories.ToList();
        }
    }
}