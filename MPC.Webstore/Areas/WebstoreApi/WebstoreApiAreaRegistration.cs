using System.Web.Mvc;
using System.Web.Http;

namespace MPC.Webstore.Areas.WebstoreApi
{
    public class WebstoreApiAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "WebstoreApi";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
          
            context.Routes.MapHttpRoute(
              name: "WebstoreApi_default",
              routeTemplate: AreaName + "/{controller}/{action}/{id}",
              defaults: new { id = UrlParameter.Optional }
           );

            context.Routes.MapHttpRoute(
             name: "WebstoreApiDefault_MultiParams",
             routeTemplate: AreaName + "/{controller}/{action}/{parameter1}/{parameter2}/{parameter3}/{parameter4}/{parameter5}/{parameter6}/{parameter7}/{parameter8}/{parameter9}",
             defaults: new { parameter1 = RouteParameter.Optional, parameter2 = RouteParameter.Optional, parameter3 = RouteParameter.Optional, parameter4 = RouteParameter.Optional, parameter5 = RouteParameter.Optional, parameter6 = RouteParameter.Optional, parameter7 = RouteParameter.Optional, parameter8 = RouteParameter.Optional, parameter9 = RouteParameter.Optional }
          );

            context.Routes.MapHttpRoute(
            name: "CostCenter",
            routeTemplate: AreaName + "/CostCenter/ExecuteCostCentre/{CostCentreId}/{ClonedItemId}/{OrderedQuantity}/{CallMode}/{Queues}",
            defaults: new { CostCentreId = RouteParameter.Optional, ClonedItemId = RouteParameter.Optional, OrderedQuantity = RouteParameter.Optional, CallMode = RouteParameter.Optional, Queues = RouteParameter.Optional }
         );

            context.Routes.MapHttpRoute(
                 name: "FileAttachments",
                 routeTemplate: AreaName + "/{controller}/{FirstName}/{LastName}/{Email}/{Mobile}/{Title}/{InquiryItemTitle1}/{InquiryItemNotes1}/{InquiryItemDeliveryDate1}/{InquiryItemTitle2}/{InquiryItemNotes2}/{InquiryItemDeliveryDate2}/{InquiryItemTitle3}/{InquiryItemNotes3}/{InquiryItemDeliveryDate3}/{hfNoOfRec}",
                 defaults: new { FirstName = RouteParameter.Optional, LastName = RouteParameter.Optional, Email = RouteParameter.Optional, Mobile = RouteParameter.Optional, Title = RouteParameter.Optional, InquiryItemTitle1 = RouteParameter.Optional, InquiryItemNotes1 = RouteParameter.Optional, InquiryItemDeliveryDate1 = RouteParameter.Optional, InquiryItemTitle2 = RouteParameter.Optional, InquiryItemNotes2 = RouteParameter.Optional, InquiryItemDeliveryDate2 = RouteParameter.Optional, InquiryItemTitle3 = RouteParameter.Optional, InquiryItemNotes3 = RouteParameter.Optional, InquiryItemDeliveryDate3 = RouteParameter.Optional, hfNoOfRec = RouteParameter.Optional}
              );

        }
    }
}