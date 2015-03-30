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
        private readonly IMyOrganizationService OrganisationService;
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="companyService"></param>
        public DeleteRoutineController(ICompanyService companyService, IItemService itemService, IMyOrganizationService OrganisationService)
        {
            this.companyService = companyService;
            this.itemService = itemService;
            this.OrganisationService = OrganisationService;
        }

        #endregion

        [HttpGet]
        public bool DeleteOrganisation(long Id)
        {
            try
            {
                return OrganisationService.DeleteOrganisation(Id);
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }

    }
}
