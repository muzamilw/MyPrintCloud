using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MPC.Interfaces.IServices;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Models.ResponseModels;

namespace MPC.Implementation.Services
{
    /// <summary>
    /// My Organization Service
    /// </summary>
    public class MyOrganizationService : IMyOrganizationService
    {
        #region Private

        /// <summary>
        /// Private members
        /// </summary>
        private readonly IOrganisationRepository organisationRepository;
        private readonly IMarkupRepository markupRepository;
        private readonly ITaxRateRepository taxRateRepository;
        private readonly IChartOfAccountRepository chartOfAccountRepository;

        #endregion

        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public MyOrganizationService(IOrganisationRepository organisationRepository, IMarkupRepository markupRepository,
            ITaxRateRepository taxRateRepository, IChartOfAccountRepository chartOfAccountRepository)
        {
            this.organisationRepository = organisationRepository;
            this.markupRepository = markupRepository;
            this.taxRateRepository = taxRateRepository;
            this.chartOfAccountRepository = chartOfAccountRepository;
        }

        #endregion

        #region Public
        /// <summary>
        /// Load My Organization Base data
        /// </summary>
        public MyOrganizationBaseResponse GetBaseData()
        {
            return new MyOrganizationBaseResponse
            {
                ChartOfAccounts = chartOfAccountRepository.GetAll(),
                Markups = markupRepository.GetAll(),
                TaxRates = taxRateRepository.GetAll(),
            };
        }

        /// <summary>
        ///  Find Organisation Detail By Organisation ID
        /// </summary>
        public Organisation FindDetailById(int companySiteId)
        {
            return organisationRepository.Find(companySiteId);
        }

        /// <summary>
        /// Add/Update Company Sites
        /// </summary>
        public long SaveCompanySite(Organisation organisation)
        {
            Organisation organisationDbVersion = organisationRepository.Find(organisation.OrganisationId);
            if (organisationDbVersion == null)
            {
                return Save(organisation);
            }
            else
            {
                //Set updated fields
                return Update(organisation);
            }
        }

        /// <summary>
        /// Add New Company Sites
        /// </summary>
        private long Save(Organisation organisation)
        {
            organisation.UserDomainKey = organisationRepository.UserDomainKey;
            organisationRepository.Add(organisation);
            organisationRepository.SaveChanges();

            //#region Tax Rate
            //foreach (var item in organisation.TaxRates)
            //{
            //    item.UserDomainKey = organisationRepository.UserDomainKey;
            //    taxRateRepository.Add(item);
            //    taxRateRepository.SaveChanges();
            //}
            //#endregion

            //#region Markup
            //foreach (var item in organisation.Markups)
            //{
            //    item.UserDomainKey = organisationRepository.UserDomainKey;
            //    markupRepository.Add(item);
            //    markupRepository.SaveChanges();
            //}
            //#endregion

            //#region Chart Of Accounts
            //foreach (var item in organisation.ChartOfAccounts)
            //{
            //    item.UserDomainKey = organisationRepository.UserDomainKey;
            //    chartOfAccountRepository.Add(item);
            //    chartOfAccountRepository.SaveChanges();
            //}
            //#endregion
            return organisation.OrganisationId;
        }

        /// <summary>
        /// Update Company Sites
        /// </summary>
        private long Update(Organisation organisation)
        {
            organisationRepository.Update(organisation);
            organisationRepository.SaveChanges();
            IEnumerable<TaxRate> taxRatesDbVersion = taxRateRepository.GetAll();
            IEnumerable<Markup> markupsDbVersion = markupRepository.GetAll();
            IEnumerable<ChartOfAccount> chartOfAccountsDbVersion = chartOfAccountRepository.GetAll();

            //#region Tax Rate
            //if (organisation.TaxRates != null)
            //{
            //    foreach (var item in organisation.TaxRates)
            //    {
            //        //In case of added new Tax Rates
            //        if (
            //            taxRatesDbVersion.All(
            //                x =>
            //                    x.TaxId != item.TaxId ||
            //                    item.TaxId == 0))
            //        {
            //            item.UserDomainKey = taxRateRepository.UserDomainKey;
            //            taxRateRepository.Add(item);
            //            taxRateRepository.SaveChanges();
            //        }
            //        else
            //        {
            //            //In case of Tax Rate Updated
            //            foreach (var dbItem in taxRatesDbVersion)
            //            {
            //                if (dbItem.TaxId == item.TaxId)
            //                {
            //                    if (dbItem.TaxName != item.TaxName || dbItem.TaxCode != item.TaxCode)
            //                    {
            //                        taxRateRepository.Update(item);
            //                        taxRateRepository.SaveChanges();
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            ////find missing items
            //List<TaxRate> missingTaxRateListItems = new List<TaxRate>();
            //foreach (TaxRate dbversionTaxRateItem in taxRatesDbVersion)
            //{
            //    if (organisation.TaxRates != null && organisation.TaxRates.All(x => x.TaxId != dbversionTaxRateItem.TaxId))
            //    {
            //        missingTaxRateListItems.Add(dbversionTaxRateItem);
            //    }
            //    //In case user delete all Tax Rate items from client side then it delete all items from db
            //    if (organisation.TaxRates == null)
            //    {
            //        missingTaxRateListItems.Add(dbversionTaxRateItem);
            //    }
            //}
            ////remove missing items
            //foreach (TaxRate missingTaxRateItem in missingTaxRateListItems)
            //{
            //    TaxRate dbVersionMissingItem = taxRatesDbVersion.First(x => x.TaxId == missingTaxRateItem.TaxId);
            //    if (dbVersionMissingItem.TaxId > 0)
            //    {
            //        taxRateRepository.Delete(dbVersionMissingItem);
            //        taxRateRepository.SaveChanges();
            //    }
            //}

            //#endregion
            //#region Markup
            //#endregion
            //#region Chart Of Accounts
            //#endregion
            return organisation.OrganisationId;
        }

        #endregion

        public System.Collections.Generic.IList<int> GetOrganizationIds(int request)
        {
            throw new System.NotImplementedException();
        }
    }
}
