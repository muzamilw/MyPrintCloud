using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;


namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Global Language Mapper
    /// </summary>
    public static class GlobalLanguageMapper
    {
        #region Public
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static GlobalLanguageDropDown CreateFromDropDown(this DomainModels.GlobalLanguage source)
        {
            return new GlobalLanguageDropDown() 
            {
                LanguageId = source.LanguageId,
                FriendlyName = source.FriendlyName,
            };
        }

        #endregion
    }
}