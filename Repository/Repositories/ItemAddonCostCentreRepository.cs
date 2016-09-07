using System.Data.Entity;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;
using System.Collections.Generic;
using System.Linq;
using MPC.Models.Common;
using System;

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
            var query = from addOns in db.ItemAddonCostCentres
                        join costcenter in db.CostCentres on addOns.CostCentreId equals costcenter.CostCentreId
                        join options in db.ItemStockOptions on addOns.ItemStockOptionId equals options.ItemStockOptionId
                        where options.ItemId == itemId && costcenter.IsDisabled != true // && options.CompanyId == companyId
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
                            ItemStockId = options.StockId ?? 0,
                            IsMandatory = addOns.IsMandatory == true ? 1 : 0,
                            QuantitySourceType = costcenter.QuantitySourceType ?? 0,
                            TimeSourceType = costcenter.TimeSourceType ?? 0,
                            ItemStockOptionId = options.ItemStockOptionId,
                            Sequence = addOns.Sequence,
                            IsAutoExec = addOns.IsSelectedOnLoad == true ? 1 : 0,
                        };
            return query.OrderBy(s => s.Sequence).ToList<AddOnCostsCenter>();
        }
        /// <summary>
        /// get cost center list according to stock option id
        /// </summary>
        /// <param name="StockOptionID"></param>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        public List<string> GetProductItemAddOnCostCentres(long StockOptionID, long CompanyID)
        {
            try
            {
                var query = from addOns in db.ItemAddonCostCentres
                            join costcenter in db.CostCentres on addOns.CostCentreId equals costcenter.CostCentreId
                            join options in db.ItemStockOptions on addOns.ItemStockOptionId equals options.ItemStockOptionId
                            where options.ItemStockOptionId == StockOptionID && options.CompanyId == CompanyID
                            select costcenter.Name;
                return query.ToList<string>();

            }
            catch (Exception ex)
            {
                throw ex;
            }



        }


        /// <summary>
        /// get id's of cost center except webstore cost cnetre 216 of first section of cloned item 
        /// </summary>
        /// <param name="StockOptionID"></param>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        public List<SectionCostcentre> GetClonedItemAddOnCostCentres(long ItemId)
        {
            try
            {

                long itemSectionId = db.ItemSections.Where(i => i.ItemId == ItemId && i.SectionNo == 1).Select(i => i.ItemSectionId).FirstOrDefault();

                

                return db.SectionCostcentres.Where(s => s.ItemSectionId == itemSectionId).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion
    }
}
