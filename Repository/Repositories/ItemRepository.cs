using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Practices.Unity;
using MPC.ExceptionHandling;
using MPC.Interfaces.Repository;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using MPC.Repository.BaseRepository;
using System.Reflection;
using System.Web;
using Newtonsoft.Json;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using WebSupergoo.ABCpdf8;
using System.Globalization;
using MPC.Interfaces.WebStoreServices;



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
        /// 
      
        private readonly Dictionary<ItemByColumn, Func<Item, object>> stockItemOrderByClause =
            new Dictionary<ItemByColumn, Func<Item, object>>
            {
                {ItemByColumn.Name, c => c.ProductName},
                {ItemByColumn.Code, c => c.ProductCode}
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
            get { return db.Items; }
        }

        #endregion

        #region public

        /// <summary>
        /// Get Item With Details
        /// </summary>
        public Item GetItemWithDetails(long itemId)
        {
            try
            {
                return
                              DbSet
                              .Include("ItemSections")
                              .Include("ItemSections.StockItem")
                              .Include("ItemSections.Machine")
                              .Include("ItemSections.MachineSide2")
                              .Include("ItemSections.SectionInkCoverages")
                              .Include("ItemStockOptions")
                              .Include("ItemStockOptions.StockItem")
                              .Include("ItemStockOptions.ItemAddonCostCentres")
                              .Include("ItemStockOptions.ItemAddonCostCentres.CostCentre")
                              .Include("ItemStockOptions.ItemAddonCostCentres.CostCentre.CostCentreType")
                              .Include("ProductCategoryItems")
                              .Include("ProductCategoryItems.ProductCategory")
                              .Include("ItemStateTaxes")
                              .Include("ItemStateTaxes.Country")
                              .Include("ItemStateTaxes.State")
                              .Include("ItemRelatedItems")
                              .Include("ItemRelatedItems.RelatedItem")
                              .Include("Template")
                              .Include("Template.TemplatePages")
                              .Include("ItemImages")
                              .Include("ItemVdpPrices")
                              .Include("ItemVideos")
                              .Include("ItemProductDetails")
                              .Include("ItemPriceMatrices")
                              .Include("ProductMarketBriefQuestions")
                              .Include("ProductMarketBriefQuestions.ProductMarketBriefAnswers")
                              .FirstOrDefault(item => item.ItemId == itemId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Check if product code provided already exists
        /// </summary>
        public bool IsDuplicateProductCode(string productCode, long? itemId, long? companyId)
        {
            try
            {
                return DbSet.Any(item => item.ProductCode == productCode && (!itemId.HasValue || item.ItemId != itemId) && item.OrganisationId == OrganisationId &&
                    item.EstimateId == null && (!companyId.HasValue || item.CompanyId == companyId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get All Items for Current Organisation
        /// </summary>
        public override IEnumerable<Item> GetAll()
        {
            try
            {
                return DbSet.Where(item => item.OrganisationId == OrganisationId).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get Items For Specified Search
        /// </summary>
        public ItemSearchResponse GetItems(ItemSearchRequestModel request)
        {
            try
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GetCategoryProduct> GetRetailOrCorpPublishedProducts(long ProductCategoryID)
        {
            try
            {
                List<GetCategoryProduct> recordds =
              db.GetCategoryProducts.Where(
                  g => g.IsPublished == true && g.EstimateId == null && g.ProductCategoryId == ProductCategoryID && (g.IsArchived == null || g.IsArchived == false))
                  .OrderBy(g => g.ProductName)
                  .ToList();
                recordds = recordds.OrderBy(s => s.SortOrder).ToList();
                return recordds;
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }
         public long TotalProductTypeFourItems(long OrderId)
         {
             return db.Items.Where(i => i.EstimateId == OrderId && i.ProductType == 4).ToList().Count;
         }
         public long OtherTheTypeFourItems(long OrderId)
         {
             return db.Items.Where(i => i.EstimateId == OrderId && i.ProductType!=4).ToList().Count;
         }
        public ItemStockOption GetFirstStockOptByItemID(long ItemId, long CompanyId)
        {
            try
            {
                //if (CompanyId > 0)
                //{
                //    return
                //        db.ItemStockOptions.Where(
                //            i => i.ItemId == ItemId && i.CompanyId == CompanyId && i.OptionSequence == 1).FirstOrDefault();
                //}
                //else
                //{
                return
                    db.ItemStockOptions.Where(i => i.ItemId == ItemId && i.OptionSequence == 1)
                        .FirstOrDefault();
                //}
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public List<ItemPriceMatrix> GetPriceMatrixByItemID(int ItemId)
        {

            try
            {
                return db.ItemPriceMatrices.Where(i => i.ItemId == ItemId && i.SupplierId == null).ToList();

            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        //public Item CloneItem(long itemID, long RefItemID, long OrderID, long CustomerID, long TemplateID, long StockID,
        //    List<AddOnCostsCenter> SelectedAddOnsList, bool isSavedDesign, bool isCopyProduct, long objContactID,
        //    long OrganisationID)
        //{
        //    try
        //    {
        //        Template clonedTemplate = null;

        //        ItemSection tblItemSectionCloned = new ItemSection();

        //        ItemAttachment Attacments = new ItemAttachment();

        //        SectionCostcentre tblISectionCostCenteresCloned = new SectionCostcentre();

        //        Item newItem = new Item();


        //        Item ActualItem = GetActualItemToClone(itemID);
        //        //******************new item*********************
        //        newItem = Clone<Item>(ActualItem);

        //        newItem.ItemId = 0;

        //        newItem.IsPublished = false;

        //        newItem.IsEnabled = false;

        //        newItem.EstimateId = OrderID;

        //        newItem.StatusId = (short)ItemStatuses.ShoppingCart; //tblStatuses.StatusID; //shopping cart

        //        newItem.Qty1 = 0; //qty

        //        newItem.Qty1BaseCharge1 = 0; //productSelection.PriceTotal + productSelection.AddonTotal; //item price

        //        newItem.Qty1Tax1Value = 0; // say vat

        //        newItem.Qty1NetTotal = 0;

        //        newItem.Qty1GrossTotal = 0;

        //        newItem.ProductType = 0;

        //        newItem.InvoiceId = null;

        //        newItem.EstimateProductionTime = ActualItem.EstimateProductionTime;

        //        newItem.DefaultItemTax = ActualItem.DefaultItemTax;

        //        newItem.ProductType = ActualItem.ProductType;

        //        newItem.DesignerCategoryId = ActualItem.DesignerCategoryId;
        //        if (isCopyProduct)
        //        {
        //            newItem.IsOrderedItem = true;
        //            newItem.Qty1 = ActualItem.Qty1; //qty

        //            newItem.Qty1BaseCharge1 = ActualItem.Qty1BaseCharge1;
        //            //productSelection.PriceTotal + productSelection.AddonTotal; //item price

        //            newItem.Qty1Tax1Value = ActualItem.Qty1Tax1Value; // say vat

        //            newItem.Qty1NetTotal = ActualItem.Qty1NetTotal;

        //            newItem.Qty1GrossTotal = ActualItem.Qty1GrossTotal;
        //            newItem.ProductType = ActualItem.ProductType;
        //            newItem.ProductName = ActualItem.ProductName + "- Copy";
        //        }
        //        else
        //        {
        //            newItem.IsOrderedItem = false;
        //            if (!isSavedDesign)  // in case of save designs ref item 
        //                newItem.RefItemId = (int)itemID;
        //            else
        //                newItem.RefItemId = ActualItem.RefItemId;
        //        }



        //        // Default Mark up rate will be always 0 ...
        //        // when updating clone item we are getting markups from organisation ask sir naveed to change needed here also 
        //        //Markup markup = (from c in db.Markups
        //        //                 where c.MarkUpId == 1 && c.MarkUpRate == 0
        //        //                 select c).FirstOrDefault();

        //        //if (markup.MarkUpId != null)
        //        //    newItem.Qty1MarkUpId1 = (int)markup.MarkUpId;  //markup id
        //        //newItem.Qty1MarkUp1Value = markup.MarkUpRate;

        //        db.Items.Add(newItem); //dbcontext added

        //        //*****************Existing item Sections and cost Centeres*********************************
        //        foreach (ItemSection tblItemSection in ActualItem.ItemSections.ToList())
        //        {
        //            tblItemSectionCloned = Clone<ItemSection>(tblItemSection);
        //            tblItemSectionCloned.ItemSectionId = 0;
        //            tblItemSectionCloned.ItemId = newItem.ItemId;
        //            db.ItemSections.Add(tblItemSectionCloned); //ContextAdded

        //            //*****************Section Cost Centeres*********************************
        //            if (tblItemSection.SectionCostcentres.Count > 0)
        //            {
        //                foreach (SectionCostcentre tblSectCostCenter in tblItemSection.SectionCostcentres.ToList())
        //                {
        //                    tblISectionCostCenteresCloned = Clone<SectionCostcentre>(tblSectCostCenter);
        //                    tblISectionCostCenteresCloned.SectionCostcentreId = 0;
        //                    tblISectionCostCenteresCloned.ItemSectionId = tblItemSectionCloned.ItemSectionId;
        //                    db.SectionCostcentres.Add(tblISectionCostCenteresCloned);
        //                }

        //            }

        //        }
        //        //Copy Template if it does exists

        //        if (newItem.TemplateId.HasValue && newItem.TemplateId.Value > 0)
        //        {
        //            clonedTemplate = new Template();
        //            if (newItem.TemplateType == 1 || newItem.TemplateType == 2 || isSavedDesign || isCopyProduct)
        //            {
        //                long result = db.sp_cloneTemplate((int)newItem.TemplateId.Value, 0, "");

        //                long? clonedTemplateID = result;
        //                clonedTemplate = db.Templates.Where(g => g.ProductId == clonedTemplateID).Single();

        //                var oCutomer = db.Companies.Where(i => i.CompanyId == CustomerID).FirstOrDefault();
        //                clonedTemplate.ProductName = clonedTemplate.ProductName == null ? newItem.ProductName : clonedTemplate.ProductName;
        //                if (oCutomer != null)
        //                {
        //                    clonedTemplate.TempString = oCutomer.WatermarkText;
        //                    clonedTemplate.isWatermarkText = oCutomer.isTextWatermark;
        //                    if (oCutomer.isTextWatermark == false)
        //                    {
        //                        clonedTemplate.TempString = HttpContext.Current.Server.MapPath("~/" + oCutomer.WatermarkText);
        //                    }

        //                }
        //                // here 

        //                //  VariablesResolve(itemID, clonedTemplate.ProductId, objContactID);
        //            }

        //        }

        //        db.SaveChanges();
        //        if (clonedTemplate != null && (newItem.TemplateType == 1 || newItem.TemplateType == 2 || isSavedDesign || isCopyProduct))
        //        {
        //            newItem.TemplateId = clonedTemplate.ProductId;
        //            TemplateID = clonedTemplate.ProductId;

        //            CopyTemplatePaths(clonedTemplate, OrganisationID);
        //        }

        //        SaveAdditionalAddonsOrUpdateStockItemType(SelectedAddOnsList, newItem.ItemId, StockID, isCopyProduct, "", 0);
        //        // additional addon required the newly inserted cloneditem
        //        newItem.ItemCode = "ITM-0-001-" + newItem.ItemId;
        //        db.SaveChanges();

        //        //else
        //        //    throw 

        //        return newItem;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;

        //    }

        //}

        // resolve c
        public void VariablesResolve(long ItemID, long ProductID, long objContactID)
        {
            try
            {
                List<FieldVariable> lstFieldVariabes = GeyFieldVariablesByItemID(ItemID);
                if (lstFieldVariabes != null && lstFieldVariabes.Count > 0)
                {
                    List<Models.Common.TemplateVariable> lstPageControls = new List<Models.Common.TemplateVariable>();
                    CompanyContact contact = db.CompanyContacts.Where(c => c.ContactId == objContactID).FirstOrDefault();
                    lstPageControls = ResolveVariables(lstFieldVariabes, contact);
                    ResolveTemplateVariables(ProductID, contact, lstPageControls);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        // gettting field variables by itemid
        public List<FieldVariable> GeyFieldVariablesByItemID(long itemId)
        {
            try
            {
                var tempID = (from i in db.Items
                              where i.ItemId == itemId
                              select i.TemplateId).FirstOrDefault();

                int templateID = Convert.ToInt32(tempID);

                var IDs = (from v in db.TemplateVariables
                           where v.TemplateId == templateID
                           select v.VariableId).ToList();

                List<FieldVariable> lstFieldVariables = new List<FieldVariable>();

                foreach (int item in IDs)
                {
                    FieldVariable objFieldVariable = (from FV in db.FieldVariables
                                                      where FV.VariableId == item
                                                      orderby FV.VariableSectionId
                                                      select FV).FirstOrDefault();

                    lstFieldVariables.Add(objFieldVariable);
                }

                List<FieldVariable> finalList =
                    (List<FieldVariable>)lstFieldVariables.OrderBy(item => item.VariableSectionId).ToList();

                return finalList;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


        public List<Models.Common.TemplateVariable> ResolveVariables(List<FieldVariable> lstFieldVariabes,
            CompanyContact objContact)
        {
            try
            {
                List<Models.Common.TemplateVariable> templateVariables = new List<Models.Common.TemplateVariable>();
                if (lstFieldVariabes != null && lstFieldVariabes.Count > 0)
                {
                    foreach (var item in lstFieldVariabes)
                    {
                        int sectionId = Convert.ToInt32(item.VariableSectionId);

                        //add controls to current section
                        var keyValue = 0;
                        string fieldValue = string.Empty;

                        switch (item.RefTableName)
                        {

                            case "CompanyContacts":
                                keyValue = (int)objContact.ContactId;
                                fieldValue = DynamicQueryToGetRecord(item.CriteriaFieldName, item.RefTableName,
                                    item.KeyField, keyValue);
                                break;
                            case "Company":
                                keyValue = (int)objContact.CompanyId;
                                fieldValue = DynamicQueryToGetRecord(item.CriteriaFieldName, item.RefTableName,
                                    item.KeyField, keyValue);
                                break;
                            case "Address":
                                keyValue = (int)objContact.AddressId;
                                fieldValue = DynamicQueryToGetRecord(item.CriteriaFieldName, item.RefTableName,
                                    item.KeyField, keyValue);
                                break;
                            default:
                                break;
                        }

                        Models.Common.TemplateVariable imgTempVar =
                            new Models.Common.TemplateVariable(Convert.ToString(keyValue), fieldValue);

                        templateVariables.Add(imgTempVar);

                    }
                }
                return templateVariables;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public string DynamicQueryToGetRecord(string feildname, string tblname, string keyName, int keyValue)
        {
            try
            {
                string oResult = null;
                System.Data.Entity.Infrastructure.DbRawSqlQuery<string> result =
                    db.Database.SqlQuery<string>(
                        "select top 1 cast(" + feildname + " as varchar(1000)) from " + tblname + " where " + keyName + "= " +
                        keyValue + "", "");
                oResult = result.FirstOrDefault();
                return oResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public int CopyTemplatePaths(Template clonedTemplate, long OrganisationID)
        {
            int result = 0;

            try
            {
                result = (int)clonedTemplate.ProductId;

                //  string BasePath = System.Web.HttpContext.Current.Server.MapPath("~/DesignEngine/Designer/Products/");
                string drURL =
                    System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" +
                                                                  OrganisationID.ToString() + "/Templates/");
                //result = dbContext.sp_cloneTemplate(ProductID, SubmittedBy, SubmittedByName).First().Value;

                string targetFolder = drURL + result.ToString();
                if (!System.IO.Directory.Exists(targetFolder))
                {
                    System.IO.Directory.CreateDirectory(targetFolder);
                }


                //copy the background PDF Templates
                //Templates oTemplate = db.Templates.Where(g => g.ProductID == result).Single();
                Template oTemplate = clonedTemplate;

                //copy the background of pages

                foreach (TemplatePage oTemplatePage in db.TemplatePages.Where(g => g.ProductId == result).ToList())
                {

                    if (oTemplatePage.BackGroundType == 1 || oTemplatePage.BackGroundType == 3)
                    {
                        //copy side 1
                        if (oTemplatePage.BackGroundType == 1) //additional background copy function
                        {
                            string oldproductid = oTemplatePage.BackgroundFileName.Substring(0,
                                oTemplatePage.BackgroundFileName.IndexOf("/"));
                            if (File.Exists(drURL + oldproductid + "/" + "templatImgBk" + oTemplatePage.PageNo + ".jpg"))
                            {


                                File.Copy(
                                    Path.Combine(drURL,
                                        oldproductid + "/" + "templatImgBk" + oTemplatePage.PageNo + ".jpg"),
                                    drURL + result.ToString() + "/" + "templatImgBk" + oTemplatePage.PageNo + ".jpg");

                            }
                        }
                        if (File.Exists(drURL + oTemplatePage.BackgroundFileName))
                        {
                            File.Copy(drURL + oTemplatePage.BackgroundFileName,
                                drURL + result.ToString() + "/" +
                                oTemplatePage.BackgroundFileName.Substring(oTemplatePage.BackgroundFileName.IndexOf("/"),
                                    oTemplatePage.BackgroundFileName.Length - oTemplatePage.BackgroundFileName.IndexOf("/")), true);
                            oTemplatePage.BackgroundFileName = result.ToString() + "/" +
                                                               oTemplatePage.BackgroundFileName.Substring(
                                                                   oTemplatePage.BackgroundFileName.IndexOf("/"),
                                                                   oTemplatePage.BackgroundFileName.Length -
                                                                   oTemplatePage.BackgroundFileName.IndexOf("/"));
                        }


                    }
                }


                //skip concatinating the path if its a placeholder, cuz place holder is kept in a different path and doesnt need to be copied.

                if (oTemplate.TemplateObjects != null)
                {
                    oTemplate.TemplateObjects.Where(
                        tempObject => tempObject.ObjectType == 3 && tempObject.IsQuickText != true)
                        .ToList()
                        .ForEach(item =>
                        {

                            string filepath =
                                item.ContentString.Substring(
                                    item.ContentString.IndexOf("/Designer/Organisation" + OrganisationID.ToString() +
                                                               "/Templates/") +
                                    ("/Designer/Organisation" + OrganisationID.ToString() + "/Templates/").Length,
                                    item.ContentString.Length -
                                    ((item.ContentString.IndexOf("/Designer/Organisation" + OrganisationID.ToString() +
                                                                 "/Templates/") + "/Designer/Organisation" +
                                      OrganisationID.ToString() + "/Templates/").Length));
                            item.ContentString = "Designer/Organisation" + OrganisationID.ToString() + "/Templates/" +
                                                 result.ToString() +
                                                 filepath.Substring(filepath.IndexOf("/"),
                                                     filepath.Length - filepath.IndexOf("/"));

                        });
                }

                //foreach (var item in dbContext.TemplateObjects.Where(g => g.ProductID == result && g.ObjectType == 3))
                //{
                //    string filepath = item.ContentString.Substring(item.ContentString.IndexOf("DesignEngine/Designer/Products/") + "DesignEngine/Designer/Products/".Length, item.ContentString.Length - (item.ContentString.IndexOf("DesignEngine/Designer/Products/") + "DesignEngine/Designer/Products/".Length));
                //    item.ContentString = "DesignEngine/Designer/Products/" + result.ToString() + filepath.Substring(filepath.IndexOf("/"), filepath.Length - filepath.IndexOf("/"));
                //}

                //

                //copy the background images



                //var backimgs = dbContext.TemplateBackgroundImages.Where(g => g.ProductID == result);
                if (oTemplate.TemplateBackgroundImages != null)
                {
                    oTemplate.TemplateBackgroundImages.ToList().ForEach(item =>
                    {

                        string filePath = drURL + item.ImageName;
                        string filename;

                        string ext = Path.GetExtension(item.ImageName);

                        // generate thumbnail 
                        if (!ext.Contains("svg"))
                        {
                            string[] results = item.ImageName.Split(new string[] { ext }, StringSplitOptions.None);
                            string destPath = results[0] + "_thumb" + ext;
                            string ThumbPath = drURL + destPath;
                            FileInfo oFileThumb = new FileInfo(ThumbPath);
                            if (oFileThumb.Exists)
                            {
                                string oThumbName = oFileThumb.Name;
                                oFileThumb.CopyTo(drURL + result.ToString() + "/" + oThumbName, true);
                            }
                            //  objSvc.GenerateThumbNail(sourcePath, destPath, 98);
                        }


                        FileInfo oFile = new FileInfo(filePath);

                        if (oFile.Exists)
                        {
                            filename = oFile.Name;
                            item.ImageName = result.ToString() + "/" +
                                             oFile.CopyTo(drURL + result.ToString() + "/" + filename, true).Name;
                        }


                    });
                }



                db.SaveChanges();



            }
            catch (Exception ex)
            {
                throw new Exception("Copy Template Paths", ex);
                //AppCommon.LogException(ex);
                //throw ex;
            }

            return result;
        }

        public Item GetActualItemToClone(long itemID)
        {
            try
            {
              
                Item productItem = null;
                db.Configuration.LazyLoadingEnabled = false;
                productItem =
                    db.Items.Include("ItemSections.SectionCostcentres")
                        .Where(item => item.ItemId == itemID)
                        .FirstOrDefault<Item>();
                return productItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int CopyTemplatePathsSavedDesigns(Template clonedTemplate)
        {
            int result = 0;

            try
            {
                result = (int)clonedTemplate.ProductId;

                string BasePath = System.Web.HttpContext.Current.Server.MapPath("~/DesignEngine/Designer/Products/");

                string targetFolder =
                    System.Web.HttpContext.Current.Server.MapPath("~/DesignEngine/Designer/Products/" +
                                                                  result.ToString());
                if (!System.IO.Directory.Exists(targetFolder))
                {
                    System.IO.Directory.CreateDirectory(targetFolder);
                }


                //copy the background PDF Templates
                Template oTemplate = clonedTemplate;

                //copy the background of pages
                foreach (TemplatePage oTemplatePage in db.TemplatePages.Where(g => g.ProductId == result).ToList())
                {

                    if (oTemplatePage.BackGroundType == 1 || oTemplatePage.BackGroundType == 3)
                    {
                        //copy side 1
                        if (oTemplatePage.BackGroundType == 1) //additional background copy function
                        {
                            string oldproductid = oTemplatePage.BackgroundFileName.Substring(0,
                                oTemplatePage.BackgroundFileName.IndexOf("/"));
                            if (
                                File.Exists(BasePath + oldproductid + "/" + "templatImgBk" + oTemplatePage.PageNo +
                                            ".jpg"))
                            {


                                File.Copy(
                                    Path.Combine(BasePath,
                                        oldproductid + "/" + "templatImgBk" + oTemplatePage.PageNo + ".jpg"),
                                    BasePath + result.ToString() + "/" + "templatImgBk" + oTemplatePage.PageNo + ".jpg");

                            }
                        }

                        File.Copy(Path.Combine(BasePath, oTemplatePage.BackgroundFileName),
                            BasePath + result.ToString() + "/" +
                            oTemplatePage.BackgroundFileName.Substring(oTemplatePage.BackgroundFileName.IndexOf("/"),
                                oTemplatePage.BackgroundFileName.Length - oTemplatePage.BackgroundFileName.IndexOf("/")));
                        oTemplatePage.BackgroundFileName = result.ToString() + "/" +
                                                           oTemplatePage.BackgroundFileName.Substring(
                                                               oTemplatePage.BackgroundFileName.IndexOf("/"),
                                                               oTemplatePage.BackgroundFileName.Length -
                                                               oTemplatePage.BackgroundFileName.IndexOf("/"));

                    }
                }


                //skip concatinating the path if its a placeholder, cuz place holder is kept in a different path and doesnt need to be copied.
                oTemplate.TemplateObjects.Where(
                    tempObject => tempObject.ObjectType == 3 && tempObject.IsQuickText != true).ToList().ForEach(item =>
                    {

                        string filepath =
                            item.ContentString.Substring(
                                item.ContentString.IndexOf("Designer/Products/") + "Designer/Products/".Length,
                                item.ContentString.Length -
                                (item.ContentString.IndexOf("Designer/Products/") + "Designer/Products/".Length));
                        item.ContentString = "Designer/Products/" + result.ToString() +
                                             filepath.Substring(filepath.IndexOf("/"),
                                                 filepath.Length - filepath.IndexOf("/"));

                    });
                oTemplate.TemplateBackgroundImages.ToList().ForEach(item =>
                {

                    string filePath =
                        System.Web.HttpContext.Current.Server.MapPath("~/DesignEngine/Designer/Products/" +
                                                                      item.ImageName);
                    string filename;

                    string ext = Path.GetExtension(item.ImageName);

                    // generate thumbnail 
                    if (!ext.Contains("svg"))
                    {
                        string[] results = item.ImageName.Split(new string[] { ext }, StringSplitOptions.None);
                        string destPath = results[0] + "_thumb" + ext;
                        string ThumbPath =
                            System.Web.HttpContext.Current.Server.MapPath("~/DesignEngine/Designer/Products/" + destPath);
                        FileInfo oFileThumb = new FileInfo(ThumbPath);
                        if (oFileThumb.Exists)
                        {
                            string oThumbName = oFileThumb.Name;
                            oFileThumb.CopyTo(
                                System.Web.HttpContext.Current.Server.MapPath("~/DesignEngine/Designer/Products/" +
                                                                              result.ToString() + "/" + oThumbName),
                                true);
                        }
                    }


                    FileInfo oFile = new FileInfo(filePath);

                    if (oFile.Exists)
                    {
                        filename = oFile.Name;
                        item.ImageName = result.ToString() + "/" +
                                         oFile.CopyTo(
                                             System.Web.HttpContext.Current.Server.MapPath(
                                                 "~/DesignEngine/Designer/Products/" + result.ToString() + "/" +
                                                 filename), true).Name;
                    }


                });

                //   db.SaveChanges(0);
                db.SaveChanges();


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public T Clone<T>(T source)
        {
            try
            {
                db.Configuration.LazyLoadingEnabled = false;
                object item = Activator.CreateInstance(typeof(T));
                List<PropertyInfo> itemPropertyInfoCollection = source.GetType().GetProperties().ToList<PropertyInfo>();
                foreach (PropertyInfo propInfo in itemPropertyInfoCollection)
                {
                    if (propInfo.CanRead &&
                        (propInfo.PropertyType.IsValueType || propInfo.PropertyType.FullName == "System.String"))
                    {
                        PropertyInfo newProp = item.GetType().GetProperty(propInfo.Name);
                        if (newProp != null && newProp.CanWrite)
                        {
                            object va = propInfo.GetValue(source, null);
                            newProp.SetValue(item, va, null);
                        }
                    }
                }

                return (T)item;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public double GrossTotalCalculation(double netTotal, double stateTaxValue)
        {
            try
            {
                return netTotal + CalculatePercentage(netTotal, stateTaxValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }



        }

        public double CalculatePercentage(double itemValue, double percentageValue)
        {
            try
            {
                double TaxValue = (itemValue * percentageValue / 100);
                TaxValue = Math.Round(TaxValue, 2, MidpointRounding.AwayFromZero);
                return TaxValue;

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


        #region "dynamic resolve template Variables"

        // resolve variables in templates
        public bool ResolveTemplateVariables(long productID, CompanyContact objContact,
            List<Models.Common.TemplateVariable> lstPageControls)
        {
            try
            {
                string CompanyLogo = "";
                string ContactLogo = "";
                string LocalCompanyLogo = "";
                string LocalContactLogo = "";

                if (lstPageControls != null && lstPageControls.Count != 0)
                {
                    List<TemplateObject> tempObjs = new List<TemplateObject>();
                    tempObjs = db.TemplateObjects.Where(g => g.ProductId == productID).ToList();
                    foreach (var obj in tempObjs)
                    {
                        if (obj.ObjectType == 2)
                        {
                            List<InlineTextStyles> styles = new List<InlineTextStyles>();
                            List<InlineTextStyles> stylesCopy = new List<InlineTextStyles>();
                            if (obj.textStyles != null)
                            {
                                styles = JsonConvert.DeserializeObject<List<InlineTextStyles>>(obj.textStyles);
                                stylesCopy = JsonConvert.DeserializeObject<List<InlineTextStyles>>(obj.textStyles);
                            }
                            foreach (var objVariable in lstPageControls)
                            {
                                if (objVariable.Value != null && objVariable.Value != "")
                                {
                                    if (obj.ContentString.Contains(objVariable.Name))
                                    {
                                        if (styles.Count > 0)
                                        {
                                            string[] objs = obj.ContentString.Split(new string[] { objVariable.Name },
                                                StringSplitOptions.None);
                                            int variableLength = objVariable.Name.Length;
                                            int lengthCount = 0;
                                            string content = "";
                                            for (int i = 0; i < objs.Length; i++)
                                            {
                                                stylesCopy = new List<InlineTextStyles>(styles);
                                                content += objs[i];
                                                if ((i + 1) != objs.Length)
                                                {
                                                    content += objVariable.Value;
                                                }
                                                lengthCount += objs[i].Length;
                                                int toMove = (i + 1) * variableLength;
                                                int toCopy = lengthCount;
                                                bool styleExist = false;
                                                int stylesRemoved = 0;
                                                InlineTextStyles StyleToCopy = null;
                                                foreach (var objStyle in styles)
                                                {
                                                    if (Convert.ToInt32(objStyle.characterIndex) == toCopy)
                                                    {
                                                        styleExist = true;
                                                        StyleToCopy = objStyle;
                                                    }
                                                    if (Convert.ToInt32(objStyle.characterIndex) <=
                                                        (lengthCount + variableLength) &&
                                                        Convert.ToInt32(objStyle.characterIndex) >= lengthCount)
                                                    {
                                                        InlineTextStyles objToRemove =
                                                            stylesCopy.Where(
                                                                g => g.characterIndex == objStyle.characterIndex)
                                                                .SingleOrDefault();
                                                        stylesCopy.Remove(objToRemove);
                                                        stylesRemoved++;
                                                    }
                                                }

                                                int diff = objVariable.Value.Length - (variableLength);
                                                foreach (var objStyle in stylesCopy)
                                                {
                                                    if (Convert.ToInt32(objStyle.characterIndex) >
                                                        (lengthCount + objVariable.Name.Length))
                                                        objStyle.characterIndex =
                                                            Convert.ToString((Convert.ToInt32(objStyle.characterIndex) +
                                                                              diff));
                                                }
                                                if (styleExist)
                                                {
                                                    for (int z = 0; z < objVariable.Value.Length; z++)
                                                    {
                                                        InlineTextStyles objToAdd = new InlineTextStyles();
                                                        objToAdd.fontName = StyleToCopy.fontName;
                                                        objToAdd.fontSize = StyleToCopy.fontSize;
                                                        objToAdd.fontStyle = StyleToCopy.fontStyle;
                                                        objToAdd.fontWeight = StyleToCopy.fontWeight;
                                                        objToAdd.textColor = StyleToCopy.textColor;
                                                        objToAdd.textCMYK = StyleToCopy.textCMYK;
                                                        objToAdd.characterIndex = Convert.ToString(lengthCount + z);
                                                        stylesCopy.Add(objToAdd);

                                                    }
                                                }
                                                styles = new List<InlineTextStyles>(stylesCopy);
                                                lengthCount += objVariable.Value.Length;
                                            }
                                            obj.ContentString = content;

                                        }
                                        else
                                        {
                                            obj.ContentString = obj.ContentString.Replace(objVariable.Name,
                                                objVariable.Value);
                                        }

                                    }
                                }
                                else
                                {
                                    if (obj.ContentString.Contains(objVariable.Name))
                                    {
                                        if (styles.Count > 0)
                                        {
                                            objVariable.Value = "";
                                            string[] objs = obj.ContentString.Split(new string[] { objVariable.Name },
                                                StringSplitOptions.None);
                                            int variableLength = objVariable.Name.Length;
                                            int lengthCount = 0;
                                            string content = "";
                                            for (int i = 0; i < objs.Length; i++)
                                            {
                                                stylesCopy = new List<InlineTextStyles>(styles);
                                                content += objs[i];
                                                if ((i + 1) != objs.Length)
                                                {
                                                    content += objVariable.Value;
                                                }
                                                lengthCount += objs[i].Length;
                                                int toMove = (i + 1) * variableLength;
                                                int toCopy = lengthCount;
                                                bool styleExist = false;
                                                int stylesRemoved = 0;
                                                InlineTextStyles StyleToCopy = null;
                                                foreach (var objStyle in styles)
                                                {
                                                    if (Convert.ToInt32(objStyle.characterIndex) == toCopy)
                                                    {
                                                        styleExist = true;
                                                        StyleToCopy = objStyle;
                                                    }
                                                    if (Convert.ToInt32(objStyle.characterIndex) <=
                                                        (lengthCount + variableLength) &&
                                                        Convert.ToInt32(objStyle.characterIndex) >= lengthCount)
                                                    {
                                                        InlineTextStyles objToRemove =
                                                            stylesCopy.Where(
                                                                g => g.characterIndex == objStyle.characterIndex)
                                                                .SingleOrDefault();
                                                        stylesCopy.Remove(objToRemove);
                                                        stylesRemoved++;
                                                    }
                                                }

                                                int diff = objVariable.Value.Length - (variableLength);
                                                foreach (var objStyle in stylesCopy)
                                                {
                                                    if (Convert.ToInt32(objStyle.characterIndex) >
                                                        (lengthCount + objVariable.Name.Length))
                                                        objStyle.characterIndex =
                                                            Convert.ToString((Convert.ToInt32(objStyle.characterIndex) +
                                                                              diff));
                                                }
                                                if (styleExist)
                                                {
                                                    for (int z = 0; z < objVariable.Value.Length; z++)
                                                    {
                                                        InlineTextStyles objToAdd = new InlineTextStyles();
                                                        objToAdd.fontName = StyleToCopy.fontName;
                                                        objToAdd.fontSize = StyleToCopy.fontSize;
                                                        objToAdd.fontStyle = StyleToCopy.fontStyle;
                                                        objToAdd.fontWeight = StyleToCopy.fontWeight;
                                                        objToAdd.textColor = StyleToCopy.textColor;
                                                        objToAdd.textCMYK = StyleToCopy.textCMYK;
                                                        objToAdd.characterIndex = Convert.ToString(lengthCount + z);
                                                        stylesCopy.Add(objToAdd);

                                                    }
                                                }
                                                styles = new List<InlineTextStyles>(stylesCopy);
                                                lengthCount += objVariable.Value.Length;
                                            }
                                            obj.ContentString = content;

                                        }
                                        else
                                        {
                                            obj.ContentString = obj.ContentString.Replace(objVariable.Name, "");
                                        }

                                    }
                                }
                            }
                            if (styles != null && styles.Count != 0)
                            {
                                obj.textStyles = JsonConvert.SerializeObject(styles, Formatting.Indented);
                            }
                        }
                        else if (obj.ObjectType == 8 || obj.ObjectType == 12)
                        {
                            string filePath = "";
                            string localFilePath = "";
                            if (obj.ObjectType == 8)
                            {
                                filePath = CompanyLogo;
                                localFilePath = LocalCompanyLogo;
                            }
                            else
                            {
                                filePath = ContactLogo;
                                localFilePath = LocalContactLogo;
                            }
                            obj.ContentString = filePath;

                            try
                            {
                                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(localFilePath)))
                                {
                                    System.Drawing.Image objImage =
                                        System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath(localFilePath));
                                    int ImageWidth = objImage.Width;
                                    int ImageHeight = objImage.Height;
                                    if (ImageWidth > ImageHeight)
                                    {
                                        obj.MaxHeight = obj.MaxWidth * Convert.ToDouble(ImageHeight / ImageWidth);
                                    }
                                    else
                                    {
                                        obj.MaxWidth = obj.MaxHeight * Convert.ToDouble(ImageWidth / ImageHeight);
                                    }
                                    objImage.Dispose();
                                }

                            }
                            catch (Exception e)
                            {
                                throw e;
                            }
                        }
                        else if (obj.ObjectType == 13) //type added for smartform
                        {
                            string filePath = "";
                            string localFilePath = "";

                            filePath = CompanyLogo;
                            localFilePath = LocalCompanyLogo;

                            obj.ContentString = filePath;

                            try
                            {
                                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(localFilePath)))
                                {

                                    System.Drawing.Image objImage =
                                        System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath(localFilePath));
                                    int ImageWidth = objImage.Width;
                                    int ImageHeight = objImage.Height;
                                    if (ImageWidth > ImageHeight)
                                    {
                                        obj.MaxHeight = obj.MaxWidth * Convert.ToDouble(ImageHeight / ImageWidth);
                                    }
                                    else
                                    {
                                        obj.MaxWidth = obj.MaxHeight * Convert.ToDouble(ImageWidth / ImageHeight);
                                    }
                                    objImage.Dispose();
                                }

                            }
                            catch (Exception e)
                            {
                                throw e;
                            }
                        }

                    }
                    db.SaveChanges();
                }


                //}
                //sessionparameters

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //private bool SaveAdditionalAddonsOrUpdateStockItemType(List<AddOnCostsCenter> selectedAddonsList, long newItemID,
        //    long stockID, bool isCopyProduct, string updateMode, long ItemStockOptionId)
        //{
        //    try
        //    {
        //        bool result = false;
        //        ItemSection SelectedtblItemSectionOne = null;

        //        //Create A new Item Section #1 to pass to the cost center

        //        SelectedtblItemSectionOne =
        //            db.ItemSections.Where(itemSect => itemSect.SectionNo == 1 && itemSect.ItemId == newItemID)
        //                .FirstOrDefault();

        //        if (isCopyProduct == true)
        //        {
        //            result = this.SaveAdditionalAddonsOrUpdateStockItemType(selectedAddonsList,
        //                Convert.ToInt64(SelectedtblItemSectionOne.StockItemID1), SelectedtblItemSectionOne, updateMode, ItemStockOptionId);
        //        }
        //        else
        //        {
        //            result = this.SaveAdditionalAddonsOrUpdateStockItemType(selectedAddonsList, stockID,
        //                SelectedtblItemSectionOne, updateMode, ItemStockOptionId);
        //        }

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }


        //}

        //private bool SaveAdditionalAddonsOrUpdateStockItemType(List<AddOnCostsCenter> selectedAddonsList, long stockID,
        //    ItemSection SelectedtblItemSectionOne, string updateMode, long ItemstockOptionID)
        //{
        //    try
        //    {
        //        SectionCostcentre SelectedtblISectionCostCenteres = null;

        //        if (SelectedtblItemSectionOne != null)
        //        {
        //            //Set or Update the paper Type stockid in the section #1
        //            if (stockID > 0)
        //                this.UpdateStockItemType(SelectedtblItemSectionOne, stockID, ItemstockOptionID);

        //            if (selectedAddonsList != null)
        //            {
        //                // Remove previous Addons
        //                db.SectionCostcentres.Where(
        //                    c => c.ItemSectionId == SelectedtblItemSectionOne.ItemSectionId && c.IsOptionalExtra == 1)
        //                    .ToList()
        //                    .ForEach(sc =>
        //                    {
        //                        db.SectionCostcentres.Remove(sc);

        //                    });
        //                //Create Additional Addons Data
        //                //Create Additional Addons Data
        //                for (int i = 0; i < selectedAddonsList.Count; i++)
        //                {
        //                    AddOnCostsCenter addonCostCenter = selectedAddonsList[i];

        //                    SelectedtblISectionCostCenteres = this.PopulateTblSectionCostCenteres(addonCostCenter);
        //                    SelectedtblISectionCostCenteres.IsOptionalExtra = 1; //1 tells that it is the Additional AddOn 

        //                    SelectedtblItemSectionOne.SectionCostcentres.Add(SelectedtblISectionCostCenteres);

        //                }
        //            }
        //        }

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        //public void UpdateStockItemType(ItemSection itemSection, long stockID, long ItemstockOptionID)
        //{
        //    try
        //    {
        //        itemSection.StockItemID1 = (int)stockID; //always set into the first column
        //        itemSection.StockItemID2 = (int)ItemstockOptionID;
        //        itemSection.StockItemID3 = null;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }


        //}

        //private SectionCostcentre PopulateTblSectionCostCenteres(AddOnCostsCenter addOn)
        //{
        //    try
        //    {
        //        SectionCostcentre tblISectionCostCenteres = new SectionCostcentre
        //        {
        //            CostCentreId = addOn.CostCenterID,
        //            IsOptionalExtra = 1,
        //            Qty1Charge = addOn.Qty1NetTotal,
        //            Qty1NetTotal = addOn.Qty1NetTotal,
        //            Qty1WorkInstructions = addOn.CostCentreDescription,
        //            Qty2WorkInstructions = addOn.CostCentreJsonData,
        //            Name = addOn.AddOnName
        //        };

        //        return tblISectionCostCenteres;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        #endregion


        public Item GetItemById(long itemId)
        {
            try
            {
                return db.Items.Include("ItemPriceMatrices")
                                   .Where(i => i.IsPublished == true && i.ItemId == itemId && i.EstimateId == null)
                                   .FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //return db.Items.Include("ItemPriceMatrices").Include("ItemSections").Where(i => i.IsPublished == true && i.ItemId == itemId && i.EstimateId == null).FirstOrDefault();

        }
        public Item GetItemByItemID(long itemId)
        {
            try
            {
                return db.Items.Where(s => s.ItemId == itemId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //return db.Items.Include("ItemPriceMatrices").Include("ItemSections").Where(i => i.IsPublished == true && i.ItemId == itemId && i.EstimateId == null).FirstOrDefault();

        }
        public Item GetItemByIdDesigner(long ItemId)
        {
            try
            {
                db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;
                Item item = db.Items.Where(i => i.ItemId == ItemId).FirstOrDefault();
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public Item GetItemByTemplateIdDesigner(long templateId)
        {
            try
            {
                db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;

                return db.Items.Where(i => i.TemplateId == templateId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public ProductItem GetItemAndDetailsByItemID(long itemId)
        {
            try
            {
                var query =
             from item in db.Items
             join productCatItem in db.ProductCategoryItems on item.ItemId equals productCatItem.ItemId
             join category in db.ProductCategories on productCatItem.CategoryId equals category.ProductCategoryId
             join ItemDetail in db.ItemProductDetails on item.ItemId equals (long)ItemDetail.ItemId
             where item.ItemId == itemId
             select new ProductItem
             {

                 ProductName = item.ProductName,
                 ThumbnailPath = item.ThumbnailPath,
                 ProductCategoryName = category.CategoryName,
                 ProductSpecification = item.WebDescription,
                 AllowBriefAttachments = ItemDetail.isAllowMarketBriefAttachment ?? false,
                 BriefSuccessMessage = ItemDetail.MarketBriefSuccessMessage

             };
                return query.FirstOrDefault<ProductItem>();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<ProductMarketBriefQuestion> GetMarketingInquiryQuestionsByItemID(int itemID)
        {
            try
            {
                return db.ProductMarketBriefQuestions.Where(i => i.ItemId == itemID).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public List<ProductMarketBriefAnswer> GetMarketingInquiryAnswersByQID(int QID)
        {
            try
            {
                return db.ProductMarketBriefAnswers.Where(i => i.MarketBriefQuestionId == QID).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        //public void CopyAttachments(int itemID, Item NewItem, string OrderCode, bool CopyTemplate,
        //    DateTime OrderCreationDate)
        //{
        //    try
        //    {
        //        int sideNumber = 1;
        //        List<ItemAttachment> attchmentRes = GetItemAttactchments(itemID);
        //        List<ItemAttachment> Newattchments = new List<ItemAttachment>();
        //        ItemAttachment obj = null;

        //        foreach (ItemAttachment attachment in attchmentRes)
        //        {
        //            obj = new ItemAttachment();

        //            obj.ApproveDate = attachment.ApproveDate;
        //            obj.Comments = attachment.Comments;
        //            obj.ContactId = attachment.ContactId;
        //            obj.ContentType = attachment.ContentType;
        //            obj.CompanyId = attachment.CompanyId;
        //            obj.FileTitle = attachment.FileTitle;
        //            obj.FileType = attachment.FileType;
        //            obj.FolderPath = attachment.FolderPath;
        //            obj.IsApproved = attachment.IsApproved;
        //            obj.isFromCustomer = attachment.isFromCustomer;
        //            obj.Parent = attachment.Parent;
        //            obj.Type = attachment.Type;
        //            obj.UploadDate = attachment.UploadDate;
        //            obj.Version = attachment.Version;
        //            obj.ItemId = NewItem.ItemId;
        //            if (NewItem.TemplateId > 0)
        //            {
        //                obj.FileName = GetTemplateAttachmentFileName(NewItem.ProductCode, OrderCode, NewItem.ItemCode,
        //                    "Side" + sideNumber.ToString(), attachment.FolderPath, "", OrderCreationDate);
        //                //NewItemID + " Side" + sideNumber + attachment.FileType;
        //            }
        //            else
        //            {
        //                obj.FileName = GetAttachmentFileName(NewItem.ProductCode, OrderCode, NewItem.ItemCode,
        //                    sideNumber.ToString() + "Copy", attachment.FolderPath, "", OrderCreationDate);
        //                //NewItemID + " Side" + sideNumber + attachment.FileType;
        //            }
        //            sideNumber += 1;
        //            db.ItemAttachments.Add(obj);
        //            Newattchments.Add(obj);

        //            // Copy physical file
        //            string sourceFileName = null;
        //            string destFileName = null;
        //            if (NewItem.TemplateId > 0 && CopyTemplate == true)
        //            {
        //                sourceFileName =
        //                    HttpContext.Current.Server.MapPath(attachment.FolderPath +
        //                                                       System.IO.Path.GetFileNameWithoutExtension(
        //                                                           attachment.FileName) + "Thumb.png");
        //                destFileName = HttpContext.Current.Server.MapPath(obj.FolderPath + obj.FileName + "Thumb.png");
        //            }
        //            else
        //            {
        //                sourceFileName = HttpContext.Current.Server.MapPath(attachment.FolderPath + "/" + attachment.FileName + attachment.FileType);
        //                destFileName = HttpContext.Current.Server.MapPath(obj.FolderPath + "/" + obj.FileName + obj.FileType);
        //            }

        //            if (File.Exists(sourceFileName))
        //            {
        //                File.Copy(sourceFileName, destFileName);

        //                // Generate the thumbnail

        //                byte[] fileData = File.ReadAllBytes(destFileName);

        //                if (obj.FileType == ".pdf" || obj.FileType == ".TIF" || obj.FileType == ".TIFF")
        //                {
        //                    GenerateThumbnailForPdf(fileData, destFileName, false);
        //                }
        //                else
        //                {
        //                    MemoryStream ms = new MemoryStream();
        //                    ms.Write(fileData, 0, fileData.Length);

        //                    CreatAndSaveThumnail(ms, destFileName);
        //                }
        //            }

        //        }

        //        db.SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}
        /// <summary>
        /// Gets the attchment list
        /// </summary>
        /// <param name="itemID"></param>
        /// <returns></returns>
        public List<ItemAttachment> GetItemAttactchments(long itemID)
        {
            try
            {

                return (from Attachment in db.ItemAttachments
                        where Attachment.ItemId == itemID
                        select Attachment).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<ArtWorkAttatchment> GetItemAttactchments(long itemID, string fileExtionsion,
            UploadFileTypes uploadedFileType)
        {
            try
            {
                string uploadFiType = uploadedFileType.ToString();
                List<ArtWorkAttatchment> itemAttactchments = new List<ArtWorkAttatchment>();

                db.Configuration.LazyLoadingEnabled = false;
                var query = from Attachment in db.ItemAttachments
                            where Attachment.ItemId == itemID &&
                                  string.Compare(Attachment.Type, uploadFiType, true) == 0 &&
                                  string.Compare(Attachment.FileType, fileExtionsion, true) == 0
                            select new ArtWorkAttatchment()
                            {
                                FileName = Attachment.FileName,
                                FileTitle = Attachment.FileTitle,
                                FileExtention = Attachment.FileType,
                                FolderPath = Attachment.FolderPath,
                            };

                itemAttactchments = query.ToList<ArtWorkAttatchment>();



                if (itemAttactchments != null && itemAttactchments.Count > 0)
                    itemAttactchments.ForEach(att => att.UploadFileType = uploadedFileType);


                return itemAttactchments;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public static string GetTemplateAttachmentFileName(string ProductCode, string OrderCode, string ItemCode,
            string SideCode, string VirtualFolderPath, string extension, DateTime CreationDate)
        {
            try
            {
                string FileName = CreationDate.Year.ToString() + CreationDate.ToString("MMMM") + CreationDate.Day.ToString() +
                             "-" + ProductCode + "-" + OrderCode + "-" + ItemCode + "-" + SideCode + extension;

                return FileName;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static string GetAttachmentFileName(string ProductCode, string OrderCode, string ItemCode,
            string SideCode, string VirtualFolderPath, string extension, DateTime OrderCreationDate)
        {
            try
            {
                string FileName = OrderCreationDate.Year.ToString() + OrderCreationDate.ToString("MMMM") +
                             OrderCreationDate.Day.ToString() + "-" + ProductCode + "-" + OrderCode + "-" + ItemCode +
                             "-" + SideCode + extension;
                //checking whether file exists or not
                while (System.IO.File.Exists(VirtualFolderPath + FileName))
                {
                    string fileName1 = System.IO.Path.GetFileNameWithoutExtension(FileName);
                    fileName1 += "a";
                    FileName = fileName1 + extension;
                }
                return FileName;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void GenerateThumbnailForPdf(byte[] PDFFile, string sideThumbnailPath, bool insertCuttingMargin)
        {
            try
            {
                //
                using (Doc theDoc = new Doc())
                {
                    theDoc.Read(PDFFile);
                    theDoc.PageNumber = 1;
                    theDoc.Rect.String = theDoc.CropBox.String;

                    if (insertCuttingMargin)
                    {
                        theDoc.Rect.Inset((int)MPC.Models.Common.Constants.CuttingMargin,
                            (int)MPC.Models.Common.Constants.CuttingMargin);
                    }

                    Stream oImgstream = new MemoryStream();

                    theDoc.Rendering.DotsPerInch = 300;
                    theDoc.Rendering.Save("tmp.png", oImgstream);

                    theDoc.Clear();
                    theDoc.Dispose();

                    CreatAndSaveThumnail(oImgstream, sideThumbnailPath);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool CreatAndSaveThumnail(Stream oImgstream, string sideThumbnailPath)
        {
            try
            {
                string baseAddress = sideThumbnailPath.Substring(0, sideThumbnailPath.LastIndexOf('\\'));
                sideThumbnailPath = Path.GetFileNameWithoutExtension(sideThumbnailPath) + "Thumb.png";

                sideThumbnailPath = baseAddress + "\\" + sideThumbnailPath;

                Image origImage = Image.FromStream(oImgstream);

                float WidthPer, HeightPer;

                int NewWidth, NewHeight;
                int ThumbnailSizeWidth = 400;
                int ThumbnailSizeHeight = 400;

                if (origImage.Width > origImage.Height)
                {
                    NewWidth = ThumbnailSizeWidth;
                    WidthPer = (float)ThumbnailSizeWidth / origImage.Width;
                    NewHeight = Convert.ToInt32(origImage.Height * WidthPer);
                }
                else
                {
                    NewHeight = ThumbnailSizeHeight;
                    HeightPer = (float)ThumbnailSizeHeight / origImage.Height;
                    NewWidth = Convert.ToInt32(origImage.Width * HeightPer);
                }

                Bitmap origThumbnail = new Bitmap(NewWidth, NewHeight, origImage.PixelFormat);
                Graphics oGraphic = Graphics.FromImage(origThumbnail);
                oGraphic.CompositingQuality = CompositingQuality.HighQuality;
                oGraphic.SmoothingMode = SmoothingMode.HighQuality;
                oGraphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                Rectangle oRectangle = new Rectangle(0, 0, NewWidth, NewHeight);
                oGraphic.DrawImage(origImage, oRectangle);


                origThumbnail.Save(sideThumbnailPath, ImageFormat.Png);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool RemoveCloneItem(long itemID, out List<ArtWorkAttatchment> itemAttatchmetList,
            out Template clonedTemplateToRemove)
        {
            try
            {
                bool result = false;
                clonedTemplateToRemove = null;
                itemAttatchmetList = null;

                Item tblItem = db.Items.Where(item => item.ItemId == itemID).FirstOrDefault();
                if (tblItem != null)
                {
                    if (RemoveCloneItem(tblItem, out itemAttatchmetList, out clonedTemplateToRemove))
                        result = db.SaveChanges() > 0 ? true : false;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


        public bool RemoveCloneItem(Item tblItem, out List<ArtWorkAttatchment> itemAttatchmetList,
            out Template clonedTemplateToRemove)
        {
            bool result = false;
            List<ArtWorkAttatchment> itemAttatchments = null;
            Template clonedTemplate = null;

            try
            {
                if (tblItem != null)
                {
                    //tbl_items tblRefItem = GetItemById(Convert.ToInt32(tblItem.RefItemID));
                    //if (tblRefItem.isStockControl == true)
                    //{
                    //    tbl_items_StockControl tblItemStock = GetStockOfItemById(dbContext, Convert.ToInt32(tblRefItem.ItemID));
                    //    if (tblItemStock != null)
                    //    {
                    //        if (tblItemStock.InStock > tblItemStock.ThresholdProductionQuantity || tblItemStock.ThresholdProductionQuantity == null)
                    //        {
                    //            tblItemStock.InStock = tblItemStock.InStock + 1;
                    //        }
                    //    }
                    //}
                    itemAttatchments = new List<ArtWorkAttatchment>();

                    //Delete Attachments                       
                    tblItem.ItemAttachments.ToList().ForEach(att =>
                    {
                        db.ItemAttachments.Remove(att);
                        //if (att.FileType == ".pdf")
                        itemAttatchments.Add(PopulateUploadedAttactchment(att)); // gathers attatments list as well.
                    });

                    //Remove the Templates if he has designed any
                    if (!ValidateIfTemplateIDIsAlreadyBooked(tblItem.ItemId, tblItem.TemplateId))
                        clonedTemplate =
                            RemoveTemplates(tblItem.TemplateId.HasValue ? (int)tblItem.TemplateId : (int?)null);

                    //Section cost centeres
                    //tblItem.ItemSections.ToList().ForEach(itemSection => itemSection.SectionCostcentres.ToList().ForEach(sectCost => db.SectionCostcentres.Remove(sectCost)));
                    List<ItemSection> listOfSections = db.ItemSections.Where(s => s.ItemId == tblItem.ItemId).ToList();
                    foreach (var itemSection in listOfSections)
                    {
                        List<SectionCostcentre> listOfSectionCC =
                            db.SectionCostcentres.Where(sec => sec.ItemSectionId == itemSection.ItemSectionId).ToList();
                        if (listOfSectionCC != null)
                        {
                            listOfSectionCC.ToList().ForEach(SectionCC =>
                            {
                                db.SectionCostcentres.Remove(SectionCC);
                            });
                        }

                        db.ItemSections.Remove(itemSection);
                    }

                    //Item Section
                    //  tblItem.ItemSections.ToList().ForEach(itemsect => db.ItemSections.Remove(itemsect));
                    //Finally the item
                    db.Items.Remove(tblItem);

                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            itemAttatchmetList = itemAttatchments;
            clonedTemplateToRemove = clonedTemplate;
            return result;
        }

        public Template RemoveTemplates(int? templateID)
        {
            try
            {
                Template clonedTemplate = null;

                if (templateID.HasValue && templateID.Value > 0)
                {
                    Template tblTemplate =
                        db.Templates.Where(template => template.ProductId == templateID.Value).FirstOrDefault();

                    if (tblTemplate != null)
                    {
                        if (tblTemplate.TemplateColorStyles != null)
                        {
                            if (tblTemplate.TemplateColorStyles.Count > 0)
                            {
                                tblTemplate.TemplateColorStyles.ToList()
                                    .ForEach(tempColorStyle => db.TemplateColorStyles.Remove(tempColorStyle));
                            }
                        }
                        //backgourd
                        if (tblTemplate.TemplateBackgroundImages != null)
                        {
                            if (tblTemplate.TemplateBackgroundImages.Count > 0)
                            {
                                tblTemplate.TemplateBackgroundImages.ToList()
                                    .ForEach(tempBGImages => db.TemplateBackgroundImages.Remove(tempBGImages));
                            }


                        }
                        //font
                        if (tblTemplate.TemplateFonts != null)
                        {
                            if (tblTemplate.TemplateFonts.Count > 0)
                            {
                                tblTemplate.TemplateFonts.ToList().ForEach(tempFonts => db.TemplateFonts.Remove(tempFonts));
                            }

                        }
                        //object
                        if (tblTemplate.TemplateObjects != null)
                        {
                            if (tblTemplate.TemplateObjects.Count > 0)
                            {
                                tblTemplate.TemplateObjects.ToList().ForEach(tempObj => db.TemplateObjects.Remove(tempObj));
                            }

                        }
                        //Page
                        if (tblTemplate.TemplatePages != null)
                        {
                            if (tblTemplate.TemplatePages.Count > 0)
                            {
                                tblTemplate.TemplatePages.ToList().ForEach(tempPage => db.TemplatePages.Remove(tempPage));
                            }

                        }

                        // the template to remove the files in web.ui
                        clonedTemplate = Clone<Template>(tblTemplate);

                        //finally template it self
                        db.Templates.Remove(tblTemplate);
                    }
                }

                return clonedTemplate;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public ArtWorkAttatchment PopulateUploadedAttactchment(ItemAttachment attatchment)
        {
            try
            {

                UploadFileTypes resultUploadedFileType;

                ArtWorkAttatchment itemAttactchment = new ArtWorkAttatchment()
                {
                    FileName = attatchment.FileName,
                    FileTitle = attatchment.FileTitle,
                    FileExtention = attatchment.FileType,
                    FolderPath = attatchment.FolderPath,
                    UploadFileType =
                        Enum.TryParse(attatchment.Type, true, out resultUploadedFileType)
                            ? resultUploadedFileType
                            : UploadFileTypes.None
                };


                return itemAttactchment;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private bool ValidateIfTemplateIDIsAlreadyBooked(long itemID, long? templateID)
        {
            try
            {
                bool result = false;

                if (templateID.HasValue && templateID > 0)
                {
                    int bookedCount =
                        db.Items.Where(item => item.ItemId != itemID && item.TemplateId == templateID.Value).Count();
                    if (bookedCount > 0)
                        result = true;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        // Get Related Items List
        public List<ProductItem> GetRelatedItemsList(long ItemId)
        {
            try
            {
                List<long?> itemIds = db.ItemRelatedItems.Where(i => i.ItemId == ItemId).Select(r => r.RelatedItemId).ToList();
                var query = from productsList in db.GetCategoryProducts
                           // join tblRelItems in db.ItemRelatedItems on productsList.ItemId equals tblRelItems.RelatedItemId
                            where
                            itemIds.Contains(productsList.ItemId) && 
                                productsList.IsPublished == true && productsList.EstimateId == null &&
                                productsList.IsEnabled == true// && tblRelItems.ItemId == ItemId 
                                //productsList.ItemId == tblRelItems.RelatedItemId

                            select new ProductItem
                            {
                                ItemID = productsList.ItemId,
                               // RelatedItemID = tblRelItems.RelatedItemId ?? 0,
                                EstimateID = productsList.EstimateId,
                                ProductName = productsList.ProductName,
                                ProductCategoryName = productsList.ProductCategoryName,
                                ProductCategoryID = productsList.ProductCategoryId,
                                MinPrice = productsList.MinPrice,
                                ImagePath = productsList.ImagePath,
                                ThumbnailPath = productsList.ThumbnailPath,
                                IconPath = productsList.IconPath,
                                IsEnabled = productsList.IsEnabled,
                                IsSpecialItem = productsList.IsSpecialItem,
                                IsPopular = productsList.IsPopular,
                                IsFeatured = productsList.IsFeatured,
                                IsPromotional = productsList.IsPromotional,
                                IsPublished = productsList.IsPublished,
                                ProductSpecification = productsList.ProductSpecification,
                                CompleteSpecification = productsList.CompleteSpecification,
                                ProductType = productsList.ProductType,
                                isUploadImage = productsList.isUploadImage ?? false,
                                ItemFriendlyName = productsList.ItemFriendlyName
                            };
                return query.ToList<ProductItem>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// get an item according to usercookiemanager.orderid or itemid 
        /// </summary>
        /// <param name="ItemID"></param>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public Item GetItemByOrderAndItemID(long ItemID, long OrderID)
        {
            try
            {
                return
              db.Items.Where(g => g.RefItemId == ItemID && g.EstimateId == OrderID && g.IsOrderedItem == false)
                  .FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// to find the minimun price of specific Product by itemid
        /// </summary>
        /// <param name="curProduct"></param>
        /// <returns></returns>
        public double FindMinimumPriceOfProduct(long itemID)
        {
            try
            {
                GetCategoryProduct products = (from c in db.GetCategoryProducts
                                               where c.ItemId == itemID
                                               select c).FirstOrDefault();
                if (products != null)
                {
                    return products.MinPrice;
                }
                else
                {
                    return 0;
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


        //public bool UpdateCloneItem(long clonedItemID, double orderedQuantity, double itemPrice, double addonsPrice,
        //    long stockItemID, List<AddOnCostsCenter> newlyAddedCostCenters, int Mode, long OrganisationId,
        //    double TaxRate, string ItemMode, bool isInculdeTax, long ItemstockOptionID, int CountOfUploads = 0, string QuestionQueue = "", string CostCentreQueue = "", string InputQueue = "")
        //{
        //try
        //{
        //    bool result = false;

        //    ItemSection FirstItemSection = null;

        //    double currentTotal = 0;
        //    double netTotal = 0;
        //    double grossTotal = 0;
        //    double? markupRate = 0;

        //    try
        //    {
        //        Item clonedItem = null;

        //        clonedItem = db.Items.Where(i => i.ItemId == clonedItemID).FirstOrDefault();

        //        long? markupid = 1;


        //        Markup OrgMarkup = db.Markups.Where(m => m.OrganisationId == OrganisationId && m.IsDefault == true).FirstOrDefault();

        //        if (OrgMarkup != null)
        //        {
        //            markupid = 0;//OrgMarkup.MarkUpId;
        //            markupRate = 0;//(int)OrgMarkup.MarkUpRate;
        //        }
        //        else
        //        {
        //            markupid = 0;
        //            markupRate = 0;
        //        }

        //        if (CountOfUploads > 0)
        //        {
        //            clonedItem.ProductName = clonedItem.ProductName + " " + CountOfUploads + " file(s) uploaded";
        //        }

        //        clonedItem.Qty1 = (int)orderedQuantity;

        //        clonedItem.IsOrderedItem = true;



        //        if (isInculdeTax == true)
        //        {
        //            if (clonedItem.DefaultItemTax != null)
        //            {
        //                clonedItem.Tax1 = Convert.ToInt32(clonedItem.DefaultItemTax);
        //                double TaxAppliedOnItemTotal = CalculatePercentage(itemPrice, Convert.ToDouble(clonedItem.DefaultItemTax));// ((itemPrice * Convert.ToDouble(clonedItem.DefaultItemTax)) / 100); 

        //                double TaxAppliedOnCostCentreTotal = CalculatePercentage(addonsPrice, Convert.ToDouble(clonedItem.DefaultItemTax));// ((addonsPrice * Convert.ToDouble(clonedItem.DefaultItemTax)) / 100);

        //                itemPrice = itemPrice;

        //                netTotal = itemPrice + addonsPrice + markupRate ?? 0;

        //                grossTotal = netTotal + (TaxAppliedOnItemTotal + TaxAppliedOnCostCentreTotal);
        //                clonedItem.Qty1Tax1Value = (TaxAppliedOnItemTotal + TaxAppliedOnCostCentreTotal);//GetTaxPercentage(netTotal, Convert.ToDouble(clonedItem.DefaultItemTax));
        //            }
        //            else
        //            {
        //                clonedItem.Tax1 = Convert.ToInt32(TaxRate);
        //                double TaxAppliedOnItemTotal = CalculatePercentage(itemPrice, TaxRate); //(itemPrice * TaxRate / 100);
        //                double TaxAppliedOnCostCentreTotal = CalculatePercentage(addonsPrice, TaxRate); //(addonsPrice * TaxRate / 100);
        //                itemPrice = itemPrice;

        //                netTotal = itemPrice + addonsPrice + markupRate ?? 0;

        //                grossTotal = netTotal + (TaxAppliedOnItemTotal + TaxAppliedOnCostCentreTotal);//CalculatePercentage(netTotal, TaxRate);
        //                clonedItem.Qty1Tax1Value = TaxAppliedOnItemTotal + TaxAppliedOnCostCentreTotal;//GetTaxPercentage(netTotal, TaxRate);
        //            }
        //        }
        //        else
        //        {
        //            clonedItem.Tax1 = Convert.ToInt32(TaxRate);

        //            netTotal = itemPrice + addonsPrice + markupRate ?? 0;

        //            GetStoreDefaultDiscountRate();

        //            grossTotal = netTotal + CalculatePercentage(netTotal, TaxRate);
        //            clonedItem.Qty1Tax1Value = GetTaxPercentage(netTotal, TaxRate);
        //        }


        //        //******************Existing item update*********************
        //        clonedItem.Qty1MarkUp1Value = markupRate;

        //        clonedItem.Qty1MarkUpId1 = (int)markupid;

        //        clonedItem.Qty1BaseCharge1 = netTotal;

        //        clonedItem.Qty1NetTotal = netTotal;

        //        clonedItem.Qty1GrossTotal = grossTotal;

        //        FirstItemSection =
        //            clonedItem.ItemSections.Where(sec => sec.SectionNo == 1 && sec.ItemId == clonedItem.ItemId)
        //                .FirstOrDefault();

        //        result = SaveAdditionalAddonsOrUpdateStockItemType(newlyAddedCostCenters, stockItemID, FirstItemSection,
        //            ItemMode, ItemstockOptionID); // additional addon required the newly inserted cloneditem

        //        FirstItemSection.Qty1 = clonedItem.Qty1;

        //        FirstItemSection.BaseCharge1 = clonedItem.Qty1BaseCharge1;



        //        FirstItemSection.Qty1MarkUpID = (int)markupid;
        //        FirstItemSection.QuestionQueue = QuestionQueue;
        //        FirstItemSection.InputQueue = InputQueue;
        //        FirstItemSection.CostCentreQueue = CostCentreQueue;


        //        bool isNewSectionCostCenter = false;


        //        List<SectionCostcentre> listOfSectionCostCentres = db.SectionCostcentres.Where(c => c.ItemSectionId == FirstItemSection.ItemSectionId).ToList();

        //        SectionCostcentre sectionCC = null;
        //        foreach (var ccItem in listOfSectionCostCentres)
        //        {
        //            if (ccItem.CostCentre != null)
        //            {
        //                if (ccItem.CostCentre.Type == 29)
        //                {
        //                    sectionCC = ccItem;
        //                }
        //            }
        //        }


        //        if (sectionCC == null)
        //        {
        //            sectionCC = new SectionCostcentre();

        //            sectionCC.Qty1MarkUpID = 1;
        //            sectionCC.Qty1Charge = itemPrice;
        //            sectionCC.Qty1NetTotal = itemPrice;

        //            isNewSectionCostCenter = true;
        //        }

        //        if (isNewSectionCostCenter)
        //        {
        //            //29 is the global type of web order cost centre
        //            var oCostCentre = db.CostCentres.Where(g => g.Type == 29 && g.OrganisationId == OrganisationId).SingleOrDefault();
        //            if (oCostCentre != null)
        //            {
        //                sectionCC.Name = oCostCentre.Name;
        //                sectionCC.CostCentreId = oCostCentre.CostCentreId;
        //                sectionCC.ItemSectionId = FirstItemSection.ItemSectionId;
        //                FirstItemSection.SectionCostcentres.Add(sectionCC);
        //            }
        //            else
        //            {
        //                throw new Exception("Critcal Error, We have lost our main costcentre.", null);
        //            }
        //        }

        //        if (result)
        //            result = db.SaveChanges() > 0 ? true : false;

        //    }
        //    catch (Exception)
        //    {
        //        result = false;
        //        throw;
        //    }

        //    return result;
        //}
        //catch (Exception ex)
        //{
        //    throw ex;
        //}


        //}

        private double GetTaxPercentage(double netTotal, double TaxRate)
        {
            try
            {
                return (netTotal * TaxRate) / 100;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public Item GetClonedItemByOrderId(long OrderId, long ReferenceItemId)
        {
            try
            {
                return
               db.Items.Include("ItemAttachments").Where(
                   i => i.EstimateId == OrderId && i.RefItemId == ReferenceItemId && i.IsOrderedItem == false)
                   .FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


        /// <summary>
        /// Get Minimum product value
        /// </summary>
        public double GetMinimumProductValue(long itemId)
        {
            try
            {
                return db.GetMinimumProductValue(itemId);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public List<ProductItem> GetRelatedItemsByItemID(long ItemID)
        {
            try
            {
                var query = from productsList in db.GetCategoryProducts
                            join tblRelItems in db.ItemRelatedItems on productsList.ItemId equals tblRelItems.RelatedItemId
                            join r in db.Items on tblRelItems.ItemId equals r.ItemId
                            //into tblRelatedGroupJoin
                            where r.ItemId == ItemID

                            //from JTble in tblRelatedGroupJoin.DefaultIfEmpty()
                            select new ProductItem
                            {
                                ItemID = productsList.ItemId,
                                //  RelatedItemID = JTble.RelatedItemID.HasValue ? JTble.RelatedItemID.Value : 0,
                                EstimateID = productsList.EstimateId,
                                ProductName = productsList.ProductName,
                                ProductCategoryName = productsList.ProductCategoryName,
                                ProductCategoryID = productsList.ProductCategoryId,
                                MinPrice = productsList.MinPrice,
                                ImagePath = productsList.ImagePath,
                                ThumbnailPath = productsList.ThumbnailPath,
                                IconPath = productsList.IconPath,
                                IsEnabled = productsList.IsEnabled,
                                IsSpecialItem = productsList.IsSpecialItem,
                                IsPopular = productsList.IsPopular,
                                IsFeatured = productsList.IsFeatured,
                                IsPromotional = productsList.IsPromotional,
                                IsPublished = productsList.IsPublished,
                                ProductSpecification = productsList.ProductSpecification,
                                CompleteSpecification = productsList.CompleteSpecification,
                                //TipsAndHints = productsList.ti,
                                ProductType = productsList.ProductType
                            };


                return query.ToList<ProductItem>();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// get flag of default section price
        /// </summary>
        /// <returns></returns>
        public int GetDefaultSectionPriceFlag()
        {
            try
            {
                return
                    db.SectionFlags.Where(i => i.SectionId == 81 && i.isDefault == true)
                        .Select(o => o.SectionFlagId)
                        .FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw ex;
            }



        }

        public List<ItemImage> getItemImagesByItemID(long ItemID)
        {
            try
            {
                return db.ItemImages.Where(g => g.ItemId == ItemID).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Replace temporary Customer and cart with real customer
        /// </summary>
        /// <param name="dummyCustomerID"></param>
        /// <param name="realCustomerID"></param>
        /// <param name="realContactID"></param>
        /// <param name="replacedOrderdID"></param>
        /// <param name="orderAllItemsAttatchmentsListToBeRemoved"></param>
        /// <param name="clonedTemplateToRemoveList"></param>
        /// <returns></returns>
        public long UpdateTemporaryCustomerOrderWithRealCustomer(long TemporaryCustomerID, long realCustomerID,
            long realContactID, long replacedOrderdID, long OrganisationId, double StoreTaxRate, long StoreId, 
            out List<ArtWorkAttatchment> orderAllItemsAttatchmentsListToBeRemoved,
            out List<Template> clonedTemplateToRemoveList)
        {
            try
            {
                string SourceTargetFolder = "";
                db.Configuration.LazyLoadingEnabled = false;
                Estimate TemporaryOrder = null;

                Estimate ActualOrder = null;

                CompanyContact TemporaryContact = null;

                CompanyContact ActualContact = null;

                long orderID = 0;

                List<TemplateBackgroundImage> TemporaryBackgroundImageRec = null;

                orderAllItemsAttatchmentsListToBeRemoved = null;

                clonedTemplateToRemoveList = null;

                //Loads the dummy customer complete order
                TemporaryOrder =
                    db.Estimates.Where(
                        order =>
                            order.EstimateId == replacedOrderdID && order.StatusId == (short)OrderStatus.ShoppingCart &&
                            order.isEstimate == false).FirstOrDefault();
                ActualOrder =
                    db.Estimates.Where(
                        order =>
                            order.CompanyId == realCustomerID && order.ContactId == realContactID &&
                            order.StatusId == (short)OrderStatus.ShoppingCart && order.isEstimate == false)
                        .FirstOrDefault();
                if (ActualOrder == null)
                {
                    ActualOrder = new Estimate();
                }
                if (ActualOrder != null && TemporaryOrder != null)
                {
                    ActualOrder.CreationTime = DateTime.Now;

                    List<SectionFlag> flagsList = db.SectionFlags.Where(s => s.OrganisationId == OrganisationId && s.FlagName.Contains("Order")).ToList();
                    if (flagsList != null && flagsList.Count > 0)
                    {
                        ActualOrder.SectionFlagId = flagsList.FirstOrDefault().SectionFlagId;
                    }
                    else
                    {
                        throw new Exception("Critcal Error, We have lost our main Order flags.", null);
                    }

                   
                    ActualOrder.LockedBy = Convert.ToInt32(realContactID);
                    ActualOrder.CompanyId = realCustomerID;
                    TemporaryContact =
                        db.CompanyContacts.Where(i => i.CompanyId == TemporaryOrder.CompanyId).FirstOrDefault();

                    // this will check if user has already an order with discount coupon
                    long previousOrderDiscountVoucherId = ActualOrder.DiscountVoucherID ?? 0;
                    if (previousOrderDiscountVoucherId > 0) 
                    {
                        ActualOrder.DiscountVoucherID = TemporaryOrder.DiscountVoucherID;
                        ActualOrder.VoucherDiscountRate = TemporaryOrder.VoucherDiscountRate;
                        RollBackSpecificDiscountedItemsByVoucherId(ActualOrder.EstimateId, StoreTaxRate, StoreId, OrganisationId, previousOrderDiscountVoucherId);

                    }
                 
                   
                    if (TemporaryContact != null)
                    {
                        ActualContact = db.CompanyContacts.Where(i => i.ContactId == realContactID).FirstOrDefault();

                        ActualContact.quickAddress1 = TemporaryContact.quickAddress1;
                        ActualContact.quickAddress2 = TemporaryContact.quickAddress2;
                        ActualContact.quickAddress3 = TemporaryContact.quickAddress3;
                        ActualContact.quickCompanyName = TemporaryContact.quickCompanyName;
                        ActualContact.quickCompMessage = TemporaryContact.quickCompMessage;
                        ActualContact.quickEmail = TemporaryContact.quickEmail;
                        ActualContact.quickFullName = TemporaryContact.quickFullName;
                        ActualContact.quickPhone = TemporaryContact.quickPhone;
                        ActualContact.quickTitle = TemporaryContact.quickTitle;
                        ActualContact.quickWebsite = TemporaryContact.quickWebsite;

                        TemporaryBackgroundImageRec =
                            db.TemplateBackgroundImages.Where(
                                i =>
                                    i.ContactCompanyId == TemporaryOrder.CompanyId &&
                                    i.ContactId == TemporaryContact.ContactId).ToList();

                        foreach (var temImge in TemporaryBackgroundImageRec)
                        {
                            temImge.ContactId = realContactID;
                            temImge.ContactCompanyId = realCustomerID;
                        }
                    }



                    if (TemporaryOrder != null)
                    {
                        if (ActualOrder != null && ActualOrder.EstimateId > 0)
                        {
                            orderID = ActualOrder.EstimateId;
                        }

                        List<Item> TemporaryOrderItems =
                            db.Items.Include("ItemAttachments")
                                .Where(
                                    i =>
                                        i.EstimateId == TemporaryOrder.EstimateId &&
                                        i.StatusId == (short)OrderStatus.ShoppingCart)
                                .ToList();

                        //Attatchments
                        if (ActualOrder == null)
                        {
                            TemporaryOrderItems.ToList().ForEach(item =>
                            {
                                item.ItemAttachments.ToList().ForEach(attatchment =>
                                {
                                    attatchment.CompanyId = realCustomerID;
                                    attatchment.ContactId = realContactID;
                                });
                            });
                        }
                        else
                        {
                            TemporaryOrderItems.ToList().ForEach(item =>
                            {
                                int PageNo = 1;
                                item.ItemAttachments.ToList().ForEach(attatchment =>
                                {
                                    if (item.TemplateId != null && item.TemplateId > 0)
                                    {
                                        string Actualfilenamepdf = attatchment.FileName + attatchment.FileType;

                                        string Sourcefilenamepdf =
                                            HttpContext.Current.Server.MapPath(attatchment.FolderPath + "/" +
                                                                               attatchment.FileName + attatchment.FileType);
                                        string newfilenamepdf = GetTemplateAttachmentFileName(item.ProductCode,
                                            ActualOrder.Order_Code, item.ItemCode, "Side" + PageNo.ToString(), "",
                                            ".pdf", TemporaryOrder.CreationDate ?? DateTime.Now);

                                        SourceTargetFolder = System.Web.HttpContext.Current.Server.MapPath(attatchment.FolderPath + "/");
                                        string destinationTargetFolder = System.Web.HttpContext.Current.Server.MapPath(attatchment.FolderPath + "/");

                                        string destnationfilepdf =
                                            HttpContext.Current.Server.MapPath(attatchment.FolderPath + "/" + newfilenamepdf);

                                        if (!System.IO.Directory.Exists(destinationTargetFolder))
                                        {
                                            System.IO.Directory.CreateDirectory(destinationTargetFolder);
                                        }

                                        System.IO.File.Move(Sourcefilenamepdf, destnationfilepdf);

                                        string Sourcefilenamepng =
                                            HttpContext.Current.Server.MapPath(attatchment.FolderPath + "/" +
                                                                               attatchment.FileName + "Thumb.png");
                                        string newfilenamepng = GetTemplateAttachmentFileName(item.ProductCode,
                                            ActualOrder.Order_Code, item.ItemCode, "Side" + PageNo.ToString(), "",
                                            "Thumb.png", TemporaryOrder.CreationDate ?? DateTime.Now);
                                        string destnationfilepng =
                                            HttpContext.Current.Server.MapPath(attatchment.FolderPath + "/" + newfilenamepng);
                                        System.IO.File.Move(Sourcefilenamepng, destnationfilepng);
                                        attatchment.FileName = System.IO.Path.GetFileNameWithoutExtension(newfilenamepdf);
                                        attatchment.FolderPath = attatchment.FolderPath;
                                    }
                                    attatchment.CompanyId = realCustomerID;
                                    attatchment.ContactId = realContactID;

                                    PageNo = PageNo + 1;

                                });
                            });
                        }

                        //if (System.IO.Directory.Exists(SourceTargetFolder) && !string.IsNullOrEmpty(SourceTargetFolder))
                        //{
                        //    System.IO.Directory.Delete(SourceTargetFolder, true);
                        //}

                        //item
                        TemporaryOrderItems.ToList().ForEach(item =>
                        {
                            item.CompanyId = realCustomerID;
                            if (orderID > 0)
                                item.EstimateId = orderID;
                        });

                        ////Order
                        if (orderID == 0)
                        {
                            orderID = TemporaryOrder.EstimateId;
                            TemporaryOrder.CompanyId = realCustomerID;
                            TemporaryOrder.ContactId = realContactID;
                        }


                        //List<Address> temporaryCustomerAddress = db.Addesses.Where(a => a.CompanyId == TemporaryCustomerID).ToList();
                        //foreach (Address add in temporaryCustomerAddress)
                        //{
                        //    db.Addesses.Remove(add);
                        //}
                        //CompanyTerritory temporaryTerritory = db.CompanyTerritories.Where(t => t.CompanyId == TemporaryCustomerID).FirstOrDefault();
                        //db.CompanyTerritories.Remove(temporaryTerritory);
                        //db.CompanyContacts.Remove(TemporaryContact);
                        //Company temporaryCompany = db.Companies.Where(c => c.CompanyId == TemporaryCustomerID).FirstOrDefault();
                        //db.Companies.Remove(temporaryCompany);

                        db.SaveChanges();

                    }
                }

                return orderID;
            }
            catch (Exception ex)
            {
                throw ex;
                return 0;
            }
        }

        private bool RemoveCustomerAndOrder(long TemporaryCustomerId,
            out List<ArtWorkAttatchment> orderAllItemsAttatchmentsListToBeRemoved,
            out List<Template> clonedTemplateToRemoveList)
        {
            try
            {
                List<ArtWorkAttatchment> artWorkAttaList = new List<ArtWorkAttatchment>();
                List<Template> clonedTemplateList = new List<Template>();


                Estimate TemporaryCustomerOrder = null;

                //Loads the dummy customer complete order
                Company customer =
                    db.Companies.Include("CompanyContacts")
                        .Where(cust => cust.CompanyId == TemporaryCustomerId)
                        .FirstOrDefault();
                if (customer != null &&
                    (customer.IsCustomer == 0 || customer.TypeId == (int)CompanyTypes.TemporaryCustomer))
                {
                    TemporaryCustomerOrder =
                        db.Estimates.Where(
                            order =>
                                order.CompanyId == TemporaryCustomerId &&
                                order.StatusId == (short)OrderStatus.ShoppingCart && order.isEstimate == false)
                            .FirstOrDefault();
                    List<Item> TemporaryOrderItems =
                        db.Items.Include("ItemAttachments")
                            .Include("ItemSections")
                            .Where(
                                i =>
                                    i.EstimateId == TemporaryCustomerOrder.EstimateId &&
                                    i.StatusId == (short)OrderStatus.ShoppingCart)
                            .ToList();

                    if (TemporaryCustomerOrder != null)
                    {
                        //order items
                        TemporaryCustomerOrder.Items.ToList().ForEach(item =>
                        {
                            //remove item and template complete structure

                            List<ArtWorkAttatchment> itemAttment = null;

                            Template cloneTemp = null;

                            RemoveCloneItem(item, out itemAttment, out cloneTemp);

                            if (itemAttment != null)
                                artWorkAttaList.AddRange(itemAttment);
                            // builds a list of whole Attatchments to be removed physically

                            if (cloneTemp != null &&
                                clonedTemplateList.Find(tempInList => tempInList.ProductId == cloneTemp.ProductId) ==
                                null)
                            {
                                clonedTemplateList.Add(cloneTemp);
                            }

                        });

                        //order 
                        db.Estimates.Remove(TemporaryCustomerOrder);
                    }

                    List<Address> addressesList = db.Addesses.Where(a => a.CompanyId == customer.CompanyId).ToList();
                    if (addressesList != null)
                    {
                        addressesList.ToList().ForEach(addr => db.Addesses.Remove(addr));
                    }

                    customer.CompanyContacts.ToList().ForEach(contacts => db.CompanyContacts.Remove(contacts));
                    //remove Customer
                    db.Companies.Remove(customer);
                }

                orderAllItemsAttatchmentsListToBeRemoved = artWorkAttaList;
                clonedTemplateToRemoveList = clonedTemplateList;

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<usp_GetRealEstateProducts_Result> GetRealEstateProductsByCompanyID(long CompanyId)
        {
            try
            {
                List<usp_GetRealEstateProducts_Result> lstProducts = new List<usp_GetRealEstateProducts_Result>();

                lstProducts = db.usp_GetRealEstateProducts(Convert.ToInt32(CompanyId)).ToList();

                return lstProducts;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public Item GetItemByOrderID(long OrderID)
        {
            try
            {
                return
                    db.Items.Where(c => c.EstimateId == OrderID && c.ItemType == (int)ItemTypes.Delivery)
                        .FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get Items For Widgets 
        /// </summary>
        public List<Item> GetItemsForWidgets()
        {
            return
                DbSet.Where(
                    i =>
                        i.IsPublished == true && i.IsArchived != true  && i.EstimateId == null && i.IsFeatured == true && i.IsEnabled == true &&
                        i.OrganisationId == OrganisationId).ToList().OrderBy(c => c.ProductName).ToList();

        }
        public List<Item> GetItemsForWidgetsByStoreId(long storeId)
        {
            return
                DbSet.Where(
                    i =>
                        i.IsPublished == true && i.IsArchived != true && i.EstimateId == null && i.IsFeatured == true && i.IsEnabled == true && i.CompanyId == storeId &&
                        i.OrganisationId == OrganisationId).ToList().OrderBy(c => c.ProductName).ToList();

        }

        public List<Item> GetItemsByOrderID(long OrderID)
        {
            try
            {
                return db.Items.Where(i => i.EstimateId == OrderID).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Item> GetListOfDeliveryItemByOrderID(long OID)
        {
            try
            {
                return db.Items.Where(i => i.EstimateId == OID && i.ItemType == (int)ItemTypes.Delivery).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


        public bool RemoveListOfDeliveryItemCostCenter(long OrderId)
        {
            try
            {
                List<Item> DItem =
             db.Items.Where(i => i.EstimateId == OrderId && i.ItemType == (int)ItemTypes.Delivery).ToList();

                foreach (var item in DItem)
                {
                    db.Items.Remove(item);
                }

                if (db.SaveChanges() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        //}

        //public bool AddUpdateItemFordeliveryCostCenter(long orderId, long DeliveryCostCenterId, double DeliveryCost,
        //    long customerID, string DeliveryName, StoreMode Mode, bool isDeliveryTaxable, bool IstaxONService,
        //    double GetServiceTAX, double TaxRate)
        //{
        //try
        //{
        //    ItemSection NewtblItemSection = null;
        //    SectionCostcentre NewtblISectionCostCenteres = null;
        //    Item newItem = null;

        //    Organisation organisation = null;
        //    // CompanySiteManager compSiteManager = new CompanySiteManager();

        //    double netTotal = 0;
        //    double grossTotal = 0;

        //    long OID =
        //        db.Companies.Where(c => c.CompanyId == customerID).Select(s => s.OrganisationId ?? 0).FirstOrDefault();
        //    organisation = db.Organisations.Where(o => o.OrganisationId == OID).FirstOrDefault();
        //    Item Record =
        //        db.Items.Where(c => c.EstimateId == orderId && c.ItemType == (int)ItemTypes.Delivery).FirstOrDefault();
        //    if (Record != null)
        //    {


        //        netTotal = DeliveryCost;

        //        if (Mode == StoreMode.Corp)
        //        {
        //            if (IstaxONService == true)
        //            {
        //                if (isDeliveryTaxable)
        //                {
        //                    Record.Tax1 = 0;
        //                    grossTotal = Math.Round(ServiceGrossTotalCalculation(netTotal, GetServiceTAX), 2, MidpointRounding.AwayFromZero);
        //                    Record.Qty1Tax1Value = Math.Round(ServiceTotalTaxCalculation(netTotal, GetServiceTAX), 2, MidpointRounding.AwayFromZero);
        //                }
        //                else
        //                {

        //                    Record.Qty1Tax1Value = CalculatePercentage(netTotal, 0);
        //                    grossTotal = netTotal + Record.Qty1Tax1Value ?? 0; //GrossTotalCalculation(netTotal, 0);
        //                }
        //            }
        //            else
        //            {
        //                if (isDeliveryTaxable == true)
        //                {
        //                    Record.Qty1Tax1Value = CalculatePercentage(netTotal, TaxRate); //calculateTaxPercentage(netTotal, Convert.ToInt32(TaxRate));
        //                    grossTotal = netTotal + Record.Qty1Tax1Value ?? 0;
        //                }
        //                else
        //                {
        //                    Record.Qty1Tax1Value = CalculatePercentage(netTotal, 0);// calculateTaxPercentage(netTotal, 0);
        //                    grossTotal = netTotal + Record.Qty1Tax1Value ?? 0;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if (IstaxONService == true)
        //            {
        //                if (isDeliveryTaxable)
        //                {
        //                    Record.Tax1 = 0;
        //                    grossTotal = Math.Round(ServiceGrossTotalCalculation(netTotal, GetServiceTAX), 2,MidpointRounding.AwayFromZero);
        //                    Record.Qty1Tax1Value = Math.Round(ServiceTotalTaxCalculation(netTotal, GetServiceTAX), 2, MidpointRounding.AwayFromZero);
        //                }
        //                else
        //                {
        //                    Record.Tax1 = 0;
        //                    Record.Qty1Tax1Value = CalculatePercentage(netTotal, 0);// calculateTaxPercentage(netTotal, 0);
        //                    grossTotal = netTotal + Record.Qty1Tax1Value ?? 0;
        //                }
        //            }
        //            else
        //            {
        //                if (isDeliveryTaxable)
        //                {
        //                    Record.Tax1 = 0;
        //                    Record.Qty1Tax1Value = CalculatePercentage(netTotal, TaxRate); //calculateTaxPercentage(netTotal, Convert.ToInt32(TaxRate));
        //                    grossTotal = netTotal + Record.Qty1Tax1Value ?? 0;
        //                }
        //                else
        //                {
        //                    Record.Tax1 = 0;
        //                    Record.Qty1Tax1Value = CalculatePercentage(netTotal, 0);// calculateTaxPercentage(netTotal, 0);
        //                    grossTotal = netTotal + Record.Qty1Tax1Value ?? 0;
        //                }
        //            }

        //        }


        //        //******************existing item*********************

        //        Record.IsPublished = false;
        //        Record.ProductName = DeliveryName;
        //        Record.EstimateId = orderId; //orderid
        //        Record.CompanyId = customerID; //customerid
        //        Record.ItemType = (int)ItemTypes.Delivery;
        //        Record.Qty1BaseCharge1 = netTotal;
        //        Record.Qty1NetTotal = netTotal;
        //        Record.Qty1GrossTotal = grossTotal;
        //        Record.InvoiceId = null;
        //        Record.IsOrderedItem = true;

        //        Markup zeroMarkup = db.Markups.Where(m => m.MarkUpRate == 0).FirstOrDefault();



        //        //*****************Existing item Sections and cost Centeres*********************************
        //        ItemSection ExistingItemSect = db.ItemSections.Where(i => i.ItemId == Record.ItemId).FirstOrDefault();
        //        ExistingItemSect.SectionName = DeliveryName;
        //        ExistingItemSect.BaseCharge1 = DeliveryCost;


        //        //*****************Existing Section Cost Centeres*********************************
        //        SectionCostcentre ExistingSectCostCentre =
        //            db.SectionCostcentres.Where(e => e.ItemSectionId == ExistingItemSect.ItemSectionId).FirstOrDefault();

        //        if (zeroMarkup != null)
        //        {
        //            ExistingSectCostCentre.Qty1MarkUpID = (int)zeroMarkup.MarkUpId;
        //        }
        //        else
        //        {
        //            ExistingSectCostCentre.Qty1MarkUpID = 1;
        //        }
        //        ExistingSectCostCentre.CostCentreId = DeliveryCostCenterId;
        //        ExistingSectCostCentre.Qty1Charge = DeliveryCost;
        //        ExistingSectCostCentre.Qty1NetTotal = DeliveryCost;
        //        if (db.SaveChanges() > 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    else
        //    {

        //        newItem = new Item();
        //        netTotal = DeliveryCost;
        //        if (Mode == StoreMode.Corp)
        //        {
        //            if (IstaxONService == true)
        //            {
        //                if (isDeliveryTaxable)
        //                {

        //                    grossTotal = Math.Round(ServiceGrossTotalCalculation(netTotal, GetServiceTAX), 2, MidpointRounding.AwayFromZero);
        //                    newItem.Qty1Tax1Value = Math.Round(ServiceTotalTaxCalculation(netTotal, GetServiceTAX), 2, MidpointRounding.AwayFromZero);
        //                }
        //                else
        //                {

        //                    newItem.Qty1Tax1Value = CalculatePercentage(netTotal, 0);// calculateTaxPercentage(netTotal, 0);
        //                    grossTotal = netTotal + newItem.Qty1Tax1Value ?? 0;
        //                }
        //            }
        //            else
        //            {
        //                if (isDeliveryTaxable)
        //                {

        //                    if (TaxRate != null && TaxRate > 0)
        //                    {
        //                        newItem.Qty1Tax1Value = CalculatePercentage(netTotal, TaxRate); //calculateTaxPercentage(netTotal, Convert.ToInt32(TaxRate));
        //                        grossTotal = netTotal + newItem.Qty1Tax1Value ?? 0;
        //                    }
        //                    else
        //                    {
        //                        newItem.Qty1Tax1Value = CalculatePercentage(netTotal, 0);
        //                        grossTotal = netTotal + newItem.Qty1Tax1Value ?? 0; 
        //                    }

        //                }
        //                else
        //                {
        //                    newItem.Qty1Tax1Value = CalculatePercentage(netTotal, 0);
        //                    grossTotal = netTotal + newItem.Qty1Tax1Value ?? 0; 
        //                }
        //            }

        //        }
        //        else
        //        {
        //            if (IstaxONService == true)
        //            {
        //                if (isDeliveryTaxable)
        //                {
        //                    grossTotal = Math.Round(ServiceGrossTotalCalculation(netTotal, GetServiceTAX), 2, MidpointRounding.AwayFromZero);
        //                    newItem.Qty1Tax1Value = Math.Round(ServiceTotalTaxCalculation(netTotal, GetServiceTAX), 2, MidpointRounding.AwayFromZero);
        //                }
        //                else
        //                {
        //                    newItem.Qty1Tax1Value = CalculatePercentage(netTotal, 0);// calculateTaxPercentage(netTotal, 0);
        //                    grossTotal = netTotal + newItem.Qty1Tax1Value ?? 0;
        //                }
        //            }
        //            else
        //            {
        //                if (isDeliveryTaxable)
        //                {
        //                    newItem.Qty1Tax1Value = CalculatePercentage(netTotal, TaxRate); //calculateTaxPercentage(netTotal, Convert.ToInt32(TaxRate));
        //                    grossTotal = netTotal + newItem.Qty1Tax1Value ?? 0;

        //                }
        //                else
        //                {
        //                    newItem.Qty1Tax1Value = CalculatePercentage(netTotal, 0);
        //                    grossTotal = netTotal + newItem.Qty1Tax1Value ?? 0; 
        //                }
        //            }

        //        }

        //        //******************new item*********************



        //        newItem.IsPublished = false;
        //        newItem.ProductName = DeliveryName;
        //        newItem.EstimateId = orderId; //orderid
        //        newItem.CompanyId = customerID; //customerid
        //        newItem.ItemType = (int)ItemTypes.Delivery;
        //        newItem.Qty1BaseCharge1 = netTotal;
        //        newItem.Qty1NetTotal = netTotal;
        //        newItem.Qty1GrossTotal = grossTotal;
        //        newItem.InvoiceId = null;
        //        newItem.IsOrderedItem = true;
        //        Markup zeroMarkup = db.Markups.Where(m => m.MarkUpRate == 0).FirstOrDefault();



        //        //*****************NEw item Sections and cost Centeres*********************************
        //        NewtblItemSection = new ItemSection();
        //        NewtblItemSection.ItemId = newItem.ItemId;
        //        NewtblItemSection.SectionName = DeliveryName;
        //        NewtblItemSection.BaseCharge1 = DeliveryCost;

        //        //dbContext.tbl_item_sections.AddObject(NewtblItemSection); //ContextAdded

        //        //*****************Section Cost Centeres*********************************
        //        NewtblISectionCostCenteres = new SectionCostcentre();

        //        if (zeroMarkup != null)
        //        {
        //            NewtblISectionCostCenteres.Qty1MarkUpID = (int)zeroMarkup.MarkUpId;
        //        }
        //        else
        //        {
        //            NewtblISectionCostCenteres.Qty1MarkUpID = 1;
        //        }

        //        NewtblISectionCostCenteres.CostCentreId = DeliveryCostCenterId;
        //        NewtblISectionCostCenteres.ItemSectionId = NewtblItemSection.ItemSectionId;
        //        NewtblISectionCostCenteres.Qty1Charge = DeliveryCost;
        //        NewtblISectionCostCenteres.Qty1NetTotal = DeliveryCost;
        //        db.SectionCostcentres.Add(NewtblISectionCostCenteres);
        //        db.ItemSections.Add(NewtblItemSection);
        //        db.Items.Add(newItem);

        //        if (db.SaveChanges() > 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}
        //catch (Exception ex)
        //{
        //    throw ex;
        //}


        //}

        public string SaveDesignAttachments(long templateID, long itemID, long customerID, string DesignName,
            string caller, long organisationId)
        {
            try
            {
                List<TemplatePage> oPages = null;
                db.Configuration.LazyLoadingEnabled = false;
                bool hasOverlayPdf = false;
                oPages =
                    db.TemplatePages.Where(
                        g => g.ProductId == templateID && (g.IsPrintable == true || g.IsPrintable == null)).ToList();

                UpdateItemName(itemID, DesignName, templateID);

                List<ArtWorkAttatchment> oLstAttachments = GetItemAttactchments(itemID, ".pdf", UploadFileTypes.Artwork);

                db.Configuration.LazyLoadingEnabled = false;

                var Item = db.Items.Where(i => i.ItemId == itemID).FirstOrDefault();
                // save template if item doesnot contain templateId
                if (Item.TemplateId == null || Item.TemplateId == 0)
                {
                    Item.TemplateId = templateID;
                    db.SaveChanges();
                }
                var Order =
                    db.Estimates.Where(order => order.EstimateId == Item.EstimateId && order.isEstimate == false)
                        .FirstOrDefault();
                string DesignerPath =
                    System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" +
                                                                  organisationId.ToString() + "/Templates/");
                // string DesignerPath = System.Web.HttpContext.Current.Server.MapPath("~/designengine/designer/products/");

                if (oLstAttachments.Count == 0)
                //no attachments already exist, hence a new entry in attachments is required
                {

                    //special working for attaching the PDF
                    List<ArtWorkAttatchment> uplodedArtWorkList = new List<ArtWorkAttatchment>();
                    ArtWorkAttatchment attatcment = null;
                    string folderPath = "mpc_content/Attachments/" + organisationId + "/" + HttpContext.Current.Request.Cookies["WBStoreId"].Value + "/Products/" + itemID;
                    //Web2Print.UI.Components.ImagePathConstants.ProductImagesPath + "Attachments/";
                    string virtualFolderPth = "";
                    if (caller == "webstore")
                    {
                        virtualFolderPth = System.Web.HttpContext.Current.Server.MapPath(folderPath + "/");
                    }
                    else
                    {
                        virtualFolderPth = System.Web.HttpContext.Current.Server.MapPath("/" + folderPath + "/");
                    }




                    if (!System.IO.Directory.Exists(virtualFolderPth))
                        System.IO.Directory.CreateDirectory(virtualFolderPth);

                    if (Item.isMultipagePDF == true)
                    {
                        //saving Page1  or Side 1 
                        //string fileName = ItemID.ToString() + " Side" + item.PageNo + ".pdf";
                        DateTime OrderCreationDate = Order.CreationDate ?? DateTime.Now;
                        string fileName = OrderCreationDate.Year.ToString() + OrderCreationDate.ToString("MMMM") +
                                          OrderCreationDate.Day.ToString() + "-" + specialCharactersEncoder(Item.ProductCode) + "-" +
                                          Order.Order_Code + "-" + Item.ItemCode + "-" + "Side1";
                        //GetAttachmentFileName(Item.ProductCode, Order.Order_Code, Item.ItemCode, "Side" + item.PageNo.ToString(), virtualFolderPth, ".pdf", Order.CreationDate ?? DateTime.Now);
                        string overlayName = OrderCreationDate.Year.ToString() + OrderCreationDate.ToString("MMMM") +
                                             OrderCreationDate.Day.ToString() + "-" + specialCharactersEncoder(Item.ProductCode) + "-" +
                                             Order.Order_Code + "-" + Item.ItemCode + "-" + "Side1overlay";
                        //GetAttachmentFileName(Item.ProductCode, Order.Order_Code, Item.ItemCode, "Side" + item.PageNo.ToString() + "overlay", virtualFolderPth, ".pdf", Order.CreationDate ?? DateTime.Now);


                        string fileCompleteAddress = System.IO.Path.Combine(virtualFolderPth, fileName + ".pdf");
                        string overlayCompleteAddress = System.IO.Path.Combine(virtualFolderPth, overlayName + ".pdf");

                        //copying file from original location to attachments location
                        System.IO.File.Copy(DesignerPath + templateID.ToString() + "/pages.pdf", fileCompleteAddress,
                            true);
                        foreach (var page in oPages)
                        {
                            if (page.hasOverlayObjects == true)
                                hasOverlayPdf = true;
                        }
                        // coping the over lay file if exisit 
                        if (hasOverlayPdf)
                        {
                            System.IO.File.Copy(DesignerPath + templateID.ToString() + "/pagesoverlay.pdf",
                                overlayCompleteAddress, true);
                            attatcment = new ArtWorkAttatchment();
                            attatcment.FileName = overlayName;
                            attatcment.FileExtention = ".pdf";
                            attatcment.FolderPath = folderPath;
                            attatcment.FileTitle = "Side1overlay";
                            uplodedArtWorkList.Add(attatcment);
                        }
                        //System.IO.File.WriteAllBytes(fileCompleteAddress, PDFSide1HighRes);
                        string ThumbnailPath = fileCompleteAddress;

                        attatcment = new ArtWorkAttatchment();
                        attatcment.FileName = fileName;
                        attatcment.FileExtention = ".pdf";
                        attatcment.FolderPath = folderPath;
                        attatcment.FileTitle = "Side1";
                       // attatcment.ImageFileType = ".jpg";
                        uplodedArtWorkList.Add(attatcment);
                        GenerateThumbnailForPdf(ThumbnailPath, true);
                    }
                    else
                    {


                        foreach (var item in oPages)
                        {
                            //saving Page1  or Side 1 
                            //string fileName = ItemID.ToString() + " Side" + item.PageNo + ".pdf";
                            DateTime OrderCreationDate = Order.CreationDate ?? DateTime.Now;
                            string fileName = OrderCreationDate.Year.ToString() + OrderCreationDate.ToString("MMMM") +
                                              OrderCreationDate.Day.ToString() + "-" + specialCharactersEncoder(Item.ProductCode) + "-" +
                                              Order.Order_Code + "-" + Item.ItemCode + "-" + "Side" +
                                              item.PageNo.ToString();
                            //GetAttachmentFileName(Item.ProductCode, Order.Order_Code, Item.ItemCode, "Side" + item.PageNo.ToString(), virtualFolderPth, ".pdf", Order.CreationDate ?? DateTime.Now);
                            string overlayName = OrderCreationDate.Year.ToString() + OrderCreationDate.ToString("MMMM") +
                                                 OrderCreationDate.Day.ToString() + "-" + specialCharactersEncoder(Item.ProductCode) + "-" +
                                                 Order.Order_Code + "-" + Item.ItemCode + "-" + "Side" +
                                                 item.PageNo.ToString() + "overlay";
                            //GetAttachmentFileName(Item.ProductCode, Order.Order_Code, Item.ItemCode, "Side" + item.PageNo.ToString() + "overlay", virtualFolderPth, ".pdf", Order.CreationDate ?? DateTime.Now);


                            string fileCompleteAddress = System.IO.Path.Combine(virtualFolderPth, fileName + ".pdf");
                            string overlayCompleteAddress = System.IO.Path.Combine(virtualFolderPth,
                                overlayName + ".pdf");

                            //copying file from original location to attachments location
                            System.IO.File.Copy(DesignerPath + item.ProductId.ToString() + "/p" + item.PageNo + ".pdf",
                                fileCompleteAddress, true);
                            // coping the over lay file if exisit 
                            if (item.hasOverlayObjects == true)
                            {
                                System.IO.File.Copy(
                                    DesignerPath + item.ProductId.ToString() + "/p" + item.PageNo + "overlay.pdf",
                                    overlayCompleteAddress, true);
                                attatcment = new ArtWorkAttatchment();
                                attatcment.FileName = overlayName;
                                attatcment.FileExtention = ".pdf";
                                attatcment.FolderPath = folderPath;
                                attatcment.FileTitle = "Side" + item.PageNo.ToString() + "overlay";
                                uplodedArtWorkList.Add(attatcment);
                            }
                            //System.IO.File.WriteAllBytes(fileCompleteAddress, PDFSide1HighRes);
                            string ThumbnailPath = fileCompleteAddress;

                            attatcment = new ArtWorkAttatchment();
                            attatcment.FileName = fileName;
                            attatcment.FileExtention = ".pdf";
                            attatcment.FolderPath = folderPath;
                            attatcment.FileTitle = "Side" + item.PageNo.ToString();
                          //  attatcment.ImageFileType = ".jpg";
                            uplodedArtWorkList.Add(attatcment);

                            GenerateThumbnailForPdf(ThumbnailPath, true);
                        }

                    }
                    //creating the attachment the attachment for the first time.
                    bool result = CreateUploadYourArtWork(itemID, customerID, uplodedArtWorkList);


                    //updating the item with templateID /design
                    UpdateTemplateIdInItem(itemID, templateID);

                }
                else // attachment alredy exists hence we need to updat the existing artwork.
                {
                    string folderPath = "mpc_content/Attachments/" + organisationId + "/" + HttpContext.Current.Request.Cookies["WBStoreId"].Value + "/Products/" + itemID;
                    // Web2Print.UI.Components.ImagePathConstants.ProductImagesPath + "Attachments/";
                    string virtualFolderPth = System.Web.HttpContext.Current.Server.MapPath("/" + folderPath + "/");
                    if (!System.IO.Directory.Exists(virtualFolderPth))
                        System.IO.Directory.CreateDirectory(virtualFolderPth);
                    int index = 0;
                    if (Item.isMultipagePDF == true) //
                    {
                        ArtWorkAttatchment oPage1Attachment = oLstAttachments[index];
                        index = index + 1;
                        //ArtWorkAttatchment oPage1Attachment = oLstAttachments.Where(g => g.FileTitle == oPage.PageName).Single();
                        if (oPage1Attachment != null)
                        {
                            string fileName = oPage1Attachment.FileName + oPage1Attachment.FileExtention;
                            string fileCompleteAddress = System.IO.Path.Combine(virtualFolderPth, fileName);
                            string sourcePath = DesignerPath + templateID.ToString() + "/pages.pdf";
                            if (fileName.Contains("overlay"))
                            {
                                sourcePath = DesignerPath + templateID.ToString() + "/pagesoverlay.pdf";
                                System.IO.File.Copy(sourcePath, fileCompleteAddress, true);
                            }
                            else
                            {
                                System.IO.File.Copy(sourcePath, fileCompleteAddress, true);
                                //System.IO.File.WriteAllBytes(fileCompleteAddress, PDFSide1HighRes);
                                string ThumbnailPath = fileCompleteAddress;
                                //System.IO.File.WriteAllBytes( System.Web.HttpContext.Current.Server.MapPath(  System.IO.Path.Combine(Web2Print.UI.Common.Utils.GetAppBasePath() +  oPage1Attachment.FolderPath, oPage1Attachment.FileName)), PDFSide1HighRes);
                                GenerateThumbnailForPdf(ThumbnailPath, true);
                            }

                        }
                        foreach (var page in oPages)
                        {
                            if (page.hasOverlayObjects == true)
                                hasOverlayPdf = true;
                        }
                        if (hasOverlayPdf == true)
                        {
                            oPage1Attachment = oLstAttachments[index];
                            index = index + 1;
                            if (oPage1Attachment != null)
                            {
                                string fileName = oPage1Attachment.FileName;
                                string fileCompleteAddress = System.IO.Path.Combine(virtualFolderPth, fileName);
                                string sourcePath = DesignerPath + templateID.ToString() + "/pages.pdf";
                                if (fileName.Contains("overlay"))
                                {
                                    sourcePath = DesignerPath + templateID.ToString() + "/pagesoverlay.pdf";
                                    System.IO.File.Copy(sourcePath, fileCompleteAddress, true);
                                }
                                else
                                {
                                    System.IO.File.Copy(sourcePath, fileCompleteAddress, true);
                                    //System.IO.File.WriteAllBytes(fileCompleteAddress, PDFSide1HighRes);
                                    string ThumbnailPath = fileCompleteAddress;
                                    //System.IO.File.WriteAllBytes( System.Web.HttpContext.Current.Server.MapPath(  System.IO.Path.Combine(Web2Print.UI.Common.Utils.GetAppBasePath() +  oPage1Attachment.FolderPath, oPage1Attachment.FileName)), PDFSide1HighRes);
                                    GenerateThumbnailForPdf(ThumbnailPath, true);
                                }

                            }

                        }
                    }
                    else
                    {

                        foreach (var oPage in oPages)
                        {
                            ArtWorkAttatchment oPage1Attachment = oLstAttachments[index];
                            index = index + 1;
                            //ArtWorkAttatchment oPage1Attachment = oLstAttachments.Where(g => g.FileTitle == oPage.PageName).Single();
                            if (oPage1Attachment != null)
                            {
                                string fileName = oPage1Attachment.FileName;
                                string fileCompleteAddress = System.IO.Path.Combine(virtualFolderPth, fileName + ".pdf");
                                string sourcePath = DesignerPath + oPage.ProductId.ToString() + "/p" + oPage.PageNo +
                                                    ".pdf";
                                if (fileName.Contains("overlay"))
                                {
                                    sourcePath = DesignerPath + oPage.ProductId.ToString() + "/p" + oPage.PageNo +
                                                 "overlay.pdf";
                                    System.IO.File.Copy(sourcePath, fileCompleteAddress, true);
                                }
                                else
                                {
                                    System.IO.File.Copy(sourcePath, fileCompleteAddress, true);
                                    //System.IO.File.WriteAllBytes(fileCompleteAddress, PDFSide1HighRes);
                                    string ThumbnailPath = fileCompleteAddress;
                                    //System.IO.File.WriteAllBytes( System.Web.HttpContext.Current.Server.MapPath(  System.IO.Path.Combine(Web2Print.UI.Common.Utils.GetAppBasePath() +  oPage1Attachment.FolderPath, oPage1Attachment.FileName)), PDFSide1HighRes);
                                    GenerateThumbnailForPdf(ThumbnailPath, true);
                                }

                            }
                            if (oPage.hasOverlayObjects == true)
                            {
                                oPage1Attachment = oLstAttachments[index];
                                index = index + 1;
                                if (oPage1Attachment != null)
                                {
                                    string fileName = oPage1Attachment.FileName;
                                    string fileCompleteAddress = System.IO.Path.Combine(virtualFolderPth, fileName + ".pdf");
                                    string sourcePath = DesignerPath + oPage.ProductId.ToString() + "/p" + oPage.PageNo +
                                                        ".pdf";
                                    if (fileName.Contains("overlay"))
                                    {
                                        sourcePath = DesignerPath + oPage.ProductId.ToString() + "/p" + oPage.PageNo +
                                                     "overlay.pdf";
                                        System.IO.File.Copy(sourcePath, fileCompleteAddress, true);
                                    }
                                    else
                                    {
                                        System.IO.File.Copy(sourcePath, fileCompleteAddress, true);
                                        //System.IO.File.WriteAllBytes(fileCompleteAddress, PDFSide1HighRes);
                                        string ThumbnailPath = fileCompleteAddress;
                                        //System.IO.File.WriteAllBytes( System.Web.HttpContext.Current.Server.MapPath(  System.IO.Path.Combine(Web2Print.UI.Common.Utils.GetAppBasePath() +  oPage1Attachment.FolderPath, oPage1Attachment.FileName)), PDFSide1HighRes);
                                        GenerateThumbnailForPdf(ThumbnailPath, true);
                                    }

                                }

                            }

                        }
                    }

                }

                return "/ProductOptions/0/" + itemID.ToString() + "/Template/" + templateID.ToString();



            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        //public static double ServiceGrossTotalCalculation(double QuantityBastotal, double Taxvalue)
        //{
        //    try
        //    {
        //        double gross = QuantityBastotal + ServiceTotalTaxCalculation(QuantityBastotal, Taxvalue);
        //        return gross;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }

        //}

        //public static double ServiceTotalTaxCalculation(double QuantityBastotal, double Taxvalue)
        //{
        //    double Quantity1Taxvalue = QuantityBastotal * Taxvalue;
        //    return Quantity1Taxvalue;

        //}

        public static double GrossTotalCalculationDelivery(double netTotal, double stateTaxValue)
        {
            try
            {
                double stateTaxPice = 0;

                stateTaxPice = netTotal + CalculatePercentageDelivery(netTotal, stateTaxValue);

                return stateTaxPice;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public static double CalculatePercentageDelivery(double itemValue, double percentageValue)
        {

            try
            {
                double percentValue = 0;

                percentValue = itemValue * (percentageValue / 100);

                return percentValue;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public double calculateTaxPercentage(double netTotal, double MarkupRate)
        {
            try
            {
                double PercentageVal = (netTotal * MarkupRate) / 100;
                return PercentageVal;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private bool UpdateItemName(long ItemID, string ProductName, long TemplateId)
        {
            try
            {
                db.Configuration.LazyLoadingEnabled = false;
                Item oItem = db.Items.Where(g => g.ItemId == ItemID).Single();
                Template oTemplate = db.Templates.Where(t => t.ProductId == TemplateId).Single();
                oItem.ProductName = ProductName;
                oTemplate.ProductName = ProductName;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public void GenerateThumbnailForPdf(string sideThumbnailPath, bool insertCuttingMargin)
        {
            try
            {
                using (Doc theDoc = new Doc())
                {
                    theDoc.Read(sideThumbnailPath);
                    theDoc.PageNumber = 1;
                    theDoc.Rect.String = theDoc.CropBox.String;

                    if (insertCuttingMargin)
                    {
                        theDoc.Rect.Inset(14.173228345, 14.173228345);
                    }

                    Stream oImgstream = new MemoryStream();

                    theDoc.Rendering.DotsPerInch = 300;
                    theDoc.Rendering.Save("tmp.png", oImgstream);

                    theDoc.Clear();
                    theDoc.Dispose();

                    CreatAndSaveThumnail(oImgstream, sideThumbnailPath);

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public bool CreateUploadYourArtWork(long itemID, long customerID, List<ArtWorkAttatchment> yourDesignList)
        {
            try
            {
                bool result = false;

                db.Configuration.LazyLoadingEnabled = false;

                Company customer =
                    db.Companies.Include("CompanyContacts").Where(c => c.CompanyId == customerID).FirstOrDefault();
                ItemAttachment tblAttatch = null;
                long contactID = 0;
                CompanyContact contact = null;

                try
                {
                    if (yourDesignList.Count > 0)
                    {
                        if (customer != null && customer.CompanyContacts.ToList().Count > 0)
                        {
                            contact = customer.CompanyContacts.ToList()[0];
                            contactID = contact.ContactId;
                        }

                        string folderPath = string.Empty;
                        //Create Additional cost Centeres
                        foreach (ArtWorkAttatchment attatchment in yourDesignList)
                        {
                            folderPath = attatchment.FolderPath.Replace("\\", "//").Replace("//", "/");

                            tblAttatch = this.PopulateAttachment(itemID, customerID, contactID, attatchment.FileTitle,
                                attatchment.FileName, attatchment.UploadFileType, attatchment.FileExtention, folderPath);
                            db.ItemAttachments.Add(tblAttatch);
                        }

                        db.SaveChanges();
                        result = true;

                    }
                }
                catch (Exception)
                {

                    throw;
                }
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public bool UpdateTemplateIdInItem(long itemID, long templateID)
        {


            bool result = false;
            Item tblItemProduct = null;

            try
            {
                db.Configuration.LazyLoadingEnabled = false;
                tblItemProduct = db.Items.Where(item => item.ItemId == itemID).FirstOrDefault();

                if (tblItemProduct != null)
                {

                    tblItemProduct.TemplateId = templateID > 0 ? templateID : tblItemProduct.TemplateId;

                    result = db.SaveChanges() > 0 ? true : false;
                }
            }
            catch (Exception)
            {
                result = false;
                throw;
            }

            return result;

        }

        private ItemAttachment PopulateAttachment(long itemID, long customerID, long contactId, string fileTitle,
            string fileName, UploadFileTypes type, string fileExtention, string folderPath)
        {
            try
            {
                ItemAttachment attchment = new ItemAttachment
                {
                    ItemId = itemID,
                    FileTitle = fileTitle,
                    FileType = fileExtention,
                    Type = type.ToString(),
                    FileName = fileName,
                    FolderPath = folderPath,
                    CompanyId = customerID,
                    ContactId = contactId,
                    IsApproved = 1,
                    UploadDate = DateTime.Now,
                    UploadTime = DateTime.Now,
                    isFromCustomer = 1

                };

                return attchment;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        /// <summary>
        /// gets the cloned item by id
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public Item GetClonedItemById(long itemId)
        {
            try
            {
                db.Configuration.LazyLoadingEnabled = false;
                return
                    db.Items.Include("ItemAttachments")
                        .Include("ItemSections")
                        .Where(i => i.ItemId == itemId && i.EstimateId != null)
                        .FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //return db.Items.Include("ItemPriceMatrices").Include("ItemSections").Where(i => i.IsPublished == true && i.ItemId == itemId && i.EstimateId == null).FirstOrDefault();

        }

        /// <summary>
        /// get first item of a order to resolve the quantity and price variables in email
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public long GetFirstItemIdByOrderId(long orderId)
        {

            try
            {

                List<Item> itemsList = (from r in db.Items
                                        where r.EstimateId == orderId && (r.ItemType == null || r.ItemType != 2)
                                        select r).ToList();
                if (itemsList != null && itemsList.Count > 0)
                {
                    return Convert.ToInt64(itemsList[0].ItemId);
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception)
            {
                throw;
            }

        }

        public Item GetItemByOrderItemID(long ItemID, long OrderID)
        {
            try
            {
                return
                    db.Items.Where(g => g.RefItemId == ItemID && g.EstimateId == OrderID && g.IsOrderedItem == false)
                        .FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public bool isTemporaryOrder(long orderId, long customerId, long contactId)
        {
            try
            {
                Estimate Order =
             db.Estimates.Where(
                 o =>
                     o.EstimateId == orderId && o.StatusId == (short)OrderStatus.ShoppingCart &&
                     o.isEstimate == false).FirstOrDefault();

                if (Order != null && Order.CompanyId == customerId && Order.ContactId == contactId)
                {
                    return false; // order is real
                }
                else
                {
                    return true; // order is dummy
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public ItemSection GetItemFirstSectionByItemId(long ItemId)
        {
            try
            {
                db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;
                ItemSection oresult = db.ItemSections.Where(o => o.ItemId == ItemId && o.SectionNo == 1).FirstOrDefault();
                return oresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public ItemSection UpdateItemFirstSectionByItemId(long ItemId, int Quantity)
        {
            try
            {
                db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;
                ItemSection oresult = db.ItemSections.Where(o => o.ItemId == ItemId && o.SectionNo == 1).FirstOrDefault();
                if (oresult != null)
                {
                    oresult.Qty1 = Quantity;
                    db.SaveChanges();
                }
                return oresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Get All Corporate and Retail Products
        /// Used in Order in Add From Retail Store
        /// </summary>
        public ItemSearchResponse GetAllStoreProducts(ItemSearchRequestModel request)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            bool isNonPrintProductSpecified = request.ProductType == 3;
            int toRow = request.PageSize;
            Expression<Func<Item, bool>> query =
                item =>
                    (string.IsNullOrEmpty(request.SearchString) || (item.ProductName.Contains(request.SearchString)) ||
                     (item.ProductCode.Contains(request.SearchString)) || (item.Company.Name.Contains(request.SearchString)))
                    && item.OrganisationId == OrganisationId
                    && item.IsPublished == true
                    && item.EstimateId == null && item.IsEnabled == true
              && ((!isNonPrintProductSpecified && item.ProductType == (int)ProductType.PrintProduct) ||
              (isNonPrintProductSpecified && item.ProductType == (int)ProductType.NonPrintProduct));

            List<Item> totalItems = DbSet.Where(query).ToList();

            List<Item> items = totalItems.OrderBy(item => item.ProductCode)
           .Skip(fromRow)
            .Take(toRow)
            .ToList();
            return new ItemSearchResponse
            {
                Items = items,
                TotalCount = totalItems.Count
            };
        }

        public ItemSearchResponse GetItemsByCompanyId(ItemSearchRequestModel request)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            bool isNonPrintProductSpecified = request.ProductType == 3;
            int toRow = request.PageSize;
            Expression<Func<Item, bool>> query =
                item =>
                    (string.IsNullOrEmpty(request.SearchString) || (item.ProductName.Contains(request.SearchString)) ||
                     (item.ProductCode.Contains(request.SearchString)))
                    && item.CompanyId.HasValue && item.CompanyId == request.CompanyId
                    && item.OrganisationId == OrganisationId
                    && item.IsPublished == true
                    && item.EstimateId == null
              && ((!isNonPrintProductSpecified && item.ProductType != (int)ProductType.MarketingBrief) ||
              (isNonPrintProductSpecified && item.ProductType == (int)ProductType.NonPrintProduct));

            List<Item> totalItems = DbSet.Where(query).ToList();

            List<Item> items = totalItems.OrderBy(item => item.ProductCode)
           .Skip(fromRow)
            .Take(toRow)
            .ToList();
            return new ItemSearchResponse
            {
                Items = items,
                TotalCount = totalItems.Count
            };
        }

        /// <summary>
        /// get cart items count 
        /// </summary>
        /// <returns></returns>
        public long GetCartItemsCount(long ContactId, long TemporaryCustomerId, long CompanyId)
        {
            try
            {
                int orderStatusID = (int)OrderStatus.ShoppingCart;
                long itemsCount = 0;
                if (ContactId > 0)
                {
                    Estimate Order =
                        db.Estimates.Include("Items")
                            .Where(
                                order =>
                                    order.ContactId == ContactId && order.CompanyId == CompanyId && order.StatusId == orderStatusID &&
                                    order.isEstimate == false)
                            .FirstOrDefault();
                    if (Order != null)
                    {
                        Order.Items.Where(c => c.ItemType != (int)ItemTypes.Delivery).ToList().ForEach(orderItem =>
                        {
                            if (orderItem.IsOrderedItem.HasValue && orderItem.IsOrderedItem.Value)
                                itemsCount += 1;
                        });
                    }
                }
                else
                {
                    if (TemporaryCustomerId > 0)
                    {
                        Estimate Order =
                            db.Estimates.Include("Items")
                                .Where(
                                    order =>
                                        order.CompanyId == TemporaryCustomerId && order.StatusId == orderStatusID &&
                                        order.isEstimate == false)
                                .FirstOrDefault();
                        if (Order != null)
                        {
                            Order.Items.Where(c => c.ItemType != (int)ItemTypes.Delivery).ToList().ForEach(orderItem =>
                            {
                                if (orderItem.IsOrderedItem.HasValue && orderItem.IsOrderedItem.Value)
                                    itemsCount += 1;
                            });
                        }
                    }

                }
                return itemsCount;
            }
            catch (Exception ex)
            {
                throw ex;
            }



        }

        public List<CmsSkinPageWidget> GetStoreWidgets()
        {
            try
            {
                db.Configuration.LazyLoadingEnabled = false;
                return db.PageWidgets.Include("PageWidgetParams").Where(i => i.CompanyId == 2205).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        /// get cart items count 
        /// </summary>
        /// <returns></returns>

        public Item CloneReOrderItem(long orderID, long ExistingItemId, long loggedInContactID, string order_code, long OrganisationId)
        {

            bool result = false;
            Template clonedTemplate = null;
            ItemSection tblItemSectionCloned = null;
            SectionCostcentre tblISectionCostCenteresCloned = null;



            Item ExistingItem = GetActualItemToClone(ExistingItemId);

            Item newItem = null;
            try
            {
                //******************new item*********************
                newItem = Clone<Item>(ExistingItem);
                newItem.ItemId = 0;
                newItem.EstimateId = orderID;
                newItem.IsOrderedItem = true;
                if (ExistingItem.DiscountVoucherID != null)
                {
                    newItem.Qty1BaseCharge1 = (ExistingItem.Qty1NetTotal ?? 0) + (ExistingItem.Qty1CostCentreProfit ?? 0);
                    newItem.Qty1NetTotal = (ExistingItem.Qty1NetTotal ?? 0) + (ExistingItem.Qty1CostCentreProfit ?? 0);
                    newItem.Qty1CostCentreProfit = null;
                    newItem.DiscountVoucherID = null;
                }
                // Status
                int statustype = Convert.ToInt16(OrderStatus.ShoppingCart);
                newItem.StatusId = Convert.ToInt16(OrderStatus.ShoppingCart); //tblStatuses.StatusID; //shopping cart               
                db.Items.Add(newItem); //dbcontext added

                //*****************Existing item Sections and cost Centers*********************************
                if (ExistingItem.ItemSections.ToList() != null)
                {
                    foreach (ItemSection tblItemSection in ExistingItem.ItemSections)
                    {
                        tblItemSectionCloned = Clone<ItemSection>(tblItemSection);
                        tblItemSectionCloned.ItemSectionId = 0;
                        tblItemSectionCloned.ItemId = newItem.ItemId;

                        db.ItemSections.Add(tblItemSectionCloned);
                        //*****************Section Cost Centers*********************************
                        if (tblItemSection.SectionCostcentres.Count > 0)
                        {
                            foreach (SectionCostcentre tblSectCostCenter in tblItemSection.SectionCostcentres.ToList())
                            {
                                tblISectionCostCenteresCloned = Clone<SectionCostcentre>(tblSectCostCenter);
                                tblISectionCostCenteresCloned.SectionCostcentreId = 0;
                                //tblItemSectionCloned.SectionCostcentres.Add(tblISectionCostCenteresCloned);
                                tblISectionCostCenteresCloned.ItemSectionId = tblItemSectionCloned.ItemSectionId;
                                db.SectionCostcentres.Add(tblISectionCostCenteresCloned);
                            }
                        }
                        else // add web order section Cost center to item
                        {
                            tblISectionCostCenteresCloned.SectionCostcentreId = 0;
                            tblISectionCostCenteresCloned.ItemSectionId = tblItemSectionCloned.ItemSectionId;
                            tblISectionCostCenteresCloned.CostCentre =
                                db.CostCentres.Where(c => c.CostCentreId == 206).FirstOrDefault();
                            db.SectionCostcentres.Add(tblISectionCostCenteresCloned);
                        }
                    }//Existing item Sections
                }
                // In re-order we will copy both the Template and the item attachments..
                //Copy Template if it does exists
                if (newItem.TemplateId.HasValue && newItem.TemplateId.Value > 0)
                {
                    clonedTemplate = new Template();
                    if (newItem.TemplateType == 1 || newItem.TemplateType == 2)
                    {
                        CompanyContact logInUser = db.CompanyContacts.Where(c => c.ContactId == loggedInContactID).FirstOrDefault();
                        long resultTemplate = db.sp_cloneTemplate((int)newItem.TemplateId.Value, 0, "");

                        long? clonedTemplateID = resultTemplate;
                        clonedTemplate = db.Templates.Where(g => g.ProductId == clonedTemplateID).Single();

                        var oCutomer = db.Companies.Where(i => i.CompanyId == logInUser.CompanyId).FirstOrDefault();

                        if (oCutomer != null)
                        {
                            clonedTemplate.TempString = oCutomer.WatermarkText;
                            clonedTemplate.isWatermarkText = oCutomer.isTextWatermark;
                            if (oCutomer.isTextWatermark == false)
                            {
                                clonedTemplate.TempString = HttpContext.Current.Server.MapPath(oCutomer.WatermarkText);
                            }

                        }
                        // here 

                        //  VariablesResolve(itemID, clonedTemplate.ProductId, objContactID);
                    }

                }
                int sideNumber = 0;

                db.SaveChanges();
                //ItemId will only be availiable after the save changes...

                if (ExistingItem.ItemAttachments != null && ExistingItem.ItemAttachments.Count() > 0)
                {
                    //Copy Attachments
                    ExistingItem.ItemAttachments.ToList().ForEach(itemAttatchments =>
                    {
                        sideNumber = sideNumber + 1;
                        ItemAttachment tblItemAttachmentCloned = Clone<ItemAttachment>(itemAttatchments);
                        if (tblItemAttachmentCloned != null)
                        {
                            tblItemAttachmentCloned.ItemId = 0;
                            tblItemAttachmentCloned.ItemId = newItem.ItemId;
                            SaveItemAttathmentPaths(tblItemAttachmentCloned, sideNumber, order_code); // create item attment copy files etc
                            db.ItemAttachments.Add(tblItemAttachmentCloned);
                        }
                    });
                }


                newItem.ItemCode = "ITM-0-001-" + newItem.ItemId;
                //dbContext.SaveChanges();
                if (db.SaveChanges() > 0)
                {
                    if (clonedTemplate != null && (newItem.TemplateType == 1 || newItem.TemplateType == 2))
                    {
                        newItem.TemplateId = clonedTemplate.ProductId;

                        CopyTemplatePaths(clonedTemplate, OrganisationId);
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return newItem;

        }
        private void SaveItemAttathmentPaths(ItemAttachment clonedItemAttachment, int sideNumber, string ordercode)
        {
            // Existing file to copy ..
            try
            {
                string[] ordercodearr = ordercode.Split('-');
                string fileCompleteAddress = string.Format("{0}{1}", clonedItemAttachment.FolderPath, clonedItemAttachment.FileName);
                string[] arrs = fileCompleteAddress.Split('-');
                string previousitemId = arrs[8];
                string today = DateTime.Now.Year.ToString() + DateTime.Now.ToString("MMMM") + DateTime.Now.Day.ToString();
                clonedItemAttachment.FileName = clonedItemAttachment.FileName.Replace(previousitemId, clonedItemAttachment.ItemId.ToString());
                clonedItemAttachment.FileName = clonedItemAttachment.FileName.Replace(arrs[4], ordercodearr[2]);
                clonedItemAttachment.FileName = clonedItemAttachment.FileName.Replace(arrs[0], today);
                string destFileCompletePath = string.Format("{0}{1}", clonedItemAttachment.FolderPath, clonedItemAttachment.FileName);

                fileCompleteAddress = HttpContext.Current.Server.MapPath(fileCompleteAddress);
                destFileCompletePath = HttpContext.Current.Server.MapPath(destFileCompletePath);

                File.Copy(fileCompleteAddress, destFileCompletePath, false);

                string ext = clonedItemAttachment.FileType;

                // Generate the thumbnail

                byte[] fileData = null;
                FileStream file = File.OpenRead(destFileCompletePath);
                MemoryStream ms = new MemoryStream();
                file.CopyTo(ms);
                fileData = ms.ToArray();

                if (ext == ".pdf" || ext == ".TIF" || ext == ".TIFF")
                {
                    GenerateThumbnailForPdf(fileData, destFileCompletePath, false);
                }
                else
                {
                    CreatAndSaveThumnail(ms, destFileCompletePath);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //if (clonedItemAttachment.FileName.IndexOf(' ') > 0)
            //{
            //    string previousitemId = clonedItemAttachment.FileName.Substring(0, clonedItemAttachment.FileName.IndexOf(' '));
            //    clonedItemAttachment.FileName = clonedItemAttachment.FileName.Replace(previousitemId, clonedItemAttachment.ItemID.ToString());

            //    string destFileCompletePath = string.Format("{0}{1}", clonedItemAttachment.FolderPath, clonedItemAttachment.FileName);

            //    fileCompleteAddress = HttpContext.Current.Server.MapPath(fileCompleteAddress);
            //    destFileCompletePath = HttpContext.Current.Server.MapPath(destFileCompletePath);

            //    File.Copy(fileCompleteAddress, destFileCompletePath, false);

            //    string ext = clonedItemAttachment.FileType;

            //    // Generate the thumbnail

            //    byte[] fileData = null;
            //    FileStream file = File.OpenRead(destFileCompletePath);
            //    MemoryStream ms = new MemoryStream();
            //    file.CopyTo(ms);
            //    fileData = ms.ToArray();

            //    if (ext == ".pdf" || ext == ".TIF" || ext == ".TIFF")
            //    {
            //        ProductManager.GenerateThumbnailForPdf(fileData, destFileCompletePath, false);
            //    }
            //    else
            //    {
            //        ProductManager.CreatAndSaveThumnail(ms, destFileCompletePath);
            //    }
            //  }
        }

        public List<SaveDesignView> GetSavedDesigns(long ContactID)
        {
            long sameItem = 0;

            List<SaveDesignView> NewList = new List<SaveDesignView>();
            List<SaveDesignView> ListsavedDesign = (from savedDesign in db.SaveDesignViews
                                                    where savedDesign.ContactID == ContactID
                                                    select savedDesign).ToList();

            foreach (var s in ListsavedDesign)
            {
                if (sameItem > 0 && s.ItemID == sameItem)
                {

                }
                else
                {
                    sameItem = s.ItemID;
                    NewList.Add(s);
                }
            }

            return NewList;

        }
        public void RemoveItemAttacmentPhysically(List<ArtWorkAttatchment> attatchmentList)
        {
            string completePath = string.Empty;
            //@Server.MapPath(folderPath);
            try
            {
                if (attatchmentList != null)
                {
                    foreach (ArtWorkAttatchment itemAtt in attatchmentList)
                    {
                        completePath = HttpContext.Current.Server.MapPath(itemAtt.FolderPath + itemAtt.FileName);
                        if (itemAtt.UploadFileType == UploadFileTypes.Artwork)
                        {

                            //delete the thumb nails as well.
                            DeleteFile(completePath.Replace(itemAtt.FileExtention, "Thumb.png"));
                        }
                        DeleteFile(completePath); //
                    }
                }
                //System.Web

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void DeleteFile(string completePath)
        {
            try
            {
                if (System.IO.File.Exists(completePath))
                {
                    System.IO.File.Delete(completePath);
                }
            }
            catch (Exception)
            { }
        }


        //public void GenerateThumbnailForPdf(byte[] PDFFile, string sideThumbnailPath, bool insertCuttingMargin)
        //{
        //    using (Doc theDoc = new Doc())
        //    {
        //        theDoc.Read(PDFFile);
        //        theDoc.PageNumber = 1;
        //        theDoc.Rect.String = theDoc.CropBox.String;

        //        if (insertCuttingMargin)
        //        {
        //            theDoc.Rect.Inset(ConstantsValues.CuttingMargin, ConstantsValues.CuttingMargin);
        //        }

        //        Stream oImgstream = new MemoryStream();

        //        theDoc.Rendering.DotsPerInch = 300;
        //        theDoc.Rendering.Save("tmp.png", oImgstream);

        //        theDoc.Clear();
        //        theDoc.Dispose();

        //        CreatAndSaveThumnail(oImgstream, sideThumbnailPath);
        //    }
        //}
        //public bool CreatAndSaveThumnail(Stream oImgstream, string sideThumbnailPath)
        //{
        //    try
        //    {
        //        string baseAddress = sideThumbnailPath.Substring(0, sideThumbnailPath.LastIndexOf('\\'));
        //        sideThumbnailPath = Path.GetFileNameWithoutExtension(sideThumbnailPath) + "Thumb.png";

        //        sideThumbnailPath = baseAddress + "\\" + sideThumbnailPath;

        //        Image origImage = Image.FromStream(oImgstream);

        //        float WidthPer, HeightPer;

        //        int NewWidth, NewHeight;
        //        int ThumbnailSizeWidth = 400;
        //        int ThumbnailSizeHeight = 400;

        //        if (origImage.Width > origImage.Height)
        //        {
        //            NewWidth = ThumbnailSizeWidth;
        //            WidthPer = (float)ThumbnailSizeWidth / origImage.Width;
        //            NewHeight = Convert.ToInt32(origImage.Height * WidthPer);
        //        }
        //        else
        //        {
        //            NewHeight = ThumbnailSizeHeight;
        //            HeightPer = (float)ThumbnailSizeHeight / origImage.Height;
        //            NewWidth = Convert.ToInt32(origImage.Width * HeightPer);
        //        }

        //        Bitmap origThumbnail = new Bitmap(NewWidth, NewHeight, origImage.PixelFormat);
        //        Graphics oGraphic = Graphics.FromImage(origThumbnail);
        //        oGraphic.CompositingQuality = CompositingQuality.HighQuality;
        //        oGraphic.SmoothingMode = SmoothingMode.HighQuality;
        //        oGraphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
        //        Rectangle oRectangle = new Rectangle(0, 0, NewWidth, NewHeight);
        //        oGraphic.DrawImage(origImage, oRectangle);


        //        origThumbnail.Save(sideThumbnailPath, ImageFormat.Png);
        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        return false;
        //    }
        //}

        /// <summary>
        /// get all published products against a store
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="OrganisationId"></param>
        /// <returns></returns>
        public List<Item> GetProductsList(long CompanyId, long OrganisationId, int offerType)
        {
            try
            {
                List<Item> ReqItemsList;
                db.Configuration.LazyLoadingEnabled = false;
                //var query = from productsList in db.Items
                //            join tblCmsOffer in db.CmsOffers on new { itemid = productsList.ItemId }
                //            equals new { itemid = tblCmsOffer .ItemId } into p2g
                //            from p2g1 in p2g.DefaultIfEmpty(null)
                //            select productsList;
                //var query = from productsList in db.Items
                //            join tblCmsOffer in db.CmsOffers on productsList.ItemId
                //            equals tblCmsOffer.ItemId into ProdTblCmsOfferGroupJoin
                //            where
                //            Object.Equals(productsList.EstimateId, null)
                //            && productsList.IsEnabled == true && productsList.IsArchived == false

                //            from JTble in ProdTblCmsOfferGroupJoin.DefaultIfEmpty()

                //            orderby productsList.SortOrder


                //            select new Item
                //            {
                              
                //                ItemID = productsList.ItemID,
                //                EstimateID = productsList.EstimateID,
                //                ProductName = productsList.ProductName,
                //                ImagePath = productsList.ImagePath,
                //                ThumbnailPath = productsList.ThumbnailPath,
                              
                //                IsEnabled = productsList.IsEnabled,
                //                IsSpecialItem = productsList.IsSpecialItem,
                //                IsPopular = productsList.IsPopular,
                //                IsFeatured = productsList.IsFeatured,
                //                IsPromotional = productsList.IsPromotional,
                //                IsPublished = productsList.IsPublished,
                //                ProductType = productsList.ProductType,
                //                ProductSpecification = productsList.ProductSpecification,
                //                CompleteSpecification = productsList.CompleteSpecification,
                //                TipsAndHints = productsList.TipsAndHints,
                //                SortOrder = productsList.SortOrder ?? 0,
                //                ProductWebDescription = productsList.WebDescription
                //            };




                ReqItemsList = db.Items.Where(
                   i =>
                       i.EstimateId == null && i.IsPublished == true && i.IsEnabled == true && (i.IsArchived == null || i.IsArchived == false) && i.CompanyId == CompanyId &&
                       i.OrganisationId == OrganisationId && i.IsFeatured == true).ToList();

                if (ReqItemsList != null || ReqItemsList.Count() > 0)
                {
                    List<long> listOfActualtemIds = ReqItemsList.Select(c => c.ItemId).ToList();
                    List<int?> ids = db.CmsOffers.Where(i => listOfActualtemIds.Contains((long)i.ItemId) && i.OfferType == offerType).Select(c => c.ItemId).ToList();
                    if (ids != null && ids.Count() > 0)
                    {
                        ReqItemsList = ReqItemsList.Where(i => ids.Contains((int)i.ItemId)).OrderBy(i => i.SortOrder).ToList();
                        
                    }
                   // else 
                   // {
                     //   return null;
                   // }
                 }
              //  else 
              ///  {
                 //   return null;
               // }
                return ReqItemsList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        /// <summary>
        /// get all parent categories and corresponding products of a category against a store
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="OrganisationId"></param>
        /// <returns></returns>
        public List<ProductCategory> GetStoreParentCategories(long CompanyId, long OrganisationId)
        {
            try
            {
                db.Configuration.LazyLoadingEnabled = false;
                var query = (from category in db.ProductCategories
                             where category.CompanyId ==
                                     CompanyId && category.OrganisationId == OrganisationId && (category.isArchived == false || category.isArchived == null) && category.isPublished == true && category.isEnabled == true
                             select category);
                return query.ToList<ProductCategory>();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void DeleteItemBySP(long ItemID)
        {
            try
            {
                db.usp_DeleteProduct(ItemID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// added by saqib to get template id of parent item , used to get template variable mapping 
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public long getParentTemplateID(long itemId)
        {
            long parentTemplateId = 0;
            long OrganisationID = 0;
            try
            {

                var item = db.Items.Where(g => g.ItemId == itemId).SingleOrDefault();
                if (item != null)
                {
                    if (item.OrganisationId.HasValue)
                    {
                        OrganisationID = item.OrganisationId.Value;
                    }
                    if (item.RefItemId.HasValue && item.RefItemId.Value != 0)
                    {
                        var refItem = db.Items.Where(g => g.ItemId == item.RefItemId).SingleOrDefault();
                        if (refItem.TemplateId.HasValue)
                            parentTemplateId = refItem.TemplateId.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new MPCException(ex.ToString(), OrganisationID);
            }
            return parentTemplateId;
        }

        public List<ProductItem> GetAllRetailDisplayProductsQuickCalc(long CompanyID)
        {

            db.Configuration.LazyLoadingEnabled = false;
            //var itemsList = GetAllRetailActiveProducts(CompanyID);



            var query = from productsList in db.GetCategoryProducts
                        where productsList.IsPublished == true && productsList.IsEnabled == true
                        && (productsList.IsArchived == null || productsList.IsArchived == false)
                        && productsList.EstimateId == null && productsList.CompanyId == CompanyID
                        orderby productsList.SortOrder

                        select new ProductItem
                        {
                            //OfferID = JTble.OfferID,
                            //OfferType = JTble.OfferType,
                            ItemID = productsList.ItemId,
                            //EstimateID = productsList.EstimateID,
                            ProductName = productsList.ProductName,
                            //ProductCategoryName = productsList.ca,
                            //ProductCategoryID = productsList.productC,
                            //ParentCategoryID = productsList.ParentCategoryID,
                            MinPrice = productsList.MinPrice,
                            //ImagePath = productsList.ImagePath,
                            //ThumbnailPath = productsList.ThumbnailPath,
                            //IconPath = productsList.IconPath,
                            //IsEnabled = productsList.IsEnabled,
                            //IsSpecialItem = productsList.IsSpecialItem,
                            //IsPopular = productsList.IsPopular,
                            //IsFeatured = productsList.IsFeatured,
                            //IsPromotional = productsList.IsPromotional,
                            // IsFinishedGoods = (productsList.IsFinishedGoods== 1 || productsList.IsFinishedGoods == 4) ? true : false,
                            //IsPublished = prod-uctsList.IsPublished,
                            //ProductSpecification = productsList.ProductSpecification,
                            //CompleteSpecification = productsList.CompleteSpecification,
                            //TipsAndHints = productsList.TipsAndHints,
                            //TopCategoryID = productsList.TopCategoryID,
                            IsQtyRanged = productsList.isQtyRanged,
                        };

            ////var query = db.vw_GetAllRetailStoreActiveProducts.ToList();
            ////List<Model.ProductItem> mylist = new List<Model.ProductItem>();
            ////query.ToList().ForEach(a => mylist.Add(new Model.ProductItem { ItemID = a.ItemID, ProductName = a.ProductName, ProductCategoryName = a.ProductCategoryName, ProductCategoryID = a.ProductCategoryID, ParentCategoryID = a.ParentCategoryID ?? 0, MinPrice = a.MinPrice, IsQtyRanged = a.isQtyRanged,  IsFinishedGoods = a.IsFinishedGoods == 1 || a.IsFinishedGoods == 4 ? true : false }));

            return query.ToList<ProductItem>();
            //// return mylist;

        }

        public List<Item> GetAllRetailActiveProducts(long CompanyID)
        {
            return db.Items.Where(i => i.CompanyId == CompanyID && i.IsPublished == true).ToList();
        }

        public List<ItemPriceMatrix> GetRetailProductsPriceMatrix(long CompanyID) // Customer ID , Broker Product List
        {


            db.Configuration.LazyLoadingEnabled = false;

            var qry = from prices in db.ItemPriceMatrices
                      join i in db.Items on prices.ItemId equals i.ItemId

                      where i.CompanyId == CompanyID && i.IsPublished == true && prices.SupplierId == null && ((i.IsQtyRanged == true && prices.QtyRangeFrom > 0) || (i.IsQtyRanged == false && prices.Quantity > 0))

                      select prices;

            return qry.ToList();
        }



        public bool UpdateItem(long itemID, long? templateID)
        {
            bool result = false;
            Item tblItemProduct = null;

            try
            {

                tblItemProduct = db.Items.Where(item => item.ItemId == itemID).FirstOrDefault();

                if (tblItemProduct != null)
                {

                    tblItemProduct.TemplateId = templateID.HasValue && templateID.Value > 0 ? templateID : tblItemProduct.TemplateId;

                    result = db.SaveChanges() > 0 ? true : false;
                }

            }
            catch (Exception)
            {
                result = false;
                throw;
            }

            return result;

        }

        public List<Item> GetItemsWithAttachmentsByOrderID(long OrderID)
        {
            try
            {
                db.Configuration.LazyLoadingEnabled = false;
                //filter the items which are of type delivery i.e. itemtype = 2
                return db.Items.Include("ItemAttachments").Where(i => i.EstimateId == OrderID && (i.ItemType == null || i.ItemType != 2)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public Item GetItemWithSections(long itemID)
        {
            try
            {
                return db.Items.Include("itemSections.sectioncostcentres").Where(i => i.ItemId == itemID).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void AddAttachment(ItemAttachment AttachmentObject)
        {
            try
            {
                db.ItemAttachments.Add(AttachmentObject);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int GetSavedDesignCountByContactId(long ContactID)
        {


            long sameItem = 0;


            List<SaveDesignView> NewList = new List<SaveDesignView>();
            List<SaveDesignView> ListsavedDesign = (from savedDesign in db.SaveDesignViews
                                                    where savedDesign.ContactID == ContactID
                                                    select savedDesign).ToList();

            foreach (var s in ListsavedDesign)
            {
                if (sameItem > 0 && s.ItemID == sameItem)
                {

                }
                else
                {
                    sameItem = s.ItemID;
                    NewList.Add(s);
                }
            }

            return NewList.Count;
        }
        public string GetProductNameByItemId(long ItemId)
        {
            try
            {
                db.Configuration.LazyLoadingEnabled = false;
                return db.Items.Where(
                     i =>
                         i.ItemId == ItemId).Select(n => n.ProductName).FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public IEnumerable<Item> GetProductsByCompanyID(long CompanyId)
        {
            try
            {
                db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;

                return db.Items.Where(c => c.CompanyId == CompanyId && (c.IsPublished == null || c.IsPublished == true) && (c.IsArchived == null || c.IsArchived == false) && c.EstimateId == null).OrderBy(c => c.ProductName).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void RollBackSpecificDiscountedItemsByVoucherId(long OrderId, double StoreTaxRate, long StoreId, long OrganisationId, long DiscountVoucherId)
        {
            try
            {
                double ItemBaseCharge = 0;

              
                List<Item> CartItems = null;

                CartItems = (from r in db.Items
                             where r.EstimateId == OrderId && r.IsOrderedItem == true && (r.ItemType == null || r.ItemType != (int)ItemTypes.Delivery)
                             select r).ToList();

                List<Item> DelvItems = GetListOfDeliveryItemByOrderID(OrderId).Where(d => d.DiscountVoucherID == DiscountVoucherId).ToList();
                foreach (Item i in DelvItems)
                {
                    CartItems.Add(i);
                }
                if (CartItems != null)
                {
                    var CouponAppliedItems = CartItems.Where(i => i.DiscountVoucherID == DiscountVoucherId).ToList();
                    foreach (Item citem in CouponAppliedItems)
                    {
                        ItemBaseCharge = (citem.Qty1NetTotal ?? 0) + (citem.Qty1CostCentreProfit ?? 0);

                        citem.Tax1 = StoreTaxRate;

                        citem.Qty1Tax1Value = CalculatePercentage(ItemBaseCharge, StoreTaxRate);

                        citem.Qty1GrossTotal = ItemBaseCharge + citem.Qty1Tax1Value;

                        citem.Qty1BaseCharge1 = ItemBaseCharge;

                        citem.Qty1NetTotal = ItemBaseCharge;

                        citem.Qty1CostCentreProfit = null;

                        citem.Qty2CostCentreProfit = null;

                        citem.DiscountVoucherID = null;

                        db.SaveChanges();

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        public  GetCategoryProduct GetPublishedProductByItemID(int itemID)
        {

            return db.GetCategoryProducts.Where(g => g.ItemId == itemID && g.IsPublished == true && g.EstimateId == null).OrderBy(g => g.ProductName).FirstOrDefault();

            
        }
        public bool typeFourItemsStatus(long OrderID)
        {
            bool Result = false;
            long Count = db.Items.Where(i => i.EstimateId == OrderID && i.ProductType== 4).ToList().Count;
            long TotalCount = db.Items.Where(i => i.EstimateId == OrderID && i.IsOrderedItem == true).ToList().Count;
            long counter = TotalCount - Count;
            if (counter== 0)
            {
                Result = true;
            }
            return Result;
        }
        //public List<MPC.Models.Common.TemplateVariable> GetAllVariablesUsedInTemplate(List<MPC.Models.Common.TemplateVariable> lstPageControls, int itemID, int contactID, int propertyID)
        //{
        //    List<MPC.Models.Common.TemplateVariable> defaultValues = new List<MPC.Models.Common.TemplateVariable>();
            
            
        //        var itemObj = db.Items.Where(g => g.ItemId == itemID).SingleOrDefault();
        //        List<MPC.Models.DomainModels.TemplateVariable> lstAllTempVar = db.TemplateVariables.Where(g => g.TemplateId == itemObj.TemplateId).ToList();
        //        List<FieldVariable> lstFieldVar = new List<FieldVariable>();
                
        //        var listing = _myCompanyService.GetListingByListingID(propertyID);
        //        int listingAgentCount = 0;
        //        int listingOFIDCount = 0;
        //        int listingVendrosCount = 0;
        //        int listingLinkCount = 0;
        //        int listingFloorPlansCount = 0;
        //        int listingConAgentCount = 0;
        //        List<tbl_ListingAgent> listingAgents = oManager.GetListingAgentsByListingID(propertyID); //Listing Agents
        //        List<tbl_ListingOFID> listingOFIDs = oManager.GetListingOFIDsByListingID(propertyID); //Listing OFIDs
        //        List<tbl_ListingFloorPlan> listingFloorPlans = oManager.GetListingFloorPlansByListingID(propertyID); //Listing Floorplans
        //        List<tbl_ListingLink> listingLinks = oManager.GetListingLinksByListingID(propertyID); //Listing Links
        //        List<tbl_ListingConjunctionAgent> listingConjuctionAgents = oManager.GetListingConjunctionAgentsByListingID(propertyID); //Listing ConjunctionAgents
        //        List<tbl_ListingVendor> listingVendors = oManager.GetListingVendorsByListingID(propertyID); //Listing Vendors
        //        var contact = db.tbl_contacts.Where(g => g.ContactID == contactID).SingleOrDefault();
        //        var company = db.tbl_contactcompanies.Where(g => g.ContactCompanyID == contact.ContactCompanyID).SingleOrDefault();
        //        foreach (var tempVar in lstAllTempVar)
        //        {
        //            var fieldVar = db.tbl_FieldVariables.Where(g => g.VariableID == tempVar.VariableID).SingleOrDefault();
        //            if (fieldVar != null)
        //            {
        //                lstFieldVar.Add(fieldVar);
        //            }
        //        }
        //        foreach (var item in lstFieldVar)
        //        {
        //            if (item.VariableType != 1)
        //            {


        //                //add controls to current section
        //                var keyValue = 0;
        //                string fieldValue = string.Empty;

        //                switch (item.RefTableName)
        //                {
        //                    case "tbl_Listing":
        //                        fieldValue = Convert.ToString(listing.GetType().GetProperty(item.CriteriaFieldName).GetValue(listing, null));
        //                        break;
        //                    case "tbl_ListingImage":
        //                        //already present
        //                        break;
        //                    case "tbl_ListingAgent":

        //                        if (listingAgents.Count > listingAgentCount)
        //                        {
        //                            fieldValue = Convert.ToString(listingAgents[listingAgentCount].GetType().GetProperty(item.CriteriaFieldName).GetValue(listingAgents[listingAgentCount], null));
        //                        }
        //                        break;
        //                    case "tbl_ListingOFID":

        //                        if (listingOFIDs.Count > listingOFIDCount)
        //                        {
        //                            fieldValue = Convert.ToString(listingOFIDs[listingOFIDCount].GetType().GetProperty(item.CriteriaFieldName).GetValue(listingOFIDs[listingOFIDCount], null));
        //                        }
        //                        listingOFIDCount++;
        //                        break;
        //                    case "tbl_ListingVendor":

        //                        if (listingVendors.Count > listingVendrosCount)
        //                        {
        //                            fieldValue = Convert.ToString(listingVendors[listingVendrosCount].GetType().GetProperty(item.CriteriaFieldName).GetValue(listingVendors[listingVendrosCount], null));
        //                        }
        //                        listingVendrosCount++;
        //                        break;
        //                    case "tbl_ListingLink":

        //                        if (listingLinks.Count > listingLinkCount)
        //                        {
        //                            fieldValue = Convert.ToString(listingLinks[listingLinkCount].GetType().GetProperty(item.CriteriaFieldName).GetValue(listingLinks[listingLinkCount], null));
        //                        }

        //                        listingLinkCount++;
        //                        break;
        //                    case "tbl_ListingFloorPlan":

        //                        if (listingFloorPlans.Count > listingFloorPlansCount)
        //                        {
        //                            fieldValue = Convert.ToString(listingFloorPlans[listingFloorPlansCount].GetType().GetProperty(item.CriteriaFieldName).GetValue(listingFloorPlans[listingFloorPlansCount], null));
        //                        }
        //                        listingFloorPlansCount++;
        //                        break;
        //                    case "tbl_ListingConjunctionAgent":

        //                        if (listingConjuctionAgents.Count > listingConAgentCount)
        //                        {
        //                            fieldValue = Convert.ToString(listingConjuctionAgents[listingConAgentCount].GetType().GetProperty(item.CriteriaFieldName).GetValue(listingConjuctionAgents[listingConAgentCount], null));
        //                        }
        //                        listingConAgentCount++;
        //                        break;
        //                    case "tbl_contacts":
        //                        keyValue = contactID;
        //                        fieldValue = DynamicQueryToGetRecord(item.CriteriaFieldName, item.RefTableName, item.KeyField, keyValue);
        //                        break;
        //                    case "tbl_contactcompanies":
        //                        keyValue = company.ContactCompanyID;
        //                        fieldValue = DynamicQueryToGetRecord(item.CriteriaFieldName, item.RefTableName, item.KeyField, keyValue);
        //                        break;
        //                    case "tbl_addresses":
        //                        keyValue = contact.AddressID;
        //                        fieldValue = DynamicQueryToGetRecord(item.CriteriaFieldName, item.RefTableName, item.KeyField, keyValue);
        //                        break;
        //                    default:
        //                        break;
        //                }
        //                TemplateVariable tVar = new TemplateVariable(item.VariableTag, fieldValue);
        //                defaultValues.Add(tVar);
        //            }
        //            else //General Variable
        //            {
        //                int keyValue = 0;
        //                string fieldValue = string.Empty;

        //                switch (item.RefTableName)
        //                {
        //                    case "tbl_contacts":
        //                        keyValue = contactID;
        //                        fieldValue = DynamicQueryToGetRecord(item.CriteriaFieldName, item.RefTableName, item.KeyField, keyValue);
        //                        break;
        //                    case "tbl_contactcompanies":

        //                        keyValue = company.ContactCompanyID;
        //                        fieldValue = DynamicQueryToGetRecord(item.CriteriaFieldName, item.RefTableName, item.KeyField, keyValue);
        //                        break;
        //                    case "tbl_addresses":
        //                        keyValue = contact.AddressID;
        //                        fieldValue = DynamicQueryToGetRecord(item.CriteriaFieldName, item.RefTableName, item.KeyField, keyValue);
        //                        break;
        //                    case "tbl_section_flags":
        //                        using (MPCEntities dbContext = new MPCEntities())
        //                        {
        //                            keyValue = company.FlagID;
        //                            fieldValue = DynamicQueryToGetRecord(item.CriteriaFieldName, item.RefTableName, item.KeyField, keyValue);
        //                        }
        //                        break;
        //                    case "tbl_ContactDepartments":
        //                        if (contact.DepartmentID.HasValue)
        //                        {
        //                            keyValue = contact.DepartmentID.Value;
        //                            fieldValue = DynamicQueryToGetRecord(item.CriteriaFieldName, item.RefTableName, item.KeyField, keyValue);
        //                        }
        //                        break;
        //                    default:
        //                        break;
        //                }

        //                TemplateVariable tVar = new TemplateVariable(item.VariableTag, fieldValue);
        //                defaultValues.Add(tVar);
        //            }
        //        }
            
        //    foreach (var obj in defaultValues)
        //    {
        //        var lstObj = lstPageControls.Where(g => g.Name == obj.Name).SingleOrDefault();
        //        if (lstObj == null)
        //        {
        //            lstPageControls.Add(obj);
        //        }
        //    }


        //    return lstPageControls;
        //}
    
        public List<usp_ExportStoreProductsAndPrices_Result> getExportedItems(long Companyid)
        {
            try
            {
                List<usp_ExportStoreProductsAndPrices_Result> items = db.usp_ExportStoreProductsAndPrices(Companyid, OrganisationId).ToList();

                return items;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private string specialCharactersEncoder(string value)
        {
            value = value.Replace("/", "-");
            value = value.Replace(" ", "-");
            value = value.Replace(";", "-");
            value = value.Replace("&#34;", "");
            value = value.Replace("&", "");
            value = value.Replace("+", "");
            return value;
        }

        public List<GetCategoryProduct> GetRetailFeaturedPublishedProducts()
        {
            try
            {
                List<GetCategoryProduct> recordds =
              db.GetCategoryProducts.Where(
                  g => g.IsPublished == true && g.EstimateId == null && g.IsFeatured == true && (g.IsArchived == null || g.IsArchived == false))
                  .OrderBy(g => g.ProductName)
                  .ToList();
                recordds = recordds.OrderBy(s => s.SortOrder).ToList();
                return recordds;
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }

   
    }
}

