using MPC.MIS.Areas.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class ReportNoteMapper
    {
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static ReportNote CreateFrom(this MPC.Models.DomainModels.ReportNote source)
        {
            return new ReportNote
            {
                Id = source.Id,
                HeadNotes = source.HeadNotes,
                FootNotes = source.FootNotes,
                AdvertitorialNotes = source.AdvertitorialNotes,
                BannerAbsolutePath = source.BannerAbsolutePath,
                isDefault = source.isDefault,
                CompanyId = source.CompanyId,
                OrganisationId = source.OrganisationId,
                ReportBanner = source.ReportBanner,
                ReportCategoryId = source.ReportCategoryId,
                ReportTitle = source.ReportTitle,
                SystemSiteId = source.SystemSiteId,
                UserId = source.UserId,
                
            };
        }
        /// <summary>
        /// Crete From Web Model
        /// </summary>
        public static MPC.Models.DomainModels.ReportNote CreateFrom(this ReportNote source)
        {
            return new MPC.Models.DomainModels.ReportNote
            {
                Id = source.Id,
                HeadNotes = source.HeadNotes,
                FootNotes = source.FootNotes,
                AdvertitorialNotes = source.AdvertitorialNotes,
                BannerAbsolutePath = source.BannerAbsolutePath,
                isDefault = source.isDefault,
                CompanyId = source.CompanyId,
                OrganisationId = source.OrganisationId,
                ReportBanner = source.ReportBanner,
                ReportCategoryId = source.ReportCategoryId,
                ReportTitle = source.ReportTitle,
                SystemSiteId = source.SystemSiteId,
                UserId = source.UserId,
            };
        }

    }
}