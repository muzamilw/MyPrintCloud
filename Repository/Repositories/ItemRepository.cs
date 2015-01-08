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
using System.Reflection;
using System.Web;
using Newtonsoft.Json;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using WebSupergoo.ABCpdf8;


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
        public List<GetCategoryProduct> GetRetailOrCorpPublishedProducts(int ProductCategoryID)
        {

            List<GetCategoryProduct> recordds = db.GetCategoryProducts.Where(g => g.IsPublished == true && g.EstimateId == null && g.ProductCategoryId == ProductCategoryID).OrderBy(g => g.ProductName).ToList();
            recordds = recordds.OrderBy(s => s.SortOrder).ToList();
            return recordds;
        }

        //public ItemStockOption GetFirstStockOptByItemID(int ItemId, int CompanyId)
        //{
        //        if (CompanyId > 0)
        //        {
        //            return db.ItemStockOptions.Where(i => i.ItemId == ItemId && i.CompanyId == CompanyId && i.OptionSequence == 1).FirstOrDefault();
        //        }
        //        else
        //        {
        //            return db.ItemStockOptions.Where(i => i.ItemId == ItemId && i.CompanyId == null && i.OptionSequence == 1).FirstOrDefault();
        //        }
        //}

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
         
                return db.ItemPriceMatrices.Where(i => i.ItemId == ItemId && i.SupplierId == null).ToList();
        }

        public Item CloneItem(int itemID, double CurrentTotal, int RefItemID, long OrderID, int CustomerID, double Quantity, int TemplateID, int StockID, List<AddOnCostsCenter> SelectedAddOnsList, bool isSavedDesign, bool isCopyProduct, long objContactID,Company NewCustomer)
        {
            Template clonedTemplate = new Template();
          
            ItemSection tblItemSectionCloned = new ItemSection();
            ItemAttachment Attacments = new ItemAttachment();
            SectionCostcentre tblISectionCostCenteresCloned = new SectionCostcentre();
            Item newItem = new Item();


            double netTotal = 0;
            double grossTotal = 0;
            int clonedNewItemID = 0;
            double CompanyTaxRate = 0;


            Item tblItemProduct = GetItemToClone(itemID);
            //******************new item*********************
            newItem = Clone<Item>(tblItemProduct);

            newItem.ItemId = 0;
            newItem.IsPublished = false;
             // the refrencedid
               
                //newItem.EstimateId = OrderID; //orderid
              //  newItem.CompanyId = CustomerID; //customerid
                newItem.StatusId = (short)ItemStatuses.ShoppingCart; //tblStatuses.StatusID; //shopping cart
              //  newItem.Qty1 = Convert.ToInt32(Quantity); //qty
               // newItem.Qty1BaseCharge1 = CurrentTotal; //productSelection.PriceTotal + productSelection.AddonTotal; //item price
             //   newItem.Qty1Tax1Value = CompanyTaxRate; // say vat
              //  newItem.Qty1NetTotal = netTotal;
              //  newItem.Qty1GrossTotal = grossTotal;
                newItem.InvoiceId = null;
            if(isCopyProduct)
                newItem.IsOrderedItem = true;
            else
            {
                newItem.IsOrderedItem = false;
                newItem.RefItemId = itemID;
            }
                
                newItem.ProductType = tblItemProduct.ProductType;
            //    newItem.IsMarketingBrief = tblItemProduct.IsMarketingBrief;
           //     newItem.EstimateProductionTime = tblItemProduct.EstimateProductionTime;
          //      newItem.IsStockControl = tblItemProduct.IsStockControl;
          //      newItem.DefaultItemTax = tblItemProduct.DefaultItemTax;
            //}

            // Default Mark up rate will be always 0 ...

            Markup markup = (from c in db.Markups
                             where c.MarkUpId == 1 && c.MarkUpRate == 0
                             select c).FirstOrDefault();

            if (markup.MarkUpId != null)
                newItem.Qty1MarkUpId1 = (int)markup.MarkUpId;  //markup id
            newItem.Qty1MarkUp1Value = markup.MarkUpRate;

            db.Items.Add(newItem); //dbcontext added

            //*****************Existing item Sections and cost Centeres*********************************
            foreach (ItemSection tblItemSection in tblItemProduct.ItemSections.ToList())
            {
                tblItemSectionCloned = Clone<ItemSection>(tblItemSection);
                tblItemSectionCloned.ItemSectionId = 0;
                tblItemSectionCloned.ItemId = newItem.ItemId;
                db.ItemSections.Add(tblItemSectionCloned); //ContextAdded

                //*****************Section Cost Centeres*********************************
                if (tblItemSection.SectionCostcentres.Count > 0)
                {
                    foreach (SectionCostcentre tblSectCostCenter in tblItemSection.SectionCostcentres.ToList())
                    {
                        tblISectionCostCenteresCloned = Clone<SectionCostcentre>(tblSectCostCenter);
                        tblISectionCostCenteresCloned.SectionCostcentreId = 0;
                        tblISectionCostCenteresCloned.ItemSectionId = tblItemSectionCloned.ItemSectionId;
                        db.SectionCostcentres.Add(tblISectionCostCenteresCloned);
                    }
                }
            }
            //Copy Template if it does exists
            
            if (newItem.TemplateId.HasValue && newItem.TemplateId.Value > 0)
            {
                if (newItem.TemplateType == 1 || newItem.TemplateType == 2)
                {
                    int result = db.sp_cloneTemplate(newItem.TemplateId.Value, 0, "");
                  //  System.Data.Objects.ObjectResult<int?> result = db.sp_cloneTemplate(newItem.TemplateId.Value, 0, "");
                    int? clonedTemplateID = result;
                    clonedTemplate = db.Templates.Where(g => g.ProductId == clonedTemplateID).Single();

                    var oCutomer = NewCustomer;
                    if (oCutomer != null)
                    {
                        clonedTemplate.TempString = oCutomer.WatermarkText;
                        clonedTemplate.isWatermarkText = oCutomer.isTextWatermark;
                        if (oCutomer.isTextWatermark == false)
                        {
                            clonedTemplate.TempString = HttpContext.Current.Server.MapPath(oCutomer.WatermarkText);
                        }

                    }
                    List<FieldVariable> lstFieldVariabes = GeyFieldVariablesByItemID(itemID);
                    if (lstFieldVariabes != null && lstFieldVariabes.Count > 0)
                    {
                        List<Models.Common.TemplateVariable> lstPageControls = new List<Models.Common.TemplateVariable>();
                        CompanyContact contact = db.CompanyContacts.Where(c => c.ContactId == objContactID).FirstOrDefault();
                        lstPageControls = ResolveVariables(lstFieldVariabes, contact);
                        ResolveTemplateVariables(clonedTemplate.ProductId, contact, StoreMode.Corp,lstPageControls);
                    }

                }

            }


            if (db.SaveChanges() > 0)
            {
                if (clonedTemplate != null && (newItem.TemplateType == 1 || newItem.TemplateType == 2))
                {
                    newItem.TemplateId = clonedTemplate.ProductId;
                    TemplateID = clonedTemplate.ProductId;

                  //  CopyTemplatePaths(clonedTemplate);
                }
                clonedNewItemID = (int)newItem.ItemId;
                SaveAdditionalAddonsOrUpdateStockItemType(SelectedAddOnsList, (int)newItem.ItemId, StockID, 0, isCopyProduct); // additional addon required the newly inserted cloneditem
             
                newItem.ItemCode = "ITM-0-001-" + newItem.ItemId;
                db.SaveChanges();
            }
            else
                throw new Exception("Nothing happened");



            return newItem;
        }
        // gettting field variables by itemid
        public List<FieldVariable> GeyFieldVariablesByItemID(int itemId)
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

                List<FieldVariable> finalList = (List<FieldVariable>)lstFieldVariables.OrderBy(item => item.VariableSectionId).ToList();

                return finalList;
           
        }

       
        public List<Models.Common.TemplateVariable> ResolveVariables(List<FieldVariable> lstFieldVariabes,CompanyContact objContact)
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
                            fieldValue = DynamicQueryToGetRecord(item.CriteriaFieldName, item.RefTableName, item.KeyField, keyValue);
                            break;
                        case "Company":
                            keyValue = (int)objContact.CompanyId;
                            fieldValue = DynamicQueryToGetRecord(item.CriteriaFieldName, item.RefTableName, item.KeyField, keyValue);
                            break;
                        case "Address":
                            keyValue = (int)objContact.AddressId;
                            fieldValue = DynamicQueryToGetRecord(item.CriteriaFieldName, item.RefTableName, item.KeyField, keyValue);
                            break;
                        default:
                            break;
                    }

                    Models.Common.TemplateVariable imgTempVar = new Models.Common.TemplateVariable(Convert.ToString(keyValue), fieldValue);

                    templateVariables.Add(imgTempVar);
                  
                }
            }
            return templateVariables;
        }

        public string DynamicQueryToGetRecord(string feildname, string tblname, string keyName, int keyValue)
        {
          
            string oResult = null;
            System.Data.Entity.Infrastructure.DbRawSqlQuery<string> result = db.Database.SqlQuery<string>("select top 1 cast(" + feildname + " as varchar(1000)) from " + tblname + " where " + keyName + "= " + keyValue + "", "");
            oResult = result.FirstOrDefault();
            return oResult;

        }

        public int CopyTemplatePaths(Template clonedTemplate)
        {
            int result = 0;

            try
            {
                result = clonedTemplate.ProductId;

                string BasePath = System.Web.HttpContext.Current.Server.MapPath("~/DesignEngine/Designer/Products/");
                //result = dbContext.sp_cloneTemplate(ProductID, SubmittedBy, SubmittedByName).First().Value;

                string targetFolder = System.Web.HttpContext.Current.Server.MapPath("~/DesignEngine/Designer/Products/" + result.ToString());
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
                            string oldproductid = oTemplatePage.BackgroundFileName.Substring(0, oTemplatePage.BackgroundFileName.IndexOf("/"));
                            if (File.Exists(BasePath + oldproductid + "/" + "templatImgBk" + oTemplatePage.PageNo + ".jpg"))
                            {


                                File.Copy(Path.Combine(BasePath, oldproductid + "/" + "templatImgBk" + oTemplatePage.PageNo + ".jpg"), BasePath + result.ToString() + "/" + "templatImgBk" + oTemplatePage.PageNo + ".jpg");

                            }
                        }

                        File.Copy(Path.Combine(BasePath, oTemplatePage.BackgroundFileName), BasePath + result.ToString() + "/" + oTemplatePage.BackgroundFileName.Substring(oTemplatePage.BackgroundFileName.IndexOf("/"), oTemplatePage.BackgroundFileName.Length - oTemplatePage.BackgroundFileName.IndexOf("/")));
                        oTemplatePage.BackgroundFileName = result.ToString() + "/" + oTemplatePage.BackgroundFileName.Substring(oTemplatePage.BackgroundFileName.IndexOf("/"), oTemplatePage.BackgroundFileName.Length - oTemplatePage.BackgroundFileName.IndexOf("/"));

                    }
                }


                //skip concatinating the path if its a placeholder, cuz place holder is kept in a different path and doesnt need to be copied.
                oTemplate.TemplateObjects.Where(tempObject => tempObject.ObjectType == 3 && tempObject.IsQuickText != true).ToList().ForEach(item =>
                {

                    string filepath = item.ContentString.Substring(item.ContentString.IndexOf("Designer/Products/") + "Designer/Products/".Length, item.ContentString.Length - (item.ContentString.IndexOf("Designer/Products/") + "Designer/Products/".Length));
                    item.ContentString = "Designer/Products/" + result.ToString() + filepath.Substring(filepath.IndexOf("/"), filepath.Length - filepath.IndexOf("/"));

                });
                //foreach (var item in dbContext.TemplateObjects.Where(g => g.ProductID == result && g.ObjectType == 3))
                //{
                //    string filepath = item.ContentString.Substring(item.ContentString.IndexOf("DesignEngine/Designer/Products/") + "DesignEngine/Designer/Products/".Length, item.ContentString.Length - (item.ContentString.IndexOf("DesignEngine/Designer/Products/") + "DesignEngine/Designer/Products/".Length));
                //    item.ContentString = "DesignEngine/Designer/Products/" + result.ToString() + filepath.Substring(filepath.IndexOf("/"), filepath.Length - filepath.IndexOf("/"));
                //}

                //

                //copy the background images



                //var backimgs = dbContext.TemplateBackgroundImages.Where(g => g.ProductID == result);

                oTemplate.TemplateBackgroundImages.ToList().ForEach(item =>
                {

                    string filePath = System.Web.HttpContext.Current.Server.MapPath("~/DesignEngine/Designer/Products/" + item.ImageName);
                    string filename;

                    string ext = Path.GetExtension(item.ImageName);

                    // generate thumbnail 
                    if (!ext.Contains("svg"))
                    {
                        string[] results = item.ImageName.Split(new string[] { ext }, StringSplitOptions.None);
                        string destPath = results[0] + "_thumb" + ext;
                        string ThumbPath = System.Web.HttpContext.Current.Server.MapPath("~/DesignEngine/Designer/Products/" + destPath);
                        FileInfo oFileThumb = new FileInfo(ThumbPath);
                        if (oFileThumb.Exists)
                        {
                            string oThumbName = oFileThumb.Name;
                            oFileThumb.CopyTo(System.Web.HttpContext.Current.Server.MapPath("~/DesignEngine/Designer/Products/" + result.ToString() + "/" + oThumbName), true);
                        }
                        //  objSvc.GenerateThumbNail(sourcePath, destPath, 98);
                    }


                    FileInfo oFile = new FileInfo(filePath);

                    if (oFile.Exists)
                    {
                        filename = oFile.Name;
                        item.ImageName = result.ToString() + "/" + oFile.CopyTo(System.Web.HttpContext.Current.Server.MapPath("~/DesignEngine/Designer/Products/" + result.ToString() + "/" + filename), true).Name;
                    }


                });


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

        public Item GetItemToClone(int itemID)
        {
            Item productItem = null;
            productItem = db.Items.Include("ItemSections.SectionCostcentres").Where(item => item.ItemId == itemID).FirstOrDefault<Item>();
            return productItem;

        }
        public int CopyTemplatePathsSavedDesigns(Template clonedTemplate)
        {
            int result = 0;

            try
            {
                result = clonedTemplate.ProductId;

                string BasePath = System.Web.HttpContext.Current.Server.MapPath("~/DesignEngine/Designer/Products/");

                string targetFolder = System.Web.HttpContext.Current.Server.MapPath("~/DesignEngine/Designer/Products/" + result.ToString());
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
                            string oldproductid = oTemplatePage.BackgroundFileName.Substring(0, oTemplatePage.BackgroundFileName.IndexOf("/"));
                            if (File.Exists(BasePath + oldproductid + "/" + "templatImgBk" + oTemplatePage.PageNo + ".jpg"))
                            {


                                File.Copy(Path.Combine(BasePath, oldproductid + "/" + "templatImgBk" + oTemplatePage.PageNo + ".jpg"), BasePath + result.ToString() + "/" + "templatImgBk" + oTemplatePage.PageNo + ".jpg");

                            }
                        }

                        File.Copy(Path.Combine(BasePath, oTemplatePage.BackgroundFileName), BasePath + result.ToString() + "/" + oTemplatePage.BackgroundFileName.Substring(oTemplatePage.BackgroundFileName.IndexOf("/"), oTemplatePage.BackgroundFileName.Length - oTemplatePage.BackgroundFileName.IndexOf("/")));
                        oTemplatePage.BackgroundFileName = result.ToString() + "/" + oTemplatePage.BackgroundFileName.Substring(oTemplatePage.BackgroundFileName.IndexOf("/"), oTemplatePage.BackgroundFileName.Length - oTemplatePage.BackgroundFileName.IndexOf("/"));

                    }
                }


                //skip concatinating the path if its a placeholder, cuz place holder is kept in a different path and doesnt need to be copied.
                oTemplate.TemplateObjects.Where(tempObject => tempObject.ObjectType == 3 && tempObject.IsQuickText != true).ToList().ForEach(item =>
                {

                    string filepath = item.ContentString.Substring(item.ContentString.IndexOf("Designer/Products/") + "Designer/Products/".Length, item.ContentString.Length - (item.ContentString.IndexOf("Designer/Products/") + "Designer/Products/".Length));
                    item.ContentString = "Designer/Products/" + result.ToString() + filepath.Substring(filepath.IndexOf("/"), filepath.Length - filepath.IndexOf("/"));

                });
                oTemplate.TemplateBackgroundImages.ToList().ForEach(item =>
                {

                    string filePath = System.Web.HttpContext.Current.Server.MapPath("~/DesignEngine/Designer/Products/" + item.ImageName);
                    string filename;

                    string ext = Path.GetExtension(item.ImageName);

                    // generate thumbnail 
                    if (!ext.Contains("svg"))
                    {
                        string[] results = item.ImageName.Split(new string[] { ext }, StringSplitOptions.None);
                        string destPath = results[0] + "_thumb" + ext;
                        string ThumbPath = System.Web.HttpContext.Current.Server.MapPath("~/DesignEngine/Designer/Products/" + destPath);
                        FileInfo oFileThumb = new FileInfo(ThumbPath);
                        if (oFileThumb.Exists)
                        {
                            string oThumbName = oFileThumb.Name;
                            oFileThumb.CopyTo(System.Web.HttpContext.Current.Server.MapPath("~/DesignEngine/Designer/Products/" + result.ToString() + "/" + oThumbName), true);
                        }
                    }


                    FileInfo oFile = new FileInfo(filePath);

                    if (oFile.Exists)
                    {
                        filename = oFile.Name;
                        item.ImageName = result.ToString() + "/" + oFile.CopyTo(System.Web.HttpContext.Current.Server.MapPath("~/DesignEngine/Designer/Products/" + result.ToString() + "/" + filename), true).Name;
                    }


                });

                //   db.SaveChanges(0);
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
        public T Clone<T>(T source)
        {
            object item = Activator.CreateInstance(typeof(T));
            List<PropertyInfo> itemPropertyInfoCollection = source.GetType().GetProperties().ToList<PropertyInfo>();
            foreach (PropertyInfo propInfo in itemPropertyInfoCollection)
            {
                if (propInfo.CanRead && (propInfo.PropertyType.IsValueType || propInfo.PropertyType.FullName == "System.String"))
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

        public double GrossTotalCalculation(double netTotal, double stateTaxValue)
        {
            double stateTaxPice = 0;

            stateTaxPice = netTotal + CalculatePercentage(netTotal, stateTaxValue);

            return stateTaxPice;

        }

        public static double CalculatePercentage(double itemValue, double percentageValue)
        {
            double percentValue = 0;

            percentValue = itemValue * (percentageValue / 100);

            return percentValue;
        }


        #region "dynamic resolve template Variables"
        // resolve variables in templates
        public bool ResolveTemplateVariables(int productID, CompanyContact objContact, StoreMode objMode, List<Models.Common.TemplateVariable> lstPageControls)
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
                                        string[] objs = obj.ContentString.Split(new string[] { objVariable.Name }, StringSplitOptions.None);
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
                                                if (Convert.ToInt32(objStyle.characterIndex) <= (lengthCount + variableLength) && Convert.ToInt32(objStyle.characterIndex) >= lengthCount)
                                                {
                                                    InlineTextStyles objToRemove = stylesCopy.Where(g => g.characterIndex == objStyle.characterIndex).SingleOrDefault();
                                                    stylesCopy.Remove(objToRemove);
                                                    stylesRemoved++;
                                                }
                                            }

                                            int diff = objVariable.Value.Length - (variableLength);
                                            foreach (var objStyle in stylesCopy)
                                            {
                                                if (Convert.ToInt32(objStyle.characterIndex) > (lengthCount + objVariable.Name.Length))
                                                    objStyle.characterIndex = Convert.ToString((Convert.ToInt32(objStyle.characterIndex) + diff));
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
                                        obj.ContentString = obj.ContentString.Replace(objVariable.Name, objVariable.Value);
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
                                        string[] objs = obj.ContentString.Split(new string[] { objVariable.Name }, StringSplitOptions.None);
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
                                                if (Convert.ToInt32(objStyle.characterIndex) <= (lengthCount + variableLength) && Convert.ToInt32(objStyle.characterIndex) >= lengthCount)
                                                {
                                                    InlineTextStyles objToRemove = stylesCopy.Where(g => g.characterIndex == objStyle.characterIndex).SingleOrDefault();
                                                    stylesCopy.Remove(objToRemove);
                                                    stylesRemoved++;
                                                }
                                            }

                                            int diff = objVariable.Value.Length - (variableLength);
                                            foreach (var objStyle in stylesCopy)
                                            {
                                                if (Convert.ToInt32(objStyle.characterIndex) > (lengthCount + objVariable.Name.Length))
                                                    objStyle.characterIndex = Convert.ToString((Convert.ToInt32(objStyle.characterIndex) + diff));
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
                                System.Drawing.Image objImage = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath(localFilePath));
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

                                System.Drawing.Image objImage = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath(localFilePath));
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

        private bool SaveAdditionalAddonsOrUpdateStockItemType(List<AddOnCostsCenter> selectedAddonsList, int? newItemID, int stockID, double BrkerPriceCC, bool isCopyProduct)
        {
            bool result = false;
            ItemSection SelectedtblItemSectionOne = null;

            //Create A new Item Section #1 to pass to the cost center

            SelectedtblItemSectionOne = db.ItemSections.Where(itemSect => itemSect.SectionNo == 1 && itemSect.ItemId == newItemID.Value).FirstOrDefault(); //this.PopulateTblItemSections(newItem.ItemID, productSelection.Quantity, productSelection.CurrentTotal, 1);
            if (isCopyProduct == true)
            {
                result = this.SaveAdditionalAddonsOrUpdateStockItemType(selectedAddonsList, Convert.ToInt32(SelectedtblItemSectionOne.StockItemID1), SelectedtblItemSectionOne, BrkerPriceCC);
            }
            else
            {
                result = this.SaveAdditionalAddonsOrUpdateStockItemType(selectedAddonsList, stockID, SelectedtblItemSectionOne, BrkerPriceCC);
            }




            return result;
        }

        private bool SaveAdditionalAddonsOrUpdateStockItemType(List<AddOnCostsCenter> selectedAddonsList, int stockID, ItemSection SelectedtblItemSectionOne, double CostCenterBPrice)
        {
            SectionCostcentre SelectedtblISectionCostCenteres = null;

            if (SelectedtblItemSectionOne != null)
            {
                //Set or Update the paper Type stockid in the section #1
                if (stockID > 0)
                    this.UpdateStockItemType(SelectedtblItemSectionOne, stockID);

                if (selectedAddonsList != null)
                {
                    // Remove previous Addons
                   ////////// db.SectionCostcentres.Where(c => c.ItemSectionId == SelectedtblItemSectionOne.ItemSectionId && c.IsOptionalExtra == 1).ToList().ForEach(db.SectionCostcentres.Remove());

                    //Create Additional Addons Data
                    for (int i = 0; i < selectedAddonsList.Count; i++)
                    {
                        AddOnCostsCenter addonCostCenter = selectedAddonsList[i];
                        SelectedtblISectionCostCenteres = this.PopulateTblSectionCostCenteres(addonCostCenter, CostCenterBPrice);
                        SelectedtblISectionCostCenteres.IsOptionalExtra = 1; //1 tells that it is the Additional AddOn                 
                        SelectedtblItemSectionOne.SectionCostcentres.Add(SelectedtblISectionCostCenteres);

                    }
                }
            }

            return true;
        }

        public void UpdateStockItemType(ItemSection itemSection, int stockID)
        {
            itemSection.StockItemID1 = stockID;  //always set into the first column
            itemSection.StockItemID2 = null;
            itemSection.StockItemID3 = null;
        }

        private SectionCostcentre PopulateTblSectionCostCenteres(AddOnCostsCenter addOn, double BAddOnPrice)
        {
            if (BAddOnPrice > 0)
            {
                SectionCostcentre tblISectionCostCenteres = new SectionCostcentre
                {
                    CostCentreId = addOn.CostCenterID,
                    IsOptionalExtra = 1,
                    Qty1Charge = BAddOnPrice,
                    Qty1NetTotal = BAddOnPrice,
                    QtyChargeBroker = addOn.ActualPrice,
                };

                return tblISectionCostCenteres;
            }
            else
            {
                SectionCostcentre tblISectionCostCenteres = new SectionCostcentre
                {
                    CostCentreId = addOn.CostCenterID,
                    IsOptionalExtra = 1,
                    Qty1Charge = addOn.ActualPrice,
                    Qty1NetTotal = addOn.Qty1NetTotal
                };

                return tblISectionCostCenteres;
            }
        }

        #endregion


        public Item GetItemById(long itemId) 
        {
            return db.Items.Where(i => i.IsPublished == true && i.ItemId == itemId && i.EstimateId == null).FirstOrDefault();
            //return db.Items.Include("ItemPriceMatrices").Include("ItemSections").Where(i => i.IsPublished == true && i.ItemId == itemId && i.EstimateId == null).FirstOrDefault();
           
        }

        public ProductItem GetItemAndDetailsByItemID(int itemId)
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
                            ProductSpecification = item.ProductSpecification,
                            AllowBriefAttachments = ItemDetail.isAllowMarketBriefAttachment ?? false,
                            BriefSuccessMessage = ItemDetail.MarketBriefSuccessMessage

                        };
            return query.FirstOrDefault<ProductItem>();
            
        }

        public List<ProductMarketBriefQuestion> GetMarketingInquiryQuestionsByItemID(int itemID)
        {

            return db.ProductMarketBriefQuestions.Where(i => i.ItemId == itemID).ToList();
           
        }

        public List<ProductMarketBriefAnswer> GetMarketingInquiryAnswersByQID(int QID)
        {
           
                return db.ProductMarketBriefAnswers.Where(i => i.MarketBriefQuestionId == QID).ToList();
            
        }
        public void CopyAttachments(int itemID, Item NewItem, string OrderCode, bool CopyTemplate, DateTime OrderCreationDate)
        {
            int sideNumber = 1;
            List<ItemAttachment> attchmentRes = GetItemAttactchments(itemID);
            List<ItemAttachment> Newattchments = new List<ItemAttachment>();
            ItemAttachment obj = null;

            foreach (ItemAttachment attachment in attchmentRes)
            {
                obj = new ItemAttachment();

                obj.ApproveDate = attachment.ApproveDate;
                obj.Comments = attachment.Comments;
                obj.ContactId = attachment.ContactId;
                obj.ContentType = attachment.ContentType;
                obj.CompanyId = attachment.CompanyId;
                obj.FileTitle = attachment.FileTitle;
                obj.FileType = attachment.FileType;
                obj.FolderPath = attachment.FolderPath;
                obj.IsApproved = attachment.IsApproved;
                obj.isFromCustomer = attachment.isFromCustomer;
                obj.Parent = attachment.Parent;
                obj.Type = attachment.Type;
                obj.UploadDate = attachment.UploadDate;
                obj.Version = attachment.Version;
                obj.ItemId = NewItem.ItemId;
                if (NewItem.TemplateId > 0)
                {
                    obj.FileName = GetTemplateAttachmentFileName(NewItem.ProductCode, OrderCode, NewItem.ItemCode, "Side" + sideNumber.ToString(), attachment.FolderPath, attachment.FileType, OrderCreationDate); //NewItemID + " Side" + sideNumber + attachment.FileType;
                }
                else
                {
                    obj.FileName = GetAttachmentFileName(NewItem.ProductCode, OrderCode, NewItem.ItemCode, sideNumber.ToString() + "Copy", attachment.FolderPath, attachment.FileType, OrderCreationDate); //NewItemID + " Side" + sideNumber + attachment.FileType;
                }
                sideNumber += 1;
                db.ItemAttachments.Add(obj);
                Newattchments.Add(obj);

                // Copy physical file
                string sourceFileName = null;
                string destFileName = null;
                if (NewItem.TemplateId > 0 && CopyTemplate == true)
                {
                    sourceFileName = HttpContext.Current.Server.MapPath(attachment.FolderPath + System.IO.Path.GetFileNameWithoutExtension(attachment.FileName) + "Thumb.png");
                    destFileName = HttpContext.Current.Server.MapPath(obj.FolderPath + obj.FileName);
                }
                else
                {
                    sourceFileName = HttpContext.Current.Server.MapPath(attachment.FolderPath + attachment.FileName);
                    destFileName = HttpContext.Current.Server.MapPath(obj.FolderPath + obj.FileName);
                }

                if (File.Exists(sourceFileName))
                {
                    File.Copy(sourceFileName, destFileName);

                    // Generate the thumbnail

                    byte[] fileData = File.ReadAllBytes(destFileName);

                    if (obj.FileType == ".pdf" || obj.FileType == ".TIF" || obj.FileType == ".TIFF")
                    {
                        GenerateThumbnailForPdf(fileData, destFileName, false);
                    }
                    else
                    {
                        MemoryStream ms = new MemoryStream();
                        ms.Write(fileData, 0, fileData.Length);

                        CreatAndSaveThumnail(ms, destFileName);
                    }
                }

            }

            db.SaveChanges();
        }
        public List<ItemAttachment> GetItemAttactchments(int itemID)
        {
          
            return (from Attachment in db.ItemAttachments
                    where Attachment.ItemId == itemID
                    select Attachment).ToList();
        }
        public static string GetTemplateAttachmentFileName(string ProductCode, string OrderCode, string ItemCode, string SideCode, string VirtualFolderPath, string extension, DateTime CreationDate)
        {
            string FileName = CreationDate.Year.ToString() + CreationDate.ToString("MMMM") + CreationDate.Day.ToString() + "-" + ProductCode + "-" + OrderCode + "-" + ItemCode + "-" + SideCode + extension;

            return FileName;
        }
        public static string GetAttachmentFileName(string ProductCode, string OrderCode, string ItemCode, string SideCode, string VirtualFolderPath, string extension, DateTime OrderCreationDate)
        {
            string FileName = OrderCreationDate.Year.ToString() + OrderCreationDate.ToString("MMMM") + OrderCreationDate.Day.ToString() + "-" + ProductCode + "-" + OrderCode + "-" + ItemCode + "-" + SideCode + extension;
            //checking whether file exists or not
            while (System.IO.File.Exists(VirtualFolderPath + FileName))
            {
                string fileName1 = System.IO.Path.GetFileNameWithoutExtension(FileName);
                fileName1 += "a";
                FileName = fileName1 + extension;
            }


            return FileName;
        }
        public void GenerateThumbnailForPdf(byte[] PDFFile, string sideThumbnailPath, bool insertCuttingMargin)
        {
            try
            {
                using (Doc theDoc = new Doc())
                {
                    theDoc.Read(PDFFile);
                    theDoc.PageNumber = 1;
                    theDoc.Rect.String = theDoc.CropBox.String;

                    if (insertCuttingMargin)
                    {
                        theDoc.Rect.Inset((int)MPC.Models.Common.Constants.CuttingMargin, (int)MPC.Models.Common.Constants.CuttingMargin);
                    }

                    Stream oImgstream = new MemoryStream();

                    theDoc.Rendering.DotsPerInch = 300;
                    theDoc.Rendering.Save("tmp.png", oImgstream);

                    theDoc.Clear();
                    theDoc.Dispose();

                    CreatAndSaveThumnail(oImgstream, sideThumbnailPath);
                }
                
            }catch(Exception ex)
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
        public bool RemoveCloneItem(long itemID, out List<ArtWorkAttatchment> itemAttatchmetList, out Template clonedTemplateToRemove)
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
            catch(Exception ex)
            {
                throw ex;
            }
          
        
        }


        public bool RemoveCloneItem(Item tblItem, out List<ArtWorkAttatchment> itemAttatchmetList, out Template clonedTemplateToRemove)
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
                        clonedTemplate = RemoveTemplates(tblItem.TemplateId);

                    //Section cost centeres
                    tblItem.ItemSections.ToList().ForEach(itemSection => itemSection.SectionCostcentres.ToList().ForEach(sectCost => db.SectionCostcentres.Remove(sectCost)));


                    //Item Section
                    tblItem.ItemSections.ToList().ForEach(itemsect => db.ItemSections.Remove(itemsect));



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
            Template clonedTemplate = null;

            if (templateID.HasValue && templateID.Value > 0)
            {
                Template tblTemplate = db.Templates.Where(template => template.ProductId == templateID.Value).FirstOrDefault();

                if (tblTemplate != null)
                {
                    //color Style
                    tblTemplate.TemplateColorStyles.ToList().ForEach(tempColorStyle => db.TemplateColorStyles.Remove(tempColorStyle));

                    //backgourd
                    tblTemplate.TemplateBackgroundImages.ToList().ForEach(tempBGImages => db.TemplateBackgroundImages.Remove(tempBGImages));

                    //font
                //    tblTemplate.templat.ToList().ForEach(tempFonts => db dbContext.DeleteObject(tempFonts));
                     
                    
                    //object
                    tblTemplate.TemplateObjects.ToList().ForEach(tempObj => db.TemplateObjects.Remove(tempObj));

                    //Page
                    tblTemplate.TemplatePages.ToList().ForEach(tempPage => db.TemplatePages.Remove(tempPage));


                    // the template to remove the files in web.ui
                    clonedTemplate = Clone<Template>(tblTemplate);

                    //finally template it self
                    db.Templates.Remove(tblTemplate);
                }
            }

            return clonedTemplate;

        }
        public ArtWorkAttatchment PopulateUploadedAttactchment(ItemAttachment attatchment)
        {

           UploadFileTypes resultUploadedFileType;

            ArtWorkAttatchment itemAttactchment = new ArtWorkAttatchment()
            {
                FileName = attatchment.FileName,
                FileTitle = attatchment.FileTitle,
                FileExtention = attatchment.FileType,
                FolderPath = attatchment.FolderPath,
                UploadFileType = Enum.TryParse(attatchment.Type, true, out resultUploadedFileType) ? resultUploadedFileType : UploadFileTypes.None
            };


            return itemAttactchment;
        }
        private bool ValidateIfTemplateIDIsAlreadyBooked(long itemID, long? templateID)
        {
            bool result = false;

            if (templateID.HasValue && templateID > 0)
            {
                int bookedCount = db.Items.Where(item => item.ItemId != itemID && item.TemplateId == templateID.Value).Count();
                if (bookedCount > 0)
                    result = true;
            }

            return result;
        }
       
        #endregion
    }
}
