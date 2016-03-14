using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.Common;
using MPC.Models.DomainModels;

namespace MPC.Implementation.MISServices
{
    public class CompanyTerritoryService : ICompanyTerritoryService
    {
        #region Private
        private readonly ICompanyTerritoryRepository companyTerritoryRepository;
        private readonly IScopeVariableRepository scopeVariableRepository;
        private readonly ICompanyContactRepository companyContactRepository;
        private readonly IAddressRepository addressRepository;
        private readonly ITemplateColorStylesRepository cmykColorRepository;
        private readonly ITemplateFontsRepository _templateFontsRepository;
        //#region Private Methods
        private CompanyTerritory Create(CompanyTerritory companyTerritory)
        {
            //check to maintain one companyTerritory as default in db
            CheckCompanyTerritoryDefualt(companyTerritory);
            companyTerritoryRepository.Add(companyTerritory);
            companyTerritoryRepository.SaveChanges();

            if (companyTerritory.ScopeVariables != null)
            {
                foreach (ScopeVariable scopeVariable in companyTerritory.ScopeVariables)
                {
                    scopeVariable.Id = companyTerritory.TerritoryId;
                    scopeVariableRepository.Add(scopeVariable);
                }
                scopeVariableRepository.SaveChanges();
            }
            if (companyTerritory.TerritorySpotColors != null)
            {
                foreach (TemplateColorStyle spotColor in companyTerritory.TerritorySpotColors)
                {
                    spotColor.TerritoryId = companyTerritory.TerritoryId;
                    cmykColorRepository.Add(spotColor);
                }
                cmykColorRepository.SaveChanges();
            }
            return companyTerritory;
        }

        private CompanyTerritory Update(CompanyTerritory companyTerritory)
        {
            //check to maintain one companyTerritory as default in db
            CheckCompanyTerritoryDefualt(companyTerritory);
            companyTerritoryRepository.Update(companyTerritory);
            if (companyTerritory.ScopeVariables != null)
            {
                UpdateScopVariables(companyTerritory);
            }
            if (companyTerritory.TerritorySpotColors != null)
            {
                UpdateTerritorySpotColors(companyTerritory);
                
            }
            if (companyTerritory.TerritoryFonts != null)
            {
                UpdateTerritoryTemplateFonts(companyTerritory);

            }
            companyTerritoryRepository.SaveChanges();

            return companyTerritory;
        }
        /// <summary>
        /// Update Scope Variables
        /// </summary>
        private void UpdateScopVariables(CompanyTerritory companyTerritory)
        {
            IEnumerable<ScopeVariable> scopeVariables = scopeVariableRepository.GetContactVariableByContactId(companyTerritory.TerritoryId, (int)FieldVariableScopeType.Territory);
            foreach (ScopeVariable scopeVariable in companyTerritory.ScopeVariables)
            {
                ScopeVariable scopeVariableDbItem = scopeVariables.FirstOrDefault(
                    scv => scv.ScopeVariableId == scopeVariable.ScopeVariableId);
                if (scopeVariableDbItem != null)
                {
                    scopeVariableDbItem.Value = scopeVariable.Value;
                }
            }
        }

