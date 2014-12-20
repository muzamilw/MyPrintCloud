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

        public Item CloneItem(int itemID, double CurrentTotal, int RefItemID, long OrderID, int CustomerID, double Quantity, int TemplateID, int StockID, List<AddOnCostsCenter> SelectedAddOnsList, bool isCorporate, bool isSavedDesign, bool isCopyProduct, Company objCompany, CompanyContact objContact,Company NewCustomer)
        {
            Template clonedTemplate = null;
          
            ItemSection tblItemSectionCloned = null;
            ItemAttachment Attacments = null;
            SectionCostcentre tblISectionCostCenteresCloned = null;
            Item newItem = null;


            double netTotal = 0;
            double grossTotal = 0;
            int clonedNewItemID = 0;
            double CompanyTaxRate = 0;


            Item tblItemProduct = GetItemToClone(itemID);
            //******************new item*********************
            newItem = Clone<Item>(tblItemProduct);

            newItem.ItemId = 0;
            newItem.IsPublished = false;
            newItem.RefItemId = itemID; // the refrencedid
               
                //newItem.EstimateId = OrderID; //orderid
              //  newItem.CompanyId = CustomerID; //customerid
                newItem.StatusId = (short)ItemStatuses.ShoppingCart; //tblStatuses.StatusID; //shopping cart
              //  newItem.Qty1 = Convert.ToInt32(Quantity); //qty
               // newItem.Qty1BaseCharge1 = CurrentTotal; //productSelection.PriceTotal + productSelection.AddonTotal; //item price
             //   newItem.Qty1Tax1Value = CompanyTaxRate; // say vat
              //  newItem.Qty1NetTotal = netTotal;
              //  newItem.Qty1GrossTotal = grossTotal;
                newItem.InvoiceId = null;
                newItem.IsOrderedItem = false;
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
                        tblItemSectionCloned.SectionCostcentres.Add(tblISectionCostCenteresCloned);
                    }
                }
            }
            //Copy Template if it does exists
            if (newItem.TemplateId.HasValue && newItem.TemplateId.Value > 0)
            {
                if (isCorporate)
                {
                    System.Data.Objects.ObjectResult<int?> result = null; // db.sp_cloneTemplate(newItem.TemplateId.Value, 0, "");

                   int? clonedTemplateID =  result.Single();
                    clonedTemplate = db.Templates.Where(g => g.ProductId == clonedTemplateID).Single();


                    // saving water mark string added by saqib and copied by MZ here


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
                        lstPageControls = ResolveVariables(lstFieldVariabes,objContact);
                        ResolveTemplateVariables(clonedTemplate.ProductId, objCompany, objContact, StoreMode.Corp,lstPageControls);
                    }

                }

            }


            if (db.SaveChanges() > 0)
            {
                if (clonedTemplate != null && isCorporate)
                {
                    newItem.TemplateId = clonedTemplate.ProductId;
                    TemplateID = clonedTemplate.ProductId;

                    CopyTemplatePaths(clonedTemplate);
                }
              
                    newItem.TemplateId = TemplateID;
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
            productItem = db.Items.Include("ItemSection.SectionCostcentre").Where(item => item.ItemId == itemID).FirstOrDefault<Item>();
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
        public bool ResolveTemplateVariables(int productID, Company objCompany, CompanyContact objContact, StoreMode objMode, List<Models.Common.TemplateVariable> lstPageControls)
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


        #endregion
    }
}
