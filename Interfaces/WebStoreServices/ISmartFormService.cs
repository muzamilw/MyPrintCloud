using MPC.Common;
using MPC.Models.DomainModels;
using MPC.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Interfaces.WebStoreServices
{
    public interface ISmartFormService
    {
        List<VariableList> GetVariablesData(bool isRealestateproduct, long companyId, long organisationId);
        List<TemplateVariablesObj> GetTemplateVariables(long templateId);
        bool SaveTemplateVariables(List<TemplateVariablesObj> obj);
        List<SmartFormUserList> GetUsersList(long contactId);

        SmartFormWebstoreResponse GetSmartForm(long smartFormId);

        List<SmartFormDetail> GetSmartFormObjects(long smartFormId,out List<VariableOption> listVariables);

        List<ScopeVariable> GetScopeVariables(List<SmartFormDetail> smartFormDetails, out bool hasContactVariables, long contactId);

        Dictionary<long, List<ScopeVariable>> GetUserScopeVariables(List<SmartFormDetail> smartFormDetails, List<SmartFormUserList> contacts, long templateId);

        string SaveUserProfilesData(Dictionary<long, List<ScopeVariable>> obj);
        string[] GetContactImageAndCompanyLogo(long contactID);

        List<ScopeVariable> GetUserTemplateVariables(long itemId, long contactID);
        List<ScopeVariable> GetTemplateScopeVariables(long templateID, long contactId);
        bool AutoResolveTemplateVariables(long itemID, long contactId);

        List<VariableExtensionWebstoreResposne> getVariableExtensions(List<ScopeVariable> listScope, long contactId);

    }
}
