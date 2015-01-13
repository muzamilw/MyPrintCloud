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

        bool DeleteTemplate(long ProductID, out long CategoryID, long organizationID);
        bool DeleteTemplateFiles(long ProductID, long organizationID);
        long CopyTemplate(long ProductID, long SubmittedBy, string SubmittedByName, long organizationID);
        List<long?> CopyTemplateList(List<long?> productIDList, long SubmittedBy, string SubmittedByName, long organizationID);
        bool generateTemplateFromPDF(string filePhysicalPath, int mode, long templateID, long organizationID);
        void processTemplatePDF(long TemplateID, long organizationID, bool printCropMarks, bool printWaterMarks, bool isroundCorners);
        long CloneTemplateByTemplateID(long TempID);

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
