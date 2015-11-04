using FluentScheduler;
using MPC.Interfaces.WebStoreServices;
using System;

public class EmailBackgroundTask : Registry
{
    private readonly ICampaignService _campaignService;
    private readonly MPC.Interfaces.MISServices.IListingService _listingService;
    

 //   public ICampaignService campaignService;

    public EmailBackgroundTask(System.Web.HttpContext context, ICampaignService campaignService, MPC.Interfaces.MISServices.IListingService _listingService)
    {
      
       
        this._campaignService = campaignService;


        Schedule(() => _campaignService.SendEmailFromQueue(context))
       .ToRunNow().AndEvery(1).Minutes();

        Schedule(() => _campaignService.MonitorScheduledEmails())
       .ToRunNow().AndEvery(5).Minutes();

        Schedule(() => _listingService.SaveListingData())
       .ToRunNow().AndEvery(10).Minutes();

        //Schedule(() => emailmgr.GetWeeklyEmailPage(context))
        //    .ToRunEvery(1).Weeks().On(DayOfWeek.Monday).At(21, 00);

        //Schedule(() => emailmgr.SearchDelayedEmails(context))
        //      .ToRunEvery(1).Days().At(21, 00); //at 9:00

        //Schedule(() => emailmgr.SearchSavedDesign(context))
        //   .ToRunEvery(1).Months().OnTheFirst(DayOfWeek.Monday).At(21, 00); //at 9:00

        //Schedule(() => emailmgr.ScheduledMails(context))
        //    .ToRunEvery(10).Minutes();


        
    }

}