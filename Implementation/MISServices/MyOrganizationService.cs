using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using MPC.ExceptionHandling;
using MPC.Interfaces.MISServices;
using System.Collections.Generic;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Models.ResponseModels;
using System.Resources;


namespace MPC.Implementation.MISServices
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
        private readonly IChartOfAccountRepository chartOfAccountRepository;
        private readonly IStateRepository stateRepository;
        private readonly ICountryRepository countryRepository;
        private readonly IPrefixRepository prefixRepository;
        private readonly ICurrencyRepository currencyRepository;
        private readonly IWeightUnitRepository weightUnitRepository;
        private readonly ILengthUnitRepository lengthUnitRepository;
        private readonly IGlobalLanguageRepository globalLanguageRepository;

        #endregion

        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public MyOrganizationService(IOrganisationRepository organisationRepository, IMarkupRepository markupRepository,
         IChartOfAccountRepository chartOfAccountRepository,
            ICountryRepository countryRepository, IStateRepository stateRepository, IPrefixRepository prefixRepository,
           ICurrencyRepository currencyRepository, IWeightUnitRepository weightUnitRepository, ILengthUnitRepository lengthUnitRepository,
            IGlobalLanguageRepository globalLanguageRepository)
        {
            this.organisationRepository = organisationRepository;
            this.markupRepository = markupRepository;
            this.chartOfAccountRepository = chartOfAccountRepository;
            this.countryRepository = countryRepository;
            this.stateRepository = stateRepository;
            this.prefixRepository = prefixRepository;
            this.currencyRepository = currencyRepository;
            this.weightUnitRepository = weightUnitRepository;
            this.lengthUnitRepository = lengthUnitRepository;
            this.globalLanguageRepository = globalLanguageRepository;
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
                Countries = countryRepository.GetAll(),
                States = stateRepository.GetAll(),
                Currencies = currencyRepository.GetAll(),
                LengthUnits = lengthUnitRepository.GetAll(),
                WeightUnits = weightUnitRepository.GetAll(),
                GlobalLanguages = globalLanguageRepository.GetAll(),
            };
        }

        /// <summary>
        ///  Find Organisation Detail By Organisation ID
        /// </summary>
        public Organisation GetOrganisationDetail()
        {
            Organisation organization = organisationRepository.Find(organisationRepository.OrganisationId);
            IEnumerable<Markup> markups = markupRepository.GetAll();
            if (markups != null && markups.Count() > 0)
            {
                organization.MarkupId = markupRepository.GetAll().First(x => x.IsDefault != null).MarkUpId;
            }
            return SetLanguageEditor(organization);
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
            //Set updated fields
            return Update(organisation, organisationDbVersion);
        }

        /// <summary>
        /// Save File path 
        /// </summary>
        /// <param name="filePath"></param>
        public void SaveFile(string filePath)
        {
            Organisation organisation = organisationRepository.Find(organisationRepository.OrganisationId);
            if (organisation.MISLogo != null)
            {
                if (File.Exists(organisation.MISLogo))
                {
                    //If already organisation logo is save,it delete it 
                    File.Delete(organisation.MISLogo);
                }
            }
            organisation.MISLogo = filePath;
            organisationRepository.SaveChanges();
        }

        /// <summary>
        /// Add New Organization
        /// </summary>
        private MyOrganizationSaveResponse Save(Organisation organisation)
        {
            organisation.OrganisationId = organisationRepository.OrganisationId;
            organisationRepository.Add(organisation);
            organisationRepository.SaveChanges();

            #region Markup

            if (organisation.Markups != null)
            {
                foreach (var item in organisation.Markups)
                {
                    item.OrganisationId = organisationRepository.OrganisationId;
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
                    item.UserDomainKey = (int)organisationRepository.OrganisationId;
                    chartOfAccountRepository.Add(item);
                    chartOfAccountRepository.SaveChanges();
                }
            }

            #endregion

            UpdateLanguageResource(organisation);
            return new MyOrganizationSaveResponse
            {
                OrganizationId = organisation.OrganisationId,
                ChartOfAccounts = chartOfAccountRepository.GetAll(),
                Markups = markupRepository.GetAll(),
            };
        }

        /// <summary>
        /// Update Organization
        /// </summary>
        private MyOrganizationSaveResponse Update(Organisation organisation, Organisation organisationDbVersion)
        {
            organisation.OrganisationId = organisationRepository.OrganisationId;
            organisation.MISLogo = organisationDbVersion.MISLogo;
            IEnumerable<Markup> markupsDbVersion = markupRepository.GetAll();
            IEnumerable<ChartOfAccount> chartOfAccountsDbVersion = chartOfAccountRepository.GetAll();
            #region Markup

            if (organisation.MarkupId != null)
            {
                if (organisation.MarkupId != markupsDbVersion.First(x => x.IsDefault != null).MarkUpId)
                {
                    Markup markup = markupsDbVersion.First(x => x.MarkUpId == organisation.MarkupId);
                    markup.IsDefault = true;
                    Markup markupOld = markupsDbVersion.First(x => x.IsDefault != null);
                    markupOld.IsDefault = null;
                    markupRepository.SaveChanges();
                }
            }

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
                        item.OrganisationId = organisationRepository.OrganisationId;
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

            //Check whether deleted markup used in prefix
            foreach (Markup missingMarkupItem in missingMarkupListItems)
            {
                Markup dbVersionMissingItem = markupsDbVersion.First(x => x.MarkUpId == missingMarkupItem.MarkUpId);
                if (dbVersionMissingItem.MarkUpId > 0)
                {
                    if (prefixRepository.PrefixUseMarkupId(dbVersionMissingItem.MarkUpId))
                    {
                        throw new MPCException("Deleted Markup used in Prefix.", 0);
                    }
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
                        item.UserDomainKey = (int)organisationRepository.OrganisationId;
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
            organisationRepository.Update(organisation);
            organisationRepository.SaveChanges();
            UpdateLanguageResource(organisation);
            return new MyOrganizationSaveResponse
            {
                OrganizationId = organisation.OrganisationId,
                ChartOfAccounts = chartOfAccountRepository.GetAll(),
                Markups = markupRepository.GetAll(),
            };
        }


        public IList<int> GetOrganizationIds(int request)
        {
            return new List<int>();
        }

        /// <summary>
        /// Save File Path
        /// </summary>
        public void SaveFilePath(string path)
        {
            // Update Organisation MISLogoStreamId
            Organisation organisation = organisationRepository.Find(organisationRepository.OrganisationId);

            if (organisation == null)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, LanguageResources.MyOrganisationService_OrganisationNotFound,
                    organisationRepository.OrganisationId));
            }
            organisation.MISLogo = path;
            organisationRepository.SaveChanges();
        }

        public LanguageEditor ReadResourceFileByLanguageId(long organisationId, long lanuageId)
        {
            LanguageEditor languageEditor = new LanguageEditor();
            GlobalLanguage globalLanguage = globalLanguageRepository.Find(lanuageId);
            string sResxPath = null;
            if (globalLanguage != null)
            {
                sResxPath =
                    HttpContext.Current.Server.MapPath("~/MPC_Content/Resources/" +
                                                                  organisationId);
                sResxPath = sResxPath + "\\" + globalLanguage.culture + "\\LanguageResource.resx";
            }
            if (sResxPath != null && File.Exists(sResxPath))
            {
                //Get existing resources
                ResXResourceReader reader = new ResXResourceReader(sResxPath);
                foreach (DictionaryEntry d in reader)
                {
                    switch (d.Key.ToString())
                    {
                        case "DefaultAddress":
                            languageEditor.DefaultAddress = (d.Value != null && d.Value.ToString().Trim() != "") ? d.Value.ToString() : string.Empty;
                            break;
                        case "DefaultShippingAddress":
                            languageEditor.DefaultShippingAddress = (d.Value != null && d.Value.ToString().Trim() != "") ? d.Value.ToString() : string.Empty;
                            break;
                        case "PONumber":
                            languageEditor.PONumber = (d.Value != null && d.Value.ToString().Trim() != "") ? d.Value.ToString() : string.Empty;
                            break;
                        case "Prices":
                            languageEditor.Prices = (d.Value != null && d.Value.ToString().Trim() != "") ? d.Value.ToString() : string.Empty;
                            break;
                        case "UserShippingAddress":
                            languageEditor.UserShippingAddress = (d.Value != null && d.Value.ToString().Trim() != "") ? d.Value.ToString() : string.Empty;
                            break;
                        case "Details":
                            languageEditor.Details = (d.Value != null && d.Value.ToString().Trim() != "") ? d.Value.ToString() : string.Empty;
                            break;
                        case "NewsLetter":
                            languageEditor.NewsLetter = (d.Value != null && d.Value.ToString().Trim() != "") ? d.Value.ToString() : string.Empty;
                            break;
                        case "ConfirmDesign":
                            languageEditor.ConfirmDesign = (d.Value != null && d.Value.ToString().Trim() != "") ? d.Value.ToString() : string.Empty;
                            break;

                    }
                }
                reader.Close();
            }

            return languageEditor;
        }

        /// <summary>
        /// Add/Update Lanuage Resource File
        /// </summary>
        /// <param name="organisation"></param>
        private void UpdateLanguageResource(Organisation organisation)
        {
            if (organisation.LanguageId != null)
            {
                GlobalLanguage globalLanguage = globalLanguageRepository.Find(organisation.LanguageId.Value);
                string sResxPath =
                    HttpContext.Current.Server.MapPath("~/MPC_Content/Resources/" +
                                                                  organisation.OrganisationId);
                if (globalLanguage != null)
                {
                    sResxPath = sResxPath + "\\" + globalLanguage.culture + "\\LanguageResource.resx";
                }
                if (sResxPath != null && File.Exists(sResxPath))
                {
                    Hashtable data = new Hashtable();
                    data.Add("DefaultAddress", organisation.LanguageEditor.DefaultAddress);
                    data.Add("DefaultShippingAddress", organisation.LanguageEditor.DefaultShippingAddress);
                    data.Add("PONumber", organisation.LanguageEditor.PONumber);
                    data.Add("Prices", organisation.LanguageEditor.Prices);
                    data.Add("UserShippingAddress", organisation.LanguageEditor.UserShippingAddress);
                    data.Add("Details", organisation.LanguageEditor.Details);
                    data.Add("NewsLetter", organisation.LanguageEditor.NewsLetter);
                    data.Add("ConfirmDesign", organisation.LanguageEditor.ConfirmDesign);
                    UpdateResourceFile(data, sResxPath);
                }
            }
        }

        /// <summary>
        /// Write Resource File
        /// </summary>
        public static void UpdateResourceFile(Hashtable data, String path)
        {
            Hashtable resourceEntries = new Hashtable();
            //Modify resources here...
            foreach (String key in data.Keys)
            {
                if (!resourceEntries.ContainsKey(key))
                {
                    String value = data[key] == null ? "" : data[key].ToString();
                    resourceEntries.Add(key, value);
                }
                else
                {
                    String value = data[key] == null ? "" : data[key].ToString();
                    resourceEntries.Remove(key);
                    resourceEntries.Add(key, value);
                }
            }
            //Write the combined resource file
            ResXResourceWriter resourceWriter = new ResXResourceWriter(path);
            foreach (String key in resourceEntries.Keys)
            {
                resourceWriter.AddResource(key, resourceEntries[key]);
            }
            resourceWriter.Generate();
            resourceWriter.Close();
        }

        /// <summary>
        /// Set Properites Language Editor(Read Resource File)
        /// </summary>
        private Organisation SetLanguageEditor(Organisation organisation)
        {
            LanguageEditor languageEditor = new LanguageEditor();
            organisation.LanguageEditor = languageEditor;
            string sResxPath = null;
            if (organisation.GlobalLanguage != null)
            {
                sResxPath =
                    HttpContext.Current.Server.MapPath("~/MPC_Content/Resources/" +
                                                                  organisation.OrganisationId);
                sResxPath = sResxPath + "\\" + organisation.GlobalLanguage.culture + "\\LanguageResource.resx";
            }
            if (sResxPath != null && File.Exists(sResxPath))
            {
                //Get existing resources
                ResXResourceReader reader = new ResXResourceReader(sResxPath);
                foreach (DictionaryEntry d in reader)
                {
                    switch (d.Key.ToString())
                    {
                        case "DefaultAddress":
                            organisation.LanguageEditor.DefaultAddress = (d.Value != null && d.Value.ToString().Trim() != "") ? d.Value.ToString() : string.Empty;
                            break;
                        case "DefaultShippingAddress":
                            organisation.LanguageEditor.DefaultShippingAddress = (d.Value != null && d.Value.ToString().Trim() != "") ? d.Value.ToString() : string.Empty;
                            break;
                        case "PONumber":
                            organisation.LanguageEditor.PONumber = (d.Value != null && d.Value.ToString().Trim() != "") ? d.Value.ToString() : string.Empty;
                            break;
                        case "Prices":
                            organisation.LanguageEditor.Prices = (d.Value != null && d.Value.ToString().Trim() != "") ? d.Value.ToString() : string.Empty;
                            break;
                        case "UserShippingAddress":
                            organisation.LanguageEditor.UserShippingAddress = (d.Value != null && d.Value.ToString().Trim() != "") ? d.Value.ToString() : string.Empty;
                            break;
                        case "Details":
                            organisation.LanguageEditor.Details = (d.Value != null && d.Value.ToString().Trim() != "") ? d.Value.ToString() : string.Empty;
                            break;
                        case "NewsLetter":
                            organisation.LanguageEditor.NewsLetter = (d.Value != null && d.Value.ToString().Trim() != "") ? d.Value.ToString() : string.Empty;
                            break;
                        case "ConfirmDesign":
                            organisation.LanguageEditor.ConfirmDesign = (d.Value != null && d.Value.ToString().Trim() != "") ? d.Value.ToString() : string.Empty;
                            break;

                    }
                }
                reader.Close();
            }

            return organisation;
        }
        #endregion

    }
}
