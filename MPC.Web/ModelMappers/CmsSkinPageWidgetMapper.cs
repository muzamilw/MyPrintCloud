using System.Linq;
using MPC.MIS.Areas.Api.ModelMappers;
using DomainModels = MPC.Models.DomainModels;
using ApiModels = MPC.MIS.Areas.Api.Models;
using DomainResponseModels = MPC.Models.ResponseModels;

namespace MPC.MIS.ModelMappers
{
    /// <summary>
    /// Cms Skin Page Widge tMapper
    /// </summary>
    public static class CmsSkinPageWidgetMapper
    {
        #region Public
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
                CmsSkinPageWidgetParams = source.CmsSkinPageWidgetParams != null ? source.CmsSkinPageWidgetParams.Select(w => w.CreateFrom()).ToList() : null
            };
        }

        /// <summary>
        /// Crete From Web Model
        /// </summary>
        public static DomainResponseModels.CmsPageWithWidgetList CreateFrom(this ApiModels.CmsPageWithWidgetList source)
        {
            return new DomainResponseModels.CmsPageWithWidgetList()
            {
                PageId = source.PageId,
                CmsSkinPageWidgets =
                    source.CmsSkinPageWidgets != null ? source.CmsSkinPageWidgets.Select(w => w.CreateFrom()).ToList() : null
            };
        }

        #endregion
    }
}