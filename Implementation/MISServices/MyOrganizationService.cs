using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using MPC.ExceptionHandling;
using MPC.Interfaces.MISServices;
using System.Collections.Generic;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Models.ResponseModels;
using System.Resources;
using MPC.Models.Common;
using System.Configuration;
using Newtonsoft.Json;


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
        private readonly ICompanyRepository _companyRepository;
        private readonly ICompanyDomainRepository companyDomainRepository;
        private readonly IMediaLibraryRepository mediaLibraryRepository;
        private readonly ICompanyBannerSetRepository bannerSetRepository;
        private readonly ICompanyBannerRepository companyBannerRepository;
        private readonly ICmsPageRepository cmsPageRepository;
        private readonly ICmsSkinPageWidgetRepository cmsSkinPageWidgetRepository;
        private readonly ICmsSkinPageWidgetParamRepository cmsSkinPageWidgetParamRepository;
        private readonly IPaymentGatewayRepository paymentGatewayRepository;
        private readonly IRaveReviewRepository raveReviewRepository;
        private readonly ICompanyTerritoryRepository companyTerritoryRepository;
        private readonly IAddressRepository addressRepository;
        private readonly ICompanyContactRepository companyContactRepository;
        private readonly ICampaignRepository campaignRepository;
        private readonly ICampaignImageRepository campaignImageRepository;
        private readonly ICompanyCostCenterRepository companyCostCenterRepository;
        private readonly ICostCentreRepository costCentreRepository;

        #endregion

        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public MyOrganizationService(IOrganisationRepository organisationRepository, IMarkupRepository markupRepository,
         IChartOfAccountRepository chartOfAccountRepository,
            ICountryRepository countryRepository, IStateRepository stateRepository, IPrefixRepository prefixRepository,
           ICurrencyRepository currencyRepository, IWeightUnitRepository weightUnitRepository, ILengthUnitRepository lengthUnitRepository,
            IGlobalLanguageRepository globalLanguageRepository, ICompanyRepository companyRepository, ICompanyDomainRepository companyDomainRepository,
            IMediaLibraryRepository mediaLibraryRepository, ICompanyBannerSetRepository bannerSetRepository, ICompanyBannerRepository companyBannerRepository, ICmsPageRepository cmsPageRepository,ICmsSkinPageWidgetRepository cmsSkinPageWidgetRepository,
            ICmsSkinPageWidgetParamRepository cmsSkinPageWidgetParamRepository, IPaymentGatewayRepository paymentGatewayRepository, IRaveReviewRepository raveReviewRepository, ICompanyTerritoryRepository companyTerritoryRepository, IAddressRepository addressRepository
            , ICompanyContactRepository companyContactRepository, ICampaignRepository campaignRepository, ICampaignImageRepository campaignImageRepository, ICompanyCostCenterRepository companyCostCenterRepository, ICostCentreRepository costCentreRepository)
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
            this._companyRepository = companyRepository;
            this.companyDomainRepository = companyDomainRepository;
            this.mediaLibraryRepository = mediaLibraryRepository;
            this.bannerSetRepository = bannerSetRepository;
            this.companyBannerRepository = companyBannerRepository;
            this.cmsPageRepository = cmsPageRepository;
            this.cmsSkinPageWidgetRepository = cmsSkinPageWidgetRepository;
            this.cmsSkinPageWidgetParamRepository = cmsSkinPageWidgetParamRepository;
            this.paymentGatewayRepository = paymentGatewayRepository;
            this.raveReviewRepository = raveReviewRepository;
            this.companyTerritoryRepository = companyTerritoryRepository;
            this.addressRepository = addressRepository;
            this.companyContactRepository = companyContactRepository;
            this.campaignRepository = campaignRepository;
            this.campaignImageRepository = campaignImageRepository;
            this.companyCostCenterRepository = companyCostCenterRepository;
            this.costCentreRepository = costCentreRepository;

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
                //ChartOfAccounts = chartOfAccountRepository.GetAll(),
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
        /// Load My Organization Base data
        /// </summary>
        public MyOrganizationBaseResponse GetRegionalSettingBaseData()
        {
            return new MyOrganizationBaseResponse
            {
                //ChartOfAccounts = chartOfAccountRepository.GetAll(),
  
                Currencies = currencyRepository.GetAll(),
                LengthUnits = lengthUnitRepository.GetAll(),
                WeightUnits = weightUnitRepository.GetAll(),
             
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
                Markup markup = markups.FirstOrDefault(x => x.IsDefault != null);
                if (markup != null)
                {
                    organization.MarkupId = markup.MarkUpId;
                }
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
                throw new MPCException(string.Format("Organisation {0} doesn't Exist.", organisation.OrganisationName),
                            organisationRepository.OrganisationId);
                //Save(organisation); // Commented by Khurram. As Organisation is not created from MIS, it is only updated.
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
            //organisationRepository.SaveChanges();

            #region Markup

            if (organisation.Markups != null)
            {
                foreach (var item in organisation.Markups)
                {
                    item.OrganisationId = organisationRepository.OrganisationId;
                    markupRepository.Add(item);
                    organisation.Markups.Add(item);
                    //markupRepository.SaveChanges();
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
                    organisation.ChartOfAccounts.Add(item);
                    //chartOfAccountRepository.SaveChanges();
                }
            }

            #endregion

            organisationRepository.SaveChanges();

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

            IEnumerable<Markup> markupsDbVersion = markupRepository.GetAll();
            IEnumerable<ChartOfAccount> chartOfAccountsDbVersion = chartOfAccountRepository.GetAll();
            organisationDbVersion.OrganisationId = organisationRepository.OrganisationId;
            organisationDbVersion.OrganisationName = organisation.OrganisationName;
           // organisationDbVersion.SmtpServer = organisation.SmtpServer;
           // organisationDbVersion.SmtpUserName = organisation.SmtpUserName;
          //  organisationDbVersion.SmtpPassword = organisation.SmtpPassword;
            organisationDbVersion.Address1 = organisation.Address1;
            organisationDbVersion.Address2 = organisation.Address2;
            organisationDbVersion.City = organisation.City;
            organisationDbVersion.StateId = organisation.StateId;
            organisationDbVersion.CountryId = organisation.CountryId;
            organisationDbVersion.ZipCode = organisation.ZipCode;
            organisationDbVersion.Tel = organisation.Tel;
            organisationDbVersion.Email = organisation.Email;
            organisationDbVersion.Fax = organisation.Fax;
            organisationDbVersion.VATRegNumber = organisation.VATRegNumber;
            organisationDbVersion.TaxRegistrationNo = organisation.TaxRegistrationNo;
            organisationDbVersion.BleedAreaSize = organisation.BleedAreaSize;
            organisationDbVersion.ShowBleedArea = organisation.ShowBleedArea;
            organisationDbVersion.CurrencyId = organisation.CurrencyId;
            organisationDbVersion.LanguageId = organisation.LanguageId;
           // organisationDbVersion.SystemLengthUnit = organisation.SystemLengthUnit;
            organisationDbVersion.SystemWeightUnit = organisation.SystemWeightUnit;
            organisationDbVersion.URL = organisation.URL;
            organisationDbVersion.Mobile = organisation.Mobile;
            organisationDbVersion.IsImperical = organisation.IsImperical;
            organisationDbVersion.AgileApiKey = organisation.AgileApiKey;
            organisationDbVersion.AgileApiUrl = organisation.AgileApiUrl;
            organisationDbVersion.isAgileActive = organisation.isAgileActive;
            organisationDbVersion.XeroApiId = organisation.XeroApiId;
            organisationDbVersion.XeroApiKey = organisation.XeroApiKey;
            organisationDbVersion.isXeroIntegrationRequired = organisation.isXeroIntegrationRequired;
            organisationDbVersion.IsZapierEnable = organisation.IsZapierEnable;
            organisationDbVersion.DefaultPOTax = organisation.DefaultPOTax;
            organisationDbVersion.ShowBleedArea = organisation.ShowBleedArea;
            organisationDbVersion.BleedAreaSize = organisation.BleedAreaSize;
            if(organisation.IsImperical == true)
            {
                organisationDbVersion.SystemLengthUnit = 3;
                organisationDbVersion.SystemWeightUnit = 1;

            }
            else
            {
                organisationDbVersion.SystemLengthUnit = 1;
                organisationDbVersion.SystemWeightUnit = 3;
            }
            #region Markup

            if (organisation.MarkupId != null)
            {

                Markup oldDefaultMarkup = markupsDbVersion.FirstOrDefault(x => x.IsDefault != null);
                // Set Default Markup
                if (oldDefaultMarkup != null)
                {
                    // Reset Old Default Markup
                    oldDefaultMarkup.IsDefault = null;
                }

                Markup markup = markupsDbVersion.FirstOrDefault(x => x.MarkUpId == organisation.MarkupId);
                if (markup != null)
                {
                    // Set New Default
                    markup.IsDefault = true;
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
                        throw new MPCException(string.Format("Can not delete markup {0} it is being used in Prefix.", dbVersionMissingItem.MarkUpName),
                            organisationRepository.OrganisationId);
                    }
                }
            }

            //remove missing items
            foreach (Markup missingMarkupItem in missingMarkupListItems)
            {
                // Markup dbVersionMissingItem = markupsDbVersion.First(x => x.MarkUpId == missingMarkupItem.MarkUpId);
                markupRepository.Delete(missingMarkupItem);
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
                    if (organisation.ChartOfAccounts != null)
                    {
                        organisation.ChartOfAccounts.Remove(dbVersionMissingItem);
                    }
                }
            }
            #endregion

            organisationDbVersion.MISLogo = SaveMiSLogo(organisation);
            organisationRepository.SaveChanges();
            UpdateLanguageResource(organisation);
            return new MyOrganizationSaveResponse
            {
                OrganizationId = organisation.OrganisationId,
                ChartOfAccounts = chartOfAccountRepository.GetAll(),
                Markups = markupRepository.GetAll(),
            };
        }
        /// <summary>
        /// Save Images for Company Contact Profile Image
        /// </summary>
        private string SaveMiSLogo(Organisation companyContact)
        {
            if (companyContact.MISLogo != null)
            {
                string base64 = companyContact.MISLogo.Substring(companyContact.MISLogo.IndexOf(',') + 1);
                base64 = base64.Trim('\0');
                byte[] data = Convert.FromBase64String(base64);

                string directoryPath = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyContact.OrganisationId + "/" + companyContact.OrganisationName + "/Logo");
                if (File.Exists(directoryPath))
                {
                    //If already organisation logo is save,it delete it 
                    File.Delete(directoryPath);
                }
                if (directoryPath != null && !Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                string savePath =
                    directoryPath + "\\" +
                    companyContact.OrganisationId + "_" + StringHelper.SimplifyString(companyContact.OrganisationName) + "_MIS_Logo.png";
                File.WriteAllBytes(savePath, data);
                int indexOf = savePath.LastIndexOf("MPC_Content", StringComparison.Ordinal);
                savePath = savePath.Substring(indexOf, savePath.Length - indexOf);
                return savePath;
            }
            return null;
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

        public List<LanguageEditor> ReadResourceFileByLanguageId(long organisationId, long lanuageId)
        {
            List<LanguageEditor> languageEditors = new List<LanguageEditor>();
            // get organisation object --> organisation.languageid
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
                    LanguageEditor languageEditor = new LanguageEditor();
                    languageEditor.Key = d.Key.ToString();
                    languageEditor.Value = (d.Value != null && d.Value.ToString().Trim() != "") ? d.Value.ToString() : string.Empty;
                    languageEditors.Add(languageEditor);
                }
                reader.Close();
            }

            return languageEditors;
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
                    if (organisation.LanguageEditors != null)
                    {
                        Hashtable data = new Hashtable();
                        foreach (LanguageEditor languageEditor in organisation.LanguageEditors)
                        {
                            data.Add(languageEditor.Key, languageEditor.Value);
                        }
                        UpdateResourceFile(data, sResxPath);
                    }


                    //data.Add("DefaultAddress", organisation.LanguageEditor.DefaultAddress);
                    //data.Add("DefaultShippingAddress", organisation.LanguageEditor.DefaultShippingAddress);
                    //data.Add("PONumber", organisation.LanguageEditor.PONumber);
                    //data.Add("Prices", organisation.LanguageEditor.Prices);
                    //data.Add("UserShippingAddress", organisation.LanguageEditor.UserShippingAddress);
                    //data.Add("Details", organisation.LanguageEditor.Details);
                    //data.Add("NewsLetter", organisation.LanguageEditor.NewsLetter);
                    //data.Add("ConfirmDesign", organisation.LanguageEditor.ConfirmDesign);

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
                    LanguageEditor languageEditor = new LanguageEditor();
                    languageEditor.Key = d.Key.ToString();
                    languageEditor.Value = (d.Value != null && d.Value.ToString().Trim() != "") ? d.Value.ToString() : string.Empty;
                    if (organisation.LanguageEditors == null)
                    {
                        organisation.LanguageEditors = new List<LanguageEditor>();
                    }
                    organisation.LanguageEditors.Add(languageEditor);
                }
                reader.Close();
            }

            return organisation;
        }


        public bool DeleteOrganisation(long OrganisationID)
        {
            try
            {


                // delete organisation files

                Organisation organisaton = organisationRepository.GetOrganizatiobByID(OrganisationID);


                // delete entities by sp
                organisationRepository.DeleteOrganisationBySP(OrganisationID);

                if (organisaton != null)
                {
                    string SourceDelAssests = HttpContext.Current.Server.MapPath("/MPC_Content/assets/" + OrganisationID);

                    if (Directory.Exists(SourceDelAssests))
                    {
                        Directory.Delete(SourceDelAssests, true);
                    }
                    string SourceDelCostCentre = HttpContext.Current.Server.MapPath("/MPC_Content/CostCentres/" + OrganisationID);

                    if (Directory.Exists(SourceDelCostCentre))
                    {
                        Directory.Delete(SourceDelCostCentre, true);
                    }

                    string SourceDelDesigner = HttpContext.Current.Server.MapPath("/MPC_Content/Designer/" + OrganisationID);

                    if (Directory.Exists(SourceDelDesigner))
                    {
                        Directory.Delete(SourceDelDesigner, true);
                    }
                    string SourceDelMedia = HttpContext.Current.Server.MapPath("/MPC_Content/Media/" + OrganisationID);

                    if (Directory.Exists(SourceDelMedia))
                    {
                        Directory.Delete(SourceDelMedia, true);
                    }

                    string SourceDelOrganisation = HttpContext.Current.Server.MapPath("/MPC_Content/Organisations/" + OrganisationID);

                    if (Directory.Exists(SourceDelOrganisation))
                    {
                        Directory.Delete(SourceDelOrganisation, true);

                    }

                    string SourceDelProducts = HttpContext.Current.Server.MapPath("/MPC_Content/Products/" + OrganisationID);

                    if (Directory.Exists(SourceDelProducts))
                    {
                        Directory.Delete(SourceDelProducts, true);
                    }

                    string SourceDelResources = HttpContext.Current.Server.MapPath("/MPC_Content/Resources/" + OrganisationID);

                    if (Directory.Exists(SourceDelResources))
                    {
                        Directory.Delete(SourceDelResources, true);
                    }


                }
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IEnumerable<Markup> GetMarkups()
        {
            return markupRepository.GetAll();
        }

        public void UpdateOrganisationLicensing(long organisationId, int storesCount, bool isTrial, int misOrdersCount, int webStoreOrdersCount, DateTime billingDate)
        {
            organisationRepository.UpdateOrganisationLicensing(organisationId, storesCount, isTrial, misOrdersCount, webStoreOrdersCount, billingDate);
            if (!isTrial)
            {
                _companyRepository.UpdateLiveStores(organisationId, storesCount);
            }
        }

        public bool CanStoreMakeLive()
        {
            var livestores = _companyRepository.GetLiveStoresCount(organisationRepository.OrganisationId);
            var org = organisationRepository.GetOrganizatiobByID();

            if (org.isTrial == false)
            {
                if (livestores < (org.LiveStoresCount ?? 0))
                    return true;
                else
                    return false;
            }
            else
            {
                return true;
            }
            
            
        }

        public void UpdateOrganisationZapTargetUrl(long organisationId, string sTargetUrl, int zapTargetType)
        {
            organisationRepository.UpdateOrganisationZapTargetUrl(organisationId, sTargetUrl, zapTargetType);
        }
        public void UnSubscriebZapTargetUrl(long organisationId, string sTargetUrl, int zapTargetType)
        {
            organisationRepository.UnSubscribeZapTargetUrl(organisationId, sTargetUrl, zapTargetType);
        }

        public string GetActiveOrganisationId(string param)
        {
            string responsestr = string.Empty;
            string credentials = param.Substring(param.IndexOf("username="),
                    param.Length - param.IndexOf("username="));
            if (!string.IsNullOrEmpty(credentials))
                credentials = credentials.Replace("username=", "email=");
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://myprintcloud.com");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var uri = "/Account/GetCustomerId?" + credentials;
                var response = client.GetAsync(uri);
                if (response.Result.IsSuccessStatusCode)
                {
                    responsestr = response.Result.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    throw new MPCException("Service Not Authenticated!", 0);
                }

            }

            return responsestr;
        }

        public string GetZapierPostUrl()
        {
            var org = organisationRepository.GetOrganizatiobByID();
            if(org.IsZapierEnable == true)
                return organisationRepository.GetOrganizatiobByID().CreateContactZapTargetUrl;
            else
            {
                return string.Empty;
            }
        }
        

        #endregion


        //#region copyStoreForOrganisation

        ///// <summary>
        ///// Clone Store
        ///// </summary>
        //public Company CloneStore(long companyId, long OrganisationId)
        //{
        //    try
        //    {
        //        // company id after clonning
        //        long NewCompanyId = 0;

        //        // Find Company - Throws Exception if not exist
        //        Company source = _companyRepository.GetCompanyByCompanyID(companyId);


        //        // Create New Instance

        //        Company target = CreateNewCompany(OrganisationId);

        //        // Clone
        //        NewCompanyId = CloneCompany(source, target, OrganisationId);


        //        // insert product categories and items
        //        // companyRepository.CopyProductByStore(NewCompanyId, companyId);




        //        // insert discount voucher
        //        CloneDiscountVouchers(companyId, NewCompanyId);

        //        // insert template fonts
        //        CloneTemplateFonts(companyId, NewCompanyId);

        //        CloneReportBanners(companyId, NewCompanyId);
        //        // update data
        //        Company objCompany = _companyRepository.LoadCompanyWithItems(NewCompanyId);

        //        companyRepository.SetTerritoryIdAddress(objCompany, source.CompanyId);
        //        companyRepository.InsertProductCategories(objCompany, source.CompanyId);
        //        companyRepository.InsertItem(objCompany, source.CompanyId);

        //        // insert reports
        //        if (objCompany != null)
        //        {
        //            string SetName = source.CompanyBannerSets.Where(c => c.CompanySetId == source.ActiveBannerSetId).Select(c => c.SetName).FirstOrDefault();
        //            SetValuesAfterClone(objCompany, SetName, source.CompanyId);

        //            // copy variable extension of system variables
        //            companyRepository.SaveSystemVariableExtension(companyId, objCompany.CompanyId);
        //            companyRepository.InsertProductCategoryItems(objCompany, source);
        //            // copy All files or images
        //            CopyCompanyFiles(objCompany, companyId);
        //        }



        //        // Load Item Full
        //        // target = itemRepository.GetItemWithDetails(target.ItemId);

        //        // Get Updated Minimum Price
        //        //target.MinPrice = itemRepository.GetMinimumProductValue(target.ItemId);

        //        // convert template length to system unit 
        //        //  ConvertTemplateLengthToSystemUnit(target);

        //        // Return Product
        //        companyRepository.SaveChanges();
        //        return objCompany;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }


        //}

        ///// <summary>
        ///// Creates New Item and assigns new generated code
        ///// </summary>
        //private Company CreateNewCompany(long Organisationid)
        //{

        //    Company companyTarget = _companyRepository.Create();
        //    _companyRepository.Add(companyTarget);
        //    companyTarget.OrganisationId = Organisationid;
        //    return companyTarget;
        //}

        ///// <summary>
        ///// Creates Copy of Product
        ///// </summary>
        //private long CloneCompany(Company source, Company target, long OrganisationId)
        //{
        //    try
        //    {
        //        // Clone Item
        //        source.Clone(target);
        //        source.OrganisationId = OrganisationId;

        //        // Clone Company Domains
        //        CloneCompanyDomain(source, target);

        //        // clone Media Library
        //        CloneMediaLibrary(source, target);


        //        // Clone company banners sets and its banner
        //        CloneCompanyBannerSet(source, target, OrganisationId);

        //        // Clone cms pages
        //        CloneCMSPages(source, target, OrganisationId);



        //        // clone payment gateways
        //        ClonePaymentGateways(source, target);

        //        // Clone Rave Reviews
        //        CloneRaveReviews(source, target, OrganisationId);



        //        // Clone Company Territory
        //        CloneCompanyTerritory(source, target);

        //        // Clone Addresses
        //        CloneAddresses(source, target, OrganisationId);

        //        // Clone company contacts
        //        CloneCompanyContacts(source, target, OrganisationId);

        //        // Clone campaignEmails
        //        CloneCampaigns(source, target, OrganisationId);

        //        // Clone Pcompany cost centre
        //        CloneCompanyCostCentre(source, target, OrganisationId);

        //        // clone template color style
        //        CloneTemplateColorStyles(source, target);

        //        // Clone company cmyk colors
        //        CloneCompanyCMYKColor(source, target);


        //        // Clone field variables
        //        CloneFieldVariables(source, target);

        //        // Clone smart forms and its details
        //        CloneSmartFom(source, target);

        //        // clone cms offers
        //        CloneCMSOffer(source, target);

        //        // Clone activities
        //        CloneActivities(source, target);


        //        _companyRepository.SaveChanges();

        //        return target.CompanyId;







        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}




        ///// <summary>
        ///// Copy Company Domains
        ///// </summary>
        //public void CloneCompanyDomain(Company source, Company target)
        //{
        //    string subdomain = HttpContext.Current.Request.Url.Host.ToString();
        //    CompanyDomain domain = companyDomainRepository.Create();
        //    domain.Domain = subdomain;
        //    domain.CompanyId = target.CompanyId;
        //    companyDomainRepository.Add(domain);


        //}



        ///// <summary>
        ///// Copy company banners
        ///// </summary>
        //private void CloneCompanyBannerSet(Company source, Company target, long OrganisationId)
        //{
        //    if (source.CompanyBannerSets == null)
        //    {
        //        return;
        //    }

        //    // Initialize List
        //    if (target.CompanyBannerSets == null)
        //    {
        //        target.CompanyBannerSets = new List<CompanyBannerSet>();
        //    }

        //    foreach (CompanyBannerSet companyBannerSet in source.CompanyBannerSets)
        //    {


        //        CompanyBannerSet targetCompanyBannerSet = bannerSetRepository.Create();
        //        bannerSetRepository.Add(targetCompanyBannerSet);
        //        targetCompanyBannerSet.CompanyId = targetCompanyBannerSet.CompanyId;
        //        target.CompanyBannerSets.Add(targetCompanyBannerSet);
        //        companyBannerSet.Clone(targetCompanyBannerSet);
        //        targetCompanyBannerSet.OrganisationId = OrganisationId;

        //        // Clone CompanyBanners
        //        if (companyBannerSet.CompanyBanners == null)
        //        {
        //            continue;
        //        }

        //        // Copy CompanyBanners
        //        CloneCompanyBanners(companyBannerSet, targetCompanyBannerSet);
        //    }
        //}
        ///// <summary>
        ///// Creates Copy of company  Banners
        ///// </summary>
        //private void CloneCompanyBanners(CompanyBannerSet companyBannerSets, CompanyBannerSet targetcompanyBannerSets)
        //{
        //    if (targetcompanyBannerSets.CompanyBanners == null)
        //    {
        //        targetcompanyBannerSets.CompanyBanners = new List<CompanyBanner>();
        //    }

        //    foreach (CompanyBanner objcompanyBanners in companyBannerSets.CompanyBanners.ToList())
        //    {
        //        CompanyBanner targetCompanyBanner = companyBannerRepository.Create();
        //        companyBannerRepository.Add(targetCompanyBanner);
        //        targetCompanyBanner.CompanySetId = targetcompanyBannerSets.CompanySetId;
        //        targetcompanyBannerSets.CompanyBanners.Add(targetCompanyBanner);
        //        objcompanyBanners.Clone(targetCompanyBanner);
        //    }
        //}

        ///// <summary>
        ///// Copy cms pages
        ///// </summary>
        //private void CloneCMSPages(Company source, Company target, long OrganisationId)
        //{
        //    if (source.CmsPages == null)
        //    {
        //        return;
        //    }

        //    // Initialize List
        //    if (target.CmsPages == null)
        //    {
        //        target.CmsPages = new List<CmsPage>();
        //    }

        //    foreach (CmsPage cmsPage in source.CmsPages)
        //    {
        //        CmsPage targetCMSPage = cmsPageRepository.Create();
        //        cmsPageRepository.Add(targetCMSPage);
        //        targetCMSPage.CompanyId = target.CompanyId;
        //        target.CmsPages.Add(targetCMSPage);
        //        cmsPage.Clone(targetCMSPage);
        //        targetCMSPage.OrganisationId = OrganisationId;

        //        // Clone CompanyBanners
        //        if (cmsPage.CmsSkinPageWidgets == null)
        //        {
        //            continue;
        //        }

        //        // Copy CompanyBanners
        //        CloneCMSSkinPageWidgets(cmsPage, targetCMSPage, target, OrganisationId);

        //    }
        //}
        ///// <summary>
        ///// Creates Copy of company  Banners
        ///// </summary>
        //private void CloneCMSSkinPageWidgets(CmsPage cmspage, CmsPage targetcmspage, Company targetCompany, long OrganisationId)
        //{
        //    if (targetcmspage.CmsSkinPageWidgets == null)
        //    {
        //        targetcmspage.CmsSkinPageWidgets = new List<CmsSkinPageWidget>();
        //    }

        //    foreach (CmsSkinPageWidget objcmsSkinPageWidget in cmspage.CmsSkinPageWidgets.ToList())
        //    {
        //        CmsSkinPageWidget targetCMSSkinPageWidget = cmsSkinPageWidgetRepository.Create();
        //        cmsSkinPageWidgetRepository.Add(targetCMSSkinPageWidget);
        //        targetCMSSkinPageWidget.PageId = targetcmspage.PageId;
        //        targetCMSSkinPageWidget.OrganisationId = OrganisationId;
        //        targetCMSSkinPageWidget.CompanyId = targetCompany.CompanyId;
        //        targetcmspage.CmsSkinPageWidgets.Add(targetCMSSkinPageWidget);
        //        objcmsSkinPageWidget.Clone(targetCMSSkinPageWidget);

        //        // Clone params
        //        if (objcmsSkinPageWidget.CmsSkinPageWidgetParams == null)
        //        {
        //            continue;
        //        }

        //        // Copy params
        //        CloneCMSSkinPageWidgetsParams(objcmsSkinPageWidget, targetCMSSkinPageWidget);

        //    }
        //}
        //private void CloneCMSSkinPageWidgetsParams(CmsSkinPageWidget cmsskinPageWidget, CmsSkinPageWidget targetcmsskinPageWidget)
        //{
        //    if (targetcmsskinPageWidget.CmsSkinPageWidgetParams == null)
        //    {
        //        targetcmsskinPageWidget.CmsSkinPageWidgetParams = new List<CmsSkinPageWidgetParam>();
        //    }
        //    foreach (CmsSkinPageWidgetParam objcmsSkinPageWidgetParams in cmsskinPageWidget.CmsSkinPageWidgetParams.ToList())
        //    {
        //        CmsSkinPageWidgetParam targetCMSSkinPageWidgetParam = cmsSkinPageWidgetParamRepository.Create();
        //        cmsSkinPageWidgetParamRepository.Add(targetCMSSkinPageWidgetParam);

        //        targetCMSSkinPageWidgetParam.PageWidgetId = targetcmsskinPageWidget.PageWidgetId;

        //        targetcmsskinPageWidget.CmsSkinPageWidgetParams.Add(targetCMSSkinPageWidgetParam);
        //        objcmsSkinPageWidgetParams.Clone(targetCMSSkinPageWidgetParam);



        //    }
        //}
        ///// <summary>
        ///// Copy  payment gateways
        ///// </summary>
        //private void ClonePaymentGateways(Company source, Company target)
        //{
        //    if (source.PaymentGateways == null)
        //    {
        //        return;
        //    }

        //    // Initialize List
        //    if (target.PaymentGateways == null)
        //    {
        //        target.PaymentGateways = new List<PaymentGateway>();
        //    }

        //    foreach (PaymentGateway paymentGateways in source.PaymentGateways)
        //    {
        //        PaymentGateway targetpaymentGateway = paymentGatewayRepository.Create();
        //        paymentGatewayRepository.Add(targetpaymentGateway);
        //        targetpaymentGateway.CompanyId = target.CompanyId;
        //        target.PaymentGateways.Add(targetpaymentGateway);
        //        paymentGateways.Clone(targetpaymentGateway);
        //    }
        //}


        ///// <summary>
        ///// Copy rave reviews
        ///// </summary>
        //private void CloneRaveReviews(Company source, Company target, long OrganisationId)
        //{
        //    if (source.RaveReviews == null)
        //    {
        //        return;
        //    }

        //    // Initialize List
        //    if (target.RaveReviews == null)
        //    {
        //        target.RaveReviews = new List<RaveReview>();
        //    }

        //    foreach (RaveReview raveReview in source.RaveReviews)
        //    {
        //        RaveReview targetRaveReviews = raveReviewRepository.Create();
        //        raveReviewRepository.Add(targetRaveReviews);
        //        targetRaveReviews.CompanyId = target.CompanyId;
        //        target.RaveReviews.Add(targetRaveReviews);
        //        raveReview.Clone(targetRaveReviews);
        //        raveReview.OrganisationId = OrganisationId;
        //    }
        //}


        ///// <summary>
        ///// Copy company Territory
        ///// </summary>
        //private void CloneCompanyTerritory(Company source, Company target)
        //{
        //    if (source.CompanyTerritories == null)
        //    {
        //        return;
        //    }

        //    // Initialize List
        //    if (target.CompanyTerritories == null)
        //    {
        //        target.CompanyTerritories = new List<CompanyTerritory>();
        //    }

        //    foreach (CompanyTerritory companyTerritory in source.CompanyTerritories)
        //    {
        //        CompanyTerritory targetCompanyTerritory = companyTerritoryRepository.Create();
        //        companyTerritoryRepository.Add(targetCompanyTerritory);
        //        targetCompanyTerritory.CompanyId = target.CompanyId;
        //        targetCompanyTerritory.Addresses = null;
        //        target.CompanyTerritories.Add(targetCompanyTerritory);
        //        companyTerritory.Clone(targetCompanyTerritory);
        //    }
        //}

        ///// <summary>
        ///// Copy ADDRESSES
        ///// </summary>
        //private void CloneAddresses(Company source, Company target, long OrgId)
        //{
        //    if (source.Addresses == null)
        //    {
        //        return;
        //    }

        //    // Initialize List
        //    if (target.Addresses == null)
        //    {
        //        target.Addresses = new List<Address>();
        //    }

        //    foreach (Address addresses in source.Addresses)
        //    {

        //        //string OldTerritoryName = addresses.CompanyTerritory.TerritoryName;
        //        //string oldTerritoryCode = addresses.CompanyTerritory.TerritoryCode;

        //        //CompanyTerritory NewTerrObj = target.CompanyTerritories.Where(c => c.TerritoryName == OldTerritoryName && c.TerritoryCode == oldTerritoryCode).FirstOrDefault();


        //        Address targetAddress = addressRepository.Create();
        //        addressRepository.Add(targetAddress);
        //        targetAddress.CompanyId = target.CompanyId;
        //        targetAddress.Tel2 = Convert.ToString(addresses.AddressId);
        //        // targetAddress.TerritoryId = NewTerrObj != null ? NewTerrObj.TerritoryId : 0;
        //        target.Addresses.Add(targetAddress);
        //        addresses.Clone(targetAddress);
        //        targetAddress.OrganisationId = OrgId;
        //    }

        //}

        ///// <summary>
        ///// Copy companyContacts
        ///// </summary>
        //private void CloneCompanyContacts(Company source, Company target, long OrganisationId)
        //{
        //    if (source.CompanyContacts == null)
        //    {
        //        return;
        //    }

        //    // Initialize List
        //    if (target.CompanyContacts == null)
        //    {
        //        target.CompanyContacts = new List<CompanyContact>();
        //    }

        //    foreach (CompanyContact contacts in source.CompanyContacts)
        //    {

        //        string OldTerritoryName = contacts.CompanyTerritory.TerritoryName;
        //        string oldTerritoryCode = contacts.CompanyTerritory.TerritoryCode;

        //        string OldAddressName = contacts.Address.AddressName;

        //        string OldShippingAddressName = contacts.Address.AddressName;

        //        // CompanyTerritory NewTerrObj = target.CompanyTerritories.Where(c => c.TerritoryName == OldTerritoryName && c.TerritoryCode == oldTerritoryCode).FirstOrDefault();

        //        Address NewAddressObj = target.Addresses.Where(c => c.AddressName == OldAddressName).FirstOrDefault();

        //        Address NewShipingAdd = target.Addresses.Where(c => c.AddressName == OldShippingAddressName).FirstOrDefault();

        //        CompanyContact targetCompanyContact = companyContactRepository.Create();
        //        companyContactRepository.Add(targetCompanyContact);
        //        targetCompanyContact.CompanyId = target.CompanyId;
        //        // targetCompanyContact.TerritoryId = NewTerrObj != null ? NewTerrObj.TerritoryId : 0;


        //        if (NewAddressObj != null)
        //        {
        //            targetCompanyContact.Address = NewAddressObj;
        //            targetCompanyContact.AddressId = NewAddressObj.AddressId;
        //        }
        //        if (NewShipingAdd != null)
        //        {
        //            targetCompanyContact.ShippingAddress = NewShipingAdd;
        //            targetCompanyContact.ShippingAddressId = NewShipingAdd.AddressId;
        //        }
        //        targetCompanyContact.quickAddress3 = Convert.ToString(contacts.ContactId);
        //        target.CompanyContacts.Add(targetCompanyContact);
        //        contacts.Clone(targetCompanyContact);
        //        targetCompanyContact.OrganisationId = OrganisationId;
        //    }
        //}

        ///// <summary>
        ///// Copy campaigns
        ///// </summary>
        //private void CloneCampaigns(Company source, Company target, long OID)
        //{
        //    if (source.Campaigns == null)
        //    {
        //        return;
        //    }

        //    // Initialize List
        //    if (target.Campaigns == null)
        //    {
        //        target.Campaigns = new List<Campaign>();
        //    }

        //    foreach (Campaign campaigns in source.Campaigns)
        //    {
        //        Campaign targetCampaigns = campaignRepository.Create();
        //        campaignRepository.Add(targetCampaigns);
        //        targetCampaigns.CompanyId = target.CompanyId;
        //        target.Campaigns.Add(targetCampaigns);
        //        campaigns.Clone(targetCampaigns);
        //        targetCampaigns.OrganisationId = OID;

        //        // Clone campaign images
        //        if (campaigns.CampaignImages == null)
        //        {
        //            continue;
        //        }

        //        // Copy Campaign Images
        //        CloneCampaignImages(campaigns, targetCampaigns);
        //    }
        //}

        ///// <summary>
        ///// Creates Copy of company  Banners
        ///// </summary>
        //public void CloneCampaignImages(Campaign campaigns, Campaign targetcampaigns)
        //{
        //    if (targetcampaigns.CampaignImages == null)
        //    {
        //        targetcampaigns.CampaignImages = new List<CampaignImage>();
        //    }

        //    foreach (CampaignImage objcampaignImages in campaigns.CampaignImages.ToList())
        //    {
        //        CampaignImage targetCampaignImage = campaignImageRepository.Create();
        //        campaignImageRepository.Add(targetCampaignImage);
        //        targetCampaignImage.CampaignId = targetcampaigns.CampaignId;
        //        targetcampaigns.CampaignImages.Add(targetCampaignImage);
        //        targetCampaignImage.Clone(targetCampaignImage);
        //    }
        //}


        ///// <summary>
        ///// Copy company cost centre
        //private void CloneCompanyCostCentre(Company source, Company target, long OrganisationId)
        //{
        //    List<CostCentre> CostCentreCopiedOrg = costCentreRepository.GetAllCentersByOrganisationId(OrganisationId);
        //    if (source.CompanyCostCentres == null)
        //    {
        //        return;
        //    }

        //    // Initialize List
        //    if (target.CompanyCostCentres == null)
        //    {
        //        target.CompanyCostCentres = new List<CompanyCostCentre>();
        //    }

        //    foreach (CompanyCostCentre companyCostCentre in source.CompanyCostCentres)
        //    {
        //        CompanyCostCentre targetCompanyCostCentre = companyCostCenterRepository.Create();
        //        companyCostCenterRepository.Add(targetCompanyCostCentre);
        //        targetCompanyCostCentre.CompanyId = target.CompanyId;
        //        target.CompanyCostCentres.Add(targetCompanyCostCentre);
        //        companyCostCentre.Clone(targetCompanyCostCentre);
        //        targetCompanyCostCentre.OrganisationId = OrganisationId;


        //    }
        //}


        ///// <summary>
        ///// Copy cmyk color
        ///// 
        ///// </summary>
        //private void CloneCompanyCMYKColor(Company source, Company target)
        //{
        //    if (source.CompanyCMYKColors == null)
        //    {
        //        return;
        //    }

        //    // Initialize List
        //    if (target.CompanyCMYKColors == null)
        //    {
        //        target.CompanyCMYKColors = new List<CompanyCMYKColor>();
        //    }

        //    foreach (CompanyCMYKColor companyCMYKColor in source.CompanyCMYKColors)
        //    {
        //        CompanyCMYKColor targetCompanyCMYKColor = companyCmykColorRepository.Create();
        //        companyCmykColorRepository.Add(targetCompanyCMYKColor);
        //        targetCompanyCMYKColor.CompanyId = target.CompanyId;
        //        target.CompanyCMYKColors.Add(targetCompanyCMYKColor);
        //        companyCMYKColor.Clone(targetCompanyCMYKColor);
        //    }
        //}

        ///// <summary>
        ///// Copy smart form or its details
        ///// 
        ///// </summary>
        //private void CloneSmartFom(Company source, Company target)
        //{
        //    if (source.SmartForms == null)
        //    {
        //        return;
        //    }

        //    // Initialize List
        //    if (target.SmartForms == null)
        //    {
        //        target.SmartForms = new List<SmartForm>();
        //    }

        //    foreach (SmartForm companySmartForm in source.SmartForms)
        //    {
        //        SmartForm targetSmartForm = smartFormRepository.Create();
        //        smartFormRepository.Add(targetSmartForm);
        //        targetSmartForm.CompanyId = target.CompanyId;
        //        target.SmartForms.Add(targetSmartForm);
        //        companySmartForm.Clone(targetSmartForm);

        //        // Clone smart form details
        //        if (companySmartForm.SmartFormDetails == null)
        //        {
        //            continue;
        //        }

        //        // Clone smart form details
        //        CloneSmartFormDetails(companySmartForm, targetSmartForm, target);
        //    }



        //}

        ///// <summary>
        ///// Creates Copy of company  Banners
        ///// </summary>
        //public void CloneSmartFormDetails(SmartForm smartForm, SmartForm targetsmartForm, Company targetCompany)
        //{
        //    if (targetsmartForm.SmartFormDetails == null)
        //    {
        //        targetsmartForm.SmartFormDetails = new List<SmartFormDetail>();
        //    }

        //    foreach (SmartFormDetail objsmartFormDetails in smartForm.SmartFormDetails.ToList())
        //    {
        //        SmartFormDetail targetsmartFormDetail = smartFormDetailRepository.Create();
        //        smartFormDetailRepository.Add(targetsmartFormDetail);
        //        targetsmartFormDetail.SmartFormId = targetsmartForm.SmartFormId;

        //        //string oldVariableName = objsmartFormDetails.FieldVariable != null ? objsmartFormDetails.FieldVariable.VariableName : "";

        //        //FieldVariable objNewFieldVariable = targetCompany.FieldVariables.Where(c => c.VariableName == oldVariableName).FirstOrDefault();

        //        //if(objNewFieldVariable != null)
        //        //{
        //        //    targetsmartFormDetail.FieldVariable = objNewFieldVariable;
        //        //    targetsmartFormDetail.VariableId = objNewFieldVariable != null ? objNewFieldVariable.VariableId : 0;

        //        //}


        //        targetsmartForm.SmartFormDetails.Add(targetsmartFormDetail);
        //        objsmartFormDetails.Clone(targetsmartFormDetail);
        //    }
        //}

        ///// <summary>
        ///// Copy smart form or its details
        ///// 
        ///// </summary>
        //private void CloneFieldVariables(Company source, Company target)
        //{
        //    if (source.FieldVariables == null)
        //    {
        //        return;
        //    }

        //    // Initialize List
        //    if (target.FieldVariables == null)
        //    {
        //        target.FieldVariables = new List<FieldVariable>();
        //    }

        //    foreach (FieldVariable companyFielVariables in source.FieldVariables)
        //    {
        //        FieldVariable targetfieldVariables = fieldVariableRepository.Create();
        //        fieldVariableRepository.Add(targetfieldVariables);
        //        targetfieldVariables.CompanyId = target.CompanyId;

        //        target.FieldVariables.Add(targetfieldVariables);
        //        companyFielVariables.Clone(targetfieldVariables);

        //        // Clone variable options
        //        if (companyFielVariables.VariableOptions == null)
        //        {
        //            continue;
        //        }

        //        // Clone smart form details
        //        CloneVariableOption(companyFielVariables, targetfieldVariables);




        //        // Clone scope variable
        //        if (companyFielVariables.ScopeVariables == null)
        //        {
        //            continue;
        //        }

        //        // Clone scope variable
        //        CloneScopeVariables(companyFielVariables, targetfieldVariables);


        //        // Clone template Variable
        //        if (companyFielVariables.TemplateVariables == null)
        //        {
        //            continue;
        //        }

        //        // Clone scope variable
        //        CloneTemplateVariables(companyFielVariables, targetfieldVariables);

        //        // Clone variable extension
        //        if (companyFielVariables.VariableExtensions == null)
        //        {
        //            continue;
        //        }

        //        // Clone variable extension
        //        CloneVariableExtension(companyFielVariables, targetfieldVariables);



        //    }



        //}


        //public void CloneVariableOption(FieldVariable fieldVariables, FieldVariable targetfieldVariables)
        //{
        //    if (targetfieldVariables.VariableOptions == null)
        //    {
        //        targetfieldVariables.VariableOptions = new List<VariableOption>();
        //    }

        //    foreach (VariableOption objvariableOptions in fieldVariables.VariableOptions.ToList())
        //    {
        //        VariableOption targetvariableOption = variableOptionRepository.Create();
        //        variableOptionRepository.Add(targetvariableOption);
        //        targetvariableOption.VariableId = targetfieldVariables.VariableId;
        //        targetfieldVariables.VariableOptions.Add(targetvariableOption);
        //        objvariableOptions.Clone(targetvariableOption);
        //    }
        //}



        //public void CloneScopeVariables(FieldVariable fieldVariables, FieldVariable targetfieldVariables)
        //{
        //    if (targetfieldVariables.ScopeVariables == null)
        //    {
        //        targetfieldVariables.ScopeVariables = new List<ScopeVariable>();
        //    }

        //    foreach (ScopeVariable objScopeVariable in fieldVariables.ScopeVariables.ToList())
        //    {
        //        ScopeVariable targetScopeVariable = scopeVariableRepository.Create();
        //        scopeVariableRepository.Add(targetScopeVariable);
        //        targetScopeVariable.VariableId = targetfieldVariables.VariableId;
        //        targetfieldVariables.ScopeVariables.Add(targetScopeVariable);
        //        objScopeVariable.Clone(targetScopeVariable);
        //    }
        //}

        //public void CloneTemplateVariables(FieldVariable fieldVariables, FieldVariable targetfieldVariables)
        //{
        //    if (targetfieldVariables.TemplateVariables == null)
        //    {
        //        targetfieldVariables.TemplateVariables = new List<MPC.Models.DomainModels.TemplateVariable>();
        //    }

        //    foreach (MPC.Models.DomainModels.TemplateVariable objtemplateVariable in fieldVariables.TemplateVariables.ToList())
        //    {
        //        MPC.Models.DomainModels.TemplateVariable targetTemplateVariable = templateVariableRepository.Create();
        //        templateVariableRepository.Add(targetTemplateVariable);
        //        targetTemplateVariable.VariableId = targetfieldVariables.VariableId;
        //        targetfieldVariables.TemplateVariables.Add(targetTemplateVariable);
        //        objtemplateVariable.Clone(targetTemplateVariable);
        //    }
        //}

        //public void CloneVariableExtension(FieldVariable fieldVariables, FieldVariable targetfieldVariables)
        //{
        //    if (targetfieldVariables.VariableExtensions == null)
        //    {
        //        targetfieldVariables.VariableExtensions = new List<MPC.Models.DomainModels.VariableExtension>();
        //    }

        //    foreach (MPC.Models.DomainModels.VariableExtension objVariableExtension in fieldVariables.VariableExtensions.ToList())
        //    {
        //        MPC.Models.DomainModels.VariableExtension targetVariableExtension = variableExtensionRespository.Create();
        //        variableExtensionRespository.Add(targetVariableExtension);
        //        targetVariableExtension.FieldVariableId = targetfieldVariables.VariableId;
        //        targetfieldVariables.VariableExtensions.Add(targetVariableExtension);
        //        objVariableExtension.Clone(targetVariableExtension);
        //    }
        //}

        ///// <summary>
        ///// Copy color palletes
        ///// </summary>
        //private void CloneActivities(Company source, Company target)
        //{
        //    if (source.Activities == null)
        //    {
        //        return;
        //    }

        //    // Initialize List
        //    if (target.Activities == null)
        //    {
        //        target.Activities = new List<Activity>();
        //    }

        //    foreach (Activity companyActivities in source.Activities)
        //    {

        //        Activity targetActivity = activityRepository.Create();
        //        activityRepository.Add(targetActivity);
        //        targetActivity.CompanyId = target.CompanyId;
        //        target.Activities.Add(targetActivity);
        //        companyActivities.Clone(targetActivity);



        //    }



        //}

        ///// <summary>
        ///// Copy template color styles
        ///// </summary>
        //private void CloneTemplateColorStyles(Company source, Company target)
        //{
        //    if (source.TemplateColorStyles == null)
        //    {
        //        return;
        //    }

        //    // Initialize List
        //    if (target.TemplateColorStyles == null)
        //    {
        //        target.TemplateColorStyles = new List<TemplateColorStyle>();
        //    }

        //    foreach (TemplateColorStyle templateColorStyles in source.TemplateColorStyles)
        //    {

        //        TemplateColorStyle targetColorStyle = templateColorStylesRepository.Create();
        //        templateColorStylesRepository.Add(targetColorStyle);
        //        targetColorStyle.CustomerId = target.CompanyId;
        //        target.TemplateColorStyles.Add(targetColorStyle);
        //        templateColorStyles.Clone(targetColorStyle);



        //    }



        //}

        ///// <summary>
        ///// Copy cms offer
        ///// </summary>
        //private void CloneCMSOffer(Company source, Company target)
        //{
        //    if (source.CmsOffers == null)
        //    {
        //        return;
        //    }

        //    // Initialize List
        //    if (target.CmsOffers == null)
        //    {
        //        target.CmsOffers = new List<CmsOffer>();
        //    }

        //    foreach (CmsOffer cmsOffer in source.CmsOffers)
        //    {
        //        CmsOffer targetCMSOffer = cmsofferRepository.Create();
        //        cmsofferRepository.Add(targetCMSOffer);
        //        targetCMSOffer.CompanyId = target.CompanyId;
        //        target.CmsOffers.Add(targetCMSOffer);
        //        cmsOffer.Clone(targetCMSOffer);



        //    }
        //}
        ///// <summary>
        ///// Copy media library
        ///// </summary>
        //private void CloneMediaLibrary(Company source, Company target)
        //{
        //    if (source.MediaLibraries == null)
        //    {
        //        return;
        //    }

        //    // Initialize List
        //    if (target.MediaLibraries == null)
        //    {
        //        target.MediaLibraries = new List<MediaLibrary>();
        //    }

        //    foreach (MediaLibrary mediaLibrary in source.MediaLibraries)
        //    {
        //        MediaLibrary targetMediaLibrary = mediaLibraryRepository.Create();
        //        mediaLibraryRepository.Add(targetMediaLibrary);
        //        targetMediaLibrary.CompanyId = target.CompanyId;
        //        target.MediaLibraries.Add(targetMediaLibrary);
        //        mediaLibrary.Clone(targetMediaLibrary);



        //    }
        //}

        //// clone discount vouchers
        //public void CloneDiscountVouchers(long OldCompanyid, long NewCompanyId)
        //{
        //    List<DiscountVoucher> discountVouchers = discountVoucherRepository.getDiscountVouchersByCompanyId(OldCompanyid);

        //    if (discountVouchers != null && discountVouchers.Count > 0)
        //    {
        //        foreach (var voucher in discountVouchers)
        //        {
        //            DiscountVoucher targetDiscountVoucher = discountVoucherRepository.Create();
        //            targetDiscountVoucher = voucher;
        //            targetDiscountVoucher.CompanyId = NewCompanyId;
        //            Guid g;
        //            // Create and display the value of two GUIDs.
        //            g = Guid.NewGuid();


        //            targetDiscountVoucher.VoucherCode = g.ToString();
        //            discountVoucherRepository.Add(targetDiscountVoucher);

        //            if (voucher.ProductCategoryVouchers != null && voucher.ProductCategoryVouchers.Count > 0)
        //            {
        //                foreach (var pcv in voucher.ProductCategoryVouchers)
        //                {
        //                    ProductCategoryVoucher objPCV = productcategoryvoucherRepository.Create();
        //                    objPCV = pcv;
        //                    objPCV.DiscountVoucher = targetDiscountVoucher;
        //                    objPCV.VoucherId = targetDiscountVoucher.DiscountVoucherId;

        //                    productcategoryvoucherRepository.Add(objPCV);

        //                }
        //            }

        //            if (voucher.ItemsVouchers != null && voucher.ItemsVouchers.Count > 0)
        //            {
        //                foreach (var iv in voucher.ItemsVouchers)
        //                {
        //                    ItemsVoucher objIV = itemsVoucherRepository.Create();
        //                    objIV = iv;
        //                    objIV.DiscountVoucher = targetDiscountVoucher;
        //                    objIV.VoucherId = targetDiscountVoucher.DiscountVoucherId;

        //                    itemsVoucherRepository.Add(objIV);

        //                }
        //            }

        //        }
        //    }


        //}

        //// clone discount vouchers
        //public void CloneTemplateFonts(long OldCompanyid, long NewCompanyId)
        //{
        //    List<TemplateFont> fonts = templatefonts.getTemplateFontsByCompanyID(OldCompanyid);

        //    if (fonts != null && fonts.Count > 0)
        //    {
        //        foreach (var font in fonts)
        //        {
        //            TemplateFont templateFont = templatefonts.Create();
        //            templateFont = font;
        //            templateFont.CustomerId = NewCompanyId;
        //            templatefonts.Add(templateFont);
        //        }
        //    }


        //}

        //// clone report banners
        //public void CloneReportBanners(long OldCompanyid, long NewCompanyId)
        //{

        //    List<ReportNote> reportNotes = reportNoteRepository.GetReportNotesByCompanyId(OldCompanyid);

        //    if (reportNotes != null && reportNotes.Count > 0)
        //    {
        //        foreach (var note in reportNotes)
        //        {
        //            ReportNote reportNote = reportNoteRepository.Create();
        //            reportNote = note;
        //            reportNote.CompanyId = NewCompanyId;
        //            if (!string.IsNullOrEmpty(note.ReportBanner))
        //            {

        //                // update reportbanner path
        //                string name = Path.GetFileName(note.ReportBanner);
        //                string BannerPath = "MPC_Content/Reports/Banners/" + companyRepository.OrganisationId + "/" + NewCompanyId + "/" + name;



        //                string DestinationBannerPath = HttpContext.Current.Server.MapPath("~/MPC_Content/Reports/Banners/" + companyRepository.OrganisationId + "/" + NewCompanyId + "/" + name);

        //                string DestinationBannerDirectory = HttpContext.Current.Server.MapPath("~/MPC_Content/Reports/Banners/" + companyRepository.OrganisationId + "/" + NewCompanyId);
        //                string BannerSourcePath = HttpContext.Current.Server.MapPath("~/" + note.ReportBanner);
        //                if (!System.IO.Directory.Exists(DestinationBannerDirectory))
        //                {
        //                    Directory.CreateDirectory(DestinationBannerDirectory);
        //                    if (Directory.Exists(DestinationBannerDirectory))
        //                    {
        //                        if (File.Exists(BannerSourcePath))
        //                        {
        //                            if (!File.Exists(DestinationBannerPath))
        //                                File.Copy(BannerSourcePath, DestinationBannerPath);
        //                        }


        //                    }

        //                }
        //                else
        //                {
        //                    if (File.Exists(BannerSourcePath))
        //                    {
        //                        if (!File.Exists(DestinationBannerPath))
        //                            File.Copy(BannerSourcePath, DestinationBannerPath);

        //                    }
        //                }


        //                reportNote.ReportBanner = BannerPath;
        //            }


        //            reportNoteRepository.Add(reportNote);

        //            // 
        //        }
        //    }


        //}

        //public void SetValuesAfterClone(Company company, string OldSelectedSetName, long OldcompanyId)
        //{
        //    // set active banner set id in company

        //    company.ActiveBannerSetId = company.CompanyBannerSets.Where(c => c.SetName == OldSelectedSetName).Select(c => c.CompanySetId).FirstOrDefault();

        //    // set pickupaddress in company
        //    if (company.PickupAddressId > 0)
        //    {
        //        long NewId = company.Addresses.Where(c => c.Tel2 == Convert.ToString(company.PickupAddressId)).Select(c => c.AddressId).FirstOrDefault();

        //        company.PickupAddressId = NewId;


        //    }

        //    // set ids of contact , address and store in scopevariables

        //    IEnumerable<CompanyTerritory> companyTerritory = companyTerritoryRepository.GetAllCompanyTerritories(OldcompanyId);
        //    if (company.FieldVariables != null && company.FieldVariables.Count > 0)
        //    {
        //        foreach (var fv in company.FieldVariables)
        //        {
        //            if (fv.ScopeVariables != null && fv.ScopeVariables.Count > 0)
        //            {
        //                foreach (var sv in fv.ScopeVariables)
        //                {
        //                    // store
        //                    if (sv.Scope == 1)
        //                    {
        //                        sv.Id = company.CompanyId;
        //                    }
        //                    // contact
        //                    if (sv.Scope == 2)
        //                    {
        //                        long NewId = company.CompanyContacts.Where(c => c.quickAddress3 == Convert.ToString(sv.Id)).Select(c => c.ContactId).FirstOrDefault();
        //                        sv.Id = NewId;
        //                    }
        //                    // addresses
        //                    if (sv.Scope == 3)
        //                    {
        //                        long NewId = company.Addresses.Where(c => c.Tel2 == Convert.ToString(sv.Id)).Select(c => c.AddressId).FirstOrDefault();
        //                        sv.Id = NewId;
        //                    }
        //                    // territory
        //                    if (sv.Scope == 4)
        //                    {
        //                        string TerritoryName = companyTerritory.Where(c => c.TerritoryId == sv.Id).Select(c => c.TerritoryName).FirstOrDefault();
        //                        if (!string.IsNullOrEmpty(TerritoryName))
        //                        {
        //                            long nEWiD = company.CompanyTerritories.Where(c => c.TerritoryName == TerritoryName).Select(c => c.TerritoryId).FirstOrDefault();
        //                            sv.Id = nEWiD;
        //                        }

        //                    }
        //                }
        //            }
        //            if (fv.VariableExtensions != null && fv.VariableExtensions.Count > 0)
        //            {
        //                foreach (var ve in fv.VariableExtensions)
        //                {
        //                    ve.CompanyId = (int)company.CompanyId;
        //                }
        //            }
        //        }
        //    }
        //    // set itemid in cmsoffer
        //    if (company.CmsOffers != null && company.CmsOffers.Count > 0)
        //    {
        //        foreach (var offer in company.CmsOffers)
        //        {
        //            offer.ItemId = company.Items.Where(c => c.Tax3 == offer.ItemId).Select(c => (int)c.ItemId).FirstOrDefault();
        //        }
        //    }

        //    // set parent category id in productcategories
        //    if (company.ProductCategories != null && company.ProductCategories.Count > 0)
        //    {
        //        foreach (var item in company.ProductCategories)
        //        {
        //            if (item.ParentCategoryId > 0) // 11859
        //            {


        //                //  string scat = item.Description2;
        //                var pCat = company.ProductCategories.Where(g => g.ContentType.Contains(item.ParentCategoryId.Value.ToString())).FirstOrDefault();
        //                if (pCat != null)
        //                {
        //                    item.ParentCategoryId = Convert.ToInt32(pCat.ProductCategoryId);

        //                }
        //            }

        //            //if (item.ProductCategoryItems != null && item.ProductCategoryItems.Count > 0)
        //            //{
        //            //    foreach (var pci in item.ProductCategoryItems)
        //            //    {
        //            //        if (company.Items != null && company.Items.Count > 0)
        //            //        {
        //            //            long PID = company.Items.Where(c => c.Tax3 == pci.ItemId).Select(x => x.ItemId).FirstOrDefault();
        //            //            if (PID > 0)
        //            //            {
        //            //                pci.ItemId = PID;
        //            //            }
        //            //            else
        //            //            {
        //            //                // PID = stockitems.Select(s => s.StockItemId).FirstOrDefault();
        //            //                pci.ItemId = null;


        //            //            }
        //            //        }

        //            //    }
        //            //}



        //        }






        //    }




        //    // copy templates in items

        //    if (company.Items != null && company.Items.Count > 0)
        //    {
        //        foreach (var item in company.Items)
        //        {
        //            if (item.TemplateId.HasValue)
        //            {
        //                long templateId = templateService.CopyTemplate(item.TemplateId.Value, 0, string.Empty, item.OrganisationId.HasValue ?
        //                    item.OrganisationId.Value : itemRepository.OrganisationId);

        //                item.TemplateId = templateId;
        //            }


        //        }
        //    }


        //}

        //public void CopyCompanyFiles(Company ObjCompany, long OldCompanyID)
        //{
        //    List<string> DestinationsPath = new List<string>();

        //    // new CompanyId
        //    long oCID = ObjCompany.CompanyId;


        //    // company logo
        //    string CompanyPathOld = string.Empty;
        //    string CompanylogoPathNew = string.Empty;
        //    if (ObjCompany.Image != null)
        //    {
        //        CompanyPathOld = Path.GetFileName(ObjCompany.Image);

        //        CompanylogoPathNew = CompanyPathOld.Replace(OldCompanyID + "_", ObjCompany.CompanyId + "_");

        //        string DestinationCompanyLogoFilePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + ObjCompany.CompanyId + "/" + CompanylogoPathNew);
        //        DestinationsPath.Add(DestinationCompanyLogoFilePath);
        //        string DestinationCompanyLogoDirectory = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + ObjCompany.CompanyId);
        //        string CompanyLogoSourcePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + OldCompanyID + "/" + CompanyPathOld);
        //        if (!System.IO.Directory.Exists(DestinationCompanyLogoDirectory))
        //        {
        //            Directory.CreateDirectory(DestinationCompanyLogoDirectory);
        //            if (Directory.Exists(DestinationCompanyLogoDirectory))
        //            {
        //                if (File.Exists(CompanyLogoSourcePath))
        //                {
        //                    if (!File.Exists(DestinationCompanyLogoFilePath))
        //                        File.Copy(CompanyLogoSourcePath, DestinationCompanyLogoFilePath);
        //                }


        //            }


        //        }
        //        else
        //        {
        //            if (File.Exists(CompanyLogoSourcePath))
        //            {
        //                if (!File.Exists(DestinationCompanyLogoFilePath))
        //                    File.Copy(CompanyLogoSourcePath, DestinationCompanyLogoFilePath);
        //            }
        //        }
        //        ObjCompany.Image = "MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + ObjCompany.CompanyId + "/" + CompanylogoPathNew;
        //    }


        //    // copy store background image
        //    if (ObjCompany.StoreBackgroundImage != null)
        //    {
        //        CompanyPathOld = Path.GetFileName(ObjCompany.StoreBackgroundImage);

        //        CompanylogoPathNew = CompanyPathOld.Replace(OldCompanyID + "_", ObjCompany.CompanyId + "_");

        //        string DestinationCompanyBackgroundFilePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + oCID + "/" + CompanylogoPathNew);
        //        DestinationsPath.Add(DestinationCompanyBackgroundFilePath);
        //        string DestinationCompanyBackgroundDirectory = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + oCID);
        //        string CompanyLogoSourcePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + OldCompanyID + "/" + CompanyPathOld);
        //        if (!System.IO.Directory.Exists(DestinationCompanyBackgroundDirectory))
        //        {
        //            Directory.CreateDirectory(DestinationCompanyBackgroundDirectory);
        //            if (Directory.Exists(DestinationCompanyBackgroundDirectory))
        //            {
        //                if (File.Exists(CompanyLogoSourcePath))
        //                {
        //                    if (!File.Exists(DestinationCompanyBackgroundFilePath))
        //                        File.Copy(CompanyLogoSourcePath, DestinationCompanyBackgroundFilePath);
        //                }


        //            }


        //        }
        //        else
        //        {
        //            if (File.Exists(CompanyLogoSourcePath))
        //            {
        //                if (!File.Exists(DestinationCompanyBackgroundFilePath))
        //                    File.Copy(CompanyLogoSourcePath, DestinationCompanyBackgroundFilePath);
        //            }
        //        }
        //        ObjCompany.StoreBackgroundImage = "MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + oCID + "/" + CompanylogoPathNew;
        //    }

        //    // copy company contacts image
        //    if (ObjCompany.CompanyContacts != null && ObjCompany.CompanyContacts.Count > 0)
        //    {
        //        foreach (var contact in ObjCompany.CompanyContacts)
        //        {
        //            string OldContactImage = string.Empty;
        //            string NewContactImage = string.Empty;
        //            string OldContactID = string.Empty;
        //            if (contact.image != null)
        //            {
        //                string name = Path.GetFileName(contact.image);
        //                string[] SplitMain = name.Split('_');
        //                if (SplitMain[0] != string.Empty)
        //                {
        //                    OldContactID = SplitMain[0];

        //                }

        //                OldContactImage = Path.GetFileName(contact.image);
        //                NewContactImage = OldContactImage.Replace(OldContactID + "_", contact.ContactId + "_");

        //                string DestinationContactFilesPath = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + oCID + "/Contacts/" + "/" + NewContactImage);
        //                DestinationsPath.Add(DestinationContactFilesPath);
        //                string DestinationContactFilesDirectory = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + oCID + "/Contacts");
        //                string ContactFilesSourcePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + OldCompanyID + "/Contacts/" + OldContactImage);
        //                if (!System.IO.Directory.Exists(DestinationContactFilesDirectory))
        //                {
        //                    Directory.CreateDirectory(DestinationContactFilesDirectory);
        //                    if (Directory.Exists(DestinationContactFilesDirectory))
        //                    {
        //                        if (File.Exists(ContactFilesSourcePath))
        //                        {
        //                            if (!File.Exists(DestinationContactFilesPath))
        //                                File.Copy(ContactFilesSourcePath, DestinationContactFilesPath);
        //                        }


        //                    }



        //                }
        //                else
        //                {
        //                    if (File.Exists(ContactFilesSourcePath))
        //                    {
        //                        if (!File.Exists(DestinationContactFilesPath))
        //                            File.Copy(ContactFilesSourcePath, DestinationContactFilesPath);
        //                    }

        //                }
        //                contact.image = "/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + oCID + "/Contacts/" + NewContactImage;
        //            }
        //        }
        //    }
        //    Dictionary<string, string> dictionaryMediaIds = new Dictionary<string, string>();
        //    // copy Media libraries
        //    if (ObjCompany.MediaLibraries != null && ObjCompany.MediaLibraries.Count > 0)
        //    {
        //        foreach (var media in ObjCompany.MediaLibraries)
        //        {
        //            string OldMediaFilePath = string.Empty;
        //            string NewMediaFilePath = string.Empty;
        //            string OldMediaID = string.Empty;
        //            string NewMediaID = string.Empty;
        //            if (media.FilePath != null)
        //            {
        //                string name = Path.GetFileName(media.FilePath);
        //                string[] SplitMain = name.Split('_');
        //                if (SplitMain[0] != string.Empty)
        //                {
        //                    OldMediaID = SplitMain[0];

        //                }



        //                if (media.MediaId > 0)
        //                    NewMediaID = Convert.ToString(media.MediaId);

        //                dictionaryMediaIds.Add(OldMediaID, NewMediaID);

        //                // DestinationsPath.Add(OldMediaID, NewMediaID);

        //                OldMediaFilePath = Path.GetFileName(media.FilePath);
        //                NewMediaFilePath = OldMediaFilePath.Replace(OldMediaID + "_", media.MediaId + "_");

        //                string DestinationMediaFilesPath = HttpContext.Current.Server.MapPath("~/MPC_Content/Media/" + companyRepository.OrganisationId + "/" + oCID + "/" + NewMediaFilePath);
        //                DestinationsPath.Add(DestinationMediaFilesPath);
        //                string DestinationMediaFilesDirectory = HttpContext.Current.Server.MapPath("~/MPC_Content/Media/" + companyRepository.OrganisationId + "/" + oCID);
        //                string MediaFilesSourcePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Media/" + companyRepository.OrganisationId + "/" + OldCompanyID + "/" + OldMediaFilePath);
        //                if (!System.IO.Directory.Exists(DestinationMediaFilesDirectory))
        //                {
        //                    Directory.CreateDirectory(DestinationMediaFilesDirectory);
        //                    if (Directory.Exists(DestinationMediaFilesDirectory))
        //                    {
        //                        if (File.Exists(MediaFilesSourcePath))
        //                        {
        //                            if (!File.Exists(DestinationMediaFilesPath))
        //                                File.Copy(MediaFilesSourcePath, DestinationMediaFilesPath);
        //                        }


        //                    }



        //                }
        //                else
        //                {
        //                    if (File.Exists(MediaFilesSourcePath))
        //                    {
        //                        if (!File.Exists(DestinationMediaFilesPath))
        //                            File.Copy(MediaFilesSourcePath, DestinationMediaFilesPath);
        //                    }

        //                }
        //                media.FilePath = "MPC_Content/Media/" + companyRepository.OrganisationId + "/" + oCID + "/" + NewMediaFilePath;
        //            }

        //        }
        //    }

        //    // copy compay banner and banner set
        //    if (ObjCompany.CompanyBannerSets != null && ObjCompany.CompanyBannerSets.Count > 0)
        //    {
        //        foreach (var sets in ObjCompany.CompanyBannerSets)
        //        {


        //            if (sets.CompanyBanners != null && sets.CompanyBanners.Count > 0)
        //            {
        //                foreach (var bann in sets.CompanyBanners)
        //                {
        //                    if (!string.IsNullOrEmpty(bann.ImageURL))
        //                    {
        //                        string OldMediaID = string.Empty;
        //                        string newMediaID = string.Empty;
        //                        string name = Path.GetFileName(bann.ImageURL);
        //                        string[] SplitMain = name.Split('_');

        //                        if (SplitMain != null)
        //                        {
        //                            if (SplitMain[0] != string.Empty)
        //                            {
        //                                OldMediaID = SplitMain[0];

        //                            }
        //                        }

        //                        if (dictionaryMediaIds != null && dictionaryMediaIds.Count > 0)
        //                        {
        //                            var dec = dictionaryMediaIds.Where(s => s.Key == OldMediaID).Select(s => s.Value).FirstOrDefault();
        //                            if (dec != null)
        //                            {
        //                                newMediaID = dec.ToString();
        //                            }
        //                        }


        //                        string NewBannerPath = name.Replace(OldMediaID + "_", newMediaID + "_");

        //                        bann.ImageURL = "/MPC_Content/Media/" + companyRepository.OrganisationId + "/" + oCID + "/" + NewBannerPath;
        //                    }
        //                }
        //            }

        //        }
        //    }
        //    if (ObjCompany.CmsPages != null && ObjCompany.CmsPages.Count > 0)
        //    {
        //        foreach (var pages in ObjCompany.CmsPages)
        //        {
        //            if (!string.IsNullOrEmpty(pages.PageBanner))
        //            {
        //                string OldMediaID = string.Empty;
        //                string newMediaID = string.Empty;
        //                string name = Path.GetFileName(pages.PageBanner);

        //                string[] SplitMain = name.Split('_');

        //                if (SplitMain != null)
        //                {
        //                    if (SplitMain[0] != string.Empty)
        //                    {
        //                        OldMediaID = SplitMain[0];

        //                    }
        //                }

        //                if (dictionaryMediaIds != null && dictionaryMediaIds.Count > 0)
        //                {
        //                    var dec = dictionaryMediaIds.Where(s => s.Key == OldMediaID).Select(s => s.Value).FirstOrDefault();
        //                    if (dec != null)
        //                    {
        //                        newMediaID = dec.ToString();
        //                    }
        //                }


        //                string newCMSPageName = name.Replace(OldMediaID + "_", newMediaID + "_");


        //                pages.PageBanner = "/MPC_Content/Media/" + companyRepository.OrganisationId + "/" + oCID + "/" + newCMSPageName;
        //            }

        //        }
        //    }
        //    if (ObjCompany.ProductCategories != null && ObjCompany.ProductCategories.Count > 0)
        //    {
        //        foreach (var prodCat in ObjCompany.ProductCategories)
        //        {
        //            string ProdCatID = string.Empty;
        //            string CatName = string.Empty;

        //            if (!string.IsNullOrEmpty(prodCat.ThumbnailPath))
        //            {
        //                string OldThumbnailPath = string.Empty;
        //                string NewThumbnailPath = string.Empty;

        //                string name = Path.GetFileName(prodCat.ThumbnailPath);
        //                string[] SplitMain = name.Split('_');
        //                if (SplitMain[1] != string.Empty)
        //                {
        //                    ProdCatID = SplitMain[1];

        //                }

        //                OldThumbnailPath = Path.GetFileName(prodCat.ThumbnailPath);
        //                NewThumbnailPath = OldThumbnailPath.Replace(ProdCatID + "_", prodCat.ProductCategoryId + "_");



        //                string DestinationThumbPathCat = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + oCID + "/ProductCategories/" + NewThumbnailPath);
        //                DestinationsPath.Add(DestinationThumbPathCat);
        //                string DestinationThumbDirectory = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + oCID + "/ProductCategories");
        //                string ThumbSourcePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + OldCompanyID + "/ProductCategories/" + OldThumbnailPath);
        //                if (!System.IO.Directory.Exists(DestinationThumbDirectory))
        //                {
        //                    Directory.CreateDirectory(DestinationThumbDirectory);
        //                    if (Directory.Exists(DestinationThumbDirectory))
        //                    {
        //                        if (File.Exists(ThumbSourcePath))
        //                        {
        //                            if (!File.Exists(DestinationThumbPathCat))
        //                                File.Copy(ThumbSourcePath, DestinationThumbPathCat);
        //                        }


        //                    }

        //                }
        //                else
        //                {
        //                    if (File.Exists(ThumbSourcePath))
        //                    {
        //                        if (!File.Exists(DestinationThumbPathCat))
        //                            File.Copy(ThumbSourcePath, DestinationThumbPathCat);
        //                    }

        //                }
        //                prodCat.ThumbnailPath = "MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + oCID + "/ProductCategories/" + NewThumbnailPath;
        //            }

        //            if (!string.IsNullOrEmpty(prodCat.ImagePath))
        //            {
        //                string OldImagePath = string.Empty;
        //                string NewImagePath = string.Empty;

        //                string name = Path.GetFileName(prodCat.ImagePath);
        //                string[] SplitMain = name.Split('_');
        //                if (SplitMain[1] != string.Empty)
        //                {
        //                    ProdCatID = SplitMain[1];

        //                }

        //                OldImagePath = Path.GetFileName(prodCat.ImagePath);
        //                NewImagePath = OldImagePath.Replace(ProdCatID + "_", prodCat.ProductCategoryId + "_");

        //                string DestinationImagePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + oCID + "/ProductCategories/" + NewImagePath);
        //                DestinationsPath.Add(DestinationImagePath);
        //                string DestinationImageDirectory = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + oCID + "/ProductCategories");
        //                string ImageSourcePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + OldCompanyID + "/ProductCategories/" + OldImagePath);

        //                if (!System.IO.Directory.Exists(DestinationImageDirectory))
        //                {
        //                    Directory.CreateDirectory(DestinationImageDirectory);
        //                    if (Directory.Exists(DestinationImageDirectory))
        //                    {
        //                        if (File.Exists(ImageSourcePath))
        //                        {
        //                            if (!File.Exists(DestinationImagePath))
        //                                File.Copy(ImageSourcePath, DestinationImagePath);
        //                        }


        //                    }

        //                }
        //                else
        //                {
        //                    if (File.Exists(ImageSourcePath))
        //                    {
        //                        if (!File.Exists(DestinationImagePath))
        //                            File.Copy(ImageSourcePath, DestinationImagePath);
        //                    }

        //                }
        //                prodCat.ImagePath = "MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + oCID + "/ProductCategories/" + NewImagePath;
        //            }


        //        }
        //    }

        //    // copy item images

        //    if (ObjCompany.Items != null && ObjCompany.Items.Count > 0)
        //    {
        //        string ItemID = string.Empty;
        //        string ItemName = string.Empty;
        //        foreach (var item in ObjCompany.Items)
        //        {
        //            // thumbnail images
        //            if (!string.IsNullOrEmpty(item.ThumbnailPath))
        //            {
        //                string OldThumbnailPath = string.Empty;
        //                string NewThumbnailPath = string.Empty;

        //                string name = Path.GetFileName(item.ThumbnailPath);
        //                string[] SplitMain = name.Split('_');
        //                if (SplitMain != null)
        //                {
        //                    if (SplitMain[1] != string.Empty)
        //                    {
        //                        ItemID = SplitMain[1];

        //                    }
        //                    int i = 0;
        //                    // string s = "108";
        //                    bool result = int.TryParse(ItemID, out i);
        //                    if (!result)
        //                    {
        //                        ItemID = SplitMain[0];
        //                    }
        //                }
        //                OldThumbnailPath = Path.GetFileName(item.ThumbnailPath);
        //                NewThumbnailPath = OldThumbnailPath.Replace(ItemID + "_", item.ItemId + "_");


        //                string DestinationThumbnailPath = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + companyRepository.OrganisationId + "/" + item.ItemId + "/" + NewThumbnailPath);
        //                DestinationsPath.Add(DestinationThumbnailPath);
        //                string DestinationThumbnailDirectory = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + companyRepository.OrganisationId + "/" + item.ItemId);
        //                string ThumbnailSourcePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + companyRepository.OrganisationId + "/" + ItemID + "/" + OldThumbnailPath);
        //                if (!System.IO.Directory.Exists(DestinationThumbnailDirectory))
        //                {
        //                    Directory.CreateDirectory(DestinationThumbnailDirectory);
        //                    if (Directory.Exists(DestinationThumbnailDirectory))
        //                    {
        //                        if (File.Exists(ThumbnailSourcePath))
        //                        {
        //                            if (!File.Exists(DestinationThumbnailPath))
        //                                File.Copy(ThumbnailSourcePath, DestinationThumbnailPath);
        //                        }


        //                    }

        //                }
        //                else
        //                {
        //                    if (File.Exists(ThumbnailSourcePath))
        //                    {
        //                        if (!File.Exists(DestinationThumbnailPath))
        //                            File.Copy(ThumbnailSourcePath, DestinationThumbnailPath);
        //                    }

        //                }
        //                item.ThumbnailPath = "MPC_Content/Products/" + companyRepository.OrganisationId + "/" + item.ItemId + "/" + NewThumbnailPath;
        //            }

        //            // main image
        //            if (!string.IsNullOrEmpty(item.ImagePath))
        //            {

        //                string OldImagePath = string.Empty;
        //                string NewImagePath = string.Empty;


        //                string name = Path.GetFileName(item.ImagePath);
        //                string[] SplitMain = name.Split('_');
        //                if (SplitMain != null)
        //                {
        //                    if (SplitMain[1] != string.Empty)
        //                    {
        //                        ItemID = SplitMain[1];

        //                    }
        //                    int i = 0;
        //                    // string s = "108";
        //                    bool result = int.TryParse(ItemID, out i);
        //                    if (!result)
        //                    {
        //                        ItemID = SplitMain[0];
        //                    }
        //                }

        //                OldImagePath = Path.GetFileName(item.ImagePath);
        //                NewImagePath = OldImagePath.Replace(ItemID + "_", item.ItemId + "_");


        //                string DestinationImagePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + companyRepository.OrganisationId + "/" + item.ItemId + "/" + NewImagePath);
        //                DestinationsPath.Add(DestinationImagePath);
        //                string DestinationImageDirectory = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + companyRepository.OrganisationId + "/" + item.ItemId);
        //                string ImageSourcePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + companyRepository.OrganisationId + "/" + ItemID + "/" + OldImagePath);
        //                if (!System.IO.Directory.Exists(DestinationImageDirectory))
        //                {
        //                    Directory.CreateDirectory(DestinationImageDirectory);
        //                    if (Directory.Exists(DestinationImageDirectory))
        //                    {
        //                        if (File.Exists(ImageSourcePath))
        //                        {
        //                            if (!File.Exists(DestinationImagePath))
        //                                File.Copy(ImageSourcePath, DestinationImagePath);
        //                        }


        //                    }

        //                }
        //                else
        //                {
        //                    if (File.Exists(ImageSourcePath))
        //                    {
        //                        if (!File.Exists(DestinationImagePath))
        //                            File.Copy(ImageSourcePath, DestinationImagePath);
        //                    }

        //                }
        //                item.ImagePath = "MPC_Content/Products/" + companyRepository.OrganisationId + "/" + item.ItemId + "/" + NewImagePath;
        //            }

        //            // Gird image
        //            if (!string.IsNullOrEmpty(item.GridImage))
        //            {
        //                string OldGridPath = string.Empty;
        //                string NewGridPath = string.Empty;

        //                string name = Path.GetFileName(item.GridImage);
        //                string[] SplitMain = name.Split('_');
        //                if (SplitMain[0] != string.Empty)
        //                {
        //                    ItemID = SplitMain[0];

        //                }
        //                int i = 0;
        //                // string s = "108";
        //                bool result = int.TryParse(ItemID, out i);
        //                if (!result)
        //                {
        //                    ItemID = SplitMain[1];
        //                }

        //                OldGridPath = Path.GetFileName(item.GridImage);
        //                NewGridPath = OldGridPath.Replace(ItemID + "_", item.ItemId + "_");

        //                string DestinationGridPath = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + companyRepository.OrganisationId + "/" + item.ItemId + "/" + NewGridPath);
        //                DestinationsPath.Add(DestinationGridPath);
        //                string DestinationGridDirectory = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + companyRepository.OrganisationId + "/" + item.ItemId);
        //                string GridSourcePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + companyRepository.OrganisationId + "/" + ItemID + "/" + OldGridPath);
        //                if (!System.IO.Directory.Exists(DestinationGridDirectory))
        //                {
        //                    Directory.CreateDirectory(DestinationGridDirectory);
        //                    if (Directory.Exists(DestinationGridDirectory))
        //                    {
        //                        if (File.Exists(GridSourcePath))
        //                        {
        //                            if (!File.Exists(DestinationGridPath))
        //                                File.Copy(GridSourcePath, DestinationGridPath);
        //                        }


        //                    }

        //                }
        //                else
        //                {
        //                    if (File.Exists(GridSourcePath))
        //                    {
        //                        if (!File.Exists(DestinationGridPath))
        //                            File.Copy(GridSourcePath, DestinationGridPath);

        //                    }
        //                }
        //                item.GridImage = "MPC_Content/Products/" + companyRepository.OrganisationId + "/" + item.ItemId + "/" + NewGridPath;
        //            }

        //            // file 1
        //            if (!string.IsNullOrEmpty(item.File1))
        //            {
        //                string OldF1Path = string.Empty;
        //                string NewF1Path = string.Empty;

        //                string name = Path.GetFileName(item.File1);
        //                string[] SplitMain = name.Split('_');
        //                if (SplitMain[0] != string.Empty)
        //                {
        //                    ItemID = SplitMain[0];

        //                }

        //                OldF1Path = Path.GetFileName(item.File1);
        //                NewF1Path = OldF1Path.Replace(ItemID + "_", item.ItemId + "_");

        //                string DestinationFile1Path = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + companyRepository.OrganisationId + "/" + item.ItemId + "/" + NewF1Path);
        //                DestinationsPath.Add(DestinationFile1Path);
        //                string DestinationFile1Directory = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + companyRepository.OrganisationId + "/" + item.ItemId);
        //                string File1SourcePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + companyRepository.OrganisationId + "/" + ItemID + "/" + OldF1Path);
        //                if (!System.IO.Directory.Exists(DestinationFile1Directory))
        //                {
        //                    Directory.CreateDirectory(DestinationFile1Directory);
        //                    if (Directory.Exists(DestinationFile1Directory))
        //                    {
        //                        if (File.Exists(File1SourcePath))
        //                        {
        //                            if (!File.Exists(DestinationFile1Path))
        //                                File.Copy(File1SourcePath, DestinationFile1Path);
        //                        }


        //                    }

        //                }
        //                else
        //                {
        //                    if (File.Exists(File1SourcePath))
        //                    {
        //                        if (!File.Exists(DestinationFile1Path))
        //                            File.Copy(File1SourcePath, DestinationFile1Path);
        //                    }

        //                }
        //                item.File1 = "MPC_Content/Products/" + companyRepository.OrganisationId + "/" + item.ItemId + "/" + NewF1Path;

        //            }

        //            // file 2
        //            if (!string.IsNullOrEmpty(item.File2))
        //            {
        //                string OldF2Path = string.Empty;
        //                string NewF2Path = string.Empty;

        //                string name = Path.GetFileName(item.File2);

        //                string[] SplitMain = name.Split('_');
        //                if (SplitMain[0] != string.Empty)
        //                {
        //                    ItemID = SplitMain[0];

        //                }

        //                OldF2Path = Path.GetFileName(item.File2);
        //                NewF2Path = OldF2Path.Replace(ItemID + "_", item.ItemId + "_");

        //                string DestinationFile2Path = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + companyRepository.OrganisationId + "/" + item.ItemId + "/" + NewF2Path);
        //                DestinationsPath.Add(DestinationFile2Path);
        //                string DestinationFile2Directory = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + companyRepository.OrganisationId + "/" + item.ItemId);
        //                string File2SourcePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + companyRepository.OrganisationId + "/" + ItemID + "/" + OldF2Path);
        //                if (!System.IO.Directory.Exists(DestinationFile2Directory))
        //                {
        //                    Directory.CreateDirectory(DestinationFile2Directory);
        //                    if (Directory.Exists(DestinationFile2Directory))
        //                    {
        //                        if (File.Exists(File2SourcePath))
        //                        {
        //                            if (!File.Exists(DestinationFile2Path))
        //                                File.Copy(File2SourcePath, DestinationFile2Path);
        //                        }


        //                    }

        //                }
        //                else
        //                {
        //                    if (File.Exists(File2SourcePath))
        //                    {
        //                        if (!File.Exists(DestinationFile2Path))
        //                            File.Copy(File2SourcePath, DestinationFile2Path);
        //                    }

        //                }
        //                item.File2 = "MPC_Content/Products/" + companyRepository.OrganisationId + "/" + item.ItemId + "/" + NewF2Path;
        //            }

        //            // file 3
        //            if (!string.IsNullOrEmpty(item.File3))
        //            {
        //                string OldF3Path = string.Empty;
        //                string NewF3Path = string.Empty;

        //                string name = Path.GetFileName(item.File3);
        //                string[] SplitMain = name.Split('_');
        //                if (SplitMain[0] != string.Empty)
        //                {
        //                    ItemID = SplitMain[0];

        //                }

        //                OldF3Path = Path.GetFileName(item.File3);
        //                NewF3Path = OldF3Path.Replace(ItemID + "_", item.ItemId + "_");

        //                string DestinationFil3Path = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + companyRepository.OrganisationId + "/" + item.ItemId + "/" + NewF3Path);
        //                DestinationsPath.Add(DestinationFil3Path);
        //                string DestinationFile3Directory = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + companyRepository.OrganisationId + "/" + item.ItemId);
        //                string File3SourcePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + companyRepository.OrganisationId + "/" + ItemID + "/" + OldF3Path);
        //                if (!System.IO.Directory.Exists(DestinationFile3Directory))
        //                {
        //                    Directory.CreateDirectory(DestinationFile3Directory);
        //                    if (Directory.Exists(DestinationFile3Directory))
        //                    {
        //                        if (File.Exists(File3SourcePath))
        //                        {
        //                            if (!File.Exists(DestinationFil3Path))
        //                                File.Copy(File3SourcePath, DestinationFil3Path);
        //                        }


        //                    }

        //                }
        //                else
        //                {
        //                    if (File.Exists(File3SourcePath))
        //                    {
        //                        if (!File.Exists(DestinationFil3Path))
        //                            File.Copy(File3SourcePath, DestinationFil3Path);
        //                    }

        //                }
        //                item.File3 = "MPC_Content/Products/" + companyRepository.OrganisationId + "/" + item.ItemId + "/" + NewF3Path;
        //            }

        //            // file 4
        //            if (!string.IsNullOrEmpty(item.File4))
        //            {
        //                string OldF4Path = string.Empty;
        //                string NewF4Path = string.Empty;

        //                string name = Path.GetFileName(item.File4);
        //                string[] SplitMain = name.Split('_');
        //                if (SplitMain[0] != string.Empty)
        //                {
        //                    ItemID = SplitMain[0];

        //                }

        //                OldF4Path = Path.GetFileName(item.File4);
        //                NewF4Path = OldF4Path.Replace(ItemID + "_", item.ItemId + "_");

        //                string DestinationFile4Path = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + companyRepository.OrganisationId + "/" + item.ItemId + "/" + NewF4Path);
        //                DestinationsPath.Add(DestinationFile4Path);
        //                string DestinationFile4Directory = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + companyRepository.OrganisationId + "/" + item.ItemId);
        //                string File4SourcePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + companyRepository.OrganisationId + "/" + ItemID + "/" + OldF4Path);
        //                if (!System.IO.Directory.Exists(DestinationFile4Directory))
        //                {
        //                    Directory.CreateDirectory(DestinationFile4Directory);
        //                    if (Directory.Exists(DestinationFile4Directory))
        //                    {
        //                        if (File.Exists(File4SourcePath))
        //                        {
        //                            if (!File.Exists(DestinationFile4Path))
        //                                File.Copy(File4SourcePath, DestinationFile4Path);
        //                        }


        //                    }

        //                }
        //                else
        //                {
        //                    if (File.Exists(File4SourcePath))
        //                    {
        //                        if (!File.Exists(DestinationFile4Path))
        //                            File.Copy(File4SourcePath, DestinationFile4Path);
        //                    }

        //                }
        //                item.File4 = "MPC_Content/Products/" + companyRepository.OrganisationId + "/" + item.ItemId + "/" + NewF4Path;
        //            }

        //            // file 5
        //            if (!string.IsNullOrEmpty(item.File5))
        //            {
        //                string OldF5Path = string.Empty;
        //                string NewF5Path = string.Empty;

        //                string name = Path.GetFileName(item.File5);
        //                string[] SplitMain = name.Split('_');
        //                if (SplitMain[0] != string.Empty)
        //                {
        //                    ItemID = SplitMain[0];

        //                }

        //                OldF5Path = Path.GetFileName(item.File5);
        //                NewF5Path = OldF5Path.Replace(ItemID + "_", item.ItemId + "_");

        //                string DestinationFile5Path = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + companyRepository.OrganisationId + "/" + item.ItemId + "/" + NewF5Path);
        //                DestinationsPath.Add(DestinationFile5Path);
        //                string DestinationFile5Directory = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + companyRepository.OrganisationId + "/" + item.ItemId);
        //                string File5SourcePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Products/" + companyRepository.OrganisationId + "/" + ItemID + "/" + OldF5Path);
        //                if (!System.IO.Directory.Exists(DestinationFile5Directory))
        //                {
        //                    Directory.CreateDirectory(DestinationFile5Directory);
        //                    if (Directory.Exists(DestinationFile5Directory))
        //                    {
        //                        if (File.Exists(File5SourcePath))
        //                        {
        //                            if (!File.Exists(DestinationFile5Path))
        //                                File.Copy(File5SourcePath, DestinationFile5Path);
        //                        }


        //                    }

        //                }
        //                else
        //                {
        //                    if (File.Exists(File5SourcePath))
        //                    {
        //                        if (!File.Exists(DestinationFile5Path))
        //                            File.Copy(File5SourcePath, DestinationFile5Path);
        //                    }

        //                }
        //                item.File5 = "MPC_Content/Products/" + companyRepository.OrganisationId + "/" + item.ItemId + "/" + NewF5Path;
        //            }
        //            if (item.ItemImages != null && item.ItemImages.Count > 0)
        //            {
        //                foreach (var img in item.ItemImages)
        //                {
        //                    if (!string.IsNullOrEmpty(img.ImageURL))
        //                    {
        //                        string OldImagePath = string.Empty;
        //                        string NewImagePath = string.Empty;

        //                        string name = Path.GetFileName(img.ImageURL);

        //                        string DestinationItemImagePath = HttpContext.Current.Server.MapPath("/MPC_Content/Products/" + companyRepository.OrganisationId + "/" + item.ItemId + "/" + name);
        //                        DestinationsPath.Add(DestinationItemImagePath);
        //                        string DestinationItemImageDirectory = HttpContext.Current.Server.MapPath("/MPC_Content/Products/" + companyRepository.OrganisationId + "/" + item.ItemId);
        //                        string ItemImageSourcePath = HttpContext.Current.Server.MapPath("/MPC_Content/Products/" + companyRepository.OrganisationId + "/" + ItemID + "/" + name);
        //                        if (!System.IO.Directory.Exists(DestinationItemImageDirectory))
        //                        {
        //                            Directory.CreateDirectory(DestinationItemImageDirectory);
        //                            if (Directory.Exists(DestinationItemImageDirectory))
        //                            {
        //                                if (File.Exists(ItemImageSourcePath))
        //                                {
        //                                    if (!File.Exists(DestinationItemImagePath))
        //                                        File.Copy(ItemImageSourcePath, DestinationItemImagePath);
        //                                }


        //                            }

        //                        }
        //                        else
        //                        {
        //                            if (File.Exists(ItemImageSourcePath))
        //                            {
        //                                if (!File.Exists(DestinationItemImagePath))
        //                                    File.Copy(ItemImageSourcePath, DestinationItemImagePath);
        //                            }

        //                        }
        //                        img.ImageURL = "MPC_Content/Products/" + companyRepository.OrganisationId + "/" + item.ItemId + "/" + name;
        //                        // item.ThumbnailPath = "MPC_Content/Products/" + ImportIDs.NewOrganisationID + "/" + item.ItemId + "/" + NewThumbnailPath;
        //                    }
        //                }
        //            }
        //            //if (item.TemplateId != null && item.TemplateId > 0)
        //            //{
        //            //    if (item.DesignerCategoryId == 0 || item.DesignerCategoryId == null)
        //            //    {
        //            //        if (item.Template != null)
        //            //        {

        //            //            // template background images
        //            //            if (item.Template.TemplateBackgroundImages != null && item.Template.TemplateBackgroundImages.Count > 0)
        //            //            {
        //            //                foreach (var tempImg in item.Template.TemplateBackgroundImages)
        //            //                {
        //            //                    if (!string.IsNullOrEmpty(tempImg.ImageName))
        //            //                    {
        //            //                        if (tempImg.ImageName.Contains("UserImgs/"))
        //            //                        {
        //            //                            string name = tempImg.ImageName;

        //            //                            string ImageName = Path.GetFileName(tempImg.ImageName);

        //            //                            string NewPath = "UserImgs/" + oCID + "/" + ImageName;

        //            //                            string[] tempID = tempImg.ImageName.Split('/');

        //            //                            string OldTempID = tempID[1];

        //            //                            string DestinationTempBackGroundImages = HttpContext.Current.Server.MapPath("/MPC_Content/Designer/Organisation" + NewOrgID + "/Templates/" + NewPath);
        //            //                            DestinationsPath.Add(DestinationTempBackGroundImages);
        //            //                            string DestinationTempBackgroundDirectory = HttpContext.Current.Server.MapPath("/MPC_Content/Designer/Organisation" + NewOrgID + "/Templates/UserImgs/" + oCID);
        //            //                            string FileBackGroundSourcePath = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Designer/Organisation" + oldOrgID + "/Templates/UserImgs/" + OldCompanyID + "/" + ImageName);
        //            //                            if (!System.IO.Directory.Exists(DestinationTempBackgroundDirectory))
        //            //                            {
        //            //                                Directory.CreateDirectory(DestinationTempBackgroundDirectory);
        //            //                                if (Directory.Exists(DestinationTempBackgroundDirectory))
        //            //                                {
        //            //                                    if (File.Exists(FileBackGroundSourcePath))
        //            //                                    {
        //            //                                        if (!File.Exists(DestinationTempBackGroundImages))
        //            //                                            File.Copy(FileBackGroundSourcePath, DestinationTempBackGroundImages);
        //            //                                    }


        //            //                                }
        //            //                            }
        //            //                            else
        //            //                            {
        //            //                                if (File.Exists(FileBackGroundSourcePath))
        //            //                                {
        //            //                                    if (!File.Exists(DestinationTempBackGroundImages))
        //            //                                        File.Copy(FileBackGroundSourcePath, DestinationTempBackGroundImages);
        //            //                                }

        //            //                            }
        //            //                            tempImg.ImageName = NewPath;
        //            //                        }
        //            //                        else
        //            //                        {
        //            //                            string name = tempImg.ImageName;

        //            //                            string ImageName = Path.GetFileName(tempImg.ImageName);

        //            //                            string NewPath = tempImg.ProductId + "/" + ImageName;

        //            //                            string[] tempID = tempImg.ImageName.Split('/');

        //            //                            string OldTempID = tempID[0];


        //            //                            string DestinationTempBackGroundImages = HttpContext.Current.Server.MapPath("/MPC_Content/Designer/Organisation" + NewOrgID + "/Templates/" + NewPath);
        //            //                            DestinationsPath.Add(DestinationTempBackGroundImages);
        //            //                            string DestinationTempBackgroundDirectory = HttpContext.Current.Server.MapPath("/MPC_Content/Designer/Organisation" + NewOrgID + "/Templates/" + tempImg.ProductId);
        //            //                            string FileBackGroundSourcePath = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Designer/Organisation" + oldOrgID + "/Templates/" + OldTempID + "/" + ImageName);
        //            //                            if (!System.IO.Directory.Exists(DestinationTempBackgroundDirectory))
        //            //                            {
        //            //                                Directory.CreateDirectory(DestinationTempBackgroundDirectory);
        //            //                                if (Directory.Exists(DestinationTempBackgroundDirectory))
        //            //                                {
        //            //                                    if (File.Exists(FileBackGroundSourcePath))
        //            //                                    {
        //            //                                        if (!File.Exists(DestinationTempBackGroundImages))
        //            //                                            File.Copy(FileBackGroundSourcePath, DestinationTempBackGroundImages);
        //            //                                    }


        //            //                                }
        //            //                            }
        //            //                            else
        //            //                            {
        //            //                                if (File.Exists(FileBackGroundSourcePath))
        //            //                                {
        //            //                                    if (!File.Exists(DestinationTempBackGroundImages))
        //            //                                        File.Copy(FileBackGroundSourcePath, DestinationTempBackGroundImages);
        //            //                                }

        //            //                            }
        //            //                            tempImg.ImageName = NewPath;
        //            //                        }



        //            //                    }

        //            //                }
        //            //            }
        //            //            if (item.Template.TemplatePages != null && item.Template.TemplatePages.Count > 0)
        //            //            {
        //            //                foreach (var tempPage in item.Template.TemplatePages)
        //            //                {
        //            //                    if (!string.IsNullOrEmpty(tempPage.BackgroundFileName))
        //            //                    {
        //            //                        string name = tempPage.BackgroundFileName;

        //            //                        string FileName = Path.GetFileName(tempPage.BackgroundFileName);

        //            //                        string NewPath = tempPage.ProductId + "/" + FileName;

        //            //                        string[] tempID = tempPage.BackgroundFileName.Split('/');

        //            //                        string OldTempID = tempID[0];


        //            //                        string DestinationTempBackGroundImages = HttpContext.Current.Server.MapPath("/MPC_Content/Designer/Organisation" + NewOrgID + "/Templates/" + NewPath);
        //            //                        DestinationsPath.Add(DestinationTempBackGroundImages);
        //            //                        string DestinationTempBackgroundDirectory = HttpContext.Current.Server.MapPath("/MPC_Content/Designer/Organisation" + NewOrgID + "/Templates/" + tempPage.ProductId);
        //            //                        string FileBackGroundSourcePath = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Designer/Organisation" + oldOrgID + "/Templates/" + OldTempID + "/" + FileName);
        //            //                        if (!System.IO.Directory.Exists(DestinationTempBackgroundDirectory))
        //            //                        {
        //            //                            Directory.CreateDirectory(DestinationTempBackgroundDirectory);
        //            //                            if (Directory.Exists(DestinationTempBackgroundDirectory))
        //            //                            {
        //            //                                if (File.Exists(FileBackGroundSourcePath))
        //            //                                {
        //            //                                    if (!File.Exists(DestinationTempBackGroundImages))
        //            //                                        File.Copy(FileBackGroundSourcePath, DestinationTempBackGroundImages);
        //            //                                }


        //            //                            }

        //            //                        }
        //            //                        else
        //            //                        {
        //            //                            if (File.Exists(FileBackGroundSourcePath))
        //            //                            {
        //            //                                if (!File.Exists(DestinationTempBackGroundImages))
        //            //                                    File.Copy(FileBackGroundSourcePath, DestinationTempBackGroundImages);
        //            //                            }

        //            //                        }
        //            //                        tempPage.BackgroundFileName = NewPath;
        //            //                    }
        //            //                    string fileName = "templatImgBk" + tempPage.PageNo + ".jpg";
        //            //                    string sPath = "/MPC_Content/Designer/Organisation" + NewOrgID + "/Templates/" + tempPage.ProductId + "/" + fileName;
        //            //                    string FilePaths = HttpContext.Current.Server.MapPath("~/" + sPath);


        //            //                    string DestinationDirectory = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + NewOrgID + "/Templates/" + tempPage.ProductId);
        //            //                    string SourcePath = HttpContext.Current.Server.MapPath("/MPC_Content/Artworks/ImportOrganisation/Designer/Organisation" + NewOrgID + "/Templates/" + tempPage.ProductId + "/" + fileName);
        //            //                    string DestinationPath = HttpContext.Current.Server.MapPath("/MPC_Content/Designer/Organisation" + NewOrgID + "/Templates/" + tempPage.ProductId + "/" + fileName);
        //            //                    if (!System.IO.Directory.Exists(DestinationDirectory))
        //            //                    {
        //            //                        Directory.CreateDirectory(DestinationDirectory);
        //            //                        if (Directory.Exists(DestinationDirectory))
        //            //                        {
        //            //                            if (File.Exists(SourcePath))
        //            //                            {
        //            //                                if (!File.Exists(DestinationPath))
        //            //                                    File.Copy(SourcePath, DestinationPath);
        //            //                            }


        //            //                        }

        //            //                    }
        //            //                    else
        //            //                    {
        //            //                        if (File.Exists(SourcePath))
        //            //                        {
        //            //                            if (!File.Exists(DestinationPath))
        //            //                                File.Copy(SourcePath, DestinationPath);
        //            //                        }

        //            //                    }


        //            //                }
        //            //            }



        //            //        }

        //            //    }

        //            //}

        //        }
        //    }

        //    List<TemplateFont> otemplatefonts = templatefonts.getTemplateFontsByCompanyID(ObjCompany.CompanyId);
        //    if (otemplatefonts != null && otemplatefonts.Count > 0)
        //    {
        //        foreach (var fonts in otemplatefonts)
        //        {
        //            string DestinationFontDirectory = string.Empty;
        //            string companyoid = string.Empty;
        //            string FontSourcePath = string.Empty;
        //            string FontSourcePath1 = string.Empty;
        //            string FontSourcePath2 = string.Empty;
        //            string NewFilePath = string.Empty;
        //            string DestinationFont1 = string.Empty;

        //            string DestinationFont2 = string.Empty;

        //            string DestinationFont3 = string.Empty;
        //            if (!string.IsNullOrEmpty(fonts.FontPath))
        //            {

        //                string NewPath = "Organisation" + companyRepository.OrganisationId + "/WebFonts/" + fonts.CustomerId;


        //                DestinationFont1 = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/" + NewPath + fonts.FontFile + ".eot");

        //                DestinationFont2 = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/" + NewPath + fonts.FontFile + ".ttf");

        //                DestinationFont3 = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/" + NewPath + fonts.FontFile + ".woff");

        //                DestinationFontDirectory = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/" + NewPath);

        //                FontSourcePath = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/" + companyRepository.OrganisationId + "/WebFonts/" + fonts.FontPath + fonts.FontFile + ".eot");

        //                FontSourcePath1 = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/" + companyRepository.OrganisationId + "/WebFonts/" + fonts.FontPath + fonts.FontFile + ".ttf");

        //                FontSourcePath2 = HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/" + companyRepository.OrganisationId + "/WebFonts/" + fonts.FontPath + fonts.FontFile + ".woff");

        //                if (!System.IO.Directory.Exists(DestinationFontDirectory))
        //                {
        //                    Directory.CreateDirectory(DestinationFontDirectory);
        //                    if (Directory.Exists(DestinationFontDirectory))
        //                    {
        //                        if (File.Exists(FontSourcePath))
        //                        {
        //                            if (!File.Exists(DestinationFont1))
        //                                File.Copy(FontSourcePath, DestinationFont1);
        //                        }

        //                        if (File.Exists(FontSourcePath1))
        //                        {
        //                            if (!File.Exists(DestinationFont2))
        //                                File.Copy(FontSourcePath1, DestinationFont2);

        //                        }

        //                        if (File.Exists(FontSourcePath2))
        //                        {
        //                            if (!File.Exists(DestinationFont3))
        //                                File.Copy(FontSourcePath2, DestinationFont3);

        //                        }

        //                    }

        //                }
        //                else
        //                {
        //                    if (File.Exists(FontSourcePath))
        //                    {
        //                        if (!File.Exists(DestinationFont1))
        //                            File.Copy(FontSourcePath, DestinationFont1);
        //                    }

        //                    if (File.Exists(FontSourcePath1))
        //                    {
        //                        if (!File.Exists(DestinationFont2))
        //                            File.Copy(FontSourcePath1, DestinationFont2);

        //                    }

        //                    if (File.Exists(FontSourcePath2))
        //                    {
        //                        if (!File.Exists(DestinationFont3))
        //                            File.Copy(FontSourcePath2, DestinationFont3);

        //                    }

        //                }
        //                fonts.FontPath = NewPath;
        //            }


        //            DestinationsPath.Add(DestinationFont1);
        //            DestinationsPath.Add(DestinationFont2);
        //            DestinationsPath.Add(DestinationFont3);



        //        }
        //    }
        //    // site.css
        //    string DestinationSiteFile = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + oCID + "/Site.css");
        //    DestinationsPath.Add(DestinationSiteFile);
        //    string DestinationSiteFileDirectory = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + oCID);
        //    string SourceSiteFile = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + OldCompanyID + "/Site.css");
        //    if (!System.IO.Directory.Exists(DestinationSiteFileDirectory))
        //    {
        //        Directory.CreateDirectory(DestinationSiteFileDirectory);
        //        if (Directory.Exists(DestinationSiteFileDirectory))
        //        {
        //            if (File.Exists(SourceSiteFile))
        //            {
        //                if (!File.Exists(DestinationSiteFile))
        //                    File.Copy(SourceSiteFile, DestinationSiteFile);
        //            }


        //        }


        //    }
        //    else
        //    {
        //        if (File.Exists(SourceSiteFile))
        //        {
        //            if (!File.Exists(DestinationSiteFile))
        //                File.Copy(SourceSiteFile, DestinationSiteFile);
        //        }

        //    }

        //    // sprite.png
        //    string DestinationSpriteFile = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + oCID + "/Sprite.png");
        //    DestinationsPath.Add(DestinationSpriteFile);
        //    string DestinationSpriteDirectory = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + oCID);
        //    string SourceSpriteFile = HttpContext.Current.Server.MapPath("~/MPC_Content/Assets/" + companyRepository.OrganisationId + "/" + OldCompanyID + "/Sprite.png");
        //    if (!System.IO.Directory.Exists(DestinationSpriteDirectory))
        //    {
        //        Directory.CreateDirectory(DestinationSpriteDirectory);
        //        if (Directory.Exists(DestinationSpriteDirectory))
        //        {
        //            if (File.Exists(SourceSiteFile))
        //            {
        //                if (!File.Exists(DestinationSpriteFile))
        //                    File.Copy(SourceSpriteFile, DestinationSpriteFile);
        //            }

        //        }
        //        else
        //        {
        //            if (File.Exists(SourceSpriteFile))
        //            {
        //                if (!File.Exists(DestinationSpriteFile))
        //                    File.Copy(SourceSpriteFile, DestinationSpriteFile);
        //            }

        //        }


        //    }
        //    else
        //    {
        //        if (File.Exists(SourceSpriteFile))
        //        {
        //            if (!File.Exists(DestinationSpriteFile))
        //                File.Copy(SourceSpriteFile, DestinationSpriteFile);
        //        }
        //    }
        //}


        //#endregion  


        #region import store from zip






        #endregion
    }
}
