using MPC.Models.Common;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.DomainModels;

namespace MPC.Interfaces.WebStoreServices
{
    public interface IItemService
    {
        List<ItemStockOption> GetStockList(long ItemId, long CompanyId);
        Item GetItemById(long ItemId);

        Item CloneItem(int itemID, double CurrentTotal, int RefItemID, long OrderID, int CustomerID, double Quantity, int TemplateID, int StockID, List<AddOnCostsCenter> SelectedAddOnsList, bool isCorporate, bool isSavedDesign, bool isCopyProduct, int objContactID);
        List<ItemPriceMatrix> GetPriceMatrix(List<ItemPriceMatrix> tblRefItemsPriceMatrix, bool IsRanged, bool IsUserLoggedIn, long CompanyId);

        string specialCharactersEncoder(string value);

        ProductItem GetItemAndDetailsByItemID(int itemId);
    }
}