        private void UpdateTerritorySpotColors(CompanyTerritory territory)
        {
            List<TemplateColorStyle> allTerritoryColors =
                cmykColorRepository.GetAll().Where(c => c.TerritoryId == territory.TerritoryId).ToList();
            foreach (TemplateColorStyle spotColor in territory.TerritorySpotColors)
            {
                if (spotColor.PelleteId <= 0)
                {
                    cmykColorRepository.Add(spotColor); 
                }
                else
                {
                    var spotColorDb = cmykColorRepository.Find(spotColor.PelleteId);
                    if (spotColorDb != null)
                    {
                        spotColorDb.ColorC = spotColor.ColorC;
                        spotColorDb.ColorM = spotColor.ColorM;
                        spotColorDb.ColorY = spotColor.ColorY;
                        spotColorDb.ColorK = spotColor.ColorK;
                        spotColorDb.SpotColor = spotColor.SpotColor;
                        spotColorDb.Name = spotColor.SpotColor;
                        spotColorDb.CustomerId = spotColor.CustomerId;
                    }
                }
            }
            List<TemplateColorStyle> linesToBeRemoved =allTerritoryColors.Where(
               vdp => !IsNewColor(vdp) && territory.TerritorySpotColors.All(sourceVdp => sourceVdp.PelleteId != vdp.PelleteId))
                 .ToList();
            linesToBeRemoved.ForEach(line => cmykColorRepository.Delete(line));
            cmykColorRepository.SaveChanges();
        }
        private void UpdateTerritoryTemplateFonts(CompanyTerritory territory)
        {
            var allTerritoryFonts = _templateFontsRepository.GetTemplateFontsByTerritory(territory.TerritoryId).ToList();
            foreach (TemplateFont font in territory.TerritoryFonts)
            {
                if (font.ProductFontId <= 0)
                {
                    font.IsPrivateFont = true;
                    if (font.FontPath.Length > 0)
                    {
                       font.FontFile = CopyFontFiles(font);
                    }
                    _templateFontsRepository.Add(font);
                }
                else
                {
                    var templateFont = _templateFontsRepository.Find(font.ProductFontId);
                    if (templateFont != null)
                    {
                        templateFont.FontName = font.FontName;
                        templateFont.FontDisplayName = font.FontDisplayName;
                        templateFont.IsEnable = font.IsEnable;
                        templateFont.IsPrivateFont = font.IsPrivateFont;
                        templateFont.CustomerId = font.CustomerId;
                        templateFont.TerritoryId = font.TerritoryId;
                    }
                }
            }
            List<TemplateFont> linesToBeRemoved = allTerritoryFonts.Where(
               vdp => !IsNewFont(vdp) && territory.TerritoryFonts.All(sourceVdp => sourceVdp.ProductFontId != vdp.ProductFontId))
                 .ToList();
            
            foreach (var templateFont in linesToBeRemoved)
            {
                DeleteFontFiles(templateFont);
            }
            linesToBeRemoved.ForEach(line => _templateFontsRepository.Delete(line));
            _templateFontsRepository.SaveChanges();
        }
        private void CheckCompanyTerritoryDefualt(CompanyTerritory companyTerritory)
        {
            if (companyTerritory.isDefault != null && companyTerritory.isDefault == true)
            {
                var companyTerritoriesToUpdate = companyTerritoryRepository.GetAll().Where(x => x.isDefault == true && x.CompanyId == companyTerritory.CompanyId);
                foreach (var territory in companyTerritoriesToUpdate)
                {
                    territory.isDefault = false;
                    companyTerritoryRepository.Update(territory);
                }
            }
        }
        #endregion
        #region Constructor

        public CompanyTerritoryService(ICompanyTerritoryRepository companyTerritoryRepository, IScopeVariableRepository scopeVariableRepository,
            ICompanyContactRepository companyContactRepository, IAddressRepository addressRepository, ITemplateColorStylesRepository cmykColorRepository,
            ITemplateFontsRepository templateFontsRepository)
        {
            if (companyContactRepository == null)
            {
                throw new ArgumentNullException("companyContactRepository");
            }
            if (addressRepository == null)
            {
                throw new ArgumentNullException("addressRepository");
            }
            if (cmykColorRepository == null)
            {
                throw new ArgumentNullException("cmykColorRepository");
            }
            if (templateFontsRepository == null)
            {
                throw new ArgumentNullException("templateFontsRepository");
            }
            this.companyTerritoryRepository = companyTerritoryRepository;
            this.scopeVariableRepository = scopeVariableRepository;
            this.companyContactRepository = companyContactRepository;
            this.addressRepository = addressRepository;
            this.cmykColorRepository = cmykColorRepository;
            this._templateFontsRepository = templateFontsRepository;
        }
        #endregion
        public CompanyTerritory Save(CompanyTerritory companyTerritory)
        {
            if (companyTerritory.TerritoryId == 0)
            {
                return Create(companyTerritory);
            }
            return Update(companyTerritory);
        }

