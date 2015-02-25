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
        public List<FieldVariable> GetVariablesData(bool isRealestateproduct, long storeId)
        {
            return _smartFormRepository.GetVariablesData(isRealestateproduct, storeId);
        }
        public Stream GetTemplateVariables(long templateId)
        {
            return _smartFormRepository.GetTemplateVariables(templateId);
        }
        #endregion
    }
}
