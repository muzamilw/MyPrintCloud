using FluentScheduler;
using MPC.Interfaces.WebStoreServices;
using MPC.Implementation.MISServices;
using MPC.Webstore.Common;
using System;
using System.Net.Http;

public class ListingBackgroundTask : Registry
{
    public string Url { get; set; }
    public ListingBackgroundTask(string sUrl)
    {
        this.Url = sUrl;
        Schedule(() => SubmitListingData())
               .ToRunNow().AndEvery(2).Minutes();
            
    }
    
    public string SubmitListingData()
    {
        using (var client = new HttpClient())
        {
            string ourl = Url;
            client.BaseAddress = new Uri(ourl);
            client.DefaultRequestHeaders.Accept.Clear();
            string url = "api/ListingProperty?OrganisationId=1682";
            var response = client.GetAsync(url);
            if (response.Result.IsSuccessStatusCode)
            {
                string responsestr = response.Result.Content.ReadAsStringAsync().Result;

            }

        }

        return string.Empty;
    }
    
}