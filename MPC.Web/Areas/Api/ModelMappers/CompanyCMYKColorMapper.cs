using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    // ReSharper disable once InconsistentNaming
    public static class CompanyCMYKColorMapper
    {
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static CompanyCMYKColor CreateFrom(this MPC.Models.DomainModels.CompanyCMYKColor source)
        {
            return new CompanyCMYKColor
            {
                ColorId = source.ColorId,
                CompanyId = source.CompanyId,
                ColorName = source.ColorName,
                ColorC = source.ColorC,
                ColorM = source.ColorM,
                ColorY = source.ColorY,
                ColorK = source.ColorK,
                TerritoryId = source.TerritoryId
            };
        }
        /// <summary>
        /// Crete From Web Model
        /// </summary>
        public static MPC.Models.DomainModels.CompanyCMYKColor CreateFrom(this CompanyCMYKColor source)
        {

            // ReSharper disable InconsistentNaming
            var companyCMYKColor = new MPC.Models.DomainModels.CompanyCMYKColor
            // ReSharper restore InconsistentNaming
            {
                ColorId = source.ColorId,
                CompanyId = source.CompanyId,
                ColorName = source.ColorName,
                ColorC = source.ColorC,
                ColorM = source.ColorM,
                ColorY = source.ColorY,
                ColorK = source.ColorK,
                TerritoryId = source.TerritoryId
            };

            return companyCMYKColor;
        }
    }
}