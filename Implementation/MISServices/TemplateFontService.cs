using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.ExceptionHandling;
using MPC.Interfaces.MISServices;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;

namespace MPC.Implementation.MISServices
{
    public class TemplateFontService : ITemplateFontService
    {
        private readonly ITemplateFontsRepository _templateFontsRepository;

        public TemplateFontService(ITemplateFontsRepository templateFontsRepository)
        {
            if(templateFontsRepository == null)
                throw new ArgumentNullException("templateFontsRepository");
            this._templateFontsRepository = templateFontsRepository;
        }
        public TemplateFont GetTemplateFontById(long fontId)
        {
            return _templateFontsRepository.GetTemplateFontById(fontId);
        }

        public IEnumerable<TemplateFont> GetTemplateFontsByStore(long companyId)
        {
            return _templateFontsRepository.getTemplateFontsByCompanyID(companyId).Where(c => c.TerritoryId == null).ToList();
        }

        public IEnumerable<TemplateFont> GetTemplateFontsByTerritory(long territoryId)
        {
            return _templateFontsRepository.GetTemplateFontsByTerritory(territoryId);

        }

        public TemplateFont SaveTemplateFont(TemplateFont templateFont)
        {
            try
            {
                TemplateFont targetFont = GetTemplateFontById(templateFont.ProductFontId) ?? CreateNewFont();
                UpdateFont(targetFont, templateFont);
                if (templateFont.TtFFileBytes != null || templateFont.EotFileBytes != null || templateFont.WofFileBytes != null)
                    UploadFontFiles(targetFont, templateFont);
                _templateFontsRepository.SaveChanges();

                return targetFont;
            }
            catch (Exception exp)
            {
                throw new MPCException("Failed to save template font. Error: " + exp.Message, _templateFontsRepository.OrganisationId);
            }
        }

        private TemplateFont CreateNewFont()
        {
            TemplateFont newFont = _templateFontsRepository.Create();
            _templateFontsRepository.Add(newFont);
            return newFont;
        }
        private static void UpdateFont(TemplateFont target, TemplateFont source)
        {
            target.FontName = source.FontName;
            target.FontDisplayName = source.FontName;
            target.IsEnable = source.IsEnable;
            target.IsPrivateFont = true;
            target.CustomerId = source.CustomerId;
            target.TerritoryId = source.TerritoryId;

        }

        private void UploadFontFiles(TemplateFont target, TemplateFont source)
        {
            string drUrl = System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + _templateFontsRepository.OrganisationId + "/WebFonts/" + source.CustomerId);
            if (!System.IO.Directory.Exists(drUrl))
            {
                System.IO.Directory.CreateDirectory(drUrl);
                
            }
            target.FontFile = source.FontFile;
            //int indexOf = drUrl.LastIndexOf("MPC_Content", StringComparison.Ordinal);
            target.FontPath = "Organisation" + _templateFontsRepository.OrganisationId + "/WebFonts/" + source.CustomerId + "/";
            
            if (source.EotFileBytes != null)
            {
                byte[] data = GetBytesFromString(source.EotFileBytes);

                string savePath = drUrl + "\\" + source.FontFile + ".eot";
                File.WriteAllBytes(savePath, data);
            }
            if (source.TtFFileBytes != null)
            {
                byte[] data = GetBytesFromString(source.TtFFileBytes);
                string savePath = drUrl + "\\" + source.FontFile + ".ttf";
                File.WriteAllBytes(savePath, data);
            }
            if (source.WofFileBytes != null)
            {
                byte[] data = GetBytesFromString(source.WofFileBytes);
                string savePath = drUrl + "\\" + source.FontFile + ".woff";
                File.WriteAllBytes(savePath, data);
            }
        }

        private static byte[] GetBytesFromString(string sBytesData)
        {
            string base64 = sBytesData.Substring(sBytesData.IndexOf(',') + 1);
            base64 = base64.Trim('\0');
            byte[] data = Convert.FromBase64String(base64);
            return data;
        }

        public bool DeleteTemplateFont(long fontId)
        {
            try
            {
                TemplateFont dbFont = _templateFontsRepository.GetTemplateFontById(fontId);
                string dirUrl = string.Empty;
                if (dbFont != null)
                {
                    _templateFontsRepository.Delete(dbFont);
                    
                    dirUrl = System.Web.HttpContext.Current.Server.MapPath("~/MPC_Content/Designer/Organisation" + _templateFontsRepository.OrganisationId + "/WebFonts/" + dbFont.CustomerId);

                    string filePath = dirUrl + "\\" + dbFont.FontFile + ".eot"; //delete eot file
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                    filePath = dirUrl + "\\" + dbFont.FontFile + ".ttf"; //delete ttf file
                    if(File.Exists(filePath))
                        File.Delete(filePath);
                    filePath = dirUrl + "\\" + dbFont.FontFile + ".woff"; //delete woff file
                    if(File.Exists(filePath))
                        File.Delete(filePath);
                    _templateFontsRepository.SaveChanges();
                }

            }
            catch (Exception)
            {
                throw new MPCException("Failed to delete template font", _templateFontsRepository.OrganisationId);
            }
            

            return true;
        }
    }
}
