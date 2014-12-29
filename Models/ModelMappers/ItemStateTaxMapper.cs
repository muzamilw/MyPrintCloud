using MPC.Models.DomainModels;
namespace MPC.Models.ModelMappers
{
    using System;

    /// <summary>
    /// Item State Tax mapper
    /// </summary>
    public static class ItemStateTaxMapper
    {
        #region Public

        /// <summary>
        ///  Copy from source entity to the target
        /// </summary>
        public static void UpdateTo(this ItemStateTax source, ItemStateTax target)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            target.ItemId = source.ItemId;
            target.CountryId = source.CountryId;
            target.StateId = source.StateId;
            target.TaxRate = source.TaxRate;
        }

        #endregion
    }
}
