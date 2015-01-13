using System.Data.Entity;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;
using System.Collections.Generic;
using System.Linq;
using MPC.Models.Common;

namespace MPC.Repository.Repositories
{
    /// <summary>
    /// ItemA Repository
    /// </summary>
    public class ItemAddonCostCentreRepository : BaseRepository<ItemAddonCostCentre>, IItemAddOnCostCentreRepository
    {
        #region privte
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ItemAddonCostCentreRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<ItemAddonCostCentre> DbSet
        {
            get
            {
                return db.ItemAddonCostCentres;
            }
        }

        #endregion

        #region public

        public List<AddOnCostsCenter> AddOnsPerStockOption(long itemId, long companyId) 
        {
            var query =     from addOns in db.ItemAddonCostCentres 
                            join costcenter in db.CostCentres  on addOns.CostCentreId equals costcenter.CostCentreId
                            join options in db.ItemStockOptions on addOns.ItemStockOptionId equals options.ItemStockOptionId
                            where options.ItemId == itemId && options.CompanyId == companyId
                            select new AddOnCostsCenter()
                            {
                                        ProductAddOnID = addOns.ProductAddOnId,
                                        ItemID = options.ItemId ?? 0,
                                        CostCenterID = addOns.CostCentreId ?? 0,
                                        AddOnName = costcenter.Name,
                                        AddOnPrice = costcenter.CalculationMethodType == 1 ? costcenter.PriceDefaultValue ?? 0 : costcenter.PricePerUnitQuantity ?? 0,
                                        WebStoreDesc = costcenter.WebStoreDesc,
                                        Type = costcenter.CalculationMethodType,
                                        SetupCost = costcenter.SetupCost,
                                        PricePerUnitQuantity = costcenter.PricePerUnitQuantity,
                                        MinimumCost = costcenter.MinimumCost,
                                        AddOnImage = costcenter.ThumbnailImageURL,
                                        Priority = costcenter.Priority ?? 0,
                                        ItemStockId = options.StockId ?? 0
                            };
            return query.ToList<AddOnCostsCenter>();
        }


        #endregion
    }
}
