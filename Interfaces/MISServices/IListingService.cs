using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.MISServices
{
    public interface IListingService
    {
         RealEstateListViewResponse GetRealEstatePropertyCompaigns(RealEstateRequestModel request);

         string SaveListingData(long OrganisationId);
    }
}
