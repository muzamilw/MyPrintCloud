using FluentScheduler;
using MPC.Interfaces.WebStoreServices;
using MPC.Implementation.MISServices;
using MPC.Webstore.Common;
using System;

public class ListingBackgroundTask : Registry
{
   
    private readonly MPC.Interfaces.MISServices.IListingService _listingService;
    

 //   public ICampaignService campaignService;

    public ListingBackgroundTask(MPC.Interfaces.MISServices.IListingService _listingService,long OrganisationId)
    {
        
        if (OrganisationId > 0)
        {
            Schedule(() => _listingService.SaveListingData(OrganisationId))
               .ToRunNow().AndEvery(30).Seconds();
        }
    }

}