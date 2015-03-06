using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Practices.Unity;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Interfaces.Repository;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using MPC.Repository.BaseRepository;
using System.Data.Entity;
using System.Security.Cryptography;
using System.Text;


namespace MPC.Repository.Repositories
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        #region Private
        private readonly Dictionary<CompanyByColumn, Func<Company, object>> companyOrderByClause = new Dictionary<CompanyByColumn, Func<Company, object>>
                    {
                        {CompanyByColumn.Code, d => d.CompanyId},
                        {CompanyByColumn.Name, c => c.Name},
                        {CompanyByColumn.Type, d => d.TypeId},
                        {CompanyByColumn.Status, d => d.Status}
                    };
        #endregion
        public CompanyRepository(IUnityContainer container)
            : base(container)
        {
        }

        protected override IDbSet<Company> DbSet
        {
            get
            {
                return db.Companies;
            }
        }

        /// <summary>
        /// USer count in last few days
        /// </summary>
        public int UserCount(long? storeId, int numberOfDays)
        {
            DateTime currenteDate = DateTime.UtcNow.Date.AddDays(-numberOfDays);
            return DbSet.Count(company => storeId == company.StoreId && company.CreationDate >= currenteDate);
        }

        public override IEnumerable<Company> GetAll()
        {
            return DbSet.Where(c => c.OrganisationId == OrganisationId).ToList();
        }

        public long GetStoreIdFromDomain(string domain)
        {
            try
            {
                var companyDomain = db.CompanyDomains.Where(d => d.Domain.Contains(domain)).ToList();
                if (companyDomain.FirstOrDefault() != null)
                {
                    return companyDomain.FirstOrDefault().CompanyId;

                }
                else
                {
                    return 0;
                }
            }
             catch(Exception ex)
            {
                throw ex;

            }
        }
        public Company GetCustomer(int CompanyId)
        {
            try
            {
                //Create Customer
                return db.Companies.Include("Address").Include("CompanyContact").Where(customer => customer.CompanyId == CompanyId).FirstOrDefault<Company>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
       
        public CompanyResponse GetCompanyById(long companyId)
        {
            try
            {
                CompanyResponse companyResponse = new CompanyResponse();
                var company = DbSet.Find(companyId);
                company.RaveReviews = company.RaveReviews.OrderBy(rv => rv.SortOrder).ToList();
                companyResponse.SecondaryPageResponse = new SecondaryPageResponse();
                companyResponse.SecondaryPageResponse.RowCount = company.CmsPages.Count;
                companyResponse.SecondaryPageResponse.CmsPages = company.CmsPages.Take(5).ToList();
                companyResponse.Company = company;

                //companyResponse.CompanyTerritoryResponse = new CompanyTerritoryResponse();
                //companyResponse.AddressResponse = new AddressResponse();
                //companyResponse.CompanyContactResponse = new CompanyContactResponse();
                //companyResponse.CompanyTerritoryResponse.RowCount = company.CompanyTerritories.Count();
                //companyResponse.CompanyTerritoryResponse.CompanyTerritories = company.CompanyTerritories.Take(5).ToList();
                //companyResponse.AddressResponse.RowCount = company.Addresses.Count();
                //companyResponse.AddressResponse.Addresses = company.Addresses.Take(5).ToList();
                //companyResponse.CompanyContactResponse.CompanyContacts = company.CompanyContacts.Take(5).ToList();
                //companyResponse.CompanyContactResponse.RowCount = company.CompanyContacts.Count;
                return companyResponse;
            }
            catch (Exception ex)
            {
                throw ex;

            }
           
        }

        /// <summary>
        /// Get Companies list for Companies List View
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CompanyResponse SearchCompanies(CompanyRequestModel request)
        {
            try
            {
                int fromRow = (request.PageNo - 1) * request.PageSize;
                int toRow = request.PageSize;
                bool isStringSpecified = !string.IsNullOrEmpty(request.SearchString);
                bool isTypeSpecified = request.CustomerType != null;
                long type = request.CustomerType ?? 0;
                Expression<Func<Company, bool>> query =
                    s =>
                    ((!isStringSpecified || s.Name.Contains(request.SearchString)) && (isTypeSpecified && s.TypeId == type || !isTypeSpecified)) && 
                    (s.OrganisationId == OrganisationId && s.isArchived != true) && (s.IsCustomer == 3 || s.IsCustomer == 4);

                int rowCount = DbSet.Count(query);
                IEnumerable<Company> companies = request.IsAsc
                    ? DbSet.Where(query)
                        .OrderBy(companyOrderByClause[request.CompanyByColumn])
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList()
                    : DbSet.Where(query)
                        .OrderByDescending(companyOrderByClause[request.CompanyByColumn])
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList();
                return new CompanyResponse
                {
                    RowCount = rowCount,
                    Companies = companies
                };
            }
            catch (Exception ex)
            {
                throw ex;

            }
           

           
        }

        /// <summary>
        /// Get Suppliers For Inventories
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public SupplierSearchResponseForInventory GetSuppliersForInventories(SupplierRequestModelForInventory request)
        {
            try
            {
                int fromRow = (request.PageNo - 1) * request.PageSize;
                int toRow = request.PageSize;
                bool isStringSpecified = !string.IsNullOrEmpty(request.SearchString);
                Expression<Func<Company, bool>> query =
                    s =>
                        (isStringSpecified && (s.Name.Contains(request.SearchString)) ||
                         !isStringSpecified) && s.IsCustomer == 2 && s.OrganisationId == OrganisationId;

                int rowCount = DbSet.Count(query);
                IEnumerable<Company> companies =
                    DbSet.Where(query).OrderByDescending(x => x.Name)
                         .Skip(fromRow)
                        .Take(toRow)
                        .ToList();

                return new SupplierSearchResponseForInventory
                {
                    TotalCount = rowCount,
                    Suppliers = companies
                };
            }
            catch (Exception ex)
            {
                throw ex;

            }
           
        }

        public Company GetStoreById(long companyId)
        {
            try
            {
                db.Configuration.LazyLoadingEnabled = false;
                return db.Companies.FirstOrDefault(c => c.CompanyId == companyId);
            }
            catch (Exception ex)
            {
                throw ex;

            }
          
        }
        public Company GetStoreByStoreId(long companyId)
        {
            try
            {
                db.Configuration.LazyLoadingEnabled = true;
                db.Configuration.ProxyCreationEnabled = true;
                return db.Companies.Where(c => c.CompanyId == companyId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;

            }
            //db.Configuration.LazyLoadingEnabled = false;
            //db.Configuration.ProxyCreationEnabled = false;
           // return db.Companies.Include("CompanyDomains").Include("CmsOffers").Include("MediaLibraries").Include("CompanyBannerSets").Include("CompanyBannerSets.CompanyBanners").Include("CmsPages").Include("RaveReviews").Include("CompanyTerritories").Include("Addresses").Include("CompanyContacts").Include("ProductCategories").Include("Items").Include("Items.ItemSections").Include("Items.ItemSections.SectionCostcentres").Include("Items.ItemSections.SectionCostcentres.SectionCostCentreResources").Include("PaymentGateways").Include("CmsSkinPageWidgets").Include("CompanyCostCentres").Include("CompanyCMYKColors").FirstOrDefault(c => c.CompanyId == companyId);
         
        }
       
        
        public ExportOrganisation ExportCompany( long CompanyId)
        {
          
            ExportOrganisation ObjExportOrg = new ExportOrganisation();
            try
            {
                List<ItemSection> ItemSections = new List<ItemSection>();
                List<SectionCostcentre> SectionCostCentre = new List<SectionCostcentre>();
                List<SectionCostCentreResource> SectionCostCentreResources = new List<SectionCostCentreResource>();
                List<ItemAttachment> ItemAttachments = new List<ItemAttachment>();
                List<TemplatePage> TemplatePages = new List<TemplatePage>();
                List<TemplateObject> TemplateObjects = new List<TemplateObject>();
                List<TemplateFont> TemplateFonts = new List<TemplateFont>();
                List<TemplateColorStyle> TemplateColorStyle = new List<TemplateColorStyle>();
                List<TemplateBackgroundImage> TemplateBackGroundImages = new List<TemplateBackgroundImage>();
                List<ImagePermission> ImagePermission = new List<ImagePermission>();

                db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;

                ObjExportOrg.Company = db.Companies.Where(c => c.CompanyId == CompanyId).FirstOrDefault();

                // set Company Domain

                ObjExportOrg.CompanyDomain = db.CompanyDomains.Where(c => c.CompanyId == CompanyId).ToList();

                // set cms offers
                
                ObjExportOrg.CmsOffer = db.CmsOffers.Where(c => c.CompanyId == CompanyId).ToList();
                

                ObjExportOrg.MediaLibrary = db.MediaLibraries.Where(c => c.CompanyId == CompanyId).ToList();


                List<CompanyBannerSet> bannerSets = new List<CompanyBannerSet>();
                bannerSets = db.CompanyBannerSets.Include("CompanyBanners").Where(c => c.CompanyId == CompanyId).ToList();

                List<CompanyBanner> Lstbanner = new List<CompanyBanner>();
                // company banners
                if (bannerSets != null)
                {
                    List<CompanyBannerSet> CompanyBannerSet = bannerSets;
                    ObjExportOrg.CompanyBannerSet = CompanyBannerSet;
                    if (CompanyBannerSet != null && CompanyBannerSet.Count > 0)
                    {
                        foreach (var banner in CompanyBannerSet)
                        {
                            if (banner.CompanyBanners != null)
                            {
                                if (banner.CompanyBanners.Count > 0)
                                {
                                    foreach (var bann in banner.CompanyBanners)
                                    {
                                        Lstbanner.Add(bann);
                                    }
                                }
                            }
                        }

                    }
                    ObjExportOrg.CompanyBanner = Lstbanner.ToList();
                }

                // Secondary Pages

                ObjExportOrg.SecondaryPages = db.CmsPages.Where(c => c.CompanyId == CompanyId).ToList();

                

                // Rave Reviews

                ObjExportOrg.RaveReview = db.RaveReviews.Where(r => r.CompanyId == CompanyId).ToList();

                

                // CompanyTerritories


                ObjExportOrg.CompanyTerritory = db.CompanyTerritories.Where(c => c.CompanyId == CompanyId).ToList();
               

                // Addresses


                ObjExportOrg.Address = db.Addesses.Where(a => a.CompanyId == CompanyId).ToList();
                

                // contacts


                ObjExportOrg.CompanyContact = db.CompanyContacts.Where(c => c.CompanyId == CompanyId).ToList();
                

                // product Categories
                
                ObjExportOrg.ProductCategory = db.ProductCategories.Where(s => s.isPublished == true && s.isArchived == false && s.CompanyId == CompanyId).ToList();
                

                // items
               
                 List<Item> items = db.Items.Where(i => i.IsPublished == true && i.IsArchived == false && i.CompanyId == CompanyId).ToList();
                    items = items.ToList();

                    ObjExportOrg.Items = items;

                    if (items != null)
                    {
                        if (items.Count > 0)
                        {
                            foreach (var item in items)
                            {
                                // itemSections
                                if (item.ItemSections != null)
                                {

                                    if (item.ItemSections != null && item.ItemSections.Count > 0)
                                    {
                                        // add item sections
                                        foreach (var sec in item.ItemSections)
                                        {
                                            if (sec.SectionCostcentres != null)
                                            {
                                                if (sec.SectionCostcentres.Count > 0)
                                                {
                                                    // add section Costcentre
                                                    foreach (var ss in sec.SectionCostcentres)
                                                    {
                                                        if (ss.SectionCostCentreResources != null)
                                                        {
                                                            if (ss.SectionCostCentreResources.Count > 0)
                                                            {
                                                                foreach (var res in ss.SectionCostCentreResources)
                                                                {
                                                                    SectionCostCentreResources.Add(res);
                                                                }

                                                            }
                                                        }

                                                        SectionCostCentre.Add(ss);
                                                    }
                                                }
                                            }
                                            ItemSections.Add(sec);
                                        }
                                    }
                                }
                                if (item.ItemAttachments != null)
                                {
                                    if(item.ItemAttachments.Count > 0)
                                    {
                                        foreach(var itemAttach in item.ItemAttachments.Where(c => c.CompanyId == CompanyId))
                                        {
                                            ItemAttachments.Add(itemAttach);
                                        }
                                    }
                                }
                                if(item.Template != null)
                                {

                                    ObjExportOrg.ItemTemplate = item.Template;

                                    long TemplateID = item.Template.ProductId;

                                    if (TemplateID > 0)
                                    {
                                        // template pages
                                        List<TemplatePage> lstTemplatePage = db.TemplatePages.Where(t => t.ProductId == TemplateID).ToList();
                                        if (lstTemplatePage != null && lstTemplatePage.Count > 0)
                                        {
                                            foreach(var tempPage in lstTemplatePage)
                                            {
                                                TemplatePages.Add(tempPage);
                                            }
                                           
                                        }


                                        // template objects
                                        List<TemplateObject> lstTemplateObjects = db.TemplateObjects.Where(c => c.ProductId == TemplateID).ToList();
                                        if (lstTemplateObjects != null && lstTemplateObjects.Count > 0)
                                        {
                                            foreach (var tempObjects in lstTemplateObjects)
                                            {
                                               TemplateObjects.Add(tempObjects);
                                            }

                                        }

                                        // template fonts
                                        List<TemplateFont> lstTemplateFont = db.TemplateFonts.Where(c => c.ProductId == TemplateID).ToList();
                                        if (lstTemplateFont != null && lstTemplateFont.Count > 0)
                                        {
                                            foreach (var tempFonts in lstTemplateFont)
                                            {
                                                TemplateFonts.Add(tempFonts);
                                            }

                                        }
                                       // template background images

                                        List<TemplateBackgroundImage> lstTemplateBackgroundImages = db.TemplateBackgroundImages.Where(c => c.ProductId == TemplateID && c.ContactCompanyId == CompanyId).ToList();
                                        if (lstTemplateBackgroundImages != null && lstTemplateBackgroundImages.Count > 0)
                                        {
                                            foreach (var tempBackImages in lstTemplateBackgroundImages)
                                            {
                                                TemplateBackGroundImages.Add(tempBackImages);
                                                if(tempBackImages.ImagePermissions != null && tempBackImages.ImagePermissions.Count > 0)
                                                {
                                                    foreach (var img in tempBackImages.ImagePermissions)
                                                    {
                                                        ImagePermission.Add(img);
                                                    }
                                                }
                                            }

                                        }

                                       
                                    }

                                }

                            }
                        }
                        ObjExportOrg.ItemSection = ItemSections;
                        ObjExportOrg.SectionCostcentre = SectionCostCentre;
                        ObjExportOrg.SectionCostCentreResource = SectionCostCentreResources;
                        ObjExportOrg.ItemAttachment = ItemAttachments;
                        ObjExportOrg.TemplatePages = TemplatePages;
                        ObjExportOrg.TemplateObjects = TemplateObjects;
                        ObjExportOrg.TemplateFonts = TemplateFonts;
                        ObjExportOrg.TemplateBackgroundImage = TemplateBackGroundImages;
                        ObjExportOrg.ImagePermission = ImagePermission;
                    }


                


                //  campaigns

                ObjExportOrg.Campaigns = db.Campaigns.Where(c => c.CompanyId == CompanyId).ToList();

                // payment gateways

                ObjExportOrg.PaymentGateways = db.PaymentGateways.Where(c => c.CompanyId == CompanyId).ToList();
                



                // cms skin page widgets
               
                ObjExportOrg.CmsSkinPageWidget = db.PageWidgets.Where(c => c.CompanyId == CompanyId).ToList();
                

                // company cost centre
               
                ObjExportOrg.CompanyCostCentre = db.CompanyCostCentres.Where(c => c.CompanyId == CompanyId).ToList();
                

               // company cmyk colors
                ObjExportOrg.CompanyCMYKColor = db.CompanyCmykColors.Where(c => c.CompanyId == CompanyId).ToList();

                // company cost centres
                ObjExportOrg.CompanyCostCentre = db.CompanyCostCentres.Where(c => c.CompanyId == CompanyId).ToList();

                // template color style
                List<TemplateColorStyle> lstTemplateColorStyle = db.TemplateColorStyles.Where(c => c.CustomerId == CompanyId).ToList();
                if (lstTemplateColorStyle != null && lstTemplateColorStyle.Count > 0)
                {
                    foreach (var tempStyle in lstTemplateColorStyle)
                    {
                        TemplateColorStyle.Add(tempStyle);
                    }

                }

                ObjExportOrg.TemplateColorStyle = TemplateColorStyle;

                ItemSections = null;
                SectionCostCentre = null;
                SectionCostCentreResources = null;
                ItemAttachments = null;
                TemplatePages = null;
                TemplateObjects = null;
                TemplateFonts = null;
                TemplateColorStyle = null;
                TemplateBackGroundImages = null;
                ImagePermission = null;

                return ObjExportOrg;

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        // export retail company 
         public ExportOrganisation ExportRetailCompany( long CompanyId)
        {
            try
            {
                ExportOrganisation ObjExportOrg = new ExportOrganisation();
                List<ItemSection> ItemSections = new List<ItemSection>();
                List<SectionCostcentre> SectionCostCentre = new List<SectionCostcentre>();
                List<SectionCostCentreResource> SectionCostCentreResources = new List<SectionCostCentreResource>();
                List<ItemAttachment> ItemAttachments = new List<ItemAttachment>();
                List<TemplatePage> TemplatePages = new List<TemplatePage>();
                List<TemplateObject> TemplateObjects = new List<TemplateObject>();
                List<TemplateFont> TemplateFonts = new List<TemplateFont>();
                List<TemplateColorStyle> TemplateColorStyle = new List<TemplateColorStyle>();
                List<TemplateBackgroundImage> TemplateBackGroundImages = new List<TemplateBackgroundImage>();
                List<ImagePermission> ImagePermission = new List<ImagePermission>();

                db.Configuration.LazyLoadingEnabled = false;
                db.Configuration.ProxyCreationEnabled = false;

                ObjExportOrg.RetailCompany = db.Companies.Where(c => c.CompanyId == CompanyId).FirstOrDefault();

                // set Company Domain

                ObjExportOrg.RetailCompanyDomain = db.CompanyDomains.Where(c => c.CompanyId == CompanyId).ToList();

                // set cms offers

                ObjExportOrg.RetailCmsOffer = db.CmsOffers.Where(c => c.CompanyId == CompanyId).ToList();


                ObjExportOrg.RetailMediaLibrary = db.MediaLibraries.Where(c => c.CompanyId == CompanyId).ToList();


                List<CompanyBannerSet> bannerSets = new List<CompanyBannerSet>();
                bannerSets = db.CompanyBannerSets.Include("CompanyBanners").Where(c => c.CompanyId == CompanyId).ToList();

                List<CompanyBanner> Lstbanner = new List<CompanyBanner>();
                // company banners
                if (bannerSets != null)
                {
                    List<CompanyBannerSet> CompanyBannerSet = bannerSets;
                    ObjExportOrg.RetailCompanyBannerSet = CompanyBannerSet;
                    if (CompanyBannerSet != null && CompanyBannerSet.Count > 0)
                    {
                        foreach (var banner in CompanyBannerSet)
                        {
                            if (banner.CompanyBanners != null)
                            {
                                if (banner.CompanyBanners.Count > 0)
                                {
                                    foreach (var bann in banner.CompanyBanners)
                                    {
                                        Lstbanner.Add(bann);
                                    }
                                }
                            }
                        }

                    }
                    ObjExportOrg.RetailCompanyBanner = Lstbanner.ToList();
                }

                // Secondary Pages

                ObjExportOrg.RetailSecondaryPages = db.CmsPages.Where(c => c.CompanyId == CompanyId).ToList();



                // Rave Reviews

                ObjExportOrg.RetailRaveReview = db.RaveReviews.Where(r => r.CompanyId == CompanyId).ToList();



                // CompanyTerritories


                ObjExportOrg.RetailCompanyTerritory = db.CompanyTerritories.Where(c => c.CompanyId == CompanyId).ToList();


                // Addresses


                ObjExportOrg.RetailAddress = db.Addesses.Where(a => a.CompanyId == CompanyId).ToList();


                // contacts


                ObjExportOrg.RetailCompanyContact = db.CompanyContacts.Where(c => c.CompanyId == CompanyId).ToList();


                // product Categories

                ObjExportOrg.RetailProductCategory = db.ProductCategories.Where(s => s.isPublished == true && s.isArchived == false && s.CompanyId == CompanyId).ToList();


                // items

                List<Item> items = db.Items.Where(i => i.IsPublished == true && i.IsArchived == false && i.CompanyId == CompanyId).ToList();
                items = items.ToList();

                ObjExportOrg.RetailItems = items;

                if (items != null)
                {
                    if (items.Count > 0)
                    {
                        foreach (var item in items)
                        {
                            // itemSections
                            if (item.ItemSections != null)
                            {

                                if (item.ItemSections != null && item.ItemSections.Count > 0)
                                {
                                    // add item sections
                                    foreach (var sec in item.ItemSections)
                                    {
                                        if (sec.SectionCostcentres != null)
                                        {
                                            if (sec.SectionCostcentres.Count > 0)
                                            {
                                                // add section Costcentre
                                                foreach (var ss in sec.SectionCostcentres)
                                                {
                                                    if (ss.SectionCostCentreResources != null)
                                                    {
                                                        if (ss.SectionCostCentreResources.Count > 0)
                                                        {
                                                            foreach (var res in ss.SectionCostCentreResources)
                                                            {
                                                                SectionCostCentreResources.Add(res);
                                                            }

                                                        }
                                                    }

                                                    SectionCostCentre.Add(ss);
                                                }
                                            }
                                        }
                                        ItemSections.Add(sec);
                                    }
                                }
                            }
                            if (item.ItemAttachments != null)
                            {
                                if (item.ItemAttachments.Count > 0)
                                {
                                    foreach (var itemAttach in item.ItemAttachments.Where(c => c.CompanyId == CompanyId))
                                    {
                                        ItemAttachments.Add(itemAttach);
                                    }
                                }
                            }
                            if (item.Template != null)
                            {

                                ObjExportOrg.ItemTemplate = item.Template;

                                long TemplateID = item.Template.ProductId;

                                if (TemplateID > 0)
                                {
                                    // template pages
                                    List<TemplatePage> lstTemplatePage = db.TemplatePages.Where(t => t.ProductId == TemplateID).ToList();
                                    if (lstTemplatePage != null && lstTemplatePage.Count > 0)
                                    {
                                        foreach (var tempPage in lstTemplatePage)
                                        {
                                            TemplatePages.Add(tempPage);
                                        }

                                    }


                                    // template objects
                                    List<TemplateObject> lstTemplateObjects = db.TemplateObjects.Where(c => c.ProductId == TemplateID).ToList();
                                    if (lstTemplateObjects != null && lstTemplateObjects.Count > 0)
                                    {
                                        foreach (var tempObjects in lstTemplateObjects)
                                        {
                                            TemplateObjects.Add(tempObjects);
                                        }

                                    }

                                    // template fonts
                                    List<TemplateFont> lstTemplateFont = db.TemplateFonts.Where(c => c.ProductId == TemplateID).ToList();
                                    if (lstTemplateFont != null && lstTemplateFont.Count > 0)
                                    {
                                        foreach (var tempFonts in lstTemplateFont)
                                        {
                                            TemplateFonts.Add(tempFonts);
                                        }

                                    }
                                    // template background images

                                    List<TemplateBackgroundImage> lstTemplateBackgroundImages = db.TemplateBackgroundImages.Where(c => c.ProductId == TemplateID && c.ContactCompanyId == CompanyId).ToList();
                                    if (lstTemplateBackgroundImages != null && lstTemplateBackgroundImages.Count > 0)
                                    {
                                        foreach (var tempBackImages in lstTemplateBackgroundImages)
                                        {
                                            TemplateBackGroundImages.Add(tempBackImages);
                                            if (tempBackImages.ImagePermissions != null && tempBackImages.ImagePermissions.Count > 0)
                                            {
                                                foreach (var img in tempBackImages.ImagePermissions)
                                                {
                                                    ImagePermission.Add(img);
                                                }
                                            }
                                        }

                                    }


                                }

                            }


                        }
                    }
                    ObjExportOrg.RetailItemSection = ItemSections;
                    ObjExportOrg.RetailSectionCostcentre = SectionCostCentre;
                    ObjExportOrg.RetailSectionCostCentreResource = SectionCostCentreResources;
                    ObjExportOrg.RetailItemAttachment = ItemAttachments;
                    ObjExportOrg.RetailTemplatePages = TemplatePages;
                    ObjExportOrg.RetailTemplateObjects = TemplateObjects;
                    ObjExportOrg.RetailTemplateFonts = TemplateFonts;
                    ObjExportOrg.RetailTemplateBackgroundImage = TemplateBackGroundImages;
                    ObjExportOrg.RetailImagePermission = ImagePermission;
                }

                //  campaigns

                ObjExportOrg.RetailCampaigns = db.Campaigns.Where(c => c.CompanyId == CompanyId).ToList();

                // payment gateways

                ObjExportOrg.RetailPaymentGateways = db.PaymentGateways.Where(c => c.CompanyId == CompanyId).ToList();

                // cms skin page widgets

                ObjExportOrg.RetailCmsSkinPageWidget = db.PageWidgets.Where(c => c.CompanyId == CompanyId).ToList();


                // company cost centre

                ObjExportOrg.RetailCompanyCostCentre = db.CompanyCostCentres.Where(c => c.CompanyId == CompanyId).ToList();


                // company cmyk colors
                ObjExportOrg.RetailCompanyCMYKColor = db.CompanyCmykColors.Where(c => c.CompanyId == CompanyId).ToList();


                // template color style
                List<TemplateColorStyle> lstTemplateColorStyle = db.TemplateColorStyles.Where(c => c.CustomerId == CompanyId).ToList();
                if (lstTemplateColorStyle != null && lstTemplateColorStyle.Count > 0)
                {
                    foreach (var tempStyle in lstTemplateColorStyle)
                    {
                        TemplateColorStyle.Add(tempStyle);
                    }

                }

                ObjExportOrg.TemplateColorStyle = TemplateColorStyle;

                ItemSections = null;
                SectionCostCentre = null;
                SectionCostCentreResources = null;
                ItemAttachments = null;
                TemplatePages = null;
                TemplateObjects = null;
                TemplateFonts = null;
                TemplateColorStyle = null;
                TemplateBackGroundImages = null;
                ImagePermission = null;

                return ObjExportOrg;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

         public long GetCorporateCompanyIDbyOrganisationID(long OID)
         {
             try
             {
                 return db.Companies.Where(o => o.OrganisationId == OID && o.IsCustomer == 3).Select(c => c.CompanyId).FirstOrDefault();
             }
             catch(Exception ex)
             {
                 throw ex;
             }
         }
         public long GetRetailCompanyIDbyOrganisationID(long OID)
         {
             try
             {
                 return db.Companies.Where(o => o.OrganisationId == OID && o.IsCustomer == 4).Select(c => c.CompanyId).FirstOrDefault();
             }
             catch (Exception ex)
             {
                 throw ex;
             }
         }
        /// <summary>
        /// Get Company Price Flag id for Price Matrix in webstore
        /// </summary>
        public int? GetPriceFlagIdByCompany(long CompanyId)
        {
            return DbSet.Where(c => c.CompanyId == CompanyId).Select(f => f.PriceFlagId).FirstOrDefault();
        }

        /// <summary>
        /// Get All Suppliers For Current Organisation
        /// </summary>
        public IEnumerable<Company> GetAllSuppliers()
        {
            try
            {
                return DbSet.Where(supplier => supplier.OrganisationId == OrganisationId && supplier.IsCustomer == 0).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public long CreateCustomer(string CompanyName, bool isEmailSubscriber, bool isNewsLetterSubscriber, CompanyTypes customerType, string RegWithSocialMedia, long OrganisationId, CompanyContact contact = null)
        {
            try
            {
                bool isCreateTemporaryCompany = true;
                if ((int)customerType == (int)CompanyTypes.TemporaryCustomer)
                {
                    Company ContactCompany = db.Companies.Where(c => c.TypeId == (int)customerType && c.OrganisationId == OrganisationId).FirstOrDefault();
                    if (ContactCompany != null)
                    {
                        isCreateTemporaryCompany = false;
                        return ContactCompany.CompanyId;
                    }
                    else
                    {
                        isCreateTemporaryCompany = true;
                    }

                }

                if (isCreateTemporaryCompany)
                {
                    Address Contactaddress = null;

                    CompanyContact ContactPerson = null;

                    long customerID = 0;


                    Company ContactCompany = new Company();

                    ContactCompany.isArchived = false;

                    ContactCompany.AccountNumber = "123";

                    ContactCompany.AccountOpenDate = DateTime.Now;

                    ContactCompany.Name = CompanyName;

                    ContactCompany.TypeId = (int)customerType;

                    ContactCompany.Status = 0;

                    if (contact != null && !string.IsNullOrEmpty(contact.Mobile))
                        ContactCompany.PhoneNo = contact.Mobile;

                    ContactCompany.CreationDate = DateTime.Now;

                    ContactCompany.CreditLimit = 0;

                    ContactCompany.IsCustomer = 0; //prospect

                    ContactCompany.OrganisationId = OrganisationId;

                    Markup OrgMarkup = db.Markups.Where(m => m.OrganisationId == OrganisationId && m.IsDefault == true).FirstOrDefault();

                    if (OrgMarkup != null)
                    {
                        ContactCompany.DefaultMarkUpId = (int)OrgMarkup.MarkUpId;
                    }
                    else
                    {
                        ContactCompany.DefaultMarkUpId = 0;
                    }


                    //Create Customer
                    db.Companies.Add(ContactCompany);

                    //Create Billing Address and Delivery Address and mark them default billing and shipping
                    Contactaddress = PopulateAddressObject(0, ContactCompany.CompanyId, true, true);
                    db.Addesses.Add(Contactaddress);

                    //Create Contact
                    ContactPerson = PopulateContactsObject(ContactCompany.CompanyId, Contactaddress.AddressId, true);
                    ContactPerson.isArchived = false;

                    if (contact != null)
                    {
                        ContactPerson.FirstName = contact.FirstName;
                        ContactPerson.LastName = contact.LastName;
                        ContactPerson.Email = contact.Email;
                        ContactPerson.Mobile = contact.Mobile;
                        ContactPerson.Password = ComputeHashSHA1(contact.Password);
                        ContactPerson.QuestionId = 1;
                        ContactPerson.SecretAnswer = "";
                        ContactPerson.ClaimIdentifer = contact.ClaimIdentifer;
                        ContactPerson.AuthentifiedBy = contact.AuthentifiedBy;
                        //Quick Text Fields
                        ContactPerson.quickAddress1 = contact.quickAddress1;
                        ContactPerson.quickAddress2 = contact.quickAddress2;
                        ContactPerson.quickAddress3 = contact.quickAddress3;
                        ContactPerson.quickCompanyName = contact.quickCompanyName;
                        ContactPerson.quickCompMessage = contact.quickCompMessage;
                        ContactPerson.quickEmail = contact.quickEmail;
                        ContactPerson.quickFax = contact.quickFax;
                        ContactPerson.quickFullName = contact.quickFullName;
                        ContactPerson.quickPhone = contact.quickPhone;
                        ContactPerson.quickTitle = contact.quickTitle;
                        ContactPerson.quickWebsite = contact.quickWebsite;
                        if (!string.IsNullOrEmpty(RegWithSocialMedia))
                        {
                            ContactPerson.twitterScreenName = RegWithSocialMedia;
                        }


                    }

                    db.CompanyContacts.Add(ContactPerson);

                    if (db.SaveChanges() > 0)
                    {
                        customerID = ContactCompany.CompanyId; // customer id
                        if (contact != null)
                        {
                            contact.ContactId = ContactPerson.ContactId;
                            contact.CompanyId = customerID;
                        }
                    }

                    return customerID;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }
        private CompanyContact PopulateContactsObject(long customerID, long addressID, bool isDefaultContact)
        {
            try
            {
                CompanyContact contactObject = new CompanyContact();
                contactObject.CompanyId = customerID;
                contactObject.AddressId = addressID;
                contactObject.FirstName = string.Empty;
                contactObject.IsDefaultContact = isDefaultContact == true ? 1 : 0;

                return contactObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private Address PopulateAddressObject(long addressId, long companyId, bool isDefaulAddress, bool isDefaultShippingAddress)
        {
            try
            {
                Address addressObject = new Address();

                addressObject.AddressId = addressId;
                addressObject.CompanyId = companyId;
                addressObject.AddressName = "Address Name";
                addressObject.IsDefaultAddress = isDefaulAddress;
                addressObject.IsDefaultShippingAddress = isDefaultShippingAddress;
                addressObject.Address1 = "Address 1";
                addressObject.City = "City";
                addressObject.isArchived = false;

                return addressObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }
        private static string ComputeHashSHA1(string plainText)
        {
            try
            {
                string salt = string.Empty;


                salt = ComputeHash(plainText, "SHA1", null);

                return salt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
          
        }
        private static string ComputeHash(string plainText,
                                    string hashAlgorithm,
                                    byte[] saltBytes)
        {
            try
            {
                // If salt is not specified, generate it on the fly.
                if (saltBytes == null)
                {
                    // Define min and max salt sizes.
                    int minSaltSize = 4;
                    int maxSaltSize = 8;

                    // Generate a random number for the size of the salt.
                    Random random = new Random();
                    int saltSize = random.Next(minSaltSize, maxSaltSize);

                    // Allocate a byte array, which will hold the salt.
                    saltBytes = new byte[saltSize];

                    // Initialize a random number generator.
                    RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

                    // Fill the salt with cryptographically strong byte values.
                    rng.GetNonZeroBytes(saltBytes);
                }

                // Convert plain text into a byte array.
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

                // Allocate array, which will hold plain text and salt.
                byte[] plainTextWithSaltBytes =
                        new byte[plainTextBytes.Length + saltBytes.Length];

                // Copy plain text bytes into resulting array.
                for (int i = 0; i < plainTextBytes.Length; i++)
                    plainTextWithSaltBytes[i] = plainTextBytes[i];

                // Append salt bytes to the resulting array.
                for (int i = 0; i < saltBytes.Length; i++)
                    plainTextWithSaltBytes[plainTextBytes.Length + i] = saltBytes[i];

                // Because we support multiple hashing algorithms, we must define
                // hash object as a common (abstract) base class. We will specify the
                // actual hashing algorithm class later during object creation.
                HashAlgorithm hash;

                // Make sure hashing algorithm name is specified.
                //if (hashAlgorithm == null)
                //    hashAlgorithm = "";
                hash = CreateHashAlgoFactory(hashAlgorithm);

                // Compute hash value of our plain text with appended salt.
                byte[] hashBytes = hash.ComputeHash(plainTextWithSaltBytes);

                // Create array which will hold hash and original salt bytes.
                byte[] hashWithSaltBytes = new byte[hashBytes.Length +
                                                    saltBytes.Length];

                // Copy hash bytes into resulting array.
                for (int i = 0; i < hashBytes.Length; i++)
                    hashWithSaltBytes[i] = hashBytes[i];

                // Append salt bytes to the result.
                for (int i = 0; i < saltBytes.Length; i++)
                    hashWithSaltBytes[hashBytes.Length + i] = saltBytes[i];

                // Convert result into a base64-encoded string.
                string hashValue = Convert.ToBase64String(hashWithSaltBytes);

                // Return the result.
                return hashValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
         
        }

        private static HashAlgorithm CreateHashAlgoFactory(string hashAlgorithm)
        {
            try
            {
                HashAlgorithm hash = null; ;
                // Initialize appropriate hashing algorithm class.
                switch (hashAlgorithm)
                {
                    case "SHA1":
                        hash = new SHA1Managed();
                        break;

                    case "SHA256":
                        hash = new SHA256Managed();
                        break;

                    case "SHA384":
                        hash = new SHA384Managed();
                        break;

                    case "SHA512":
                        hash = new SHA512Managed();
                        break;

                    default:
                        hash = new MD5CryptoServiceProvider(); // mdf default
                        break;
                }
                return hash;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        public string SystemWeight(long OrganisationID)
        {
            try
            {

                var qry = from systemWeight in db.WeightUnits
                          join organisation in db.Organisations on systemWeight.Id equals organisation.SystemWeightUnit
                          where organisation.OrganisationId == OrganisationID
                          select systemWeight.UnitName;

                return qry.ToString();

            }
            catch (Exception ex)
            {
                throw ex;

            }

        }
        public string SystemLength(long OrganisationID)
        {
            try
            {

                var qry = from systemLength in db.LengthUnits
                          join organisation in db.Organisations on systemLength.Id equals organisation.SystemLengthUnit
                          where organisation.OrganisationId == OrganisationID
                          select systemLength.UnitName;

                return qry.ToString();

                //  return db.Organisations.Where(o => o.OrganisationId == OrganisationID).Select(s => s.SystemLengthUnit ?? 0).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }
        //Update Just Company Name 
        public bool UpdateCompanyName(Company Instance)
        {
            bool Result = false;
            try
            {
                Company Company = db.Companies.Where(i => i.CompanyId == Instance.CompanyId).FirstOrDefault(); 
                Company.Name = Instance.Name;
                db.Companies.Attach(Company);
                db.Entry(Company).State = EntityState.Modified;
                if (db.SaveChanges() > 0)
                {
                    Result = true;

                }
                else
                {
                    Result = false;
                
                }
            }
            catch (Exception ex)
            {

                throw ex;
            
            }
            return Result;
        }
        /// <summary>
        /// Get Company By Is Customer Type
        /// </summary>
        public CompanySearchResponseForCalendar GetByIsCustomerType(CompanyRequestModelForCalendar request)
        {
            try
            {
                int fromRow = (request.PageNo - 1) * request.PageSize;
                int toRow = request.PageSize;
                bool isStringSpecified = !string.IsNullOrEmpty(request.SearchString);
                Expression<Func<Company, bool>> query =
                    s =>
                        (isStringSpecified && (s.Name.Contains(request.SearchString)) ||
                         !isStringSpecified) && s.IsCustomer == request.IsCustomerType && s.OrganisationId == OrganisationId;

                int rowCount = DbSet.Count(query);
                IEnumerable<Company> companies =
                    DbSet.Where(query).OrderByDescending(x => x.Name)
                         .Skip(fromRow)
                        .Take(toRow)
                        .ToList();

                return new CompanySearchResponseForCalendar
                {
                    TotalCount = rowCount,
                    Companies = companies
                };
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        /// <summary>
        /// Count of live stores
        /// </summary>
        public int LiveStoresCountForDashboard()
        {
            try
            {
                return DbSet.Count(company => !company.isArchived.HasValue && company.OrganisationId == OrganisationId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
    }
}
