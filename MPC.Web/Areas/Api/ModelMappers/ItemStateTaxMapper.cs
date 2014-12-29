using MPC.MIS.Areas.Api.Models;
namespace MPC.MIS.Areas.Api.ModelMappers
{
    using DomainModels = MPC.Models.DomainModels;

    /// <summary>
    /// Item State Tax WebApi Mapper
    /// </summary>
    public static class ItemStateTaxMapper
    {
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static ItemStateTax CreateFrom(this DomainModels.ItemStateTax source)
        {
            return new ItemStateTax
            {
                ItemStateTaxId = source.ItemStateTaxId,
                ItemId = source.ItemId,
                CountryId = source.CountryId,
                StateId = source.StateId,
                TaxRate = source.TaxRate,
                CountryName = source.Country != null ? source.Country.CountryName : string.Empty,
                StateName = source.State != null ? source.State.StateName : string.Empty
            };
        }

        /// <summary>
        /// Crete From WebApi Model
        /// </summary>
        public static DomainModels.ItemStateTax CreateFrom(this ItemStateTax source)
        {
            return new DomainModels.ItemStateTax
            {
                ItemStateTaxId = source.ItemStateTaxId,
                ItemId = source.ItemId,
                CountryId = source.CountryId,
                StateId = source.StateId,
                TaxRate = source.TaxRate
            };
        }

    }
}