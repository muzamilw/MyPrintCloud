using Microsoft.Practices.Unity;
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
            try
            {
                List<AssetItem> Items = db.AssetItems.Where(i => i.AssetId == AssetID).ToList();
                foreach (var item in Items)
                {
                    db.AssetItems.Remove(item);
                }
                Asset Asset = db.Assets.Where(i => i.AssetId == AssetID).FirstOrDefault();
                db.Assets.Remove(Asset);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateAsset(Asset UpdatedAsset)
        {
            try
            {
                Asset Asset = db.Assets.Where(i => i.AssetId == UpdatedAsset.AssetId).FirstOrDefault();
                Asset.AssetName = UpdatedAsset.AssetName;
                Asset.Description = UpdatedAsset.Description;
                Asset.FolderId = UpdatedAsset.FolderId;
                if (UpdatedAsset.ImagePath != null && UpdatedAsset.ImagePath != string.Empty)
                {
                    Asset.ImagePath = UpdatedAsset.ImagePath;
                }
                Asset.Keywords = UpdatedAsset.Keywords;
                Asset.Price = UpdatedAsset.Price;
                Asset.Quantity = UpdatedAsset.Quantity;
                Asset.UpdateDateTime = DateTime.Now.Date;
                db.Assets.Attach(Asset);
                db.Entry(Asset).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public long AddAsset(Asset Asset)
        {
            try
            {
                long AssetID = 0;
                Asset As = new Asset();
                As.AssetName = Asset.AssetName;
                As.CompanyId = Asset.CompanyId;
                As.Description = Asset.Description;
                As.CreationDateTime = System.DateTime.Now.Date;
                As.UpdateDateTime = DateTime.Now.Date;
                As.FolderId = Asset.FolderId;
                As.Price = Asset.Price;
                As.Quantity = Asset.Quantity;
                As.Keywords = Asset.Keywords;
                db.Assets.Add(As);
                if (db.SaveChanges() > 0)
                {
                    AssetID = As.AssetId;
                }
                return AssetID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateAssetImage(Asset Asset)
        {
            try
            {
                Asset getAsset = db.Assets.Where(i => i.AssetId == Asset.AssetId).FirstOrDefault();
                getAsset.ImagePath = Asset.ImagePath;
                db.Assets.Attach(getAsset);
                db.Entry(getAsset).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Asset GetAsset(long AssetId)
        {
            try
            {
                db.Configuration.LazyLoadingEnabled = false;
                return db.Assets.Where(i => i.AssetId == AssetId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

}
