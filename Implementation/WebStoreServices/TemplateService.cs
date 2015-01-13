using MPC.Common;
using MPC.ExceptionHandling;
using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Implementation.WebStoreServices
{
    class TemplateService : ITemplateService
    {
        #region private
        public readonly ITemplateRepository _templateRepository;
        public readonly IProductCategoryRepository _ProductCategoryRepository;
        #endregion
        #region constructor
        public TemplateService(ITemplateRepository templateRepository, IProductCategoryRepository ProductCategoryRepository)
        {
            this._templateRepository = templateRepository;
            this._ProductCategoryRepository = ProductCategoryRepository;
        }
        #endregion

        #region public
        // called from webstore usually for coping template
        public Template GetTemplate(long productID)
        {
            var product= _templateRepository.GetTemplate(productID);
            if (product.Orientation == 2) //rotating the canvas in case of vert orientation
            {
                double tmp = product.PDFTemplateHeight.Value;
                product.PDFTemplateHeight = product.PDFTemplateWidth;
                product.PDFTemplateWidth = tmp;
            }
            return product;
        }

        // called from designer, all the units are converted to pixel before sending 
        public Template GetTemplateInDesigner(long productID)
        {
            var product = _templateRepository.GetTemplate(productID);

            product.PDFTemplateHeight = DesignerUtils.PointToPixel(product.PDFTemplateHeight.Value);
            product.PDFTemplateWidth = DesignerUtils.PointToPixel(product.PDFTemplateWidth.Value);
            product.CuttingMargin = DesignerUtils.PointToPixel(product.CuttingMargin.Value);


            return product;
        }
        // delete template and all references 
        public bool DeleteTemplate(long ProductID, out long CategoryID, long organizationID)
        {
            var result = false;
            try
            {
                result=  _templateRepository.DeleteTemplate(ProductID, out CategoryID);
                var drURL = System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organization" + organizationID.ToString() + "/Templates/" + ProductID.ToString());
                if (Directory.Exists(drURL))
                {
                      foreach (string item in System.IO.Directory.GetFiles(drURL))
                        {
                            System.IO.File.Delete(item);
                        }

                    Directory.Delete(drURL);
                }
            }
            catch(Exception ex)
            {
                throw new MPCException(ex.ToString(), organizationID);
            }
            return result;
        }
        public bool DeleteTemplateFiles(long ProductID, long organizationID)
        {
            try
            {

                bool result = false;

                var drURL = System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organization" + organizationID.ToString() + "/Templates/" + ProductID.ToString());
                if (Directory.Exists(drURL))
                {
                    foreach (string item in System.IO.Directory.GetFiles(drURL))
                    {
                        System.IO.File.Delete(item);
                    }

                    Directory.Delete(drURL);
                }

                result = true;

                return result;

            }
            catch (Exception ex)
            {
                throw new MPCException(ex.ToString(), organizationID);
            }
        }
        public List<MatchingSets> BindTemplatesList(string TemplateName, int pageNumber, long CustomerID, int CompanyID)
        {
            List<ProductCategoriesView> PCview = _ProductCategoryRepository.GetMappedCategoryNames(false, CompanyID);
            return _templateRepository.BindTemplatesList(TemplateName, pageNumber, CustomerID, CompanyID, PCview);
        }
        
        public string GetTemplateNameByTemplateID(long tempID)
        {
            return _templateRepository.GetTemplateNameByTemplateID(tempID);
        }

      
        public int CloneTemplateByTemplateID(int TempID)
        {
            return _templateRepository.CloneTemplateByTemplateID(TempID);
        }

        /// <summary>
        /// To populate the template information base on template id and item rec by zohaib 10/1/2015
        /// </summary>
        /// <param name="templateID"></param>
        /// <param name="ItemRecc"></param>
        /// <param name="template"></param>
        /// <param name="tempPages"></param>
        public void populateTemplateInfo(long templateID, Item ItemRecc, out Template template, out List<TemplatePage> tempPages)
        {
            try
            {
                _templateRepository.populateTemplateInfo(templateID, ItemRecc, out template, out tempPages);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
