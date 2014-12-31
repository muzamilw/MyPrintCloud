using DomainModels = MPC.Models.DomainModels;
using ApiModels = MPC.Webstore.Models; 

namespace MPC.Webstore.ModelMappers
{
    public static class CmsSkinPageWidgetMapper
    {
        #region Public
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static ApiModels.CmsSkinPageWidget CreateFrom(this DomainModels.CmsSkinPageWidget source)
        {
            return new ApiModels.CmsSkinPageWidget
            {
                PageWidgetId = source.PageWidgetId,
                PageId = source.PageId,
                Sequence = source.Sequence,
                CompanyId = source.CompanyId,
                WidgetId = source.WidgetId,
                Widget = source.Widget,
                CmsSkinPageWidgetParams = source.CmsSkinPageWidgetParams
            };
        }

        /// <summary>
        /// Crete From Web Model
        /// </summary>
        public static DomainModels.CmsSkinPageWidget CreateFrom(this ApiModels.CmsSkinPageWidget source)
        {
            return new DomainModels.CmsSkinPageWidget
            {
                PageWidgetId = source.PageWidgetId,
                PageId = source.PageId,
                Sequence = source.Sequence,
                CompanyId = source.CompanyId,
                WidgetId = source.WidgetId,
                Widget = source.Widget,
                CmsSkinPageWidgetParams = source.CmsSkinPageWidgetParams
            };
        }


        public static DomainResponseModels.CmsPageWithWidgetList CreateFrom(this ApiModels.CmsPageWithWidgetList source)
        {

        }

        #endregion
    }
}