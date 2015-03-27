using MPC.Interfaces.MISServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MPC.MIS.Areas.Api.Controllers
{
    public class DeleteRoutineController : ApiController
    {
         #region Private

        private readonly ICompanyService companyService;
        private readonly IItemService itemService;
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="companyService"></param>
        public DeleteRoutineController(ICompanyService companyService, IItemService itemService)
        {
            this.companyService = companyService;
            this.itemService = itemService;
        }

        #endregion

        public bool DeleteItem([FromUri] long Id1, long Id2)
        {
            try
            {
                return itemService.DeleteItem(Id1, Id2);
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }

    }
}
