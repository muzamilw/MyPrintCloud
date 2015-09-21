using MPC.Interfaces.MISServices;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MPC.MIS.Areas.Api.ModelMappers;
using System.Web.Http;
using MPC.MIS.Areas.Api.Models;
using MPC.Models.RequestModels;


namespace MPC.MIS.Areas.Api.Controllers
{
    public class RealEstateCompaignController : ApiController
    {
        private readonly IListingService objlistingservice;

        public RealEstateCompaignController(IListingService objlistingservice)
        {
            this.objlistingservice = objlistingservice;
        }

        public Models.RealEstateListViewResponse Get([FromUri]RealEstateRequestModel request)
        {
            return objlistingservice.GetRealEstatePropertyCompaigns(request).CreateFromListView();
        }
    }
}