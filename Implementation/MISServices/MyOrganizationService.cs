using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Linq;
using MPC.ExceptionHandling;
using MPC.Interfaces.MISServices;
using System.Collections.Generic;
using MPC.Interfaces.Repository;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.ResponseModels;
using System.Resources;
using System.Web;


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
        private readonly IOrganisationFileTableViewRepository mpcFileTableViewRepository;
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
         IChartOfAccountRepository chartOfAccountRepository, IOrganisationFileTableViewRepository mpcFileTableViewRepository,
            ICountryRepository countryRepository, IStateRepository stateRepository, IPrefixRepository prefixRepository,
           ICurrencyRepository currencyRepository, IWeightUnitRepository weightUnitRepository, ILengthUnitRepository lengthUnitRepository,
            IGlobalLanguageRepository globalLanguageRepository)
        {
            if (mpcFileTableViewRepository == null)
            {
                throw new ArgumentNullException("mpcFileTableViewRepository");
            }
            this.organisationRepository = organisationRepository;
            this.markupRepository = markupRepository;
            this.chartOfAccountRepository = chartOfAccountRepository;
            this.mpcFileTableViewRepository = mpcFileTableViewRepository;
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

            if (organization.MISLogoStreamId.HasValue)
            {
                MpcFileTableView fileTableView = mpcFileTableViewRepository.GetByStreamId(organization.MISLogoStreamId.Value);
                if (fileTableView != null)
                {
                    organization.MisLogoBytes = fileTableView.FileStream;
                }
            }

            IEnumerable<Markup> markups = markupRepository.GetAll();
            if (markups != null)
            {
                organization.MarkupId = markupRepository.GetAll().First(x => x.IsDefault != null).MarkUpId;
            }
            return organization;
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

        /// <summary>
        /// Add/Update Lanuage Resource File
        /// </summary>
        /// <param name="organisation"></param>
        private void UpdateLanguageResource(Organisation organisation)
        {

            string directoryPath = System.Web.Hosting.HostingEnvironment.MapPath("~/MPC_Content/Resources/Organisation" + organisation.OrganisationId);
            if (directoryPath != null && Directory.Exists(directoryPath))
            {
                //Write the combined resource file
                //ResourceWriter resourceWriter = new ResourceWriter(directoryPath + "/en-US/LanguageResource.resx");

                //foreach (String key in resourceEntries.Keys)
                //{
                //    resourceWriter.AddResource(key, resourceEntries[key]);
                //}
                // resourceWriter.AddResource("abc", "test21");
                // resourceWriter.Generate();
                //resourceWriter.Close();

                //resourceWriter.AddResource("myString", "test");
               // resourceWriter.Close();

                string sResxPath = directoryPath + "\\en-US\\LanguageResource.resx";

                Hashtable data = new Hashtable();
                data.Add("name", "sunil");
                UpdateResourceFile(data, sResxPath);


            }
        }
        public static void UpdateResourceFile(Hashtable data, String path)
        {
            Hashtable resourceEntries = new Hashtable();
            //string FILE_NAME = PATH + "Resource." + aCultureID + ".resx";

            FileStream cultureFile = File.Open(path, FileMode.Open);

            try
            {
                IResourceReader reader1 = new ResourceReader(cultureFile); // <<< Exception!

                IDictionaryEnumerator cultureInfo = reader1.GetEnumerator();

                while (cultureInfo.MoveNext())
                {
                    //MessageBox.Show("<br>");
                    //MessageBox.Show("Name: " + cultureInfo.Key.ToString());
                    //MessageBox.Show("Value: " + cultureInfo.Value.ToString());
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(METHOD + "unexpected exception" + ex);
                throw ex;
            }
           //ResXResourceReader resXResource = new ResXResourceReader();
            var fs = new System.IO.FileStream(path,
                                  System.IO.FileMode.Open);
            //Get existing resources
            ResourceReader reader = new ResourceReader(fs);
                 if (reader != null)
            {
                IDictionaryEnumerator id = reader.GetEnumerator();
                foreach (DictionaryEntry d in reader)
                {
                    if (d.Value == null)
                        resourceEntries.Add(d.Key.ToString(), "");
                    else
                        resourceEntries.Add(d.Key.ToString(), d.Value.ToString());
                } reader.Close();
            }
            //Modify resources here...
            foreach (String key in data.Keys)
            {
                if (!resourceEntries.ContainsKey(key))
                {
                    String value = data[key].ToString();
                    if (value == null)
                        value = "";
                    resourceEntries.Add(key, value);
                }
                else
                {
                    String value = data[key].ToString();
                    if (value == null)
                        value = "";
                    resourceEntries.Remove(key);
                    resourceEntries.Add(key, data[key].ToString());
                }
            }
            //Write the combined resource file
            ResourceWriter resourceWriter = new ResourceWriter(path);
            foreach (String key in resourceEntries.Keys)
            {
                resourceWriter.AddResource(key, resourceEntries[key]);
            }
            resourceWriter.Generate();
            resourceWriter.Close();
        }
        #endregion

    }
}
