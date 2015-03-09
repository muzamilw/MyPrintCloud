﻿
using System.Data.Entity;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using MPC.Repository.BaseRepository;
using System.Collections.Generic;
using System.Linq;
using MPC.Models.Common;
using System;
using System.Linq;

namespace MPC.Repository.Repositories
{
    /// <summary>
    /// CMS Page Repository
    /// </summary>
    public class CmsPageRepository : BaseRepository<CmsPage>, ICmsPageRepository
    {
        public CmsPageRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<CmsPage> DbSet
        {
            get
            {
                return db.CmsPages;
            }
        }

        public List<CmsPage> GetSecondaryPages(long CompanyId)
        {
            try
            {
                return db.CmsPages.Where(p => (p.CompanyId == CompanyId || p.CompanyId == null) && p.isEnabled == true).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        /// <summary>
        /// Get Cms pages for orders
        /// </summary>
        public IEnumerable<CmsPage> GetCmsPagesForOrders(long companyId)
        {
            try
            {
                return DbSet.Where(
              cmspage =>
              cmspage.isUserDefined == false && cmspage.OrganisationId == OrganisationId &&
              cmspage.CompanyId == companyId).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// Get CMS Pages
        /// </summary>
        public SecondaryPageResponse GetCMSPages(SecondaryPageRequestModel request)
        {
            try
            {
                int fromRow = (request.PageNo - 1) * request.PageSize;
                int toRow = request.PageSize;
                IEnumerable<CmsPage> CmsPages =
                DbSet.Where(x => x.CompanyId == request.CompanyId).OrderBy(x => x.PageId)
                        .Skip(fromRow)
                    .Take(toRow)
                    .ToList();

                return new SecondaryPageResponse
                {
                    RowCount = CmsPages.Count(),
                    CmsPages = CmsPages
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// Get System pages and User defined secondary pages by company id
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>

        public List<CmsPageModel> GetSystemPagesAndSecondaryPages(long CompanyId)
        {
            try
            {
                var query = from page in db.CmsPages
                            where (page.CompanyId == CompanyId) && page.isEnabled == true
                            select new CmsPageModel
                            {
                                PageId = page.PageId,
                                PageName = page.PageName,
                                isEnabled = page.isEnabled,
                                CategoryId = page.CategoryId,
                                isUserDefined = page.isUserDefined,
                                CompanyId = page.CompanyId,
                                Meta_AuthorContent = page.Meta_AuthorContent,
                                Meta_CategoryContent = page.Meta_CategoryContent,
                                Meta_DateContent = page.Meta_DateContent,
                                Meta_DescriptionContent = page.Meta_DescriptionContent,
                                Meta_HiddenDescriptionContent = page.Meta_HiddenDescriptionContent,
                                Meta_KeywordContent = page.Meta_KeywordContent,
                                Meta_LanguageContent = page.Meta_LanguageContent,
                                Meta_RevisitAfterContent = page.Meta_RevisitAfterContent,
                                Meta_RobotsContent = page.Meta_RobotsContent,
                                Meta_Title = page.Meta_Title,
                                PageTitle = page.PageTitle

                            };
                return query.ToList<CmsPageModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public CmsPage getPageByID(long PageID)
        {
            try
            {
                return db.CmsPages.Where(p => p.PageId == PageID).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        /// <summary>
        /// Get Cms Pages By Company Id
        /// </summary>
        public List<CmsPage> GetCmsPagesByCompanyId(long companyId)
        {
            return DbSet.Where(cp => cp.CompanyId == companyId).ToList();
        }
    }
}
