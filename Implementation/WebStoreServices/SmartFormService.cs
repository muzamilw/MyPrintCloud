using MPC.Common;
using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.DomainModels;
using MPC.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Implementation.WebStoreServices
{
    class SmartFormService : ISmartFormService
    {
        public readonly ISmartFormRepository _smartFormRepository;
         #region constructor
        public SmartFormService(ISmartFormRepository smartFormRepository)
        {
            this._smartFormRepository = smartFormRepository;
 
        }
        #endregion

        #region public
        public List<VariableList> GetVariablesData(bool isRealestateproduct, long companyId, long organisationId)
        {
            return _smartFormRepository.GetVariablesData(isRealestateproduct, companyId, organisationId);
        }
        public List<TemplateVariablesObj> GetTemplateVariables(long templateId)
        {
            return _smartFormRepository.GetTemplateVariables(templateId);
        }

        public bool SaveTemplateVariables(List<TemplateVariablesObj> obj)
        {
            return _smartFormRepository.SaveTemplateVariables(obj);
        }
        public List<SmartFormUserList> GetUsersList(long contactId)
        {
            return _smartFormRepository.GetUsersList(contactId);
        }
        public SmartFormWebstoreResponse GetSmartForm(long smartFormId)
        {
            return _smartFormRepository.GetSmartForm(smartFormId);
        }

        public List<SmartFormDetail> GetSmartFormObjects(long smartFormId, out List<VariableOption> listVariables)
        {
            return _smartFormRepository.GetSmartFormObjects(smartFormId, out listVariables);
        }
        public List<ScopeVariable> GetScopeVariables(List<SmartFormDetail> smartFormDetails, out bool hasContactVariables, long contactId)
        {
            return _smartFormRepository.GetScopeVariables(smartFormDetails,out hasContactVariables,contactId);
        }
        public Dictionary<long, List<ScopeVariable>> GetUserScopeVariables(List<SmartFormDetail> smartFormDetails, List<SmartFormUserList> contacts, long templateId)
        {
            return _smartFormRepository.GetUserScopeVariables(smartFormDetails, contacts, templateId);
        }
        public string SaveUserProfilesData(Dictionary<long, List<ScopeVariable>> obj)
        {
            string result = "true";
            try
            {
                 _smartFormRepository.SaveUserProfilesData(obj);
            }
            catch(Exception ex)
            {
                result = ex.ToString();
                //throw ex;
            }
            return result;
           
        }
        public string[] GetContactImageAndCompanyLogo(long contactID)
        {
           return _smartFormRepository.GetContactImageAndCompanyLogo(contactID);

        }
        public List<ScopeVariable> GetUserTemplateVariables(long itemId, long contactID)
        {
            return _smartFormRepository.GetUserTemplateVariables(itemId, contactID);
        }
        public List<ScopeVariable> GetTemplateScopeVariables(long templateID, long contactId)
        {
            return _smartFormRepository.GetTemplateScopeVariables(templateID, contactId);
        }

        public bool AutoResolveTemplateVariables(long itemID, long contactId)
        {
            return _smartFormRepository.AutoResolveTemplateVariables(itemID, contactId);
        }
        public List<VariableExtensionWebstoreResposne> getVariableExtensions(List<ScopeVariable> listScope, long contactId)
        {
            return _smartFormRepository.getVariableExtensions(listScope, contactId);
        }
        #endregion
    }
}
