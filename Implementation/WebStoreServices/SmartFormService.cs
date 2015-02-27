using MPC.Common;
using MPC.Interfaces.Repository;
using MPC.Interfaces.WebStoreServices;
using MPC.Models.DomainModels;
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
        #endregion
    }
}
