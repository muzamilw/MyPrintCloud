using MPC.Models.Common;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.WebStoreServices
{
    public interface ITemplateService
    {
        Template GetTemplate(long productID);

        Template GetTemplateInDesigner(long productID);

        List<MatchingSets> BindTemplatesList(string TemplateName, int pageNumber, long CustomerID, int CompanyID);
        string GetTemplateNameByTemplateID(long tempID);

        bool DeleteTemplate(long ProductID, out long CategoryID, long OrganisationID);
        bool DeleteTemplateFiles(long ProductID, long OrganisationID);
        long CopyTemplate(long ProductID, long SubmittedBy, string SubmittedByName, long OrganisationID);
        List<long?> CopyTemplateList(List<long?> productIDList, long SubmittedBy, string SubmittedByName, long OrganisationID);
        bool generateTemplateFromPDF(string filePhysicalPath, int mode, long templateID, long OrganisationID);
        void processTemplatePDF(long TemplateID, long OrganisationID, bool printCropMarks, bool printWaterMarks, bool isroundCorners);
        long CloneTemplateByTemplateID(long TempID);
        void regeneratePDFs(long productID,long OrganisationID, bool printCuttingMargins);
        long MergeRetailTemplate(int RemoteTemplateID, long LocalTempalteID, long organisationId);
        string SaveTemplate(List<TemplateObject> lstTemplatesObjects, List<TemplatePage> lstTemplatePages, long organisationID, bool printCropMarks, bool printWaterMarks, bool isRoundCorners);
        long SaveTemplateLocally(Template oTemplate, List<TemplatePage> oTemplatePages, List<TemplateObject> oTemplateObjects, List<TemplateBackgroundImage> oTemplateImages, List<TemplateFont> oTemplateFonts, string RemoteUrlBasePath, string BasePath,  long organisationID,int mode, long localTemplateID);
        /// <summary>
        /// To populate the template information base on template id and item rec by zohaib 10/1/2015
        /// </summary>
        /// <param name="templateID"></param>
        /// <param name="ItemRecc"></param>
        /// <param name="template"></param>
        /// <param name="tempPages"></param>
        void populateTemplateInfo(long templateID, Item ItemRecc, out Template template, out List<TemplatePage> tempPages);
    }
}
