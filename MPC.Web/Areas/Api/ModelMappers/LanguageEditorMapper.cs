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
                Key = source.Key,
                Value = source.Value,
            };
        }

        /// <summary>
        /// Crete From Api Model
        /// </summary>
        public static DomainModels.LanguageEditor CreateFrom(this LanguageEditor source)
        {
            return new DomainModels.LanguageEditor
            {
                Key = source.Key,
                Value = source.Value,
            };
        }

        #endregion
    }
}