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
        string GetTemplateNameByTemplateID(int tempID);

        bool DeleteTemplate(long ProductID, out long CategoryID, long organizationID);
        bool DeleteTemplateFiles(long ProductID, long organizationID);
        long CopyTemplate(long ProductID, long SubmittedBy, string SubmittedByName, long organizationID);
        List<long?> CopyTemplateList(List<long?> productIDList, long SubmittedBy, string SubmittedByName, long organizationID);
        bool generateTemplateFromPDF(string filePhysicalPath, int mode, long templateID, long CustomerID, long organizationID);
        int CloneTemplateByTemplateID(int TempID);

    }
}
