using MPC.Interfaces.MISServices;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPC.MIS.Areas.Api.ModelMappers;
using System.Web.Http;
using MPC.MIS.Areas.Api.Models;


namespace MPC.MIS.Areas.Api.Controllers
{
    public class RealEstateCompaignController : ApiController
    {
        private readonly IListingService objlistingservice;

        public RealEstateCompaignController(IListingService objlistingservice)
        {
            this.objlistingservice = objlistingservice;
        }

        public IEnumerable<vw_RealEstateProperties> Get()
        {
            return objlistingservice.GetRealEstatePropertyCompaigns().Select(r => r.CreateFrom());
        }
    }
}