using System;
using System.Net;
using System.Web;
using System.Web.Http;
using MPC.Interfaces.MISServices;
using MPC.MIS.Areas.Api.ModelMappers;

namespace MPC.MIS.Areas.Api.Controllers
{
    /// <summary>
    /// Item Designer Template Base API Controller
    /// </summary>
    public class ItemDesignerTemplateBaseController : ApiController
    {
        #region Private

        private readonly IItemService itemService;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ItemDesignerTemplateBaseController(IItemService itemService)
        {
            if (itemService == null)
            {
                throw new ArgumentNullException("itemService");
            }

            this.itemService = itemService;
        }

        #endregion

        #region Public
        /// <summary>
        /// Get Item Designer Template Base Data
        /// </summary>
        public Models.ItemDesignerTemplateBaseResponse Get(int? id)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, "Invalid Request");
            }

            return itemService.GetBaseDataForDesignerTemplate(id).CreateFrom();
        }

        #endregion
    }
}