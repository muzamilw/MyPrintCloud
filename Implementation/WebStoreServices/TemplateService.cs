﻿using MPC.Common;
using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
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

        public List<MatchingSets> BindTemplatesList(string TemplateName, int pageNumber, long CustomerID, int CompanyID)
        {
            List<ProductCategoriesView> PCview = _ProductCategoryRepository.GetMappedCategoryNames(false, CompanyID);
            return _templateRepository.BindTemplatesList(TemplateName, pageNumber, CustomerID, CompanyID, PCview);
        }
        
        public string GetTemplateNameByTemplateID(int tempID)
        {
            return _templateRepository.GetTemplateNameByTemplateID(tempID);
        }

      
        public int CloneTemplateByTemplateID(int TempID)
        {
            return _templateRepository.CloneTemplateByTemplateID(TempID);
        }
        #endregion
    }
}
