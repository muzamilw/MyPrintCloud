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
    class AssetsRepository :BaseRepository<Asset>, IAssetsRepository
    {
        public AssetsRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Asset> DbSet
        {
            get
            {
                return db.Assets;
            }
        }

        public List<Asset> GetAssetsByCompanyID(long CompanyID)
        {
            return db.Assets.Where(i => i.CompanyId == CompanyID).ToList();
        }
    }

}
