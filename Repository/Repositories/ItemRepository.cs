using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    /// <summary>
    /// Item Repository
    /// </summary>
    public class ItemRepository : BaseRepository<Item>, IItemRepository
    {
        #region privte

        /// <summary>
        /// Item Orderby clause
        /// </summary>
        private readonly Dictionary<ItemByColumn, Func<Item, object>> stockItemOrderByClause = 
            new Dictionary<ItemByColumn, Func<Item, object>>
                    {
                         { ItemByColumn.Name, c => c.ProductName },
                         { ItemByColumn.Code, c => c.ProductCode }
                    };
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ItemRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Item> DbSet
        {
            get
            {
                return db.Items;
            }
        }

        #endregion

        #region public

        /// <summary>
        /// Get All Items for Current Organisation
        /// </summary>
        public override IEnumerable<Item> GetAll()
        {
            return DbSet.Where(item => item.OrganisationId == OrganisationId).ToList();
        }

        /// <summary>
        /// Get Items For Specified Search
        /// </summary>
        public ItemSearchResponse GetItems(ItemSearchRequestModel request)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;

            Expression<Func<Item, bool>> query =
                item =>
                    ((string.IsNullOrEmpty(request.SearchString) || item.ProductName.Contains(request.SearchString)) &&
                    item.OrganisationId == OrganisationId);

            IEnumerable<Item> items = request.IsAsc
               ? DbSet.Where(query)
                   .OrderBy(stockItemOrderByClause[request.ItemOrderBy])
                   .Skip(fromRow)
                   .Take(toRow)
                   .ToList()
               : DbSet.Where(query)
                   .OrderByDescending(stockItemOrderByClause[request.ItemOrderBy])
                   .Skip(fromRow)
                   .Take(toRow)
                   .ToList();

            return new ItemSearchResponse { Items = items, TotalCount = DbSet.Count(query) };
        }
        public List<GetItemsListView> GetRetailOrCorpPublishedProducts(int ProductCategoryID)
        {
            //g.ProductCategoryId == ProductCategoryID , ProductCategoryId is no more in Items
            List<GetItemsListView> recordds = db.GetItemsListViews.Where(g => g.IsPublished == true && g.EstimateId == null).OrderBy(g => g.ProductName).ToList();
            
            recordds = recordds.OrderBy(s => s.SortOrder).ToList();
            return recordds;
        }

        public ItemStockOption GetFirstStockOptByItemID(int ItemId, int CompanyId)
        {
                if (CompanyId > 0)
                {
                    return db.ItemStockOptions.Where(i => i.ItemId == ItemId && i.CompanyId == CompanyId && i.OptionSequence == 1).FirstOrDefault();
                }
                else
                {
                    return db.ItemStockOptions.Where(i => i.ItemId == ItemId && i.CompanyId == null && i.OptionSequence == 1).FirstOrDefault();
                }
        }

        public List<ItemPriceMatrix> GetPriceMatrixByItemID(int ItemId)
        {
         
                return db.ItemPriceMatrices.Where(i => i.ItemId == ItemId && i.SupplierId == null).Take(2).ToList();
        }

        public Item CloneItem(int itemID, double CurrentTotal, int RefItemID, long OrderID, int CustomerID, double Quantity, int TemplateID, int StockID, List<AddOnCostsCenter> SelectedAddOnsList, bool isCorporate, bool isSavedDesign, bool isCopyProduct, Company objCompany, CompanyContact objContact, Company BrokerCompany)
        {
            //Template clonedTemplate = null;
            //OrderManager ordManager = new OrderManager();
            //ItemSection tblItemSectionCloned = null;
            //ItemAttachment Attacments = null;
            //SectionCostcentre tblISectionCostCenteresCloned = null;
            //Item newItem = null;

            //vw_CompanySites vwCompanySite = null;
            //CompanySiteManager compSiteManager = new CompanySiteManager();
            //double netTotal = 0;
            //double grossTotal = 0;
            //int clonedNewItemID = 0;

            //try
            //{
            //    vwCompanySite = compSiteManager.GetCompanySiteDataWithTaxes();
            //    Item tblItemProduct = GetItemToClone(itemID);
            //    //******************new item*********************
            //    newItem = CloneService.Clone<Item>(tblItemProduct);

            //    newItem.ItemID = 0;
            //    newItem.IsPublished = false;

            //    if (isCopyProduct == true)
            //    {
            //        newItem.ProductName = tblItemProduct.ProductName + " Copy";

            //    }
            //    else
            //    {
            //        netTotal = CurrentTotal;
            //        if (BrokerCompany != null)
            //        {
            //            if (BrokerCompany.isIncludeVAT == true)
            //            {
            //                tbl_taxrate rate = CompanySiteManager.GetTaxRatesByID(Convert.ToInt32(BrokerCompany.TaxPercentageID));
            //                if (rate != null)
            //                {
            //                    grossTotal = ProductManager.GrossTotalCalculation(netTotal, rate.Tax1 ?? 0);
            //                }
            //                else
            //                {
            //                    grossTotal = ProductManager.GrossTotalCalculation(netTotal, 0);
            //                }
            //            }
            //            else
            //            {
            //                grossTotal = ProductManager.GrossTotalCalculation(netTotal, 0);
            //            }

            //        }
            //        else if (objCompany != null)
            //        {
            //            if (objCompany.IsCustomer == (int)CustomerTypes.Corporate)
            //            {
            //                if (objCompany.isIncludeVAT == true)
            //                {
            //                    tbl_taxrate rate = CompanySiteManager.GetTaxRatesByID(Convert.ToInt32(objCompany.TaxPercentageID));
            //                    if (rate != null)
            //                    {
            //                        grossTotal = ProductManager.GrossTotalCalculation(netTotal, rate.Tax1 ?? 0);
            //                    }
            //                    else
            //                    {
            //                        grossTotal = ProductManager.GrossTotalCalculation(netTotal, 0);
            //                    }
            //                }
            //                else
            //                {
            //                    grossTotal = ProductManager.GrossTotalCalculation(netTotal, 0);
            //                }
            //            }
            //            else
            //            {
            //                grossTotal = ProductManager.GrossTotalCalculation(netTotal, vwCompanySite.StateTaxValue);
            //            }

            //        }
            //        else
            //        {
            //            grossTotal = ProductManager.GrossTotalCalculation(netTotal, vwCompanySite.StateTaxValue);
            //        }

            //        if (isSavedDesign)
            //        {
            //            newItem.RefItemID = RefItemID;
            //        }
            //        else
            //        {
            //            newItem.RefItemID = itemID; // the refrencedid
            //        }
            //        newItem.EstimateID = OrderID; //orderid
            //        newItem.ContactCompanyID = CustomerID; //customerid
            //        newItem.Status = (short)ProductManager.ItemStatuses.ShoppingCart; //tblStatuses.StatusID; //shopping cart
            //        newItem.Qty1 = Convert.ToInt32(Quantity); //qty
            //        newItem.Qty1BaseCharge1 = CurrentTotal; //productSelection.PriceTotal + productSelection.AddonTotal; //item price
            //        newItem.Qty1Tax1Value = vwCompanySite.StateTaxValue; // say vat
            //        newItem.Qty1NetTotal = netTotal;
            //        newItem.Qty1GrossTotal = grossTotal;
            //        newItem.InvoiceID = null;
            //        newItem.IsOrderedItem = false;
            //        newItem.IsFinishedGoods = tblItemProduct.IsFinishedGoods;
            //        newItem.isMarketingBrief = tblItemProduct.isMarketingBrief;
            //        newItem.EstimateProductionTime = tblItemProduct.EstimateProductionTime;
            //        newItem.isStockControl = tblItemProduct.isStockControl;
            //        newItem.DefaultItemTax = tblItemProduct.DefaultItemTax;
            //    }
            //    using (MPCEntities dbContext = new MPCEntities())
            //    {

            //        // Default Mark up rate will be always 0 ...

            //        tbl_markup markup = (from c in dbContext.tbl_markup
            //                             where c.MarkUpID == 1 && c.MarkUpRate == 0
            //                             select c).FirstOrDefault();

            //        newItem.Qty1MarkUpID1 = markup.MarkUpID;  //markup id
            //        newItem.Qty1MarkUp1Value = markup.MarkUpRate;

            //        dbContext.tbl_items.AddObject(newItem); //dbcontext added

            //        //*****************Existing item Sections and cost Centeres*********************************
            //        foreach (tbl_item_sections tblItemSection in tblItemProduct.tbl_item_sections.ToList())
            //        {
            //            tblItemSectionCloned = CloneService.Clone<tbl_item_sections>(tblItemSection);
            //            tblItemSectionCloned.ItemSectionID = 0;
            //            tblItemSectionCloned.ItemID = newItem.ItemID;
            //            dbContext.tbl_item_sections.AddObject(tblItemSectionCloned); //ContextAdded

            //            //*****************Section Cost Centeres*********************************
            //            if (tblItemSection.tbl_section_costcentres.Count > 0)
            //            {
            //                foreach (tbl_section_costcentres tblSectCostCenter in tblItemSection.tbl_section_costcentres.ToList())
            //                {
            //                    tblISectionCostCenteresCloned = CloneService.Clone<tbl_section_costcentres>(tblSectCostCenter);
            //                    tblISectionCostCenteresCloned.SectionCostcentreID = 0;
            //                    tblItemSectionCloned.tbl_section_costcentres.Add(tblISectionCostCenteresCloned);
            //                }
            //            }
            //        }
            //        //Copy Template if it does exists
            //        if (newItem.TemplateID.HasValue && newItem.TemplateID.Value > 0)
            //        {
            //            if (isCorporate)
            //            {
            //                System.Data.Objects.ObjectResult<int?> result = dbContext.sp_cloneTemplate(newItem.TemplateID.Value, 0, "");
            //                int? clonedTemplateID = result.Single();
            //                clonedTemplate = dbContext.Templates.Where(g => g.ProductID == clonedTemplateID).Single();


            //                // saving water mark string added by saqib and copied by MZ here

            //                CustomerManager ocMngr = new CustomerManager();
            //                var oCutomer = CustomerManager.GetCustomer(CustomerID);
            //                if (oCutomer != null)
            //                {
            //                    clonedTemplate.TempString = oCutomer.WatermarkText;
            //                    clonedTemplate.isWatermarkText = oCutomer.isTextWatermark;
            //                    if (oCutomer.isTextWatermark == false)
            //                    {
            //                        clonedTemplate.TempString = HttpContext.Current.Server.MapPath(oCutomer.WatermarkText);
            //                    }

            //                }

            //                ResolveTemplateVariables(clonedTemplate.ProductID, objCompany, objContact, StoreMode.Corp);

            //            }
            //            else if (isSavedDesign)
            //            {
            //                System.Data.Objects.ObjectResult<int?> result = dbContext.sp_cloneTemplate(newItem.TemplateID.Value, 0, "");
            //                int? clonedTemplateID = result.Single();
            //                clonedTemplate = dbContext.Templates.Where(g => g.ProductID == clonedTemplateID).Single();
            //                newItem.TemplateID = clonedTemplate.ProductID;
            //                TemplateID = clonedTemplate.ProductID;
            //                CopyTemplatePathsSavedDesigns(dbContext, clonedTemplate);

            //            }
            //        }


            //        if (dbContext.SaveChanges() > 0)
            //        {
            //            if (clonedTemplate != null && isCorporate)
            //            {
            //                newItem.TemplateID = clonedTemplate.ProductID;
            //                TemplateID = clonedTemplate.ProductID;

            //                CopyTemplatePaths(dbContext, clonedTemplate);
            //            }
            //            else if (isCorporate)
            //            {
            //                throw new Exception("Copy template failed");
            //            }
            //            if (isCopyProduct == true)
            //            {

            //                clonedNewItemID = newItem.ItemID;
            //                SaveAdditionalAddonsOrUpdateStockItemType(dbContext, SelectedAddOnsList, newItem.ItemID, 0, 0, isCopyProduct); // additional addon required the newly inserted cloneditem
            //            }
            //            else
            //            {
            //                newItem.TemplateID = TemplateID;
            //                clonedNewItemID = newItem.ItemID;
            //                SaveAdditionalAddonsOrUpdateStockItemType(dbContext, SelectedAddOnsList, newItem.ItemID, StockID, 0, isCopyProduct); // additional addon required the newly inserted cloneditem
            //            }
            //            newItem.ItemCode = "ITM-0-001-" + newItem.ItemID;
            //            dbContext.SaveChanges();
            //        }
            //        else
            //            throw new Exception("Nothing happened");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            Item newItem = null;
            return newItem;
        }

        public Item GetItemToClone(int itemID)
        {
            Item productItem = null;
            productItem = db.Items.Include("ItemSection.SectionCostcentre").Where(item => item.ItemId == itemID).FirstOrDefault<Item>();
            return productItem;
          
        }

        #endregion
    }
}
