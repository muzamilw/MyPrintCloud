using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Implementation.WebStoreServices
{
    public class ItemService : IItemService
    {

        private readonly IItemRepository _IItemRepository;
        private readonly ICompanyRepository _ICompanyRepository;
        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public ItemService(IItemRepository IItemRepository, ICompanyRepository ICompanyRepository)
        {
            this._IItemRepository = IItemRepository;
            this._ICompanyRepository = ICompanyRepository;
        }

        public Item CloneItem(int itemID, double CurrentTotal, int RefItemID, long OrderID, int CustomerID, double Quantity, int TemplateID, int StockID, List<AddOnCostsCenter> SelectedAddOnsList, bool isCorporate, bool isSavedDesign, bool isCopyProduct, int objContactID)
        {
            Models.DomainModels.Company company = _ICompanyRepository.GetStoreById((int)CustomerID);
            return _IItemRepository.CloneItem(itemID, CurrentTotal, RefItemID, OrderID, CustomerID, Quantity, TemplateID, StockID, SelectedAddOnsList, isCorporate, isSavedDesign, isCopyProduct, objContactID, company);

        }
        #endregion

        

    }
}
