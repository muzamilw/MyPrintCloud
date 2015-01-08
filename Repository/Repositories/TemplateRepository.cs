using System.Data.Entity;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;
using System.Linq;
using System;
using System.Collections.Generic;
using MPC.Models.Common;
namespace MPC.Repository.Repositories
{
    /// <summary>
    /// Template Repository
    /// </summary>
    public class TemplateRepository : BaseRepository<Template>, ITemplateRepository
    {
        #region privte
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public TemplateRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Template> DbSet
        {
            get
            {
                return db.Templates;
            }
        }

        #endregion

        #region public

        /// <summary>
        /// Find Template
        /// </summary>
        public Template Find(int id)
        {
            return DbSet.Find(id);
        }
        /// <summary>
        ///  Get template object by template id 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public Template GetTemplate(long productID)
        {
           // db.Configuration.LazyLoadingEnabled = true;
            var template = db.Templates.Where(g => g.ProductId == productID).SingleOrDefault();
            return template;

        }

        public List<MatchingSets> BindTemplatesList(string TemplateName, int pageNumber,long CustomerID,int CompanyID)
        {
            try
            {
               

                //The Remote mapped categoryName
                int totalPagesCount = 0;
                int SearchCount = 0;


                List<MatchingSets> templatesList = GetTemplateDataFromService(TemplateName, pageNumber - 1, out totalPagesCount, out SearchCount,CustomerID,CompanyID);


                if (templatesList.Count > 0)
                {
                    foreach (var i in templatesList)
                    {
                        ProductCategory pRec = GetDisplayOrderAndSave(Convert.ToInt16(i.ProductCategoryID));
                        if (pRec != null)
                        {
                            i.DisplayOrder = pRec.DisplayOrder;
                        }
                    }
                    templatesList = templatesList.OrderBy(c => c.DisplayOrder).ToList();

                    return templatesList;
                }
                else
                {
                   
                    return null;
                }

            }
            catch (Exception ex)
            {
               
                return null;
            }
        }

        private List<MatchingSets> GetTemplateDataFromService(string templateName, int pageNumber, out int totalPagesCount, out int SearchCount, long CustomerID,int CompanyID)
        {
          
            using (GlobalTemplateDesigner.TemplateSvcSPClient tsc = new GlobalTemplateDesigner.TemplateSvcSPClient())
            {

                string[] categoryNames = (from c in GetMappedCategoryNames(false, CompanyID).ToList()
                                          select c.TemplateDesignerMappedCategoryName).ToArray();
                string NewTemplateName = templateName.Replace("Copy", "");
                List<MatchingSets> objList = new List<MatchingSets>();
                int PageSize = 15;
                var tempList = tsc.GetTemplatesbyTemplateName(out totalPagesCount, out SearchCount, NewTemplateName + ":" + CustomerID.ToString(), categoryNames, pageNumber, PageSize).ToList();
                foreach (var item in tempList)
                {
                    MatchingSets objMatchingSet = new MatchingSets();
                    objMatchingSet.BaseColorID = item.BaseColorID ?? 0;
                    objMatchingSet.CategoryName = item.CategoryName;
                    objMatchingSet.Code = item.Code;
                    objMatchingSet.Description = item.Description;
                    objMatchingSet.DisplayOrder = item.DisplayOrder;
                    objMatchingSet.FullView = item.FullView;
                    objMatchingSet.Image = item.Image;
                    objMatchingSet.IsDisabled = item.IsDisabled ?? false;
                    objMatchingSet.IsDoubleSide = item.IsDoubleSide;
                    objMatchingSet.isEditorChoice = item.isEditorChoice ?? false;
                    objMatchingSet.IsPrivate = item.IsPrivate ?? false;
                    objMatchingSet.IsUseBackGroundColor = item.IsUseBackGroundColor ?? false;
                    objMatchingSet.MultiPageCount = item.MultiPageCount ?? 0;
                    objMatchingSet.Orientation = item.Orientation ?? 0;
                    objMatchingSet.ProductCategoryID = item.ProductCategoryID;
                    objMatchingSet.ProductID = item.ProductID;
                    objMatchingSet.ProductName = item.ProductName;
                    objMatchingSet.PTempId = item.PTempId ?? 0;
                    objMatchingSet.SLThumbnail = item.SLThumbnail;
                    objMatchingSet.Status = item.Status ?? 0;
                    objMatchingSet.SubmittedBy = item.SubmittedBy ?? 0;
                    objMatchingSet.TemplateOwner = item.TemplateOwner ?? 0;
                    objMatchingSet.SuperView = item.SuperView;
                    objMatchingSet.Thumbnail = item.Thumbnail;
                    objList.Add(objMatchingSet);

                }
                //tempList = tempList.Where(c => c.Status == 3).ToList();

                return objList;

            }

        }

        public List<ProductCategoriesView> GetMappedCategoryNames(bool isClearCache,int companyID)
        {
            List<ProductCategoriesView> mappedCategories = null;
            //if (HttpContext.Current.Cache["MappedCategoryNames"] == null || isClearCache == true)
            //{
            mappedCategories = GetProductDesignerMappedCategoryNames(companyID);
            //    HttpContext.Current.Cache["MappedCategoryNames"] = mappedCategories;
            //}
            //else
            //{
            //    mappedCategories = HttpContext.Current.Cache["MappedCategoryNames"] as List<vw_ProductCategories>;
            //}

            return mappedCategories;
        }

        public List<ProductCategoriesView> GetProductDesignerMappedCategoryNames(int CompanyID)
        {
            try
            {

                return (from c in db.ProductCategoriesViews
                        where !string.IsNullOrEmpty(c.TemplateDesignerMappedCategoryName) && c.CompanyId == CompanyID
                        select c).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ProductCategoriesView GetMappedCategory(string CatName,int CID)
        {
            try
            {
                return (from c in GetMappedCategoryNames(false,CID).ToList()
                        where c.TemplateDesignerMappedCategoryName == CatName
                                                  select c).FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ProductCategory GetDisplayOrderAndSave(int pCID)
        {
           
            return (from c in db.ProductCategories
                    where c.ProductCategoryId == pCID
                    select c).FirstOrDefault();
        }

        public string GetTemplateNameByTemplateID(int tempID)
        {
            try
            {
                return db.Templates.Where(t => t.ProductId == tempID).Select(s => s.ProductName).FirstOrDefault();
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public int CloneTemplateByTemplateID(int TempID)
        {

            try
            {
                int result = db.sp_cloneTemplate(TempID, 0, "");
                return result;
            }
            catch (Exception ex)
            {

                return 0;
            }

        }

        #endregion

        
    }
}
