using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPC.Interfaces.MISServices;
using MPC.Models.ResponseModels;

namespace MPC.MIS.Areas.Stores.Controllers
{
    public class ExportTemplateController : Controller
    {
        // GET: Stores/ExportTemplate
        private readonly ICompanyService companyService;
        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public ExportTemplateController(ICompanyService companyService)
        {
            if (companyService == null)
            {
                throw new ArgumentNullException("companyService");
            }

            this.companyService = companyService;


        }
        #endregion
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProductTemplatesIndex(long storeId, long categoryId, long parentCategoryId)
        {
            ProductTemplateListResponseModel responseModel = companyService.GetFilteredProductTemplates(storeId,
                categoryId, parentCategoryId);
            return View(responseModel);
        }
    }
}