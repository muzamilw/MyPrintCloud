using MPC.Interfaces.Data;
using MPC.Interfaces.MISServices;
using MPC.WebBase.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class ExportItemController : ApiController
    {


        
          #region Private
        private readonly IItemService _itemService;
     
        #endregion
        #region Constructor
        public ExportItemController(IItemService itemService)
        {
            this._itemService = itemService;
           
        }
        #endregion


        [CompressFilterAttribute]
        public string Get(long CompanyId)
        {
            string path = _itemService.ExportItems(CompanyId);
            return path;
        }


     
    }
}