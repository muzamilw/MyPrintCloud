using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.ResponseModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Language Editor Mapper
    /// </summary>
    public static class LanguageEditorMapper
    {
        #region Public

        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static LanguageEditor CreateFrom(this DomainModels.LanguageEditor source)
        {
            return new LanguageEditor
            {
                DefaultAddress = source.DefaultAddress,
                DefaultShippingAddress = source.DefaultShippingAddress,
                ConfirmDesign = source.ConfirmDesign,
                Details = source.Details,
                NewsLetter = source.NewsLetter,
                PONumber = source.PONumber,
                Prices = source.Prices,
                UserShippingAddress = source.UserShippingAddress,
            };
        }

        /// <summary>
        /// Crete From Api Model
        /// </summary>
        public static DomainModels.LanguageEditor CreateFrom(this LanguageEditor source)
        {
            return new DomainModels.LanguageEditor
            {
                DefaultAddress = source.DefaultAddress,
                DefaultShippingAddress = source.DefaultShippingAddress,
                ConfirmDesign = source.ConfirmDesign,
                Details = source.Details,
                NewsLetter = source.NewsLetter,
                PONumber = source.PONumber,
                Prices = source.Prices,
                UserShippingAddress = source.UserShippingAddress,
            };
        }

        #endregion
    }
}