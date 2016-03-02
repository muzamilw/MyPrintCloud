
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
                PelleteId = source.PelleteId,
                ColorC = source.ColorC,
                ColorM = source.ColorM,
                ColorY = source.ColorY,
                ColorK = source.ColorK,
                CustomerId = source.CustomerId,
                IsColorActive = source.IsColorActive,
                Name = source.Name,
                IsSpotColor = source.IsSpotColor,
                SpotColor = source.SpotColor,
                TerritoryId = source.TerritoryId
            };
        }

        /// <summary>
        /// Crete From Web Model
        /// </summary>
        public static MPC.Models.DomainModels.TemplateColorStyle CreateFrom(this TemplateColorStyle source)
        {

            // ReSharper disable InconsistentNaming
            var companyCMYKColor = new MPC.Models.DomainModels.TemplateColorStyle
            // ReSharper restore InconsistentNaming
            {
                PelleteId = source.PelleteId,
                ColorC = source.ColorC,
                ColorM = source.ColorM,
                ColorY = source.ColorY,
                ColorK = source.ColorK,
                CustomerId = source.CustomerId,
                IsColorActive = source.IsColorActive,
                Name = source.Name,
                IsSpotColor = source.IsSpotColor,
                SpotColor = source.SpotColor,
                TerritoryId = source.TerritoryId
            };

            return companyCMYKColor;
        }
    }
}