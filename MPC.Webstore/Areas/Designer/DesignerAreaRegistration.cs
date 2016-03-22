using System.Web.Mvc;
using System.Web.Http;

namespace MPC.Webstore.Areas.Designer
{
    public class DesignerAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Designer";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            //string designName, int categoryIDV2, int templateID, int itemID, int customerID, int contactID, bool printCropMarks, bool printWaterMarks,int isCalledFrom ,bool isEmbedded,int territoryId)
            context.MapRoute(
                  "DesignerDefault_MultiParams",
                  AreaName + "/{designName}/{categoryIDV2}/{templateID}/{itemID}/{customerID}/{contactID}/{isCalledFrom}/{organisationId}/{printCropMarks}/{printWaterMarks}/{isEmbedded}/{territoryId}/{colorTerritoryId}",
                  new { controller = "Designer", action = "Index", designName = RouteParameter.Optional, categoryIDV2 = RouteParameter.Optional, templateID = RouteParameter.Optional, itemID = RouteParameter.Optional, customerID = RouteParameter.Optional, contactID = RouteParameter.Optional, isCalledFrom = RouteParameter.Optional, organisationId = RouteParameter.Optional, printCropMarks = RouteParameter.Optional, printWaterMarks = RouteParameter.Optional, isEmbedded = RouteParameter.Optional, territoryId = RouteParameter.Optional, colorTerritoryId = RouteParameter.Optional }
            );
        }
    }
}