using FluentScheduler;
using MPC.Interfaces.WebStoreServices;
using MPC.Implementation.MISServices;
using MPC.Webstore.Common;
using System;

public class ListingBackgroundTask : Registry
{

    private readonly IListingService _listingService;
 //   public ICampaignService campaignService;

    public ListingBackgroundTask(MPC.Interfaces.MISServices.IListingService listingService)
    {
        
            Schedule(() => listingService.SaveListingData())
               .ToRunNow().AndEvery(2).Minutes();
            
    }
    

    
}