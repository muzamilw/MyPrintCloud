using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.RequestModels;
using MPC.WebBase.Mvc;
using StockCategory = MPC.Models.DomainModels.StockCategory;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Stock Category Controller
    /// </summary>
    public class StockCategoryController : ApiController
    {
        #region Private

        private readonly IStockCategoryService stockCategoryService;

        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public StockCategoryController(IStockCategoryService stockCategoryService)
        {
            this.stockCategoryService = stockCategoryService;
        }

        #endregion
        #region Public
        /// <summary>
        /// Get Stock Categories
        /// </summary>
        public StockCategorySubCategoryResponse Get()
        {
            StockCategorySubCategoryResponse response = new StockCategorySubCategoryResponse();
            IEnumerable<StockCategory> list= stockCategoryService.GetAllStockCategories();
            if (list != null)
            {
               response.StockCategories = list.Select(category => category.CreateFromDropDown());
               response.StockSubCategories = list.SelectMany(c => c.StockSubCategories).Select(c => c.CreateFromDropDownSubCat());
               
            }
            return response;
        }


      
        #endregion
	}
}