using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.Repository
{
    public interface IAssetItemsRepository : IBaseRepository<AssetItem, long>
    {
        bool AddAssetItems(List<AssetItem> AssetItemsList);
        List<AssetItem> GetAssetItemsByAssetID(long AssetID);
        void RemoveAssetItem(long AssetID);
    }
}
