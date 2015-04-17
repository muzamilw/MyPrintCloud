
using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Template Color Style Mapper
    /// </summary>
    public static class TemplateColorStyleMapper
    {
        public static TemplateColorStyle CreateFrom(this MPC.Models.DomainModels.TemplateColorStyle source)
        {
            return new TemplateColorStyle
            {
            ColorC =source.ColorC,
            //obj.ColorM = M;
            //obj.ColorY = Y;
            //obj.ColorK = K;
            //obj.IsSpotColor = true;
            //obj.SpotColor = Name;
            //obj.IsColorActive = true;
            //obj.CustomerId = CustomerID;
            };
        }
    }
}