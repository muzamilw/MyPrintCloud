using MPC.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MPC.Web.Areas.Api.Controllers
{
    public class GetSettingsBaseData : ApiController
    {
        private IMyOrganizationService myorganizationService;
        public GetSettingsBaseData(IMyOrganizationService myorganizationService)
        {

            this.myorganizationService = myorganizationService;
        }

        [HttpGet]
        public List<int> Get(int request)
        {
            return myorganizationService.GetOrganizationIds(request).ToList();
            
        }

    }
}