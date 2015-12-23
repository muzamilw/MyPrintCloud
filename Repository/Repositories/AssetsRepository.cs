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

        public List<Asset> GetAssetsByCompanyIDAndFolderID(long CompanyID, long FolderId)
        {
            return db.Assets.Where(i => i.CompanyId == CompanyID && i.FolderId == FolderId).ToList();
        
        }

        public void  DeleteAsset(long AssetID)
        {
            Asset Asset = db.Assets.Where(i => i.AssetId == AssetID).FirstOrDefault();
            db.Assets.Remove(Asset);
            db.SaveChanges();
        }
        public void UpdateAsset(long AssetID)
        {
            Asset Asset = db.Assets.Where(i => i.AssetId == AssetID).FirstOrDefault();
            db.Assets.Attach(Asset);
            db.Entry(Asset).State = EntityState.Modified;
        }
        public long AddAsset(Asset Asset)
        {
            long AssetID = 0;
            Asset As = new Asset();
            As.AssetName = Asset.AssetName;
            As.CompanyId = Asset.CompanyId;
            As.Description = Asset.Description;
            As.CreationDateTime = System.DateTime.Now.Date;
            As.FolderId = Asset.FolderId;
            As.Price = Asset.Price;
            As.Quantity = Asset.Quantity;
            As.Keywords = Asset.Keywords;
            db.Assets.Add(As);
            if (db.SaveChanges()> 0)
            {
                AssetID = As.AssetId;
            }
            return AssetID;
        }
        public void UpdateAssetImage(Asset Asset)
        {
            Asset getAsset = db.Assets.Where(i => i.AssetId == Asset.AssetId).FirstOrDefault();
            getAsset.ImagePath = Asset.ImagePath;
            db.Assets.Attach(getAsset);
            db.Entry(getAsset).State = EntityState.Modified;
            db.SaveChanges();
        }
    }

}