        public bool Delete(long companyTerritoryId)
        {
            var dbCompanyTerritory = companyTerritoryRepository.GetTerritoryById(companyTerritoryId);
            // Only Delete Territory if all of its referencing contacts and addresses have been archived
            // Before Deleting Territory delete them as well
            if (dbCompanyTerritory != null && (dbCompanyTerritory.Addresses == null || dbCompanyTerritory.Addresses.All(address => address.isArchived == true)) && 
                (dbCompanyTerritory.CompanyContacts == null || dbCompanyTerritory.CompanyContacts.All(contact => contact.isArchived == true)))
            {
                // Remove Archived Contacts
                if (dbCompanyTerritory.CompanyContacts != null)
                {
                    List<CompanyContact> companyContacts = dbCompanyTerritory.CompanyContacts.Where(contact => contact.isArchived == true).ToList();
                    companyContacts.ForEach(contact =>
                                            {
                                                dbCompanyTerritory.CompanyContacts.Remove(contact);
                                                companyContactRepository.Delete(contact);
                                            });
                }
                // Remove Archived Addresses 
                if (dbCompanyTerritory.Addresses != null)
                {
                    List<Address> addresses = dbCompanyTerritory.Addresses.Where(contact => contact.isArchived == true).ToList();
                    addresses.ForEach(address =>
                    {
                        dbCompanyTerritory.Addresses.Remove(address);
                        addressRepository.Delete(address);
                    });
                }
                companyTerritoryRepository.Delete(dbCompanyTerritory);
                companyTerritoryRepository.SaveChanges();
                return true;
            }
            return false;
        }
        /// <summary>
        /// Get Company Territory By Id
        /// </summary>
        /// <param name="companyTerritoryId"></param>
        /// <returns></returns>
        public CompanyTerritory Get(long companyTerritoryId)
        {
            if (companyTerritoryId > 0)
            {
                var result = companyTerritoryRepository.GetTerritoryById(companyTerritoryId);
                if (result != null)
                {
                    return result;
                }
                return null;
            }
            return null;
        }
        private static bool IsNewColor(TemplateColorStyle sourceItem)
        {
            return sourceItem.PelleteId <= 0;
        }
        private static bool IsNewFont(TemplateFont sourceItem)
        {
            return sourceItem.ProductFontId <= 0;
        }

        private string CopyFontFiles(TemplateFont font)
        {
            string drUrl = System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + companyTerritoryRepository.OrganisationId + "/WebFonts/" + font.CustomerId);
            if (!System.IO.Directory.Exists(drUrl))
            {
                System.IO.Directory.CreateDirectory(drUrl);

            }
            string sFileNewUrl = drUrl + "\\" + font.TerritoryId + "_" + font.FontFile;
            if (File.Exists(drUrl + "\\" + font.FontFile + ".ttf"))
            {
                byte[] data = File.ReadAllBytes(drUrl + "\\" + font.FontFile + ".ttf");
                File.WriteAllBytes(sFileNewUrl + ".ttf", data);
            }
            if (File.Exists(drUrl + "\\" + font.FontFile + ".eot"))
            {
                byte[] data = File.ReadAllBytes(drUrl + "\\" + font.FontFile + ".eot");
                File.WriteAllBytes(sFileNewUrl + ".eot", data);
            }
            if (File.Exists(drUrl + "\\" + font.FontFile + ".woff"))
            {
                byte[] data = File.ReadAllBytes(drUrl + "\\" + font.FontFile + ".woff");
                File.WriteAllBytes(sFileNewUrl + ".woff", data);
            }
            return font.TerritoryId + "_" + font.FontFile;
        }

        private void DeleteFontFiles(TemplateFont font)
        {
            string drUrl = System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + companyTerritoryRepository.OrganisationId + "/WebFonts/" + font.CustomerId);
            
            if (File.Exists(drUrl + "\\" + font.FontFile + ".ttf"))
            {
                File.Delete(drUrl + "\\" + font.FontFile + ".ttf");
            }
            if (File.Exists(drUrl + "\\" + font.FontFile + ".eot"))
            {
                File.Delete(drUrl + "\\" + font.FontFile + ".eot");
            }
            if (File.Exists(drUrl + "\\" + font.FontFile + ".woff"))
            {
                File.Delete(drUrl + "\\" + font.FontFile + ".woff");
            }
        }
    }
}
