using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.Repository
{
    public  interface IAssetsRepository:IBaseRepository<Asset, long>
    {
        List<Asset> GetAssetsByCompanyID(long CompanyID);
        void DeleteAsset(long AssetID);
        void UpdateAsset(Asset UpdatedAsset);
        List<Asset> GetAssetsByCompanyIDAndFolderID(long CompanyID, long FolderId);
        long AddAsset(Asset Asset);
        void UpdateAssetImage(Asset Asset);
        Asset GetAsset(long AssetId);
        List<Asset> GetAssetsByFolderIds(List<long> folderIds);

        List<Asset> SearchAssetsByFolderId(long ParentFolderId, string searchText);


    }
}
