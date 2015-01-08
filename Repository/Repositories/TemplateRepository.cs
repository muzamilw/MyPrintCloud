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

        public List<MatchingSets> BindTemplatesList(string TemplateName, int pageNumber)
        {
            try
            {
               

                //The Remote mapped categoryName
                int totalPagesCount = 0;
                int SearchCount = 0;


                List<MatchingSets> templatesList = null;// GetTemplateDataFromService(TemplateName, pageNumber - 1, out totalPagesCount, out SearchCount);


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

        //private List<MatchingSets> GetTemplateDataFromService(string templateName, int pageNumber, out int totalPagesCount, out int SearchCount,int CustomerID)
        //{
        //    using (GlobalTemplateDesigner.TemplateSvcSPClient tsc = new GlobalTemplateDesigner.TemplateSvcSPClient())
        //    {
               
        //            string[] categoryNames = (from c in GetMappedCategoryNames(false).ToList()
        //                                      select c.TemplateDesignerMappedCategoryName).ToArray();
        //            string NewTemplateName = templateName.Replace("Copy", "");

        //            List<MatchingSets> tempList = tsc.GetTemplatesbyTemplateName(out totalPagesCount, out SearchCount, NewTemplateName + ":" + CustomerID.ToString(), categoryNames, pageNumber,0).ToList();

        //            //tempList = tempList.Where(c => c.Status == 3).ToList();

        //            return tempList;
               
        //    }
        
        //}
      
        //public List<vw_ProductCategories> GetMappedCategoryNames(bool isClearCache)
        //{
        //    List<vw_ProductCategories> mappedCategories = null;
        //    //if (HttpContext.Current.Cache["MappedCategoryNames"] == null || isClearCache == true)
        //    //{
        //        mappedCategories = GetProductDesignerMappedCategoryNames();
        //    //    HttpContext.Current.Cache["MappedCategoryNames"] = mappedCategories;
        //    //}
        //    //else
        //    //{
        //    //    mappedCategories = HttpContext.Current.Cache["MappedCategoryNames"] as List<vw_ProductCategories>;
        //    //}

        //    return mappedCategories;
        //}

        //public List<vw_ProductCategories> GetProductDesignerMappedCategoryNames()
        //{
        //    try
        //    {
               
        //            return (from c in db.vw_ProductCategories
        //                    where !string.IsNullOrEmpty(c.TemplateDesignerMappedCategoryName)
        //                    select c).ToList();
               
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public ProductCategory GetDisplayOrderAndSave(int pCID)
        {
           
            return (from c in db.ProductCategories
                    where c.ProductCategoryId == pCID
                    select c).FirstOrDefault();
        }
        #endregion

        
    }
}
