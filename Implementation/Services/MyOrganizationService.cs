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
        public Organisation FindDetailById(long organizationId)
        {
            return organisationRepository.Find(organizationId);
        }

        /// <summary>
        /// Add/Update Organization
        /// </summary>
        public MyOrganizationSaveResponse SaveOrganization(Organisation organisation)
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
        /// Add New Organization
        /// </summary>
        private MyOrganizationSaveResponse Save(Organisation organisation)
        {
            organisation.UserDomainKey = organisationRepository.UserDomainKey;
            organisationRepository.Add(organisation);
            organisationRepository.SaveChanges();

            #region Markup

            if (organisation.Markups != null)
            {
                foreach (var item in organisation.Markups)
                {
                    item.UserDomainKey = organisationRepository.UserDomainKey;
                    markupRepository.Add(item);
                    markupRepository.SaveChanges();
                }
            }

            #endregion

            #region Chart Of Accounts

            if (organisation.ChartOfAccounts != null)
            {
                foreach (var item in organisation.ChartOfAccounts)
                {
                    item.UserDomainKey = organisationRepository.UserDomainKey;
                    chartOfAccountRepository.Add(item);
                    chartOfAccountRepository.SaveChanges();
                }
            }

            #endregion

            return new MyOrganizationSaveResponse
            {
                OrganizationId = organisation.OrganisationId,
                ChartOfAccounts = chartOfAccountRepository.GetAll(),
                Markups = markupRepository.GetAll(),
                //TaxRates = taxRateRepository.GetAll(),
            };
        }

        /// <summary>
        /// Update Organization
        /// </summary>
        private MyOrganizationSaveResponse Update(Organisation organisation)
        {
            organisation.UserDomainKey = organisationRepository.UserDomainKey;
            organisationRepository.Update(organisation);
            organisationRepository.SaveChanges();
            IEnumerable<TaxRate> taxRatesDbVersion = taxRateRepository.GetAll();
            IEnumerable<Markup> markupsDbVersion = markupRepository.GetAll();
            IEnumerable<ChartOfAccount> chartOfAccountsDbVersion = chartOfAccountRepository.GetAll();

            #region Tax Rate
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

            #endregion

            #region Markup
            if (organisation.Markups != null)
            {
                foreach (var item in organisation.Markups)
                {
                    //In case of added new Markup
                    if (
                        markupsDbVersion.All(
                            x =>
                                x.MarkUpId != item.MarkUpId ||
                                item.MarkUpId == 0))
                    {
                        item.UserDomainKey = organisationRepository.UserDomainKey;
                        markupRepository.Add(item);
                        markupRepository.SaveChanges();
                    }
                    else
                    {
                        //In case of Markup Updated
                        foreach (var dbItem in markupsDbVersion)
                        {
                            if (dbItem.MarkUpId == item.MarkUpId)
                            {
                                if (dbItem.MarkUpName != item.MarkUpName || dbItem.MarkUpRate != item.MarkUpRate)
                                {
                                    dbItem.MarkUpName = item.MarkUpName;
                                    dbItem.MarkUpRate = item.MarkUpRate;
                                }
                            }
                        }
                    }
                }
                markupRepository.SaveChanges();
            }
            //find missing items
            List<Markup> missingMarkupListItems = new List<Markup>();
            foreach (Markup dbversionMarkupItem in markupsDbVersion)
            {
                if (organisation.Markups != null && organisation.Markups.All(x => x.MarkUpId != dbversionMarkupItem.MarkUpId))
                {
                    missingMarkupListItems.Add(dbversionMarkupItem);
                }
                //In case user delete all Markup items from client side then it delete all items from db
                if (organisation.Markups == null)
                {
                    missingMarkupListItems.Add(dbversionMarkupItem);
                }
            }
            //remove missing items
            foreach (Markup missingMarkupItem in missingMarkupListItems)
            {
                Markup dbVersionMissingItem = markupsDbVersion.First(x => x.MarkUpId == missingMarkupItem.MarkUpId);
                if (dbVersionMissingItem.MarkUpId > 0)
                {
                    markupRepository.Delete(dbVersionMissingItem);
                    markupRepository.SaveChanges();
                }
            }
            #endregion

            #region Chart Of Accounts
            if (organisation.ChartOfAccounts != null)
            {
                foreach (var item in organisation.ChartOfAccounts)
                {
                    //In case of added new Chart Of Account
                    if (
                        chartOfAccountsDbVersion.All(
                            x =>
                                x.Id != item.Id ||
                                item.Id == 0))
                    {
                        item.UserDomainKey = organisationRepository.UserDomainKey;
                        chartOfAccountRepository.Add(item);
                        chartOfAccountRepository.SaveChanges();
                    }
                    else
                    {
                        //In case of Chart Of Account Updated
                        foreach (var dbItem in chartOfAccountsDbVersion)
                        {
                            if (dbItem.Id == item.Id)
                            {
                                if (dbItem.Name != item.Name || dbItem.AccountNo != item.AccountNo)
                                {
                                    dbItem.Name = item.Name;
                                    dbItem.AccountNo = item.AccountNo;
                                }
                            }
                        }
                    }
                }
                chartOfAccountRepository.SaveChanges();
            }
            //find missing items
            List<ChartOfAccount> missingChartOfAccountItems = new List<ChartOfAccount>();
            foreach (ChartOfAccount dbversionChartOfAccountItem in chartOfAccountsDbVersion)
            {
                if (organisation.ChartOfAccounts != null && organisation.ChartOfAccounts.All(x => x.Id != dbversionChartOfAccountItem.Id))
                {
                    missingChartOfAccountItems.Add(dbversionChartOfAccountItem);
                }
                //In case user delete all Chart Of Accounts items from client side then it delete all items from db
                if (organisation.ChartOfAccounts == null)
                {
                    missingChartOfAccountItems.Add(dbversionChartOfAccountItem);
                }
            }
            //remove missing items
            foreach (ChartOfAccount missingChartOfAccountItem in missingChartOfAccountItems)
            {
                ChartOfAccount dbVersionMissingItem = chartOfAccountsDbVersion.First(x => x.Id == missingChartOfAccountItem.Id);
                if (dbVersionMissingItem.Id > 0)
                {
                    chartOfAccountRepository.Delete(dbVersionMissingItem);
                    chartOfAccountRepository.SaveChanges();
                }
            }
            #endregion

            return new MyOrganizationSaveResponse
            {
                OrganizationId = organisation.OrganisationId,
                ChartOfAccounts = chartOfAccountRepository.GetAll(),
                Markups = markupRepository.GetAll(),
                //TaxRates = taxRateRepository.GetAll(),
            };
        }

        #endregion

        public System.Collections.Generic.IList<int> GetOrganizationIds(int request)
        {
            throw new System.NotImplementedException();
        }
    }
}
